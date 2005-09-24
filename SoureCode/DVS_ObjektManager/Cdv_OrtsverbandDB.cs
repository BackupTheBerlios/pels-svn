using System;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Zusammenfassung für OrtsverbandDB.
	/// </summary>
	public class Cdv_OrtsverbandDB: Cdv_PelsObjectDB
	{
		public Cdv_OrtsverbandDB()
		{}

		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_Ortsverband coOV;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_Ortsverband)
				coOV = (Cdv_Ortsverband) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}
	}
}
