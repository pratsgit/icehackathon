using Owin;
using System.Web.Http;

namespace EntityServiceWorkerRole
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "Default",
                "{controller}",
                new { id = RouteParameter.Optional });

            app.UseWebApi(config);
        }
    }
}