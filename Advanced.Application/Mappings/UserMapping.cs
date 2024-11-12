using Advanced.Application.Dtos;
using Advanced.Domain.Entities;
using AutoMapper;

namespace Advanced.Application.Mappings;

public class UserMapping : Profile
{
    public UserMapping()
    {
        UserDtoMapping();
        UsersMapping();
    }

    private void UserDtoMapping()
    {
        CreateMap<User, UserDto>();
    }
    
    private void UsersMapping()
    {
        CreateMap<UserDto, User>();
    }
}