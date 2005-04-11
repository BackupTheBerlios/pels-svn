using System;

namespace pELS.APS.Server.Interface
{
	// benötigt für: IPelsObject
	using pELS.DV.Server.Interfaces;
	// benötigt für: pELS-Objekte
	using pELS.DV;

	/// <summary>
	/// Interface, dass alle für das Portal Report benötigten Methoden zur
	/// Verfügung stellt
	/// </summary>
	public interface IPortalLogik_Report
	{
		/// <summary>
		/// Erzeugt aus einer CrystalReportsDatei oder einem PDF einen PDF Stream und sendet ihn an den Client
		/// </summary>
		/// <param name="pin_ReportVorlage">Pfad der Vorlage</param>
		/// <param name="pin_ReportAuswahl">reportspezifische Daten</param>
		/// <returns>Stream der ein PDF enthält</returns>
		System.IO.Stream ErzeugeReport(string pin_ReportVorlage, string pin_ReportAuswahl);
		/// <summary>
		/// Holen der Meldungen die in einem Report dargestellt werden können
		/// </summary>
		/// <returns></returns>
		pELS.DV.Server.Interfaces.IPelsObject[] LadeMeldungen();
		/// <summary>
		/// Holen der Aufträge die in einem Report dargestellt werden können
		/// </summary>
		/// <returns></returns>
		pELS.DV.Server.Interfaces.IPelsObject[] LadeAuftraege();
		/// <summary>
		/// Holen der Materialübergaben die in einem Report dargestellt werden können
		/// </summary>
		/// <returns></returns>
		pELS.DV.Server.Interfaces.IPelsObject[] LadeMaterialuebergaben();
	}
}
