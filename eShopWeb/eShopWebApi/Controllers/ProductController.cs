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
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        IConfiguration _iconfiguration;
        public ProductController(IProductService productService, IMapper mapper, IConfiguration configuration)
        {
            _productService = productService;
            _mapper = mapper;
            _iconfiguration = configuration;
        }

       // [Authorize(Roles = "admin,vendor")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductCreateDto product)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            }
            var addToProduct = _mapper.Map<Product>(product);

            DatabaseResponse response = await _productService.CreateProductAsync(addToProduct);

            if (response.ResponseCode == (int)DbReturnValue.CreateSuccess)
            {
                return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.CreateSuccess));
            }
            else
            {
                return Ok(ApiResponse.OkResult(false, response.Results, DbReturnValue.RecordExists));
            }

        }

        //[Authorize(Roles = "admin,vendor")]
        //[HttpPost("BulkInsert")]
        //public async Task<IActionResult> PostBulkProductsAsync([FromBody] List<ProductCreateDto> products)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Ok(ApiResponse.ValidationErrorResponse(ModelState));
        //    }

        //    DatabaseResponse response = await _productService.CreateProductsAsync(products);

        //    if (response.ResponseCode == (int)DbReturnValue.CreateSuccess)
        //    {
        //        return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.CreateSuccess));
        //    }
        //    else
        //    {
        //        return Ok(ApiResponse.OkResult(false, response.Results, DbReturnValue.RecordExists));
        //    }

        //}

       // [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] ProductUpdateDto product)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            }
            var updateProduct = _mapper.Map<Product>(product);

            DatabaseResponse response = await _productService.UpdateProductAsync(updateProduct);

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

        //[Authorize(Roles = "admin,vendor")]
        //[HttpPost("uploadfile")]
        //public async Task<UploadResponse> UploadFile(IFormFile file, string type)
        //{
        //    string ext = string.Empty;

        //    // SETTING FILE NAME
        //    string ImageFileName = Guid.NewGuid().ToString();

        //    if (file == null)
        //        return new UploadResponse()
        //        {
        //            HasSucceed = false,
        //            FileName = null,
        //            Message = "File is empty"

        //        };
        //    else if (!new CommonHelper().IsSupportedContentType_Images(file.ContentType))
        //    {
        //        return new UploadResponse()
        //        {
        //            HasSucceed = false,
        //            FileName = null,
        //            Message = "unsupported file type"

        //        };
        //    }

        //    AWSS3Config aWSS3Config = new AWSS3Config();
        //    aWSS3Config.AWSAccessKey = _iconfiguration.GetValue<string>("AwsS3:accessKey");
        //    aWSS3Config.AWSSecretKey = _iconfiguration.GetValue<string>("AwsS3:accessSecret");
        //    aWSS3Config.AWSBucketName = _iconfiguration.GetValue<string>("AwsS3:bucket_" + type);
        //    AmazonS3 amazonS3 = new AmazonS3(aWSS3Config);
        //    UploadResponse response = await amazonS3.UploadFile(file, _iconfiguration.GetValue<string>("AwsS3:subfolder_" + type) + "/" + ImageFileName);
        //    response.FileUrl = _iconfiguration.GetValue<string>("AwsS3:baseUrl") + response.FileName;
        //    response.FileName = ImageFileName;
        //    return response;
        //}

       // [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int productId)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            }
            DatabaseResponse response = await _productService.GetProductByIdAsync(productId);

            if (response.ResponseCode == (int)DbReturnValue.RecordExists)
            {
                return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.RecordExists));

            }
            else
            {
                return Ok(ApiResponse.OkResult(false, response.Results, DbReturnValue.NotExists));
            }

        }

     //   [Authorize(Roles = "admin,vendor")]
        [HttpGet("ByVendorId")]
        public async Task<IActionResult> GetByVendorIdAsync(int vendorId)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            }
            DatabaseResponse response = await _productService.GetProductByVendorIdAsync(vendorId);

            if (response.ResponseCode == (int)DbReturnValue.RecordExists)
            {
                return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.RecordExists));

            }
            else
            {
                return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.NotExists));
            }

        }


      //  [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            }
            DatabaseResponse response = await _productService.GetProductsAsync();

            if (response.ResponseCode == (int)DbReturnValue.RecordExists)
            {
                return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.RecordExists));

            }
            else
            {
                return Ok(ApiResponse.OkResult(false, response.Results, DbReturnValue.NotExists));
            }

        }

        //[Authorize(Roles = "admin")]
        //[HttpDelete]
        //public async Task<IActionResult> Delete(string ids)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Ok(ApiResponse.ValidationErrorResponse(ModelState));
        //    }

        //    DatabaseResponse response = await _productService.DeleteProductAsync(ids);

        //    if (response.ResponseCode == (int)DbReturnValue.DeleteSuccess)
        //    {
        //        List<ProductImagesDto> images = (List<ProductImagesDto>)response.Results;
        //        string[] values = ids.Split(',');
        //        for (int i = 0; i < values.Length; i++)
        //        {
        //            values[i] = values[i].Trim();
        //            await CleanUpFiles("products", images);
        //        }
        //        return Ok(ApiResponse.OkResult(true, null, DbReturnValue.DeleteSuccess));
        //    }
        //    else if (response.ResponseCode == (int)DbReturnValue.ActiveTryDelete)
        //    {
        //        return Ok(ApiResponse.OkResult(true, null, DbReturnValue.ActiveTryDelete));
        //    }
        //    else
        //    {
        //        return Ok(ApiResponse.OkResult(true, null, DbReturnValue.NotExists));
        //    }

        //}
        //private async Task<bool> CleanUpFiles(string type, List<ProductImagesDto> imagesDto)
        //{
        //    AWSS3Config aWSS3Config = new AWSS3Config();
        //    aWSS3Config.AWSAccessKey = _iconfiguration.GetValue<string>("AwsS3:accessKey");
        //    aWSS3Config.AWSSecretKey = _iconfiguration.GetValue<string>("AwsS3:accessSecret");
        //    aWSS3Config.AWSBucketName = _iconfiguration.GetValue<string>("AwsS3:bucket_" + type);
        //    string subfolder = _iconfiguration.GetValue<string>("AwsS3:subfolder_products");
        //    AmazonS3 amazonS3 = new AmazonS3(aWSS3Config);
        //    foreach (ProductImagesDto image in imagesDto)
        //    {
        //        string filepath = image.ImageUrl;
        //        Uri uri = new Uri(filepath);
        //        string filename = System.IO.Path.GetFileName(uri.LocalPath);
        //        UploadResponse response = await amazonS3.RemoveUploadedFile(subfolder + "/" + filename);
        //    }
        //    return true;
        //}

    }
}
