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
		#region eigene Variablen
		private Cst_MAT _stMAT;
		private bool _istMeldungAbfassen;
		/// <summary>
		/// ermöglicht das Anzeigen von fehlerhaften Eingaben
		/// </summary>
		protected System.Windows.Forms.ErrorProvider ep_Eingabe;
		/// <summary>
		/// gibt an, ob bereits Eingaben geschehen sind
		/// </summary>
		protected bool _b_FelderModifiziert = false;
		/// <summary>
		/// zur Übergabe an das Event ReportRequested, um die Mitteilung drucken zu können
		/// </summary>
		private Cdv_Mitteilung _zuletztGespeicherteMitteilung;
	
		#endregion

		#region graphische Elemente der Klasse
		public System.Windows.Forms.Panel pnl_Meldung;
		public System.Windows.Forms.Button btn_Meldung_Drucken;
		public System.Windows.Forms.Button btn_Meldung_Zuruecksetzen;
		public System.Windows.Forms.Button btn_Meldung_Speichern;
		public System.Windows.Forms.TreeView tvw_Meldung_Empfaenger;
		public System.Windows.Forms.Label lbl_Meldung_Empfaenger;
		public System.Windows.Forms.GroupBox gbx_Meldung_Meldungskontext;
		public System.Windows.Forms.Label lbl_Meldung_Kategorie;
		public System.Windows.Forms.Label lbl_Meldung_Meldungstext;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.ComboBox cmb_Benutzerempfaenger;
		private System.Windows.Forms.Label lbl_Meldung_BearbeiterText;
		private System.Windows.Forms.Label lbl_Meldung_RolleText;
		private System.Windows.Forms.Label lbl_Meldung_Bearbeiter;
		private System.Windows.Forms.Label lbl_Meldung_Rolle;
		private System.Windows.Forms.TextBox txt_Meldung_AbsenderText;
		private System.Windows.Forms.Label lbl_Meldung_Absender;

		private System.ComponentModel.Container components = null;

		private System.Windows.Forms.GroupBox gbx_Meldungstyp;
		private System.Windows.Forms.RadioButton rBtn_Meldungart_Erkundungsbericht;
		private System.Windows.Forms.RadioButton rBtn_Meldungart_Meldung;
		public System.Windows.Forms.ComboBox cmb_Meldungskategorie;
		private System.Windows.Forms.GroupBox gbx_Abfassung;
		public System.Windows.Forms.Label lbl_Auftrag_Auftragskontext_Datum;
		public System.Windows.Forms.DateTimePicker dtp_AbfassungsDatum;
		private System.Windows.Forms.GroupBox gbx_Ubermittlung;
		public System.Windows.Forms.DateTimePicker dtp_Uebermittlungsdatum;
		private System.Windows.Forms.Label lbl_UMZeitpunkt;
		public System.Windows.Forms.Label lbl_Auftrag_Auftragskontext_Uebermittlungsart;
		public System.Windows.Forms.CheckBox cbx_UebermittlungszeitpunktJetzt;
		public System.Windows.Forms.ComboBox cmb_Uebermittlungsart;
		public System.Windows.Forms.CheckBox cbx_AbfassungsdatumJetzt;
		public System.Windows.Forms.RichTextBox txt_Meldungstext;
		public System.Windows.Forms.CheckBox cbx_IstUebermittelt;
		private System.Windows.Forms.GroupBox gbx_MeldungAbfassenUndErfassenPlugskontainer;
		protected System.Windows.Forms.GroupBox gbx_Erkundung;
		private System.Windows.Forms.GroupBox groupBox5;
		protected System.Windows.Forms.CheckBox cbx_Strom;
		protected System.Windows.Forms.CheckBox cbx_Wasser;
		protected System.Windows.Forms.CheckBox cbx_Abwasser;
		private System.Windows.Forms.GroupBox groupBox4;
		protected pELS.Tools.MaskedTextBox txt_KellerInProzent;
		protected System.Windows.Forms.CheckBox cbx_KellerIstVorhanden;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		protected System.Windows.Forms.TextBox txt_Strasse;
		protected System.Windows.Forms.TextBox txt_Ort;
		protected System.Windows.Forms.TextBox txt_PLZ;
		protected System.Windows.Forms.TextBox txt_HausNr;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		protected System.Windows.Forms.DateTimePicker dtp_Erk_Datum;
		protected System.Windows.Forms.Label lbl_Erk_Datum;
		private System.Windows.Forms.Label lbl_Einsatzschwerpunkt;
		public System.Windows.Forms.ComboBox cmb_Einsatzschwerpunkte;
		protected System.Windows.Forms.Label lbl_Heizung;
		protected System.Windows.Forms.Label lbl_Bezeichnung;
		protected System.Windows.Forms.TextBox txt_Erkundungsobjekt;
		protected System.Windows.Forms.TextBox txt_Heizung;
		protected System.Windows.Forms.TextBox txt_Haustyp;
		protected System.Windows.Forms.TextBox txt_Erkunder;
		protected System.Windows.Forms.ComboBox cmb_Bauart;
		protected System.Windows.Forms.Label lbl_Erkunder;
		protected System.Windows.Forms.Label lbl_Haustyp;
		protected System.Windows.Forms.Label lbl_TEXT_laufendeNummer;
		private System.Windows.Forms.GroupBox gbx_;
		protected System.Windows.Forms.Label lbl_Bauart;
