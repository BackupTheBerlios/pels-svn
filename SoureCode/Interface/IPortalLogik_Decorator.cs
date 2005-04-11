using System;

namespace pELS.Server
{
	/// <summary>
	/// Summary description for IPortalLogik_Decorator.
	/// </summary>
	public interface IPortalLogik_Decorator
	{
		// stelle Verbindung zum Portal her
		void StarteRemotingObject(int pin_Port);
		// deregistriere den Decorator vom Remoting
		void Disconnect();
	}

}
