using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als abstrakte Klasse für die
	/// Objekte die zur Laufzeit und als 
	/// 
	/// Cdv_Material
	/// Cdv_Verbrauchsgut
	/// 
	/// auftreten können.
	/// 	
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   24.02.2005
	/// </summary>
	[Serializable]
	public abstract class Cdv_Gut : Cdv_pELSObject
	{
		#region Klassenvariablen
		//Bezeichnung des Guts
		private string _str_Bezeichnung = null;

		//Menge
		private float _f_Menge = 0;

		//Lagerort des Guts
		private string _str_Lagerort = null;

		//Gutart
		private string _str_Art = null;

		#endregion
		public Cdv_Gut()
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

		public float Menge
		{
			get{return this._f_Menge ;}
			set{this._f_Menge = value;}
		}

		public string Lagerort
		{
			get{return this._str_Lagerort ;}
			set{this._str_Lagerort = value;}
		}

		public string Art
		{
			get{return this._str_Art ;}
			set{this._str_Art = value;}
		}
		#endregion
	}
}
