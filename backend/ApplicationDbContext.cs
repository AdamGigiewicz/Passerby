using WebApi.Helpers;
using WebApi.Services;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : DbContext
{
 public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
}
