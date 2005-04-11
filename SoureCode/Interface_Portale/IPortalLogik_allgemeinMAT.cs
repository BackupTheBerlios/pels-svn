using System;
// benötigt für: Cdv_XXX
using pELS.DV;

namespace pELS.APS.Server.Interface
{
	/// <summary>
	/// Interface, welches von allen Klassen implementiert wird, die Mitteilungen
	/// speichern müssen.
	/// </summary>
	public interface IPortalLogik_allgemeinMAT
	{
		/// <summary>
		/// Speichere einen Auftrag		
		/// </summary>
		/// <param name="pin_auftrag">ein Auftrag- Objekt, das gespeichert werden soll</param>
		/// <returns>Den gespeicherten Auftrag mit der vom System regenerierten eindeutigen ID</returns>		
		Cdv_Mitteilung SpeichereMitteilung(Cdv_Auftrag pin_auftrag);

		/// <summary>
		/// Speichere einen Meldung		
		/// </summary>
		/// <param name="pin_meldung">ein Meldung- Objekt, das gespeichert werden soll</param>
		/// <returns>Díe gespeicherte Meldung mit der vom System regenerierten eindeutigen ID</returns>		
		Cdv_Mitteilung SpeichereMitteilung(Cdv_Meldung pin_meldung);

		/// <summary>
		/// Liefere alle Aufträge zurück		
		/// </summary>
		/// <returns>Alle Aufgräge</returns>		
		Cdv_Auftrag[] LadeAuftraege();

		/// <summary>
		/// Liefere alle Meldungen zurück		
		/// </summary>
		/// <returns>Alle Meldungen</returns>		
		Cdv_Meldung[] LadeMeldungen();

		/// <summary>
		/// Liefere alle Einheiten zurück		
		/// </summary>
		/// <returns>Alle Einheiten</returns>		
		Cdv_Einheit[] HoleAlleEinheiten();
	
		/// <summary>
		/// Liefere alle Kräftefahrzeuge zurück		
		/// </summary>
		/// <returns>Alle Kräftefahrzeuge</returns>		
		Cdv_KFZ[] HoleAlleKFZ();

		/// <summary>
		/// Liefere alle Helfer zurück		
		/// </summary>
		/// <returns>Alle Helfer</returns>		
		Cdv_Helfer[] HoleAlleHelfer();
	}
}
