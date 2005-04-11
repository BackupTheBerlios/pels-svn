using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pELS.Client.EK
{
	// benötigt für: PopUp
	using pELS.GUI.PopUp;
	// benötigt für: pELS-Objekte
	using pELS.DV;

	#region Dokumentation
	/**				aktuelle Version: 1.0 Schuppe
	INFO:
		- kapselt die Eingabemaske zum Erfassen/Anzeigen von Aufträgen
		
	**/
	#region Member-Doku
	/**		

	public
		- Konstruktor

		- alle graphischen Elemente können angesprochen werden - keine Kapselung
		
		- Zuruecksetzen()
			- setzt alle Eingabefelder in den initialen Zustand zurück
		
		- ZuruecksetzenMitRueckfrage()
			- fragt, wenn, nötig, ob alle Felder zurückgesetzt werden sollen
	private
		- FelderModifiziert()
			- Delegate
			- wird ausgelöst, sobald eine Eingabe stattfindet
			- damit wird vermerkt, ob bspw. ein Zurücksetzten erforderlich ist
		
	**/
	#endregion			

	#region letzte Änderungen
	/**
	erstellt von: Schuppe					am: 23.11.2004
	geändert von: Schuppe					am: 25.11.2004				
	review von:	  Hütte						am: 29.11.2004 Rücksprache mit Schuppe per Mail wegen anstehenden Änderungen
	getestet von:							am:
	**/
	#endregion

	#region History/Hinweise/Bekannte Bugs:
	/**
	History:



	Hinweise/Bekannte Bugs:
		- alle graphischen Elemente können angesprochen werden - keine Kapselung

	**/
	#endregion

	#endregion	
	public class usc_Einsatzschwerpunkte : System.Windows.Forms.UserControl
	{
		#region eigene Variablen
		/// <summary>
		/// ermöglicht das Anzeigen von fehlerhaften Eingaben
		/// </summary>
		protected System.Windows.Forms.ErrorProvider ep_Eingabe = new System.Windows.Forms.ErrorProvider();

		/// <summary>
		/// gibt an, ob bereits Eingaben geschehen sind
		/// </summary>
		protected bool _b_FelderModifiziert = false;

		protected Cdv_Einsatzschwerpunkt _esp = null;

		private Cst_EK _stEK;
		#endregion
	
		#region graphische Variablen

		public System.Windows.Forms.Button btn_Speichern;
		private System.Windows.Forms.TextBox txt_Kraefte_Kfz_Kommentar;
		private System.Windows.Forms.GroupBox gbx_Kommentar;
		private System.Windows.Forms.TabPage tabpage_Fahrer;
		private System.Windows.Forms.DataGrid dtg_EinsatzBetriebsstunden;
		private System.Windows.Forms.TabPage tabPage_EinsatzBetriebsstunden;
		private System.Windows.Forms.DataGrid dtg_Fahrer;
		private System.Windows.Forms.Button btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen;
		private System.Windows.Forms.Button btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern;
		private System.Windows.Forms.Button btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen;
		private System.Windows.Forms.Label lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter;
		private System.Windows.Forms.Label lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung;
		private System.Windows.Forms.DataGrid dgrid_Einsatzschwerpunkte_Erkundungsergebnisse;
		private System.Windows.Forms.Label lblTemp;
		private System.Windows.Forms.GroupBox gbx_Lagebeschreibung;
		private System.Windows.Forms.GroupBox gbx_Erkundungsergebnisse;
		private System.Windows.Forms.GroupBox gbx_Eingabemaske;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.Button btn_Zuruecksetzen;
		private System.Windows.Forms.TextBox txt_Lage;
		private System.Windows.Forms.ComboBox cmb_Prioritaet;
		private System.Windows.Forms.TextBox txt_ESPBezeichnung;
		public System.Windows.Forms.ComboBox cmb_Einsatzleiter;
		public System.Windows.Forms.ComboBox cmb_EinsatzBezeichnung;
		private System.Windows.Forms.Label lbl_EinsatzBezeichnung;
		private System.Windows.Forms.GroupBox gbx_EinsatzBezeichnung;
		public System.ComponentModel.Container components = null;
		#endregion

		#region Konstruktor & Destruktor
		public usc_Einsatzschwerpunkte(Cst_EK pin_stEK)
		{
			this._stEK = pin_stEK;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.InitAlleSTE();
			this._b_FelderModifiziert = false;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
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
			this.btn_Speichern = new System.Windows.Forms.Button();
			this.btn_Zuruecksetzen = new System.Windows.Forms.Button();
			this.txt_Kraefte_Kfz_Kommentar = new System.Windows.Forms.TextBox();
			this.dtg_EinsatzBetriebsstunden = new System.Windows.Forms.DataGrid();
			this.gbx_Kommentar = new System.Windows.Forms.GroupBox();
			this.tabpage_Fahrer = new System.Windows.Forms.TabPage();
			this.tabPage_EinsatzBetriebsstunden = new System.Windows.Forms.TabPage();
			this.dtg_Fahrer = new System.Windows.Forms.DataGrid();
			this.gbx_Lagebeschreibung = new System.Windows.Forms.GroupBox();
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen = new System.Windows.Forms.Button();
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern = new System.Windows.Forms.Button();
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen = new System.Windows.Forms.Button();
			this.txt_Lage = new System.Windows.Forms.TextBox();
			this.cmb_Prioritaet = new System.Windows.Forms.ComboBox();
			this.cmb_Einsatzleiter = new System.Windows.Forms.ComboBox();
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter = new System.Windows.Forms.Label();
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung = new System.Windows.Forms.Label();
			this.txt_ESPBezeichnung = new System.Windows.Forms.TextBox();
			this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse = new System.Windows.Forms.DataGrid();
			this.lblTemp = new System.Windows.Forms.Label();
			this.gbx_Erkundungsergebnisse = new System.Windows.Forms.GroupBox();
			this.gbx_Eingabemaske = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmb_EinsatzBezeichnung = new System.Windows.Forms.ComboBox();
			this.lbl_EinsatzBezeichnung = new System.Windows.Forms.Label();
			this.gbx_EinsatzBezeichnung = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.dtg_EinsatzBetriebsstunden)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtg_Fahrer)).BeginInit();
			this.gbx_Lagebeschreibung.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse)).BeginInit();
			this.gbx_Erkundungsergebnisse.SuspendLayout();
			this.gbx_Eingabemaske.SuspendLayout();
			this.gbx_EinsatzBezeichnung.SuspendLayout();
			this.SuspendLayout();
			// 
			// btn_Speichern
			// 
			this.btn_Speichern.Location = new System.Drawing.Point(536, 424);
			this.btn_Speichern.Name = "btn_Speichern";
			this.btn_Speichern.Size = new System.Drawing.Size(80, 25);
			this.btn_Speichern.TabIndex = 18;
			this.btn_Speichern.Text = "Speichern";
			this.btn_Speichern.Click += new System.EventHandler(this.btn_Speichern_Click);
			// 
			// btn_Zuruecksetzen
			// 
			this.btn_Zuruecksetzen.Location = new System.Drawing.Point(448, 424);
			this.btn_Zuruecksetzen.Name = "btn_Zuruecksetzen";
			this.btn_Zuruecksetzen.Size = new System.Drawing.Size(80, 25);
			this.btn_Zuruecksetzen.TabIndex = 19;
			this.btn_Zuruecksetzen.Text = "Zurücksetzen";
			this.btn_Zuruecksetzen.Click += new System.EventHandler(this.btn_Zuruecksetzen_Click);
			// 
			// txt_Kraefte_Kfz_Kommentar
			// 
			this.txt_Kraefte_Kfz_Kommentar.Location = new System.Drawing.Point(0, 0);
			this.txt_Kraefte_Kfz_Kommentar.Name = "txt_Kraefte_Kfz_Kommentar";
			this.txt_Kraefte_Kfz_Kommentar.TabIndex = 0;
			this.txt_Kraefte_Kfz_Kommentar.Text = "";
			this.txt_Kraefte_Kfz_Kommentar.TextChanged += new System.EventHandler(this.AuswahlModifiziert);
			// 
			// dtg_EinsatzBetriebsstunden
			// 
			this.dtg_EinsatzBetriebsstunden.DataMember = "";
			this.dtg_EinsatzBetriebsstunden.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtg_EinsatzBetriebsstunden.Location = new System.Drawing.Point(0, 0);
			this.dtg_EinsatzBetriebsstunden.Name = "dtg_EinsatzBetriebsstunden";
			this.dtg_EinsatzBetriebsstunden.TabIndex = 0;
			// 
			// gbx_Kommentar
			// 
			this.gbx_Kommentar.Location = new System.Drawing.Point(0, 0);
			this.gbx_Kommentar.Name = "gbx_Kommentar";
			this.gbx_Kommentar.TabIndex = 0;
			this.gbx_Kommentar.TabStop = false;
			// 
			// tabpage_Fahrer
			// 
			this.tabpage_Fahrer.Location = new System.Drawing.Point(0, 0);
			this.tabpage_Fahrer.Name = "tabpage_Fahrer";
			this.tabpage_Fahrer.TabIndex = 0;
			// 
			// tabPage_EinsatzBetriebsstunden
			// 
			this.tabPage_EinsatzBetriebsstunden.Location = new System.Drawing.Point(0, 0);
			this.tabPage_EinsatzBetriebsstunden.Name = "tabPage_EinsatzBetriebsstunden";
			this.tabPage_EinsatzBetriebsstunden.TabIndex = 0;
			// 
			// dtg_Fahrer
			// 
			this.dtg_Fahrer.DataMember = "";
			this.dtg_Fahrer.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtg_Fahrer.Location = new System.Drawing.Point(0, 0);
			this.dtg_Fahrer.Name = "dtg_Fahrer";
			this.dtg_Fahrer.TabIndex = 0;
			// 
			// gbx_Lagebeschreibung
			// 
			this.gbx_Lagebeschreibung.BackColor = System.Drawing.Color.White;
			this.gbx_Lagebeschreibung.Controls.Add(this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen);
			this.gbx_Lagebeschreibung.Controls.Add(this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern);
			this.gbx_Lagebeschreibung.Controls.Add(this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen);
			this.gbx_Lagebeschreibung.Controls.Add(this.txt_Lage);
			this.gbx_Lagebeschreibung.Location = new System.Drawing.Point(335, 5);
			this.gbx_Lagebeschreibung.Name = "gbx_Lagebeschreibung";
			this.gbx_Lagebeschreibung.Size = new System.Drawing.Size(275, 120);
			this.gbx_Lagebeschreibung.TabIndex = 21;
			this.gbx_Lagebeschreibung.TabStop = false;
			this.gbx_Lagebeschreibung.Text = "Lagebeschreibung/Erkundungsdetails";
			// 
			// btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen
			// 
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen.Location = new System.Drawing.Point(460, 75);
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen.Name = "btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen";
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen.Size = new System.Drawing.Size(155, 35);
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen.TabIndex = 11;
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen.Text = "Erkundungsergebnisse hinzufügen";
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen.Visible = false;
			// 
			// btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern
			// 
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern.Location = new System.Drawing.Point(460, 115);
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern.Name = "btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern";
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern.Size = new System.Drawing.Size(155, 23);
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern.TabIndex = 10;
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern.Text = "Priorität / Lage ändern";
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern.Visible = false;
			// 
			// btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen
			// 
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen.Location = new System.Drawing.Point(460, 115);
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen.Name = "btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen";
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen.Size = new System.Drawing.Size(155, 23);
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen.TabIndex = 9;
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen.Text = "Schwerpunkt anlegen";
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen.Visible = false;
			// 
			// txt_Lage
			// 
			this.txt_Lage.Location = new System.Drawing.Point(5, 15);
			this.txt_Lage.Multiline = true;
			this.txt_Lage.Name = "txt_Lage";
			this.txt_Lage.Size = new System.Drawing.Size(265, 100);
			this.txt_Lage.TabIndex = 3;
			this.txt_Lage.Text = "Lagebeschreibung";
			this.txt_Lage.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Lage.Leave += new System.EventHandler(this.txt_Einsatzschwerpunkte_Lage_Leave);
			// 
			// cmb_Prioritaet
			// 
			this.cmb_Prioritaet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Prioritaet.Items.AddRange(new object[] {
																"1",
																"2",
																"3",
																"4",
																"5",
																"6",
																"7"});
			this.cmb_Prioritaet.Location = new System.Drawing.Point(125, 50);
			this.cmb_Prioritaet.Name = "cmb_Prioritaet";
			this.cmb_Prioritaet.Size = new System.Drawing.Size(195, 21);
			this.cmb_Prioritaet.TabIndex = 8;
			this.cmb_Prioritaet.Leave += new System.EventHandler(this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet_Leave);
			this.cmb_Prioritaet.SelectionChangeCommitted += new System.EventHandler(this.FelderModifiziert);
			// 
			// cmb_Einsatzleiter
			// 
			this.cmb_Einsatzleiter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Einsatzleiter.Items.AddRange(new object[] {
																   "Kraft 1",
																   "Kraft 2",
																   "Kraft 3",
																   "Kraft 4"});
			this.cmb_Einsatzleiter.Location = new System.Drawing.Point(125, 30);
			this.cmb_Einsatzleiter.Name = "cmb_Einsatzleiter";
			this.cmb_Einsatzleiter.Size = new System.Drawing.Size(195, 21);
			this.cmb_Einsatzleiter.TabIndex = 6;
			this.cmb_Einsatzleiter.Leave += new System.EventHandler(this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter_Leave);
			this.cmb_Einsatzleiter.SelectionChangeCommitted += new System.EventHandler(this.FelderModifiziert);
			// 
			// lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter
			// 
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.Location = new System.Drawing.Point(5, 30);
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.Name = "lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter";
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.Size = new System.Drawing.Size(70, 15);
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.TabIndex = 5;
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.Text = "Einsatzleiter";
			// 
			// lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung
			// 
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung.Location = new System.Drawing.Point(5, 10);
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung.Name = "lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung";
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung.Size = new System.Drawing.Size(95, 15);
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung.TabIndex = 1;
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung.Text = "ESP Bezeichnung";
			// 
			// txt_ESPBezeichnung
			// 
			this.txt_ESPBezeichnung.Location = new System.Drawing.Point(125, 10);
			this.txt_ESPBezeichnung.Name = "txt_ESPBezeichnung";
			this.txt_ESPBezeichnung.Size = new System.Drawing.Size(195, 20);
			this.txt_ESPBezeichnung.TabIndex = 0;
			this.txt_ESPBezeichnung.Text = "";
			this.txt_ESPBezeichnung.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_ESPBezeichnung.Leave += new System.EventHandler(this.txt_Einsatzschwerpuntke_Bezeichnung_Leave);
			// 
			// dgrid_Einsatzschwerpunkte_Erkundungsergebnisse
			// 
			this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse.DataMember = "";
			this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse.Location = new System.Drawing.Point(5, 20);
			this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse.Name = "dgrid_Einsatzschwerpunkte_Erkundungsergebnisse";
			this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse.Size = new System.Drawing.Size(585, 255);
			this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse.TabIndex = 0;
			// 
			// lblTemp
			// 
			this.lblTemp.Location = new System.Drawing.Point(185, 105);
			this.lblTemp.Name = "lblTemp";
			this.lblTemp.Size = new System.Drawing.Size(255, 35);
			this.lblTemp.TabIndex = 1;
			this.lblTemp.Text = "Hier sind nachher Erkundungsergebnisse tabellarisch dargestellt";
			// 
			// gbx_Erkundungsergebnisse
			// 
			this.gbx_Erkundungsergebnisse.Controls.Add(this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse);
			this.gbx_Erkundungsergebnisse.Controls.Add(this.lblTemp);
			this.gbx_Erkundungsergebnisse.Location = new System.Drawing.Point(10, 135);
			this.gbx_Erkundungsergebnisse.Name = "gbx_Erkundungsergebnisse";
			this.gbx_Erkundungsergebnisse.Size = new System.Drawing.Size(605, 285);
			this.gbx_Erkundungsergebnisse.TabIndex = 22;
			this.gbx_Erkundungsergebnisse.TabStop = false;
			this.gbx_Erkundungsergebnisse.Text = "Erkundungsergebnisse";
			// 
			// gbx_Eingabemaske
			// 
			this.gbx_Eingabemaske.BackColor = System.Drawing.Color.White;
			this.gbx_Eingabemaske.Controls.Add(this.label1);
			this.gbx_Eingabemaske.Controls.Add(this.cmb_Einsatzleiter);
			this.gbx_Eingabemaske.Controls.Add(this.txt_ESPBezeichnung);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter);
			this.gbx_Eingabemaske.Controls.Add(this.cmb_Prioritaet);
			this.gbx_Eingabemaske.Location = new System.Drawing.Point(5, 50);
			this.gbx_Eingabemaske.Name = "gbx_Eingabemaske";
			this.gbx_Eingabemaske.Size = new System.Drawing.Size(330, 75);
			this.gbx_Eingabemaske.TabIndex = 24;
			this.gbx_Eingabemaske.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5, 55);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 15);
			this.label1.TabIndex = 24;
			this.label1.Text = "Priorität:";
			// 
			// cmb_EinsatzBezeichnung
			// 
			this.cmb_EinsatzBezeichnung.Location = new System.Drawing.Point(125, 15);
			this.cmb_EinsatzBezeichnung.Name = "cmb_EinsatzBezeichnung";
			this.cmb_EinsatzBezeichnung.Size = new System.Drawing.Size(195, 21);
			this.cmb_EinsatzBezeichnung.TabIndex = 25;
			this.cmb_EinsatzBezeichnung.SelectedIndexChanged += new System.EventHandler(this.AuswahlModifiziert);
			// 
			// lbl_EinsatzBezeichnung
			// 
			this.lbl_EinsatzBezeichnung.Location = new System.Drawing.Point(5, 15);
			this.lbl_EinsatzBezeichnung.Name = "lbl_EinsatzBezeichnung";
			this.lbl_EinsatzBezeichnung.Size = new System.Drawing.Size(115, 20);
			this.lbl_EinsatzBezeichnung.TabIndex = 26;
			this.lbl_EinsatzBezeichnung.Text = "Einsatz Bezeichnung";
			// 
			// gbx_EinsatzBezeichnung
			// 
			this.gbx_EinsatzBezeichnung.BackColor = System.Drawing.Color.White;
			this.gbx_EinsatzBezeichnung.Controls.Add(this.lbl_EinsatzBezeichnung);
			this.gbx_EinsatzBezeichnung.Controls.Add(this.cmb_EinsatzBezeichnung);
			this.gbx_EinsatzBezeichnung.Location = new System.Drawing.Point(5, 5);
			this.gbx_EinsatzBezeichnung.Name = "gbx_EinsatzBezeichnung";
			this.gbx_EinsatzBezeichnung.Size = new System.Drawing.Size(330, 45);
			this.gbx_EinsatzBezeichnung.TabIndex = 27;
			this.gbx_EinsatzBezeichnung.TabStop = false;
			// 
			// usc_Einsatzschwerpunkte
			// 
			this.Controls.Add(this.gbx_EinsatzBezeichnung);
			this.Controls.Add(this.gbx_Eingabemaske);
			this.Controls.Add(this.gbx_Erkundungsergebnisse);
			this.Controls.Add(this.btn_Zuruecksetzen);
			this.Controls.Add(this.btn_Speichern);
			this.Controls.Add(this.gbx_Lagebeschreibung);
			this.Location = new System.Drawing.Point(6, 21);
			this.Name = "usc_Einsatzschwerpunkte";
			this.Size = new System.Drawing.Size(624, 456);
			((System.ComponentModel.ISupportInitialize)(this.dtg_EinsatzBetriebsstunden)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtg_Fahrer)).EndInit();
			this.gbx_Lagebeschreibung.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse)).EndInit();
			this.gbx_Erkundungsergebnisse.ResumeLayout(false);
			this.gbx_Eingabemaske.ResumeLayout(false);
			this.gbx_EinsatzBezeichnung.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void dtg_EinheitenHelferKFZ_Navigate(object sender, System.Windows.Forms.NavigateEventArgs ne)
		{
		
		}

		#endregion

		#region Setze- Methode
		public void InitAlleSTE()
		{
			this.SetzeEingabefelderModi(true);
			this.FuelleEinsatzscherpunkteBezeichnung();
			this.FuelleEinsatzschwerpunktEinsatzleiter();
		}
		// Die Eingabemaske hat zwei Modi, zum einen sind
		// alle Felder aktiv zum Datenspeichern, zum anderen
		// wird die Enable-Eigenschaft teilweiser Eingabefelder 
		// mit false zu setzen, dies hat den Zweck zum Modifizieren
		// der Daten. Falls pin_Modifizieren true ist, wird die
		// Eingabemaske mit Modifzierenmodi gesetzt.
		private void SetzeEingabefelderModi(bool pin_Modifizieren)
		{
			this.gbx_EinsatzBezeichnung.Enabled = pin_Modifizieren;
			this.gbx_Eingabemaske.Enabled = !pin_Modifizieren;
			this.gbx_Lagebeschreibung.Enabled = !pin_Modifizieren;
			this.gbx_Erkundungsergebnisse.Enabled = !pin_Modifizieren;
		}
	
		private void SetzePrioritaet()
		{
		
		}

		private void Zuruecksetzen()
		{
			this.cmb_EinsatzBezeichnung.SelectedItem = null;
			this.ep_Eingabe.SetError(this.txt_ESPBezeichnung, "");
			this.txt_ESPBezeichnung.Text = "";
			this.ep_Eingabe.SetError(this.txt_Lage, "");
			this.txt_Lage.Text = "";
			this.ep_Eingabe.SetError(this.cmb_Einsatzleiter, "");
			this.cmb_Einsatzleiter.SelectedItem = null;
			this.ep_Eingabe.SetError(this.cmb_Prioritaet, "");
			this.cmb_Prioritaet.SelectedItem = null;
			this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse.DataSource = null;

			this.SetzeEingabefelderModi(true);
			this._b_FelderModifiziert = false;
		}

		private void ZuruecksetzenMitRueckfrage()
		{
				if(CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes)
					this.Zuruecksetzen();
		}

		#endregion

		#region Laden (ESP, Erkundungsergebnisse)

		public void FuelleEinsatzscherpunkteBezeichnung()
		{
			Cdv_Einsatzschwerpunkt[] espMenge = this._stEK.AlleEinsatzschwerpunkte;
			IEnumerator ie = espMenge.GetEnumerator();
			this.cmb_EinsatzBezeichnung.Items.Clear();
			this.cmb_EinsatzBezeichnung.Items.Add("<neuer Einsatzschwerpunkt>");
			while(ie.MoveNext())
			{
				Cdv_Einsatzschwerpunkt esp = (Cdv_Einsatzschwerpunkt) ie.Current;
				this.cmb_EinsatzBezeichnung.Items.Add(esp);
			}
		}

		public void FuelleEinsatzschwerpunktEinsatzleiter()
		{
			IEnumerator ie = this._stEK.AlleHelfer.GetEnumerator();
			this.cmb_Einsatzleiter.Items.Clear();
			while(ie.MoveNext())
			{
				Cdv_Helfer helfer = (Cdv_Helfer) ie.Current;
				this.cmb_Einsatzleiter.Items.Add(helfer);
			}
		}

		private DataTable ErstelleESPDataTable()
		{
			DataColumn[] dcol_erg =
					{
						Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_erg_ID", "ID", "System.String"),
						Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_erg_Erkunder", "Erkunder", "System.String"),
						Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_erg_Text", "Text", "System.String"),
						Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_erg_Uebermittlungsart", "Übermittlungsart", "System.String"),
						Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_erg_Uebermittlungsdatum", "Übermittlungsdatum", "System.String"),
						Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_erg_IstUbermittelt", "Ist übermittelt", "System.String"),
						Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_erg_Kategorie", "Kategorie", "System.String"),
						Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_erg_Erkundungsobjekt", "Erkundungsobjekt", "System.String"),
						Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_erg_Abfassungsdatum", "Abfassungsdatum", "System.String"),
						Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_erg_Absender", "Absender", "System.String")
					};
			DataTable dtable_Erg = Cpr_EK_AllgFkt.ErstellenEinerDataTable("dtable_Ereg", dcol_erg);
			return(dtable_Erg);
		}

		private void LadeAlleErkundungsergebnisse()
		{
			Cdv_Erkundungsergebnis[] ergMenge = this._stEK.HoleAlleErkundungsergebnisse();
			if(ergMenge != null)
			{
				DataTable dtable_erk = this.ErstelleESPDataTable();
				IEnumerator ie = ergMenge.GetEnumerator();
				while(ie.MoveNext())
				{
					Cdv_Erkundungsergebnis e = (Cdv_Erkundungsergebnis) ie.Current;
					object[] obj_Row = new object[]
					{
						e.ID,
						e.Erkunder,
						e.Text,
						e.Uebermittlungsart.ToString(),
						e.Uebermittlungsdatum.ToString(),
						e.IstUebermittelt.ToString(),
						e.Kategorie.ToString(),
						e.Erkundungsobjekt.Bezeichnung,
						e.Abfassungsdatum.ToString(),
						e.Absender
					};
					dtable_erk.Rows.Add(obj_Row);					
				}
				this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse.DataSource = dtable_erk;
			}
		}

		private void LadeErkundungsergebnisseZuESP(Cdv_Einsatzschwerpunkt pin_esp)
		{
			Cdv_Erkundungsergebnis[] erg = this._stEK.HoleErkundungsergebnisseZuESP(pin_esp.ID);
			if(erg.Length > 0)
			{
				DataTable dtable_Erg = this.ErstelleESPDataTable();
				IEnumerator ie = erg.GetEnumerator();
				while(ie.MoveNext())
				{
					Cdv_Erkundungsergebnis e = (Cdv_Erkundungsergebnis) ie.Current;
					object[] obj_Row = new object[]
					{
						e.ID,
						e.Erkunder,
						e.Text,
						e.Uebermittlungsart.ToString(),
						e.Uebermittlungsdatum.ToString(),
						e.IstUebermittelt.ToString(),
						e.Kategorie.ToString(),
						e.Erkundungsobjekt.Bezeichnung,
						e.Abfassungsdatum.ToString(),
						e.Absender
					};
					dtable_Erg.Rows.Add(obj_Row);
				}
				this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse.DataSource = dtable_Erg;
			}
		}

		public void LadeESP(Cdv_Einsatzschwerpunkt pin_esp)
		{
			this.SetzeEingabefelderModi(false);
			if((this.cmb_EinsatzBezeichnung.Text == "") ||
				(this.cmb_EinsatzBezeichnung.Text != pin_esp.Bezeichnung))
			{
				this.cmb_EinsatzBezeichnung.Text = pin_esp.Bezeichnung;
				//this.cmb_EinsatzBezeichnung.SelectedItem = pin_esp;
			}
			this.txt_ESPBezeichnung.Text = pin_esp.Bezeichnung;
			this.txt_Lage.Text = pin_esp.Lage.Text;
			this.cmb_Prioritaet.Text = pin_esp.Prioritaet.ToString();
			foreach(object ESPLeiter in this.cmb_Einsatzleiter.Items)
			{
				if (ESPLeiter is Cdv_Helfer)
				{
					if ((ESPLeiter as Cdv_Helfer).ID == pin_esp.EinsatzleiterHelferID)
					{
						this.cmb_Einsatzleiter.SelectedItem=ESPLeiter;
						break;
					}
				}

			
			}
			this.LadeErkundungsergebnisseZuESP(pin_esp);
			this._esp = pin_esp;
			this._b_FelderModifiziert = false;
		}
		#endregion

		#region Eingabevalidierung ESP
		
		private bool EingabevalidierungESP()
		{
			if(this.ValidiereBezeichnungESP() && this.ValidiereLageESP() && this.ValidierePrioESP() && this.ValidiereEinsatzleiterESP())
				return(true);
			this.txt_Bezeichnung_Validated_ESP(null, null);
			this.txt_Lage_Validated_ESP(null, null);
			this.cmb_Prio_Validated_ESP(null, null);
			this.cmb_Einsatzleiter_Validated_ESP(null, null);
			return(false);
		}

		private bool ValidiereEinsatzleiterESP()
		{
			return(this.cmb_Einsatzleiter.SelectedItem is Cdv_Helfer);
		}

		private void cmb_Einsatzleiter_Validated_ESP(object sender, System.EventArgs e)
		{
			if(ValidiereEinsatzleiterESP())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.cmb_Einsatzleiter, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.cmb_Einsatzleiter, "Bitte wählen Sie einen Einsatzleiter aus");
			}
		}		

		private bool ValidierePrioESP()
		{
			int iPrio;
			try
			{
				iPrio = Int32.Parse(this.cmb_Prioritaet.Text);
			}
			catch
			{
				return(false);
			}
			if(iPrio > 0 && iPrio < 7)
				return(true);
			return(false);
		}

		private bool ValidiereLageESP()
		{
			return(this.txt_Lage.Text.Length > 0);
		}

		private bool ValidiereBezeichnungESP()
		{
			return(this.txt_ESPBezeichnung.Text.Length > 0);
		}

		private void cmb_Prio_Validated_ESP(object sender, System.EventArgs e)
		{
			if(ValidierePrioESP())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.cmb_Prioritaet, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.cmb_Prioritaet, "Bitte geben Sie eine Priorität ein");
			}
		}

		private void txt_Bezeichnung_Validated_ESP(object sender, System.EventArgs e)
		{
			if(ValidiereBezeichnungESP())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_ESPBezeichnung, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_ESPBezeichnung, "Bitte geben Sie eine Bezeichnung ein");
			}
		}

		private void txt_Lage_Validated_ESP(object sender, System.EventArgs e)
		{
			if(ValidiereLageESP())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_Lage, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_Lage, "Bitte geben Sie eine Lage ein");
			}
		}

		#endregion

		#region event handler
		/// <summary>
		/// event, welches bei allen Eingabeelementen registriert ist
		/// und eine vorgenommene Änderung registriert
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AuswahlModifiziert(object sender, System.EventArgs e)
		{
			this.SetzeEingabefelderModi(false);
			if (this.cmb_EinsatzBezeichnung.Text != "<neuer Einsatzschwerpunkt>"
				&& (this.cmb_EinsatzBezeichnung.Text != ""))
			{
				this.LadeESP((Cdv_Einsatzschwerpunkt) this.cmb_EinsatzBezeichnung.SelectedItem);
			}
			else
				if(this.cmb_EinsatzBezeichnung.Text != "")
					this.LadeAlleErkundungsergebnisse();
			_b_FelderModifiziert = true;
		}

		private void FelderModifiziert(object sender, System.EventArgs e)
		{
			this._b_FelderModifiziert = true;
		}

		private void btn_Zuruecksetzen_Click(object sender, System.EventArgs e)
		{
			this.ZuruecksetzenMitRueckfrage();
		}
	
		#region  Eingabevalidierung

		private void txt_Einsatzschwerpuntke_Bezeichnung_Leave(object sender, System.EventArgs e)
		{
			this.txt_Bezeichnung_Validated_ESP(null,null);
		}

		private void txt_Einsatzschwerpunkte_Lage_Leave(object sender, System.EventArgs e)
		{
			this.txt_Lage_Validated_ESP(null, null);
		}

		private void cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet_Leave(object sender, System.EventArgs e)
		{
			this.cmb_Prio_Validated_ESP(null, null);
		}

		private void cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter_Leave(object sender, System.EventArgs e)
		{
			this.cmb_Einsatzleiter_Validated_ESP(null, null);
		}

		#endregion

		private void btn_Speichern_Click(object sender, System.EventArgs e)
		{
			// Unterscheidung ob neuer Einsatzschwerpunkt oder schon vorhandener
			bool bNeuerESP = false;
			DialogResult _dr_speichern = new DialogResult();
			if (this.cmb_EinsatzBezeichnung.Text == "<neuer Einsatzschwerpunkt>")
			{
				bNeuerESP = true;
				_dr_speichern = CPopUp.SpeichernOhneUeberschreiben();
			}
			else _dr_speichern = CPopUp.SpeichernMitUeberschreiben();
			
			if ( _dr_speichern == DialogResult.OK)
			{
				if(!this.EingabevalidierungESP())
					return;
				Cdv_Einsatzschwerpunkt esp = null;
				if(bNeuerESP)
					esp = new Cdv_Einsatzschwerpunkt();
				else
				{
					esp = this._esp;
					//esp = (Cdv_Einsatzschwerpunkt) this.cmb_EinsatzBezeichnung.SelectedItem;
				}
				esp.Bezeichnung = this.txt_ESPBezeichnung.Text;
				esp.EinsatzleiterHelferID = ((Cdv_Helfer)this.cmb_Einsatzleiter.SelectedItem).ID;
				esp.Lage.Text = this.txt_Lage.Text;
				esp.Lage.Autor = this._stEK.Einstellung.Benutzer.Benutzername;
				esp.Prioritaet = Int32.Parse(this.cmb_Prioritaet.Text);
				this.SpeichereESP(esp, bNeuerESP);
				this.Zuruecksetzen();
				this._esp = null;
				this.cmb_EinsatzBezeichnung.Text = "";
			}
		}

		#endregion

		#region Speichern/Erstellen (ESP)
		public void SpeichereESP(Cdv_Einsatzschwerpunkt pin_esp, bool bIstNeu)
		{
			if(bIstNeu)
				pin_esp.EinsatzID = this._stEK.Einsatz.ID;
			this._stEK.SpeichereESP(pin_esp);
			this.FuelleEinsatzscherpunkteBezeichnung();
		}

		public void ErstelleESP(Cdv_Einsatzschwerpunkt pin_esp)
		{
//			pin_esp.EinsatzID = this._Einsatz.ID;
//			pin_esp.Bezeichnung = "Neuer ESP";
//			this._PortalLogikEK.SpeichereESP(pin_esp);
		}
		#endregion

		#region get-Methoden
		public bool FelderIstModifiziert
		{
			get{return this._b_FelderModifiziert;}
		}
		#endregion

		#region Dynamische Datenanpassung

		public void AktualisiereLeiter()
		{
			this.FuelleEinsatzschwerpunktEinsatzleiter();
		}

		#endregion

	}
}