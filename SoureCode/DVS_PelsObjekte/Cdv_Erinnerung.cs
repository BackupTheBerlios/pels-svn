using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über eine Erinnerung.
	/// 
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   24.02.2005
	/// 	
	/// </summary>
	[Serializable]
	public class Cdv_Erinnerung : Cdv_pELSObject
	{
		#region Klassenvariablen

		//zeitpunkt der erinnerung
		private DateTime _date_zeitpunkt = DateTime.Parse("01.01.1800");

		//Der Text der Zur erinnerung gehört
		private string _str_erinnerungstext = null;

		//TerminID zu dem diese Erinnerung gehört
		private int _i_TerminID = 0;

		#endregion
		#region Konstruktoren
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.				
		/// </summary>
		public Cdv_Erinnerung()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Nimmt die MussFelder und belegt diese beim Erzeugen des Objektes
		/// </summary>
		/// <param name="pin_TerminID">Verweis auf den Termin, an den erinnert werden soll</param>
		/// <param name="pin_Zeitpunkt">Zeitpunkt, zu dem erinnert werden soll</param>		
		public Cdv_Erinnerung(int pin_TerminID, DateTime pin_Zeitpunkt)
		{
			this.TerminID = pin_TerminID;
			this.Zeitpunkt = pin_Zeitpunkt;			
		}
		#endregion

		#region Properties
		public DateTime Zeitpunkt
		{
			get{return this._date_zeitpunkt ;}
			set{this._date_zeitpunkt = value;}
		}

		public string Erinnerungstext
		{
			get{return this._str_erinnerungstext ;}
			set{this._str_erinnerungstext = value;}
		}

		public int TerminID
		{
			get{return this._i_TerminID;}
			set{this._i_TerminID = value;}
		}
		#endregion
	}
}
