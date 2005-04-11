using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

#region Dokumentation
/**				aktuelle Version: 0.9 Schuppe
INFO:
graphischer Aufbau
	3 TabPages: - Versenden
				- Aufnehmen & Versenden
				- Erfassen					von Mitteilungen
	TabPage Versenden
		- gestattet das Laden von Meldungen bzw. Aufträgen
		- geladende Mitteilungen dürfen nicht geändert werden
		
	TabPage Aufnehmen & Versenden
		- neue Mitteilungen können erstellt werden
		- vorher muss bestimmt werden, ob eine Meldung oder ein Auftrag erstellt werden soll
		
	TabPage Erfasse
		- eingehende Mitteilungen können in das System eingepflegt werden
		- vorher muss bestimmt werden, ob eine Meldung oder ein Auftrag eingpflegt werden soll
		
logischer Aufbau
	Klasse CMeldungsPanel
		- kapselt die Eingabemaske zum Erfassen/Anzeigen von Meldungen

	Klasse CAuftragsPanel
		- kapselt die Eingabemaske zum Erfassen/Anzeigen von Aufträgen

	Klasse usc_Funk
		- erstellt GUI-Struktur
		- initialisiert Eingabemasken

Ablauf
	- GUI-Oberfläche erzeugen
	- für alle 3 TabPages jeweils ein Objekt von Typ CMeldungsPanel und CAuftragsPanel erzeugen
		- und korrekt einstellen
			- z.B. Sichtbarkeit einzelner Elemente
	- Auswahl von Meldungs- bzw. Auftragsmasken abfragen
		- evtl. Fragen, ob Änderungen verworfen werden sollen
		- evtl. Speichern  auslösen
		- umschalten
		
**/
#region Member-Doku
/**		
	es existiert nur ein Konstruktor
**/
#endregion			

#region letzte Änderungen
/**
erstellt von: AlexG						am: 18.11.2004
geändert von: Quecky					am: 18.11.2004				
geändert von: Schuppe					am: 25.11.2004				
review von:	  Hütte						am: 29.11.2004 Rücksprache mit Schuppe per Mail wegen anstehenden Änderungen
getestet von:			am:
**/
#endregion

#region History/Hinweise/Bekannte Bugs:
/**
History:
	- 18.11.	- Regionen angelegt
				- Rechtevergabe durch Rollen vollständig
	- 25.11		- die Eingabemasken für Meldung und Auftrag als separate Klassen erstelle


Hinweise/Bekannte Bugs:

**/
#endregion

#endregion

namespace pELS.Client.Funk
{
	// benötigt für: PopUp
	using pELS.GUI.PopUp;
	// benötigt für: pELS-Objekte
	using pELS.DV;


	public class Cpr_usc_Funk : System.Windows.Forms.UserControl
	{
		#region graphische Variablen
		private System.Windows.Forms.TabPage tabpage_Aufnehmen_Versenden;
		private System.Windows.Forms.TabPage tabpage_Erfassen;
		private System.Windows.Forms.TabControl tabctrl_FUNK;
		private System.Windows.Forms.TabPage tabPage_Versenden;
		private System.Windows.Forms.Panel pnl_Versenden_AuswahlOffeneMitteilungen;
		private System.Windows.Forms.Button btn_Versenden_SchliesseAuswahlOffeneMitteilungen;
		private System.Windows.Forms.Button btn_Versenden_MitteilungAuswaehlen;
		private System.Windows.Forms.Button btn_Versenden_OeffneAuswahlOffeneAuftraege;
		private System.Windows.Forms.Button btn_Versenden_OeffneAuswahlOffeneMeldungen;
		private System.Windows.Forms.Button btn_AVMitteilung_ErstelleAuftrag;
		private System.Windows.Forms.Button btn_AVMitteilung_ErstelleMeldung;
		private System.Windows.Forms.Button btn_Erfassen_ErstelleAuftrag;
		private System.Windows.Forms.Button btn_Erfassen_ErstelleMeldung;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.DataGrid dgrid_Versenden_NichtVersandteMitteilungen;
		#endregion
		#region funktionale Variablen
		//		DataSet _dset_VersendenLadeMitteilung;
		DataTable dtable_Mitteilung;
		/// <summary>
		/// fasst alle vorhandenen Meldungspanel in einer ArrayList zusammen
		/// </summary>
		usc_Meldung[] _Meldungspanel = new usc_Meldung[3];
		/// <summary>
		/// fasst alle vorhandenen Auftragspanel in einer ArrayList zusammen
		/// </summary>
		usc_Auftrag[] _Auftragspanel = new usc_Auftrag[3];
		//Panel für alle Meldungs- und Auftragsfenster
		usc_Meldung pnl_VersendenMeldung;
		usc_Auftrag pnl_VersendenAuftrag;
		usc_Meldung pnl_AufnehmenVersendenMeldung;
		usc_Auftrag pnl_AufnehmenVersendenAuftrag;
		usc_Meldung pnl_ErfassenMeldung;
		usc_Auftrag pnl_ErfassenAuftrag;
		/// <summary>
		/// gibt an, welche Art von Mitteilung zur Zeit im NVM-Grid angezeigt wird
		/// Werte: "Meldung" oder "Auftrag"
		/// </summary>
		public string _NVM_Typ;
		private System.Windows.Forms.HelpProvider pelsHelp;
		private Cst_Funk _st_Funk;
		#endregion


