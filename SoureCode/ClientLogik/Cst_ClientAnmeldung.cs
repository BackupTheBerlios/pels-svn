using System;
// ben�tigt f�r: Cst_Einstellung
using pELS.Tools.Client;
// ben�tigt f�r: Cst_PortalLogik
using pELS.Client;
// ben�tigt f�r: IPortalLogik_XXX
using pELS.APS.Server.Interface;

// ben�tigt f�r: Initialisierung des Proxy-Objekts von IPortalLogik_allgFkt
//using pELS.Client;
using System.Windows.Forms;

namespace ClientLogik
{
	/// <summary>
	/// Summary description for Cst_ClientAnmeldung.
	/// </summary>
	public class Cst_ClientAnmeldung : Cst_PortalLogik
	{
		#region Variablen
		// das proxy-objekt der Klasse Cap_MAT 
		IPortalLogik_allgFkt _PortalLogikAllgFkt;
		#endregion
		#region Konstruktor
		public Cst_ClientAnmeldung(Cst_Einstellung pin_Einstellung) : 
			base(pin_Einstellung)
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region Cst_PortalLogik members
		override protected void SetzeRemotingPfad()
		{
			this._Pfad = "PortalClient";
		}
		
		override protected void SetzePortalTyp()
		{
			this._PortalTyp = typeof(IPortalLogik_allgFkt);
		}
		#endregion
	}
}
