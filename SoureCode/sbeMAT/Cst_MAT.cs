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
// benötigt für: Anzeige der Eingabesfehler
using pELS.GUI.PopUp;

//namespace pELS.GUI.MAT
namespace pELS.Client.MAT
{
	/// <summary>
	/// Summary description for Cst_MAT.
	/// Ist erste Implementation (Referenz) zu Isbe-Interface
	/// </summary>
	public class Cst_MAT :  Cst_PortalLogik, pELS.GUI.Interface.Isbe
	{
		#region Instanzvariablen
		#region sbe
		/// <summary>
		/// Event, welches von ClientLogik bearbeitet wird, wenn der "Drucken"-buttom gedrückt wird
		/// </summary>
		public event pELS.Tools.Client.ReportRequestedEventHandler _ev_ReportRequestedEvent;
		
		//Hält den Namen der Icon Datei fest
		private string _strIconName = @"SBEImages\mat.JPG";
		//Hier wird die Beschriftung unterhalb des Icons festgehalten
		private string _strSbeName = "Aufträge";
		//hier wird das Objekt von MAT auf der PRS gehalten, die die User Control enthält
		private Cpr_usc_MAT _usc_Mat = null;
		#endregion

		// das proxy-objekt der Klasse Cap_MAT 
		IPortalLogik_MAT _PortalLogikMAT;
		// enthaehlt die Menge der Systembenutzer
		private Cdv_Benutzer[] _benutzermenge;
		private Cdv_Auftrag[] _nachzuverfolgendeAuftraege;
		private Cdv_Einsatzschwerpunkt[] _alleESP;
		private Cdv_Einheit[] _alleEinheiten;
		private Cdv_KFZ[] _alleKFZ;
		private Cdv_Helfer[] _alleHelfer;

		#region EventHandler
		/// <summary>
		/// eventhandler für Benutzer
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Benutzer;
		/// <summary>
		/// eventhandler für Auftraege
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Auftraege;

		/// <summary>
		/// eventhandler für Erkundungsbefehle
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Erkundungsbefehle;


		/// <summary>
		/// eventhandler für alle Esp
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_ESPs;

		/// <summary>
		/// eventhandler für Einheiten
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Einheiten;
		/// <summary>
		/// eventhandler für alle KFz
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_KFZs;
		/// <summary>
		/// eventhandler für Helfer
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Helfer;
		/// <summary>
		/// Quecky: Delegate für die Aktualisierung der TreeViews und Datagridsdurch den UI-Thread
		/// </summary>
		private delegate void UpdateGuiDelegate();

		#endregion


		#endregion

		#region Cst_PortalLogik members
		override protected void SetzeRemotingPfad()
		{
			this._Pfad = "PortalMAT";
		}
		
		override protected void SetzePortalTyp()
		{
			this._PortalTyp = typeof(IPortalLogik_MAT);
		}

		#endregion

		#region Konstruktor
		
		public Cst_MAT(Cst_Einstellung pin_Einstellung) : 
			base(pin_Einstellung)
		{
			
			// INIT Proxyobjekte
			this._PortalLogikMAT = (IPortalLogik_MAT) this._PortalLogik;
			
							
			// INIT allgFkt, Init benutzermenge
			InitDatenmenge();
		
			#region UpdateEvent registrieren
			// Event Handler erstellen, welche sich für benötigte pELS Objekte registrieren
			
			// registriere für Benutzer
			_ueh_Benutzer = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventBenutzer));
			this._Portal_Update.RegistriereFuerBenutzer(_ueh_Benutzer);

