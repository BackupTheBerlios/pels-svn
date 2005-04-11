using System;

namespace pELS.APS.Server.Interface
{
	using pELS.Events; 

	/// <summary>
	/// ermöglicht die Registrierung von EventHandlern, die bei einer 
	/// Änderung der entsprechenden Objekt(listen) ausgeführt werden wollen 
	/// </summary>
	public interface IPortal_Update
	{
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// Anforderungsmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerAnforderung(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// Auftragsmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerAuftrag(pELS.Events.UpdateEventHandler pin_Delegate);
		
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// Benutzersmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerBenutzer(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// Einheitenmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerEinheit(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// Einsatzsmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerEinsatz(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// Einsatzschwerpunktmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerEinsatzschwerpunkte(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// ETBKommentarmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerEtbKommentare(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// ETBEintragsmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerEtbEintraege(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// Helfermenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerHelfer(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// KFZmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerKfZ(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// Materialmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerMaterial(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// Materialübergabemenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerMaterialübergabe(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// Meldungsmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerMeldung(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// Modulmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerModul(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// Ortsverbandmengesmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerOrtsverband(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// Terminmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerTermin(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer Änderung in der 
		/// Verbrauchsgütermenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerVerbrauchsgut(pELS.Events.UpdateEventHandler pin_Delegate);
	}
}