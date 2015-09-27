using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Host.Startup))]

namespace Host
{
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using System.Net;
    using System.Web.Http;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // add the console logging middleware
            app.Use<ConsoleLoggingMiddleware>();

            // add the WebAPI middleware
            SetUpWebApi(app);

            // add the FileServer middleware
            SetUpFileServer(app);

            // handle leftover requests
            app.Run(async context =>
            {
                await context.Response.WriteAsync("404 - Not found");
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            });
        }

        private void SetUpWebApi(IAppBuilder app)
        {
            HttpConfiguration configuration = new HttpConfiguration();
            configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            configuration.MapHttpAttributeRoutes();
            configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);
            configuration.Formatters.JsonFormatter.Indent = true;

            app.UseWebApi(configuration);
        }

        private void SetUpFileServer(IAppBuilder app)
        {
            app.UseFileServer(new FileServerOptions
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileSystem = new PhysicalFileSystem(@"Public")
            });
        }
    }
}