using System;
// TODO: löschen
using System.Windows.Forms;

namespace Portal_Update
{
	using pELS.APS.Server.Interface;
	using pELS.Server;

	/// <summary>
	/// 
	/// </summary>
	public class CPortal_Update : Cap_PortalLogik, IPortal_Update
	{
		public CPortal_Update(int pin_OMPort, string pin_URL, int pin_Port) : 
			base(pin_OMPort, pin_URL, pin_Port)
		{
		}

		#region Cap_PortalLogik members
		// diese Methode ist nur zur Erinnerung, dass die Variable "_Pfad" gesetzt werden muss
		protected override void SetzeRemotingPfad()
		{
			this._Pfad = "Update";
		}

		// startet die Logik des Portals
		// muss von der erbenden Klasse implementiert werden
		public override void StartePortalLogik()
		{
		}
		#endregion

		#region IPortal_Update Members

		public void RegistriereFuerHelfer(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.Helfer.RegistriereFuerAenderungsEvents(pin_Delegate);
		}

		public void RegistriereFuerAnforderung(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.Anforderungen.RegistriereFuerAenderungsEvents(pin_Delegate);
		}

		public void RegistriereFuerMaterial(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.Material.RegistriereFuerAenderungsEvents(pin_Delegate);
		}

		public void RegistriereFuerEinsatzschwerpunkte(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.Einsatzschwerpunkte.RegistriereFuerAenderungsEvents(pin_Delegate);
		}

		public void RegistriereFuerEinheit(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.Einheiten.RegistriereFuerAenderungsEvents(pin_Delegate);
		}

		public void RegistriereFuerEinsatz(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.Einsaetze.RegistriereFuerAenderungsEvents(pin_Delegate);
		}

		public void RegistriereFuerEtbEintraege(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.EtbEintraege.RegistriereFuerAenderungsEvents(pin_Delegate);
		}
		
		public void RegistriereFuerEtbKommentare(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.EtbKommentare.RegistriereFuerAenderungsEvents(pin_Delegate);
		}
		
		public void RegistriereFuerOrtsverband(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.Ortsverbaende.RegistriereFuerAenderungsEvents(pin_Delegate);
		}

		public void RegistriereFuerKfZ(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.Kfz.RegistriereFuerAenderungsEvents(pin_Delegate);
		}

		public void RegistriereFuerMaterialübergabe(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.Materialuebergaben.RegistriereFuerAenderungsEvents(pin_Delegate);
		}

		public void RegistriereFuerVerbrauchsgut(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.Verbrauchsgueter.RegistriereFuerAenderungsEvents(pin_Delegate);
		}

		public void RegistriereFuerBenutzer(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.Benutzer.RegistriereFuerAenderungsEvents(pin_Delegate);
		}

		public void RegistriereFuerModul(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.Moduln.RegistriereFuerAenderungsEvents(pin_Delegate);
		}

		public void RegistriereFuerTermin(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.Termine.RegistriereFuerAenderungsEvents(pin_Delegate);
		}

		public void RegistriereFuerMeldung(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.Meldungen.RegistriereFuerAenderungsEvents(pin_Delegate);
		}

		public void RegistriereFuerAuftrag(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			this._ObjektManager.Auftraege.RegistriereFuerAenderungsEvents(pin_Delegate);
		}
		
		#endregion
	}
}
