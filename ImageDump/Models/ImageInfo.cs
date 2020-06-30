using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageDump.Models
{
	public class ImageInfo : AbstractModel
	{
		public string Name { get; set; }
		public string ImagePath { get; set; }
		public string Description { get; set; }
	}
}
