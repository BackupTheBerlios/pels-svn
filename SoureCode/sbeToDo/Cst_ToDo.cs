using System;
using System.Drawing;



namespace pELS.Client.ToDo
{
	// benötigt für Cst_PortalLogik
	using pELS.Client;
	// benötigt für: Interface_Portale
	using pELS.APS.Server.Interface;
	// benötigt für: Cst_Einstellung
	using pELS.Tools.Client;
	// benötigt für: pELS-Objekte
	using pELS.DV;
	// benötigt für: IPelsObject
	using pELS.DV.Server.Interfaces;
	// benötigt für: Arraylist
	using System.Collections;

	/// <summary>
	/// Summary description for CsbeMAT.
	/// Ist erste Implementation (Referenz) zu Isbe-Interface
	/// </summary>
	public class Cst_ToDo : Cst_PortalLogik , pELS.GUI.Interface.Isbe
	{
		#region funktionale Variablen		
		/// <summary>
		/// das proxy-objekt der Klasse Cap_ToDo 
		/// </summary>
		private IPortalLogik_ToDo _PortalLogikToDo;

		#region ISBE
		/// <summary>
		/// Hält den Namen der Icon Datei fest
		/// </summary>
		private string _strIconName = @"SBEImages\todo.JPG";
		/// <summary>
		/// Hier wird die Beschriftung unterhalb des Icons festgehalten
		/// </summary>
		private string _strSbeName = "ToDo Liste";
		#endregion
		
		#region pELS-Objekte
		/// <summary>
		/// Alle anzuzeigenen Meldungen
		/// </summary>
		private Cdv_Meldung[] _ToDoListeMeldungen;
		/// <summary>
		/// Alle anzuzeigenen Aufträge
		/// </summary>
		private Cdv_Auftrag[] _ToDoListeAuftraege;
		/// <summary>
		/// Alle anzuzeigenen Termine
		/// </summary>
		private Cdv_Termin [] _ToDoListeTermine;
		#endregion

		#region eventHandler
		/// <summary>
		/// Update Eventhandler für Meldungen
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Meldungen;
		/// <summary>
		/// Update Eventhandler für Aufträge
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Auftraege;
		/// <summary>
		/// Update Eventhandler für Termine
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Termine;
		/// <summary>
		/// Delegate für die Aktualisierung der TreeViews durch den UI-Thread
		/// </summary>
		private delegate void UpdateTreeDelegate(); 
	
		#endregion
		/// <summary>
		/// Verweis auf die User-Control
		/// </summary>
		private Cpr_usc_TODO _usc_ToDo;
		/// <summary>
		/// registriert ob ein Event von diesem Cst ausgelöst wurde
		/// </summary>
		public bool _EventSelbstAusgeloest = false;
		#endregion

		#region SETs und GETs
		public Cdv_Meldung[] ToDoListeMeldungen
		{
			get{return _ToDoListeMeldungen;}
		}
		public Cdv_Auftrag[] ToDoListeAuftraege
		{
			get{return _ToDoListeAuftraege;}
		}
		public Cdv_Termin[] ToDoListeTermine
		{
			get{return _ToDoListeTermine;}
		}
		#endregion

		#region Konstruktor
		public Cst_ToDo(Cst_Einstellung pin_Einstellung): 
			base(pin_Einstellung)
		{			
			// INIT Proxyobjekte
			this._PortalLogikToDo = (IPortalLogik_ToDo) this._PortalLogik;

			#region UpdateEvents registrieren
			// Registriere für Meldungen
			_ueh_Meldungen = pELS.Events.UpdateEventAdapter.Create( 
				new pELS.Events.UpdateEventHandler(this.BehandleEventMeldungen));
			this._Portal_Update.RegistriereFuerMeldung(_ueh_Meldungen);
			// Registriere für Auftraege
			_ueh_Auftraege = pELS.Events.UpdateEventAdapter.Create( 
				new pELS.Events.UpdateEventHandler(this.BehandleEventAuftraege));
			this._Portal_Update.RegistriereFuerAuftrag(_ueh_Auftraege);
			// Registriere für Termine
			_ueh_Termine= pELS.Events.UpdateEventAdapter.Create( 
				new pELS.Events.UpdateEventHandler(this.BehandleEventTermine));
			this._Portal_Update.RegistriereFuerTermin(_ueh_Termine);			
			#endregion

			// Laden der benötigten Daten aus der Datenbank
			// (Meldungen, Aufträge, Termine)
			this.InitialisiereStartwerte();
			// INIT GUI
			this._usc_ToDo = new Cpr_usc_TODO(this);	
			
					
		}
		#endregion

