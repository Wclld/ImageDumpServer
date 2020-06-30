using System;

namespace ImageDump.Models
{
	public class ImageInfo : IModel
	{
		public string ID { get; set; }
		public string Name { get; set; }
		public string ImagePath { get; set; }
		public string Description { get; set; }

		public void Copy ( ImageInfo newInfo )
		{
			Name = newInfo.Name;
			ImagePath = newInfo.ImagePath;
			Description = newInfo.Description;
		}

		public void GenerateID ( ) => ID = Guid.NewGuid( ).ToString( );
	}
}
