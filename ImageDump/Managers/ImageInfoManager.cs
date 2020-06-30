using System;
using System.Collections.Generic;

using ImageDump.Managers.Database;
using ImageDump.Models;

namespace ImageDump.Managers
{
	public sealed class ImageInfoManager
	{
		public event Action<ImageInfo> OnDataChanged;
		private readonly List<ImageInfo> _data;

		public ImageInfoManager ( IDataProvider dataProvider )
		{
			_data = dataProvider.GetData<ImageInfo>( );
			OnDataChanged += dataProvider.Update;
		}

		public ImageInfo GetById ( string id )
		{
			var target = _data.Find( x => x.ID == id );
			if ( target != null )
			{
				return target;
			}

			throw new Exception( "ID miss!" );
		}

		public IEnumerable<ImageInfo> GetAll ( )
		{
			return _data;
		}

		public void Create ( ImageInfo info )
		{
			info.GenerateID( );

			_data.Add( info );

			OnDataChanged?.Invoke( info );
		}

		public void Edit ( string id, ImageInfo newInfo )
		{
			var target = GetById( id );
			target.Copy( newInfo );

			OnDataChanged?.Invoke( target );
		}

		public void Remove ( string id )
		{
			var target = GetById( id );
			_data.Remove( target );
			
			OnDataChanged?.Invoke( target );
		}
	}
}
