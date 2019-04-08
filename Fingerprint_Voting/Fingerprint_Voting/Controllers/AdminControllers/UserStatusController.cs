using Fingerprint_Voting.Models;
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
    public class UserStatusController : Controller
    {
        private SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        // GET: UserStatus/Index
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Index()
        public ActionResult Index()
        {
            // get all the list of candidates from the UserStatusViewModels and return the model to cshtml page

            UserStatusViewModel userStatusVM = new UserStatusViewModel();
            List<UserStatusDTO> status = userStatusVM.GetAllUserStatus();

            return View(status);

        }
        #endregion
        // GET: /UserStatus/Create
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Create()
        public ActionResult Create()
        {
            UserStatusDTO userStatusDTO = new UserStatusDTO();
            return View(userStatusDTO);


        }
        #endregion

        // POST: /UserStatus/Create
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        #region public ActionResult Create(UserStatusDTO paramUserStatusDTO)


        public ActionResult Create(UserStatusDTO paramUserStatusDTO)
        {


            try
            {
                if (paramUserStatusDTO == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //var Name = paramCandidateDTO.UserPic; 
                var Description = paramUserStatusDTO.Description.Trim();

                using (sqlconn)
                {
                    using (SqlCommand cmd = new SqlCommand("InsertIntoUserStatusTable", sqlconn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;


                        cmd.Parameters.AddWithValue("@Description", Description);
                       
                        sqlconn.Open();
                        cmd.ExecuteNonQuery();
                        sqlconn.Close();

                    }
                }
              
                return Redirect("~/UserStatus/Create");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View("Create");
            }
        }
        #endregion
        // GET: /UserStatus/Details
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Details()
        public ActionResult Details(string id)
        {
            UserStatusViewModel candidateVM = new UserStatusViewModel();
            UserStatusDTO userStatus = candidateVM.GetUserStatusDetailsById(id);
            return View(userStatus);
        }
        #endregion
        //GET: /UserStatus/Delete
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Delete()
        public ActionResult Delete(string id)
        {
            UserStatusViewModel userStatusVM = new UserStatusViewModel();
            userStatusVM.DeleteUserStatusDetailsById(id);
            return Redirect("~/UserStatus/Index");
        }
        #endregion

        // GET: /UserStatus/Edit
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Edit()
        public ActionResult Edit(string id)
        {
            UserStatusViewModel userStatus = new UserStatusViewModel();
            UserStatusDTO status = userStatus.GetUserStatusDetailsById(id);
            return View(status);
        }
        #endregion
        // POST: /UserStatus/Edit
        [Authorize(Roles = "Administrator")]
        #region public ActionResult Edit()
        [HttpPost]
        public ActionResult Edit(UserStatusDTO objeUserStatus)
        {
            if (ModelState.IsValid)
            {
               
                using (sqlconn)
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateUserStatusDetails", sqlconn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;


                        cmd.Parameters.AddWithValue("@UserStatusID", objeUserStatus.UserStatusID);
                        cmd.Parameters.AddWithValue("@Description", objeUserStatus.Description);

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