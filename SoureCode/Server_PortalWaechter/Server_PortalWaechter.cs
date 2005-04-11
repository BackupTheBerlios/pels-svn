using System;
// ben�tigt f�r: das Laden der PortalLogik-Assembly
using System.Reflection;


using System.Runtime.Remoting.Activation;
using System.Windows.Forms;


namespace pELS.Server.PortalWaechter
{
	using pELS.Server;

	//TODO: l�schen

	interface IPortalLogikWaechter
	{
		// l�dt eine dll von dem angebenen DllURI und initialisiert das remotable Object bei RemotingURI
		bool LadeAssembly(string pin_DllURI, string pin_RemotingURL, int pin_RemotingPort);
		// startet die PortalLogik
		void StartePortalLogik();

	}

	/// <summary>
	/// Summary description for Server_PortalWrapper.
	/// </summary>
	public class Server_PortalWaechter : MarshalByRefObject, IPortalLogikWaechter
	{
        private Object _PortalLogik;
		private Type _PortalLogikType;

		public Server_PortalWaechter()
		{
		}

		public bool LadeAssembly(string pin_DllURI, string pin_RemotingURL, int pin_RemotingPort)
		{
			Assembly _asm_PortalLogik = null;
			bool _Assemblygeladen = false;
			// laden der Assembly
			_asm_PortalLogik = Assembly.LoadFrom(pin_DllURI);

			object[] args = new object[2];
            args[0] = pin_RemotingURL;
			args[1] = pin_RemotingPort;

			// Laden aller Typeninformationen
			Type[] _AvailableTypes = _asm_PortalLogik.GetTypes();
			foreach(Type _AktuellerType in _AvailableTypes)
			{									
				// �berp�fen, ob aktueller Typ das ben�tigte Interface implementiert
				if (null != _AktuellerType.GetInterface((typeof(pELS.Server.IPortalLogik).FullName)))
				{
					_PortalLogikType = _AktuellerType;
					// erzeuge neues Object vom aktuellen Typ
					try
					{
						_PortalLogik = Activator.CreateInstance(_PortalLogikType, args, null);
						// rufe die Methode "StartePortalLogik" aus dem Interface auf
						// Schuppe: ich konnte hier nicht imt TypeCasts arbeiten
						// ich wei� nicht warum, es hat einfach nicht geklappt
						MethodInfo mi = _PortalLogikType.GetMethod("InitialisiereRemotableObject");
						mi.Invoke(_PortalLogik, null);
						_Assemblygeladen = true;
					}
						// es konnte kein Objekt erstellt werden
						// dieser Fall tritt f�r abstrakte Klassen ein
					catch(System.MemberAccessException e)
					{
					}

				}
			}
			return _Assemblygeladen;
		}

		public void StartePortalLogik()
		{
			// rufe die Methode "StartePortalLogik" aus dem Interface auf
			// Schuppe: ich konnte hier nicht imt TypeCasts arbeiten
			// ich wei� nicht warum, es hat einfach nicht geklappt
			if (_PortalLogikType != null)
			{
				MethodInfo mi = _PortalLogikType.GetMethod("StartePortalLogik");
				mi.Invoke(_PortalLogik, null);
			}
		}
	}


}
