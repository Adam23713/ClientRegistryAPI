using AutoMapper;
using ClientRegistryAPI.Models.Domain;
using ClientRegistryAPI.Models.DTO;
using ClientRegistryAPI.Requests;

namespace ClientRegistryAPI.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<UserDTO, CachedUser>().ReverseMap();
            CreateMap<CachedUserDTO, CachedUser>().ReverseMap();
            CreateMap<AddUserRequest, User>().ReverseMap();
            CreateMap<AddUserRequest, CachedUser>().ReverseMap();
            CreateMap<UpdateUserRequest, User>().ReverseMap();
        }
    }
}
