using System.Collections.Generic;

using ImageDump.Managers.Database;
using ImageDump.Models;

namespace ImageDump.Managers
{
	public sealed class ImageInfoManager
	{
		private readonly List<ImageInfo> _data;

		public ImageInfoManager ( IDataProvider dbProvider ) => _data = dbProvider.GetData<ImageInfo>( );

	}
}
