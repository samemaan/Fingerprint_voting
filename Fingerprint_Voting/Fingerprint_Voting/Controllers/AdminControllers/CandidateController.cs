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
using System.Globalization;
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
            // get all the campaigns in the list with thier ids 
            var camp = candidateDTO.Campaigns;
            camp = camVM.GetAllCampaignNamesAndID();
            // get countries list to View from account
            GetCountriesList getCountriesList = new GetCountriesList();
            candidateDTO.Countries = getCountriesList.CountriesList(); // set the countries list to, where list is string and it's in the model
            List<string> campainglist = new List<string>();  // get the campaigns name from the list campaings model and add them to string list
            foreach (var item in camp)
            {
                campainglist.Add(item.Description);
            }
            candidateDTO.CampaignsList = campainglist; // string list of campaigns names, store them in the Campaigns list where declared in the model

            return View(candidateDTO);
        }
        #endregion

        // POST: /Candidate/Create
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        #region public ActionResult Create(RoleDTO paramRoleDTO)


        public ActionResult Create(CandidateDTO paramCandidateDTO, HttpPostedFileBase CandidateImage)
        {



            if (paramCandidateDTO == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // get the campaign ID by camparing the name from the list which was selected
            var CampId = "";
            CandidateDTO candidateDTO = new CandidateDTO();
            CampaignViewModel camVM = new CampaignViewModel();
            // get all the campaigns in the list with thier ids 
            var camp = candidateDTO.Campaigns;
            camp = camVM.GetAllCampaignNamesAndID();

            foreach (var item in camp)
            {
                if (item.Description == paramCandidateDTO.CampaignID)
                {
                    CampId = item.CampaignID;
                }
            }
            //var Name = paramCandidateDTO.UserPic; 
            var UserName = paramCandidateDTO.FirstName.Trim();
            var Surname = paramCandidateDTO.Surname.Trim();
            var Gender = paramCandidateDTO.Gender;
            var Country = paramCandidateDTO.Country.Trim();
            var City = paramCandidateDTO.City.Trim();
            var DOB = paramCandidateDTO.DOB.Trim();
            var CampaignID = CampId;

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
                // no check if the same candidate is already registerd in the same campaing twice 
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
            CampaignViewModel camVM = new CampaignViewModel();
            var camp = camVM.GetAllCampaignNamesAndID(); 

            foreach(var cam in camp) // find the name of campaing in the details
            {
                if(candidate.CampaignID == cam.CampaignID)
                {
                    candidate.CampaignID = cam.Description; 
                }
            }

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


            //CampaignViewModel camVM = new CampaignViewModel();
            //// get all the campaigns in the list with thier ids 
            //var camp = candidate.Campaigns;
            //camp = camVM.GetAllCampaignNamesAndID();
            //// get countries list to View from account
            //GetCountriesList getCountriesList = new GetCountriesList();
            //candidate.Countries = getCountriesList.CountriesList(); // set the countries list to, where list is string and it's in the model
            //List<string> campainglist = new List<string>();  // get the campaigns name from the list campaings model and add them to string list
            //foreach (var item in camp)
            //{
            //    campainglist.Add(item.Description);
            //}
            //candidate.CampaignsList = campainglist; // string list of campaigns names, store them in the Campaigns list where declared in the model




            return View(candidate);
        }
        #endregion
        // GET: /Candidate/Edit
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Edit()
        [HttpPost]
        public ActionResult Edit(CandidateDTO objeCandidate, HttpPostedFileBase Candepic)
        {

            //byte CandidatePic = paramCandidateDTO.CandidatePic;
            if (Candepic != null)
            {
                // To convert the user uploaded Photo as Byte Array before save to DB
                objeCandidate.CandidatePic = new byte[Candepic.ContentLength];
                Candepic.InputStream.Read(objeCandidate.CandidatePic, 0, Candepic.ContentLength);

            }

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("UpdateCandidateDetails", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlconn.Open();

                    cmd.Parameters.AddWithValue("@CandidateID", objeCandidate.CandidateId);
                    cmd.Parameters.AddWithValue("@Name", objeCandidate.FirstName);
                    cmd.Parameters.AddWithValue("@Surname", objeCandidate.Surname);
                    cmd.Parameters.AddWithValue("@Gender", objeCandidate.Gender);
                    cmd.Parameters.AddWithValue("@Country", objeCandidate.Country);
                    cmd.Parameters.AddWithValue("@City", objeCandidate.City);
                    cmd.Parameters.AddWithValue("@DOB", objeCandidate.DOB);
                    cmd.Parameters.AddWithValue("@Picture ", objeCandidate.CandidatePic);
                    cmd.Parameters.AddWithValue("@CampaignID", objeCandidate.CampaignID);


                    cmd.ExecuteNonQuery();
                    sqlconn.Close();

                }

            }
            return View();
        }
        #endregion

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