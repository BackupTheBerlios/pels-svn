using System;

namespace pELS.Server
{
	public interface IPortalLogik
	{
		// anmelden des remotable objects
		void InitialisiereRemotableObject();
		// starten der PortalLogik
		void StartePortalLogik();

		int LiefereRemotingPort();
		string LiefereRemotingPfad();
	}
}

