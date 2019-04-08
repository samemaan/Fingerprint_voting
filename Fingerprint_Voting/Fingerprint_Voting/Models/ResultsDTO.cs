using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fingerprint_Voting.Models
{
    public class ResultsDTO
    {
        [Key]
        public string UserId { get; set; }
        public string RoleId { get; set; }

        [Display(Name = "Campaign")]
        public string Campagin { get; set; }

        public List<string> Campaigns { get; set; }


        public string Candidates { get; set; }
        public string Votes { get; set; }
    }
}