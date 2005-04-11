using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pELS.Client.MAT
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
	public class usc_Termin : System.Windows.Forms.UserControl
	{
		#region Variablen
		/// <summary>
		/// gibt an, ob bereits Eingaben geschehen sind
		/// </summary>
		protected bool _b_FelderModifiziert = false;
		/// <summary>
		/// ermöglicht das Anzeigen von fehlerhaften Eingaben
		/// </summary>		
		protected System.Windows.Forms.ErrorProvider ep_Eingabe;
		private Cst_MAT _stMAT;
		#endregion

		#region graphische Variablen

		public System.ComponentModel.Container components = null;
		
		public System.Windows.Forms.Button btn_Auftrag_AuftragZuruecksetzen;
		public System.Windows.Forms.Button btn_Auftrag_AuftragSpeichern;
		private System.Windows.Forms.CheckBox cbx_TerminErstellen_IstInToDoListe;
		private System.Windows.Forms.CheckBox cbx_Termin_DatumVonJetzt;
		private System.Windows.Forms.CheckBox cbx_Termin_DatumBisJetzt;
		private System.Windows.Forms.CheckBox cbx_Termin_ErinnerungszeitpunktJetzt;
		private System.Windows.Forms.Label lbl_Termin_ErstellenVonText;
		private System.Windows.Forms.GroupBox gbx_TerminErstellen_NeuerTermin;
		private System.Windows.Forms.GroupBox gbx_TerminErstellen_Erinnerung;
		private System.Windows.Forms.DateTimePicker dtp_TerminErstellen_ErinnernAm;
		private System.Windows.Forms.CheckBox cbx_TerminErstellen_ErinnernAm;
		private System.Windows.Forms.DateTimePicker dtp_TerminErstellen_DatumBis;
		private System.Windows.Forms.Label lbl_TerminErstellen_ErstelltFuer;
		private System.Windows.Forms.Label lbl_TerminErstellen_DatumBis;
		private System.Windows.Forms.DateTimePicker dtp_TerminErstellen_DatumVon;
		private System.Windows.Forms.Label lbl_TerminErstellen_ErstelltVon;
		private System.Windows.Forms.Label lbl_TerminErstellen_DatumVon;
		private System.Windows.Forms.CheckBox cbx_IstWichtig;
		private System.Windows.Forms.RichTextBox txt_TerminErstellen_Betreff;
		private System.Windows.Forms.ComboBox cmb_TerminErstellen_ErstelltFuer;
		
		#endregion


		#region Konstruktor & Destruktor
		public usc_Termin(Cst_MAT pin_stMAT)
		{
			this._stMAT = pin_stMAT;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			this.SetzeEvents();

			this.InitAlleSTE();
			this._b_FelderModifiziert = false;
		}
		#region Setze- Methoden
		private void InitComboBox(ComboBox pinout_cmb)
		{
			if(pinout_cmb.Items.Count >0 )
				pinout_cmb.SelectedIndex = 0;
			else // TODO: Fehldermeldung
				pinout_cmb.Items.Add("Daten werden nicht geladen");
		
		}
	
		private void InitAlleSTE()
		{
			SetzeErstelltVon();
			SetzeErstelltFuer();			
		}

		private void SetzeErstelltVon()
		{
			this.lbl_Termin_ErstellenVonText.Text = this._stMAT.HoleAktuellenBenutzer().Benutzername;
		}
		private void SetzeErstelltFuer()
		{
			Cdv_Benutzer[] benutzermenge = this._stMAT.AlleBenutzer;
			foreach(Cdv_Benutzer benutzer in benutzermenge)
			{
				this.cmb_TerminErstellen_ErstelltFuer.Items.Add(benutzer);
			}
			this.InitComboBox(cmb_TerminErstellen_ErstelltFuer);
		}

		/// <summary>
		/// setzt alle Eingabefelder zurück in den Ausgangszustand
		/// </summary>
		private void Zuruecksetzen()
		{
			
			this.InitComboBox(this.cmb_TerminErstellen_ErstelltFuer);
			
			this.txt_TerminErstellen_Betreff.Text = "";

			this.dtp_TerminErstellen_DatumBis.Value = DateTime.Now;
			this.dtp_TerminErstellen_DatumVon.Value = DateTime.Now;
			this.dtp_TerminErstellen_ErinnernAm.Value = DateTime.Now;

			this.cbx_Termin_DatumBisJetzt.Checked = false;
			this.cbx_Termin_DatumVonJetzt.Checked = false;
			this.cbx_Termin_ErinnerungszeitpunktJetzt.Checked = false;
			this.cbx_TerminErstellen_ErinnernAm.Checked = false;
			this.cbx_TerminErstellen_IstInToDoListe.Checked = false;
			this.cbx_IstWichtig.Checked = false;
			this.cbx_IstWichtig.Checked = false;

			_b_FelderModifiziert = false;

		}

		/// <summary>
		/// erlaubt die Rückfrage beim Benutzer bevor alle Werte zurückgesetzt werden
		/// </summary>
		public void ZuruecksetzenMitRueckfrage()
		{
			if (_b_FelderModifiziert && 
				(pELS.GUI.PopUp.CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes))
			{
				Zuruecksetzen();
			}
		}

		/// <summary>
		/// Referenz auf das entsprechende Element der Steuerungsschicht
		/// </summary>
		public bool b_FelderModifiziert
		{
			get { return _b_FelderModifiziert;}
			set { _b_FelderModifiziert = value;}
		}
		/// <summary>
		/// event, welches bei allen Eingabeelementen registriert ist
		/// und eine vorgenommene Änderung registriert
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FelderModifiziert(object sender, System.EventArgs e)
		{
			_b_FelderModifiziert = true;
		}


		#endregion


	

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
			this.ep_Eingabe = new System.Windows.Forms.ErrorProvider();
			this.gbx_TerminErstellen_NeuerTermin = new System.Windows.Forms.GroupBox();
			this.lbl_Termin_ErstellenVonText = new System.Windows.Forms.Label();
			this.cbx_Termin_DatumBisJetzt = new System.Windows.Forms.CheckBox();
			this.cbx_Termin_DatumVonJetzt = new System.Windows.Forms.CheckBox();
			this.cbx_TerminErstellen_IstInToDoListe = new System.Windows.Forms.CheckBox();
			this.gbx_TerminErstellen_Erinnerung = new System.Windows.Forms.GroupBox();
			this.dtp_TerminErstellen_ErinnernAm = new System.Windows.Forms.DateTimePicker();
			this.cbx_TerminErstellen_ErinnernAm = new System.Windows.Forms.CheckBox();
			this.cbx_Termin_ErinnerungszeitpunktJetzt = new System.Windows.Forms.CheckBox();
			this.dtp_TerminErstellen_DatumBis = new System.Windows.Forms.DateTimePicker();
			this.lbl_TerminErstellen_ErstelltFuer = new System.Windows.Forms.Label();
			this.lbl_TerminErstellen_DatumBis = new System.Windows.Forms.Label();
			this.dtp_TerminErstellen_DatumVon = new System.Windows.Forms.DateTimePicker();
			this.lbl_TerminErstellen_ErstelltVon = new System.Windows.Forms.Label();
			this.lbl_TerminErstellen_DatumVon = new System.Windows.Forms.Label();
			this.cmb_TerminErstellen_ErstelltFuer = new System.Windows.Forms.ComboBox();
			this.btn_Auftrag_AuftragZuruecksetzen = new System.Windows.Forms.Button();
			this.btn_Auftrag_AuftragSpeichern = new System.Windows.Forms.Button();
			this.cbx_IstWichtig = new System.Windows.Forms.CheckBox();
			this.txt_TerminErstellen_Betreff = new System.Windows.Forms.RichTextBox();
			this.gbx_TerminErstellen_NeuerTermin.SuspendLayout();
			this.gbx_TerminErstellen_Erinnerung.SuspendLayout();
			this.SuspendLayout();
			// 
			// ep_Eingabe
			// 
			this.ep_Eingabe.ContainerControl = this;
			// 
			// gbx_TerminErstellen_NeuerTermin
			// 
			this.gbx_TerminErstellen_NeuerTermin.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_TerminErstellen_NeuerTermin.Controls.Add(this.txt_TerminErstellen_Betreff);
			this.gbx_TerminErstellen_NeuerTermin.Controls.Add(this.cbx_IstWichtig);
			this.gbx_TerminErstellen_NeuerTermin.Controls.Add(this.lbl_Termin_ErstellenVonText);
			this.gbx_TerminErstellen_NeuerTermin.Controls.Add(this.cbx_Termin_DatumBisJetzt);
			this.gbx_TerminErstellen_NeuerTermin.Controls.Add(this.cbx_Termin_DatumVonJetzt);
			this.gbx_TerminErstellen_NeuerTermin.Controls.Add(this.cbx_TerminErstellen_IstInToDoListe);
			this.gbx_TerminErstellen_NeuerTermin.Controls.Add(this.gbx_TerminErstellen_Erinnerung);
			this.gbx_TerminErstellen_NeuerTermin.Controls.Add(this.dtp_TerminErstellen_DatumBis);
			this.gbx_TerminErstellen_NeuerTermin.Controls.Add(this.lbl_TerminErstellen_ErstelltFuer);
			this.gbx_TerminErstellen_NeuerTermin.Controls.Add(this.lbl_TerminErstellen_DatumBis);
			this.gbx_TerminErstellen_NeuerTermin.Controls.Add(this.dtp_TerminErstellen_DatumVon);
			this.gbx_TerminErstellen_NeuerTermin.Controls.Add(this.lbl_TerminErstellen_ErstelltVon);
			this.gbx_TerminErstellen_NeuerTermin.Controls.Add(this.lbl_TerminErstellen_DatumVon);
			this.gbx_TerminErstellen_NeuerTermin.Controls.Add(this.cmb_TerminErstellen_ErstelltFuer);
			this.gbx_TerminErstellen_NeuerTermin.Location = new System.Drawing.Point(5, 5);
			this.gbx_TerminErstellen_NeuerTermin.Name = "gbx_TerminErstellen_NeuerTermin";
			this.gbx_TerminErstellen_NeuerTermin.Size = new System.Drawing.Size(615, 240);
			this.gbx_TerminErstellen_NeuerTermin.TabIndex = 0;
			this.gbx_TerminErstellen_NeuerTermin.TabStop = false;
			this.gbx_TerminErstellen_NeuerTermin.Text = "Neuer Termin";
			// 
			// lbl_Termin_ErstellenVonText
			// 
			this.lbl_Termin_ErstellenVonText.BackColor = System.Drawing.SystemColors.Window;
			this.lbl_Termin_ErstellenVonText.Location = new System.Drawing.Point(85, 25);
			this.lbl_Termin_ErstellenVonText.Name = "lbl_Termin_ErstellenVonText";
			this.lbl_Termin_ErstellenVonText.Size = new System.Drawing.Size(130, 15);
			this.lbl_Termin_ErstellenVonText.TabIndex = 52;
			// 
			// cbx_Termin_DatumBisJetzt
			// 
			this.cbx_Termin_DatumBisJetzt.Location = new System.Drawing.Point(425, 40);
			this.cbx_Termin_DatumBisJetzt.Name = "cbx_Termin_DatumBisJetzt";
			this.cbx_Termin_DatumBisJetzt.Size = new System.Drawing.Size(50, 20);
			this.cbx_Termin_DatumBisJetzt.TabIndex = 5;
			this.cbx_Termin_DatumBisJetzt.Text = "Jetzt";
			this.cbx_Termin_DatumBisJetzt.CheckedChanged += new System.EventHandler(this.cbx_Termin_DatumBisJetzt_CheckedChanged);
			// 
			// cbx_Termin_DatumVonJetzt
			// 
			this.cbx_Termin_DatumVonJetzt.Location = new System.Drawing.Point(425, 20);
			this.cbx_Termin_DatumVonJetzt.Name = "cbx_Termin_DatumVonJetzt";
			this.cbx_Termin_DatumVonJetzt.Size = new System.Drawing.Size(50, 20);
			this.cbx_Termin_DatumVonJetzt.TabIndex = 3;
			this.cbx_Termin_DatumVonJetzt.Text = "Jetzt";
			this.cbx_Termin_DatumVonJetzt.CheckedChanged += new System.EventHandler(this.cbx_Termin_DatumVonJetzt_CheckedChanged);
			// 
			// cbx_TerminErstellen_IstInToDoListe
			// 
			this.cbx_TerminErstellen_IstInToDoListe.Location = new System.Drawing.Point(20, 90);
			this.cbx_TerminErstellen_IstInToDoListe.Name = "cbx_TerminErstellen_IstInToDoListe";
			this.cbx_TerminErstellen_IstInToDoListe.Size = new System.Drawing.Size(115, 20);
			this.cbx_TerminErstellen_IstInToDoListe.TabIndex = 2;
			this.cbx_TerminErstellen_IstInToDoListe.Text = "ist in ToDo-Liste";
			this.cbx_TerminErstellen_IstInToDoListe.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// gbx_TerminErstellen_Erinnerung
			// 
			this.gbx_TerminErstellen_Erinnerung.Controls.Add(this.dtp_TerminErstellen_ErinnernAm);
			this.gbx_TerminErstellen_Erinnerung.Controls.Add(this.cbx_TerminErstellen_ErinnernAm);
			this.gbx_TerminErstellen_Erinnerung.Controls.Add(this.cbx_Termin_ErinnerungszeitpunktJetzt);
			this.gbx_TerminErstellen_Erinnerung.Location = new System.Drawing.Point(315, 70);
			this.gbx_TerminErstellen_Erinnerung.Name = "gbx_TerminErstellen_Erinnerung";
			this.gbx_TerminErstellen_Erinnerung.Size = new System.Drawing.Size(300, 48);
			this.gbx_TerminErstellen_Erinnerung.TabIndex = 7;
			this.gbx_TerminErstellen_Erinnerung.TabStop = false;
			this.gbx_TerminErstellen_Erinnerung.Text = "Erinnerung";
			// 
			// dtp_TerminErstellen_ErinnernAm
			// 
			this.dtp_TerminErstellen_ErinnernAm.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_TerminErstellen_ErinnernAm.Enabled = false;
			this.dtp_TerminErstellen_ErinnernAm.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_TerminErstellen_ErinnernAm.Location = new System.Drawing.Point(170, 20);
			this.dtp_TerminErstellen_ErinnernAm.MinDate = new System.DateTime(2004, 11, 2, 0, 0, 0, 0);
			this.dtp_TerminErstellen_ErinnernAm.Name = "dtp_TerminErstellen_ErinnernAm";
			this.dtp_TerminErstellen_ErinnernAm.Size = new System.Drawing.Size(120, 20);
			this.dtp_TerminErstellen_ErinnernAm.TabIndex = 2;
			// 
			// cbx_TerminErstellen_ErinnernAm
			// 
			this.cbx_TerminErstellen_ErinnernAm.Location = new System.Drawing.Point(10, 20);
			this.cbx_TerminErstellen_ErinnernAm.Name = "cbx_TerminErstellen_ErinnernAm";
			this.cbx_TerminErstellen_ErinnernAm.Size = new System.Drawing.Size(94, 20);
			this.cbx_TerminErstellen_ErinnernAm.TabIndex = 0;
			this.cbx_TerminErstellen_ErinnernAm.Text = "Erinnern am:";
			this.cbx_TerminErstellen_ErinnernAm.CheckedChanged += new System.EventHandler(this.cbx_TerminErstellen_ErinnernAm_CheckedChanged);
			// 
			// cbx_Termin_ErinnerungszeitpunktJetzt
			// 
			this.cbx_Termin_ErinnerungszeitpunktJetzt.Enabled = false;
			this.cbx_Termin_ErinnerungszeitpunktJetzt.Location = new System.Drawing.Point(110, 20);
			this.cbx_Termin_ErinnerungszeitpunktJetzt.Name = "cbx_Termin_ErinnerungszeitpunktJetzt";
			this.cbx_Termin_ErinnerungszeitpunktJetzt.Size = new System.Drawing.Size(50, 20);
			this.cbx_Termin_ErinnerungszeitpunktJetzt.TabIndex = 1;
			this.cbx_Termin_ErinnerungszeitpunktJetzt.Text = "Jetzt";
			this.cbx_Termin_ErinnerungszeitpunktJetzt.CheckedChanged += new System.EventHandler(this.cbx_Termin_ErinnerungszeitpunktJetzt_CheckedChanged);
			// 
			// dtp_TerminErstellen_DatumBis
			// 
			this.dtp_TerminErstellen_DatumBis.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_TerminErstellen_DatumBis.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_TerminErstellen_DatumBis.Location = new System.Drawing.Point(485, 40);
			this.dtp_TerminErstellen_DatumBis.MinDate = new System.DateTime(2004, 11, 2, 0, 0, 0, 0);
			this.dtp_TerminErstellen_DatumBis.Name = "dtp_TerminErstellen_DatumBis";
			this.dtp_TerminErstellen_DatumBis.Size = new System.Drawing.Size(120, 20);
			this.dtp_TerminErstellen_DatumBis.TabIndex = 6;
			// 
			// lbl_TerminErstellen_ErstelltFuer
			// 
			this.lbl_TerminErstellen_ErstelltFuer.BackColor = System.Drawing.SystemColors.Window;
			this.lbl_TerminErstellen_ErstelltFuer.Location = new System.Drawing.Point(16, 48);
			this.lbl_TerminErstellen_ErstelltFuer.Name = "lbl_TerminErstellen_ErstelltFuer";
			this.lbl_TerminErstellen_ErstelltFuer.Size = new System.Drawing.Size(64, 15);
			this.lbl_TerminErstellen_ErstelltFuer.TabIndex = 21;
			this.lbl_TerminErstellen_ErstelltFuer.Text = "Erstellt für:";
			// 
			// lbl_TerminErstellen_DatumBis
			// 
			this.lbl_TerminErstellen_DatumBis.Location = new System.Drawing.Point(330, 45);
			this.lbl_TerminErstellen_DatumBis.Name = "lbl_TerminErstellen_DatumBis";
			this.lbl_TerminErstellen_DatumBis.Size = new System.Drawing.Size(80, 15);
			this.lbl_TerminErstellen_DatumBis.TabIndex = 18;
			this.lbl_TerminErstellen_DatumBis.Text = "Datum Bis:";
			// 
			// dtp_TerminErstellen_DatumVon
			// 
			this.dtp_TerminErstellen_DatumVon.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_TerminErstellen_DatumVon.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_TerminErstellen_DatumVon.Location = new System.Drawing.Point(485, 20);
			this.dtp_TerminErstellen_DatumVon.MinDate = new System.DateTime(2004, 11, 2, 0, 0, 0, 0);
			this.dtp_TerminErstellen_DatumVon.Name = "dtp_TerminErstellen_DatumVon";
			this.dtp_TerminErstellen_DatumVon.Size = new System.Drawing.Size(120, 20);
			this.dtp_TerminErstellen_DatumVon.TabIndex = 4;
			// 
			// lbl_TerminErstellen_ErstelltVon
			// 
			this.lbl_TerminErstellen_ErstelltVon.BackColor = System.Drawing.SystemColors.Window;
			this.lbl_TerminErstellen_ErstelltVon.Location = new System.Drawing.Point(16, 24);
			this.lbl_TerminErstellen_ErstelltVon.Name = "lbl_TerminErstellen_ErstelltVon";
			this.lbl_TerminErstellen_ErstelltVon.Size = new System.Drawing.Size(64, 15);
			this.lbl_TerminErstellen_ErstelltVon.TabIndex = 16;
			this.lbl_TerminErstellen_ErstelltVon.Text = "Erstellt von:";
			// 
			// lbl_TerminErstellen_DatumVon
			// 
			this.lbl_TerminErstellen_DatumVon.Location = new System.Drawing.Point(330, 24);
			this.lbl_TerminErstellen_DatumVon.Name = "lbl_TerminErstellen_DatumVon";
			this.lbl_TerminErstellen_DatumVon.Size = new System.Drawing.Size(80, 15);
			this.lbl_TerminErstellen_DatumVon.TabIndex = 2;
			this.lbl_TerminErstellen_DatumVon.Text = "Datum von: ";
			// 
			// cmb_TerminErstellen_ErstelltFuer
			// 
			this.cmb_TerminErstellen_ErstelltFuer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_TerminErstellen_ErstelltFuer.Location = new System.Drawing.Point(80, 45);
			this.cmb_TerminErstellen_ErstelltFuer.Name = "cmb_TerminErstellen_ErstelltFuer";
			this.cmb_TerminErstellen_ErstelltFuer.Size = new System.Drawing.Size(136, 21);
			this.cmb_TerminErstellen_ErstelltFuer.TabIndex = 0;
			this.cmb_TerminErstellen_ErstelltFuer.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// btn_Auftrag_AuftragZuruecksetzen
			// 
			this.btn_Auftrag_AuftragZuruecksetzen.Location = new System.Drawing.Point(420, 420);
			this.btn_Auftrag_AuftragZuruecksetzen.Name = "btn_Auftrag_AuftragZuruecksetzen";
			this.btn_Auftrag_AuftragZuruecksetzen.Size = new System.Drawing.Size(85, 25);
			this.btn_Auftrag_AuftragZuruecksetzen.TabIndex = 2;
			this.btn_Auftrag_AuftragZuruecksetzen.Text = "&Zurücksetzen";
			this.btn_Auftrag_AuftragZuruecksetzen.Click += new System.EventHandler(this.btn_Auftrag_AuftragZuruecksetzen_Click);
			// 
			// btn_Auftrag_AuftragSpeichern
			// 
			this.btn_Auftrag_AuftragSpeichern.Location = new System.Drawing.Point(510, 420);
			this.btn_Auftrag_AuftragSpeichern.Name = "btn_Auftrag_AuftragSpeichern";
			this.btn_Auftrag_AuftragSpeichern.Size = new System.Drawing.Size(80, 25);
			this.btn_Auftrag_AuftragSpeichern.TabIndex = 1;
			this.btn_Auftrag_AuftragSpeichern.Text = "&Speichern";
			this.btn_Auftrag_AuftragSpeichern.Click += new System.EventHandler(this.btn_Auftrag_AuftragSpeichern_Click);
			// 
			// cbx_IstWichtig
			// 
			this.cbx_IstWichtig.Location = new System.Drawing.Point(20, 70);
			this.cbx_IstWichtig.Name = "cbx_IstWichtig";
			this.cbx_IstWichtig.Size = new System.Drawing.Size(115, 20);
			this.cbx_IstWichtig.TabIndex = 53;
			this.cbx_IstWichtig.Text = "ist wichtig";
			// 
			// txt_TerminErstellen_Betreff
			// 
			this.txt_TerminErstellen_Betreff.Location = new System.Drawing.Point(5, 125);
			this.txt_TerminErstellen_Betreff.Name = "txt_TerminErstellen_Betreff";
			this.txt_TerminErstellen_Betreff.Size = new System.Drawing.Size(595, 110);
			this.txt_TerminErstellen_Betreff.TabIndex = 54;
			this.txt_TerminErstellen_Betreff.Text = "";
			// 
			// usc_Termin
			// 
			this.Controls.Add(this.btn_Auftrag_AuftragZuruecksetzen);
			this.Controls.Add(this.btn_Auftrag_AuftragSpeichern);
			this.Controls.Add(this.gbx_TerminErstellen_NeuerTermin);
			this.Location = new System.Drawing.Point(6, 21);
			this.Name = "usc_Termin";
			this.Size = new System.Drawing.Size(624, 456);
			this.gbx_TerminErstellen_NeuerTermin.ResumeLayout(false);
			this.gbx_TerminErstellen_Erinnerung.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#endregion

		#region Setze- Methoden
		private void SetzeEvents()
		{
			this.btn_Auftrag_AuftragZuruecksetzen.Click += new System.EventHandler(this.btn_Auftrag_AuftragZuruecksetzen_Click);
			this.cmb_TerminErstellen_ErstelltFuer.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_Termin_DatumVonJetzt.CheckedChanged += new System.EventHandler(this.cbx_Termin_DatumVonJetzt_CheckedChanged);
			this.cbx_TerminErstellen_IstInToDoListe.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_TerminErstellen_ErinnernAm.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_TerminErstellen_ErinnernAm.CheckedChanged += new EventHandler( cbx_TerminErstellen_ErinnernAm_CheckedChanged);
			this.cbx_Termin_ErinnerungszeitpunktJetzt.CheckedChanged += new System.EventHandler(this.cbx_Termin_ErinnerungszeitpunktJetzt_CheckedChanged);
			this.txt_TerminErstellen_Betreff.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_IstWichtig.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
		
		}

		#endregion


		#region Eingabevalidierung
		/// <summary>
		/// überprüft alle zwingend benötigten Eingaben auf Korrektheit
		/// </summary>
		/// <returns></returns>
		virtual protected bool Eingabevalidierung()
		{	
			// prüfe ob alle benötigten Felder korrekt sind
			return ValidiereText();	
		}

	
		#region Betreff
		/// <summary>
		/// überprüfe den Text
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereText()
		{
			if(txt_TerminErstellen_Betreff.Text.Length > 0)
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(txt_TerminErstellen_Betreff, "");
				return true;
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(txt_TerminErstellen_Betreff, "Bitte geben Sie einen Text ein");
				return false;
			}
		}


		#endregion
		#endregion

		#region event handler
		private void btn_Auftrag_AuftragSpeichern_Click(object sender, System.EventArgs e)
		{
			if(Eingabevalidierung())
			{
				this.SpeichereTermin();
				// Eingaben zurücksetzen
				this.Zuruecksetzen();
			}
			else
			{
				pELS.GUI.PopUp.CPopUp.MeldenVonFalscherEingabe();
			}
			
		}
		private void btn_Auftrag_AuftragZuruecksetzen_Click(object sender, System.EventArgs e)
		{

			this.ZuruecksetzenMitRueckfrage();
		}

		private void cbx_TerminErstellen_ErinnernAm_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.cbx_TerminErstellen_ErinnernAm.Checked == true)
			{
				this.cbx_Termin_ErinnerungszeitpunktJetzt.Enabled = true;
				this.dtp_TerminErstellen_ErinnernAm.Enabled = true;
			}
			else
			{
				this.cbx_Termin_ErinnerungszeitpunktJetzt.Enabled = false;
				this.dtp_TerminErstellen_ErinnernAm.Enabled = false;
			
			}
		}

		private void cbx_Termin_DatumVonJetzt_CheckedChanged(object sender, System.EventArgs e)
		{
			if(cbx_Termin_DatumVonJetzt.Checked == true)
				this.dtp_TerminErstellen_DatumVon.Enabled = false;
			else
				this.dtp_TerminErstellen_DatumVon.Enabled = true;
		}

		private void cbx_Termin_DatumBisJetzt_CheckedChanged(object sender, System.EventArgs e)
		{
			if(cbx_Termin_DatumBisJetzt.Checked == true)
				this.dtp_TerminErstellen_DatumBis.Enabled = false;
			else
				this.dtp_TerminErstellen_DatumBis.Enabled = true;
		}
		private void cbx_Termin_ErinnerungszeitpunktJetzt_CheckedChanged(object sender, System.EventArgs e)
		{
			if(cbx_Termin_ErinnerungszeitpunktJetzt.Checked == true)
				this.dtp_TerminErstellen_ErinnernAm.Enabled = false;
			else
				this.dtp_TerminErstellen_ErinnernAm.Enabled = true;
		}

		#endregion

		#region Speichern
		private void SpeichereTermin()
		{
			Cdv_Termin pout_Termin = new Cdv_Termin(this.txt_TerminErstellen_Betreff.Text, 
				                                   this._stMAT.HoleAktuellenBenutzer().ID, 
													(this.cmb_TerminErstellen_ErstelltFuer.SelectedItem as Cdv_Benutzer).ID,
													this.cbx_TerminErstellen_IstInToDoListe.Checked);

			pout_Termin.IstWichtig = this.cbx_IstWichtig.Checked;
			pout_Termin.WirdErinnert = this.cbx_TerminErstellen_ErinnernAm.Checked;
			if(this.cbx_Termin_DatumBisJetzt.Checked == true) pout_Termin.ZeitBis = DateTime.Now;
			else pout_Termin.ZeitBis = this.dtp_TerminErstellen_DatumBis.Value;
			if(this.cbx_Termin_DatumVonJetzt.Checked == true) pout_Termin.ZeitVon = DateTime.Now;
			else pout_Termin.ZeitVon = this.dtp_TerminErstellen_DatumVon.Value;
			pout_Termin.Erinnerung.Erinnerungstext = this.txt_TerminErstellen_Betreff.Text;
			if(this.cbx_TerminErstellen_ErinnernAm.Checked == true) 
			{
				if(this.cbx_Termin_ErinnerungszeitpunktJetzt.Checked == true)
					pout_Termin.Erinnerung.Zeitpunkt = DateTime.Now;
				else pout_Termin.Erinnerung.Zeitpunkt = this.dtp_TerminErstellen_ErinnernAm.Value;
			}
			else pout_Termin.Erinnerung.Zeitpunkt = DateTimePicker.MinDateTime;			
			//TODO: was ist TerminID unter Erinnerung

			this._stMAT.SpeichereTermin(pout_Termin);
		}
		#endregion

		#region dynamische Daten-Akualisierung
		/// <summary>
		/// Wenn die Menge aller Benutzer verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AkualisiereBenutzer()
		{
			this.SetzeErstelltFuer();
		}

		/// <summary>
		/// Wenn die Systemrolle des aktuellen Benutzers verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AkualisiereAktBenutzer()
		{
			this.lbl_Termin_ErstellenVonText.Text = this._stMAT.HoleAktuellenBenutzer().Benutzername;
		}
		#endregion

	

	}
}