using Microsoft.Extensions.Configuration;

namespace WidgetCoUser
{
    public class FunctionConfiguration
    {
        public string CosmosAccountEndpoint { get; }
        public string CosmosAccountKey { get; }
        public string CosmosDatabaseName { get; }

        public FunctionConfiguration(IConfiguration configuration)
        {
            CosmosAccountEndpoint = configuration["CosmosAccountEndpoint"];
            CosmosAccountKey = configuration["CosmosAccountKey"];
            CosmosDatabaseName = configuration["CosmosDatabaseName"];
        }
    }
}