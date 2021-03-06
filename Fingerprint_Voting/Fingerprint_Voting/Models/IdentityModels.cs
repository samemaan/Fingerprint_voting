﻿using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Fingerprint_Voting.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
       
        [Display(Name ="First Name"),Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Display(Name ="Surname"), Required]
        [StringLength(100)]
        public string Surname { get; set; }

        [Display(Name = "Date Of Birth"), Required]
        [StringLength(50)]
        public string DOB { get; set; }

        [Display(Name = "Gender"), Required]
        [StringLength(50)]
        public string Gender { get; set; }

        [Display(Name = "Country"), Required]
        [StringLength(100)]
        public string Country { get; set; }

        [Display(Name = "City"), Required]
        [StringLength(100)]
        public string City { get; set; }

        [Display(Name = "User Image")]
        public byte[] UserPic { get; set; }


        [Display(Name = "Fingerprint"), Required]
        public string UserFingerprint { get; set; }

        public string UserStatusId { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }



        public System.Data.Entity.DbSet<Fingerprint_Voting.Models.CandidateDTO> CandidateDTO { get; set; }

        public System.Data.Entity.DbSet<Fingerprint_Voting.Models.UserStatusDTO> UserStatusDTO { get; set; }

        public System.Data.Entity.DbSet<Fingerprint_Voting.Models.AdminModelsDTO.CampaignDTO> CampaignDTO { get; set; }

        public System.Data.Entity.DbSet<Fingerprint_Voting.Models.ExpandedUserDTO> ExpandedUserDTO { get; set; }

        public System.Data.Entity.DbSet<Fingerprint_Voting.Models.VotesDTO> VotesDTO { get; set; }
    }
}