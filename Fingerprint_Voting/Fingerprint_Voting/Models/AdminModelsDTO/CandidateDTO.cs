using Fingerprint_Voting.Models.AdminModelsDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fingerprint_Voting.Models
{
    
    public class CandidateDTO
    {
        [Key]
        public string CandidateId { get; set; }

        [Display(Name = "First Name"), Required]
        public string FirstName { get; set; }

        [Display(Name = "Surname"), Required]
        public string Surname { get; set; }

        [Display(Name = "Gender"), Required]
        public string Gender { get; set; }

        [Display(Name = "City"), Required]
        public string City { get; set; }

        [Display(Name = "Country"), Required]
        public string Country { get; set; }

        [Display(Name = "Date Of Birth"), Required]
        public string DOB { get; set; }

        [Display(Name = "User Image")]
        public byte[] CandidatePic { get; set; }

        public string CampaignID { get; set; }

        [Display(Name = "Campaing"), Required]

        public List<CampaignNames> Campaigns { get; set; }
    }
}