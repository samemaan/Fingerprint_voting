using Fingerprint_Voting.Models.AdminModelsDTO;
using Fingerprint_Voting.Models.UserStatusModels;
using Fingerprint_Voting.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fingerprint_Voting.Models.ChartsViewModels
{
    public class ChartsViewModel
    {
        
        public List<RegisterViewModel> GetAllUsres()
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            List<RegisterViewModel> list = new List<RegisterViewModel>();
            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("GetAllUsers", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            RegisterViewModel users = new RegisterViewModel
                            {
                                UserId = rdr["Id"].ToString(),
                                FirstName = rdr["FirstName"].ToString(),
                                Surname = rdr["Surname"].ToString(),
                                Gender = rdr["Gender"].ToString(),
                                Country = rdr["Country"].ToString(),
                                City = rdr["City"].ToString(),
                                DOB = rdr["DOB"].ToString()
                            };

                            list.Add(users);
                        }

                    }
                }
            }
            return list;
        }
        public List<VotesDTO> GetAllVotes()
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            List<VotesDTO> list = new List<VotesDTO>();
            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("GetAllVotes", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            VotesDTO objCandidateDTO = new VotesDTO
                            {
                                UserId = rdr["UserId"].ToString(),
                                CampaignID = rdr["CampaignID"].ToString(),
                                CandidateId = rdr["CandidateId"].ToString()
                            };
                            list.Add(objCandidateDTO);
                        }
                    }
                }
            }
            return list;
        }

        public List<CampaignDTO> GetCampaigns() // accessing the database in CampaignViewModels/CampaignViewModel to get all the campaigns
        {
            CampaignViewModel campVM = new CampaignViewModel();
            List<CampaignDTO> campaigns = campVM.GetAllCampaigns();

            return campaigns;
        }
        public List<CandidateDTO> GetCandidates() // accessing the database to get all the candidates
        {
            CandidatesViewModel candidateVM = new CandidatesViewModel();
            List<CandidateDTO> candidates = candidateVM.GetAllCandidates();

            return candidates;
        }
    }
}