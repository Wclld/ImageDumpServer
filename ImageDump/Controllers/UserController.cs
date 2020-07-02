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
		public void Connect ( [FromForm] string userId )
		{
			var address = GetUserAddress( );
			_connectionService.Connect( userId, address );
		}

		[HttpGet( "Disconnect" )]
		public void Disconnect ( )
		{
			var address = GetUserAddress( );
			_connectionService.DisconnectUser( address );
		}


		private string GetUserAddress ( )
		{
			var result = String.Empty;

			var connectionFeature = HttpContext.Features.Get<IHttpConnectionFeature>();
			if ( connectionFeature != null )
			{
				var remoteIp = connectionFeature?.RemoteIpAddress;
				var remotePort = connectionFeature?.RemotePort;

				result = $"{remoteIp}:{remotePort}";
			}
			return result;
		}
	}
}
