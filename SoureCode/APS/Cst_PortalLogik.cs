using System;
using pELS;
// benötigt für:  Cst_Einstellung
using pELS.Tools.Client;

namespace pELS.Client
{
	using pELS.APS.Server.Interface;

	/// <summary>
	/// Zusammenfassung für Cap_BasisFunktionalitaet.
	/// </summary>
	abstract public class Cst_PortalLogik
	{
		/// <summary>
		/// URL des Server mit Port 
		/// z.B. "tcp://127.0.0.1:9001/"
		/// </summary>
		private string _URL = "";
		/// <summary>
		/// Pfad der Portallogik unter welcher via Remoting die notwendigen Befehle ausgeführt werden können
		/// z.B. "PortalMAT"
		/// </summary>
		protected string _Pfad;
		/// <summary>
		/// Typ der Portallogik, z.B. "IPortalLogik"
		///  z.B. "PortalMAT"
		/// </summary>
		protected Type _PortalTyp;
		/// <summary>
		/// Proxy auf das Portal bei dem man sich für Updates registrieren kann
		/// </summary>
		protected IPortal_Update _Portal_Update;
		/// <summary>
		/// Proxy auf das Portal bei dem man sich für Updates registrieren kann
		/// </summary>
		protected IPortalLogik_allgFkt _Portal_AllgFkt;

		/// <summary>
		/// Referenz auf die PortalLogik
		/// ist aber nur vom Typ "object"
		/// kann also nur via typecast angesprochen werden
		/// </summary>
		protected object _PortalLogik;

		/// <summary>
		/// beinhaltet alle aktuellen Einstellungen, bzgl. Benutzer, Server, etc.
		/// </summary>
		protected Cst_Einstellung _Einstellung;
		public Cst_Einstellung Einstellung
		{
			get{return _Einstellung;}
		}

		// Konstruktor
		// setzt die Pfad-Variable
		// verbindet sich mit der PortalLogik
		public Cst_PortalLogik(Cst_Einstellung pin_Einstellung)
		{		
			_URL = "tcp://" + pin_Einstellung.ServerIP + ":" + pin_Einstellung.ServerPort + "/";
			SetzeRemotingPfad();
			SetzePortalTyp();

			_Portal_Update = (IPortal_Update)Activator.GetObject(
				typeof(IPortal_Update),
				_URL + "Update");

			_Portal_AllgFkt = (IPortalLogik_allgFkt)Activator.GetObject(
				typeof(IPortalLogik_allgFkt),
				_URL + "PortalAllgFkt");

			// hole Referenz auf das Remote-Objekt ObjektManager
			_PortalLogik = (object)Activator.GetObject(
				_PortalTyp,
				_URL + _Pfad);

			_Einstellung = pin_Einstellung;
		}


		// diese Methode ist nur zur Erinnerung, dass die Variable "_Pfad" gesetzt werden muss
		abstract protected void SetzeRemotingPfad();
		// diese Methode ist nur zur Erinnerung, dass die Variable "_PortalTyp" gesetzt werden muss
		abstract protected void SetzePortalTyp();
	}
}
