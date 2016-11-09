using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApplication5.Filters
{
    public class RolesAllowed : ActionFilterAttribute
    {
        private static List<string> Roles = new List<string>();

        public RolesAllowed(string roles)
        {
            Roles = SplitString(roles);
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Roles.ForEach(x => Debug.WriteLine(x));
        }

        private static List<string> SplitString(string original)
        {
            if (string.IsNullOrEmpty(original)) return Roles;

            return original.Split(',').Select(piece => new
            {
                piece,
                trimmed = piece.Trim()
            }).Where(param0 => !string.IsNullOrEmpty(param0.trimmed)).Select(param0 => param0.trimmed).ToList();
        }

    }
}