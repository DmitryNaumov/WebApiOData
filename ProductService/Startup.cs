using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using NHibernate;
using Owin;
using ProductService.Models;

namespace ProductService
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            Singleton<ISessionFactory>.Instance = SessionFactory.CreateSessionFactory();

            var configuration = new HttpConfiguration();

            configuration.EnableSystemDiagnosticsTracing();

            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Product>("Products");
            builder.EntitySet<Supplier>("Suppliers");

            builder.Action("Seed");
            
            configuration.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());

            configuration.EnsureInitialized();
            appBuilder.UseWebApi(configuration);
        }
    }
}
