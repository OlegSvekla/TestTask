using Microsoft.EntityFrameworkCore;
using TestTask.BL.Services.Interfaces;
using TestTask.Domain.Enums;
using TestTask.Domain.Models;
using TestTask.Infrastructure.Data.Repository.IRepository;

namespace TestTask.BL.Services.Implementation
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
            var userWithMostOrders = await _userRepository
                .GetOneByAsync(include: q => q.Include(u => u.Orders), 
                               orderBy: q => q.OrderByDescending(user => user.Orders.Count));

            return userWithMostOrders;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var query = _userRepository
                .GetAllByAsync(include: null, expression: user => user.Status == UserStatus.Inactive);

            return await query;
        }
    }
}