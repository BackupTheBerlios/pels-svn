using System;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
// needed for creating TypeFilterLevel.Full-channels
using System.Collections;
// needed for setting up the lifetime lease
using System.Runtime.Remoting.Lifetime;

using System.Windows.Forms;

using pELS;

namespace pELS.Client
{
	
	

////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////

	//TODO: Die Klasse kann denke ich komplett raus. Gruß Hütte

////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////


	// enthaelt die Server Informationen
	public class Cst_ServerKonfig
	{
		// TODO: IP = die aktuelle IP
		//       Port = der aktuelle Port
		//       Adresse = die aktuelle Adresse
			
		public const string _URL = "tcp://127.0.0.1:9001/HuettesTolleBasisfunktionalitaet";
		public const int _Port = 9001;
	}


	// zur Erzeugung des Proxy-objekts auf der Client-Seite
	public class Cst_ClientVerbindung
	{
		#region variablen
		private IClient _proxyClient;
		#endregion

		#region get-, set- methods
		public IClient proxyClient
		{
			get{return this._proxyClient;}
		}
		#endregion


		#region Konstruktor
		public Cst_ClientVerbindung()
		{
		}

		#endregion

		#region Methoden
		public bool InitRemotableObjekt(int pin_Port, string pin_URL)
		{
			try
			{
				// Creates a proxy for the well-known object 
				// indicated by the specified type and URL.
				IDictionary prop = new Hashtable();
				prop["name"] = "tcppELSClient";
				prop["port"] = pin_Port.ToString();
				ChannelServices.RegisterChannel(new TcpChannel(prop, null, null));

				
			
				this._proxyClient
					= (IClient)Activator.GetObject(
					// type of object
					typeof(IClient),
					// specified URL
					pin_URL);
				
				Console.WriteLine("Remoting initialized");
			
				return true;
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());
				return false;

			}
		}
		#endregion Methoden
	}
}
