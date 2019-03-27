using Fingerprint_Voting.Models.AdminModelsDTO;
using Fingerprint_Voting.Models.AdminModelsDTO.VoteViewModels;
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
        public RegisterViewModel GetUserDetailsById(string id)
        {
            SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            // get candidate id from the model
            RegisterViewModel registerUser = new RegisterViewModel();

            using (sqlconn)
            {
                using (SqlCommand cmd = new SqlCommand("getOneUserDetails", sqlconn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlconn.Open();
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();

                        registerUser.FirstName = rdr["FirstName"].ToString();
                        registerUser.Surname = rdr["Surname"].ToString();
                        registerUser.Gender = rdr["Gender"].ToString();
                        registerUser.Country = rdr["Country"].ToString();
                        registerUser.City = rdr["City"].ToString();
                        registerUser.DOB = rdr["DOB"].ToString();
                        
                    }
                }
            }
            return registerUser;
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

        public List<string> GetAllTheDeTailsRelatedToEachCountry(string selectedCountry, string selectedCampaign)
        {

           
            DynamicChartsModel dM = new DynamicChartsModel(); 
            // get the data from the DB
            ChartsViewModel cVM = new ChartsViewModel();
            List<RegisterViewModel> users = cVM.GetAllUsres(); // get the users 

            // get all the Campaign to count the number of Campaigns in each country 
            CampaignViewModel campVM = new CampaignViewModel();
            List<CampaignDTO> campaigns = campVM.GetAllCampaigns();

            // get all the candidates of the system and count the total number of candidates of each country 
            CandidatesViewModel candidateVM = new CandidatesViewModel();
            List<CandidateDTO> candidates = candidateVM.GetAllCandidates();

            // get alll the Votes for each candidates of each country. 
            VoteViewModel vVM = new VoteViewModel();
            List<VotesDTO> votes = vVM.GetAllTheVotes();

            string cand = "";
            selectedCountry = selectedCountry.Trim();
            selectedCampaign = selectedCampaign.Trim(); 

            List<string> countryCampainglist = new List<string>();
            foreach (var item in campaigns)
            {
                if (!countryCampainglist.Contains(item.Country.ToString()))
                {
                    countryCampainglist.Add(item.Country);
                }
            }
            int total_votes = 0;
            int total_users = 0; 
            bool canFound = false; 
            var votesForEachCandidate = "";
            var totalUsresForEachCampaignVoted = ""; 
            foreach (var country in countryCampainglist) // get the candidates related to the selected country and campaigns 
            {
                if(selectedCountry == country)
                {
                    foreach(var cam in campaigns)
                    {
                        if(selectedCampaign == cam.Description)
                        {
                            foreach(var can in candidates)
                            {
                                if(can.CampaignID == cam.CampaignID)
                                {
                                    cand += can.FirstName + ",";
                                    canFound = true; 
                                    foreach (var vot in votes)
                                    {
                                        if (can.CandidateId == vot.CandidateId) // total votes for the each candidate
                                        {
                                            total_votes++;
                                        }
                                    }
                                    if (canFound == true)
                                    {
                                        votesForEachCandidate += total_votes + ",";
                                        total_votes = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            foreach (var user in users) // count total number of users related to that country 
            {
                if (selectedCountry == user.Country)
                {
                    total_users++;
                }
            }

            var totalVoters = total_users.ToString(); 
            cand = cand.TrimEnd(',', ' '); // removing the last comma from the string 
            votesForEachCandidate = votesForEachCandidate.TrimEnd(',', ' '); // removing the last comma from the string 
            //totalUsresForEachCampaignVoted = totalUsresForEachCampaignVoted.TrimEnd(',', ' '); // removing the last comma from the string 

            var arrayCand = votesForEachCandidate.Split(','); 
            for(int i =0; i< arrayCand.Length; i++)
            {
                total_users = (total_users - Int32.Parse(arrayCand[i]));// get the possiable votes left 
            }
            totalUsresForEachCampaignVoted = total_users.ToString(); 
            dM.Candidates = cand;
            dM.Votes = votesForEachCandidate;


            // this is just for the testing////////////////////////////DEMO/////////////////DEMO////////////////////////DEMO/////////////
            cand = "Hamid Karzai,Abdullah Abdullah,Ashraf Ghani,Abdul Rashid Dostum,Mohammad Hanif Atmar";
            votesForEachCandidate = "350000,2100000,2800000,840000,910000";
            totalUsresForEachCampaignVoted = "3000000";
            totalVoters = "10000000";
            //////////////////////////////////////////////DEMO/////////////////////DEMO////////////////////DEMO/////////////////////////


            List<string> data = new List<string>// return this list to the view and extract the values in the script to chart. 
            {
                selectedCountry,
                selectedCampaign,
                cand,
                votesForEachCandidate,
                totalUsresForEachCampaignVoted,
                totalVoters
            };
            return data;  // return the data to the view 
        }
    }
}