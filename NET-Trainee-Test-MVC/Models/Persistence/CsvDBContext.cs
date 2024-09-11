using Microsoft.EntityFrameworkCore;
using NET_Trainee_Test_MVC.Models.Entities;

namespace NET_Trainee_Test_MVC.Models.Persistence
{
    public class CsvDBContext : DbContext
    {
        public CsvDBContext(DbContextOptions<CsvDBContext> options)
        : base(options)
        {
        }

        public DbSet<Person>? Person { get; set; }
    }
}
