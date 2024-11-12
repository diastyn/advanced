using Advanced.Application.Dtos;
using Advanced.Application.Services.Interfaces;
using Advanced.Domain.Entities;
using Advanced.Domain.Repositories.Users;
using Advanced.Domain.Wrappers;
using MongoDB.Bson;

namespace Advanced.Application.Services.Implementations;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;

    public AuthenticationService(IUserRepository userRepository, 
        IPasswordService passwordService, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }
    public async Task<BaseResponse<ObjectId>> Registration(RegistrationDto registration)
    {
        if (!registration.IsValid) 
            return new BaseResponse<ObjectId> { 
                HasData = false, 
                Error = "Registration is not valid!",
                Success = false 
            };
        
        var isAnyUserByEmail = await _userRepository.AnyUserByEmail(registration.Email);
        if (isAnyUserByEmail)
        {
            return new BaseResponse<ObjectId>
            {
                HasData = false,
                Error = "Provided email exists!",
                Success = false
            };
        }
        
        var user = new User
        {
            Email = registration.Email,
            Password = _passwordService.HashPassword(registration.Password),
            Name = registration.Name,
            Surname = registration.Surname
        };

        try
        {
            await _userRepository.AddAsync(user);
            return new BaseResponse<ObjectId> 
            { 
                HasData = true, 
                Success = true, 
                Data = user.Id
            };
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<BaseResponse<string>> Login(LoginDto login)
    {
        if (!login.IsValid)
        {
            return new BaseResponse<string>
            {
                HasData = false,
                Error = "Login is not valid!",
                Success = false
            };
        }

        var user = await _userRepository.FirstOrDefaultUserAsync<UserDto>(login.Email);

        if (user == null || !_passwordService.VerifyPassword(login.Password, user.Password))
        {
            return new BaseResponse<string>
            {
                HasData = false,
                Error = "Email or Password are not valid!",
                Success = false
            };
        }
        return new BaseResponse<string>
        {
            HasData = true,
            Success = true,
            Data = _jwtService.GenerateJwtToken(login.Email, user.Id)
        };
    }
}