using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fingerprint_Voting.Models
{
    public class CountryModel
    {
        [Key]
        public string CountryTitle { get; set; }
        public string RegisterdTitle { get; set; }
        public string VoteTitle { get; set; }
        public string CampaignTitle { get; set; }

        public Country CountrytData { get; set; }
    }
    public class Country
    {
        [Key]
        public string CountryName { get; set; }
        public string Registerd { get; set; }
        public string Vote  { get; set; }
        public string Campaign  { get; set; }
    }
}