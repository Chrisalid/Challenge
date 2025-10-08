using System;
using System.Security.Claims;
using Challenge.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Challenge.Api.Controllers.Base;

[ApiController]
[Route("[apiName]/v{version:apiVersion}/[controller]")]
public class BaseController : ControllerBase
{
    protected long GetCurrentUserId()
    {
        var authTokenUserId = HttpContext.Items["User"]?.ToString();

        if (!string.IsNullOrWhiteSpace(authTokenUserId) && long.TryParse(authTokenUserId, out var userId))
            return userId;

        var authTokenUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrWhiteSpace(authTokenUserIdClaim) && long.TryParse(authTokenUserIdClaim, out var userIdClaim))
            return userIdClaim;

        throw new UnauthorizedAccessException("Usuário não autenticado ou token inválido");
    }

    public sealed class CustomValueRoutingConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            if (!action.RouteValues.TryGetValue(ApiSettingsExtension.projectNameSpace, out _))
                action.RouteValues.Add("apiName", ApiSettingsExtension.projectNameSpace);
        }
    }
}
