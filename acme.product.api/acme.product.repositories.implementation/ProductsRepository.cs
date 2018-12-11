using acme.product.models;
using acme.product.repositories.interfaces;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme.product.repositories.implementation
{
    public class ProductsRepository : IProductsRepository
    {
        private CosmosContainer _cosmosContainer;

        public ProductsRepository(CosmosContainer cosmosContainer)
        {
            this._cosmosContainer = cosmosContainer;
        }

        public async Task DeleteAsync(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException("productId");
            }

            await this._cosmosContainer.Items.DeleteItemAsync<Product>(null, productId);
        }

        public IEnumerable<Product> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Product GetByIdAsync(string productId)
        {
            throw new NotImplementedException();
        }

        public Product UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
