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

        public string getUserFingerprint(string id)
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
    }
}