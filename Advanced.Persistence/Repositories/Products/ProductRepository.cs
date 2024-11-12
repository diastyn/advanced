using Advanced.Domain.Entities;
using Advanced.Domain.Repositories.Products;
using Advanced.Persistence.Constants;
using Advanced.Persistence.Repositories.Common;
using AutoMapper;

namespace Advanced.Persistence.Repositories.Products;

public class ProductRepository(MongoDb<Product> mongoDb, IMapper mapper) 
    : CommonRepository<Product>(mongoDb, mapper, MongoCollection.Products), IProductRepository;