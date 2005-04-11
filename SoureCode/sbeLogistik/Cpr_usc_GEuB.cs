using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pELS.Client.Logistik
{
	// benötigt für: pELS-Objekte
	using pELS.DV;
	using pELS.Tools;


	/// <summary>
	/// Summary description for Cpr_usc_GEuB.
	/// </summary>
	public class Cpr_usc_GEuB : System.Windows.Forms.UserControl
	{
		#region graphische Variablen
		private System.Windows.Forms.GroupBox grp_Eingabegruppe;
		private System.Windows.Forms.GroupBox gbx_Guttyp;
		private System.Windows.Forms.RadioButton rBtn_Material;
		private System.Windows.Forms.RadioButton rBtn_Verbrauchsgut;
		private System.Windows.Forms.Label lbl_Bezeichnung;
		private System.Windows.Forms.TextBox txt_Lagerort;
		private System.Windows.Forms.Label lbl_Lagerort;
		private System.Windows.Forms.TextBox txt_Art;
		private System.Windows.Forms.Label lbl_Art;
		private System.Windows.Forms.Label lbl_Menge;
		private System.Windows.Forms.TextBox txt_Bezeichnung;
		private System.Windows.Forms.Button btn_NeuErstellen;
		private System.Windows.Forms.Button btn_Zuruecksetzen;
		private System.Windows.Forms.Button btn_Speichern;
		private System.Windows.Forms.GroupBox grp_NeuErstellen_Guetervorratslistegruppe;
		private System.Windows.Forms.DataGrid dtg_Listegruppe_Guetervorratsliste;
		private System.Windows.Forms.DateTimePicker dtp_Wiederbeschaffung;
		private System.Windows.Forms.Label lbl_Wiederbeschaffung;
		private System.Windows.Forms.Label lbl_Besitzer;
		private System.Windows.Forms.Label lbl_Eigentuemer;
		private System.Windows.Forms.ErrorProvider ep_Eingabe;
		private pELS.Tools.MaskedTextBox txt_Menge;
		private System.Windows.Forms.CheckBox cbx_Edit;
		public System.Windows.Forms.TreeView tvw_Besitzer;
		public System.Windows.Forms.TreeView tvw_Eigentuemer;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region funktionale Variablen
		
		/// <summary>
		/// Referenz auf das entsprechende Element der Steuerungsschicht
		/// </summary>
		private Cst_Logistik _st_Logistik;
		/// <summary>
		///  Referenz auf das Oberobjekt der Präsentationsschicht
		/// </summary>
		private Cpr_usc_Logistik _usc_Logistik;

		/// <summary>
		/// gibt an, ob bereits Eingaben geschehen sind
		/// </summary>
		private bool _b_FelderModifiziert = false;

		/// <summary>
		/// gibt an, ob Werte in der Eingabemaske nur gelesen werden dürfen
		/// </summary>
		private bool __NurLesen = true;
		private bool _NurLesen
		{
			get { return __NurLesen;}
			set
			{
				__NurLesen = value;
				// setze auf Nur-Lesen-Zugriff
				if (__NurLesen)
				{
					txt_Art.ReadOnly = true;
					txt_Bezeichnung.ReadOnly = true;
					txt_Menge.ReadOnly = true;
					txt_Lagerort.ReadOnly = true;
					dtp_Wiederbeschaffung.Enabled = false;
				}
					// setze auf Schreiben-Zugriff
				else
				{
					txt_Art.ReadOnly = false;
					txt_Bezeichnung.ReadOnly = false;
					txt_Menge.ReadOnly = false;
					txt_Lagerort.ReadOnly = false;
					dtp_Wiederbeschaffung.Enabled = true;
				}
			}
		}

		/// <summary>
		/// Knoten im Treeview welcher den aktuellen Besitzer angibt
		/// </summary>
		private TreeNode _tn_Besitzer;
		/// <summary>
		/// Knoten im Treeview welcher den aktuellen Eigentümer angibt
		/// </summary>
		private TreeNode _tn_Eigentuemer;

		/// <summary>
		/// ID des aktuell im Datagrid ausgewählten Guts
		/// </summary>
		private int _aktuelleGutID;
		/// <summary>
		/// Tabelle, welche alle vorhandenen Güter anzeigt
		/// </summary>
		private DataTable dtable_Gueter;

		#endregion

		#region Konstruktor & Destruktor
		public Cpr_usc_GEuB(Cst_Logistik pin_cst_Logistik, Cpr_usc_Logistik pin_usc_Logistik)
		{
			_st_Logistik = pin_cst_Logistik;
			_usc_Logistik = pin_usc_Logistik;

			
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			InitialisiereGrid();

			// lade alle möglichen Besitzer
			_usc_Logistik.SetzeTreeViewKraft(tvw_Besitzer);
			// lade alle möglichen Eigentümer
			_usc_Logistik.SetzeTreeViewKraft(tvw_Eigentuemer);
			// wähle standardmäßig Material als Eingabe
			rBtn_Material.Checked = true;
			__NurLesen = false;
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
			this.grp_Eingabegruppe = new System.Windows.Forms.GroupBox();
			this.txt_Menge = new pELS.Tools.MaskedTextBox();
			this.lbl_Bezeichnung = new System.Windows.Forms.Label();
			this.txt_Lagerort = new System.Windows.Forms.TextBox();
			this.lbl_Lagerort = new System.Windows.Forms.Label();
			this.txt_Art = new System.Windows.Forms.TextBox();
			this.lbl_Art = new System.Windows.Forms.Label();
			this.lbl_Menge = new System.Windows.Forms.Label();
			this.txt_Bezeichnung = new System.Windows.Forms.TextBox();
			this.dtp_Wiederbeschaffung = new System.Windows.Forms.DateTimePicker();
			this.lbl_Wiederbeschaffung = new System.Windows.Forms.Label();
			this.lbl_Besitzer = new System.Windows.Forms.Label();
			this.lbl_Eigentuemer = new System.Windows.Forms.Label();
			this.tvw_Eigentuemer = new System.Windows.Forms.TreeView();
			this.tvw_Besitzer = new System.Windows.Forms.TreeView();
			this.gbx_Guttyp = new System.Windows.Forms.GroupBox();
			this.rBtn_Material = new System.Windows.Forms.RadioButton();
			this.rBtn_Verbrauchsgut = new System.Windows.Forms.RadioButton();
			this.btn_NeuErstellen = new System.Windows.Forms.Button();
			this.btn_Zuruecksetzen = new System.Windows.Forms.Button();
			this.btn_Speichern = new System.Windows.Forms.Button();
			this.grp_NeuErstellen_Guetervorratslistegruppe = new System.Windows.Forms.GroupBox();
			this.dtg_Listegruppe_Guetervorratsliste = new System.Windows.Forms.DataGrid();
			this.cbx_Edit = new System.Windows.Forms.CheckBox();
			this.ep_Eingabe = new System.Windows.Forms.ErrorProvider();
			this.grp_Eingabegruppe.SuspendLayout();
			this.gbx_Guttyp.SuspendLayout();
			this.grp_NeuErstellen_Guetervorratslistegruppe.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtg_Listegruppe_Guetervorratsliste)).BeginInit();
			this.SuspendLayout();
			// 
			// grp_Eingabegruppe
			// 
			this.grp_Eingabegruppe.Controls.Add(this.txt_Menge);
			this.grp_Eingabegruppe.Controls.Add(this.lbl_Bezeichnung);
			this.grp_Eingabegruppe.Controls.Add(this.txt_Lagerort);
			this.grp_Eingabegruppe.Controls.Add(this.lbl_Lagerort);
			this.grp_Eingabegruppe.Controls.Add(this.txt_Art);
			this.grp_Eingabegruppe.Controls.Add(this.lbl_Art);
			this.grp_Eingabegruppe.Controls.Add(this.lbl_Menge);
			this.grp_Eingabegruppe.Controls.Add(this.txt_Bezeichnung);
			this.grp_Eingabegruppe.Controls.Add(this.dtp_Wiederbeschaffung);
			this.grp_Eingabegruppe.Controls.Add(this.lbl_Wiederbeschaffung);
			this.grp_Eingabegruppe.Controls.Add(this.lbl_Besitzer);
			this.grp_Eingabegruppe.Controls.Add(this.lbl_Eigentuemer);
			this.grp_Eingabegruppe.Controls.Add(this.tvw_Eigentuemer);
			this.grp_Eingabegruppe.Controls.Add(this.tvw_Besitzer);
			this.grp_Eingabegruppe.Dock = System.Windows.Forms.DockStyle.Top;
			this.grp_Eingabegruppe.Location = new System.Drawing.Point(0, 44);
			this.grp_Eingabegruppe.Name = "grp_Eingabegruppe";
			this.grp_Eingabegruppe.Size = new System.Drawing.Size(642, 156);
			this.grp_Eingabegruppe.TabIndex = 1;
			this.grp_Eingabegruppe.TabStop = false;
			// 
			// txt_Menge
			// 
			this.txt_Menge.Location = new System.Drawing.Point(92, 92);
			this.txt_Menge.Name = "txt_Menge";
			this.txt_Menge.OnlyDecimal = true;
			this.txt_Menge.OnlyDigit = true;
			this.txt_Menge.OnlyIPAddr = false;
			this.txt_Menge.Size = new System.Drawing.Size(152, 20);
			this.txt_Menge.TabIndex = 3;
			this.txt_Menge.Text = "";
			this.txt_Menge.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// lbl_Bezeichnung
			// 
			this.lbl_Bezeichnung.Location = new System.Drawing.Point(12, 36);
			this.lbl_Bezeichnung.Name = "lbl_Bezeichnung";
			this.lbl_Bezeichnung.Size = new System.Drawing.Size(75, 15);
			this.lbl_Bezeichnung.TabIndex = 0;
			this.lbl_Bezeichnung.Text = "Bezeichnung:";
			// 
			// txt_Lagerort
			// 
			this.txt_Lagerort.Location = new System.Drawing.Point(92, 120);
			this.txt_Lagerort.Name = "txt_Lagerort";
			this.txt_Lagerort.Size = new System.Drawing.Size(150, 20);
			this.txt_Lagerort.TabIndex = 4;
			this.txt_Lagerort.Text = "";
			this.txt_Lagerort.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// lbl_Lagerort
			// 
			this.lbl_Lagerort.Location = new System.Drawing.Point(12, 124);
			this.lbl_Lagerort.Name = "lbl_Lagerort";
			this.lbl_Lagerort.Size = new System.Drawing.Size(60, 15);
			this.lbl_Lagerort.TabIndex = 6;
			this.lbl_Lagerort.Text = "Lagerort:";
			// 
			// txt_Art
			// 
			this.txt_Art.Location = new System.Drawing.Point(92, 60);
			this.txt_Art.Name = "txt_Art";
			this.txt_Art.Size = new System.Drawing.Size(150, 20);
			this.txt_Art.TabIndex = 2;
			this.txt_Art.Text = "";
			this.txt_Art.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// lbl_Art
			// 
			this.lbl_Art.Location = new System.Drawing.Point(12, 60);
			this.lbl_Art.Name = "lbl_Art";
			this.lbl_Art.Size = new System.Drawing.Size(40, 15);
			this.lbl_Art.TabIndex = 4;
			this.lbl_Art.Text = "Art:";
			// 
			// lbl_Menge
			// 
			this.lbl_Menge.Location = new System.Drawing.Point(12, 96);
			this.lbl_Menge.Name = "lbl_Menge";
			this.lbl_Menge.Size = new System.Drawing.Size(60, 15);
			this.lbl_Menge.TabIndex = 2;
			this.lbl_Menge.Text = "Menge:";
			// 
			// txt_Bezeichnung
			// 
			this.txt_Bezeichnung.Location = new System.Drawing.Point(92, 32);
			this.txt_Bezeichnung.Name = "txt_Bezeichnung";
			this.txt_Bezeichnung.Size = new System.Drawing.Size(150, 20);
			this.txt_Bezeichnung.TabIndex = 1;
			this.txt_Bezeichnung.Text = "";
			this.txt_Bezeichnung.Validated += new System.EventHandler(this.txt_Bezeichnung_Validated);
			this.txt_Bezeichnung.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// dtp_Wiederbeschaffung
			// 
			this.dtp_Wiederbeschaffung.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_Wiederbeschaffung.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Wiederbeschaffung.Location = new System.Drawing.Point(420, 68);
			this.dtp_Wiederbeschaffung.Name = "dtp_Wiederbeschaffung";
			this.dtp_Wiederbeschaffung.Size = new System.Drawing.Size(124, 20);
			this.dtp_Wiederbeschaffung.TabIndex = 7;
			this.dtp_Wiederbeschaffung.Visible = false;
			this.dtp_Wiederbeschaffung.ValueChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// lbl_Wiederbeschaffung
			// 
			this.lbl_Wiederbeschaffung.Location = new System.Drawing.Point(256, 64);
			this.lbl_Wiederbeschaffung.Name = "lbl_Wiederbeschaffung";
			this.lbl_Wiederbeschaffung.Size = new System.Drawing.Size(160, 28);
			this.lbl_Wiederbeschaffung.TabIndex = 34;
			this.lbl_Wiederbeschaffung.Text = "spätester Wiederbeschaffungszeitpunkt";
			this.lbl_Wiederbeschaffung.Visible = false;
			// 
			// lbl_Besitzer
			// 
			this.lbl_Besitzer.Location = new System.Drawing.Point(456, 12);
			this.lbl_Besitzer.Name = "lbl_Besitzer";
			this.lbl_Besitzer.Size = new System.Drawing.Size(100, 16);
			this.lbl_Besitzer.TabIndex = 32;
			this.lbl_Besitzer.Text = "aktueller Besitzer";
			// 
			// lbl_Eigentuemer
			// 
			this.lbl_Eigentuemer.Location = new System.Drawing.Point(268, 12);
			this.lbl_Eigentuemer.Name = "lbl_Eigentuemer";
			this.lbl_Eigentuemer.Size = new System.Drawing.Size(100, 16);
			this.lbl_Eigentuemer.TabIndex = 33;
			this.lbl_Eigentuemer.Text = "Eigentümer";
			// 
			// tvw_Eigentuemer
			// 
			this.tvw_Eigentuemer.CheckBoxes = true;
			this.tvw_Eigentuemer.ImageIndex = -1;
			this.tvw_Eigentuemer.Location = new System.Drawing.Point(264, 28);
			this.tvw_Eigentuemer.Name = "tvw_Eigentuemer";
			this.tvw_Eigentuemer.SelectedImageIndex = -1;
			this.tvw_Eigentuemer.Size = new System.Drawing.Size(176, 116);
			this.tvw_Eigentuemer.TabIndex = 6;
			this.tvw_Eigentuemer.Validated += new System.EventHandler(this.tvw_Eigentuemer_Validated);
			this.tvw_Eigentuemer.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvw_Eigentuemer_BeforeCheck);
			// 
			// tvw_Besitzer
			// 
			this.tvw_Besitzer.CheckBoxes = true;
			this.tvw_Besitzer.ImageIndex = -1;
			this.tvw_Besitzer.Location = new System.Drawing.Point(456, 28);
			this.tvw_Besitzer.Name = "tvw_Besitzer";
			this.tvw_Besitzer.SelectedImageIndex = -1;
			this.tvw_Besitzer.Size = new System.Drawing.Size(176, 116);
			this.tvw_Besitzer.TabIndex = 5;
			this.tvw_Besitzer.Validated += new System.EventHandler(this.tvw_Besitzer_Validated);
			this.tvw_Besitzer.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvw_Besitzer_BeforeCheck);
			// 
			// gbx_Guttyp
			// 
			this.gbx_Guttyp.Controls.Add(this.rBtn_Material);
			this.gbx_Guttyp.Controls.Add(this.rBtn_Verbrauchsgut);
			this.gbx_Guttyp.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbx_Guttyp.Location = new System.Drawing.Point(0, 0);
			this.gbx_Guttyp.Name = "gbx_Guttyp";
			this.gbx_Guttyp.Size = new System.Drawing.Size(642, 44);
			this.gbx_Guttyp.TabIndex = 0;
			this.gbx_Guttyp.TabStop = false;
			this.gbx_Guttyp.Text = "Typ";
			// 
			// rBtn_Material
			// 
			this.rBtn_Material.Location = new System.Drawing.Point(132, 16);
			this.rBtn_Material.Name = "rBtn_Material";
			this.rBtn_Material.Size = new System.Drawing.Size(70, 24);
			this.rBtn_Material.TabIndex = 2;
			this.rBtn_Material.Text = "Material";
			this.rBtn_Material.CheckedChanged += new System.EventHandler(this.rBtn_Gutart_CheckedChanged);
			// 
			// rBtn_Verbrauchsgut
			// 
			this.rBtn_Verbrauchsgut.Checked = true;
			this.rBtn_Verbrauchsgut.Location = new System.Drawing.Point(5, 16);
			this.rBtn_Verbrauchsgut.Name = "rBtn_Verbrauchsgut";
			this.rBtn_Verbrauchsgut.Size = new System.Drawing.Size(100, 24);
			this.rBtn_Verbrauchsgut.TabIndex = 1;
			this.rBtn_Verbrauchsgut.TabStop = true;
			this.rBtn_Verbrauchsgut.Text = "Verbrauchsgut";
			this.rBtn_Verbrauchsgut.CheckedChanged += new System.EventHandler(this.rBtn_Gutart_CheckedChanged);
			// 
			// btn_NeuErstellen
			// 
			this.btn_NeuErstellen.Location = new System.Drawing.Point(4, 204);
			this.btn_NeuErstellen.Name = "btn_NeuErstellen";
			this.btn_NeuErstellen.Size = new System.Drawing.Size(95, 25);
			this.btn_NeuErstellen.TabIndex = 4;
			this.btn_NeuErstellen.Text = "&Neu Erstellen";
			this.btn_NeuErstellen.Click += new System.EventHandler(this.btn_NeuErstellen_Click);
			// 
			// btn_Zuruecksetzen
			// 
			this.btn_Zuruecksetzen.Location = new System.Drawing.Point(468, 204);
			this.btn_Zuruecksetzen.Name = "btn_Zuruecksetzen";
			this.btn_Zuruecksetzen.Size = new System.Drawing.Size(81, 25);
			this.btn_Zuruecksetzen.TabIndex = 3;
			this.btn_Zuruecksetzen.Text = "&Zurücksetzen";
			this.btn_Zuruecksetzen.Click += new System.EventHandler(this.btn_Zuruecksetzen_Click);
			// 
			// btn_Speichern
			// 
			this.ep_Eingabe.SetIconPadding(this.btn_Speichern, 3);
			this.btn_Speichern.Location = new System.Drawing.Point(556, 204);
			this.btn_Speichern.Name = "btn_Speichern";
			this.btn_Speichern.Size = new System.Drawing.Size(80, 25);
			this.btn_Speichern.TabIndex = 2;
			this.btn_Speichern.Text = "&Speichern";
			this.btn_Speichern.Click += new System.EventHandler(this.btn_Speichern_Click);
			// 
			// grp_NeuErstellen_Guetervorratslistegruppe
			// 
			this.grp_NeuErstellen_Guetervorratslistegruppe.Controls.Add(this.dtg_Listegruppe_Guetervorratsliste);
			this.grp_NeuErstellen_Guetervorratslistegruppe.Controls.Add(this.cbx_Edit);
			this.grp_NeuErstellen_Guetervorratslistegruppe.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.grp_NeuErstellen_Guetervorratslistegruppe.Location = new System.Drawing.Point(0, 232);
			this.grp_NeuErstellen_Guetervorratslistegruppe.Name = "grp_NeuErstellen_Guetervorratslistegruppe";
			this.grp_NeuErstellen_Guetervorratslistegruppe.Size = new System.Drawing.Size(642, 272);
			this.grp_NeuErstellen_Guetervorratslistegruppe.TabIndex = 5;
			this.grp_NeuErstellen_Guetervorratslistegruppe.TabStop = false;
			this.grp_NeuErstellen_Guetervorratslistegruppe.Text = "Gütervorratsliste";
			// 
			// dtg_Listegruppe_Guetervorratsliste
			// 
			this.dtg_Listegruppe_Guetervorratsliste.DataMember = "";
			this.dtg_Listegruppe_Guetervorratsliste.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtg_Listegruppe_Guetervorratsliste.Location = new System.Drawing.Point(5, 16);
			this.dtg_Listegruppe_Guetervorratsliste.Name = "dtg_Listegruppe_Guetervorratsliste";
			this.dtg_Listegruppe_Guetervorratsliste.ReadOnly = true;
			this.dtg_Listegruppe_Guetervorratsliste.Size = new System.Drawing.Size(631, 220);
			this.dtg_Listegruppe_Guetervorratsliste.TabIndex = 0;
			this.dtg_Listegruppe_Guetervorratsliste.Click += new System.EventHandler(this.dtg_Listegruppe_Guetervorratsliste_Click);
			// 
			// cbx_Edit
			// 
			this.cbx_Edit.Location = new System.Drawing.Point(536, 244);
			this.cbx_Edit.Name = "cbx_Edit";
			this.cbx_Edit.Size = new System.Drawing.Size(95, 20);
			this.cbx_Edit.TabIndex = 1;
			this.cbx_Edit.Text = "Bearbeiten";
			this.cbx_Edit.CheckedChanged += new System.EventHandler(this.cbx_Edit_CheckedChanged);
			// 
			// ep_Eingabe
			// 
			this.ep_Eingabe.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.ep_Eingabe.ContainerControl = this;
			// 
			// Cpr_usc_GEuB
			// 
			this.Controls.Add(this.grp_NeuErstellen_Guetervorratslistegruppe);
			this.Controls.Add(this.grp_Eingabegruppe);
			this.Controls.Add(this.btn_NeuErstellen);
			this.Controls.Add(this.btn_Zuruecksetzen);
			this.Controls.Add(this.btn_Speichern);
			this.Controls.Add(this.gbx_Guttyp);
			this.Name = "Cpr_usc_GEuB";
			this.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Size = new System.Drawing.Size(642, 504);
			this.grp_Eingabegruppe.ResumeLayout(false);
			this.gbx_Guttyp.ResumeLayout(false);
			this.grp_NeuErstellen_Guetervorratslistegruppe.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtg_Listegruppe_Guetervorratsliste)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// initialisiert das DataGrid
		/// </summary>
		private void InitialisiereGrid()
		{
			// erstelle die Tabelle zur Anzeige der Daten.
			dtable_Gueter = new DataTable("dtable_Gueter");

			// erstelle die benötigten Spalten
			DataColumn dcol_Bezeichung = new DataColumn("Bezeichnung", typeof(string));
			DataColumn dcol_Art = new DataColumn("Art", typeof(string));
			DataColumn dcol_Menge = new DataColumn("Menge", typeof(float));
			DataColumn dcol_Lagerort = new DataColumn("Lagerort", typeof(string));
			DataColumn dcol_pelsID = new DataColumn("pelsID", typeof(int));

			// füge die Spalten zur Tabelle hinzu
			dtable_Gueter.Columns.Add(dcol_Bezeichung);
			dtable_Gueter.Columns.Add(dcol_Art);
			dtable_Gueter.Columns.Add(dcol_Menge);
			dtable_Gueter.Columns.Add(dcol_Lagerort);
			dtable_Gueter.Columns.Add(dcol_pelsID);


			this.dtg_Listegruppe_Guetervorratsliste.DataSource = 
				dtable_Gueter;

			// erzeuge neuen Table-Style
			DataGridTableStyle ts = new DataGridTableStyle();
			ts.MappingName = "dtable_Gueter";
			this.dtg_Listegruppe_Guetervorratsliste.TableStyles.Add(ts);
			DataGridColumnStyle _dgcs;
			// Bezeichnung
			_dgcs = this.dtg_Listegruppe_Guetervorratsliste.
				TableStyles["dtable_Gueter"].
				GridColumnStyles["Bezeichnung"];
			_dgcs.HeaderText = "Bezeichnung";
			_dgcs.Width = 190;
			// Art
			_dgcs = this.dtg_Listegruppe_Guetervorratsliste.
				TableStyles["dtable_Gueter"].
				GridColumnStyles["Art"];
			_dgcs.HeaderText = "Art";
			_dgcs.Width = 120;
			// Menge
			_dgcs = this.dtg_Listegruppe_Guetervorratsliste.
				TableStyles["dtable_Gueter"].
				GridColumnStyles["Menge"];
			_dgcs.HeaderText = "Menge";
			_dgcs.Width = 50;
			// Lagerort
			_dgcs = this.dtg_Listegruppe_Guetervorratsliste.
				TableStyles["dtable_Gueter"].
				GridColumnStyles["Lagerort"];
			_dgcs.HeaderText = "Lagerort";
			_dgcs.Width = 230;
			// pelsID soll nicht sichtbar sein
			_dgcs = this.dtg_Listegruppe_Guetervorratsliste.
				TableStyles["dtable_Gueter"].
				GridColumnStyles["pelsID"];
			_dgcs.Width = 0;
		}

		#endregion

		#region SetzeXXX
		/// <summary>
		///  stellt den Ausgangszustand der Eingabemaske her
		/// </summary>
		private void Zuruecksetzen()
		{
			_aktuelleGutID = 0;
			txt_Art.Text = "";
			txt_Bezeichnung.Text = "";
			txt_Lagerort.Text = "";
			txt_Menge.Text = "";
			dtp_Wiederbeschaffung.Value = DateTime.Now;

			_usc_Logistik.SetzeTreeViewKraft(tvw_Besitzer);
			_usc_Logistik.SetzeTreeViewKraft(tvw_Eigentuemer);
			if (_tn_Besitzer != null)
			{
				_tn_Besitzer.Checked = false;
				_tn_Besitzer = null;
			}
			if (_tn_Eigentuemer != null)
			{
				_tn_Eigentuemer.Checked = false;
				_tn_Eigentuemer = null;
			}

			_b_FelderModifiziert = false;
		
			// Alle Fehlermeldungen deaktivieren
			foreach(Control gbx in this.Controls)
				if (gbx is GroupBox)
					foreach (Control ctrl in gbx.Controls)
						ep_Eingabe.SetError(ctrl, String.Empty);
		}

		/// <summary>
		/// erlaubt die Rückfrage beim Benutzer bevor alle Werte zurückgesetzt werden
		/// </summary>
		private void ZuruecksetzenMitRueckfrage()
		{
			if (_b_FelderModifiziert && 
				(pELS.GUI.PopUp.CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes))
			{
				Zuruecksetzen();
			}
		}

		#endregion

		#region LadeXXX

		/// <summary>
		/// setzt Häkchen bei dem Elementen deren ID gleich der übergebenen
		/// ID ist
		/// </summary>
		/// <param name="pin_TreeNode"></param>
		/// <param name="pin_IDMenge"></param>
		private TreeNode LadeKraft(TreeNodeCollection pin_TreeNode, int pin_ID)
		{
			// gehe durch alle enthaltenen Knoten
			if (pin_TreeNode.Count != 0)
			{
				foreach(TreeNode tn in pin_TreeNode)
				{
					tn.Checked = false;
					// gehe durch alle in diesem enthaltenen Knoten Knoten
					if (tn.Nodes != null)
					{
						// rekursiver Aufruf
						TreeNode pout_tn = LadeKraft(tn.Nodes, pin_ID);
						if(pout_tn != null) return pout_tn;
					}
					// prüfe, ob ein Tag-Value existiert
					if (tn.Tag != null)
						// prüfe, ob das Element ausgewählt wurde
						if (((Cdv_pELSObject)tn.Tag).ID == pin_ID)
						{
							// falls entsprechendes Element gefunden, gebe den 
							// zugehörigen Knoten zurück
							tn.Checked = true;
							return tn;
						}
				}
			}
			return null;
		}

		/// <summary>
		/// fügt dem dataGrid ein weiteres Gut hinzu
		/// </summary>
		/// <param name="pin_Mitteilung"></param>
		private void FuegeGutZuGridHinzu(Cdv_Gut pin_Gut)
		{

			DataRow neuerEintrag = dtable_Gueter.NewRow();
			neuerEintrag["Bezeichnung"]		= pin_Gut.Bezeichnung;
			neuerEintrag["Art"]				= pin_Gut.Art;
			neuerEintrag["Menge"]			= pin_Gut.Menge;
			neuerEintrag["Lagerort"]		= pin_Gut.Lagerort;
			neuerEintrag["pelsID"]			= pin_Gut.ID;

			dtable_Gueter.Rows.Add(neuerEintrag);
		}

		/// <summary>
		/// lädt alle Güter eines bestimmten Typs in das Datagrid
		/// </summary>
		/// <param name="pin_Guetertyp"></param>
		public void LadeGueterGrid(string pin_Guetertyp)
		{
			// hole die Tabelle, in welcher geschrieben werden soll
			// entferne alle alten Einträge aus der Tabelle
			dtable_Gueter.Clear();
			// fülle die Tabelle entweder 
			switch (pin_Guetertyp)
			{
				// mit Materialen
				case "Material":
					this.dtg_Listegruppe_Guetervorratsliste.CaptionText = 
						"Materialien";
					foreach(Cdv_Material Material in _st_Logistik._AlleMaterialien)
					{
						FuegeGutZuGridHinzu(Material);
					}
					break;
				// oder mit Verbruachsgütern
				case "Verbrauchsgut":
					this.dtg_Listegruppe_Guetervorratsliste.CaptionText = 
						"Verbrauchsgüter";
					foreach(Cdv_Verbrauchsgut VB in _st_Logistik._AlleVerbrauchsgueter)
					{
						FuegeGutZuGridHinzu(VB);
					}
					break;
			}
		}

		/// <summary>
		/// lädt ein Gut in die Eingabemaske
		/// </summary>
		/// <param name="pin_Gut"></param>
		private void LadeGutInEingabemaske(Cdv_Gut pin_Gut)
		{
			_aktuelleGutID = pin_Gut.ID;

			txt_Bezeichnung.Text = pin_Gut.Bezeichnung;
			txt_Art.Text = pin_Gut.Art;
			txt_Lagerort.Text = pin_Gut.Lagerort;
			txt_Menge.Text = pin_Gut.Menge.ToString();

			Cdv_Verbrauchsgut tmpVB = pin_Gut as Cdv_Verbrauchsgut;
			Cdv_Material tmpMaterial = pin_Gut as Cdv_Material;
			if (tmpVB != null)
			{
				dtp_Wiederbeschaffung.Value = tmpVB.SpaetesterWiederbeschaffungszeitpunkt;
			}
			if (tmpMaterial != null)
			{
				
				int[] EigentuemerID = new int[1];
				EigentuemerID[0] = tmpMaterial.EigentuemerKraftID;
				TreeNode[] KnotenMenge;
				KnotenMenge = _usc_Logistik.LadeTreeViewKraefte(tvw_Eigentuemer, EigentuemerID);
				if (KnotenMenge.Length != 0)
                    _tn_Eigentuemer = KnotenMenge[0];
				else 
					_tn_Eigentuemer = null;
				if (_tn_Besitzer != null) _tn_Besitzer.Checked = true;
				int[] BesitzerID = new int[1];
				BesitzerID[0] = tmpMaterial.AktuellerBesitzerKraftID;
				KnotenMenge = _usc_Logistik.LadeTreeViewKraefte(tvw_Besitzer, BesitzerID);
				if (KnotenMenge.Length != 0)
					_tn_Besitzer = KnotenMenge[0];
				else
					_tn_Besitzer = null;
				if (_tn_Eigentuemer != null) _tn_Eigentuemer.Checked = true;
			}

		}

		
		#endregion

		#region Eingabevalidierung
		/// <summary>
		/// überprüft alle zwingend benötigten Eingaben auf Korrektheit
		/// </summary>
		/// <returns></returns>
		private bool Eingabevalidierung()
		{
			// setze Validierungsanzeigen falls nötig
			txt_Bezeichnung_Validated(null, null);
			tvw_Besitzer_Validated(null, null);
			tvw_Eigentuemer_Validated(null, null);
			// prüfe ob alle benötigten Felder korrekt sind
			// für Material
			if (rBtn_Material.Checked)
				if(ValidiereBezeichnung() && ValidiereBesitzer() && ValidiereEigentuemer())
					return true;
			// für Verbrauchsgüter
			if (rBtn_Verbrauchsgut.Checked)
				if(ValidiereBezeichnung())
					return true;
			return false;
		}

		/// <summary>
		/// überprüfe die Gutbezeichnung
		/// </summary>
		/// <returns></returns>
		private bool ValidiereBezeichnung()
		{
			return (txt_Bezeichnung.Text.Length > 0);
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txt_Bezeichnung_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereBezeichnung())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(txt_Bezeichnung, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(txt_Bezeichnung, "Bitte geben Sie eine Bezeichnung ein");
			}
		}

		/// <summary>
		/// überprüfe ob ein Besitzer ausgewählt wurde
		/// </summary>
		/// <returns></returns>
		private bool ValidiereBesitzer()
		{
			if (_tn_Besitzer == null)
				return false;
			else 
				return true;
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvw_Besitzer_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereBesitzer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(tvw_Besitzer, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(tvw_Besitzer, "Bitte geben Sie einen Besitzer an");
			}
		}

		/// <summary>
		/// überprüfe ob ein Eigentümer ausgewählt wurde
		/// </summary>
		/// <returns></returns>
		private bool ValidiereEigentuemer()
		{
			if (_tn_Eigentuemer == null)
				return false;
			else 
				return true;
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvw_Eigentuemer_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereEigentuemer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(tvw_Eigentuemer, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(tvw_Eigentuemer, "Bitte geben Sie einen Eigentümer an");
			}
		}

		#endregion

		#region Speichern

		private void SpeichereGut()
		{
			if(rBtn_Verbrauchsgut.Checked)
			{
				Cdv_Verbrauchsgut neuesVGut = new Cdv_Verbrauchsgut(txt_Bezeichnung.Text);
				neuesVGut.ID = _aktuelleGutID;
				neuesVGut.Art = txt_Art.Text;
				neuesVGut.Lagerort = txt_Lagerort.Text;
				if (txt_Menge.Text == "")
					neuesVGut.Menge = 0;
				else
					neuesVGut.Menge = Convert.ToSingle(txt_Menge.Text);
				neuesVGut.SpaetesterWiederbeschaffungszeitpunkt = dtp_Wiederbeschaffung.Value;

				_st_Logistik.SpeichereGut(neuesVGut);
			}
			else if(rBtn_Material.Checked)
			{
				if((_tn_Eigentuemer != null) || (_tn_Besitzer != null))
				{
					Cdv_Material neuesMaterial = new Cdv_Material(
						txt_Bezeichnung.Text, 
						((Cdv_pELSObject) _tn_Eigentuemer.Tag).ID);
					neuesMaterial.ID = _aktuelleGutID;
					neuesMaterial.Art = txt_Art.Text;
					neuesMaterial.Lagerort = txt_Lagerort.Text;
					if (txt_Menge.Text == "")
						neuesMaterial.Menge = 0;
					else
						neuesMaterial.Menge = (float) Convert.ToDouble(txt_Menge.Text);
					neuesMaterial.AktuellerBesitzerKraftID = 
						((Cdv_pELSObject) _tn_Besitzer.Tag).ID;

					_st_Logistik.SpeichereGut(neuesMaterial);

				}
			}
		}
		#endregion

		#region Events
		#region Modififierung
		/// <summary>
		/// eventbehandlung, welche bei allen Eingabeelementen registriert ist
		/// und eine vorgenommene Änderung registriert
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FelderModifiziert(object sender, System.EventArgs e)
		{
			_b_FelderModifiziert = true;
		}

		#endregion
		#region Tastendruck
		/// <summary>
		/// modifiziert die GUI in Abhängigkeit der ausgewählten Güterart
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rBtn_Gutart_CheckedChanged(object sender, System.EventArgs e)
		{
			Zuruecksetzen();
			if(rBtn_Verbrauchsgut.Checked)
			{
				lbl_Wiederbeschaffung.Visible = true;
				dtp_Wiederbeschaffung.Visible = true;
				lbl_Besitzer.Visible = false;
				tvw_Besitzer.Visible = false;
				lbl_Eigentuemer.Visible = false;
				tvw_Eigentuemer.Visible = false;
				LadeGueterGrid("Verbrauchsgut");
			}
			else
			{
				lbl_Wiederbeschaffung.Visible = false;
				dtp_Wiederbeschaffung.Visible = false;
				lbl_Besitzer.Visible = true;
				tvw_Besitzer.Visible = true;
				lbl_Eigentuemer.Visible = true;
				tvw_Eigentuemer.Visible = true;
				LadeGueterGrid("Material");
			}
			Zuruecksetzen();
		
		}

		/// <summary>
		/// ermöglicht das Erstellen eines neuen Gutes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_NeuErstellen_Click(object sender, System.EventArgs e)
		{
			Zuruecksetzen();
			_NurLesen = false;
		}
	
		/// <summary>
		/// führt eine Eingabeprüfung durch
		/// war diese erfolgreich wird das Gut gespeichert
		/// und die Eingabemaske zurückgesetzt
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Speichern_Click(object sender, System.EventArgs e)
		{
			if (Eingabevalidierung())
			{
				SpeichereGut();
				Zuruecksetzen();
			}
			else
			{
				pELS.GUI.PopUp.CPopUp.MeldenVonFalscherEingabe();
			}
		}

		/// <summary>
		/// stellt den Ausgangszustand der Eingabemaske her
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Zuruecksetzen_Click(object sender, System.EventArgs e)
		{
			ZuruecksetzenMitRueckfrage();
		}

		/// <summary>
		/// setzt die Zugriffsart auf die angezeigten Objekte (Lesen-Schreiben)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cbx_Edit_CheckedChanged(object sender, System.EventArgs e)
		{
			_NurLesen = !cbx_Edit.Checked;
		}
		#endregion
		#region Treeviews
		/// <summary>
		/// stellt sicher, dass immer nur eine Besitzer zur Zeit ausgewählt werden kann
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvw_Besitzer_BeforeCheck(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			// überprüfe ob der ausgewählte Knoten auf dem Root-Level liegt
			if(e.Node.Parent == null)
				// wenn ja, dann beende
				e.Cancel = true;
				// überprüfe ob das event vom Benutzer ausgelöst wurde
			else if(e.Action != TreeViewAction.Unknown)
			{
				// überprüfe ob ein Gut geladen wurde
				if (_aktuelleGutID != 0)
					e.Cancel = true;
				else
				{
					// falls ja, überprüfe ob der Zwischenspeicher-Knoten schon gesetzt ist
					if(_tn_Besitzer != null)
						// falss ja, dann setze diesen auf nicht ausgewählt
						_tn_Besitzer.Checked = false;
					// falls der ausgewählte Knoten nicht dem vorherigen Knoten entspricht
					if (_tn_Besitzer != e.Node)
					{
						// merke dir den ausgewählte Knoten
						_tn_Besitzer = e.Node;
						FelderModifiziert(null, null);
					}
					else 
						// kein Knoten ist markiert
						_tn_Besitzer = null;
				}
			}
		}

		/// <summary>
		/// stellt sicher, dass immer nur eine Besitzer zur Zeit ausgewählt werden kann
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvw_Eigentuemer_BeforeCheck(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			// überprüfe ob der ausgewählte Knoten auf dem Root-Level liegt
			if(e.Node.Parent == null)
				e.Cancel = true;
				// überprüfe ob das event vom Benutzer ausgelöst wurde
			else if(e.Action != TreeViewAction.Unknown)
			{
				// überprüfe ob ein Gut geladen wurde
				if (_aktuelleGutID != 0)
					e.Cancel = true;
				else
				{
					// falls ja, überprüfe ob der Zwischenspeicher-Knoten schon gesetzt ist
					if(_tn_Eigentuemer != null)
						// falss ja, dann setze diesen auf nicht ausgewählt
						_tn_Eigentuemer.Checked = false;
					// falls der ausgewählte Knoten nicht dem vorherigen Knoten entspricht
					if (_tn_Eigentuemer != e.Node)
					{
						// merke dir den ausgewählte Knoten
						_tn_Eigentuemer = e.Node;
						FelderModifiziert(null, null);
					}
					else 
						// kein Knoten ist markiert
						_tn_Eigentuemer = null;
				}
			}
		}

		#endregion
		#region Grid
		/// <summary>
		/// wählt den aktuellen Eintrag aus und lädt ihn
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dtg_Listegruppe_Guetervorratsliste_Click(object sender, System.EventArgs e)
		{
			// Schuppe: ich weiß nicht, warum dies hier nötig ist, aber es muss so sein
			_NurLesen = false;

			if (dtg_Listegruppe_Guetervorratsliste.CurrentRowIndex != -1)
			{
				int ID = (int)
					dtable_Gueter.Rows[dtg_Listegruppe_Guetervorratsliste.CurrentRowIndex]["pelsID"];
				Cdv_Gut tmpGut = _st_Logistik.ID2Gut(ID);
				if (tmpGut != null)
				{
					LadeGutInEingabemaske(tmpGut);
					_NurLesen = true;
					cbx_Edit.Checked = false;
					_b_FelderModifiziert = false;
					// entferne Validierungsanzeigen (falls das geladene Objekt korrekt ist)
					Eingabevalidierung();
				}
			}
		}

		#endregion
		#endregion

	}

}
