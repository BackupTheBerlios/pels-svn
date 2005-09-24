using System;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Zusammenfassung f�r EinsatzDB.
	/// </summary>
	public class Cdv_EinsatzDB: Cdv_PelsObjectDB
	{
		public Cdv_EinsatzDB()
		{}

		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_Einsatz ceEinsatz;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ung�ltiger Schl�sselwert");
			if(value is Cdv_Einsatz)
				ceEinsatz = (Cdv_Einsatz) value;
			else
				throw new ArgumentException("Ung�ltiger Objekttyp");
		}
	}
}
