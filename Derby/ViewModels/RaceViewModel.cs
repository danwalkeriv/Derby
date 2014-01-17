﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Derby.Models;

namespace Derby.ViewModels
{
    public class RaceViewModel
    {
        public int Id { get; set; }
        public int CompetitionId { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Completed Date")]
        public DateTime CompletedDate { get; set; }

        public int DenId { get; set; }

        public Competition Competition { get; set; }
        public ICollection<Den> Dens { get; set; }

        public IList DenList { get; set; } 
    }
}