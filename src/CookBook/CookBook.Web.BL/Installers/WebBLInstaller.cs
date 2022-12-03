using CookBook.Common.BL.Facades;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Web.BL.Installers
{
    public class WebBLInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRecipeApiClient, RecipeApiClient>();
            serviceCollection.AddTransient<IIngredientApiClient, IngredientApiClient>();

            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<WebBLInstaller>()
                    .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());
        }
    }
}
