using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fingerprint_Voting.Models.ViewModels
{
    public class CandidatesViewModel
    {
        private SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public List<CandidateDTO> GetAllCandidates()
        {
            List<CandidateDTO> list = new List<CandidateDTO>();

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("SelectAllCandidatesDetails", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            CandidateDTO objCandidateDTO = new CandidateDTO
                            {
                                CandidateId = rdr["CandidateID"].ToString(),
                                FirstName = rdr["Name"].ToString(),
                                Surname = rdr["Surname"].ToString(),
                                Gender = rdr["Gender"].ToString(),
                                Country = rdr["Country"].ToString(),
                                City = rdr["City"].ToString(),
                                DOB = rdr["DOB"].ToString(),
                            CandidatePic = (byte[])(rdr["Picture"]),
                                CampaignID = rdr["CampaignID"].ToString()
                            };

                            list.Add(objCandidateDTO);
                        }

                    }
                }
            }
            return list;
        }

        public CandidateDTO GetCandidateDetailsById(string id)
        {
            // get candidate id from the model
            CandidateDTO paramCandidateDTO = new CandidateDTO();

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("SelecOneCandidate", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.Parameters.AddWithValue("@CandidateId", id);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();

                        paramCandidateDTO.CandidateId = rdr["CandidateID"].ToString();
                        paramCandidateDTO.FirstName = rdr["Name"].ToString();
                        paramCandidateDTO.Surname = rdr["Surname"].ToString();
                        paramCandidateDTO.Gender = rdr["Gender"].ToString();
                        paramCandidateDTO.Country = rdr["Country"].ToString();
                        paramCandidateDTO.City = rdr["City"].ToString();
                        paramCandidateDTO.DOB = rdr["DOB"].ToString();
                        paramCandidateDTO.CandidatePic = (byte[])(rdr["Picture"]);
                        paramCandidateDTO.CampaignID = rdr["CampaignID"].ToString();


                    }
                }
            }
            return paramCandidateDTO;
        }
        public void DeleteCandidateDetailsById(string id)
        {
            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("DeleteCandidate", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.Parameters.AddWithValue("@CandidateID", id);

                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}