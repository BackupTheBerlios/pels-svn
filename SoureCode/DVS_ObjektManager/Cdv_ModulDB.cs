using System;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Zusammenfassung für ModulDB.
	/// </summary>
	public class Cdv_ModulDB: Cdv_PelsObjectDB
	{
		public Cdv_ModulDB()
		{}

		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_Modul cmMod;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_Modul)
				cmMod = (Cdv_Modul) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}
	}
}
