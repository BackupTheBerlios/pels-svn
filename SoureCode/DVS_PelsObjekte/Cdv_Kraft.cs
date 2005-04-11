using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als abstrakte Klasse für die
	/// Objekte die zur Laufzeit und als 
	/// 
	/// Cdv_Einheit
	/// Cdv_Helfert
	/// Cdv_KFZ
	/// 
	/// auftreten können.
	/// 	
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   1.02.2005
	/// </summary>
	[Serializable]
	public abstract class Cdv_Kraft: Cdv_pELSObject
	{
		#region Klassenvariablen
		//Status der Kraft
		private Tdv_Kraeftestatus _Tdv_Kraeftestatus_kraeftestatus = Tdv_Kraeftestatus.Einsatzbereit;

		//Modul, dem diese Kraft evtl. gerade zugeordnet ist
		private int _i_ModulID = 0;

		//Einsatzschwerpunkt, an dem diese Kraft gerade arbeitet
		private int _i_ESPID = 0;

		//Kommentar zu dieser Kraft
		private Cdv_Kommentar _ckom_kommentar = new Cdv_Kommentar();
		#endregion

		public Cdv_Kraft()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Properties
		public Tdv_Kraeftestatus Kraeftestatus
		{
			get{return this._Tdv_Kraeftestatus_kraeftestatus ;}
			set{this._Tdv_Kraeftestatus_kraeftestatus = value;}
		}
		public int ModulID
		{
			get{return this._i_ModulID;}
			set{this._i_ModulID= value;}
		}
		public int EinsatzschwerpunktID
		{
			get{return this._i_ESPID;}
			set{this._i_ESPID= value;}
		}
		public Cdv_Kommentar Kommentar
		{
			get{return this._ckom_kommentar ;}
			set{this._ckom_kommentar = value;}
		}
		#endregion
	}
}
