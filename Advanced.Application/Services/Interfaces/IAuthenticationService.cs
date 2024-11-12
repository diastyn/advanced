using Advanced.Application.Dtos;
using Advanced.Domain.Wrappers;
using MongoDB.Bson;

namespace Advanced.Application.Services.Interfaces;

public interface IAuthenticationService
{
    Task<BaseResponse<ObjectId>> Registration(RegistrationDto registration);
    Task<BaseResponse<string>> Login(LoginDto login);
}