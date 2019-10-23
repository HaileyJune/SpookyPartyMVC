using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpookyPartyMVC.Models
{
    public class Entry
    {
        [Key]
        public int EntryId {get;set;}

        [MinLength(2)]
        public string CostumeName {get;set;}

        [MinLength(2)]
        public string Description {get;set;}

        public int UserId {get;set;}
        public User User {get;set;}

        public List<Vote> Votes {get;set;}

        public Entry()
        {

        }

        public Entry(EntryCreation userInput, User user, int userId)
        {
            CostumeName = userInput.CostumeName;
            Description = userInput.Description;
            User = user;
            UserId = userId;

            Votes = new List<Vote>();
        }
    }
}