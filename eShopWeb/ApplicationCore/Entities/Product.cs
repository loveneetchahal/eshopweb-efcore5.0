using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
   public class Product:BaseEntity, IAggregateRoot
    {
        [Required]
        [StringLength(100)]
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
        public ICollection<ProductImage> ProductImages { get; set; }
        public Category Category { get; set; }
    }
}
