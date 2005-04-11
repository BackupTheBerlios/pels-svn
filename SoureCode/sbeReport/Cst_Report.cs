using System;
//benötigt für: Image
using System.Drawing;
// benötigt für ArrayList
using System.Collections;
// Benötigt für das Übertragen des Reports
using System.IO;

namespace pELS.Client.Report
{
	// benötigt für Cst_PortalLogik
	using pELS.Client;
	// benötigt für: Interface_Portale
	using pELS.APS.Server.Interface;
	// benötigt für: Cst_Einstellung
	using pELS.Tools.Client;
	// benötigt für: pELS-Objekte
	using pELS.DV;


	/// <summary>
	/// Steuerungsschicht des Portals Report.
	/// </summary>
	public class Cst_Report : Cst_PortalLogik, pELS.GUI.Interface.Isbe, pELS.Client.IReportRequested	
	{
		#region Instanzvariablen
		//Hält den Namen der Icon Datei fest
		private string _str_iconName = @"SBEImages\report.JPG";
		//Hier wird die Beschriftung unterhalb des Icons festgehalten
		private string _str_sbeName = "Reports";
		//hier wird die Klassenvariable gehalten, die die User Control enthält
		private Cpr_usc_Report _usc_Report;

		// das proxy-objekt der Klasse Cap_Report 
		private IPortalLogik_Report _ap_Report;

		#endregion
	
		#region Cst_PortalLogik members
		override protected void SetzeRemotingPfad()
		{
			this._Pfad = "PortalReport";
		}
		
		override protected void SetzePortalTyp()
		{
			this._PortalTyp = typeof(IPortalLogik_Report);
		}

		#endregion

		#region Konstruktor
		public Cst_Report(Cst_Einstellung pin_Einstellung) : 
			base(pin_Einstellung)
		{
			// INIT Proxyobjekt
			this._ap_Report = (IPortalLogik_Report) this._PortalLogik;

			// INIT Gui
			this._usc_Report = new Cpr_usc_Report(this);

			InitialisiereStartwerte();
		}
	#endregion

		#region pELS.Client.IReportRequested members
		public void BehandleReportRequestedEvent(object pin_mitteilung)
		{
			if (pin_mitteilung != null)
			{
				// Ermittelt Typ und ID und reicht diese dann zur Reportauswahl weiter
				if (this.BildeAuswahlAufReportvorlageAb(pin_mitteilung.GetType().ToString(), ((Cdv_Mitteilung)pin_mitteilung).ID))
				{
					try
					{
						// Wenn laden erfolgreich war, wurde die Datei Report.pdf erstellt
						_usc_Report.pdfViewer.LoadFile("Report.pdf");
						// Zeige PDF Viewer an fall noch nicht sichtbar
						if (!_usc_Report.pdfViewer.Visible) _usc_Report.pdfViewer.Visible = true;
					}
					catch (Exception ex)
					{
						System.Windows.Forms.MessageBox.Show("Fehler auf dem Server: Es konnte keine Verbundung zur Datenbank mit dem ODBC Treiber hergestellt werden.");
						System.Windows.Forms.MessageBox.Show("Fehler beim Laden des Reports: " + ex.ToString());
					}
				}		
			}
		}
		#endregion

		#region Cst_Report members		

		/// <summary>
		/// holt alle Daten vom Server und speichert diese in lokalen Variablen
		/// </summary>
		private void InitialisiereStartwerte()
		{
		}


