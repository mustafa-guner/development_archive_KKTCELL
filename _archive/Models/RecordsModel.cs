using _archive.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _archive.Models
{
   
    public class RecordsModel
    {
        [Key]
        public int ArchiveID { get; set; }

       
        [DisplayName("Changeset ID")]
        public int ChangesetID { get;set;}

       
        public string Title { get; set; }

     
        [DisplayName("Update Details")]
        public string UpdateDetails { get;set;}
      
        public string Analysis { get;set;}

        public int BPMNo { get; set; }

        
        [DisplayName("Test Results")]
        public string TestResults { get;set;}
        

        [DisplayName("Start Time")]
        public DateTime StartTime { get;set;}
        [DisplayName("End Time")]
        public DateTime EndTime { get;set;}
        [DisplayName("Release Time")]
        public DateTime ReleaseTime { get;set;}
       
        public string Status { get;set;}

        [DisplayName("Category")]
        public RecordsCategory RecordsCategory { get; set; }    


        //Relationships with UsersModel

        public int UserID { get; set; } = 0;
        [ForeignKey("UserID")]
        public UsersModel User { get; set; }    
        

    }
}
