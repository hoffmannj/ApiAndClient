using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace DrinkApi.WebApi.Authentication
{
    public class AuthAttribute : AuthorizeAttribute
    {
        private readonly string[] _testKeys;

        public AuthAttribute()
        {
            _testKeys = ConfigurationManager.AppSettings["TestKeys"].Split(';');
        }

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (AuthorizeRequest(actionContext)) return;
            base.OnAuthorization(actionContext);
        }

        private bool AuthorizeRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var auth = actionContext.Request.Headers.Authorization?.ToString();
            if (!string.IsNullOrEmpty(auth) && _testKeys.Any(tk => tk == auth)) return true;
            return false;
        }
    }
}