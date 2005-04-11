using System;

namespace pELS.DV
{
	/// <summary>	
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über einen Auftrag.
	/// 
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   16.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Auftrag : Cdv_Mitteilung
	{
		#region Klasssenvariablen
		//Ausführungszeitpunkt des Auftrags
		private DateTime _date_ausfuehrungszeitpunkt = DateTime.Parse("01.01.1800");

		//Spätester Erfüllungszeitpunkt des Auftrags
		private DateTime _date_spaetesterErfuellungszeitpunkt = DateTime.Parse("01.01.1800");

		//Ist der Auftrag ein Befehl ?
		private bool _b_istBefehl = false;

		// Wird der Auftrag nachverfolgt
		private bool _b_wirdNachverfolgt = false;
		#endregion

		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Auftrag()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes
		/// </summary>
		/// <param name="pin_Text">Belegt Auftrag.Text</param>
		/// <param name="pin_Abfassungsdatum">Zeitpunkt der Abfassung</param>
		/// <param name="pin_Absender">Absender des Auftrages</param>
		/// <param name="pin_Uebermittlungsart">Tdv_UebermittlungsArt des Auftrags</param>
		/// <param name="pin_wirdNachverfolgt">true, wenn der Auftrag nachverfolgt werden soll, sonst false</param>
		/// <param name="pin_ZeigeInToDoListe">true-> Auftrag wird in der ToDoListe des Empfaengers Angezeigt (falls EmpfaengerBenutzerID gesetzt)</param>
		public Cdv_Auftrag(string pin_Text, DateTime pin_Abfassungsdatum, string pin_Absender, Tdv_Uebermittlungsart pin_Uebermittlungsart, bool pin_wirdNachverfolgt, bool pin_ZeigeInToDoListe, int pin_BearbeiterID)
		{
			this.Text = pin_Text;
			this.Abfassungsdatum = pin_Abfassungsdatum;
			this.Absender = pin_Absender;
			this.Uebermittlungsart = pin_Uebermittlungsart;
			this.WirdNachverfolgt = pin_wirdNachverfolgt;
			this.IstInToDoListe = pin_ZeigeInToDoListe;
			this.BearbeiterBenutzerID = pin_BearbeiterID;
		}
		#region Properties
		public DateTime Ausfuehrungszeitpunkt
		{
			get{return this._date_ausfuehrungszeitpunkt ;}
			set{this._date_ausfuehrungszeitpunkt = value;}
		}
		public DateTime SpaetesterErfuellungszeitpunkt
		{
			get{return this._date_spaetesterErfuellungszeitpunkt ;}
			set{this._date_spaetesterErfuellungszeitpunkt = value;}
		}
		public bool IstBefehl
		{
			get{return this._b_istBefehl ;}
			set{this._b_istBefehl = value;}
		}
		public bool WirdNachverfolgt
		{
			get{return this._b_wirdNachverfolgt ;}
			set{this._b_wirdNachverfolgt = value;}
		}
		#endregion
	}
}