		#region Konstruktor, Initialisierung und Destruktor
		public Cpr_usc_Funk(Cst_Funk pin_Cst_Funk)
		{
			this._st_Funk = pin_Cst_Funk;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			LadeMeldungsUndAuftragsPanelInFunkGUI();
			ErstelleVersendeMitteilungsGrid();
			// Hilfe festlegen
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
		
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabctrl_FUNK = new System.Windows.Forms.TabControl();
			this.tabPage_Versenden = new System.Windows.Forms.TabPage();
			this.pnl_Versenden_AuswahlOffeneMitteilungen = new System.Windows.Forms.Panel();
			this.btn_Versenden_SchliesseAuswahlOffeneMitteilungen = new System.Windows.Forms.Button();
			this.btn_Versenden_MitteilungAuswaehlen = new System.Windows.Forms.Button();
			this.dgrid_Versenden_NichtVersandteMitteilungen = new System.Windows.Forms.DataGrid();
			this.btn_Versenden_OeffneAuswahlOffeneAuftraege = new System.Windows.Forms.Button();
			this.btn_Versenden_OeffneAuswahlOffeneMeldungen = new System.Windows.Forms.Button();
			this.tabpage_Aufnehmen_Versenden = new System.Windows.Forms.TabPage();
			this.btn_AVMitteilung_ErstelleAuftrag = new System.Windows.Forms.Button();
			this.btn_AVMitteilung_ErstelleMeldung = new System.Windows.Forms.Button();
			this.tabpage_Erfassen = new System.Windows.Forms.TabPage();
			this.btn_Erfassen_ErstelleAuftrag = new System.Windows.Forms.Button();
			this.btn_Erfassen_ErstelleMeldung = new System.Windows.Forms.Button();
			this.pelsHelp = new System.Windows.Forms.HelpProvider();
			this.tabctrl_FUNK.SuspendLayout();
			this.tabPage_Versenden.SuspendLayout();
			this.pnl_Versenden_AuswahlOffeneMitteilungen.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Versenden_NichtVersandteMitteilungen)).BeginInit();
			this.tabpage_Aufnehmen_Versenden.SuspendLayout();
			this.tabpage_Erfassen.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabctrl_FUNK
			// 
			this.tabctrl_FUNK.Controls.Add(this.tabPage_Versenden);
			this.tabctrl_FUNK.Controls.Add(this.tabpage_Aufnehmen_Versenden);
			this.tabctrl_FUNK.Controls.Add(this.tabpage_Erfassen);
			this.tabctrl_FUNK.Location = new System.Drawing.Point(5, 5);
			this.tabctrl_FUNK.Name = "tabctrl_FUNK";
			this.tabctrl_FUNK.SelectedIndex = 0;
			this.tabctrl_FUNK.Size = new System.Drawing.Size(645, 525);
			this.tabctrl_FUNK.TabIndex = 5;
			// 
			// tabPage_Versenden
			// 
			this.tabPage_Versenden.Controls.Add(this.pnl_Versenden_AuswahlOffeneMitteilungen);
			this.tabPage_Versenden.Controls.Add(this.btn_Versenden_OeffneAuswahlOffeneAuftraege);
			this.tabPage_Versenden.Controls.Add(this.btn_Versenden_OeffneAuswahlOffeneMeldungen);
			this.tabPage_Versenden.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Versenden.Name = "tabPage_Versenden";
			this.tabPage_Versenden.Size = new System.Drawing.Size(637, 499);
			this.tabPage_Versenden.TabIndex = 3;
			this.tabPage_Versenden.Text = "Versenden einer Mitteilung";
			// 
			// pnl_Versenden_AuswahlOffeneMitteilungen
			// 
			this.pnl_Versenden_AuswahlOffeneMitteilungen.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.pnl_Versenden_AuswahlOffeneMitteilungen.Controls.Add(this.btn_Versenden_SchliesseAuswahlOffeneMitteilungen);
			this.pnl_Versenden_AuswahlOffeneMitteilungen.Controls.Add(this.btn_Versenden_MitteilungAuswaehlen);
			this.pnl_Versenden_AuswahlOffeneMitteilungen.Controls.Add(this.dgrid_Versenden_NichtVersandteMitteilungen);
			this.pnl_Versenden_AuswahlOffeneMitteilungen.Location = new System.Drawing.Point(4, 8);
			this.pnl_Versenden_AuswahlOffeneMitteilungen.Name = "pnl_Versenden_AuswahlOffeneMitteilungen";
			this.pnl_Versenden_AuswahlOffeneMitteilungen.Size = new System.Drawing.Size(628, 488);
			this.pnl_Versenden_AuswahlOffeneMitteilungen.TabIndex = 5;
			this.pnl_Versenden_AuswahlOffeneMitteilungen.Visible = false;
			// 
			// btn_Versenden_SchliesseAuswahlOffeneMitteilungen
			// 
			this.btn_Versenden_SchliesseAuswahlOffeneMitteilungen.Location = new System.Drawing.Point(416, 456);
			this.btn_Versenden_SchliesseAuswahlOffeneMitteilungen.Name = "btn_Versenden_SchliesseAuswahlOffeneMitteilungen";
			this.btn_Versenden_SchliesseAuswahlOffeneMitteilungen.Size = new System.Drawing.Size(88, 23);
			this.btn_Versenden_SchliesseAuswahlOffeneMitteilungen.TabIndex = 2;
			this.btn_Versenden_SchliesseAuswahlOffeneMitteilungen.Text = "a&bbrechen";
			this.btn_Versenden_SchliesseAuswahlOffeneMitteilungen.Click += new System.EventHandler(this.btn_Versenden_SchliesseAuswahlOffeneMitteilungen_Click);
			// 
			// btn_Versenden_MitteilungAuswaehlen
			// 
			this.btn_Versenden_MitteilungAuswaehlen.Location = new System.Drawing.Point(528, 456);
			this.btn_Versenden_MitteilungAuswaehlen.Name = "btn_Versenden_MitteilungAuswaehlen";
			this.btn_Versenden_MitteilungAuswaehlen.Size = new System.Drawing.Size(80, 23);
			this.btn_Versenden_MitteilungAuswaehlen.TabIndex = 1;
			this.btn_Versenden_MitteilungAuswaehlen.Text = "aus&wählen";
			this.btn_Versenden_MitteilungAuswaehlen.Click += new System.EventHandler(this.btn_Versenden_MitteilungAuswaehlen_Click);
			// 
			// dgrid_Versenden_NichtVersandteMitteilungen
			// 
			this.dgrid_Versenden_NichtVersandteMitteilungen.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dgrid_Versenden_NichtVersandteMitteilungen.DataMember = "";
			this.dgrid_Versenden_NichtVersandteMitteilungen.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgrid_Versenden_NichtVersandteMitteilungen.Location = new System.Drawing.Point(8, 8);
			this.dgrid_Versenden_NichtVersandteMitteilungen.Name = "dgrid_Versenden_NichtVersandteMitteilungen";
			this.dgrid_Versenden_NichtVersandteMitteilungen.ParentRowsVisible = false;
			this.dgrid_Versenden_NichtVersandteMitteilungen.ReadOnly = true;
			this.dgrid_Versenden_NichtVersandteMitteilungen.Size = new System.Drawing.Size(608, 440);
			this.dgrid_Versenden_NichtVersandteMitteilungen.TabIndex = 0;
			this.dgrid_Versenden_NichtVersandteMitteilungen.Click += new System.EventHandler(this.dgrid_Versenden_NichtVersandteMitteilungen_Click);
			this.dgrid_Versenden_NichtVersandteMitteilungen.DoubleClick += new System.EventHandler(this.dgrid_Versenden_NichtVersandteMitteilungen_DoubleClick);
			// 
			// btn_Versenden_OeffneAuswahlOffeneAuftraege
			// 
			this.btn_Versenden_OeffneAuswahlOffeneAuftraege.Location = new System.Drawing.Point(136, 8);
			this.btn_Versenden_OeffneAuswahlOffeneAuftraege.Name = "btn_Versenden_OeffneAuswahlOffeneAuftraege";
			this.btn_Versenden_OeffneAuswahlOffeneAuftraege.Size = new System.Drawing.Size(104, 23);
			this.btn_Versenden_OeffneAuswahlOffeneAuftraege.TabIndex = 4;
			this.btn_Versenden_OeffneAuswahlOffeneAuftraege.Text = "Lade Auftrag";
			this.btn_Versenden_OeffneAuswahlOffeneAuftraege.Click += new System.EventHandler(this.btn_Versenden_OeffneAuswahlOffeneAuftraege_Click);
			// 
			// btn_Versenden_OeffneAuswahlOffeneMeldungen
			// 
			this.btn_Versenden_OeffneAuswahlOffeneMeldungen.Location = new System.Drawing.Point(8, 8);
			this.btn_Versenden_OeffneAuswahlOffeneMeldungen.Name = "btn_Versenden_OeffneAuswahlOffeneMeldungen";
			this.btn_Versenden_OeffneAuswahlOffeneMeldungen.Size = new System.Drawing.Size(112, 23);
			this.btn_Versenden_OeffneAuswahlOffeneMeldungen.TabIndex = 3;
			this.btn_Versenden_OeffneAuswahlOffeneMeldungen.Text = "Lade Meldung";
			this.btn_Versenden_OeffneAuswahlOffeneMeldungen.Click += new System.EventHandler(this.btn_Versenden_OeffneAuswahlOffeneMeldungen_Click);
			// 
			// tabpage_Aufnehmen_Versenden
			// 
			this.tabpage_Aufnehmen_Versenden.Controls.Add(this.btn_AVMitteilung_ErstelleAuftrag);
			this.tabpage_Aufnehmen_Versenden.Controls.Add(this.btn_AVMitteilung_ErstelleMeldung);
			this.tabpage_Aufnehmen_Versenden.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Aufnehmen_Versenden.Name = "tabpage_Aufnehmen_Versenden";
			this.tabpage_Aufnehmen_Versenden.Size = new System.Drawing.Size(637, 499);
			this.tabpage_Aufnehmen_Versenden.TabIndex = 1;
			this.tabpage_Aufnehmen_Versenden.Text = "Aufnehmen und Versenden einer Mitteilung";
			// 
			// btn_AVMitteilung_ErstelleAuftrag
			// 
			this.btn_AVMitteilung_ErstelleAuftrag.Location = new System.Drawing.Point(136, 8);
			this.btn_AVMitteilung_ErstelleAuftrag.Name = "btn_AVMitteilung_ErstelleAuftrag";
			this.btn_AVMitteilung_ErstelleAuftrag.Size = new System.Drawing.Size(104, 23);
			this.btn_AVMitteilung_ErstelleAuftrag.TabIndex = 9;
			this.btn_AVMitteilung_ErstelleAuftrag.Text = "Erstelle &Auftrag";
			this.btn_AVMitteilung_ErstelleAuftrag.Click += new System.EventHandler(this.btn_AVMitteilung_ErstelleClick);
			// 
			// btn_AVMitteilung_ErstelleMeldung
			// 
			this.btn_AVMitteilung_ErstelleMeldung.Location = new System.Drawing.Point(8, 8);
			this.btn_AVMitteilung_ErstelleMeldung.Name = "btn_AVMitteilung_ErstelleMeldung";
			this.btn_AVMitteilung_ErstelleMeldung.Size = new System.Drawing.Size(112, 23);
			this.btn_AVMitteilung_ErstelleMeldung.TabIndex = 8;
			this.btn_AVMitteilung_ErstelleMeldung.Text = "Erstelle &Meldung";
			this.btn_AVMitteilung_ErstelleMeldung.Click += new System.EventHandler(this.btn_AVMitteilung_ErstelleMeldung_Click);
			// 
			// tabpage_Erfassen
			// 
			this.tabpage_Erfassen.Controls.Add(this.btn_Erfassen_ErstelleAuftrag);
			this.tabpage_Erfassen.Controls.Add(this.btn_Erfassen_ErstelleMeldung);
			this.tabpage_Erfassen.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Erfassen.Name = "tabpage_Erfassen";
			this.tabpage_Erfassen.Size = new System.Drawing.Size(637, 499);
			this.tabpage_Erfassen.TabIndex = 2;
			this.tabpage_Erfassen.Text = "Erfassen eingehender Mitteilungen";
			// 
			// btn_Erfassen_ErstelleAuftrag
			// 
			this.btn_Erfassen_ErstelleAuftrag.Location = new System.Drawing.Point(136, 8);
			this.btn_Erfassen_ErstelleAuftrag.Name = "btn_Erfassen_ErstelleAuftrag";
			this.btn_Erfassen_ErstelleAuftrag.Size = new System.Drawing.Size(104, 23);
			this.btn_Erfassen_ErstelleAuftrag.TabIndex = 11;
			this.btn_Erfassen_ErstelleAuftrag.Text = "Erstelle &Auftrag";
			this.btn_Erfassen_ErstelleAuftrag.Click += new System.EventHandler(this.btn_Erfassen_ErstelleClick);
			// 
			// btn_Erfassen_ErstelleMeldung
			// 
			this.btn_Erfassen_ErstelleMeldung.Location = new System.Drawing.Point(8, 8);
			this.btn_Erfassen_ErstelleMeldung.Name = "btn_Erfassen_ErstelleMeldung";
			this.btn_Erfassen_ErstelleMeldung.Size = new System.Drawing.Size(112, 23);
			this.btn_Erfassen_ErstelleMeldung.TabIndex = 10;
			this.btn_Erfassen_ErstelleMeldung.Text = "Erstelle &Meldung";
			this.btn_Erfassen_ErstelleMeldung.Click += new System.EventHandler(this.btn_Erfassen_ErstelleMeldung_Click);
			// 
			// Cpr_usc_Funk
			// 
			this.Controls.Add(this.tabctrl_FUNK);
			this.Name = "Cpr_usc_Funk";
			this.Size = new System.Drawing.Size(650, 530);
			this.tabctrl_FUNK.ResumeLayout(false);
			this.tabPage_Versenden.ResumeLayout(false);
			this.pnl_Versenden_AuswahlOffeneMitteilungen.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Versenden_NichtVersandteMitteilungen)).EndInit();
			this.tabpage_Aufnehmen_Versenden.ResumeLayout(false);
			this.tabpage_Erfassen.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// erzeugt und initialisiert die 3 Meldungs- 
		/// und Auftragspanels und verteilt sie auf die einzelnen TabPages
		/// </summary>
		private void LadeMeldungsUndAuftragsPanelInFunkGUI()
		{
			// erzeugen
			_Meldungspanel[0] = new usc_Meldung_Verschicken(this._st_Funk);
			_Meldungspanel[1] = new usc_Meldung(this._st_Funk);
			_Meldungspanel[2] = new usc_Meldung(this._st_Funk);
			_Auftragspanel[0] = new usc_Auftrag_Verschicken(this._st_Funk);
			_Auftragspanel[1] = new usc_Auftrag(this._st_Funk);
			_Auftragspanel[2] = new usc_Auftrag(this._st_Funk);
			pnl_VersendenMeldung = _Meldungspanel[0];
			pnl_AufnehmenVersendenMeldung = _Meldungspanel[1];
			pnl_ErfassenMeldung = _Meldungspanel[2];
			pnl_VersendenAuftrag = _Auftragspanel[0];
			pnl_AufnehmenVersendenAuftrag = _Auftragspanel[1];
			pnl_ErfassenAuftrag = _Auftragspanel[2];

			//positionieren
			foreach(usc_Meldung USCMeldung in _Meldungspanel)
			{
				USCMeldung.Location = new Point(3,45);
			}
			foreach(usc_Auftrag USCAuftrag in _Auftragspanel)
			{
				USCAuftrag.Location = new Point(3,45);
			}
			// hinzufügen
			this.tabPage_Versenden.Controls.Add(_Meldungspanel[0]);
			this.tabPage_Versenden.Controls.Add(_Auftragspanel[0]);
			this.tabpage_Aufnehmen_Versenden.Controls.Add(_Meldungspanel[1]);
			this.tabpage_Aufnehmen_Versenden.Controls.Add(_Auftragspanel[1]);
			this.tabpage_Erfassen.Controls.Add(_Meldungspanel[2]);
			this.tabpage_Erfassen.Controls.Add(_Auftragspanel[2]);
		}

		/// <summary>
		/// Erstellt das DataGrid für nicht versendete Mitteilungen
		/// </summary>
		public void ErstelleVersendeMitteilungsGrid()
		{
			// erstelle die Tabelle zur Anzeige der Daten.
			dtable_Mitteilung = new DataTable("dataTableNVM");

			// erstelle die benötigten Spalten
			DataColumn dcol_AbDate = new DataColumn("Abfassungsdatum", typeof(DateTime));
			DataColumn dcol_Absender = new DataColumn("Absender", typeof(string));
			DataColumn dcol_Text = new DataColumn("Text", typeof(string));
			DataColumn dcol_UeArt = new DataColumn("Uebermittlungsart", typeof(string));
			dcol_UeArt.Caption = "Übermittlungsart";

			// füge die Spalten zur Tabelle hinzu
			dtable_Mitteilung.Columns.Add(dcol_AbDate);
			dtable_Mitteilung.Columns.Add(dcol_Absender);
			dtable_Mitteilung.Columns.Add(dcol_Text);
			dtable_Mitteilung.Columns.Add(dcol_UeArt);

			// füge die Tabelle zur DataSet hinzu
			this.dgrid_Versenden_NichtVersandteMitteilungen.DataSource = dtable_Mitteilung;

			// erzeuge neuen Table-Style
			DataGridTableStyle ts = new DataGridTableStyle();
			ts.MappingName = "dataTableNVM";
			this.dgrid_Versenden_NichtVersandteMitteilungen.TableStyles.Add(ts);
			DataGridColumnStyle _dgcs;
			// Abfassungsdatum
			_dgcs = this.dgrid_Versenden_NichtVersandteMitteilungen.
				TableStyles["dataTableNVM"].
				GridColumnStyles["Abfassungsdatum"];
			_dgcs.HeaderText = "Abf.Datum";
			_dgcs.Width = 60;
			// Absender
			_dgcs = this.dgrid_Versenden_NichtVersandteMitteilungen.
				TableStyles["dataTableNVM"].
				GridColumnStyles["Absender"];
			_dgcs.HeaderText = "Absender";
			_dgcs.Width = 120;
			// Text
			_dgcs = this.dgrid_Versenden_NichtVersandteMitteilungen.
				TableStyles["dataTableNVM"].
				GridColumnStyles["Text"];
			_dgcs.HeaderText = "Mitteilungstext";
			_dgcs.Width = 335;
			// Übermittlungsart
			_dgcs = this.dgrid_Versenden_NichtVersandteMitteilungen.
				TableStyles["dataTableNVM"].
				GridColumnStyles["Uebermittlungsart"];
			_dgcs.HeaderText = "ÜM-Art";
			_dgcs.Width = 50;
		}

		
	
		#endregion

		#region SetzeXXX
		/// <summary>
		/// überträgt die aktuellen Einheiten, KFZ und Helfer in ein TreeView-Element
		/// </summary>
		/// <param name="pin_TreeView">das zu modifizierende TreeView</param>
		private void SetzeTreeViewMitteilungsEmpfaenger(TreeView pin_TreeView)
		{
			pin_TreeView.BeginUpdate();
			pin_TreeView.Nodes.Clear();
			// alle mögliche Kräftetypen
			string[] str_typmenge = new string[3];
			str_typmenge[0] = "Einheiten";
			str_typmenge[1] = "Helfer";
			str_typmenge[2] = "KFZ";
			
			// Knoten auf der obersten Hierachieebene erstellen
			for(int i=0; i<str_typmenge.Length; i++)
			{
				pin_TreeView.Nodes.Add(str_typmenge[i]);
			}
			// Knoten unter dem Oberknoten "Einheit"
			foreach(Cdv_Einheit Einheit in this._st_Funk._AlleEinheiten)
			{
				TreeNode neuerKnoten = new TreeNode();
				neuerKnoten.Text = Einheit.ToString();
				neuerKnoten.Tag = Einheit;
				pin_TreeView.Nodes[0].Nodes.Add(neuerKnoten);
			}

			foreach(Cdv_Helfer Helfer in this._st_Funk._AlleHelfer)
			{
				TreeNode neuerKnoten = new TreeNode();
				neuerKnoten.Text = Helfer.ToString();
				neuerKnoten.Tag = Helfer;
				pin_TreeView.Nodes[1].Nodes.Add(neuerKnoten);
			}
			pin_TreeView.EndUpdate();

			foreach(Cdv_KFZ KFZ in this._st_Funk._AlleKFZs)
			{
				TreeNode neuerKnoten = new TreeNode();
				neuerKnoten.Text = KFZ.ToString();
				neuerKnoten.Tag = KFZ;
				pin_TreeView.Nodes[2].Nodes.Add(neuerKnoten);
			}

		}

		/// <summary>
		/// Füllt alle Comboboxen mit alle möglichen Einsatzschwerpunkte
		/// </summary>
		public void SetzeESP()
		{
			foreach(usc_Meldung USCMeldung in _Meldungspanel)
			{
				SetzeESP(USCMeldung.cmb_Einsatzschwerpunkte);
			}
		}
		
		/// <summary>
		/// Füllt konkrete Combobox mit allen möglichen Einsatzschwerpunkte
		/// </summary>
		private void SetzeESP(ComboBox pin_ComboBox)
		{
			pin_ComboBox.Items.Clear();
			// Selbst wenn es noch kein ESP existiert, wirf keine Exception aus
			Cdv_Einsatzschwerpunkt[] AlleEsp;
			if((AlleEsp = _st_Funk._AlleESP) == null)
			{
				AlleEsp = new Cdv_Einsatzschwerpunkt[0];
			}
			foreach(Cdv_Einsatzschwerpunkt ESP in AlleEsp)
			{
				pin_ComboBox.Items.Add(ESP);
			}

		}
		private void SetzeAktuellenBenutzer(ComboBox pin_ComboBox)
		{
			pin_ComboBox.Items.Clear();
			foreach(Cdv_Benutzer Benutzer in _st_Funk._AlleBenutzer)
			{
				pin_ComboBox.Items.Add(Benutzer.ToString());
			}

		}

		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
		{
			// Akualisiere den Benutzer aller user-controls
			this.AkualisiereAktBenutzer();

			Tdv_Systemrolle rolle = (Tdv_Systemrolle)pin_i_aktuelleRolle;
			this.tabctrl_FUNK.TabPages.Clear();
			
			switch (rolle)
			{
					//Haben alle die kompletten Rechte
				case Tdv_Systemrolle.Zugführer: 
				case Tdv_Systemrolle.Zugtruppführer:
				case Tdv_Systemrolle.Fernmelder :
				{
					//F90
					this.tabctrl_FUNK.TabPages.Add(this.tabPage_Versenden);
					//F100
					this.tabctrl_FUNK.TabPages.Add(this.tabpage_Aufnehmen_Versenden);
					//F110
					this.tabctrl_FUNK.TabPages.Add(this.tabpage_Erfassen);
					break;
				}
				
					// Darf nur der Führungsgehilfe
				case Tdv_Systemrolle.Führungsgehilfe:
				{
					//F110
					this.tabctrl_FUNK.TabPages.Add(this.tabpage_Erfassen);
					break;
				}
					
					//Haben alle keine Rechte (ALLE)
				case Tdv_Systemrolle.Einsatzleiter:
				case Tdv_Systemrolle.LeiterFüSt:
				case Tdv_Systemrolle.LeiterStab:
				case Tdv_Systemrolle.Sichter :
				case Tdv_Systemrolle.S1:
				case Tdv_Systemrolle.S2:
				case Tdv_Systemrolle.S4: 
				case Tdv_Systemrolle.S5: 
				case Tdv_Systemrolle.S6:
				case Tdv_Systemrolle.S3 :
				default:	break;
			}
		}


		/// <summary>
		/// setzt alle Meldungs- und Auftragsempfänger
		/// </summary>
		public void SetzeMitteilungsEmpfaenger()
		{

			// Hole alle IDs der Kraft, die im Treeview beim letzten mal gecheckt wurden.
			// Nachdem die kraftdaten neu geladen werden, setze die Häckchen vor diesen IDs
			// wieder.
			// Falls das Treeview gar nicht ausgecheckt wurde, wirkt der Code nichts als 
			// nur die Kraftdaten neu zu laden aus
			foreach(usc_Meldung USCMeldung in _Meldungspanel)
			{	
				int[]	IDMengeINT;
				if(USCMeldung.tvw_Empfaenger.Nodes.Count != 0)				
				{
					ArrayList IDMenge =
						Cpr_Funk_AllgFkt.HoleAlleAusgewaehltenEmpfaengerIDs(USCMeldung.tvw_Empfaenger.Nodes);
					IDMengeINT = (int[]) IDMenge.ToArray(typeof(int));
				}
				else
					IDMengeINT = new int[0];					

				SetzeTreeViewMitteilungsEmpfaenger(
					USCMeldung.tvw_Empfaenger);

				if(IDMengeINT.Length != 0)
				{
					Cpr_Funk_AllgFkt.SetzeAlleAusgewaehltenEmpfaenger(USCMeldung.tvw_Empfaenger.Nodes,IDMengeINT);
				}

			}
			foreach(usc_Auftrag USCAuftrag in _Auftragspanel)
			{

				int[]	IDMengeINT;
				if(USCAuftrag.tvw_AuftragsEmpfaenger.Nodes.Count != 0)				
				{
					ArrayList IDMenge =
						Cpr_Funk_AllgFkt.HoleAlleAusgewaehltenEmpfaengerIDs(USCAuftrag.tvw_AuftragsEmpfaenger.Nodes);
					IDMengeINT = (int[]) IDMenge.ToArray(typeof(int));
				}
				else
					IDMengeINT = new int[0];					

				SetzeTreeViewMitteilungsEmpfaenger(
					USCAuftrag.tvw_AuftragsEmpfaenger);

				if(IDMengeINT.Length != 0)
				{
					Cpr_Funk_AllgFkt.SetzeAlleAusgewaehltenEmpfaenger(USCAuftrag.tvw_AuftragsEmpfaenger.Nodes, IDMengeINT);
				}

				
			}

		}
		/// <summary>
		/// setzt alle möglichen Benutzer-Empfänger 
		/// </summary>
		public void SetzeBenutzerEmpfaenger()
		{
			foreach(usc_Meldung USCMeldung in _Meldungspanel)
			{
				SetzeAktuellenBenutzer(
					USCMeldung.cmb_Benutzerempfaenger);
			}
			foreach(usc_Auftrag USCAuftrag in _Auftragspanel)
			{
				SetzeAktuellenBenutzer(
					USCAuftrag.cmb_Benutzerempfaenger);
			}
		}
		/// <summary>
		/// setzt den aktuellen Benutzer
		/// </summary>
		public void SetzeAktuellenBenutzer()
		{
			foreach(usc_Meldung USCMeldung in _Meldungspanel)
			{
				USCMeldung.SetzeBenutzer(this._st_Funk.HoleAktuellenBenutzer());
			}
			foreach(usc_Auftrag USCAuftrag in _Auftragspanel)
			{
				USCAuftrag.SetzeBenutzer(this._st_Funk.HoleAktuellenBenutzer());
			}
		}
		

		/// <summary>
		/// setzt alle möglichen Werte
		/// </summary>
		public void SetzeAlle()
		{
			SetzeMitteilungsEmpfaenger();
			SetzeBenutzerEmpfaenger();
			SetzeAktuellenBenutzer();
			SetzeESP();
		}
		#endregion

		#region Methoden für das Nicht-Versendete-Mitteilungen-Grid
		/// <summary>
		/// fügt dem dataGrid NVM eine weitere Mitteilung hinzu
		/// </summary>
		/// <param name="pin_Mitteilung"></param>
		private void FuegeMitteilungZuNVMGridHinzu(Cdv_Mitteilung pin_Mitteilung)
		{

			DataRow neuerEintrag = dtable_Mitteilung.NewRow();
			neuerEintrag["Abfassungsdatum"]		= pin_Mitteilung.Abfassungsdatum.ToString();
			neuerEintrag["Absender"]			= pin_Mitteilung.Absender;
			neuerEintrag["Text"]				= pin_Mitteilung.Text;
			neuerEintrag["Uebermittlungsart"]	= pin_Mitteilung.Uebermittlungsart.ToString();

			dtable_Mitteilung.Rows.Add(neuerEintrag);

		}

		/// <summary>
		/// lädt alle Mitteilungen in das dataGrid NVM (Überladen)
		/// </summary>
		public void LadeNVMGrid()
		{
			LadeNVMGrid(this._NVM_Typ);
		}
		/// <summary>
		/// lädt alle Mitteilungen in das dataGrid NVM (Überladen)
		/// </summary>
		/// <param name="pin_Mitteilungstyp">"Meldung" oder "Auftrag"</param>
		public void LadeNVMGrid(string pin_Mitteilungstyp)
		{
			// hole die Tabelle, in welcher geschrieben werden soll
			// entferne alle alten Einträge aus der Tabelle
			dtable_Mitteilung.Clear();
			// fülle die Tabelle entweder 
			switch (pin_Mitteilungstyp)
			{
				// mit Meldungen
				case "Meldung":
				{
					this.dgrid_Versenden_NichtVersandteMitteilungen.CaptionText = 
						"nicht versendete Meldungen";
					foreach(Cdv_Mitteilung Meldung in _st_Funk._AlleMeldungen)
					{
						FuegeMitteilungZuNVMGridHinzu(Meldung);
					}
					break;
				}
				// oder mit Aufträgen
				case "Auftrag":
				{
					this.dgrid_Versenden_NichtVersandteMitteilungen.CaptionText = 
						"nicht versendete Aufträge";
					foreach(Cdv_Mitteilung Meldung in _st_Funk._AlleAuftraege)
					{
						FuegeMitteilungZuNVMGridHinzu(Meldung);
					}
					break;
				}
			}
		}
		#endregion

		private void SetzeHilfe()
		{
			this.pelsHelp.HelpNamespace = _st_Funk.Einstellung.Helpfile;
			this.pelsHelp.SetShowHelp(this,true);
			this.pelsHelp.SetHelpKeyword(this,"Funk");

		}
		
		/// <summary>
		/// Wenn die Systemrolle des aktuellen Benutzers verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AkualisiereAktBenutzer()
		{
			foreach(usc_Meldung USCMeldung in _Meldungspanel)
				USCMeldung.AkualisiereAktBenutzer();
			foreach(usc_Auftrag USCAuftrag in _Auftragspanel)
				USCAuftrag.AkualisiereAktBenutzer();
		}
		#region events
		private void btn_Versenden_OeffneAuswahlOffeneMeldungen_Click(object sender, System.EventArgs e)
		{
			LadeNVMGrid("Meldung");
			pnl_Versenden_AuswahlOffeneMitteilungen.Visible = true;

			pnl_VersendenMeldung.Zuruecksetzen();
			pnl_VersendenAuftrag.Zuruecksetzen();
			pnl_VersendenMeldung.Visible = true;
			pnl_VersendenAuftrag.Visible = false;
			this._NVM_Typ = "Meldung";
		}

		private void btn_Versenden_OeffneAuswahlOffeneAuftraege_Click(object sender, System.EventArgs e)
		{
			LadeNVMGrid("Auftrag");
			pnl_Versenden_AuswahlOffeneMitteilungen.Visible = true;
			pnl_VersendenMeldung.Zuruecksetzen();
			pnl_VersendenAuftrag.Zuruecksetzen();
			pnl_VersendenMeldung.Visible = false;
			pnl_VersendenAuftrag.Visible = true;
			this._NVM_Typ = "Auftrag";
		}

		private void btn_Versenden_MitteilungAuswaehlen_Click(object sender, System.EventArgs e)
		{
			switch (_NVM_Typ)
			{
				case "Meldung":
					if ((dgrid_Versenden_NichtVersandteMitteilungen.CurrentRowIndex < _st_Funk._AlleMeldungen.Length) &&
						(dgrid_Versenden_NichtVersandteMitteilungen.CurrentRowIndex != -1))
					{
						pnl_VersendenMeldung.LadeMeldung(this._st_Funk._AlleMeldungen[
							this.dgrid_Versenden_NichtVersandteMitteilungen.CurrentRowIndex]);
					}
					break;
				case "Auftrag":
					if ((dgrid_Versenden_NichtVersandteMitteilungen.CurrentRowIndex < _st_Funk._AlleAuftraege.Length) &&
						(dgrid_Versenden_NichtVersandteMitteilungen.CurrentRowIndex != -1))
					{
						pnl_VersendenAuftrag.LadeAuftrag(this._st_Funk._AlleAuftraege[
							this.dgrid_Versenden_NichtVersandteMitteilungen.CurrentRowIndex]);
					}
					break;
			}
			pnl_Versenden_AuswahlOffeneMitteilungen.Visible = false;
		}

		private void btn_AVMitteilung_ErstelleMeldung_Click(object sender, System.EventArgs e)
		{
			// ermitteln, ob im aktuell sichtbaren Panel Änderungen vorgenommen wurden
			bool _b_modifiziert;
			if (pnl_AufnehmenVersendenMeldung.Visible == true)
				_b_modifiziert = pnl_AufnehmenVersendenMeldung.b_FelderModifiziert;
			else 
				_b_modifiziert = pnl_AufnehmenVersendenAuftrag.b_FelderModifiziert;
			// falls Änderungen vorgenommen
			if (_b_modifiziert)
			{
				// frage, ob Änderungen verworfen werden sollen
				if (CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes)
				{
					pnl_AufnehmenVersendenMeldung.Zuruecksetzen();
					pnl_AufnehmenVersendenAuftrag.Zuruecksetzen();
					pnl_AufnehmenVersendenMeldung.Visible = true;
					pnl_AufnehmenVersendenAuftrag.Visible = false;
					// Empfängerliste aktualisieren
					SetzeTreeViewMitteilungsEmpfaenger(
						pnl_AufnehmenVersendenMeldung.tvw_Empfaenger);
				}
			}	
			// falls keine Änderungen, wechsle Ansicht
			else 
			{
				pnl_AufnehmenVersendenMeldung.Zuruecksetzen();
				pnl_AufnehmenVersendenAuftrag.Zuruecksetzen();
				pnl_AufnehmenVersendenMeldung.Visible = true;
				pnl_AufnehmenVersendenAuftrag.Visible = false;
				// Empfängerliste aktualisieren
				SetzeTreeViewMitteilungsEmpfaenger(pnl_AufnehmenVersendenMeldung.tvw_Empfaenger);
			}
		}

		private void btn_AVMitteilung_ErstelleClick(object sender, System.EventArgs e)
		{
			// ermitteln, ob im aktuell sichtbaren Panel Änderungen vorgenommen wurden
			bool _b_modifiziert;
			if (pnl_AufnehmenVersendenMeldung.Visible)
				_b_modifiziert = pnl_AufnehmenVersendenMeldung.b_FelderModifiziert;
			else 
				_b_modifiziert = pnl_AufnehmenVersendenAuftrag.b_FelderModifiziert;
			// falls Änderungen vorgenommen
			if (_b_modifiziert)
			{
				// frage, ob Änderungen verworfen werden sollen
				if (CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes)
				{
					pnl_AufnehmenVersendenMeldung.Zuruecksetzen();
					pnl_AufnehmenVersendenAuftrag.Zuruecksetzen();
					pnl_AufnehmenVersendenMeldung.Visible = false;
					pnl_AufnehmenVersendenAuftrag.Visible = true;
					// Empfängerliste aktualisieren
					SetzeTreeViewMitteilungsEmpfaenger(
					pnl_AufnehmenVersendenAuftrag.tvw_AuftragsEmpfaenger);
				}
			}	
				// falls keine Änderungen, wechsle Ansicht
			else 
			{
				pnl_AufnehmenVersendenMeldung.Zuruecksetzen();
				pnl_AufnehmenVersendenAuftrag.Zuruecksetzen();
				pnl_AufnehmenVersendenMeldung.Visible = false;
				pnl_AufnehmenVersendenAuftrag.Visible = true;
				// Empfängerliste aktualisieren
				SetzeTreeViewMitteilungsEmpfaenger(pnl_AufnehmenVersendenAuftrag.tvw_AuftragsEmpfaenger);
			}
		}

		private void btn_Erfassen_ErstelleMeldung_Click(object sender, System.EventArgs e)
		{
			// ermitteln, ob im aktuell sichtbaren Panel Änderungen vorgenommen wurden
			bool _b_modifiziert = false;
			if (pnl_ErfassenMeldung.Visible)
				_b_modifiziert = pnl_ErfassenMeldung.b_FelderModifiziert;
			else 
				_b_modifiziert = pnl_ErfassenAuftrag.b_FelderModifiziert;
			// falls Änderungen vorgenommen
			if (_b_modifiziert)
			{
				// frage, ob Änderungen verworfen werden sollen
				if (CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes)
				{
					pnl_ErfassenMeldung.Zuruecksetzen();
					pnl_ErfassenAuftrag.Zuruecksetzen();
					pnl_ErfassenMeldung.Visible = true;
					pnl_ErfassenAuftrag.Visible = false;
					// Empfängerliste aktualisieren
					SetzeTreeViewMitteilungsEmpfaenger(
					pnl_ErfassenMeldung.tvw_Empfaenger);
				}
			}	
				// falls keine Änderungen, wechsle Ansicht
			else 
			{
				pnl_ErfassenMeldung.Zuruecksetzen();
				pnl_ErfassenAuftrag.Zuruecksetzen();
				pnl_ErfassenMeldung.Visible = true;
				pnl_ErfassenAuftrag.Visible = false;
				pnl_ErfassenAuftrag.b_FelderModifiziert = false;
				// Empfängerliste aktualisieren
				SetzeTreeViewMitteilungsEmpfaenger(
				pnl_ErfassenMeldung.tvw_Empfaenger);
			}
		}

		private void btn_Erfassen_ErstelleClick(object sender, System.EventArgs e)
		{
			// ermitteln, ob im aktuell sichtbaren Panel Änderungen vorgenommen wurden
			bool _b_modifiziert;
			if (pnl_ErfassenMeldung.Visible == true)
				_b_modifiziert = pnl_ErfassenMeldung.b_FelderModifiziert;
			else 
				_b_modifiziert = pnl_ErfassenAuftrag.b_FelderModifiziert;
			if (_b_modifiziert)
			{
				// frage, ob Änderungen verworfen werden sollen
				if (CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes)
				{
					pnl_ErfassenMeldung.Zuruecksetzen();
					pnl_ErfassenAuftrag.Zuruecksetzen();
					pnl_ErfassenMeldung.Visible = false;
					pnl_ErfassenAuftrag.Visible = true;
					// Empfängerliste aktualisieren
					SetzeTreeViewMitteilungsEmpfaenger(
						pnl_ErfassenAuftrag.tvw_AuftragsEmpfaenger);
				}
			}	
				// falls keine Änderungen, wechsle Ansicht
			else 
			{
				pnl_ErfassenMeldung.Zuruecksetzen();
				pnl_ErfassenAuftrag.Zuruecksetzen();
				pnl_ErfassenMeldung.Visible = false;
				pnl_ErfassenAuftrag.Visible = true;
				// Empfängerliste aktualisieren
				SetzeTreeViewMitteilungsEmpfaenger(
					pnl_ErfassenAuftrag.tvw_AuftragsEmpfaenger);
			}
		}
		
		#region NVM-Grid
		/// <summary>
		/// wählt immer eine komplette Zeile aus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrid_Versenden_NichtVersandteMitteilungen_Click(object sender, System.EventArgs e)
		{
			//Fängt den Fall ab, dass jemand außerhalb des Datenbereichs klickt
			if(this.dgrid_Versenden_NichtVersandteMitteilungen.CurrentRowIndex >= 0)
			{
				this.dgrid_Versenden_NichtVersandteMitteilungen.Select
					(this.dgrid_Versenden_NichtVersandteMitteilungen.CurrentRowIndex);
			}
		}

		/// <summary>
		/// wählt beim Doppelklick den aktuellen Eintrag aus und lädt ihn
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgrid_Versenden_NichtVersandteMitteilungen_DoubleClick(object sender, System.EventArgs e)
		{
			btn_Versenden_MitteilungAuswaehlen_Click(this, null);
		}
		/// <summary>
		///  blendet das NVM-Grid aus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Versenden_SchliesseAuswahlOffeneMitteilungen_Click(object sender, System.EventArgs e)
		{
			pnl_Versenden_AuswahlOffeneMitteilungen.Visible = false;
		}

		#endregion
		#endregion

		
	
}
}
