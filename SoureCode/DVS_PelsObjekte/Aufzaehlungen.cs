using System;

namespace pELS
{
	/// <summary>
	/// In dieser Datei werden alle Aufzaehlungstypen gesammelt, so wie sie in Pels gebraucht werden
	/// alle Integer-Entsprechungen sollen eindeutig sein. einmal getroffene Zuordnungen sollten nicht
	/// verändert werden. die Nummerierung erfolgt fortlaufend
	/// </summary>
	

	// Achtung! Wird in Crystal Reports verwendet. Bei Änderungen auch in den Reports ändern.
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
	
	// Achtung! Wird in Crystal Reports verwendet. Bei Änderungen auch in den Reports ändern.
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
		Zugführer			=	   1,
		Zugtruppführer		=	   2,
		Einsatzleiter		=	   3,
		LeiterFüSt			=	   4,
		Führungsgehilfe		=	   5,
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

	// Achtung! Wird in Crystal Reports verwendet. Bei Änderungen auch in den Reports ändern.
	// Aufzaehlungstyp-> wird benutzt als Attribut von Cdv_Helfer
	public enum Tdv_Position	
	{	
		Führer			=	1, 
		Zugführer		=	2,
		Unterführer		=	3, 
		Gruppenführer	=	4, 
		Truppführer		=	5,
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

	// Achtung! Wird in Crystal Reports verwendet. Bei Änderungen auch in den Reports ändern.
	// Wird benutzt als Attribut von Cdv_Kraft
	public enum Tdv_Kraeftestatus
	{
		Angefordert					=	1,
		ImEinsatz					=	2,
		Ruht						=	3,
		Einsatzbereit				=	4,
		EingeschränktEinsatzbereit	=	5,
		NichtVerfügbar				=	6
	}

	// Achtung! Wird in Crystal Reports verwendet. Bei Änderungen auch in den Reports ändern.
	// Aufzaehlungstyp-> wird benutzt als Attribut von Cdv_Erkundungsbefehl
	public enum Tdv_BefehlArt
	{
		Wasserschaden	=	1, 
		Ölschaden		=	2, 
		Erdarbeiten		=	3, 
		Transport		=	4, 
		Infrastruktur	=	5, 
		Wasser			=	6, 
		Strom			=	7, 
		Heizung			=	8, 
		Tierbergung		=	9, 
		Bergung			=	10, 
		Bootseinsatz	=	11, 
		Gebäude			=	12, 
		Gas				=	13, 
		Abwasser		=	14, 
		Fernmelde		=	15, 
		Sonstiges		=	16
	}

	// Aufzählungstyp-> Wird benutzt als Attribut von Cdv_Systemereignis
	public enum Tdv_SystemereignisArt
	{
		Einsatzdaten			=	1,
		Erinnerung				=	2,
		Mitteilung				=	3,
		Kraftankunft			=	4,
		Materialübergabe		=	5,
		Güteranforderung		=	6,
		Güterankunft			=	7,
		Güterausgang			=	8,
		Modulzuweisung			=	9,
		Statuswechsel			=	10,
		NeuerEinsatzschwerpunkt =	11,
		Benutzeranmeldung		=	12,
		Sonstiges				=	13
	}
}
