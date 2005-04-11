using System;
using pELS.DV.Server.Interfaces;
// benötigt für: Events
using pELS.Events;
namespace pELS.DV.Server.ObjectManager.Interfaces
{
	/// <summary>
	/// Dieses Interface wird von der CVerwaltung Klassen implementiert. Es beschreibt
	/// die grundlegenden Methoden, die es erlauben Objekte vom ObjectManager zu holen,
	/// aber auch Objekte mit Hilfe des ObjectManagers in der DB zu speichern
	/// </summary>
	public interface IVerwaltung
	{
		IPelsObject Holen(int pin_iPK);		
		IPelsObject[] HolenAlle();
		IPelsObject Speichern(IPelsObject pin_ipo);
		IPelsObject[] Speichern(IPelsObject[] pin_ipoa);
		void DatenErneutLaden();
		bool LadenErfolgreich{get;}
		string GetErrorMessage {get;}
		void RegistriereFuerAenderungsEvents(pELS.Events.UpdateEventHandler pin_Delegate);				
	}

	/// <summary>
	/// Dieses Interface ist eigentlich eine Menge von Properties. Wenn ein Client
	/// eine Property aufruft, dann kreigt er ein Interface zurück. Mit diesem Interface
	/// kann der Client die entsprechende Objektmenge manipulieren (z.B. ein Termin speichern
	/// oder holen)
	/// </summary>
	public interface IObjectManager
	{
		IVerwaltung Anforderungen{get;}	
		IVerwaltung Einsaetze{get;}
		IVerwaltung Ortsverbaende{get;}
		IVerwaltung Einsatzschwerpunkte{get;}
		//IVerwaltung Personen{get;}
		IVerwaltung Helfer{get;}
		IVerwaltung Benutzer{get;}
		IVerwaltung Auftraege{get;}
		IVerwaltung Erkundungsbefehle{get;}
		IVerwaltung Einheiten{get;}
		//IVerwaltung Erkundungsergebnisse{get;}
		IVerwaltung Kfz{get;}
		IVerwaltung Material{get;}
		IVerwaltung Materialuebergaben{get;}
		IVerwaltung Meldungen{get;}
		IVerwaltung Moduln{get;}
		IVerwaltung Termine{get;}
		IVerwaltung Verbrauchsgueter{get;}
		//Hier das ETB
		IVerwaltung EtbEintraege{get;}
		IVerwaltung EtbKommentare{get;}

		//Gebe nicht geladene Verwaltungsinterfaces zurück
		string NichtGeladeneVerwaltungen{get;}
	}
}
