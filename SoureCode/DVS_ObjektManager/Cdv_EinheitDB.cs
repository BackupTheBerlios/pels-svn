using System;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Zusammenfassung f�r EinheitDB.
	/// </summary>
	public class Cdv_EinheitDB: Cdv_PelsObjectDB
	{
		public Cdv_EinheitDB()
		{}

		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_Einheit ceEinheit;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ung�ltiger Schl�sselwert");
			if(value is Cdv_Einheit)
				ceEinheit = (Cdv_Einheit) value;
			else
				throw new ArgumentException("Ung�ltiger Objekttyp");
		}
	}
}
