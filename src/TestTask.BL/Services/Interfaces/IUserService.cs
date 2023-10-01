using TestTask.Domain.Models;

namespace TestTask.BL.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUser();

        public Task<IEnumerable<User>> GetUsers();
    }
}