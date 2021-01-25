using ApplicationCore.Dto;
using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly IAsyncRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserService(IAsyncRepository<User> userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UserDto> AuthenticateAsync(string username, string password)
        {
            var userSpec = new UserFilterSpecification(username, password);
            var user = await _userRepository.FirstOrDefaultAsync(userSpec);
            UserDto userDto = new UserDto();
            userDto = _mapper.Map(user, userDto);
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings").GetSection("Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role,"Admin")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration.GetSection("AppSettings").GetSection("Audience").Value,//<string>("AppSettings:Audience"),
                Issuer = _configuration.GetSection("AppSettings").GetSection("Issuer").Value,// _configuration.GetValue<string>("AppSettings:Issuer")
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userDto.Token = tokenHandler.WriteToken(token);
            return userDto;
        }

        public async Task<DatabaseResponse> CreateUserAsync(User user)
        {
            var result = await _userRepository.AddAsync(user);
            int status = 0;
            if (result.Id != 0)
            {
                status = (int)DbReturnValue.CreateSuccess;
            }
            //if user exists to do tripat
            // status = (int)DbReturnValue.RecordExists
            return new DatabaseResponse { ResponseCode = status };
        }

        public async Task<DatabaseResponse> DeleteUserAsync(int userId)
        {
            User user = await _userRepository.GetByIdAsync(userId);
            int affectedrows = await _userRepository.DeleteAsync(user);
            int status = 0;
            if (affectedrows > 0)
            {
                status = (int)DbReturnValue.DeleteSuccess;
            }
            else
            {
                status = (int)DbReturnValue.NotExists;
            }

            return new DatabaseResponse { ResponseCode = status };
        }

        public Task<DatabaseResponse> ForgotPasswordAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<DatabaseResponse> GetUserByIdAsync(int userId)
        {
            User user = await _userRepository.GetByIdAsync(userId);
            await _userRepository.UpdateAsync(user);
            int status = 0;
            if (user != null)
            {
                status = (int)DbReturnValue.RecordExists;
            }

            return new DatabaseResponse { ResponseCode = status, Results = user };
        }
        //todo for tripat
        public async Task<DatabaseResponse> GetUsersAsync(int? RoleId)
        {
            var userSpec = new UserFilterSpecification(RoleId);
            var users = await _userRepository.ListAsync(userSpec);
            int status = 0;
            status = (int)DbReturnValue.RecordExists;
            return new DatabaseResponse { ResponseCode = status, Results = users };
        }

        public async Task<DatabaseResponse> UpdateUserAsync(UserUpdateDto userDto, int userId)
        {
            User user = await _userRepository.GetByIdAsync(userId);
            var updateUser = _mapper.Map(userDto, user);
            await _userRepository.UpdateAsync(updateUser);
            int status = 0;
            status = (int)DbReturnValue.UpdateSuccess;

            return new DatabaseResponse { ResponseCode = status };

        }
    }
}
