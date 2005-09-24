using System;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Zusammenfassung für MaterialuebergabeDB.
	/// </summary>
	public class Cdv_MaterialuebergabeDB: Cdv_PelsObjectDB
	{
		public Cdv_MaterialuebergabeDB()
		{}

		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_Materialuebergabe cmMu;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_Materialuebergabe)
				cmMu = (Cdv_Materialuebergabe) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}
	}
}
