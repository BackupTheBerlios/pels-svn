using System;

namespace pELS.APS.Server.Interface
{
	// ben�tigt f�r: IPelsObject
	using pELS.DV.Server.Interfaces;
	// ben�tigt f�r: pELS-Objekte
	using pELS.DV;

	/// <summary>
	/// Interface, dass alle f�r das Portal Report ben�tigten Methoden zur
	/// Verf�gung stellt
	/// </summary>
	public interface IPortalLogik_Report
	{
		/// <summary>
		/// Erzeugt aus einer CrystalReportsDatei oder einem PDF einen PDF Stream und sendet ihn an den Client
		/// </summary>
		/// <param name="pin_ReportVorlage">Pfad der Vorlage</param>
		/// <param name="pin_ReportAuswahl">reportspezifische Daten</param>
		/// <returns>Stream der ein PDF enth�lt</returns>
		System.IO.Stream ErzeugeReport(string pin_ReportVorlage, string pin_ReportAuswahl);
		/// <summary>
		/// Holen der Meldungen die in einem Report dargestellt werden k�nnen
		/// </summary>
		/// <returns></returns>
		pELS.DV.Server.Interfaces.IPelsObject[] LadeMeldungen();
		/// <summary>
		/// Holen der Auftr�ge die in einem Report dargestellt werden k�nnen
		/// </summary>
		/// <returns></returns>
		pELS.DV.Server.Interfaces.IPelsObject[] LadeAuftraege();
		/// <summary>
		/// Holen der Material�bergaben die in einem Report dargestellt werden k�nnen
		/// </summary>
		/// <returns></returns>
		pELS.DV.Server.Interfaces.IPelsObject[] LadeMaterialuebergaben();
	}
}
