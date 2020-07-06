using System;

namespace ImageDump.Models
{
	public class UserModel : IModel
	{
		public string ID { get; set; }
		public int DataVersion { get; set; }
		public Uri Address { get; set; }
	}
}
