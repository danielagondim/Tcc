using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace TCC.CursosOnline.Web.Security 
{
    class PermissoesFiltro : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
                
                filterContext.HttpContext.Response.RedirectToRoute("Default");
        }
    }
}
