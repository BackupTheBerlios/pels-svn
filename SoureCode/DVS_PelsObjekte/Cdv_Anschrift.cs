using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über eine Anschrift.
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   15.02.2005
	/// 
	/// Besonderheit:
	/// Diese erbt NICHT von Cdv_pELSObject.
	/// </summary>
	[Serializable]
	public class Cdv_Anschrift
	{
		#region Klassenvariablen
		//Postleitzahl
		private string _str_PLZ;
		
		//Ort
		private string _str_Ort;

		//straße
		private string _str_Strasse;

		//Hausnummer
		private string _str_Hausnummer;
		#endregion
		
		public Cdv_Anschrift()
		{
			
		}


		#region Properties
		public string PLZ
		{
			get{return this._str_PLZ;}
			set{this._str_PLZ = value;}
		}
		public string Ort
		{
			get{return this._str_Ort;}
			set{this._str_Ort = value;}
		}
		public string Strasse
		{
			get{return this._str_Strasse;}
			set{this._str_Strasse = value;}
		}
		public string Hausnummer
		{
			get{return this._str_Hausnummer;}
			set{this._str_Hausnummer = value;}
		}
		#endregion
	}


}
