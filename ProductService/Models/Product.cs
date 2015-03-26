using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Models
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
        public virtual string Category { get; set; }

        [ForeignKey("Supplier")]
        public virtual Supplier Supplier { get; set; }
    }
}