		#region Cst_ToDo members
		/// <summary>
		/// Lädt beim initialisieren alle Meldungen, Aufträge und Termine
		/// </summary>
		private void InitialisiereStartwerte()
		{
			// Laden der ToDo_Liste für den aktuell angemeldeten Benutzer			
			this._ToDoListeMeldungen = _PortalLogikToDo.LadeMeldungenFuerToDoListe(_Einstellung.Benutzer);
			this._ToDoListeAuftraege = _PortalLogikToDo.LadeAuftraegeFuerToDoListe(_Einstellung.Benutzer);
			this._ToDoListeTermine   = _PortalLogikToDo.LadeTermineFuerToDoListe  (_Einstellung.Benutzer);
			//siehe Kommentar bei Methode "Cpr_usc_ToDo._timer_ErinnerungsTimer_Tick"
//			// Alle Termine, an die erinnert werden sollen in einer extra ArrayList speichern
//			_usc_ToDo.Ar_TermineZumErinnern.Clear();
//			foreach(Cdv_Termin t in _ToDoListeTermine)
//				if (t.WirdErinnert) _usc_ToDo.Ar_TermineZumErinnern.Add(t);

		}	
		/// <summary>
		/// Entfernt eine Meldung aus der ToDo-Liste und gibt den Aufruf
		/// auch nach unten weiter
		/// </summary>
		/// <param name="pin_meldung">zu entfernde Meldung</param>
		public void EntferneMeldungAusToDoListe(Cdv_Meldung pin_Meldung)
		{
			_EventSelbstAusgeloest = true;
			if(pin_Meldung != null)
				 _PortalLogikToDo.EntferneMeldungAusToDoListe(pin_Meldung);
			_EventSelbstAusgeloest = false;
			BehandleEventMeldungen(null);
		}
		/// <summary>
		/// Entfernt einen Auftrag aus der ToDo-Liste und gibt den Aufruf
		/// auch nach unten weiter
		/// </summary>
		/// <param name="pin_meldung">zu entfernder Aufruf</param>
		public void EntferneAuftragAusToDoListe(Cdv_Auftrag pin_Auftrag)
		{
			_EventSelbstAusgeloest = true;
			if(pin_Auftrag != null)
				_PortalLogikToDo.EntferneAuftragAusToDoListe(pin_Auftrag);
			_EventSelbstAusgeloest = false;
			BehandleEventAuftraege(null);
		}
		/// <summary>
		/// Entfernt einen Termin aus der ToDo-Liste und gibt den Aufruf
		/// auch nach unten weiter
		/// </summary>
		/// <param name="pin_meldung">zu entfernder Termin</param>
		public void EntferneTerminAusToDoListe(Cdv_Termin pin_Termin)
		{
			_EventSelbstAusgeloest = true;
			if(pin_Termin != null)
				_PortalLogikToDo.EntferneTerminAusToDoListe(pin_Termin);
			_EventSelbstAusgeloest = false;
			BehandleEventTermine(null);
		}

		/// <summary>
		/// Holt den Verfasser eines Auftrags, einer Meldung oder eines Termins
		/// (Siehe beschreibung im Interface)
		/// </summary>
		/// <param name="pin_BenutzerID"></param>
		/// <returns></returns>
		public Cdv_Benutzer ID2Benutzer(int pin_BenutzerID)
		{
			return _PortalLogikToDo.LadeBenutzer(pin_BenutzerID);
		}

		/// <summary>
		/// Holt die Empfänger für einen Auftrag oder eine Meldung und speichert die
		/// darzustellenden Namen in einem String-Array getrennt nach Einheiten, Helfern
		/// und Kfz
		/// </summary>
		/// <param name="pin_EmpfaengerIDMenge">die Empfängermenge, die geholt werden soll</param>
		/// <param name="pout_einheitenMenge">Namen der Einheiten</param>
		/// <param name="pout_helfermenge">Namen der Helfer</param>
		/// <param name="pout_kfzMenge">Namen der Kfz</param>
		public void HoleEmpfaenger(
			int[] pin_EmpfaengerIDMenge, 
			out String[] pout_einheitenMenge, 
			out String[] pout_helfermenge, 
			out String[] pout_kfzMenge)
		{
			_PortalLogikToDo.LadeEmpfaenger(pin_EmpfaengerIDMenge, out pout_einheitenMenge, out pout_helfermenge, out pout_kfzMenge);
		}

		public Cdv_Einsatzschwerpunkt ID2ESP(int pin_ID)
		{
			return _PortalLogikToDo.LadeESP(pin_ID);
		}

		#endregion

		#region Update Eventhandler

