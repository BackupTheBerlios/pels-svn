using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über einen Erkundungsbefehl sowie alle von Auftrag eingeerbten Attribute.
	/// 
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   16.02.2005
	/// 
	/// Besonderheit:
	/// Das Auftragsattribut 'IstBefehl' wird im Konstruktor des Erkundungsbefehls auf true gesetzt.
	/// </summary>
	[Serializable]
	public class Cdv_Erkundungsbefehl :Cdv_Auftrag
	{
		#region Klassenvariablen
		//Art des Befehls
		private Tdv_BefehlArt _Tdv_BefehlArt_befehlsart = Tdv_BefehlArt.Sonstiges;

		//Kommentar zum Erkundungsbefehl
//		private Cdv_Kommentar _ckom_kommentar = new Cdv_Kommentar();
		#endregion

		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Erkundungsbefehl()
		{
			this.IstBefehl = true;
		}
		
		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes
		/// 
		/// Bis auf die Befehlsart deckt sich die Signatur mit Cdv_Auftrag.
		/// </summary>
		/// <param name="pin_Text">Belegt Erkundungsbefehl.Text</param>
		/// <param name="pin_Abfassungsdatum">Zeitpunkt der Abfassung</param>
		/// <param name="pin_Absender">Absender des Befehls</param>
		/// <param name="pin_Uebermittlungsart">Tdv_UebermittlungsArt des Erkundungsbefehl</param>
		/// <param name="pin_wirdNachverfolgt">true, wenn der Erkundungsbefehl nachverfolgt werden soll(empfehlenswert), sonst false</param>
		/// <param name="pin_Befehlsart">Tdv_Befehlsart des Befehls</param>
		/// <param name="pin_Befehlsart">Tdv_Befehlsart des Befehls</param>
		/// <param name="pin_ZeigeInToDoListe">true-> Erkundungsbefehl wird in der ToDoListe des Empfaengers Angezeigt (falls EmpfaengerBenutzerID gesetzt)</param>
		public Cdv_Erkundungsbefehl(string pin_Text, DateTime pin_Abfassungsdatum, string pin_Absender, Tdv_Uebermittlungsart pin_Uebermittlungsart, bool pin_wirdNachverfolgt, Tdv_BefehlArt pin_Befehlsart, bool pin_ZeigeInToDoListe, int pin_BearbeiterID):
				base(pin_Text,pin_Abfassungsdatum,pin_Absender,pin_Uebermittlungsart,pin_wirdNachverfolgt,pin_ZeigeInToDoListe,pin_BearbeiterID)
		{
			this.BefehlsArt = pin_Befehlsart;
		}

		#region Properties
		public Tdv_BefehlArt BefehlsArt
		{
			get{return this._Tdv_BefehlArt_befehlsart ;}
			set{this._Tdv_BefehlArt_befehlsart = value;}
		}
//		public Cdv_Kommentar Kommentar
//		{
//			get{return this._ckom_kommentar ;}
//			set{this._ckom_kommentar = value;}
//		}
		#endregion
	}
}
