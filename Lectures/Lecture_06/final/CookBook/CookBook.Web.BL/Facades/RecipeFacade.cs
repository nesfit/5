﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookBook.Common.Models;

namespace CookBook.Web.BL.Facades
{
    public class RecipeFacade : FacadeBase<RecipeDetailModel, RecipeListModel>
    {
        private readonly IRecipeApiClient apiClient;

        public RecipeFacade(IRecipeApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<ICollection<RecipeListModel>> GetAllAsync()
        {
            return await apiClient.RecipeGetAsync(culture);
        }

        public async Task<RecipeDetailModel> GetByIdAsync(Guid id)
        {
            return await apiClient.RecipeGetAsync(id, culture);
        }

        public async Task DeleteAsync(Guid id)
        {
            await apiClient.RecipeDeleteAsync(id, culture);
        }

        public async Task SaveAsync(RecipeDetailModel data)
        {
            await apiClient.UpsertAsync(culture, data);
        }
    }
}
