using System;
using pELS.DV;
using pELS.DV.Server.ObjectManager.Interfaces;
using pELS.DV.Server.Interfaces;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Liefert die Basisfunktionalität der Dictionary Base, ist
	/// auf CAnforderung typisiert
	/// </summary>
	public class Cdv_AnforderungDB: Cdv_PelsObjectDB
	{
		#region Konstruktor
		public Cdv_AnforderungDB()
		{}
		#endregion

		#region Methoden und virtuelle Methoden
		protected override void OnInsert(Object key, Object value)
		{
			int iKey;
			Cdv_Anforderung caAnf;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_Anforderung)
				caAnf = (Cdv_Anforderung) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}
		
		protected override void OnRemove(Object key, Object value)
		{
			int iKey;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Objekttyp");			
		}

		protected override void OnSet(Object key, Object oldValue, Object newValue)
		{
			int iKey;
			Cdv_Anforderung caAnf;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(newValue is Cdv_Anforderung)
				caAnf = (Cdv_Anforderung) newValue;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}

		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_Anforderung caAnf;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_Anforderung)
				caAnf = (Cdv_Anforderung) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}
		#endregion
	}
}
