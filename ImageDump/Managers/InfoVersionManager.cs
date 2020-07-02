using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ImageDump.Managers.Database;
using ImageDump.Managers.User;
using ImageDump.Models;

namespace ImageDump.Managers
{
	public class InfoVersionManager
	{
		private readonly List<InfoVersion> _data;
		private readonly UserManager _userManager;
		private readonly ImageInfoManager _imageManager;


		public InfoVersionManager ( IDataProvider dataProvider, UserManager userManager, ImageInfoManager imageManager )
		{
			_data = dataProvider.GetData<InfoVersion>( );
			_userManager = userManager;
			_imageManager = imageManager;

			_userManager.OnUserConnected += CompareVersions;
			_imageManager.OnDataChanged += UpdateVersion;

			if ( _data.Count == 0 )
			{
				_data.Add( new InfoVersion( )
				{
					ID = Guid.NewGuid( ).ToString( ),
					Version = 0,
					ChangedInfoID = String.Empty
				}
				);
			}
		}


		private void CompareVersions ( UserModel user )
		{
			var userVersion = user.DataVersion;
			int lastVersion = _data[^1].Version;

			if ( userVersion < lastVersion )
			{
				var infoVersionIndex = GetClosestInfoVersionIndex( userVersion );
				Parallel.For( infoVersionIndex, _data.Count, i =>
				{
					var imageInfo = _imageManager.GetById( _data[i].ChangedInfoID );
					_userManager.UpdateUserInfo( user.ID, imageInfo, _data[i].Version );
				} );
			}
		}

		private int GetClosestInfoVersionIndex ( int targetVersion )
		{
			var versionInfo = _data.Find( x => x.Version == targetVersion );
			int closestVersionIndex = _data.IndexOf(versionInfo);

			if ( versionInfo == null )
			{
				closestVersionIndex = _data.
					Select( x => x.Version ).
					Where( x => x <= targetVersion ).
					Select( x => targetVersion - x ).
					Min( );

			}
			return closestVersionIndex;
		}

		private void UpdateVersion ( ImageInfo updatedInfo )
		{
			var newVersion = new InfoVersion( )
			{
				ID = Guid.NewGuid( ).ToString( ),
				ChangedInfoID = updatedInfo.ID,
				Version = _data[^1].Version + 1
			};

			_data.Add( newVersion );
			_userManager.UpdateConnectedUsers( updatedInfo, newVersion.Version );
		}
	}
}
