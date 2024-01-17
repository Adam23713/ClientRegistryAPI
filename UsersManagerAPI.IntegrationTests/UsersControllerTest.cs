using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using UsersManagerAPI.Models.DTO;
using UsersManagerAPI.Requests;
using MongoDB.Bson;
using System.Net;
using UsersManagerAPI.Models.Domain;
using UsersManagerAPI.Responses;

namespace UsersManagerAPI.IntegrationTests
{
    [Collection("Sequential")]
    [TestCaseOrderer(ordererTypeName: "UsersManagerAPI.IntegrationTests.AlphabeticalOrderer", ordererAssemblyName: "UsersManagerAPI.IntegrationTests")]
    public class UsersControllerTest
    {
        readonly string userUrl = "/Users";

        HttpClient client;
        List<UserDTO> testUsers;
        UserManagerAPIWebApplicationFactory application;

        public UsersControllerTest() 
        {
            application = new UserManagerAPIWebApplicationFactory();
            client = application.CreateClient();

            string json = File.ReadAllText(Directory.GetCurrentDirectory() + "/TestDatas.txt");
            var list = JsonConvert.DeserializeObject<List<UserDTO>>(json);
            testUsers = (list != null) ? list : new List<UserDTO>();
            
        }

        protected void CleanDB()
        {
            //Clean the test DB
            var response = client.GetAsync(userUrl);
            response.Wait();
            response.Result.EnsureSuccessStatusCode();
            var users = response.Result.Content.ReadFromJsonAsync<List<UserDTO>>();
            users.Wait();
            if (users != null && users.Result != null)
            {
                foreach (var user in users.Result)
                {
                    response = client.DeleteAsync($"{userUrl}/{user.Id}");
                    response.Wait();
                }
            }
        }

        protected AddUserRequest GenerateAddUserRequest(UserDTO user)
        {
            return new AddUserRequest()
            {
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                Website = user.Website,
                Company = user.Company,

            };
        }

        protected UpdateUserRequest GenerateUpdateUserRequest(UserDTO user)
        {
            return new UpdateUserRequest()
            {
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                Website = user.Website,
                Company = user.Company,

            };
        }

        protected void AddTestUsersToDB()
        {
            foreach(var user in testUsers)
            {
                string json = JsonConvert.SerializeObject(GenerateAddUserRequest(user), Formatting.Indented);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(userUrl, content);
                response.Wait();
            }
        }

        protected bool IsSameUser(UserDTO testUser, UserDTO userFromDB)
        {
            return testUser.Name == userFromDB.Name &&
                testUser.Username == userFromDB.Username &&
                testUser.Email == userFromDB.Email &&
                testUser.Phone == userFromDB.Phone &&
                testUser.Address.Street == userFromDB.Address.Street &&
                testUser.Address.Suite == userFromDB.Address.Suite &&
                testUser.Address.City == userFromDB.Address.City &&
                testUser.Address.Zipcode == userFromDB.Address.Zipcode &&
                testUser.Address.Geo.Lat == userFromDB.Address.Geo.Lat &&
                testUser.Address.Geo.Lng == userFromDB.Address.Geo.Lng &&
                testUser.Website == userFromDB.Website &&
                testUser.Company.Name == userFromDB.Company.Name &&
                testUser.Company.CatchPhrase == userFromDB.Company.CatchPhrase &&
                testUser.Company.Bs == userFromDB.Company.Bs;
        }

        protected bool IsValidMongoDBId(string id)
        {
            ObjectId objectId;
            return ObjectId.TryParse(id, out objectId);
        }

