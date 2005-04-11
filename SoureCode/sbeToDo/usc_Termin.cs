using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pELS.Client.ToDo
{
	#region Dokumentation
	/**				aktuelle Version: 1.0 Schuppe
	INFO:
		- kapselt die Eingabemaske zum Erfassen/Anzeigen von Terminen
		
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
	geändert von: Hütte						am: 09.03.2005				
	review von:								am:
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
	
	public class usc_Termin : System.Windows.Forms.UserControl
	{
		#region Windows Designer Variablen
		public System.Windows.Forms.GroupBox gbx_NeuerTermin;
		public System.Windows.Forms.Label lbl_DatumBis;
		public System.Windows.Forms.DateTimePicker dtp_DatumVon;
		public System.Windows.Forms.Label lbl_DatumVon;
		public System.Windows.Forms.CheckBox cbx_ErinnernAm;
		public System.Windows.Forms.Button btn_Zuruecksetzen;
		public System.Windows.Forms.Button btn_Speichern;
		public System.Windows.Forms.DateTimePicker dtp_ErinnernAm;
		public System.Windows.Forms.DateTimePicker dtp_DatumBis;
		public System.Windows.Forms.GroupBox gbx_Befehl;
		public System.Windows.Forms.CheckBox cbx_IstWichtig;
		private System.ComponentModel.Container components = null;
		public System.Windows.Forms.Label label2;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.Label lbl_ErstelltVon;
		#endregion
		public System.Windows.Forms.Label lbl_ErstelltFuer;
		public System.Windows.Forms.RichTextBox txt_Betreff;
		
		
	
		#region Instanzvariablen
		// gibt an, ob bereits eine Eingabe geschehen ist
		private bool _b_FelderModifiziert = false;
		// Die zugehörige Steuerschicht		
		#endregion

		#region SETs und GETs
		public bool b_FelderModifiziert
		{
			get { return _b_FelderModifiziert;}
			set { _b_FelderModifiziert = value;}
		}
		#endregion

		#region Konstruktor und Destruktor
		public usc_Termin()
		{			
			
			// This call is required by the Windows.Forms Form Designer.
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
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btn_Zuruecksetzen = new System.Windows.Forms.Button();
			this.btn_Speichern = new System.Windows.Forms.Button();
			this.gbx_NeuerTermin = new System.Windows.Forms.GroupBox();
			this.txt_Betreff = new System.Windows.Forms.RichTextBox();
			this.lbl_ErstelltFuer = new System.Windows.Forms.Label();
			this.lbl_ErstelltVon = new System.Windows.Forms.Label();
			this.gbx_Befehl = new System.Windows.Forms.GroupBox();
			this.dtp_ErinnernAm = new System.Windows.Forms.DateTimePicker();
			this.cbx_ErinnernAm = new System.Windows.Forms.CheckBox();
			this.dtp_DatumBis = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.lbl_DatumBis = new System.Windows.Forms.Label();
			this.dtp_DatumVon = new System.Windows.Forms.DateTimePicker();
			this.Label1 = new System.Windows.Forms.Label();
			this.lbl_DatumVon = new System.Windows.Forms.Label();
			this.cbx_IstWichtig = new System.Windows.Forms.CheckBox();
			this.gbx_NeuerTermin.SuspendLayout();
			this.gbx_Befehl.SuspendLayout();
			this.SuspendLayout();
			// 
			// btn_Zuruecksetzen
			// 
			this.btn_Zuruecksetzen.Location = new System.Drawing.Point(456, 160);
			this.btn_Zuruecksetzen.Name = "btn_Zuruecksetzen";
			this.btn_Zuruecksetzen.Size = new System.Drawing.Size(80, 23);
			this.btn_Zuruecksetzen.TabIndex = 51;
			this.btn_Zuruecksetzen.Text = "Zurücksetzen";
			// 
			// btn_Speichern
			// 
			this.btn_Speichern.Location = new System.Drawing.Point(544, 160);
			this.btn_Speichern.Name = "btn_Speichern";
			this.btn_Speichern.Size = new System.Drawing.Size(80, 23);
			this.btn_Speichern.TabIndex = 50;
			this.btn_Speichern.Text = "Speichern";
			// 
			// gbx_NeuerTermin
			// 
			this.gbx_NeuerTermin.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_NeuerTermin.Controls.Add(this.txt_Betreff);
			this.gbx_NeuerTermin.Controls.Add(this.lbl_ErstelltFuer);
			this.gbx_NeuerTermin.Controls.Add(this.lbl_ErstelltVon);
			this.gbx_NeuerTermin.Controls.Add(this.gbx_Befehl);
			this.gbx_NeuerTermin.Controls.Add(this.dtp_DatumBis);
			this.gbx_NeuerTermin.Controls.Add(this.label2);
			this.gbx_NeuerTermin.Controls.Add(this.lbl_DatumBis);
			this.gbx_NeuerTermin.Controls.Add(this.dtp_DatumVon);
			this.gbx_NeuerTermin.Controls.Add(this.Label1);
			this.gbx_NeuerTermin.Controls.Add(this.lbl_DatumVon);
			this.gbx_NeuerTermin.Controls.Add(this.cbx_IstWichtig);
			this.gbx_NeuerTermin.Location = new System.Drawing.Point(8, 8);
			this.gbx_NeuerTermin.Name = "gbx_NeuerTermin";
			this.gbx_NeuerTermin.Size = new System.Drawing.Size(615, 144);
			this.gbx_NeuerTermin.TabIndex = 21;
			this.gbx_NeuerTermin.TabStop = false;
			this.gbx_NeuerTermin.Text = "Termin";
			// 
			// txt_Betreff
			// 
			this.txt_Betreff.Location = new System.Drawing.Point(16, 64);
			this.txt_Betreff.Name = "txt_Betreff";
			this.txt_Betreff.Size = new System.Drawing.Size(344, 72);
			this.txt_Betreff.TabIndex = 51;
			this.txt_Betreff.Text = "Betreff";
			// 
			// lbl_ErstelltFuer
			// 
			this.lbl_ErstelltFuer.Location = new System.Drawing.Point(80, 48);
			this.lbl_ErstelltFuer.Name = "lbl_ErstelltFuer";
			this.lbl_ErstelltFuer.Size = new System.Drawing.Size(176, 16);
			this.lbl_ErstelltFuer.TabIndex = 50;
			// 
			// lbl_ErstelltVon
			// 
			this.lbl_ErstelltVon.Location = new System.Drawing.Point(80, 24);
			this.lbl_ErstelltVon.Name = "lbl_ErstelltVon";
			this.lbl_ErstelltVon.Size = new System.Drawing.Size(176, 16);
			this.lbl_ErstelltVon.TabIndex = 49;
			// 
			// gbx_Befehl
			// 
			this.gbx_Befehl.Controls.Add(this.dtp_ErinnernAm);
			this.gbx_Befehl.Controls.Add(this.cbx_ErinnernAm);
			this.gbx_Befehl.Location = new System.Drawing.Point(378, 88);
			this.gbx_Befehl.Name = "gbx_Befehl";
			this.gbx_Befehl.Size = new System.Drawing.Size(232, 48);
			this.gbx_Befehl.TabIndex = 48;
			this.gbx_Befehl.TabStop = false;
			this.gbx_Befehl.Text = "Befehl";
			// 
			// dtp_ErinnernAm
			// 
			this.dtp_ErinnernAm.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_ErinnernAm.Enabled = false;
			this.dtp_ErinnernAm.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_ErinnernAm.Location = new System.Drawing.Point(107, 18);
			this.dtp_ErinnernAm.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp_ErinnernAm.Name = "dtp_ErinnernAm";
			this.dtp_ErinnernAm.Size = new System.Drawing.Size(120, 20);
			this.dtp_ErinnernAm.TabIndex = 23;
			// 
			// cbx_ErinnernAm
			// 
			this.cbx_ErinnernAm.Location = new System.Drawing.Point(10, 19);
			this.cbx_ErinnernAm.Name = "cbx_ErinnernAm";
			this.cbx_ErinnernAm.Size = new System.Drawing.Size(94, 20);
			this.cbx_ErinnernAm.TabIndex = 6;
			this.cbx_ErinnernAm.Text = "Erinnern am:";
			// 
			// dtp_DatumBis
			// 
			this.dtp_DatumBis.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_DatumBis.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_DatumBis.Location = new System.Drawing.Point(485, 40);
			this.dtp_DatumBis.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp_DatumBis.Name = "dtp_DatumBis";
			this.dtp_DatumBis.Size = new System.Drawing.Size(120, 20);
			this.dtp_DatumBis.TabIndex = 22;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.SystemColors.Window;
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 15);
			this.label2.TabIndex = 21;
			this.label2.Text = "Erstellt für:";
			// 
			// lbl_DatumBis
			// 
			this.lbl_DatumBis.Location = new System.Drawing.Point(376, 45);
			this.lbl_DatumBis.Name = "lbl_DatumBis";
			this.lbl_DatumBis.Size = new System.Drawing.Size(100, 15);
			this.lbl_DatumBis.TabIndex = 18;
			this.lbl_DatumBis.Text = "Datum Bis:";
			// 
			// dtp_DatumVon
			// 
			this.dtp_DatumVon.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_DatumVon.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_DatumVon.Location = new System.Drawing.Point(485, 20);
			this.dtp_DatumVon.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp_DatumVon.Name = "dtp_DatumVon";
			this.dtp_DatumVon.Size = new System.Drawing.Size(120, 20);
			this.dtp_DatumVon.TabIndex = 17;
			// 
			// Label1
			// 
			this.Label1.BackColor = System.Drawing.SystemColors.Window;
			this.Label1.Location = new System.Drawing.Point(16, 24);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(64, 15);
			this.Label1.TabIndex = 16;
			this.Label1.Text = "Erstellt von:";
			// 
			// lbl_DatumVon
			// 
			this.lbl_DatumVon.Location = new System.Drawing.Point(376, 24);
			this.lbl_DatumVon.Name = "lbl_DatumVon";
			this.lbl_DatumVon.Size = new System.Drawing.Size(104, 15);
			this.lbl_DatumVon.TabIndex = 2;
			this.lbl_DatumVon.Text = "Datum von: ";
			// 
			// cbx_IstWichtig
			// 
			this.cbx_IstWichtig.Location = new System.Drawing.Point(377, 64);
			this.cbx_IstWichtig.Name = "cbx_IstWichtig";
			this.cbx_IstWichtig.Size = new System.Drawing.Size(112, 20);
			this.cbx_IstWichtig.TabIndex = 14;
			this.cbx_IstWichtig.Text = "ist wichtig";
			// 
			// usc_Termin
			// 
			this.Controls.Add(this.btn_Zuruecksetzen);
			this.Controls.Add(this.btn_Speichern);
			this.Controls.Add(this.gbx_NeuerTermin);
			this.Location = new System.Drawing.Point(4, 22);
			this.Name = "usc_Termin";
			this.Size = new System.Drawing.Size(632, 184);
			this.gbx_NeuerTermin.ResumeLayout(false);
			this.gbx_Befehl.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

				
	}

}
