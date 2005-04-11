using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über eine KFZ sowie alle von Cdv_Kraft eingeerbten Eigenschaften.
	/// 
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   24.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_KFZ : Cdv_Kraft
	{
		#region Klassenvariablen
		//Kennzeichen des KFZ
		private string _str_kennzeichen = null;

		//Funkrufname des Fahrzeugs
		private string _str_funkrufname = null;

		//typ des KFZ (auto, lkw, etc.)
		private string _str_kfzTyp = null;

		//Fahrer des Fahrzeugs
		private int _i_fahrerHelferID = 0;

		//Die Betriebsstunden des KFZ im Einsatz
		private float _f_einsatzbetriebsstunden = 0;

		//Die gefahrenen Kilometer im Einsatz
		private float _f_einsatzKm = 0;
		#endregion
		#region Konstruktoren
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_KFZ()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes		
		/// 		 
		/// </summary>
		/// <param name="pin_Kennzeichen">Kennzeichen des Fahrzeugs</param>
		/// <param name="pin_Funkrufname">Funkrufname des Fahrzeugs</param>
		/// <param name="pin_Kraeftestatus">Tdv_Kraeftestatus des Fahrzeugs</param>
		public Cdv_KFZ(string pin_Kennzeichen, string pin_Funkrufname, Tdv_Kraeftestatus pin_Kraeftestatus)
		{
			this.Kennzeichen = pin_Kennzeichen;
			this.Funkrufname = pin_Funkrufname;			
			this.Kraeftestatus = pin_Kraeftestatus;
		}

		#endregion

		#region Properties
		public string Kennzeichen
		{
			get{return this._str_kennzeichen ;}
			set{this._str_kennzeichen = value;}
		}
		public string Funkrufname
		{
			get{return this._str_funkrufname ;}
			set{this._str_funkrufname = value;}
		}
		public string KfzTyp
		{
			get{return this._str_kfzTyp ;}
			set{this._str_kfzTyp = value;}
		}
		public int FahrerHelferID
		{
			get{return this._i_fahrerHelferID;}
			set{this._i_fahrerHelferID= value;}
		}
		public float Einsatzbetriebsstunden
		{
			get{return this._f_einsatzbetriebsstunden ;}
			set{this._f_einsatzbetriebsstunden = value;}
		}
		public float EinsatzKm
		{
			get{return this._f_einsatzKm ;}
			set{this._f_einsatzKm = value;}
		}

		#endregion

		public override string ToString()
		{
			return this._str_funkrufname +
				"(" + this._str_kfzTyp + ")";
		}
	}
}
