using RepresentativePanel.Domain.Core;
using RepresentativePanel.Domain.Enum;

namespace KermanBatterySeller.Web.Controllers
{
    public static class CheckUserAccess
    {
        public static async Task<bool> IsUserHasAccess(string tokenAuth, Roles type, IConfiguration configuration)
        {
            if (type == Roles.Admin)
            {
                var isCompanyRole = await ApiService.GetData<Result<Roles>>(configuration["GlobalSettings:ApiUrl"], $"Shared/CheckUserRoleAccess?role={type}",
               tokenAuth);
                if (isCompanyRole.Equals(Roles.Seller)) return false;
            }
            if (type == Roles.Seller)
            {
                var isApplicantRole = await ApiService.GetData<Result<Roles>>(configuration["GlobalSettings:ApiUrl"], $"Shared/CheckUserRoleAccess?role={type}",
            tokenAuth);
                if (isApplicantRole.Equals( Roles.Admin)) return false;
            }
            return true;
        }
    }
}
