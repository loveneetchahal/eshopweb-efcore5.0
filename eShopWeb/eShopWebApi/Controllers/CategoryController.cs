using ApplicationCore.Dto;
using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using AutoMapper;
using eShopWebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        IConfiguration _iconfiguration;
        public CategoryController(ICategoryService categoryService, IMapper mapper, IConfiguration configuration)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _iconfiguration = configuration;
        }
        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="category">Category object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryCreateDto category)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            }
            var addToCategory = _mapper.Map<Category>(category);

            DatabaseResponse response = await _categoryService.CreateCategoryAsync(addToCategory);

            if (response.ResponseCode == (int)DbReturnValue.CreateSuccess)
            {
                return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.CreateSuccess));
            }
            else
            {
                return Ok(ApiResponse.OkResult(false, response.Results, DbReturnValue.RecordExists));
            }

        }
        /// <summary>
        /// Update a category
        /// </summary>
        /// <param name="category">Updated Object</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CategoryUpdateDto category)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            }
            var updateCategory = _mapper.Map<Category>(category);


            DatabaseResponse response = await _categoryService.UpdateCategoryAsync(updateCategory);

            if (response.ResponseCode == (int)DbReturnValue.UpdateSuccess)
            {
                return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.UpdateSuccess));
            }
            else if (response.ResponseCode == (int)DbReturnValue.RecordExists)
            {
                return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.RecordExists));
            }
            else
            {
                return Ok(ApiResponse.OkResult(false, response.Results, DbReturnValue.NotExists));
            }

        }
        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="id">CategoryId</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            }

            DatabaseResponse response = await _categoryService.DeleteCategoryAsync(id);

            if (response.ResponseCode == (int)DbReturnValue.DeleteSuccess)
            {
                return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.DeleteSuccess));
            }
            else
            {
                return Ok(ApiResponse.OkResult(false, response.Results, DbReturnValue.NotExists));
            }

        }
       // [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            }
            DatabaseResponse response = await _categoryService.GetCategoriesAsync();

            if (response.ResponseCode == (int)DbReturnValue.RecordExists)
            {
                return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.RecordExists));

            }
            else
            {
                return Ok(ApiResponse.OkResult(false, response.Results, DbReturnValue.NotExists));
            }

        }
    }
}
