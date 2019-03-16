using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fingerprint_Voting.Models
{
    public class ExpandedUserDTO
    {
        [Key]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Lockout End Date Utc")]




        public DateTime? LockoutEndDateUtc { get; set; }

        [Display(Name = "First Name"), Required]
        public string FirstName { get; set; }

        [Display(Name = "Surname"), Required]
        public string Surname { get; set; }

        [Display(Name = "Date Of Birth"), Required]
        public string DOB { get; set; }

        [Display(Name = "Gender"), Required]
        public string Gender { get; set; }

        [Display(Name = "Country"), Required]
        public string Country { get; set; }

        [Display(Name = "City"), Required]
        public string City { get; set; }

        [Display(Name = "User Image")]
        public byte[] UserPic { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Fingerprint"), Required]
        public string UserFingerprint { get; set; }

        public int AccessFailedCount { get; set; }
        public string PhoneNumber { get; set; }




        public IEnumerable<UserRolesDTO> Roles { get; set; }


        public string UserStatusId { get; set; }

        public List<string> CountriesList { get; set; }
    }

    public class UserRolesDTO
    {
        [Key]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }

    public class UserRoleDTO
    {
        [Key]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }

    public class RoleDTO
    {
        [Key]
        public string Id { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }

    public class UserAndRolesDTO
    {
        [Key]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public List<UserRoleDTO> ColUserRoleDTO { get; set; }
    }
}