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
	public class usc_Einheit : System.Windows.Forms.UserControl
	{
		#region eigene Variablen
		/// <summary>
		/// gibt an, ob bereits Eingaben geschehen sind
		/// </summary>
		protected bool _b_FelderModifiziert = false;
		public bool FelderIstModifziert
		{
			get{return this._b_FelderModifiziert;}
		}

		private Cdv_Einheit _aktuelleEinheit;

		private Cst_EK _stEK;
		/// <summary>
		/// ermöglicht das Anzeigen von fehlerhaften Eingaben
		/// </summary>
		protected System.Windows.Forms.ErrorProvider ep_Eingabe;

		
		/// <summary>
		/// Dieses Datagrid enthält alle Material, die die sich gerade im Modifikationsmodus
		/// befindete Einheit besitzt.
		/// </summary>
		private DataGrid dtg_geladeneMaterial = new DataGrid();

		/// <summary>
		/// Das Datagrid Personen enthält alle Helfer, die beim Speichern einer Einheit
		/// auszuwählen sind.
		/// </summary>
		private usc_dtg_Personen _usc_dtg_Personen;
		/// <summary>
		/// Dieses Datagrid enthält alle Helfer, die zu der sich im Modifikationsmodus
		/// befindeten Einheit gehören.
		/// </summary>
		private usc_dtg_Personen _usc_dtg_geladenePersonen;

		#endregion

		#region graphische Variablen
		private System.Windows.Forms.GroupBox gbx_Eingabemaske;
		private System.Windows.Forms.Label lbl_Name;
		private System.Windows.Forms.Label lbl_OV;
		private System.Windows.Forms.Label lbl_SollStaerke;
		private System.Windows.Forms.Label lbl_IstStaerke;
		private System.Windows.Forms.TextBox txt_Name;
		private System.Windows.Forms.TextBox txt_SollStaerke;
		private System.Windows.Forms.TextBox txt_IstStaerke;
		private System.Windows.Forms.RichTextBox txt_Erreichbarkeit;
		private System.Windows.Forms.TabControl tabctrl_Einheit;
		private System.Windows.Forms.DataGrid dtg_Material;
		private System.Windows.Forms.TabPage tabpage_Material;
		public System.Windows.Forms.Button btn_Speichern;
		public System.Windows.Forms.Button btn_Zuruecksetzen;
		private System.Windows.Forms.Label lbl_Funkrufname;
		private System.Windows.Forms.TextBox txt_Funkrufname;
		private System.Windows.Forms.TextBox txt_GST;
		private System.Windows.Forms.ComboBox cmb_OV;
		private System.Windows.Forms.ComboBox cmb_Kraeftestatus;
		private System.Windows.Forms.Label lbl_Kraeftestatus;
		private System.Windows.Forms.Label lbl_Betriebsverbrauch;
		private System.Windows.Forms.TextBox txt_Betriebsverbrauch;
		private System.Windows.Forms.Label lbl_EinheitsfuehrerID;
		private System.Windows.Forms.Label lbl_StellvertreterID;
		private System.Windows.Forms.ComboBox cmb_EinheitsfuehrerID;
		private System.Windows.Forms.ComboBox cmb_StellvertreterID;
		private System.Windows.Forms.TabPage tabpage_Helfer;
		private System.Windows.Forms.GroupBox gbx_Erreichbarkeit;
		private System.Windows.Forms.Label lbl_GST;
		public System.Windows.Forms.Button btn_Aktualisieren;
		private System.Windows.Forms.GroupBox gbx_Kommenter;
		private System.Windows.Forms.Label lbl_ModulID;
		private System.Windows.Forms.Label lbl_ESPID;
		private System.Windows.Forms.Label lbl_ModulIDLBL;
		private System.Windows.Forms.Label lb_ESPIDLBL;
		private System.Windows.Forms.RichTextBox txt_Kommentar;
		public System.ComponentModel.Container components = null;
		#endregion

		#region Konstruktor & Destruktor
		public usc_Einheit(Cst_EK pin_stEK)
		{
			this._stEK = pin_stEK;
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
			this.ep_Eingabe = new System.Windows.Forms.ErrorProvider();
			this.btn_Speichern = new System.Windows.Forms.Button();
			this.btn_Zuruecksetzen = new System.Windows.Forms.Button();
			this.gbx_Eingabemaske = new System.Windows.Forms.GroupBox();
			this.lbl_ModulID = new System.Windows.Forms.Label();
			this.lbl_ESPID = new System.Windows.Forms.Label();
			this.lbl_ModulIDLBL = new System.Windows.Forms.Label();
			this.lb_ESPIDLBL = new System.Windows.Forms.Label();
			this.gbx_Kommenter = new System.Windows.Forms.GroupBox();
			this.txt_Kommentar = new System.Windows.Forms.RichTextBox();
			this.cmb_StellvertreterID = new System.Windows.Forms.ComboBox();
			this.cmb_EinheitsfuehrerID = new System.Windows.Forms.ComboBox();
			this.lbl_StellvertreterID = new System.Windows.Forms.Label();
			this.lbl_EinheitsfuehrerID = new System.Windows.Forms.Label();
			this.txt_Betriebsverbrauch = new System.Windows.Forms.TextBox();
			this.lbl_Kraeftestatus = new System.Windows.Forms.Label();
			this.cmb_Kraeftestatus = new System.Windows.Forms.ComboBox();
			this.txt_IstStaerke = new System.Windows.Forms.TextBox();
			this.txt_Name = new System.Windows.Forms.TextBox();
			this.txt_Funkrufname = new System.Windows.Forms.TextBox();
			this.lbl_IstStaerke = new System.Windows.Forms.Label();
			this.lbl_Name = new System.Windows.Forms.Label();
			this.lbl_Funkrufname = new System.Windows.Forms.Label();
			this.txt_SollStaerke = new System.Windows.Forms.TextBox();
			this.lbl_SollStaerke = new System.Windows.Forms.Label();
			this.lbl_Betriebsverbrauch = new System.Windows.Forms.Label();
			this.lbl_OV = new System.Windows.Forms.Label();
			this.cmb_OV = new System.Windows.Forms.ComboBox();
			this.txt_GST = new System.Windows.Forms.TextBox();
			this.lbl_GST = new System.Windows.Forms.Label();
			this.gbx_Erreichbarkeit = new System.Windows.Forms.GroupBox();
			this.txt_Erreichbarkeit = new System.Windows.Forms.RichTextBox();
			this.tabctrl_Einheit = new System.Windows.Forms.TabControl();
			this.tabpage_Helfer = new System.Windows.Forms.TabPage();
			this.tabpage_Material = new System.Windows.Forms.TabPage();
			this.dtg_Material = new System.Windows.Forms.DataGrid();
			this.btn_Aktualisieren = new System.Windows.Forms.Button();
			this.gbx_Eingabemaske.SuspendLayout();
			this.gbx_Kommenter.SuspendLayout();
			this.gbx_Erreichbarkeit.SuspendLayout();
			this.tabctrl_Einheit.SuspendLayout();
			this.tabpage_Material.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtg_Material)).BeginInit();
			this.SuspendLayout();
			// 
			// ep_Eingabe
			// 
			this.ep_Eingabe.ContainerControl = this;
			// 
			// btn_Speichern
			// 
			this.btn_Speichern.Location = new System.Drawing.Point(536, 424);
			this.btn_Speichern.Name = "btn_Speichern";
			this.btn_Speichern.Size = new System.Drawing.Size(80, 25);
			this.btn_Speichern.TabIndex = 2;
			this.btn_Speichern.Text = "Speichern";
			this.btn_Speichern.Click += new System.EventHandler(this.btn_Speichern_Click);
			// 
			// btn_Zuruecksetzen
			// 
			this.btn_Zuruecksetzen.Location = new System.Drawing.Point(448, 424);
			this.btn_Zuruecksetzen.Name = "btn_Zuruecksetzen";
			this.btn_Zuruecksetzen.Size = new System.Drawing.Size(80, 25);
			this.btn_Zuruecksetzen.TabIndex = 3;
			this.btn_Zuruecksetzen.Text = "Zurücksetzen";
			this.btn_Zuruecksetzen.Click += new System.EventHandler(this.btn_Zuruecksetzen_Click);
			// 
			// gbx_Eingabemaske
			// 
			this.gbx_Eingabemaske.BackColor = System.Drawing.Color.White;
			this.gbx_Eingabemaske.Controls.Add(this.lbl_ModulID);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_ESPID);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_ModulIDLBL);
			this.gbx_Eingabemaske.Controls.Add(this.lb_ESPIDLBL);
			this.gbx_Eingabemaske.Controls.Add(this.gbx_Kommenter);
			this.gbx_Eingabemaske.Controls.Add(this.cmb_StellvertreterID);
			this.gbx_Eingabemaske.Controls.Add(this.cmb_EinheitsfuehrerID);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_StellvertreterID);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_EinheitsfuehrerID);
			this.gbx_Eingabemaske.Controls.Add(this.txt_Betriebsverbrauch);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_Kraeftestatus);
			this.gbx_Eingabemaske.Controls.Add(this.cmb_Kraeftestatus);
			this.gbx_Eingabemaske.Controls.Add(this.txt_IstStaerke);
			this.gbx_Eingabemaske.Controls.Add(this.txt_Name);
			this.gbx_Eingabemaske.Controls.Add(this.txt_Funkrufname);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_IstStaerke);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_Name);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_Funkrufname);
			this.gbx_Eingabemaske.Controls.Add(this.txt_SollStaerke);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_SollStaerke);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_Betriebsverbrauch);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_OV);
			this.gbx_Eingabemaske.Controls.Add(this.cmb_OV);
			this.gbx_Eingabemaske.Controls.Add(this.txt_GST);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_GST);
			this.gbx_Eingabemaske.Controls.Add(this.gbx_Erreichbarkeit);
			this.gbx_Eingabemaske.Location = new System.Drawing.Point(5, 5);
			this.gbx_Eingabemaske.Name = "gbx_Eingabemaske";
			this.gbx_Eingabemaske.Size = new System.Drawing.Size(610, 165);
			this.gbx_Eingabemaske.TabIndex = 0;
			this.gbx_Eingabemaske.TabStop = false;
			// 
			// lbl_ModulID
			// 
			this.lbl_ModulID.Location = new System.Drawing.Point(305, 130);
			this.lbl_ModulID.Name = "lbl_ModulID";
			this.lbl_ModulID.Size = new System.Drawing.Size(120, 15);
			this.lbl_ModulID.TabIndex = 30;
			// 
			// lbl_ESPID
			// 
			this.lbl_ESPID.Location = new System.Drawing.Point(305, 145);
			this.lbl_ESPID.Name = "lbl_ESPID";
			this.lbl_ESPID.Size = new System.Drawing.Size(115, 15);
			this.lbl_ESPID.TabIndex = 29;
			// 
			// lbl_ModulIDLBL
			// 
			this.lbl_ModulIDLBL.Location = new System.Drawing.Point(205, 125);
			this.lbl_ModulIDLBL.Name = "lbl_ModulIDLBL";
			this.lbl_ModulIDLBL.Size = new System.Drawing.Size(50, 15);
			this.lbl_ModulIDLBL.TabIndex = 28;
			this.lbl_ModulIDLBL.Text = "ModulID:";
			// 
			// lb_ESPIDLBL
			// 
			this.lb_ESPIDLBL.Location = new System.Drawing.Point(205, 145);
			this.lb_ESPIDLBL.Name = "lb_ESPIDLBL";
			this.lb_ESPIDLBL.Size = new System.Drawing.Size(45, 15);
			this.lb_ESPIDLBL.TabIndex = 27;
			this.lb_ESPIDLBL.Text = "ESPID:";
			// 
			// gbx_Kommenter
			// 
			this.gbx_Kommenter.Controls.Add(this.txt_Kommentar);
			this.gbx_Kommenter.Location = new System.Drawing.Point(435, 10);
			this.gbx_Kommenter.Name = "gbx_Kommenter";
			this.gbx_Kommenter.Size = new System.Drawing.Size(165, 150);
			this.gbx_Kommenter.TabIndex = 11;
			this.gbx_Kommenter.TabStop = false;
			this.gbx_Kommenter.Text = "Kommentar";
			// 
			// txt_Kommentar
			// 
			this.txt_Kommentar.Location = new System.Drawing.Point(5, 15);
			this.txt_Kommentar.Name = "txt_Kommentar";
			this.txt_Kommentar.Size = new System.Drawing.Size(155, 129);
			this.txt_Kommentar.TabIndex = 0;
			this.txt_Kommentar.Text = "";
			// 
			// cmb_StellvertreterID
			// 
			this.cmb_StellvertreterID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_StellvertreterID.Location = new System.Drawing.Point(305, 55);
			this.cmb_StellvertreterID.Name = "cmb_StellvertreterID";
			this.cmb_StellvertreterID.Size = new System.Drawing.Size(120, 21);
			this.cmb_StellvertreterID.TabIndex = 9;
			// 
			// cmb_EinheitsfuehrerID
			// 
			this.cmb_EinheitsfuehrerID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_EinheitsfuehrerID.Location = new System.Drawing.Point(305, 35);
			this.cmb_EinheitsfuehrerID.Name = "cmb_EinheitsfuehrerID";
			this.cmb_EinheitsfuehrerID.Size = new System.Drawing.Size(120, 21);
			this.cmb_EinheitsfuehrerID.TabIndex = 8;
			// 
			// lbl_StellvertreterID
			// 
			this.lbl_StellvertreterID.Location = new System.Drawing.Point(205, 55);
			this.lbl_StellvertreterID.Name = "lbl_StellvertreterID";
			this.lbl_StellvertreterID.Size = new System.Drawing.Size(100, 20);
			this.lbl_StellvertreterID.TabIndex = 21;
			this.lbl_StellvertreterID.Text = "StellvertreterID:";
			// 
			// lbl_EinheitsfuehrerID
			// 
			this.lbl_EinheitsfuehrerID.Location = new System.Drawing.Point(205, 35);
			this.lbl_EinheitsfuehrerID.Name = "lbl_EinheitsfuehrerID";
			this.lbl_EinheitsfuehrerID.Size = new System.Drawing.Size(100, 20);
			this.lbl_EinheitsfuehrerID.TabIndex = 20;
			this.lbl_EinheitsfuehrerID.Text = "EinheitsfuehrerID:";
			// 
			// txt_Betriebsverbrauch
			// 
			this.txt_Betriebsverbrauch.Location = new System.Drawing.Point(305, 15);
			this.txt_Betriebsverbrauch.Name = "txt_Betriebsverbrauch";
			this.txt_Betriebsverbrauch.Size = new System.Drawing.Size(120, 20);
			this.txt_Betriebsverbrauch.TabIndex = 7;
			this.txt_Betriebsverbrauch.Text = "";
			// 
			// lbl_Kraeftestatus
			// 
			this.lbl_Kraeftestatus.Location = new System.Drawing.Point(5, 55);
			this.lbl_Kraeftestatus.Name = "lbl_Kraeftestatus";
			this.lbl_Kraeftestatus.Size = new System.Drawing.Size(70, 20);
			this.lbl_Kraeftestatus.TabIndex = 17;
			this.lbl_Kraeftestatus.Text = "Kräftestatus:";
			// 
			// cmb_Kraeftestatus
			// 
			this.cmb_Kraeftestatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Kraeftestatus.Location = new System.Drawing.Point(75, 55);
			this.cmb_Kraeftestatus.Name = "cmb_Kraeftestatus";
			this.cmb_Kraeftestatus.Size = new System.Drawing.Size(120, 21);
			this.cmb_Kraeftestatus.TabIndex = 2;
			// 
			// txt_IstStaerke
			// 
			this.txt_IstStaerke.Location = new System.Drawing.Point(75, 75);
			this.txt_IstStaerke.Name = "txt_IstStaerke";
			this.txt_IstStaerke.Size = new System.Drawing.Size(120, 20);
			this.txt_IstStaerke.TabIndex = 3;
			this.txt_IstStaerke.Text = "";
			// 
			// txt_Name
			// 
			this.txt_Name.Location = new System.Drawing.Point(75, 35);
			this.txt_Name.Name = "txt_Name";
			this.txt_Name.Size = new System.Drawing.Size(120, 20);
			this.txt_Name.TabIndex = 1;
			this.txt_Name.Text = "";
			// 
			// txt_Funkrufname
			// 
			this.txt_Funkrufname.Location = new System.Drawing.Point(75, 15);
			this.txt_Funkrufname.Name = "txt_Funkrufname";
			this.txt_Funkrufname.Size = new System.Drawing.Size(120, 20);
			this.txt_Funkrufname.TabIndex = 0;
			this.txt_Funkrufname.Text = "";
			// 
			// lbl_IstStaerke
			// 
			this.lbl_IstStaerke.Location = new System.Drawing.Point(5, 75);
			this.lbl_IstStaerke.Name = "lbl_IstStaerke";
			this.lbl_IstStaerke.Size = new System.Drawing.Size(55, 20);
			this.lbl_IstStaerke.TabIndex = 5;
			this.lbl_IstStaerke.Text = "Ist Stärke:";
			// 
			// lbl_Name
			// 
			this.lbl_Name.Location = new System.Drawing.Point(5, 35);
			this.lbl_Name.Name = "lbl_Name";
			this.lbl_Name.Size = new System.Drawing.Size(50, 20);
			this.lbl_Name.TabIndex = 1;
			this.lbl_Name.Text = "Name:";
			// 
			// lbl_Funkrufname
			// 
			this.lbl_Funkrufname.Location = new System.Drawing.Point(5, 15);
			this.lbl_Funkrufname.Name = "lbl_Funkrufname";
			this.lbl_Funkrufname.Size = new System.Drawing.Size(75, 20);
			this.lbl_Funkrufname.TabIndex = 0;
			this.lbl_Funkrufname.Text = "Funkrufname:";
			// 
			// txt_SollStaerke
			// 
			this.txt_SollStaerke.Location = new System.Drawing.Point(75, 95);
			this.txt_SollStaerke.Name = "txt_SollStaerke";
			this.txt_SollStaerke.Size = new System.Drawing.Size(120, 20);
			this.txt_SollStaerke.TabIndex = 4;
			this.txt_SollStaerke.Text = "";
			// 
			// lbl_SollStaerke
			// 
			this.lbl_SollStaerke.Location = new System.Drawing.Point(5, 95);
			this.lbl_SollStaerke.Name = "lbl_SollStaerke";
			this.lbl_SollStaerke.Size = new System.Drawing.Size(65, 20);
			this.lbl_SollStaerke.TabIndex = 4;
			this.lbl_SollStaerke.Text = "Soll Stärke:";
			// 
			// lbl_Betriebsverbrauch
			// 
			this.lbl_Betriebsverbrauch.Location = new System.Drawing.Point(205, 15);
			this.lbl_Betriebsverbrauch.Name = "lbl_Betriebsverbrauch";
			this.lbl_Betriebsverbrauch.Size = new System.Drawing.Size(100, 20);
			this.lbl_Betriebsverbrauch.TabIndex = 18;
			this.lbl_Betriebsverbrauch.Text = "Betriebsverbrauch:";
			// 
			// lbl_OV
			// 
			this.lbl_OV.Location = new System.Drawing.Point(5, 135);
			this.lbl_OV.Name = "lbl_OV";
			this.lbl_OV.Size = new System.Drawing.Size(25, 20);
			this.lbl_OV.TabIndex = 3;
			this.lbl_OV.Text = "OV:";
			// 
			// cmb_OV
			// 
			this.cmb_OV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_OV.Location = new System.Drawing.Point(75, 135);
			this.cmb_OV.Name = "cmb_OV";
			this.cmb_OV.Size = new System.Drawing.Size(120, 21);
			this.cmb_OV.TabIndex = 6;
			// 
			// txt_GST
			// 
			this.txt_GST.Location = new System.Drawing.Point(75, 115);
			this.txt_GST.Name = "txt_GST";
			this.txt_GST.Size = new System.Drawing.Size(120, 20);
			this.txt_GST.TabIndex = 5;
			this.txt_GST.Text = "";
			// 
			// lbl_GST
			// 
			this.lbl_GST.Location = new System.Drawing.Point(5, 115);
			this.lbl_GST.Name = "lbl_GST";
			this.lbl_GST.Size = new System.Drawing.Size(40, 20);
			this.lbl_GST.TabIndex = 2;
			this.lbl_GST.Text = "GST:";
			// 
			// gbx_Erreichbarkeit
			// 
			this.gbx_Erreichbarkeit.Controls.Add(this.txt_Erreichbarkeit);
			this.gbx_Erreichbarkeit.Location = new System.Drawing.Point(205, 75);
			this.gbx_Erreichbarkeit.Name = "gbx_Erreichbarkeit";
			this.gbx_Erreichbarkeit.Size = new System.Drawing.Size(230, 50);
			this.gbx_Erreichbarkeit.TabIndex = 10;
			this.gbx_Erreichbarkeit.TabStop = false;
			this.gbx_Erreichbarkeit.Text = "Erreichbarkeit";
			// 
			// txt_Erreichbarkeit
			// 
			this.txt_Erreichbarkeit.Location = new System.Drawing.Point(5, 15);
			this.txt_Erreichbarkeit.Name = "txt_Erreichbarkeit";
			this.txt_Erreichbarkeit.Size = new System.Drawing.Size(215, 30);
			this.txt_Erreichbarkeit.TabIndex = 0;
			this.txt_Erreichbarkeit.Text = "";
			// 
			// tabctrl_Einheit
			// 
			this.tabctrl_Einheit.Controls.Add(this.tabpage_Helfer);
			this.tabctrl_Einheit.Controls.Add(this.tabpage_Material);
			this.tabctrl_Einheit.Location = new System.Drawing.Point(5, 175);
			this.tabctrl_Einheit.Name = "tabctrl_Einheit";
			this.tabctrl_Einheit.SelectedIndex = 0;
			this.tabctrl_Einheit.Size = new System.Drawing.Size(610, 245);
			this.tabctrl_Einheit.TabIndex = 1;
			// 
			// tabpage_Helfer
			// 
			this.tabpage_Helfer.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Helfer.Name = "tabpage_Helfer";
			this.tabpage_Helfer.Size = new System.Drawing.Size(602, 219);
			this.tabpage_Helfer.TabIndex = 0;
			this.tabpage_Helfer.Text = "Helfer";
			// 
			// tabpage_Material
			// 
			this.tabpage_Material.Controls.Add(this.dtg_Material);
			this.tabpage_Material.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Material.Name = "tabpage_Material";
			this.tabpage_Material.Size = new System.Drawing.Size(602, 219);
			this.tabpage_Material.TabIndex = 1;
			this.tabpage_Material.Text = "Material";
			// 
			// dtg_Material
			// 
			this.dtg_Material.DataMember = "";
			this.dtg_Material.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtg_Material.Location = new System.Drawing.Point(6, 4);
			this.dtg_Material.Name = "dtg_Material";
			this.dtg_Material.Size = new System.Drawing.Size(590, 280);
			this.dtg_Material.TabIndex = 1;
			// 
			// btn_Aktualisieren
			// 
			this.btn_Aktualisieren.Location = new System.Drawing.Point(5, 425);
			this.btn_Aktualisieren.Name = "btn_Aktualisieren";
			this.btn_Aktualisieren.Size = new System.Drawing.Size(80, 25);
			this.btn_Aktualisieren.TabIndex = 4;
			this.btn_Aktualisieren.Text = "Akualisieren";
			this.btn_Aktualisieren.Click += new System.EventHandler(this.btn_Aktualisieren_Click);
			// 
			// usc_Einheit
			// 
			this.Controls.Add(this.btn_Aktualisieren);
			this.Controls.Add(this.tabctrl_Einheit);
			this.Controls.Add(this.gbx_Eingabemaske);
			this.Controls.Add(this.btn_Zuruecksetzen);
			this.Controls.Add(this.btn_Speichern);
			this.Location = new System.Drawing.Point(6, 21);
			this.Name = "usc_Einheit";
			this.Size = new System.Drawing.Size(624, 456);
			this.gbx_Eingabemaske.ResumeLayout(false);
			this.gbx_Kommenter.ResumeLayout(false);
			this.gbx_Erreichbarkeit.ResumeLayout(false);
			this.tabctrl_Einheit.ResumeLayout(false);
			this.tabpage_Material.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtg_Material)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		
		#endregion

		#region Setze- Methode
		public void InitAlleSTE()
		{
			this.SetzePersonen();
			this.SetzeKraeftestatus();
			this.SetzeOV();
			this.SetzeMaterial(this._stEK.AlleMaterial,false);
			this.SetzeAlleDynSTE();
		}
		private void SetzeEvents()
		{
			this.cmb_StellvertreterID.SelectedValueChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_EinheitsfuehrerID.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Betriebsverbrauch.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_Kraeftestatus.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_IstStaerke.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Name.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Funkrufname.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_SollStaerke.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.cmb_OV.SelectedIndexChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_GST.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Erreichbarkeit.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Kommentar.TextChanged += new System.EventHandler(this.FelderModifiziert);
		}
		/// <summary>
		/// Datagrid Personen wird mit allen Helfer gefühllt. ComboBoxes EinheitsfueherID und
		/// StelltvertreterID werden mit IDs aller Helfer gefüllt.
		/// </summary>		
		private void SetzePersonen()
		{
			Cdv_Helfer[] alleHelfer = this._stEK.AlleHelfer;

			#region Setze Datagrid Personen/Helfer
			this._usc_dtg_Personen = new usc_dtg_Personen(this.tabpage_Helfer.Size.Width -5, this.tabpage_Helfer.Size.Height -5);
			this._usc_dtg_Personen.LadePersonen(alleHelfer);
			this.tabpage_Helfer.Controls.Add(this._usc_dtg_Personen);


			#endregion

			#region Setze EinheitsfuehrerID, StellvertreterID
			this.cmb_EinheitsfuehrerID.Items.Add("");
			this.cmb_StellvertreterID.Items.Add("");
			foreach(Cdv_Helfer helfer in alleHelfer)
			{
				this.cmb_EinheitsfuehrerID.Items.Add(helfer);
				this.cmb_StellvertreterID.Items.Add(helfer);
			}
			this.cmb_EinheitsfuehrerID.SelectedIndex = 0;
			this.cmb_StellvertreterID.SelectedIndex = 0;
			#endregion
		}
		private void SetzeKraeftestatus()
		{
			this.cmb_Kraeftestatus.Items.Clear();
			this.cmb_Kraeftestatus.Items.Add("");
			foreach(pELS.Tdv_Kraeftestatus ks in 
				Enum.GetValues(typeof(pELS.Tdv_Kraeftestatus)))
			{
				this.cmb_Kraeftestatus.Items.Add(ks);
			}
			this.cmb_Kraeftestatus.SelectedIndex = 0;
		}
		private void SetzeOV()
		{
			this.cmb_OV.Items.Clear();
			this.cmb_OV.Items.Add("");
			Cdv_Ortsverband[] OVMenge;
			if((OVMenge = this._stEK.AlleOVs) != null)
				foreach(Cdv_Ortsverband ov in OVMenge)
					this.cmb_OV.Items.Add(ov);
			this.cmb_OV.SelectedIndex = 0;
		}
		/// <summary>
		/// Datagrid Material wird mit allen Material gefüllt
		/// </summary>
		private void SetzeMaterial(Cdv_Material[] pin_Materialmenge, bool pin_istModifikationsModus)
		{
			DataTable dtable_Material = this.ErstelleMaterialTabelle();
			Cdv_Material[] alleMaterial = pin_Materialmenge;
			string str_AktuellerBesitzerKraft = "";
			string str_EingentuemerKraft = "";
			string tmp_Name = "";
			string tmp_Typ = "";
			Cdv_Einheit einheit;
			Cdv_KFZ kfz;
			Cdv_Helfer helfer;
			foreach(Cdv_Material material in alleMaterial)
			{
				#region Hole AktuellerBesitzerKraft
				einheit = null;
				kfz = null;
				helfer = null;

				einheit = this._stEK.HoleEinheit(material.AktuellerBesitzerKraftID);
				if(einheit != null)
				{
					tmp_Name = einheit.Name;
					tmp_Typ = "Einheit:";
				}
				else
				{
					kfz = this._stEK.HoleKfz(material.AktuellerBesitzerKraftID);
					if(kfz != null)
					{
						tmp_Name = kfz.Kennzeichen;
						tmp_Typ = "KFZ:";
					}
					else
					{
						helfer = this._stEK.HoleHelfer(material.AktuellerBesitzerKraftID);
						if(helfer != null)
						{
							tmp_Name = helfer.Personendaten.Vorname  + " " + helfer.Personendaten.Name;
							tmp_Typ = "Helfer:";
						}
					}
				}
				if(tmp_Name.CompareTo("") != 0)
				{
					str_AktuellerBesitzerKraft = tmp_Typ + " " + tmp_Name;				
				}
				#endregion

				#region Hole EingentuemerKraft
				einheit = null;
				kfz = null;
				helfer = null;


				einheit = this._stEK.HoleEinheit(material.EigentuemerKraftID);
				if(einheit != null)
				{
					tmp_Name = einheit.Name;
					tmp_Typ = "Einheit:";
				}
				else
				{
					kfz = this._stEK.HoleKfz(material.EigentuemerKraftID);
					if(kfz != null)
					{
						tmp_Name = kfz.Kennzeichen;
						tmp_Typ = "KFZ:";
					}
					else
					{
						helfer = this._stEK.HoleHelfer(material.EigentuemerKraftID);
						if(helfer != null)
						{
							tmp_Name = helfer.Personendaten.Vorname  + " " + helfer.Personendaten.Name;
							tmp_Typ = "Helfer:";
						}
					}
				}
				if(tmp_Name.CompareTo("") != 0)
				{
					str_EingentuemerKraft = tmp_Typ + " " + tmp_Name;				
				}

				#endregion



														// ID
				object[] obj_tabellezeile = new object[] { material.ID.ToString(),
														// Bezeichnung
															 material.Bezeichnung,
														// Menge
															 material.Menge.ToString(),
														// Lagerort
															 material.Lagerort,
														// Art
															 material.Art,
														// AktuellerBesitzerKraftID
															 str_AktuellerBesitzerKraft,
														// EigentuemerKraftID
															str_EingentuemerKraft
															
														};
				dtable_Material.Rows.Add(obj_tabellezeile);
			}
			if(pin_istModifikationsModus == false)
				this.dtg_Material.DataSource = dtable_Material;
			else
				this.dtg_geladeneMaterial.DataSource = dtable_Material;
		}

		// Die Eingabemaske hat zwei Modi, zum einen sind
		// alle Felder aktiv zum Datenspeichern, zum anderen
		// wird die Enable-Eigenschaft teilweiser Eingabefelder 
		// mit false zu setzen, dies hat den Zweck zum Modifizieren
		// der Daten. Falls pin_Modifizieren true ist, wird die
		// Eingabemaske mit Modifzierenmodi gesetzt.
		private void SetzeEingabefelderModi(bool pin_Modifizieren)
		{
			bool b_FuerModifikationsModus;
			bool b_FuerSpeichernModus;
			if(pin_Modifizieren == true)
			{
				b_FuerModifikationsModus = false;
				b_FuerSpeichernModus = true;
			}
			else
			{
				b_FuerModifikationsModus = true;
				b_FuerSpeichernModus = false;
			}

			this._usc_dtg_Personen.Visible = b_FuerModifikationsModus;
			this._usc_dtg_geladenePersonen.Visible = b_FuerSpeichernModus;

			this.dtg_Material.Visible = b_FuerModifikationsModus;
			this.dtg_geladeneMaterial.Visible = b_FuerSpeichernModus;

			this.btn_Aktualisieren.Enabled = b_FuerModifikationsModus;
			
			
		}
		private void SetzeAlleDynSTE()
		{
			// Helfer
			this._usc_dtg_geladenePersonen = new usc_dtg_Personen(this.tabpage_Helfer.Size.Width -5, this.tabpage_Helfer.Size.Height -5);
			_usc_dtg_geladenePersonen.Visible = false;
			this.tabpage_Helfer.Controls.Add(this._usc_dtg_geladenePersonen);		

			// Material
			this.dtg_geladeneMaterial.Visible = false;
			this.dtg_geladeneMaterial.Size = new System.Drawing.Size(this.dtg_Material.Size.Width,this.dtg_Material.Height);
			 
			this.tabpage_Material.Controls.Add(this.dtg_geladeneMaterial);

		}

		public void LadeEinheit(Cdv_Einheit pin_Einheit)
		{
			this._aktuelleEinheit = null;
			this._aktuelleEinheit= pin_Einheit;
			
			this.SetzeEingabefelderModi(true);

			// Lade Eingabefelder
			this.txt_Funkrufname .Text = pin_Einheit.Funkrufname ;
			this.txt_Name.Text = pin_Einheit.Name ;
			this.cmb_Kraeftestatus.Text = pin_Einheit.Kraeftestatus.ToString();
			this.txt_IstStaerke.Text = pin_Einheit.IstStaerke.ToString();
			this.txt_SollStaerke.Text = pin_Einheit.SollStaerke.ToString();
			this.txt_Betriebsverbrauch.Text = pin_Einheit.Betriebsverbrauch;
			
			#region foreach(object HelferItem in cmb_EinheitsfuehrerID.Items)
			foreach(object HelferItem in cmb_EinheitsfuehrerID.Items)
			{
				if (HelferItem is Cdv_Helfer)
				{
					if (pin_Einheit.EinheitenfuehrerHelferID == (HelferItem as Cdv_Helfer).ID)
					{
						this.cmb_EinheitsfuehrerID.SelectedItem=HelferItem;
						break;
					}
				}				
			}
			#endregion

			#region foreach(object HelferItem in cmb_StellvertreterID.Items)
			foreach(object HelferItem in cmb_StellvertreterID.Items)
			{
				if (HelferItem is Cdv_Helfer)
				{
					if (pin_Einheit.StellvertreterHelferID == (HelferItem as Cdv_Helfer).ID)
					{
						this.cmb_StellvertreterID.SelectedItem=HelferItem;
						break;
					}
				}				
			}
			#endregion

			this.txt_GST.Text = pin_Einheit.GST;

			#region foreach(object OVItem in cmb_OV.Items)

			foreach(object OVItem in cmb_OV.Items)
			{
				if (OVItem is Cdv_Ortsverband)
				{
					if (pin_Einheit.OVID == (OVItem as Cdv_Ortsverband).ID)
					{
						this.cmb_OV.SelectedItem=OVItem;
						break;
					}
				}				
			}
			#endregion

			this.txt_Erreichbarkeit.Text = pin_Einheit.Erreichbarkeit;
			
			this.lbl_ESPID.Text = pin_Einheit.EinsatzschwerpunktID.ToString();
			this.lbl_ModulID.Text = pin_Einheit.ModulID.ToString();

			this.txt_Kommentar.Text = "Autor:" + pin_Einheit.Kommentar.Autor + "\n" +
									  "Kommentar" + pin_Einheit.Kommentar.Text;

			// Lade Helfer
			int[] helferIDMenge = pin_Einheit.HelferIDMenge;
			if(helferIDMenge != null)
			{
				Cdv_Helfer[] geladeneHelfermenge = new Cdv_Helfer[helferIDMenge.Length];

				int i=0;
				foreach(int id in helferIDMenge)
				{
					geladeneHelfermenge[i] = this._stEK.HoleHelfer(id);
					i++;
				}
				this._usc_dtg_geladenePersonen.LadePersonen(geladeneHelfermenge);
			}

			// Lade Material
			Cdv_Material[] Materialmenge = this._stEK.HoleAlleMaterialZuEinheit(pin_Einheit.ID);
			this.SetzeMaterial(Materialmenge,true);
			
			this._b_FelderModifiziert = false;
		}

	
		private void Zuruecksetzen()
		{
			this.txt_Funkrufname .Text = "";
			this.txt_Name.Text = "";
			this.cmb_Kraeftestatus.Text = "";
			this.txt_IstStaerke.Text = "";
			this.txt_SollStaerke.Text = "";
			this.txt_Betriebsverbrauch.Text = "";
			this.cmb_EinheitsfuehrerID.Text = "";
			this.cmb_StellvertreterID.Text = "";
			this.txt_GST.Text = "";
			this.cmb_OV.Text = "";			
			this.txt_Erreichbarkeit.Text = "";
			this.txt_Kommentar.Text = "";

			this.lbl_ModulID.Text = "" ;
			this.lbl_ESPID.Text = "";

			this._b_FelderModifiziert = false;
			this.SetzeEingabefelderModi(false);
			this._aktuelleEinheit=null;
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


		#region Für Datagrid
		private DataTable ErstelleMaterialTabelle()
		{
			DataColumn[] dcol_a_Material = 
			{							
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_ID", "ID", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Bezeichnung", "Bezeichnung", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Menge", "Menge", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Lagerort", "Lagerort", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Art", "Art", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_AktuellerBesitzerKraftID", "AktuellerBesitzerKraftID", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_EigentuemerKraftID", "EigentuemerKraftID", "System.String"),
			};			
			DataTable dtable_Material = Cpr_EK_AllgFkt.ErstellenEinerDataTable("dtable_Material", dcol_a_Material);
			return dtable_Material;
		}

		private DataTable ErstelleVerbrauchsgueterTabelle()
		{
			DataColumn[] dcol_a_Verbrauchsgut = 
			{							
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_ID", "ID", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Bezeichnung", "Bezeichnung", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Menge", "Menge", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Lagerort", "Lagerort", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Art", "Art", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_spaetesterWbzpkt", "spaetesterWbzpkt", "System.String")
			};	
			DataTable dtable_Verbrauchsgut = Cpr_EK_AllgFkt.ErstellenEinerDataTable("dtable_Verbrauchsgut", dcol_a_Verbrauchsgut);
			return dtable_Verbrauchsgut;
		}



		#endregion
		#endregion

		#region Funktionalität

		/// <summary>
		/// Speichere eine Einheit. Die Daten, die in der DB nicht null sein dürfen, sind
		/// durch die Methode Eingabevalidierung() geprüft worden. Für die restlichen Daten,
		/// die nicht über die Eingabemaske gesetzt werden, sind mit Defaultwerten gesetzt.
		/// </summary>		
		private void SpeichereEinheit()
		{
			Cdv_Einheit pout_Einheit = new Cdv_Einheit();
			if (this._aktuelleEinheit!=null)
			{
				pout_Einheit = this._aktuelleEinheit;
			}
			pout_Einheit.Funkrufname =	this.txt_Funkrufname.Text ;
			pout_Einheit.Name =	this.txt_Name.Text;
			pout_Einheit.Kraeftestatus =	(Tdv_Kraeftestatus) Enum.Parse(typeof(Tdv_Kraeftestatus), this.cmb_Kraeftestatus.SelectedItem.ToString(),true);
			pout_Einheit.IstStaerke =	uint.Parse(this.txt_IstStaerke.Text);
			pout_Einheit.SollStaerke = uint.Parse(this.txt_SollStaerke.Text);
			pout_Einheit.Betriebsverbrauch =	this.txt_Betriebsverbrauch.Text;
			pout_Einheit.EinheitenfuehrerHelferID = (this.cmb_EinheitsfuehrerID.SelectedItem as Cdv_Helfer).ID;
			// Stellvertreter

			if((this.cmb_StellvertreterID.SelectedItem == null))
				pout_Einheit.StellvertreterHelferID = 0;
			else
				pout_Einheit.StellvertreterHelferID = (this.cmb_StellvertreterID.SelectedItem as Cdv_Helfer).ID;
			
			pout_Einheit.GST =	this.txt_GST.Text;
			// OV
			if(this.cmb_OV.SelectedItem.ToString().CompareTo("")==0)
				pout_Einheit.OVID = 0;
			else
				pout_Einheit.OVID = (this.cmb_OV.SelectedItem as Cdv_Ortsverband).ID;			

			pout_Einheit.Erreichbarkeit =	this.txt_Erreichbarkeit.Text;

			// HelferIDMenge		
			int[] HelferIDMenge;
			if(this.cmb_StellvertreterID.SelectedItem == null)
			{
				HelferIDMenge = new int[1];
				HelferIDMenge[0] = (this.cmb_EinheitsfuehrerID.SelectedItem as Cdv_Helfer).ID;
			}
			else
			{
				HelferIDMenge = new int[2];
				HelferIDMenge[0] = (this.cmb_EinheitsfuehrerID.SelectedItem as Cdv_Helfer).ID;
				HelferIDMenge[1] = (this.cmb_StellvertreterID.SelectedItem as Cdv_Helfer).ID;
			}
			pout_Einheit.HelferIDMenge = HelferIDMenge;

			//Kommentar
			pout_Einheit.Kommentar.Autor = this._stEK.HoleAktBenutzer().Benutzername;
			pout_Einheit.Kommentar.Text = this.txt_Kommentar.Text;

			this._stEK.SpeichereEinheit(pout_Einheit);

		}

		#endregion

		#region Eingabevalidierung
		/// <summary>
		/// überprüft alle zwingend benötigten Eingaben auf Korrektheit
		/// </summary>
		/// <returns></returns>
		virtual protected bool Eingabevalidierung()
		{
			bool b_Eingabepruefung;
		    b_Eingabepruefung = 
				ValidiereFunkrufname() 
				&& ValidiereName()
				&& ValidiereKraeftestatus()
				&& ValidiereIstStaerke()
				&& ValidiereSollStaerke() 
				&& ValidiereEinheitsfueherID()
				&& this.ValidiereFuehrerStellvertreterID()
				&& ValidiereStellvertreterID();

			return b_Eingabepruefung;
		}

		#region Stellv. und Einheitsf.

		protected bool ValidiereFuehrerStellvertreterID()
		{
			Cdv_Helfer hStellvertreter = null;
			Cdv_Helfer hFuehrer = null;
			if(this.cmb_StellvertreterID.Text != "")
				hStellvertreter = (Cdv_Helfer) this.cmb_StellvertreterID.SelectedItem;
			if(this.cmb_EinheitsfuehrerID.Text != "")
				hFuehrer = (Cdv_Helfer) this.cmb_EinheitsfuehrerID.SelectedItem;
			if(hFuehrer == null || hStellvertreter == null)
				return(false);
			if(hFuehrer.ID == hStellvertreter.ID)
			{
				ep_Eingabe.SetError(this.cmb_EinheitsfuehrerID, "Einheitsführer kann kein Stellvertreter sein");
				ep_Eingabe.SetError(this.cmb_StellvertreterID, "Stellvertreter kann kein Einheitsführer sein");
				return(false);
			}
			ep_Eingabe.SetError(this.cmb_EinheitsfuehrerID, "");
			ep_Eingabe.SetError(this.cmb_StellvertreterID, "");
			return(true);
		}

		#endregion

		#region Funktrufname
		/// <summary>
		/// überprüfe den Funkrufname
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereFunkrufname()
		{
			bool b = false;
			if((b = (txt_Funkrufname.Text.Length > 0)) == true)
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(txt_Funkrufname, "");			
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(txt_Funkrufname, "Bitte geben Sie einen Text ein");
			}
			return b;
		}
		#endregion

		#region Name
		protected bool ValidiereName()
		{
			bool b = false;
			if((b = (txt_Name.Text.Length > 0)) == true)
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(txt_Name, "");			
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(txt_Name, "Bitte geben Sie einen Text ein");
			}
			return b;
		}

		#endregion

		#region cmb_Kraeftestatus
		/// <summary>
		/// überprüfe den Kraeftestatus
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereKraeftestatus()
		{
			bool b = false;
			if(b = (cmb_Kraeftestatus.SelectedItem.ToString().CompareTo("")!=0))
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(cmb_Kraeftestatus, "");			
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(cmb_Kraeftestatus, "Bitte wählen Sie einen Eintrag aus");
			}
			return b;
		}
		#endregion

		#region txt_IstStaerke

		/// <summary>
		/// überprüfe den istStaerke
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereIstStaerke()
		{
			bool b = false;
			if(b = (txt_IstStaerke.Text.Length > 0))
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(txt_IstStaerke, "");			

				try
				{
					uint.Parse(txt_IstStaerke.Text);
				}
				catch(Exception)
				{
					ep_Eingabe.SetError(txt_IstStaerke, "Bitte geben eine Zahl ein");
				}
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(txt_IstStaerke, "Bitte geben Sie eine Zahl ein");
			}
			return b;
		}

		#endregion

		#region txt_SollStaerke
		/// <summary>
		/// überprüfe den SollStaerke
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereSollStaerke()
		{
			bool b = false;
			if(b = (txt_SollStaerke.Text.Length > 0))
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(txt_SollStaerke, "");			

				try
				{
					uint.Parse(txt_SollStaerke.Text);
				}
				catch(Exception)
				{
					ep_Eingabe.SetError(txt_SollStaerke, "Bitte geben eine Zahl ein");
				}
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(txt_SollStaerke, "Bitte geben Sie eine Zahl ein");
			}
			return b;
		}
	
		#endregion

		#region cmb_EinheitsfuehrerID
		/// <summary>
		/// überprüfe den EinheitsfueherID
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereEinheitsfueherID()
		{
			bool b = false;
			if(b = (cmb_EinheitsfuehrerID.SelectedItem!=null))
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(cmb_EinheitsfuehrerID, "");			
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(cmb_EinheitsfuehrerID, "Bitte wählen Sie einen Eintrag aus");
			}
			return b;
		}
		
		#endregion

		#region cmb_StellvertreterID
		/// <summary>
		/// überprüfe den StellverterID
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereStellvertreterID()
		{
			bool b = false;
			if(b = (cmb_StellvertreterID.SelectedItem != null))
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(cmb_StellvertreterID, "");			
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(cmb_StellvertreterID, "Bitte wählen Sie einen Eintrag aus");
			}
			return b;
		}
		
		#endregion


		#endregion						

		#region unknown
