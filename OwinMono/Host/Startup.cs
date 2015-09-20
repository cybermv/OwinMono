using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Host.Startup))]

namespace Host
{
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration configuration = new HttpConfiguration();
            configuration.MapHttpAttributeRoutes();
            configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);
            configuration.Formatters.JsonFormatter.Indent = true;

            app.UseWebApi(configuration);

            app.Run(context =>
            {
                context.Response.Write("404 - Not found");
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Task.FromResult(0);
            });
        }
    }
}