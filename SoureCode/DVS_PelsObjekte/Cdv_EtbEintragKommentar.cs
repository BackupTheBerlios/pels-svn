using System;

namespace pELS.DV

{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über einen EtbEintragskommentar.
	/// 
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   07.03.2005
	/// </summary>
	[Serializable]
	public class Cdv_EtbEintragKommentar : Cdv_pELSObject
	{

		#region Klassenvariablen

		//Kommentarinhalt
		Cdv_Kommentar _ckomm_Kommentar = new Cdv_Kommentar();

		//Erstelldatum des Kommentars
		DateTime _date_erstellDatum  = DateTime.Parse("01.01.1800");

		//erscheint der Kommentar im ETB ?
		Boolean _b_erscheintInEtb = false;

		//Verweis auf den ETBEintrag zu dem dies der Kommentar ist
		int _i_EtbEintragID = 0;
		#endregion

		#region Konstruktoren		
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_EtbEintragKommentar()
		{}

		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes		
		/// </summary>
		/// <param name="pin_EtbEintragID">Verweis auf den Eintrag, zu dem dieser Kommentar gehört</param>
		/// <param name="pin_ErstellDatum">im allg. DateTime.Now</param>
		/// <param name="pin_ErscheintInEtb">Kann später geändert werden, default sollte 'false' sein</param>
		public Cdv_EtbEintragKommentar(int pin_EtbEintragID ,DateTime pin_ErstellDatum, bool pin_ErscheintInEtb)
		{
			this._i_EtbEintragID = pin_EtbEintragID;
			this._date_erstellDatum = pin_ErstellDatum;
			this._b_erscheintInEtb = pin_ErscheintInEtb;
		}
		#endregion

		#region Properties
		public Cdv_Kommentar Kommentar
		{
			get{return this._ckomm_Kommentar;}
			set{this._ckomm_Kommentar = value;}
		}

		public DateTime ErstellDatum
		{
			//set{this._date_erstellDatum = value;}
			get{return this._date_erstellDatum;}
		}

		public int EtbEintragID
		{
			//set{this._i_EtbEintragID = value;}
			get{return this._i_EtbEintragID;}
		}

		public bool ErscheintInEtb
		{
			get{return this._b_erscheintInEtb;}
			set{this._b_erscheintInEtb = value;}
		}

		#endregion

		/// <summary>
		/// Optimiert die Kommentare um ideal in Comboboxen angezeigt zu werden.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			int i_max_Benutzername = 10;
			int i_max_Beschreibung = 20;
			string pout_str;
			
			
			if(this.Kommentar.Autor.Length > i_max_Benutzername)
			{
				pout_str = this.Kommentar.Autor.Substring(0,i_max_Benutzername-2)+"..-";
			}
			else
			{
				pout_str = this.Kommentar.Autor+"-";
			}

			if (this.Kommentar.Text.Length > i_max_Beschreibung)
			{
				pout_str += this.Kommentar.Text.Substring(0,i_max_Beschreibung-2)+"..";
			}
			else
				pout_str += this.Kommentar.Text;

			
			return pout_str;		
		}
	}
}
