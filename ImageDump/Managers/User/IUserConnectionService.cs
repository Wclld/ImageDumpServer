namespace ImageDump.Managers.User
{
	public interface IUserConnectionService
	{
		void Connect ( string id, string userAddress );
		void DisconnectUser ( string userAddress );
	}
}