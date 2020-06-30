using System;

namespace ImageDump.Models
{
	public abstract class AbstractModel
	{
		public string ID { get; private set; }

		public AbstractModel ( )
		{
			ID = Guid.NewGuid( ).ToString( );
		}
	}
}
