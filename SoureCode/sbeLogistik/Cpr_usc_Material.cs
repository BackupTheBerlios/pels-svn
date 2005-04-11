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
	/// <summary>
	/// Summary description for Cpr_usc_Material.
	/// </summary>
	public class Cpr_usc_Material : System.Windows.Forms.UserControl
	{
		#region graphische Variablen

		private System.Windows.Forms.Button btn_Zuruecksetze;
		private System.Windows.Forms.Button btn_Speichern;
		private System.Windows.Forms.GroupBox grp_Ziel;
		private System.Windows.Forms.GroupBox grp_Quelle;
		public System.Windows.Forms.TreeView tvw_aktuellerBesitzer;
		public System.Windows.Forms.TreeView tvw_neuerBesitzer;
		private System.Windows.Forms.Label lbl_Material_aktuellerBesitzer;
		private System.Windows.Forms.Label lbl_Material_neuerBesitzer;
		private System.Windows.Forms.GroupBox gbx_allgAngaben;
		public System.Windows.Forms.DateTimePicker dtp_Ausfuehrungszeitpunkt;
		public System.Windows.Forms.CheckBox cbx_AusfuehrungszeitpunktJetzt;
		private System.Windows.Forms.TextBox txt_Kommentar;
		private System.Windows.Forms.Label lbl_Kommentar;
		private System.Windows.Forms.Label lbl_Bearbeiter;
		private System.Windows.Forms.Label lbl_Datum;
		private System.Windows.Forms.Label lbl_Bearbeitername;
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
		/// Knoten im Treeview welcher den aktuellen Besitzer angibt
		/// </summary>
		private TreeNode _tn_aktuellerBesitzer;
		/// <summary>
		/// Knoten im Treeview welcher den neuen Besitzer angibt
		/// </summary>
		private TreeNode _tn_neuerBesitzer;
		/// <summary>
		/// Tabelle, welche alle Materialien des aktuellen Besitzers anzeigt
		/// </summary>
		private DataTable dtable_aktuellerBesitzer;
		private System.Windows.Forms.DataGrid dtg_neuerBesitzer;
		private System.Windows.Forms.DataGrid dtg_aktuellerBesitzer;
		private System.Windows.Forms.Label lbl_Menge;
		private System.Windows.Forms.ErrorProvider ep_Eingabe;
		private pELS.Tools.MaskedTextBox txt_Menge;
		/// <summary>
		/// Tabelle, welche alle Materialien des neuen Besitzers anzeigt
		/// </summary>
		private DataTable dtable_neuerBesitzer;



		#endregion

		#region Konstruktor & Destruktor
		public Cpr_usc_Material(Cst_Logistik pin_st_Logistik, Cpr_usc_Logistik pin_usc_Logistik)
		{
			_st_Logistik = pin_st_Logistik;
			_usc_Logistik = pin_usc_Logistik;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

			_usc_Logistik.SetzeTreeViewKraft(tvw_aktuellerBesitzer);
			_usc_Logistik.SetzeTreeViewKraft(tvw_neuerBesitzer);
			InitialisiereGrid();

			SetzeBenutzer(_st_Logistik.Einstellung.Benutzer);

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
			this.grp_Ziel = new System.Windows.Forms.GroupBox();
			this.dtg_neuerBesitzer = new System.Windows.Forms.DataGrid();
			this.lbl_Material_neuerBesitzer = new System.Windows.Forms.Label();
			this.tvw_neuerBesitzer = new System.Windows.Forms.TreeView();
			this.btn_Zuruecksetze = new System.Windows.Forms.Button();
			this.btn_Speichern = new System.Windows.Forms.Button();
			this.grp_Quelle = new System.Windows.Forms.GroupBox();
			this.dtg_aktuellerBesitzer = new System.Windows.Forms.DataGrid();
			this.lbl_Material_aktuellerBesitzer = new System.Windows.Forms.Label();
			this.tvw_aktuellerBesitzer = new System.Windows.Forms.TreeView();
			this.gbx_allgAngaben = new System.Windows.Forms.GroupBox();
			this.txt_Menge = new pELS.Tools.MaskedTextBox();
			this.lbl_Menge = new System.Windows.Forms.Label();
			this.lbl_Bearbeitername = new System.Windows.Forms.Label();
			this.lbl_Datum = new System.Windows.Forms.Label();
			this.lbl_Bearbeiter = new System.Windows.Forms.Label();
			this.lbl_Kommentar = new System.Windows.Forms.Label();
			this.txt_Kommentar = new System.Windows.Forms.TextBox();
			this.cbx_AusfuehrungszeitpunktJetzt = new System.Windows.Forms.CheckBox();
			this.dtp_Ausfuehrungszeitpunkt = new System.Windows.Forms.DateTimePicker();
			this.ep_Eingabe = new System.Windows.Forms.ErrorProvider();
			this.grp_Ziel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtg_neuerBesitzer)).BeginInit();
			this.grp_Quelle.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtg_aktuellerBesitzer)).BeginInit();
			this.gbx_allgAngaben.SuspendLayout();
			this.SuspendLayout();
			// 
			// grp_Ziel
			// 
			this.grp_Ziel.Controls.Add(this.dtg_neuerBesitzer);
			this.grp_Ziel.Controls.Add(this.lbl_Material_neuerBesitzer);
			this.grp_Ziel.Controls.Add(this.tvw_neuerBesitzer);
			this.grp_Ziel.Location = new System.Drawing.Point(324, 12);
			this.grp_Ziel.Name = "grp_Ziel";
			this.grp_Ziel.Size = new System.Drawing.Size(316, 356);
			this.grp_Ziel.TabIndex = 74;
			this.grp_Ziel.TabStop = false;
			this.grp_Ziel.Text = "neuer Besitzer";
			// 
			// dtg_neuerBesitzer
			// 
			this.dtg_neuerBesitzer.DataMember = "";
			this.dtg_neuerBesitzer.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtg_neuerBesitzer.Location = new System.Drawing.Point(5, 152);
			this.dtg_neuerBesitzer.Name = "dtg_neuerBesitzer";
			this.dtg_neuerBesitzer.ReadOnly = true;
			this.dtg_neuerBesitzer.Size = new System.Drawing.Size(291, 200);
			this.dtg_neuerBesitzer.TabIndex = 2;
			this.dtg_neuerBesitzer.Click += new System.EventHandler(this.dtg_neuerBesitzer_Click);
			// 
			// lbl_Material_neuerBesitzer
			// 
			this.lbl_Material_neuerBesitzer.Location = new System.Drawing.Point(8, 136);
			this.lbl_Material_neuerBesitzer.Name = "lbl_Material_neuerBesitzer";
			this.lbl_Material_neuerBesitzer.Size = new System.Drawing.Size(100, 20);
			this.lbl_Material_neuerBesitzer.TabIndex = 35;
			this.lbl_Material_neuerBesitzer.Text = "Material";
			// 
			// tvw_neuerBesitzer
			// 
			this.tvw_neuerBesitzer.CheckBoxes = true;
			this.tvw_neuerBesitzer.ImageIndex = -1;
			this.tvw_neuerBesitzer.Location = new System.Drawing.Point(4, 16);
			this.tvw_neuerBesitzer.Name = "tvw_neuerBesitzer";
			this.tvw_neuerBesitzer.SelectedImageIndex = -1;
			this.tvw_neuerBesitzer.Size = new System.Drawing.Size(292, 116);
			this.tvw_neuerBesitzer.TabIndex = 34;
			this.tvw_neuerBesitzer.Validated += new System.EventHandler(this.tvw_neuerBesitzer_Validated);
			this.tvw_neuerBesitzer.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvw_neuerBesitzer_BeforeCheck);
			// 
			// btn_Zuruecksetze
			// 
			this.btn_Zuruecksetze.Location = new System.Drawing.Point(468, 472);
			this.btn_Zuruecksetze.Name = "btn_Zuruecksetze";
			this.btn_Zuruecksetze.Size = new System.Drawing.Size(81, 25);
			this.btn_Zuruecksetze.TabIndex = 72;
			this.btn_Zuruecksetze.Text = "&Zurücksetzen";
			this.btn_Zuruecksetze.Click += new System.EventHandler(this.btn_Zuruecksetze_Click);
			// 
			// btn_Speichern
			// 
			this.btn_Speichern.Location = new System.Drawing.Point(552, 472);
			this.btn_Speichern.Name = "btn_Speichern";
			this.btn_Speichern.Size = new System.Drawing.Size(80, 25);
			this.btn_Speichern.TabIndex = 71;
			this.btn_Speichern.Text = "Zu&ordnen";
			this.btn_Speichern.Click += new System.EventHandler(this.btn_Speichern_Click);
			// 
			// grp_Quelle
			// 
			this.grp_Quelle.Controls.Add(this.dtg_aktuellerBesitzer);
			this.grp_Quelle.Controls.Add(this.lbl_Material_aktuellerBesitzer);
			this.grp_Quelle.Controls.Add(this.tvw_aktuellerBesitzer);
			this.grp_Quelle.Location = new System.Drawing.Point(0, 12);
			this.grp_Quelle.Name = "grp_Quelle";
			this.grp_Quelle.Size = new System.Drawing.Size(316, 356);
			this.grp_Quelle.TabIndex = 69;
			this.grp_Quelle.TabStop = false;
			this.grp_Quelle.Text = "aktueller Besitzer";
			// 
			// dtg_aktuellerBesitzer
			// 
			this.dtg_aktuellerBesitzer.DataMember = "";
			this.dtg_aktuellerBesitzer.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtg_aktuellerBesitzer.Location = new System.Drawing.Point(5, 152);
			this.dtg_aktuellerBesitzer.Name = "dtg_aktuellerBesitzer";
			this.dtg_aktuellerBesitzer.ReadOnly = true;
			this.dtg_aktuellerBesitzer.Size = new System.Drawing.Size(291, 200);
			this.dtg_aktuellerBesitzer.TabIndex = 2;
			this.dtg_aktuellerBesitzer.Click += new System.EventHandler(this.dtg_aktuellerBesitzer_Click);
			this.dtg_aktuellerBesitzer.Validated += new System.EventHandler(this.dtg_aktuellerBesitzer_Validated);
			// 
			// lbl_Material_aktuellerBesitzer
			// 
			this.lbl_Material_aktuellerBesitzer.Location = new System.Drawing.Point(8, 136);
			this.lbl_Material_aktuellerBesitzer.Name = "lbl_Material_aktuellerBesitzer";
			this.lbl_Material_aktuellerBesitzer.Size = new System.Drawing.Size(100, 20);
			this.lbl_Material_aktuellerBesitzer.TabIndex = 34;
			this.lbl_Material_aktuellerBesitzer.Text = "Material";
			// 
			// tvw_aktuellerBesitzer
			// 
			this.tvw_aktuellerBesitzer.CheckBoxes = true;
			this.tvw_aktuellerBesitzer.ImageIndex = -1;
			this.tvw_aktuellerBesitzer.Location = new System.Drawing.Point(4, 16);
			this.tvw_aktuellerBesitzer.Name = "tvw_aktuellerBesitzer";
			this.tvw_aktuellerBesitzer.SelectedImageIndex = -1;
			this.tvw_aktuellerBesitzer.Size = new System.Drawing.Size(292, 116);
			this.tvw_aktuellerBesitzer.TabIndex = 33;
			this.tvw_aktuellerBesitzer.Validated += new System.EventHandler(this.tvw_aktuellerBesitzer_Validated);
			this.tvw_aktuellerBesitzer.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvw_aktuellerBesitzer_BeforeCheck);
			// 
			// gbx_allgAngaben
			// 
			this.gbx_allgAngaben.Controls.Add(this.txt_Menge);
			this.gbx_allgAngaben.Controls.Add(this.lbl_Menge);
			this.gbx_allgAngaben.Controls.Add(this.lbl_Bearbeitername);
			this.gbx_allgAngaben.Controls.Add(this.lbl_Datum);
			this.gbx_allgAngaben.Controls.Add(this.lbl_Bearbeiter);
			this.gbx_allgAngaben.Controls.Add(this.lbl_Kommentar);
			this.gbx_allgAngaben.Controls.Add(this.txt_Kommentar);
			this.gbx_allgAngaben.Controls.Add(this.cbx_AusfuehrungszeitpunktJetzt);
			this.gbx_allgAngaben.Controls.Add(this.dtp_Ausfuehrungszeitpunkt);
			this.gbx_allgAngaben.Location = new System.Drawing.Point(12, 372);
			this.gbx_allgAngaben.Name = "gbx_allgAngaben";
			this.gbx_allgAngaben.Size = new System.Drawing.Size(624, 92);
			this.gbx_allgAngaben.TabIndex = 76;
			this.gbx_allgAngaben.TabStop = false;
			// 
			// txt_Menge
			// 
			this.txt_Menge.Location = new System.Drawing.Point(112, 64);
			this.txt_Menge.Name = "txt_Menge";
			this.txt_Menge.OnlyDecimal = true;
			this.txt_Menge.OnlyDigit = true;
			this.txt_Menge.OnlyIPAddr = false;
			this.txt_Menge.Size = new System.Drawing.Size(112, 20);
			this.txt_Menge.TabIndex = 83;
			this.txt_Menge.Text = "";
			// 
			// lbl_Menge
			// 
			this.lbl_Menge.Location = new System.Drawing.Point(8, 60);
			this.lbl_Menge.Name = "lbl_Menge";
			this.lbl_Menge.Size = new System.Drawing.Size(100, 28);
			this.lbl_Menge.TabIndex = 81;
			this.lbl_Menge.Text = "übergebene Menge";
			// 
			// lbl_Bearbeitername
			// 
			this.lbl_Bearbeitername.Location = new System.Drawing.Point(116, 20);
			this.lbl_Bearbeitername.Name = "lbl_Bearbeitername";
			this.lbl_Bearbeitername.Size = new System.Drawing.Size(100, 16);
			this.lbl_Bearbeitername.TabIndex = 80;
			// 
			// lbl_Datum
			// 
			this.lbl_Datum.Location = new System.Drawing.Point(8, 40);
			this.lbl_Datum.Name = "lbl_Datum";
			this.lbl_Datum.Size = new System.Drawing.Size(48, 16);
			this.lbl_Datum.TabIndex = 79;
			this.lbl_Datum.Text = "Datum";
			// 
			// lbl_Bearbeiter
			// 
			this.lbl_Bearbeiter.Location = new System.Drawing.Point(8, 20);
			this.lbl_Bearbeiter.Name = "lbl_Bearbeiter";
			this.lbl_Bearbeiter.Size = new System.Drawing.Size(100, 16);
			this.lbl_Bearbeiter.TabIndex = 78;
			this.lbl_Bearbeiter.Text = "Bearbeiter";
			// 
			// lbl_Kommentar
			// 
			this.lbl_Kommentar.Location = new System.Drawing.Point(272, 16);
			this.lbl_Kommentar.Name = "lbl_Kommentar";
			this.lbl_Kommentar.Size = new System.Drawing.Size(100, 16);
			this.lbl_Kommentar.TabIndex = 77;
			this.lbl_Kommentar.Text = "Kommentar";
			// 
			// txt_Kommentar
			// 
			this.txt_Kommentar.Location = new System.Drawing.Point(272, 32);
			this.txt_Kommentar.Multiline = true;
			this.txt_Kommentar.Name = "txt_Kommentar";
			this.txt_Kommentar.Size = new System.Drawing.Size(348, 52);
			this.txt_Kommentar.TabIndex = 76;
			this.txt_Kommentar.Text = "";
			// 
			// cbx_AusfuehrungszeitpunktJetzt
			// 
			this.cbx_AusfuehrungszeitpunktJetzt.Location = new System.Drawing.Point(64, 40);
			this.cbx_AusfuehrungszeitpunktJetzt.Name = "cbx_AusfuehrungszeitpunktJetzt";
			this.cbx_AusfuehrungszeitpunktJetzt.Size = new System.Drawing.Size(44, 16);
			this.cbx_AusfuehrungszeitpunktJetzt.TabIndex = 75;
			this.cbx_AusfuehrungszeitpunktJetzt.Text = "jetzt";
			// 
			// dtp_Ausfuehrungszeitpunkt
			// 
			this.dtp_Ausfuehrungszeitpunkt.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_Ausfuehrungszeitpunkt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Ausfuehrungszeitpunkt.Location = new System.Drawing.Point(112, 36);
			this.dtp_Ausfuehrungszeitpunkt.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp_Ausfuehrungszeitpunkt.Name = "dtp_Ausfuehrungszeitpunkt";
			this.dtp_Ausfuehrungszeitpunkt.Size = new System.Drawing.Size(116, 20);
			this.dtp_Ausfuehrungszeitpunkt.TabIndex = 74;
			// 
			// ep_Eingabe
			// 
			this.ep_Eingabe.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.ep_Eingabe.ContainerControl = this;
			// 
			// Cpr_usc_Material
			// 
			this.Controls.Add(this.gbx_allgAngaben);
			this.Controls.Add(this.grp_Ziel);
			this.Controls.Add(this.btn_Zuruecksetze);
			this.Controls.Add(this.btn_Speichern);
			this.Controls.Add(this.grp_Quelle);
			this.Name = "Cpr_usc_Material";
			this.Size = new System.Drawing.Size(642, 504);
			this.grp_Ziel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtg_neuerBesitzer)).EndInit();
			this.grp_Quelle.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtg_aktuellerBesitzer)).EndInit();
			this.gbx_allgAngaben.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void InitialisiereGrid()
		{
			// erstelle die Tabelle zur Anzeige der Daten.
			dtable_aktuellerBesitzer = new DataTable("dtable_aktuellerBesitzer");
			dtable_neuerBesitzer = new DataTable("dtable_neuerBesitzer");

			// erstelle die benötigten Spalten
			DataColumn dcol_aBBezeichnung= new DataColumn("Bezeichnung", typeof(string));
			DataColumn dcol_aBArt = new DataColumn("Art", typeof(string));
			DataColumn dcol_aBMenge = new DataColumn("Menge", typeof(float));
			DataColumn dcol_aBpelsID = new DataColumn("pelsID", typeof(int));

			// füge die Spalten zur Tabelle hinzu
			dtable_aktuellerBesitzer.Columns.Add(dcol_aBBezeichnung);
			dtable_aktuellerBesitzer.Columns.Add(dcol_aBArt);
			dtable_aktuellerBesitzer.Columns.Add(dcol_aBMenge);
			dtable_aktuellerBesitzer.Columns.Add(dcol_aBpelsID);

			DataColumn dcol_nBBezeichnung= new DataColumn("Bezeichnung", typeof(string));
			DataColumn dcol_nBArt = new DataColumn("Art", typeof(string));
			DataColumn dcol_nBMenge = new DataColumn("Menge", typeof(float));
			DataColumn dcol_nBpelsID = new DataColumn("pelsID", typeof(int));
			dtable_neuerBesitzer.Columns.Add(dcol_nBBezeichnung);
			dtable_neuerBesitzer.Columns.Add(dcol_nBArt);
			dtable_neuerBesitzer.Columns.Add(dcol_nBMenge);
			dtable_neuerBesitzer.Columns.Add(dcol_nBpelsID);


			this.dtg_aktuellerBesitzer.DataSource = 
				dtable_aktuellerBesitzer;
			this.dtg_neuerBesitzer.DataSource = 
				dtable_neuerBesitzer;

			// erzeuge neuen Table-Style
			DataGridTableStyle ts = new DataGridTableStyle();
			ts.SelectionBackColor = Color.CadetBlue;
			ts.MappingName = "dtable_aktuellerBesitzer";
			this.dtg_aktuellerBesitzer.TableStyles.Add(ts);
			DataGridColumnStyle _dgcs;
			// Bezeichnung
			_dgcs = this.dtg_aktuellerBesitzer.
				TableStyles["dtable_aktuellerBesitzer"].
				GridColumnStyles["Bezeichnung"];
			_dgcs.HeaderText = "Bezeichnung";
			_dgcs.Width = 130;
			// Art
			_dgcs = this.dtg_aktuellerBesitzer.
				TableStyles["dtable_aktuellerBesitzer"].
				GridColumnStyles["Art"];
			_dgcs.HeaderText = "Art";
			_dgcs.Width = 70;
			// Menge
			_dgcs = this.dtg_aktuellerBesitzer.
				TableStyles["dtable_aktuellerBesitzer"].
				GridColumnStyles["Menge"];
			_dgcs.HeaderText = "Menge";
			_dgcs.Width = 50;
			// Lagerort
			// pelsID soll nicht sichtbar sein
			_dgcs = this.dtg_aktuellerBesitzer.
				TableStyles["dtable_aktuellerBesitzer"].
				GridColumnStyles["pelsID"];
			_dgcs.Width = 0;

			// erzeuge neuen Table-Style
			DataGridTableStyle nBts = new DataGridTableStyle();
			nBts.MappingName = "dtable_neuerBesitzer";
			this.dtg_neuerBesitzer.TableStyles.Add(nBts);
			// Bezeichnung
			_dgcs = this.dtg_neuerBesitzer.
				TableStyles["dtable_neuerBesitzer"].
				GridColumnStyles["Bezeichnung"];
			_dgcs.HeaderText = "Bezeichnung";
			_dgcs.Width = 130;
			// Art
			_dgcs = this.dtg_neuerBesitzer.
				TableStyles["dtable_neuerBesitzer"].
				GridColumnStyles["Art"];
			_dgcs.HeaderText = "Art";
			_dgcs.Width = 70;
			// Menge
			_dgcs = this.dtg_neuerBesitzer.
				TableStyles["dtable_neuerBesitzer"].
				GridColumnStyles["Menge"];
			_dgcs.HeaderText = "Menge";
			_dgcs.Width = 50;
			// Lagerort
			// pelsID soll nicht sichtbar sein
			_dgcs = this.dtg_neuerBesitzer.
				TableStyles["dtable_neuerBesitzer"].
				GridColumnStyles["pelsID"];
			_dgcs.Width = 0;
		}


		#endregion

		#region SetzeXXX

		/// <summary>
		/// setzt die Anzeige des aktuellen Benutzers
		/// </summary>
		/// <param name="pin_Benutzer"></param>
		public void SetzeBenutzer(Cdv_Benutzer pin_Benutzer)
		{
			this.lbl_Bearbeitername.Text = pin_Benutzer.Benutzername;
		}


		public void Zuruecksetzen()
		{
			_usc_Logistik.SetzeTreeViewKraft(tvw_aktuellerBesitzer);
			_usc_Logistik.SetzeTreeViewKraft(tvw_neuerBesitzer);
			dtable_aktuellerBesitzer.Clear();
			dtable_neuerBesitzer.Clear();
			txt_Kommentar.Text = String.Empty;
			txt_Menge.Text = String.Empty;
			dtp_Ausfuehrungszeitpunkt.Value = DateTime.Now;

			ep_Eingabe.SetError(txt_Menge, "");
		}
		#endregion

		#region LadeXXX
		private void LadeMaterial(int pin_BesitzerID, DataTable pin_Table)
		{
			ArrayList Materialien = _st_Logistik.HoleAlleMaterialien(pin_BesitzerID);

			pin_Table.Clear();
			foreach(Cdv_Material Material in Materialien)
			{
				DataRow neuerEintrag = pin_Table.NewRow();
				neuerEintrag["Bezeichnung"]		= Material.Bezeichnung;
				neuerEintrag["Art"]				= Material.Art;
				neuerEintrag["Menge"]			= Material.Menge;
				neuerEintrag["pelsID"]			= Material.ID;

				pin_Table.Rows.Add(neuerEintrag);
			}

		}
		/// <summary>
		/// lädt alle Kraefte in die TreeViews
		/// </summary>
		public void LadeKraefteNeu()
		{
			_usc_Logistik.SetzeTreeViewKraft(tvw_aktuellerBesitzer);
			_usc_Logistik.SetzeTreeViewKraft(tvw_neuerBesitzer);
		}

		#endregion

		#region Eingabevalidierung
		private bool Eingabevalidierung()
		{
			if(ValidiereAktuellerBesitzer() && 
				ValidiereNeuerBesitzer() && 
				ValidiereMaterial() && 
				ValidiereMenge())
					return true;
			txt_Menge_Validated(null, null);
			tvw_aktuellerBesitzer_Validated(null, null);
			tvw_neuerBesitzer_Validated(null, null);
			dtg_aktuellerBesitzer_Validated(null, null);
			return false;
		}

		private bool ValidiereMenge()
		{
			if (txt_Menge.Text == "")
				return false;
			if (_tn_aktuellerBesitzer != null && dtg_aktuellerBesitzer.CurrentRowIndex > -1)
			{
				float tmp = Convert.ToSingle(txt_Menge.Text);
				if(tmp == 0 || tmp > ((Cdv_Material) _st_Logistik.ID2Gut( (int)
					dtable_aktuellerBesitzer.Rows[dtg_aktuellerBesitzer.CurrentRowIndex]["pelsID"])).Menge)
					return false;
			}
			else 
				return false;
			return true;
		}

		private bool ValidiereAktuellerBesitzer()
		{
			if(_tn_aktuellerBesitzer != null) return true;
			else return false;
		}

		private bool ValidiereNeuerBesitzer()
		{
			if(_tn_neuerBesitzer != null && _tn_neuerBesitzer.Text != _tn_aktuellerBesitzer.Text) return true;
			else return false;
		}

		private bool ValidiereMaterial()
		{
			if (dtg_aktuellerBesitzer.CurrentRowIndex > -1) return true;
			else return false;
		}

		
		private void txt_Menge_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereMenge())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(txt_Menge, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(txt_Menge, "Bitte geben Sie eine gültige Menge ein");
			}
		}

		private void tvw_aktuellerBesitzer_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereAktuellerBesitzer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(tvw_aktuellerBesitzer, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(tvw_aktuellerBesitzer, "Bitte wählen Sie den aktuellen Besitzer");
			}
		}
		private void tvw_neuerBesitzer_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereNeuerBesitzer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(tvw_neuerBesitzer, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(tvw_neuerBesitzer, "Bitte wählen Sie den neuen Besitzer");
			}
		}

		private void dtg_aktuellerBesitzer_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereMaterial())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(dtg_aktuellerBesitzer, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(dtg_aktuellerBesitzer, "Bitte wählen Sie das zu übergebene Material");
			}
		}

		#endregion


		#region Speichern
		private void SpeichereMaterialUebergabe()
		{
			// Materialübergabeobjekt aus Eingaben erstellen
			Cdv_Materialuebergabe MatUebergabe = new Cdv_Materialuebergabe(dtp_Ausfuehrungszeitpunkt.Value,
				((Cdv_Kraft) _tn_aktuellerBesitzer.Tag).ID,
				((Cdv_Kraft) _tn_neuerBesitzer.Tag).ID,
				(int) dtable_aktuellerBesitzer.Rows[dtg_aktuellerBesitzer.CurrentRowIndex]["pelsID"],
				Convert.ToInt16(txt_Menge.Text));
			MatUebergabe.AllgBemerkungen.Text = txt_Kommentar.Text;
			MatUebergabe.AllgBemerkungen.Autor = _st_Logistik.Einstellung.Benutzer.Benutzername;
			// Objekt an die Steuerungsschicht weitereichen
			_st_Logistik.SpeichereMaterialuebergabe(MatUebergabe);
			
		}

		private void UebergebeMaterial(
			Cdv_Material pin_Material,
			int pin_neuerBesitzerID,
			float pin_Menge)
		{
			// Falls das gesamte Material übergeben wurde
			if (pin_Material.Menge == pin_Menge)
			{
				pin_Material.AktuellerBesitzerKraftID = pin_neuerBesitzerID;
				_st_Logistik.SpeichereGut(pin_Material);
			}
			// Falls nur ein Teil übergeben wurde
			else
			{
				Cdv_Material neueMaterialMenge = new Cdv_Material(
					pin_Material.Bezeichnung,
					pin_Material.EigentuemerKraftID);
				neueMaterialMenge.AktuellerBesitzerKraftID = pin_neuerBesitzerID;
				neueMaterialMenge.Art = pin_Material.Art;
				neueMaterialMenge.Lagerort = pin_Material.Lagerort;
				neueMaterialMenge.Menge = pin_Menge;
				_st_Logistik.SpeichereGut(neueMaterialMenge);

				pin_Material.Menge -= pin_Menge;
				_st_Logistik.SpeichereGut(pin_Material);	
			}
			// Speichert die Übergabe, so dass daraus an anderer Stelle ein Übergabebeleg erstellt werden kann
			this.SpeichereMaterialUebergabe();
		}

		#endregion


		#region events
		#region Modifizierung
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
		private void btn_Speichern_Click(object sender, System.EventArgs e)
		{
			if (Eingabevalidierung())
			{
				UebergebeMaterial(
					(Cdv_Material) _st_Logistik.ID2Gut( (int)
					dtable_aktuellerBesitzer.Rows[dtg_aktuellerBesitzer.CurrentRowIndex]["pelsID"]),
					((Cdv_Kraft) _tn_neuerBesitzer.Tag).ID,
					Single.Parse(txt_Menge.Text));
				Zuruecksetzen();
			}
			else
			{
				pELS.GUI.PopUp.CPopUp.MeldenVonFalscherEingabe();
			}
		}


		private void btn_Zuruecksetze_Click(object sender, System.EventArgs e)
		{
			this.Zuruecksetzen();
		}


		#endregion

		#region TreeViews
		/// <summary>
		/// stellt sicher, dass immer nur eine Besitzer zur Zeit ausgewählt werden kann
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvw_aktuellerBesitzer_BeforeCheck(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			// überprüfe ob der ausgewählte Knoten auf dem Root-Level liegt
			if(e.Node.Parent == null)
				// wenn ja, dann beende
				e.Cancel = true;
				// überprüfe ob das event vom Benutzer ausgelöst wurde
			else if(e.Action != TreeViewAction.Unknown)
			{
				// falls ja, überprüfe ob der Zwischenspeicher-Knoten schon gesetzt ist
				if(_tn_aktuellerBesitzer != null)
				{
					// falls der ausgewählte Knoten nicht dem vorherigen Knoten entspricht
					if (_tn_aktuellerBesitzer != e.Node)
					{
						// dann setze diesen auf nicht ausgewählt
						_tn_aktuellerBesitzer.Checked = false;
						// merke dir den ausgewählte Knoten
						_tn_aktuellerBesitzer = e.Node;
						LadeMaterial(
							((Cdv_Kraft) _tn_aktuellerBesitzer.Tag).ID,
							dtable_aktuellerBesitzer);
						FelderModifiziert(null, null);
					}
					else 
						// sonst tue nichts
						e.Cancel = true;
				}
				else
				{
					_tn_aktuellerBesitzer = e.Node;
					LadeMaterial(
						((Cdv_Kraft) _tn_aktuellerBesitzer.Tag).ID,
						dtable_aktuellerBesitzer);
					FelderModifiziert(null, null);
				}
			}
		}

		/// <summary>
		/// stellt sicher, dass immer nur eine Besitzer zur Zeit ausgewählt werden kann
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvw_neuerBesitzer_BeforeCheck(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			// überprüfe ob der ausgewählte Knoten auf dem Root-Level liegt
			if(e.Node.Parent == null)
				// wenn ja, dann beende
				e.Cancel = true;
				// überprüfe ob das event vom Benutzer ausgelöst wurde
			else if(e.Action != TreeViewAction.Unknown)
			{
				// falls ja, überprüfe ob der Zwischenspeicher-Knoten schon gesetzt ist
				if(_tn_neuerBesitzer != null)
				{
					// falls der ausgewählte Knoten nicht dem vorherigen Knoten entspricht
					if (_tn_neuerBesitzer != e.Node)
					{
						// dann setze diesen auf nicht ausgewählt
						_tn_neuerBesitzer.Checked = false;
						// merke dir den ausgewählte Knoten
						_tn_neuerBesitzer = e.Node;
						LadeMaterial(
							((Cdv_Kraft) _tn_neuerBesitzer.Tag).ID,
							dtable_neuerBesitzer);
						FelderModifiziert(null, null);
					}
					else 
						// sonst tue nichts
						e.Cancel = true;
				}
				else
				{
					_tn_neuerBesitzer = e.Node;
					LadeMaterial(
						((Cdv_Kraft) _tn_neuerBesitzer.Tag).ID,
						dtable_neuerBesitzer);
					FelderModifiziert(null, null);
				}
			}
		}

		#endregion

		#region Grids
		private void dtg_aktuellerBesitzer_Click(object sender, System.EventArgs e)
		{
			if (dtable_aktuellerBesitzer.Rows.Count != 0)
			{
				this.dtg_aktuellerBesitzer.Select
					(this.dtg_aktuellerBesitzer.CurrentRowIndex);
				txt_Menge.Text = 
					((float) dtable_aktuellerBesitzer.Rows[dtg_aktuellerBesitzer.CurrentRowIndex]["Menge"]).ToString();
			}
		}
		private void dtg_neuerBesitzer_Click(object sender, System.EventArgs e)
		{

			if (dtable_aktuellerBesitzer.Rows.Count != 0)
			{
				this.dtg_neuerBesitzer.Select
					(this.dtg_neuerBesitzer.CurrentRowIndex);
			}
		}

		#endregion

		#endregion
	}
}
