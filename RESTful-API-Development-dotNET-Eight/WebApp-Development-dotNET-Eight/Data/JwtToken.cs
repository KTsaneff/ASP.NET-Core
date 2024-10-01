using Newtonsoft.Json;

namespace WebApp_Development_dotNET_Eight.Data
{
    public class JwtToken
    {
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }
    }
}
