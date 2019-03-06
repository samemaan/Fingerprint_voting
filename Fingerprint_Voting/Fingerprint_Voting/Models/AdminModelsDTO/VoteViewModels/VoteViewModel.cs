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
        SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public string GetUserFingerprint(string id)
        {
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
        public string GetUserStatusId(string id)
        {
            // get userStatus id from the model
            string userStatus = "";

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("getUserStatusId", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.Parameters.AddWithValue("@UserId", id);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            userStatus = rdr["UserStatusId"].ToString();
                        }

                        

                    }
                }
            }
            //sqlconn.Close(); 

            return userStatus;
        }
        // The following method returns the userStatus Description
        public string GetUserStatusDescription(string id)
        {
            // get userStatus id from the model
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
            // get userStatus id by the status description 
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
    }
}