using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ImageDump.Managers;

using Microsoft.AspNetCore.Mvc;


namespace ImageDump.Controllers
{
	[Route( "api/[controller]" )]
	[ApiController]
	public class ImageInfoController : ControllerBase
	{
		private readonly ImageInfoManager _infoManager;

		public ImageInfoController ( ImageInfoManager manager )
		{
			_infoManager = manager;
		}

		[HttpGet]
		public IEnumerable<string> Get ( )
		{
			return new string[] { "value1", "value2" };
		}

		[HttpGet( "{id}" )]
		public string Get ( int id )
		{
			return "value";
		}

		[HttpPost]
		public void Post ( [FromBody] string value )
		{
		}

		[HttpPut( "{id}" )]
		public void Put ( int id, [FromBody] string value )
		{
		}

		[HttpDelete( "{id}" )]
		public void Delete ( int id )
		{
		}
	}
}
