
using Api_Users.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Users.DAL.Context
{
    public class ApiUserContext : DbContext
    {
        public ApiUserContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
    }
}
