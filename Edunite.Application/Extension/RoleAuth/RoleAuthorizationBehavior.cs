using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Edunite.Application.Extension.RoleAuth
{
    public class RoleAuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleAuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (request is IRequireRoles roleRequest)
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null || !user.Identity.IsAuthenticated)
                    throw new UnauthorizedAccessException("User is not authenticated");

                var userRoles = user.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value);

                var hasRequiredRole = roleRequest.Roles.Intersect(userRoles).Any();
                if (!hasRequiredRole)
                    throw new UnauthorizedAccessException("User does not have the required role");
            }

            return await next();
        }
    }
}
