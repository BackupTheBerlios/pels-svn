using System;
// ben�tigt f�r: IPelsObject
using pELS.DV.Server.Interfaces;
// ben�tigt f�r: Cdv_XXX
using pELS.DV;
// zum Testen
using pELS;

namespace pELS.APS.Server.Interface
{
	/// <summary>
	/// Interface, dass alle f�r das Portal MAT ben�tigten Methoden zur
	/// Verf�gung stellt
	/// </summary>
	public interface IPortalLogik_MAT : IPortalLogik_allgemeinMAT
	{
		#region Hole- Methoden
		/// <summary>
		/// Liefere alle Einsatzschwerpunkte zur�ck		
		/// </summary>
		/// <returns>Alle Einsatzschwerpunkte</returns>		
		Cdv_Einsatzschwerpunkt[] HoleAlleESP();
		/// <summary>
		/// Liefere den Einsatzschwerpunkt mit der gleichen als Parameter �bergebenen ID zur�ck.	
		/// </summary>
		/// <param name="pin_ID">die ID des zur�ckzuholenden Einsatzschwerpunktes </param>
		/// <returns>Den Einsatzschwerpunkt mit der gleichen ID </returns>		
		Cdv_Einsatzschwerpunkt HoleESP(int pin_ID);

		/// <summary>
		/// Liefere alle Auftr�ge zur�ck, die als "wird Nachverfolgt" markiert sind.	
		/// </summary>
		/// <returns>Alle Auftr�ge, die nachzuverfolgen sind </returns>		
		Cdv_Auftrag[] HoleNachzuverfolgendeAuftraege();

		/// <summary>
		/// Liefere den Systembenutzer mit der gleichen als Parameter �bergebenen ID zur�ck.	
		/// </summary>
		/// <param name="pin_ID">die ID des zur�ckzuholenden Benutzers </param>
		/// <returns>Den Benutzer mit der gleichen ID </returns>				
		Cdv_Benutzer HoleBenutzer(int pin_ID);

		#endregion

		#region Speichere- Methoden
		/// <summary>
		/// Speichere einen Termin		
		/// </summary>
		/// <param name="pin_termin">ein Termin- Objekt, das gespeichert werden soll</param>
		/// <returns>Den gespeicherten Termin mit der vom System regenerierten eindeutigen ID</returns>		
		Cdv_Termin SpeichereTermin(Cdv_Termin pin_termin);
		/// <summary>
		/// Speichere eine Meldung		
		/// </summary>
		/// <param name="pin_Meldung">eine Meldung- Objekt, das gespeichert werden soll</param>
		/// <returns></returns>		
		void SpeichereMeldung(Cdv_Meldung pin_Meldung);
		/// <summary>
		/// Speichere ein Erkundungsergebnis		
		/// </summary>
		/// <param name="pin_Erkundungsergebnis">ein Erkundungsergebnis- Objekt, das gespeichert werden soll</param>
		/// <returns></returns>		
		void SpeichereErkundungsergebnis(Cdv_Erkundungsergebnis pin_Erkundungsergebnis);
		/// <summary>
		/// Speichere einen Auftrag	
		/// </summary>
		/// <param name="pin_Auftrag">ein Auftrag- Objekt, das gespeichert werden soll</param>
		/// <returns></returns>		
		void SpeichereAuftrag(Cdv_Auftrag pin_Auftrag);
		/// <summary>
		/// Speichere einen Erkundungsbefehl		
		/// </summary>
		/// <param name="pin_Erkundungsbefehl">ein Erkundungsbefehl- Objekt, das gespeichert werden soll</param>
		/// <returns></returns>		
		void SpeichereErkundungsbefehl(Cdv_Erkundungsbefehl pin_Erkundungsbefehl);
		

		#endregion
	}
}
