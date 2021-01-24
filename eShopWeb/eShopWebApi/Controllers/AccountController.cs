using ApplicationCore.Common;
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
   // [Authorize(Roles = "client,vendor,admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
       // private readonly IEmailSender _emailSender;
        IConfiguration _iconfiguration;
        public AccountController(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _iconfiguration = configuration;
            //_emailSender = emailSender;
        }
        #region Methods
       // [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserCreateDto user)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            }
            var addUser = _mapper.Map<User>(user);
            if (!string.IsNullOrEmpty(addUser.Password))
            {
                addUser.Password = Cryptography.Encrypt(user.Password);
            }
            DatabaseResponse response = await _userService.CreateUserAsync(addUser);

            if (response.ResponseCode == (int)DbReturnValue.CreateSuccess)
            {
                return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.CreateSuccess));
            }
            else
            {
                return Ok(ApiResponse.OkResult(false, response.Results, DbReturnValue.RecordExists));
            }

        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UserUpdateDto user)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            }
            var updateUser = _mapper.Map<User>(user);

            DatabaseResponse response = await _userService.UpdateUserAsync(updateUser,updateUser.Id);

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

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int userId)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            }
            DatabaseResponse response = await _userService.GetUserByIdAsync(userId);

            if (response.ResponseCode == (int)DbReturnValue.RecordExists)
            {
                return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.RecordExists));

            }
            else
            {
                return Ok(ApiResponse.OkResult(false, response.Results, DbReturnValue.NotExists));
            }

        }

      //  [Authorize(Roles = "admin")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            }
            DatabaseResponse response = await _userService.GetUsersAsync(id);

            if (response.ResponseCode == (int)DbReturnValue.RecordExists)
            {
                return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.RecordExists));

            }
            else
            {
                return Ok(ApiResponse.OkResult(false, response.Results, DbReturnValue.NotExists));
            }

        }

        [HttpDelete]
       // [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAsync(int userId)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            }

            DatabaseResponse response = await _userService.DeleteUserAsync(userId);

            if (response.ResponseCode == (int)DbReturnValue.DeleteSuccess)
            {
                return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.DeleteSuccess));
            }
            else
            {
                return Ok(ApiResponse.OkResult(false, response.Results, DbReturnValue.NotExists));
            }

        }
      //  [AllowAnonymous]
        //[HttpPost("authenticate")]
        //public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        //{
        //    var user = await _userService.AuthenticateAsync(model.Username, Cryptography.Encrypt(model.Password));

        //    if (user == null)
        //        return BadRequest(new { message = "Username or password is incorrect" });

        //    return Ok(user);
        //}
     //   [HttpPut("changepassword")]
        //public async Task<IActionResult> ChangePasswordAsync([FromBody] UserPasswordDto user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Ok(ApiResponse.ValidationErrorResponse(ModelState));
        //    }
        //    else if (user.NewPassword != user.ConfirmPassword)
        //    {
        //        return Ok(ApiResponse.OkResult(false, null, DbReturnValue.PassNotMatch));
        //    }
        //    if (!string.IsNullOrEmpty(user.NewPassword) && !string.IsNullOrEmpty(user.CurrentPassword))
        //    {
        //        user.NewPassword = Cryptography.Encrypt(user.NewPassword);
        //        user.CurrentPassword = Cryptography.Encrypt(user.CurrentPassword);
        //    }
        //    DatabaseResponse response = await _userService.ChangePasswordAsync(user);

        //    if (response.ResponseCode == (int)DbReturnValue.UpdateSuccess)
        //    {
        //        return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.UpdateSuccess));
        //    }
        //    else
        //    {
        //        return Ok(ApiResponse.OkResult(false, response.Results, DbReturnValue.PassNotMatch));
        //    }

        //}

       // [AllowAnonymous]
        //[HttpPost("ForgotPassword")]
        //public async Task<IActionResult> ForgotPassword([FromBody] string email)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            new OperationResponse
        //            {
        //                HasSucceeded = false,
        //                IsDomainValidationErrors = true,
        //                StatusCode = ((int)ResponseStatus.BadRequest).ToString(),
        //                Message = string.Join("; ", ModelState.Values
        //                                         .SelectMany(x => x.Errors)
        //                                         .Select(x => x.ErrorMessage))
        //            };
        //        }
        //        DatabaseResponse response = await _userService.CheckValidUserAsync(email);

        //        if (response.ResponseCode == (int)DbReturnValue.RecordExists)
        //        {
        //            return Ok(new OperationResponse
        //            {
        //                HasSucceeded = true,
        //                IsDomainValidationErrors = false,
        //                Message = EnumExtensions.GetDescription(DbReturnValue.EmailSent),
        //                ReturnedObject = response.Results
        //            });
        //        }
        //        else
        //        {

        //            return Ok(new OperationResponse
        //            {
        //                HasSucceeded = false,
        //                IsDomainValidationErrors = false,
        //                Message = EnumExtensions.GetDescription(DbReturnValue.NotExists),
        //                ReturnedObject = response.Results
        //            });
        //        }

        //    }
        //    finally { }

        //}
        /// <summary>
        /// Reset Password of user
        /// </summary>
        /// <param name="resetPassword"></param>
        /// <returns></returns>
      //  [AllowAnonymous]
        //[HttpPut("ResetPassword")]
        //public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Ok(ApiResponse.ValidationErrorResponse(ModelState));
        //    }
        //    else if (resetPassword.Password != resetPassword.ConfirmPassword)
        //    {

        //        return Ok(ApiResponse.OkResult(false, null, DbReturnValue.PassNotMatch));
        //    }

        //    DatabaseResponse response = await _userService.ResetPasswordAsync(resetPassword);

        //    if (response.ResponseCode == (int)DbReturnValue.UpdateSuccess)
        //    {
        //        return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.UpdateSuccess));
        //    }
        //    else
        //    {

        //        return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.UsernameNotExist));
        //    }
        //}
        #endregion
        #region Private Methods
        //private EmailAddressDto CreateEmailObject(string userName, string email, string subject, string url, string emailBody,string emailType = "")
        //{
        //    string body = this.CreateEmailBody(emailType, userName, email, subject, url, emailBody);

        //    EmailAddressDto message = new EmailAddressDto();
        //    message.Subject = subject;

        //    // TO EMAIL ADDRESS INFO
        //    EmailAddress emailAddress = new EmailAddress();
        //    emailAddress.Name = userName;
        //    emailAddress.Address = email;
        //    List<EmailAddress> emailAddresses = new List<EmailAddress>();
        //    emailAddresses.Add(emailAddress);
        //    message.ToAddresses = emailAddresses;

        //    // FROM EMAIL ADDRESS INFO
        //    List<EmailAddress> emailAddresses1 = new List<EmailAddress>();
        //    EmailAddress emailAddress1 = new EmailAddress();
        //    emailAddress1.Name = "Tramatix Support";
        //    emailAddress1.Address = _iconfiguration.GetSection("Smtp").GetSection("emailusername").Value;
        //    List<EmailAddress> emailAddresseslist = new List<EmailAddress>();
        //    emailAddresseslist.Add(emailAddress1);
        //    message.MessageBody = body;
        //    message.FromAddresses = emailAddresseslist;
        //    return message;

        //}
        //private string CreateEmailBody(string emailType, string userName, string email, string subject, string url, string emailBody)
        //{
        //    string body = string.Empty;
        //    string EmailTemplate = string.Empty;

        //    switch (emailType)
        //    {
        //        case "forAction":

        //            EmailTemplate = "./EmailTemplates/forAction.html";
        //            break;

        //        case "forInfo":
        //            EmailTemplate = "./EmailTemplates/forInfo.html";
        //            break;

        //        default:
        //            EmailTemplate = "./EmailTemplates/forAction.html";
        //            break;
        //    }

        //    using (StreamReader reader = new StreamReader(EmailTemplate))
        //    {
        //        body = reader.ReadToEnd();
        //    }


        //    body = body.Replace("{UserName}", userName).Replace("{title}", subject).
        //        Replace("{URL}", url)
        //        .Replace("{Text1}", emailBody)
        //         .Replace(" {buttonText}", "click here");

        //    return body;

        //}
        #endregion

     //   [AllowAnonymous]
        //[HttpPost("ConfirmVerifyToken")]
        //public async Task<IActionResult> ConfirmVerificationToken([FromBody] string token)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        new OperationResponse
        //        {
        //            HasSucceeded = false,
        //            IsDomainValidationErrors = true,
        //            StatusCode = ((int)ResponseStatus.BadRequest).ToString(),
        //            Message = string.Join("; ", ModelState.Values
        //                                     .SelectMany(x => x.Errors)
        //                                     .Select(x => x.ErrorMessage))
        //        };
        //    }


        //    var response = await _userService.ConfirmVerificationToken(token);

        //    if (response.ResponseCode == (int)DbReturnValue.UpdateSuccess)
        //    {
        //        return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.UpdateSuccess));
        //    }
        //    else
        //    {
        //        return Ok(ApiResponse.OkResult(false, response.Results, DbReturnValue.TokenExpired));
        //    }


        //}

        //[HttpPost("ActivateEmail")]
        //public async Task<IActionResult> ActivateEmail([FromBody] VerificationTokenDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        new OperationResponse
        //        {
        //            HasSucceeded = false,
        //            IsDomainValidationErrors = true,
        //            StatusCode = ((int)ResponseStatus.BadRequest).ToString(),
        //            Message = string.Join("; ", ModelState.Values
        //                                     .SelectMany(x => x.Errors)
        //                                     .Select(x => x.ErrorMessage))
        //        };
        //    }

        //    var response = await _userService.ActivateEmail(model);

        //    if (response.ResponseCode == (int)DbReturnValue.EmailSent)
        //    {
        //        return Ok(ApiResponse.OkResult(true, response.Results, DbReturnValue.EmailSent));
        //    }
        //    else
        //    {
        //        return Ok(ApiResponse.OkResult(false, response.Results, DbReturnValue.EmailNotSent));
        //    }


        //}

        //[HttpPost("uploadProfilePhoto")]
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

        //    ext = file.FileName.Split(".")[1];

        //    ImageFileName = string.Concat(ImageFileName, ".", ext);

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

    }
}
