using System.Net.Http;

namespace CookBook.Web.BL;

public class AuthenticatedHttpClient
{
    public HttpClient HttpClient { get; }

    public AuthenticatedHttpClient(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }
}
