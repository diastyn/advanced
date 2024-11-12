using Advanced.Application.Dtos;
using Advanced.Domain.Entities;
using AutoMapper;

namespace Advanced.Application.Mappings;

public class UserHistoryMapping : Profile
{
    public UserHistoryMapping()
    {
        
    }

    private void InteractionMapping()
    {
        CreateMap<InteractionDto, UserHistory>();
    }
}