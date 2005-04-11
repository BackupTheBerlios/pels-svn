using System;

namespace pELS
{
	/// <summary>
	/// In dieser Datei werden alle Aufzaehlungstypen gesammelt, so wie sie in Pels gebraucht werden
	/// alle Integer-Entsprechungen sollen eindeutig sein. einmal getroffene Zuordnungen sollten nicht
	/// ver�ndert werden. die Nummerierung erfolgt fortlaufend
	/// </summary>
	

	// Achtung! Wird in Crystal Reports verwendet. Bei �nderungen auch in den Reports �ndern.
	// Aufzaehlungstyp-> wird benutzt als Attribut von Cdv_Anforderung
	public enum Tdv_AnforderungsStatus
	{
		Neu 		=	1,
		Offen 		=	2,
		Bearbeitet 	=	3, 
		Abgewiesen	=	4
	}

	//Aufzaehlungstyp-> wird benutzt als Attribut von Cdv_Mitteilung
	public enum Tdv_Uebermittlungsart
	{
		Funk	=	1, 
		Fax		=	2,
		Telefon	=	3, 
		Kurier	=	4
	}
	
	//Aufzaehlungstyp-> wird benutzt als Attribut von Cdv_Meldung
	public enum Tdv_MeldungsKategorie
	{
		Normal		=	1, 
		Sofort		=	2, 
		Blitz		=	3, 
		Staatsnot	=	4
	}
	
	// Achtung! Wird in Crystal Reports verwendet. Bei �nderungen auch in den Reports �ndern.
	// Aufzaehlungstyp-> wird benutzt als Attribut von Cdv_ErkundungsObjekt
	public enum Tdv_Bauart
	{
		Fertighaus	=	1,
		Massivhaus	=	2, 
		Sonstiges	=	3
	}

	// Wird benutzt um auf Clientseite Rechte zu vergeben
	public enum Tdv_Systemrolle
	{
		Zugf�hrer			=	   1,
		Zugtruppf�hrer		=	   2,
		Einsatzleiter		=	   3,
		LeiterF�St			=	   4,
		F�hrungsgehilfe		=	   5,
		Sichter				=	   6,
		Fernmelder			=	   7,
		S1					=	   8,
		S2					=	   9,
		S3					=	  10,
		S4					=	  11,
		S5					=	  12,
		S6					=	  13,
		LeiterStab			=	  14
	}

	// Achtung! Wird in Crystal Reports verwendet. Bei �nderungen auch in den Reports �ndern.
	// Aufzaehlungstyp-> wird benutzt als Attribut von Cdv_Helfer
	public enum Tdv_Position	
	{	
		F�hrer			=	1, 
		Zugf�hrer		=	2,
		Unterf�hrer		=	3, 
		Gruppenf�hrer	=	4, 
		Truppf�hrer		=	5,
		EinfacherHelfer =	6
	}

	//Aufzaehlungstyp-> wird benutzt als Attribut von Cdv_Helfer
	public enum Tdv_Helferstatus
	{
		AktiverHelfer	=	1, 
		ReserveHelfer	=	2, 
		AltHelfer		=	3, 
		JungHelfer		=	4
	}

	// Achtung! Wird in Crystal Reports verwendet. Bei �nderungen auch in den Reports �ndern.
	// Wird benutzt als Attribut von Cdv_Kraft
	public enum Tdv_Kraeftestatus
	{
		Angefordert					=	1,
		ImEinsatz					=	2,
		Ruht						=	3,
		Einsatzbereit				=	4,
		Eingeschr�nktEinsatzbereit	=	5,
		NichtVerf�gbar				=	6
	}

	// Achtung! Wird in Crystal Reports verwendet. Bei �nderungen auch in den Reports �ndern.
	// Aufzaehlungstyp-> wird benutzt als Attribut von Cdv_Erkundungsbefehl
	public enum Tdv_BefehlArt
	{
		Wasserschaden	=	1, 
		�lschaden		=	2, 
		Erdarbeiten		=	3, 
		Transport		=	4, 
		Infrastruktur	=	5, 
		Wasser			=	6, 
		Strom			=	7, 
		Heizung			=	8, 
		Tierbergung		=	9, 
		Bergung			=	10, 
		Bootseinsatz	=	11, 
		Geb�ude			=	12, 
		Gas				=	13, 
		Abwasser		=	14, 
		Fernmelde		=	15, 
		Sonstiges		=	16
	}

	// Aufz�hlungstyp-> Wird benutzt als Attribut von Cdv_Systemereignis
	public enum Tdv_SystemereignisArt
	{
		Einsatzdaten			=	1,
		Erinnerung				=	2,
		Mitteilung				=	3,
		Kraftankunft			=	4,
		Material�bergabe		=	5,
		G�teranforderung		=	6,
		G�terankunft			=	7,
		G�terausgang			=	8,
		Modulzuweisung			=	9,
		Statuswechsel			=	10,
		NeuerEinsatzschwerpunkt =	11,
		Benutzeranmeldung		=	12,
		Sonstiges				=	13
	}
}
