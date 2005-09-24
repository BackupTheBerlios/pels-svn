using System;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Zusammenfassung für HelferDB.
	/// </summary>
	public class Cdv_HelferDB: Cdv_PelsObjectDB
	{
		public Cdv_HelferDB()
		{}
		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_Helfer chHelfer;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_Helfer)
				chHelfer = (Cdv_Helfer) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}
	}
}
