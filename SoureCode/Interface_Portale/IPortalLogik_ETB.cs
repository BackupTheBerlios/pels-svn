using System;
// benötigt für: IPelsObject
using pELS.DV.Server.Interfaces;
// benötigt für: Cdv_XXX
using pELS.DV;

namespace pELS.APS.Server.Interface
{
	/// <summary>
	/// Interface, dass alle für das Portal ETB benötigten Methoden zur
	/// Verfügung stellt
	/// </summary>
	public interface IPortalLogik_Etb
	{		
		/// <summary>
		/// Diese Funktion lädt alle Systemereignisse.
		/// <returns name="pout_Systemereignisse">Menge aller Systemereignisse die bisher geworfen wurde.</returns>
		/// </summary>
		System.IO.Stream ErzeugeEtb();
		/// <summary>
		/// Diese Funktion lädt alle Systemereignisse.
		/// <returns name="pout_Systemereignisse">Menge aller Systemereignisse die bisher geworfen wurde.</returns>
		/// </summary>
		Cdv_Systemereignis[] LadeSystemereignisse();
		/// <summary>
		/// Diese Funktion gibt alle EtbEintraege zurueck, die nicht Systemereignisse sind.
		/// </summary>
		/// <returns>Menge aller EtbZusatzeintraege</returns>
		Cdv_EtbEintrag[] LadeEtbEintraege();
		/// <summary>
		/// Holt alle EtbEintragKommentare und liefert sie in einem Array zurück
		/// </summary>
		/// <returns></returns>
		Cdv_EtbEintragKommentar[] LadeEtbKommentare();
		/// <summary>
		/// Schreibt ein neu markiertes Systemereignis in die Datenbank
		/// </summary>
		/// <param name="pin_ID">neuer ETB Eintrag</param>
		/// <returns>nichts</returns>
		void MarkiereSystemereignis(Cdv_EtbEintrag pin_markiertesSE);
		/// <summary>
		/// Speichert einen neuen Zusatzeintrag in die Datenbank
		/// </summary>
		/// <param name="pin_neuerZeOhneID">neuer ETB Eintrag</param>
		/// <returns>Objekt mit ID</returns>
		Cdv_EtbEintrag SpeichereZusatzeintrag(Cdv_EtbEintrag pin_neuerZE);
		/// <summary>
		/// Speichert einen neuen Kommentar zu einem Zusatzeintrag in die Datenbank
		/// </summary>
		/// <param name="pin_ID">neuer Kommentar</param>
		/// <returns>Objekt mit ID</returns>
		Cdv_EtbEintragKommentar SpeichereEintragKommentar(Cdv_EtbEintragKommentar pin_neuerKommentar);
	}
}
