using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über einen Einsatz.
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   15.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Einsatz : Cdv_pELSObject
	{
		#region Klassenvariablen			
		//Die Bezeichnung eines Einsatzes
		private string _str_Bezeichnung = String.Empty;
		//Einsatzort
		private string _str_Einsatzort = String.Empty;
		//VonDatum
		private DateTime _date_VonDatum = DateTime.Parse("01.01.1800");
		//Bis datum
		private DateTime _date_BisDatum = DateTime.Parse("01.01.1800");		
		// Einsatzbeschreibung
		private Cdv_Kommentar _ckom_Beschreibung = new Cdv_Kommentar();
		// Daten für Vorblatteinsatz
		private string _str_ArtDerHilfe = "Technische Hilfe";
		// Bei Technischer Hilfe
		private bool _b_Einsatzbericht = false;
		private bool _b_Kostenabrechnung = false;
		private bool _b_Erfahrungsbericht = false;
		private bool _b_Pressemitteilung = false;
		private bool _b_Kostenerstattung = false;
		// Zusätzlich bei Sonstiger Technischer Hilfe
		private bool _b_Haftungsfreistellung = false;
		private bool _b_IhkBescheinigung = false;
		#endregion
		
		#region Konstruktoren
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Einsatz()
		{
		}
		
		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes
		/// </summary>
		/// <param name="pin_Bezeichnung">Benennt den Einsatz</param>		
		/// <param name="pin_Einsatzort">Ort des Einsatzes</param>
		/// <param name="pin_EinsatzBegin">Belegt das Feld 'EinsatzVon'</param>				
		public Cdv_Einsatz(string pin_Bezeichnung,string pin_Einsatzort, DateTime pin_EinsatzBegin)
		{
			this._str_Bezeichnung = pin_Bezeichnung;
			this._str_Einsatzort = pin_Einsatzort;
			this.VonDatum = pin_EinsatzBegin;
		}
		#endregion
		
		#region Properties		

		public string Bezeichnung
		{
			get{return this._str_Bezeichnung;}
			set{this._str_Bezeichnung = value;}
		}
		public string Einsatzort
		{
			get{return this._str_Einsatzort;}
			set{this._str_Einsatzort = value;}
		}

		public DateTime VonDatum
		{
			get{return this._date_VonDatum;}
			set{this._date_VonDatum = value;}
		}
		public DateTime BisDatum
		{
			get{return this._date_BisDatum;}
			set{this._date_BisDatum = value;}
		}
		public Cdv_Kommentar Beschreibung
		{
			get{return this._ckom_Beschreibung;}
			set{this._ckom_Beschreibung = value;}
		}
		public string ArtDerHilfeleistung
		{
			get{return this._str_ArtDerHilfe;}
			set{this._str_ArtDerHilfe = value;}
		}
		public bool EinsatzberichtGefertigt
		{
			get{return this._b_Einsatzbericht;}
			set{this._b_Einsatzbericht = value;}
		}
		public bool KostenabrechnungGefertigt
		{
			get{return this._b_Kostenabrechnung;}
			set{this._b_Kostenabrechnung = value;}
		}
		public bool ErfahrungsberichtGeschrieben
		{
			get{return this._b_Erfahrungsbericht;}
			set{this._b_Erfahrungsbericht = value;}
		}		
		public bool PressemitteilungGeschrieben
		{
			get{return this._b_Pressemitteilung;}
			set{this._b_Pressemitteilung = value;}
		}
		public bool KostenerstattungKontrolliert
		{
			get{return this._b_Kostenerstattung;}
			set{this._b_Kostenerstattung = value;}
		}
		public bool HaftungsfreistellungVorhanden
		{
			get{return this._b_Haftungsfreistellung;}
			set{this._b_Haftungsfreistellung = value;}
		}
		public bool IhkBescheinigungVorhanden
		{
			get{return this._b_IhkBescheinigung;}
			set{this._b_IhkBescheinigung = value;}
		}


		#endregion 
	}
}
