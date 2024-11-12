using Advanced.Domain.Entities;
using Advanced.Domain.Repositories.Common;

namespace Advanced.Domain.Repositories.Users;

public interface IUserRepository : ICommonRepository<User>
{
    Task<bool> AnyUserByEmail(string email);
    Task<T?> FirstOrDefaultUserAsync<T>(string email);
}