using System.Linq.Expressions;
using Advanced.Domain.Entities;
using Advanced.Domain.Wrappers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Advanced.Domain.Repositories.Common;

public interface ICommonRepository<T>
{ 
    Task<IEnumerable<TDto>> GetAllAsync<TDto>(); 
    Task<T> GetByIdAsync(ObjectId? id); 
    Task AddAsync<TK>(TK entity); 
    Task UpdateAsync<TDto>(ObjectId id, TDto entity); 
    Task DeleteAsync(ObjectId id);

    Task<PaginatedResult<K>> GetPaginatedAsync<K>(int pageNumber, int pageSize);
    Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null);
    Task<TK?> FirstOrDefaultAsync<TK>(Expression<Func<T, bool>>? filter = null);
    Task AddEntitiesAsync<TDto>(List<TDto> entities);

    Task<List<TDto>> GetAsync<TDto>(Expression<Func<T, bool>>? filter = null);

    Task<List<T>> SearchAsync(Expression<Func<T, bool>> filter, int page, int pageSize);
}