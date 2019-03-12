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
        

        public List<CampaignDTO> GetAllCampaigns()
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
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
                                Description = reader["Description"].ToString(),
                                Country = reader["Country"].ToString()
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
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
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
                        campaignDTO.Country = reader["Country"].ToString(); 

                    }
                }
            }
            return campaignDTO;
        }
        public void DeleteCampaignById(string id)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
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
        // the following method returns the name and id of the campaign 
        public List<CampaignNames> GetAllCampaignNamesAndID()
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            List<CampaignNames> list = new List<CampaignNames>();

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
                            CampaignNames campaignDTO = new CampaignNames
                            {
                                CampaignID = reader["CampaignID"].ToString(),
                                Description = reader["Description"].ToString()
                            };

                            list.Add(campaignDTO);
                        }

                    }
                }
            }
            return list;
        }
    }
}