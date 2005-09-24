using System;
using pELS.DV.Server.Interfaces;
using pELS.DV;
using pELS.Server; 
using System.Data;
using Npgsql;


namespace pELS.DV.Server.Wrapper
{
	/// <summary>
	/// Abstrakte klasse, von der direkt alle Wrapper Klassen erben m�ssen.
	/// Sie liefert 3 virtuelle Methoden, die die Unterklassen �berschreiben sollen.
	/// Anmerkung: Bei den Methoden NeuerEintrag und AktualisiereEintrag wurde die Typpr�fung
	/// schon vorher durchgef�hrt, ein cast ist dann auch sicher (z.B. f�r die Klasse Cdv_TerminWrapper
	/// kann man das pin_ob Objekt auf Cdv_Termin ohne weiteres casten)
	/// </summary>
	public abstract class Cdv_WrapperBase
	{
		private Cdv_DB _db = null;

		protected Cdv_DB db
		{
			get{return _db;}
			set{_db = value;}
		}

		
		/// <summary>
		/// Speichert einen neuen Eintrag in der DB
		/// </summary>
		/// <param name="pin_ob">Das Objekt, das gespeichert werden soll</param>
		/// <returns>Liefert den Schl�ssel des Objektes zur�ck</returns>
		public virtual int NeuerEintrag(IPelsObject pin_ob)
		{
			return(0);
		}

		/// <summary>
		/// Aktualisiert einen Eintrag in der DB
		/// </summary>
		/// <param name="pin_ob">Das Objekt, dass aktualisiert werden soll</param>
		/// <returns>true f�r Erfolg, false f�r Misserfolg</returns>
		public virtual bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			return false;
		}

		/// <summary>
		/// L�dt die entsprechenden Objekte aus der DB in eine Objektliste
		/// </summary>
		/// <returns>Die Objekte, die geladen wurden, werden als eine Array zur�ckgeliefert</returns>
		public virtual IPelsObject[] LadeAusDerDB()
		{

			return(null);
		}
	}

	
	
	
}