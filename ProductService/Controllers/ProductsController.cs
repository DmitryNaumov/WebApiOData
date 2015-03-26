using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using NHibernate;
using NHibernate.Linq;
using ProductService.Models;

namespace ProductService.Controllers
{
    public class ProductsController : ODataController
    {
        private readonly ISession _session;

        public ProductsController(ISession session)
        {
            _session = session;
        }

        public ProductsController()
            : this(Singleton<ISessionFactory>.Instance.OpenSession())
        {
        }

        // GET /Products()
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<Product> Get()
        {
            return _session.Query<Product>();
        }

        // GET /Products(1)
        [EnableQuery]
        public SingleResult<Product> Get([FromODataUri] int key)
        {
            var result = _session.Query<Product>().Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

        // GET /Products(1)/Supplier
        [EnableQuery]
        public SingleResult<Supplier> GetSupplier([FromODataUri] int key)
        {
            var result = _session.Query<Product>().Where(m => m.Id == key).Select(m => m.Supplier);
            return SingleResult.Create(result);
        }
    }
}
