using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using pELS.Client;

namespace pELS.Client.MAT
{
	// benötigt für: PopUp
	using pELS.GUI.PopUp;
	// benötigt für: pELS-Objekte
	using pELS.DV;



	// benötigt für: ChannelServices
	using System.Runtime.Remoting.Channels;
	// benötigt für: TcpChannel
//	using System.Runtime.Remoting.Channels.Tcp;
	// benötigt für: TypeFilterLevel
	using System.Runtime.Serialization.Formatters;




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

		public System.Windows.Forms.DateTimePicker dtp_Auftrag_spaetesterErfuellungszeitpunkt;
		public System.Windows.Forms.CheckBox cbx_Auftrag_nachverfolgen;
		public System.Windows.Forms.GroupBox gbx_Auftrag_Auftragskontext;
		public System.Windows.Forms.Label lbl_Auftrag_Auftragskontext_Uebermittlungsart;
		public System.Windows.Forms.Label lbl_Auftrag_Auftragskontext_Datum;
		public System.Windows.Forms.Button btn_Auftrag_AuftragZuruecksetzen;
		public System.Windows.Forms.Button btn_Auftrag_AuftragSpeichern;
		public System.Windows.Forms.Label lbl_Auftrag_Auftragstext;
		public System.Windows.Forms.RichTextBox txt_Auftrag_Auftragstext;
		public System.Windows.Forms.TreeView tvw_Auftrag_AuftragsEmpfaenger;
		public System.Windows.Forms.Label lbl_Auftrag_AuftragsEmpfaenger;
		public System.Windows.Forms.CheckBox cbx_IstUebermittelt;
		public System.Windows.Forms.Button btn_Auftrag_AuftragDrucken;
		public System.ComponentModel.Container components = null;
		public System.Windows.Forms.ComboBox cmb_Benutzerempfaenger;
		public System.Windows.Forms.Label label1;
		public System.Windows.Forms.DateTimePicker dtp_Uebermittlungsdatum;
		public System.Windows.Forms.ComboBox cmb_Befehlsart;
		public System.Windows.Forms.Label _lbl_Befehl;
		public System.Windows.Forms.Label lbl_UMZeitpunkt;
		public System.Windows.Forms.GroupBox gbx_Ubermittlung;
		public System.Windows.Forms.CheckBox cbx_spaetesterErfuellungszeitpunktJetzt;
		public System.Windows.Forms.CheckBox cbx_AbfassungsdatumJetzt;
		public System.Windows.Forms.DateTimePicker dtp_AbfassungsDatum;
		public System.Windows.Forms.Label lbl_BearbeiterName;
		public System.Windows.Forms.Label lbl_BearbeiterRolle;
		public System.Windows.Forms.Label lbl_TEXT_Bearbeiter;
		public System.Windows.Forms.Label lbl_TEXT_Rolle;
		public System.Windows.Forms.Label lbl_TEXT_Absender;
		public System.Windows.Forms.TextBox txt_Absender;
		public System.Windows.Forms.Label lbl_spaetester_Ausfuehrungszeitpunkt;
		public System.Windows.Forms.DateTimePicker dtp_Ausfuehrungszeitpunkt;
		public System.Windows.Forms.Label lbl_Ausfuehrungszeitpunkt;
		public System.Windows.Forms.CheckBox cbx_UebermittlungszeitpunktJetzt;
		public System.Windows.Forms.CheckBox cbx_AusfuehrungszeitpunktJetzt;
		public System.Windows.Forms.ComboBox cmb_UebermittlungsArt;
		public System.Windows.Forms.GroupBox gbx_Abfassung;
		public System.Windows.Forms.GroupBox gbx_Ausfuehrung;

		#endregion

		#region Variablen
		/// <summary>
		/// zur Übergabe an das Event ReportRequested, um die Mitteilung drucken zu können
		/// </summary>
		private Cdv_Mitteilung _zuletztGespeicherteMitteilung;
		/// <summary>
		/// ermöglicht das Anzeigen von fehlerhaften Eingaben
		/// </summary>
		protected System.Windows.Forms.ErrorProvider ep_Eingabe;
		/// <summary>
		/// gibt an, ob bereits Eingaben geschehen sind
		/// </summary>
		protected bool _b_FelderModifiziert = false;
		protected System.Windows.Forms.Label lbl_TEXT_laufendeNummer;
		
	
		public bool b_FelderModifiziert
		{
			get { return _b_FelderModifiziert;}
			set { _b_FelderModifiziert = value;}
		}
		/// <summary>
		/// Referenz auf das entsprechende Element der Steuerungsschicht
		/// </summary>
		private Cst_MAT _stMAT;

		#endregion		


