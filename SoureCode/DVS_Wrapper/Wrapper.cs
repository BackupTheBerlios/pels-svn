using System;
using pELS.DV.Server.Interfaces;
using pELS.DV;
using pELS.Server; 
using System.Data;
using Npgsql;


namespace pELS.DV.Server.Wrapper
{
	/// <summary>
	/// Abstrakte klasse, von der direkt alle Wrapper Klassen erben müssen.
	/// Sie liefert 3 virtuelle Methoden, die die Unterklassen überschreiben sollen.
	/// Anmerkung: Bei den Methoden NeuerEintrag und AktualisiereEintrag wurde die Typprüfung
	/// schon vorher durchgeführt, ein cast ist dann auch sicher (z.B. für die Klasse Cdv_TerminWrapper
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
		/// <returns>Liefert den Schlüssel des Objektes zurück</returns>
		public virtual int NeuerEintrag(IPelsObject pin_ob)
		{
			return(0);
		}

		/// <summary>
		/// Aktualisiert einen Eintrag in der DB
		/// </summary>
		/// <param name="pin_ob">Das Objekt, dass aktualisiert werden soll</param>
		/// <returns>true für Erfolg, false für Misserfolg</returns>
		public virtual bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			return false;
		}

		/// <summary>
		/// Lädt die entsprechenden Objekte aus der DB in eine Objektliste
		/// </summary>
		/// <returns>Die Objekte, die geladen wurden, werden als eine Array zurückgeliefert</returns>
		public virtual IPelsObject[] LadeAusDerDB()
		{

			return(null);
		}
	}

	
	
	
}