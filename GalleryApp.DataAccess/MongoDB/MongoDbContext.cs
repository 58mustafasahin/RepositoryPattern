using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GalleryApp.DataAccess.MongoDB
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDbContext(IOptions<MongoOptions> mongoOptions)
        {
            var mongoClient = new MongoClient(mongoOptions.Value.ConnectionString);
            _mongoDatabase = mongoClient.GetDatabase(mongoOptions.Value.DatabaseName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _mongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name);
        }


        public IMongoDatabase GetDatabase()
        {
            return _mongoDatabase;
        }
    }
}