		#region Konstruktor & Destruktor
		public usc_Auftrag(Cst_MAT pin_stMAT)
		{

			#region hütte
			try
			{
//				System.Runtime.Remoting.RemotingConfiguration.Configure(@"C:\pels\Code\pELS\ClientLogik\bin\Debug\SBE\CsbeMAT.dll.config");

//				// Creating a custom formatter for a TcpChannel sink chain.
//				BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
//				provider.TypeFilterLevel = TypeFilterLevel.Full;
//				// Creating the IDictionary to set the port on the channel instance.
//				IDictionary props = new Hashtable();
//				props["name"] = "tcppELSClient2";
//				props["port"] = 1024;
//				// Pass the properties for the port setting and the server provider in the server chain argument. (Client remains null here.)
//				_pout_neuer_Kanal = new System.Runtime.Remoting.Channels.Http.HttpChannel(props, null, provider);
//				ChannelServices.RegisterChannel(_pout_neuer_Kanal);
			}
			catch(Exception e)
			{
				int i = 0;
			}
			
			
			#endregion
			
			
			this._stMAT = pin_stMAT;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			this.SetzeEvents();

			this.InitAlleSTE();
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
			this.dtp_Auftrag_spaetesterErfuellungszeitpunkt = new System.Windows.Forms.DateTimePicker();
			this.cbx_spaetesterErfuellungszeitpunktJetzt = new System.Windows.Forms.CheckBox();
			this.cmb_Befehlsart = new System.Windows.Forms.ComboBox();
			this.cbx_Auftrag_nachverfolgen = new System.Windows.Forms.CheckBox();
			this.gbx_Auftrag_Auftragskontext = new System.Windows.Forms.GroupBox();
			this.lbl_TEXT_laufendeNummer = new System.Windows.Forms.Label();
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
			this.lbl_Auftrag_Auftragskontext_Uebermittlungsart = new System.Windows.Forms.Label();
			this.cbx_UebermittlungszeitpunktJetzt = new System.Windows.Forms.CheckBox();
			this.btn_Auftrag_AuftragZuruecksetzen = new System.Windows.Forms.Button();
			this.btn_Auftrag_AuftragSpeichern = new System.Windows.Forms.Button();
			this.lbl_Auftrag_Auftragstext = new System.Windows.Forms.Label();
			this.txt_Auftrag_Auftragstext = new System.Windows.Forms.RichTextBox();
			this.tvw_Auftrag_AuftragsEmpfaenger = new System.Windows.Forms.TreeView();
			this.lbl_Auftrag_AuftragsEmpfaenger = new System.Windows.Forms.Label();
			this.cbx_IstUebermittelt = new System.Windows.Forms.CheckBox();
			this.btn_Auftrag_AuftragDrucken = new System.Windows.Forms.Button();
			this.cmb_Benutzerempfaenger = new System.Windows.Forms.ComboBox();
			this.ep_Eingabe = new System.Windows.Forms.ErrorProvider();
			this.label1 = new System.Windows.Forms.Label();
			this.cbx_IstUebermittelt = new System.Windows.Forms.CheckBox();
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
			this.dtp_Auftrag_spaetesterErfuellungszeitpunkt.TabIndex = 3;
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
			// 
			// cbx_Auftrag_nachverfolgen
			// 
			this.cbx_Auftrag_nachverfolgen.Location = new System.Drawing.Point(8, 424);
			this.cbx_Auftrag_nachverfolgen.Name = "cbx_Auftrag_nachverfolgen";
			this.cbx_Auftrag_nachverfolgen.Size = new System.Drawing.Size(100, 20);
			this.cbx_Auftrag_nachverfolgen.TabIndex = 2;
			this.cbx_Auftrag_nachverfolgen.Text = "nachverfolgen";
			// 
			// gbx_Auftrag_Auftragskontext
			// 
			this.gbx_Auftrag_Auftragskontext.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.lbl_TEXT_laufendeNummer);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.gbx_Abfassung);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.gbx_Ausfuehrung);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this._lbl_Befehl);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.lbl_BearbeiterName);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.lbl_BearbeiterRolle);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.lbl_TEXT_Bearbeiter);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.lbl_TEXT_Rolle);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.cmb_Befehlsart);
			this.gbx_Auftrag_Auftragskontext.Controls.Add(this.gbx_Ubermittlung);
			this.gbx_Auftrag_Auftragskontext.Location = new System.Drawing.Point(0, 0);
			this.gbx_Auftrag_Auftragskontext.Name = "gbx_Auftrag_Auftragskontext";
			this.gbx_Auftrag_Auftragskontext.Size = new System.Drawing.Size(620, 164);
			this.gbx_Auftrag_Auftragskontext.TabIndex = 0;
			this.gbx_Auftrag_Auftragskontext.TabStop = false;
			this.gbx_Auftrag_Auftragskontext.Text = "Auftragskontext";
			// 
			// lbl_TEXT_laufendeNummer
			// 
			this.lbl_TEXT_laufendeNummer.Location = new System.Drawing.Point(15, 55);
			this.lbl_TEXT_laufendeNummer.Name = "lbl_TEXT_laufendeNummer";
			this.lbl_TEXT_laufendeNummer.Size = new System.Drawing.Size(75, 20);
			this.lbl_TEXT_laufendeNummer.TabIndex = 80;
			this.lbl_TEXT_laufendeNummer.Text = "lfd. Nummer";
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
			this.gbx_Abfassung.Size = new System.Drawing.Size(292, 64);
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
			this.cbx_AbfassungsdatumJetzt.CheckedChanged += new System.EventHandler(this.cbx_AbfassungsdatumJetzt_CheckedChanged);
			// 
			// txt_Absender
			// 
			this.txt_Absender.Location = new System.Drawing.Point(120, 12);
			this.txt_Absender.Name = "txt_Absender";
			this.txt_Absender.Size = new System.Drawing.Size(152, 20);
			this.txt_Absender.TabIndex = 0;
			this.txt_Absender.Text = "";
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
			this.cbx_AusfuehrungszeitpunktJetzt.CheckedChanged += new System.EventHandler(this.cbx_AusfuehrungszeitpunktJetzt_CheckedChanged);
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
			this.lbl_BearbeiterName.BackColor = System.Drawing.Color.White;
			this.lbl_BearbeiterName.Location = new System.Drawing.Point(128, 32);
			this.lbl_BearbeiterName.Name = "lbl_BearbeiterName";
			this.lbl_BearbeiterName.Size = new System.Drawing.Size(168, 15);
			this.lbl_BearbeiterName.TabIndex = 24;
			// 
			// lbl_BearbeiterRolle
			// 
			this.lbl_BearbeiterRolle.BackColor = System.Drawing.Color.White;
			this.lbl_BearbeiterRolle.Location = new System.Drawing.Point(128, 16);
			this.lbl_BearbeiterRolle.Name = "lbl_BearbeiterRolle";
			this.lbl_BearbeiterRolle.Size = new System.Drawing.Size(168, 15);
			this.lbl_BearbeiterRolle.TabIndex = 23;
			// 
			// lbl_TEXT_Bearbeiter
			// 
			this.lbl_TEXT_Bearbeiter.BackColor = System.Drawing.Color.White;
			this.lbl_TEXT_Bearbeiter.Location = new System.Drawing.Point(12, 32);
			this.lbl_TEXT_Bearbeiter.Name = "lbl_TEXT_Bearbeiter";
			this.lbl_TEXT_Bearbeiter.Size = new System.Drawing.Size(56, 15);
			this.lbl_TEXT_Bearbeiter.TabIndex = 16;
			this.lbl_TEXT_Bearbeiter.Text = "Bearbeiter";
			// 
			// lbl_TEXT_Rolle
			// 
			this.lbl_TEXT_Rolle.BackColor = System.Drawing.Color.White;
			this.lbl_TEXT_Rolle.Location = new System.Drawing.Point(12, 16);
			this.lbl_TEXT_Rolle.Name = "lbl_TEXT_Rolle";
			this.lbl_TEXT_Rolle.Size = new System.Drawing.Size(64, 15);
			this.lbl_TEXT_Rolle.TabIndex = 15;
			this.lbl_TEXT_Rolle.Text = "Rolle";
			// 
			// gbx_Ubermittlung
			// 
			this.gbx_Ubermittlung.Controls.Add(this.cbx_IstUebermittelt);
			this.gbx_Ubermittlung.Controls.Add(this.cmb_UebermittlungsArt);
			this.gbx_Ubermittlung.Controls.Add(this.dtp_Uebermittlungsdatum);
			this.gbx_Ubermittlung.Controls.Add(this.lbl_UMZeitpunkt);
			this.gbx_Ubermittlung.Controls.Add(this.lbl_Auftrag_Auftragskontext_Uebermittlungsart);
			this.gbx_Ubermittlung.Controls.Add(this.cbx_UebermittlungszeitpunktJetzt);
			this.gbx_Ubermittlung.Location = new System.Drawing.Point(324, 72);
			this.gbx_Ubermittlung.Name = "gbx_Ubermittlung";
			this.gbx_Ubermittlung.Size = new System.Drawing.Size(292, 84);
			this.gbx_Ubermittlung.TabIndex = 3;
			this.gbx_Ubermittlung.TabStop = false;
			// 
			// cmb_UebermittlungsArt
			// 
			this.cmb_UebermittlungsArt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_UebermittlungsArt.Location = new System.Drawing.Point(124, 12);
			this.cmb_UebermittlungsArt.Name = "cmb_UebermittlungsArt";
			this.cmb_UebermittlungsArt.Size = new System.Drawing.Size(148, 21);
			this.cmb_UebermittlungsArt.TabIndex = 0;
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
			// 
			// btn_Auftrag_AuftragZuruecksetzen
			// 
			this.btn_Auftrag_AuftragZuruecksetzen.Location = new System.Drawing.Point(448, 424);
			this.btn_Auftrag_AuftragZuruecksetzen.Name = "btn_Auftrag_AuftragZuruecksetzen";
			this.btn_Auftrag_AuftragZuruecksetzen.Size = new System.Drawing.Size(81, 25);
			this.btn_Auftrag_AuftragZuruecksetzen.TabIndex = 6;
			this.btn_Auftrag_AuftragZuruecksetzen.Text = "&Zurücksetzen";
			this.btn_Auftrag_AuftragZuruecksetzen.Click += new System.EventHandler(this.btn_Auftrag_AuftragZuruecksetzen_Click);
			// 
			// btn_Auftrag_AuftragSpeichern
			// 
			this.btn_Auftrag_AuftragSpeichern.Location = new System.Drawing.Point(536, 424);
			this.btn_Auftrag_AuftragSpeichern.Name = "btn_Auftrag_AuftragSpeichern";
			this.btn_Auftrag_AuftragSpeichern.Size = new System.Drawing.Size(80, 25);
			this.btn_Auftrag_AuftragSpeichern.TabIndex = 5;
			this.btn_Auftrag_AuftragSpeichern.Text = "&Speichern";
			this.btn_Auftrag_AuftragSpeichern.Click += new System.EventHandler(this.btn_Auftrag_AuftragSpeichern_Click);
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
			this.txt_Auftrag_Auftragstext.Location = new System.Drawing.Point(0, 196);
			this.txt_Auftrag_Auftragstext.Name = "txt_Auftrag_Auftragstext";
			this.txt_Auftrag_Auftragstext.Size = new System.Drawing.Size(395, 216);
			this.txt_Auftrag_Auftragstext.TabIndex = 1;
			this.txt_Auftrag_Auftragstext.Text = "";
			// 
			// tvw_Auftrag_AuftragsEmpfaenger
			// 
			this.tvw_Auftrag_AuftragsEmpfaenger.CheckBoxes = true;
			this.tvw_Auftrag_AuftragsEmpfaenger.ImageIndex = -1;
			this.tvw_Auftrag_AuftragsEmpfaenger.Location = new System.Drawing.Point(410, 196);
			this.tvw_Auftrag_AuftragsEmpfaenger.Name = "tvw_Auftrag_AuftragsEmpfaenger";
			this.tvw_Auftrag_AuftragsEmpfaenger.SelectedImageIndex = -1;
			this.tvw_Auftrag_AuftragsEmpfaenger.Size = new System.Drawing.Size(200, 172);
			this.tvw_Auftrag_AuftragsEmpfaenger.TabIndex = 3;
			// 
			// lbl_Auftrag_AuftragsEmpfaenger
			// 
			this.lbl_Auftrag_AuftragsEmpfaenger.Location = new System.Drawing.Point(420, 176);
			this.lbl_Auftrag_AuftragsEmpfaenger.Name = "lbl_Auftrag_AuftragsEmpfaenger";
			this.lbl_Auftrag_AuftragsEmpfaenger.Size = new System.Drawing.Size(128, 16);
			this.lbl_Auftrag_AuftragsEmpfaenger.TabIndex = 15;
			this.lbl_Auftrag_AuftragsEmpfaenger.Text = "Auftragsempfänger";
			// 
			// cbx_IstUebermittelt
			// 
			this.cbx_IstUebermittelt.Location = new System.Drawing.Point(0, 0);
			this.cbx_IstUebermittelt.Name = "cbx_IstUebermittelt";
			this.cbx_IstUebermittelt.TabIndex = 0;
			// 
			// btn_Auftrag_AuftragDrucken
			// 
			this.btn_Auftrag_AuftragDrucken.Location = new System.Drawing.Point(305, 424);
			this.btn_Auftrag_AuftragDrucken.Name = "btn_Auftrag_AuftragDrucken";
			this.btn_Auftrag_AuftragDrucken.Size = new System.Drawing.Size(135, 25);
			this.btn_Auftrag_AuftragDrucken.TabIndex = 7;
			this.btn_Auftrag_AuftragDrucken.Text = "Speichern && &Drucken";
			this.btn_Auftrag_AuftragDrucken.Click += new System.EventHandler(this.btn_Auftrag_AuftragDrucken_Click);
			// 
			// cmb_Benutzerempfaenger
			// 
			this.cmb_Benutzerempfaenger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Benutzerempfaenger.Location = new System.Drawing.Point(410, 392);
			this.cmb_Benutzerempfaenger.Name = "cmb_Benutzerempfaenger";
			this.cmb_Benutzerempfaenger.Size = new System.Drawing.Size(200, 21);
			this.cmb_Benutzerempfaenger.TabIndex = 4;
			// 
			// ep_Eingabe
			// 
			this.ep_Eingabe.ContainerControl = this;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(416, 376);
			this.label1.Name = "label1";
			this.label1.TabIndex = 64;
			this.label1.Text = "interner Empänger";
			// 
			// cbx_IstUebermittelt
			// 
			this.cbx_IstUebermittelt.Checked = true;
			this.cbx_IstUebermittelt.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbx_IstUebermittelt.Location = new System.Drawing.Point(10, 35);
			this.cbx_IstUebermittelt.Name = "cbx_IstUebermittelt";
			this.cbx_IstUebermittelt.Size = new System.Drawing.Size(104, 16);
			this.cbx_IstUebermittelt.TabIndex = 67;
			this.cbx_IstUebermittelt.Text = "ist Übermittelt";
			
			// 
			// usc_Auftrag
			// 
			this.Controls.Add(this.cmb_Benutzerempfaenger);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btn_Auftrag_AuftragDrucken);
			this.Controls.Add(this.gbx_Auftrag_Auftragskontext);
			this.Controls.Add(this.btn_Auftrag_AuftragZuruecksetzen);
			this.Controls.Add(this.btn_Auftrag_AuftragSpeichern);
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

		#region Eingabevalidierung
		/// <summary>
		/// überprüft alle zwingend benötigten Eingaben auf Korrektheit
		/// </summary>
		/// <returns></returns>
		virtual protected bool Eingabevalidierung()
		{
			// setze Validierungsanzeigen falls nötig
			txt_Absender_Validated(null, null);
			tvw_Auftrag_AuftragsEmpfaenger_Validated(null, null);
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
			return (this.cmb_Befehlsart.Text.Length > 0);
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
			return (cmb_UebermittlungsArt.Text.Length > 0);
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
				ep_Eingabe.SetError(cmb_UebermittlungsArt, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(cmb_UebermittlungsArt, "Bitte bestimmen Sie die Übermittlungsart");
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
			return (txt_Auftrag_Auftragstext.Text.Length > 0);
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
				ep_Eingabe.SetError(txt_Auftrag_Auftragstext, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(txt_Auftrag_Auftragstext, "Bitte geben Sie einen Text ein");
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
			ArrayList Empfaenger = HoleAlleAusgewaehltenEmpfaengerIDs(tvw_Auftrag_AuftragsEmpfaenger.Nodes);
			if (Empfaenger.Count > 0  || cmb_Benutzerempfaenger.SelectedIndex >= 0)
				return true;
			else return false;
		}

		/// <summary>
		/// Validierungseventhandler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void tvw_Auftrag_AuftragsEmpfaenger_Validated(object sender, System.EventArgs e)
		{
			if(ValidiereAuftragsempfaenger())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(tvw_Auftrag_AuftragsEmpfaenger, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(tvw_Auftrag_AuftragsEmpfaenger, "Bitte bestimmen Sie die Auftragsempfänger");
			}
		}
		#endregion

		#endregion



		#region treeview
		private void SetzeMitteilungsEmpfaengerKraftknoten()
		{
			
		}

		private void UpdateTreeViewMitteilungsEmpfaenger(TreeView pin_TreeView)
		{
			pin_TreeView.Nodes.Clear();
			// alle mögliche Kräftetypen
			// Init 1. Hierachie Nodes 
			pin_TreeView.Nodes.Add("Einheiten");
			pin_TreeView.Nodes.Add("Helfer")	;
			pin_TreeView.Nodes.Add("KFZ");
			
			// Lade alle Einheiten
			Cdv_Einheit[] einheitsmenge = this._stMAT.AlleEinheiten;		
			// Init 2. Hierachie Nodes[0] 
			foreach(Cdv_Einheit e in einheitsmenge)
			{
				TreeNode neuerKnoten = new TreeNode();
				neuerKnoten.Text = e.ToString();
				neuerKnoten.Tag = e;
				pin_TreeView.Nodes[0].Nodes.Add(neuerKnoten);
			}
			// Lade alle Helfer
			Cdv_Helfer[] helfermenge = this._stMAT.AlleHelfer;
			// Init 2.Hierarchie Nodes[1]
			foreach(Cdv_Helfer p in helfermenge)
			{
				TreeNode neuerKnoten = new TreeNode();
				neuerKnoten.Text = p.ToString();
				neuerKnoten.Tag = p;
				pin_TreeView.Nodes[1].Nodes.Add(neuerKnoten);			
			}

			// Lade alle KFZ
			Cdv_KFZ[] kfzmenge = this._stMAT.AlleKFZ;
			// Init 2. Hierachie Nodes[2] 
			foreach(Cdv_KFZ k in kfzmenge)
			{
				TreeNode neuerKnoten = new TreeNode();
				neuerKnoten.Text = k.ToString();
				neuerKnoten.Tag = k;
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

		#region SetzeXXX

		private void InitAlleSTE()
		{
			this.SetzeBenutzerEmpfaenger();
			SetzeBenutzer(this._stMAT.HoleAktuellenBenutzer());
			SetzeGUIElemente();
			SetzeBefehlsart();
			SetzeUebermittlungsart();
			SetzeTreeViewMitteilungsEmpfaenger();
			Zuruecksetzen();
		
		}
		private void SetzeEvents()
		{
			this.cmb_Befehlsart.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_Befehlsart.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_Benutzerempfaenger.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);	
			this.cmb_Benutzerempfaenger.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);	
			this.cmb_UebermittlungsArt.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);

			this.cbx_Auftrag_nachverfolgen.CheckedChanged += new System.EventHandler(this.FelderModifiziert);					
			this.cbx_AusfuehrungszeitpunktJetzt.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_Auftrag_nachverfolgen.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_AbfassungsdatumJetzt.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_IstUebermittelt.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_UebermittlungszeitpunktJetzt.CheckedChanged += new System.EventHandler(this.cbx_UebermittlungszeitpunktJetzt_CheckedChanged);
			this.cbx_IstUebermittelt.CheckedChanged += new System.EventHandler(this.FelderModifiziert);
			this.cbx_IstUebermittelt.CheckedChanged += new System.EventHandler(this.cbx_IstUebermittelt_CheckedChanged);
			
			this.txt_Auftrag_Auftragstext.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Absender.TextChanged += new System.EventHandler(this.FelderModifiziert);
		}

		// Falls beim Checken die Kraftdaten neu geladen werden, sollen die 
		// Häckchen, die der Benutzer gesetzt hatte, noch da sein.
		private void SetzeTreeViewMitteilungsEmpfaenger()
		{
			int[] IDMengeINT = new int[0];
			if(this.tvw_Auftrag_AuftragsEmpfaenger.Nodes.Count != 0)				
			{
				ArrayList arr_IDMenge = new ArrayList();
				arr_IDMenge = this.HoleAlleAusgewaehltenEmpfaengerIDs(this.tvw_Auftrag_AuftragsEmpfaenger.Nodes);
				IDMengeINT = new int[arr_IDMenge.Count];
				arr_IDMenge.CopyTo(IDMengeINT);
			}
			else
				IDMengeINT = new int[0];

			// Setze alle Einträge
			this.UpdateTreeViewMitteilungsEmpfaenger(this.tvw_Auftrag_AuftragsEmpfaenger);
			

			if(IDMengeINT.Length != 0)
			{
				this.SetzeAlleAusgewaehltenEmpfaenger(this.tvw_Auftrag_AuftragsEmpfaenger.Nodes, IDMengeINT);
			}
		}

		private void InitComboBox(ComboBox pinout_cmb)
		{
			if(pinout_cmb.Items.Count >0 )
				pinout_cmb.SelectedIndex = 0;
			else // TODO: Fehldermeldung
				pinout_cmb.Items.Add("Daten werden nicht geladen");
		
		}

		/// <summary>
		/// setzt alle möglichen Befehlsarten
		/// </summary>
		/// 
		private void SetzeBefehlsart()
		{
			this.cmb_Befehlsart.Items.Clear();
			this.cmb_Befehlsart.Items.Add("<<kein Befehl>>");
			this.cmb_Befehlsart.Text = "<<kein Befehl>>";
			//this.cmb_Befehlsart.Items.Add("Befehl");
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
			this.InitComboBox(cmb_UebermittlungsArt);
		}

		/// <summary>
		/// modifiziert Standardwerte von GUI-Elementen (z.B. Sichtbarkeit)
		/// </summary>
		virtual protected void SetzeGUIElemente()
		{
			this.txt_Auftrag_Auftragstext.ReadOnly = false;
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
		/// setzt alle Eingabefelder zurück in den Ausgangszustand
		/// </summary>
		virtual public void Zuruecksetzen()
		{
			_b_FelderModifiziert = false;
			txt_Auftrag_Auftragstext.Text = "";
			txt_Absender.Text = "";
			
			dtp_AbfassungsDatum.Value = DateTime.Now;
			dtp_Ausfuehrungszeitpunkt.Value = DateTime.Now;
			dtp_Uebermittlungsdatum.Value = DateTime.Now;
			dtp_Auftrag_spaetesterErfuellungszeitpunkt.Value = DateTime.Now;

			this.InitComboBox(cmb_UebermittlungsArt);
			this.InitComboBox(cmb_Befehlsart);
			cmb_Benutzerempfaenger.SelectedIndex = -1;
			
			cbx_Auftrag_nachverfolgen.Checked = false;
			cbx_spaetesterErfuellungszeitpunktJetzt.Checked = false;
			cbx_AbfassungsdatumJetzt.Checked = false;
			cbx_UebermittlungszeitpunktJetzt.Checked = false;
			cbx_AusfuehrungszeitpunktJetzt.Checked = false;
			cbx_IstUebermittelt.Checked = true;

			this.ZuruecksetzenTreeView(this.tvw_Auftrag_AuftragsEmpfaenger.Nodes);
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

		#region LadeAuftrag - Fkr. zum Laden eines Auftrags
		/// <summary>
		/// lädt einen Auftrag in das Formular
		/// </summary>
		/// <param name="pin_Meldung"></param>
		public void LadeAuftrag(Cdv_Auftrag pin_Auftrag)
		{
			this.dtp_AbfassungsDatum.Value = pin_Auftrag.Abfassungsdatum;
			this.txt_Absender.Text = pin_Auftrag.Absender;
			this.dtp_Auftrag_spaetesterErfuellungszeitpunkt.Value = pin_Auftrag.Ausfuehrungszeitpunkt;

			Cdv_Benutzer BenutzerEmpfaenger = _stMAT.HoleBenutzer(pin_Auftrag.EmpfaengerBenutzerID);
			if (BenutzerEmpfaenger != null)
				this.cmb_Benutzerempfaenger.Text = BenutzerEmpfaenger.Benutzername;
			else
				this.cmb_Benutzerempfaenger.Text = "";

			SetzeAlleAusgewaehltenEmpfaenger(
				this.tvw_Auftrag_AuftragsEmpfaenger.Nodes, pin_Auftrag.EmpfaengerMengeKraftID);
			this.tvw_Auftrag_AuftragsEmpfaenger.ExpandAll();
	
			// TODO
			//			this.cbx_Auftrag_Befehl.Checked = pin_Auftrag.IstBefehl;
			this.cbx_IstUebermittelt.Checked = pin_Auftrag.IstUebermittelt;
			this.dtp_Auftrag_spaetesterErfuellungszeitpunkt.Value = 
				pin_Auftrag.SpaetesterErfuellungszeitpunkt;
			this.txt_Auftrag_Auftragstext.Text = pin_Auftrag.Text;
			this.cmb_UebermittlungsArt.SelectedItem
				= pin_Auftrag.Uebermittlungsart;
			this.dtp_Uebermittlungsdatum.Value = pin_Auftrag.Uebermittlungsdatum;
			this.cbx_Auftrag_nachverfolgen.Checked = pin_Auftrag.WirdNachverfolgt;
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
			else
			{
			}
		}

		#endregion

		#region SpeichereAuftrag - Fkt. zum Speichern eines Auftrags
		/// <summary>
		/// stellt einen Auftrag aus den Werten der Eingabeelemente zusammen 
		/// und speichert ihn
		/// </summary>
		private bool SpeichereAuftrag()
		{
			// erstelle einen neuen Auftrag
			Cdv_Erkundungsbefehl neuerAuftrag = new Cdv_Erkundungsbefehl();
			neuerAuftrag.ID = 0;
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
				neuerAuftrag.SpaetesterErfuellungszeitpunkt= this.dtp_Auftrag_spaetesterErfuellungszeitpunkt.Value;
			// Bearbeiter
			neuerAuftrag.BearbeiterBenutzerID = this._stMAT.HoleAktuellenBenutzer().ID;
			// Benutzerempfänger
			if (cmb_Benutzerempfaenger.SelectedIndex != -1)
			{
				neuerAuftrag.EmpfaengerBenutzerID = 
					((Cdv_Benutzer) this._stMAT.AlleBenutzer[cmb_Benutzerempfaenger.SelectedIndex]).ID;
				neuerAuftrag.IstInToDoListe = true;
			}
			// EmpfängerKräfteMenge
			ArrayList IDMenge = 
				this.HoleAlleAusgewaehltenEmpfaengerIDs(this.tvw_Auftrag_AuftragsEmpfaenger.Nodes);
			int[] IDMengeINT = (int[]) IDMenge.ToArray(typeof(int));
			neuerAuftrag.EmpfaengerMengeKraftID = IDMengeINT;
			// Übermittlungsart
			neuerAuftrag.Uebermittlungsart = 
				(pELS.Tdv_Uebermittlungsart) this.cmb_UebermittlungsArt.SelectedItem;
			//Übermittlung
			
			// Übermittlungsdatum: wähle JETZT oder das angebene Datum
			if((neuerAuftrag.IstUebermittelt = this.cbx_IstUebermittelt.Checked)== true)
			{
				if (this.cbx_UebermittlungszeitpunktJetzt.Checked)
					neuerAuftrag.Uebermittlungsdatum = DateTime.Now;
				else
					neuerAuftrag.Uebermittlungsdatum = this.dtp_Uebermittlungsdatum.Value;
			}
			else
			{
				neuerAuftrag.Uebermittlungsdatum = DateTimePicker.MaxDateTime;
			}
			// Text
			neuerAuftrag.Text = this.txt_Auftrag_Auftragstext.Text;
			// Nachverfolgen
			neuerAuftrag.WirdNachverfolgt = this.cbx_Auftrag_nachverfolgen.Checked;
			// ermittle, ob es sich um einen Befehl handelt			
			if ((this.cmb_Befehlsart.SelectedIndex != 0) && (this.cmb_Befehlsart.SelectedIndex != -1))
			{
				neuerAuftrag.IstBefehl = true;
				// ermittle, ob es sich um einen Erkundungsbefehl handelt
				Tdv_BefehlArt aktuelleEBArt = new Tdv_BefehlArt();
				aktuelleEBArt = PruefeErkundungsbefehl();
				if (Enum.IsDefined(typeof(Tdv_BefehlArt),aktuelleEBArt))
				{
					neuerAuftrag.BefehlsArt = aktuelleEBArt;
					this._stMAT.SpeichereErkundungsbefehl(neuerAuftrag);
				}
			}
			else 
			{
				neuerAuftrag.IstBefehl = false;
				this._stMAT.SpeichereAuftrag(neuerAuftrag);
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
		/// löst das Zurücksetzen der Eingabemaske aus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Auftrag_AuftragZuruecksetzen_Click(object sender, System.EventArgs e)
		{
			ZuruecksetzenMitRueckfrage();		
		}

				
		/// <summary>
		/// deaktiviert die 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cbx_IstUebermittelt_CheckedChanged(object sender, System.EventArgs e)
		{
			if ((sender as CheckBox).Checked == true)
			{
				//auf aktuelle Zeit zurück setzen
				dtp_Uebermittlungsdatum.Value = DateTime.Now;
				// DateTimePicker ausgrauen
				cbx_UebermittlungszeitpunktJetzt.Enabled = true;
				dtp_Uebermittlungsdatum.Enabled = true;
			}
			else
			{
				cbx_UebermittlungszeitpunktJetzt.Enabled = false;
				//DateTimePicker wieder aktivieren
				dtp_Uebermittlungsdatum.Enabled = false;
			}
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




		private void cbx_AusfuehrungszeitpunktJetzt_CheckedChanged(object sender, System.EventArgs e)
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
		/// <summary>
		/// Speichert einen Auftrag und feuert dann das Event, um nach Report zu wechseln
		/// und den Auftrag drucken zu können
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_Auftrag_AuftragDrucken_Click(object sender, System.EventArgs e)
		{			
			if (Eingabevalidierung())
			{
				// Auftrag erst speichern und die Felder zurücksetzen
				SpeichereAuftrag();
				Zuruecksetzen();
				// Event feuern lassen
				_stMAT.FeuereReportRequestedEvent(_zuletztGespeicherteMitteilung);
			}
			else
			{
				pELS.GUI.PopUp.CPopUp.MeldenVonFalscherEingabe();
			}			
		}

		#endregion

		#region dynamische Daten-Akualisierung
		/// <summary>
		/// Wenn die Menge aller Einheiten, Helfer, oder Kfz verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AktualisiereEHK()
		{
			this.SetzeTreeViewMitteilungsEmpfaenger();	
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
		/// Wenn die Systemrolle des aktuellen Benutzers verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AkualisiereAktBenutzer()
		{
			this.lbl_BearbeiterName.Text = this._stMAT.HoleAktuellenBenutzer().Benutzername;
			this.lbl_BearbeiterRolle.Text = this._stMAT.HoleAktuellenBenutzer().Systemrolle.ToString();
		}
		#endregion

		



		

	}
}