		/// <summary>
		/// Hier wird die Abbildung der Einträge der Comboboxen auf Reportvorlagen vorgenommen
		/// und dann die Reporterzeugung angestoßen
		/// </summary>
		/// <returns>Erfolgreich oder nicht Erfolgreich</returns>
		public bool BildeAuswahlAufReportvorlageAb(string pin_str_Auswahl, int pin_ID)
		{
			// Dateiname der Reportvorlage
			string _str_ReportVorlage = @"\ReportVorlagen\";
			string _str_ReportAuswahl = String.Empty;

			// Abbildung der Bezeichnung in der Auswahlliste auf die Reportvorlage
			switch (pin_str_Auswahl)
			{
				#region Reportvorlagen für noch nicht implementierten Datenverwaltung
					// Die nötigen Daten für den Funkplan werden in Version 1.0 noch nicht verwaltet.
					// Falls dies in späteren Versionen geschieht, brauch der folgende Codeabschnitt nur freigeschalten werden
					/*case "Funkplan": 
					{
						_str_ReportVorlage += "Funkplan.rpt";
						break;
					}*/
					// Die nötigen Daten für die Güteranforderungsliste werd in Version 1.0 noch nicht verwaltet.
					// Falls dies in späteren Versionen geschieht, brauch der folgende Codeabschnitt nur freigeschalten werden
					/*case "Güteranforderungsliste": 
					{
						_str_ReportVorlage += "Gueteranforderungsliste.rpt";
						break;
					}*/
					#endregion
			
				#region Reportvorlagen die durch die Software mit Daten gefüllt werden
				case "pELS.DV.Cdv_Auftrag": 
				case "pELS.DV.Cdv_Erkundungsbefehl": 
				{
					_str_ReportVorlage += "Auftrag.rpt";
					_str_ReportAuswahl = "{Auftraege.ID} = " + pin_ID.ToString();
					break;
				}
				case "Einheitenerfassungsbogen":
				{
					_str_ReportVorlage += "Einheitenerfassungsbogen.rpt";
					break;
				}
				case "Einsatzschwerpunkte":
				{
					_str_ReportVorlage += "Einsatzschwerpunkte.rpt";
					break;
				}
				case "pELS.DV.Cdv_Erkundungsergebnis":
				{
					_str_ReportVorlage += "Erkundungsergebnis.rpt";
					_str_ReportAuswahl = "{Meldungen.ID} = " + pin_ID.ToString();
					break;
				}
				case "ETB":
				{
					_str_ReportVorlage += "ETB.rpt";
					break;
				}
				case "Gütervorratsliste": 
				{
					_str_ReportVorlage += "Guetervorratsliste.rpt";
					break;
				}
				case "Kräfte im Einsatz": 
				{
					_str_ReportVorlage += "KraefteImEinsatz.rpt";
					break;
				}
				case "pELS.DV.Cdv_Materialuebergabe": 
				{
					_str_ReportVorlage += "Materialuebergabebeleg.rpt";
					_str_ReportAuswahl = "{Materialuebergaben.ID} = " + pin_ID.ToString();
					break;
				}
				case "pELS.DV.Cdv_Meldung": 
				{
					_str_ReportVorlage += "Meldung.rpt";
					_str_ReportAuswahl = "{Meldungen.ID} = " + pin_ID.ToString();
					break;
				}
				#endregion

				#region Vordrucke für bestimmte Meldungen
				case "Vorblatt Einsatzbericht":
				{
					_str_ReportVorlage += "VorblattEinsatzbericht.rpt";
					break;
				}
				case "Meldevordruck Standard": 
				{
					_str_ReportVorlage += "Meldung.pdf";
					break;
				}
				case "Meldevordruck FGr Brückenbau": 
				{
					_str_ReportVorlage += "Meldevordruck-FGr-BrB.pdf";
					break;
				}
				case "Meldevordruck FGr Elektroversorgung": 
				{
					_str_ReportVorlage += "Meldevordruck-FGr-E.pdf";
					break;
				}
				case "Meldevordruck FGr Führung/Kommunikation": 
				{
					_str_ReportVorlage += "Meldevordruck-FGr-FK.pdf";
					break;
				}
				case "Meldevordruck FGr Infrastruktur": 
				{
					_str_ReportVorlage += "Meldevordruck-FGr-I.pdf";
					break;
				}
				case "Meldevordruck FGr Logistik": 
				{
					_str_ReportVorlage += "Meldevordruck-FGr-Log.pdf";
					break;
				}
				case "Meldevordruck FGr Ortung": 
				{
					_str_ReportVorlage += "Meldevordruck-FGr-O.pdf";
					break;
				}
				case "Meldevordruck FGr Ölschaden": 
				{
					_str_ReportVorlage += "Meldevordruck-FGr-Oe.pdf";
					break;
				}
				case "Meldevordruck FGr Räumen": 
				{
					_str_ReportVorlage += "Meldevordruck-FGr-R.pdf";
					break;
				}
				case "Meldevordruck FGr Trinkwasserversorgung": 
				{
					_str_ReportVorlage += "Meldevordruck-FGr-TW.pdf";
					break;
				}
				case "Meldevordruck FGr Wassergefahren": 
				{
					_str_ReportVorlage += "Meldevordruck-FGr-W.pdf";
					break;
				}
				case "Meldevordruck FGr Wasserschaden/Pumpen": 
				{
					_str_ReportVorlage += "Meldevordruck-FGr-WP.pdf";
					break;
				}
				case "Meldevordruck Sonstige Helfer": 
				{
					_str_ReportVorlage += "Meldevordruck-Sonstige-Helfer.pdf";
					break;
				}
				case "Meldevordruck Transportkomponente": 
				{
					_str_ReportVorlage += "Meldevordruck-Transportkomponente.pdf";
					break;
				}	
				case "Meldevordruck Technischer Zug": 
				{
					_str_ReportVorlage += "Meldevordruck-TZ.pdf";
					break;
				}	
					
				#endregion
			}
		
			// Report erzeugen		
			return this.ErzeugeReport(_str_ReportVorlage, _str_ReportAuswahl);
		}
		

