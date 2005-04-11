using System;

#region Dokumentation
/**				aktuelle Version: 0.1.2 alexG & Michal
INFO:
Von diesem Interface sollen alle Softwarebauelemente erben.
Vorrangig designed f�r die GUI zur ersten Pflichtkonsultation.
<bei �nderungen Doku anpassen>
**/
#region Member-Doku
/**		
 * Image GetSbeImage();
	Gibt das Image zur�ck, dass links in der SBE-Auswahl-Leiste angezeigt wird
	Wird direkt aus der Assembly referenziert und zur�ckgegeben per AssemblyInfo
		
 * String GetSbeName();
	// Gibt den Namen des SBEs zur�ck
   Verwendung:  Icon Untertitel, <Wenn neue Verwendungen auftreten, bitte hier eintragen>
	

 * System.Windows.Forms.UserControl GetSbeUserControl();
	// Gibt das UserControll zur�ck, welches rechts im Hauptfenster angezeigt wird
   garantierte Gr��e:
		size 650; 530	-> Wenn gr��er, dann auf scrollen achten
**/
#endregion			
#region letzte �nderungen
/**
erstellt von: Xiao, AlexG, Quecky		am: 1.November.2004
ge�ndert von: AlexG						am: 7.Novemver.2004				
ge�ndert von: Michal 					am:	12.11.04
  review von:			am:
getestet von:			am:
usw.
**/
#endregion
#region History/Hinweise/Bekannte Bugs:
//- �nderung am 7.11.  GetSbeText() -> GetSbeName()
#endregion
#endregion


namespace pELS.GUI.Interface
{
		
	public interface Isbe
	{		
		
		//Erzeuge ein enstprechende Image Objekt und liefere es zur�ck
		System.Drawing.Image GetSbeImage();
		
		// Gibt den Namen des SBEs zur�ck
		String GetSbeName();
	
		// Gibt das UserControll zur�ck das rechts im Hauptfenster angezeigt wird
		System.Windows.Forms.UserControl GetSbeUserControl();


		//startet die Anpassung des sbes an die �bergebene Rolle
		void SetzeRollenRechte(int pin_i_aktuelleRolle);
		
	}
}