//		private void ctx_Einheiten_Popup(object sender, System.EventArgs e)
//		{
//			
//		}

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


		private void btn_Zuruecksetzen_Click(object sender, System.EventArgs e)
		{
			if(this._b_FelderModifiziert == true && (CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes))
			{
				this.Zuruecksetzen();
			}
	
		}

		private void btn_Speichern_Click(object sender, System.EventArgs e)
		{
			if(this.Eingabevalidierung() == true)
			{
				this.SpeichereEinheit();
				this.Zuruecksetzen();
			}
			else
			{
				pELS.GUI.PopUp.CPopUp.MeldenVonFalscherEingabe();
			}

		}
		
	
		private void btn_Aktualisieren_Click(object sender, System.EventArgs e)
		{
			if(this.tabctrl_Einheit.SelectedTab.Name == "tabpage_Helfer")
				this._usc_dtg_Personen.LadePersonen(this._stEK.AlleHelfer);
			if(this.tabctrl_Einheit.SelectedTab.Name == "tabpage_Material")
				this.SetzeMaterial(this._stEK.AlleMaterial,false);
		}

		#endregion

		#region für dynamische Datenakualisierung
		/// <summary>
		/// Wenn die Menge aller Helfer verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AktualisiereHelfer()
		{
			this.cmb_EinheitsfuehrerID.Items.Clear();
			this.cmb_StellvertreterID.Items.Clear();
			this.SetzePersonen();
		}
		/// <summary>
		/// Wenn die Menge aller OV verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>		
		public void AktualisiereOV()
		{
			this.SetzeOV();
		}
		/// <summary>
		/// Wenn die Menge aller Material verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>		
		public void AktualisiereMaterial()
		{
			this.SetzeMaterial(this._stEK.AlleMaterial,false);
		}
		#endregion

		private void cmb_EinheitsfuehrerID_Leave(object sender, System.EventArgs e)
		{
			this.ValidiereFuehrerStellvertreterID();
		}

		private void cmb_StellvertreterID_Leave(object sender, System.EventArgs e)
		{
			this.ValidiereFuehrerStellvertreterID();
		}

		
	}
}