using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fingerprint_Voting.Models.AdminModelsDTO.VoteViewModels
{
    public class VoteViewModel
    {
        public string GetUserFingerprint(string id)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            // get userStatus id from the model
            string userFingerprint = ""; 

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("getUserFingerPrint", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.Parameters.AddWithValue("@UserId", id);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();

                        userFingerprint = rdr["UserFingerprint"].ToString();

                    }
                }
            }
            return userFingerprint;
        }
        // The following method returns the user StatusId
        public UserDetails GetUserDetails(string id)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            // get user detials

            UserDetails userDetails = new UserDetails(); 


            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("getUserDetails", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.Parameters.AddWithValue("@UserId", id);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            userDetails.DOB = rdr["DOB"].ToString();
                            userDetails.UserStatusId = rdr["UserStatusId"].ToString();
                        }
                        else
                        {
                            userDetails = null; 
                        }
                        
                    }
                }
            }
            return userDetails;
        }
        // The following method returns the userStatus Description
        public string GetUserStatusDescription(string id)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            string userStatusDescription = "";

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("getStatusDescription", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.Parameters.AddWithValue("@UserStatusID", id);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();

                        userStatusDescription = rdr["Description"].ToString();

                    }
                }
            }
            return userStatusDescription;
        }        
        // The following method returns the userStatus id by passing the status description
        public string GetUserStatusIdByDescriptio(string desc)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            string statusId = "";

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("statusIdByDescription", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.Parameters.AddWithValue("@Description", desc);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            statusId = rdr["UserStatusID"].ToString();
                        }
                        else
                        {
                            statusId = null;
                            return statusId; 
                        }

                        

                    }
                }
            }
            return statusId;
        }

        // If the user Status Table is Empty than insert into table the description
        public void InsertDescriptionToUserStatus(string desc)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("InsertIntoUserStatusTable", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@Description", desc);

                    sqlconn.Open();
                    cmd.ExecuteNonQuery();
                    sqlconn.Close();
                }
            }
        }

        // If the user Status Table is Empty than insert into table the description
        public string GetLoggedUserCountry(string id)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var userCountry = ""; 
            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("getUserCountry", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();

                        userCountry = rdr["Country"].ToString();

                    }
                }
            }
            return userCountry; 
        }

        public Candidate GetCandidateDetailsById(string id)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            // get candidate id from the model
            Candidate paramCandidate = new Candidate();

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("getCandidateDetail", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.Parameters.AddWithValue("@CanId", id);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read(); 
                        
                        paramCandidate.Name = rdr["Name"].ToString();
                        paramCandidate.Surname = rdr["Surname"].ToString();
                        paramCandidate.Gender = rdr["Gender"].ToString();
                        paramCandidate.CampaignID = rdr["CampaignID"].ToString();
                        paramCandidate.CandidatePic = (byte[])(rdr["Picture"]);


                    }
                }
            }
            return paramCandidate;
        }


        public UserCampaign GetUserCampaign(string UserId, string CampaignID)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            // get users with the campaign they vote 
            UserCampaign userCampaign = new UserCampaign();

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("getUserCampaign", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@CampaignID", CampaignID);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            userCampaign.UserId = rdr["UserId"].ToString();
                            userCampaign.CampaignID = rdr["CampaignID"].ToString();
                        }
                        else
                        {
                            userCampaign = null; 
                        }
                        
                    }
                }
            }
            return userCampaign;
        }
    }
}