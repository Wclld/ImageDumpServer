using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using ImageDump.Models;

using Newtonsoft.Json;

namespace ImageDump.Managers.Database
{
	public class JSONDB : IDataProvider
	{
		private const string _extention = ".json";
		private readonly string _defaultDataFolderpath = Path.Combine(Environment.CurrentDirectory, "jsonData");

		private readonly Dictionary<string, IEnumerable<IModel>> _dataSets;


		public JSONDB ( ) => _dataSets = new Dictionary<string, IEnumerable<IModel>>( );


		public List<T> GetData<T> ( ) where T : IModel
		{
			var typeName = GetTypeName ( typeof ( T ) );

			return _dataSets.ContainsKey( typeName ) ? _dataSets[typeName] as List<T> : LoadData<T>( typeName );
		}

		public void Update<T> ( T data ) where T : IModel
		{
			//_datasets are already updated, because they are reftype
			SaveData<T>( );
		}

		public void SaveData ( )
		{
			foreach ( var item in _dataSets )
			{
				SaveData( item.Key );
			}
		}


		private void SaveData<T> ( )
		{
			var typeName = GetTypeName( typeof( T ) );
			SaveData( typeName );
		}

		private void SaveData ( string typeName )
		{
			if ( _dataSets.ContainsKey( typeName ) )
			{
				var path = GetDataPath( typeName );
				var jsonString = JsonConvert.SerializeObject( _dataSets[typeName] );
				Task.Run( ( ) => File.WriteAllText( path, jsonString ) );
			}
		}

		private List<T> LoadData<T> ( string name )
		{
			var dataPath = GetDataPath( name );

			var data = File.Exists( dataPath ) ?
				JsonConvert.DeserializeObject<List<T>>( File.ReadAllText( dataPath ) ) :
				new List<T>( );
			_dataSets.Add( name, data as IEnumerable<IModel> );

			return data;
		}

		private string GetDataPath ( string typeName )
		{
			if (! Directory.Exists( _defaultDataFolderpath ) )
			{
				Directory.CreateDirectory( _defaultDataFolderpath );
			}
			return Path.Combine( _defaultDataFolderpath, $"{typeName}{_extention}" );
		}
		private string GetTypeName ( Type type ) => type.Name;
	}
}
