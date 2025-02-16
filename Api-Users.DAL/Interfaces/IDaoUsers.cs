
using Api_Users.DAL.Entities;

namespace Api_Users.DAL.Interfaces
{
    public interface IDaoUsers
    {
        Task<List<Users>> GetAllAsync();
        Task<Users> GetByIdAsync(int id);
        Task CreateAsync(Users user);
        Task UpdateAsync(Users user);
        Task DeleteAsync(int id);
    }
}
