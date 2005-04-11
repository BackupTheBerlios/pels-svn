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
	public class usc_Auftrag : System.Windows.Forms.UserControl
	{
		#region graphische Variablen

		protected System.Windows.Forms.DateTimePicker dtp_spaetesterErfuellungszeitpunkt;
		protected System.Windows.Forms.CheckBox cbx_nachverfolgen;
		protected System.Windows.Forms.GroupBox gbx_Auftragskontext;
		protected System.Windows.Forms.Label lbl_Auftragskontext_Uebermittlungsart;
		protected System.Windows.Forms.Label lbl_Auftragskontext_Datum;
		protected System.Windows.Forms.Button btn_AuftragZuruecksetzen;
		protected System.Windows.Forms.Button btn_AuftragSpeichern;
		protected System.Windows.Forms.Label lbl_Auftragstext;
		protected System.Windows.Forms.RichTextBox txt_Auftragstext;
		protected System.Windows.Forms.Label lbl_AuftragsEmpfaenger;
		protected System.Windows.Forms.Button btn_AuftragDrucken;
		protected System.ComponentModel.Container components = null;
		protected System.Windows.Forms.Label label1;
		protected System.Windows.Forms.DateTimePicker dtp_Uebermittlungsdatum;
		protected System.Windows.Forms.ComboBox cmb_Befehlsart;
		protected System.Windows.Forms.Label _lbl_Befehl;
		protected System.Windows.Forms.Label lbl_UMZeitpunkt;
		protected System.Windows.Forms.GroupBox gbx_Ubermittlung;
		protected System.Windows.Forms.CheckBox cbx_spaetesterErfuellungszeitpunktJetzt;
		protected System.Windows.Forms.CheckBox cbx_AbfassungsdatumJetzt;
		protected System.Windows.Forms.DateTimePicker dtp_AbfassungsDatum;
		protected System.Windows.Forms.Label lbl_BearbeiterRolle;
		protected System.Windows.Forms.Label lbl_TEXT_Bearbeiter;
		protected System.Windows.Forms.Label lbl_TEXT_Rolle;
		protected System.Windows.Forms.Label lbl_TEXT_Absender;
		protected System.Windows.Forms.TextBox txt_Absender;
		protected System.Windows.Forms.Label lbl_spaetester_Ausfuehrungszeitpunkt;
		protected System.Windows.Forms.DateTimePicker dtp_Ausfuehrungszeitpunkt;
		protected System.Windows.Forms.Label lbl_Ausfuehrungszeitpunkt;
		protected System.Windows.Forms.CheckBox cbx_UebermittlungszeitpunktJetzt;
		protected System.Windows.Forms.CheckBox cbx_AusfuehrungszeitpunktJetzt;
		protected System.Windows.Forms.ComboBox cmb_Uebermittlungsart;
		protected System.Windows.Forms.GroupBox gbx_Abfassung;
		protected System.Windows.Forms.Label lbl_BearbeiterName;
		protected System.Windows.Forms.GroupBox gbx_Ausfuehrung;
		protected System.Windows.Forms.CheckBox cbx_IstUebermittelt;
		protected System.Windows.Forms.Label lbl_TEXT_laufendeNummer;
		protected System.Windows.Forms.Label lbl_laufendeNummer;

		// müssen nach außen hin sichtbar sein, da diese 
		// ständig anpassbar sein müssen
		public System.Windows.Forms.ComboBox cmb_Benutzerempfaenger;
		public System.Windows.Forms.TreeView tvw_AuftragsEmpfaenger;
		#endregion
		#region Variablen				
		/// <summary>
		/// ermöglicht das Anzeigen von fehlerhaften Eingaben
		/// </summary>
		protected System.Windows.Forms.ErrorProvider ep_Eingabe;

		
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
		/// <summary>
		/// speichert die ID der aktuell angezeigten Mitteilung
		/// wird benötigt beim Laden und anschließendem Speichern einer Mitteilung
		/// </summary>
		private int _aktuelleMitteilungsID = 0;
		private Cdv_Mitteilung _zuletztGespeicherteMitteilung;

		#endregion


		#region Konstruktor & Destruktor
		public usc_Auftrag(Cst_Funk pin_Cst_Funk)
		{
			this._st_Funk = pin_Cst_Funk;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			SetzeEvents();

			SetzeGUIElemente();
			SetzeBefehlsart();
			SetzeUebermittlungsart();
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
			this.dtp_spaetesterErfuellungszeitpunkt = new System.Windows.Forms.DateTimePicker();
			this.cbx_spaetesterErfuellungszeitpunktJetzt = new System.Windows.Forms.CheckBox();
			this.cmb_Befehlsart = new System.Windows.Forms.ComboBox();
			this.cbx_nachverfolgen = new System.Windows.Forms.CheckBox();
			this.gbx_Auftragskontext = new System.Windows.Forms.GroupBox();
			this.lbl_laufendeNummer = new System.Windows.Forms.Label();
			this.lbl_TEXT_laufendeNummer = new System.Windows.Forms.Label();
			this.gbx_Abfassung = new System.Windows.Forms.GroupBox();
			this.lbl_TEXT_Absender = new System.Windows.Forms.Label();
			this.lbl_Auftragskontext_Datum = new System.Windows.Forms.Label();
			this.dtp_AbfassungsDatum = new System.Windows.Forms.DateTimePicker();
			this.cbx_AbfassungsdatumJetzt = new System.Windows.Forms.CheckBox();
			this.txt_Absender = new System.Windows.Forms.TextBox();
			this.gbx_Ausfuehrung = new System.Windows.Forms.GroupBox();
			this.lbl_spaetester_Ausfuehrungszeitpunkt = new System.Windows.Forms.Label();
			this.dtp_Ausfuehrungszeitpunkt = new System.Windows.Forms.DateTimePicker();
			this.lbl_Ausfuehrungszeitpunkt = new System.Windows.Forms.Label();
			this.cbx_AusfuehrungszeitpunktJetzt = new System.Windows.Forms.CheckBox();
			this._lbl_Befehl = new System.Windows.Forms.Label();
			this.lbl_BearbeiterName = new System.Windows.Forms.Label();
			this.lbl_BearbeiterRolle = new System.Windows.Forms.Label();
			this.lbl_TEXT_Bearbeiter = new System.Windows.Forms.Label();
			this.lbl_TEXT_Rolle = new System.Windows.Forms.Label();
			this.gbx_Ubermittlung = new System.Windows.Forms.GroupBox();
			this.cmb_Uebermittlungsart = new System.Windows.Forms.ComboBox();
			this.dtp_Uebermittlungsdatum = new System.Windows.Forms.DateTimePicker();
			this.lbl_UMZeitpunkt = new System.Windows.Forms.Label();
			this.cbx_IstUebermittelt = new System.Windows.Forms.CheckBox();
			this.lbl_Auftragskontext_Uebermittlungsart = new System.Windows.Forms.Label();
			this.cbx_UebermittlungszeitpunktJetzt = new System.Windows.Forms.CheckBox();
			this.btn_AuftragZuruecksetzen = new System.Windows.Forms.Button();
			this.btn_AuftragSpeichern = new System.Windows.Forms.Button();
			this.lbl_Auftragstext = new System.Windows.Forms.Label();
			this.txt_Auftragstext = new System.Windows.Forms.RichTextBox();
			this.tvw_AuftragsEmpfaenger = new System.Windows.Forms.TreeView();
			this.lbl_AuftragsEmpfaenger = new System.Windows.Forms.Label();
			this.btn_AuftragDrucken = new System.Windows.Forms.Button();
			this.cmb_Benutzerempfaenger = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.ep_Eingabe = new System.Windows.Forms.ErrorProvider();
			this.gbx_Auftragskontext.SuspendLayout();
			this.gbx_Abfassung.SuspendLayout();
			this.gbx_Ausfuehrung.SuspendLayout();
			this.gbx_Ubermittlung.SuspendLayout();
			this.SuspendLayout();
			// 
			// dtp_spaetesterErfuellungszeitpunkt
			// 
			this.dtp_spaetesterErfuellungszeitpunkt.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_spaetesterErfuellungszeitpunkt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_spaetesterErfuellungszeitpunkt.Location = new System.Drawing.Point(184, 40);
			this.dtp_spaetesterErfuellungszeitpunkt.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp_spaetesterErfuellungszeitpunkt.Name = "dtp_spaetesterErfuellungszeitpunkt";
			this.dtp_spaetesterErfuellungszeitpunkt.Size = new System.Drawing.Size(116, 20);
			this.dtp_spaetesterErfuellungszeitpunkt.TabIndex = 3;
			// 
			// cbx_spaetesterErfuellungszeitpunktJetzt
			// 
			this.cbx_spaetesterErfuellungszeitpunktJetzt.Location = new System.Drawing.Point(132, 44);
			this.cbx_spaetesterErfuellungszeitpunktJetzt.Name = "cbx_spaetesterErfuellungszeitpunktJetzt";
			this.cbx_spaetesterErfuellungszeitpunktJetzt.Size = new System.Drawing.Size(44, 16);
			this.cbx_spaetesterErfuellungszeitpunktJetzt.TabIndex = 2;
			this.cbx_spaetesterErfuellungszeitpunktJetzt.Text = "jetzt";
			this.cbx_spaetesterErfuellungszeitpunktJetzt.CheckedChanged += new System.EventHandler(this.cbx_spaetesterErfuellungszeitpunktJetzt_CheckedChanged);
			// 
			// cmb_Befehlsart
			// 
			this.cmb_Befehlsart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Befehlsart.Location = new System.Drawing.Point(128, 72);
			this.cmb_Befehlsart.Name = "cmb_Befehlsart";
			this.cmb_Befehlsart.Size = new System.Drawing.Size(188, 21);
			this.cmb_Befehlsart.TabIndex = 0;
			this.cmb_Befehlsart.Validated += new System.EventHandler(this.cmb_Befehlsart_Validated);
			this.cmb_Befehlsart.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// cbx_nachverfolgen
			// 
			this.cbx_nachverfolgen.Location = new System.Drawing.Point(8, 424);
			this.cbx_nachverfolgen.Name = "cbx_nachverfolgen";
			this.cbx_nachverfolgen.Size = new System.Drawing.Size(100, 20);
			this.cbx_nachverfolgen.TabIndex = 2;
			this.cbx_nachverfolgen.Text = "nachverfolgen";
			// 
			// gbx_Auftragskontext
			// 
			this.gbx_Auftragskontext.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_Auftragskontext.Controls.Add(this.lbl_laufendeNummer);
			this.gbx_Auftragskontext.Controls.Add(this.lbl_TEXT_laufendeNummer);
			this.gbx_Auftragskontext.Controls.Add(this.gbx_Abfassung);
			this.gbx_Auftragskontext.Controls.Add(this.gbx_Ausfuehrung);
			this.gbx_Auftragskontext.Controls.Add(this._lbl_Befehl);
			this.gbx_Auftragskontext.Controls.Add(this.lbl_BearbeiterName);
			this.gbx_Auftragskontext.Controls.Add(this.lbl_BearbeiterRolle);
			this.gbx_Auftragskontext.Controls.Add(this.lbl_TEXT_Bearbeiter);
			this.gbx_Auftragskontext.Controls.Add(this.lbl_TEXT_Rolle);
			this.gbx_Auftragskontext.Controls.Add(this.cmb_Befehlsart);
			this.gbx_Auftragskontext.Controls.Add(this.gbx_Ubermittlung);
			this.gbx_Auftragskontext.Location = new System.Drawing.Point(6, 0);
			this.gbx_Auftragskontext.Name = "gbx_Auftragskontext";
			this.gbx_Auftragskontext.Size = new System.Drawing.Size(615, 164);
			this.gbx_Auftragskontext.TabIndex = 3;
			this.gbx_Auftragskontext.TabStop = false;
			this.gbx_Auftragskontext.Text = "Auftragskontext";
			// 
			// lbl_laufendeNummer
			// 
			this.lbl_laufendeNummer.Location = new System.Drawing.Point(128, 52);
			this.lbl_laufendeNummer.Name = "lbl_laufendeNummer";
			this.lbl_laufendeNummer.Size = new System.Drawing.Size(100, 16);
			this.lbl_laufendeNummer.TabIndex = 78;
			// 
			// lbl_TEXT_laufendeNummer
			// 
			this.lbl_TEXT_laufendeNummer.Location = new System.Drawing.Point(12, 52);
			this.lbl_TEXT_laufendeNummer.Name = "lbl_TEXT_laufendeNummer";
			this.lbl_TEXT_laufendeNummer.Size = new System.Drawing.Size(100, 20);
			this.lbl_TEXT_laufendeNummer.TabIndex = 77;
			this.lbl_TEXT_laufendeNummer.Text = "lfd. Nummer";
			// 
			// gbx_Abfassung
			// 
			this.gbx_Abfassung.Controls.Add(this.lbl_TEXT_Absender);
			this.gbx_Abfassung.Controls.Add(this.lbl_Auftragskontext_Datum);
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
			// lbl_Auftragskontext_Datum
			// 
			this.lbl_Auftragskontext_Datum.Location = new System.Drawing.Point(8, 40);
			this.lbl_Auftragskontext_Datum.Name = "lbl_Auftragskontext_Datum";
			this.lbl_Auftragskontext_Datum.Size = new System.Drawing.Size(96, 15);
			this.lbl_Auftragskontext_Datum.TabIndex = 2;
			this.lbl_Auftragskontext_Datum.Text = "Abfassungsdatum";
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
			this.cbx_AbfassungsdatumJetzt.CheckedChanged += new System.EventHandler(this.cbx_AbfassungsdatumJetzt_CheckedChanged);
			// 
			// txt_Absender
			// 
			this.txt_Absender.Location = new System.Drawing.Point(120, 12);
			this.txt_Absender.Name = "txt_Absender";
			this.txt_Absender.Size = new System.Drawing.Size(152, 20);
			this.txt_Absender.TabIndex = 0;
			this.txt_Absender.Text = "";
			this.txt_Absender.Validated += new System.EventHandler(this.txt_Absender_Validated);
			// 
			// gbx_Ausfuehrung
			// 
			this.gbx_Ausfuehrung.Controls.Add(this.cbx_spaetesterErfuellungszeitpunktJetzt);
			this.gbx_Ausfuehrung.Controls.Add(this.lbl_spaetester_Ausfuehrungszeitpunkt);
			this.gbx_Ausfuehrung.Controls.Add(this.dtp_Ausfuehrungszeitpunkt);
			this.gbx_Ausfuehrung.Controls.Add(this.lbl_Ausfuehrungszeitpunkt);
			this.gbx_Ausfuehrung.Controls.Add(this.cbx_AusfuehrungszeitpunktJetzt);
			this.gbx_Ausfuehrung.Controls.Add(this.dtp_spaetesterErfuellungszeitpunkt);
			this.gbx_Ausfuehrung.Location = new System.Drawing.Point(8, 92);
			this.gbx_Ausfuehrung.Name = "gbx_Ausfuehrung";
			this.gbx_Ausfuehrung.Size = new System.Drawing.Size(308, 64);
			this.gbx_Ausfuehrung.TabIndex = 1;
			this.gbx_Ausfuehrung.TabStop = false;
			// 
			// lbl_spaetester_Ausfuehrungszeitpunkt
			// 
			this.lbl_spaetester_Ausfuehrungszeitpunkt.Location = new System.Drawing.Point(8, 44);
			this.lbl_spaetester_Ausfuehrungszeitpunkt.Name = "lbl_spaetester_Ausfuehrungszeitpunkt";
			this.lbl_spaetester_Ausfuehrungszeitpunkt.Size = new System.Drawing.Size(132, 16);
			this.lbl_spaetester_Ausfuehrungszeitpunkt.TabIndex = 72;
			this.lbl_spaetester_Ausfuehrungszeitpunkt.Text = "spätester Ausf.-Zeitpunkt";
			// 
			// dtp_Ausfuehrungszeitpunkt
			// 
			this.dtp_Ausfuehrungszeitpunkt.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_Ausfuehrungszeitpunkt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Ausfuehrungszeitpunkt.Location = new System.Drawing.Point(184, 16);
			this.dtp_Ausfuehrungszeitpunkt.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp_Ausfuehrungszeitpunkt.Name = "dtp_Ausfuehrungszeitpunkt";
			this.dtp_Ausfuehrungszeitpunkt.Size = new System.Drawing.Size(116, 20);
			this.dtp_Ausfuehrungszeitpunkt.TabIndex = 1;
			// 
			// lbl_Ausfuehrungszeitpunkt
			// 
			this.lbl_Ausfuehrungszeitpunkt.BackColor = System.Drawing.SystemColors.Window;
			this.lbl_Ausfuehrungszeitpunkt.Location = new System.Drawing.Point(8, 20);
			this.lbl_Ausfuehrungszeitpunkt.Name = "lbl_Ausfuehrungszeitpunkt";
			this.lbl_Ausfuehrungszeitpunkt.Size = new System.Drawing.Size(116, 16);
			this.lbl_Ausfuehrungszeitpunkt.TabIndex = 71;
			this.lbl_Ausfuehrungszeitpunkt.Text = "Ausführungszeitpunkt";
			// 
			// cbx_AusfuehrungszeitpunktJetzt
			// 
			this.cbx_AusfuehrungszeitpunktJetzt.Location = new System.Drawing.Point(132, 20);
			this.cbx_AusfuehrungszeitpunktJetzt.Name = "cbx_AusfuehrungszeitpunktJetzt";
			this.cbx_AusfuehrungszeitpunktJetzt.Size = new System.Drawing.Size(44, 16);
			this.cbx_AusfuehrungszeitpunktJetzt.TabIndex = 0;
			this.cbx_AusfuehrungszeitpunktJetzt.Text = "jetzt";
			this.cbx_AusfuehrungszeitpunktJetzt.CheckedChanged += new System.EventHandler(this.cbx_Ausfuehrungszeitpunkt_CheckedChanged);
			// 
			// _lbl_Befehl
			// 
			this._lbl_Befehl.Location = new System.Drawing.Point(12, 76);
			this._lbl_Befehl.Name = "_lbl_Befehl";
			this._lbl_Befehl.Size = new System.Drawing.Size(68, 16);
			this._lbl_Befehl.TabIndex = 29;
			this._lbl_Befehl.Text = "Befehl";
			// 
			// lbl_BearbeiterName
			// 
			this.lbl_BearbeiterName.Location = new System.Drawing.Point(128, 32);
			this.lbl_BearbeiterName.Name = "lbl_BearbeiterName";
			this.lbl_BearbeiterName.Size = new System.Drawing.Size(168, 15);
			this.lbl_BearbeiterName.TabIndex = 24;
			// 
			// lbl_BearbeiterRolle
			// 
			this.lbl_BearbeiterRolle.Location = new System.Drawing.Point(128, 16);
			this.lbl_BearbeiterRolle.Name = "lbl_BearbeiterRolle";
			this.lbl_BearbeiterRolle.Size = new System.Drawing.Size(168, 15);
			this.lbl_BearbeiterRolle.TabIndex = 23;
			// 
			// lbl_TEXT_Bearbeiter
			// 
			this.lbl_TEXT_Bearbeiter.Location = new System.Drawing.Point(12, 32);
			this.lbl_TEXT_Bearbeiter.Name = "lbl_TEXT_Bearbeiter";
			this.lbl_TEXT_Bearbeiter.Size = new System.Drawing.Size(56, 15);
			this.lbl_TEXT_Bearbeiter.TabIndex = 16;
			this.lbl_TEXT_Bearbeiter.Text = "Bearbeiter";
			// 
			// lbl_TEXT_Rolle
			// 
			this.lbl_TEXT_Rolle.Location = new System.Drawing.Point(12, 16);
			this.lbl_TEXT_Rolle.Name = "lbl_TEXT_Rolle";
			this.lbl_TEXT_Rolle.Size = new System.Drawing.Size(64, 15);
			this.lbl_TEXT_Rolle.TabIndex = 15;
			this.lbl_TEXT_Rolle.Text = "Rolle";
			// 
			// gbx_Ubermittlung
			// 
			this.gbx_Ubermittlung.Controls.Add(this.cmb_Uebermittlungsart);
			this.gbx_Ubermittlung.Controls.Add(this.dtp_Uebermittlungsdatum);
			this.gbx_Ubermittlung.Controls.Add(this.lbl_UMZeitpunkt);
			this.gbx_Ubermittlung.Controls.Add(this.cbx_IstUebermittelt);
			this.gbx_Ubermittlung.Controls.Add(this.lbl_Auftragskontext_Uebermittlungsart);
			this.gbx_Ubermittlung.Controls.Add(this.cbx_UebermittlungszeitpunktJetzt);
			this.gbx_Ubermittlung.Location = new System.Drawing.Point(324, 72);
			this.gbx_Ubermittlung.Name = "gbx_Ubermittlung";
			this.gbx_Ubermittlung.Size = new System.Drawing.Size(280, 84);
			this.gbx_Ubermittlung.TabIndex = 67;
			this.gbx_Ubermittlung.TabStop = false;
			// 
			// cmb_Uebermittlungsart
			// 
			this.cmb_Uebermittlungsart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Uebermittlungsart.Location = new System.Drawing.Point(124, 12);
			this.cmb_Uebermittlungsart.Name = "cmb_Uebermittlungsart";
			this.cmb_Uebermittlungsart.Size = new System.Drawing.Size(148, 21);
			this.cmb_Uebermittlungsart.TabIndex = 0;
			this.cmb_Uebermittlungsart.Validated += new System.EventHandler(this.cmb_Uebermittlungsart_Validated);
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
			// lbl_Auftragskontext_Uebermittlungsart
			// 
			this.lbl_Auftragskontext_Uebermittlungsart.Location = new System.Drawing.Point(8, 16);
			this.lbl_Auftragskontext_Uebermittlungsart.Name = "lbl_Auftragskontext_Uebermittlungsart";
			this.lbl_Auftragskontext_Uebermittlungsart.Size = new System.Drawing.Size(100, 15);
			this.lbl_Auftragskontext_Uebermittlungsart.TabIndex = 3;
			this.lbl_Auftragskontext_Uebermittlungsart.Text = "Übermittlungsart";
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
			// btn_AuftragZuruecksetzen
			// 
			this.btn_AuftragZuruecksetzen.Location = new System.Drawing.Point(448, 424);
			this.btn_AuftragZuruecksetzen.Name = "btn_AuftragZuruecksetzen";
			this.btn_AuftragZuruecksetzen.Size = new System.Drawing.Size(81, 25);
			this.btn_AuftragZuruecksetzen.TabIndex = 6;
			this.btn_AuftragZuruecksetzen.Text = "&Zurücksetzen";
			this.btn_AuftragZuruecksetzen.Click += new System.EventHandler(this.btn_AuftragZuruecksetzen_Click);
			// 
			// btn_AuftragSpeichern
			// 
			this.btn_AuftragSpeichern.Location = new System.Drawing.Point(536, 424);
			this.btn_AuftragSpeichern.Name = "btn_AuftragSpeichern";
			this.btn_AuftragSpeichern.Size = new System.Drawing.Size(80, 25);
			this.btn_AuftragSpeichern.TabIndex = 5;
			this.btn_AuftragSpeichern.Text = "&Speichern";
			this.btn_AuftragSpeichern.Click += new System.EventHandler(this.btn_AuftragSpeichern_Click);
			// 
			// lbl_Auftragstext
			// 
			this.lbl_Auftragstext.Location = new System.Drawing.Point(8, 176);
			this.lbl_Auftragstext.Name = "lbl_Auftragstext";
			this.lbl_Auftragstext.Size = new System.Drawing.Size(65, 15);
			this.lbl_Auftragstext.TabIndex = 17;
			this.lbl_Auftragstext.Text = "Auftragstext";
			// 
			// txt_Auftragstext
			// 
			this.txt_Auftragstext.Location = new System.Drawing.Point(4, 196);
			this.txt_Auftragstext.Name = "txt_Auftragstext";
			this.txt_Auftragstext.Size = new System.Drawing.Size(392, 216);
			this.txt_Auftragstext.TabIndex = 1;
			this.txt_Auftragstext.Text = "";
			this.txt_Auftragstext.Validated += new System.EventHandler(this.txt_Auftragstext_Validated);
			// 
			// tvw_AuftragsEmpfaenger
			// 
			this.tvw_AuftragsEmpfaenger.CheckBoxes = true;
			this.tvw_AuftragsEmpfaenger.ImageIndex = -1;
			this.tvw_AuftragsEmpfaenger.Location = new System.Drawing.Point(416, 196);
			this.tvw_AuftragsEmpfaenger.Name = "tvw_AuftragsEmpfaenger";
			this.tvw_AuftragsEmpfaenger.SelectedImageIndex = -1;
			this.tvw_AuftragsEmpfaenger.Size = new System.Drawing.Size(192, 172);
			this.tvw_AuftragsEmpfaenger.TabIndex = 3;
			this.tvw_AuftragsEmpfaenger.Validated += new System.EventHandler(this.tvw_AuftragsEmpfaenger_Validated);
			this.tvw_AuftragsEmpfaenger.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvw_AuftragsEmpfaenger_BeforeCheck);
			// 
			// lbl_AuftragsEmpfaenger
			// 
			this.lbl_AuftragsEmpfaenger.Location = new System.Drawing.Point(420, 176);
			this.lbl_AuftragsEmpfaenger.Name = "lbl_AuftragsEmpfaenger";
			this.lbl_AuftragsEmpfaenger.Size = new System.Drawing.Size(128, 16);
			this.lbl_AuftragsEmpfaenger.TabIndex = 15;
			this.lbl_AuftragsEmpfaenger.Text = "Auftragsempfänger";
			// 
			// btn_AuftragDrucken
			// 
			this.btn_AuftragDrucken.Location = new System.Drawing.Point(304, 424);
			this.btn_AuftragDrucken.Name = "btn_AuftragDrucken";
			this.btn_AuftragDrucken.Size = new System.Drawing.Size(136, 25);
			this.btn_AuftragDrucken.TabIndex = 7;
			this.btn_AuftragDrucken.Text = "Speichern && &Drucken";
			this.btn_AuftragDrucken.Click += new System.EventHandler(this.btn_AuftragDrucken_Click);
			// 
			// cmb_Benutzerempfaenger
			// 
			this.cmb_Benutzerempfaenger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Benutzerempfaenger.Location = new System.Drawing.Point(416, 392);
			this.cmb_Benutzerempfaenger.Name = "cmb_Benutzerempfaenger";
			this.cmb_Benutzerempfaenger.Size = new System.Drawing.Size(200, 21);
			this.cmb_Benutzerempfaenger.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(416, 376);
			this.label1.Name = "label1";
			this.label1.TabIndex = 64;
			this.label1.Text = "interner Empänger";
			// 
			// ep_Eingabe
			// 
			this.ep_Eingabe.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.ep_Eingabe.ContainerControl = this;
			// 
			// usc_Auftrag
			// 
			this.Controls.Add(this.cmb_Benutzerempfaenger);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btn_AuftragDrucken);
			this.Controls.Add(this.gbx_Auftragskontext);
			this.Controls.Add(this.btn_AuftragZuruecksetzen);
			this.Controls.Add(this.btn_AuftragSpeichern);
			this.Controls.Add(this.lbl_Auftragstext);
			this.Controls.Add(this.txt_Auftragstext);
			this.Controls.Add(this.tvw_AuftragsEmpfaenger);
			this.Controls.Add(this.lbl_AuftragsEmpfaenger);
			this.Controls.Add(this.cbx_nachverfolgen);
			this.Location = new System.Drawing.Point(6, 21);
			this.Name = "usc_Auftrag";
			this.Size = new System.Drawing.Size(624, 456);
			this.gbx_Auftragskontext.ResumeLayout(false);
			this.gbx_Abfassung.ResumeLayout(false);
			this.gbx_Ausfuehrung.ResumeLayout(false);
			this.gbx_Ubermittlung.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		#endregion

		#region SetzeXXX
		private void SetzeEvents()
		{
			// Diese Events würden nach Änderung der Gui automatisch verschwinden,
			// wenn sie in InitializeComponent() stehen. Daher werden sie hierher
			// verschoben. Und Diese Methode wird im Kontruktor aufgerufen
			this.cmb_Befehlsart.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_nachverfolgen.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Absender.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_Uebermittlungsart.Click += new System.EventHandler(this.FelderModifiziert);
			this.cbx_IstUebermittelt.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Auftragstext.Enter += new System.EventHandler(this.FelderModifiziert);
			this.cmb_Benutzerempfaenger.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
		}
		/// <summary>
		/// setzt alle möglichen Befehlsarten
		/// </summary>
		private void SetzeBefehlsart()
		{
			this.cmb_Befehlsart.Items.Clear();
			this.cmb_Befehlsart.Items.Add("<<kein Befehl>>");
			this.cmb_Befehlsart.Text = "<<kein Befehl>>";
			foreach(pELS.Tdv_BefehlArt ua in 
				Enum.GetValues(typeof(pELS.Tdv_BefehlArt)))
			{
				this.cmb_Befehlsart.Items.Add("Erkundungsbefehl (" + ua + ")");
			}
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
		/// modifiziert Standardwerte von GUI-Elementen (z.B. Sichtbarkeit)
		/// </summary>
		virtual protected void SetzeGUIElemente()
		{
			ep_Eingabe.SetIconAlignment(cmb_Befehlsart, ErrorIconAlignment.MiddleLeft);
			ep_Eingabe.SetIconAlignment(txt_Absender, ErrorIconAlignment.MiddleLeft);
			ep_Eingabe.SetIconAlignment(cmb_Uebermittlungsart, ErrorIconAlignment.MiddleLeft);
			ep_Eingabe.SetIconAlignment(cbx_IstUebermittelt, ErrorIconAlignment.MiddleRight);
			ep_Eingabe.SetIconAlignment(txt_Auftragstext, ErrorIconAlignment.MiddleRight);
			ep_Eingabe.SetIconAlignment(tvw_AuftragsEmpfaenger, ErrorIconAlignment.MiddleRight);
		}
		/// <summary>
		/// setzt die Anzeige des aktuellen Benutzers
		/// </summary>
		/// <param name="pin_Benutzer"></param>
		public void SetzeBenutzer(Cdv_Benutzer pin_Benutzer)
		{
			this.lbl_BearbeiterRolle.Text = pin_Benutzer.Systemrolle.ToString();
			this.lbl_BearbeiterRolle.Tag = pin_Benutzer.Systemrolle;
			this.lbl_BearbeiterName.Text = pin_Benutzer.Benutzername;
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
		virtual public void Zuruecksetzen()
		{
			// Übermittlung
			this.cmb_Uebermittlungsart.SelectedIndex = 0;
			this.cbx_IstUebermittelt.Checked = false;
			this.cbx_UebermittlungszeitpunktJetzt.Checked = false;
			this.dtp_Uebermittlungsdatum.Value = DateTime.Now;
			// Absendung
			this.txt_Absender.Text = "";
			this.cbx_AbfassungsdatumJetzt.Checked = false;
			this.dtp_AbfassungsDatum.Value = DateTime.Now;
			// Empfänger
			this.ZuruecksetzenTreeView(this.tvw_AuftragsEmpfaenger.Nodes);
			this.cmb_Benutzerempfaenger.SelectedIndex = -1;
			// Auftragssinhalt
			this.txt_Auftragstext.Text = "";
			// Auftragskontext
			this.cmb_Befehlsart.SelectedIndex = 0;
			this.cbx_AusfuehrungszeitpunktJetzt.Checked = false;
			this.dtp_Ausfuehrungszeitpunkt.Value = DateTime.Now;
			this.cbx_spaetesterErfuellungszeitpunktJetzt.Checked = false;
			this.dtp_spaetesterErfuellungszeitpunkt.Value = DateTime.Now;
			// Nachverfolgen
			this.cbx_nachverfolgen.Checked = false;
			// Keine Änderungen mehr vorhanden
			_aktuelleMitteilungsID = 0;
			_b_FelderModifiziert = false;
			// Errorprovider deaktivieren
			// TODO:
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
			tvw_AuftragsEmpfaenger_Validated(null, null);
			cmb_Befehlsart_Validated(null, null);
			txt_Auftragstext_Validated(null, null);
			cmb_Uebermittlungsart_Validated(null, null);
			// prüfe ob alle benötigten Felder korrekt sind
			if (ValidiereAbsender() &&
				ValidiereAuftragsempfaenger() &&
				ValidiereBefehlsart() &&
				ValidiereText() &&
				ValidiereUebermittlungsart())
				return true;
			else return false;
		}

		#region Befehlsart
		/// <summary>
		/// überprüfe die Befehlsart
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereBefehlsart()
		{
			return (cmb_Befehlsart.Text.Length > 0);
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void cmb_Befehlsart_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereBefehlsart())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(cmb_Befehlsart, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(cmb_Befehlsart, "Bitte bestimmen Sie die Befehlsart");
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

		#region Text
		/// <summary>
		/// überprüfe den Text
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereText()
		{
			return (txt_Auftragstext.Text.Length > 0);
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void txt_Auftragstext_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereText())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(txt_Auftragstext, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(txt_Auftragstext, "Bitte geben Sie einen Text ein");
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
		#region Auftragsempfänger
		/// <summary>
		/// überprüfe die Auftragsempfänger
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereAuftragsempfaenger()
		{
			ArrayList Empfaenger = HoleAlleAusgewaehltenEmpfaengerIDs(tvw_AuftragsEmpfaenger.Nodes);
			if (Empfaenger.Count > 0 || cmb_Benutzerempfaenger.SelectedIndex >= 0)
                return true;
			else return false;
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void tvw_AuftragsEmpfaenger_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereAuftragsempfaenger())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(tvw_AuftragsEmpfaenger, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(tvw_AuftragsEmpfaenger, "Bitte bestimmen Sie die Auftragsempfänger");
			}
		}
		#endregion

		#endregion

		#region LadeAuftrag - Fkr. zum Laden eines Auftrags
		/// <summary>
		/// lädt einen Auftrag in das Formular
		/// </summary>
		/// <param name="pin_Meldung"></param>
		public void LadeAuftrag(Cdv_Auftrag pin_Auftrag)
		{
			_aktuelleMitteilungsID = pin_Auftrag.ID;
			this.lbl_laufendeNummer.Text = "A" + pin_Auftrag.LaufendeNummer.ToString();
			this.dtp_AbfassungsDatum.Value = pin_Auftrag.Abfassungsdatum;
			// Bearbeiter
			Cdv_Benutzer BenutzerBearbeiter = _st_Funk.ID2Benutzer(pin_Auftrag.BearbeiterBenutzerID);
			if (BenutzerBearbeiter != null)
			{
				this.lbl_BearbeiterRolle.Text = BenutzerBearbeiter.Systemrolle.ToString();
				this.lbl_BearbeiterName.Text = BenutzerBearbeiter.Benutzername;
			}
			// Absender
			this.txt_Absender.Text = pin_Auftrag.Absender;
			// Benutzerempfänger
			Cdv_Benutzer BenutzerEmpfaenger = _st_Funk.ID2Benutzer(pin_Auftrag.EmpfaengerBenutzerID);
			if (BenutzerEmpfaenger != null)
				this.cmb_Benutzerempfaenger.Text = BenutzerEmpfaenger.Benutzername;
			else
				this.cmb_Benutzerempfaenger.Text = "";
			// EmpfängerKräfteMenge
			SetzeAlleAusgewaehltenEmpfaenger(
				this.tvw_AuftragsEmpfaenger.Nodes, pin_Auftrag.EmpfaengerMengeKraftID);
			this.tvw_AuftragsEmpfaenger.ExpandAll();
			this.dtp_spaetesterErfuellungszeitpunkt.Value = pin_Auftrag.Ausfuehrungszeitpunkt;
			if (pin_Auftrag.IstBefehl)
			{
				Cdv_Erkundungsbefehl tmpEB = pin_Auftrag as Cdv_Erkundungsbefehl;
				this.cmb_Befehlsart.SelectedItem = "Erkundungsbefehl (" + tmpEB.BefehlsArt + ")";
			}
			else 
				this.cmb_Befehlsart.SelectedIndex = 0;
			//Übermittlung
			this.cbx_IstUebermittelt.Checked = pin_Auftrag.IstUebermittelt;
			// Text
			this.txt_Auftragstext.Text = pin_Auftrag.Text;
			// Übermittlungsart
			this.cmb_Uebermittlungsart.SelectedItem = pin_Auftrag.Uebermittlungsart;
			// Übermittlungsdatum
			this.dtp_Uebermittlungsdatum.Value = pin_Auftrag.Uebermittlungsdatum;
			this.cbx_nachverfolgen.Checked = pin_Auftrag.WirdNachverfolgt;

			this.dtp_spaetesterErfuellungszeitpunkt.Value = 
				pin_Auftrag.SpaetesterErfuellungszeitpunkt;
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

		#region SpeichereAuftrag - Fkt. zum Speichern eines Auftrags
		/// <summary>
		/// stellt einen Auftrag aus den Werten der Eingabeelemente zusammen 
		/// und speichert ihn
		/// </summary>
		protected bool SpeichereAuftrag()
		{
			// erstelle einen neuen Auftrag
			Cdv_Auftrag neuerAuftrag = new Cdv_Auftrag();
			neuerAuftrag.ID = _aktuelleMitteilungsID;
			// falls laufende Nummer bereits vergeben wurde
			if (this.lbl_laufendeNummer.Text != "")
				neuerAuftrag.LaufendeNummer = Convert.ToInt16(this.lbl_laufendeNummer.Text.Remove(0,1));
			// Abfassungsdatum: wähle JETZT oder das angebene Datum
			if (this.cbx_AbfassungsdatumJetzt.Checked)
				neuerAuftrag.Abfassungsdatum = DateTime.Now;
			else
				neuerAuftrag.Abfassungsdatum = this.dtp_AbfassungsDatum.Value;
			// Absender
			neuerAuftrag.Absender = this.txt_Absender.Text;
			// Ausführungszeitpunkt: wähle JETZT oder das angebene Datum
			if (this.cbx_AusfuehrungszeitpunktJetzt.Checked)
				neuerAuftrag.Ausfuehrungszeitpunkt = DateTime.Now;
			else
				neuerAuftrag.Ausfuehrungszeitpunkt = this.dtp_Ausfuehrungszeitpunkt.Value;
			// spätester Ausführungszeitpunkt: wähle JETZT oder das angebene Datum
			if (this.cbx_spaetesterErfuellungszeitpunktJetzt.Checked)
				neuerAuftrag.SpaetesterErfuellungszeitpunkt = DateTime.Now;
			else
				neuerAuftrag.SpaetesterErfuellungszeitpunkt= this.dtp_spaetesterErfuellungszeitpunkt.Value;
			// Bearbeiter
			neuerAuftrag.BearbeiterBenutzerID = ((Cdv_Benutzer) this._st_Funk.HoleAktuellenBenutzer()).ID;
			// Benutzerempfänger
			if (cmb_Benutzerempfaenger.SelectedIndex != -1)
			{
				neuerAuftrag.EmpfaengerBenutzerID = 
					((Cdv_Benutzer) this._st_Funk._AlleBenutzer[cmb_Benutzerempfaenger.SelectedIndex]).ID;
				neuerAuftrag.IstInToDoListe = true;
			}
			// EmpfängerKräfteMenge
			ArrayList IDMenge = 
				this.HoleAlleAusgewaehltenEmpfaengerIDs(this.tvw_AuftragsEmpfaenger.Nodes);
			int[] IDMengeINT = (int[]) IDMenge.ToArray(typeof(int));
			neuerAuftrag.EmpfaengerMengeKraftID = IDMengeINT;
			//Übermittlung
			neuerAuftrag.IstUebermittelt = this.cbx_IstUebermittelt.Checked;
			// Übermittlungsart
			neuerAuftrag.Uebermittlungsart = 
				(pELS.Tdv_Uebermittlungsart) this.cmb_Uebermittlungsart.SelectedItem;
			// Übermittlungsdatum: wähle JETZT oder das angebene Datum
			if (this.cbx_UebermittlungszeitpunktJetzt.Checked)
				neuerAuftrag.Uebermittlungsdatum = DateTime.Now;
			else
				neuerAuftrag.Uebermittlungsdatum = this.dtp_Uebermittlungsdatum.Value;
			// Text
			neuerAuftrag.Text = this.txt_Auftragstext.Text;
			// Nachverfolgen
			neuerAuftrag.WirdNachverfolgt = this.cbx_nachverfolgen.Checked;
			// ermittle, ob es sich um einen Befehl handelt
		
			if ((this.cmb_Befehlsart.SelectedIndex != 0) && (this.cmb_Befehlsart.SelectedIndex != -1))
			{
				neuerAuftrag.IstBefehl = true;
				// ermittle, ob es sich um einen Erkundungsbefehl handelt
				Tdv_BefehlArt aktuelleEBArt = new Tdv_BefehlArt();
				aktuelleEBArt = PruefeErkundungsbefehl();
					
				#region kopiere Daten aus Auftrag nach erkundungsbefehl
				Cdv_Erkundungsbefehl neuerErkundungsbefehl = new Cdv_Erkundungsbefehl(
					neuerAuftrag.Text,
					neuerAuftrag.Abfassungsdatum,
					neuerAuftrag.Absender,
					neuerAuftrag.Uebermittlungsart,
					neuerAuftrag.WirdNachverfolgt,
					aktuelleEBArt,
					neuerAuftrag.IstInToDoListe,
					neuerAuftrag.BearbeiterBenutzerID);
				neuerErkundungsbefehl.Ausfuehrungszeitpunkt = neuerAuftrag.Ausfuehrungszeitpunkt;
				neuerErkundungsbefehl.EmpfaengerBenutzerID = neuerAuftrag.EmpfaengerBenutzerID;
				neuerErkundungsbefehl.EmpfaengerMengeKraftID = neuerAuftrag.EmpfaengerMengeKraftID;
				neuerErkundungsbefehl.ID = neuerAuftrag.ID;
				neuerErkundungsbefehl.IstBefehl = neuerAuftrag.IstBefehl;
				if (Enum.IsDefined(typeof(Tdv_BefehlArt),aktuelleEBArt))
				{
					neuerErkundungsbefehl.BefehlsArt = aktuelleEBArt;
				}
				neuerErkundungsbefehl.IstInToDoListe = neuerAuftrag.IstInToDoListe;
				neuerErkundungsbefehl.IstUebermittelt = neuerAuftrag.IstUebermittelt;
				neuerErkundungsbefehl.Uebermittlungsdatum = neuerAuftrag.Uebermittlungsdatum;
				neuerErkundungsbefehl.SpaetesterErfuellungszeitpunkt = neuerAuftrag.SpaetesterErfuellungszeitpunkt;
				neuerErkundungsbefehl.Text = neuerAuftrag.Text;
				neuerErkundungsbefehl.Uebermittlungsart = neuerAuftrag.Uebermittlungsart;
				neuerErkundungsbefehl.WirdNachverfolgt = neuerAuftrag.WirdNachverfolgt;
				this._st_Funk.SpeichereAuftrag(neuerErkundungsbefehl);
				#endregion
			}
			else 
			{
				neuerAuftrag.IstBefehl = false;
				neuerAuftrag = this._st_Funk.SpeichereAuftrag(neuerAuftrag);
			}

			_zuletztGespeicherteMitteilung = neuerAuftrag;
			// TODO: prüfen, ob speichern erfolgreich
			return true;
		}
		/// <summary>
		/// ermittelt die Art des Erkundungsbefehls aus dem entsprechenden
		/// Eingabeelement
		/// </summary>
		/// <param name="pinout_Befehlart"></param>
		/// <returns></returns>
		private Tdv_BefehlArt PruefeErkundungsbefehl()
		{
			string tmp = this.cmb_Befehlsart.Text;
			foreach(pELS.Tdv_BefehlArt ua in 
				Enum.GetValues(typeof(pELS.Tdv_BefehlArt)))
			{
				if (-1 != tmp.IndexOf(ua.ToString()))
					return ua;
			}
			return (Tdv_BefehlArt) 0;
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
		protected virtual void btn_AuftragSpeichern_Click(object sender, System.EventArgs e)
		{
			if (b_FelderModifiziert)
				if (Eingabevalidierung())
				{
					SpeichereAuftrag();
					Zuruecksetzen();
				}
				else
				{
					pELS.GUI.PopUp.CPopUp.MeldenVonFalscherEingabe();
				}
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
		/// löst das Zurücksetzen der Eingabemaske aus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_AuftragZuruecksetzen_Click(object sender, System.EventArgs e)
		{
			ZuruecksetzenMitRueckfrage();		
		}

		/// <summary>
		/// Speichert einen Auftrag und feuert dann das Event, um nach Report zu wechseln
		/// und den Auftrag drucken zu können
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_AuftragDrucken_Click(object sender, System.EventArgs e)
		{
			if (b_FelderModifiziert)
				if (Eingabevalidierung())
				{
					SpeichereAuftrag();
					Zuruecksetzen();
					// Event feuern lassen
					_st_Funk.FeuereReportRequestedEvent(_zuletztGespeicherteMitteilung);
				}
				else
				{
					pELS.GUI.PopUp.CPopUp.MeldenVonFalscherEingabe();
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

		private void cbx_Ausfuehrungszeitpunkt_CheckedChanged(object sender, System.EventArgs e)
		{
			if (cbx_AusfuehrungszeitpunktJetzt.Checked==true)
			{
				//auf aktuelle Zeit zurück setzen
				dtp_Ausfuehrungszeitpunkt.Value = DateTime.Now;
				// DateTimePicker ausgrauen
				dtp_Ausfuehrungszeitpunkt.Enabled = false;
			}
			else
			{
				//DateTimePicker wieder aktivieren
				dtp_Ausfuehrungszeitpunkt.Enabled = true;
			}
		}

		private void cbx_spaetesterErfuellungszeitpunktJetzt_CheckedChanged(object sender, System.EventArgs e)
		{
			if (cbx_spaetesterErfuellungszeitpunktJetzt.Checked==true)
			{
				//auf aktuelle Zeit zurück setzen
				dtp_spaetesterErfuellungszeitpunkt.Value = DateTime.Now;
				// dtp_spaetesterErfuellungszeitpunkt ausgrauen
				dtp_spaetesterErfuellungszeitpunkt.Enabled = false;
			}
			else
			{
				//dtp_spaetesterErfuellungszeitpunkt wieder aktivieren
				dtp_spaetesterErfuellungszeitpunkt.Enabled = true;
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


		/// <summary>
		/// Wenn die Systemrolle des aktuellen Benutzers verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AkualisiereAktBenutzer()
		{
			this.lbl_BearbeiterName.Text = this._st_Funk.HoleAktuellenBenutzer().Benutzername;
			this.lbl_BearbeiterRolle.Text = this._st_Funk.HoleAktuellenBenutzer().Systemrolle.ToString();
		}

	}
}
