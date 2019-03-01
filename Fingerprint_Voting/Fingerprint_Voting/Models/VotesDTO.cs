using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fingerprint_Voting.Models
{
    public class VotesDTO
    {
       [Key]
        public string UserId { get; set; }

        public string CandidateId { get; set; }
    }
}