using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Dto
{
    public class LoginUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public int? RoleId { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public int? CompanyTypeId { get; set; }
        public string CompanyName { get; set; }
        public string Photo { get; set; }
        public DateTime? Lastlogin { get; set; }
        public string UserStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? PlanId { get; set; }
        public string RoleName { get; set; }
        public string CompanyType { get; set; }
        public string Token { get; set; }

    }
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public int RoleId { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string CompanyName { get; set; }
        public int? CompanyTypeId { get; set; }
        public DateTime? Lastlogin { get; set; }
        public string UserStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public string RoleName { get; set; }
        public string Photo { get; set; }
        public int? PlanId { get; set; }
        public string CompanyType { get; set; }
        public string Token { get; set; }

    }
    public class UserCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int TwoFactorEnabled { get; set; }
        public string CompanyName { get; set; }
        public string Photo { get; set; }
        public int? PlanId { get; set; }
        public int? CompanyTypeId { get; set; }
        public int? UserStatus { get; set; }
        public int? CreatedBy { get; set; }
    }
    public class UserUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public int RoleId { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string CompanyName { get; set; }
        public string Photo { get; set; }
        public int? CompanyTypeId { get; set; }
        public int UserStatus { get; set; }
        public int ModifiedBy { get; set; }
        public int? PlanId { get; set; }
        public int UserId { get; set; }
    }
    public class UserPasswordDto
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
