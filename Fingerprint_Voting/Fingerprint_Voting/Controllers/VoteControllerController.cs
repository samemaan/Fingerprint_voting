using Fingerprint_Voting.Models;
using Fingerprint_Voting.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fingerprint_Voting.Controllers
{
    public class VoteControllerController : Controller
    {
        // GET: /Vote/Index
        public ActionResult Index()
        {
            CandidatesViewModel candidateVM = new CandidatesViewModel();
            List<CandidateDTO> candidates = candidateVM.GetAllCandidates();

            return View(candidates);
        }
    }
}