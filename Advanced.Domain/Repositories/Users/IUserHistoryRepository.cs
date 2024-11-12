using Advanced.Domain.Entities;
using Advanced.Domain.Repositories.Common;

namespace Advanced.Domain.Repositories.Users;

public interface IUserHistoryRepository : ICommonRepository<UserHistory>
{
    Task AddInteractionAsync<TDto>(TDto dto);
}