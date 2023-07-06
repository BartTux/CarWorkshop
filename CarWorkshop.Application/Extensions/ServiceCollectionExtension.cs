using CarWorkshop.Application.Profiles;
using CarWorkshop.Application.Validation;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using CarWorkshop.Application.Services;
using AutoMapper;
using CarWorkshop.Application.CQRS.CarWorkshops.Commands;
using CarWorkshop.Application.Services.Contracts;

namespace CarWorkshop.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserContextService, UserContextService>();

        services.AddMediatR(config => 
            config.RegisterServicesFromAssemblyContaining<CreateCarWorkshopCommand>());

        //services.AddAutoMapper(typeof(CarWorkshopMappingProfile));
        services.AddScoped(provider => new MapperConfiguration(config =>
        {
            var scope = provider.CreateScope();
            var userContext = scope.ServiceProvider.GetRequiredService<IUserContextService>();

            config.AddProfile(new CarWorkshopMappingProfile(userContext));
        })
        .CreateMapper());

        services.AddValidatorsFromAssemblyContaining<CreateCarWorkshopCommandValidator>()
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();
    }

}
