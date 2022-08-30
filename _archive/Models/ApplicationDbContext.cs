using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace _archive.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            /*
                @DESC: 
                A user may have many archive records but one record always belongs to one user
                Relationship -> One To Many
             */
            modelbuilder.Entity<RecordsModel>().HasOne(record => record.User).WithMany(user => user.RecordsModel);
            base.OnModelCreating(modelbuilder);
        }

        //Creating Table Names
        public DbSet<RecordsModel> RecordsModel { get; set; } 
        public DbSet<UsersModel> UsersModel { get; set; }   
     
    }
}
