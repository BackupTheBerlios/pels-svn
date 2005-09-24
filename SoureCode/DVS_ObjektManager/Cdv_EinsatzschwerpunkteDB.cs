using System;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Zusammenfassung für EinsatzschwerpunkteDB.
	/// </summary>
	public class Cdv_EinsatzschwerpunktDB: Cdv_PelsObjectDB
	{
		public Cdv_EinsatzschwerpunktDB()
		{}

		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_Einsatzschwerpunkt ceEsp;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_Einsatzschwerpunkt)
				ceEsp = (Cdv_Einsatzschwerpunkt) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}
	}
}
