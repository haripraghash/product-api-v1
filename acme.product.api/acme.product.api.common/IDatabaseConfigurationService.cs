namespace acme.product.api.common
{
    public interface IDatabaseConfigurationService
    {
        string CosmosDbName { get; }

        string ProductCollectionName { get; }

        string ConnectionString { get; }
    }
}