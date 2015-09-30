[assembly: Microsoft.Owin.OwinStartup(typeof(Host.Startup))]

namespace Host
{
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Owin;
    using System.Net;
    using WebAPI;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // add the console logging middleware
            app.Use<ConsoleLoggingMiddleware>();

            // add the WebAPI middleware
            app.UseFruitsWebApi();

            // add the FileServer middleware
            app.UseFileServer(new FileServerOptions
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileSystem = new PhysicalFileSystem(@"../../../StaticFiles/Public")
                //FileSystem = new PhysicalFileSystem(@"Public")
            });

            // handle leftover requests
            app.Run(async context =>
            {
                await context.Response.WriteAsync("404 - Not found");
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            });
        }
    }
}