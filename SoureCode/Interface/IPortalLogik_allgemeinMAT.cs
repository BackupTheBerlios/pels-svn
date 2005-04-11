using System;
using pELS.DV.Server;
using pELS.Server;
using pELS.DV;

namespace Interface
{
	/// <summary>
	/// Interface, welches von allen Klassen implementiert wird, die Mitteilungen
	/// speichern müssen.
	/// </summary>
	public interface IPortalLogik_allgemeinMAT
	{
		// Schreibt einen Auftrag oder eine Meldung in die Datenbank
		public Cdv_Mitteilung SpeichereMitteilung(Cdv_Auftrag pin_auftrag);
		public Cdv_Mitteilung SpeichereMitteilung(Cdv_Meldung pin_meldung);
	}
}
