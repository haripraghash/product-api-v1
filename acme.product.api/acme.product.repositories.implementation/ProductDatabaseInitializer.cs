using acme.product.api.common;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace acme.product.repositories.implementation
{
    public class ProductDatabaseInitializer : IProductDatabaseInitializer
    {
        private readonly IDatabaseConfigurationService _databaseConfigurationService;
        private CosmosDatabase _productDatabase;

        public ProductDatabaseInitializer(IDatabaseConfigurationService databaseConfigurationService)
        {
            this._databaseConfigurationService = databaseConfigurationService;
        }

        public async Task<CosmosContainer> CreateOrGetProuctsCollectionAsync()
        {
            if(this._productDatabase == null)
            {
                await this.CreateCosmosDatabaseAsync();
            }

            return await this._productDatabase.Containers
                .CreateContainerIfNotExistsAsync(
                    new CosmosContainerSettings() {
                        Id = this._databaseConfigurationService.ProductCollectionName
                    },
                    throughput: 400);
        }

        private async Task CreateCosmosDatabaseAsync()
        {
            //TODO - Concurreny
            var cosmosClient = new CosmosClient(this._databaseConfigurationService.ConnectionString);
            this._productDatabase = await cosmosClient.Databases.CreateDatabaseIfNotExistsAsync(this._databaseConfigurationService.CosmosDbName, 1000);
        }
    }
}
