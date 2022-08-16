using System.ComponentModel.DataAnnotations;

namespace _archive.Models
{
    public class UsersModel
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string MiddleName { get; set; }
  
        public string MobileNo { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime LastLogin { get; set; }  

    }
}
