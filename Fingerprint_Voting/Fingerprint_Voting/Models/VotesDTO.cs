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
        public string CampaignID { get; set; }


    }
    public class Candidate
    {
        [Key]
        public string Name { get; set;  }
        public string Surname { get; set;  }
        public string Gender { get; set;  }
        public byte[] CandidatePic { get; set; }
        public string CampaignID { get; set; }


    }
    public class UserCampaign
    {
        [Key]
        public string UserId { get; set; }
        public string CampaignID { get; set; }
    }
    public class UserDetails
    {
        [Key]
        public string DOB { set; get; }
        public string UserStatusId { set; get; }
    }
}