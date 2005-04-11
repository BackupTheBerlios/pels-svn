using System;
// ben�tigt f�r: IPelsObject
using pELS.DV.Server.Interfaces;
// ben�tigt f�r: Cdv_XXX
using pELS.DV;

namespace pELS.APS.Server.Interface
{
	/// <summary>
	/// Interface, dass alle f�r das Portal Logistik ben�tigten Methoden zur
	/// Verf�gung stellt
	/// </summary>
	public interface IPortalLogik_Logistik
	{		
		/// <summary>
		/// f�gt ein Gut zur G�tervorratsliste hinzu
		/// </summary>
		/// <param name="pin_Gut">zu speicherndes Gut</param>
		void SpeichereGut(Cdv_Gut pin_Gut);
		/// <summary>
		/// f�gt eine Anforderung der Anforderungsmenge hinzu
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
		/// speichert eine Material�bergabe
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
		/// liefert alle Verbrauchsg�ter
		/// </summary>
		/// <returns></returns>
		Cdv_Verbrauchsgut[] HoleAlleVerbrauchsgueter();
		/// <summary>
		/// liefert alle Materialien
		/// </summary>
		/// <returns></returns>
		Cdv_Material[] HoleAlleMaterialien();
		/// <summary>
		/// l�dt alle Materialien die zu einer KraftID geh�ren
		/// </summary>
		/// <param name="pin_BesitzerID"></param>
		/// <returns></returns>
		int[] HoleAlleMaterialIDs(int pin_BesitzerID);
	}
}
