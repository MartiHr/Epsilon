using Epsilon.Common;
using Epsilon.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epsilon.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.ModeratorRoleName}")]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
