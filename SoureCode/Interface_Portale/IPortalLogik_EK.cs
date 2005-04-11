using System;
// benötigt für: IPelsObject
using pELS.DV.Server.Interfaces;
// benötigt für: pELS-Objekte
using pELS.DV;

namespace pELS.APS.Server.Interface
{
	/// <summary>
	/// Interface, dass alle für das Portal EK benötigten Methoden zur
	/// Verfügung stellt
	/// </summary>
	public interface IPortalLogik_EK
	{
		#region Hole- Methoden
		/// <summary>
		/// Gibt alle Helfer aus der Datenverwaltungsschicht
		/// </summary>
		/// <returns>Liefert die Helfer als Array</returns>
		Cdv_Helfer[] HoleAlleHelfer();
		/// <summary>
		/// Gibt den Helfer mit der entsprechenden ID aus der Datenverwaltungsschicht
		/// </summary>
		/// <param name="pin_HelferID">Helfer ID</param>
		/// <returns>Helfer mit ID</returns>
		Cdv_Helfer HoleHelfer(int pin_HelferID);
		/// <summary>
		/// Gibt alle Helfer aus der Datenverwaltungsschicht die einer Einheit angehören
		/// </summary>
		/// <param name="pin_EinheitID">ID der Einheit</param>
		/// <returns>Array der Helfer</returns>
		Cdv_Helfer[] HoleHelferZurEinheit(int pin_EinheitID);
		/// <summary>
		/// Gibt alle Einheiten aus der Datenverwaltungsschicht
		/// </summary>
		/// <returns></returns>
		Cdv_Einheit[] HoleAlleEinheiten();
		/// <summary>
		/// Gibt eine Einheit aus der Datenverwaltungsschicht
		/// </summary>
		/// <param name="pin_EinheitsID">ID der Einheit</param>
		/// <returns>Einheit mit der gesuchten ID</returns>
		Cdv_Einheit HoleEinheit(int pin_EinheitsID);
		/// <summary>
		/// Gibt alle KFZ aus der Datenverwaltungsschicht
		/// </summary>
		/// <returns>Array der KFZ</returns>
		Cdv_KFZ[] HoleKFZ();
		/// <summary>
		/// Holt ein KFZ mit entsprechender ID aus der DV-Schicht
		/// </summary>
		/// <param name="pin_KfzID">KFZ-ID des gesuchten KFZ</param>
		/// <returns>gesuchtes KFZ</returns>
		Cdv_KFZ HoleKfz(int pin_KfzID);
		/// <summary>
		/// Holt alle Module aus der DV-Schicht
		/// </summary>
		/// <returns>Array der Module</returns>
		Cdv_Modul[] HoleModule();
		/// <summary>
		/// Lädt ein Modul mit einer konkreten ID
		/// </summary>
		/// <param name="pin_ID">ID des gesuchten Modul</param>
		/// <returns>konkretes Modul</returns>
		Cdv_Modul HoleModul(int pin_ID);
		/// <summary>
		/// Liefert den laufenden Einsatz
		/// </summary>
		/// <returns>Cdv_Einsatz</returns>
		Cdv_Einsatz HoleEinsatz();
		/// <summary>
		/// Lädt alle Materialien aus der DV-Schicht
		/// </summary>
		/// <returns>Array der Materialien</returns>
		Cdv_Material[] HoleAlleMaterial();
		/// <summary>
		/// Lädt Material mit einer konkreten ID
		/// </summary>
		/// <param name="pin_MaterialID">ID des gesuchten Material</param>
		/// <returns>konkretes Material</returns>
		Cdv_Material HoleMaterial(int pin_MaterialID);
		/// <summary>
		///  Lädt alle einer Einheit zugeordneten Materialien
		/// </summary>
		/// <param name="pin_EinheitID">Id der Einheit deren Material gesucht wird</param>
		/// <returns>Array der Materialien</returns>
		Cdv_Material[] HoleAlleMaterialZuEinheit(int pin_EinheitID);
                /// <summary>
                /// Liefert ein OV-Objekt mit der konkreten ID aus der DV-Schicht
                /// </summary>
                /// <param name="pin_ID">ID des Ortsverband</param>
                /// <returns>Ortsverband mit spezieller ID</returns>
		Cdv_Ortsverband HoleOV(int pin_ID);
		/// <summary>
		/// Lädt alle Ortsverbände aus der DV-Schicht
		/// </summary>
		/// <returns>Array der Ortsverbände</returns>
		Cdv_Ortsverband[] HoleAlleOrtsverbaende();
		/// <summary>
		/// Lädt alle Einheiten aus der DV-Schicht
		/// </summary>
		/// <returns>Array der Einheiten</returns>
		//Cdv_Einheit[] HoleAlleEinheiten();
		/// <summary>
		/// Lädt alle Einsatzschwerpunkte aus der Datenhaltung
		/// </summary>
		/// <returns>Array der Einsatzschwerpunkte</returns>
		Cdv_Einsatzschwerpunkt[] HoleEinsatzschwerpunkte();
		/// <summary>
		/// Lädt alle Verbrauchsgüter der DV-Schicht
		/// </summary>
		/// <returns>Array der Verbrauchsgüter</returns>
		Cdv_Verbrauchsgut[] HoleAlleVerbrauchsgueter();
		/// <summary>
		/// Liefert alle Einheiten die zu einem Modul gruppiert sind
		/// </summary>
		/// <param name="pin_modul">ModulID</param>
		/// <returns>einheiten des Modules</returns>
		Cdv_Einheit[] HoleEinheitenZumModul(Cdv_Modul pin_modul);
		/// <summary>
		/// Lädt Material zum Helfer Laden
		/// </summary>
		/// <param name="pin_ID">ID des Helfer</param>
		/// <returns>Dem Helfer zugeordnetes Material</returns>
		Cdv_Material[] HoleMaterialZumHelfer(int pin_ID);
		/// <summary>
		/// Lädt die Einheit der ein spezielles KFZ zugeordnet ist
		/// </summary>
		/// <param name="pin_ID">KFZ-ID</param>
		/// <returns>Einheit</returns>
		Cdv_Einheit HoleEinheitZumKfz(int pin_ID);
		/// <summary>
		/// Liefere alle Erkundungsergebnisse zurück, deren Empfänger mit der ID pin_ID sind.
		/// </summary>
		Cdv_Erkundungsergebnis[] HoleAlleErkundungsergebnisseZuESP(int pin_ID);
		/// <summary>
		/// Lädt alle Erkundungsdetails
		/// </summary>
		/// <returns>Array der Erkundungsdetails</returns>
		Cdv_Erkundungsergebnis[] HoleAlleErkundungsergebnisse();
		/// <summary>
		/// Lädt ein Einsatzschwerpunkt mit der gegebenen ID
		/// </summary>
		/// <param name="pin_ID">ID des ESP</param>
		/// <returns>ESP</returns>
		Cdv_Einsatzschwerpunkt HoleESP(int pin_ID);
		#endregion

