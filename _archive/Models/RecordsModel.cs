using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _archive.Models
{
   
    public class RecordsModel
    {
        [Key]
        public int ArchiveID { get; set; }
        [Required]
        [DisplayName("Changeset ID")]
        public int ChangesetID { get;set;}
        [Required]
        [DisplayName("User ID")]
        public int UserID { get;set;}
        [Required]
        public string Description { get;set;}
        [Required]
        [DisplayName("Analysis No")]
        public int AnalysisNo { get;set;}
        [Required]
        [DisplayName("Test Description")]
        public string TestDescription { get;set;}
        [Required]

        [DisplayName("Start Time")]
        public DateTime StartTime { get;set;}
        [DisplayName("End Time")]
        public DateTime EndTime { get;set;}
        [DisplayName("Release Time")]
        public DateTime ReleaseTime { get;set;}
        [Required]
        public string Status { get;set;}
      
    }
}
