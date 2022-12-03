using System;
using System.Globalization;
using AutoMapper.Internal;
using Blazored.LocalStorage;
using CookBook.Common.Extensions;
using CookBook.Web.App;
using CookBook.Web.App.Options;
using CookBook.Web.BL;
using CookBook.Web.BL.Extensions;
using CookBook.Web.BL.Installers;
using CookBook.Web.BL.Options;
using CookBook.Web.DAL.Installers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

const string defaultCultureString = "cs";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");

builder.Services.AddInstaller<WebDALInstaller>();
builder.Services.AddInstaller<WebBLInstaller>();

builder.Services.AddTransient<AuthenticationHeaderHandler>();

var apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl") ?? string.Empty;

builder.Services.AddHttpClient<AuthenticatedHttpClient>()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<AuthenticationHeaderHandler>();

builder.Services.AddAutoMapper(configuration =>
    {
        // This is a temporary fix - should be able to remove this when version 11.0.2 comes out
        // More information here: https://github.com/AutoMapper/AutoMapper/issues/3988
        configuration.Internal().MethodMappingEnabled = false;
    }, typeof(WebBLInstaller));
builder.Services.AddLocalization();

builder.Services.Configure<LocalDbOptions>(options =>
{
    options.IsLocalDbEnabled = bool.Parse(builder.Configuration.GetSection(nameof(LocalDbOptions))[nameof(LocalDbOptions.IsLocalDbEnabled)]);
});

builder.Services.Configure<IdentityServerOptions>(options => builder.Configuration.GetSection("IdentityServer").Bind(options));
builder.Services.AddBlazoredLocalStorage();

var host = builder.Build();

var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();
var cultureString = (await jsRuntime.InvokeAsync<string>("blazorCulture.get"))
                    ?? defaultCultureString;

var culture = new CultureInfo(cultureString);
await jsRuntime.InvokeVoidAsync("blazorCulture.set", cultureString);

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();
