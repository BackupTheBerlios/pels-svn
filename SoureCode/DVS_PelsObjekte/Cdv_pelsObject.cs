using System;
using pELS.DV.Server.Interfaces;

namespace pELS.DV
{
	/// <summary>
	/// Summary description for Cdv_pELSObject.
	/// </summary>
	[Serializable]
	public class Cdv_pELSObject : Object, pELS.DV.Server.Interfaces.IPelsObject
	{
		#region Klassenvariablen
		//Primärschlüssel
		private int _i_ID =0;

		//Versionsnummer
		private int _i_version =0;

		#endregion

		public Cdv_pELSObject()
		{
		}

		#region Properties
		public int Version
		{
			get{return this._i_version;}
			set{this._i_version = value;}
		}
			
		public int ID
		{
			get{return this._i_ID;}			
			set{this._i_ID = value;}
		}
		#endregion
	}
}
