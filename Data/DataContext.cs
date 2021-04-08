using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : IdentityDbContext<EntityUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}