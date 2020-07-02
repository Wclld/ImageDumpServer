using ImageDump.Managers;
using ImageDump.Managers.Database;
using ImageDump.Managers.User;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImageDump
{
	public class Startup
	{
		public IConfiguration Configuration { get; }


		public Startup ( IConfiguration configuration )
		{
			Configuration = configuration;
		}

		public void ConfigureServices ( IServiceCollection services )
		{
			services.AddSingleton<IDataProvider, JSONDB>( );
			services.AddSingleton<ImageInfoManager>( );
			services.AddSingleton<IUserConnectionService, UserManager>( );
			services.AddSingleton<UserManager>( );
			services.AddHttpClient( );

			services.AddControllers( );
		}

		public void Configure ( IApplicationBuilder app, IWebHostEnvironment env )
		{
			if ( env.IsDevelopment( ) )
			{
				app.UseDeveloperExceptionPage( );
			}

			app.UseHttpsRedirection( );

			app.UseRouting( );

			app.UseAuthorization( );

			app.UseEndpoints( endpoints =>
			 {
				 endpoints.MapControllers( );
			 } );
		}
	}
}
