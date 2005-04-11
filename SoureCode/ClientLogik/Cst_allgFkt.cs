using System;
using System.Drawing;
using pELS.DV;

// benötigt für: Cst_Einstellung
using pELS.Tools.Client;
// benötigt für: Cst_PortalLogik
using pELS.Client;
// benötigt für: IPortalLogik_XXX
using pELS.APS.Server.Interface;


using System.Windows.Forms;

namespace pELS.Client.PortalLogik_allgFkt
{
	
	public class Cst_allgFkt :  Cst_PortalLogik, IPortalLogik_allgFkt	
	{
		#region Instanzvariablen
		
		// ein proxy-objekt zur Unterstuetzung allgemeiner Fkts
		IPortalLogik_allgFkt _PortalLogikAllgFkt;
		pELS.Events.UpdateEventHandler ueh;
 
		#endregion

		#region Cst_PortalLogik members
		override protected void SetzeRemotingPfad()
		{
			this._Pfad = "PortalAllgFkt";
		}
		
		override protected void SetzePortalTyp()
		{
			this._PortalTyp = typeof(IPortalLogik_allgFkt);
		}

		#endregion

		#region Konstruktor
		// TODO:
		public Cst_allgFkt(Cst_Einstellung pin_Einstellung) : 
			base(pin_Einstellung)
		{
			// INIT Proxyobjekte
//			this._PortalLogikAllgFkt = (IPortalLogik_allgFkt) this._PortalLogik;
			//TODO: Diese Datei zu löschen
			this._PortalLogikAllgFkt = _Portal_AllgFkt;

			#region UpdateEvent registrieren
			// einen neuen Event Handler erstellen
			// durch den Adapter kapseln
			ueh = pELS.Events.UpdateEventAdapter.Create(
			new pELS.Events.UpdateEventHandler(this.VerarbeiteBenutzerUpdate));
			// registrieren
			this._Portal_Update.RegistriereFuerBenutzer(ueh);
			#endregion

		}
		#endregion

		#region get method
		public IPortalLogik_allgFkt apAllgFkt
		{
			get{return this._PortalLogikAllgFkt;}
		}
		#endregion
	
		#region Funktionalität
		
		public Cdv_Benutzer[] HoleAlleBenutzer()
		{
			Cdv_Benutzer[] pout_benutzermenge;
			if((pout_benutzermenge = this._PortalLogikAllgFkt.HoleAlleBenutzer())== null)
				pout_benutzermenge = new Cdv_Benutzer[0];
			else 
			{}
			return pout_benutzermenge;
		}

		public Cdv_Benutzer SpeichereBenutzer(Cdv_Benutzer pin_neuerBenutzer)
		{
			Cdv_Benutzer pout_benutzer =  this._PortalLogikAllgFkt.SpeichereBenutzer(pin_neuerBenutzer);
			return pout_benutzer;
		}
		
		//Hierüber können Systemereignisse geworfen werden
		public void WerfeSystemereignis(Cdv_Systemereignis pin_syserg)
		{
			this._Portal_AllgFkt.WerfeSystemereignis(pin_syserg);
		}

		public void VerarbeiteBenutzerUpdate(pELS.Events.UpdateEventArgs pin_e)
		{
			//Diese MessageBox wurde immer beim anlegen neuer Benutzer ausgeführt -> Sehr verwirrend
			//TODO: brauchen wir diese Methode überhaupt ?
			//MessageBox.Show("Cst_AllgFkt.VerarbeiteBenutzerUpdate(..): startet!");
		}

		public bool PruefeAllgVorraussetzungen()
		{
			try
			{
				return this._Portal_AllgFkt.PruefeAllgVorraussetzungen();
			}
			catch(Exception e)
			{
				string tmp = e.ToString();
				return false;

			}
		}

		#endregion
	}
}
