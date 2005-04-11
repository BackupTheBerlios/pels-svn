using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über einen Helfer.
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   24.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Helfer : Cdv_Kraft
	{

		#region Klassenvariablen
		//Die Position des Helfers (in der Hackordnung :o)
		private Tdv_Position _Tdv_Position_position = Tdv_Position.EinfacherHelfer;

		//Die Menge der Möglichkeiten jemanden zu erreichen 
		private string _str_erreichbarkeit = null;

		//Ortsverband dem der Helfer angehört
		private int _i_ovID = 0;

		//Informationen über besondere Fähigkeiten des Helfers
		private string _str_faehigkeiten = null;

		//Status des Helfers
		private Tdv_Helferstatus _Tdv_Helferstatus_helferstatus = Tdv_Helferstatus.AktiverHelfer;

		//Angaben zur Person
		private Cdv_Person _Cdv_Person_daten = new Cdv_Person();

		//letzte Verpflegung des Helfers
		private DateTime _date_letzteVerpflegung = DateTime.Parse("01.01.1800");
		
		//Ist das eine Führungskraft von einem Modul?
		private bool _istFuehrungskraftVonModul = false;

		#endregion
		#region Konstruktor
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Helfer()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes		
		/// </summary>
		/// <param name="pin_Name">Name des Helfers</param>
		/// <param name="pin_Vorname">Vorname des Helfers</param>
		/// <param name="pin_Kraeftestatus">Tdv_Kraeftestatus des Helfers</param>
		/// <param name="pin_HelferStatus">Tdv_Helferstatus des Helfers</param>
		public Cdv_Helfer(string pin_Name, string pin_Vorname, Tdv_Kraeftestatus pin_Kraeftestatus, Tdv_Helferstatus pin_HelferStatus)
		{
			this.Personendaten.Name = pin_Name;
			this.Personendaten.Vorname = pin_Vorname;
			this.Kraeftestatus = pin_Kraeftestatus;
			this.Helferstatus = pin_HelferStatus;	
		}
		#endregion

		#region Properties
		public bool istFuehrungskraftVonModul
		{
			get{return this._istFuehrungskraftVonModul;}
			set{_istFuehrungskraftVonModul=value;}
		}

		public Tdv_Position Position
		{
			get{return this._Tdv_Position_position;}
			set{this._Tdv_Position_position = value;}
		}
		public string Erreichbarkeit
		{
			get{return this._str_erreichbarkeit;}
			set{this._str_erreichbarkeit = value;}
		}
		public int OVID
		{
			get{return this._i_ovID;}
			set{this._i_ovID= value;}
		}
		public string Faehigkeiten
		{
			get{return this._str_faehigkeiten ;}
			set{this._str_faehigkeiten = value;}
		}

		public Tdv_Helferstatus Helferstatus		
		{
			get{return this._Tdv_Helferstatus_helferstatus ;}
			set{this._Tdv_Helferstatus_helferstatus = value;}
		}

		public Cdv_Person Personendaten
		{
			get{return this._Cdv_Person_daten;}
			set{this._Cdv_Person_daten = value;}
		}
		public DateTime LetzteVerfplegung
		{
			get{return this._date_letzteVerpflegung ;}
			set{this._date_letzteVerpflegung = value;}
		}


		#endregion

		public override string ToString()
		{
			return 
				this._Cdv_Person_daten.Name + "," +
				this._Cdv_Person_daten.Vorname + 
				"(" + this._Tdv_Position_position + ")";
		}

	}
}
