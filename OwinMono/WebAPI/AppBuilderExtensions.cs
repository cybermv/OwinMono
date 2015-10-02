namespace WebAPI
{
    using Newtonsoft.Json.Serialization;
    using Owin;
    using System.Web.Http;

    public static class AppBuilderExtensions
    {
        public static void UseFruitsWebApi(this IAppBuilder app)
        {
            HttpConfiguration configuration = new HttpConfiguration
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
            };

            configuration.MapHttpAttributeRoutes();

            configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);
            configuration.Formatters.JsonFormatter.Indent = true;
            configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            app.UseWebApi(configuration);
        }
    }
}