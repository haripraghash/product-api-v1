using acme.product.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme.product.services
{
    public interface IProductsService
    {
        IEnumerable<Product> GetAll();

        Product GetById(string productId);

        Product Update(Product product);

        Product Delete(Product product);
    }

    public class ProductsService : IProductsService
    {
        public Product Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetById(string productId)
        {
            throw new NotImplementedException();
        }

        public Product Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
