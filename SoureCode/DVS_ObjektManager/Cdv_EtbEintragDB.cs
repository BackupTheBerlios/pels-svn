using System;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Summary description for EtbEintragDB.
	/// </summary>
	public class Cdv_EtbEintragDB : Cdv_PelsObjectDB
	{
		public Cdv_EtbEintragDB()
		{}

		protected override void OnInsert(Object key, Object value)
		{
			int iKey;
			Cdv_EtbEintrag cmMu;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_EtbEintrag)
				cmMu = (Cdv_EtbEintrag) value;
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
			Cdv_EtbEintrag cmMu;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(newValue is Cdv_EtbEintrag)
				cmMu = (Cdv_EtbEintrag) newValue;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}

		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_EtbEintrag cmMu;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_EtbEintrag)
				cmMu = (Cdv_EtbEintrag) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}
	}
}
