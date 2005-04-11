using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über einen Termin.
	/// 
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   07.03.2005
	/// </summary>
	[Serializable]
	public class Cdv_Systemereignis : Cdv_EtbEintrag
	{

		#region Klassenvariablen
		//Art des Systemereignisses
		Tdv_SystemereignisArt _Tdv_SystemereignisArt_sysErg = Tdv_SystemereignisArt.Sonstiges;

		//erscheint dieses Systemereignis im ETB
		bool _b_erscheintInEtb = false;
 
		//erscheint
		#endregion

		#region Konstruktoren
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Systemereignis()
		{}

		public Cdv_Systemereignis(string pin_Benutzername, DateTime pin_ErstellDatum, string pin_Beschreibung,Tdv_SystemereignisArt pin_sysereig, bool pin_erscheintInEtb)
		{
			this._str_benutzername = pin_Benutzername;
			this._date_erstellDatum = pin_ErstellDatum;
			this._str_Beschreibung = pin_Beschreibung;
			this._Tdv_SystemereignisArt_sysErg = pin_sysereig;
			this._b_erscheintInEtb = pin_erscheintInEtb;
	
		}
		#endregion

		#region Properties

		public Tdv_SystemereignisArt Systemereignisart
		{
			//set{this._Tdv_SystemereignisArt_sysErg = value;}
			get{return this._Tdv_SystemereignisArt_sysErg;}
		}

		public bool ErscheintInEtb
		{
			get{return this._b_erscheintInEtb;}
			set{this._b_erscheintInEtb = value;}
		}
		#endregion


	}
}
