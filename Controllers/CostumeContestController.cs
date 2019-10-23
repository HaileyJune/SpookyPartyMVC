using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpookyPartyMVC.Models;

namespace SpookyPartyMVC.Controllers
{
    public class CostumeContestController : Controller
    {

        private SpookyContext dbContext;
        public CostumeContestController(SpookyContext context)
        {
            dbContext = context;
        }

        [Route("create")]
        [HttpGet]
        public IActionResult CreateEntry()
        {
            return View();
        }

        [Route("create")]
        [HttpPost]
        public IActionResult DoCreateEntry(EntryCreation userInput)
        {
            int? userId = HttpContext.Session.GetInt32("userid");
            User activeUser = dbContext.Users
                .Where(use => use.UserId == userId)
                .FirstOrDefault();
            if (activeUser != null)
            {
                Entry newEntry = new Entry(userInput, activeUser, activeUser.UserId);
                dbContext.Entries.Add(newEntry);

                activeUser.Entry = newEntry;
                activeUser.EntryId = newEntry.EntryId;

                dbContext.SaveChanges();
            }
            return Redirect("/");
        }

        [Route("vote")]
        [HttpGet]
        public IActionResult Voting()
        {

            int? userId = HttpContext.Session.GetInt32("userid");
            User activeUser = dbContext.Users
                .Where(use => use.UserId == userId)
                .FirstOrDefault();
            if (activeUser != null)
            {
                List<Catergory> allCatergories = dbContext.Catergories
                    .ToList();

                List<Entry> allCostumes = dbContext.Entries
                    .ToList();

                List<Vote> userVotes = activeUser.Votes;

                VotingViewModel viewModel = new VotingViewModel(allCatergories, userVotes, allCostumes, activeUser);

                return View(viewModel);
            }
            return Redirect("/");
        }
        [Route("vote")]
        [HttpPost]
        public IActionResult DoVote(int entryId, int catergoryId)
        {
            int? userId = HttpContext.Session.GetInt32("userid");

            User activeUser = dbContext.Users
                .Where(use => use.UserId == userId)
                .FirstOrDefault();
            if (activeUser != null)
            {
                Entry entryToVote = dbContext.Entries
                    .Where(ent => ent.EntryId == entryId)
                    .Include(ent => ent.Votes)
                    .FirstOrDefault();

                Catergory selectedCatergory = dbContext.Catergories
                    .Where(cat => cat.CatergoryId == catergoryId)
                    .FirstOrDefault();

                Vote ifVoteExists = selectedCatergory.Votes
                    .Where(vote => vote.UserId == userId)
                    .FirstOrDefault();

                if (ifVoteExists == null)
                {
                    Vote NewVote = new Vote(activeUser, entryToVote, selectedCatergory);
                    dbContext.Votes.Add(NewVote);
                    selectedCatergory.Votes.Add(NewVote);
                    activeUser.Votes.Add(NewVote);
                    entryToVote.Votes.Add(NewVote);
                }
                else
                {
                    ifVoteExists.Entry = entryToVote;
                    ifVoteExists.EntryId = entryToVote.EntryId;
                }
                dbContext.SaveChanges();

                return Redirect("success");
            }
            return Redirect("/");            
        }
    }
}