		// Todo: Testen
		private void BehandleEventMeldungen(pELS.Events.UpdateEventArgs pin_e)
		{
			if(!_EventSelbstAusgeloest)
			{
				this._ToDoListeMeldungen = _PortalLogikToDo.LadeMeldungenFuerToDoListe(_Einstellung.Benutzer);
				_usc_ToDo.BeginInvoke(new UpdateTreeDelegate(_usc_ToDo.AktualisiereTreeView));
				#region Optimierungscode - für später mal
				//			// speichert die IDs aller neuen Meldungen
				//			ArrayList neueMeldungsIDs = new ArrayList();
				//			
				//			// überprüfe, ob eine ID aus dem EventArgument schon 
				//			// in der Menge der gehaltenen Objekte vorhanden ist...
				//			foreach(int EventID in pin_e.IDMenge)
				//			{				
				//				for(int i = 0; i < _ToDoListeMeldungen.Length; i++)				
				//					if(_ToDoListeMeldungen[i].ID == EventID)		
				//						// ...falls ja, dann überschreibe das veraltete Objekt mit dem Neuen 
				//
				//				// TODO: dies ist nur gültig, wenn EmpfaengerBenutzerID nicht mehr verändert werden kann
				//
				//						_ToDoListeMeldungen[i] = this._PortalLogikToDo.LadeMeldung(_Einstellung.Benutzer, EventID);
				//						// ...falls nein, dann nehme die EventID zu den neuen IDs auf
				//					else				
				//						neueMeldungsIDs.Add(EventID);
				//			}
				//
				//			// beinhaltet alle neuen Meldungen
				//			ArrayList neueMeldungen = new ArrayList();
				//			// hole alle neuen Meldungen
				//			foreach (int ID in neueMeldungsIDs)
				//			{
				//				Cdv_Meldung neueMeldung = this._PortalLogikToDo.LadeMeldung(_Einstellung.Benutzer, ID);
				//				if (neueMeldung != null) neueMeldungen.Add(neueMeldung);
				//			}
				//
				//			// erstelle neues Objektarray mit der Länge der alten plus der neuen Objekte
				//			Cdv_Meldung[] neueMeldungsMenge = new Cdv_Meldung[_ToDoListeMeldungen.Length + neueMeldungen.Count];
				//			// kopiere Objekte in die neue Liste
				//			_ToDoListeMeldungen.CopyTo(neueMeldungsMenge, 0);
				//			neueMeldungen.CopyTo(neueMeldungsMenge, _ToDoListeMeldungen.Length);
				//			_ToDoListeMeldungen = neueMeldungsMenge;		
				//
				#endregion
			}
		}

		private void BehandleEventAuftraege(pELS.Events.UpdateEventArgs pin_e)
		{
			if(!_EventSelbstAusgeloest)
			{
				this._ToDoListeAuftraege = _PortalLogikToDo.LadeAuftraegeFuerToDoListe(_Einstellung.Benutzer);
				_usc_ToDo.BeginInvoke(new UpdateTreeDelegate(_usc_ToDo.AktualisiereTreeView));
			}
		}
		
		private void BehandleEventTermine(pELS.Events.UpdateEventArgs pin_e)
		{			
			if(!_EventSelbstAusgeloest)
			{
				this._ToDoListeTermine   = _PortalLogikToDo.LadeTermineFuerToDoListe  (_Einstellung.Benutzer);
				//siehe Kommentar bei Methode "Cpr_usc_ToDo._timer_ErinnerungsTimer_Tick"
//				// Alle Termine, an die erinnert werden sollen in einer extra ArrayList speichern
//				_usc_ToDo.Ar_TermineZumErinnern.Clear();
//				foreach(Cdv_Termin t in _ToDoListeTermine)
//					if (t.WirdErinnert) _usc_ToDo.Ar_TermineZumErinnern.Add(t);

				_usc_ToDo.BeginInvoke(new UpdateTreeDelegate(_usc_ToDo.AktualisiereTreeView));				
			}
		}
		

		#endregion

		#region Cst_PortalLogik members
		override protected void SetzeRemotingPfad()
		{
			this._Pfad = "PortalToDo";
		}
		
		override protected void SetzePortalTyp()
		{
			this._PortalTyp = typeof(IPortalLogik_ToDo);
		}
		#endregion

		#region Isbe members
		public Image GetSbeImage()
		{
			System.Reflection.Assembly asm_Sbe;
			//Informationen über die ausführende Assembly sammeln
			asm_Sbe = System.Reflection.Assembly.GetExecutingAssembly();
			//Liefere Name der Assembly als AssemblyName
			System.Reflection.AssemblyName asm_SbeName = asm_Sbe.GetName();
			//Speichere den dll Namen im String
			string strAssemblyName = asm_SbeName.Name;
			//Erstelle ein Stream, aus dem die Icon Daten gelesen werden
			//Icon holen
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
			// nicht nötig, da hier alle Benutzer die gleichen Rechte haben
		}
		public System.Windows.Forms.UserControl GetSbeUserControl()
		{			
			return this._usc_ToDo;
		}


		#endregion

 		
	}
}
