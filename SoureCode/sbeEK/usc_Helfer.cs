using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pELS.Client.EK
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
	public class usc_Helfer : System.Windows.Forms.UserControl
	{
		#region eigene Variablen
		/// <summary>
		/// ermöglicht das Anzeigen von fehlerhaften Eingaben
		/// </summary>
		protected System.Windows.Forms.ErrorProvider ep_Eingabe = new System.Windows.Forms.ErrorProvider();

		/// <summary>
		/// gibt an, ob bereits Eingaben geschehen sind
		/// </summary>
		protected bool _b_FelderModifiziert = false;

		/// <summary>
		/// Datagrid Personen
		/// </summary>
		private usc_dtg_Personen _usc_dtg_Personen;
	
		private Cst_EK _stEK;

		private bool _bIstNeuerHelfer = false;

		private int _iNeuerHelferID = -1;

		#endregion
	
		#region graphische Variablen

		protected System.Windows.Forms.TextBox txt_Strasse;
		protected System.Windows.Forms.TextBox txt_Ort;
		protected System.Windows.Forms.TextBox txt_PLZ;
		protected System.Windows.Forms.TextBox txt_HausNr;
		private System.Windows.Forms.GroupBox gbx_Personen;
		private System.Windows.Forms.Label lbl_NameLBL;
		private System.Windows.Forms.Label lbl_GeburtsdatumLBL;
		private System.Windows.Forms.Label lbl_PositionLBL;
		private System.Windows.Forms.Label lbl_StatusLBL;
		private System.Windows.Forms.Label lbl_OrtsverbandLBL;
		private System.Windows.Forms.Label lbl_VornameLBL;
		private System.Windows.Forms.DateTimePicker dtp_Geburtsdatum;
		private System.Windows.Forms.ComboBox cmb_Position;
		private System.Windows.Forms.ComboBox cmb_Status;
		private System.Windows.Forms.GroupBox gbx_Anschrift;
		private System.Windows.Forms.Label lbl_PLZLBL;
		private System.Windows.Forms.Label lbl_Nr;
		private System.Windows.Forms.Label lbl_StrasseLBL;
		private System.Windows.Forms.Label lbl_StadtLBL;
		private System.Windows.Forms.GroupBox gbx_ZusatzInfo;
		private System.Windows.Forms.RichTextBox txt_Erreichbarkeit;
		private System.Windows.Forms.Button btn_Speichern;
		private System.Windows.Forms.Button btn_Zuruecksetzen;
		private System.Windows.Forms.GroupBox gbx_Erreichbarkeit;
		private System.Windows.Forms.TextBox txt_Vorname;
		private System.Windows.Forms.TextBox txt_Name;
		private System.Windows.Forms.RichTextBox txt_ZusatzInfo;
		private System.Windows.Forms.GroupBox gbx_PersonenUntergroupbox;
		private System.Windows.Forms.ComboBox cmb_OV;
		private System.Windows.Forms.GroupBox gbx_Eingabemaske;
		private System.Windows.Forms.Label lbl_ModulID;
		private System.Windows.Forms.Label lbl_ESPID;
		private System.Windows.Forms.Label lbl_ModulIDLBL;
		private System.Windows.Forms.Label lb_ESPIDLBL;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmb_KrfStatus;
		public System.ComponentModel.Container components = null;

		#endregion

		#region Konstruktor & Destruktor
		public usc_Helfer(Cst_EK pin_stEK)
		{
			this._stEK = pin_stEK;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.ep_Eingabe.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.InitAlleSBE();
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
			this.lbl_NameLBL = new System.Windows.Forms.Label();
			this.lbl_GeburtsdatumLBL = new System.Windows.Forms.Label();
			this.lbl_PositionLBL = new System.Windows.Forms.Label();
			this.lbl_StatusLBL = new System.Windows.Forms.Label();
			this.lbl_OrtsverbandLBL = new System.Windows.Forms.Label();
			this.txt_Name = new System.Windows.Forms.TextBox();
			this.txt_ZusatzInfo = new System.Windows.Forms.RichTextBox();
			this.txt_Erreichbarkeit = new System.Windows.Forms.RichTextBox();
			this.lbl_VornameLBL = new System.Windows.Forms.Label();
			this.gbx_Eingabemaske = new System.Windows.Forms.GroupBox();
			this.cmb_KrfStatus = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lbl_ModulID = new System.Windows.Forms.Label();
			this.lbl_ESPID = new System.Windows.Forms.Label();
			this.lbl_ModulIDLBL = new System.Windows.Forms.Label();
			this.lb_ESPIDLBL = new System.Windows.Forms.Label();
			this.gbx_Erreichbarkeit = new System.Windows.Forms.GroupBox();
			this.gbx_ZusatzInfo = new System.Windows.Forms.GroupBox();
			this.gbx_Anschrift = new System.Windows.Forms.GroupBox();
			this.lbl_PLZLBL = new System.Windows.Forms.Label();
			this.lbl_Nr = new System.Windows.Forms.Label();
			this.txt_Strasse = new System.Windows.Forms.TextBox();
			this.txt_Ort = new System.Windows.Forms.TextBox();
			this.txt_PLZ = new System.Windows.Forms.TextBox();
			this.txt_HausNr = new System.Windows.Forms.TextBox();
			this.lbl_StrasseLBL = new System.Windows.Forms.Label();
			this.lbl_StadtLBL = new System.Windows.Forms.Label();
			this.cmb_Status = new System.Windows.Forms.ComboBox();
			this.cmb_Position = new System.Windows.Forms.ComboBox();
			this.dtp_Geburtsdatum = new System.Windows.Forms.DateTimePicker();
			this.txt_Vorname = new System.Windows.Forms.TextBox();
			this.cmb_OV = new System.Windows.Forms.ComboBox();
			this.gbx_Personen = new System.Windows.Forms.GroupBox();
			this.gbx_PersonenUntergroupbox = new System.Windows.Forms.GroupBox();
			this.btn_Speichern = new System.Windows.Forms.Button();
			this.btn_Zuruecksetzen = new System.Windows.Forms.Button();
			this.gbx_Eingabemaske.SuspendLayout();
			this.gbx_Erreichbarkeit.SuspendLayout();
			this.gbx_ZusatzInfo.SuspendLayout();
			this.gbx_Anschrift.SuspendLayout();
			this.gbx_Personen.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbl_NameLBL
			// 
			this.lbl_NameLBL.Location = new System.Drawing.Point(5, 15);
			this.lbl_NameLBL.Name = "lbl_NameLBL";
			this.lbl_NameLBL.Size = new System.Drawing.Size(45, 16);
			this.lbl_NameLBL.TabIndex = 0;
			this.lbl_NameLBL.Text = "Name:";
			// 
			// lbl_GeburtsdatumLBL
			// 
			this.lbl_GeburtsdatumLBL.Location = new System.Drawing.Point(5, 75);
			this.lbl_GeburtsdatumLBL.Name = "lbl_GeburtsdatumLBL";
			this.lbl_GeburtsdatumLBL.Size = new System.Drawing.Size(65, 16);
			this.lbl_GeburtsdatumLBL.TabIndex = 1;
			this.lbl_GeburtsdatumLBL.Text = "Geb.Datum:";
			// 
			// lbl_PositionLBL
			// 
			this.lbl_PositionLBL.Location = new System.Drawing.Point(5, 95);
			this.lbl_PositionLBL.Name = "lbl_PositionLBL";
			this.lbl_PositionLBL.Size = new System.Drawing.Size(55, 16);
			this.lbl_PositionLBL.TabIndex = 2;
			this.lbl_PositionLBL.Text = "Position:";
			// 
			// lbl_StatusLBL
			// 
			this.lbl_StatusLBL.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.lbl_StatusLBL.Location = new System.Drawing.Point(5, 115);
			this.lbl_StatusLBL.Name = "lbl_StatusLBL";
			this.lbl_StatusLBL.Size = new System.Drawing.Size(60, 15);
			this.lbl_StatusLBL.TabIndex = 3;
			this.lbl_StatusLBL.Text = "Helf.status:";
			// 
			// lbl_OrtsverbandLBL
			// 
			this.lbl_OrtsverbandLBL.Location = new System.Drawing.Point(5, 55);
			this.lbl_OrtsverbandLBL.Name = "lbl_OrtsverbandLBL";
			this.lbl_OrtsverbandLBL.Size = new System.Drawing.Size(70, 16);
			this.lbl_OrtsverbandLBL.TabIndex = 5;
			this.lbl_OrtsverbandLBL.Text = "Ortsverband:";
			// 
			// txt_Name
			// 
			this.txt_Name.Location = new System.Drawing.Point(75, 15);
			this.txt_Name.Name = "txt_Name";
			this.txt_Name.Size = new System.Drawing.Size(135, 20);
			this.txt_Name.TabIndex = 0;
			this.txt_Name.Text = "";
			this.txt_Name.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Name.Leave += new System.EventHandler(this.txt_Name_Leave);
			// 
			// txt_ZusatzInfo
			// 
			this.txt_ZusatzInfo.Location = new System.Drawing.Point(5, 20);
			this.txt_ZusatzInfo.Name = "txt_ZusatzInfo";
			this.txt_ZusatzInfo.Size = new System.Drawing.Size(130, 85);
			this.txt_ZusatzInfo.TabIndex = 0;
			this.txt_ZusatzInfo.Text = "";
			this.txt_ZusatzInfo.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// txt_Erreichbarkeit
			// 
			this.txt_Erreichbarkeit.Location = new System.Drawing.Point(5, 15);
			this.txt_Erreichbarkeit.Name = "txt_Erreichbarkeit";
			this.txt_Erreichbarkeit.Size = new System.Drawing.Size(220, 70);
			this.txt_Erreichbarkeit.TabIndex = 0;
			this.txt_Erreichbarkeit.Text = "";
			this.txt_Erreichbarkeit.Leave += new System.EventHandler(this.txt_Erreichbarkeit_Leave);
			this.txt_Erreichbarkeit.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// lbl_VornameLBL
			// 
			this.lbl_VornameLBL.Location = new System.Drawing.Point(5, 35);
			this.lbl_VornameLBL.Name = "lbl_VornameLBL";
			this.lbl_VornameLBL.Size = new System.Drawing.Size(60, 16);
			this.lbl_VornameLBL.TabIndex = 14;
			this.lbl_VornameLBL.Text = "Vorname:";
			// 
			// gbx_Eingabemaske
			// 
			this.gbx_Eingabemaske.BackColor = System.Drawing.Color.White;
			this.gbx_Eingabemaske.Controls.Add(this.cmb_KrfStatus);
			this.gbx_Eingabemaske.Controls.Add(this.label1);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_ModulID);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_ESPID);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_ModulIDLBL);
			this.gbx_Eingabemaske.Controls.Add(this.lb_ESPIDLBL);
			this.gbx_Eingabemaske.Controls.Add(this.gbx_Erreichbarkeit);
			this.gbx_Eingabemaske.Controls.Add(this.gbx_ZusatzInfo);
			this.gbx_Eingabemaske.Controls.Add(this.gbx_Anschrift);
			this.gbx_Eingabemaske.Controls.Add(this.cmb_Status);
			this.gbx_Eingabemaske.Controls.Add(this.cmb_Position);
			this.gbx_Eingabemaske.Controls.Add(this.dtp_Geburtsdatum);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_NameLBL);
			this.gbx_Eingabemaske.Controls.Add(this.txt_Name);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_VornameLBL);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_GeburtsdatumLBL);
			this.gbx_Eingabemaske.Controls.Add(this.txt_Vorname);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_PositionLBL);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_StatusLBL);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_OrtsverbandLBL);
			this.gbx_Eingabemaske.Controls.Add(this.cmb_OV);
			this.gbx_Eingabemaske.Location = new System.Drawing.Point(5, 5);
			this.gbx_Eingabemaske.Name = "gbx_Eingabemaske";
			this.gbx_Eingabemaske.Size = new System.Drawing.Size(615, 165);
			this.gbx_Eingabemaske.TabIndex = 15;
			this.gbx_Eingabemaske.TabStop = false;
			// 
			// cmb_KrfStatus
			// 
			this.cmb_KrfStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_KrfStatus.Location = new System.Drawing.Point(75, 135);
			this.cmb_KrfStatus.Name = "cmb_KrfStatus";
			this.cmb_KrfStatus.Size = new System.Drawing.Size(135, 21);
			this.cmb_KrfStatus.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5, 135);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 15);
			this.label1.TabIndex = 80;
			this.label1.Text = "Krf.Status:";
			// 
			// lbl_ModulID
			// 
			this.lbl_ModulID.Location = new System.Drawing.Point(505, 135);
			this.lbl_ModulID.Name = "lbl_ModulID";
			this.lbl_ModulID.Size = new System.Drawing.Size(100, 20);
			this.lbl_ModulID.TabIndex = 79;
			// 
			// lbl_ESPID
			// 
			this.lbl_ESPID.Location = new System.Drawing.Point(505, 120);
			this.lbl_ESPID.Name = "lbl_ESPID";
			this.lbl_ESPID.Size = new System.Drawing.Size(100, 15);
			this.lbl_ESPID.TabIndex = 78;
			// 
			// lbl_ModulIDLBL
			// 
			this.lbl_ModulIDLBL.Location = new System.Drawing.Point(455, 135);
			this.lbl_ModulIDLBL.Name = "lbl_ModulIDLBL";
			this.lbl_ModulIDLBL.Size = new System.Drawing.Size(50, 20);
			this.lbl_ModulIDLBL.TabIndex = 77;
			this.lbl_ModulIDLBL.Text = "ModulID:";
			// 
			// lb_ESPIDLBL
			// 
			this.lb_ESPIDLBL.Location = new System.Drawing.Point(455, 120);
			this.lb_ESPIDLBL.Name = "lb_ESPIDLBL";
			this.lb_ESPIDLBL.Size = new System.Drawing.Size(45, 20);
			this.lb_ESPIDLBL.TabIndex = 76;
			this.lb_ESPIDLBL.Text = "ESPID:";
			// 
			// gbx_Erreichbarkeit
			// 
			this.gbx_Erreichbarkeit.Controls.Add(this.txt_Erreichbarkeit);
			this.gbx_Erreichbarkeit.Location = new System.Drawing.Point(215, 70);
			this.gbx_Erreichbarkeit.Name = "gbx_Erreichbarkeit";
			this.gbx_Erreichbarkeit.Size = new System.Drawing.Size(235, 90);
			this.gbx_Erreichbarkeit.TabIndex = 75;
			this.gbx_Erreichbarkeit.TabStop = false;
			this.gbx_Erreichbarkeit.Text = "Erreichbarkeit";
			// 
			// gbx_ZusatzInfo
			// 
			this.gbx_ZusatzInfo.Controls.Add(this.txt_ZusatzInfo);
			this.gbx_ZusatzInfo.Location = new System.Drawing.Point(455, 10);
			this.gbx_ZusatzInfo.Name = "gbx_ZusatzInfo";
			this.gbx_ZusatzInfo.Size = new System.Drawing.Size(145, 110);
			this.gbx_ZusatzInfo.TabIndex = 74;
			this.gbx_ZusatzInfo.TabStop = false;
			this.gbx_ZusatzInfo.Text = "Zusatz Info.";
			// 
			// gbx_Anschrift
			// 
			this.gbx_Anschrift.Controls.Add(this.lbl_PLZLBL);
			this.gbx_Anschrift.Controls.Add(this.lbl_Nr);
			this.gbx_Anschrift.Controls.Add(this.txt_Strasse);
			this.gbx_Anschrift.Controls.Add(this.txt_Ort);
			this.gbx_Anschrift.Controls.Add(this.txt_PLZ);
			this.gbx_Anschrift.Controls.Add(this.txt_HausNr);
			this.gbx_Anschrift.Controls.Add(this.lbl_StrasseLBL);
			this.gbx_Anschrift.Controls.Add(this.lbl_StadtLBL);
			this.gbx_Anschrift.Location = new System.Drawing.Point(215, 10);
			this.gbx_Anschrift.Name = "gbx_Anschrift";
			this.gbx_Anschrift.Size = new System.Drawing.Size(235, 60);
			this.gbx_Anschrift.TabIndex = 73;
			this.gbx_Anschrift.TabStop = false;
			this.gbx_Anschrift.Text = "Anschrift";
			this.gbx_Anschrift.Leave += new System.EventHandler(this.gbx_Anschrift_Leave);
			// 
			// lbl_PLZLBL
			// 
			this.lbl_PLZLBL.Location = new System.Drawing.Point(8, 40);
			this.lbl_PLZLBL.Name = "lbl_PLZLBL";
			this.lbl_PLZLBL.Size = new System.Drawing.Size(28, 16);
			this.lbl_PLZLBL.TabIndex = 8;
			this.lbl_PLZLBL.Text = "PLZ";
			// 
			// lbl_Nr
			// 
			this.lbl_Nr.Location = new System.Drawing.Point(180, 20);
			this.lbl_Nr.Name = "lbl_Nr";
			this.lbl_Nr.Size = new System.Drawing.Size(20, 16);
			this.lbl_Nr.TabIndex = 1;
			this.lbl_Nr.Text = "Nr.";
			// 
			// txt_Strasse
			// 
			this.txt_Strasse.Location = new System.Drawing.Point(44, 16);
			this.txt_Strasse.MaxLength = 50;
			this.txt_Strasse.Name = "txt_Strasse";
			this.txt_Strasse.Size = new System.Drawing.Size(131, 20);
			this.txt_Strasse.TabIndex = 0;
			this.txt_Strasse.Text = "";
			this.txt_Strasse.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// txt_Ort
			// 
			this.txt_Ort.Location = new System.Drawing.Point(120, 36);
			this.txt_Ort.MaxLength = 50;
			this.txt_Ort.Name = "txt_Ort";
			this.txt_Ort.Size = new System.Drawing.Size(105, 20);
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
			this.txt_PLZ.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// txt_HausNr
			// 
			this.txt_HausNr.Location = new System.Drawing.Point(200, 16);
			this.txt_HausNr.MaxLength = 5;
			this.txt_HausNr.Name = "txt_HausNr";
			this.txt_HausNr.Size = new System.Drawing.Size(28, 20);
			this.txt_HausNr.TabIndex = 1;
			this.txt_HausNr.Text = "";
			this.txt_HausNr.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// lbl_StrasseLBL
			// 
			this.lbl_StrasseLBL.Location = new System.Drawing.Point(4, 20);
			this.lbl_StrasseLBL.Name = "lbl_StrasseLBL";
			this.lbl_StrasseLBL.Size = new System.Drawing.Size(40, 16);
			this.lbl_StrasseLBL.TabIndex = 7;
			this.lbl_StrasseLBL.Text = "Straße";
			// 
			// lbl_StadtLBL
			// 
			this.lbl_StadtLBL.Location = new System.Drawing.Point(85, 40);
			this.lbl_StadtLBL.Name = "lbl_StadtLBL";
			this.lbl_StadtLBL.Size = new System.Drawing.Size(36, 16);
			this.lbl_StadtLBL.TabIndex = 9;
			this.lbl_StadtLBL.Text = "Stadt";
			// 
			// cmb_Status
			// 
			this.cmb_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Status.Location = new System.Drawing.Point(75, 115);
			this.cmb_Status.Name = "cmb_Status";
			this.cmb_Status.Size = new System.Drawing.Size(135, 21);
			this.cmb_Status.TabIndex = 5;
			this.cmb_Status.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_Status.Leave += new System.EventHandler(this.cmb_Status_Leave);
			// 
			// cmb_Position
			// 
			this.cmb_Position.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Position.Location = new System.Drawing.Point(75, 95);
			this.cmb_Position.Name = "cmb_Position";
			this.cmb_Position.Size = new System.Drawing.Size(135, 21);
			this.cmb_Position.TabIndex = 4;
			this.cmb_Position.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_Position.Leave += new System.EventHandler(this.cmb_Position_Leave);
			// 
			// dtp_Geburtsdatum
			// 
			this.dtp_Geburtsdatum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtp_Geburtsdatum.Location = new System.Drawing.Point(75, 75);
			this.dtp_Geburtsdatum.Name = "dtp_Geburtsdatum";
			this.dtp_Geburtsdatum.Size = new System.Drawing.Size(135, 20);
			this.dtp_Geburtsdatum.TabIndex = 3;
			this.dtp_Geburtsdatum.Leave += new System.EventHandler(this.dtp_Geburtsdatum_Leave);
			this.dtp_Geburtsdatum.ValueChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// txt_Vorname
			// 
			this.txt_Vorname.Location = new System.Drawing.Point(75, 35);
			this.txt_Vorname.Name = "txt_Vorname";
			this.txt_Vorname.Size = new System.Drawing.Size(135, 20);
			this.txt_Vorname.TabIndex = 1;
			this.txt_Vorname.Text = "";
			this.txt_Vorname.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Vorname.Leave += new System.EventHandler(this.txt_Vorname_Leave);
			// 
			// cmb_OV
			// 
			this.cmb_OV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_OV.Location = new System.Drawing.Point(75, 55);
			this.cmb_OV.Name = "cmb_OV";
			this.cmb_OV.Size = new System.Drawing.Size(135, 21);
			this.cmb_OV.TabIndex = 2;
			this.cmb_OV.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_OV.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_OV.Leave += new System.EventHandler(this.cmb_OV_Leave);
			// 
			// gbx_Personen
			// 
			this.gbx_Personen.Controls.Add(this.gbx_PersonenUntergroupbox);
			this.gbx_Personen.Location = new System.Drawing.Point(5, 170);
			this.gbx_Personen.Name = "gbx_Personen";
			this.gbx_Personen.Size = new System.Drawing.Size(615, 250);
			this.gbx_Personen.TabIndex = 16;
			this.gbx_Personen.TabStop = false;
			this.gbx_Personen.Text = "Personen";
			// 
			// gbx_PersonenUntergroupbox
			// 
			this.gbx_PersonenUntergroupbox.Location = new System.Drawing.Point(5, 15);
			this.gbx_PersonenUntergroupbox.Name = "gbx_PersonenUntergroupbox";
			this.gbx_PersonenUntergroupbox.Size = new System.Drawing.Size(605, 230);
			this.gbx_PersonenUntergroupbox.TabIndex = 0;
			this.gbx_PersonenUntergroupbox.TabStop = false;
			// 
			// btn_Speichern
			// 
			this.btn_Speichern.Location = new System.Drawing.Point(525, 425);
			this.btn_Speichern.Name = "btn_Speichern";
			this.btn_Speichern.Size = new System.Drawing.Size(90, 25);
			this.btn_Speichern.TabIndex = 0;
			this.btn_Speichern.Text = "Speichern";
			this.btn_Speichern.Click += new System.EventHandler(this.btn_Speichern_Click);
			// 
			// btn_Zuruecksetzen
			// 
			this.btn_Zuruecksetzen.Location = new System.Drawing.Point(430, 425);
			this.btn_Zuruecksetzen.Name = "btn_Zuruecksetzen";
			this.btn_Zuruecksetzen.Size = new System.Drawing.Size(90, 25);
			this.btn_Zuruecksetzen.TabIndex = 1;
			this.btn_Zuruecksetzen.Text = "Zurücksetzen";
			this.btn_Zuruecksetzen.Click += new System.EventHandler(this.btn_Zuruecksetzen_Click);
			// 
			// usc_Helfer
			// 
			this.Controls.Add(this.btn_Zuruecksetzen);
			this.Controls.Add(this.btn_Speichern);
			this.Controls.Add(this.gbx_Personen);
			this.Controls.Add(this.gbx_Eingabemaske);
			this.Location = new System.Drawing.Point(6, 21);
			this.Name = "usc_Helfer";
			this.Size = new System.Drawing.Size(624, 456);
			this.gbx_Eingabemaske.ResumeLayout(false);
			this.gbx_Erreichbarkeit.ResumeLayout(false);
			this.gbx_ZusatzInfo.ResumeLayout(false);
			this.gbx_Anschrift.ResumeLayout(false);
			this.gbx_Personen.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#endregion

		#region Setze- Methode
		private void InitAlleSBE()
		{
			this.SetzePosition();
			this.SetzeStatus();
			this.SetzeKraefteStatus();
			this.SetzePersonen();
			this.SetzeOV();
			this._usc_dtg_Personen.SetzeEreignissMouseUp(new MouseEventHandler(this.dtg_Personen_MouseUp));
		}

		private void SetzeOV()
		{
			// Unterschiedlicher Still zu MAT und FUNK, da Leerzeichen auch hinzugefügt werden.
			this.cmb_OV.Items.Clear();
		//	this.cmb_OV.Items.Add("");
			Cdv_Ortsverband[] OVMenge = this._stEK.AlleOVs;
			IEnumerator ie = OVMenge.GetEnumerator();
			while(ie.MoveNext())
			{
				Cdv_Ortsverband OV = (Cdv_Ortsverband) ie.Current;
				this.cmb_OV.Items.Add(OV);
			}
		}
		private void SetzePersonen()
		{
			// Einstellen die Größe des Datagrid
			this._usc_dtg_Personen = new usc_dtg_Personen(this.gbx_PersonenUntergroupbox.Size.Width, this.gbx_PersonenUntergroupbox.Size.Height);
			this._usc_dtg_Personen.LadePersonen(this._stEK.AlleHelfer);
			this.gbx_PersonenUntergroupbox.Controls.Add(this._usc_dtg_Personen);

		}

		private void SetzePosition()
		{
			// Unterschiedlicher Still zu MAT und FUNK, da Leerzeichen auch hinzugefügt werden.
			this.cmb_Position.Items.Clear();
		//	this.cmb_Position.Items.Add("");
			foreach(Tdv_Position po in 
				Enum.GetValues(typeof(Tdv_Position)))
			{
				this.cmb_Position.Items.Add(po);
			}
		}
		private void SetzeStatus()
		{
			// Unterschiedlicher Still zu MAT und FUNK, da Leerzeichen auch hinzugefügt werden.
			this.cmb_Status.Items.Clear();
		//	this.cmb_Status.Items.Add("");
			foreach(Tdv_Helferstatus he in 
				Enum.GetValues(typeof(Tdv_Helferstatus)))
			{
				this.cmb_Status.Items.Add(he);
			}
		}

		private void SetzeKraefteStatus()
		{
			// Unterschiedlicher Still zu MAT und FUNK, da Leerzeichen auch hinzugefügt werden.
			this.cmb_KrfStatus.Items.Clear();
			//	this.cmb_Status.Items.Add("");
			foreach(Tdv_Kraeftestatus he in 
				Enum.GetValues(typeof(Tdv_Kraeftestatus)))
			{
				this.cmb_KrfStatus.Items.Add(he);
			}
		}

		// Die Eingabemaske hat zwei Modi, zum einen sind
		// alle Felder aktiv zum Datenspeichern, zum anderen
		// wird die Enable-Eigenschaft teilweiser Eingabefelder 
		// mit false zu setzen, dies hat den Zweck zum Modifizieren
		// der Daten. Falls pin_Modifizieren true ist, wird die
		// Eingabemaske mit Modifzierenmodi gesetzt.
		private void SetzeEingabefelderModi(bool pin_Modifizieren)
		{
			//TODO
			// Ein Beispiel, damit dies nicht vergessen werden.
			this.txt_Name.Enabled = pin_Modifizieren;
			
		}
		private void SetzeHelferAusPeronentabelle()
		{
			this.LadeHelfer(this._usc_dtg_Personen.HoleAusgewaehltePerson());	
		}

	
		private void Zuruecksetzen()
		{
			this.txt_Name.Text = "";
			this.ep_Eingabe.SetError(this.txt_Name, "");
			this.txt_Vorname.Text = "";
			this.ep_Eingabe.SetError(this.txt_Vorname, "");
			this.cmb_OV.Text = "";
			this.ep_Eingabe.SetError(this.cmb_OV, "");
			this.dtp_Geburtsdatum.Value = DateTime.Now;
			this.ep_Eingabe.SetError(this.dtp_Geburtsdatum, "");
			this.cmb_Position.SelectedIndex = -1;
			this.ep_Eingabe.SetError(this.cmb_Position, "");
			this.cmb_OV.SelectedIndex = -1;
			this.ep_Eingabe.SetError(this.cmb_OV, "");
			this.cmb_Status.SelectedIndex = -1;
			this.ep_Eingabe.SetError(this.cmb_Status, "");
			this.cmb_KrfStatus.SelectedIndex =-1;
			this.ep_Eingabe.SetError(this.cmb_KrfStatus, "");
			this.cmb_Status.Text = "";
			this.ep_Eingabe.SetError(this.cmb_Status, "");
			this.txt_Strasse.Text ="";
			this.ep_Eingabe.SetError(this.txt_Strasse, "");
			this.txt_HausNr.Text = "";
			this.ep_Eingabe.SetError(this.txt_HausNr, "");
			this.txt_PLZ.Text = "";
			this.ep_Eingabe.SetError(this.txt_PLZ, "");
			this.txt_Ort.Text = "";
			this.ep_Eingabe.SetError(this.txt_Ort, "");
			this.txt_Erreichbarkeit.Text = "";
			this.ep_Eingabe.SetError(this.txt_Erreichbarkeit, "");
			this.txt_ZusatzInfo.Text = "";
			this.ep_Eingabe.SetError(this.txt_ZusatzInfo, "");

			this.lbl_ModulID.Text = "";
			this.lbl_ESPID.Text = "";

			this.SetzeEingabefelderModi(true);
			this._b_FelderModifiziert = false;
			this._bIstNeuerHelfer = false;
			this._iNeuerHelferID = -1;
		

		}
		private void ZuruecksetzenMitRueckfrage()
		{
			if(this._b_FelderModifiziert == true)
			{
				if(CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes )
				{
					this.Zuruecksetzen();
				}
			}
		}

		#endregion

		#region LadeHelfer in die Controls
		public void LadeHelfer(Cdv_Helfer pin_Helfer)
		{
			this.SetzeEingabefelderModi(true);
			this.txt_Name.Text = pin_Helfer.Personendaten.Name;
			this.txt_Vorname.Text = pin_Helfer.Personendaten.Vorname;

			#region cmb_OV
			foreach(object OVItem in cmb_OV.Items)
			{
				if (OVItem is Cdv_Ortsverband)
				{
					if ((OVItem as Cdv_Ortsverband).ID==pin_Helfer.OVID)
					{
						this.cmb_OV.SelectedItem=OVItem;
						break;
					}
				}
			}
			#endregion

			this.dtp_Geburtsdatum.Value = pin_Helfer.Personendaten.GebDatum;
			
			#region cmb_Position
			foreach (object PositionItem in this.cmb_Position.Items)
			{
				if (PositionItem is Tdv_Position)
				{
					if ((int) (PositionItem) == (int) pin_Helfer.Position)
					{
						this.cmb_Position.SelectedItem=PositionItem;
						break;
					}
				}
			}
			#endregion

			#region cmb_Status
			foreach (object StatusItem in this.cmb_Status.Items)
			{
				if (StatusItem is Tdv_Helferstatus)
				{
					if ((int) (StatusItem) == (int) pin_Helfer.Helferstatus)
					{
						this.cmb_Status.SelectedItem=StatusItem;
						break;
					}
				}
			}
			#endregion

			#region cmb_KrfStatusStatus
			foreach (object KrfStatusItem in this.cmb_KrfStatus.Items)
			{
				if (KrfStatusItem is Tdv_Kraeftestatus)
				{
					if ((int) (KrfStatusItem) == (int) pin_Helfer.Kraeftestatus)
					{
						this.cmb_KrfStatus.SelectedItem=KrfStatusItem;
						break;
					}
				}
			}
			#endregion

			this.txt_Strasse.Text = pin_Helfer.Personendaten.Anschrift.Strasse;
			this.txt_HausNr.Text = pin_Helfer.Personendaten.Anschrift.Hausnummer;
			this.txt_PLZ.Text = pin_Helfer.Personendaten.Anschrift.PLZ;
			this.txt_Ort.Text = pin_Helfer.Personendaten.Anschrift.Ort;
			this.txt_Erreichbarkeit.Text = pin_Helfer.Erreichbarkeit;
			this.txt_ZusatzInfo.Text = pin_Helfer.Personendaten.ZusatzInfo;
			this._bIstNeuerHelfer = true;
			this._iNeuerHelferID = pin_Helfer.ID;
			this.lbl_ESPID.Text =  pin_Helfer.EinsatzschwerpunktID.ToString();
			this.lbl_ModulID.Text = pin_Helfer.ModulID.ToString();
			this._b_FelderModifiziert = false;
		}
		#endregion
	
		#region event handler
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


		private void btn_Speichern_Click(object sender, System.EventArgs e)
		{
			if(CPopUp.SpeichernOhneUeberschreiben() == DialogResult.OK)
			{
				if(!this.EingabevalidierungHelfer())
					return;
				Cdv_Helfer helfer = new Cdv_Helfer();
				if((this._bIstNeuerHelfer) && (this._iNeuerHelferID != -1))
					helfer.ID = this._iNeuerHelferID;
				helfer.Erreichbarkeit = this.txt_Erreichbarkeit.Text;
				helfer.Personendaten.Name = this.txt_Name.Text;
				helfer.Personendaten.Vorname = this.txt_Vorname.Text;
				helfer.Personendaten.GebDatum = this.dtp_Geburtsdatum.Value;
				helfer.Personendaten.Anschrift.Ort = this.txt_Ort.Text;
				helfer.Personendaten.Anschrift.PLZ = this.txt_PLZ.Text;
				helfer.Personendaten.Anschrift.Hausnummer = this.txt_HausNr.Text;
				helfer.Personendaten.Anschrift.Strasse = this.txt_Strasse.Text;
				
				helfer.Helferstatus = (Tdv_Helferstatus) this.cmb_Status.SelectedItem;
				helfer.Position = (Tdv_Position) this.cmb_Position.SelectedItem;
				helfer.Kraeftestatus= (Tdv_Kraeftestatus) this.cmb_KrfStatus.SelectedItem;

				helfer.Personendaten.ZusatzInfo = this.txt_ZusatzInfo.Text;
				helfer.OVID = (this.cmb_OV.SelectedItem as Cdv_Ortsverband).ID;
				if(helfer.Personendaten.ZusatzInfo.Length == 0)
					helfer.Personendaten.ZusatzInfo = "keine Zusatzinfos";
				this._stEK.SpeichereHelfer(helfer);
				this.Zuruecksetzen();
				this.SetzeEingabefelderModi(true);
				this._usc_dtg_Personen.LadePersonen(this._stEK.AlleHelfer);
			}

		}

		private void btn_Zuruecksetzen_Click(object sender, System.EventArgs e)
		{
			if(this._bIstNeuerHelfer && !this._b_FelderModifiziert)
			{
				this.Zuruecksetzen();
				return;
			}
			this.ZuruecksetzenMitRueckfrage();
		}

		private void dtg_Personen_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			DataGrid myGrid = (DataGrid) sender;
			DataGrid.HitTestInfo myHitInfo = myGrid.HitTest(e.X, e.Y);
			if((myHitInfo.Row < 0) || (myHitInfo.Column != -1))
				return;
			try
			{
				object o = myGrid[myHitInfo.Row, 0];
				int iHelferID = Int32.Parse(o.ToString());
				Cdv_Helfer helfer = this._stEK.HoleHelfer(iHelferID);
				if(!this._b_FelderModifiziert)
				{
					this._bIstNeuerHelfer = false;
					this._iNeuerHelferID = helfer.ID;
					this.LadeHelfer(helfer);
				}
				this._usc_dtg_Personen.SetzteFehlerMeldung("");
			}
			catch
			{
				this._usc_dtg_Personen.SetzteFehlerMeldung("Helfer konnte nicht geladen werden!");
			}
		}

		#endregion

		#region Eingabevalidierung Helfer
		
		private bool EingabevalidierungHelfer()
		{
			if(this.ValidiereNameHelfer() && this.ValidiereVornameHelfer() && this.ValidierePositionHelfer()
				&& this.ValidiereStatusHelfer() && this.ValidiereGeburtsdatumHelfer() && this.ValidiereOVHelfer()
				&& this.ValidiereErreichbarkeitHelfer())
				return(true);
			this.txt_Name_Validated_ESP(null, null);
			this.txt_Vorname_Validated_ESP(null, null);
			this.cmb_Position_Validated_ESP(null, null);
			this.cmb_Status_Validated_ESP(null, null);
			this.dtp_Geburtsdatum_Validated_ESP(null, null);
			this.txt_Erreichbarkeit_Validated_ESP(null, null);
			this.cmb_OV_Validated_ESP(null, null);
			return(false);
		}

		private bool ValidiereOVHelfer()
		{
			return(this.cmb_OV.Text.Length > 0);
		}

		private void cmb_OV_Validated_ESP(object sender, System.EventArgs e)
		{
			if(this.ValidiereOVHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.cmb_OV, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.cmb_OV, "Bitte wählen Sie ein Ortsverband aus");
			}
		}

		private bool ValidiereAnschriftHelfer()
		{
			bool bGueltig = false;
			bGueltig = ((this.txt_Ort.Text.Length > 0) && (this.txt_HausNr.Text.Length > 0)
				&& (this.txt_PLZ.Text.Length > 0)
				&& (this.txt_Strasse.Text.Length > 0));
			return(bGueltig);
		}

		private void gbx_Anschrift_Helfer_Validated(object sender, System.EventArgs e)
		{
			if(this.ValidiereAnschriftHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.gbx_Anschrift, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.gbx_Anschrift, "Bitte geben sie eine vollständige Helferanschrift an");
			}
		}

		private bool ValidiereErreichbarkeitHelfer()
		{
			return(this.txt_Erreichbarkeit.Text.Length > 0);
		}

		private void txt_Erreichbarkeit_Validated_ESP(object sender, System.EventArgs e)
		{
			if(this.ValidiereErreichbarkeitHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_Erreichbarkeit, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_Erreichbarkeit, "Bitte geben sie die Helfererreichbarkeit an");
			}
		}

		private string[] ParseAnschrift(string pin_StrasseHausnummer, string pin_PLZOrt)
		{
			string[] szReturnVal = new string[4];
			try
			{
				szReturnVal[0] = pin_StrasseHausnummer.Substring(0,pin_StrasseHausnummer.LastIndexOf(" "));
				szReturnVal[1] = pin_StrasseHausnummer.Substring(pin_StrasseHausnummer.LastIndexOf(" ") + 1);
				szReturnVal[2] = pin_PLZOrt.Substring(0, pin_PLZOrt.LastIndexOf(" "));
				szReturnVal[3] = pin_PLZOrt.Substring(pin_PLZOrt.LastIndexOf(" ") + 1);
			}
			catch
			{

				return(null);
			}
			return(szReturnVal);
		}

		private bool ValidiereGeburtsdatumHelfer()
		{
			return(this.dtp_Geburtsdatum.Value < DateTime.Now);
		}

		private void dtp_Geburtsdatum_Validated_ESP(object sender, System.EventArgs e)
		{
			if(this.ValidiereGeburtsdatumHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.dtp_Geburtsdatum, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.dtp_Geburtsdatum, "Bitte wählen Sie ein gültiges Geburtsdatum aus");
			}
		}

		private bool ValidiereStatusHelfer()
		{
			return(this.cmb_Status.Text.Length > 0);
		}

		private void cmb_Status_Validated_ESP(object sender, System.EventArgs e)
		{
			if(this.ValidiereStatusHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.cmb_Status, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.cmb_Status, "Bitte wählen Sie einen Helferstatus aus");
			}
		}

		private bool ValidierePositionHelfer()
		{
			return(this.cmb_Position.Text.Length > 0);
		}

		private void cmb_Position_Validated_ESP(object sender, System.EventArgs e)
		{
			if(this.ValidierePositionHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.cmb_Position, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.cmb_Position, "Bitte wählen Sie eine Helferposition aus");
			}
		}

		private bool ValidiereVornameHelfer()
		{
			return(this.txt_Vorname.Text.Length > 0);
		}

		private void txt_Vorname_Validated_ESP(object sender, System.EventArgs e)
		{
			if(this.ValidiereVornameHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_Vorname, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_Vorname, "Bitte geben Sie ein Helfervorname ein");
			}
		}

		private bool ValidiereNameHelfer()
		{
			return(this.txt_Name.Text.Length > 0);
		}

		private void txt_Name_Validated_ESP(object sender, System.EventArgs e)
		{
			if(this.ValidiereNameHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_Name, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_Name, "Bitte geben Sie ein Helfername ein");
			}
		}

		private void txt_Name_Leave(object sender, System.EventArgs e)
		{
			this.txt_Name_Validated_ESP(null, null);
		}

		private void txt_Vorname_Leave(object sender, System.EventArgs e)
		{
			this.txt_Vorname_Validated_ESP(null, null);
		}

		private void dtp_Geburtsdatum_Leave(object sender, System.EventArgs e)
		{
			this.dtp_Geburtsdatum_Validated_ESP(null, null);
		}

		private void cmb_Position_Leave(object sender, System.EventArgs e)
		{
			this.cmb_Position_Validated_ESP(null, null);
		}

		private void cmb_Status_Leave(object sender, System.EventArgs e)
		{
			this.cmb_Status_Validated_ESP(null, null);
		}

		private void txt_Erreichbarkeit_Leave(object sender, System.EventArgs e)
		{
			this.txt_Erreichbarkeit_Validated_ESP(null, null);
		}

		private void gbx_Anschrift_Leave(object sender, System.EventArgs e)
		{
		//	this.gbx_Anschrift_Helfer_Validated(null, null);
		}

		private void cmb_OV_Leave(object sender, System.EventArgs e)
		{
			this.cmb_OV_Validated_ESP(null, null);
		}

		#endregion

		#region get-Methoden
		public bool FelderIstModifiziert
		{
			get{return this._b_FelderModifiziert;}
		}
		#endregion

		#region Aktualisieren von Daten

		public void AktualisiereOV()
		{
			this.cmb_OV.Items.Clear();
			Cdv_Ortsverband[] OVMenge;
			if((OVMenge = this._stEK.AlleOVs) != null)
				foreach(Cdv_Ortsverband ov in OVMenge)
					this.cmb_OV.Items.Add(ov);
			this.cmb_OV.SelectedIndex = -1;
		}

		#endregion
		
	}
}
