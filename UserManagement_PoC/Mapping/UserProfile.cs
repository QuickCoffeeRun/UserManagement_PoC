using AutoMapper;
using UserManagement_PoC.DTOs;
using UserManagement_PoC.Models;

namespace UserManagement_PoC.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, User>();
        }
    }
}
