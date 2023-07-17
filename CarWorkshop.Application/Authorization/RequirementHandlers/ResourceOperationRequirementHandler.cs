using CarWorkshop.Application.Authorization.Requirements;
using CarWorkshop.Application.Models;
using CarWorkshop.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CarWorkshop.Application.Authorization.RequirementHandlers;

public class ResourceOperationRequirementHandler 
    : AuthorizationHandler<ResourceOperationRequirement, CarWorkshopService>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   ResourceOperationRequirement requirement,
                                                   CarWorkshopService resource)
    {
        if (requirement.ResourceOperation is ResourceOperation.Create or ResourceOperation.Read)
            context.Succeed(requirement);

        var userId = context.User
            .FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)!.Value;

        if (userId == resource.CarWorkshop.CreatedById) 
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
