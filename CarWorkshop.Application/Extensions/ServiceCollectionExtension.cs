using CarWorkshop.Application.Profiles;
using CarWorkshop.Application.Validation;
using CarWorkshop.Application.Services;
using CarWorkshop.Application.Services.Contracts;
using CarWorkshop.Application.CQRS.CarWorkshops.Commands;
using CarWorkshop.Application.Authorization.Requirements;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using CarWorkshop.Application.Authorization.RequirementHandlers;

namespace CarWorkshop.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserContextService, UserContextService>();

        services.AddMediatR(config => 
            config.RegisterServicesFromAssemblyContaining<CreateCarWorkshopCommand>());

        //services.AddAutoMapper(typeof(CarWorkshopMappingProfile));
        services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
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
