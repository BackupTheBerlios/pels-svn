using System;
// benötigt für: setzen der Lease time des remotable objects
using System.Runtime.Remoting.Lifetime;
// benötigt für: remotable objects
using System.Runtime.Remoting;
// benötigt für: IDictionary
using System.Collections;
// benötigt für: ChannelServices
using System.Runtime.Remoting.Channels;
// benötigt für: TcpChannel
using System.Runtime.Remoting.Channels.Tcp;
// benötigt für: TypeFilterLevel
using System.Runtime.Serialization.Formatters;

//benötigt für: ObjMgr
using pELS.DV.Server.ObjectManager;


namespace pELS.Server
{
	abstract public class Cap_PortalLogik : MarshalByRefObject, IPortalLogik
	{
		// URL des Server mit Port 
		//z.B. "tcp://127.0.0.1:9001/"
		protected string _URL = "";
		protected int _Port = 0;
		// Pfad dieses Portals unter welchem es via Remoting erreicht werden kann
		// z.B. "PortalMAT"
		protected string _Pfad;
		// Referenz auf den ObjektManager
		protected Cdv_ObjMgr _ObjektManager;
		private ObjRef _PortalLogikObjRef;

		// speichert die URL des Servers
		public Cap_PortalLogik(int pin_OMPort, string pin_URL, int pin_Port)
		{
			_URL = pin_URL;
			_Port = pin_Port;
			SetzeRemotingPfad();

			InitialisiereKanal(_Port);

			// hole Referenz auf das Remote-Objekt ObjektManager
			_ObjektManager = (Cdv_ObjMgr)Activator.GetObject(
				typeof(Cdv_ObjMgr),
				_URL + ":" + Convert.ToString(pin_OMPort) + "/" + "ObjektManager");
		}

		public void VerarbeiteMeldungsUpdate(pELS.Events.UpdateEventArgs pin_e)
		{
			System.Windows.Forms.MessageBox.Show("Cap_PortalLogik.VerarbeiteMeldungsUpdate(..): startet!");
		}




		// öffnet eine Kanal auf dieses RemotableObject lauscht
		private void InitialisiereKanal(int pin_Port)
		{
			while (!Tools.Server.CMethoden.IstPortFrei(_Port))
			{
				_Port++;
			}
			// öffne Kanal auf dem alle Remotable Objects dieses Servers angesprochen werden können
			Tools.Server.CMethoden.InitialisiereKanal(_Port);
			
		}

		// diese Methode ist nur zur Erinnerung, dass die Variable "_Pfad" gesetzt werden muss
		abstract protected void SetzeRemotingPfad();

		// setzen der Lease-time für remotable-objects dieser AppDomain
		public override Object InitializeLifetimeService()
		{
			// Lifetime ist unendlich
			return null;
		}

		// startet die Logik des Portals
		// muss von der erbenden Klasse implementiert werden
		abstract public void StartePortalLogik();
		
		// stellt dieses Objekt via Remoting zur Verfügung
		public void InitialisiereRemotableObject()
		{	
			// melde dieses Objekt unter der gegebenen URL auf dem spezifierten Pfad an
			_PortalLogikObjRef = RemotingServices.Marshal(
				this, 
				 this._Pfad, 
				((Object)this).GetType());
		}

		public string LiefereRemotingPfad()
		{
			return this._Pfad;
		}

		public int LiefereRemotingPort()
		{
			return _Port;
		}

		~ Cap_PortalLogik()
		{
			RemotingServices.Disconnect(this);

			// Creating a custom formatter for a TcpChannel sink chain.
			BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
			provider.TypeFilterLevel = TypeFilterLevel.Full;
			// Creating the IDictionary to set the port on the channel instance.
			IDictionary props = new Hashtable();
			props["port"] = _Port;
			// Pass the properties for the port setting and the server provider in the server chain argument. (Client remains null here.)
			TcpChannel _Kanal = new TcpChannel(props, null, provider);


			ChannelServices.UnregisterChannel(_Kanal);
		}
	}

}