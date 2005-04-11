using System;

namespace pELS.APS.Server.Interface
{
	using pELS.Events; 

	/// <summary>
	/// erm�glicht die Registrierung von EventHandlern, die bei einer 
	/// �nderung der entsprechenden Objekt(listen) ausgef�hrt werden wollen 
	/// </summary>
	public interface IPortal_Update
	{
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// Anforderungsmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerAnforderung(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// Auftragsmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerAuftrag(pELS.Events.UpdateEventHandler pin_Delegate);
		
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// Benutzersmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerBenutzer(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// Einheitenmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerEinheit(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// Einsatzsmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerEinsatz(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// Einsatzschwerpunktmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerEinsatzschwerpunkte(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// ETBKommentarmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerEtbKommentare(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// ETBEintragsmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerEtbEintraege(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// Helfermenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerHelfer(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// KFZmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerKfZ(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// Materialmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerMaterial(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// Material�bergabemenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerMaterial�bergabe(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// Meldungsmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerMeldung(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// Modulmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerModul(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// Ortsverbandmengesmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerOrtsverband(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// Terminmenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerTermin(pELS.Events.UpdateEventHandler pin_Delegate);
		/// <summary>
		/// Registriere eine Methode zur Benachrichtigung
		/// bei einer �nderung in der 
		/// Verbrauchsg�termenge
		/// </summary>
		/// <param name="pin_Delegate">EventHandler</param>
		void RegistriereFuerVerbrauchsgut(pELS.Events.UpdateEventHandler pin_Delegate);
	}
}