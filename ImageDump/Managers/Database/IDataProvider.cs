using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ImageDump.Models;

namespace ImageDump.Managers.Database
{
	public interface IDataProvider
	{
		List<T> GetData<T> ( ) where T : IModel;
		void Update<T> ( T data ) where T : IModel;
		void SaveData ( );
	}
}
