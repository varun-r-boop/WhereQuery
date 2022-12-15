using Final.Entities;
using System.Text.Json.Serialization;

namespace Final.Model.Auth
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }

        public string? userName { get; set; }

        public string? email { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role customerRole { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Role providerRole { get; set; }
        public string? Token { get; set; }
    }
}
