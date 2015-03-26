using System;
using System.Collections.Generic;

namespace ProductService.Models
{
    public class Supplier
    {
        public Supplier()
        {
            Products = new List<Product>();
        }

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual void AddProduct(Product product)
        {
            if (product.Supplier != null)
                throw new InvalidOperationException();

            if (Products.Contains(product))
                return;

            product.Supplier = this;
            Products.Add(product);
        }
    }
}