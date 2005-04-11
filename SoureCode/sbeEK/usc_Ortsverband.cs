

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

	public class usc_Ortsverband : System.Windows.Forms.UserControl
	{
		#region eigene Variablen

	
		private Cst_EK _stEK;

		/// <summary>
		/// ermöglicht das Anzeigen von fehlerhaften Eingaben
		/// </summary>
		protected System.Windows.Forms.ErrorProvider ep_Eingabe = new System.Windows.Forms.ErrorProvider();
		/// <summary>
		/// gibt an, ob bereits Eingaben geschehen sind
		/// </summary>
		protected bool _b_FelderModifiziert = false;

		private int _aktuelleOVID;
#endregion
	

		#region grafische Variablen
	
		private System.Windows.Forms.Button btn_Speichern;
		private System.Windows.Forms.Button btn_Zuruecksetzen;
		private System.Windows.Forms.TextBox txt_Ortsbeauftragter;
		private System.Windows.Forms.TextBox txt_Landesverband;
		private System.Windows.Forms.Label lbl_Ortsbeauftragter;
		private System.Windows.Forms.Label lbl_Landesverband;
		private System.Windows.Forms.TextBox txt_Geschaeftsfuehrerbereich;
		private System.Windows.Forms.TextBox txt_OVName;
		private System.Windows.Forms.GroupBox gbx_Geschaeftsfuehreranschrift;
		private System.Windows.Forms.Label lbl_GAPLZ;
		private System.Windows.Forms.Label lbl_GAHausNr;
		protected System.Windows.Forms.TextBox txt_GAStrasse;
		protected System.Windows.Forms.TextBox txt_GAStadt;
		protected System.Windows.Forms.TextBox txt_GAPLZ;
		protected System.Windows.Forms.TextBox txt_GAHausNr;
		private System.Windows.Forms.Label lbl_GAStrasse;
		private System.Windows.Forms.Label lbl_GAStadt;
		private System.Windows.Forms.GroupBox gbx_Erreichbarkeit;
		private System.Windows.Forms.RichTextBox txt_Erreichbarkeit;
		private System.Windows.Forms.GroupBox gbx_OVAnschrift;
		private System.Windows.Forms.Label lbl_PLZLBL;
		private System.Windows.Forms.Label lbl_Nr;
		protected System.Windows.Forms.TextBox txt_Strasse;
		protected System.Windows.Forms.TextBox txt_Ort;
		protected System.Windows.Forms.TextBox txt_PLZ;
		protected System.Windows.Forms.TextBox txt_HausNr;
		private System.Windows.Forms.Label lbl_StrasseLBL;
		private System.Windows.Forms.Label lbl_StadtLBL;
		private System.Windows.Forms.Label lbl_Geschaeftsfuehrerbereich;
		private System.Windows.Forms.Label lbl_OVName;
		private System.Windows.Forms.DataGrid dtg_OVGrid;
		private System.Windows.Forms.GroupBox gbx_Eingabemaske;

		
		#endregion

		
		#region Konstruktor & Destruktor
		public usc_Ortsverband(Cst_EK pin_stEK)
		{
			this._stEK = pin_stEK;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.ep_Eingabe.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.InitAlleSBE();
			this._b_FelderModifiziert = false;

			FuelleDatagrid(this._stEK.AlleOVs);
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

		
		private System.ComponentModel.Container components = null;
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btn_Speichern = new System.Windows.Forms.Button();
			this.btn_Zuruecksetzen = new System.Windows.Forms.Button();
			this.txt_Ortsbeauftragter = new System.Windows.Forms.TextBox();
			this.txt_Landesverband = new System.Windows.Forms.TextBox();
			this.lbl_Ortsbeauftragter = new System.Windows.Forms.Label();
			this.lbl_Landesverband = new System.Windows.Forms.Label();
			this.txt_Geschaeftsfuehrerbereich = new System.Windows.Forms.TextBox();
			this.txt_OVName = new System.Windows.Forms.TextBox();
			this.gbx_Geschaeftsfuehreranschrift = new System.Windows.Forms.GroupBox();
			this.lbl_GAPLZ = new System.Windows.Forms.Label();
			this.lbl_GAHausNr = new System.Windows.Forms.Label();
			this.txt_GAStrasse = new System.Windows.Forms.TextBox();
			this.txt_GAStadt = new System.Windows.Forms.TextBox();
			this.txt_GAPLZ = new System.Windows.Forms.TextBox();
			this.txt_GAHausNr = new System.Windows.Forms.TextBox();
			this.lbl_GAStrasse = new System.Windows.Forms.Label();
			this.lbl_GAStadt = new System.Windows.Forms.Label();
			this.gbx_Erreichbarkeit = new System.Windows.Forms.GroupBox();
			this.txt_Erreichbarkeit = new System.Windows.Forms.RichTextBox();
			this.gbx_OVAnschrift = new System.Windows.Forms.GroupBox();
			this.lbl_PLZLBL = new System.Windows.Forms.Label();
			this.lbl_Nr = new System.Windows.Forms.Label();
			this.txt_Strasse = new System.Windows.Forms.TextBox();
			this.txt_Ort = new System.Windows.Forms.TextBox();
			this.txt_PLZ = new System.Windows.Forms.TextBox();
			this.txt_HausNr = new System.Windows.Forms.TextBox();
			this.lbl_StrasseLBL = new System.Windows.Forms.Label();
			this.lbl_StadtLBL = new System.Windows.Forms.Label();
			this.lbl_Geschaeftsfuehrerbereich = new System.Windows.Forms.Label();
			this.lbl_OVName = new System.Windows.Forms.Label();
			this.gbx_Eingabemaske = new System.Windows.Forms.GroupBox();
			this.dtg_OVGrid = new System.Windows.Forms.DataGrid();
			this.gbx_Geschaeftsfuehreranschrift.SuspendLayout();
			this.gbx_Erreichbarkeit.SuspendLayout();
			this.gbx_OVAnschrift.SuspendLayout();
			this.gbx_Eingabemaske.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtg_OVGrid)).BeginInit();
			this.SuspendLayout();
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
			// txt_Ortsbeauftragter
			// 
			this.txt_Ortsbeauftragter.Location = new System.Drawing.Point(425, 55);
			this.txt_Ortsbeauftragter.Name = "txt_Ortsbeauftragter";
			this.txt_Ortsbeauftragter.Size = new System.Drawing.Size(170, 20);
			this.txt_Ortsbeauftragter.TabIndex = 3;
			this.txt_Ortsbeauftragter.Text = "";
			this.txt_Ortsbeauftragter.TabIndexChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// txt_Landesverband
			// 
			this.txt_Landesverband.Location = new System.Drawing.Point(425, 35);
			this.txt_Landesverband.Name = "txt_Landesverband";
			this.txt_Landesverband.Size = new System.Drawing.Size(170, 20);
			this.txt_Landesverband.TabIndex = 2;
			this.txt_Landesverband.Text = "";
			this.txt_Landesverband.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// lbl_Ortsbeauftragter
			// 
			this.lbl_Ortsbeauftragter.BackColor = System.Drawing.Color.White;
			this.lbl_Ortsbeauftragter.Location = new System.Drawing.Point(300, 55);
			this.lbl_Ortsbeauftragter.Name = "lbl_Ortsbeauftragter";
			this.lbl_Ortsbeauftragter.Size = new System.Drawing.Size(90, 20);
			this.lbl_Ortsbeauftragter.TabIndex = 83;
			this.lbl_Ortsbeauftragter.Text = "Ortsbeauftragter:";
			// 
			// lbl_Landesverband
			// 
			this.lbl_Landesverband.BackColor = System.Drawing.Color.White;
			this.lbl_Landesverband.Location = new System.Drawing.Point(300, 35);
			this.lbl_Landesverband.Name = "lbl_Landesverband";
			this.lbl_Landesverband.Size = new System.Drawing.Size(85, 20);
			this.lbl_Landesverband.TabIndex = 82;
			this.lbl_Landesverband.Text = "Landesverband:";
			// 
			// txt_Geschaeftsfuehrerbereich
			// 
			this.txt_Geschaeftsfuehrerbereich.Location = new System.Drawing.Point(425, 15);
			this.txt_Geschaeftsfuehrerbereich.Name = "txt_Geschaeftsfuehrerbereich";
			this.txt_Geschaeftsfuehrerbereich.Size = new System.Drawing.Size(170, 20);
			this.txt_Geschaeftsfuehrerbereich.TabIndex = 1;
			this.txt_Geschaeftsfuehrerbereich.Text = "";
			this.txt_Geschaeftsfuehrerbereich.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// txt_OVName
			// 
			this.txt_OVName.Location = new System.Drawing.Point(70, 15);
			this.txt_OVName.Name = "txt_OVName";
			this.txt_OVName.Size = new System.Drawing.Size(170, 20);
			this.txt_OVName.TabIndex = 0;
			this.txt_OVName.Text = "";
			this.txt_OVName.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// gbx_Geschaeftsfuehreranschrift
			// 
			this.gbx_Geschaeftsfuehreranschrift.BackColor = System.Drawing.Color.White;
			this.gbx_Geschaeftsfuehreranschrift.Controls.Add(this.lbl_GAPLZ);
			this.gbx_Geschaeftsfuehreranschrift.Controls.Add(this.lbl_GAHausNr);
			this.gbx_Geschaeftsfuehreranschrift.Controls.Add(this.txt_GAStrasse);
			this.gbx_Geschaeftsfuehreranschrift.Controls.Add(this.txt_GAStadt);
			this.gbx_Geschaeftsfuehreranschrift.Controls.Add(this.txt_GAPLZ);
			this.gbx_Geschaeftsfuehreranschrift.Controls.Add(this.txt_GAHausNr);
			this.gbx_Geschaeftsfuehreranschrift.Controls.Add(this.lbl_GAStrasse);
			this.gbx_Geschaeftsfuehreranschrift.Controls.Add(this.lbl_GAStadt);
			this.gbx_Geschaeftsfuehreranschrift.Location = new System.Drawing.Point(300, 85);
			this.gbx_Geschaeftsfuehreranschrift.Name = "gbx_Geschaeftsfuehreranschrift";
			this.gbx_Geschaeftsfuehreranschrift.Size = new System.Drawing.Size(235, 60);
			this.gbx_Geschaeftsfuehreranschrift.TabIndex = 77;
			this.gbx_Geschaeftsfuehreranschrift.TabStop = false;
			this.gbx_Geschaeftsfuehreranschrift.Text = "Geschäftsführeranschrift";
			// 
			// lbl_GAPLZ
			// 
			this.lbl_GAPLZ.Location = new System.Drawing.Point(8, 40);
			this.lbl_GAPLZ.Name = "lbl_GAPLZ";
			this.lbl_GAPLZ.Size = new System.Drawing.Size(28, 16);
			this.lbl_GAPLZ.TabIndex = 8;
			this.lbl_GAPLZ.Text = "PLZ";
			// 
			// lbl_GAHausNr
			// 
			this.lbl_GAHausNr.Location = new System.Drawing.Point(180, 20);
			this.lbl_GAHausNr.Name = "lbl_GAHausNr";
			this.lbl_GAHausNr.Size = new System.Drawing.Size(20, 16);
			this.lbl_GAHausNr.TabIndex = 6;
			this.lbl_GAHausNr.Text = "Nr.";
			// 
			// txt_GAStrasse
			// 
			this.txt_GAStrasse.Location = new System.Drawing.Point(44, 16);
			this.txt_GAStrasse.MaxLength = 50;
			this.txt_GAStrasse.Name = "txt_GAStrasse";
			this.txt_GAStrasse.Size = new System.Drawing.Size(131, 20);
			this.txt_GAStrasse.TabIndex = 0;
			this.txt_GAStrasse.Text = "";
			this.txt_GAStrasse.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// txt_GAStadt
			// 
			this.txt_GAStadt.Location = new System.Drawing.Point(120, 36);
			this.txt_GAStadt.MaxLength = 50;
			this.txt_GAStadt.Name = "txt_GAStadt";
			this.txt_GAStadt.Size = new System.Drawing.Size(105, 20);
			this.txt_GAStadt.TabIndex = 3;
			this.txt_GAStadt.Text = "";
			this.txt_GAStadt.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// txt_GAPLZ
			// 
			this.txt_GAPLZ.Location = new System.Drawing.Point(44, 36);
			this.txt_GAPLZ.MaxLength = 5;
			this.txt_GAPLZ.Name = "txt_GAPLZ";
			this.txt_GAPLZ.Size = new System.Drawing.Size(40, 20);
			this.txt_GAPLZ.TabIndex = 2;
			this.txt_GAPLZ.Text = "";
			this.txt_GAPLZ.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// txt_GAHausNr
			// 
			this.txt_GAHausNr.Location = new System.Drawing.Point(200, 16);
			this.txt_GAHausNr.MaxLength = 5;
			this.txt_GAHausNr.Name = "txt_GAHausNr";
			this.txt_GAHausNr.Size = new System.Drawing.Size(28, 20);
			this.txt_GAHausNr.TabIndex = 1;
			this.txt_GAHausNr.Text = "";
			this.txt_GAHausNr.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// lbl_GAStrasse
			// 
			this.lbl_GAStrasse.Location = new System.Drawing.Point(4, 20);
			this.lbl_GAStrasse.Name = "lbl_GAStrasse";
			this.lbl_GAStrasse.Size = new System.Drawing.Size(40, 16);
			this.lbl_GAStrasse.TabIndex = 7;
			this.lbl_GAStrasse.Text = "Straße";
			// 
			// lbl_GAStadt
			// 
			this.lbl_GAStadt.Location = new System.Drawing.Point(85, 40);
			this.lbl_GAStadt.Name = "lbl_GAStadt";
			this.lbl_GAStadt.Size = new System.Drawing.Size(36, 16);
			this.lbl_GAStadt.TabIndex = 9;
			this.lbl_GAStadt.Text = "Stadt";
			// 
			// gbx_Erreichbarkeit
			// 
			this.gbx_Erreichbarkeit.BackColor = System.Drawing.Color.White;
			this.gbx_Erreichbarkeit.Controls.Add(this.txt_Erreichbarkeit);
			this.gbx_Erreichbarkeit.Location = new System.Drawing.Point(10, 105);
			this.gbx_Erreichbarkeit.Name = "gbx_Erreichbarkeit";
			this.gbx_Erreichbarkeit.Size = new System.Drawing.Size(235, 60);
			this.gbx_Erreichbarkeit.TabIndex = 76;
			this.gbx_Erreichbarkeit.TabStop = false;
			this.gbx_Erreichbarkeit.Text = "OVErreichbarkeit";
			// 
			// txt_Erreichbarkeit
			// 
			this.txt_Erreichbarkeit.Location = new System.Drawing.Point(5, 15);
			this.txt_Erreichbarkeit.Name = "txt_Erreichbarkeit";
			this.txt_Erreichbarkeit.Size = new System.Drawing.Size(220, 40);
			this.txt_Erreichbarkeit.TabIndex = 0;
			this.txt_Erreichbarkeit.Text = "";
			this.txt_Erreichbarkeit.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// gbx_OVAnschrift
			// 
			this.gbx_OVAnschrift.BackColor = System.Drawing.Color.White;
			this.gbx_OVAnschrift.Controls.Add(this.lbl_PLZLBL);
			this.gbx_OVAnschrift.Controls.Add(this.lbl_Nr);
			this.gbx_OVAnschrift.Controls.Add(this.txt_Strasse);
			this.gbx_OVAnschrift.Controls.Add(this.txt_Ort);
			this.gbx_OVAnschrift.Controls.Add(this.txt_PLZ);
			this.gbx_OVAnschrift.Controls.Add(this.txt_HausNr);
			this.gbx_OVAnschrift.Controls.Add(this.lbl_StrasseLBL);
			this.gbx_OVAnschrift.Controls.Add(this.lbl_StadtLBL);
			this.gbx_OVAnschrift.Location = new System.Drawing.Point(5, 40);
			this.gbx_OVAnschrift.Name = "gbx_OVAnschrift";
			this.gbx_OVAnschrift.Size = new System.Drawing.Size(235, 60);
			this.gbx_OVAnschrift.TabIndex = 74;
			this.gbx_OVAnschrift.TabStop = false;
			this.gbx_OVAnschrift.Text = "OVAnschrift";
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
			this.lbl_Nr.TabIndex = 6;
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
			this.txt_Ort.TextChanged += new System.EventHandler(this.FelderModifiziert);
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
			// lbl_Geschaeftsfuehrerbereich
			// 
			this.lbl_Geschaeftsfuehrerbereich.BackColor = System.Drawing.Color.White;
			this.lbl_Geschaeftsfuehrerbereich.Location = new System.Drawing.Point(300, 15);
			this.lbl_Geschaeftsfuehrerbereich.Name = "lbl_Geschaeftsfuehrerbereich";
			this.lbl_Geschaeftsfuehrerbereich.Size = new System.Drawing.Size(125, 20);
			this.lbl_Geschaeftsfuehrerbereich.TabIndex = 1;
			this.lbl_Geschaeftsfuehrerbereich.Text = "Geschäftsführerbereich:";
			// 
			// lbl_OVName
			// 
			this.lbl_OVName.BackColor = System.Drawing.Color.White;
			this.lbl_OVName.Location = new System.Drawing.Point(10, 15);
			this.lbl_OVName.Name = "lbl_OVName";
			this.lbl_OVName.Size = new System.Drawing.Size(60, 20);
			this.lbl_OVName.TabIndex = 0;
			this.lbl_OVName.Text = "OVName:";
			// 
			// gbx_Eingabemaske
			// 
			this.gbx_Eingabemaske.BackColor = System.Drawing.Color.White;
			this.gbx_Eingabemaske.Controls.Add(this.txt_Ortsbeauftragter);
			this.gbx_Eingabemaske.Controls.Add(this.txt_Landesverband);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_Ortsbeauftragter);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_Landesverband);
			this.gbx_Eingabemaske.Controls.Add(this.txt_Geschaeftsfuehrerbereich);
			this.gbx_Eingabemaske.Controls.Add(this.txt_OVName);
			this.gbx_Eingabemaske.Controls.Add(this.gbx_Geschaeftsfuehreranschrift);
			this.gbx_Eingabemaske.Controls.Add(this.gbx_Erreichbarkeit);
			this.gbx_Eingabemaske.Controls.Add(this.gbx_OVAnschrift);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_Geschaeftsfuehrerbereich);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_OVName);
			this.gbx_Eingabemaske.Location = new System.Drawing.Point(5, 0);
			this.gbx_Eingabemaske.Name = "gbx_Eingabemaske";
			this.gbx_Eingabemaske.Size = new System.Drawing.Size(615, 180);
			this.gbx_Eingabemaske.TabIndex = 0;
			this.gbx_Eingabemaske.TabStop = false;
			// 
			// dtg_OVGrid
			// 
			this.dtg_OVGrid.AllowSorting = false;
			this.dtg_OVGrid.DataMember = "";
			this.dtg_OVGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtg_OVGrid.Location = new System.Drawing.Point(5, 190);
			this.dtg_OVGrid.Name = "dtg_OVGrid";
			this.dtg_OVGrid.ReadOnly = true;
			this.dtg_OVGrid.Size = new System.Drawing.Size(605, 225);
			this.dtg_OVGrid.TabIndex = 78;
			this.dtg_OVGrid.Click += new System.EventHandler(this.dtg_OVGrid_Click);
			// 
			// usc_Ortsverband
			// 
			this.Controls.Add(this.dtg_OVGrid);
			this.Controls.Add(this.btn_Zuruecksetzen);
			this.Controls.Add(this.btn_Speichern);
			this.Controls.Add(this.gbx_Eingabemaske);
			this.Name = "usc_Ortsverband";
			this.Size = new System.Drawing.Size(624, 456);
			this.gbx_Geschaeftsfuehreranschrift.ResumeLayout(false);
			this.gbx_Erreichbarkeit.ResumeLayout(false);
			this.gbx_OVAnschrift.ResumeLayout(false);
			this.gbx_Eingabemaske.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtg_OVGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#endregion


		#region Setze- Methode
		
		private void InitAlleSBE()
		{
		}
		private void SetzeEingabefelderModi(bool pin_Modifizieren)
		{
			//TODO
			// Ein Beispiel, damit dies nicht vergessen werden.
			
			
		}

	
		private void Zuruecksetzen()
		{
			this._aktuelleOVID=-1;
			this.txt_OVName.Text = "";
			this.txt_Strasse.Text = "";
			this.txt_HausNr.Text = "";
			this.txt_Ort.Text = "";
			this.txt_PLZ.Text = "";
			this.txt_Erreichbarkeit.Text = "";

			this.txt_Geschaeftsfuehrerbereich.Text = "";
			this.txt_Landesverband.Text = "";
			this.txt_Ortsbeauftragter.Text = "";
			this.txt_GAStrasse.Text = "";
			this.txt_GAHausNr.Text = "";
			this.txt_GAStadt.Text = "";
			this.txt_GAPLZ.Text = "";

			this.SetzeEingabefelderModi(true);
			this._b_FelderModifiziert = false;
			this.FuelleDatagrid(this._stEK.AlleOVs);
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


		#region Funktionalität
		private void SpeichereOV()
		{
			Cdv_Ortsverband ov = new Cdv_Ortsverband(this.txt_OVName.Text);
			if (this._aktuelleOVID>0)
			{
				ov.ID=_aktuelleOVID;
			}
	
			ov.OVName =	this.txt_OVName.Text ;
			ov.OVAnschrift.Strasse = this.txt_Strasse.Text;
			ov.OVAnschrift.Hausnummer =	this.txt_HausNr.Text ;
			ov.OVAnschrift.Ort =	this.txt_Ort.Text; 
			ov.OVAnschrift.PLZ =	this.txt_PLZ.Text ;
			ov.OVErreichbarkeit =	this.txt_Erreichbarkeit.Text;

			ov.Geschaeftsfuehrerbereich =	this.txt_Geschaeftsfuehrerbereich.Text;
			ov.Landesverband =	this.txt_Landesverband.Text ;
			ov.Ortsbeauftragter =	this.txt_Ortsbeauftragter.Text; 
			ov.Geschaeftsfuehreranschrift.Strasse =	this.txt_GAStrasse.Text ;
			ov.Geschaeftsfuehreranschrift.Hausnummer =	this.txt_GAHausNr.Text ;
			ov.Geschaeftsfuehreranschrift.Ort =	this.txt_GAStadt.Text ;
			ov.Geschaeftsfuehreranschrift.PLZ =	this.txt_GAPLZ.Text ;
			
			this._stEK.SpeichereOV(ov);

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
			b_Eingabepruefung = ValidiereOVName(); 
			
			return b_Eingabepruefung;
		}
		#region txt_SollStaerke
		/// <summary>
		/// überprüfe den SollStaerke
		/// </summary>
		/// <returns></returns>
		protected bool ValidiereOVName()
		{
			bool b = false;
			if(b = (txt_OVName.Text.Length > 0))
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(txt_OVName, "");						
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(txt_OVName, "Bitte geben Sie einen Namen ein");
			}
			return b;
		}
	
		#endregion

		#endregion						


		#region Datagrid Methoden
		// Spalten für eine Tabelle erstellen
		private DataColumn ErstellenEinerDataColumn(string pin_str_Name, string pin_str_Caption, string pin_str_Type) 
		{
			// Type der Spalte generieren
			System.Type type_meinType = Type.GetType(pin_str_Type);						
			if (type_meinType == null)
			{
				//TODO: Löschen MessageBox
				MessageBox.Show("type = null");
				return null;
			}
			// Neue Spalte erstellen 
			DataColumn pout_dcol_Spalte = new DataColumn(pin_str_Name, type_meinType);
			pout_dcol_Spalte.ReadOnly = true; 
			pout_dcol_Spalte.ColumnName = pin_str_Caption;			
			
			return pout_dcol_Spalte;
		}

		
		// Tabelle für ein DataGrid erstellen
		private DataTable ErstellenEinerDataTable(string pin_str_Name, DataColumn[] pin_dcol_a_Spalten) 
		{			
			DataTable pout_dtable_meineTabelle = new DataTable(pin_str_Name);

			pout_dtable_meineTabelle.Columns.AddRange(pin_dcol_a_Spalten);		
			return pout_dtable_meineTabelle;
		}


		public DataTable ErstelleDatagridOVs()
		{
			// Spalten generieren
			DataColumn[] dcol_a_OVs = 
			{								
				ErstellenEinerDataColumn("dcol_Termine_ID", "ID", "System.String"),
				// Wenn der Name "ID" verändert werden soll, muss in der Methode "HoleAusgewaehlteAuftragsID"
				ErstellenEinerDataColumn("dcol_OVName", "OV Name", "System.String"),
				ErstellenEinerDataColumn("dcol_OVPLZ", "PLZ", "System.String"),
				ErstellenEinerDataColumn("dcol_OVOrt", "Ort", "System.String"),
				ErstellenEinerDataColumn("dcol_OVStrasse", "Strasse", "System.String"),
				ErstellenEinerDataColumn("dcol_OVNummer", "Nr.", "System.String"),
				ErstellenEinerDataColumn("dcol_OVName", "Landesverband", "System.String")
			};

			DataTable dtable_OVs = ErstellenEinerDataTable("dtable_Ortsverbände", dcol_a_OVs);
			return dtable_OVs;
		}


		private void FuelleDatagrid(Cdv_Ortsverband[] pin_OVmenge)
		{	
			DataTable dtable_OVs = this.ErstelleDatagridOVs();
			foreach(Cdv_Ortsverband myOV in pin_OVmenge)
			{
				object[] obj_tabellezeile = new object[] {	myOV.ID.ToString(),
															myOV.OVName.ToString(),
															 myOV.OVAnschrift.PLZ.ToString(),
															 myOV.OVAnschrift.Ort.ToString(),
															 myOV.OVAnschrift.Strasse.ToString(),
															 myOV.OVAnschrift.Hausnummer.ToString(),
															 myOV.Landesverband.ToString()
															
														 };

				dtable_OVs.Rows.Add(obj_tabellezeile);

			}
			dtg_OVGrid.DataSource= dtable_OVs;
		}

		#endregion


		#region get-Methoden
		public bool FelderIstModifiziert
		{
			get{return this._b_FelderModifiziert;}
		}
		#endregion

		
		#region eventhandler

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
			if(this.Eingabevalidierung() == true)
			{
				this.SpeichereOV();
				this.Zuruecksetzen();
			}
		}

		private void btn_Zuruecksetzen_Click(object sender, System.EventArgs e)
		{
			if((this._b_FelderModifiziert == true) && pELS.GUI.PopUp.CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes)
			{
				this.Zuruecksetzen();
			}
		}
		
	
		#endregion

		private void dtg_OVGrid_Click(object sender, System.EventArgs e)
		{
			
			CurrencyManager xCM = (CurrencyManager)this.dtg_OVGrid.BindingContext
				[this.dtg_OVGrid.DataSource, this.dtg_OVGrid.DataMember];
			DataTable dtable_OV = this.dtg_OVGrid.DataSource as DataTable;
			int Zeile = dtg_OVGrid.CurrentRowIndex;


			if (Zeile>-1)
			{
				int i_IndexOfID = dtable_OV.Columns.IndexOf("ID");		
				object[] ZeilenInhalte = (dtable_OV.Rows[Zeile].ItemArray);
				//MessageBox.Show(ZeilenInhalte[i_IndexOfID].ToString());

				int OVID=System.Convert.ToInt32(ZeilenInhalte[i_IndexOfID].ToString());
				this._aktuelleOVID=OVID;

				foreach(Cdv_Ortsverband OV in this._stEK.AlleOVs)
				{
					if (OV.ID==OVID)
					{
						this.txt_GAHausNr.Text=OV.Geschaeftsfuehreranschrift.Hausnummer;
						this.txt_GAStadt.Text=OV.Geschaeftsfuehreranschrift.Ort;
						this.txt_GAPLZ.Text=OV.Geschaeftsfuehreranschrift.PLZ;
						this.txt_GAStrasse.Text=OV.Geschaeftsfuehreranschrift.Strasse;
						this.txt_Geschaeftsfuehrerbereich.Text=OV.Geschaeftsfuehrerbereich;
						this.txt_Landesverband.Text=OV.Landesverband;
						this.txt_Ortsbeauftragter.Text=OV.Ortsbeauftragter;
						this.txt_HausNr.Text=OV.OVAnschrift.Hausnummer;
						this.txt_Ort.Text=OV.OVAnschrift.Ort;
						this.txt_PLZ.Text=OV.OVAnschrift.PLZ;
						this.txt_Strasse.Text=OV.OVAnschrift.Strasse;
						this.txt_Erreichbarkeit.Text=OV.OVErreichbarkeit;
						this.txt_OVName.Text=OV.OVName;
					}
				}
			}
		}

	}
}


