using System;
// benötigt für: IPelsObject
using pELS.DV.Server.Interfaces;
// benötigt für: Cdv_XXX
using pELS.DV;

namespace pELS.APS.Server.Interface
{
	/// <summary>
	/// Summary description for IPortaLogik_ToDo.
	/// </summary>
	public interface IPortalLogik_ToDo
	{		
		/// <summary>
		/// Ändert das Attribut "istInToDoListe" einer Meldung auf der DB.
		/// Dazu wird das entsprechende Attribut auf false gesetzt und der 
		/// Datensatz in die DB geschrieben
		/// </summary>
		/// <param name="pin_meldung">Die Meldung, die nicht mehr angezeigt werden soll</param>
		/// <returns></returns>
		bool EntferneMeldungAusToDoListe(Cdv_Meldung pin_meldung);
		/// <summary>
		/// Ändert das Attribut "istInToDoListe" eines Auftrags auf der DB.
		/// Dazu wird das entsprechende Attribut auf false gesetzt und der 
		/// Datensatz in die DB geschrieben
		/// </summary>
		/// <param name="pin_meldung">Der Auftrag, der nicht mehr angezeigt werden soll</param>
		/// <returns></returns>
		bool EntferneAuftragAusToDoListe(Cdv_Auftrag pin_auftrag);	
		/// <summary>
		/// Ändert das Attribut "istInToDoListe" eine Termins auf der DB.
		/// Dazu wird das entsprechende Attribut auf false gesetzt und der 
		/// Datensatz in die DB geschrieben
		/// </summary>
		/// <param name="pin_meldung">Der Termin, der nicht mehr angezeigt werden soll</param>
		/// <returns></returns>
		bool EntferneTerminAusToDoListe(Cdv_Termin pin_termin);		
		/// <summary>
		/// Lädt einen Benutzer. Gedacht, um den Verfasser eines Auftrags, einer Meldung
		/// und eines Termins zu erhalten
		/// </summary>
		/// <param name="pin_BenutzerID">Die ID des zu holenden Benutzers</param>
		/// <returns>Den gewünschten Benutzer</returns>
		Cdv_Benutzer LadeBenutzer(int pin_BenutzerID);		
		/// <summary>
		/// Gibt die Namen der Empfänger einer Meldung oder eines Auftrags getrennt nach Einheiten
		/// Helfer und Kfz zurück. So können die Namen der Empfänger in dem Empfänger-Treeview 
		/// dargestellt werden.
		/// </summary>
		/// <param name="pin_EmpfaengerIDMenge">Die EmpfängerIDs</param>
		/// <param name="pout_EinheitenMenge">Namen der Einheiten, die Empfänger sind</param>
		/// <param name="pout_Helfermenge">Namen der Helfer, die Empfänger sind</param>
		/// <param name="pout_KfzMenge">Namen der Kfz, die Empfänger sind</param>
		void LadeEmpfaenger(
			int[] pin_EmpfaengerIDMenge, 
			out String[] pout_EinheitenMenge, 
			out String[] pout_Helfermenge, 
			out String[] pout_KfzMenge);
		/// <summary>
		/// Lade Alle Meldungen, die für einen bestimmten Benutzer bestimmt sind
		/// und bei denen "istInToDoListe" == true ist
		/// </summary>
		/// <param name="pin_benutzer">
		/// aktueller Benutzer für den die Meldungen geholt werden sollen</param>
		/// <returns>Die Meldungen für die ToDo-Liste des aktuellen Benutzers</returns>
		Cdv_Meldung[] LadeMeldungenFuerToDoListe(Cdv_Benutzer pin_benutzer);
		/// <summary>
		/// Lade Alle Auftraege, die für einen bestimmten Benutzer bestimmt sind
		/// und bei denen "istInToDoListe" == true ist
		/// </summary>
		/// <param name="pin_benutzer">
		/// aktueller Benutzer für den die Auftraege geholt werden sollen</param>
		/// <returns>Die Auftraege für die ToDo-Liste des aktuellen Benutzers</returns>
		Cdv_Auftrag[] LadeAuftraegeFuerToDoListe(Cdv_Benutzer pin_benutzer);
		/// <summary>
		/// Lade Alle Termine, die für einen bestimmten Benutzer bestimmt sind
		/// und bei denen "istInToDoListe" == true ist
		/// </summary>
		/// <param name="pin_benutzer">
		/// aktueller Benutzer für den die Termine geholt werden sollen</param>
		/// <returns>Die Termine für die ToDo-Liste des aktuellen Benutzers</returns>
		Cdv_Termin [] LadeTermineFuerToDoListe  (Cdv_Benutzer pin_benutzer);
		/// <summary>
		/// Dient zum Nachladen einer Meldung bei Bearbeitung des Update-Events
		/// in der Steuerungsschicht, also wenn es eine neue Meldung gibt
		/// </summary>
		/// <param name="pin_benutzer">
		///		Benutzer, für den die Meldung für die ToDo_Liste geholt wird</param>
		/// <param name="pin_pELSID">MeldungsID</param>
		/// <returns>neue Meldung aus der Datenbank</returns>
		Cdv_Meldung LadeMeldung(Cdv_Benutzer pin_benutzer, int pin_pELSID);
		/// <summary>
		/// Dient zum Nachladen eines Auftrags bei Bearbeitung des Update-Events
		/// in der Steuerungsschicht, also wenn es einen neuen Auftrag gibt
		/// </summary>
		/// <param name="pin_benutzer">
		/// Benutzer, für den der Auftrag für die ToDo_Liste geholt wird
		/// <param name="pin_pELSID">AuftragsID</param>
		/// <returns>neuer Auftrag aus der Datenbank</returns>
		Cdv_Auftrag LadeAuftrag(Cdv_Benutzer pin_benutzer, int pin_pELSID);
		/// <summary>
		/// Dient zum Nachladen eines Termins bei Bearbeitung des Update-Events
		/// in der Steuerungsschicht, also wenn es einen neuen Termin gibt
		/// </summary>
		/// <param name="pin_benutzer">
		/// Benutzer, für den der Termin für die ToDo_Liste geholt wird</param>
		/// <param name="pin_pELSID">MeldungsID</param>
		/// <returns>neuer Termin aus der Datenbank</returns>
		Cdv_Termin  LadeTermin (Cdv_Benutzer pin_benutzer, int pin_pELSID);
		/// <summary>
		/// lädt einen Einsatzschwerpunkt mit einer spezifischen ID
		/// </summary>
		/// <param name="pin_ID"></param>
		/// <returns></returns>
		Cdv_Einsatzschwerpunkt LadeESP(int pin_ID);
	}
}