        protected async Task<UserPair?> AddRandomTestUSerToTestDbAndReturn()
        {
            int index = new Random().Next(testUsers.Count);
            var selectedTestUser = testUsers.ElementAt(index);
            string json = JsonConvert.SerializeObject(GenerateAddUserRequest(selectedTestUser), Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var postResponse = await client.PostAsync(userUrl, content);
            var postedUser = await postResponse.Content.ReadFromJsonAsync<UserDTO>();

            // Assert
            Assert.True(postResponse != null);
            Assert.True(postResponse.StatusCode == HttpStatusCode.Created);

            return new UserPair() { TestUser = selectedTestUser, UserFromDb = postedUser};
        }

        /// <summary>
        /// Delete test db, than fill test users. Then GetAllUSerAsync and check response
        /// </summary>
        [Fact]
        public async void GetAllUserAsync_ReturnAllUser()
        {
            // Arrange
            CleanDB();
            AddTestUsersToDB();

            //Act
            var response = await client.GetAsync(userUrl);
            var usersFromDb = await response.Content.ReadFromJsonAsync<List<UserDTO>>();

            // Assert
            Assert.True(response != null);
            Assert.True(usersFromDb != null);
            Assert.True(response.IsSuccessStatusCode);
            Assert.True(usersFromDb.Count == testUsers.Count);

            //Check equals
            List<UserDTO> sortedTestUser = testUsers.OrderBy(o => o.Name).ToList();
            List<UserDTO> sortedUsersFromDb = usersFromDb.OrderBy(o => o.Name).ToList();
            for (int i = 0; i < sortedTestUser.Count; i++)
            {
                Assert.True(IsSameUser(sortedTestUser.ElementAt(i), sortedUsersFromDb.ElementAt(i)));
            }
        }

        /// <summary>
        /// Delete test db, add a random user to db than get it by id
        /// </summary>
        [Fact]
        public async void GetUserAsync_ReturnUserById()
        {
            // Arrange
            CleanDB();
            var userPair = AddRandomTestUSerToTestDbAndReturn().Result;
            Assert.True(userPair != null && userPair.UserFromDb != null && userPair.TestUser != null);

            //Act
            var getResponse = await client.GetAsync($"{userUrl}/{userPair.UserFromDb.Id}");
            var userFromDb = await getResponse.Content.ReadFromJsonAsync<UserDTO>();

            // Assert
            Assert.True(getResponse != null);
            Assert.True(getResponse.IsSuccessStatusCode);
            Assert.True(userFromDb != null);
            Assert.True(IsSameUser(userPair.TestUser, userFromDb));

        }

        /// <summary>
        /// Test GetUserAsync if user doesn't exists
        /// </summary>
        [Fact]
        public async void GetNotExistsUSerAsync_ReturnNotFound()
        {
            // Arrange
            CleanDB();

            // Act
            ObjectId randomId = ObjectId.GenerateNewId();
            var getResponse = await client.GetAsync($"{userUrl}/{randomId}");

            // Assert
            Assert.True(getResponse != null);
            Assert.True(getResponse.StatusCode == System.Net.HttpStatusCode.NotFound);

        }

        /// <summary>
        /// Delete test db, than post a test users. Then check the response
        /// </summary>
        [Fact]
        public void AddUserAsync_AddAndReturnCreatedUser()
        { 
            // Arrange
            CleanDB();

            // Act
            var userPair = AddRandomTestUSerToTestDbAndReturn().Result;

            Assert.True(userPair != null && userPair.UserFromDb != null && userPair.TestUser != null);
            Assert.True(IsSameUser(userPair.TestUser, userPair.UserFromDb));
            Assert.True(IsValidMongoDBId(userPair.UserFromDb.Id));
        }


        /// <summary>
        /// Delete test db, than post a test users. Then delete it and check response
        /// </summary>
        [Fact]
        public async void DeleteUserAsync_DeleteAndReturnDeletedUser()
        {
            // Arrange
            CleanDB();
            var userPair = AddRandomTestUSerToTestDbAndReturn().Result;
            Assert.True(userPair != null && userPair.UserFromDb != null && userPair.TestUser != null);

            // Act
            var response = client.DeleteAsync($"{userUrl}/{userPair.UserFromDb.Id}");
            response.Wait();
            response.Result.EnsureSuccessStatusCode();

            string json = await response.Result.Content.ReadAsStringAsync();
            var deletedUser = JsonConvert.DeserializeObject<DeleteUserResponse>(json);

            Assert.True(deletedUser != null && deletedUser.User != null);
            Assert.True(IsSameUser(userPair.TestUser, deletedUser.User));
            Assert.True(IsValidMongoDBId(userPair.UserFromDb.Id));
        }

        /// <summary>
        /// Delete test db, than post a test users. Then update name property and check response
        /// </summary>
        [Fact]
        public async void UpdateUserAsync_UpdateAndReturn()
        {
            // Arrange
            CleanDB();
            string newName = "TESTUSER NAME";
            var userPair = AddRandomTestUSerToTestDbAndReturn().Result;
            Assert.True(userPair != null && userPair.UserFromDb != null && userPair.TestUser != null);

            // Act
            var request = GenerateUpdateUserRequest(userPair.TestUser);
            request.Name = newName;
            string json = JsonConvert.SerializeObject(request, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{userUrl}/{userPair.UserFromDb.Id}", content);
            string responseJson = await response.Content.ReadAsStringAsync();
            var userFromDb = JsonConvert.DeserializeObject<UserDTO>(responseJson);

            //Assert
            Assert.True(response != null && response.IsSuccessStatusCode);
            Assert.True(userFromDb != null);
            Assert.True(userFromDb.Name == newName);
        }
    }

    public class UserPair
    {
        public UserDTO? TestUser { get; set; }
        public UserDTO? UserFromDb { get; set; }
    }
}