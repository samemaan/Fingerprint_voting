using Fingerprint_Voting.Models;
using Fingerprint_Voting.Models.AdminModelsDTO;
using Fingerprint_Voting.Models.UserStatusModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Fingerprint_Voting.Controllers
{
    public class CampaignController : Controller
    {
        SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        // GET: Campaign/Index
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Index()
        public ActionResult Index()
        {
            // get all the list of Campaign from the UserStatusViewModels and return the model to cshtml page

            CampaignViewModel campVM = new CampaignViewModel();
            List<CampaignDTO> campaigns = campVM.GetAllCampaigns();

            return View(campaigns);

        }
        #endregion
        // GET: /Campaign/Create
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Create()
        public ActionResult Create()
        {
            CampaignDTO campaignDTO = new CampaignDTO();
            return View(campaignDTO);


        }
        #endregion

        // POST: /Campaign/Create
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region public ActionResult Create(CampaignDTO CampaignDTO)


        public ActionResult Create(CampaignDTO CampaignDTO)
        {


            try
            {
                if (CampaignDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var StartDate = CampaignDTO.StartDate;
                var EndDate = CampaignDTO.EndDate; 
                var Description = CampaignDTO.Description.Trim();

                using (sqlconn)
                {
                    using (SqlCommand cmd = new SqlCommand("InsertIntoCampaignTable", sqlconn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;


                        cmd.Parameters.AddWithValue("@StartDate", StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", EndDate);
                        cmd.Parameters.AddWithValue("@Description", Description);

                        sqlconn.Open();
                        cmd.ExecuteNonQuery();
                        sqlconn.Close();

                    }
                }

                return Redirect("~/Campaign/Create");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("Create");
            }
        }
        #endregion
        // GET: /Campaign/Details
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Details(string id)
        public ActionResult Details(string id)
        {
            CampaignViewModel campaignVM = new CampaignViewModel();
            CampaignDTO campaign = campaignVM.GetCampaignById(id);
            return View(campaign);
        }
        #endregion
        //GET: /Campaign/Delete
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Delete(string id)
        public ActionResult Delete(string id)
        {
            CampaignViewModel campaignVM = new CampaignViewModel();
            campaignVM.DeleteCampaignById(id);
            return Redirect("~/Campaign/Index");
        }
        #endregion

        // GET: /Campaign/Edit
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Edit(string id)
        public ActionResult Edit(string id)
        {
            CampaignViewModel campaignVM = new CampaignViewModel();
            CampaignDTO status = campaignVM.GetCampaignById(id);
            return View(status);
        }
        #endregion
        // POST: /UserStatus/Edit
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Edit(CampaignDTO objeCampaign)
        [HttpPost]
        public ActionResult Edit(CampaignDTO objeCampaign)
        {
            if (ModelState.IsValid)
            {

                using (sqlconn)
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateCampaignDescription", sqlconn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;


                        cmd.Parameters.AddWithValue("@CampaignID", objeCampaign.CampaignID);
                        cmd.Parameters.AddWithValue("@StartDate", objeCampaign.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", objeCampaign.EndDate);
                        cmd.Parameters.AddWithValue("@Description", objeCampaign.Description);

                        sqlconn.Open();
                        cmd.ExecuteNonQuery();
                        sqlconn.Close();

                    }
                }
            }
            return View();
        }
        #endregion
    }
}