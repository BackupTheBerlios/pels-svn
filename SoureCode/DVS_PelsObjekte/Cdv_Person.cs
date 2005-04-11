using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über eine Person.
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// Hinweis: Cdv_Person tritt nur komposit als 'Personendaten' von Cdv_Helfer auf.
	/// alexG   24.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Person : Cdv_pELSObject
	{

		#region Klassenvariablen

		//(Nach)Name der Person
		private string _str_Name = null;

		//Vorname der Person
		private string _str_Vorname = null;

		//geburtsdatum der Person
		private DateTime _date_gebDatum = DateTime.Parse("01.01.1800");

		//zusätzliche Informationen über die Person
		private string _str_Zusatzinfo = null;

		//Anschrift der Person
		private Cdv_Anschrift _ans_Anschrift = new Cdv_Anschrift();

		#endregion

		public Cdv_Person()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Properties
		public string Name
		{
			get{return this._str_Name ;}
			set{this._str_Name = value;}
		}

		public string Vorname
		{
			get{return this._str_Vorname ;}
			set{this._str_Vorname = value;}
		}

		public DateTime GebDatum
		{
			get{return this._date_gebDatum ;}
			set{this._date_gebDatum = value;}
		}

		public string ZusatzInfo
		{
			get{return this._str_Zusatzinfo ;}
			set{this._str_Zusatzinfo = value;}
		}

		public Cdv_Anschrift Anschrift
		{
			get{return this._ans_Anschrift ;}
			set{this._ans_Anschrift = value;}
		}
		#endregion
	}
}
