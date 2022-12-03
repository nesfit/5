namespace CookBook.Web.BL;

public partial class RecipeApiClient
{
    public RecipeApiClient(AuthenticatedHttpClient authenticatedHttpClient)
    {
        _httpClient = authenticatedHttpClient.HttpClient;
        _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
    }
}
