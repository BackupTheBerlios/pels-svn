using System;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Zusammenfassung für EinheitDB.
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
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_Einheit)
				ceEinheit = (Cdv_Einheit) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}
	}
}
