using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace pELS.Server
{
	using pELS.DV;

	/// <summary>
	/// Summary description for Cpr_frm_Einsatzdaten.
	/// </summary>
	public class Cpr_frm_Einsatzdaten : System.Windows.Forms.Form
	{
		#region GUI-Variablen
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txt_Einsatzort;
		private System.Windows.Forms.DateTimePicker dateBisDatum;
		private System.Windows.Forms.DateTimePicker dateVonDatum;
		private System.Windows.Forms.Button btn_Schliessen;
		private System.Windows.Forms.Button btn_SpeichernSchliessen;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		Cst_Server _Cst_Server = null;
		private System.Windows.Forms.TextBox txt_Einsatzbezeichnung;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox cbx_KostenAbrechnung;
		private System.Windows.Forms.CheckBox cbx_Erfahrungsbericht;
		private System.Windows.Forms.CheckBox cbx_Pressemitteilung;
		private System.Windows.Forms.CheckBox cbx_Kostenerstattung;
		private System.Windows.Forms.CheckBox cbx_Haftungsfreistellung;
		private System.Windows.Forms.CheckBox cbx_IhKBescheinigung;
		private System.Windows.Forms.CheckBox cbx_Einsatzbericht;
		private System.Windows.Forms.ComboBox cmb_ArtDerHilfeleistung;
		private System.Windows.Forms.Label lbl_Einatzart;
		private System.Windows.Forms.TextBox txt_BeschreibungAutor;
		private System.Windows.Forms.TextBox txt_BeschreibungText;
		private System.Windows.Forms.GroupBox groupBox1;
		Cdv_Einsatz _Cdv_Einsatz = null;

		/// <summary>
		///  lädt die aktuellen Einsatzdaten in den Dialog
		/// </summary>
		/// <param name="pin_Cst_Server"></param>
		public Cpr_frm_Einsatzdaten(ref Cst_Server pin_Cst_Server)
		{
			_Cst_Server = pin_Cst_Server;			
			_Cdv_Einsatz = pin_Cst_Server.Einsatz;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.dateBisDatum = new System.Windows.Forms.DateTimePicker();
			this.dateVonDatum = new System.Windows.Forms.DateTimePicker();
			this.txt_Einsatzort = new System.Windows.Forms.TextBox();
			this.btn_Schliessen = new System.Windows.Forms.Button();
			this.btn_SpeichernSchliessen = new System.Windows.Forms.Button();
			this.txt_Einsatzbezeichnung = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cbx_KostenAbrechnung = new System.Windows.Forms.CheckBox();
			this.cbx_Erfahrungsbericht = new System.Windows.Forms.CheckBox();
			this.cbx_Pressemitteilung = new System.Windows.Forms.CheckBox();
			this.cbx_Kostenerstattung = new System.Windows.Forms.CheckBox();
			this.cbx_Haftungsfreistellung = new System.Windows.Forms.CheckBox();
			this.cbx_IhKBescheinigung = new System.Windows.Forms.CheckBox();
			this.cmb_ArtDerHilfeleistung = new System.Windows.Forms.ComboBox();
			this.cbx_Einsatzbericht = new System.Windows.Forms.CheckBox();
			this.lbl_Einatzart = new System.Windows.Forms.Label();
			this.txt_BeschreibungAutor = new System.Windows.Forms.TextBox();
			this.txt_BeschreibungText = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.TabIndex = 1;
			this.label2.Text = "Einsatzort";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "von";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 104);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 23);
			this.label4.TabIndex = 4;
			this.label4.Text = "bis";
			// 
			// dateBisDatum
			// 
			this.dateBisDatum.CustomFormat = "dddd, dd. MMM.`yy - HH:mm";
			this.dateBisDatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateBisDatum.Location = new System.Drawing.Point(84, 104);
			this.dateBisDatum.Name = "dateBisDatum";
			this.dateBisDatum.Size = new System.Drawing.Size(184, 20);
			this.dateBisDatum.TabIndex = 3;
			// 
			// dateVonDatum
			// 
			this.dateVonDatum.CustomFormat = "dddd, dd. MMM.`yy - HH:mm";
			this.dateVonDatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateVonDatum.Location = new System.Drawing.Point(84, 72);
			this.dateVonDatum.Name = "dateVonDatum";
			this.dateVonDatum.Size = new System.Drawing.Size(184, 20);
			this.dateVonDatum.TabIndex = 2;
			// 
			// txt_Einsatzort
			// 
			this.txt_Einsatzort.Location = new System.Drawing.Point(84, 40);
			this.txt_Einsatzort.Name = "txt_Einsatzort";
			this.txt_Einsatzort.Size = new System.Drawing.Size(184, 20);
			this.txt_Einsatzort.TabIndex = 1;
			this.txt_Einsatzort.Text = "";
			// 
			// btn_Schliessen
			// 
			this.btn_Schliessen.Location = new System.Drawing.Point(132, 316);
			this.btn_Schliessen.Name = "btn_Schliessen";
			this.btn_Schliessen.TabIndex = 5;
			this.btn_Schliessen.Text = "schließen";
			this.btn_Schliessen.Click += new System.EventHandler(this.btn_Schliessen_Click);
			// 
			// btn_SpeichernSchliessen
			// 
			this.btn_SpeichernSchliessen.Location = new System.Drawing.Point(220, 316);
			this.btn_SpeichernSchliessen.Name = "btn_SpeichernSchliessen";
			this.btn_SpeichernSchliessen.Size = new System.Drawing.Size(144, 23);
			this.btn_SpeichernSchliessen.TabIndex = 4;
			this.btn_SpeichernSchliessen.Text = "speichern und schließen";
			this.btn_SpeichernSchliessen.Click += new System.EventHandler(this.btn_SpeichernSchliessen_Click);
			// 
			// txt_Einsatzbezeichnung
			// 
			this.txt_Einsatzbezeichnung.Location = new System.Drawing.Point(84, 8);
			this.txt_Einsatzbezeichnung.Name = "txt_Einsatzbezeichnung";
			this.txt_Einsatzbezeichnung.Size = new System.Drawing.Size(184, 20);
			this.txt_Einsatzbezeichnung.TabIndex = 0;
			this.txt_Einsatzbezeichnung.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 14;
			this.label1.Text = "Bezeichnung";
			// 
			// cbx_KostenAbrechnung
			// 
			this.cbx_KostenAbrechnung.Location = new System.Drawing.Point(84, 164);
			this.cbx_KostenAbrechnung.Name = "cbx_KostenAbrechnung";
			this.cbx_KostenAbrechnung.Size = new System.Drawing.Size(184, 24);
			this.cbx_KostenAbrechnung.TabIndex = 15;
			this.cbx_KostenAbrechnung.Text = "Kostenabrechnung gefertigt";
			// 
			// cbx_Erfahrungsbericht
			// 
			this.cbx_Erfahrungsbericht.Location = new System.Drawing.Point(84, 188);
			this.cbx_Erfahrungsbericht.Name = "cbx_Erfahrungsbericht";
			this.cbx_Erfahrungsbericht.Size = new System.Drawing.Size(184, 24);
			this.cbx_Erfahrungsbericht.TabIndex = 16;
			this.cbx_Erfahrungsbericht.Text = "Erfahrungsbericht geschrieben";
			// 
			// cbx_Pressemitteilung
			// 
			this.cbx_Pressemitteilung.Location = new System.Drawing.Point(84, 212);
			this.cbx_Pressemitteilung.Name = "cbx_Pressemitteilung";
			this.cbx_Pressemitteilung.Size = new System.Drawing.Size(184, 24);
			this.cbx_Pressemitteilung.TabIndex = 17;
			this.cbx_Pressemitteilung.Text = "Pressemitteilung geschrieben";
			// 
			// cbx_Kostenerstattung
			// 
			this.cbx_Kostenerstattung.Location = new System.Drawing.Point(84, 236);
			this.cbx_Kostenerstattung.Name = "cbx_Kostenerstattung";
			this.cbx_Kostenerstattung.Size = new System.Drawing.Size(184, 24);
			this.cbx_Kostenerstattung.TabIndex = 18;
			this.cbx_Kostenerstattung.Text = "Kostenerstattung kontrolliert";
			// 
			// cbx_Haftungsfreistellung
			// 
			this.cbx_Haftungsfreistellung.Location = new System.Drawing.Point(84, 260);
			this.cbx_Haftungsfreistellung.Name = "cbx_Haftungsfreistellung";
			this.cbx_Haftungsfreistellung.Size = new System.Drawing.Size(184, 24);
			this.cbx_Haftungsfreistellung.TabIndex = 19;
			this.cbx_Haftungsfreistellung.Text = "Haftungsfreistellung vorhanden";
			this.cbx_Haftungsfreistellung.Visible = false;
			// 
			// cbx_IhKBescheinigung
			// 
			this.cbx_IhKBescheinigung.Location = new System.Drawing.Point(84, 284);
			this.cbx_IhKBescheinigung.Name = "cbx_IhKBescheinigung";
			this.cbx_IhKBescheinigung.Size = new System.Drawing.Size(184, 24);
			this.cbx_IhKBescheinigung.TabIndex = 20;
			this.cbx_IhKBescheinigung.Text = "IHK_Bescheinigung vorhanden";
			this.cbx_IhKBescheinigung.Visible = false;
			// 
			// cmb_ArtDerHilfeleistung
			// 
			this.cmb_ArtDerHilfeleistung.Items.AddRange(new object[] {
																		 "Technische Hilfeleistung",
																		 "Sonstige Technische Hilfeleistung"});
			this.cmb_ArtDerHilfeleistung.Location = new System.Drawing.Point(84, 136);
			this.cmb_ArtDerHilfeleistung.Name = "cmb_ArtDerHilfeleistung";
			this.cmb_ArtDerHilfeleistung.Size = new System.Drawing.Size(184, 21);
			this.cmb_ArtDerHilfeleistung.TabIndex = 21;
			this.cmb_ArtDerHilfeleistung.Text = "Art der Hilfeleistung";
			this.cmb_ArtDerHilfeleistung.SelectedIndexChanged += new System.EventHandler(this.cmb_ArtDerHilfeleistung_SelectedIndexChanged);
			// 
			// cbx_Einsatzbericht
			// 
			this.cbx_Einsatzbericht.Location = new System.Drawing.Point(84, 260);
			this.cbx_Einsatzbericht.Name = "cbx_Einsatzbericht";
			this.cbx_Einsatzbericht.Size = new System.Drawing.Size(184, 24);
			this.cbx_Einsatzbericht.TabIndex = 22;
			this.cbx_Einsatzbericht.Text = "Einsatzbericht gefertigt";
			// 
			// lbl_Einatzart
			// 
			this.lbl_Einatzart.Location = new System.Drawing.Point(4, 136);
			this.lbl_Einatzart.Name = "lbl_Einatzart";
			this.lbl_Einatzart.Size = new System.Drawing.Size(56, 23);
			this.lbl_Einatzart.TabIndex = 23;
			this.lbl_Einatzart.Text = "Einsatzart";
			// 
			// txt_BeschreibungAutor
			// 
			this.txt_BeschreibungAutor.Location = new System.Drawing.Point(8, 20);
			this.txt_BeschreibungAutor.Name = "txt_BeschreibungAutor";
			this.txt_BeschreibungAutor.Size = new System.Drawing.Size(176, 20);
			this.txt_BeschreibungAutor.TabIndex = 24;
			this.txt_BeschreibungAutor.Text = "Autor";
			// 
			// txt_BeschreibungText
			// 
			this.txt_BeschreibungText.Location = new System.Drawing.Point(8, 44);
			this.txt_BeschreibungText.Multiline = true;
			this.txt_BeschreibungText.Name = "txt_BeschreibungText";
			this.txt_BeschreibungText.Size = new System.Drawing.Size(176, 248);
			this.txt_BeschreibungText.TabIndex = 25;
			this.txt_BeschreibungText.Text = "(Beschreibung fehlt noch)";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txt_BeschreibungText);
			this.groupBox1.Controls.Add(this.txt_BeschreibungAutor);
			this.groupBox1.Location = new System.Drawing.Point(276, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(192, 300);
			this.groupBox1.TabIndex = 26;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Einsatzbeschreibung";
			// 
			// Cpr_frm_Einsatzdaten
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(470, 344);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.lbl_Einatzart);
			this.Controls.Add(this.cbx_Einsatzbericht);
			this.Controls.Add(this.cmb_ArtDerHilfeleistung);
			this.Controls.Add(this.cbx_IhKBescheinigung);
			this.Controls.Add(this.cbx_Haftungsfreistellung);
			this.Controls.Add(this.cbx_Kostenerstattung);
			this.Controls.Add(this.cbx_Pressemitteilung);
			this.Controls.Add(this.cbx_Erfahrungsbericht);
			this.Controls.Add(this.cbx_KostenAbrechnung);
			this.Controls.Add(this.txt_Einsatzbezeichnung);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btn_SpeichernSchliessen);
			this.Controls.Add(this.btn_Schliessen);
			this.Controls.Add(this.txt_Einsatzort);
			this.Controls.Add(this.dateVonDatum);
			this.Controls.Add(this.dateBisDatum);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "Cpr_frm_Einsatzdaten";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Einsatzdaten";
			this.Load += new System.EventHandler(this.Cpr_frm_Einsatzdaten_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// trägt Einsatzdaten korrekt in die vorhanden Felder ein
		/// </summary>
		/// <param name="pin_Einsatz"></param>
		private void ZeigeEinsatzdaten(Cdv_Einsatz pin_Einsatz)
		{
			if (pin_Einsatz != null)
			{
				if (pin_Einsatz.VonDatum == DateTime.MinValue) this.dateVonDatum.Value = DateTime.Now;
					else this.dateVonDatum.Value = pin_Einsatz.VonDatum;
				if (pin_Einsatz.BisDatum == DateTime.MinValue) this.dateBisDatum.Value = DateTime.Now;
					else this.dateBisDatum.Value = pin_Einsatz.BisDatum;								
				this.txt_Einsatzbezeichnung.Text = pin_Einsatz.Bezeichnung;
				this.txt_Einsatzort.Text = pin_Einsatz.Einsatzort;
				this.txt_BeschreibungText.Text = pin_Einsatz.Beschreibung.Text;
				this.txt_BeschreibungAutor.Text = pin_Einsatz.Beschreibung.Autor;
				this.cmb_ArtDerHilfeleistung.SelectedItem = pin_Einsatz.ArtDerHilfeleistung;
				this.cbx_Einsatzbericht.Checked = pin_Einsatz.EinsatzberichtGefertigt;
				this.cbx_Erfahrungsbericht.Checked = pin_Einsatz.ErfahrungsberichtGeschrieben;
				this.cbx_Haftungsfreistellung.Checked = pin_Einsatz.HaftungsfreistellungVorhanden;
				this.cbx_IhKBescheinigung.Checked = pin_Einsatz.IhkBescheinigungVorhanden;
				this.cbx_KostenAbrechnung.Checked = pin_Einsatz.KostenabrechnungGefertigt;
				this.cbx_Kostenerstattung.Checked = pin_Einsatz.KostenerstattungKontrolliert;
				this.cbx_Pressemitteilung.Checked = pin_Einsatz.PressemitteilungGeschrieben;
			}			
		}

		/// <summary>
		/// schließt das Fenster
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Schliessen_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// speichert Änderungen und schließt dann das Fenster
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_SpeichernSchliessen_Click(object sender, System.EventArgs e)
		{
			// Haupteinsatzdaten
			_Cdv_Einsatz.Bezeichnung = this.txt_Einsatzbezeichnung.Text;
			_Cdv_Einsatz.Einsatzort = this.txt_Einsatzort.Text;
			_Cdv_Einsatz.VonDatum = this.dateVonDatum.Value;
			_Cdv_Einsatz.BisDatum = this.dateBisDatum.Value;
			// Einsatzbeschreibung
			_Cdv_Einsatz.Beschreibung.Text = this.txt_BeschreibungText.Text;
			_Cdv_Einsatz.Beschreibung.Autor = this.txt_BeschreibungAutor.Text;
			// Daten für Vorblatteinsatzbereicht
			if (this.cmb_ArtDerHilfeleistung.SelectedItem != null)
				_Cdv_Einsatz.ArtDerHilfeleistung = this.cmb_ArtDerHilfeleistung.SelectedItem.ToString();
			_Cdv_Einsatz.KostenabrechnungGefertigt = this.cbx_KostenAbrechnung.Checked;
			_Cdv_Einsatz.ErfahrungsberichtGeschrieben = this.cbx_Erfahrungsbericht.Checked;
			_Cdv_Einsatz.PressemitteilungGeschrieben = this.cbx_Pressemitteilung.Checked;
			_Cdv_Einsatz.KostenerstattungKontrolliert = this.cbx_Kostenerstattung.Checked;
			// Überprüfen welche Art der Hilfeleistung vorliegt
			if (this.cmb_ArtDerHilfeleistung.SelectedItem != null)
			{
				if (cmb_ArtDerHilfeleistung.SelectedItem.ToString() == "Sonstige Technische Hilfeleistung")
				{
					_Cdv_Einsatz.EinsatzberichtGefertigt = false;
					_Cdv_Einsatz.HaftungsfreistellungVorhanden = this.cbx_Haftungsfreistellung.Checked;
					_Cdv_Einsatz.IhkBescheinigungVorhanden = this.cbx_IhKBescheinigung.Checked;
				}
			}
			else
			{
				_Cdv_Einsatz.EinsatzberichtGefertigt = this.cbx_Einsatzbericht.Checked;
				_Cdv_Einsatz.HaftungsfreistellungVorhanden = false;
				_Cdv_Einsatz.IhkBescheinigungVorhanden = false;
			}
			// Daten speichern
			_Cdv_Einsatz = (Cdv_Einsatz)_Cst_Server.SpeichereEinsatzdaten(_Cdv_Einsatz);	
			// Form schließen;
			this.Close();
		}

		private void Cpr_frm_Einsatzdaten_Load(object sender, System.EventArgs e)
		{
			ZeigeEinsatzdaten(_Cdv_Einsatz);
			cmb_ArtDerHilfeleistung.SelectedItem = _Cdv_Einsatz.ArtDerHilfeleistung;
		}

		private void cmb_ArtDerHilfeleistung_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cmb_ArtDerHilfeleistung.SelectedItem.ToString() == "Sonstige Technische Hilfeleistung")
			{
				this.cbx_Haftungsfreistellung.Visible = true;
				this.cbx_IhKBescheinigung.Visible = true;
				this.cbx_Einsatzbericht.Visible = false;	
			}
			else
			{
				this.cbx_Haftungsfreistellung.Visible = false;
				this.cbx_IhKBescheinigung.Visible = false;
				this.cbx_Einsatzbericht.Visible = true;
			}
		}

	}
}
