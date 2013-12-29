﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Derby.Models
{
	public class Pack
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Region { get; set; }
        //public Guid CreatedBy { get; set; }

        public ICollection<Den> Dens { get; set; }
        public ICollection<Competition> Competitions { get; set; }
        public ICollection<Scout> Scouts { get; set; }
	}
}