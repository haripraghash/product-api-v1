using acme.product.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme.product.repositories.interfaces
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAll();

        Product GetById(string productId);

        Product Update(Product product);

        Product Delete(Product product);
    }
}
