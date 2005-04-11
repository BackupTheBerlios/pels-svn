using System;

#region Dokumentation
/**				aktuelle Version: 0.1.2 alexG & Michal
INFO:
Von diesem Interface sollen alle Softwarebauelemente erben.
Vorrangig designed für die GUI zur ersten Pflichtkonsultation.
<bei Änderungen Doku anpassen>
**/
#region Member-Doku
/**		
 * Image GetSbeImage();
	Gibt das Image zurück, dass links in der SBE-Auswahl-Leiste angezeigt wird
	Wird direkt aus der Assembly referenziert und zurückgegeben per AssemblyInfo
		
 * String GetSbeName();
	// Gibt den Namen des SBEs zurück
   Verwendung:  Icon Untertitel, <Wenn neue Verwendungen auftreten, bitte hier eintragen>
	

 * System.Windows.Forms.UserControl GetSbeUserControl();
	// Gibt das UserControll zurück, welches rechts im Hauptfenster angezeigt wird
   garantierte Größe:
		size 650; 530	-> Wenn größer, dann auf scrollen achten
**/
#endregion			
#region letzte Änderungen
/**
erstellt von: Xiao, AlexG, Quecky		am: 1.November.2004
geändert von: AlexG						am: 7.Novemver.2004				
geändert von: Michal 					am:	12.11.04
  review von:			am:
getestet von:			am:
usw.
**/
#endregion
#region History/Hinweise/Bekannte Bugs:
//- änderung am 7.11.  GetSbeText() -> GetSbeName()
#endregion
#endregion


namespace pELS.GUI.Interface
{
		
	public interface Isbe
	{		
		
		//Erzeuge ein enstprechende Image Objekt und liefere es zurück
		System.Drawing.Image GetSbeImage();
		
		// Gibt den Namen des SBEs zurück
		String GetSbeName();
	
		// Gibt das UserControll zurück das rechts im Hauptfenster angezeigt wird
		System.Windows.Forms.UserControl GetSbeUserControl();


		//startet die Anpassung des sbes an die übergebene Rolle
		void SetzeRollenRechte(int pin_i_aktuelleRolle);
		
	}
}
