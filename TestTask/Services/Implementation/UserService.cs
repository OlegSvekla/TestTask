using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestTask.Data.IRepository;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;


        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;          
        }

        public async Task<User> GetUser()
        {
            var userWithMostOrders = _userRepository
                .GetAllAsync()
                .OrderByDescending(user => user.Orders.Count)
                .FirstOrDefault();

            return userWithMostOrders;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var query = _userRepository.GetAllByAsync(include: null, expression: user => user.Status == UserStatus.Inactive);
            return await query;
        }
    }
}
