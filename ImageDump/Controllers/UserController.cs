using System;

using ImageDump.Managers.User;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace ImageDump.Controllers
{
	[Route( "api/[controller]" )]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserConnectionService _connectionService;


		public UserController ( IUserConnectionService connectionService ) => _connectionService = connectionService;


		[HttpGet( "Connect" )]
		public string Connect ( [FromForm] string userId )
		{
			var address = GetUserAddress( );
			_connectionService.Connect( userId, address );

			return address.Port.ToString( );;
		}

		[HttpGet( "Disconnect" )]
		public void Disconnect ( )
		{
			var address = GetUserAddress( );
			_connectionService.DisconnectUser( address );
		}


		private Uri GetUserAddress ( )
		{
			var builder = new UriBuilder();

			var connectionFeature = HttpContext.Features.Get<IHttpConnectionFeature>();
			if ( connectionFeature != null )
			{
				var remoteIp = connectionFeature?.RemoteIpAddress.ToString();
				if ( remoteIp == "::1" )
				{
					remoteIp = "127.0.0.1";
				}
				builder.Host = remoteIp;
				
				var remotePort = connectionFeature?.RemotePort;

				builder.Port = remotePort.Value;
			}
			return builder.Uri;
		}
	}
}
