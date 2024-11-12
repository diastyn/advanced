using System.Linq.Expressions;
using Advanced.Domain.Repositories.Common;
using Advanced.Domain.Wrappers;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Advanced.Persistence.Repositories.Common;

public class CommonRepository<T> : ICommonRepository<T>
{
    private readonly IMongoCollection<T> _collection;
    private readonly IMapper _mapper;
    private readonly MongoDb<T> _mongoDb;

    protected CommonRepository(MongoDb<T> mongoDb, IMapper mapper, string collectionName)
    {
        _mapper = mapper;
        _mongoDb = mongoDb;
        _collection = mongoDb.GetCollection(collectionName);
    }

    public async Task<IEnumerable<TDto>> GetAllAsync<TDto>()
    {
        var entities = await _collection.Find(_ => true).ToListAsync();
        return entities.Select(e => _mapper.Map<TDto>(e)).ToList();
    }

    public async Task<T> GetByIdAsync(ObjectId? id) => 
        await _collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync();

    public async Task<List<TDto>> GetAsync<TDto>(Expression<Func<T, bool>>? filter = null)
    {
        var entities = await _collection.Find(filter ?? (_ => true)).ToListAsync();
        return entities.Select(e => _mapper.Map<TDto>(e)).ToList();
    }
    
    public async Task<List<T>> SearchAsync(Expression<Func<T, bool>> filter, int page, int pageSize)
    {
        return await _collection.Find(filter)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }

    public async Task AddAsync<TDto>(TDto dto)
    {
        var entity = _mapper.Map<T>(dto);
        await _collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync<TDto>(ObjectId id, TDto entity)
    {
        var mappedEntity = _mapper.Map<T>(entity);
        await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", id), mappedEntity);
    }

    public async Task DeleteAsync(ObjectId id) => 
        await _collection.UpdateOneAsync(Builders<T>.Filter.Eq("_id", id), 
            Builders<T>.Update.Set("IsDeleted", true));

    public async Task<PaginatedResult<TK>> GetPaginatedAsync<TK>(int pageNumber, int pageSize)
    {
        var filter = Builders<T>.Filter.Empty;
        
        var paginatedData = await _collection.Find(filter)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
        
        return new PaginatedResult<TK>
        {
            TotalCount = await _collection.CountDocumentsAsync(filter),
            PageNumber = pageNumber,
            PageSize = pageSize,
            Data = paginatedData.Select(t => _mapper.Map<TK>(t)).ToList()
        };
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null) 
        => await _collection.Find(filter ?? (_ => true)).AnyAsync();

    public async Task<TK?> FirstOrDefaultAsync<TK>(Expression<Func<T, bool>>? filter = null)
    {
        T entity = await _collection.Find(filter ?? (_ => true)).FirstOrDefaultAsync();
        return _mapper.Map<TK>(entity);
    }
    
    public async Task AddEntitiesAsync<TDto>(List<TDto> entities)
    {
        var mappedEntities = entities.Select(e => _mapper.Map<T>(e));
        await _collection.InsertManyAsync(mappedEntities);
    }
}