using System;
// ben�tigt f�r: Cdv_XXX
using pELS.DV;

namespace pELS.APS.Server.Interface
{
	/// <summary>
	/// Interface, welches von allen Klassen implementiert wird, die Mitteilungen
	/// speichern m�ssen.
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
		/// <returns>D�e gespeicherte Meldung mit der vom System regenerierten eindeutigen ID</returns>		
		Cdv_Mitteilung SpeichereMitteilung(Cdv_Meldung pin_meldung);

		/// <summary>
		/// Liefere alle Auftr�ge zur�ck		
		/// </summary>
		/// <returns>Alle Aufgr�ge</returns>		
		Cdv_Auftrag[] LadeAuftraege();

		/// <summary>
		/// Liefere alle Meldungen zur�ck		
		/// </summary>
		/// <returns>Alle Meldungen</returns>		
		Cdv_Meldung[] LadeMeldungen();

		/// <summary>
		/// Liefere alle Einheiten zur�ck		
		/// </summary>
		/// <returns>Alle Einheiten</returns>		
		Cdv_Einheit[] HoleAlleEinheiten();
	
		/// <summary>
		/// Liefere alle Kr�ftefahrzeuge zur�ck		
		/// </summary>
		/// <returns>Alle Kr�ftefahrzeuge</returns>		
		Cdv_KFZ[] HoleAlleKFZ();

		/// <summary>
		/// Liefere alle Helfer zur�ck		
		/// </summary>
		/// <returns>Alle Helfer</returns>		
		Cdv_Helfer[] HoleAlleHelfer();
	}
}
