using System;
// ben�tigt f�r: IPelsObject
using pELS.DV.Server.Interfaces;
// ben�tigt f�r: pELS-Objekte
using pELS.DV;

namespace pELS.APS.Server.Interface
{
	/// <summary>
	/// Interface, dass alle f�r das Portal Funk ben�tigten Methoden zur
	/// Verf�gung stellt
	/// </summary>
	public interface IPortalLogik_Funk : IPortalLogik_allgemeinMAT
	{
		/// <summary>
		///  l�dt alle noch nicht versendeten Meldungen
		/// </summary>
		/// <returns></returns>
		Cdv_Meldung[] LadeAlleNichtVersendetenMeldungen();
		/// <summary>
		/// l�dt alle noch nicht versendeten Auftr�ge
		/// </summary>
		/// <returns></returns>
		Cdv_Auftrag[] LadeAlleNichtVersendetenAuftraege();
		/// <summary>
		/// l�dt alle Einsatzschwerpunkte
		/// </summary>
		/// <returns></returns>
		Cdv_Einsatzschwerpunkt[] LadeAlleEinsatzschwerpunkte();

		/// <summary>
		/// l�dt eine Meldung mit einer spezifischen ID
		/// </summary>
		/// <param name="pin_pELSID">ID der Meldung</param>
		/// <returns></returns>
		Cdv_Meldung LadeMeldung(int pin_pELSID);
		/// <summary>
		/// l�dt einen Auftrag mit einer spezifischen ID
		/// </summary>
		/// <param name="pin_pELSID">ID des Auftrags</param>
		/// <returns></returns>
		Cdv_Auftrag LadeAuftrag(int pin_pELSID);
	}
}
