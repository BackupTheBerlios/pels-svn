using System;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Zusammenfassung für ErkundungsergebnissDB.
	/// </summary>
	public class Cdv_ErkundungsergebnisDB: Cdv_PelsObjectDB
	{
		public Cdv_ErkundungsergebnisDB()
		{}

		protected override void OnInsert(Object key, Object value)
		{
			int iKey;
			Cdv_Erkundungsergebnis ceErg;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_Erkundungsergebnis)
				ceErg = (Cdv_Erkundungsergebnis) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}
		
		protected override void OnRemove(Object key, Object value)
		{
			int iKey;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");			
		}

		protected override void OnSet(Object key, Object oldValue, Object newValue)
		{
			int iKey;
			Cdv_Erkundungsergebnis ceErg;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(newValue is Cdv_Erkundungsergebnis)
				ceErg = (Cdv_Erkundungsergebnis) newValue;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}

		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_Erkundungsergebnis ceErg;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_Erkundungsergebnis)
				ceErg = (Cdv_Erkundungsergebnis) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}
	}
}
