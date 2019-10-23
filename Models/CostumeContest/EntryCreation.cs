using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpookyPartyMVC.Models
{
    public class EntryCreation
    {

        [MinLength(2)]
        public string CostumeName {get;set;}

        [MinLength(2)]
        public string Description {get;set;}
    }
}