using SydneyHotel.Models;
using SydneyHotel1.Data;
using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace SydneyHotel1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                    string role;

                    using (SydneyHotel1Context db = new SydneyHotel1Context())
                    {
                        Account account = db.Accounts.SingleOrDefault(a => a.EmailAddress == username);

                        role = account.Role.ObjectName;
                    }
                    e.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(username, "Forms"), role.Split(','));
                }
            }

        }

        //public override void Init()
        //{
        //    base.AuthenticateRequest += OnAuthenticateRequest;
        //}

        //private void OnAuthenticateRequest(object sender, EventArgs e)
        //{
        //    if (HttpContext.Current.User.Identity.IsAuthenticated)
        //    {
        //        var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
        //        var decodedTicket = FormsAuthentication.Decrypt(cookie.Value);
        //        var roles = decodedTicket.UserData.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        //        var principal = new GenericPrincipal(HttpContext.Current.User.Identity, roles);
        //        HttpContext.Current.User = principal;
        //    }
        //}
    }
}
