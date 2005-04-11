using System;
using System.Windows.Forms;

namespace PortalLogik_ToDo
{
	// benötigt für: Cap_PortalLogik
	using pELS.Server;
	// benötigt für: IPortalLogik_XXX
	using pELS.APS.Server.Interface;
	// benötigt für: pELS.DV.Cdv_XXX
	using pELS.DV;
	// benötigt für: ObjectManager
	using pELS.DV.Server.ObjectManager;
	// benötigt für: pels-Objecte
	using pELS.DV.Server.Interfaces;
	// benötigt für: ArrayList
	using System.Collections;

	/// <summary>
	/// Summary description for PortalLogik_ToDo
	/// </summary>
	public class CPortalLogik_ToDo : Cap_PortalLogik , IPortalLogik_ToDo	
	{
		public CPortalLogik_ToDo(int pin_OMPort, string pin_URL, int pin_Port) : 
			base(pin_OMPort, pin_URL, pin_Port)
		{	
		}

		
		#region Cap_PortalLogik members
		protected override void SetzeRemotingPfad()
		{
			this._Pfad = "PortalToDo";
		}

		public override void StartePortalLogik()
		{
		}
		#endregion

		#region IPortaLogik_ToDo Members
		public bool EntferneMeldungAusToDoListe(Cdv_Meldung pin_meldung)
		{
			try
			{
				// istInToDoListe auf false setzen, dadurch wird die Meldung nicht 
				// mehr in die ToDo-Liste des ehemaligen Benutzers gesetzt
				pin_meldung.IstInToDoListe = false;
				_ObjektManager.Meldungen.Speichern(pin_meldung);
				return true;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool EntferneAuftragAusToDoListe(Cdv_Auftrag pin_auftrag)
		{
			try
			{
				// istInToDoListe auf false setzen, dadurch wird die Meldung nicht 
				// mehr in die ToDo-Liste des ehemaligen Benutzers gesetzt
				pin_auftrag.IstInToDoListe = false;
				_ObjektManager.Auftraege.Speichern(pin_auftrag);
				return true;
			}
			catch (Exception e)
			{
				throw e;
			}			
		}

		public bool EntferneTerminAusToDoListe(Cdv_Termin pin_termin)
		{
			try
			{
				// istInToDoListe auf false setzen, dadurch wird die Meldung nicht 
				// mehr in die ToDo-Liste des ehemaligen Benutzers gesetzt
				pin_termin.IstInToDoListe = false;
				_ObjektManager.Termine.Speichern(pin_termin);
				return true;
			}
			catch (Exception e)
			{
				throw e;
			}
		}		

		public void LadeEmpfaenger(
			int[] pin_EmpfaengerIDMenge, 
			out String[] pout_einheitenMenge, 
			out String[] pout_helfermenge, 
			out String[] pout_kfzMenge)
		{
			// falls die EmpfaengerIDMenge leer ist gebe leere Mengen zurück
			if (pin_EmpfaengerIDMenge == null)
			{
				pout_einheitenMenge = new String[0];
				pout_helfermenge = new String[0];
				pout_kfzMenge = new String[0];

			}
			else
			{
				// Zwischenspeicher für die Ergebnisse
				ArrayList temp_EinheitenMenge = new ArrayList();
				ArrayList temp_HelferMenge = new ArrayList();
				ArrayList temp_KfzMenge = new ArrayList();
				// per ID angefragtes PelsObjekt
				IPelsObject ipo = new Cdv_pELSObject();

				// Holen der Empfänger. Dabei könnte eine EmpfängerID eine Einheit
				// ein Helfer oder ein Kfz sein. 
				foreach(int i in pin_EmpfaengerIDMenge)
				{				
					// Prüfe, ob aktuelles i eine Einheit ist
					ipo = _ObjektManager.Einheiten.Holen(i);
					if (ipo != null) temp_EinheitenMenge.Add( ((Cdv_Einheit)ipo).ToString() );
						// wenn nicht, dann prüfen, ob i ein Helfer ist
					else
					{
						ipo = _ObjektManager.Helfer.Holen(i);
						if (ipo != null) temp_HelferMenge.Add( ((Cdv_Helfer )ipo).ToString() );
							// wenn nicht, dann prüfen, ob i ein Kfz ist
						else
						{
							ipo = _ObjektManager.Kfz.Holen(i);
							if (ipo != null) temp_KfzMenge.Add( ((Cdv_KFZ)ipo).ToString() );
						}
					}
				}

				// Kopieren der Arraylists in die entsprechenden String[]
				// Einheiten
				pout_einheitenMenge = new String[temp_EinheitenMenge.Count];
				temp_EinheitenMenge.CopyTo(pout_einheitenMenge);
				// Helfer
				pout_helfermenge = new String[temp_HelferMenge.Count];
				temp_HelferMenge.CopyTo(pout_helfermenge);
				// Kfz
				pout_kfzMenge = new String[temp_KfzMenge.Count];
				temp_KfzMenge.CopyTo(pout_kfzMenge);	
			}
		}

		public Cdv_Benutzer LadeBenutzer(int pin_BenutzerID)
		{
			IPelsObject temp_ipo = _ObjektManager.Benutzer.Holen(pin_BenutzerID);
			Cdv_Benutzer pout_benutzer = (Cdv_Benutzer) temp_ipo;
			if (pout_benutzer != null) return pout_benutzer;
			else return null;
		}

		public Cdv_Meldung[] LadeMeldungenFuerToDoListe(Cdv_Benutzer pin_benutzer)
		{
			// Alle Meldungen aus der DB 						
			IPelsObject[] temp_ipoa = _ObjektManager.Meldungen.HolenAlle();							
			Cdv_Meldung[] meldungsMenge = new Cdv_Meldung[temp_ipoa.Length];
			temp_ipoa.CopyTo(meldungsMenge, 0);							
			// Zwischenspeicher für alle benötigten Meldungen (Benutzer stimmt und istInToDoListe == true)
			ArrayList al_meldungen = new ArrayList();
			// Benötigten Meldungen in ArrayList schreiben
			foreach(Cdv_Meldung m in meldungsMenge)
				if ( (m.EmpfaengerBenutzerID == pin_benutzer.ID) && (m.IstInToDoListe == true) ) 
					al_meldungen.Add(m);

			// benötigte Meldungen
			Cdv_Meldung[] pout_meldungsMenge = new Cdv_Meldung[al_meldungen.Count];
			// Meldungen von ArrayList nach Cdv_MeldungsArray überführen
			al_meldungen.CopyTo(pout_meldungsMenge);
			
			if (pout_meldungsMenge.Length != 0) return pout_meldungsMenge;
			else return null;
		}

		public Cdv_Auftrag[] LadeAuftraegeFuerToDoListe(Cdv_Benutzer pin_benutzer)
		{
			// Alle Aufträge aus der DB
			IPelsObject[] temp_ipoa = _ObjektManager.Auftraege.HolenAlle();
			Cdv_Auftrag[] auftragsMenge = new Cdv_Auftrag[temp_ipoa.Length];
			temp_ipoa.CopyTo(auftragsMenge, 0);
			// Zwischenspeicher für alle benötigten Auftraege (Benutzer stimmt und istInToDoListe == true)
			ArrayList al_auftraege = new ArrayList();
			// Benötigten Auftraege in ArrayList schreiben
			foreach(Cdv_Auftrag a in auftragsMenge)
				if ( (a.EmpfaengerBenutzerID == pin_benutzer.ID) && (a.IstInToDoListe == true) ) 
					al_auftraege.Add(a);

			// benötigte Auftraege
			Cdv_Auftrag[] pout_auftragsMenge = new Cdv_Auftrag[al_auftraege.Count];
			// Auftraege von ArrayList nach Cdv_AuftragsArray überführen
			al_auftraege.CopyTo(pout_auftragsMenge);

			if (pout_auftragsMenge.Length != 0) return pout_auftragsMenge;
			else return null;
		}

		public Cdv_Termin [] LadeTermineFuerToDoListe  (Cdv_Benutzer pin_benutzer)
		{
			// Alle Termine aus der DB
			IPelsObject[] temp_ipoa = _ObjektManager.Termine.HolenAlle();
            Cdv_Termin[] terminMenge = new Cdv_Termin[temp_ipoa.Length];
			temp_ipoa.CopyTo(terminMenge, 0);
			// Zwischenspeicher für alle benötigten Termine (Benutzer stimmt und istInToDoListe == true)
			ArrayList al_termine = new ArrayList();
			// Benötigten Termine in ArrayList schreiben
			foreach(Cdv_Termin t in terminMenge)
				if ( (t.ErstelltFuerBenutzerID == pin_benutzer.ID) && (t.IstInToDoListe == true) ) 
					al_termine.Add(t);

			// benötigte Termine
			Cdv_Termin[] pout_terminMenge = new Cdv_Termin[al_termine.Count];
			// Termine von ArrayList nach Cdv_TerminArray überführen
			al_termine.CopyTo(pout_terminMenge);

			if (pout_terminMenge.Length != 0) return pout_terminMenge;
			else return null;
		}

		public Cdv_Meldung LadeMeldung(Cdv_Benutzer pin_benutzer, int pin_pELSID)
		{
			// Hole die Meldung mit der übergebenen ID
			IPelsObject temp_ipo = _ObjektManager.Meldungen.Holen(pin_pELSID);
			Cdv_Meldung pout_meldung = (Cdv_Meldung)temp_ipo;
			// Prüfe ob der aktuelle Benutzer der Empfänger dieser Meldung ist
			// wenn ja, Meldung zurückgeben, sonst nichts zurückgeben
			if (pout_meldung.EmpfaengerBenutzerID == pin_benutzer.ID) return pout_meldung;
			else return null;
		}

		public Cdv_Auftrag LadeAuftrag(Cdv_Benutzer pin_benutzer, int pin_pELSID)
		{
			// Hole den Auftrag mit der übergebenen ID
			IPelsObject temp_ipo = _ObjektManager.Auftraege.Holen(pin_pELSID);
			Cdv_Auftrag pout_auftrag = (Cdv_Auftrag)temp_ipo;
			// Prüfe ob der aktuelle Benutzer der Empfänger dieses Auftrags ist
			// wenn ja, Auftrag zurückgeben, sonst nichts zurückgeben
			if (pout_auftrag.EmpfaengerBenutzerID == pin_benutzer.ID) return pout_auftrag;
			else return null;
		}

		public Cdv_Termin  LadeTermin (Cdv_Benutzer pin_benutzer, int pin_pELSID)
		{
			// Hole des Termins mit der übergebenen ID
			IPelsObject temp_ipo = _ObjektManager.Termine.Holen(pin_pELSID);
			Cdv_Termin pout_termin = (Cdv_Termin)temp_ipo;
			// Prüfe ob der aktuelle Benutzer der Empfänger dieses Termins ist
			// wenn ja, Termin zurückgeben, sonst nichts zurückgeben
			if (pout_termin.ErstelltFuerBenutzerID == pin_benutzer.ID) return pout_termin;
			else return null;
		}
		public Cdv_Einsatzschwerpunkt LadeESP(int pin_ID)
		{
			return (Cdv_Einsatzschwerpunkt) _ObjektManager.Einsatzschwerpunkte.Holen(pin_ID);
		}
		#endregion
	}
}
