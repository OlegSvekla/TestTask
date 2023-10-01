using TestTask.Models;

namespace TestTask.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUser();

        public Task<IEnumerable<User>> GetUsers();
    }
}