using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Dto
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public int? AvailableQty { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public int? Discount { get; set; }
        public int? ProductTypeId { get; set; }
        public string ProductStatus { get; set; }
        public int? VendorId { get; set; }
        public int? ProductId { get; set; }
        public List<ProductImagesDto> ProductImages { get; set; }
        public string VendorName { get; set; }

        public string Category { get; set; }

        public string ProductType { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class ProductImagesDto
    {
        public string ImageUrl { get; set; }
        public int? ProductId { get; set; }
    }
    public class ProductCreateDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Text length should not exceed 100 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(3999, ErrorMessage = "Text length should not exceed 3999 characters")]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int AvailableQty { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }

        public int Discount { get; set; }
        [Required]
        public int ProductTypeId { get; set; }
        public int ProductStatus { get; set; }
        public int? VendorId { get; set; }
        public int? CreatedBy { get; set; }
        public List<ProductImagesDto> ProductImages { get; set; }
    }
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int AvailableQty { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int Discount { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductStatus { get; set; }
        public int? VendorId { get; set; }
        public int? ModifiedBy { get; set; }
        public int ProductId { get; set; }
        public string ImageurlJson { get; set; }
        public List<ProductImagesDto> ProductImages { get; set; }
    }
}
