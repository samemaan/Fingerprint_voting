using Fingerprint_Voting.Models;
using Fingerprint_Voting.Models.AdminModelsDTO;
using Fingerprint_Voting.Models.UserStatusModels;
using Fingerprint_Voting.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        // GET: /Candidate/Index
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Index()
        public ActionResult Index()
        {
            // get all the list of candidates from the candidatesViewModel and return the model to cshtml page

            CandidatesViewModel candidateVM = new CandidatesViewModel();
            List<CandidateDTO> candidates = candidateVM.GetAllCandidates();

            return View(candidates);

        }
        #endregion
        // GET: /Admin/Create
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Create()
        public ActionResult Create()
        {

            CandidateDTO candidateDTO = new CandidateDTO();

            CampaignViewModel camVM = new CampaignViewModel();

            candidateDTO.Campaigns = camVM.GetAllCampaignNamesAndID();

            return View(candidateDTO);


        }
        #endregion

        // POST: /Candidate/Create
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        #region public ActionResult Create(RoleDTO paramRoleDTO)


        public ActionResult Create(CandidateDTO paramCandidateDTO, HttpPostedFileBase CandidateImage, string CampaignIDFromView)
        {



            if (paramCandidateDTO == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //var Name = paramCandidateDTO.UserPic; 
            var UserName = paramCandidateDTO.FirstName.Trim();
            var Surname = paramCandidateDTO.Surname.Trim();
            var Gender = paramCandidateDTO.Gender;
            var Country = paramCandidateDTO.Country.Trim();
            var City = paramCandidateDTO.City.Trim();
            var DOB = paramCandidateDTO.DOB.Trim();
            var CampaignID = CampaignIDFromView;

            // get the country of the from Campaign table,  check the country matches the candidate country
            // enter the details to the database else redirect the user to an message Page. 
            var CampCountry = "";
            CampaignViewModel cmVM = new CampaignViewModel();
            CampaignDTO cmDTO = new CampaignDTO();
            cmDTO = cmVM.GetCampaignById(CampaignID);
            CampCountry = cmDTO.Country;
            CampCountry = CampCountry.Trim();

            if (Country != CampCountry)
            {
                return View("Error");
            }
            else
            {
                try
                {
                    //byte CandidatePic = paramCandidateDTO.CandidatePic;
                    if (CandidateImage != null)
                    {
                        // To convert the user uploaded Photo as Byte Array before save to DB
                        paramCandidateDTO.CandidatePic = new byte[CandidateImage.ContentLength];
                        CandidateImage.InputStream.Read(paramCandidateDTO.CandidatePic, 0, CandidateImage.ContentLength);

                    }

                    using (sqlconn)
                    {
                        using (SqlCommand cmd = new SqlCommand("InsertIntoCandidatesTable", sqlconn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;


                            cmd.Parameters.AddWithValue("@UserName", UserName);
                            cmd.Parameters.AddWithValue("@Surname", Surname);
                            cmd.Parameters.AddWithValue("@Gender", Gender);
                            cmd.Parameters.AddWithValue("@Country", Country);
                            cmd.Parameters.AddWithValue("@City", City);
                            cmd.Parameters.AddWithValue("@DOB", DOB);
                            cmd.Parameters.AddWithValue("@CandidatePic", paramCandidateDTO.CandidatePic);
                            cmd.Parameters.AddWithValue("@CampaignID", CampaignID);

                            sqlconn.Open();
                            cmd.ExecuteNonQuery();
                            sqlconn.Close();

                        }
                    }
                    //roleManager.Create(new IdentityRole(UserName));
                    //return Redirect("~/Candidate/AddCandidate");


                    return Redirect("~/Candidate/Create");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error: " + ex);
                    return View("Create");
                }

            }

        }
        #endregion

        // GET: /Candidate/Details
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Details()
        public ActionResult Details(string id)
        {
            CandidatesViewModel candidateVM = new CandidatesViewModel();
            CandidateDTO candidate = candidateVM.GetCandidateDetailsById(id);
            return View(candidate);
        }
        #endregion

        // GET: /Candidate/Edit
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Edit()
        public ActionResult Edit(string id)
        {
            CandidatesViewModel candidateVM = new CandidatesViewModel();
            CandidateDTO candidate = candidateVM.GetCandidateDetailsById(id);
            return View(candidate);
        }
        #endregion
        //// GET: /Candidate/Edit
        //[Authorize(Roles = "Administrator")]
        //#region public ActionResult Edit()
        //[HttpPost]
        //public ActionResult Edit(CandidateDTO objeCandidate, HttpPostedFileBase Candidatepic)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //byte CandidatePic = paramCandidateDTO.CandidatePic;
        //        if (Candidatepic != null)
        //        {
        //            // To convert the user uploaded Photo as Byte Array before save to DB
        //            objeCandidate.CandidatePic = new byte[Candidatepic.ContentLength];
        //            Candidatepic.InputStream.Read(objeCandidate.CandidatePic, 0, Candidatepic.ContentLength);

        //        }

        //        using (sqlconn)
        //        {
        //            using (SqlCommand cmd = new SqlCommand("UpdateCandidateDetails", sqlconn))
        //            {
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //                sqlconn.Open();
        //                 = Candidatepic;
        //                byte[] image = objeCandidate.CandidatePic; 

        //                cmd.Parameters.AddWithValue("@CandidateID", objeCandidate.CandidateId);
        //                cmd.Parameters.AddWithValue("@Name", objeCandidate.FirstName);
        //                cmd.Parameters.AddWithValue("@Surname", objeCandidate.Surname);
        //                cmd.Parameters.AddWithValue("@Gender", objeCandidate.Gender);
        //                cmd.Parameters.AddWithValue("@Country", objeCandidate.Country);
        //                cmd.Parameters.AddWithValue("@City", objeCandidate.City);
        //                cmd.Parameters.AddWithValue("@DOB", objeCandidate.DOB);
        //                cmd.Parameters.AddWithValue("@Picture ", objeCandidate.CandidatePic);


        //                cmd.ExecuteNonQuery();
        //                sqlconn.Close();

        //            }
        //        }
        //    }
        //    return View();
        //}
        //#endregion

        //GET: /Candidate/Delete
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Delete()
        public ActionResult Delete(string id)
        {
            CandidatesViewModel candidateVM = new CandidatesViewModel();
            candidateVM.DeleteCandidateDetailsById(id);
            return Redirect("~/Candidate/Index");
        }
        #endregion
    }
}