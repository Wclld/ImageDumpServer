using System.Collections.Generic;

using ImageDump.Managers;
using ImageDump.Models;

using Microsoft.AspNetCore.Mvc;


namespace ImageDump.Controllers
{
	[Route( "api/[controller]" )]
	[ApiController]
	public class ImageInfoController : ControllerBase
	{
		private readonly ImageInfoManager _infoManager;
		private readonly InfoVersionManager _versionManager;

		public ImageInfoController ( InfoVersionManager versionManager, ImageInfoManager infoManager )
		{
			_versionManager = versionManager;
			_infoManager = infoManager;
		}

		[HttpGet("TEST")]
		public void CreateTestEntity( )
		{
			var value = new ImageInfo()
			{
				Name = "qwe",
				Description = "asd",
				ImagePath = "zxc"
			};
			_infoManager.Create( value );
		}


		[HttpGet]
		public IEnumerable<ImageInfo> Get ( )
		{
			return _infoManager.GetAll( );
		}

		[HttpGet( "{id}" )]
		public ImageInfo Get ( string id )
		{
			return _infoManager.GetById( id );
		}

		[HttpPost]
		public void Post ( [FromBody] ImageInfo value )
		{
			_infoManager.Create( value );
		}

		[HttpPut( "{id}" )]
		public void Put ( string id, [FromBody] ImageInfo value )
		{
			_infoManager.Edit( id, value );
		}

		[HttpDelete( "{id}" )]
		public void Delete ( string id )
		{
			_infoManager.Remove( id );
		}
	}
}
