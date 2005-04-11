using System;

namespace pELS.DV
{
	/// <summary>
	/// erfasst die IP des Servers
	/// 
	/// schuppe 17.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Serverkonfiguration
	{
		#region Klassenvariablen
		// IP des Servers im Netzwerk
		private string _IP;
		private string _Port;
		#endregion
		
		public Cdv_Serverkonfiguration()
		{
		}

		#region Properties
		public string IP
		{
			get{return this._IP;}
			set{this._IP = value;}
		}
		public string Port
		{
			get{return this._Port;}
			set{this._Port = value;}
		}
		#endregion
	}
}
