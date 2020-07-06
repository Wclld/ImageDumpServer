using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using ImageDump.Managers.Database;
using ImageDump.Models;

using Newtonsoft.Json;

namespace ImageDump.Managers.User
{
	public class UserManager : IUserConnectionService
	{
		public event Action<UserModel> OnUserConnected;

		private readonly List<UserModel> _data;
		private readonly IHttpClientFactory _clientFactory;

		private readonly object _lock = new object();


		public UserManager ( IDataProvider dataProvider, IHttpClientFactory clientFactory )
		{
			_data = dataProvider.GetData<UserModel>( );
			_clientFactory = clientFactory;
		}


		public void Connect ( string id, Uri address )
		{
			var user = GetByID(id);

			user.Address = address;

			OnUserConnected?.Invoke( user );
		}

		public void DisconnectUser ( Uri userAddress )
		{
			var user = GetByAddress(userAddress);

			if ( user != null )
			{
				_data.Remove( user );
			}
		}

		public void UpdateConnectedUsers ( ImageInfo info, int version )
		{
			Parallel.ForEach( _data, async user =>
			{
				await UpdateUserInfo( user, info, version );
			} );
		}

		public void UpdateUserInfo ( string id, ImageInfo info, int version )
		{
			var user = GetByID( id );
			Task.Run( ( ) => UpdateUserInfo( user, info, version ) );
		}


		private async Task UpdateUserInfo ( UserModel user, ImageInfo info, int version )
		{
			var bodyJson = JsonConvert.SerializeObject(info);

			var request = new HttpRequestMessage( HttpMethod.Post, user.Address )
			{
				Content = new StringContent( bodyJson, Encoding.UTF8, "application/json" )
			};

			var client = _clientFactory.CreateClient( );
			var responce = await client.SendAsync(request);

			if ( responce.IsSuccessStatusCode )
			{
				//what if user is on last version(1886), and db - drops(to 10) all the versions are ignored?
				if ( user.DataVersion < version )
				{
					user.DataVersion = version;
				}
			}
			else
			{
				lock ( _lock )
				{
					DisconnectUser( user.Address );
				}
			}
		}

		private UserModel GetByID ( string id )
		{
			var result = _data.Find( x => x.ID == id);

			if ( result == null )
			{
				result = new UserModel( )
				{
					ID = id,
					DataVersion = 0
				};

				_data.Add( result );
			}
			return result;
		}

		private UserModel GetByAddress ( Uri address )
		{
			return _data.Find( x => x.Address == address );
		}
	}
}
