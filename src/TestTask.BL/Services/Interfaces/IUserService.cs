using TestTask.Domain.Entities;

namespace TestTask.BL.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUser();

        public Task<IEnumerable<User>> GetUsers();
    }
}