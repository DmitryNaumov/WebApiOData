using System.Linq;
using System.Web.OData;
using NHibernate;
using NHibernate.Linq;
using ProductService.Models;

namespace ProductService.Controllers
{
    public class SuppliersController : ODataController
    {
        private readonly ISession _session;

        public SuppliersController(ISession session)
        {
            _session = session;
        }

        public SuppliersController()
            : this(Singleton<ISessionFactory>.Instance.OpenSession())
        {
        }

        // GET /Suppliers(1)/Products
        [EnableQuery]
        public IQueryable<Product> GetProducts([FromODataUri] int key)
        {
            return _session.Query<Supplier>().Where(m => m.Id.Equals(key)).SelectMany(m => m.Products);
        }
    }
}