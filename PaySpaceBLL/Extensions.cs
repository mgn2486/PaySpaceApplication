using Microsoft.Extensions.DependencyInjection;
using MediatR;
using PaySpaceBLL.Abstractions;
using PaySpaceBLL.DomainServices;
using System.Reflection;
using AutoMapper;
using PaySpaceBLL.AutoMapperProfile;
using PaySpaceDAL.Abstracions;
using PaySpaceDAL;
using Microsoft.Extensions.Configuration;

namespace PaySpaceBLL;

public static class PaySpaceBLLServiceCollections
{
    public static IServiceCollection AddPaySpaceBLLServiceCollections(this IServiceCollection servicesCollection, IConfiguration configuration)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new PaySpaceMappingProfile());
        });
        var mapper = mapperConfig.CreateMapper();

        return servicesCollection
            .AddSingleton(mapper)
            .AddPaySpaceDALServiceCollections(configuration)
            .AddTransient<IPaySpaceTaxService, PaySpaceTaxService>()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
