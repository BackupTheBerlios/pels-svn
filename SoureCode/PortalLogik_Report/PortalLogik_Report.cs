using System;
// Ben�tigt f�r CrystalReports
using CrystalDecisions.CrystalReports.Engine;

// Ben�tigt zur �bertragung des fertigen Reports als Stream an den Client
using System.IO;

namespace PortalLogik_Report
{
	// ben�tigt f�r: Cap_PortalLogik
	using pELS.Server;
	// ben�tigt f�r: IPortalLogik_XXX
	using pELS.APS.Server.Interface;
	// ben�tigt f�r: pELS.DV.Cdv_XXX
	using pELS.DV;
	// ben�tigt f�r: ObjectManager
	using pELS.DV.Server.ObjectManager;
	// ben�tigt um die Datenbankkonfiguration auszulesen
	using pELS.Tools;
	
	/// <summary>
	/// Summary description for PortalLogik_Report.
	/// </summary>
	public class CPortalLogik_Report : Cap_PortalLogik, IPortalLogik_Report
	{
		// Objekt welches den Zugriff auf die Konfiguration durchf�hrt
		XMLZugriff XMLZugriffsObjekt = new XMLZugriff();

		public CPortalLogik_Report(int pin_OMPort, string pin_URL, int pin_Port) : 
			base(pin_OMPort, pin_URL, pin_Port)
		{			
		}

		#region Cap_PortalLogik members
		protected override void SetzeRemotingPfad()
		{
			this._Pfad = "PortalReport";
		}

		public override void StartePortalLogik()
		{
		}
		#endregion

		#region IPortalLogik_Report members
		/// <summary>
		/// Erzeugt aus einer CrystalReportsDatei oder einem PDF einen PDF Stream und sendet ihn an den Client
		/// </summary>
		/// <param name="pin_ReportVorlage">Pfad der Vorlage</param>
		/// <param name="pin_ReportAuswahl">reportspezifische Daten</param>
		/// <returns>Stream der ein PDF enth�lt</returns>
		public Stream ErzeugeReport(string pin_ReportVorlage, string pin_ReportAuswahl)
		{
			if (pin_ReportVorlage.Substring(pin_ReportVorlage.LastIndexOf(".") + 1) == "pdf")
			{
				// Umwandeln des PDF Vordrucks in einen Stream
				FileStream Report = new FileStream(Directory.GetCurrentDirectory() + pin_ReportVorlage, System.IO.FileMode.Open, System.IO.FileAccess.Read);
				byte[] Buffer = new byte[(int)Report.Length];
				Report.Read(Buffer, 0, (int)Report.Length);
				Report.Close();
				// Versenden der Daten als MemoryStream
				return new MemoryStream(Buffer, 0, Buffer.Length);
	
			}
			else
			{
				// Erstellen eines Reports
				ReportDocument Report = new ReportDocument();
				// Lade Reportvorlage			
				Report.Load(Directory.GetCurrentDirectory() + pin_ReportVorlage);
				// Besondere SELECT Anfrage auf den Datensatz anwenden
				if (pin_ReportAuswahl != String.Empty)
					Report.DataDefinition.RecordSelectionFormula = pin_ReportAuswahl;
				// muss als Stream �bertragen werden, da ReportDocuments nicht Serializable sind
				return Report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
			}
		}

		/// <summary>
		/// Holt alle Meldungen und liefert sie in einem Array zur�ck
		/// </summary>
		/// <returns>Standardmeldungen oder Erkundungsergebnisse</returns>
		public pELS.DV.Server.Interfaces.IPelsObject[] LadeMeldungen()
		{
			return _ObjektManager.Meldungen.HolenAlle(); 
		}
		/// <summary>
		/// Holt alle Auftrg�ge und liefert sie in einem Array zur�ck
		/// </summary>
		/// <returns>Auftr�ge</returns>
		public pELS.DV.Server.Interfaces.IPelsObject[] LadeAuftraege()
		{
			// hole alle Auftraege
			return _ObjektManager.Auftraege.HolenAlle(); 
		}
		/// <summary>
		/// Holt alle Material�bergaben und liefert sie in einem Array zur�ck
		/// </summary>
		/// <returns>Auftr�ge</returns>
		public pELS.DV.Server.Interfaces.IPelsObject[] LadeMaterialuebergaben()
		{
			// hole alle Auftraege
			return _ObjektManager.Materialuebergaben.HolenAlle(); 
		}
		#endregion
	}
}
