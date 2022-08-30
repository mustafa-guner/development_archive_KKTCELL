using _archive.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _archive.Models
{
    public class UsersModel
    {
        [Key]
       
        public int Id { get; set; }
       

        [DisplayName("Username")]
        public string UserName { get; set; }
       

        public string Email { get; set; }
      
        public string Password { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.Now;
        public DateTime LastLogin { get; set; } = DateTime.Now;

        public UsersRoles Role { get; set; }


        //Relationships with Records Model

        public List <RecordsModel> RecordsModel { get; set; }  


    }
}
