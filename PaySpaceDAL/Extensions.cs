using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaySpaceDAL.Abstracions;
using PaySpaceDAL.Repositories;

namespace PaySpaceDAL;

public static class Extensions
{
    public static IServiceCollection AddPaySpaceDALServiceCollections(this IServiceCollection servicesCollection, IConfiguration configuration)
    {
        servicesCollection.AddDbContext<PaySpaceDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PaySpaceDbConnection"))
            );

        return servicesCollection
            .AddTransient<ITaxRepository, TaxRepository>();
    }

}
