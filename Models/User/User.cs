using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpookyPartyMVC.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [MinLength(2)]
        public string Username {get;set;}

        [MinLength(2)]
        public string Name {get;set;}

        [MinLength(4)]
        [DataType(DataType.Password)]
        public string Password {get;set;}

        [NotMapped]
        [Display(Name = "Re-type password")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string PasswordConfirm {get;set;}

        public int EntryId {get;set;}
        public Entry Entry{get;set;}

        public List<Vote> Votes {get;set;}
    }
}