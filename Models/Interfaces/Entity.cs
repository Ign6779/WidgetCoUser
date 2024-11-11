using Newtonsoft.Json;

namespace WidgetCoUser.Models.Interfaces
{
    public class Entity : IEntity
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
    }
}