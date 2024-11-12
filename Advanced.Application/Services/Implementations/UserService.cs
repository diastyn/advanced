using Advanced.Application.Services.Interfaces;
using Advanced.Domain.Entities;
using Advanced.Domain.Repositories.Users;
using Advanced.Persistence;
using MongoDB.Bson;

namespace Advanced.Application.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(MongoDb<User> mongoDb, IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUser(ObjectId? id) => await _userRepository.GetByIdAsync(id);
}