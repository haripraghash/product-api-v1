using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme.product.api.common
{
    public class DatabaseConfigurationService : IDatabaseConfigurationService
    {
        private IConfigurationProvider _configurationProvider;

        public DatabaseConfigurationService(IConfigurationProvider configurationProvider)
        {
            this._configurationProvider = configurationProvider;
        }

        public string CosmosDbName
        {
            get
            {
                return this._configurationProvider.GetValue<string>("CosmosDbName");
            }
        }

        public string ProductCollectionName
        {
            get
            {
                return this._configurationProvider.GetValue<string>("ProductCollectionName");
            }
        }

        public string ConnectionString
        {
            get
            {
                return this._configurationProvider.GetValue<string>("ConnectionString");
            }
        }
    }
}
