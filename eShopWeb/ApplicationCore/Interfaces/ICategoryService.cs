using ApplicationCore.Dto;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICategoryService
    {
        Task<DatabaseResponse> CreateCategoryAsync(Category category);
        Task<DatabaseResponse> UpdateCategoryAsync(Category category);
        Task<DatabaseResponse> DeleteCategoryAsync(int categoryId);
        Task<DatabaseResponse> GetCategoriesAsync();
    }
}
