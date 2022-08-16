using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace _archive.Models
{
    public class ApplicationDbContext : DbContext
    {

        
      

        [Required]
        public DbSet<RecordsModel> RecordsModel { get; set; }    

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        public ApplicationDbContext()
        {


        }
    }
}