#endregion

		#region construktor
		public usc_Meldung(Cst_MAT pin_stMAT, bool pin_istAbfassen)
		{ 
			this._stMAT = pin_stMAT;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.SetzeEvents();
			
			this._istMeldungAbfassen = pin_istAbfassen;	
			// Initialisiere alle Steuerelemente
			InitAlleSTE();
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
			this.ep_Eingabe = new System.Windows.Forms.ErrorProvider();
			this.pnl_Meldung = new System.Windows.Forms.Panel();
			this.btn_Meldung_Drucken = new System.Windows.Forms.Button();
			this.btn_Meldung_Zuruecksetzen = new System.Windows.Forms.Button();
			this.btn_Meldung_Speichern = new System.Windows.Forms.Button();
			this.tvw_Meldung_Empfaenger = new System.Windows.Forms.TreeView();
			this.lbl_Meldung_Empfaenger = new System.Windows.Forms.Label();
			this.gbx_Meldung_Meldungskontext = new System.Windows.Forms.GroupBox();
			this.lbl_TEXT_laufendeNummer = new System.Windows.Forms.Label();
			this.gbx_MeldungAbfassenUndErfassenPlugskontainer = new System.Windows.Forms.GroupBox();
			this.lbl_Meldung_BearbeiterText = new System.Windows.Forms.Label();
			this.lbl_Meldung_RolleText = new System.Windows.Forms.Label();
			this.lbl_Meldung_Bearbeiter = new System.Windows.Forms.Label();
			this.lbl_Meldung_Rolle = new System.Windows.Forms.Label();
			this.gbx_Ubermittlung = new System.Windows.Forms.GroupBox();
			this.cmb_Uebermittlungsart = new System.Windows.Forms.ComboBox();
			this.dtp_Uebermittlungsdatum = new System.Windows.Forms.DateTimePicker();
			this.lbl_UMZeitpunkt = new System.Windows.Forms.Label();
			this.cbx_IstUebermittelt = new System.Windows.Forms.CheckBox();
			this.lbl_Auftrag_Auftragskontext_Uebermittlungsart = new System.Windows.Forms.Label();
			this.cbx_UebermittlungszeitpunktJetzt = new System.Windows.Forms.CheckBox();
			this.gbx_Abfassung = new System.Windows.Forms.GroupBox();
			this.txt_Meldung_AbsenderText = new System.Windows.Forms.TextBox();
			this.lbl_Meldung_Absender = new System.Windows.Forms.Label();
			this.lbl_Auftrag_Auftragskontext_Datum = new System.Windows.Forms.Label();
			this.dtp_AbfassungsDatum = new System.Windows.Forms.DateTimePicker();
			this.cbx_AbfassungsdatumJetzt = new System.Windows.Forms.CheckBox();
			this.gbx_Meldungstyp = new System.Windows.Forms.GroupBox();
			this.rBtn_Meldungart_Meldung = new System.Windows.Forms.RadioButton();
			this.rBtn_Meldungart_Erkundungsbericht = new System.Windows.Forms.RadioButton();
			this.lbl_Meldung_Kategorie = new System.Windows.Forms.Label();
			this.cmb_Meldungskategorie = new System.Windows.Forms.ComboBox();
			this.txt_Meldungstext = new System.Windows.Forms.RichTextBox();
			this.lbl_Meldung_Meldungstext = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cmb_Benutzerempfaenger = new System.Windows.Forms.ComboBox();
			this.gbx_Erkundung = new System.Windows.Forms.GroupBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.cbx_Strom = new System.Windows.Forms.CheckBox();
			this.cbx_Wasser = new System.Windows.Forms.CheckBox();
			this.cbx_Abwasser = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.txt_KellerInProzent = new pELS.Tools.MaskedTextBox();
			this.cbx_KellerIstVorhanden = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txt_Strasse = new System.Windows.Forms.TextBox();
			this.txt_Ort = new System.Windows.Forms.TextBox();
			this.txt_PLZ = new System.Windows.Forms.TextBox();
			this.txt_HausNr = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.dtp_Erk_Datum = new System.Windows.Forms.DateTimePicker();
			this.lbl_Erk_Datum = new System.Windows.Forms.Label();
			this.lbl_Einsatzschwerpunkt = new System.Windows.Forms.Label();
			this.cmb_Einsatzschwerpunkte = new System.Windows.Forms.ComboBox();
			this.lbl_Bezeichnung = new System.Windows.Forms.Label();
			this.txt_Erkundungsobjekt = new System.Windows.Forms.TextBox();
			this.txt_Erkunder = new System.Windows.Forms.TextBox();
			this.lbl_Erkunder = new System.Windows.Forms.Label();
			this.gbx_ = new System.Windows.Forms.GroupBox();
			this.cmb_Bauart = new System.Windows.Forms.ComboBox();
			this.lbl_Heizung = new System.Windows.Forms.Label();
			this.lbl_Haustyp = new System.Windows.Forms.Label();
			this.lbl_Bauart = new System.Windows.Forms.Label();
			this.txt_Heizung = new System.Windows.Forms.TextBox();
			this.txt_Haustyp = new System.Windows.Forms.TextBox();
			this.gbx_Meldung_Meldungskontext.SuspendLayout();
			this.gbx_MeldungAbfassenUndErfassenPlugskontainer.SuspendLayout();
			this.gbx_Ubermittlung.SuspendLayout();
			this.gbx_Abfassung.SuspendLayout();
			this.gbx_Meldungstyp.SuspendLayout();
			this.gbx_Erkundung.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.gbx_.SuspendLayout();
			this.SuspendLayout();
			// 
			// ep_Eingabe
			// 
			this.ep_Eingabe.ContainerControl = this;
			// 
			// pnl_Meldung
			// 
			this.pnl_Meldung.Location = new System.Drawing.Point(0, 0);
			this.pnl_Meldung.Name = "pnl_Meldung";
			this.pnl_Meldung.TabIndex = 0;
			// 
			// btn_Meldung_Drucken
			// 
			this.btn_Meldung_Drucken.Location = new System.Drawing.Point(304, 424);
			this.btn_Meldung_Drucken.Name = "btn_Meldung_Drucken";
			this.btn_Meldung_Drucken.Size = new System.Drawing.Size(136, 25);
			this.btn_Meldung_Drucken.TabIndex = 7;
			this.btn_Meldung_Drucken.Text = "Speichern && &Drucken";
			this.btn_Meldung_Drucken.Click += new System.EventHandler(this.btn_Meldung_Drucken_Click);
			// 
			// btn_Meldung_Zuruecksetzen
			// 
			this.btn_Meldung_Zuruecksetzen.Location = new System.Drawing.Point(448, 424);
			this.btn_Meldung_Zuruecksetzen.Name = "btn_Meldung_Zuruecksetzen";
			this.btn_Meldung_Zuruecksetzen.Size = new System.Drawing.Size(81, 25);
			this.btn_Meldung_Zuruecksetzen.TabIndex = 6;
			this.btn_Meldung_Zuruecksetzen.Text = "&Zurücksetzen";
			this.btn_Meldung_Zuruecksetzen.Click += new System.EventHandler(this.btn_Meldung_Zuruecksetzen_Click);
			// 
			// btn_Meldung_Speichern
			// 
			this.btn_Meldung_Speichern.Location = new System.Drawing.Point(536, 424);
			this.btn_Meldung_Speichern.Name = "btn_Meldung_Speichern";
			this.btn_Meldung_Speichern.Size = new System.Drawing.Size(80, 25);
			this.btn_Meldung_Speichern.TabIndex = 5;
			this.btn_Meldung_Speichern.Text = "&Speichern";
			this.btn_Meldung_Speichern.Click += new System.EventHandler(this.btn_Meldung_Speichern_Click);
			// 
			// tvw_Meldung_Empfaenger
			// 
			this.tvw_Meldung_Empfaenger.CheckBoxes = true;
			this.tvw_Meldung_Empfaenger.ImageIndex = -1;
			this.tvw_Meldung_Empfaenger.Location = new System.Drawing.Point(412, 188);
			this.tvw_Meldung_Empfaenger.Name = "tvw_Meldung_Empfaenger";
			this.tvw_Meldung_Empfaenger.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																							   new System.Windows.Forms.TreeNode("aktueller Einsatz", new System.Windows.Forms.TreeNode[] {
																																															  new System.Windows.Forms.TreeNode("Module", new System.Windows.Forms.TreeNode[] {
																																																																				  new System.Windows.Forms.TreeNode("Bergungsgruppe1"),
																																																																				  new System.Windows.Forms.TreeNode("Bergungsgruppe2"),
																																																																				  new System.Windows.Forms.TreeNode("Technischer Zug (Cottbus)")}),
																																															  new System.Windows.Forms.TreeNode("Einsatzschwerpunkte", new System.Windows.Forms.TreeNode[] {
																																																																							   new System.Windows.Forms.TreeNode("brennendes Haus"),
																																																																							   new System.Windows.Forms.TreeNode("Elbebrücke1")})}),
																							   new System.Windows.Forms.TreeNode("Kräfte", new System.Windows.Forms.TreeNode[] {
																																												   new System.Windows.Forms.TreeNode("Einheiten"),
																																												   new System.Windows.Forms.TreeNode("Personen", new System.Windows.Forms.TreeNode[] {
																																																																		 new System.Windows.Forms.TreeNode("Meier"),
																																																																		 new System.Windows.Forms.TreeNode("Müller"),
																																																																		 new System.Windows.Forms.TreeNode("Schulze")})})});
			this.tvw_Meldung_Empfaenger.SelectedImageIndex = -1;
			this.tvw_Meldung_Empfaenger.Size = new System.Drawing.Size(197, 180);
			this.tvw_Meldung_Empfaenger.TabIndex = 3;
			this.tvw_Meldung_Empfaenger.Click += new System.EventHandler(this.FelderModifiziert);
			// 
			// lbl_Meldung_Empfaenger
			// 
			this.lbl_Meldung_Empfaenger.Location = new System.Drawing.Point(412, 168);
			this.lbl_Meldung_Empfaenger.Name = "lbl_Meldung_Empfaenger";
			this.lbl_Meldung_Empfaenger.Size = new System.Drawing.Size(128, 16);
			this.lbl_Meldung_Empfaenger.TabIndex = 55;
			this.lbl_Meldung_Empfaenger.Text = "Meldungsempfänger";
			// 
			// gbx_Meldung_Meldungskontext
			// 
			this.gbx_Meldung_Meldungskontext.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_Meldung_Meldungskontext.Controls.Add(this.lbl_TEXT_laufendeNummer);
			this.gbx_Meldung_Meldungskontext.Controls.Add(this.gbx_MeldungAbfassenUndErfassenPlugskontainer);
			this.gbx_Meldung_Meldungskontext.Controls.Add(this.gbx_Ubermittlung);
			this.gbx_Meldung_Meldungskontext.Controls.Add(this.gbx_Abfassung);
			this.gbx_Meldung_Meldungskontext.Controls.Add(this.gbx_Meldungstyp);
			this.gbx_Meldung_Meldungskontext.Controls.Add(this.lbl_Meldung_Kategorie);
			this.gbx_Meldung_Meldungskontext.Controls.Add(this.cmb_Meldungskategorie);
			this.gbx_Meldung_Meldungskontext.Location = new System.Drawing.Point(6, 0);
			this.gbx_Meldung_Meldungskontext.Name = "gbx_Meldung_Meldungskontext";
			this.gbx_Meldung_Meldungskontext.Size = new System.Drawing.Size(615, 164);
			this.gbx_Meldung_Meldungskontext.TabIndex = 0;
			this.gbx_Meldung_Meldungskontext.TabStop = false;
			this.gbx_Meldung_Meldungskontext.Text = "Meldungskontext";
			// 
			// lbl_TEXT_laufendeNummer
			// 
			this.lbl_TEXT_laufendeNummer.Location = new System.Drawing.Point(12, 80);
			this.lbl_TEXT_laufendeNummer.Name = "lbl_TEXT_laufendeNummer";
			this.lbl_TEXT_laufendeNummer.Size = new System.Drawing.Size(80, 20);
			this.lbl_TEXT_laufendeNummer.TabIndex = 80;
			this.lbl_TEXT_laufendeNummer.Text = "lfd. Nummer";
			// 
			// gbx_MeldungAbfassenUndErfassenPlugskontainer
			// 
			this.gbx_MeldungAbfassenUndErfassenPlugskontainer.Controls.Add(this.lbl_Meldung_BearbeiterText);
			this.gbx_MeldungAbfassenUndErfassenPlugskontainer.Controls.Add(this.lbl_Meldung_RolleText);
			this.gbx_MeldungAbfassenUndErfassenPlugskontainer.Controls.Add(this.lbl_Meldung_Bearbeiter);
			this.gbx_MeldungAbfassenUndErfassenPlugskontainer.Controls.Add(this.lbl_Meldung_Rolle);
			this.gbx_MeldungAbfassenUndErfassenPlugskontainer.Location = new System.Drawing.Point(8, 12);
			this.gbx_MeldungAbfassenUndErfassenPlugskontainer.Name = "gbx_MeldungAbfassenUndErfassenPlugskontainer";
			this.gbx_MeldungAbfassenUndErfassenPlugskontainer.Size = new System.Drawing.Size(284, 64);
			this.gbx_MeldungAbfassenUndErfassenPlugskontainer.TabIndex = 79;
			this.gbx_MeldungAbfassenUndErfassenPlugskontainer.TabStop = false;
			// 
			// lbl_Meldung_BearbeiterText
			// 
			this.lbl_Meldung_BearbeiterText.BackColor = System.Drawing.SystemColors.Window;
			this.lbl_Meldung_BearbeiterText.Location = new System.Drawing.Point(88, 40);
			this.lbl_Meldung_BearbeiterText.Name = "lbl_Meldung_BearbeiterText";
			this.lbl_Meldung_BearbeiterText.Size = new System.Drawing.Size(130, 15);
			this.lbl_Meldung_BearbeiterText.TabIndex = 24;
			// 
			// lbl_Meldung_RolleText
			// 
			this.lbl_Meldung_RolleText.BackColor = System.Drawing.SystemColors.Window;
			this.lbl_Meldung_RolleText.Location = new System.Drawing.Point(88, 20);
			this.lbl_Meldung_RolleText.Name = "lbl_Meldung_RolleText";
			this.lbl_Meldung_RolleText.Size = new System.Drawing.Size(130, 15);
			this.lbl_Meldung_RolleText.TabIndex = 23;
			// 
			// lbl_Meldung_Bearbeiter
			// 
			this.lbl_Meldung_Bearbeiter.BackColor = System.Drawing.SystemColors.Window;
			this.lbl_Meldung_Bearbeiter.Location = new System.Drawing.Point(4, 40);
			this.lbl_Meldung_Bearbeiter.Name = "lbl_Meldung_Bearbeiter";
			this.lbl_Meldung_Bearbeiter.Size = new System.Drawing.Size(65, 15);
			this.lbl_Meldung_Bearbeiter.TabIndex = 22;
			this.lbl_Meldung_Bearbeiter.Text = "Bearbeiter:";
			// 
			// lbl_Meldung_Rolle
			// 
			this.lbl_Meldung_Rolle.BackColor = System.Drawing.SystemColors.Window;
			this.lbl_Meldung_Rolle.Location = new System.Drawing.Point(4, 20);
			this.lbl_Meldung_Rolle.Name = "lbl_Meldung_Rolle";
			this.lbl_Meldung_Rolle.Size = new System.Drawing.Size(35, 15);
			this.lbl_Meldung_Rolle.TabIndex = 21;
			this.lbl_Meldung_Rolle.Text = "Rolle: ";
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
			this.gbx_Ubermittlung.Size = new System.Drawing.Size(284, 84);
			this.gbx_Ubermittlung.TabIndex = 3;
			this.gbx_Ubermittlung.TabStop = false;
			// 
			// cmb_Uebermittlungsart
			// 
			this.cmb_Uebermittlungsart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Uebermittlungsart.Items.AddRange(new object[] {
																	   "",
																	   "Funk",
																	   "Fax",
																	   "Telefon",
																	   "Kurier"});
			this.cmb_Uebermittlungsart.Location = new System.Drawing.Point(124, 12);
			this.cmb_Uebermittlungsart.Name = "cmb_Uebermittlungsart";
			this.cmb_Uebermittlungsart.Size = new System.Drawing.Size(148, 21);
			this.cmb_Uebermittlungsart.TabIndex = 1;
			// 
			// dtp_Uebermittlungsdatum
			// 
			this.dtp_Uebermittlungsdatum.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_Uebermittlungsdatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Uebermittlungsdatum.Location = new System.Drawing.Point(152, 52);
			this.dtp_Uebermittlungsdatum.MinDate = new System.DateTime(2004, 11, 2, 0, 0, 0, 0);
			this.dtp_Uebermittlungsdatum.Name = "dtp_Uebermittlungsdatum";
			this.dtp_Uebermittlungsdatum.Size = new System.Drawing.Size(120, 20);
			this.dtp_Uebermittlungsdatum.TabIndex = 3;
			// 
			// lbl_UMZeitpunkt
			// 
			this.lbl_UMZeitpunkt.Location = new System.Drawing.Point(8, 56);
			this.lbl_UMZeitpunkt.Name = "lbl_UMZeitpunkt";
			this.lbl_UMZeitpunkt.Size = new System.Drawing.Size(92, 20);
			this.lbl_UMZeitpunkt.TabIndex = 6;
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
			this.cbx_IstUebermittelt.CheckedChanged += new System.EventHandler(this.cbx_IstUebermittelt_CheckedChanged);
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
			this.gbx_Abfassung.Controls.Add(this.txt_Meldung_AbsenderText);
			this.gbx_Abfassung.Controls.Add(this.lbl_Meldung_Absender);
			this.gbx_Abfassung.Controls.Add(this.lbl_Auftrag_Auftragskontext_Datum);
			this.gbx_Abfassung.Controls.Add(this.dtp_AbfassungsDatum);
			this.gbx_Abfassung.Controls.Add(this.cbx_AbfassungsdatumJetzt);
			this.gbx_Abfassung.Location = new System.Drawing.Point(324, 8);
			this.gbx_Abfassung.Name = "gbx_Abfassung";
			this.gbx_Abfassung.Size = new System.Drawing.Size(284, 64);
			this.gbx_Abfassung.TabIndex = 2;
			this.gbx_Abfassung.TabStop = false;
			// 
			// txt_Meldung_AbsenderText
			// 
			this.txt_Meldung_AbsenderText.Location = new System.Drawing.Point(95, 10);
			this.txt_Meldung_AbsenderText.Name = "txt_Meldung_AbsenderText";
			this.txt_Meldung_AbsenderText.Size = new System.Drawing.Size(175, 20);
			this.txt_Meldung_AbsenderText.TabIndex = 0;
			this.txt_Meldung_AbsenderText.Text = "";
			// 
			// lbl_Meldung_Absender
			// 
			this.lbl_Meldung_Absender.Location = new System.Drawing.Point(10, 15);
			this.lbl_Meldung_Absender.Name = "lbl_Meldung_Absender";
			this.lbl_Meldung_Absender.Size = new System.Drawing.Size(60, 15);
			this.lbl_Meldung_Absender.TabIndex = 29;
			this.lbl_Meldung_Absender.Text = "Absender:";
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
			this.dtp_AbfassungsDatum.Location = new System.Drawing.Point(145, 36);
			this.dtp_AbfassungsDatum.MinDate = new System.DateTime(2004, 11, 2, 0, 0, 0, 0);
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
			// gbx_Meldungstyp
			// 
			this.gbx_Meldungstyp.Controls.Add(this.rBtn_Meldungart_Meldung);
			this.gbx_Meldungstyp.Controls.Add(this.rBtn_Meldungart_Erkundungsbericht);
			this.gbx_Meldungstyp.Location = new System.Drawing.Point(4, 120);
			this.gbx_Meldungstyp.Name = "gbx_Meldungstyp";
			this.gbx_Meldungstyp.Size = new System.Drawing.Size(284, 36);
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
			this.rBtn_Meldungart_Erkundungsbericht.Size = new System.Drawing.Size(132, 20);
			this.rBtn_Meldungart_Erkundungsbericht.TabIndex = 2;
			this.rBtn_Meldungart_Erkundungsbericht.TabStop = true;
			this.rBtn_Meldungart_Erkundungsbericht.Text = "Erkundungsergebnis";
			this.rBtn_Meldungart_Erkundungsbericht.CheckedChanged += new System.EventHandler(this.rBtn_Meldungart_Erkundungsbericht_CheckedChanged);
			// 
			// lbl_Meldung_Kategorie
			// 
			this.lbl_Meldung_Kategorie.Location = new System.Drawing.Point(12, 104);
			this.lbl_Meldung_Kategorie.Name = "lbl_Meldung_Kategorie";
			this.lbl_Meldung_Kategorie.Size = new System.Drawing.Size(100, 15);
			this.lbl_Meldung_Kategorie.TabIndex = 18;
			this.lbl_Meldung_Kategorie.Text = "Kategorie";
			// 
			// cmb_Meldungskategorie
			// 
			this.cmb_Meldungskategorie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Meldungskategorie.Items.AddRange(new object[] {
																	   "",
																	   "Sofort",
																	   "Blitz",
																	   "Staatsnot"});
			this.cmb_Meldungskategorie.Location = new System.Drawing.Point(128, 100);
			this.cmb_Meldungskategorie.Name = "cmb_Meldungskategorie";
			this.cmb_Meldungskategorie.Size = new System.Drawing.Size(164, 21);
			this.cmb_Meldungskategorie.TabIndex = 0;
			// 
			// txt_Meldungstext
			// 
			this.txt_Meldungstext.Location = new System.Drawing.Point(8, 188);
			this.txt_Meldungstext.Name = "txt_Meldungstext";
			this.txt_Meldungstext.Size = new System.Drawing.Size(384, 228);
			this.txt_Meldungstext.TabIndex = 2;
			this.txt_Meldungstext.Text = "";
			// 
			// lbl_Meldung_Meldungstext
			// 
			this.lbl_Meldung_Meldungstext.Location = new System.Drawing.Point(4, 168);
			this.lbl_Meldung_Meldungstext.Name = "lbl_Meldung_Meldungstext";
			this.lbl_Meldung_Meldungstext.Size = new System.Drawing.Size(150, 15);
			this.lbl_Meldung_Meldungstext.TabIndex = 53;
			this.lbl_Meldung_Meldungstext.Text = "Meldungstext";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(416, 376);
			this.label1.Name = "label1";
			this.label1.TabIndex = 62;
			this.label1.Text = "interner Empänger";
			// 
			// cmb_Benutzerempfaenger
			// 
			this.cmb_Benutzerempfaenger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Benutzerempfaenger.Location = new System.Drawing.Point(412, 392);
			this.cmb_Benutzerempfaenger.Name = "cmb_Benutzerempfaenger";
			this.cmb_Benutzerempfaenger.Size = new System.Drawing.Size(196, 21);
			this.cmb_Benutzerempfaenger.TabIndex = 4;
			// 
			// gbx_Erkundung
			// 
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
			this.gbx_Erkundung.Controls.Add(this.gbx_);
			this.gbx_Erkundung.Location = new System.Drawing.Point(4, 164);
			this.gbx_Erkundung.Name = "gbx_Erkundung";
			this.gbx_Erkundung.Size = new System.Drawing.Size(390, 212);
			this.gbx_Erkundung.TabIndex = 1;
			this.gbx_Erkundung.TabStop = false;
			this.gbx_Erkundung.Visible = false;
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
			this.cbx_Strom.TabIndex = 27;
			this.cbx_Strom.Text = "Strom";
			// 
			// cbx_Wasser
			// 
			this.cbx_Wasser.Location = new System.Drawing.Point(8, 16);
			this.cbx_Wasser.Name = "cbx_Wasser";
			this.cbx_Wasser.Size = new System.Drawing.Size(67, 16);
			this.cbx_Wasser.TabIndex = 25;
			this.cbx_Wasser.Text = "Wasser";
			// 
			// cbx_Abwasser
			// 
			this.cbx_Abwasser.Location = new System.Drawing.Point(8, 32);
			this.cbx_Abwasser.Name = "cbx_Abwasser";
			this.cbx_Abwasser.Size = new System.Drawing.Size(76, 16);
			this.cbx_Abwasser.TabIndex = 26;
			this.cbx_Abwasser.Text = "Abwasser";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.txt_KellerInProzent);
			this.groupBox4.Controls.Add(this.cbx_KellerIstVorhanden);
			this.groupBox4.Controls.Add(this.label2);
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
			this.txt_KellerInProzent.TabIndex = 24;
			this.txt_KellerInProzent.Text = "0";
			// 
			// cbx_KellerIstVorhanden
			// 
			this.cbx_KellerIstVorhanden.Location = new System.Drawing.Point(8, 16);
			this.cbx_KellerIstVorhanden.Name = "cbx_KellerIstVorhanden";
			this.cbx_KellerIstVorhanden.Size = new System.Drawing.Size(92, 20);
			this.cbx_KellerIstVorhanden.TabIndex = 23;
			this.cbx_KellerIstVorhanden.Text = "ist vorhanden";
			this.cbx_KellerIstVorhanden.CheckedChanged += new System.EventHandler(this.cbx_KellerIstVorhanden_CheckedChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 10;
			this.label2.Text = "in Prozent";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.txt_Strasse);
			this.groupBox3.Controls.Add(this.txt_Ort);
			this.groupBox3.Controls.Add(this.txt_PLZ);
			this.groupBox3.Controls.Add(this.txt_HausNr);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.label6);
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
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(192, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(20, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "Nr.";
			// 
			// txt_Strasse
			// 
			this.txt_Strasse.Location = new System.Drawing.Point(44, 16);
			this.txt_Strasse.MaxLength = 50;
			this.txt_Strasse.Name = "txt_Strasse";
			this.txt_Strasse.Size = new System.Drawing.Size(140, 20);
			this.txt_Strasse.TabIndex = 16;
			this.txt_Strasse.Text = "";
			// 
			// txt_Ort
			// 
			this.txt_Ort.Location = new System.Drawing.Point(124, 36);
			this.txt_Ort.MaxLength = 50;
			this.txt_Ort.Name = "txt_Ort";
			this.txt_Ort.Size = new System.Drawing.Size(128, 20);
			this.txt_Ort.TabIndex = 19;
			this.txt_Ort.Text = "";
			// 
			// txt_PLZ
			// 
			this.txt_PLZ.Location = new System.Drawing.Point(44, 36);
			this.txt_PLZ.MaxLength = 5;
			this.txt_PLZ.Name = "txt_PLZ";
			this.txt_PLZ.Size = new System.Drawing.Size(40, 20);
			this.txt_PLZ.TabIndex = 18;
			this.txt_PLZ.Text = "";
			// 
			// txt_HausNr
			// 
			this.txt_HausNr.Location = new System.Drawing.Point(212, 16);
			this.txt_HausNr.MaxLength = 5;
			this.txt_HausNr.Name = "txt_HausNr";
			this.txt_HausNr.Size = new System.Drawing.Size(40, 20);
			this.txt_HausNr.TabIndex = 17;
			this.txt_HausNr.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(4, 20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(40, 16);
			this.label5.TabIndex = 7;
			this.label5.Text = "Straße";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(88, 40);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(36, 16);
			this.label6.TabIndex = 9;
			this.label6.Text = "Stadt";
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
			this.txt_Erkundungsobjekt.Size = new System.Drawing.Size(192, 20);
			this.txt_Erkundungsobjekt.TabIndex = 3;
			this.txt_Erkundungsobjekt.Text = "";
			// 
			// txt_Erkunder
			// 
			this.txt_Erkunder.Location = new System.Drawing.Point(60, 8);
			this.txt_Erkunder.MaxLength = 50;
			this.txt_Erkunder.Name = "txt_Erkunder";
			this.txt_Erkunder.Size = new System.Drawing.Size(108, 20);
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
			// gbx_
			// 
			this.gbx_.Controls.Add(this.cmb_Bauart);
			this.gbx_.Controls.Add(this.lbl_Heizung);
			this.gbx_.Controls.Add(this.lbl_Haustyp);
			this.gbx_.Controls.Add(this.lbl_Bauart);
			this.gbx_.Controls.Add(this.txt_Heizung);
			this.gbx_.Controls.Add(this.txt_Haustyp);
			this.gbx_.Location = new System.Drawing.Point(5, 135);
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
			this.cmb_Bauart.TabIndex = 21;
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
			this.txt_Heizung.TabIndex = 22;
			this.txt_Heizung.Text = "";
			// 
			// txt_Haustyp
			// 
			this.txt_Haustyp.Location = new System.Drawing.Point(64, 8);
			this.txt_Haustyp.MaxLength = 50;
			this.txt_Haustyp.Name = "txt_Haustyp";
			this.txt_Haustyp.Size = new System.Drawing.Size(204, 20);
			this.txt_Haustyp.TabIndex = 20;
			this.txt_Haustyp.Text = "";
			// 
			// usc_Meldung
			// 
			this.Controls.Add(this.gbx_Erkundung);
			this.Controls.Add(this.cmb_Benutzerempfaenger);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btn_Meldung_Drucken);
			this.Controls.Add(this.btn_Meldung_Zuruecksetzen);
			this.Controls.Add(this.btn_Meldung_Speichern);
			this.Controls.Add(this.gbx_Meldung_Meldungskontext);
			this.Controls.Add(this.txt_Meldungstext);
			this.Controls.Add(this.lbl_Meldung_Meldungstext);
			this.Controls.Add(this.tvw_Meldung_Empfaenger);
			this.Controls.Add(this.lbl_Meldung_Empfaenger);
			this.Location = new System.Drawing.Point(6, 32);
			this.Name = "usc_Meldung";
			this.Size = new System.Drawing.Size(624, 456);
			this.gbx_Meldung_Meldungskontext.ResumeLayout(false);
			this.gbx_MeldungAbfassenUndErfassenPlugskontainer.ResumeLayout(false);
			this.gbx_Ubermittlung.ResumeLayout(false);
			this.gbx_Abfassung.ResumeLayout(false);
			this.gbx_Meldungstyp.ResumeLayout(false);
			this.gbx_Erkundung.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.gbx_.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#endregion

		#region Setze- Methoden

		private void SetzeEvents()
		{
			this.cmb_Uebermittlungsart.SelectedIndexChanged += new EventHandler(FelderModifiziert);
			this.dtp_Uebermittlungsdatum.ValueChanged += new EventHandler(FelderModifiziert);
			this.cbx_IstUebermittelt.CheckedChanged += new EventHandler(FelderModifiziert);
			this.cbx_UebermittlungszeitpunktJetzt.CheckedChanged += new EventHandler(FelderModifiziert);
			this.txt_Meldung_AbsenderText.TextChanged += new EventHandler(FelderModifiziert);
			this.dtp_AbfassungsDatum.ValueChanged += new EventHandler(FelderModifiziert);
			this.cbx_AbfassungsdatumJetzt.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_Meldungskategorie.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Meldungstext.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_Benutzerempfaenger.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_Strom.CheckedChanged += new EventHandler(FelderModifiziert);
			this.cbx_Wasser.TextChanged += new EventHandler(FelderModifiziert);
			this.cbx_Abwasser.TextChanged += new EventHandler(FelderModifiziert);
			this.txt_KellerInProzent.TextChanged += new EventHandler(FelderModifiziert);
			this.txt_Strasse.TextChanged += new EventHandler(FelderModifiziert);
			this.txt_Ort.TextChanged += new EventHandler(FelderModifiziert);
			this.txt_PLZ.TextChanged += new EventHandler(FelderModifiziert);
			this.txt_HausNr.TextChanged += new EventHandler(FelderModifiziert);
			this.dtp_Erk_Datum.ValueChanged += new EventHandler(FelderModifiziert);
			this.cmb_Einsatzschwerpunkte.SelectedIndexChanged += new EventHandler(FelderModifiziert);
			this.txt_Erkundungsobjekt.TextChanged += new EventHandler(FelderModifiziert);
			this.txt_Heizung.TextChanged += new EventHandler(FelderModifiziert);
			this.txt_Haustyp.TextChanged += new EventHandler(FelderModifiziert);
			this.txt_Erkunder.TextChanged += new EventHandler(FelderModifiziert);
			this.cmb_Bauart.SelectedIndexChanged += new EventHandler(FelderModifiziert);
		}
		// Init alle Steuerelemente
		private void InitAlleSTE()
		{
			this.SetzeBenutzer();
			this.SetzeMeldungsKategorie();
			this.SetzeUebermittlungsart();
			this.SetzeBenutzerEmpfaenger();
			this.SetzeTreeViewMitteilungsEmpfaenger();
			// für Erkundungsergebnis
			this.SetzeBauart();
			this.SetzeESP();			
		}
		private void InitComboBox(ComboBox pinout_cmb)
		{
			if(pinout_cmb.Items.Count >0 )
				pinout_cmb.SelectedIndex = 0;
			else // TODO: Fehldermeldung
				pinout_cmb.Items.Add("Daten werden nicht geladen");
		
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
		// Falls beim Checken die Kraftdaten neu geladen werden, sollen die 
		// Häckchen, die der Benutzer gesetzt hatte, noch da sein.
		private void SetzeTreeViewMitteilungsEmpfaenger()
		{
			int[]	IDMengeINT;
			if(this.tvw_Meldung_Empfaenger.Nodes.Count != 0)				
			{
				ArrayList IDMenge =
					this.HoleAlleAusgewaehltenEmpfaengerIDs(this.tvw_Meldung_Empfaenger.Nodes);
				IDMengeINT = (int[]) IDMenge.ToArray(typeof(int));
			}
			else
				IDMengeINT = new int[0];

			// Setze alle Einträge
			this.UpdateTreeViewMitteilungsEmpfaenger(this.tvw_Meldung_Empfaenger);

			if(IDMengeINT.Length != 0)
			{
				this.SetzeAlleAusgewaehltenEmpfaenger(this.tvw_Meldung_Empfaenger.Nodes, IDMengeINT);
			}
		}
		

		private void SetzeBenutzer()
		{
			Cdv_Benutzer benuzter = this._stMAT.HoleAktuellenBenutzer();
			this.lbl_Meldung_BearbeiterText.Text = benuzter.Benutzername;
			this.lbl_Meldung_RolleText.Text = benuzter.Systemrolle.ToString();
			if(this._istMeldungAbfassen == true)
			{
				this.txt_Meldung_AbsenderText.Text = benuzter.Benutzername;
				this.txt_Meldung_AbsenderText.Enabled = false;
			}


		}
		private void SetzeBauart()
		{
			this.cmb_Bauart.Items.Clear();
			foreach(pELS.Tdv_Bauart b in Enum.GetValues(typeof(pELS.Tdv_Bauart)))
			{
				this.cmb_Bauart.Items.Add(b);
			}
			this.InitComboBox(cmb_Bauart);
		}
		private void SetzeMeldungsKategorie()
		{
			this.cmb_Meldungskategorie.Items.Clear();
			foreach(pELS.Tdv_MeldungsKategorie mk in 
				Enum.GetValues(typeof(pELS.Tdv_MeldungsKategorie)))
			{
				this.cmb_Meldungskategorie.Items.Add(mk);
			}
			this.InitComboBox(cmb_Meldungskategorie);
		}
		private void SetzeUebermittlungsart()
		{
			this.cmb_Uebermittlungsart.Items.Clear();
			foreach(pELS.Tdv_Uebermittlungsart ua in 
				Enum.GetValues(typeof(pELS.Tdv_Uebermittlungsart)))
			{
				this.cmb_Uebermittlungsart.Items.Add(ua);
			}
			this.InitComboBox(cmb_Uebermittlungsart);
		}
		private void SetzeBenutzerEmpfaenger()
		{
			Cdv_Benutzer[] benutzermenge = this._stMAT.AlleBenutzer;
			this.cmb_Benutzerempfaenger.Items.Clear();
			for(int i=0; i<benutzermenge.Length;i++)
			{
				this.cmb_Benutzerempfaenger.Items.Add(benutzermenge[i]);
			}
			// Der Nutzer darf dieses Combobox leer lassen	
		}
		private void SetzeESP()
		{
			this.cmb_Einsatzschwerpunkte.Items.Clear();
			// cbx_Einsatzschwerpunkt
			Cdv_Einsatzschwerpunkt[] espMenge = this._stMAT.AlleESP;
			// Diese SelectBox darf nicht ausgewählt werden
			this.cmb_Einsatzschwerpunkte.Items.Add("");
			foreach(Cdv_Einsatzschwerpunkt esp in espMenge)
			{
				this.cmb_Einsatzschwerpunkte.Items.Add(esp);
			}
			this.InitComboBox(cmb_Einsatzschwerpunkte);
		}
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
			else
			{
			}
		}
		public void Zuruecksetzen()
		{
			_b_FelderModifiziert = false;

			dtp_AbfassungsDatum.Value = DateTime.Now;
			dtp_Erk_Datum.Value =  DateTime.Now;
			dtp_Uebermittlungsdatum.Value =  DateTime.Now;

			this.InitComboBox(cmb_Meldungskategorie);
			this.InitComboBox(cmb_Uebermittlungsart);
			this.InitComboBox(this.cmb_Bauart);
			this.InitComboBox(this.cmb_Einsatzschwerpunkte);
			this.cmb_Benutzerempfaenger.SelectedIndex = -1;

			cbx_IstUebermittelt.Checked = true;
			cbx_AbfassungsdatumJetzt.Checked = false;
			cbx_UebermittlungszeitpunktJetzt.Checked = false;
			cbx_KellerIstVorhanden.Checked = false;
			cbx_Strom.Checked = false;
			cbx_Wasser.Checked = false;
			cbx_Abwasser.Checked = false;
				
			
			txt_Meldungstext.Text = "";
			txt_Heizung.Text = "";
			txt_Haustyp.Text = "";
			txt_Erkunder.Text = "";
			txt_Erkundungsobjekt.Text = "";
			txt_Strasse.Text = "";
			txt_HausNr.Text = "";
			txt_PLZ.Text = "";
			txt_Ort.Text = "";
			txt_KellerInProzent.Text = "";

			this.ZuruecksetzenTreeView(this.tvw_Meldung_Empfaenger.Nodes);

			txt_KellerInProzent.Enabled = false;

			if(this._istMeldungAbfassen == false)
				this.txt_Meldung_AbsenderText.Text = "";		
			// TODO: checkboxes im tvw löschen	
			
		}

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
			txt_Meldung_AbsenderText_Validated(null, null);
			tvw_Meldung_Empfaenger_Validated(null, null);
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
			return (txt_Meldung_AbsenderText.Text.Length > 0);
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void txt_Meldung_AbsenderText_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereAbsender())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(txt_Meldung_AbsenderText, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(txt_Meldung_AbsenderText, "Bitte geben Sie einen Absender ein");
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
			ArrayList Empfaenger = HoleAlleAusgewaehltenEmpfaengerIDs(tvw_Meldung_Empfaenger.Nodes);
			if (Empfaenger.Count > 0  || cmb_Benutzerempfaenger.SelectedIndex >= 0)
				return true;
			else return false;
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void tvw_Meldung_Empfaenger_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereMeldungsempfaenger())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(tvw_Meldung_Empfaenger, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(tvw_Meldung_Empfaenger, "Bitte bestimmen Sie die Auftragsempfänger");
			}
		}
		#endregion

		#endregion

		#region funktionalität
		private bool SpeichereMeldung()
		{
			// Datenaufnahme
			// der obere Teil
			Cdv_Benutzer aktBenutzer = this._stMAT.HoleAktuellenBenutzer();
//			Cdv_Erkundungsergebnis meldung = new Cdv_Erkundungsergebnis();
			Cdv_Meldung meldung = new Cdv_Meldung();
			meldung.BearbeiterBenutzerID = aktBenutzer.ID;
			if(this.cbx_AbfassungsdatumJetzt.Checked == true) meldung.Abfassungsdatum = DateTime.Now;
			else meldung.Abfassungsdatum = this.dtp_AbfassungsDatum.Value;
			meldung.Kategorie = (Tdv_MeldungsKategorie)Enum.Parse(typeof(Tdv_MeldungsKategorie), cmb_Meldungskategorie.SelectedItem.ToString(), true);
			meldung.Uebermittlungsart= (Tdv_Uebermittlungsart) Enum.Parse(typeof(Tdv_Uebermittlungsart), cmb_Uebermittlungsart.SelectedItem.ToString(), true);

			if((meldung.IstUebermittelt = this.cbx_IstUebermittelt.Checked)== true)
			{
				// = rBtn_Meldungart_Erkundungsbericht.Checked;
				if(this.cbx_UebermittlungszeitpunktJetzt.Checked == true) 
					meldung.Uebermittlungsdatum = DateTime.Now;
				else
					meldung.Uebermittlungsdatum = this.dtp_Uebermittlungsdatum.Value;
			}
			else
			{
				meldung.Uebermittlungsdatum = DateTimePicker.MaxDateTime;
			}
			// der untere Teil: Erkundungsergebnis
			meldung.Text = this.txt_Meldungstext.Text;
			// Treeview: EmpfängerKräfteMenge
			ArrayList IDMenge = 
				this.HoleAlleAusgewaehltenEmpfaengerIDs(this.tvw_Meldung_Empfaenger.Nodes);
			int[] IDMengeINT = (int[]) IDMenge.ToArray(typeof(int));
			meldung.EmpfaengerMengeKraftID = IDMengeINT;

			// 
			Cdv_Benutzer EmpfangerBenutzer = this.cmb_Benutzerempfaenger.SelectedItem as Cdv_Benutzer;
			if(EmpfangerBenutzer == null)
				meldung.IstInToDoListe= false;
			else
			{
				meldung.IstInToDoListe = true;
				meldung.EmpfaengerBenutzerID = EmpfangerBenutzer.ID;
			}
			
			meldung.Absender = this.txt_Meldung_AbsenderText.Text;

			if(this.rBtn_Meldungart_Erkundungsbericht.Checked == false)
			{
//				Cdv_Meldung freieMeldung = new Cdv_Meldung();
//				//freieMeldung
//				freieMeldung = meldung;
				this._stMAT.SpeichereMeldung(meldung);
//				_zuletztGespeicherteMitteilung = freieMeldung;
				_zuletztGespeicherteMitteilung = meldung;
				// FreieMeldung als zuletztgespeicherte Meldung speichern
				return true;
			}
			else
			{
				Cdv_Erkundungsergebnis neuesErkundungsergebnis = new Cdv_Erkundungsergebnis(
					meldung.Text,
					meldung.Absender,
					meldung.Uebermittlungsart,
					meldung.Kategorie,
					true,
					meldung.IstInToDoListe,
					meldung.BearbeiterBenutzerID,
					0,
					this.txt_Erkunder.Text);

				neuesErkundungsergebnis.Erkunder = this.txt_Erkunder.Text;
				Cdv_Einsatzschwerpunkt esp = this.cmb_Einsatzschwerpunkte.SelectedItem as Cdv_Einsatzschwerpunkt;
				if(esp == null)
					neuesErkundungsergebnis.EinsatzschwerpunkID = 0;
				else
					neuesErkundungsergebnis.EinsatzschwerpunkID = esp.ID;
				neuesErkundungsergebnis.Erkundungsobjekt.Bezeichnung = this.txt_Erkundungsobjekt.Text;
				neuesErkundungsergebnis.Erkundungsobjekt.Erkundungsdatum = this.dtp_Erk_Datum.Value;
				neuesErkundungsergebnis.Erkundungsobjekt.Anschrift.Strasse = this.txt_Strasse.Text;
				neuesErkundungsergebnis.Erkundungsobjekt.Anschrift.Hausnummer= this.txt_HausNr.Text;
				neuesErkundungsergebnis.Erkundungsobjekt.Anschrift.PLZ = this.txt_PLZ.Text;
				neuesErkundungsergebnis.Erkundungsobjekt.Anschrift.Ort = this.txt_Ort.Text;
				neuesErkundungsergebnis.Erkundungsobjekt.Haustyp = txt_Haustyp.Text;
				neuesErkundungsergebnis.Erkundungsobjekt.Bauart = (Tdv_Bauart)Enum.Parse(typeof(Tdv_Bauart),cmb_Bauart.SelectedItem.ToString(),true);
				
				// Abfangen des Fehlers, falls der Nutzer gar nichts eingibt.
				if((neuesErkundungsergebnis.Erkundungsobjekt.Keller.Vorhanden = cbx_KellerIstVorhanden.Checked)==true)
				{
					if(txt_KellerInProzent.Text.CompareTo("") == 0)
						neuesErkundungsergebnis.Erkundungsobjekt.Keller.Prozentsatz = 0;
					else
						neuesErkundungsergebnis.Erkundungsobjekt.Keller.Prozentsatz = int.Parse(txt_KellerInProzent.Text);
				}
				else
				{
					neuesErkundungsergebnis.Erkundungsobjekt.Keller.Prozentsatz = 0;
				}
				neuesErkundungsergebnis.Erkundungsobjekt.Heizung = txt_Heizung.Text;
				neuesErkundungsergebnis.Erkundungsobjekt.Elektroversorgung =cbx_Strom.Checked;
				neuesErkundungsergebnis.Erkundungsobjekt.Wasserversorgung = cbx_Wasser.Checked;
				neuesErkundungsergebnis.Erkundungsobjekt.Abwasserentsorgung = this.cbx_Abwasser.Checked;
				this._stMAT.SpeichereErkundungsergebnis(neuesErkundungsergebnis);
				// neuesErkundungsergebnis als zuletztgespeicherte neuesErkundungsergebnis speichern
				_zuletztGespeicherteMitteilung = neuesErkundungsergebnis;
				return true;
			}
			
			
		}
		private GroupBox LadeErkundungsergebnis()
		{
			//this.SetzeBauart();
			return this.gbx_Erkundung;
		}
	
		

		#endregion

		#region treeview
	
		private void UpdateTreeViewMitteilungsEmpfaenger(TreeView pin_TreeView)
		{
			pin_TreeView.BeginUpdate();
			pin_TreeView.Nodes.Clear();
			// alle mögliche Kräftetypen
			string[] str_typmenge = new string[3];
			str_typmenge[0] = "Einheiten";
			str_typmenge[1] = "Helfer";
			str_typmenge[2] = "KFZ";
			
			// Knoten auf der obersten Hierachieebene erstellen
			for(int i=0; i<str_typmenge.Length; i++)
			{
				pin_TreeView.Nodes.Add(str_typmenge[i]);
			}
			// Knoten unter dem Oberknoten "Einheit"
			foreach(Cdv_Einheit Einheit in this._stMAT.AlleEinheiten)
			{
				TreeNode neuerKnoten = new TreeNode();
				neuerKnoten.Text = Einheit.ToString();
				neuerKnoten.Tag = Einheit;
				pin_TreeView.Nodes[0].Nodes.Add(neuerKnoten);
			}

			foreach(Cdv_Helfer Helfer in this._stMAT.AlleHelfer)
			{
				TreeNode neuerKnoten = new TreeNode();
				neuerKnoten.Text = Helfer.ToString();
				neuerKnoten.Tag = Helfer;
				pin_TreeView.Nodes[1].Nodes.Add(neuerKnoten);
			}
			pin_TreeView.EndUpdate();

			foreach(Cdv_KFZ KFZ in this._stMAT.AlleKFZ)
			{
				TreeNode neuerKnoten = new TreeNode();
				neuerKnoten.Text = KFZ.ToString();
				neuerKnoten.Tag = KFZ;
				pin_TreeView.Nodes[2].Nodes.Add(neuerKnoten);
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

		#region event handler
		// event, welches bei allen Eingabeelementen registriert ist
		// und eine vorgenommene Änderung registriert
		private void FelderModifiziert(object sender, System.EventArgs e)
		{
			_b_FelderModifiziert = true;
		}

		private void btn_Meldung_Speichern_Click(object sender, System.EventArgs e)
		{
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
		private void btn_Meldung_Zuruecksetzen_Click(object sender, System.EventArgs e)
		{
				ZuruecksetzenMitRueckfrage();			
		}


		private void btn_Meldung_DatumJetzt_Click(object sender, System.EventArgs e)
		{
//			this.dtp_AbfassungsDatum.Value = DateTime.Now;
		}


		private void rBtn_Meldungart_Meldung_CheckedChanged(object sender, System.EventArgs e)
		{
			if (rBtn_Meldungart_Meldung.Checked)
			{
				// Erkundung
				gbx_Erkundung.Hide();
				// Textfeld
				lbl_Meldung_Meldungstext.Text = "Meldungstext";
				this.lbl_Meldung_Meldungstext.Location = new System.Drawing.Point(4, 168);
				this.txt_Meldungstext.Location = new System.Drawing.Point(4, 188);
				this.txt_Meldungstext.Size = new System.Drawing.Size(390, 200);
			}
		}

		private void rBtn_Meldungart_Erkundungsbericht_CheckedChanged(object sender, System.EventArgs e)
		{
			if (rBtn_Meldungart_Erkundungsbericht.Checked)
			{
				//Erkundung
				this.LadeErkundungsergebnis().Show();
				this.SetzeTabOrdnung("Erkundungsbericht");
				//TODO: an neues Layout anpassen
				// Label Textfeld
				this.lbl_Meldung_Meldungstext.Text = "Meldungstext";
				this.lbl_Meldung_Meldungstext.Location = new System.Drawing.Point(4, 360);
				// Textfeld
				this.txt_Meldungstext.Location = new System.Drawing.Point(8, 380);
				this.txt_Meldungstext.Size = new System.Drawing.Size(384, 40);
			}
		}
		/// <summary>
		/// modifiziert die Reihenfolge der TabOrdnung in Abhängigkeit der
		/// ausgewählten Meldungsart
		/// </summary>
		/// <param name="pin_Modus"></param>
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
			//Anschließend das ChangeEvent auslösen:
			this.FelderModifiziert(sender, e);
		}

		private void cbx_KellerIstVorhanden_CheckedChanged(object sender, System.EventArgs e)
		{
			this.txt_KellerInProzent.Enabled = cbx_KellerIstVorhanden.Checked;
		}
		private void cbx_IstUebermittelt_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.cbx_IstUebermittelt.Checked == true)
			{
				this.cbx_UebermittlungszeitpunktJetzt.Enabled = true;
				this.dtp_Uebermittlungsdatum.Enabled = true;
			}
			else
			{
				this.cbx_UebermittlungszeitpunktJetzt.Enabled = false;
				this.dtp_Uebermittlungsdatum.Enabled = false;
			}
		}
		/// <summary>
		/// Speichert eine Meldung und feuert dann das Event, um nach Report zu wechseln
		/// und die Meldung drucken zu können
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Meldung_Drucken_Click(object sender, System.EventArgs e)
		{
			if (Eingabevalidierung())
			{
				SpeichereMeldung();
				Zuruecksetzen();	
				_stMAT.FeuereReportRequestedEvent(_zuletztGespeicherteMitteilung);
			}
			else
			{
				pELS.GUI.PopUp.CPopUp.MeldenVonFalscherEingabe();
			}
		}
	

		#endregion
	
		#region get-, set- Methoden
		public bool b_FelderModifiziert
		{
			get { return _b_FelderModifiziert;}
			set { _b_FelderModifiziert = value;}
		}
	
		#endregion

		#region dynamische Daten-Akualisierung
		/// <summary>
		/// Wenn die Menge aller Einsatzschwerpunkte verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AkualisiereESP()
		{
			this.SetzeESP();
		}
		/// <summary>
		/// Wenn die Menge aller Benutzer verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>		
		public void AktualisiereBenutzer()
		{
			this.SetzeBenutzerEmpfaenger();
		}
		/// <summary>
		/// Wenn die Menge aller Einheiten, Helfer, oder Kfz verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AktualisiereEHK()
		{
			this.SetzeTreeViewMitteilungsEmpfaenger();	
		}
		
		/// <summary>
		/// Wenn die Systemrolle des aktuellen Benutzers verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AkualisiereAktBenutzer()
		{
			this.lbl_Meldung_BearbeiterText .Text = this._stMAT.HoleAktuellenBenutzer().Benutzername;
			this.lbl_Meldung_RolleText.Text = this._stMAT.HoleAktuellenBenutzer().Systemrolle.ToString();
			if(this._istMeldungAbfassen == true)
				this.txt_Meldung_AbsenderText.Text = this._stMAT.HoleAktuellenBenutzer().Benutzername;
		}
		#endregion

			
	}
}
