using System.Security.Claims;
using SchoolManagementSystem.Application.Tenancy;

namespace SchoolManagementSystem.Web.Tenancy;

public class TenantContext : ITenantContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public TenantContext(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public Guid InstitutionId
    {
        get
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext is not null)
            {
                var institutionId = httpContext.User.FindFirstValue("institution_id")
                    ?? httpContext.Request.Headers["X-Institution-Id"].ToString();

                if (Guid.TryParse(institutionId, out var parsed))
                {
                    return parsed;
                }
            }

            return _configuration.GetValue<Guid?>("Tenant:DefaultInstitutionId") ?? Guid.Empty;
        }
    }
}
