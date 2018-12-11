using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace acme.product.repositories.implementation
{
    public interface IProductDatabaseInitializer
    {
        Task<CosmosContainer> CreateOrGetProuctsCollectionAsync();
    }
}