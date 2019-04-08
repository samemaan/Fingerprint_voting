using Fingerprint_Voting.Models.ChartsViewModels;
using Fingerprint_Voting.Models.ResultsViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fingerprint_Voting.Models.ResultsViewModels
{
    public class ResultsViewModel
    {
        public List<ResultsDTO> GetUsersRole()
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            List<ResultsDTO> list = new List<ResultsDTO>();

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("getUserRoles", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ResultsDTO userCampaign = new ResultsDTO
                            {
                                UserId = rdr["UserId"].ToString(),
                                RoleId = rdr["UserId"].ToString()
                            };
                            list.Add(userCampaign);
                        }
                    }
                }
            }
            return list;
        }
      
    }
}