			// registriere für Aufträge
			_ueh_Auftraege = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventAuftraege));
			this._Portal_Update.RegistriereFuerAuftrag(_ueh_Auftraege);

			// registriere für Einheiten
			_ueh_Einheiten = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventEinheiten));
			this._Portal_Update.RegistriereFuerEinheit(_ueh_Einheiten);

			// registriere für Helfer
			_ueh_Helfer = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventHelfer));
			this._Portal_Update.RegistriereFuerHelfer(_ueh_Helfer);

			// registriere für KFZs
			_ueh_KFZs = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventKFZ));
			this._Portal_Update.RegistriereFuerKfZ(_ueh_KFZs);

			// registriere für Einsatzschwerpunkte
			_ueh_ESPs = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventESP));
			this._Portal_Update.RegistriereFuerEinsatzschwerpunkte(_ueh_ESPs);


			#endregion			
			// INIT Gui
			this._usc_Mat = new Cpr_usc_MAT(this);
			// Eventhandler übergeben.
			// Das Interface IReportRequested wird von Cst_client implementiert und stellt die Methode bereit, die das Event behandelt.
			_ev_ReportRequestedEvent += new ReportRequestedEventHandler((_Einstellung.O_Cst_Client as IReportRequested).BehandleReportRequestedEvent);
			
		}
		#endregion

		#region Isbe Members

		public Image GetSbeImage()
		{
			System.Reflection.Assembly asm_Sbe;
			//Informationen über die ausführende Assembly sammeln
			asm_Sbe = System.Reflection.Assembly.GetExecutingAssembly();
			//Hole Name der Assembly als AssemblyName
			System.Reflection.AssemblyName asm_SbeName = asm_Sbe.GetName();
			//Speichere den dll Namen im String
			string strAssemblyName = asm_SbeName.Name;
			//Erstelle ein Stream, aus dem die Icon Daten gelesen werden
			//hole Icon
			Image myImage = Image.FromFile(_strIconName);
			//Gebe myImage zurück
			return(myImage);
		}

		public String GetSbeName()
		{			
			return this._strSbeName;
		}

		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
		{
			_usc_Mat.SetzeRollenRechte(pin_i_aktuelleRolle);
		}

		public System.Windows.Forms.UserControl GetSbeUserControl()
		{
			return this._usc_Mat;
		}


		#endregion

		#region Funktionalität

		private void InitDatenmenge()
		{
			this._benutzermenge = this.HoleAlleBenutzer();
			this.HoleNachzuverfolgendeAuftraegeAusAPS();
			this._alleEinheiten = this.HoleAlleEinheiten();
			this._alleESP = this.HoleAlleESP();
			this._alleHelfer = this.HoleAlleHelfer();
			this._alleKFZ = this.HoleAlleKFZ();
		}
		/// <summary>
		/// Feuert das Event, wenn der "Drucken"-Buttom gedrückt wird.
		/// Übergeben wird die eben gespeicherte Mitteilung, die gedruckt werden soll
		/// </summary>
		public void FeuereReportRequestedEvent(Cdv_Mitteilung pin_mitteilung)
		{			
			_ev_ReportRequestedEvent( (object)pin_mitteilung);			
		}
		// Hole den aktuellen Benutzer zurück
		public Cdv_Benutzer HoleAktuellenBenutzer()
		{
			return this._Einstellung.Benutzer;
		}

		// Hole alle Systembenutzer zurück
		private Cdv_Benutzer[] HoleAlleBenutzer()
		{
			Cdv_Benutzer[] pout_benutzermenge;
			if((pout_benutzermenge = _Portal_AllgFkt.HoleAlleBenutzer())== null)
				pout_benutzermenge = new Cdv_Benutzer[0];
			else 
			{}
			return pout_benutzermenge;
		}

		public Cdv_Benutzer HoleBenutzer(int pin_ID)
		{
			return this._PortalLogikMAT.HoleBenutzer(pin_ID);
		}

		private Cdv_Einsatzschwerpunkt[] HoleAlleESP()
		{
			Cdv_Einsatzschwerpunkt[] pout_ESP;
			// Falls es noch keinen ESP vorhanden ist.
			if((pout_ESP = this._PortalLogikMAT.HoleAlleESP()) == null)
				pout_ESP = new Cdv_Einsatzschwerpunkt[0];
			else
			{}
			return pout_ESP;
		}

		public Cdv_Einsatzschwerpunkt HoleESP(int pin_ID)
		{
			Cdv_Einsatzschwerpunkt esp = this._PortalLogikMAT.HoleESP(pin_ID);
			return esp;
		}
		private Cdv_Einheit[] HoleAlleEinheiten()
		{
			Cdv_Einheit[] pout_Einheit;
			if((pout_Einheit = this._PortalLogikMAT.HoleAlleEinheiten()) == null)
				pout_Einheit = new Cdv_Einheit[0];
			else
			{}
			return pout_Einheit;
		}

		private Cdv_KFZ[] HoleAlleKFZ()
		{
			Cdv_KFZ[] pout_KFZ;
			if((pout_KFZ = this._PortalLogikMAT.HoleAlleKFZ()) == null)
				pout_KFZ = new Cdv_KFZ[0];
			else
			{}
			return pout_KFZ;
		}

		private Cdv_Helfer[] HoleAlleHelfer()
		{
			Cdv_Helfer[] pout_Helfer;
			if((pout_Helfer = this._PortalLogikMAT.HoleAlleHelfer()) == null)
				pout_Helfer = new Cdv_Helfer[0];
			else
			{}
			return pout_Helfer;
		}

			
		public Cdv_Helfer HoleHelfer(int pin_ID)
		{
			foreach(Cdv_Helfer h in this._alleHelfer)
			{
				if(h.ID == pin_ID)
					return h;
			}
			return null;
		}

		public Cdv_KFZ HoleKFZ(int pin_ID)
		{
			foreach(Cdv_KFZ kfz in this._alleKFZ)
			{
				if(kfz.ID == pin_ID)
					return kfz;
			}
			return null;
		}

		public Cdv_Einheit HoleEinheit(int pin_ID)
		{
			foreach(Cdv_Einheit e in this._alleEinheiten)
			{
				if(e.ID == pin_ID)
					return e;
			}
			return null;
		}



		private void HoleNachzuverfolgendeAuftraegeAusAPS()
		{
			this._nachzuverfolgendeAuftraege 
				= this._PortalLogikMAT.HoleNachzuverfolgendeAuftraege();
			// Falls keine Auftrag geladen wird
			if(this._nachzuverfolgendeAuftraege == null)
			{
				this._nachzuverfolgendeAuftraege = new Cdv_Auftrag[0];
			}
			
			
		}
 	
		// Gibt den Auftrag mit der ID aus _nachzuverfolgendeAuftraege zurück
		public Cdv_Auftrag HoleAuftrag(int pin_ID)
		{
			foreach(Cdv_Auftrag auftrag in this._nachzuverfolgendeAuftraege)
			{
				if(auftrag.ID == pin_ID)
					return auftrag;
			}
			return null;
		}

		
		public bool SpeichereMeldung(Cdv_Meldung pin_Meldung)
		{
			this._PortalLogikMAT.SpeichereMeldung(pin_Meldung);
			this.WerfeSystemereignis(pin_Meldung);
			return true;
		}

		public bool SpeichereErkundungsergebnis(Cdv_Erkundungsergebnis pin_Erkundungsergebnis)
		{
			this._PortalLogikMAT.SpeichereErkundungsergebnis(pin_Erkundungsergebnis);
			this.WerfeSystemereignis(pin_Erkundungsergebnis);
			return true;
		}
		public bool SpeichereAuftrag(Cdv_Auftrag pin_Auftrag)
		{
			this._PortalLogikMAT.SpeichereAuftrag(pin_Auftrag);
			this.WerfeSystemereignis(pin_Auftrag);
			return true;
		}

		public bool SpeichereTermin(Cdv_Termin pin_Termin)
		{
			this._PortalLogikMAT.SpeichereTermin(pin_Termin);
			return true;
		}
		public bool SpeichereErkundungsbefehl(Cdv_Erkundungsbefehl pin_Erkundungsbefehl)
		{
			this._PortalLogikMAT.SpeichereErkundungsbefehl(pin_Erkundungsbefehl);
			this.WerfeSystemereignis(pin_Erkundungsbefehl);
			return true;
		}

		private void WerfeSystemereignis(Cdv_Mitteilung pin_mit)
		{
			string str_Beschreibung = (pin_mit.GetType().ToString()).Replace("pELS.DV.Cdv_","");

			#region Empfängerliste als String zusammenstellen
			string str_Empfaenger = String.Empty;

			if (pin_mit.EmpfaengerMengeKraftID != null)
			foreach (int empf in pin_mit.EmpfaengerMengeKraftID)
			{
				bool _b_gefunden = false;
				
				foreach(Cdv_Einheit en in _PortalLogikMAT.HoleAlleEinheiten())
					if(empf == en.ID)
					{
						str_Empfaenger += en.Name+ ", ";
						_b_gefunden = true;
					}

				if (!_b_gefunden)
					foreach(Cdv_Helfer he in _PortalLogikMAT.HoleAlleHelfer())
						if(empf == he.ID)
						{
							str_Empfaenger += he.Personendaten.Vorname + " "+ he.Personendaten.Name+", ";
							_b_gefunden = true;
						}

				if (!_b_gefunden)
					foreach(Cdv_KFZ kfz in _PortalLogikMAT.HoleAlleKFZ())
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
				+ "\nÜbermittelt per "+ pin_mit.Uebermittlungsart;

			Cdv_Systemereignis mySyserg = new Cdv_Systemereignis(Einstellung.Benutzer.Benutzername, 
				DateTime.Now,
				str_Beschreibung,
				Tdv_SystemereignisArt.Mitteilung,
				false);
			this._Portal_AllgFkt.WerfeSystemereignis(mySyserg);
			#endregion
		}
		#endregion
		
		#region Eventhandler für Datenaktualisierung
		private void BehandleEventBenutzer(pELS.Events.UpdateEventArgs pin_e)
		{
			_benutzermenge = this.HoleAlleBenutzer();
			_usc_Mat.BeginInvoke(new UpdateGuiDelegate(this._usc_Mat.AktualisiereBenutzer));
			
		}

		private void BehandleEventAuftraege(pELS.Events.UpdateEventArgs pin_e)
		{
			this.HoleNachzuverfolgendeAuftraegeAusAPS();
			_usc_Mat.BeginInvoke(new UpdateGuiDelegate(this._usc_Mat.AktualisiereAuftrag));
		}
		
		private void BehandleEventESP(pELS.Events.UpdateEventArgs pin_e)		
		{
			_alleESP = this.HoleAlleESP();
//			this._usc_Mat.AkualisiereESP();
			_usc_Mat.BeginInvoke(new UpdateGuiDelegate(this._usc_Mat.AkualisiereESP));
		}


		private void BehandleEventEinheiten(pELS.Events.UpdateEventArgs pin_e)		
		{
			_alleEinheiten = this.HoleAlleEinheiten();
			//this._usc_Mat.AktualisiereEHK();
			_usc_Mat.BeginInvoke(new UpdateGuiDelegate(this._usc_Mat.AktualisiereEHK));
		}

		private void BehandleEventHelfer(pELS.Events.UpdateEventArgs pin_e)		
		{
			_alleHelfer = this.HoleAlleHelfer();
//			this._usc_Mat.AktualisiereEHK();
			_usc_Mat.BeginInvoke(new UpdateGuiDelegate(this._usc_Mat.AktualisiereEHK));
		}

		private void BehandleEventKFZ(pELS.Events.UpdateEventArgs pin_e)		
		{
			_alleKFZ = this.HoleAlleKFZ();
//			this._usc_Mat.AktualisiereEHK();
			_usc_Mat.BeginInvoke(new UpdateGuiDelegate(this._usc_Mat.AktualisiereEHK));
		}

		#endregion

		#region get-, set Methoden
	
		public Cdv_Benutzer[] AlleBenutzer
		{
			get{return this._benutzermenge;}
		}
		public Cdv_Auftrag[] NachzuverfolgendeAuftraege
		{
			get{return this._nachzuverfolgendeAuftraege;}
		}
		public Cdv_Einsatzschwerpunkt[] AlleESP
		{
			get{return this._alleESP;}
		}
		public Cdv_Einheit[] AlleEinheiten
		{
			get{return this._alleEinheiten;}
		}
		public Cdv_KFZ[] AlleKFZ
		{
			get{return this._alleKFZ;}
		}
		public Cdv_Helfer[] AlleHelfer
		{
			get{return this._alleHelfer;}
		}

		#endregion

		
	}
}
