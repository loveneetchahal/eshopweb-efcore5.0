using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class ProductImage:BaseEntity, IAggregateRoot
    {
        [StringLength(500)]
        public string ImageUrl { get; set; }
        public int ProductId { get; set; }
    }
}