		/// <summary>
		/// Hier wird ein PDF aus der Vorlage die vom Server als Stream kommt auf dem Client erstellt
		/// </summary>
		/// <returns>Erfolgreich oder nicht Erfolgreich</returns>
		public bool ErzeugeReport(string pin_str_ReportVorlage, string pin_str_ReportAuswahl)
		{
			try
			{
				// Weiterleitung der zu verwendenden Vorlage an den Server und Rückgabe des Reports als Stream
				Stream _stream_Report = _ap_Report.ErzeugeReport(pin_str_ReportVorlage, pin_str_ReportAuswahl);
				// Auslesen des Streams
				System.IO.StreamReader input = new System.IO.StreamReader(_stream_Report, System.Text.Encoding.BigEndianUnicode);
				// Estellen eines PDFs zum Anzeigen
				System.IO.StreamWriter output = new System.IO.StreamWriter("Report.pdf", false, System.Text.Encoding.BigEndianUnicode);
				// Umwandeln des Streams in PDF Datei
				output.Write(input.ReadToEnd());
				// Streams schließen
				input.Close();
				output.Close();
				return true;
			}
			catch(Exception)
			{
				System.Windows.Forms.MessageBox.Show("Fehler auf dem Server: Es konnte keine ODBC Verbindung zur Datenbank hergestellt werden.");
				return false;
			}
		}

		
		/// <summary>
		/// Holt Mitteilungen oder Leihübergabebelege die einzeln gedruckt werden können
		/// </summary>
		/// <returns>Alle Meldungen oder Aufträge aus der DV</returns>
		public pELS.DV.Server.Interfaces.IPelsObject[] LadeAuswahl(string pin_ObjektTyp)
		{
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = null;
			switch (pin_ObjektTyp)
			{
				case "Auftrag":
				{
					ipoa = _ap_Report.LadeAuftraege();
					break;
				}
				case "Meldung":
				{
					ipoa = _ap_Report.LadeMeldungen();
					break;
				}
				case "Leihübergabebeleg":
				{
					ipoa = _ap_Report.LadeMaterialuebergaben();
					break;
				}
			}

			// Hier Sortierung einführen
			return SortierePelsobjekte(ipoa);
		}
		
		#region Sortieren
		/// <summary>
		/// Implementation eines Comparers für Mitteilungen
		/// </summary>
		private class PelsObjektNachDatumSortierer : IComparer
		{
			public int Compare(object x,object y)
			{
				// Materialübergabebelege				
				if (x is Cdv_Materialuebergabe)
				{
					if((x as Cdv_Materialuebergabe).Datum < (y as Cdv_Materialuebergabe).Datum)
						return -1;
					if((x as Cdv_Materialuebergabe).Datum == (y as Cdv_Materialuebergabe).Datum)
						return 0;
					else
						return 1;				
				}
				
				// Mitteilungen
				if (x is Cdv_Mitteilung)
				{
					if((x as Cdv_Mitteilung).Abfassungsdatum < (y as Cdv_Mitteilung).Abfassungsdatum)
						return -1;
					if((x as Cdv_Mitteilung).Abfassungsdatum == (y as Cdv_Mitteilung).Abfassungsdatum)
						return 0;
					else
						return 1;	
				}
				return 0;
			}
		}

		/// <summary>
		/// Sortiert Mitteilungen nach dem Abfassungsdatum.
		/// </summary>
		/// <param name="pin_ipoa">zu Sortierendes Array</param>
		/// <returns>Sortiertes Array</returns>
		private pELS.DV.Server.Interfaces.IPelsObject[] SortierePelsobjekte(pELS.DV.Server.Interfaces.IPelsObject[] pin_ipoa)
		{
			IComparer MeinVergleicher = new PelsObjektNachDatumSortierer();
			Array.Sort(pin_ipoa, MeinVergleicher);

			return pin_ipoa;
		}
		#endregion
		#endregion
		
		#region Isbe Members

		public Image GetSbeImage()
		{
			System.Reflection.Assembly asm_sbe;
			//Informationen über die ausführende Assembly sammeln
			asm_sbe = System.Reflection.Assembly.GetExecutingAssembly();
			//Liefere Name der Assembly als AssemblyName
			System.Reflection.AssemblyName asm_sbeName = asm_sbe.GetName();
			//Speichere den dll Namen im String
			string strAssemblyName = asm_sbeName.Name;
			// Icon holen
			Image im_bild = Image.FromFile(_str_iconName);
			//Gebe myImage zurück
			return(im_bild);
		}

		public String GetSbeName()
		{			
			return this._str_sbeName;
		}

		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
		{
			_usc_Report.SetzeRollenRechte(pin_i_aktuelleRolle);
		}



		public System.Windows.Forms.UserControl GetSbeUserControl()
		{
			
			return this._usc_Report;
		}

		#endregion


	}
}