		/// <summary>
		/// Speichert einen Einsatzschwerpunkt
		/// </summary>
		/// <param name="pin_ESP">Einsatzschwerpunkt der gespeichert werden soll</param>
		/// <returns>Gespeicherter einsatzschwerpunkt</returns>
		Cdv_Einsatzschwerpunkt SpeichereESP(Cdv_Einsatzschwerpunkt pin_ESP);
		/// <summary>
		/// Speichert einen Helfer
		/// </summary>
		/// <param name="pin_Helfer">Zu speichernder Helfer</param>
		/// <returns>Gibt den gespeicherten Helfer</returns>
		Cdv_Helfer SpeichereHelfer(Cdv_Helfer pin_Helfer);
		/// <summary>
		/// Speichert einen Ortsverband
		/// </summary>
		/// <param name="pin_OV">zu speichernder Ortsverband</param>
		void SpeichereOV(Cdv_Ortsverband pin_OV);
		/// <summary>
		/// Speichert eine Einheit
		/// </summary>
		/// <param name="pin_Einheit">zu speichernde Einheit</param>
		void SpeichereEinheit(Cdv_Einheit pin_Einheit);
		/// <summary>
		/// Speichert ein Modul
		/// </summary>
		/// <param name="pin_modul">Übergebenes Modul</param>
		void SpeichereModul(Cdv_Modul pin_modul);
		/// <summary>
		/// Speichert ein KFZ
		/// </summary>
		/// <param name="pin_kfz">zu speicherndes KFZ</param>
		/// <returns>ID des KFZ</returns>
		int SpeichereKfz(Cdv_KFZ pin_kfz);
	}
}
