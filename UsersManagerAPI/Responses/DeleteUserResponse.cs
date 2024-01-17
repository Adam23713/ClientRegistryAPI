using ClientRegistryAPI.Models.DTO;

namespace ClientRegistryAPI.Responses
{
    /// <summary>
    /// Contains the deleted user
    /// </summary>
    public class DeleteUserResponse
    {
        public DeleteUserResponse(UserDTO? userDTO)
        {
            User = userDTO;
        }

        public UserDTO? User { get; set; }
    }
}
