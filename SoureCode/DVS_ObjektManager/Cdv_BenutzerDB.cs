using System;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Zusammenfassung f�r BenutzerDB.
	/// </summary>
	public class Cdv_BenutzerDB: Cdv_PelsObjectDB
	{
		public Cdv_BenutzerDB()
		{}

		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_Benutzer cbBenutzer;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ung�ltiger Schl�sselwert");
			if(value is Cdv_Benutzer)
				cbBenutzer = (Cdv_Benutzer) value;
			else
				throw new ArgumentException("Ung�ltiger Objekttyp");
		}
	}
}
