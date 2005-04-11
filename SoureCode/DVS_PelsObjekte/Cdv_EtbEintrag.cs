using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über einen EtbEintrag.
	/// 
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   07.03.2005
	/// </summary>
	[Serializable]
	public class Cdv_EtbEintrag : Cdv_pELSObject
	{
		
		#region Klassenvariablen

		//Beschreibung des Eintrags (inhalt)
		protected string _str_Beschreibung = null;
		
		//Erstelldatum des Eintrags
		protected DateTime _date_erstellDatum  = DateTime.Parse("01.01.1800");

		//Benutzername des Nutzers, der diesen ausgelöst/eingetragen hat
		protected string _str_benutzername = null;

		#endregion

		#region Konstruktor
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_EtbEintrag()
		{}

		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes/// 
		/// </summary>
		/// <param name="pin_Benutzername">Name des Benutzers, der diesen Eintrag erstellt hat</param>
		/// <param name="pin_ErstellDatum">Default sollte DateTime.Now sein</param>
		/// <param name="pin_Beschreibung">Beschreibung des Eintrags (Inhalt)</param>
		public Cdv_EtbEintrag(string pin_Benutzername, DateTime pin_ErstellDatum, string pin_Beschreibung)
		{
			this._str_benutzername = pin_Benutzername;
			this._date_erstellDatum = pin_ErstellDatum;
			this._str_Beschreibung = pin_Beschreibung;			
		}
		#endregion

		#region Properties

		public string Beschreibung
		{
				//set{this._str_Beschreibung = value;}
				get{return this._str_Beschreibung;}		
		}

		public DateTime ErstellDatum
		{ 
			//set{this._date_erstellDatum = value;}
			get{return this._date_erstellDatum;}
		}

		public string Benutzername
		{ 
			//set{this._str_benutzername = value;}
			get{ return this._str_benutzername;}
		}

		#endregion

		/// <summary>
		/// Hier werden die Systemereignisse aufgearbeitet um
		/// besser im entsprechenden Fenster angezeigt werden zu können
		/// </summary>
		/// <returns>einen String, der die optimale Länge zur Darstellung hat</returns>
		public override string ToString()
		{
			//Hier wird abgeschnitten, wenn es sich um zu Lange
			//Be
			int i_Max_BeschreibungsLaenge = 22;
			int i_Max_BenutzernameLaenge = 8;

			string pout_str;
			pout_str = (this.ErstellDatum.ToShortDateString()).Substring(0,6);
			pout_str += " "+this.ErstellDatum.ToShortTimeString();
			pout_str += " - ";
			if(this.Beschreibung.Length >= i_Max_BeschreibungsLaenge)
			{
				pout_str += this.Beschreibung.Substring(0, i_Max_BeschreibungsLaenge-3);
				pout_str += "...";
			}
			else
				pout_str += this.Beschreibung;
			if(this.Benutzername.Length >= i_Max_BenutzernameLaenge)
			{
				pout_str += "(" + this.Benutzername.Substring(0, i_Max_BenutzernameLaenge-2);
				pout_str +="..)";
			}
			else
				pout_str += "("+this.Benutzername+")";


			return pout_str;
		}
	}
}
