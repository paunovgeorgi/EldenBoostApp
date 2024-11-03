﻿using EldenBoost.Core.Contracts;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EldenBoost.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<string> GetUserNicknameAsync(string userId)
        {
            string? nickname = await repository.AllReadOnly<ApplicationUser>()
               .Where(u => u.Id == userId)
               .Select(u => u.Nickname)
               .FirstOrDefaultAsync();

            if (nickname == null)
            {
                return string.Empty;
            }
            return nickname;
        }
    }
}
