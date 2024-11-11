using WidgetCoUser.Models.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WidgetCoUser.Models
{
    public class User : Entity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}