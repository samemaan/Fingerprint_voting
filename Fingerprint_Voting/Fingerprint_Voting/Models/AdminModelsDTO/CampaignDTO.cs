using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fingerprint_Voting.Models.AdminModelsDTO
{
    public class CampaignDTO
    {
        [Key]

        public string CampaignID { get; set; }

        [Display(Name = "Start Date"), Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date"), Required]
        public DateTime EndDate { get; set; }

        [Display(Name = "Description"), Required]
        public string Description { get; set; }
    }
}