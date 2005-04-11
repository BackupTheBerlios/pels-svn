using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über eine Anforderung.
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   15.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Anforderung : Cdv_pELSObject
	{
		#region Klassenvariablen
		
		//Das angeforderte Gut
		private int _i_gutID = 0;
		
		//Fremdschlüssel der anfordernden Kraft
		private int _i_anforderndeKraftID = 0;

		//Umfang der Anforderung
		private float _f_Menge = 0;

		//Status der Anforderung
		private Tdv_AnforderungsStatus _Tdv_AnforderungsStatus_anforderungsstatus = Tdv_AnforderungsStatus.Neu;
		
		//Anforderungsdatum
		private DateTime _date_Anforderungsdatum = DateTime.Parse("01.01.1800");

		//Datums der Zuführung
		private DateTime _date_Zufuehrungsdatum = DateTime.Parse("01.01.1800");

		//Zweck
		private string _str_Zweck = null;

		//Kommentar
		private Cdv_Kommentar _ckom_Kommentar = new Cdv_Kommentar();

		//Ist die Anforderung gesplittet worden ?
		private bool _istTeilGueteranforderung = false;		
		#endregion Klassenvariablen
		
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Anforderung()
		{
			
			//Erzeugen aller KlassenAttribute
			this._ckom_Kommentar = new Cdv_Kommentar();
		}
		
		
		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes
		/// </summary>
		/// <param name="pin_GutID">Verweis auf das Angeforderte Gut</param>
		/// <param name="pin_Status">Status der Anforderung</param>
		/// <param name="pin_anforderndeKraftID">Verweis auf die Kraft, welche das Gut angefordert hat</param>
		/// <param name="pin_AnforderungsDatum">Zeitpunkt der Anforderung</param>
		/// <param name="pin_istTGA"></param>
		public Cdv_Anforderung(int pin_GutID,Tdv_AnforderungsStatus pin_Status, int pin_anforderndeKraftID, DateTime pin_AnforderungsDatum, bool pin_istTGA)
		{
			this._i_gutID = pin_GutID;
			this._Tdv_AnforderungsStatus_anforderungsstatus = pin_Status;
			this._i_anforderndeKraftID = pin_anforderndeKraftID;
			this._date_Anforderungsdatum = pin_AnforderungsDatum;
			this._istTeilGueteranforderung = pin_istTGA;
		}

		#region Properties
		public int GutID
		{
			get{return this._i_gutID;}
			set{this._i_gutID = value;}
		}
		
		public float Menge
		{
			get{return this._f_Menge;}
			set{this._f_Menge= value;}
		}

		public Tdv_AnforderungsStatus AnforderungsStatus
		{
			get{return this._Tdv_AnforderungsStatus_anforderungsstatus;}
			set{this._Tdv_AnforderungsStatus_anforderungsstatus= value;}
		}

		public int AnforderndeKraftID
		{
			get{return this._i_anforderndeKraftID;}
			set{this._i_anforderndeKraftID= value;}
		}
		
		public DateTime Anforderungsdatum
		{
			get{return this._date_Anforderungsdatum;}
			set{this._date_Anforderungsdatum = value;}
		}

		public DateTime Zufuehrungsdatum
		{
			get{return this._date_Zufuehrungsdatum;}
			set{this._date_Zufuehrungsdatum= value;}
		}

		public string Zweck
		{
			get{return this._str_Zweck;}
			set{this._str_Zweck= value;}
		}
		
		public Cdv_Kommentar Kommentar
		{
			get{return this._ckom_Kommentar;}
			set{this._ckom_Kommentar= value;}
		}
		public bool IstTeilgueteranforderung
		{
			get{return this._istTeilGueteranforderung;}
			set{this._istTeilGueteranforderung = value;}
		}
		#endregion

	}
}
