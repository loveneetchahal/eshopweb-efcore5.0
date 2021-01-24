using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Role:BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
