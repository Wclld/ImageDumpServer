namespace ImageDump.Models
{
	public class InfoVersion : IModel
	{
		public string ID { get; set; }
		public int Version { get; set; }
		public string ChangedInfoID { get; set; }
	}
}
