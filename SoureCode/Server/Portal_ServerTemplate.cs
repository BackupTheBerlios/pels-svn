using System;
// ben�tigt f�r: setzen der Lease time des remotable objects
using System.Runtime.Remoting.Lifetime;
// ben�tigt f�r: remotable objects
using System.Runtime.Remoting;
// ben�tigt f�r: IDictionary
using System.Collections;
// ben�tigt f�r: ChannelServices
using System.Runtime.Remoting.Channels;
// ben�tigt f�r: TcpChannel
using System.Runtime.Remoting.Channels.Tcp;

// TODO: l�schen; nur f�r Testzwecke
using System.Windows.Forms;


namespace pELS.Server
{
	interface IPortalLogik
	{
		// setzen der InitialLeaseTime (auf TimeSpan.Zero)
		Object InitializeLifetimeService();
		// anmelden des remotable objects
		bool InitialisiereRemotableObject();
		// starten der PortalLogik
		bool StartePortalLogik();
	}

	/// <summary>
	/// template f�r Logik-Klassen eines Portals 
	/// die auf der Serverseite laufen
	/// </summary>
	public class Portal_ServerTemplate : MarshalByRefObject, IPortalLogik
	{
		public Portal_ServerTemplate()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public bool StartePortalLogik()
		{
			MessageBox.Show("Test aus Portal_ServerTemplate");
			return false;
		}

		public override Object InitializeLifetimeService()
		{
			ILease lease = (ILease)base.InitializeLifetimeService();
			if (lease.CurrentState == LeaseState.Initial)  
			{
				// unendliche Lease-time 
				lease.InitialLeaseTime = TimeSpan.Zero;
				lease.SponsorshipTimeout = TimeSpan.Zero;
				lease.RenewOnCallTime = TimeSpan.Zero;
			}
			return lease;
		}
		
		public bool InitialisiereRemotableObject()
		{
			string	_Pfad = "Template_Pfad";
			int		_Port = 9001;
			// falls es ein konkretes Objekt PortalLogik gibt
			try
			{
				IDictionary prop = new Hashtable();
				prop["name"] = "";
				prop["port"] = _Port;
				ChannelServices.RegisterChannel(new TcpChannel(prop, null, null));
				RemotingServices.Marshal(this, _Pfad, typeof(Portal_ServerTemplate));
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return false;
			}
			
		}

	}
}
