using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pELS.Client.ToDo
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

		protected System.Windows.Forms.DateTimePicker dtp_Auftrag_spaetesterErfuellungszeitpunkt;
		protected System.Windows.Forms.CheckBox cbx_Auftrag_nachverfolgen;
		protected System.Windows.Forms.GroupBox gbx_Auftrag_Auftragskontext;
		protected System.Windows.Forms.Label lbl_Auftrag_Auftragskontext_Uebermittlungsart;
		protected System.Windows.Forms.Label lbl_Auftrag_Auftragskontext_Datum;
		protected System.Windows.Forms.Label lbl_Auftrag_Auftragstext;
		protected System.Windows.Forms.RichTextBox txt_Auftrag_Auftragstext;
		protected System.Windows.Forms.TreeView tvw_Auftrag_AuftragsEmpfaenger;
		protected System.Windows.Forms.Label lbl_Auftrag_AuftragsEmpfaenger;
		protected System.Windows.Forms.CheckBox cbx_Auftrag_IstUebermittelt;
		protected System.ComponentModel.Container components = null;
		protected System.Windows.Forms.ComboBox cmb_Benutzerempfaenger;
		protected System.Windows.Forms.Label label1;
		protected System.Windows.Forms.DateTimePicker dtp_Uebermittlungsdatum;
		protected System.Windows.Forms.ComboBox cmb_Befehlsart;
		protected System.Windows.Forms.Label _lbl_Befehl;
		protected System.Windows.Forms.Label lbl_UMZeitpunkt;
		protected System.Windows.Forms.GroupBox gbx_Ubermittlung;
		protected System.Windows.Forms.CheckBox cbx_spaetesterErfuellungszeitpunktJetzt;
		protected System.Windows.Forms.CheckBox cbx_AbfassungsdatumJetzt;
		protected System.Windows.Forms.DateTimePicker dtp_AbfassungsDatum;
		protected System.Windows.Forms.CheckBox chk_IstUebermittelt;
		protected System.Windows.Forms.Label lbl_BearbeiterName;
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
		protected System.Windows.Forms.ComboBox cmb_UebermittlungsArt;
		protected System.Windows.Forms.GroupBox gbx_Abfassung;
		protected System.Windows.Forms.GroupBox gbx_Ausfuehrung;
		#endregion
		#region Variablen
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
		private Cst_ToDo _st_ToDo;
		private Cpr_usc_TODO _pr_ToDo;
		protected System.Windows.Forms.Button btn_Zuruecksetzen;
		protected System.Windows.Forms.Button btn_Speichern;
		protected System.Windows.Forms.Button btn_Drucken;
		protected System.Windows.Forms.Label lbl_laufendeNummer;
		protected System.Windows.Forms.Label lbl_TEXT_laufendeNummer;
		/// <summary>
		///  beinhaltet den Zustand des zuletzt gecheckten Knoten aus dem EmpfängerTreeView
		/// </summary>
		private bool LetzterTVKnotenZustand;


		#endregion

		#region Konstruktor & Destruktor
		public usc_Auftrag(Cst_ToDo pin_Cst_ToDo, Cpr_usc_TODO pin_Cpr_ToDo)
		{
			this._st_ToDo = pin_Cst_ToDo;
			this._pr_ToDo = pin_Cpr_ToDo;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			SetzeGUIElemente();
			SetzeBefehlsart();
			SetzeUebermittlungsart();
			SetzeTreeView();

			Zuruecksetzen();
//			this._b_FelderModifiziert = false;
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
			this.dtp_Auftrag_spaetesterErfuellungszeitpunkt = new System.Windows.Forms.DateTimePicker();
			this.cbx_spaetesterErfuellungszeitpunktJetzt = new System.Windows.Forms.CheckBox();
			this.cmb_Befehlsart = new System.Windows.Forms.ComboBox();
			this.cbx_Auftrag_nachverfolgen = new System.Windows.Forms.CheckBox();
			this.gbx_Auftrag_Auftragskontext = new System.Windows.Forms.GroupBox();
			this.gbx_Abfassung = new System.Windows.Forms.GroupBox();
			this.lbl_TEXT_Absender = new System.Windows.Forms.Label();
			this.lbl_Auftrag_Auftragskontext_Datum = new System.Windows.Forms.Label();
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
			this.cmb_UebermittlungsArt = new System.Windows.Forms.ComboBox();
			this.dtp_Uebermittlungsdatum = new System.Windows.Forms.DateTimePicker();
			this.lbl_UMZeitpunkt = new System.Windows.Forms.Label();
			this.chk_IstUebermittelt = new System.Windows.Forms.CheckBox();
			this.lbl_Auftrag_Auftragskontext_Uebermittlungsart = new System.Windows.Forms.Label();
			this.cbx_UebermittlungszeitpunktJetzt = new System.Windows.Forms.CheckBox();
			this.lbl_laufendeNummer = new System.Windows.Forms.Label();
			this.lbl_TEXT_laufendeNummer = new System.Windows.Forms.Label();
			this.btn_Zuruecksetzen = new System.Windows.Forms.Button();
			this.btn_Speichern = new System.Windows.Forms.Button();
			this.lbl_Auftrag_Auftragstext = new System.Windows.Forms.Label();
			this.txt_Auftrag_Auftragstext = new System.Windows.Forms.RichTextBox();
			this.tvw_Auftrag_AuftragsEmpfaenger = new System.Windows.Forms.TreeView();
			this.lbl_Auftrag_AuftragsEmpfaenger = new System.Windows.Forms.Label();
			this.cbx_Auftrag_IstUebermittelt = new System.Windows.Forms.CheckBox();
			this.btn_Drucken = new System.Windows.Forms.Button();
			this.cmb_Benutzerempfaenger = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.gbx_Auftrag_Auftragskontext.SuspendLayout();
			this.gbx_Abfassung.SuspendLayout();
			this.gbx_Ausfuehrung.SuspendLayout();
			this.gbx_Ubermittlung.SuspendLayout();
			this.SuspendLayout();
			// 
			// dtp_Auftrag_spaetesterErfuellungszeitpunkt
			// 
			this.dtp_Auftrag_spaetesterErfuellungszeitpunkt.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_Auftrag_spaetesterErfuellungszeitpunkt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Auftrag_spaetesterErfuellungszeitpunkt.Location = new System.Drawing.Point(184, 40);
			this.dtp_Auftrag_spaetesterErfuellungszeitpunkt.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp_Auftrag_spaetesterErfuellungszeitpunkt.Name = "dtp_Auftrag_spaetesterErfuellungszeitpunkt";
			this.dtp_Auftrag_spaetesterErfuellungszeitpunkt.Size = new System.Drawing.Size(116, 20);
			this.dtp_Auftrag_spaetesterErfuellungszeitpunkt.TabIndex = 1;
			// 
			// cbx_spaetesterErfuellungszeitpunktJetzt
			// 
			this.cbx_spaetesterErfuellungszeitpunktJetzt.Location = new System.Drawing.Point(132, 44);
			this.cbx_spaetesterErfuellungszeitpunktJetzt.Name = "cbx_spaetesterErfuellungszeitpunktJetzt";
			this.cbx_spaetesterErfuellungszeitpunktJetzt.Size = new System.Drawing.Size(44, 16);
			this.cbx_spaetesterErfuellungszeitpunktJetzt.TabIndex = 0;
			this.cbx_spaetesterErfuellungszeitpunktJetzt.Text = "jetzt";
			this.cbx_spaetesterErfuellungszeitpunktJetzt.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// cmb_Befehlsart
			// 
			this.cmb_Befehlsart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Befehlsart.Location = new System.Drawing.Point(128, 72);
			this.cmb_Befehlsart.Name = "cmb_Befehlsart";
			this.cmb_Befehlsart.Size = new System.Drawing.Size(188, 21);
			this.cmb_Befehlsart.TabIndex = 7;
			this.cmb_Befehlsart.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_Befehlsart.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// cbx_Auftrag_nachverfolgen
			// 
			this.cbx_Auftrag_nachverfolgen.Location = new System.Drawing.Point(8, 424);
			this.cbx_Auftrag_nachverfolgen.Name = "cbx_Auftrag_nachverfolgen";
			this.cbx_Auftrag_nachverfolgen.Size = new System.Drawing.Size(100, 20);
			this.cbx_Auftrag_nachverfolgen.TabIndex = 7;
			this.cbx_Auftrag_nachverfolgen.Text = "nachverfolgen";
			this.cbx_Auftrag_nachverfolgen.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// gbx_Auftrag_Auftragskontext
			// 
			this.gbx_Auftrag_Auftragskontext.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.gbx_Abfassung);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.gbx_Ausfuehrung);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this._lbl_Befehl);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.lbl_BearbeiterName);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.lbl_BearbeiterRolle);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.lbl_TEXT_Bearbeiter);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.lbl_TEXT_Rolle);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.cmb_Befehlsart);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.gbx_Ubermittlung);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.lbl_laufendeNummer);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.lbl_TEXT_laufendeNummer);
			this.gbx_Auftrag_Auftragskontext.Location = new System.Drawing.Point(6, 0);
			this.gbx_Auftrag_Auftragskontext.Name = "gbx_Auftrag_Auftragskontext";
			this.gbx_Auftrag_Auftragskontext.Size = new System.Drawing.Size(615, 164);
			this.gbx_Auftrag_Auftragskontext.TabIndex = 20;
			this.gbx_Auftrag_Auftragskontext.TabStop = false;
			this.gbx_Auftrag_Auftragskontext.Text = "Auftragskontext";
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
			this.gbx_Abfassung.TabIndex = 76;
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
			this.dtp_AbfassungsDatum.TabIndex = 17;
			// 
			// cbx_AbfassungsdatumJetzt
			// 
			this.cbx_AbfassungsdatumJetzt.Location = new System.Drawing.Point(104, 40);
			this.cbx_AbfassungsdatumJetzt.Name = "cbx_AbfassungsdatumJetzt";
			this.cbx_AbfassungsdatumJetzt.Size = new System.Drawing.Size(44, 16);
			this.cbx_AbfassungsdatumJetzt.TabIndex = 68;
			this.cbx_AbfassungsdatumJetzt.Text = "jetzt";
			this.cbx_AbfassungsdatumJetzt.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// txt_Absender
			// 
			this.txt_Absender.Location = new System.Drawing.Point(120, 12);
			this.txt_Absender.Name = "txt_Absender";
			this.txt_Absender.Size = new System.Drawing.Size(152, 20);
			this.txt_Absender.TabIndex = 70;
			this.txt_Absender.Text = "";
			this.txt_Absender.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// gbx_Ausfuehrung
			// 
			this.gbx_Ausfuehrung.Controls.Add(this.cbx_spaetesterErfuellungszeitpunktJetzt);
			this.gbx_Ausfuehrung.Controls.Add(this.lbl_spaetester_Ausfuehrungszeitpunkt);
			this.gbx_Ausfuehrung.Controls.Add(this.dtp_Ausfuehrungszeitpunkt);
			this.gbx_Ausfuehrung.Controls.Add(this.lbl_Ausfuehrungszeitpunkt);
			this.gbx_Ausfuehrung.Controls.Add(this.cbx_AusfuehrungszeitpunktJetzt);
			this.gbx_Ausfuehrung.Controls.Add(this.dtp_Auftrag_spaetesterErfuellungszeitpunkt);
			this.gbx_Ausfuehrung.Location = new System.Drawing.Point(8, 92);
			this.gbx_Ausfuehrung.Name = "gbx_Ausfuehrung";
			this.gbx_Ausfuehrung.Size = new System.Drawing.Size(308, 64);
			this.gbx_Ausfuehrung.TabIndex = 75;
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
			this.dtp_Ausfuehrungszeitpunkt.TabIndex = 73;
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
			this.cbx_AusfuehrungszeitpunktJetzt.TabIndex = 74;
			this.cbx_AusfuehrungszeitpunktJetzt.Text = "jetzt";
			this.cbx_AusfuehrungszeitpunktJetzt.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
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
			this.gbx_Ubermittlung.Controls.Add(this.cmb_UebermittlungsArt);
			this.gbx_Ubermittlung.Controls.Add(this.dtp_Uebermittlungsdatum);
			this.gbx_Ubermittlung.Controls.Add(this.lbl_UMZeitpunkt);
			this.gbx_Ubermittlung.Controls.Add(this.chk_IstUebermittelt);
			this.gbx_Ubermittlung.Controls.Add(this.lbl_Auftrag_Auftragskontext_Uebermittlungsart);
			this.gbx_Ubermittlung.Controls.Add(this.cbx_UebermittlungszeitpunktJetzt);
			this.gbx_Ubermittlung.Location = new System.Drawing.Point(324, 72);
			this.gbx_Ubermittlung.Name = "gbx_Ubermittlung";
			this.gbx_Ubermittlung.Size = new System.Drawing.Size(280, 84);
			this.gbx_Ubermittlung.TabIndex = 67;
			this.gbx_Ubermittlung.TabStop = false;
			// 
			// cmb_UebermittlungsArt
			// 
			this.cmb_UebermittlungsArt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_UebermittlungsArt.Location = new System.Drawing.Point(124, 12);
			this.cmb_UebermittlungsArt.Name = "cmb_UebermittlungsArt";
			this.cmb_UebermittlungsArt.Size = new System.Drawing.Size(148, 21);
			this.cmb_UebermittlungsArt.TabIndex = 13;
			this.cmb_UebermittlungsArt.Click += new System.EventHandler(this.FelderModifiziert);
			// 
			// dtp_Uebermittlungsdatum
			// 
			this.dtp_Uebermittlungsdatum.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_Uebermittlungsdatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Uebermittlungsdatum.Location = new System.Drawing.Point(152, 52);
			this.dtp_Uebermittlungsdatum.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp_Uebermittlungsdatum.Name = "dtp_Uebermittlungsdatum";
			this.dtp_Uebermittlungsdatum.Size = new System.Drawing.Size(120, 20);
			this.dtp_Uebermittlungsdatum.TabIndex = 28;
			// 
			// lbl_UMZeitpunkt
			// 
			this.lbl_UMZeitpunkt.Location = new System.Drawing.Point(8, 56);
			this.lbl_UMZeitpunkt.Name = "lbl_UMZeitpunkt";
			this.lbl_UMZeitpunkt.Size = new System.Drawing.Size(92, 20);
			this.lbl_UMZeitpunkt.TabIndex = 66;
			this.lbl_UMZeitpunkt.Text = "ÜM-Zeitpunkt";
			// 
			// chk_IstUebermittelt
			// 
			this.chk_IstUebermittelt.Checked = true;
			this.chk_IstUebermittelt.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk_IstUebermittelt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chk_IstUebermittelt.Location = new System.Drawing.Point(8, 36);
			this.chk_IstUebermittelt.Name = "chk_IstUebermittelt";
			this.chk_IstUebermittelt.Size = new System.Drawing.Size(92, 20);
			this.chk_IstUebermittelt.TabIndex = 14;
			this.chk_IstUebermittelt.Text = "ist übermittelt";
			this.chk_IstUebermittelt.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
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
			this.cbx_UebermittlungszeitpunktJetzt.TabIndex = 75;
			this.cbx_UebermittlungszeitpunktJetzt.Text = "jetzt";
			this.cbx_UebermittlungszeitpunktJetzt.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// lbl_laufendeNummer
			// 
			this.lbl_laufendeNummer.Location = new System.Drawing.Point(128, 52);
			this.lbl_laufendeNummer.Name = "lbl_laufendeNummer";
			this.lbl_laufendeNummer.Size = new System.Drawing.Size(100, 16);
			this.lbl_laufendeNummer.TabIndex = 80;
			// 
			// lbl_TEXT_laufendeNummer
			// 
			this.lbl_TEXT_laufendeNummer.Location = new System.Drawing.Point(12, 52);
			this.lbl_TEXT_laufendeNummer.Name = "lbl_TEXT_laufendeNummer";
			this.lbl_TEXT_laufendeNummer.Size = new System.Drawing.Size(100, 16);
			this.lbl_TEXT_laufendeNummer.TabIndex = 79;
			this.lbl_TEXT_laufendeNummer.Text = "lfd. Nummer";
			// 
			// btn_Zuruecksetzen
			// 
			this.btn_Zuruecksetzen.Location = new System.Drawing.Point(448, 424);
			this.btn_Zuruecksetzen.Name = "btn_Zuruecksetzen";
			this.btn_Zuruecksetzen.Size = new System.Drawing.Size(81, 25);
			this.btn_Zuruecksetzen.TabIndex = 19;
			this.btn_Zuruecksetzen.Text = "&Zurücksetzen";
			this.btn_Zuruecksetzen.Click += new System.EventHandler(this.btn_Auftrag_AuftragZuruecksetzen_Click);
			// 
			// btn_Speichern
			// 
			this.btn_Speichern.Location = new System.Drawing.Point(536, 424);
			this.btn_Speichern.Name = "btn_Speichern";
			this.btn_Speichern.Size = new System.Drawing.Size(80, 25);
			this.btn_Speichern.TabIndex = 18;
			this.btn_Speichern.Text = "&Speichern";
			this.btn_Speichern.Click += new System.EventHandler(this.btn_Auftrag_AuftragSpeichern_Click);
			// 
			// lbl_Auftrag_Auftragstext
			// 
			this.lbl_Auftrag_Auftragstext.Location = new System.Drawing.Point(8, 176);
			this.lbl_Auftrag_Auftragstext.Name = "lbl_Auftrag_Auftragstext";
			this.lbl_Auftrag_Auftragstext.Size = new System.Drawing.Size(65, 15);
			this.lbl_Auftrag_Auftragstext.TabIndex = 17;
			this.lbl_Auftrag_Auftragstext.Text = "Auftragstext";
			// 
			// txt_Auftrag_Auftragstext
			// 
			this.txt_Auftrag_Auftragstext.Location = new System.Drawing.Point(4, 196);
			this.txt_Auftrag_Auftragstext.Name = "txt_Auftrag_Auftragstext";
			this.txt_Auftrag_Auftragstext.Size = new System.Drawing.Size(400, 216);
			this.txt_Auftrag_Auftragstext.TabIndex = 16;
			this.txt_Auftrag_Auftragstext.Text = "";
			this.txt_Auftrag_Auftragstext.Enter += new System.EventHandler(this.FelderModifiziert);
			// 
			// tvw_Auftrag_AuftragsEmpfaenger
			// 
			this.tvw_Auftrag_AuftragsEmpfaenger.CheckBoxes = true;
			this.tvw_Auftrag_AuftragsEmpfaenger.ImageIndex = -1;
			this.tvw_Auftrag_AuftragsEmpfaenger.Location = new System.Drawing.Point(416, 196);
			this.tvw_Auftrag_AuftragsEmpfaenger.Name = "tvw_Auftrag_AuftragsEmpfaenger";
			this.tvw_Auftrag_AuftragsEmpfaenger.SelectedImageIndex = -1;
			this.tvw_Auftrag_AuftragsEmpfaenger.Size = new System.Drawing.Size(200, 172);
			this.tvw_Auftrag_AuftragsEmpfaenger.TabIndex = 14;
			// 
			// lbl_Auftrag_AuftragsEmpfaenger
			// 
			this.lbl_Auftrag_AuftragsEmpfaenger.Location = new System.Drawing.Point(420, 176);
			this.lbl_Auftrag_AuftragsEmpfaenger.Name = "lbl_Auftrag_AuftragsEmpfaenger";
			this.lbl_Auftrag_AuftragsEmpfaenger.Size = new System.Drawing.Size(128, 16);
			this.lbl_Auftrag_AuftragsEmpfaenger.TabIndex = 15;
			this.lbl_Auftrag_AuftragsEmpfaenger.Text = "Auftragsempfänger";
			// 
			// cbx_Auftrag_IstUebermittelt
			// 
			this.cbx_Auftrag_IstUebermittelt.Location = new System.Drawing.Point(0, 0);
			this.cbx_Auftrag_IstUebermittelt.Name = "cbx_Auftrag_IstUebermittelt";
			this.cbx_Auftrag_IstUebermittelt.TabIndex = 0;
			this.cbx_Auftrag_IstUebermittelt.CheckStateChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// btn_Drucken
			// 
			this.btn_Drucken.Location = new System.Drawing.Point(360, 424);
			this.btn_Drucken.Name = "btn_Drucken";
			this.btn_Drucken.Size = new System.Drawing.Size(80, 25);
			this.btn_Drucken.TabIndex = 62;
			this.btn_Drucken.Text = "&Drucken";
			// 
			// cmb_Benutzerempfaenger
			// 
			this.cmb_Benutzerempfaenger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Benutzerempfaenger.Location = new System.Drawing.Point(416, 392);
			this.cmb_Benutzerempfaenger.Name = "cmb_Benutzerempfaenger";
			this.cmb_Benutzerempfaenger.Size = new System.Drawing.Size(200, 21);
			this.cmb_Benutzerempfaenger.TabIndex = 65;
			this.cmb_Benutzerempfaenger.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(416, 376);
			this.label1.Name = "label1";
			this.label1.TabIndex = 64;
			this.label1.Text = "interner Empänger";
			// 
			// usc_Auftrag
			// 
			this.Controls.Add(this.cmb_Benutzerempfaenger);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btn_Drucken);
			this.Controls.Add(this.gbx_Auftrag_Auftragskontext);
			this.Controls.Add(this.btn_Zuruecksetzen);
			this.Controls.Add(this.btn_Speichern);
			this.Controls.Add(this.lbl_Auftrag_Auftragstext);
			this.Controls.Add(this.txt_Auftrag_Auftragstext);
			this.Controls.Add(this.tvw_Auftrag_AuftragsEmpfaenger);
			this.Controls.Add(this.lbl_Auftrag_AuftragsEmpfaenger);
			this.Controls.Add(this.cbx_Auftrag_nachverfolgen);
			this.Location = new System.Drawing.Point(6, 21);
			this.Name = "usc_Auftrag";
			this.Size = new System.Drawing.Size(624, 456);
			this.gbx_Auftrag_Auftragskontext.ResumeLayout(false);
			this.gbx_Abfassung.ResumeLayout(false);
			this.gbx_Ausfuehrung.ResumeLayout(false);
			this.gbx_Ubermittlung.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		#endregion

		#region SetzeXXX
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
			this.cmb_UebermittlungsArt.Items.Clear();
			foreach(pELS.Tdv_Uebermittlungsart ua in 
				Enum.GetValues(typeof(pELS.Tdv_Uebermittlungsart)))
			{
				this.cmb_UebermittlungsArt.Items.Add(ua);
			}
			this.cmb_UebermittlungsArt.SelectedIndex = 0;
		}

		/// <summary>
		/// modifiziert Standardwerte von GUI-Elementen (z.B. Sichtbarkeit)
		/// </summary>
		virtual protected void SetzeGUIElemente()
		{
			this.gbx_Abfassung.Enabled = false;
			this.gbx_Ausfuehrung.Enabled = false;
			this.gbx_Ubermittlung.Enabled = false;
			this.txt_Auftrag_Auftragstext.ReadOnly = true;
			this.cmb_Benutzerempfaenger.Enabled = false;
			this.chk_IstUebermittelt.Visible = true;
			this.chk_IstUebermittelt.Checked = true;
			this.cmb_Befehlsart.Enabled = false;
			this.cbx_AbfassungsdatumJetzt.Visible = false;
			this.cbx_AusfuehrungszeitpunktJetzt.Visible = false;
			this.cbx_spaetesterErfuellungszeitpunktJetzt.Visible = false;
			this.cbx_Auftrag_nachverfolgen.Enabled = false;
			this.cmb_Benutzerempfaenger.DropDownStyle = ComboBoxStyle.DropDown;

			this.btn_Drucken.Visible = false;
			this.btn_Speichern.Visible = false;
			this.btn_Zuruecksetzen.Visible = false;

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
			_b_FelderModifiziert = false;
			txt_Auftrag_Auftragstext.Text = "";
			dtp_AbfassungsDatum.Value = DateTime.Now;
			cmb_UebermittlungsArt.SelectedIndex = 0;
			cmb_Befehlsart.Text = "Befehlsart";
			cbx_Auftrag_nachverfolgen.Checked = false;
			cbx_spaetesterErfuellungszeitpunktJetzt.Checked = false;
			this.ZuruecksetzenTreeView(this.tvw_Auftrag_AuftragsEmpfaenger.Nodes);
		}

		/// <summary>
		/// erlaubt die Rückfrage beim Benutzer bevor alle Werte zurückgesetzt werden
		/// </summary>
		public void ZuruecksetzenMitRueckfrage()
		{
		}

		#endregion

		#region LadeAuftrag - Fkr. zum Laden eines Auftrags
		/// <summary>
		/// lädt einen Auftrag in das Formular
		/// </summary>
		/// <param name="pin_Meldung"></param>
		public void LadeAuftrag(Cdv_Auftrag pin_Auftrag)
		{
			this.lbl_laufendeNummer.Text = pin_Auftrag.LaufendeNummer.ToString();
			this.dtp_AbfassungsDatum.Value = pin_Auftrag.Abfassungsdatum;
			// Bearbeiter
			Cdv_Benutzer BenutzerBearbeiter = _st_ToDo.ID2Benutzer(pin_Auftrag.BearbeiterBenutzerID);
			if (BenutzerBearbeiter != null)
			{
				this.lbl_BearbeiterRolle.Text = BenutzerBearbeiter.Systemrolle.ToString();
				this.lbl_BearbeiterName.Text = BenutzerBearbeiter.Benutzername;
			}
			// Absender
			this.txt_Absender.Text = pin_Auftrag.Absender;
			// Benutzerempfänger
			Cdv_Benutzer BenutzerEmpfaenger = _st_ToDo.ID2Benutzer(pin_Auftrag.EmpfaengerBenutzerID);
			if (BenutzerEmpfaenger != null)
				this.cmb_Benutzerempfaenger.Text = BenutzerEmpfaenger.Benutzername;
			else
				this.cmb_Benutzerempfaenger.Text = "";
			// EmpfängerKräfteMenge
			_pr_ToDo.LadeMitteilungsEmpfaenger(
				this.tvw_Auftrag_AuftragsEmpfaenger, 
				pin_Auftrag.EmpfaengerMengeKraftID);
			this.dtp_Auftrag_spaetesterErfuellungszeitpunkt.Value = pin_Auftrag.Ausfuehrungszeitpunkt;
			if (pin_Auftrag.IstBefehl)
			{
				Cdv_Erkundungsbefehl tmpEB = pin_Auftrag as Cdv_Erkundungsbefehl;
				this.cmb_Befehlsart.SelectedItem = "Erkundungsbefehl (" + tmpEB.BefehlsArt + ")";
			}
			else 
				this.cmb_Befehlsart.SelectedIndex = 0;
			//Übermittlung
			this.cbx_Auftrag_IstUebermittelt.Checked = pin_Auftrag.IstUebermittelt;
			// Text
			this.txt_Auftrag_Auftragstext.Text = pin_Auftrag.Text;
			// Übermittlungsart
			this.cmb_UebermittlungsArt.SelectedItem = pin_Auftrag.Uebermittlungsart;
			// Übermittlungsdatum
			this.dtp_Uebermittlungsdatum.Value = pin_Auftrag.Uebermittlungsdatum;
			this.cbx_Auftrag_nachverfolgen.Checked = pin_Auftrag.WirdNachverfolgt;

			this.dtp_Auftrag_spaetesterErfuellungszeitpunkt.Value = 
				pin_Auftrag.SpaetesterErfuellungszeitpunkt;
		}

		#endregion

		#region events
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

		/// <summary>
		/// löst das Speichern eines Auftrags aus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Auftrag_AuftragSpeichern_Click(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// löst das Zurücksetzen der Eingabemaske aus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Auftrag_AuftragZuruecksetzen_Click(object sender, System.EventArgs e)
		{
			ZuruecksetzenMitRueckfrage();		
		}

		private void cbx_Auftrag_Befehl_CheckedChanged(object sender, System.EventArgs e)
		{
			// TODO
//			if (this.cbx_Auftrag_Befehl.Checked == true)
//			{
//				this.cmb_Auftrag_Befehlsart.Visible = true;
//			}
//			else
//			{
//				cmb_Auftrag_Befehlsart.Visible = false;
//			}
		}
				
		/// <summary>
		/// deaktiviert die 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cbx_IstUebermittelt_CheckedChanged(object sender, System.EventArgs e)
		{
			if (cbx_Auftrag_IstUebermittelt.Checked==true)
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
				dtp_Auftrag_spaetesterErfuellungszeitpunkt.Value = DateTime.Now;
				// dtp_spaetesterErfuellungszeitpunkt ausgrauen
				dtp_Auftrag_spaetesterErfuellungszeitpunkt.Enabled = false;
			}
			else
			{
				//dtp_spaetesterErfuellungszeitpunkt wieder aktivieren
				dtp_Auftrag_spaetesterErfuellungszeitpunkt.Enabled = true;
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
		#endregion

		#region TreeView - ReadOnly
		private void SetzeTreeView()
		{
			this.tvw_Auftrag_AuftragsEmpfaenger.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvw_Auftrag_AuftragsEmpfaenger_AfterCheck);
			this.tvw_Auftrag_AuftragsEmpfaenger.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvw_Auftrag_AuftragsEmpfaenger_BeforeCheck);

		}
		/// <summary>
		///  setzt den letzten Zustand von Checked eines TreeView-Knotens falls 
		///  EmpfaengerTreeViewModifizierbar == false
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvw_Auftrag_AuftragsEmpfaenger_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if(e.Action != TreeViewAction.Unknown)
			{
				e.Node.Checked = LetzterTVKnotenZustand;
			}
		}

		/// <summary>
		///  speichert den letzten Zustand von Checked eines TreeView-Knotens falls 
		///  EmpfaengerTreeViewModifizierbar == false
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvw_Auftrag_AuftragsEmpfaenger_BeforeCheck(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			if(e.Action != TreeViewAction.Unknown)
			{
				LetzterTVKnotenZustand = e.Node.Checked;
			}
		}

		#endregion

	}
}