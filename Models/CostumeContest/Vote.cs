using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpookyPartyMVC.Models
{
    public class Vote
    {
        [Key]
        public int VoteId {get;set;}

        public int UserId {get;set;}
        public User User {get;set;}

        public int EntryId {get;set;}
        public Entry Entry {get;set;}

        public int CatergoryId {get;set;}
        public Catergory Catergory {get;set;}

        public Vote()
        {}

        public Vote (User user, Entry entry, Catergory catergory)
        {
            User = user;
            UserId = user.UserId;
            Entry = entry;
            EntryId = entry.EntryId;
            Catergory = catergory;
            CatergoryId = catergory.CatergoryId;
        }
    }
}