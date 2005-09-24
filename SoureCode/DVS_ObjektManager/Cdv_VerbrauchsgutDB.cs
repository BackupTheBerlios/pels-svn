using System;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Zusammenfassung f�r VerbrauchsgutDB.
	/// </summary>
	public class Cdv_VerbrauchsgutDB: Cdv_PelsObjectDB
	{
		public Cdv_VerbrauchsgutDB()
		{}

		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_Verbrauchsgut cvgGut;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ung�ltiger Schl�sselwert");
			if(value is Cdv_Verbrauchsgut)
				cvgGut = (Cdv_Verbrauchsgut) value;
			else
				throw new ArgumentException("Ung�ltiger Objekttyp");
		}
	}
}
