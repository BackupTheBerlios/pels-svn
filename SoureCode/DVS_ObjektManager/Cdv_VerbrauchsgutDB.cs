using System;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Zusammenfassung f�r VerbrauchsgutDB.
	/// </summary>
	public class Cdv_VerbrauchsgutDB: Cdv_PelsObjectDB
	{
		public Cdv_VerbrauchsgutDB()
		{}

		protected override void OnInsert(Object key, Object value)
		{
			int iKey;
			Cdv_Verbrauchsgut cvgGut;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ung�ltiger Schl�sselwert");
			if(value is Cdv_Verbrauchsgut)
				cvgGut = (Cdv_Verbrauchsgut) value;
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
			Cdv_Verbrauchsgut cvgGut;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ung�ltiger Schl�sselwert");
			if(newValue is Cdv_Verbrauchsgut)
				cvgGut = (Cdv_Verbrauchsgut) newValue;
			else
				throw new ArgumentException("Ung�ltiger Objekttyp");
		}

		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_Verbrauchsgut cvgGut;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ung�ltiger Schl�sselwert");
			if(value is Cdv_Verbrauchsgut)
				cvgGut = (Cdv_Verbrauchsgut) value;
			else
				throw new ArgumentException("Ung�ltiger Objekttyp");
		}
	}
}
