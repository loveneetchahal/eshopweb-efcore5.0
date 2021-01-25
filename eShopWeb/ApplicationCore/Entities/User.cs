using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class User:BaseEntity, IAggregateRoot
    {
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        [StringLength(50)]
        public string CountryCode { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        [StringLength(150)]
        public string Password { get; set; }
      //  public int RoleId { get; set; }
        public bool TwoFactorEnabled { get; set; }
        [StringLength(100)]
        public string CompanyName { get; set; }
        //public int CompanyTypeId { get; set; }
        public DateTime Lastlogin { get; set; }
        public int UserStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public int RoleId { get; set; }

    }
}
