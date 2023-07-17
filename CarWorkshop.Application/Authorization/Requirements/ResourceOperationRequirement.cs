using CarWorkshop.Application.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarWorkshop.Application.Authorization.Requirements;

public class ResourceOperationRequirement : IAuthorizationRequirement
{
    public ResourceOperation ResourceOperation { get; }

    public ResourceOperationRequirement(ResourceOperation resourceOperation)
    {
        ResourceOperation = resourceOperation;
    }
}