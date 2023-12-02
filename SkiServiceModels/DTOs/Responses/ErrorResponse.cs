using System.Net;

namespace SkiServiceModels.DTOs.Responses
{
    /// <summary>
    /// Primarily used for parsing error messages from the API. (on the client)
    /// </summary>
    public class ErrorResponse
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
