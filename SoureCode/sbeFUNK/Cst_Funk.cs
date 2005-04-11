using System;
//ben�tigt f�r: Image
using System.Drawing;
// ben�tigt f�r ArrayList
using System.Collections;


namespace pELS.Client.Funk
{
	// ben�tigt f�r Cst_PortalLogik
	using pELS.Client;
	// ben�tigt f�r: Interface_Portale
	using pELS.APS.Server.Interface;
	// ben�tigt f�r: Cst_Einstellung
	using pELS.Tools.Client;
	// ben�tigt f�r: pELS-Objekte
	using pELS.DV;

	/// <summary>
	/// Summary description for Cst_Funk.
	/// </summary>
	public class Cst_Funk : Cst_PortalLogik, pELS.GUI.Interface.Isbe
	{
		#region Variablen
		/// <summary>
		/// Event, welches von ClientLogik bearbeitet wird, wenn der "Drucken"-buttom gedr�ckt wird
		/// </summary>
		public event pELS.Tools.Client.ReportRequestedEventHandler _ev_ReportRequestedEvent;
		//H�lt den Namen der Icon Datei fest
		private string _strIconName = @"SBEImages\funk.jpg";
		//Hier wird die Beschriftung unterhalb des Icons festgehalten
		private string _strSbeName = "Funk";
		//hier wird die Klassenvariable gehalten, die die User Control enth�lt
		private Cpr_usc_Funk _usc_Funk;

		/// <summary>
		/// das proxy-objekt der Klasse Cap_Funk 
		/// </summary>
		private IPortalLogik_Funk _PortalLogikFunk;

		#region EventHandler
		/// <summary>
		/// eventhandler f�r Meldungen
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Meldungen;
		/// <summary>
		/// eventhandler f�r Auftraege
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Auftraege;
		/// <summary>
		/// eventhandler f�r Einheiten
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Einheiten;
		/// <summary>
		/// eventhandler f�r KFZ
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_KFZ;
		/// <summary>
		/// eventhandler f�r Helfer
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Helfer;
		/// <summary>
		/// eventhandler f�r ESP
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_ESP;
		/// <summary>
		/// eventhandler f�r Benutzer
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Benutzer;
		/// <summary>
		/// Delegate f�r die Aktualisierung der TreeViews und Datagridsdurch den UI-Thread
		/// </summary>
		private delegate void UpdateGuiDelegate();
		#endregion
		#region pELSObjekt-Speicher
		/// <summary>
		/// enth�lt alle noch nicht versendeten Auftr�ge
		/// </summary>
		public Cdv_Auftrag[] _AlleAuftraege;
		/// <summary>
		/// enth�lt alle noch nicht versendeten Meldungen
		/// </summary>
		public Cdv_Meldung[] _AlleMeldungen;

		public Cdv_Einheit[] _AlleEinheiten;
		public Cdv_Helfer[] _AlleHelfer;
		public Cdv_KFZ[] _AlleKFZs;
		public Cdv_Einsatzschwerpunkt[] _AlleESP;
		public Cdv_Benutzer[] _AlleBenutzer;
		
		#endregion
		#endregion

