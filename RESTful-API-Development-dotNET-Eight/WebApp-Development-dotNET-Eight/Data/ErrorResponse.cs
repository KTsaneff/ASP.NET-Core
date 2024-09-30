using System.Text.Json.Serialization;

namespace WebApp_Development_dotNET_Eight.Data
{
    public class ErrorResponse
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("errors")]
        public Dictionary<string, List<string>>? Errors { get; set; }
    }
}
