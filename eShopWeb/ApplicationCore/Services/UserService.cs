using ApplicationCore.Dto;
using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly IAsyncRepository<User> _userRepository;
        public UserService(IAsyncRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<DatabaseResponse> CreateUserAsync(User user)
        {
            var result =  await _userRepository.AddAsync(user);
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
            await _userRepository.DeleteAsync(user);
            int status = 0;
            status = (int)DbReturnValue.DeleteSuccess;

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

            return new DatabaseResponse { ResponseCode = status,Results = user };
        }
        //todo for tripat
        public async Task<DatabaseResponse> GetUsersAsync(int? RoleId)
        {
            var users = await _userRepository.ListAllAsync();
            throw new NotImplementedException();
        }

        public async Task<DatabaseResponse> UpdateUserAsync(User user1, int userId)
        {
            User user = await _userRepository.GetByIdAsync(userId);
            await _userRepository.UpdateAsync(user);
            int status = 0;
            status = (int)DbReturnValue.UpdateSuccess;

            return new DatabaseResponse { ResponseCode = status };

        }
    }
}
