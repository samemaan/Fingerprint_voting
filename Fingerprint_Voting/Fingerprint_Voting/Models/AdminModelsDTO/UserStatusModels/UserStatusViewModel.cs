using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fingerprint_Voting.Models.UserStatusModels
{
    public class UserStatusViewModel
    {
        private SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public List<UserStatusDTO> GetAllUserStatus()
        {
            List<UserStatusDTO> list = new List<UserStatusDTO>();

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("SelectAllUserStatusDescription", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            UserStatusDTO objUserStatusDTO = new UserStatusDTO
                            {
                                UserStatusID = rdr["UserStatusID"].ToString(),
                                Description = rdr["Description"].ToString()
                            };

                            list.Add(objUserStatusDTO);
                        }

                    }
                }
            }
            return list;
        }
        public UserStatusDTO GetUserStatusDetailsById(string id)
        {
            // get userStatus id from the model
            UserStatusDTO paramUserStatusDTO = new UserStatusDTO();

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("SelecOneDescription", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.Parameters.AddWithValue("@UserStatusID", id);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();

                        paramUserStatusDTO.UserStatusID = rdr["UserStatusID"].ToString();
                        paramUserStatusDTO.Description = rdr["Description"].ToString();

                    }
                }
            }
            return paramUserStatusDTO;
        }
        public void DeleteUserStatusDetailsById(string id)
        {
            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("DeleteDescription", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.Parameters.AddWithValue("@UserStatusID", id);

                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}