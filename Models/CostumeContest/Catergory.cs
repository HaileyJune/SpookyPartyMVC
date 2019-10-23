using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpookyPartyMVC.Models
{
    public class Catergory
    {
        [Key]
        public int CatergoryId {get;set;}

        public string CatergoryName {get;set;}

        public List<Vote> Votes {get;set;}

        public Catergory()
        {
            
        }
    }
}