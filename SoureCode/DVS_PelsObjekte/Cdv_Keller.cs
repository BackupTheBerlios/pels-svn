using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über einen Keller.
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// Hinweis: Diese Klasse tritt nur komposit als Teil eines Erkundungsobjektes auf!
	/// 
	/// alexG   15.02.2005
	/// 	
	/// </summary>
	[Serializable]
	public class Cdv_Keller
	{

		#region Klassenvariablen
		//Keller vorhanden ?
		private bool _b_vorhanden = false;

		//Wieviel Prozent sind unterkellert
		private int _i_prozent = 0;

		#endregion
		public Cdv_Keller()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Properties

		public bool Vorhanden
		{
			get{return this._b_vorhanden;}
			set{this._b_vorhanden = value;}		
		}

		public int Prozentsatz
		{
			get{ return this._i_prozent;}
			set{ this._i_prozent = value;}
		}
		#endregion
	}
}
