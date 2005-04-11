using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pELS.Client.Funk
{
	// benötigt für: PopUp
	using pELS.GUI.PopUp;
	// benötigt für: PelsObjekte
	using pELS.DV;

	#region Dokumentation
	/**				aktuelle Version: 1.0 Schuppe
	INFO:
		- kapselt die Eingabemaske zum Erfassen/Anzeigen von Meldungen
		
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
	public class usc_Meldung : System.Windows.Forms.UserControl
	{
		#region graphische Variablen
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		protected System.Windows.Forms.Label lbl_Empfaenger;
		protected System.Windows.Forms.Label lbl_Kategorie;
		protected System.Windows.Forms.Label lbl_Erkunder;
		protected System.Windows.Forms.Label lbl_Bezeichnung;
		protected System.Windows.Forms.Label lbl_Meldungstext;
		protected System.Windows.Forms.Label lbl_RolleDP;
		protected System.Windows.Forms.Label lbl_Rolle;
		protected System.Windows.Forms.Label lbl_TEXT_Absender;
		protected System.Windows.Forms.Label lbl_Auftrag_Auftragskontext_Datum;
		protected System.Windows.Forms.Label lbl_UMZeitpunkt;
		protected System.Windows.Forms.Label lbl_Auftrag_Auftragskontext_Uebermittlungsart;
		protected System.Windows.Forms.Label lbl_internerEmpfaenger;
		protected System.Windows.Forms.Panel pnl_Meldung;
		protected System.Windows.Forms.Button btn_Drucken;
		protected System.Windows.Forms.Button btn_Zuruecksetzen;
		protected System.Windows.Forms.Button btn_Speichern;
		protected System.Windows.Forms.GroupBox gbx_Meldungskontext;
		protected System.Windows.Forms.GroupBox gbx_Erkundung;
		protected System.Windows.Forms.TextBox txt_Strasse;
		protected System.Windows.Forms.TextBox txt_Erkundungsobjekt;
		protected System.Windows.Forms.CheckBox cbx_Strom;
		protected System.Windows.Forms.CheckBox cbx_Wasser;
		protected System.Windows.Forms.TextBox txt_Erkunder;
		protected System.Windows.Forms.TextBox txt_Ort;
		protected System.Windows.Forms.CheckBox cbx_Abwasser;
		protected System.Windows.Forms.TextBox txt_PLZ;
		protected System.Windows.Forms.TextBox txt_HausNr;
		protected System.Windows.Forms.GroupBox gbx_Meldungstyp;
		protected System.Windows.Forms.RadioButton rBtn_Meldungart_Erkundungsbericht;
		protected System.Windows.Forms.RadioButton rBtn_Meldungart_Meldung;
		protected System.Windows.Forms.ComboBox cmb_Meldungskategorie;
		protected System.Windows.Forms.GroupBox gbx_Abfassung;
		protected System.Windows.Forms.DateTimePicker dtp_AbfassungsDatum;
		protected System.Windows.Forms.TextBox txt_Absender;
		protected System.Windows.Forms.GroupBox gbx_Ubermittlung;
		protected System.Windows.Forms.DateTimePicker dtp_Uebermittlungsdatum;
		protected System.Windows.Forms.CheckBox cbx_UebermittlungszeitpunktJetzt;
		protected System.Windows.Forms.ComboBox cmb_Uebermittlungsart;
		protected System.Windows.Forms.CheckBox cbx_AbfassungsdatumJetzt;
		protected System.Windows.Forms.RichTextBox txt_Meldungstext;
		protected System.Windows.Forms.CheckBox cbx_IstUebermittelt;
		protected System.Windows.Forms.CheckBox cbx_KellerIstVorhanden;
		protected System.Windows.Forms.Label lbl_Einsatzschwerpunkt;
		protected System.Windows.Forms.Label lbl_Erk_Datum;
		protected System.Windows.Forms.DateTimePicker dtp_Erk_Datum;
		protected System.Windows.Forms.GroupBox groupBox3;
		protected System.Windows.Forms.GroupBox groupBox4;
		protected System.Windows.Forms.Label label1;
		protected System.Windows.Forms.Label label2;
		protected System.Windows.Forms.Label label3;
		protected System.Windows.Forms.Label label4;
		protected System.Windows.Forms.Label label5;
		protected System.Windows.Forms.GroupBox groupBox5;
		protected System.Windows.Forms.Label lbl_Beabeiter;
		protected System.Windows.Forms.Label lbl_BearbeiterID;
		protected pELS.Tools.MaskedTextBox txt_KellerInProzent;
		protected System.Windows.Forms.ErrorProvider ep_Eingabe;
		protected System.Windows.Forms.Label lbl_laufendeNummer;
		protected System.Windows.Forms.Label lbl_TEXT_laufendeNummer;

		// müssen nach außen hin sichtbar sein, da diese 
		// ständig anpassbar sein müssen
		public System.Windows.Forms.ComboBox cmb_Benutzerempfaenger;
		public System.Windows.Forms.TreeView tvw_Empfaenger;

		private System.ComponentModel.Container components = null;
		#endregion
		#region Variablen
		/// <summary>
		/// gibt an, ob bereits eine Eingabe geschehen ist
		/// </summary>

	
		/// <summary>
		/// gibt an, ob bereits Eingaben geschehen sind
		/// </summary>
		protected bool _b_FelderModifiziert = false;
		public bool b_FelderModifiziert
		{
			get { return _b_FelderModifiziert;}
			set { _b_FelderModifiziert = value;}
		}
		/// <summary>
		/// Referenz auf das entsprechende Element der Steuerungsschicht
		/// </summary>
		private Cst_Funk _st_Funk;
		public System.Windows.Forms.ComboBox cmb_Einsatzschwerpunkte;
		/// <summary>
		/// speichert die ID der aktuell angezeigten Mitteilung
		/// wird benötigt beim Laden und anschließendem Speichern einer Mitteilung
		/// </summary>
		private int _aktuelleMitteilungsID = 0;
		private System.Windows.Forms.GroupBox gbx_;
		protected System.Windows.Forms.ComboBox cmb_Bauart;
		protected System.Windows.Forms.Label lbl_Heizung;
		protected System.Windows.Forms.Label lbl_Haustyp;
		protected System.Windows.Forms.Label lbl_Bauart;
		protected System.Windows.Forms.TextBox txt_Heizung;
		protected System.Windows.Forms.TextBox txt_Haustyp;
		/// <summary>
		/// zur Übergabe an das Event ReportRequested, um die Mitteilung drucken zu können
		/// </summary>
		private Cdv_Mitteilung _zuletztGespeicherteMitteilung;
		#endregion


		#region Konstruktor & Destruktor
		public usc_Meldung(Cst_Funk pin_Cst_Funk)
		{ 
			this._st_Funk = pin_Cst_Funk;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			SetzeEvents();
			
			SetzeGUIElemente();
			SetzeMeldungsKategorie();
			SetzeUebermittlungsart();
			SetzeBauart();
			Zuruecksetzen();
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
			this.pnl_Meldung = new System.Windows.Forms.Panel();
			this.btn_Drucken = new System.Windows.Forms.Button();
			this.btn_Zuruecksetzen = new System.Windows.Forms.Button();
			this.btn_Speichern = new System.Windows.Forms.Button();
			this.tvw_Empfaenger = new System.Windows.Forms.TreeView();
			this.lbl_Empfaenger = new System.Windows.Forms.Label();
			this.gbx_Meldungskontext = new System.Windows.Forms.GroupBox();
			this.lbl_laufendeNummer = new System.Windows.Forms.Label();
			this.lbl_TEXT_laufendeNummer = new System.Windows.Forms.Label();
			this.gbx_Ubermittlung = new System.Windows.Forms.GroupBox();
			this.cmb_Uebermittlungsart = new System.Windows.Forms.ComboBox();
			this.dtp_Uebermittlungsdatum = new System.Windows.Forms.DateTimePicker();
			this.lbl_UMZeitpunkt = new System.Windows.Forms.Label();
			this.cbx_IstUebermittelt = new System.Windows.Forms.CheckBox();
			this.lbl_Auftrag_Auftragskontext_Uebermittlungsart = new System.Windows.Forms.Label();
			this.cbx_UebermittlungszeitpunktJetzt = new System.Windows.Forms.CheckBox();
			this.gbx_Abfassung = new System.Windows.Forms.GroupBox();
			this.lbl_TEXT_Absender = new System.Windows.Forms.Label();
			this.lbl_Auftrag_Auftragskontext_Datum = new System.Windows.Forms.Label();
			this.dtp_AbfassungsDatum = new System.Windows.Forms.DateTimePicker();
			this.cbx_AbfassungsdatumJetzt = new System.Windows.Forms.CheckBox();
			this.txt_Absender = new System.Windows.Forms.TextBox();
			this.gbx_Meldungstyp = new System.Windows.Forms.GroupBox();
			this.rBtn_Meldungart_Meldung = new System.Windows.Forms.RadioButton();
			this.rBtn_Meldungart_Erkundungsbericht = new System.Windows.Forms.RadioButton();
			this.lbl_Beabeiter = new System.Windows.Forms.Label();
			this.lbl_Rolle = new System.Windows.Forms.Label();
			this.lbl_Kategorie = new System.Windows.Forms.Label();
			this.cmb_Meldungskategorie = new System.Windows.Forms.ComboBox();
			this.lbl_BearbeiterID = new System.Windows.Forms.Label();
			this.lbl_RolleDP = new System.Windows.Forms.Label();
			this.gbx_Erkundung = new System.Windows.Forms.GroupBox();
			this.gbx_ = new System.Windows.Forms.GroupBox();
			this.cmb_Bauart = new System.Windows.Forms.ComboBox();
			this.lbl_Heizung = new System.Windows.Forms.Label();
			this.lbl_Haustyp = new System.Windows.Forms.Label();
			this.lbl_Bauart = new System.Windows.Forms.Label();
			this.txt_Heizung = new System.Windows.Forms.TextBox();
			this.txt_Haustyp = new System.Windows.Forms.TextBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.cbx_Strom = new System.Windows.Forms.CheckBox();
			this.cbx_Wasser = new System.Windows.Forms.CheckBox();
			this.cbx_Abwasser = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.txt_KellerInProzent = new pELS.Tools.MaskedTextBox();
			this.cbx_KellerIstVorhanden = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txt_Strasse = new System.Windows.Forms.TextBox();
			this.txt_Ort = new System.Windows.Forms.TextBox();
			this.txt_PLZ = new System.Windows.Forms.TextBox();
			this.txt_HausNr = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.dtp_Erk_Datum = new System.Windows.Forms.DateTimePicker();
			this.lbl_Erk_Datum = new System.Windows.Forms.Label();
			this.lbl_Einsatzschwerpunkt = new System.Windows.Forms.Label();
			this.cmb_Einsatzschwerpunkte = new System.Windows.Forms.ComboBox();
			this.lbl_Bezeichnung = new System.Windows.Forms.Label();
			this.txt_Erkundungsobjekt = new System.Windows.Forms.TextBox();
			this.txt_Erkunder = new System.Windows.Forms.TextBox();
			this.lbl_Erkunder = new System.Windows.Forms.Label();
			this.txt_Meldungstext = new System.Windows.Forms.RichTextBox();
			this.lbl_Meldungstext = new System.Windows.Forms.Label();
			this.lbl_internerEmpfaenger = new System.Windows.Forms.Label();
			this.cmb_Benutzerempfaenger = new System.Windows.Forms.ComboBox();
			this.ep_Eingabe = new System.Windows.Forms.ErrorProvider();
			this.gbx_Meldungskontext.SuspendLayout();
			this.gbx_Ubermittlung.SuspendLayout();
			this.gbx_Abfassung.SuspendLayout();
			this.gbx_Meldungstyp.SuspendLayout();
			this.gbx_Erkundung.SuspendLayout();
			this.gbx_.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnl_Meldung
			// 
			this.pnl_Meldung.Location = new System.Drawing.Point(0, 0);
			this.pnl_Meldung.Name = "pnl_Meldung";
			this.pnl_Meldung.TabIndex = 0;
			// 
			// btn_Drucken
			// 
			this.btn_Drucken.Location = new System.Drawing.Point(304, 424);
			this.btn_Drucken.Name = "btn_Drucken";
			this.btn_Drucken.Size = new System.Drawing.Size(136, 25);
			this.btn_Drucken.TabIndex = 4;
			this.btn_Drucken.Text = "Speichern && &Drucken";
			this.btn_Drucken.Click += new System.EventHandler(this.btn_Drucken_Click);
			// 
			// btn_Zuruecksetzen
			// 
			this.btn_Zuruecksetzen.Location = new System.Drawing.Point(448, 424);
			this.btn_Zuruecksetzen.Name = "btn_Zuruecksetzen";
			this.btn_Zuruecksetzen.Size = new System.Drawing.Size(81, 25);
			this.btn_Zuruecksetzen.TabIndex = 5;
			this.btn_Zuruecksetzen.Text = "&Zurücksetzen";
			this.btn_Zuruecksetzen.Click += new System.EventHandler(this.btn_Zuruecksetzen_Click);
			// 
			// btn_Speichern
			// 
			this.btn_Speichern.Location = new System.Drawing.Point(536, 424);
			this.btn_Speichern.Name = "btn_Speichern";
			this.btn_Speichern.Size = new System.Drawing.Size(80, 25);
			this.btn_Speichern.TabIndex = 6;
			this.btn_Speichern.Text = "&Speichern";
			this.btn_Speichern.Click += new System.EventHandler(this.btn_Speichern_Click);
			// 
			// tvw_Empfaenger
			// 
			this.tvw_Empfaenger.CheckBoxes = true;
			this.tvw_Empfaenger.ImageIndex = -1;
			this.tvw_Empfaenger.Location = new System.Drawing.Point(416, 188);
			this.tvw_Empfaenger.Name = "tvw_Empfaenger";
			this.tvw_Empfaenger.SelectedImageIndex = -1;
			this.tvw_Empfaenger.Size = new System.Drawing.Size(189, 180);
			this.tvw_Empfaenger.TabIndex = 3;
			// 
			// lbl_Empfaenger
			// 
			this.lbl_Empfaenger.Location = new System.Drawing.Point(412, 168);
			this.lbl_Empfaenger.Name = "lbl_Empfaenger";
			this.lbl_Empfaenger.Size = new System.Drawing.Size(128, 16);
			this.lbl_Empfaenger.TabIndex = 55;
			this.lbl_Empfaenger.Text = "Meldungsempfänger";
			// 
			// gbx_Meldungskontext
			// 
			this.gbx_Meldungskontext.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_Meldungskontext.Controls.Add(this.lbl_laufendeNummer);
			this.gbx_Meldungskontext.Controls.Add(this.lbl_TEXT_laufendeNummer);
			this.gbx_Meldungskontext.Controls.Add(this.gbx_Ubermittlung);
			this.gbx_Meldungskontext.Controls.Add(this.gbx_Abfassung);
			this.gbx_Meldungskontext.Controls.Add(this.gbx_Meldungstyp);
			this.gbx_Meldungskontext.Controls.Add(this.lbl_Beabeiter);
			this.gbx_Meldungskontext.Controls.Add(this.lbl_Rolle);
			this.gbx_Meldungskontext.Controls.Add(this.lbl_Kategorie);
			this.gbx_Meldungskontext.Controls.Add(this.cmb_Meldungskategorie);
			this.gbx_Meldungskontext.Controls.Add(this.lbl_BearbeiterID);
			this.gbx_Meldungskontext.Controls.Add(this.lbl_RolleDP);
			this.gbx_Meldungskontext.Location = new System.Drawing.Point(6, 0);
			this.gbx_Meldungskontext.Name = "gbx_Meldungskontext";
			this.gbx_Meldungskontext.Size = new System.Drawing.Size(615, 164);
			this.gbx_Meldungskontext.TabIndex = 0;
			this.gbx_Meldungskontext.TabStop = false;
			this.gbx_Meldungskontext.Text = "Meldungskontext";
			// 
			// lbl_laufendeNummer
			// 
			this.lbl_laufendeNummer.Location = new System.Drawing.Point(124, 60);
			this.lbl_laufendeNummer.Name = "lbl_laufendeNummer";
			this.lbl_laufendeNummer.Size = new System.Drawing.Size(100, 16);
			this.lbl_laufendeNummer.TabIndex = 80;
			// 
			// lbl_TEXT_laufendeNummer
			// 
			this.lbl_TEXT_laufendeNummer.Location = new System.Drawing.Point(12, 60);
			this.lbl_TEXT_laufendeNummer.Name = "lbl_TEXT_laufendeNummer";
			this.lbl_TEXT_laufendeNummer.Size = new System.Drawing.Size(100, 20);
			this.lbl_TEXT_laufendeNummer.TabIndex = 79;
			this.lbl_TEXT_laufendeNummer.Text = "lfd. Nummer";
			// 
			// gbx_Ubermittlung
			// 
			this.gbx_Ubermittlung.Controls.Add(this.cmb_Uebermittlungsart);
			this.gbx_Ubermittlung.Controls.Add(this.dtp_Uebermittlungsdatum);
			this.gbx_Ubermittlung.Controls.Add(this.lbl_UMZeitpunkt);
			this.gbx_Ubermittlung.Controls.Add(this.cbx_IstUebermittelt);
			this.gbx_Ubermittlung.Controls.Add(this.lbl_Auftrag_Auftragskontext_Uebermittlungsart);
			this.gbx_Ubermittlung.Controls.Add(this.cbx_UebermittlungszeitpunktJetzt);
			this.gbx_Ubermittlung.Location = new System.Drawing.Point(324, 72);
			this.gbx_Ubermittlung.Name = "gbx_Ubermittlung";
			this.gbx_Ubermittlung.Size = new System.Drawing.Size(280, 84);
			this.gbx_Ubermittlung.TabIndex = 3;
			this.gbx_Ubermittlung.TabStop = false;
			// 
			// cmb_Uebermittlungsart
			// 
			this.cmb_Uebermittlungsart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Uebermittlungsart.Location = new System.Drawing.Point(124, 12);
			this.cmb_Uebermittlungsart.Name = "cmb_Uebermittlungsart";
			this.cmb_Uebermittlungsart.Size = new System.Drawing.Size(148, 21);
			this.cmb_Uebermittlungsart.TabIndex = 0;
			// 
			// dtp_Uebermittlungsdatum
			// 
			this.dtp_Uebermittlungsdatum.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_Uebermittlungsdatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Uebermittlungsdatum.Location = new System.Drawing.Point(152, 52);
			this.dtp_Uebermittlungsdatum.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp_Uebermittlungsdatum.Name = "dtp_Uebermittlungsdatum";
			this.dtp_Uebermittlungsdatum.Size = new System.Drawing.Size(120, 20);
			this.dtp_Uebermittlungsdatum.TabIndex = 3;
			// 
			// lbl_UMZeitpunkt
			// 
			this.lbl_UMZeitpunkt.Location = new System.Drawing.Point(8, 56);
			this.lbl_UMZeitpunkt.Name = "lbl_UMZeitpunkt";
			this.lbl_UMZeitpunkt.Size = new System.Drawing.Size(92, 20);
			this.lbl_UMZeitpunkt.TabIndex = 66;
			this.lbl_UMZeitpunkt.Text = "ÜM-Zeitpunkt";
			// 
			// cbx_IstUebermittelt
			// 
			this.cbx_IstUebermittelt.Checked = true;
			this.cbx_IstUebermittelt.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbx_IstUebermittelt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbx_IstUebermittelt.Location = new System.Drawing.Point(8, 36);
			this.cbx_IstUebermittelt.Name = "cbx_IstUebermittelt";
			this.cbx_IstUebermittelt.Size = new System.Drawing.Size(92, 20);
			this.cbx_IstUebermittelt.TabIndex = 1;
			this.cbx_IstUebermittelt.Text = "ist übermittelt";
			this.cbx_IstUebermittelt.CheckStateChanged += new System.EventHandler(this.cbx_IstUebermittelt_CheckedChanged);
			// 
			// lbl_Auftrag_Auftragskontext_Uebermittlungsart
			// 
			this.lbl_Auftrag_Auftragskontext_Uebermittlungsart.Location = new System.Drawing.Point(8, 16);
			this.lbl_Auftrag_Auftragskontext_Uebermittlungsart.Name = "lbl_Auftrag_Auftragskontext_Uebermittlungsart";
			this.lbl_Auftrag_Auftragskontext_Uebermittlungsart.Size = new System.Drawing.Size(100, 15);
			this.lbl_Auftrag_Auftragskontext_Uebermittlungsart.TabIndex = 3;
			this.lbl_Auftrag_Auftragskontext_Uebermittlungsart.Text = "Übermittlungsart";
			// 
			// cbx_UebermittlungszeitpunktJetzt
			// 
			this.cbx_UebermittlungszeitpunktJetzt.Location = new System.Drawing.Point(104, 56);
			this.cbx_UebermittlungszeitpunktJetzt.Name = "cbx_UebermittlungszeitpunktJetzt";
			this.cbx_UebermittlungszeitpunktJetzt.Size = new System.Drawing.Size(44, 16);
			this.cbx_UebermittlungszeitpunktJetzt.TabIndex = 2;
			this.cbx_UebermittlungszeitpunktJetzt.Text = "jetzt";
			this.cbx_UebermittlungszeitpunktJetzt.CheckedChanged += new System.EventHandler(this.cbx_UebermittlungszeitpunktJetzt_CheckedChanged);
			// 
			// gbx_Abfassung
			// 
			this.gbx_Abfassung.Controls.Add(this.lbl_TEXT_Absender);
			this.gbx_Abfassung.Controls.Add(this.lbl_Auftrag_Auftragskontext_Datum);
			this.gbx_Abfassung.Controls.Add(this.dtp_AbfassungsDatum);
			this.gbx_Abfassung.Controls.Add(this.cbx_AbfassungsdatumJetzt);
			this.gbx_Abfassung.Controls.Add(this.txt_Absender);
			this.gbx_Abfassung.Location = new System.Drawing.Point(324, 8);
			this.gbx_Abfassung.Name = "gbx_Abfassung";
			this.gbx_Abfassung.Size = new System.Drawing.Size(280, 64);
			this.gbx_Abfassung.TabIndex = 2;
			this.gbx_Abfassung.TabStop = false;
			// 
			// lbl_TEXT_Absender
			// 
			this.lbl_TEXT_Absender.Location = new System.Drawing.Point(8, 16);
			this.lbl_TEXT_Absender.Name = "lbl_TEXT_Absender";
			this.lbl_TEXT_Absender.Size = new System.Drawing.Size(100, 16);
			this.lbl_TEXT_Absender.TabIndex = 69;
			this.lbl_TEXT_Absender.Text = "Absender";
			// 
			// lbl_Auftrag_Auftragskontext_Datum
			// 
			this.lbl_Auftrag_Auftragskontext_Datum.Location = new System.Drawing.Point(8, 40);
			this.lbl_Auftrag_Auftragskontext_Datum.Name = "lbl_Auftrag_Auftragskontext_Datum";
			this.lbl_Auftrag_Auftragskontext_Datum.Size = new System.Drawing.Size(96, 15);
			this.lbl_Auftrag_Auftragskontext_Datum.TabIndex = 2;
			this.lbl_Auftrag_Auftragskontext_Datum.Text = "Abfassungsdatum";
			// 
			// dtp_AbfassungsDatum
			// 
			this.dtp_AbfassungsDatum.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_AbfassungsDatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_AbfassungsDatum.Location = new System.Drawing.Point(152, 36);
			this.dtp_AbfassungsDatum.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp_AbfassungsDatum.Name = "dtp_AbfassungsDatum";
			this.dtp_AbfassungsDatum.Size = new System.Drawing.Size(124, 20);
			this.dtp_AbfassungsDatum.TabIndex = 2;
			// 
			// cbx_AbfassungsdatumJetzt
			// 
			this.cbx_AbfassungsdatumJetzt.Location = new System.Drawing.Point(104, 40);
			this.cbx_AbfassungsdatumJetzt.Name = "cbx_AbfassungsdatumJetzt";
			this.cbx_AbfassungsdatumJetzt.Size = new System.Drawing.Size(44, 16);
			this.cbx_AbfassungsdatumJetzt.TabIndex = 1;
			this.cbx_AbfassungsdatumJetzt.Text = "jetzt";
			// 
			// txt_Absender
			// 
			this.txt_Absender.Location = new System.Drawing.Point(120, 12);
			this.txt_Absender.Name = "txt_Absender";
			this.txt_Absender.Size = new System.Drawing.Size(152, 20);
			this.txt_Absender.TabIndex = 0;
			this.txt_Absender.Text = "";
			// 
			// gbx_Meldungstyp
			// 
			this.gbx_Meldungstyp.Controls.Add(this.rBtn_Meldungart_Meldung);
			this.gbx_Meldungstyp.Controls.Add(this.rBtn_Meldungart_Erkundungsbericht);
			this.gbx_Meldungstyp.Location = new System.Drawing.Point(4, 112);
			this.gbx_Meldungstyp.Name = "gbx_Meldungstyp";
			this.gbx_Meldungstyp.Size = new System.Drawing.Size(288, 36);
			this.gbx_Meldungstyp.TabIndex = 1;
			this.gbx_Meldungstyp.TabStop = false;
			// 
			// rBtn_Meldungart_Meldung
			// 
			this.rBtn_Meldungart_Meldung.Checked = true;
			this.rBtn_Meldungart_Meldung.Location = new System.Drawing.Point(8, 12);
			this.rBtn_Meldungart_Meldung.Name = "rBtn_Meldungart_Meldung";
			this.rBtn_Meldungart_Meldung.Size = new System.Drawing.Size(104, 20);
			this.rBtn_Meldungart_Meldung.TabIndex = 1;
			this.rBtn_Meldungart_Meldung.TabStop = true;
			this.rBtn_Meldungart_Meldung.Text = "Meldung";
			this.rBtn_Meldungart_Meldung.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.rBtn_Meldungart_Meldung.CheckedChanged += new System.EventHandler(this.rBtn_Meldungart_Meldung_CheckedChanged);
			// 
			// rBtn_Meldungart_Erkundungsbericht
			// 
			this.rBtn_Meldungart_Erkundungsbericht.Location = new System.Drawing.Point(136, 12);
			this.rBtn_Meldungart_Erkundungsbericht.Name = "rBtn_Meldungart_Erkundungsbericht";
			this.rBtn_Meldungart_Erkundungsbericht.Size = new System.Drawing.Size(124, 20);
			this.rBtn_Meldungart_Erkundungsbericht.TabIndex = 2;
			this.rBtn_Meldungart_Erkundungsbericht.Text = "Erkundungsbericht";
			this.rBtn_Meldungart_Erkundungsbericht.CheckedChanged += new System.EventHandler(this.rBtn_Meldungart_Erkundungsbericht_CheckedChanged);
			// 
			// lbl_Beabeiter
			// 
			this.lbl_Beabeiter.BackColor = System.Drawing.SystemColors.Window;
			this.lbl_Beabeiter.Location = new System.Drawing.Point(124, 36);
			this.lbl_Beabeiter.Name = "lbl_Beabeiter";
			this.lbl_Beabeiter.Size = new System.Drawing.Size(164, 15);
			this.lbl_Beabeiter.TabIndex = 23;
			// 
			// lbl_Rolle
			// 
			this.lbl_Rolle.Location = new System.Drawing.Point(124, 16);
			this.lbl_Rolle.Name = "lbl_Rolle";
			this.lbl_Rolle.Size = new System.Drawing.Size(164, 15);
			this.lbl_Rolle.TabIndex = 22;
			// 
			// lbl_Kategorie
			// 
			this.lbl_Kategorie.Location = new System.Drawing.Point(8, 92);
			this.lbl_Kategorie.Name = "lbl_Kategorie";
			this.lbl_Kategorie.Size = new System.Drawing.Size(100, 15);
			this.lbl_Kategorie.TabIndex = 18;
			this.lbl_Kategorie.Text = "Kategorie";
			// 
			// cmb_Meldungskategorie
			// 
			this.cmb_Meldungskategorie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Meldungskategorie.Location = new System.Drawing.Point(128, 84);
			this.cmb_Meldungskategorie.Name = "cmb_Meldungskategorie";
			this.cmb_Meldungskategorie.Size = new System.Drawing.Size(164, 21);
			this.cmb_Meldungskategorie.TabIndex = 0;
			// 
			// lbl_BearbeiterID
			// 
			this.lbl_BearbeiterID.BackColor = System.Drawing.SystemColors.Window;
			this.lbl_BearbeiterID.Location = new System.Drawing.Point(12, 36);
			this.lbl_BearbeiterID.Name = "lbl_BearbeiterID";
			this.lbl_BearbeiterID.Size = new System.Drawing.Size(56, 15);
			this.lbl_BearbeiterID.TabIndex = 16;
			this.lbl_BearbeiterID.Text = "Bearbeiter";
			// 
			// lbl_RolleDP
			// 
			this.lbl_RolleDP.Location = new System.Drawing.Point(12, 16);
			this.lbl_RolleDP.Name = "lbl_RolleDP";
			this.lbl_RolleDP.Size = new System.Drawing.Size(40, 15);
			this.lbl_RolleDP.TabIndex = 15;
			this.lbl_RolleDP.Text = "Rolle";
			// 
			// gbx_Erkundung
			// 
			this.gbx_Erkundung.Controls.Add(this.gbx_);
			this.gbx_Erkundung.Controls.Add(this.groupBox5);
			this.gbx_Erkundung.Controls.Add(this.groupBox4);
			this.gbx_Erkundung.Controls.Add(this.groupBox3);
			this.gbx_Erkundung.Controls.Add(this.dtp_Erk_Datum);
			this.gbx_Erkundung.Controls.Add(this.lbl_Erk_Datum);
			this.gbx_Erkundung.Controls.Add(this.lbl_Einsatzschwerpunkt);
			this.gbx_Erkundung.Controls.Add(this.cmb_Einsatzschwerpunkte);
			this.gbx_Erkundung.Controls.Add(this.lbl_Bezeichnung);
			this.gbx_Erkundung.Controls.Add(this.txt_Erkundungsobjekt);
			this.gbx_Erkundung.Controls.Add(this.txt_Erkunder);
			this.gbx_Erkundung.Controls.Add(this.lbl_Erkunder);
			this.gbx_Erkundung.Location = new System.Drawing.Point(6, 164);
			this.gbx_Erkundung.Name = "gbx_Erkundung";
			this.gbx_Erkundung.Size = new System.Drawing.Size(390, 212);
			this.gbx_Erkundung.TabIndex = 1;
			this.gbx_Erkundung.TabStop = false;
			this.gbx_Erkundung.Visible = false;
			// 
			// gbx_
			// 
			this.gbx_.Controls.Add(this.cmb_Bauart);
			this.gbx_.Controls.Add(this.lbl_Heizung);
			this.gbx_.Controls.Add(this.lbl_Haustyp);
			this.gbx_.Controls.Add(this.lbl_Bauart);
			this.gbx_.Controls.Add(this.txt_Heizung);
			this.gbx_.Controls.Add(this.txt_Haustyp);
			this.gbx_.Location = new System.Drawing.Point(8, 136);
			this.gbx_.Name = "gbx_";
			this.gbx_.Size = new System.Drawing.Size(280, 75);
			this.gbx_.TabIndex = 5;
			this.gbx_.TabStop = false;
			// 
			// cmb_Bauart
			// 
			this.cmb_Bauart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Bauart.ItemHeight = 13;
			this.cmb_Bauart.Location = new System.Drawing.Point(64, 28);
			this.cmb_Bauart.Name = "cmb_Bauart";
			this.cmb_Bauart.Size = new System.Drawing.Size(204, 21);
			this.cmb_Bauart.TabIndex = 1;
			// 
			// lbl_Heizung
			// 
			this.lbl_Heizung.Location = new System.Drawing.Point(8, 52);
			this.lbl_Heizung.Name = "lbl_Heizung";
			this.lbl_Heizung.Size = new System.Drawing.Size(54, 15);
			this.lbl_Heizung.TabIndex = 27;
			this.lbl_Heizung.Text = "Heizung";
			// 
			// lbl_Haustyp
			// 
			this.lbl_Haustyp.Location = new System.Drawing.Point(8, 8);
			this.lbl_Haustyp.Name = "lbl_Haustyp";
			this.lbl_Haustyp.Size = new System.Drawing.Size(48, 15);
			this.lbl_Haustyp.TabIndex = 24;
			this.lbl_Haustyp.Text = "Haustyp";
			// 
			// lbl_Bauart
			// 
			this.lbl_Bauart.Location = new System.Drawing.Point(8, 28);
			this.lbl_Bauart.Name = "lbl_Bauart";
			this.lbl_Bauart.Size = new System.Drawing.Size(52, 15);
			this.lbl_Bauart.TabIndex = 25;
			this.lbl_Bauart.Text = "Bauart";
			// 
			// txt_Heizung
			// 
			this.txt_Heizung.Location = new System.Drawing.Point(64, 52);
			this.txt_Heizung.MaxLength = 50;
			this.txt_Heizung.Name = "txt_Heizung";
			this.txt_Heizung.Size = new System.Drawing.Size(204, 20);
			this.txt_Heizung.TabIndex = 2;
			this.txt_Heizung.Text = "";
			// 
			// txt_Haustyp
			// 
			this.txt_Haustyp.Location = new System.Drawing.Point(64, 8);
			this.txt_Haustyp.MaxLength = 50;
			this.txt_Haustyp.Name = "txt_Haustyp";
			this.txt_Haustyp.Size = new System.Drawing.Size(204, 20);
			this.txt_Haustyp.TabIndex = 0;
			this.txt_Haustyp.Text = "";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.cbx_Strom);
			this.groupBox5.Controls.Add(this.cbx_Wasser);
			this.groupBox5.Controls.Add(this.cbx_Abwasser);
			this.groupBox5.Location = new System.Drawing.Point(292, 140);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(92, 68);
			this.groupBox5.TabIndex = 7;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Versorgung";
			// 
			// cbx_Strom
			// 
			this.cbx_Strom.Location = new System.Drawing.Point(8, 48);
			this.cbx_Strom.Name = "cbx_Strom";
			this.cbx_Strom.Size = new System.Drawing.Size(59, 16);
			this.cbx_Strom.TabIndex = 2;
			this.cbx_Strom.Text = "Strom";
			// 
			// cbx_Wasser
			// 
			this.cbx_Wasser.Location = new System.Drawing.Point(8, 16);
			this.cbx_Wasser.Name = "cbx_Wasser";
			this.cbx_Wasser.Size = new System.Drawing.Size(67, 16);
			this.cbx_Wasser.TabIndex = 0;
			this.cbx_Wasser.Text = "Wasser";
			// 
			// cbx_Abwasser
			// 
			this.cbx_Abwasser.Location = new System.Drawing.Point(8, 32);
			this.cbx_Abwasser.Name = "cbx_Abwasser";
			this.cbx_Abwasser.Size = new System.Drawing.Size(76, 16);
			this.cbx_Abwasser.TabIndex = 1;
			this.cbx_Abwasser.Text = "Abwasser";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.txt_KellerInProzent);
			this.groupBox4.Controls.Add(this.cbx_KellerIstVorhanden);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Location = new System.Drawing.Point(268, 80);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(116, 60);
			this.groupBox4.TabIndex = 6;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Keller";
			// 
			// txt_KellerInProzent
			// 
			this.txt_KellerInProzent.Enabled = false;
			this.txt_KellerInProzent.Location = new System.Drawing.Point(64, 36);
			this.txt_KellerInProzent.MaxLength = 3;
			this.txt_KellerInProzent.Name = "txt_KellerInProzent";
			this.txt_KellerInProzent.OnlyDecimal = false;
			this.txt_KellerInProzent.OnlyDigit = true;
			this.txt_KellerInProzent.OnlyIPAddr = false;
			this.txt_KellerInProzent.Size = new System.Drawing.Size(32, 20);
			this.txt_KellerInProzent.TabIndex = 63;
			this.txt_KellerInProzent.Text = "0";
			// 
			// cbx_KellerIstVorhanden
			// 
			this.cbx_KellerIstVorhanden.Location = new System.Drawing.Point(8, 16);
			this.cbx_KellerIstVorhanden.Name = "cbx_KellerIstVorhanden";
			this.cbx_KellerIstVorhanden.Size = new System.Drawing.Size(92, 20);
			this.cbx_KellerIstVorhanden.TabIndex = 0;
			this.cbx_KellerIstVorhanden.Text = "ist vorhanden";
			this.cbx_KellerIstVorhanden.CheckedChanged += new System.EventHandler(this.cbx_KellerIstVorhanden_CheckedChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "in Prozent";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.txt_Strasse);
			this.groupBox3.Controls.Add(this.txt_Ort);
			this.groupBox3.Controls.Add(this.txt_PLZ);
			this.groupBox3.Controls.Add(this.txt_HausNr);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Location = new System.Drawing.Point(8, 80);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(256, 60);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Anschrift";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 40);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(28, 16);
			this.label4.TabIndex = 8;
			this.label4.Text = "PLZ";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(192, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(20, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Nr.";
			// 
			// txt_Strasse
			// 
			this.txt_Strasse.Location = new System.Drawing.Point(44, 16);
			this.txt_Strasse.MaxLength = 50;
			this.txt_Strasse.Name = "txt_Strasse";
			this.txt_Strasse.Size = new System.Drawing.Size(140, 20);
			this.txt_Strasse.TabIndex = 0;
			this.txt_Strasse.Text = "";
			// 
			// txt_Ort
			// 
			this.txt_Ort.Location = new System.Drawing.Point(124, 36);
			this.txt_Ort.MaxLength = 50;
			this.txt_Ort.Name = "txt_Ort";
			this.txt_Ort.Size = new System.Drawing.Size(128, 20);
			this.txt_Ort.TabIndex = 3;
			this.txt_Ort.Text = "";
			// 
			// txt_PLZ
			// 
			this.txt_PLZ.Location = new System.Drawing.Point(44, 36);
			this.txt_PLZ.MaxLength = 5;
			this.txt_PLZ.Name = "txt_PLZ";
			this.txt_PLZ.Size = new System.Drawing.Size(40, 20);
			this.txt_PLZ.TabIndex = 2;
			this.txt_PLZ.Text = "";
			// 
			// txt_HausNr
			// 
			this.txt_HausNr.Location = new System.Drawing.Point(212, 16);
			this.txt_HausNr.MaxLength = 5;
			this.txt_HausNr.Name = "txt_HausNr";
			this.txt_HausNr.Size = new System.Drawing.Size(40, 20);
			this.txt_HausNr.TabIndex = 1;
			this.txt_HausNr.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(4, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 7;
			this.label3.Text = "Straße";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(88, 40);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(36, 16);
			this.label5.TabIndex = 9;
			this.label5.Text = "Stadt";
			// 
			// dtp_Erk_Datum
			// 
			this.dtp_Erk_Datum.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_Erk_Datum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Erk_Datum.Location = new System.Drawing.Point(252, 8);
			this.dtp_Erk_Datum.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp_Erk_Datum.Name = "dtp_Erk_Datum";
			this.dtp_Erk_Datum.Size = new System.Drawing.Size(128, 20);
			this.dtp_Erk_Datum.TabIndex = 1;
			// 
			// lbl_Erk_Datum
			// 
			this.lbl_Erk_Datum.Location = new System.Drawing.Point(172, 12);
			this.lbl_Erk_Datum.Name = "lbl_Erk_Datum";
			this.lbl_Erk_Datum.Size = new System.Drawing.Size(80, 16);
			this.lbl_Erk_Datum.TabIndex = 68;
			this.lbl_Erk_Datum.Text = "Erkund.-Datum";
			// 
			// lbl_Einsatzschwerpunkt
			// 
			this.lbl_Einsatzschwerpunkt.Location = new System.Drawing.Point(4, 36);
			this.lbl_Einsatzschwerpunkt.Name = "lbl_Einsatzschwerpunkt";
			this.lbl_Einsatzschwerpunkt.Size = new System.Drawing.Size(108, 16);
			this.lbl_Einsatzschwerpunkt.TabIndex = 47;
			this.lbl_Einsatzschwerpunkt.Text = "Einsatzschwerpunkt";
			// 
			// cmb_Einsatzschwerpunkte
			// 
			this.cmb_Einsatzschwerpunkte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Einsatzschwerpunkte.Location = new System.Drawing.Point(8, 52);
			this.cmb_Einsatzschwerpunkte.Name = "cmb_Einsatzschwerpunkte";
			this.cmb_Einsatzschwerpunkte.Size = new System.Drawing.Size(180, 21);
			this.cmb_Einsatzschwerpunkte.TabIndex = 2;
			// 
			// lbl_Bezeichnung
			// 
			this.lbl_Bezeichnung.Location = new System.Drawing.Point(192, 36);
			this.lbl_Bezeichnung.Name = "lbl_Bezeichnung";
			this.lbl_Bezeichnung.Size = new System.Drawing.Size(100, 15);
			this.lbl_Bezeichnung.TabIndex = 22;
			this.lbl_Bezeichnung.Text = "Erkundungsobjekt";
			// 
			// txt_Erkundungsobjekt
			// 
			this.txt_Erkundungsobjekt.Location = new System.Drawing.Point(192, 52);
			this.txt_Erkundungsobjekt.MaxLength = 50;
			this.txt_Erkundungsobjekt.Name = "txt_Erkundungsobjekt";
			this.txt_Erkundungsobjekt.Size = new System.Drawing.Size(180, 20);
			this.txt_Erkundungsobjekt.TabIndex = 3;
			this.txt_Erkundungsobjekt.Text = "";
			// 
			// txt_Erkunder
			// 
			this.txt_Erkunder.Location = new System.Drawing.Point(60, 8);
			this.txt_Erkunder.MaxLength = 50;
			this.txt_Erkunder.Name = "txt_Erkunder";
			this.txt_Erkunder.TabIndex = 0;
			this.txt_Erkunder.Text = "";
			// 
			// lbl_Erkunder
			// 
			this.lbl_Erkunder.Location = new System.Drawing.Point(4, 12);
			this.lbl_Erkunder.Name = "lbl_Erkunder";
			this.lbl_Erkunder.Size = new System.Drawing.Size(68, 15);
			this.lbl_Erkunder.TabIndex = 21;
			this.lbl_Erkunder.Text = "Erkunder";
			// 
			// txt_Meldungstext
			// 
			this.txt_Meldungstext.Location = new System.Drawing.Point(8, 188);
			this.txt_Meldungstext.Name = "txt_Meldungstext";
			this.txt_Meldungstext.Size = new System.Drawing.Size(384, 228);
			this.txt_Meldungstext.TabIndex = 2;
			this.txt_Meldungstext.Text = "";
			// 
			// lbl_Meldungstext
			// 
			this.lbl_Meldungstext.Location = new System.Drawing.Point(4, 168);
			this.lbl_Meldungstext.Name = "lbl_Meldungstext";
			this.lbl_Meldungstext.Size = new System.Drawing.Size(150, 15);
			this.lbl_Meldungstext.TabIndex = 53;
			this.lbl_Meldungstext.Text = "Meldungstext";
			// 
			// lbl_internerEmpfaenger
			// 
			this.lbl_internerEmpfaenger.Location = new System.Drawing.Point(416, 376);
			this.lbl_internerEmpfaenger.Name = "lbl_internerEmpfaenger";
			this.lbl_internerEmpfaenger.TabIndex = 62;
			this.lbl_internerEmpfaenger.Text = "interner Empänger";
			// 
			// cmb_Benutzerempfaenger
			// 
			this.cmb_Benutzerempfaenger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Benutzerempfaenger.Location = new System.Drawing.Point(416, 392);
			this.cmb_Benutzerempfaenger.Name = "cmb_Benutzerempfaenger";
			this.cmb_Benutzerempfaenger.Size = new System.Drawing.Size(196, 21);
			this.cmb_Benutzerempfaenger.TabIndex = 4;
			// 
			// ep_Eingabe
			// 
			this.ep_Eingabe.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.ep_Eingabe.ContainerControl = this;
			// 
			// usc_Meldung
			// 
			this.Controls.Add(this.cmb_Benutzerempfaenger);
			this.Controls.Add(this.lbl_internerEmpfaenger);
			this.Controls.Add(this.btn_Drucken);
			this.Controls.Add(this.btn_Zuruecksetzen);
			this.Controls.Add(this.btn_Speichern);
			this.Controls.Add(this.gbx_Meldungskontext);
			this.Controls.Add(this.gbx_Erkundung);
			this.Controls.Add(this.txt_Meldungstext);
			this.Controls.Add(this.lbl_Meldungstext);
			this.Controls.Add(this.tvw_Empfaenger);
			this.Controls.Add(this.lbl_Empfaenger);
			this.Location = new System.Drawing.Point(6, 32);
			this.Name = "usc_Meldung";
			this.Size = new System.Drawing.Size(624, 456);
			this.gbx_Meldungskontext.ResumeLayout(false);
			this.gbx_Ubermittlung.ResumeLayout(false);
			this.gbx_Abfassung.ResumeLayout(false);
			this.gbx_Meldungstyp.ResumeLayout(false);
			this.gbx_Erkundung.ResumeLayout(false);
			this.gbx_.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		#endregion

		#region SetzeXXX - setzt bestimmte Eingabefelder mit vordefinierten Werten
		private void SetzeEvents()
		{
			// Diese Events würden nach Änderung der Gui automatisch verschwinden,
			// wenn sie in InitializeComponent() stehen. Daher werden sie hierher
			// verschoben. Und Diese Methode wird im Kontruktor aufgerufen
			this.cmb_Uebermittlungsart.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_IstUebermittelt.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_AbfassungsdatumJetzt.CheckedChanged += new System.EventHandler(this.cbx_AbfassungsdatumJetzt_CheckedChanged);
			this.txt_Absender.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_Meldungskategorie.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_Strom.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_Wasser.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_Abwasser.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_KellerIstVorhanden.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Strasse.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Ort.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_PLZ.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_HausNr.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_Einsatzschwerpunkte.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Erkundungsobjekt.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Heizung.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Haustyp.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Erkunder.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_Bauart.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Meldungstext.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_Benutzerempfaenger.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
		}
		/// <summary>
		/// setzt alle möglichen Meldungskategorien
		/// </summary>
		private void SetzeMeldungsKategorie()
		{
			this.cmb_Meldungskategorie.Items.Clear();
			foreach(pELS.Tdv_MeldungsKategorie mk in 
				Enum.GetValues(typeof(pELS.Tdv_MeldungsKategorie)))
			{
				this.cmb_Meldungskategorie.Items.Add(mk);
			}
			this.cmb_Meldungskategorie.SelectedIndex = 0;
		}
		/// <summary>
		/// setzt alle möglichen Übermittlunsarten
		/// </summary>
		private void SetzeUebermittlungsart()
		{
			this.cmb_Uebermittlungsart.Items.Clear();
			foreach(pELS.Tdv_Uebermittlungsart ua in 
				Enum.GetValues(typeof(pELS.Tdv_Uebermittlungsart)))
			{
				this.cmb_Uebermittlungsart.Items.Add(ua);
			}
			this.cmb_Uebermittlungsart.SelectedIndex = 0;
		}

		/// <summary>
		/// setzt alle möglichen Bauarten (Erkundungsbericht)
		/// </summary>
		private void SetzeBauart()
		{
			this.cmb_Bauart.Items.Clear();
			foreach(pELS.Tdv_Bauart ua in 
				Enum.GetValues(typeof(pELS.Tdv_Bauart)))
			{
				this.cmb_Bauart.Items.Add(ua);
			}
			this.cmb_Bauart.SelectedIndex = 0;
		}

		/// <summary>
		/// modifiziert Standardwerte von GUI-Elementen (z.B. Sichtbarkeit)
		/// </summary>
		virtual protected void SetzeGUIElemente()
		{
			ep_Eingabe.SetIconAlignment(cmb_Meldungskategorie, ErrorIconAlignment.MiddleLeft);
			ep_Eingabe.SetIconAlignment(txt_Absender, ErrorIconAlignment.MiddleLeft);
			ep_Eingabe.SetIconAlignment(cmb_Uebermittlungsart, ErrorIconAlignment.MiddleLeft);
			ep_Eingabe.SetIconAlignment(cbx_IstUebermittelt, ErrorIconAlignment.MiddleRight);
			ep_Eingabe.SetIconAlignment(txt_Meldungstext, ErrorIconAlignment.MiddleRight);
			ep_Eingabe.SetIconAlignment(tvw_Empfaenger, ErrorIconAlignment.MiddleRight);
			ep_Eingabe.SetIconAlignment(txt_Erkunder, ErrorIconAlignment.MiddleRight);
			ep_Eingabe.SetIconAlignment(txt_Erkundungsobjekt, ErrorIconAlignment.MiddleRight);
		}

		/// <summary>
		/// setzt die Anzeige des aktuellen Benutzers
		/// </summary>
		/// <param name="pin_Benutzer"></param>
		public void SetzeBenutzer(Cdv_Benutzer pin_Benutzer)
		{
			this.lbl_Rolle.Text = pin_Benutzer.Systemrolle.ToString();
			this.lbl_Rolle.Tag = pin_Benutzer.Systemrolle;
			this.lbl_Beabeiter.Text = pin_Benutzer.Benutzername;
		}

		/// <summary>
		/// modifiziert die Reihenfolge der TabOrdnung in Abhängigkeit der
		/// ausgewählten Meldungsart
		/// </summary>
		/// <param name="pin_Modus"></param>
		private void SetzeTabOrdnung(string pin_Modus)
		{
			switch(pin_Modus)
			{
				case "Meldung": 
					//Erkundungsmeldung
					this.gbx_Erkundung.TabStop = false;
					break;
				case "Erkundungsbericht":
					this.gbx_Erkundung.TabStop = true;
					break;
			}
		}

		/// <summary>
		/// entfernt alle Häkchen von den TreeView-Einträgen der Empfänger
		/// rekursiv
		/// </summary>
		/// <param name="pin_TreeNode">Oberknoten des TreeView</param>
		protected void ZuruecksetzenTreeView(TreeNodeCollection pin_TreeNode)
		{
			// gehe durch alle enthaltenen Knoten
			if (pin_TreeNode.Count != 0)
			{
				foreach(TreeNode tn in pin_TreeNode)
				{
					// gehe durch alle in diesem enthaltenen Knoten Knoten
					if (tn.Nodes != null)
					{
						ZuruecksetzenTreeView(tn.Nodes);
					}
					tn.Checked = false;
				}
			}
		}

		/// <summary>
		/// setzt alle Eingabefelder zurück in den Ausgangszustand
		/// </summary>
		public virtual void Zuruecksetzen()
		{
			this.cmb_Meldungskategorie.SelectedIndex = 0;
			this.lbl_laufendeNummer.Text = "";
			// Absendung
			this.txt_Absender.Text = "";
			this.cbx_AbfassungsdatumJetzt.Checked = false;
			this.dtp_AbfassungsDatum.Value = DateTime.Now;
			// Übermittlung
			this.cmb_Uebermittlungsart.SelectedIndex = 0;
			this.cbx_IstUebermittelt.Checked = false;
			this.cbx_UebermittlungszeitpunktJetzt.Checked = false;
			this.dtp_Uebermittlungsdatum.Value = DateTime.Now;
			//Meldungsinhalt
			this.txt_Meldungstext.Text = "";
			//Erkundungsmeldung
			this.cmb_Einsatzschwerpunkte.SelectedIndex = -1;
			this.dtp_Erk_Datum.Value = DateTime.Now;
			this.txt_Heizung.Text = "";
			this.txt_Haustyp.Text = "";
			this.txt_Erkunder.Text = "";
			this.txt_Erkundungsobjekt.Text = "";
			this.txt_Strasse.Text = "";
			this.txt_HausNr.Text = "";
			this.txt_PLZ.Text = "";
			this.txt_Ort.Text = "";
			this.cmb_Bauart.SelectedIndex = 0;
			this.cbx_KellerIstVorhanden.Checked = false;
			this.txt_KellerInProzent.Text = "";
			this.txt_KellerInProzent.Enabled = false;
			this.cbx_Strom.Checked = false;
			this.cbx_Wasser.Checked = false;
			this.cbx_Abwasser.Checked = false;
			// Empfänger
			this.ZuruecksetzenTreeView(this.tvw_Empfaenger.Nodes);
			this.cmb_Benutzerempfaenger.SelectedIndex = -1;
			// Keine Änderungen mehr vorhanden
			this._aktuelleMitteilungsID = 0;
			this._b_FelderModifiziert = false;
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

		#endregion

		#region Eingabevalidierung
		/// <summary>
		/// überprüft alle zwingend benötigten Eingaben auf Korrektheit
		/// </summary>
		/// <returns></returns>
		virtual protected bool Eingabevalidierung()
		{
			// setze Validierungsanzeigen falls nötig
			txt_Absender_Validated(null, null);
			tvw_Empfaenger_Validated(null, null);
			cmb_Meldungskategorie_Validated(null, null);
			txt_Meldungstext_Validated(null, null);
			cmb_Uebermittlungsart_Validated(null, null);
			txt_Erkunder_Validated(null, null);
			txt_Erkundungsobjekt_Validated(null, null);

			// prüfe ob alle benötigten Felder korrekt sind
			if (rBtn_Meldungart_Meldung.Checked &&
				ValidiereAbsender() &&
				ValidiereMeldungsempfaenger() &&
				ValidiereKategorie() &&
				ValidiereText() &&
				ValidiereUebermittlungsart())
				return true;
			else if (rBtn_Meldungart_Erkundungsbericht.Checked &&
				ValidiereAbsender() &&
				ValidiereMeldungsempfaenger() &&
				ValidiereKategorie() &&
				ValidiereText() &&
				ValidiereUebermittlungsart() &&
				// zusätzliche Überprüfung notwendig
				ValidiereErkunder() &&
				ValidiereErkundungsobjekt()
				)
				return true;
			else return false;
		}

		#region Kategorie
		/// <summary>
		/// überprüfe die Meldungskategorie
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereKategorie()
		{
			return (cmb_Meldungskategorie.Text.Length > 0);
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void cmb_Meldungskategorie_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereKategorie())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(cmb_Meldungskategorie, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(cmb_Meldungskategorie, "Bitte bestimmen Sie die Meldungskategorie");
			}
		}
		#endregion
		#region Absender
		/// <summary>
		/// überprüfe die Absender
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereAbsender()
		{
			return (txt_Absender.Text.Length > 0);
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void txt_Absender_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereAbsender())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(txt_Absender, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(txt_Absender, "Bitte geben Sie einen Absender ein");
			}
		}
		#endregion
		#region Übermittlungsart
		/// <summary>
		/// überprüfe die Übermittlungsart
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereUebermittlungsart()
		{
			return (cmb_Uebermittlungsart.Text.Length > 0);
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void cmb_Uebermittlungsart_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereUebermittlungsart())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(cmb_Uebermittlungsart, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(cmb_Uebermittlungsart, "Bitte bestimmen Sie die Übermittlungsart");
			}
		}
		#endregion
		#region istUebermittelt
		/// <summary>
		/// überprüfe die Übermittlung
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereIstUebermittelt()
		{
			return (cbx_IstUebermittelt.Checked);
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void cbx_IstUebermittelt_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereIstUebermittelt())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(cbx_IstUebermittelt, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(cbx_IstUebermittelt, "Bitte geben Sie einen Text ein");
			}
		}
		#endregion

		#region Text
		/// <summary>
		/// überprüfe den Text
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereText()
		{
			return (txt_Meldungstext.Text.Length > 0);
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void txt_Meldungstext_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereText())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(txt_Meldungstext, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(txt_Meldungstext, "Bitte geben Sie einen Text ein");
			}
		}
		#endregion
		#region Erkunder
		/// <summary>
		/// überprüfe den Erkunder
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereErkunder()
		{
			return (txt_Erkunder.Text.Length > 0);
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void txt_Erkunder_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereErkunder())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(txt_Erkunder, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(txt_Erkunder, "Bitte geben Sie einen Text ein");
			}
		}
		#endregion
		#region Erkundungsobjekt
		/// <summary>
		/// überprüfe das Erkundungsobjekt
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereErkundungsobjekt()
		{
			return (txt_Erkundungsobjekt.Text.Length > 0);
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void txt_Erkundungsobjekt_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereErkundungsobjekt())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(txt_Erkundungsobjekt, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(txt_Erkundungsobjekt, "Bitte geben Sie einen Text ein");
			}
		}
		#endregion

		#region Meldungsempfänger
		/// <summary>
		/// überprüfe die Meldungsempfänger
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereMeldungsempfaenger()
		{
			ArrayList Empfaenger = HoleAlleAusgewaehltenEmpfaengerIDs(tvw_Empfaenger.Nodes);
			if (Empfaenger.Count > 0 || cmb_Benutzerempfaenger.SelectedIndex >= 0)
				return true;
			else return false;
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void tvw_Empfaenger_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereMeldungsempfaenger())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(tvw_Empfaenger, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(tvw_Empfaenger, "Bitte bestimmen Sie die Auftragsempfänger");
			}
		}
		#endregion

		#endregion

		#region LadeMeldung - Fkt. zum Laden einer Meldung
		/// <summary>
		/// lädt eine Meldung in das Formular
		/// </summary>
		/// <param name="pin_Meldung"></param>
		public void LadeMeldung(Cdv_Meldung pin_Meldung)
		{
			_aktuelleMitteilungsID = pin_Meldung.ID;
			this.lbl_laufendeNummer.Text = "E" + pin_Meldung.LaufendeNummer.ToString();
			// Abfassungsdatum
			this.dtp_AbfassungsDatum.Value = pin_Meldung.Abfassungsdatum;
			// Bearbeiter
			Cdv_Benutzer BenutzerBearbeiter = _st_Funk.ID2Benutzer(pin_Meldung.BearbeiterBenutzerID);
			if (BenutzerBearbeiter != null)
			{
				this.lbl_Rolle.Text = BenutzerBearbeiter.Systemrolle.ToString();
				this.lbl_Beabeiter.Text = BenutzerBearbeiter.Benutzername;
			}
			// Absender
			this.txt_Absender.Text = pin_Meldung.Absender;
			// Benutzerempfänger
			Cdv_Benutzer BenutzerEmpfaenger = _st_Funk.ID2Benutzer(pin_Meldung.EmpfaengerBenutzerID);
			if (BenutzerEmpfaenger != null)
				this.cmb_Benutzerempfaenger.Text = BenutzerEmpfaenger.Benutzername;
			else
				this.cmb_Benutzerempfaenger.Text = "";
			// EmpfängerKräfteMenge
			SetzeAlleAusgewaehltenEmpfaenger(
				this.tvw_Empfaenger.Nodes, pin_Meldung.EmpfaengerMengeKraftID);
			this.tvw_Empfaenger.ExpandAll();
			//Übermittlung
			this.cbx_IstUebermittelt.Checked = pin_Meldung.IstUebermittelt;
			// Übermittlungsart
			this.cmb_Uebermittlungsart.SelectedItem = pin_Meldung.Uebermittlungsart;
			// Übermittlungsdatum
			this.dtp_Uebermittlungsdatum.Value = pin_Meldung.Uebermittlungsdatum;
			// Text
			this.txt_Meldungstext.Text = pin_Meldung.Text;
			//Kategorie			
			this.cmb_Meldungskategorie.Enabled = false;
			this.cmb_Meldungskategorie.SelectedItem = pin_Meldung.Kategorie;
			// überprüfe, ob es sich um einen Erkundungsbericht handelt
			Cdv_Erkundungsergebnis ErkBericht = pin_Meldung as  Cdv_Erkundungsergebnis;
			if(ErkBericht != null)
			{
				this.txt_Erkunder.Text = ErkBericht.Erkunder;
				this.rBtn_Meldungart_Erkundungsbericht.Checked = true;
				// Einsatzschwerpunkt
				Cdv_Einsatzschwerpunkt neuerESP = _st_Funk.ID2ESP(ErkBericht.EinsatzschwerpunkID);
				if(neuerESP != null)
					//XTY cmb_Einsatzschwerpunkte.SelectedItem = neuerESP.Bezeichnung;
					cmb_Einsatzschwerpunkte.SelectedItem = neuerESP;

				else cmb_Einsatzschwerpunkte.SelectedIndex = -1;
				// Anschrift
				this.txt_HausNr.Text = ErkBericht.Erkundungsobjekt.Anschrift.Hausnummer;
				this.txt_Ort.Text = ErkBericht.Erkundungsobjekt.Anschrift.Ort;
				this.txt_PLZ.Text = ErkBericht.Erkundungsobjekt.Anschrift.PLZ;
				this.txt_Strasse.Text = ErkBericht.Erkundungsobjekt.Anschrift.Strasse;
				// Versorgung
				this.cbx_Abwasser.Checked = ErkBericht.Erkundungsobjekt.Abwasserentsorgung;
				this.cbx_Wasser.Checked = ErkBericht.Erkundungsobjekt.Wasserversorgung;
				this.cbx_Strom.Checked = ErkBericht.Erkundungsobjekt.Elektroversorgung;
				// Bauart
				cmb_Bauart.SelectedItem = ErkBericht.Erkundungsobjekt.Bauart;
				txt_Erkundungsobjekt.Text = ErkBericht.Erkundungsobjekt.Bezeichnung;
				dtp_Erk_Datum.Value = ErkBericht.Erkundungsobjekt.Erkundungsdatum;
				txt_Haustyp.Text = ErkBericht.Erkundungsobjekt.Haustyp;
				txt_Heizung.Text = ErkBericht.Erkundungsobjekt.Heizung;
				// neues Kellerobjekt
				cbx_KellerIstVorhanden.Checked = ErkBericht.Erkundungsobjekt.Keller.Vorhanden;
				if (ErkBericht.Erkundungsobjekt.Keller.Vorhanden) 
					txt_KellerInProzent.Text = ErkBericht.Erkundungsobjekt.Keller.Prozentsatz.ToString();
			}
			else
			{
				this.rBtn_Meldungart_Meldung.Checked = true;
			}
		}
	
		/// <summary>
		/// setzt Häkchen bei allen Elementen deren ID in der übergebenen
		/// ID-Menge enthalten ist
		/// </summary>
		/// <param name="pin_TreeNode"></param>
		/// <param name="pin_IDMenge"></param>
		private void SetzeAlleAusgewaehltenEmpfaenger(
			TreeNodeCollection pin_TreeNode, int[] pin_IDMenge)
		{
			if (pin_IDMenge != null)
			// gehe durch alle enthaltenen Knoten
				if (pin_TreeNode.Count != 0)
				{
					foreach(TreeNode tn in pin_TreeNode)
					{
						tn.Checked = false;
						// gehe durch alle in diesem enthaltenen Knoten Knoten
						if (tn.Nodes != null)
						{
							SetzeAlleAusgewaehltenEmpfaenger(tn.Nodes, pin_IDMenge);
						}
						// prüfe, ob ein Tag-Value existiert
						if (tn.Tag != null)
							// prüfe, ob das Element ausgewählt wurde
							foreach(int ID in pin_IDMenge)
							{
								if (((Cdv_pELSObject)tn.Tag).ID == ID)
								{
									tn.Checked = true;
								}
							}
					}
				}
		}
	
		#endregion

		#region SpeichereMeldung - Fkt. zum Speichern einer Meldung
		/// <summary>
		/// stellt eine Meldung aus den Werten der Eingabeelemente zusammen 
		/// und speichert sie
		/// </summary>
		protected void SpeichereMeldung()
		{
			// erstelle eine neue Meldung
			Cdv_Meldung neueMeldung = new Cdv_Meldung();
			neueMeldung.ID = _aktuelleMitteilungsID;
			// falls laufende Nummer bereits vergeben wurde
			if (this.lbl_laufendeNummer.Text != "")
				neueMeldung.LaufendeNummer = Convert.ToInt16(this.lbl_laufendeNummer.Text.Remove(0,1));
			// Abfassungsdatum: wähle JETZT oder das angebene Datum
			if (this.cbx_AbfassungsdatumJetzt.Checked)
				neueMeldung.Abfassungsdatum = DateTime.Now;
			else	
				neueMeldung.Abfassungsdatum = this.dtp_AbfassungsDatum.Value;
			// Absender
			neueMeldung.Absender = this.txt_Absender.Text;
			// Bearbeiter
			neueMeldung.BearbeiterBenutzerID = ((Cdv_Benutzer) this._st_Funk.HoleAktuellenBenutzer()).ID;
			// Benutzerempfänger
			if (cmb_Benutzerempfaenger.SelectedIndex != -1)
			{
				neueMeldung.EmpfaengerBenutzerID = 
					((Cdv_Benutzer) this._st_Funk._AlleBenutzer[cmb_Benutzerempfaenger.SelectedIndex]).ID;
				neueMeldung.IstInToDoListe = true;
			}
			// EmpfängerKräfteMenge
			ArrayList IDMenge = 
				this.HoleAlleAusgewaehltenEmpfaengerIDs(this.tvw_Empfaenger.Nodes);
			int[] IDMengeINT = (int[]) IDMenge.ToArray(typeof(int));
			neueMeldung.EmpfaengerMengeKraftID = IDMengeINT;
			//Übermittlung
			neueMeldung.IstUebermittelt = this.cbx_IstUebermittelt.Checked;
			// Übermittlungsart
			neueMeldung.Uebermittlungsart = (pELS.Tdv_Uebermittlungsart)this.cmb_Uebermittlungsart.SelectedItem;
			// Übermittlungsdatum: wähle JETZT oder das angebene Datum
			if (this.cbx_UebermittlungszeitpunktJetzt.Checked)
				neueMeldung.Uebermittlungsdatum = DateTime.Now;
			else
				neueMeldung.Uebermittlungsdatum = this.dtp_Uebermittlungsdatum.Value;
			// Text
			neueMeldung.Text = this.txt_Meldungstext.Text;
			//Kategorie			
			neueMeldung.Kategorie = (pELS.Tdv_MeldungsKategorie)this.cmb_Meldungskategorie.SelectedItem;

			// überprüfe, ob es sich um einen Erkundungsbericht handelt
			if (this.rBtn_Meldungart_Erkundungsbericht.Checked == true)
			{
				#region kopiere Daten für Erkundungsbericht
				int ESPID = -1;
				// EinsatzschwerpunktID ermitteln
				if (cmb_Einsatzschwerpunkte.SelectedIndex != -1)
				{
					Cdv_Einsatzschwerpunkt esp = this.cmb_Einsatzschwerpunkte.SelectedItem as Cdv_Einsatzschwerpunkt;
					if(esp != null)
						ESPID = esp.ID;
					else
						ESPID = 0;
				}
				// neues Erkundungsergebnis
				Cdv_Erkundungsergebnis neuesErkundungsergebnis = new Cdv_Erkundungsergebnis(
					neueMeldung.Text,
					neueMeldung.Absender,
					neueMeldung.Uebermittlungsart,
					neueMeldung.Kategorie,
					true,
					neueMeldung.IstInToDoListe,
					neueMeldung.BearbeiterBenutzerID,
					ESPID,
					this.txt_Erkunder.Text);
				neuesErkundungsergebnis.Abfassungsdatum = neueMeldung.Abfassungsdatum;
				neuesErkundungsergebnis.EmpfaengerBenutzerID = neueMeldung.EmpfaengerBenutzerID;
				neuesErkundungsergebnis.EmpfaengerMengeKraftID = neueMeldung.EmpfaengerMengeKraftID;
				neuesErkundungsergebnis.ID = neueMeldung.ID;
				neuesErkundungsergebnis.IstUebermittelt = neueMeldung.IstUebermittelt;
				neuesErkundungsergebnis.Uebermittlungsdatum = neueMeldung.Uebermittlungsdatum;
				#endregion
				#region erstelle Erkundungsobjekt
				// neues Erkundungsobjekt
				Cdv_Erkundungsobjekt neuesErkundungsobjekt = new Cdv_Erkundungsobjekt();
				// neue Anschrift
				Cdv_Anschrift neueAnschrift = new Cdv_Anschrift();
				neueAnschrift.Hausnummer	= this.txt_HausNr.Text;
				neueAnschrift.Ort			= this.txt_Ort.Text;
				neueAnschrift.PLZ			= this.txt_PLZ.Text;
				neueAnschrift.Strasse		= this.txt_Strasse.Text;
				neuesErkundungsobjekt.Anschrift = neueAnschrift;
				// Versorgung
				neuesErkundungsobjekt.Abwasserentsorgung = this.cbx_Abwasser.Checked;
				neuesErkundungsobjekt.Wasserversorgung = this.cbx_Wasser.Checked;
				neuesErkundungsobjekt.Elektroversorgung = this.cbx_Strom.Checked;
				// Bauart
				neuesErkundungsobjekt.Bauart = (Tdv_Bauart) cmb_Bauart.SelectedItem;
				neuesErkundungsobjekt.Bezeichnung = txt_Erkundungsobjekt.Text;
				neuesErkundungsobjekt.Erkundungsdatum = dtp_Erk_Datum.Value;
				neuesErkundungsobjekt.Haustyp = txt_Haustyp.Text;
				neuesErkundungsobjekt.Heizung = txt_Heizung.Text;
				// neues Kellerobjekt
				Cdv_Keller neuerKeller = new Cdv_Keller();
				// Abfangen des Fehlers, falls der Nutzer gar nichts eingibt.
				if ((neuerKeller.Vorhanden = cbx_KellerIstVorhanden.Checked)==true) 
				{
					if(txt_KellerInProzent.Text.CompareTo("") == 0)
						neuerKeller.Prozentsatz = 0;
					else
						neuerKeller.Prozentsatz = Convert.ToInt16(txt_KellerInProzent.Text);
				}
				else
				{
					neuerKeller.Prozentsatz = 0;
				}
				neuesErkundungsobjekt.Keller = neuerKeller;
				neuesErkundungsergebnis.Erkundungsobjekt = neuesErkundungsobjekt;
				#endregion
				this._st_Funk.SpeichereMeldung(neuesErkundungsergebnis);
			}
			else
			{
				this._st_Funk.SpeichereMeldung(neueMeldung);
			}
			_zuletztGespeicherteMitteilung = neueMeldung;
		}

			
		/// <summary>
		/// liest alle IDs der ausgewählten Meldungsempfänger ein
		/// </summary>
		/// <param name="pin_TreeNode"></param>
		/// <returns></returns>
		private ArrayList HoleAlleAusgewaehltenEmpfaengerIDs(TreeNodeCollection pin_TreeNode)
		{
			// neue leere Arraylist
			ArrayList pout_AL = new ArrayList();
			// gehe durch alle enthaltenen Knoten
			if (pin_TreeNode.Count != 0)
			{
				foreach(TreeNode tn in pin_TreeNode)
				{
					// gehe durch alle in diesem enthaltenen Knoten Knoten
					if (tn.Nodes != null)
					{
						ArrayList tmp = HoleAlleAusgewaehltenEmpfaengerIDs(tn.Nodes);
						if (tmp != null)
						{
							// füge Rückgabewerte der aktuellen ArrayList hinzu
							pout_AL.AddRange(tmp);
						}
					}
					// prüfe, ob ein Tag-Value existiert
					if (tn.Tag != null)
						// prüfe, ob das Element ausgewählt wurde
						if(tn.Checked)
						{
							// hole die PelsObject.ID und speichere diese in der ArrayList
							pout_AL.Add(((Cdv_pELSObject) tn.Tag).ID);
						}
				}
			}
			else
			{
			}
			return pout_AL;
		}

		#endregion

		/// <summary>
		/// Wenn die Systemrolle des aktuellen Benutzers verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AkualisiereAktBenutzer()
		{
			this.lbl_Beabeiter.Text = this._st_Funk.HoleAktuellenBenutzer().Benutzername;
			this.lbl_Rolle.Text = this._st_Funk.HoleAktuellenBenutzer().Systemrolle.ToString();
		}
		#region events
		#region Modifizierung
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
		#region Tastendruck
		/// <summary>
		/// löst das Speichern eines Auftrags aus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void btn_Speichern_Click(object sender, System.EventArgs e)
		{			
				if (b_FelderModifiziert)
					if (Eingabevalidierung())
					{
						SpeichereMeldung();
						Zuruecksetzen();		
					}
					else
					{
						pELS.GUI.PopUp.CPopUp.MeldenVonFalscherEingabe();
					}		
		}

		/// <summary>
		/// löst das Zurücksetzen der Eingabemaske aus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Zuruecksetzen_Click(object sender, System.EventArgs e)
		{
				ZuruecksetzenMitRueckfrage();			
		}


		/// <summary>
		/// reagiert auf das Vorhandensein eines Kellers
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cbx_KellerIstVorhanden_CheckedChanged(object sender, System.EventArgs e)
		{
			this.txt_KellerInProzent.Enabled = cbx_KellerIstVorhanden.Checked;
		}
		/// <summary>
		/// reagiert auf die Eingabe, dass die Mitteilung übermittelt wurde 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cbx_IstUebermittelt_CheckedChanged(object sender, System.EventArgs e)
		{
			if (cbx_IstUebermittelt.Checked)
			{
				//auf aktuelle Zeit zurück setzen
				dtp_Uebermittlungsdatum.Value = DateTime.Now;
				// DateTimePicker ausgrauen
				dtp_Uebermittlungsdatum.Enabled = true;
				cbx_UebermittlungszeitpunktJetzt.Enabled = true;
			}
			else
			{
				//DateTimePicker wieder aktivieren
				dtp_Uebermittlungsdatum.Enabled = false;
				cbx_UebermittlungszeitpunktJetzt.Enabled = false;
			}
		}
		/// <summary>
		/// Speichert eine Meldung und feuert dann das Event, um nach Report zu wechseln
		/// und die Meldung drucken zu können
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Drucken_Click(object sender, System.EventArgs e)
		{
			if (b_FelderModifiziert)
				if (Eingabevalidierung())
				{
					SpeichereMeldung();
					Zuruecksetzen();		
					_st_Funk.FeuereReportRequestedEvent(_zuletztGespeicherteMitteilung);
				}
				else
				{
					pELS.GUI.PopUp.CPopUp.MeldenVonFalscherEingabe();
				}		
		}
		#endregion

		#region Auswahl des Meldungstyps
		/// <summary>
		/// reagiert auf die Auswahl einer Meldung
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rBtn_Meldungart_Meldung_CheckedChanged(object sender, System.EventArgs e)
		{
			if (rBtn_Meldungart_Meldung.Checked)
			{
				// Erkundung
				gbx_Erkundung.Hide();
				this.SetzeTabOrdnung("Meldung");
				// Label Textfeld
				this.lbl_Meldungstext.Text = "Meldungstext";
				this.lbl_Meldungstext.Location = new System.Drawing.Point(4, 168);
				// Textfeld
				this.txt_Meldungstext.Location = new System.Drawing.Point(8, 188);
				this.txt_Meldungstext.Size = new System.Drawing.Size(384, 228);
			}
		}

		/// <summary>
		/// reagiert auf die Auswahl eines Erkundungsberichts
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rBtn_Meldungart_Erkundungsbericht_CheckedChanged(object sender, System.EventArgs e)
		{
			if (rBtn_Meldungart_Erkundungsbericht.Checked)
			{
				//Erkundung
				gbx_Erkundung.Show();
				this.SetzeTabOrdnung("Erkundungsbericht");
				//TODO: an neues Layout anpassen
				// Label Textfeld
				this.lbl_Meldungstext.Text = "Meldungstext";
				this.lbl_Meldungstext.Location = new System.Drawing.Point(4, 360);
				// Textfeld
				this.txt_Meldungstext.Location = new System.Drawing.Point(8, 380);
				this.txt_Meldungstext.Size = new System.Drawing.Size(384, 40);
			}
		}

		#endregion
		#region Auswahl des jetzigen Datums
		private void cbx_AbfassungsdatumJetzt_CheckedChanged(object sender, System.EventArgs e)
		{
			if (cbx_AbfassungsdatumJetzt.Checked==true)
			{
				//auf aktuelle Zeit zurück setzen
				dtp_AbfassungsDatum.Value = DateTime.Now;
				// DateTimePicker ausgrauen
				dtp_AbfassungsDatum.Enabled = false;
			}
			else
			{
				//DateTimePicker wieder aktivieren
				dtp_AbfassungsDatum.Enabled = true;
			}
		}

		private void cbx_UebermittlungszeitpunktJetzt_CheckedChanged(object sender, System.EventArgs e)
		{
			if (cbx_UebermittlungszeitpunktJetzt.Checked==true)
			{
				//auf aktuelle Zeit zurück setzen
				dtp_Uebermittlungsdatum.Value = DateTime.Now;
				// DateTimePicker ausgrauen
				dtp_Uebermittlungsdatum.Enabled = false;
			}
			else
			{
				//DateTimePicker wieder aktivieren
				dtp_Uebermittlungsdatum.Enabled = true;
			}
		}

		#endregion
		
		#region TreeView
		private void tvw_AuftragsEmpfaenger_BeforeCheck(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			// überprüfe ob der ausgewählte Knoten auf dem Root-Level liegt
			if(e.Node.Parent == null)
				// wenn ja, dann beende
				e.Cancel = true;
		}
		#endregion

		

		
		#endregion

	}
}
