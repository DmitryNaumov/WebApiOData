using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using ProductService.Models;

namespace ProductService
{
    internal sealed class SessionFactory
    {
        public static ISessionFactory CreateSessionFactory()
        {
            var seed = false;

            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(@"Server=.\SQLEXPRESS;Database=WebApiOData;Trusted_Connection=True;"))
                .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<SessionFactory>(ShouldMapType)))
                .ExposeConfiguration(config => seed = BuildSchema(config))
                .BuildSessionFactory();

            if (seed)
                Seed(sessionFactory);

            return sessionFactory;
        }

        private static bool BuildSchema(Configuration config)
        {
            try
            {
                new SchemaValidator(config).Validate();
                return false;
            }
            catch (HibernateException ex)
            {
                new SchemaExport(config).Create(false, true);
                return true;
            }
        }

        private static bool ShouldMapType(Type type)
        {
            return type.Namespace.EndsWith("Models");
        }

        private static void Seed(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var product1 = new Product
                {
                    Name = "Hat",
                    Price = 14.95m,
                    Category = "Clothing"
                };

                var product2 = new Product
                {
                    Name = "Socks",
                    Price = 6.95m,
                    Category = "Clothing"
                };

                var product3 = new Product
                {
                    Name = "Pogo Stick",
                    Price = 29.99m,
                    Category = "Toys"
                };

                var supplier = new Supplier
                {
                    Name = "Wingtip Toys",
                };

                session.Save(supplier);
                session.Save(product1);
                session.Save(product2);
                session.Save(product3);

                supplier.AddProduct(product1);
                supplier.AddProduct(product2);
                supplier.AddProduct(product3);

                session.Flush();
            }
        }
    }
}