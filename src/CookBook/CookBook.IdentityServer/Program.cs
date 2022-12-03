using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.EF;
using Duende.IdentityServer;
using Duende.IdentityServer.AspNetIdentity;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<CookBookDbContext>(options => options.UseSqlServer(
    connectionString: builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUserEntity, AppRoleEntity>()
    .AddEntityFrameworkStores<CookBookDbContext>();

builder.Services.AddIdentityServer()
    .AddInMemoryApiScopes(builder.Configuration.GetSection("Identity:ApiScopes"))
    .AddInMemoryClients(builder.Configuration.GetSection("Identity:Clients"))
    .AddAspNetIdentity<AppUserEntity>()
    .AddProfileService<ProfileService<AppUserEntity>>();

builder.Services.AddAuthentication()
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, "CookBook IdentityServer", options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        options.SignOutScheme = IdentityServerConstants.SignoutScheme;
        options.SaveTokens = true;

        options.Authority = "https://localhost:7279";
        options.ClientId = "cookbook_identity";
        options.ClientSecret = "cookbook_identity_secret";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name",
            RoleClaimType = "role"
        };
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(o =>
        o.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();
app.MapRazorPages();
app.MapGet("test", () => "test is fine");

app.Run();
