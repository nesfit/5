using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using CookBook.Web.App.Options;
using CookBook.Web.BL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CookBook.Web.App.Pages;

public partial class LoginPage
{
    [Inject]
    public HttpClient HttpClient { get; set; } = null!;

    [Inject]
    public ILocalStorageService LocalStorageService { get; set; } = null!;

    [Inject]
    public IOptions<IdentityServerOptions> IdentityServerOptions { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; }
    
    private LoginModel LoginModel { get; set; } = new();

    public string ErrorMessage { get; set; }

    private async Task LoginAsync()
    {
        using var authenticationRequest = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
        {
            new ("grant_type", "password"),
            new ("scope", "cookbook_api offline_access"),
            new ("username", LoginModel.Username),
            new ("password", LoginModel.Password),
            new ("client_id", IdentityServerOptions.Value.ClientId),
            new ("client_secret", IdentityServerOptions.Value.ClientSecret),
        });

        var response = await HttpClient.PostAsync(IdentityServerOptions.Value.TokenEndpointUrl, authenticationRequest);

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            ErrorMessage = "Wrong username or password";
        }
        else
        {
            using var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync());
            var responseContent = await streamReader.ReadToEndAsync();
            var authenticationResult = JsonConvert.DeserializeObject<AuthenticationResultModel>(responseContent);
            if (authenticationResult is not null)
            {
                HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authenticationResult.AccessToken}");

                await LocalStorageService.SetItemAsStringAsync("access-token", authenticationResult.AccessToken);
                await LocalStorageService.SetItemAsStringAsync("refresh-token", authenticationResult.RefreshToken);
            }

            NavigationManager.NavigateTo("/");
        }
    }
}
