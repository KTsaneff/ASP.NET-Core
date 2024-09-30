using System.Text.Json;

namespace WebApp_Development_dotNET_Eight.Data
{
    public class WebApiException : Exception
    {
        public ErrorResponse? ErrorResponse { get; set; }

        public WebApiException(string errorJson)
        {
            ErrorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorJson);
        }
    }
}
