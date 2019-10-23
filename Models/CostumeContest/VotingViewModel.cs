using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpookyPartyMVC.Models
{
    public class VotingViewModel
    {
        public List<Catergory> Catergories {get;set;}

        public List<Entry> AllCostumes {get;set;}

        public User ActiveUser {get;set;}

        public VotingViewModel(List<Catergory> catergories, List<Entry> allCostumes, User activeUser)
        {
            Catergories = catergories;
            AllCostumes = allCostumes;
            ActiveUser = activeUser;
        }
    }
}