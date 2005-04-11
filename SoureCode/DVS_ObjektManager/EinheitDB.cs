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

		protected override void OnInsert(Object key, Object value)
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
		
		protected override void OnRemove(Object key, Object value)
		{
			int iKey;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ung�ltiger Schl�sselwert");			
		}

		protected override void OnSet(Object key, Object oldValue, Object newValue)
		{
			int iKey;
			Cdv_Einheit ceEinheit;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ung�ltiger Schl�sselwert");
			if(newValue is Cdv_Einheit)
				ceEinheit = (Cdv_Einheit) newValue;
			else
				throw new ArgumentException("Ung�ltiger Objekttyp");
		}

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
