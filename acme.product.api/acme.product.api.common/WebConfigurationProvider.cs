using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme.product.api.common
{
    public interface IConfigurationProvider
    {
        T GetValue<T>(string key);
    }

    public class WebConfigurationProvider : IConfigurationProvider
    {
        public T GetValue<T>(string key)
        {
            //TODO - Cache the configs
            var value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception($"Configuration value or key missing for key {key}");
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
