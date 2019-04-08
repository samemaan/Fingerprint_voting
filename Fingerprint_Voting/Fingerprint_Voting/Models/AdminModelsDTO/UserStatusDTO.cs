using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fingerprint_Voting.Models
{
    public class UserStatusDTO
    {
        [Key]
        public string UserStatusID { get; set; }

        [Display(Name = "Description"), Required]
        public string Description { get; set; }
    }
}