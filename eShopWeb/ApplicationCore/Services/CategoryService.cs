using ApplicationCore.Dto;
using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public CategoryService(IAsyncRepository<Category> categoryRepository, IMapper mapper, IConfiguration configuration)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<DatabaseResponse> CreateCategoryAsync(Category category)
        {
            var result = await _categoryRepository.AddAsync(category);
            int status = 0;
            if (result.Id != 0)
            {
                status = (int)DbReturnValue.CreateSuccess;
            }
            //if category exists to do 
            // status = (int)DbReturnValue.RecordExists
            return new DatabaseResponse { ResponseCode = status };
        }

        public Task<DatabaseResponse> DeleteCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<DatabaseResponse> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.ListAllAsync();
            int status = 0;
            if (categories != null)
            {
                status = (int)DbReturnValue.RecordExists;
            }

            return new DatabaseResponse { ResponseCode = status, Results = categories };
        }

        public Task<DatabaseResponse> UpdateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
