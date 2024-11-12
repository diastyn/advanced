using Advanced.Domain.Entities;
using Advanced.Domain.Repositories.Users;
using Advanced.Persistence.Constants;
using Advanced.Persistence.Repositories.Common;
using AutoMapper;

namespace Advanced.Persistence.Repositories.Users;

public class UserRepository(MongoDb<User> mongoDb, IMapper mapper)
    : CommonRepository<User>(mongoDb, mapper, MongoCollection.Users), IUserRepository
{
    public async Task<bool> AnyUserByEmail(string email) => await AnyAsync(u => u.Email == email);

    public async Task<T?> FirstOrDefaultUserAsync<T>(string email) => await FirstOrDefaultAsync<T>(u => u.Email == email);
}