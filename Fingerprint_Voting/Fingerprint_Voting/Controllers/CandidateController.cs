using Fingerprint_Voting.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Fingerprint_Voting.Controllers
{
    public class CandidateController : Controller
    {
        SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        // GET: /Admin/AddRole
        [Authorize(Roles = "Administrator")]
        #region public ActionResult AddCandidate()
        public ActionResult AddCandidate()
        {
            CandidateDTO objRoleDTO = new CandidateDTO();

            return View(objRoleDTO);


        }
        #endregion

        // PUT: /Admin/AddRole
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        #region public ActionResult AddCandidate(RoleDTO paramRoleDTO)


        public ActionResult AddCandidate(CandidateDTO paramCandidateDTO, HttpPostedFileBase CandidateImage)
        {

            
            try
            {
                if (paramCandidateDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //var Name = paramCandidateDTO.UserPic; 
                var UserName = paramCandidateDTO.FirstName.Trim();
                var Surname = paramCandidateDTO.FirstName.Trim();
                var Gender = paramCandidateDTO.Gender;
                var Country = paramCandidateDTO.Country.Trim();
                var City = paramCandidateDTO.City.Trim();
                var DOB = paramCandidateDTO.City.Trim();

                //byte CandidatePic = paramCandidateDTO.CandidatePic;
                if (CandidateImage != null)
                {
                    // To convert the user uploaded Photo as Byte Array before save to DB
                    paramCandidateDTO.CandidatePic = new byte[CandidateImage.ContentLength];
                    CandidateImage.InputStream.Read(paramCandidateDTO.CandidatePic, 0, CandidateImage.ContentLength);

                }


                SqlCommand cmd = new SqlCommand("InsertIntoCandidatesTable", sqlconn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Surname", Surname);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@Country", Country);
                cmd.Parameters.AddWithValue("@City", City);
                cmd.Parameters.AddWithValue("@DOB", DOB);
                cmd.Parameters.AddWithValue("@CandidatePic", paramCandidateDTO.CandidatePic);

                sqlconn.Open();
                cmd.ExecuteNonQuery();

                sqlconn.Close();

                //roleManager.Create(new IdentityRole(UserName));


                //return Redirect("~/Candidate/AddCandidate");


                return Redirect("~/Candidate/Details");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("AddCandidate");
            }

        }
        #endregion

        [Authorize(Roles = "Administrator")]
        #region public ActionResult Details()
        public ActionResult Details()
        {
            CandidateDTO objRoleDTO = new CandidateDTO();

            return View(objRoleDTO);


        }
        #endregion



    }
}