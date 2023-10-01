using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTask.BL.Exceptions;
using TestTask.BL.Services.Interfaces;
using TestTask.Domain.Entities;
using TestTask.Domain.Enums;
using TestTask.Infrastructure.Data.Repository.IRepository;

namespace TestTask.BL.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IRepository<User> userRepository,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;    
            _logger = logger;
        }

        public async Task<User> GetUser()
        {
            _logger.LogInformation("Getting User.");

            var userWithMostOrders = await _userRepository
                .GetOneByAsync(include: q => q.Include(u => u.Orders), 
                               orderBy: q => q.OrderByDescending(user => user.Orders.Count));
            if (userWithMostOrders is null)
            {
                _logger.LogWarning("User not found.");

                throw new UserNotFoundException($"The base doesn't contain the user.");
            }

            _logger.LogInformation("Retrieved user successfully.");

            return userWithMostOrders;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            _logger.LogInformation("Getting users.");

            var usersInactive = _userRepository
                .GetAllByAsync(include: null, expression: user => user.Status == UserStatus.Inactive);
            if (usersInactive is null)
            {
                _logger.LogWarning("Users not found.");

                throw new UserNotFoundException($"The base doesn't contain any users.");
            }

            _logger.LogInformation("Retrieved all nacessary users successfully.");

            return await usersInactive;
        }
    }
}