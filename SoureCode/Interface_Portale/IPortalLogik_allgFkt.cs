using System;
// benötigt für: Cdv_XXX
using pELS.DV;

namespace pELS.APS.Server.Interface
{
	/// <summary>
	/// stellt allgemeine vom Client und allen Portalen genutzte 
	/// Funktionen zur Verfügung
	/// </summary>
	public interface IPortalLogik_allgFkt
	{
		/// <summary>
		/// Liefere alle Systembenutzer zurück		
		/// </summary>
		/// <returns>Alle Systembenutzer</returns>				
		Cdv_Benutzer[] HoleAlleBenutzer();

		/// <summary>
		/// Speichere einen Systembenutzer		
		/// </summary>
		/// <param name="pin_neuerBenutzer">ein Benutzer- Objekt, das gespeichert werden soll</param>
		/// <returns>Den gespeicherten Benutzer mit der vom System regenerierten eindeutigen ID</returns>		
		Cdv_Benutzer SpeichereBenutzer(Cdv_Benutzer pin_neuerBenutzer);

		/// <summary>
		/// Hier für ETB Systemereignisse werfen
		/// </summary>
		/// <param name="pin_syserg">Inhalt des Systemereignisses</param>
		void WerfeSystemereignis(Cdv_Systemereignis pin_syserg);

		bool PruefeAllgVorraussetzungen();
	}
}
