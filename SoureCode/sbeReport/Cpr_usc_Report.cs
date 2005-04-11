using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

#region Dokumentation
/**				aktuelle Version: 0.1 Quecky
INFO:
	TODO: Muss noch ergänzt werden
	- Reports müssen vernünftiges Layout bekommen
	- einige Reports/Dokumente aus dem Pflichtenheft fehlen (siehe Hinweise)
**/
#region Member-Doku
/**		
 
**/
#endregion			

#region letzte Änderungen
/**
erstellt von: Quecky					am: 21.11.2004
geändert von: Hütte						am: 24.11.2004
  review von:			am:
getestet von:			am:
**/
#endregion

#region History/Hinweise/Bekannte Bugs:
/**
History:
	- 21.11.	- Regionen angelegt
				- Struktur der Rechtevergabe durch Rollen angelegt
	- 24.11.	- Layout erstellt
				- Rechtevergabe durch Rollen implementiert (siehe Hinweise)				

Hinweise/Bekannte Bugs:
Was ist mit Dokumenten wie: Erkundungsbefehl, Erkundungsergebnisse, Einsatzschwerpunkteübersicht
**/
#endregion

#endregion

namespace pELS.Client.Report
{

	public class Cpr_usc_Report : System.Windows.Forms.UserControl
	{

		#region Klassenvariablen
		private System.Windows.Forms.TabControl tabctrl_Report;
		private System.Windows.Forms.TabPage tabpage_Report_ReportErstellen;
		private System.Windows.Forms.GroupBox gbx_Reports_ReportErstellen_ReportAuswaehlen;
		private System.Windows.Forms.ComboBox cmb_Report_ReportErstellen_ReportAuswahl;
		private System.ComponentModel.Container components = null;
		public AxPdfLib.AxPdf pdfViewer;
		private System.Windows.Forms.Button btn_Laden;
		private System.Windows.Forms.ComboBox cmb_WertAuswahl;
		private System.Windows.Forms.ComboBox cmd_Meldevordrucke;
		private System.Windows.Forms.HelpProvider pelsHelp;
		private Cst_Report _st_Report;
		#endregion

		#region Konstruktoren und Destruktoren

