using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über einer Materialübergabe
	/// 
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   16.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Materialuebergabe : Cdv_pELSObject
	{

		#region Klassenvariablen

		//Datum der Übergabe
		private DateTime _date_datum = DateTime.Parse("01.01.1800");

		//Verleiher des Materials
		private int _i_verleiherKraftID = 0;

		//Empfänger des Materials
		private int _i_empfaengerKraftID = 0;

		//Was wird übergeben
		private int _i_uebergabepostenGutID = 0;

		//allgemeine bemerkungen
		private Cdv_Kommentar _ckom_allgBemerkungen = new Cdv_Kommentar();

		// Übergebene Menge
		private int _i_menge = 0;
		#endregion

		#region Konstruktoren
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Materialuebergabe()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes
		/// </summary>
		/// <param name="pin_Datum">Zeitpunkt der Matrialuebergabe</param>
		/// <param name="pin_VerleiherKraftID">Verweis auf die Kraft, die das Material verleiht (also aktuellerBesitzer oder Eigentumer)</param>
		/// <param name="pin_EmpfaengerKraftID">Verweis auf die Kraft, die das Material uebergeben bekommt</param>
		/// <param name="pin_GutID">Verweis auf das Gut, welches übergeben wird</param>
		public Cdv_Materialuebergabe(DateTime pin_Datum, int pin_VerleiherKraftID, int pin_EmpfaengerKraftID, int pin_GutID, int pin_Menge)
		{
			this.Datum = pin_Datum;
			this.VerleiherKraftID = pin_VerleiherKraftID;
			this.EmpfaengerKraftID = pin_EmpfaengerKraftID;
			this.UebergabepostenGutID = pin_GutID;
			this.Menge = pin_Menge;
		}

		#endregion

		#region Properties
		public DateTime Datum
		{
			get{return this._date_datum ;}
			set{this._date_datum = value;}
		}
		public int VerleiherKraftID
		{
			get{return this._i_verleiherKraftID ;}
			set{this._i_verleiherKraftID = value;}
		}
		public int EmpfaengerKraftID
		{
			get{return this._i_empfaengerKraftID ;}
			set{this._i_empfaengerKraftID = value;}
		}
		public int UebergabepostenGutID
		{
			get{return this._i_uebergabepostenGutID ;}
			set{this._i_uebergabepostenGutID = value;}
		}
		public Cdv_Kommentar AllgBemerkungen
		{
			get{return this._ckom_allgBemerkungen ;}
			set{this._ckom_allgBemerkungen = value;}
		}
		public int Menge
		{
			get{return this._i_menge;}
			set{this._i_menge = value;}
		}
		#endregion
	
		/// <summary>
		/// Hier werden die Materialübergaben aufgearbeitet um
		/// besser im entsprechenden Fenster angezeigt werden zu können
		/// </summary>
		/// <returns>einen String, der die optimale Länge zur Darstellung hat</returns>
		public override string ToString()
		{
			string pout_Text = "Materialübergabe " + this.ID.ToString() + " vom " + this.Datum.ToString() + " Uhr.";
			return pout_Text;
		}
	}
}
