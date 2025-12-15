using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;
using ChoreoSample.Entities.Books;

namespace ChoreoSample.Data;

[ConnectionStringName("Default")]
public class ChoreoSampleDbContext : AbpMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */
    
    public IMongoCollection<Book> Books => Collection<Book>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        //builder.Entity<YourEntity>(b =>
        //{
        //    //...
        //});
    }
}

