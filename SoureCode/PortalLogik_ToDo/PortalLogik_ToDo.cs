using System;
using System.Windows.Forms;

namespace PortalLogik_ToDo
{
	// ben�tigt f�r: Cap_PortalLogik
	using pELS.Server;
	// ben�tigt f�r: IPortalLogik_XXX
	using pELS.APS.Server.Interface;
	// ben�tigt f�r: pELS.DV.Cdv_XXX
	using pELS.DV;
	// ben�tigt f�r: ObjectManager
	using pELS.DV.Server.ObjectManager;
	// ben�tigt f�r: pels-Objecte
	using pELS.DV.Server.Interfaces;
	// ben�tigt f�r: ArrayList
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
			// falls die EmpfaengerIDMenge leer ist gebe leere Mengen zur�ck
			if (pin_EmpfaengerIDMenge == null)
			{
				pout_einheitenMenge = new String[0];
				pout_helfermenge = new String[0];
				pout_kfzMenge = new String[0];

			}
			else
			{
				// Zwischenspeicher f�r die Ergebnisse
				ArrayList temp_EinheitenMenge = new ArrayList();
				ArrayList temp_HelferMenge = new ArrayList();
				ArrayList temp_KfzMenge = new ArrayList();
				// per ID angefragtes PelsObjekt
				IPelsObject ipo = new Cdv_pELSObject();

				// Holen der Empf�nger. Dabei k�nnte eine Empf�ngerID eine Einheit
				// ein Helfer oder ein Kfz sein. 
				foreach(int i in pin_EmpfaengerIDMenge)
				{				
					// Pr�fe, ob aktuelles i eine Einheit ist
					ipo = _ObjektManager.Einheiten.Holen(i);
					if (ipo != null) temp_EinheitenMenge.Add( ((Cdv_Einheit)ipo).ToString() );
						// wenn nicht, dann pr�fen, ob i ein Helfer ist
					else
					{
						ipo = _ObjektManager.Helfer.Holen(i);
						if (ipo != null) temp_HelferMenge.Add( ((Cdv_Helfer )ipo).ToString() );
							// wenn nicht, dann pr�fen, ob i ein Kfz ist
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
			// Zwischenspeicher f�r alle ben�tigten Meldungen (Benutzer stimmt und istInToDoListe == true)
			ArrayList al_meldungen = new ArrayList();
			// Ben�tigten Meldungen in ArrayList schreiben
			foreach(Cdv_Meldung m in meldungsMenge)
				if ( (m.EmpfaengerBenutzerID == pin_benutzer.ID) && (m.IstInToDoListe == true) ) 
					al_meldungen.Add(m);

			// ben�tigte Meldungen
			Cdv_Meldung[] pout_meldungsMenge = new Cdv_Meldung[al_meldungen.Count];
			// Meldungen von ArrayList nach Cdv_MeldungsArray �berf�hren
			al_meldungen.CopyTo(pout_meldungsMenge);
			
			if (pout_meldungsMenge.Length != 0) return pout_meldungsMenge;
			else return null;
		}

		public Cdv_Auftrag[] LadeAuftraegeFuerToDoListe(Cdv_Benutzer pin_benutzer)
		{
			// Alle Auftr�ge aus der DB
			IPelsObject[] temp_ipoa = _ObjektManager.Auftraege.HolenAlle();
			Cdv_Auftrag[] auftragsMenge = new Cdv_Auftrag[temp_ipoa.Length];
			temp_ipoa.CopyTo(auftragsMenge, 0);
			// Zwischenspeicher f�r alle ben�tigten Auftraege (Benutzer stimmt und istInToDoListe == true)
			ArrayList al_auftraege = new ArrayList();
			// Ben�tigten Auftraege in ArrayList schreiben
			foreach(Cdv_Auftrag a in auftragsMenge)
				if ( (a.EmpfaengerBenutzerID == pin_benutzer.ID) && (a.IstInToDoListe == true) ) 
					al_auftraege.Add(a);

			// ben�tigte Auftraege
			Cdv_Auftrag[] pout_auftragsMenge = new Cdv_Auftrag[al_auftraege.Count];
			// Auftraege von ArrayList nach Cdv_AuftragsArray �berf�hren
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
			// Zwischenspeicher f�r alle ben�tigten Termine (Benutzer stimmt und istInToDoListe == true)
			ArrayList al_termine = new ArrayList();
			// Ben�tigten Termine in ArrayList schreiben
			foreach(Cdv_Termin t in terminMenge)
				if ( (t.ErstelltFuerBenutzerID == pin_benutzer.ID) && (t.IstInToDoListe == true) ) 
					al_termine.Add(t);

			// ben�tigte Termine
			Cdv_Termin[] pout_terminMenge = new Cdv_Termin[al_termine.Count];
			// Termine von ArrayList nach Cdv_TerminArray �berf�hren
			al_termine.CopyTo(pout_terminMenge);

			if (pout_terminMenge.Length != 0) return pout_terminMenge;
			else return null;
		}

		public Cdv_Meldung LadeMeldung(Cdv_Benutzer pin_benutzer, int pin_pELSID)
		{
			// Hole die Meldung mit der �bergebenen ID
			IPelsObject temp_ipo = _ObjektManager.Meldungen.Holen(pin_pELSID);
			Cdv_Meldung pout_meldung = (Cdv_Meldung)temp_ipo;
			// Pr�fe ob der aktuelle Benutzer der Empf�nger dieser Meldung ist
			// wenn ja, Meldung zur�ckgeben, sonst nichts zur�ckgeben
			if (pout_meldung.EmpfaengerBenutzerID == pin_benutzer.ID) return pout_meldung;
			else return null;
		}

		public Cdv_Auftrag LadeAuftrag(Cdv_Benutzer pin_benutzer, int pin_pELSID)
		{
			// Hole den Auftrag mit der �bergebenen ID
			IPelsObject temp_ipo = _ObjektManager.Auftraege.Holen(pin_pELSID);
			Cdv_Auftrag pout_auftrag = (Cdv_Auftrag)temp_ipo;
			// Pr�fe ob der aktuelle Benutzer der Empf�nger dieses Auftrags ist
			// wenn ja, Auftrag zur�ckgeben, sonst nichts zur�ckgeben
			if (pout_auftrag.EmpfaengerBenutzerID == pin_benutzer.ID) return pout_auftrag;
			else return null;
		}

		public Cdv_Termin  LadeTermin (Cdv_Benutzer pin_benutzer, int pin_pELSID)
		{
			// Hole des Termins mit der �bergebenen ID
			IPelsObject temp_ipo = _ObjektManager.Termine.Holen(pin_pELSID);
			Cdv_Termin pout_termin = (Cdv_Termin)temp_ipo;
			// Pr�fe ob der aktuelle Benutzer der Empf�nger dieses Termins ist
			// wenn ja, Termin zur�ckgeben, sonst nichts zur�ckgeben
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
