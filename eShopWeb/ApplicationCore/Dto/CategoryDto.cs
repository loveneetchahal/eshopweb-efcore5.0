using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Dto
{
    public class CategoryDto
    {
        public string Text { get; set; }
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
    public class CategoryCreateDto
    {
        public string Text { get; set; }
        public int? CreatedBy { get; set; }
    }
    public class CategoryUpdateDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
