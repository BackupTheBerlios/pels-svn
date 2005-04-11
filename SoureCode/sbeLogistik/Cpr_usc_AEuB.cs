using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pELS.Client.Logistik
{
	/// <summary>
	/// Summary description for AEuB.
	/// </summary>
	public class Cpr_usc_AEuB : System.Windows.Forms.UserControl
	{
		#region graphische Variablen
		private System.Windows.Forms.Button btn_NeuErstellen;
		private System.Windows.Forms.Button btn_Kennzeichnen;
		private System.Windows.Forms.Button btn_Splitten;
		private System.Windows.Forms.Button btn_Loeschen;
		private System.Windows.Forms.GroupBox grp_Listegruppe;
		private System.Windows.Forms.DataGrid dtg_Listegruppe_Gueteranforderungsliste;
		private System.Windows.Forms.GroupBox grp_Eingabegruppe;
		private System.Windows.Forms.GroupBox grp_Eingabegruppe_Eingabe;
		private System.Windows.Forms.CheckBox chb_Eingabegruppe_Eingabe_ZufuehrungsdatumJetzt;
		private System.Windows.Forms.CheckBox chb_Eingabegruppe_Eingabe_AnforderungsdatumJetzt;
		private System.Windows.Forms.Label lbl_Eingabegruppe_Eingabe_Menge;
		private System.Windows.Forms.TextBox txt_Eingabegruppe_Eingabe_Menge;
		private System.Windows.Forms.ComboBox cmb_Eingabegruppe_Eingabe_Gut;
		private System.Windows.Forms.Label lbl_Eingabegruppe_Eingabe_Gut;
		private System.Windows.Forms.DateTimePicker dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum;
		private System.Windows.Forms.DateTimePicker dtp_Eingabegruppe_Eingabe_Anforderungsdatum;
		private System.Windows.Forms.Label lbl_Eingabegruppe_Eingabe_Zuführungsdatum;
		private System.Windows.Forms.Label lbl_Eingabegruppe_Eingabe_Anforderungsdatum;
		private System.Windows.Forms.ComboBox cmb_Eingabegruppe_Eingabe_Anforderungsstatus;
		private System.Windows.Forms.Label lbl_Eingabegruppe_Eingabe_Anforderungsstatus;
		private System.Windows.Forms.Label lbl_Eingabegruppe_Eingabe_AnforderndeKraft;
		private System.Windows.Forms.Label lbl_Eingabegruppe_Eingabe_Zweck;
		private System.Windows.Forms.TextBox txt_Eingabegruppe_Eingabe_Zweck;
		private System.Windows.Forms.TextBox txt_Eingabegruppe_Eingabe_AnforderndeKraft;
		private System.Windows.Forms.GroupBox grp_Eingabegruppe_Bemerkungsgruppe;
		private System.Windows.Forms.TextBox grp_Eingabegruppe_Bemerkungsgruppe_Autor;
		private System.Windows.Forms.Label lbl_Eingabegruppe_Bemerkungsgruppe_Autor;
		private System.Windows.Forms.RichTextBox txt_Eingabegruppe_Bemerkungsgruppe_Text;
		private System.Windows.Forms.Button btn_Eingabegruppe_Abbrechen;
		private System.Windows.Forms.Button btn_Eingabegruppe_Zuruecksetzen;
		private System.Windows.Forms.Button btn_Eingabegruppe_Speichern;
		private System.Windows.Forms.GroupBox grp_KngBemGruppe;
		private System.Windows.Forms.Button btn_KngBemGruppe_Speichern;
		private System.Windows.Forms.Button btn_KngBemGruppe_Abbrechen;
		private System.Windows.Forms.TextBox txt_Autor;
		private System.Windows.Forms.Label lbl_KngBemGruppe_Autor;
		private System.Windows.Forms.RichTextBox txt_KngBemGruppe_Text;
		private System.Windows.Forms.CheckBox chb_Edit;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public Cpr_usc_AEuB()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
			this.btn_NeuErstellen = new System.Windows.Forms.Button();
			this.btn_Kennzeichnen = new System.Windows.Forms.Button();
			this.btn_Splitten = new System.Windows.Forms.Button();
			this.btn_Loeschen = new System.Windows.Forms.Button();
			this.grp_Listegruppe = new System.Windows.Forms.GroupBox();
			this.dtg_Listegruppe_Gueteranforderungsliste = new System.Windows.Forms.DataGrid();
			this.chb_Edit = new System.Windows.Forms.CheckBox();
			this.grp_Eingabegruppe = new System.Windows.Forms.GroupBox();
			this.grp_Eingabegruppe_Eingabe = new System.Windows.Forms.GroupBox();
			this.chb_Eingabegruppe_Eingabe_ZufuehrungsdatumJetzt = new System.Windows.Forms.CheckBox();
			this.chb_Eingabegruppe_Eingabe_AnforderungsdatumJetzt = new System.Windows.Forms.CheckBox();
			this.lbl_Eingabegruppe_Eingabe_Menge = new System.Windows.Forms.Label();
			this.txt_Eingabegruppe_Eingabe_Menge = new System.Windows.Forms.TextBox();
			this.cmb_Eingabegruppe_Eingabe_Gut = new System.Windows.Forms.ComboBox();
			this.lbl_Eingabegruppe_Eingabe_Gut = new System.Windows.Forms.Label();
			this.dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum = new System.Windows.Forms.DateTimePicker();
			this.dtp_Eingabegruppe_Eingabe_Anforderungsdatum = new System.Windows.Forms.DateTimePicker();
			this.lbl_Eingabegruppe_Eingabe_Zuführungsdatum = new System.Windows.Forms.Label();
			this.lbl_Eingabegruppe_Eingabe_Anforderungsdatum = new System.Windows.Forms.Label();
			this.cmb_Eingabegruppe_Eingabe_Anforderungsstatus = new System.Windows.Forms.ComboBox();
			this.lbl_Eingabegruppe_Eingabe_Anforderungsstatus = new System.Windows.Forms.Label();
			this.lbl_Eingabegruppe_Eingabe_AnforderndeKraft = new System.Windows.Forms.Label();
			this.lbl_Eingabegruppe_Eingabe_Zweck = new System.Windows.Forms.Label();
			this.txt_Eingabegruppe_Eingabe_Zweck = new System.Windows.Forms.TextBox();
			this.txt_Eingabegruppe_Eingabe_AnforderndeKraft = new System.Windows.Forms.TextBox();
			this.grp_Eingabegruppe_Bemerkungsgruppe = new System.Windows.Forms.GroupBox();
			this.grp_Eingabegruppe_Bemerkungsgruppe_Autor = new System.Windows.Forms.TextBox();
			this.lbl_Eingabegruppe_Bemerkungsgruppe_Autor = new System.Windows.Forms.Label();
			this.txt_Eingabegruppe_Bemerkungsgruppe_Text = new System.Windows.Forms.RichTextBox();
			this.btn_Eingabegruppe_Abbrechen = new System.Windows.Forms.Button();
			this.btn_Eingabegruppe_Zuruecksetzen = new System.Windows.Forms.Button();
			this.btn_Eingabegruppe_Speichern = new System.Windows.Forms.Button();
			this.grp_KngBemGruppe = new System.Windows.Forms.GroupBox();
			this.btn_KngBemGruppe_Speichern = new System.Windows.Forms.Button();
			this.btn_KngBemGruppe_Abbrechen = new System.Windows.Forms.Button();
			this.txt_Autor = new System.Windows.Forms.TextBox();
			this.lbl_KngBemGruppe_Autor = new System.Windows.Forms.Label();
			this.txt_KngBemGruppe_Text = new System.Windows.Forms.RichTextBox();
			this.grp_Listegruppe.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtg_Listegruppe_Gueteranforderungsliste)).BeginInit();
			this.grp_Eingabegruppe.SuspendLayout();
			this.grp_Eingabegruppe_Eingabe.SuspendLayout();
			this.grp_Eingabegruppe_Bemerkungsgruppe.SuspendLayout();
			this.grp_KngBemGruppe.SuspendLayout();
			this.SuspendLayout();
			// 
			// btn_NeuErstellen
			// 
			this.btn_NeuErstellen.Location = new System.Drawing.Point(452, 176);
			this.btn_NeuErstellen.Name = "btn_NeuErstellen";
			this.btn_NeuErstellen.Size = new System.Drawing.Size(96, 32);
			this.btn_NeuErstellen.TabIndex = 66;
			this.btn_NeuErstellen.Text = "<<&Neu Erstellen";
			// 
			// btn_Kennzeichnen
			// 
			this.btn_Kennzeichnen.Enabled = false;
			this.btn_Kennzeichnen.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.btn_Kennzeichnen.Location = new System.Drawing.Point(548, 160);
			this.btn_Kennzeichnen.Name = "btn_Kennzeichnen";
			this.btn_Kennzeichnen.Size = new System.Drawing.Size(85, 50);
			this.btn_Kennzeichnen.TabIndex = 65;
			this.btn_Kennzeichnen.Text = "als \"bearbeitet\" kennzeichnen";
			// 
			// btn_Splitten
			// 
			this.btn_Splitten.Location = new System.Drawing.Point(548, 208);
			this.btn_Splitten.Name = "btn_Splitten";
			this.btn_Splitten.Size = new System.Drawing.Size(85, 25);
			this.btn_Splitten.TabIndex = 62;
			this.btn_Splitten.Text = "Splitten";
			// 
			// btn_Loeschen
			// 
			this.btn_Loeschen.Enabled = false;
			this.btn_Loeschen.Location = new System.Drawing.Point(540, 228);
			this.btn_Loeschen.Name = "btn_Loeschen";
			this.btn_Loeschen.Size = new System.Drawing.Size(95, 25);
			this.btn_Loeschen.TabIndex = 61;
			this.btn_Loeschen.Text = "&Löschen";
			this.btn_Loeschen.Visible = false;
			// 
			// grp_Listegruppe
			// 
			this.grp_Listegruppe.Controls.Add(this.dtg_Listegruppe_Gueteranforderungsliste);
			this.grp_Listegruppe.Controls.Add(this.btn_Loeschen);
			this.grp_Listegruppe.Controls.Add(this.chb_Edit);
			this.grp_Listegruppe.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.grp_Listegruppe.Location = new System.Drawing.Point(0, 244);
			this.grp_Listegruppe.Name = "grp_Listegruppe";
			this.grp_Listegruppe.Size = new System.Drawing.Size(642, 260);
			this.grp_Listegruppe.TabIndex = 60;
			this.grp_Listegruppe.TabStop = false;
			this.grp_Listegruppe.Text = "Güteranforderungsliste";
			// 
			// dtg_Listegruppe_Gueteranforderungsliste
			// 
			this.dtg_Listegruppe_Gueteranforderungsliste.DataMember = "";
			this.dtg_Listegruppe_Gueteranforderungsliste.Dock = System.Windows.Forms.DockStyle.Top;
			this.dtg_Listegruppe_Gueteranforderungsliste.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtg_Listegruppe_Gueteranforderungsliste.Location = new System.Drawing.Point(3, 16);
			this.dtg_Listegruppe_Gueteranforderungsliste.Name = "dtg_Listegruppe_Gueteranforderungsliste";
			this.dtg_Listegruppe_Gueteranforderungsliste.Size = new System.Drawing.Size(636, 208);
			this.dtg_Listegruppe_Gueteranforderungsliste.TabIndex = 0;
			// 
			// chb_Edit
			// 
			this.chb_Edit.Location = new System.Drawing.Point(448, 232);
			this.chb_Edit.Name = "chb_Edit";
			this.chb_Edit.Size = new System.Drawing.Size(80, 20);
			this.chb_Edit.TabIndex = 63;
			this.chb_Edit.Text = "Bearbeiten";
			// 
			// grp_Eingabegruppe
			// 
			this.grp_Eingabegruppe.Controls.Add(this.grp_Eingabegruppe_Eingabe);
			this.grp_Eingabegruppe.Controls.Add(this.grp_Eingabegruppe_Bemerkungsgruppe);
			this.grp_Eingabegruppe.Controls.Add(this.btn_Eingabegruppe_Abbrechen);
			this.grp_Eingabegruppe.Controls.Add(this.btn_Eingabegruppe_Zuruecksetzen);
			this.grp_Eingabegruppe.Controls.Add(this.btn_Eingabegruppe_Speichern);
			this.grp_Eingabegruppe.Enabled = false;
			this.grp_Eingabegruppe.Location = new System.Drawing.Point(4, 0);
			this.grp_Eingabegruppe.Name = "grp_Eingabegruppe";
			this.grp_Eingabegruppe.Size = new System.Drawing.Size(445, 235);
			this.grp_Eingabegruppe.TabIndex = 59;
			this.grp_Eingabegruppe.TabStop = false;
			// 
			// grp_Eingabegruppe_Eingabe
			// 
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.chb_Eingabegruppe_Eingabe_ZufuehrungsdatumJetzt);
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.chb_Eingabegruppe_Eingabe_AnforderungsdatumJetzt);
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.lbl_Eingabegruppe_Eingabe_Menge);
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.txt_Eingabegruppe_Eingabe_Menge);
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.cmb_Eingabegruppe_Eingabe_Gut);
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.lbl_Eingabegruppe_Eingabe_Gut);
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum);
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.dtp_Eingabegruppe_Eingabe_Anforderungsdatum);
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.lbl_Eingabegruppe_Eingabe_Zuführungsdatum);
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.lbl_Eingabegruppe_Eingabe_Anforderungsdatum);
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.cmb_Eingabegruppe_Eingabe_Anforderungsstatus);
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.lbl_Eingabegruppe_Eingabe_Anforderungsstatus);
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.lbl_Eingabegruppe_Eingabe_AnforderndeKraft);
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.lbl_Eingabegruppe_Eingabe_Zweck);
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.txt_Eingabegruppe_Eingabe_Zweck);
			this.grp_Eingabegruppe_Eingabe.Controls.Add(this.txt_Eingabegruppe_Eingabe_AnforderndeKraft);
			this.grp_Eingabegruppe_Eingabe.Location = new System.Drawing.Point(5, 10);
			this.grp_Eingabegruppe_Eingabe.Name = "grp_Eingabegruppe_Eingabe";
			this.grp_Eingabegruppe_Eingabe.Size = new System.Drawing.Size(270, 190);
			this.grp_Eingabegruppe_Eingabe.TabIndex = 1;
			this.grp_Eingabegruppe_Eingabe.TabStop = false;
			this.grp_Eingabegruppe_Eingabe.Text = "Anforderung";
			// 
			// chb_Eingabegruppe_Eingabe_ZufuehrungsdatumJetzt
			// 
			this.chb_Eingabegruppe_Eingabe_ZufuehrungsdatumJetzt.Checked = true;
			this.chb_Eingabegruppe_Eingabe_ZufuehrungsdatumJetzt.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chb_Eingabegruppe_Eingabe_ZufuehrungsdatumJetzt.Location = new System.Drawing.Point(115, 170);
			this.chb_Eingabegruppe_Eingabe_ZufuehrungsdatumJetzt.Name = "chb_Eingabegruppe_Eingabe_ZufuehrungsdatumJetzt";
			this.chb_Eingabegruppe_Eingabe_ZufuehrungsdatumJetzt.Size = new System.Drawing.Size(50, 15);
			this.chb_Eingabegruppe_Eingabe_ZufuehrungsdatumJetzt.TabIndex = 56;
			this.chb_Eingabegruppe_Eingabe_ZufuehrungsdatumJetzt.Text = "Jetzt";
			// 
			// chb_Eingabegruppe_Eingabe_AnforderungsdatumJetzt
			// 
			this.chb_Eingabegruppe_Eingabe_AnforderungsdatumJetzt.Checked = true;
			this.chb_Eingabegruppe_Eingabe_AnforderungsdatumJetzt.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chb_Eingabegruppe_Eingabe_AnforderungsdatumJetzt.Location = new System.Drawing.Point(115, 135);
			this.chb_Eingabegruppe_Eingabe_AnforderungsdatumJetzt.Name = "chb_Eingabegruppe_Eingabe_AnforderungsdatumJetzt";
			this.chb_Eingabegruppe_Eingabe_AnforderungsdatumJetzt.Size = new System.Drawing.Size(50, 15);
			this.chb_Eingabegruppe_Eingabe_AnforderungsdatumJetzt.TabIndex = 55;
			this.chb_Eingabegruppe_Eingabe_AnforderungsdatumJetzt.Text = "Jetzt";
			// 
			// lbl_Eingabegruppe_Eingabe_Menge
			// 
			this.lbl_Eingabegruppe_Eingabe_Menge.Location = new System.Drawing.Point(5, 35);
			this.lbl_Eingabegruppe_Eingabe_Menge.Name = "lbl_Eingabegruppe_Eingabe_Menge";
			this.lbl_Eingabegruppe_Eingabe_Menge.Size = new System.Drawing.Size(47, 15);
			this.lbl_Eingabegruppe_Eingabe_Menge.TabIndex = 54;
			this.lbl_Eingabegruppe_Eingabe_Menge.Text = "Menge:";
			// 
			// txt_Eingabegruppe_Eingabe_Menge
			// 
			this.txt_Eingabegruppe_Eingabe_Menge.Location = new System.Drawing.Point(115, 35);
			this.txt_Eingabegruppe_Eingabe_Menge.MaxLength = 50;
			this.txt_Eingabegruppe_Eingabe_Menge.Name = "txt_Eingabegruppe_Eingabe_Menge";
			this.txt_Eingabegruppe_Eingabe_Menge.Size = new System.Drawing.Size(150, 20);
			this.txt_Eingabegruppe_Eingabe_Menge.TabIndex = 53;
			this.txt_Eingabegruppe_Eingabe_Menge.Text = "";
			// 
			// cmb_Eingabegruppe_Eingabe_Gut
			// 
			this.cmb_Eingabegruppe_Eingabe_Gut.ItemHeight = 13;
			this.cmb_Eingabegruppe_Eingabe_Gut.Items.AddRange(new object[] {
																			   "Benzin",
																			   "Gas"});
			this.cmb_Eingabegruppe_Eingabe_Gut.Location = new System.Drawing.Point(115, 15);
			this.cmb_Eingabegruppe_Eingabe_Gut.Name = "cmb_Eingabegruppe_Eingabe_Gut";
			this.cmb_Eingabegruppe_Eingabe_Gut.Size = new System.Drawing.Size(150, 21);
			this.cmb_Eingabegruppe_Eingabe_Gut.TabIndex = 52;
			// 
			// lbl_Eingabegruppe_Eingabe_Gut
			// 
			this.lbl_Eingabegruppe_Eingabe_Gut.Location = new System.Drawing.Point(5, 15);
			this.lbl_Eingabegruppe_Eingabe_Gut.Name = "lbl_Eingabegruppe_Eingabe_Gut";
			this.lbl_Eingabegruppe_Eingabe_Gut.Size = new System.Drawing.Size(37, 15);
			this.lbl_Eingabegruppe_Eingabe_Gut.TabIndex = 51;
			this.lbl_Eingabegruppe_Eingabe_Gut.Text = "Gut:";
			// 
			// dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum
			// 
			this.dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum.Enabled = false;
			this.dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum.Location = new System.Drawing.Point(115, 150);
			this.dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum.MinDate = new System.DateTime(2004, 11, 2, 0, 0, 0, 0);
			this.dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum.Name = "dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum";
			this.dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum.Size = new System.Drawing.Size(150, 20);
			this.dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum.TabIndex = 50;
			this.dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum.Value = new System.DateTime(2004, 11, 24, 10, 49, 48, 622);
			// 
			// dtp_Eingabegruppe_Eingabe_Anforderungsdatum
			// 
			this.dtp_Eingabegruppe_Eingabe_Anforderungsdatum.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_Eingabegruppe_Eingabe_Anforderungsdatum.Enabled = false;
			this.dtp_Eingabegruppe_Eingabe_Anforderungsdatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Eingabegruppe_Eingabe_Anforderungsdatum.Location = new System.Drawing.Point(115, 115);
			this.dtp_Eingabegruppe_Eingabe_Anforderungsdatum.MinDate = new System.DateTime(2004, 11, 2, 0, 0, 0, 0);
			this.dtp_Eingabegruppe_Eingabe_Anforderungsdatum.Name = "dtp_Eingabegruppe_Eingabe_Anforderungsdatum";
			this.dtp_Eingabegruppe_Eingabe_Anforderungsdatum.Size = new System.Drawing.Size(150, 20);
			this.dtp_Eingabegruppe_Eingabe_Anforderungsdatum.TabIndex = 49;
			this.dtp_Eingabegruppe_Eingabe_Anforderungsdatum.Value = new System.DateTime(2004, 11, 24, 10, 49, 48, 622);
			// 
			// lbl_Eingabegruppe_Eingabe_Zuführungsdatum
			// 
			this.lbl_Eingabegruppe_Eingabe_Zuführungsdatum.Location = new System.Drawing.Point(5, 150);
			this.lbl_Eingabegruppe_Eingabe_Zuführungsdatum.Name = "lbl_Eingabegruppe_Eingabe_Zuführungsdatum";
			this.lbl_Eingabegruppe_Eingabe_Zuführungsdatum.Size = new System.Drawing.Size(104, 15);
			this.lbl_Eingabegruppe_Eingabe_Zuführungsdatum.TabIndex = 46;
			this.lbl_Eingabegruppe_Eingabe_Zuführungsdatum.Text = "Zuführungsdatum:";
			// 
			// lbl_Eingabegruppe_Eingabe_Anforderungsdatum
			// 
			this.lbl_Eingabegruppe_Eingabe_Anforderungsdatum.Location = new System.Drawing.Point(5, 115);
			this.lbl_Eingabegruppe_Eingabe_Anforderungsdatum.Name = "lbl_Eingabegruppe_Eingabe_Anforderungsdatum";
			this.lbl_Eingabegruppe_Eingabe_Anforderungsdatum.Size = new System.Drawing.Size(112, 15);
			this.lbl_Eingabegruppe_Eingabe_Anforderungsdatum.TabIndex = 45;
			this.lbl_Eingabegruppe_Eingabe_Anforderungsdatum.Text = "Anforderungsdatum: ";
			// 
			// cmb_Eingabegruppe_Eingabe_Anforderungsstatus
			// 
			this.cmb_Eingabegruppe_Eingabe_Anforderungsstatus.ItemHeight = 13;
			this.cmb_Eingabegruppe_Eingabe_Anforderungsstatus.Items.AddRange(new object[] {
																							  "neu",
																							  "offen",
																							  "bearbeitet",
																							  "abgewiesen"});
			this.cmb_Eingabegruppe_Eingabe_Anforderungsstatus.Location = new System.Drawing.Point(115, 95);
			this.cmb_Eingabegruppe_Eingabe_Anforderungsstatus.Name = "cmb_Eingabegruppe_Eingabe_Anforderungsstatus";
			this.cmb_Eingabegruppe_Eingabe_Anforderungsstatus.Size = new System.Drawing.Size(150, 21);
			this.cmb_Eingabegruppe_Eingabe_Anforderungsstatus.TabIndex = 44;
			// 
			// lbl_Eingabegruppe_Eingabe_Anforderungsstatus
			// 
			this.lbl_Eingabegruppe_Eingabe_Anforderungsstatus.Location = new System.Drawing.Point(5, 95);
			this.lbl_Eingabegruppe_Eingabe_Anforderungsstatus.Name = "lbl_Eingabegruppe_Eingabe_Anforderungsstatus";
			this.lbl_Eingabegruppe_Eingabe_Anforderungsstatus.Size = new System.Drawing.Size(107, 15);
			this.lbl_Eingabegruppe_Eingabe_Anforderungsstatus.TabIndex = 43;
			this.lbl_Eingabegruppe_Eingabe_Anforderungsstatus.Text = "Anforderungsstatus::";
			// 
			// lbl_Eingabegruppe_Eingabe_AnforderndeKraft
			// 
			this.lbl_Eingabegruppe_Eingabe_AnforderndeKraft.Location = new System.Drawing.Point(5, 55);
			this.lbl_Eingabegruppe_Eingabe_AnforderndeKraft.Name = "lbl_Eingabegruppe_Eingabe_AnforderndeKraft";
			this.lbl_Eingabegruppe_Eingabe_AnforderndeKraft.Size = new System.Drawing.Size(97, 15);
			this.lbl_Eingabegruppe_Eingabe_AnforderndeKraft.TabIndex = 37;
			this.lbl_Eingabegruppe_Eingabe_AnforderndeKraft.Text = "Anfordernde Kraft:";
			// 
			// lbl_Eingabegruppe_Eingabe_Zweck
			// 
			this.lbl_Eingabegruppe_Eingabe_Zweck.Location = new System.Drawing.Point(5, 75);
			this.lbl_Eingabegruppe_Eingabe_Zweck.Name = "lbl_Eingabegruppe_Eingabe_Zweck";
			this.lbl_Eingabegruppe_Eingabe_Zweck.Size = new System.Drawing.Size(52, 15);
			this.lbl_Eingabegruppe_Eingabe_Zweck.TabIndex = 38;
			this.lbl_Eingabegruppe_Eingabe_Zweck.Text = "Zweck:";
			// 
			// txt_Eingabegruppe_Eingabe_Zweck
			// 
			this.txt_Eingabegruppe_Eingabe_Zweck.Location = new System.Drawing.Point(115, 75);
			this.txt_Eingabegruppe_Eingabe_Zweck.MaxLength = 50;
			this.txt_Eingabegruppe_Eingabe_Zweck.Name = "txt_Eingabegruppe_Eingabe_Zweck";
			this.txt_Eingabegruppe_Eingabe_Zweck.Size = new System.Drawing.Size(150, 20);
			this.txt_Eingabegruppe_Eingabe_Zweck.TabIndex = 41;
			this.txt_Eingabegruppe_Eingabe_Zweck.Text = "";
			// 
			// txt_Eingabegruppe_Eingabe_AnforderndeKraft
			// 
			this.txt_Eingabegruppe_Eingabe_AnforderndeKraft.Location = new System.Drawing.Point(115, 55);
			this.txt_Eingabegruppe_Eingabe_AnforderndeKraft.MaxLength = 50;
			this.txt_Eingabegruppe_Eingabe_AnforderndeKraft.Name = "txt_Eingabegruppe_Eingabe_AnforderndeKraft";
			this.txt_Eingabegruppe_Eingabe_AnforderndeKraft.Size = new System.Drawing.Size(150, 20);
			this.txt_Eingabegruppe_Eingabe_AnforderndeKraft.TabIndex = 40;
			this.txt_Eingabegruppe_Eingabe_AnforderndeKraft.Text = "";
			// 
			// grp_Eingabegruppe_Bemerkungsgruppe
			// 
			this.grp_Eingabegruppe_Bemerkungsgruppe.Controls.Add(this.grp_Eingabegruppe_Bemerkungsgruppe_Autor);
			this.grp_Eingabegruppe_Bemerkungsgruppe.Controls.Add(this.lbl_Eingabegruppe_Bemerkungsgruppe_Autor);
			this.grp_Eingabegruppe_Bemerkungsgruppe.Controls.Add(this.txt_Eingabegruppe_Bemerkungsgruppe_Text);
			this.grp_Eingabegruppe_Bemerkungsgruppe.Location = new System.Drawing.Point(275, 10);
			this.grp_Eingabegruppe_Bemerkungsgruppe.Name = "grp_Eingabegruppe_Bemerkungsgruppe";
			this.grp_Eingabegruppe_Bemerkungsgruppe.Size = new System.Drawing.Size(165, 190);
			this.grp_Eingabegruppe_Bemerkungsgruppe.TabIndex = 54;
			this.grp_Eingabegruppe_Bemerkungsgruppe.TabStop = false;
			this.grp_Eingabegruppe_Bemerkungsgruppe.Text = "Bemerkung";
			// 
			// grp_Eingabegruppe_Bemerkungsgruppe_Autor
			// 
			this.grp_Eingabegruppe_Bemerkungsgruppe_Autor.Location = new System.Drawing.Point(50, 165);
			this.grp_Eingabegruppe_Bemerkungsgruppe_Autor.Name = "grp_Eingabegruppe_Bemerkungsgruppe_Autor";
			this.grp_Eingabegruppe_Bemerkungsgruppe_Autor.Size = new System.Drawing.Size(110, 20);
			this.grp_Eingabegruppe_Bemerkungsgruppe_Autor.TabIndex = 47;
			this.grp_Eingabegruppe_Bemerkungsgruppe_Autor.Text = "<Vorname> <Name>";
			// 
			// lbl_Eingabegruppe_Bemerkungsgruppe_Autor
			// 
			this.lbl_Eingabegruppe_Bemerkungsgruppe_Autor.Location = new System.Drawing.Point(5, 165);
			this.lbl_Eingabegruppe_Bemerkungsgruppe_Autor.Name = "lbl_Eingabegruppe_Bemerkungsgruppe_Autor";
			this.lbl_Eingabegruppe_Bemerkungsgruppe_Autor.Size = new System.Drawing.Size(40, 15);
			this.lbl_Eingabegruppe_Bemerkungsgruppe_Autor.TabIndex = 46;
			this.lbl_Eingabegruppe_Bemerkungsgruppe_Autor.Text = "von:";
			this.lbl_Eingabegruppe_Bemerkungsgruppe_Autor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt_Eingabegruppe_Bemerkungsgruppe_Text
			// 
			this.txt_Eingabegruppe_Bemerkungsgruppe_Text.Location = new System.Drawing.Point(5, 15);
			this.txt_Eingabegruppe_Bemerkungsgruppe_Text.Name = "txt_Eingabegruppe_Bemerkungsgruppe_Text";
			this.txt_Eingabegruppe_Bemerkungsgruppe_Text.Size = new System.Drawing.Size(155, 145);
			this.txt_Eingabegruppe_Bemerkungsgruppe_Text.TabIndex = 44;
			this.txt_Eingabegruppe_Bemerkungsgruppe_Text.Text = "";
			// 
			// btn_Eingabegruppe_Abbrechen
			// 
			this.btn_Eingabegruppe_Abbrechen.Location = new System.Drawing.Point(180, 205);
			this.btn_Eingabegruppe_Abbrechen.Name = "btn_Eingabegruppe_Abbrechen";
			this.btn_Eingabegruppe_Abbrechen.Size = new System.Drawing.Size(75, 25);
			this.btn_Eingabegruppe_Abbrechen.TabIndex = 42;
			this.btn_Eingabegruppe_Abbrechen.Text = "&Abbrechen";
			// 
			// btn_Eingabegruppe_Zuruecksetzen
			// 
			this.btn_Eingabegruppe_Zuruecksetzen.Location = new System.Drawing.Point(5, 205);
			this.btn_Eingabegruppe_Zuruecksetzen.Name = "btn_Eingabegruppe_Zuruecksetzen";
			this.btn_Eingabegruppe_Zuruecksetzen.Size = new System.Drawing.Size(81, 25);
			this.btn_Eingabegruppe_Zuruecksetzen.TabIndex = 22;
			this.btn_Eingabegruppe_Zuruecksetzen.Text = "&Zurücksetzen";
			// 
			// btn_Eingabegruppe_Speichern
			// 
			this.btn_Eingabegruppe_Speichern.Location = new System.Drawing.Point(95, 205);
			this.btn_Eingabegruppe_Speichern.Name = "btn_Eingabegruppe_Speichern";
			this.btn_Eingabegruppe_Speichern.Size = new System.Drawing.Size(75, 25);
			this.btn_Eingabegruppe_Speichern.TabIndex = 21;
			this.btn_Eingabegruppe_Speichern.Text = "&Speichern";
			// 
			// grp_KngBemGruppe
			// 
			this.grp_KngBemGruppe.Controls.Add(this.btn_KngBemGruppe_Speichern);
			this.grp_KngBemGruppe.Controls.Add(this.btn_KngBemGruppe_Abbrechen);
			this.grp_KngBemGruppe.Controls.Add(this.txt_Autor);
			this.grp_KngBemGruppe.Controls.Add(this.lbl_KngBemGruppe_Autor);
			this.grp_KngBemGruppe.Controls.Add(this.txt_KngBemGruppe_Text);
			this.grp_KngBemGruppe.Location = new System.Drawing.Point(444, 8);
			this.grp_KngBemGruppe.Name = "grp_KngBemGruppe";
			this.grp_KngBemGruppe.Size = new System.Drawing.Size(185, 145);
			this.grp_KngBemGruppe.TabIndex = 64;
			this.grp_KngBemGruppe.TabStop = false;
			this.grp_KngBemGruppe.Text = "Bemerkung";
			this.grp_KngBemGruppe.Visible = false;
			// 
			// btn_KngBemGruppe_Speichern
			// 
			this.btn_KngBemGruppe_Speichern.Location = new System.Drawing.Point(90, 115);
			this.btn_KngBemGruppe_Speichern.Name = "btn_KngBemGruppe_Speichern";
			this.btn_KngBemGruppe_Speichern.Size = new System.Drawing.Size(80, 25);
			this.btn_KngBemGruppe_Speichern.TabIndex = 57;
			this.btn_KngBemGruppe_Speichern.Text = "Speichern";
			// 
			// btn_KngBemGruppe_Abbrechen
			// 
			this.btn_KngBemGruppe_Abbrechen.Location = new System.Drawing.Point(10, 115);
			this.btn_KngBemGruppe_Abbrechen.Name = "btn_KngBemGruppe_Abbrechen";
			this.btn_KngBemGruppe_Abbrechen.Size = new System.Drawing.Size(80, 25);
			this.btn_KngBemGruppe_Abbrechen.TabIndex = 56;
			this.btn_KngBemGruppe_Abbrechen.Text = "Abbrechen";
			// 
			// txt_Autor
			// 
			this.txt_Autor.Location = new System.Drawing.Point(70, 95);
			this.txt_Autor.Name = "txt_Autor";
			this.txt_Autor.Size = new System.Drawing.Size(110, 20);
			this.txt_Autor.TabIndex = 47;
			this.txt_Autor.Text = "<Vorname> <Name>";
			// 
			// lbl_KngBemGruppe_Autor
			// 
			this.lbl_KngBemGruppe_Autor.Location = new System.Drawing.Point(10, 95);
			this.lbl_KngBemGruppe_Autor.Name = "lbl_KngBemGruppe_Autor";
			this.lbl_KngBemGruppe_Autor.Size = new System.Drawing.Size(55, 15);
			this.lbl_KngBemGruppe_Autor.TabIndex = 46;
			this.lbl_KngBemGruppe_Autor.Text = "von:";
			this.lbl_KngBemGruppe_Autor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txt_KngBemGruppe_Text
			// 
			this.txt_KngBemGruppe_Text.Location = new System.Drawing.Point(5, 15);
			this.txt_KngBemGruppe_Text.Name = "txt_KngBemGruppe_Text";
			this.txt_KngBemGruppe_Text.Size = new System.Drawing.Size(175, 79);
			this.txt_KngBemGruppe_Text.TabIndex = 44;
			this.txt_KngBemGruppe_Text.Text = "";
			// 
			// Cpr_usc_AEuB
			// 
			this.Controls.Add(this.btn_NeuErstellen);
			this.Controls.Add(this.btn_Kennzeichnen);
			this.Controls.Add(this.btn_Splitten);
			this.Controls.Add(this.grp_Listegruppe);
			this.Controls.Add(this.grp_Eingabegruppe);
			this.Controls.Add(this.grp_KngBemGruppe);
			this.Name = "Cpr_usc_AEuB";
			this.Size = new System.Drawing.Size(642, 504);
			this.grp_Listegruppe.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtg_Listegruppe_Gueteranforderungsliste)).EndInit();
			this.grp_Eingabegruppe.ResumeLayout(false);
			this.grp_Eingabegruppe_Eingabe.ResumeLayout(false);
			this.grp_Eingabegruppe_Bemerkungsgruppe.ResumeLayout(false);
			this.grp_KngBemGruppe.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region events
		private void chb_Eingabegruppe_Eingabe_AnforderungsdatumJetzt_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.chb_Eingabegruppe_Eingabe_AnforderungsdatumJetzt.Checked == false)
			{
				this.dtp_Eingabegruppe_Eingabe_Anforderungsdatum.Enabled = true;
			}
			else
			{
				this.dtp_Eingabegruppe_Eingabe_Anforderungsdatum.Enabled = false;
			}
		}

		private void chb_Eingabegruppe_Eingabe_ZufuehrungsdatumJetzt_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.chb_Eingabegruppe_Eingabe_ZufuehrungsdatumJetzt.Checked == false)
			{
				this.dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum.Enabled = true;
			}
			else
			{
				this.dtp_Eingabegruppe_Eingabe_Zufuehrungsdatum.Enabled = false;
			}
		}

		private void btn_Eingabegruppe_Speichern_Click(object sender, System.EventArgs e)
		{
			
		}

		private void btn_Eingabegruppe_Abbrechen_Click(object sender, System.EventArgs e)
		{
			
		}

		
		// vorbedingung: sobald ein Eintrag ausgewaehlt wird, wird das Button erst aktiv		
		private void btn_Eingabegruppe_Loeschen_Click(object sender, System.EventArgs e)
		{
		
		}

		private void chb_Edit_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.grp_Eingabegruppe.Enabled == true)
			{
				this.grp_Eingabegruppe.Enabled = false;
			}
			else
			{
				this.grp_Eingabegruppe.Enabled = true;
				// bei der Aenderung werden zwei Daten nicht verandert
				this.chb_Eingabegruppe_Eingabe_AnforderungsdatumJetzt.Enabled = false;
				this.chb_Eingabegruppe_Eingabe_ZufuehrungsdatumJetzt.Enabled = false;
			}
			


		}

		#endregion




	}
}
