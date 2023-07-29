using CarWorkshop.Application.Authorization.Requirements;
using CarWorkshop.Application.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CarWorkshop.Application.Authorization.RequirementHandlers;

public class ResourceOperationRequirementHandler
    : AuthorizationHandler<ResourceOperationRequirement, Domain.Entities.CarWorkshop>
{
    public string UserId { get; private set; } = default!;

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   ResourceOperationRequirement requirement,
                                                   Domain.Entities.CarWorkshop resource)
    {
        if (requirement.ResourceOperation is ResourceOperation.Read)
            context.Succeed(requirement);

        UserId = context.User
            .FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)!.Value;

        if (UserId == resource.CreatedById) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
