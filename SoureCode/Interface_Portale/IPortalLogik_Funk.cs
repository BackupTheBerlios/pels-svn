using System;
// benötigt für: IPelsObject
using pELS.DV.Server.Interfaces;
// benötigt für: pELS-Objekte
using pELS.DV;

namespace pELS.APS.Server.Interface
{
	/// <summary>
	/// Interface, dass alle für das Portal Funk benötigten Methoden zur
	/// Verfügung stellt
	/// </summary>
	public interface IPortalLogik_Funk : IPortalLogik_allgemeinMAT
	{
		/// <summary>
		///  lädt alle noch nicht versendeten Meldungen
		/// </summary>
		/// <returns></returns>
		Cdv_Meldung[] LadeAlleNichtVersendetenMeldungen();
		/// <summary>
		/// lädt alle noch nicht versendeten Aufträge
		/// </summary>
		/// <returns></returns>
		Cdv_Auftrag[] LadeAlleNichtVersendetenAuftraege();
		/// <summary>
		/// lädt alle Einsatzschwerpunkte
		/// </summary>
		/// <returns></returns>
		Cdv_Einsatzschwerpunkt[] LadeAlleEinsatzschwerpunkte();

		/// <summary>
		/// lädt eine Meldung mit einer spezifischen ID
		/// </summary>
		/// <param name="pin_pELSID">ID der Meldung</param>
		/// <returns></returns>
		Cdv_Meldung LadeMeldung(int pin_pELSID);
		/// <summary>
		/// lädt einen Auftrag mit einer spezifischen ID
		/// </summary>
		/// <param name="pin_pELSID">ID des Auftrags</param>
		/// <returns></returns>
		Cdv_Auftrag LadeAuftrag(int pin_pELSID);
	}
}
