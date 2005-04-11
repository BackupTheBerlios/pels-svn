using System;
// benötigt für: IPelsObject
using pELS.DV.Server.Interfaces;
// benötigt für: Cdv_XXX
using pELS.DV;

namespace pELS.APS.Server.Interface
{
	/// <summary>
	/// Interface, dass alle für das Portal Logistik benötigten Methoden zur
	/// Verfügung stellt
	/// </summary>
	public interface IPortalLogik_Logistik
	{		
		/// <summary>
		/// fügt ein Gut zur Gütervorratsliste hinzu
		/// </summary>
		/// <param name="pin_Gut">zu speicherndes Gut</param>
		void SpeichereGut(Cdv_Gut pin_Gut);
		/// <summary>
		/// fügt eine Anforderung der Anforderungsmenge hinzu
		/// </summary>
		/// <param name="pin_Anforderung">zu speichernde Anforderung</param>
		void SpeichereAnforderung(Cdv_Anforderung pin_Anforderung);
		/// <summary>
		/// ordnet Material zu einer Einheit zu
		/// </summary>
		/// <param name="pin_Material">behandeltes Material</param>
		/// <param name="pin_Einheit">Zieleinheit</param>
		void OrdneMaterialZuEinheit(Cdv_Material pin_Material, Cdv_Einheit pin_Einheit);
		/// <summary>
		/// speichert eine Materialübergabe
		/// </summary>
		/// <param name="pin_Materialuebergabe"></param>
		void MaterialUebergabe(Cdv_Materialuebergabe pin_Materialuebergabe);
		/// <summary>
		/// liefert alle Einheiten
		/// </summary>
		/// <returns></returns>
		Cdv_Einheit[] HoleAlleEinheiten();
		/// <summary>
		/// liefert alle Helfer
		/// </summary>
		/// <returns></returns>
		Cdv_Helfer[] HoleAlleHelfer();
		/// <summary>
		/// liefert alle KFZ
		/// </summary>
		/// <returns></returns>
		Cdv_KFZ[] HoleAlleKFZ();
		/// <summary>
		/// liefert alle Verbrauchsgüter
		/// </summary>
		/// <returns></returns>
		Cdv_Verbrauchsgut[] HoleAlleVerbrauchsgueter();
		/// <summary>
		/// liefert alle Materialien
		/// </summary>
		/// <returns></returns>
		Cdv_Material[] HoleAlleMaterialien();
		/// <summary>
		/// lädt alle Materialien die zu einer KraftID gehören
		/// </summary>
		/// <param name="pin_BesitzerID"></param>
		/// <returns></returns>
		int[] HoleAlleMaterialIDs(int pin_BesitzerID);
	}
}
