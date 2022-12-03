namespace CookBook.Web.BL
{
    public partial class IngredientApiClient
    {
        public IngredientApiClient(AuthenticatedHttpClient authenticatedHttpClient)
        {
            _httpClient = authenticatedHttpClient.HttpClient;
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
        }
    }
}
