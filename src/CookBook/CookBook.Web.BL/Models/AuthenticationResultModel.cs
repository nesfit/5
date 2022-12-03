using Newtonsoft.Json;

namespace CookBook.Web.BL.Models;

public class AuthenticationResultModel
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }
}
