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
            CreateMap<AddUserRequest, User>().ReverseMap();
            CreateMap<UpdateUserRequest, User>().ReverseMap();
        }
    }
}
