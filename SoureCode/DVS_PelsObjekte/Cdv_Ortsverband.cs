using System;

namespace pELS.DV
{
	/// <summary>	
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über einen Ortsverband.
	/// 
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   24.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Ortsverband : Cdv_pELSObject
	{
		#region Klassenvariablen
		//name des Ortsverband
		private string _str_OVName = null;

		//Ortsbeauftragter
		private string _str_Ortsbeauftragter = null;

		//Erreichbarkeit
		private string _str_OVErreichbarkeit = null;

		//Landesverband
		private string _str_Landesverband = null;
		
		//Adresse des Ortsverbandes
		private Cdv_Anschrift _ans_OVAnschrift = new Cdv_Anschrift();

		//Geschäftsführerbereich
		private string _str_GFBereich = null;

		//Geschaeftsfüreradresse
		private Cdv_Anschrift _ans_GFAnschrift = new Cdv_Anschrift();
		
		#endregion
		#region Konstruktoren
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Ortsverband()
		{
		}
		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes
		/// </summary>
		/// <param name="pin_ovName">Name des Ortsverbandes</param>
		public Cdv_Ortsverband(string pin_ovName)
		{
			this.OVName = pin_ovName;
		}
		#endregion
		public override string ToString()
		{
			return this.OVName+" ("+ this.ID +")";
		}

		#region Properties
		public string OVName
		{
			get{return this._str_OVName;}
			set{this._str_OVName = value;}
		}

		public string Ortsbeauftragter
		{
			get{return this._str_Ortsbeauftragter;}
			set{this._str_Ortsbeauftragter = value;}
		}

		public string OVErreichbarkeit		
		{
			get{return this._str_OVErreichbarkeit;}
			set{this._str_OVErreichbarkeit = value;}
		}
		
		public Cdv_Anschrift OVAnschrift
		{
			get{return this._ans_OVAnschrift;}
			set{this._ans_OVAnschrift= value;}
		}

		public string Landesverband
		{
			get{return this._str_Landesverband;}
			set{this._str_Landesverband = value;}
		}

		public string Geschaeftsfuehrerbereich
		{
			get{return this._str_GFBereich;}
			set{this._str_GFBereich = value;}
		}

		public Cdv_Anschrift Geschaeftsfuehreranschrift
		{
			get{return this._ans_GFAnschrift;}
			set{this._ans_GFAnschrift = value;}
		}

		#endregion
	}
}
