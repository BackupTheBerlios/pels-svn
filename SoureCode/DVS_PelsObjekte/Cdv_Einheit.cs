using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über eine Einheit sowie alle von Cdv_Kraft eingeerbten Eigenschaften.
	/// 
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   15.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Einheit : Cdv_Kraft
	{
		#region Klassenvariablen

		//name der einheit
		private string _str_name = null;

		//funkrufname der Einheit
		private string _str_funkrufname = null;

		//erreichbarkeit
		private string _str_erreichbarkeit = null;

		//Die Menge der Zugeordneten Helfer
		private int[] _i_helferIDMenge = null;

		//Die Menge der Zugeordneten Kfz
		private int[] _i_KfzKraefteIDMenge = null;

		//SollStärke der Einheit
		private uint _i_sollStaerke = 0;

		//IstStärke der Einheit
		private uint _i_istStaerke = 0;

		//Geschäftsstelle der Einheit
		private string _str_gst = null;

		//Ortsverband der Einheit
		private int _i_ortsverbandID = 0;
		
		//Führer der Einheit
		private int _i_einheitenfuehrerID = 0;

		//Stellvertretender Führer
		private int _i_stellvertreterID = 0;

		//Betriebsverbrauch der Einheit (in Brötchen :o)
		private string _str_betriebsverbrauch = null;

		#endregion
		#region Konstruktoren
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Einheit()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	
		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes
		/// </summary>
		/// <param name="pin_Name">Name der Einheit</param>
		/// <param name="pin_Kraeftestatus">Tdv_KraefteSatus der gesamten Einheit</param>
		/// <param name="pin_Funkrufname">Funkrufname einer Einheit</param>
		/// <param name="pin_EinheitenfuehrerHelferID">Verweis auf den Helfer, welcher die Einheit führt</param>
		/// <param name="pin_SollStaerke">Sollstaerke einer Einheit (unbedingt größer 0)</param>
		/// <param name="pin_IstStaerke">Iststaerke einer Einheit (unbedingt größer oder gleich 0)</param>
		public Cdv_Einheit(string pin_Name, Tdv_Kraeftestatus pin_Kraeftestatus, string pin_Funkrufname, int pin_EinheitenfuehrerHelferID, int pin_StellvertreterHelferID,int pin_SollStaerke, int pin_IstStaerke)
		{
			this.Name = pin_Name;
			this.Kraeftestatus = pin_Kraeftestatus;
			this.Funkrufname = pin_Funkrufname;
			this.EinheitenfuehrerHelferID = pin_EinheitenfuehrerHelferID;
			this.StellvertreterHelferID = pin_StellvertreterHelferID;
			this.SollStaerke = (uint) pin_SollStaerke;
			this.IstStaerke = (uint) pin_IstStaerke;
			
		}
		#endregion

		#region Properties
		public string Name
		{
			get{return this._str_name ;}
			set{this._str_name = value;}
		}
		public string Funkrufname
		{
			get{return this._str_funkrufname ;}
			set{this._str_funkrufname = value;}
		}
		public string Erreichbarkeit
		{
			get{return this._str_erreichbarkeit ;}
			set{this._str_erreichbarkeit= value;}
		}
		public int[] HelferIDMenge
		{
			get{return this._i_helferIDMenge ;}
			set{this._i_helferIDMenge = value;}
		}

		public int[] KfzKraefteIDMenge
		{
			get{return this._i_KfzKraefteIDMenge ;}
			set{this._i_KfzKraefteIDMenge = value;}
		}


		public uint SollStaerke
		{
			get{return this._i_sollStaerke ;}
			set{
				if(value < 0) 
					throw (new ArgumentException("Falsches Argument übergeben. \nDie Sollstärke einer Einheit muss größer als '0' sein! \nMethode: Cdv_Einheit.SollStaerke (set)"));
				else
					this._i_sollStaerke = value;}
		}
		public uint IstStaerke
		{
			get{return this._i_istStaerke ;}
			set{if (value <= 0)
					throw (new ArgumentException("Falsches Argument übergeben. \nDie Sollstärke einer Einheit muss größer oder gleich '0' sein! \nMethode: Cdv_Einheit.IstStaerke (set)"));
				this._i_istStaerke = value;}
		}
		public string GST
		{
			get{return this._str_gst ;}
			set{this._str_gst = value;}
		}
		public int OVID
		{
			get{return this._i_ortsverbandID ;}
			set{this._i_ortsverbandID = value;}
		}
		public int EinheitenfuehrerHelferID
		{
			get{return this._i_einheitenfuehrerID ;}
			set{this._i_einheitenfuehrerID = value;}
		}
		public int StellvertreterHelferID
		{
			get{return this._i_stellvertreterID ;}
			set{this._i_stellvertreterID = value;}
		}
		public string Betriebsverbrauch
		{
			get{return this._str_betriebsverbrauch ;}
			set{this._str_betriebsverbrauch = value;}
		}
		#endregion

		public override string ToString()
		{
			return this.Name + "(" + this.Funkrufname + ")";// + " ID:"+this.ID.ToString();
		}
	}
}
