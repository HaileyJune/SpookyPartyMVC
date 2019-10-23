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
                if (activeUser.Entry ==null)
                {
                    Entry newEntry = new Entry(userInput, activeUser, activeUser.UserId);
                    dbContext.Entries.Add(newEntry);
                    activeUser.Entry = newEntry;
                    activeUser.EntryId = newEntry.EntryId;
                }
                else
                {
                    activeUser.Entry.CostumeName = userInput.CostumeName;
                    activeUser.Entry.Description = userInput.Description;
                }

                dbContext.SaveChanges();
                return Redirect("/vote");

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
                .Include(use => use.Entry)
                    .ThenInclude(ent => ent.Votes)
                        .ThenInclude(vot => vot.Catergory)
                .Include(use => use.Entry)
                    .ThenInclude(ent => ent.Votes)
                        .ThenInclude(vot => vot.User)
                .Include(use => use.Votes)
                    .ThenInclude(vote => vote.Catergory)
                .FirstOrDefault();
            if (activeUser != null)
            {
                List<Catergory> allCatergories = dbContext.Catergories
                    .ToList();

                List<Entry> allCostumes = dbContext.Entries
                    .ToList();


                VotingViewModel viewModel = new VotingViewModel(allCatergories, allCostumes, activeUser);

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
                .Include(use => use.Votes)
                    .ThenInclude(vot => vot.Entry)
                .Include(use => use.Votes)
                    .ThenInclude(vot => vot.Catergory)
                .FirstOrDefault();
            if (activeUser != null)
            {
                Entry entryToVote = dbContext.Entries
                    .Where(ent => ent.EntryId == entryId)
                    .FirstOrDefault();

                Catergory selectedCatergory = dbContext.Catergories
                    .Where(cat => cat.CatergoryId == catergoryId)
                    .FirstOrDefault();

                Vote userVote = activeUser.Votes
                    .Where(vot => vot.CatergoryId == selectedCatergory.CatergoryId)
                    .FirstOrDefault();

                userVote.SetVote(entryToVote);
                dbContext.SaveChanges();

                return Redirect("success");
            }
            return Redirect("/");            
        }
    }
}