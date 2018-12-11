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
        IEnumerable<Product> GetAllAsync();

        Product GetByIdAsync(string productId);

        Product UpdateAsync(Product product);

        Task DeleteAsync(string productId);
    }
}
