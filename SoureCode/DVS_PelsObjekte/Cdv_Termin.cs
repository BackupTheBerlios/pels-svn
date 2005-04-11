using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über einen Termin.
	/// 
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   24.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Termin : Cdv_pELSObject
	{

		#region Klassenvariablen

		//Begin des Termins
		private DateTime _date_zeitVon = DateTime.Parse("01.01.1800");

		//ende des Termins
		private DateTime _date_zeitBis = DateTime.Parse("01.01.1800");

		//Für welchen Benutzer ist dieser Termin gedacht
		private int _i_erstelltFuerBenutzerID = 0;

		//von welchem Benutzer wurde dieser Termin erstellt
		private int _i_erstelltVonBenutzerID = 0;

		//Ist dieser Termin wichtig
		private bool _b_istWichtig = false;

		//Betreff des Termins
		private string _str_betreff = null;

		//Erinnerung
		private Cdv_Erinnerung _cdvErinnerung = new Cdv_Erinnerung();

		//Wenn true, dann wird an dem Termin erinnert, wenn false nicht
		private bool _b_WirdErinnert = false;

		//Gibt an, ob ein Termin in einer ToDo-Liste erscheinen soll oder nicht
		private bool _istInToDoListe = false;

		#endregion
		#region Konstruktoren
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Termin()
		{
			//standardkonstruktor
		}
		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes
		/// </summary>
		/// <param name="pin_Betreff">Betreff des Termins</param>
		/// <param name="pin_ErstelltVonBenutzerID">aktuell angemeldeter Benutzer</param>
		/// <param name="pin_ErstelltFuerBenutzerID">Benutzer für den dieser Termin gilt</param>
		/// <param name="pin_ZeigeInToDo">true-> Termin wird in ToDo des Benutzers angezeigt</param>
		public Cdv_Termin(string pin_Betreff, int pin_ErstelltVonBenutzerID, int pin_ErstelltFuerBenutzerID,bool pin_ZeigeInToDo)
		{
			this.Betreff = pin_Betreff;
			this.ErstelltVonBenutzerID =  pin_ErstelltVonBenutzerID;
			this.ErstelltFuerBenutzerID = pin_ErstelltFuerBenutzerID;
			this.IstInToDoListe = pin_ZeigeInToDo;			
		}
		#endregion

		#region Properties
		public DateTime ZeitVon
		{
			get{return this._date_zeitVon ;}
			set{this._date_zeitVon = value;}
		}
		public DateTime ZeitBis
		{
			get{return this._date_zeitBis ;}
			set{this._date_zeitBis = value;}
		}
		public int ErstelltFuerBenutzerID
		{
			get{return this._i_erstelltFuerBenutzerID;}
			set{this._i_erstelltFuerBenutzerID = value;}
		}
		public int ErstelltVonBenutzerID
		{
			get{return this._i_erstelltVonBenutzerID ;}
			set{this._i_erstelltVonBenutzerID = value;}
		}
		public bool IstWichtig
		{
			get{return this._b_istWichtig ;}
			set{this._b_istWichtig = value;}
		}
		public string Betreff
		{
			get{return this._str_betreff ;}
			set{this._str_betreff = value;}
		}
		public bool WirdErinnert
		{
			get{return(this._b_WirdErinnert);}
			set{this._b_WirdErinnert = value;}
		}
		public Cdv_Erinnerung Erinnerung
		{
			get{return(this._cdvErinnerung);}
			set{this._cdvErinnerung = value;
				this._b_WirdErinnert = true;}
		}

		public bool IstInToDoListe
		{
			set{this._istInToDoListe = value;}
			get{return this._istInToDoListe;}
		}
		#endregion
	}
}
