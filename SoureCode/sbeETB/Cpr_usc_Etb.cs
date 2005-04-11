using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using pELS.Client;


#region Dokumentation
/**				aktuelle Version: 0.1 Quecky
INFO:
	TODO: Muss noch ergänzt werden
**/
#region Member-Doku
/**		
 
**/
#endregion			

#region letzte Änderungen
/**
erstellt von: Quecky					am: 18.11.2004
geändert von: Quecky					am: 23.11.2004
  review von: alexG						am: 5.12.2004
getestet von:			am:
**/
#endregion

#region History/Hinweise/Bekannte Bugs:
/**
History:
	- 18.11.	- Regionen angelegt
				- Rechtevergabe durch Rollen vollständig
	- 23.11		- GUI soweit ich es überblicke fertig

Hinweise/Bekannte Bugs:

**/
#endregion

#endregion

namespace pELS.Client.Etb		  
{

	//Für Navigation in PopUp
	using pELS.GUI;
	//Für alle pELSObjekte
	using pELS.DV;

	public class Cpr_usc_Etb : System.Windows.Forms.UserControl
	{
		#region Klassenvariablen

		private System.Windows.Forms.TabPage tabpage_ETB;
		private System.Windows.Forms.TabControl tabctrl_ETB;
		private System.Windows.Forms.TabPage tabpage_Zusatzeintrag;
		private System.Windows.Forms.TabPage tabpage_Systemereignisse;
		private System.Windows.Forms.CheckedListBox lbx_Systemereignisse_Liste;
		private System.Windows.Forms.GroupBox grp_Systemereignisse_Liste;
		private System.Windows.Forms.DateTimePicker dtp_Zusatzeintrag_Datum;
		private System.Windows.Forms.RichTextBox txt_Zusatzeintrag_Text;
		private System.Windows.Forms.TextBox txt_Zusatzeintrag_Autor;
		private System.Windows.Forms.GroupBox grp_Zusatzeintrag;
		private System.Windows.Forms.GroupBox grp_Zusatzeintrag_Liste;
		private System.Windows.Forms.ListBox lbx_Zusatzeintrag_Liste;
		private System.Windows.Forms.Button btn_Zusatzeintrag_Verwerfen;
		private System.Windows.Forms.Button btn_Zusatzeintrag_Speichern;
		private System.Windows.Forms.GroupBox grp_Zusatzeintrag_Kommentar;
		private System.Windows.Forms.ComboBox cmb_Zusatzeintrag_AlteKommentare;
		private System.Windows.Forms.Button btn_Zusatzeintrag_NeuerKommentar;
		private System.Windows.Forms.Button btn_Zusatzeintrag_Kommentar_Speichern;
		private System.Windows.Forms.DateTimePicker dtp_Zusatzeintrag_Kommentar_Datum;
		private System.Windows.Forms.TextBox txt_Zusatzeintrag_Kommentar_Autor;
		private System.Windows.Forms.RichTextBox txt_Zusatzeintrag_Kommentar_Text;
		private System.Windows.Forms.RichTextBox txt_Systemereignisse_Beschreibung;
		private System.Windows.Forms.GroupBox grp_Systemereignisse_Auswahl;
		private System.Windows.Forms.GroupBox grp_Systemereignis_Kommentar;
		private System.Windows.Forms.Button btn_Systemereignis_Kommentar_Speichern;
		private System.Windows.Forms.DateTimePicker dtp_Systemereignis_Kommentar_Datum;
		private System.Windows.Forms.TextBox txt_Systemereignis_Kommentar_Autor;
		private System.Windows.Forms.RichTextBox txt_Systemereignis_Kommentar_Text;
		private System.Windows.Forms.Button btn_Systemereignis_Kommentar_NeuerKommentar;
		private System.Windows.Forms.ComboBox cmb_Systemereignis_Kommentar_AlteKommentare;
	

		#endregion
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button btn_Systemereignis_Kommentar_Verwerfen;
		private System.Windows.Forms.Label lbl_ausgewaehltesSystemereignis_Info;
		private System.Windows.Forms.Button btn_Zusatzeintrag_Kommentar_Verwerfen;
		private System.Windows.Forms.CheckBox cbx_Systemereignis_Kommentar_ErscheintInEtb;
		private System.Windows.Forms.CheckBox cbx_Zusatzeintrag_Kommentar_ErscheintInEtb;
		private AxPdfLib.AxPdf pdfViewer;
		private System.Windows.Forms.Button btn_Zusatzeintrag_Neu;
		private System.Windows.Forms.ErrorProvider ep_LeereFelder;
		private System.Windows.Forms.Button btn_erneut_laden;
		private System.Windows.Forms.HelpProvider pelsHelp;

		#region eigene Instanzvariablen
		private Cst_Etb _st_etb;
		#endregion

