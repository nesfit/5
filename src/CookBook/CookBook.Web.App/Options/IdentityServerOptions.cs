namespace CookBook.Web.App.Options;

public record IdentityServerOptions
{
    public string TokenEndpointUrl { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}
