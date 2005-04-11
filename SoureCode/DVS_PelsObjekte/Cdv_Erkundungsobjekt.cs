using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über eine Erkundungsobjekt.
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// Hinweis: Diese Klasse tritt nur komposit als Teil eines Erkundungsergebnisses auf!
	/// 
	/// alexG   24.02.2005
	/// </summary>	
	[Serializable]
	public class Cdv_Erkundungsobjekt
	{
		#region Klassenvariablen
		//Bezeichnung des Erkundungsobjektes
		private string _str_Bezeichnung = null;

		//Anschrift des Erkundungsobjektes
		private Cdv_Anschrift _ans_Anschrift = new Cdv_Anschrift();

		//Erkundungsdatum
		private DateTime _date_Erkundungsdatum = DateTime.Parse("01.01.1800");

		//Haustyp
		private string _str_Haustyp = null;

		//Keller
		private Cdv_Keller _Cdv_Keller_Keller = new Cdv_Keller();

		//Bauart des Hauses
		private Tdv_Bauart _Tdv_Bauart_Bauart = Tdv_Bauart.Sonstiges;


		//Elektroversorgung ?
		private bool _b_Elektroversorgung = false;

		//Wasserversorgung ?
		private bool _b_Wasserversorgung = false;
		
		//Abwasserentsorgung?
		private bool _b_Abwasserentsorgung = false;

		//Heizung
		private string _str_Heizung = null;
		#endregion
		public Cdv_Erkundungsobjekt()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Properties
		public string Bezeichnung
		{
			get{return this._str_Bezeichnung ;}
			set{this._str_Bezeichnung = value;}
		}
		
		public Cdv_Anschrift Anschrift
		{
			get{return this._ans_Anschrift ;}
			set{this._ans_Anschrift = value;}
		}
		
		public DateTime Erkundungsdatum
		{
			get{return this._date_Erkundungsdatum ;}
			set{this._date_Erkundungsdatum = value;}
		}
		
		public string Haustyp
		{
			get{return this._str_Haustyp ;}
			set{this._str_Haustyp = value;}
		}
		
		public Cdv_Keller Keller
		{
			get{return this._Cdv_Keller_Keller ;}
			set{this._Cdv_Keller_Keller = value;}
		}
		
		public Tdv_Bauart Bauart
		{
			get{return this._Tdv_Bauart_Bauart ;}
			set{this._Tdv_Bauart_Bauart = value;}
		}

		public bool Elektroversorgung
		{
			get{return this._b_Elektroversorgung ;}
			set{this._b_Elektroversorgung = value;}
		}
		public bool Wasserversorgung
		{
			get{return this._b_Wasserversorgung ;}
			set{this._b_Wasserversorgung = value;}
		}
		
		public bool Abwasserentsorgung
		{
			get{return this._b_Abwasserentsorgung ;}
			set{this._b_Abwasserentsorgung = value;}
		}

		public string Heizung
		{
			get{return this._str_Heizung ;}
			set{this._str_Heizung = value;}
		}
		#endregion
	}
}
