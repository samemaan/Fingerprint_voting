using Fingerprint_Voting.Models.AdminModelsDTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fingerprint_Voting.Models.UserStatusModels
{
    public class CampaignViewModel
    {
        SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public List<CampaignDTO> GetAllCampaigns()
        {
            List<CampaignDTO> list = new List<CampaignDTO>();

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("SelectAllCampaigns", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CampaignDTO campaignDTO = new CampaignDTO
                            {
                                CampaignID = reader["CampaignID"].ToString(),
                                StartDate = (DateTime)reader["StartDate"],
                                EndDate = (DateTime)reader["EndDate"],
                                Description = reader["Description"].ToString()
                            };

                            list.Add(campaignDTO);
                        }

                    }
                }
            }
            return list;
        }
        public CampaignDTO GetCampaignById(string id)
        {
            // get campaign id from the model
            CampaignDTO campaignDTO = new CampaignDTO();

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("SelecOneCampaign", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.Parameters.AddWithValue("@CampaignID", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();

                        campaignDTO.StartDate = (DateTime)reader["StartDate"];
                        campaignDTO.EndDate = (DateTime)reader["EndDate"];
                        campaignDTO.Description = reader["Description"].ToString(); 

                    }
                }
            }
            return campaignDTO;
        }
        public void DeleteCampaignById(string id)
        {
            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("DeleteCampaign", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.Parameters.AddWithValue("@CampaignID", id);

                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}