		public Cpr_usc_Report(Cst_Report pin_Cst_Report)
		{
			this._st_Report = pin_Cst_Report;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			// Setze Hilfe
			SetzeHilfe();
		}


		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion
		
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Cpr_usc_Report));
			this.tabctrl_Report = new System.Windows.Forms.TabControl();
			this.tabpage_Report_ReportErstellen = new System.Windows.Forms.TabPage();
			this.pdfViewer = new AxPdfLib.AxPdf();
			this.gbx_Reports_ReportErstellen_ReportAuswaehlen = new System.Windows.Forms.GroupBox();
			this.cmd_Meldevordrucke = new System.Windows.Forms.ComboBox();
			this.btn_Laden = new System.Windows.Forms.Button();
			this.cmb_WertAuswahl = new System.Windows.Forms.ComboBox();
			this.cmb_Report_ReportErstellen_ReportAuswahl = new System.Windows.Forms.ComboBox();
			this.pelsHelp = new System.Windows.Forms.HelpProvider();
			this.tabctrl_Report.SuspendLayout();
			this.tabpage_Report_ReportErstellen.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pdfViewer)).BeginInit();
			this.gbx_Reports_ReportErstellen_ReportAuswaehlen.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabctrl_Report
			// 
			this.tabctrl_Report.Controls.Add(this.tabpage_Report_ReportErstellen);
			this.tabctrl_Report.Location = new System.Drawing.Point(3, 2);
			this.tabctrl_Report.Name = "tabctrl_Report";
			this.tabctrl_Report.SelectedIndex = 0;
			this.tabctrl_Report.Size = new System.Drawing.Size(645, 525);
			this.tabctrl_Report.TabIndex = 0;
			// 
			// tabpage_Report_ReportErstellen
			// 
			this.tabpage_Report_ReportErstellen.Controls.Add(this.pdfViewer);
			this.tabpage_Report_ReportErstellen.Controls.Add(this.gbx_Reports_ReportErstellen_ReportAuswaehlen);
			this.tabpage_Report_ReportErstellen.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Report_ReportErstellen.Name = "tabpage_Report_ReportErstellen";
			this.tabpage_Report_ReportErstellen.Size = new System.Drawing.Size(637, 499);
			this.tabpage_Report_ReportErstellen.TabIndex = 0;
			this.tabpage_Report_ReportErstellen.Text = "Report erstellen";
			// 
			// pdfViewer
			// 
			this.pdfViewer.ContainingControl = this;
			this.pdfViewer.Enabled = true;
			this.pdfViewer.Location = new System.Drawing.Point(8, 64);
			this.pdfViewer.Name = "pdfViewer";
			this.pdfViewer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("pdfViewer.OcxState")));
			this.pdfViewer.Size = new System.Drawing.Size(624, 432);
			this.pdfViewer.TabIndex = 1;
			this.pdfViewer.Visible = false;
			// 
			// gbx_Reports_ReportErstellen_ReportAuswaehlen
			// 
			this.gbx_Reports_ReportErstellen_ReportAuswaehlen.Controls.Add(this.cmd_Meldevordrucke);
			this.gbx_Reports_ReportErstellen_ReportAuswaehlen.Controls.Add(this.btn_Laden);
			this.gbx_Reports_ReportErstellen_ReportAuswaehlen.Controls.Add(this.cmb_WertAuswahl);
			this.gbx_Reports_ReportErstellen_ReportAuswaehlen.Controls.Add(this.cmb_Report_ReportErstellen_ReportAuswahl);
			this.gbx_Reports_ReportErstellen_ReportAuswaehlen.Location = new System.Drawing.Point(5, 5);
			this.gbx_Reports_ReportErstellen_ReportAuswaehlen.Name = "gbx_Reports_ReportErstellen_ReportAuswaehlen";
			this.gbx_Reports_ReportErstellen_ReportAuswaehlen.Size = new System.Drawing.Size(628, 55);
			this.gbx_Reports_ReportErstellen_ReportAuswaehlen.TabIndex = 0;
			this.gbx_Reports_ReportErstellen_ReportAuswaehlen.TabStop = false;
			this.gbx_Reports_ReportErstellen_ReportAuswaehlen.Text = "Report auswählen";
			// 
			// cmd_Meldevordrucke
			// 
			this.cmd_Meldevordrucke.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmd_Meldevordrucke.Items.AddRange(new object[] {
																	"Meldevordruck Standard",
																	"Meldevordruck FGr Brückenbau",
																	"Meldevordruck FGr Elektroversorgung",
																	"Meldevordruck FGr Führung/Kommunikation",
																	"Meldevordruck FGr Infrastruktur",
																	"Meldevordruck FGr Logistik",
																	"Meldevordruck FGr Ortung",
																	"Meldevordruck FGr Ölschaden",
																	"Meldevordruck FGr Räumen",
																	"Meldevordruck FGr Trinkwasserversorgung",
																	"Meldevordruck FGr Wassergefahren",
																	"Meldevordruck FGr Wasserschaden/Pumpen",
																	"Meldevordruck Sonstige Helfer",
																	"Meldevordruck Transportkomponente",
																	"Meldevordruck Technischer Zug"});
			this.cmd_Meldevordrucke.Location = new System.Drawing.Point(300, 20);
			this.cmd_Meldevordrucke.Name = "cmd_Meldevordrucke";
			this.cmd_Meldevordrucke.Size = new System.Drawing.Size(292, 21);
			this.cmd_Meldevordrucke.TabIndex = 3;
			this.cmd_Meldevordrucke.Visible = false;
			this.cmd_Meldevordrucke.SelectedIndexChanged += new System.EventHandler(this.cmd_Meldevordrucke_SelectedIndexChanged);
			// 
			// btn_Laden
			// 
			this.btn_Laden.Enabled = false;
			this.btn_Laden.Location = new System.Drawing.Point(20, 20);
			this.btn_Laden.Name = "btn_Laden";
			this.btn_Laden.TabIndex = 2;
			this.btn_Laden.Text = "&Laden";
			this.btn_Laden.Click += new System.EventHandler(this.btn_Laden_Click);
			// 
			// cmb_WertAuswahl
			// 
			this.cmb_WertAuswahl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_WertAuswahl.Items.AddRange(new object[] {
																 "Keine Mitteilung gefunden"});
			this.cmb_WertAuswahl.Location = new System.Drawing.Point(300, 20);
			this.cmb_WertAuswahl.Name = "cmb_WertAuswahl";
			this.cmb_WertAuswahl.Size = new System.Drawing.Size(312, 21);
			this.cmb_WertAuswahl.TabIndex = 1;
			this.cmb_WertAuswahl.Visible = false;
			this.cmb_WertAuswahl.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectWert_SelectedIndexChanged);
			// 
			// cmb_Report_ReportErstellen_ReportAuswahl
			// 
			this.cmb_Report_ReportErstellen_ReportAuswahl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Report_ReportErstellen_ReportAuswahl.Items.AddRange(new object[] {
																						  "Auftrag",
																						  "Einheitenerfassungsbogen",
																						  "Einsatzschwerpunkte",
																						  "ETB",
																						  "Gütervorratsliste",
																						  "Kräfte im Einsatz",
																						  "Leihübergabebeleg",
																						  "Meldevordruck",
																						  "Meldung",
																						  "Vorblatt Einsatzbericht"});
			this.cmb_Report_ReportErstellen_ReportAuswahl.Location = new System.Drawing.Point(104, 20);
			this.cmb_Report_ReportErstellen_ReportAuswahl.Name = "cmb_Report_ReportErstellen_ReportAuswahl";
			this.cmb_Report_ReportErstellen_ReportAuswahl.Size = new System.Drawing.Size(185, 21);
			this.cmb_Report_ReportErstellen_ReportAuswahl.TabIndex = 0;
			this.cmb_Report_ReportErstellen_ReportAuswahl.SelectedIndexChanged += new System.EventHandler(this.cmb_Report_ReportErstellen_ReportAuswahl_SelectedIndexChanged);
			// 
			// Cpr_usc_Report
			// 
			this.Controls.Add(this.tabctrl_Report);
			this.Name = "Cpr_usc_Report";
			this.Size = new System.Drawing.Size(650, 530);
			this.tabctrl_Report.ResumeLayout(false);
			this.tabpage_Report_ReportErstellen.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pdfViewer)).EndInit();
			this.gbx_Reports_ReportErstellen_ReportAuswaehlen.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Funktionalität
		private void SetzeHilfe()
		{
			this.pelsHelp.HelpNamespace = _st_Report.Einstellung.Helpfile;
			this.pelsHelp.SetShowHelp(this,true);
			this.pelsHelp.SetHelpKeyword(this,"Report");

		}
		#endregion

		#region Eventhandler
		
		private void cmb_Report_ReportErstellen_ReportAuswahl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Die Extraauswahlboxen werden auf nicht ausgewählt gesetzt
			cmb_WertAuswahl.SelectedItem = null;
			cmd_Meldevordrucke.SelectedItem = null;
			
			// Abbruch wenn nichts ausgewählt wurde
			if (cmb_Report_ReportErstellen_ReportAuswahl.SelectedItem == null)
				return;
			
			// Ermitteln der Auswahl
			string str_ReportAuswahl = cmb_Report_ReportErstellen_ReportAuswahl.SelectedItem.ToString();
			
			// Überprüfen ob ein Extraauswahlbox angezeigt werden muss
			if (str_ReportAuswahl == "Meldevordruck")
			{
				// Meldevordrucke
				cmd_Meldevordrucke.Visible = true;
				cmb_WertAuswahl.Visible = false;
				btn_Laden.Enabled = false;
			}
			else if (str_ReportAuswahl == "Meldung" || str_ReportAuswahl == "Auftrag" || str_ReportAuswahl == "Leihübergabebeleg")
			{
				// Liste alle Meldungen bzw. Aufträge
				cmb_WertAuswahl.Items.Clear();
				cmb_WertAuswahl.Items.AddRange(_st_Report.LadeAuswahl(str_ReportAuswahl));
				
				cmd_Meldevordrucke.Visible = false;
				cmb_WertAuswahl.Visible = true;
				btn_Laden.Enabled = false;
			}
			else
			{
				// Keine Extrabox --> LadenButton aktivieren
				cmd_Meldevordrucke.Visible = false;
				cmb_WertAuswahl.Visible = false;
				btn_Laden.Enabled = true;
			}
		}
		
		private void cmb_SelectWert_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cmb_WertAuswahl.SelectedItem != null)
				btn_Laden.Enabled = true;
		}

		private void cmd_Meldevordrucke_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cmd_Meldevordrucke.SelectedItem != null)
				btn_Laden.Enabled = true;
		}

		private void btn_Laden_Click(object sender, System.EventArgs e)
		{
			string str_name = String.Empty;
			
			// Reportname ermitteln
			if (cmd_Meldevordrucke.SelectedItem != null)
				str_name = cmd_Meldevordrucke.SelectedItem.ToString();
			else			
				str_name = cmb_Report_ReportErstellen_ReportAuswahl.SelectedItem.ToString();
			// Wert (ID des Objektes) und Typ ermitteln falls ausgewählt
			int i_wert = -1;
			if (cmb_WertAuswahl.SelectedItem != null)
			{
				str_name = cmb_WertAuswahl.SelectedItem.GetType().ToString();
				i_wert = ((pELS.DV.Cdv_pELSObject)cmb_WertAuswahl.SelectedItem).ID;
			}
			// Ausgewählte Parameter an Steuerungsschicht weiterreichen
			if (_st_Report.BildeAuswahlAufReportvorlageAb(str_name, i_wert))
			{
				try
				{
					// Wenn laden erfolgreich war, wurde die Datei Report.pdf erstellt
					pdfViewer.LoadFile("Report.pdf");
					// Zeige PDF Viewer an fall noch nicht sichtbar
					if (!pdfViewer.Visible) pdfViewer.Visible = true;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Fehler beim Laden des Reports: " + ex.ToString());
				}
			}		
		}
		
		#endregion
		
		#region Setzen der Rollenrechte
		//Test steht noch aus.
		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
		{
			
			Tdv_Systemrolle rolle = (Tdv_Systemrolle) pin_i_aktuelleRolle;
			cmb_Report_ReportErstellen_ReportAuswahl.Items.Clear();
			cmb_Report_ReportErstellen_ReportAuswahl.Text = "";
			pdfViewer.Visible = false;
			switch (rolle)
			{
					//Haben alle die kompletten Rechte
				case Tdv_Systemrolle.Zugführer: 
				case Tdv_Systemrolle.Zugtruppführer:
				case Tdv_Systemrolle.Einsatzleiter:
				case Tdv_Systemrolle.LeiterFüSt:
				case Tdv_Systemrolle.LeiterStab:
				case Tdv_Systemrolle.Führungsgehilfe:
				{
					this.cmb_Report_ReportErstellen_ReportAuswahl.Items.AddRange(new object[] {
																								  "Auftrag",
																								  "Einheitenerfassungsbogen",
																								  "Einsatzschwerpunkte",
																								  "ETB",
																								  "Gütervorratsliste",
																								  "Kräfte im Einsatz",
																								  "Leihübergabebeleg",
																								  "Meldevordruck",
																								  "Meldung",
																								  "Vorblatt Einsatzbericht"});
					break;
				}
				// Eingeschränkte Rechte
				case Tdv_Systemrolle.Sichter :
				case Tdv_Systemrolle.Fernmelder :
				{
					this.cmb_Report_ReportErstellen_ReportAuswahl.Items.AddRange(new object[] {
																								  "Auftrag",
																								  "Einheitenerfassungsbogen",
																								  "Einsatzschwerpunkte",
																								  "ETB",
																								  "Gütervorratsliste",
																								  "Kräfte im Einsatz",
																								  "Leihübergabebeleg",
																								  "Meldevordruck",
																								  "Meldung"});
					break;
				}
				// Eingeschränkte Rechte
				case Tdv_Systemrolle.S1:
				{
					this.cmb_Report_ReportErstellen_ReportAuswahl.Items.AddRange(new object[] {
																								  "Auftrag",
																								  "Einheitenerfassungsbogen",
																								  "ETB",
																								  "Gütervorratsliste",
																								  "Kräfte im Einsatz",
																								  "Leihübergabebeleg",
																								  "Meldevordruck",
																								  "Meldung"});
					break;
				}
				
				// Eingeschränkte Rechte
				case Tdv_Systemrolle.S3:
				{
					this.cmb_Report_ReportErstellen_ReportAuswahl.Items.AddRange(new object[] {
																								  "Auftrag",
																								  "Einheitenerfassungsbogen",
																								  "Einsatzschwerpunkte",
																								  "ETB",
																								  "Gütervorratsliste",
																								  "Leihübergabebeleg",
																								  "Meldevordruck",
																								  "Meldung"});
					break;
				}

				// Eingeschränkte Rechte
				case Tdv_Systemrolle.S2:
				case Tdv_Systemrolle.S4: 
				case Tdv_Systemrolle.S5: 
				case Tdv_Systemrolle.S6:
				{
					this.cmb_Report_ReportErstellen_ReportAuswahl.Items.AddRange(new object[] {
																								  "Auftrag",
																								  "Einheitenerfassungsbogen",
																								  "ETB",
																								  "Gütervorratsliste",
																								  "Leihübergabebeleg",
																								  "Meldevordruck",
																								  "Meldung"});
					break;
				}
				default:	break;
			}
		}

		#endregion


		
	}
}
