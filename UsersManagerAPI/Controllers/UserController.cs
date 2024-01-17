using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ClientRegistryAPI.Filters;
using ClientRegistryAPI.Models.Domain;
using ClientRegistryAPI.Models.DTO;
using ClientRegistryAPI.Repositories;
using ClientRegistryAPI.Requests;
using ClientRegistryAPI.Responses;

namespace ClientRegistryAPI.Controllers
{
    /// <summary>
    /// This controller manage users in the system
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns all the users in the system
        /// </summary>
        /// <remarks>
        ///    Sample **request**:
        ///    
        ///        GET /User/
        /// </remarks>
        /// <response code="200">Returns all the users in the system</response>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<UserDTO>), 200)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userRepository.GetAllUserAsync();

            var usersDto = mapper.Map<ICollection<UserDTO>>(users);

            return Ok(usersDto);
        }

        /// <summary>
        /// Returns with the user in the system, according to its id
        /// </summary>
        /// <remarks>
        ///    Sample **request**:
        ///    
        ///        GET /User/1
        /// </remarks>
        /// <response code="200">Returns the selected user</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">User not found</response>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(UserDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var user = await userRepository.GetUserAsync(id);

            if (user == null)
            {
                return NotFound(new ErrorResponse("", "User not found"));
            }

            var usernDTO = mapper.Map<UserDTO>(user);

            return Ok(usernDTO);
        }

        /// <summary>
        /// Create a user in the system
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="201">Create a user in the system</response>
        /// <response code="400">Bad request</response>
        /// <response code="409">User already exists</response>
        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 409)]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest addUserRequest)
        {
            // Convert DTO to Domain Modell
            var newUsers = mapper.Map<User>(addUserRequest);

            var used = await userRepository.IsUserNameOrEmailUsed(newUsers.Name, newUsers.Email);
            if (used)
            {
                return Conflict(new ErrorResponse("", "User already exists"));
            }

            var savedUser = await userRepository.AddUserAsync(newUsers);
            if (savedUser == null)
            {
                return BadRequest(new ErrorResponse("", "Unable to create user"));
            }

            // Convert back to DTO
            var savedUserDto = mapper.Map<UserDTO>(savedUser);

            // Return created http code and show saved user
            return CreatedAtAction(nameof(GetUserById), new { id = savedUserDto.Id }, savedUserDto);
        }


        /// <summary>
        /// Update the user in the system
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">User not found</response>
        /// <response code="409">Username or email already used</response>
        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(UserDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 409)]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequest updateUserRequest)
        {
            // Convert DTO to Domain model
            var user = mapper.Map<User>(updateUserRequest);

            // Check if the user exists
            var existingUser = await userRepository.GetUserAsync(id);
            if (existingUser == null)
            {
                return NotFound(new ErrorResponse("", "User not found"));
            }

            // Update User
            user = await userRepository.UpdateUserAsync(id, user);
            if (user == null)
            {
                return BadRequest(new ErrorResponse("", "Unable to update user"));
            }

            var selectedUserByNameAndEmail = await userRepository.GetUserByUserNameAndEmail(user.Name, user.Email);
            if (selectedUserByNameAndEmail != null && selectedUserByNameAndEmail.Id != user.Id)
            {
                return Conflict(new ErrorResponse("Username, Email", "Username or email already used"));
            }

            // Convert Domain back to DTO
            var userDTO = mapper.Map<UserDTO>(user);

            return Ok(userDTO);
        }


        /// <summary>
        /// Delete the user in the system, according to its id and return that deleted user
        /// </summary>
        /// <remarks>
        ///    Sample **request**:
        ///    
        ///        DELETE /User/1
        /// </remarks>
        /// <response code="200">Delete and return the selected user</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">User not found</response>
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(DeleteUserResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {

            // Get user from database
            var user = await userRepository.DeleteUserAsync(id);

            // If null NotFound
            if (user == null)
            {
                return NotFound(new ErrorResponse("", "User not found"));
            }

            // Convert response back to DTO
            var userDTO = mapper.Map<UserDTO>(user);

            return Ok(new DeleteUserResponse(userDTO));
        }
    }
}