		#region Konstruktoren und Destruktoren
		public Cpr_usc_Etb(Cst_Etb pin_st_etb)
		{
			this._st_etb = pin_st_etb;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
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
		#endregion
		
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Cpr_usc_Etb));
			this.tabctrl_ETB = new System.Windows.Forms.TabControl();
			this.tabpage_ETB = new System.Windows.Forms.TabPage();
			this.btn_erneut_laden = new System.Windows.Forms.Button();
			this.pdfViewer = new AxPdfLib.AxPdf();
			this.tabpage_Systemereignisse = new System.Windows.Forms.TabPage();
			this.grp_Systemereignisse_Liste = new System.Windows.Forms.GroupBox();
			this.lbx_Systemereignisse_Liste = new System.Windows.Forms.CheckedListBox();
			this.grp_Systemereignisse_Auswahl = new System.Windows.Forms.GroupBox();
			this.lbl_ausgewaehltesSystemereignis_Info = new System.Windows.Forms.Label();
			this.txt_Systemereignisse_Beschreibung = new System.Windows.Forms.RichTextBox();
			this.grp_Systemereignis_Kommentar = new System.Windows.Forms.GroupBox();
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb = new System.Windows.Forms.CheckBox();
			this.btn_Systemereignis_Kommentar_Verwerfen = new System.Windows.Forms.Button();
			this.btn_Systemereignis_Kommentar_Speichern = new System.Windows.Forms.Button();
			this.dtp_Systemereignis_Kommentar_Datum = new System.Windows.Forms.DateTimePicker();
			this.txt_Systemereignis_Kommentar_Autor = new System.Windows.Forms.TextBox();
			this.txt_Systemereignis_Kommentar_Text = new System.Windows.Forms.RichTextBox();
			this.btn_Systemereignis_Kommentar_NeuerKommentar = new System.Windows.Forms.Button();
			this.cmb_Systemereignis_Kommentar_AlteKommentare = new System.Windows.Forms.ComboBox();
			this.tabpage_Zusatzeintrag = new System.Windows.Forms.TabPage();
			this.grp_Zusatzeintrag_Kommentar = new System.Windows.Forms.GroupBox();
			this.cbx_Zusatzeintrag_Kommentar_ErscheintInEtb = new System.Windows.Forms.CheckBox();
			this.btn_Zusatzeintrag_Kommentar_Verwerfen = new System.Windows.Forms.Button();
			this.btn_Zusatzeintrag_Kommentar_Speichern = new System.Windows.Forms.Button();
			this.dtp_Zusatzeintrag_Kommentar_Datum = new System.Windows.Forms.DateTimePicker();
			this.txt_Zusatzeintrag_Kommentar_Autor = new System.Windows.Forms.TextBox();
			this.txt_Zusatzeintrag_Kommentar_Text = new System.Windows.Forms.RichTextBox();
			this.btn_Zusatzeintrag_NeuerKommentar = new System.Windows.Forms.Button();
			this.cmb_Zusatzeintrag_AlteKommentare = new System.Windows.Forms.ComboBox();
			this.grp_Zusatzeintrag_Liste = new System.Windows.Forms.GroupBox();
			this.lbx_Zusatzeintrag_Liste = new System.Windows.Forms.ListBox();
			this.btn_Zusatzeintrag_Neu = new System.Windows.Forms.Button();
			this.grp_Zusatzeintrag = new System.Windows.Forms.GroupBox();
			this.btn_Zusatzeintrag_Verwerfen = new System.Windows.Forms.Button();
			this.btn_Zusatzeintrag_Speichern = new System.Windows.Forms.Button();
			this.dtp_Zusatzeintrag_Datum = new System.Windows.Forms.DateTimePicker();
			this.txt_Zusatzeintrag_Autor = new System.Windows.Forms.TextBox();
			this.txt_Zusatzeintrag_Text = new System.Windows.Forms.RichTextBox();
			this.ep_LeereFelder = new System.Windows.Forms.ErrorProvider();
			this.pelsHelp = new System.Windows.Forms.HelpProvider();
			this.tabctrl_ETB.SuspendLayout();
			this.tabpage_ETB.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pdfViewer)).BeginInit();
			this.tabpage_Systemereignisse.SuspendLayout();
			this.grp_Systemereignisse_Liste.SuspendLayout();
			this.grp_Systemereignisse_Auswahl.SuspendLayout();
			this.grp_Systemereignis_Kommentar.SuspendLayout();
			this.tabpage_Zusatzeintrag.SuspendLayout();
			this.grp_Zusatzeintrag_Kommentar.SuspendLayout();
			this.grp_Zusatzeintrag_Liste.SuspendLayout();
			this.grp_Zusatzeintrag.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabctrl_ETB
			// 
			this.tabctrl_ETB.Controls.Add(this.tabpage_ETB);
			this.tabctrl_ETB.Controls.Add(this.tabpage_Systemereignisse);
			this.tabctrl_ETB.Controls.Add(this.tabpage_Zusatzeintrag);
			this.tabctrl_ETB.Location = new System.Drawing.Point(0, 0);
			this.tabctrl_ETB.Name = "tabctrl_ETB";
			this.tabctrl_ETB.SelectedIndex = 0;
			this.tabctrl_ETB.Size = new System.Drawing.Size(656, 528);
			this.tabctrl_ETB.TabIndex = 5;
			this.tabctrl_ETB.SelectedIndexChanged += new System.EventHandler(this.pdf_ETB_Load);
			// 
			// tabpage_ETB
			// 
			this.tabpage_ETB.Controls.Add(this.btn_erneut_laden);
			this.tabpage_ETB.Controls.Add(this.pdfViewer);
			this.tabpage_ETB.Location = new System.Drawing.Point(4, 22);
			this.tabpage_ETB.Name = "tabpage_ETB";
			this.tabpage_ETB.Size = new System.Drawing.Size(648, 502);
			this.tabpage_ETB.TabIndex = 0;
			this.tabpage_ETB.Text = "Einsatztagebuch";
			// 
			// btn_erneut_laden
			// 
			this.btn_erneut_laden.Location = new System.Drawing.Point(0, 485);
			this.btn_erneut_laden.Name = "btn_erneut_laden";
			this.btn_erneut_laden.Size = new System.Drawing.Size(645, 20);
			this.btn_erneut_laden.TabIndex = 1;
			this.btn_erneut_laden.Text = "ETB Vordruck aktualisieren";
			this.btn_erneut_laden.Click += new System.EventHandler(this.btn_erneut_laden_Click);
			// 
			// pdfViewer
			// 
			this.pdfViewer.ContainingControl = this;
			this.pdfViewer.Enabled = true;
			this.pdfViewer.Location = new System.Drawing.Point(0, 5);
			this.pdfViewer.Name = "pdfViewer";
			this.pdfViewer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("pdfViewer.OcxState")));
			this.pdfViewer.Size = new System.Drawing.Size(645, 480);
			this.pdfViewer.TabIndex = 0;
			// 
			// tabpage_Systemereignisse
			// 
			this.tabpage_Systemereignisse.Controls.Add(this.grp_Systemereignisse_Liste);
			this.tabpage_Systemereignisse.Controls.Add(this.grp_Systemereignisse_Auswahl);
			this.tabpage_Systemereignisse.Controls.Add(this.grp_Systemereignis_Kommentar);
			this.tabpage_Systemereignisse.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Systemereignisse.Name = "tabpage_Systemereignisse";
			this.tabpage_Systemereignisse.Size = new System.Drawing.Size(648, 502);
			this.tabpage_Systemereignisse.TabIndex = 2;
			this.tabpage_Systemereignisse.Text = "Systemereignisse auswählen";
			// 
			// grp_Systemereignisse_Liste
			// 
			this.grp_Systemereignisse_Liste.Controls.Add(this.lbx_Systemereignisse_Liste);
			this.grp_Systemereignisse_Liste.Location = new System.Drawing.Point(8, 10);
			this.grp_Systemereignisse_Liste.Name = "grp_Systemereignisse_Liste";
			this.grp_Systemereignisse_Liste.Size = new System.Drawing.Size(300, 480);
			this.grp_Systemereignisse_Liste.TabIndex = 54;
			this.grp_Systemereignisse_Liste.TabStop = false;
			this.grp_Systemereignisse_Liste.Text = "Systemereignisse";
			// 
			// lbx_Systemereignisse_Liste
			// 
			this.lbx_Systemereignisse_Liste.Location = new System.Drawing.Point(8, 20);
			this.lbx_Systemereignisse_Liste.Name = "lbx_Systemereignisse_Liste";
			this.lbx_Systemereignisse_Liste.Size = new System.Drawing.Size(285, 454);
			this.lbx_Systemereignisse_Liste.TabIndex = 0;
			this.lbx_Systemereignisse_Liste.SelectedIndexChanged += new System.EventHandler(this.lbx_Systemereignisse_Liste_SelectedIndexChanged);
			this.lbx_Systemereignisse_Liste.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lbx_Systemereignisse_Liste_ItemCheck);
			// 
			// grp_Systemereignisse_Auswahl
			// 
			this.grp_Systemereignisse_Auswahl.Controls.Add(this.lbl_ausgewaehltesSystemereignis_Info);
			this.grp_Systemereignisse_Auswahl.Controls.Add(this.txt_Systemereignisse_Beschreibung);
			this.grp_Systemereignisse_Auswahl.Location = new System.Drawing.Point(312, 10);
			this.grp_Systemereignisse_Auswahl.Name = "grp_Systemereignisse_Auswahl";
			this.grp_Systemereignisse_Auswahl.Size = new System.Drawing.Size(312, 215);
			this.grp_Systemereignisse_Auswahl.TabIndex = 53;
			this.grp_Systemereignisse_Auswahl.TabStop = false;
			this.grp_Systemereignisse_Auswahl.Text = "<ausgewähltes Ereigniss>";
			// 
			// lbl_ausgewaehltesSystemereignis_Info
			// 
			this.lbl_ausgewaehltesSystemereignis_Info.Location = new System.Drawing.Point(24, 168);
			this.lbl_ausgewaehltesSystemereignis_Info.Name = "lbl_ausgewaehltesSystemereignis_Info";
			this.lbl_ausgewaehltesSystemereignis_Info.Size = new System.Drawing.Size(264, 40);
			this.lbl_ausgewaehltesSystemereignis_Info.TabIndex = 58;
			// 
			// txt_Systemereignisse_Beschreibung
			// 
			this.ep_LeereFelder.SetIconAlignment(this.txt_Systemereignisse_Beschreibung, System.Windows.Forms.ErrorIconAlignment.TopLeft);
			this.txt_Systemereignisse_Beschreibung.Location = new System.Drawing.Point(16, 16);
			this.txt_Systemereignisse_Beschreibung.Name = "txt_Systemereignisse_Beschreibung";
			this.txt_Systemereignisse_Beschreibung.ReadOnly = true;
			this.txt_Systemereignisse_Beschreibung.Size = new System.Drawing.Size(285, 144);
			this.txt_Systemereignisse_Beschreibung.TabIndex = 43;
			this.txt_Systemereignisse_Beschreibung.Text = "<Hier steht die Bechreibung des Ereignisses>";
			// 
			// grp_Systemereignis_Kommentar
			// 
			this.grp_Systemereignis_Kommentar.Controls.Add(this.cbx_Systemereignis_Kommentar_ErscheintInEtb);
			this.grp_Systemereignis_Kommentar.Controls.Add(this.btn_Systemereignis_Kommentar_Verwerfen);
			this.grp_Systemereignis_Kommentar.Controls.Add(this.btn_Systemereignis_Kommentar_Speichern);
			this.grp_Systemereignis_Kommentar.Controls.Add(this.dtp_Systemereignis_Kommentar_Datum);
			this.grp_Systemereignis_Kommentar.Controls.Add(this.txt_Systemereignis_Kommentar_Autor);
			this.grp_Systemereignis_Kommentar.Controls.Add(this.txt_Systemereignis_Kommentar_Text);
			this.grp_Systemereignis_Kommentar.Controls.Add(this.btn_Systemereignis_Kommentar_NeuerKommentar);
			this.grp_Systemereignis_Kommentar.Controls.Add(this.cmb_Systemereignis_Kommentar_AlteKommentare);
			this.grp_Systemereignis_Kommentar.Enabled = false;
			this.grp_Systemereignis_Kommentar.Location = new System.Drawing.Point(312, 240);
			this.grp_Systemereignis_Kommentar.Name = "grp_Systemereignis_Kommentar";
			this.grp_Systemereignis_Kommentar.Size = new System.Drawing.Size(312, 250);
			this.grp_Systemereignis_Kommentar.TabIndex = 57;
			this.grp_Systemereignis_Kommentar.TabStop = false;
			this.grp_Systemereignis_Kommentar.Text = "Kommentare";
			// 
			// cbx_Systemereignis_Kommentar_ErscheintInEtb
			// 
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb.Checked = true;
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb.Enabled = false;
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb.Location = new System.Drawing.Point(24, 218);
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb.Name = "cbx_Systemereignis_Kommentar_ErscheintInEtb";
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb.Size = new System.Drawing.Size(88, 16);
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb.TabIndex = 57;
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb.Text = "in ETB";
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb.Visible = false;
			// 
			// btn_Systemereignis_Kommentar_Verwerfen
			// 
			this.btn_Systemereignis_Kommentar_Verwerfen.Enabled = false;
			this.btn_Systemereignis_Kommentar_Verwerfen.Location = new System.Drawing.Point(128, 215);
			this.btn_Systemereignis_Kommentar_Verwerfen.Name = "btn_Systemereignis_Kommentar_Verwerfen";
			this.btn_Systemereignis_Kommentar_Verwerfen.Size = new System.Drawing.Size(81, 25);
			this.btn_Systemereignis_Kommentar_Verwerfen.TabIndex = 56;
			this.btn_Systemereignis_Kommentar_Verwerfen.Text = "&Zurücksetzen";
			this.btn_Systemereignis_Kommentar_Verwerfen.Click += new System.EventHandler(this.btn_Systemereignis_Kommentar_Verwerfen_Click);
			// 
			// btn_Systemereignis_Kommentar_Speichern
			// 
			this.btn_Systemereignis_Kommentar_Speichern.Enabled = false;
			this.btn_Systemereignis_Kommentar_Speichern.Location = new System.Drawing.Point(224, 215);
			this.btn_Systemereignis_Kommentar_Speichern.Name = "btn_Systemereignis_Kommentar_Speichern";
			this.btn_Systemereignis_Kommentar_Speichern.Size = new System.Drawing.Size(80, 25);
			this.btn_Systemereignis_Kommentar_Speichern.TabIndex = 55;
			this.btn_Systemereignis_Kommentar_Speichern.Text = "&Speichern";
			this.btn_Systemereignis_Kommentar_Speichern.Click += new System.EventHandler(this.btn_Systemereignis_Kommentar_Speichern_Click);
			// 
			// dtp_Systemereignis_Kommentar_Datum
			// 
			this.dtp_Systemereignis_Kommentar_Datum.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_Systemereignis_Kommentar_Datum.Enabled = false;
			this.dtp_Systemereignis_Kommentar_Datum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Systemereignis_Kommentar_Datum.Location = new System.Drawing.Point(184, 55);
			this.dtp_Systemereignis_Kommentar_Datum.MinDate = new System.DateTime(2004, 11, 2, 0, 0, 0, 0);
			this.dtp_Systemereignis_Kommentar_Datum.Name = "dtp_Systemereignis_Kommentar_Datum";
			this.dtp_Systemereignis_Kommentar_Datum.Size = new System.Drawing.Size(120, 20);
			this.dtp_Systemereignis_Kommentar_Datum.TabIndex = 54;
			// 
			// txt_Systemereignis_Kommentar_Autor
			// 
			this.txt_Systemereignis_Kommentar_Autor.Location = new System.Drawing.Point(24, 55);
			this.txt_Systemereignis_Kommentar_Autor.Name = "txt_Systemereignis_Kommentar_Autor";
			this.txt_Systemereignis_Kommentar_Autor.ReadOnly = true;
			this.txt_Systemereignis_Kommentar_Autor.Size = new System.Drawing.Size(160, 20);
			this.txt_Systemereignis_Kommentar_Autor.TabIndex = 53;
			this.txt_Systemereignis_Kommentar_Autor.Text = "<Benutzer>";
			// 
			// txt_Systemereignis_Kommentar_Text
			// 
			this.txt_Systemereignis_Kommentar_Text.Enabled = false;
			this.ep_LeereFelder.SetIconAlignment(this.txt_Systemereignis_Kommentar_Text, System.Windows.Forms.ErrorIconAlignment.TopLeft);
			this.txt_Systemereignis_Kommentar_Text.Location = new System.Drawing.Point(24, 80);
			this.txt_Systemereignis_Kommentar_Text.Name = "txt_Systemereignis_Kommentar_Text";
			this.txt_Systemereignis_Kommentar_Text.Size = new System.Drawing.Size(280, 130);
			this.txt_Systemereignis_Kommentar_Text.TabIndex = 52;
			this.txt_Systemereignis_Kommentar_Text.Text = "<Eintrag>";
			// 
			// btn_Systemereignis_Kommentar_NeuerKommentar
			// 
			this.btn_Systemereignis_Kommentar_NeuerKommentar.Location = new System.Drawing.Point(192, 20);
			this.btn_Systemereignis_Kommentar_NeuerKommentar.Name = "btn_Systemereignis_Kommentar_NeuerKommentar";
			this.btn_Systemereignis_Kommentar_NeuerKommentar.Size = new System.Drawing.Size(112, 23);
			this.btn_Systemereignis_Kommentar_NeuerKommentar.TabIndex = 1;
			this.btn_Systemereignis_Kommentar_NeuerKommentar.Text = "neuer &Kommentar";
			this.btn_Systemereignis_Kommentar_NeuerKommentar.Click += new System.EventHandler(this.btn_Systemereignis_Kommentar_NeuerKommentar_Click);
			// 
			// cmb_Systemereignis_Kommentar_AlteKommentare
			// 
			this.cmb_Systemereignis_Kommentar_AlteKommentare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Systemereignis_Kommentar_AlteKommentare.Enabled = false;
			this.cmb_Systemereignis_Kommentar_AlteKommentare.Location = new System.Drawing.Point(24, 22);
			this.cmb_Systemereignis_Kommentar_AlteKommentare.Name = "cmb_Systemereignis_Kommentar_AlteKommentare";
			this.cmb_Systemereignis_Kommentar_AlteKommentare.Size = new System.Drawing.Size(160, 21);
			this.cmb_Systemereignis_Kommentar_AlteKommentare.TabIndex = 0;
			this.cmb_Systemereignis_Kommentar_AlteKommentare.SelectedIndexChanged += new System.EventHandler(this.cmb_Systemereignis_Kommentar_AlteKommentare_SelectedIndexChanged);
			// 
			// tabpage_Zusatzeintrag
			// 
			this.tabpage_Zusatzeintrag.Controls.Add(this.grp_Zusatzeintrag_Kommentar);
			this.tabpage_Zusatzeintrag.Controls.Add(this.grp_Zusatzeintrag_Liste);
			this.tabpage_Zusatzeintrag.Controls.Add(this.grp_Zusatzeintrag);
			this.tabpage_Zusatzeintrag.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Zusatzeintrag.Name = "tabpage_Zusatzeintrag";
			this.tabpage_Zusatzeintrag.Size = new System.Drawing.Size(648, 502);
			this.tabpage_Zusatzeintrag.TabIndex = 1;
			this.tabpage_Zusatzeintrag.Text = "Zusatzeinträge bearbeiten";
			// 
			// grp_Zusatzeintrag_Kommentar
			// 
			this.grp_Zusatzeintrag_Kommentar.Controls.Add(this.cbx_Zusatzeintrag_Kommentar_ErscheintInEtb);
			this.grp_Zusatzeintrag_Kommentar.Controls.Add(this.btn_Zusatzeintrag_Kommentar_Verwerfen);
			this.grp_Zusatzeintrag_Kommentar.Controls.Add(this.btn_Zusatzeintrag_Kommentar_Speichern);
			this.grp_Zusatzeintrag_Kommentar.Controls.Add(this.dtp_Zusatzeintrag_Kommentar_Datum);
			this.grp_Zusatzeintrag_Kommentar.Controls.Add(this.txt_Zusatzeintrag_Kommentar_Autor);
			this.grp_Zusatzeintrag_Kommentar.Controls.Add(this.txt_Zusatzeintrag_Kommentar_Text);
			this.grp_Zusatzeintrag_Kommentar.Controls.Add(this.btn_Zusatzeintrag_NeuerKommentar);
			this.grp_Zusatzeintrag_Kommentar.Controls.Add(this.cmb_Zusatzeintrag_AlteKommentare);
			this.grp_Zusatzeintrag_Kommentar.Enabled = false;
			this.grp_Zusatzeintrag_Kommentar.Location = new System.Drawing.Point(312, 240);
			this.grp_Zusatzeintrag_Kommentar.Name = "grp_Zusatzeintrag_Kommentar";
			this.grp_Zusatzeintrag_Kommentar.Size = new System.Drawing.Size(312, 250);
			this.grp_Zusatzeintrag_Kommentar.TabIndex = 56;
			this.grp_Zusatzeintrag_Kommentar.TabStop = false;
			this.grp_Zusatzeintrag_Kommentar.Text = "Kommentare";
			// 
			// cbx_Zusatzeintrag_Kommentar_ErscheintInEtb
			// 
			this.cbx_Zusatzeintrag_Kommentar_ErscheintInEtb.Enabled = false;
			this.cbx_Zusatzeintrag_Kommentar_ErscheintInEtb.Location = new System.Drawing.Point(16, 218);
			this.cbx_Zusatzeintrag_Kommentar_ErscheintInEtb.Name = "cbx_Zusatzeintrag_Kommentar_ErscheintInEtb";
			this.cbx_Zusatzeintrag_Kommentar_ErscheintInEtb.Size = new System.Drawing.Size(88, 16);
			this.cbx_Zusatzeintrag_Kommentar_ErscheintInEtb.TabIndex = 58;
			this.cbx_Zusatzeintrag_Kommentar_ErscheintInEtb.Text = "in ETB";
			this.cbx_Zusatzeintrag_Kommentar_ErscheintInEtb.Visible = false;
			// 
			// btn_Zusatzeintrag_Kommentar_Verwerfen
			// 
			this.btn_Zusatzeintrag_Kommentar_Verwerfen.Enabled = false;
			this.btn_Zusatzeintrag_Kommentar_Verwerfen.Location = new System.Drawing.Point(128, 215);
			this.btn_Zusatzeintrag_Kommentar_Verwerfen.Name = "btn_Zusatzeintrag_Kommentar_Verwerfen";
			this.btn_Zusatzeintrag_Kommentar_Verwerfen.Size = new System.Drawing.Size(81, 25);
			this.btn_Zusatzeintrag_Kommentar_Verwerfen.TabIndex = 56;
			this.btn_Zusatzeintrag_Kommentar_Verwerfen.Text = "&Zurücksetzen";
			this.btn_Zusatzeintrag_Kommentar_Verwerfen.Click += new System.EventHandler(this.btn_Zusatzeintrag_Kommentar_Verwerfen_Click);
			// 
			// btn_Zusatzeintrag_Kommentar_Speichern
			// 
			this.btn_Zusatzeintrag_Kommentar_Speichern.Enabled = false;
			this.btn_Zusatzeintrag_Kommentar_Speichern.Location = new System.Drawing.Point(216, 215);
			this.btn_Zusatzeintrag_Kommentar_Speichern.Name = "btn_Zusatzeintrag_Kommentar_Speichern";
			this.btn_Zusatzeintrag_Kommentar_Speichern.Size = new System.Drawing.Size(80, 25);
			this.btn_Zusatzeintrag_Kommentar_Speichern.TabIndex = 55;
			this.btn_Zusatzeintrag_Kommentar_Speichern.Text = "&Speichern";
			this.btn_Zusatzeintrag_Kommentar_Speichern.Click += new System.EventHandler(this.btn_Zusatzeintrag_Kommentar_Speichern_Click);
			// 
			// dtp_Zusatzeintrag_Kommentar_Datum
			// 
			this.dtp_Zusatzeintrag_Kommentar_Datum.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_Zusatzeintrag_Kommentar_Datum.Enabled = false;
			this.dtp_Zusatzeintrag_Kommentar_Datum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Zusatzeintrag_Kommentar_Datum.Location = new System.Drawing.Point(184, 55);
			this.dtp_Zusatzeintrag_Kommentar_Datum.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp_Zusatzeintrag_Kommentar_Datum.Name = "dtp_Zusatzeintrag_Kommentar_Datum";
			this.dtp_Zusatzeintrag_Kommentar_Datum.Size = new System.Drawing.Size(120, 20);
			this.dtp_Zusatzeintrag_Kommentar_Datum.TabIndex = 54;
			// 
			// txt_Zusatzeintrag_Kommentar_Autor
			// 
			this.txt_Zusatzeintrag_Kommentar_Autor.Location = new System.Drawing.Point(24, 55);
			this.txt_Zusatzeintrag_Kommentar_Autor.Name = "txt_Zusatzeintrag_Kommentar_Autor";
			this.txt_Zusatzeintrag_Kommentar_Autor.ReadOnly = true;
			this.txt_Zusatzeintrag_Kommentar_Autor.Size = new System.Drawing.Size(160, 20);
			this.txt_Zusatzeintrag_Kommentar_Autor.TabIndex = 53;
			this.txt_Zusatzeintrag_Kommentar_Autor.Text = "<Benutzer>";
			// 
			// txt_Zusatzeintrag_Kommentar_Text
			// 
			this.txt_Zusatzeintrag_Kommentar_Text.Enabled = false;
			this.ep_LeereFelder.SetIconAlignment(this.txt_Zusatzeintrag_Kommentar_Text, System.Windows.Forms.ErrorIconAlignment.TopLeft);
			this.txt_Zusatzeintrag_Kommentar_Text.Location = new System.Drawing.Point(24, 80);
			this.txt_Zusatzeintrag_Kommentar_Text.Name = "txt_Zusatzeintrag_Kommentar_Text";
			this.txt_Zusatzeintrag_Kommentar_Text.Size = new System.Drawing.Size(280, 130);
			this.txt_Zusatzeintrag_Kommentar_Text.TabIndex = 52;
			this.txt_Zusatzeintrag_Kommentar_Text.Text = "<Eintrag>";
			// 
			// btn_Zusatzeintrag_NeuerKommentar
			// 
			this.btn_Zusatzeintrag_NeuerKommentar.Location = new System.Drawing.Point(200, 20);
			this.btn_Zusatzeintrag_NeuerKommentar.Name = "btn_Zusatzeintrag_NeuerKommentar";
			this.btn_Zusatzeintrag_NeuerKommentar.Size = new System.Drawing.Size(105, 23);
			this.btn_Zusatzeintrag_NeuerKommentar.TabIndex = 1;
			this.btn_Zusatzeintrag_NeuerKommentar.Text = "neuer &Kommentar";
			this.btn_Zusatzeintrag_NeuerKommentar.Click += new System.EventHandler(this.btn_Zusatzeintrag_NeuerKommentar_Click);
			// 
			// cmb_Zusatzeintrag_AlteKommentare
			// 
			this.cmb_Zusatzeintrag_AlteKommentare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Zusatzeintrag_AlteKommentare.Enabled = false;
			this.cmb_Zusatzeintrag_AlteKommentare.Items.AddRange(new object[] {
																				  "Peter Müller",
																				  "Max Mustermann"});
			this.cmb_Zusatzeintrag_AlteKommentare.Location = new System.Drawing.Point(24, 22);
			this.cmb_Zusatzeintrag_AlteKommentare.Name = "cmb_Zusatzeintrag_AlteKommentare";
			this.cmb_Zusatzeintrag_AlteKommentare.Size = new System.Drawing.Size(170, 21);
			this.cmb_Zusatzeintrag_AlteKommentare.TabIndex = 0;
			this.cmb_Zusatzeintrag_AlteKommentare.SelectedIndexChanged += new System.EventHandler(this.cmb_Zusatzeintrag_AlteKommentare_SelectedIndexChanged);
			// 
			// grp_Zusatzeintrag_Liste
			// 
			this.grp_Zusatzeintrag_Liste.Controls.Add(this.lbx_Zusatzeintrag_Liste);
			this.grp_Zusatzeintrag_Liste.Controls.Add(this.btn_Zusatzeintrag_Neu);
			this.grp_Zusatzeintrag_Liste.Location = new System.Drawing.Point(8, 10);
			this.grp_Zusatzeintrag_Liste.Name = "grp_Zusatzeintrag_Liste";
			this.grp_Zusatzeintrag_Liste.Size = new System.Drawing.Size(300, 480);
			this.grp_Zusatzeintrag_Liste.TabIndex = 55;
			this.grp_Zusatzeintrag_Liste.TabStop = false;
			this.grp_Zusatzeintrag_Liste.Text = "Zusatzeinträge";
			// 
			// lbx_Zusatzeintrag_Liste
			// 
			this.lbx_Zusatzeintrag_Liste.Location = new System.Drawing.Point(8, 20);
			this.lbx_Zusatzeintrag_Liste.Name = "lbx_Zusatzeintrag_Liste";
			this.lbx_Zusatzeintrag_Liste.Size = new System.Drawing.Size(285, 407);
			this.lbx_Zusatzeintrag_Liste.TabIndex = 53;
			this.lbx_Zusatzeintrag_Liste.SelectedIndexChanged += new System.EventHandler(this.lbx_Zusatzeintrag_Liste_SelectedIndexChanged);
			// 
			// btn_Zusatzeintrag_Neu
			// 
			this.btn_Zusatzeintrag_Neu.Location = new System.Drawing.Point(88, 435);
			this.btn_Zusatzeintrag_Neu.Name = "btn_Zusatzeintrag_Neu";
			this.btn_Zusatzeintrag_Neu.Size = new System.Drawing.Size(100, 25);
			this.btn_Zusatzeintrag_Neu.TabIndex = 52;
			this.btn_Zusatzeintrag_Neu.Text = "&neuer Eintrag";
			this.btn_Zusatzeintrag_Neu.Click += new System.EventHandler(this.btn_Zusatzeintrag_Neu_Click);
			// 
			// grp_Zusatzeintrag
			// 
			this.grp_Zusatzeintrag.Controls.Add(this.btn_Zusatzeintrag_Verwerfen);
			this.grp_Zusatzeintrag.Controls.Add(this.btn_Zusatzeintrag_Speichern);
			this.grp_Zusatzeintrag.Controls.Add(this.dtp_Zusatzeintrag_Datum);
			this.grp_Zusatzeintrag.Controls.Add(this.txt_Zusatzeintrag_Autor);
			this.grp_Zusatzeintrag.Controls.Add(this.txt_Zusatzeintrag_Text);
			this.grp_Zusatzeintrag.Enabled = false;
			this.grp_Zusatzeintrag.Location = new System.Drawing.Point(312, 10);
			this.grp_Zusatzeintrag.Name = "grp_Zusatzeintrag";
			this.grp_Zusatzeintrag.Size = new System.Drawing.Size(312, 215);
			this.grp_Zusatzeintrag.TabIndex = 3;
			this.grp_Zusatzeintrag.TabStop = false;
			this.grp_Zusatzeintrag.Text = "Zusatzeintrag";
			// 
			// btn_Zusatzeintrag_Verwerfen
			// 
			this.btn_Zusatzeintrag_Verwerfen.Location = new System.Drawing.Point(72, 184);
			this.btn_Zusatzeintrag_Verwerfen.Name = "btn_Zusatzeintrag_Verwerfen";
			this.btn_Zusatzeintrag_Verwerfen.Size = new System.Drawing.Size(80, 25);
			this.btn_Zusatzeintrag_Verwerfen.TabIndex = 51;
			this.btn_Zusatzeintrag_Verwerfen.Text = "&Verwerfen";
			this.btn_Zusatzeintrag_Verwerfen.Click += new System.EventHandler(this.btn_Zusatzeintrag_Verwerfen_Click);
			// 
			// btn_Zusatzeintrag_Speichern
			// 
			this.btn_Zusatzeintrag_Speichern.Location = new System.Drawing.Point(168, 184);
			this.btn_Zusatzeintrag_Speichern.Name = "btn_Zusatzeintrag_Speichern";
			this.btn_Zusatzeintrag_Speichern.Size = new System.Drawing.Size(80, 25);
			this.btn_Zusatzeintrag_Speichern.TabIndex = 50;
			this.btn_Zusatzeintrag_Speichern.Text = "&Speichern";
			this.btn_Zusatzeintrag_Speichern.Click += new System.EventHandler(this.btn_Zusatzeintrag_Speichern_Click);
			// 
			// dtp_Zusatzeintrag_Datum
			// 
			this.dtp_Zusatzeintrag_Datum.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_Zusatzeintrag_Datum.Enabled = false;
			this.dtp_Zusatzeintrag_Datum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Zusatzeintrag_Datum.Location = new System.Drawing.Point(184, 15);
			this.dtp_Zusatzeintrag_Datum.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp_Zusatzeintrag_Datum.Name = "dtp_Zusatzeintrag_Datum";
			this.dtp_Zusatzeintrag_Datum.Size = new System.Drawing.Size(120, 20);
			this.dtp_Zusatzeintrag_Datum.TabIndex = 18;
			// 
			// txt_Zusatzeintrag_Autor
			// 
			this.txt_Zusatzeintrag_Autor.Enabled = false;
			this.txt_Zusatzeintrag_Autor.Location = new System.Drawing.Point(16, 15);
			this.txt_Zusatzeintrag_Autor.Name = "txt_Zusatzeintrag_Autor";
			this.txt_Zusatzeintrag_Autor.Size = new System.Drawing.Size(165, 20);
			this.txt_Zusatzeintrag_Autor.TabIndex = 1;
			this.txt_Zusatzeintrag_Autor.Text = "<Vorname><Name>";
			// 
			// txt_Zusatzeintrag_Text
			// 
			this.txt_Zusatzeintrag_Text.Location = new System.Drawing.Point(16, 40);
			this.txt_Zusatzeintrag_Text.Name = "txt_Zusatzeintrag_Text";
			this.txt_Zusatzeintrag_Text.Size = new System.Drawing.Size(290, 136);
			this.txt_Zusatzeintrag_Text.TabIndex = 0;
			this.txt_Zusatzeintrag_Text.Text = "<Eintrag>";
			// 
			// ep_LeereFelder
			// 
			this.ep_LeereFelder.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.ep_LeereFelder.ContainerControl = this;
			// 
			// Cpr_usc_Etb
			// 
			this.Controls.Add(this.tabctrl_ETB);
			this.Name = "Cpr_usc_Etb";
			this.Size = new System.Drawing.Size(650, 530);
			this.Load += new System.EventHandler(this.pdf_ETB_Load);
			this.tabctrl_ETB.ResumeLayout(false);
			this.tabpage_ETB.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pdfViewer)).EndInit();
			this.tabpage_Systemereignisse.ResumeLayout(false);
			this.grp_Systemereignisse_Liste.ResumeLayout(false);
			this.grp_Systemereignisse_Auswahl.ResumeLayout(false);
			this.grp_Systemereignis_Kommentar.ResumeLayout(false);
			this.tabpage_Zusatzeintrag.ResumeLayout(false);
			this.grp_Zusatzeintrag_Kommentar.ResumeLayout(false);
			this.grp_Zusatzeintrag_Liste.ResumeLayout(false);
			this.grp_Zusatzeintrag.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Funktionalität

		private void SetzeHilfe()
		{
			this.pelsHelp.HelpNamespace = _st_etb.Einstellung.Helpfile;
			this.pelsHelp.SetShowHelp(this,true);
			this.pelsHelp.SetHelpKeyword(this,"Einsatztagebuch");

		}

		#region Funktionalität zu den Systemereignissen
		public void LadeAlleSystemeregnisseInListe(Cdv_Systemereignis[] pin_SeArray)
		{
			lbx_Systemereignisse_Liste.SelectedItem = null;
			
			// Alle Systemereigniss in die Liste eintragen
			lbx_Systemereignisse_Liste.Items.Clear();
			lbx_Systemereignisse_Liste.Items.AddRange(pin_SeArray);
			
			// Jedes Systemereignis das ins ETB soll markieren
			foreach (Cdv_Systemereignis se in pin_SeArray)
				lbx_Systemereignisse_Liste.SetItemChecked(lbx_Systemereignisse_Liste.Items.IndexOf(se), se.ErscheintInEtb);
		}

		private void InitSystemereignisKommentare()
		{
			//alles zu der Kombobox
			this.cmb_Systemereignis_Kommentar_AlteKommentare.Items.Clear();
			this.cmb_Systemereignis_Kommentar_AlteKommentare.Text = "";
			this.cmb_Systemereignis_Kommentar_AlteKommentare.Enabled = false;
			
			//datetimepicker
			this.dtp_Systemereignis_Kommentar_Datum.Enabled = false;
			this.dtp_Systemereignis_Kommentar_Datum.Text = "";
			
			//Kommentar (autor & text)
			this.txt_Systemereignis_Kommentar_Autor.Text = "";
			this.txt_Systemereignis_Kommentar_Text.Text = "";
			
			//erscheint in Etb
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb.Enabled = false;
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb.Checked = true;

			//speichern & verwerfen buttons
			this.btn_Systemereignis_Kommentar_Verwerfen.Enabled = false;
			this.btn_Systemereignis_Kommentar_Speichern.Enabled = false;
		}

		//Hier alle GUI Elemente Sperren/entsperren für die Eingabe eines neuen Kommentars
		private void SystemereignisStarteModusNeuerKommentar()
		{
			//erstmal das Kommentarfeld initalisieren
			InitSystemereignisKommentare();
			//Linke auswahlliste ausgrauen
			this.grp_Systemereignisse_Liste.Enabled = false;
			//combobox für alte Kommentare ausgrauen
			this.cmb_Systemereignis_Kommentar_AlteKommentare.Enabled = false;
			//DateTimePiker für Datum aktivieren und auf aktuelle Zeit setzen
			this.dtp_Systemereignis_Kommentar_Datum.Value = DateTime.Now;
			//Autorenfeld mit dem aktuellen Benutzer füllen
			this.txt_Systemereignis_Kommentar_Autor.Text = this._st_etb.Einstellung.Benutzer.Benutzername;
			//Kommentarfeld beschreibbar machen
			this.txt_Systemereignis_Kommentar_Text.Enabled = true;
			//Auswahlfeld für 'ErscheintInEtb' auf den Wert des Systemereignisses per default setzen
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb.Enabled = false;
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb.Checked = ((Cdv_Systemereignis)lbx_Systemereignisse_Liste.SelectedItem).ErscheintInEtb;			

			//Buttons für Speichern und Verwerfen aktivieren
			this.btn_Systemereignis_Kommentar_Speichern.Enabled = true;
			this.btn_Systemereignis_Kommentar_Verwerfen.Enabled = true;
		}

		private void SystemereignisBeendeModusNeuerKommentar()
		{	
			// Die anderen Felder wieder freigeben
			this.grp_Systemereignisse_Liste.Enabled = true;
			// Kommentareingabe sperren
			this.txt_Systemereignis_Kommentar_Text.Enabled = false;
			this.btn_Systemereignis_Kommentar_Speichern.Enabled = false;
			this.btn_Systemereignis_Kommentar_Verwerfen.Enabled = false;
			//nochmal den Eventhandler aktivieren, der die Kommentare
			//zu einem Systemereignis läd
			this.lbx_Systemereignisse_Liste_SelectedIndexChanged(new object(), new System.EventArgs());
		}

		//Stellt das übergebene Systemereignis rechts im Auswahlfeld dar
		private void SystemereignisInAuswahlfeldDarstellen(Cdv_Systemereignis pin_syserg)
		{
			//Setze Titel der GroupBox auf ausgewähltes Systemereignis
			
			string str_tempSysErgToString = pin_syserg.ToString();
			// die Zeilenumbrüche müssen an dieser Stelle enfernt werden, um den titel der Groupbox richtig darstellen zu können
			str_tempSysErgToString = str_tempSysErgToString.Replace("\n", " ");
			grp_Systemereignisse_Auswahl.Text = str_tempSysErgToString;			
			//Setze Beschreibungstext auf die Beschreibung des ausgewählten Systemereignisses
			txt_Systemereignisse_Beschreibung.Text = pin_syserg.Beschreibung;
				
			//Label setzen mit zusätzlichen Informationen zu diesem Systemereignis
			lbl_ausgewaehltesSystemereignis_Info.Text = "Datum: "
				+ pin_syserg.ErstellDatum.ToLongDateString()
				+ " - "+pin_syserg.ErstellDatum.ToLongTimeString()
				+ "\n"
				+ "Art:                "
				+ pin_syserg.Systemereignisart
				+ "\n" 
				+ "Benutzer:      "
				+ pin_syserg.Benutzername;
		}
		
		//Stellt den übergebenen Kommentar auf dem Reiter Sytemereignis im Kommentarbereich dar
		private void SystemereignisAlteKommentareLaden(Cdv_Systemereignis pin_syserg)
		{			

			//Präventives löschen der Kommentarfelder Inhalte
			this.InitSystemereignisKommentare();
			//Laden aller Kommentare zu diesem Systemereignis in die Combobox				
			foreach(Cdv_EtbEintragKommentar etbK in this._st_etb._myEtbKommentarMenge)
			{
				if(etbK.EtbEintragID == pin_syserg.ID) 
				{
					this.cmb_Systemereignis_Kommentar_AlteKommentare.Items.Add(etbK);
					this.cmb_Systemereignis_Kommentar_AlteKommentare.Enabled = true;					
				}
			}
		}


		private void SystemereignisGewaehltenKommentarDarstellen(Cdv_EtbEintragKommentar pin_etbK)
		{		
			//Belegen:
			//Autor
			this.txt_Systemereignis_Kommentar_Autor.Text = pin_etbK.Kommentar.Autor;
			//Datum
			this.dtp_Systemereignis_Kommentar_Datum.Value = pin_etbK.ErstellDatum;
			//Inhalt
			this.txt_Systemereignis_Kommentar_Text.Text = pin_etbK.Kommentar.Text;
			//ErscheintInEtb
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb.Checked = pin_etbK.ErscheintInEtb;
			this.cbx_Systemereignis_Kommentar_ErscheintInEtb.Enabled = true;
		}


		#endregion

		#region Funktionalität zu den Zusatzeintraegen
		public void LadeAlleZusatzeintraegeInListe(Cdv_EtbEintrag[] pin_ZeArray)
		{
			lbx_Zusatzeintrag_Liste.Items.Clear();
			lbx_Zusatzeintrag_Liste.Items.AddRange(pin_ZeArray);
		}

		public void LadeEtbZusatzEintragKommentarInListe(Cdv_EtbEintragKommentar pin_Kommentar)
		{
			//hier wird das Element in die Auswahlliste gepackt
			cmb_Zusatzeintrag_AlteKommentare.Items.Add(pin_Kommentar);		
		}

		private void InitZusatzeintragKommentare()
		{
			//alles zu der Kombobox
			this.cmb_Zusatzeintrag_AlteKommentare.Items.Clear();			     
			this.cmb_Zusatzeintrag_AlteKommentare.Text = "";
			this.cmb_Zusatzeintrag_AlteKommentare.Enabled = false;
			
			//datetimepicker
			this.dtp_Zusatzeintrag_Kommentar_Datum.Enabled = false;
			this.dtp_Zusatzeintrag_Kommentar_Datum.Text = "";
			
			//Kommentar (autor & text)
			this.txt_Zusatzeintrag_Kommentar_Autor.Text = "";
			this.txt_Zusatzeintrag_Kommentar_Text.Text = "";
			
			//erscheint in Etb
			this.cbx_Zusatzeintrag_Kommentar_ErscheintInEtb.Enabled = false;
			this.cbx_Zusatzeintrag_Kommentar_ErscheintInEtb.Checked = true;

			//speichern & verwerfen buttons
			this.btn_Zusatzeintrag_Kommentar_Verwerfen.Enabled = false;
			this.btn_Zusatzeintrag_Kommentar_Speichern.Enabled = false;
		}

		private void ZusatzeintragStarteModusNeuerKommentar()
		{
			//erstmal das Kommentarfeld initalisieren
			this.InitZusatzeintragKommentare();
			//Linke auswahlliste ausgrauen
			this.grp_Zusatzeintrag_Liste.Enabled = false;
			//aktuelles Zusatzeintrag Anzeigefeld ausgrauen
			this.grp_Zusatzeintrag.Enabled = false;
			//combobox für alte Kommentare ausgrauen
			this.cmb_Zusatzeintrag_AlteKommentare.Enabled = false;
			//DateTimePiker für Datum auf aktuelle Zeit setzen
			this.dtp_Zusatzeintrag_Kommentar_Datum.Value = DateTime.Now;
			//Autorenfeld mit dem aktuellen Benutzer füllen
			this.txt_Zusatzeintrag_Kommentar_Autor.Text = this._st_etb.Einstellung.Benutzer.Benutzername;
			//Kommentarfeld beschreibbar machen			
			this.txt_Zusatzeintrag_Kommentar_Text.Enabled = true;
			//Auswahlfeld für 'ErscheintInEtb' auf true setzen			
			this.cbx_Zusatzeintrag_Kommentar_ErscheintInEtb.Enabled = false;
			this.cbx_Zusatzeintrag_Kommentar_ErscheintInEtb.Checked = true;

			//Buttons für Speichern und Verwerfen aktivieren
			this.btn_Zusatzeintrag_Kommentar_Speichern.Enabled = true;
			this.btn_Zusatzeintrag_Kommentar_Verwerfen.Enabled = true;
		}

		private void ZusatzeintragBeendeModusNeuerKommentar()
		{	
			// Die anderen Felder wieder freigeben
			this.grp_Zusatzeintrag_Liste.Enabled = true;
			// Kommentareingabe sperren
			this.txt_Zusatzeintrag_Kommentar_Text.Enabled = false;
			this.btn_Zusatzeintrag_Kommentar_Speichern.Enabled = false;
			this.btn_Zusatzeintrag_Kommentar_Verwerfen.Enabled = false;
			//nochmal den Eventhandler aktivieren, der die Kommentare zu einem Zusatzeintrag läd
			this.lbx_Zusatzeintrag_Liste_SelectedIndexChanged(new object(), new System.EventArgs());
		}

		private void ZusatzeintragStarteModusNeuerEintrag()
		{
			// Eingabeelemente freigeben und sperren
			grp_Zusatzeintrag.Enabled = true;
			grp_Zusatzeintrag_Liste.Enabled = false;
			grp_Zusatzeintrag_Kommentar.Enabled = false;
			// Felder leeren
			txt_Zusatzeintrag_Autor.Text = _st_etb.Einstellung.Benutzer.Benutzername;
			txt_Zusatzeintrag_Text.Text = String.Empty;
			dtp_Zusatzeintrag_Datum.Value = System.DateTime.Now;
		}
		
		private void ZusatzeintragBeendeModusNeuerEintrag(bool pin_neuerEintrag)
		{	
			// Eingabeelemente freigeben und sperren
			grp_Zusatzeintrag.Enabled = false;
			grp_Zusatzeintrag_Liste.Enabled = true;
			grp_Zusatzeintrag_Kommentar.Enabled = false;
			// Felder leeren
			txt_Zusatzeintrag_Text.Text = String.Empty;
			dtp_Zusatzeintrag_Datum.Value = System.DateTime.Now;
			// Eventuell neuen Eintrag auswählen
			if (pin_neuerEintrag)
				lbx_Zusatzeintrag_Liste.SelectedIndex = lbx_Zusatzeintrag_Liste.Items.Count - 1;
			else
				lbx_Zusatzeintrag_Liste.ClearSelected();
		}
		
		//Stellt den übergebenen EtbEintrag rechts im Auswahlfeld dar
		private void ZusatzeintragInAuswahlfeldDarstellen(Cdv_EtbEintrag pin_etbE)
		{
			txt_Zusatzeintrag_Autor.Text = pin_etbE.Benutzername;
			dtp_Zusatzeintrag_Datum.Value = pin_etbE.ErstellDatum;
			txt_Zusatzeintrag_Text.Text = pin_etbE.Beschreibung;
		}
		
		//Stellt den übergebenen Kommentar auf dem Reiter ZusatzErinträe im Kommentarbereich dar
		private void ZusatzeintragAlteKommentareLaden(Cdv_EtbEintrag pin_ebtE)
		{			
			//Initialisieren des Kommentarbereiches
			this.InitZusatzeintragKommentare();

			//Kommentare zu diesem Eintrag in die Groupbox laden				
			foreach(Cdv_EtbEintragKommentar etbK in this._st_etb._myEtbKommentarMenge)
			{
				if(etbK.EtbEintragID == pin_ebtE.ID)
				{
					cmb_Zusatzeintrag_AlteKommentare.Items.Add(etbK);
					cmb_Zusatzeintrag_AlteKommentare.Enabled = true;
				}
				
			}		
		}

		private void ZusatzeintragGewaehltenKommentarDarstellen(Cdv_EtbEintragKommentar pin_etbK)
		{			
			//Datum
			this.dtp_Zusatzeintrag_Kommentar_Datum.Value = pin_etbK.ErstellDatum;
			//Autor
			this.txt_Zusatzeintrag_Kommentar_Autor.Text = pin_etbK.Kommentar.Autor;
			//Text
			this.txt_Zusatzeintrag_Kommentar_Text.Text = pin_etbK.Kommentar.Text;
			//ErscheintInEtb
			this.cbx_Zusatzeintrag_Kommentar_ErscheintInEtb.Checked = pin_etbK.ErscheintInEtb;
			this.cbx_Zusatzeintrag_Kommentar_ErscheintInEtb.Enabled = false;
		}
		#endregion
		/// <summary>
		/// Überprüft, ob das übergebene Textfeld einen leeren Inhalt hat.
		/// Setzt ggf. den Errorprovider an das ensprechende Feld.
		/// </summary>
		/// <param name="pin_textbox">RichTextBox, die zu überprüfen ist</param>
		/// <returns>true-> Eingabe korrekt, sonst false</returns>
		private bool ValidiereTextfeldNichtLeer(RichTextBox pin_textbox)
		{
			if(pin_textbox.Text.Length > 0)
			{
				ep_LeereFelder.SetError(pin_textbox, "");
				return true;
			}
			else
			{
				ep_LeereFelder.SetError(pin_textbox, "Sie können nicht fortfahren, solange dieses Feld keinen Inhalt hat");
				return false;
			}			
		}
		#endregion

		#region Eventhandler
		// Laden des Einsatztagebuchs aus Datenbank wenn in das Fenstergewechselt wird
		private void pdf_ETB_Load(object sender, System.EventArgs e)
		{
			try
			{
				if (tabctrl_ETB.SelectedIndex == 0 && this.tabctrl_ETB.Focused)
				{
					if (_st_etb.ErzeugeEtb())
					{
						pdfViewer.LoadFile("ETB.pdf");
					}
					else
					{
						MessageBox.Show("ETB konnte nicht erstellt werden!");
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
										
	
		#region Eventhandler zu den Systemereignissen

		// Lädt das ausgewählte Systemereigniss vollständig inklusive Kommentare		
		private void lbx_Systemereignisse_Liste_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lbx_Systemereignisse_Liste.SelectedItem != null)
			{
				//Laden in den Rechten bereich zum darstellen
				this.SystemereignisInAuswahlfeldDarstellen((Cdv_Systemereignis) lbx_Systemereignisse_Liste.SelectedItem);
				//Laden aller vorhandenen Kommentare zu diesem Systemereignis
				this.SystemereignisAlteKommentareLaden((Cdv_Systemereignis) lbx_Systemereignisse_Liste.SelectedItem);
				this.grp_Systemereignis_Kommentar.Enabled = true;
			}
			else
				this.grp_Systemereignis_Kommentar.Enabled = false;
		}
				
		private void lbx_Systemereignisse_Liste_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			// Erstes IF verhindert, dass beim Laden aller Systemereignisse diese Überprüfung durchgeführt wird
			if (lbx_Systemereignisse_Liste.SelectedItem != null) 
			{
				if (e.CurrentValue == CheckState.Checked)
				{
					// Verhindert, dass eine Markierung wieder Rückgängig gemacht werden kann
					e.NewValue = CheckState.Checked;
				}
				else
				{
					// Setzt das Flag im Objekt in der ListBox
					((Cdv_Systemereignis)lbx_Systemereignisse_Liste.SelectedItem).ErscheintInEtb = true;
					// Reicht die Forderung nach Änderung des Status' an die Steuerungsschicht weiter
					_st_etb.MarkiereSystemereignis((Cdv_Systemereignis)lbx_Systemereignisse_Liste.SelectedItem);
				}
			}
			
		}
		
		private void cmb_Systemereignis_Kommentar_AlteKommentare_SelectedIndexChanged(object sender, System.EventArgs e)
		{			
			//Belegen mit dem entsprechenden wert:
			this.SystemereignisGewaehltenKommentarDarstellen((Cdv_EtbEintragKommentar) cmb_Systemereignis_Kommentar_AlteKommentare.SelectedItem);
			
		}
		

		private void btn_Systemereignis_Kommentar_NeuerKommentar_Click(object sender, System.EventArgs e)
		{
			//Jetzt wird der Modus arktiviert, der es erlaubt einen neuen Kommentar aufzunehmen			
			SystemereignisStarteModusNeuerKommentar();				
		}

		
		private void btn_Systemereignis_Kommentar_Verwerfen_Click(object sender, System.EventArgs e)
		{
			//evtl. gesetzten Errorprovider zurücksetzen
			this.ep_LeereFelder.SetError(txt_Systemereignis_Kommentar_Text, "");
			//Benden des Modus (änderungen gehen verloren)
			this.SystemereignisBeendeModusNeuerKommentar();
		}

		
		private void btn_Systemereignis_Kommentar_Speichern_Click(object sender, System.EventArgs e)
		{
			if(ValidiereTextfeldNichtLeer(txt_Systemereignis_Kommentar_Text))
			{
				// Kommentarobjekt mit den Eingaben füllen
				Cdv_EtbEintragKommentar neuerKommentar = new Cdv_EtbEintragKommentar(
					((Cdv_EtbEintrag)lbx_Systemereignisse_Liste.SelectedItem).ID,
					dtp_Systemereignis_Kommentar_Datum.Value,
					cbx_Systemereignis_Kommentar_ErscheintInEtb.Checked);
				neuerKommentar.Kommentar.Autor = txt_Systemereignis_Kommentar_Autor.Text;
				neuerKommentar.Kommentar.Text = txt_Systemereignis_Kommentar_Text.Text;
				// Objekt an die Steuerungschicht weiterreichen
				_st_etb.SpeichereEtbEintragKommentar(neuerKommentar);
			
				//Danach den Modus beenden
				this.SystemereignisBeendeModusNeuerKommentar();
			}
		}

		#endregion

		#region Eventhandler zu den ZusatzEinträgen
		private void lbx_Zusatzeintrag_Liste_SelectedIndexChanged(object sender, System.EventArgs e)
		{

			if (lbx_Zusatzeintrag_Liste.SelectedItem != null)
			{				
				//Informationen des ausgewählten Zusatzeintrages in Anzeigen
				this.ZusatzeintragInAuswahlfeldDarstellen((Cdv_EtbEintrag) lbx_Zusatzeintrag_Liste.SelectedItem);
				
				//Laden aller Kommentare zu diesem EtbEintrag
				this.ZusatzeintragAlteKommentareLaden((Cdv_EtbEintrag) lbx_Zusatzeintrag_Liste.SelectedItem);				

				//Eingabefelder Kommentar aktivieren
				this.grp_Zusatzeintrag_Kommentar.Enabled = true;
			}
			else
			{
				// Eingabefeld Kommentar deaktivieren
				this.grp_Zusatzeintrag_Kommentar.Enabled = false;
			}

		}
		

		private void btn_Zusatzeintrag_Neu_Click(object sender, System.EventArgs e)
		{
			this.ZusatzeintragStarteModusNeuerEintrag();
		}


		private void btn_Zusatzeintrag_Speichern_Click(object sender, System.EventArgs e)
		{
			if(ValidiereTextfeldNichtLeer(txt_Zusatzeintrag_Text))
			{
				// Objekt aus den Eingabefeldern füllen
				Cdv_EtbEintrag neuerZE = new Cdv_EtbEintrag(
					txt_Zusatzeintrag_Autor.Text,
					dtp_Zusatzeintrag_Datum.Value,
					txt_Zusatzeintrag_Text.Text);
				// Objekt zum Speichern an die Steuerungsschicht weiterreichen
				_st_etb.SpeichereEtbZusatzEintrag(neuerZE);
			
				this.ZusatzeintragBeendeModusNeuerEintrag(true);
			}
		}


		private void btn_Zusatzeintrag_Verwerfen_Click(object sender, System.EventArgs e)
		{
			//evtl. gesetzten Errorprovider zurücksetzen
			this.ep_LeereFelder.SetError(txt_Zusatzeintrag_Text,"");
			//Modus beenden
			this.ZusatzeintragBeendeModusNeuerEintrag(false);
		}


		private void btn_Zusatzeintrag_NeuerKommentar_Click(object sender, System.EventArgs e)
		{
			this.ZusatzeintragStarteModusNeuerKommentar();
		}

		
		private void btn_Zusatzeintrag_Kommentar_Verwerfen_Click(object sender, System.EventArgs e)
		{
			//evtl. gesetzten Errorprovider zurücksetzen
			this.ep_LeereFelder.SetError(txt_Zusatzeintrag_Kommentar_Text,"");
			this.ZusatzeintragBeendeModusNeuerKommentar();
		}


		private void btn_Zusatzeintrag_Kommentar_Speichern_Click(object sender, System.EventArgs e)
		{
			if(ValidiereTextfeldNichtLeer(txt_Zusatzeintrag_Kommentar_Text))
			{
				// Kommentarobjekt mit den Eingaben füllen
				Cdv_EtbEintragKommentar neuerKommentar = new Cdv_EtbEintragKommentar(
					((Cdv_EtbEintrag)lbx_Zusatzeintrag_Liste.SelectedItem).ID,
					dtp_Zusatzeintrag_Kommentar_Datum.Value,
					cbx_Zusatzeintrag_Kommentar_ErscheintInEtb.Checked);
				neuerKommentar.Kommentar.Autor = txt_Zusatzeintrag_Kommentar_Autor.Text;
				neuerKommentar.Kommentar.Text = txt_Zusatzeintrag_Kommentar_Text.Text;
				// Objekt an die Steuerungschicht weiterreichen
				_st_etb.SpeichereEtbEintragKommentar(neuerKommentar);
				// Beende den Kommentarmodus
				this.ZusatzeintragBeendeModusNeuerKommentar();
			}
		}


		private void cmb_Zusatzeintrag_AlteKommentare_SelectedIndexChanged(object sender, System.EventArgs e)
		{			
			this.ZusatzeintragGewaehltenKommentarDarstellen((Cdv_EtbEintragKommentar) cmb_Zusatzeintrag_AlteKommentare.SelectedItem);
		}

		#endregion			
		#endregion
		
		#region Setzen der Rollenrechte
		//Test steht noch aus.
		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
		{
			
			Tdv_Systemrolle rolle = (Tdv_Systemrolle) pin_i_aktuelleRolle;
			this.tabctrl_ETB.TabPages.Clear();
			
			switch (rolle)
			{
					//Haben alle die kompletten Rechte
				case Tdv_Systemrolle.Zugführer: 
				case Tdv_Systemrolle.Zugtruppführer:
				case Tdv_Systemrolle.S2:
				case Tdv_Systemrolle.Einsatzleiter:
				case Tdv_Systemrolle.LeiterFüSt:
				case Tdv_Systemrolle.LeiterStab:
				case Tdv_Systemrolle.Sichter :
				case Tdv_Systemrolle.Führungsgehilfe:
				{
					this.tabctrl_ETB.TabPages.Add(this.tabpage_ETB);
					this.tabctrl_ETB.TabPages.Add(this.tabpage_Systemereignisse);
					this.tabctrl_ETB.TabPages.Add(this.tabpage_Zusatzeintrag);
					break;
				}
					//Haben alle keine Rechte (ALLE)
				case Tdv_Systemrolle.Fernmelder :
				case Tdv_Systemrolle.S1:
				case Tdv_Systemrolle.S3:
				case Tdv_Systemrolle.S4: 
				case Tdv_Systemrolle.S5: 
				case Tdv_Systemrolle.S6:
				default:	
				{
					this.tabctrl_ETB.TabPages.Add(this.tabpage_ETB);
					break;
				}
			}
		}

		#endregion	

		private void btn_erneut_laden_Click(object sender, System.EventArgs e)
		{
			try
			{
					if (_st_etb.ErzeugeEtb())
					{
						pdfViewer.LoadFile("ETB.pdf");
					}
					else
					{
						MessageBox.Show("ETB konnte nicht erstellt werden!");
					}				
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		




	}
}
