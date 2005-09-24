using System;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Zusammenfassung für kfZDB.
	/// </summary>
	public class Cdv_KFZDB: Cdv_PelsObjectDB
	{
		public Cdv_KFZDB()
		{}

		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_KFZ ckKfz;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_KFZ)
				ckKfz = (Cdv_KFZ) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}
	}
}
