using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IMongoClient>(new MongoClient("mongodb://localhost:27017"));
    services.AddControllers();
}

}
