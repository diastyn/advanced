using Advanced.Domain.Entities;
using MongoDB.Bson;

namespace Advanced.Application.Services.Interfaces;

public interface IUserService
{
    Task<User> GetUser(ObjectId? id);
}