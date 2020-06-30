namespace ImageDump.Models
{
	public interface IModel
	{
		public string ID { get; set; }

		public void GenerateID ( );
	}
}
