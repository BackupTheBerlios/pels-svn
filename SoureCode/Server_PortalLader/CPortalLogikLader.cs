using System;
// benötigt für: das Laden der PortalLogik-Assembly
using System.Reflection;


using System.Runtime.Remoting.Activation;
using System.Windows.Forms;


namespace pELS.Server
{
	using pELS.Server;

	//TODO: löschen

	interface IPortalLogikLader
	{
		// lädt eine dll von dem angebenen DllURI und initialisiert das remotable Object bei RemotingURI
		bool LadeAssembly(string pin_DllURI, int pin_InternerPort, 
			string pin_RemotingURL, int pin_RemotingPort);
		// startet die PortalLogik
		void StartePortalLogik();
		
		int LiefereRemotingPort();
		string LieferePortalPfad();
	}

	/// <summary>
	/// Summary description for Server_PortalWrapper.
	/// </summary>
	public class CPortalLogikLader : MarshalByRefObject, IPortalLogikLader
	{
        private Object _PortalLogik;
		private Type _PortalLogikType;

		public CPortalLogikLader()
		{
		}

		public bool LadeAssembly(string pin_DllURI, int pin_InternerPort, 
			string pin_RemotingURL, int pin_RemotingPort)
		{
			Assembly _asm_PortalLogik = null;
			bool _Assemblygeladen = false;
			// laden der Assembly
			_asm_PortalLogik = Assembly.LoadFrom(pin_DllURI);

			object[] args = new object[3];
			args[0] = pin_InternerPort;
			args[1] = pin_RemotingURL;
			args[2] = pin_RemotingPort;

			// Laden aller Typeninformationen
			Type[] _AvailableTypes = _asm_PortalLogik.GetTypes();
			foreach(Type _AktuellerType in _AvailableTypes)
			{									
				// überpüfen, ob aktueller Typ das benötigte Interface implementiert
				if (null != _AktuellerType.GetInterface((typeof(pELS.Server.IPortalLogik).FullName)))
				{
					_PortalLogikType = _AktuellerType;
					// erzeuge neues Object vom aktuellen Typ
					if (!_PortalLogikType.IsAbstract)
					{
						_PortalLogik = Activator.CreateInstance(_PortalLogikType, args, null);
						// rufe die Methode "StartePortalLogik" aus dem Interface auf
						// Schuppe: ich konnte hier nicht imt TypeCasts arbeiten
						// ich weiß nicht warum, es hat einfach nicht geklappt
						MethodInfo mi = _PortalLogikType.GetMethod("InitialisiereRemotableObject");
						mi.Invoke(_PortalLogik, null);
						_Assemblygeladen = true;
					}
				}
			}
			return _Assemblygeladen;
		}

		public void StartePortalLogik()
		{
			// rufe die Methode "StartePortalLogik" aus dem Interface auf
			// Schuppe: ich konnte hier nicht imt TypeCasts arbeiten
			// ich weiß nicht warum, es hat einfach nicht geklappt
			if (_PortalLogikType != null)
			{
				MethodInfo mi = _PortalLogikType.GetMethod("StartePortalLogik");
				mi.Invoke(_PortalLogik, null);
			}
		}

		public int LiefereRemotingPort()
		{
			MethodInfo mi = _PortalLogikType.GetMethod("LiefereRemotingPort");
			return (int) mi.Invoke(_PortalLogik, null);
		}

		public string LieferePortalPfad()
		{
			MethodInfo mi = _PortalLogikType.GetMethod("LiefereRemotingPfad");
			return (string) mi.Invoke(_PortalLogik, null);
		}
	}


}
