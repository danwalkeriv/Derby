﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Derby.Models
{
	public class Den
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public DateTime CreateDateTime { get; set; }
		public int PackId { get; set; }
        //public ICollection<Scout> Scouts { get; set; }
	}
}