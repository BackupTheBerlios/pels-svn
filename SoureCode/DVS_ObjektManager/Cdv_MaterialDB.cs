using System;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Zusammenfassung für MaterialDB.
	/// </summary>
	public class Cdv_MaterialDB: Cdv_PelsObjectDB
	{
		public Cdv_MaterialDB()
		{}

		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_Material cmMat;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_Material)
				cmMat = (Cdv_Material) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}
	}
}
