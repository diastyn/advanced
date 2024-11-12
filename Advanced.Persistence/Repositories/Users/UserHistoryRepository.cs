using Advanced.Domain.Entities;
using Advanced.Domain.Repositories.Users;
using Advanced.Persistence.Constants;
using Advanced.Persistence.Repositories.Common;
using AutoMapper;

namespace Advanced.Persistence.Repositories.Users;

public class UserHistoryRepository(MongoDb<UserHistory> mongoDb, IMapper mapper)
    : CommonRepository<UserHistory>(mongoDb, mapper, MongoCollection.UserHistories), IUserHistoryRepository
{
    public async Task AddInteractionAsync<TDto>(TDto dto) => await AddAsync(dto);
}