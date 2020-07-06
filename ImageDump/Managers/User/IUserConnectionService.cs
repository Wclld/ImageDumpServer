using System;

namespace ImageDump.Managers.User
{
	public interface IUserConnectionService
	{
		void Connect ( string id, Uri userAddress );
		void DisconnectUser ( Uri userAddress );
	}
}