		#region Konstruktor
		public Cst_Funk(Cst_Einstellung pin_Einstellung) : 
			base(pin_Einstellung)
		{
			// INIT Proxyobjekte
			this._PortalLogikFunk = (IPortalLogik_Funk) this._PortalLogik;
			#region UpdateEvent registrieren
			// registriere f�r Meldungen
			_ueh_Meldungen = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventMeldungen));
			this._Portal_Update.RegistriereFuerMeldung(_ueh_Meldungen);
			// registriere f�r Auftr�ge
			_ueh_Auftraege = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventAuftraege));
			this._Portal_Update.RegistriereFuerAuftrag(_ueh_Auftraege);
			// registriere f�r Einheiten
			_ueh_Einheiten = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventEinheiten));
			this._Portal_Update.RegistriereFuerEinheit(_ueh_Einheiten);
			// registriere f�r KFZ
			_ueh_KFZ = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventKFZ));
			this._Portal_Update.RegistriereFuerKfZ(_ueh_KFZ);
			// registriere f�r Helfer
			_ueh_Helfer = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventHelfer));
			this._Portal_Update.RegistriereFuerHelfer(_ueh_Helfer);
			// registriere f�r ESP
			_ueh_ESP = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventESP));
			this._Portal_Update.RegistriereFuerEinsatzschwerpunkte(_ueh_ESP);
			// registriere f�r benutzer
			_ueh_Benutzer = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventBenutzer));
			this._Portal_Update.RegistriereFuerBenutzer(_ueh_Benutzer);
			#endregion

			InitialisiereStartwerte();
			// INIT Gui
			this._usc_Funk = new Cpr_usc_Funk(this);
			// lade alle Startwerte in die Pr�sentationschicht
			_usc_Funk.SetzeAlle();

			// Eventhandler �bergeben.
			// Das Interface IReportRequested wird von Cst_client implementiert und stellt die Methode bereit, die das Event behandelt.
			_ev_ReportRequestedEvent += new ReportRequestedEventHandler((_Einstellung.O_Cst_Client as IReportRequested).BehandleReportRequestedEvent);
		}
		#endregion

		#region Cst_Funk members

		/// <summary>
		/// holt alle Daten vom Server und speichert diese in lokalen Variablen
		/// </summary>
		private void InitialisiereStartwerte()
		{
			// lade alle nicht versendeten Mitteilungen
			_AlleAuftraege = this._PortalLogikFunk.LadeAlleNichtVersendetenAuftraege();
			_AlleMeldungen = this._PortalLogikFunk.LadeAlleNichtVersendetenMeldungen();
			// lade alle Einheiten, KFZs und Helfer
			_AlleEinheiten = this._PortalLogikFunk.HoleAlleEinheiten();
			_AlleKFZs = this._PortalLogikFunk.HoleAlleKFZ();
			_AlleHelfer = this._PortalLogikFunk.HoleAlleHelfer();
			_AlleESP = this._PortalLogikFunk.LadeAlleEinsatzschwerpunkte();
			_AlleBenutzer = this._Portal_AllgFkt.HoleAlleBenutzer();
		}

		/// <summary>
		/// Feuert das Event, wenn der "Drucken"-Buttom gedr�ckt wird.
		/// �bergeben wird die eben gespeicherte Mitteilung, die gedruckt werden soll
		/// </summary>
		public void FeuereReportRequestedEvent(Cdv_Mitteilung pin_mitteilung)
		{
			_ev_ReportRequestedEvent( (object)pin_mitteilung);			
		}

		private void BehandleEventMeldungen(pELS.Events.UpdateEventArgs pin_e)
		{
			_AlleMeldungen = this._PortalLogikFunk.LadeAlleNichtVersendetenMeldungen();
			if (_usc_Funk._NVM_Typ == "Meldung")
				_usc_Funk.BeginInvoke(new UpdateGuiDelegate(_usc_Funk.LadeNVMGrid));
		}

		private void BehandleEventAuftraege(pELS.Events.UpdateEventArgs pin_e)
		{
			_AlleAuftraege = this._PortalLogikFunk.LadeAlleNichtVersendetenAuftraege();
			if (_usc_Funk._NVM_Typ == "Auftrag")
				_usc_Funk.BeginInvoke(new UpdateGuiDelegate(_usc_Funk.LadeNVMGrid));
		}

		private void BehandleEventEinheiten(pELS.Events.UpdateEventArgs pin_e)
		{
			_AlleEinheiten = this._PortalLogikFunk.HoleAlleEinheiten();
			_usc_Funk.BeginInvoke(new UpdateGuiDelegate(_usc_Funk.SetzeMitteilungsEmpfaenger));
		}
		private void BehandleEventKFZ(pELS.Events.UpdateEventArgs pin_e)
		{
			_AlleKFZs = this._PortalLogikFunk.HoleAlleKFZ();
			_usc_Funk.BeginInvoke(new UpdateGuiDelegate(_usc_Funk.SetzeMitteilungsEmpfaenger));
		}
		private void BehandleEventHelfer(pELS.Events.UpdateEventArgs pin_e)
		{
			_AlleHelfer = this._PortalLogikFunk.HoleAlleHelfer();
			_usc_Funk.BeginInvoke(new UpdateGuiDelegate(_usc_Funk.SetzeMitteilungsEmpfaenger));
		}

		private void BehandleEventESP(pELS.Events.UpdateEventArgs pin_e)
		{
			_AlleESP = this._PortalLogikFunk.LadeAlleEinsatzschwerpunkte();
			_usc_Funk.SetzeESP();
		}

		private void BehandleEventBenutzer(pELS.Events.UpdateEventArgs pin_e)
		{
			_AlleBenutzer = this._Portal_AllgFkt.HoleAlleBenutzer();
			_usc_Funk.SetzeBenutzerEmpfaenger();
		}


		public Cdv_Benutzer HoleAktuellenBenutzer()
		{ 
			return _Einstellung.Benutzer;
		}

		public Cdv_Benutzer ID2Benutzer(int pin_BenutzerID)
		{
			foreach (Cdv_Benutzer benutzer in _AlleBenutzer)
			{
				if(benutzer.ID == pin_BenutzerID)
				{
					return benutzer;
				}
			}
			return null;
		}

		public Cdv_Einsatzschwerpunkt ID2ESP(int pin_ESPID)
		{
			foreach (Cdv_Einsatzschwerpunkt esp in _AlleESP)
			{
				if(esp.ID == pin_ESPID)
				{
					return esp;
				}
			}
			return null;
		}

		public Cdv_Auftrag SpeichereAuftrag(Cdv_Auftrag pin_Auftrag)
		{
			Cdv_Mitteilung tmp = this._PortalLogikFunk.SpeichereMitteilung(pin_Auftrag);
			this.WerfeSystemereignis(pin_Auftrag);
			return (Cdv_Auftrag) tmp;
		}
		public Cdv_Meldung SpeichereMeldung(Cdv_Meldung pin_Meldung)
		{
			Cdv_Mitteilung tmp = this._PortalLogikFunk.SpeichereMitteilung(pin_Meldung);
			this.WerfeSystemereignis(pin_Meldung);
			return (Cdv_Meldung) tmp;
		}

		private void WerfeSystemereignis(Cdv_Mitteilung pin_mit)
		{
			string str_Beschreibung = (pin_mit.GetType().ToString()).Replace("pELS.DV.Cdv_","");
			

			#region Empf�ngerliste als String zusammenstellen
			string str_Empfaenger = String.Empty;
			foreach (int empf in pin_mit.EmpfaengerMengeKraftID)
			{
				bool _b_gefunden = false;
				
				foreach(Cdv_Einheit en in _AlleEinheiten)
					if(empf == en.ID)
					{
						str_Empfaenger += en.Name+ ", ";
						_b_gefunden = true;
					}

				if (!_b_gefunden)
					foreach(Cdv_Helfer he in _AlleHelfer)
						if(empf == he.ID)
						{
							str_Empfaenger += he.Personendaten.Vorname + " "+ he.Personendaten.Name+", ";
							_b_gefunden = true;
						}

				if (!_b_gefunden)
					foreach(Cdv_KFZ kfz in _AlleKFZs)
						if(empf == kfz.ID)
							str_Empfaenger += kfz.Funkrufname+", ";
			} 
			
			// entferne das letzte ", "
			if (str_Empfaenger.Length > 1)
				str_Empfaenger = str_Empfaenger.Remove(str_Empfaenger.Length - 2,2);
			#endregion

			#region Systemereignis erstellen
			str_Beschreibung += "\nVon: " + pin_mit.Absender
				+ "\nAn: " + str_Empfaenger;
			if (pin_mit is Cdv_Erkundungsergebnis)
				str_Beschreibung += "\nErkundungsobjekt: " + ((Cdv_Erkundungsergebnis) pin_mit).Erkundungsobjekt.Bezeichnung;
			str_Beschreibung += "\n"+ pin_mit.Text
				+ "\n�bermittelt per "+ pin_mit.Uebermittlungsart;

			Cdv_Systemereignis mySyserg = new Cdv_Systemereignis(Einstellung.Benutzer.Benutzername, 
				DateTime.Now,
				str_Beschreibung,
				Tdv_SystemereignisArt.Mitteilung,
				false);
			this._Portal_AllgFkt.WerfeSystemereignis(mySyserg);
			#endregion
		}

		#endregion

		#region Cst_PortalLogik members
		override protected void SetzeRemotingPfad()
		{
			this._Pfad = "PortalFunk";
		}
		
		override protected void SetzePortalTyp()
		{
			this._PortalTyp = typeof(IPortalLogik_Funk);
		}

		#endregion

		#region Isbe Members

		public Image GetSbeImage() 
		{ 
			System.Reflection.Assembly asm_Sbe; 
			//Informationen �ber die ausf�hrende Assembly sammeln 
			asm_Sbe = System.Reflection.Assembly.GetExecutingAssembly(); 
			//Liefere Name der Assembly als AssemblyName 
			System.Reflection.AssemblyName asm_SbeName = asm_Sbe.GetName(); 
			//Speichere den dll Namen im String 
			string strAssemblyName = asm_SbeName.Name; 
			//Icon holen 
			Image myImage = Image.FromFile(_strIconName); 
			//Gebe myImage zur�ck 
			return(myImage); 
		}

		public String GetSbeName()
		{			
			return this._strSbeName;
		}

		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
		{
			_usc_Funk.SetzeRollenRechte(pin_i_aktuelleRolle);			
		}


		public System.Windows.Forms.UserControl GetSbeUserControl()
		{
			return this._usc_Funk;
		}
		
		// TODO: Sichern alle Eingaben veranlassen und wenn alles erfolgreich true zur�ckgeben, sonst false
		public bool CloseSbeUserControl()
		{
			return true;
		}
		#endregion

	}
}
