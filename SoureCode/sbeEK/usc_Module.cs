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
	public class usc_Module : System.Windows.Forms.UserControl
	{
		#region eigene Variablen
		/// <summary>
		/// gibt an, ob bereits Eingaben geschehen sind
		/// </summary>
		protected bool _b_FelderModifiziert = false;

		protected Cdv_Modul _modul = null;

		protected System.Windows.Forms.ErrorProvider ep_Eingabe = new System.Windows.Forms.ErrorProvider();

		private Cst_EK _stEK;
		#endregion
	
		#region graphische Variablen

		public System.Windows.Forms.CheckBox cbx_Auftrag_IstUebermittelt;
		public System.Windows.Forms.Button btn_Auftrag_AuftragSpeichern;
		public System.Windows.Forms.Button btn_Auftrag_AuftragZuruecksetzen;
		private System.Windows.Forms.TextBox txt_Kraefte_Kfz_Kommentar;
		private System.Windows.Forms.GroupBox gbx_Kommentar;
		private System.Windows.Forms.TabPage tabpage_Fahrer;
		private System.Windows.Forms.DataGrid dtg_EinsatzBetriebsstunden;
		private System.Windows.Forms.TabPage tabPage_EinsatzBetriebsstunden;
		private System.Windows.Forms.DataGrid dtg_Fahrer;
		private System.Windows.Forms.GroupBox gbx_Eingabemaske;
		private System.Windows.Forms.TextBox txt_Name;
		private System.Windows.Forms.Label lbl_Name;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.DataGrid dtg_EinheitenHelferKFZ;
		public System.ComponentModel.Container components = null;
		#endregion

	
		#region Konstruktor & Destruktor
		public usc_Module(Cst_EK pin_stEK)
		{
			this._stEK = pin_stEK;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

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
			this.cbx_Auftrag_IstUebermittelt = new System.Windows.Forms.CheckBox();
			this.btn_Auftrag_AuftragSpeichern = new System.Windows.Forms.Button();
			this.btn_Auftrag_AuftragZuruecksetzen = new System.Windows.Forms.Button();
			this.txt_Kraefte_Kfz_Kommentar = new System.Windows.Forms.TextBox();
			this.dtg_EinsatzBetriebsstunden = new System.Windows.Forms.DataGrid();
			this.gbx_Kommentar = new System.Windows.Forms.GroupBox();
			this.tabpage_Fahrer = new System.Windows.Forms.TabPage();
			this.tabPage_EinsatzBetriebsstunden = new System.Windows.Forms.TabPage();
			this.dtg_Fahrer = new System.Windows.Forms.DataGrid();
			this.dtg_EinheitenHelferKFZ = new System.Windows.Forms.DataGrid();
			this.txt_Name = new System.Windows.Forms.TextBox();
			this.lbl_Name = new System.Windows.Forms.Label();
			this.gbx_Eingabemaske = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.dtg_EinsatzBetriebsstunden)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtg_Fahrer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtg_EinheitenHelferKFZ)).BeginInit();
			this.gbx_Eingabemaske.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbx_Auftrag_IstUebermittelt
			// 
			this.cbx_Auftrag_IstUebermittelt.Location = new System.Drawing.Point(0, 0);
			this.cbx_Auftrag_IstUebermittelt.Name = "cbx_Auftrag_IstUebermittelt";
			this.cbx_Auftrag_IstUebermittelt.TabIndex = 0;
			// 
			// btn_Auftrag_AuftragSpeichern
			// 
			this.btn_Auftrag_AuftragSpeichern.Location = new System.Drawing.Point(536, 424);
			this.btn_Auftrag_AuftragSpeichern.Name = "btn_Auftrag_AuftragSpeichern";
			this.btn_Auftrag_AuftragSpeichern.Size = new System.Drawing.Size(80, 25);
			this.btn_Auftrag_AuftragSpeichern.TabIndex = 18;
			this.btn_Auftrag_AuftragSpeichern.Text = "Speichern";
			this.btn_Auftrag_AuftragSpeichern.Click += new System.EventHandler(this.btn_Auftrag_AuftragSpeichern_Click);
			// 
			// btn_Auftrag_AuftragZuruecksetzen
			// 
			this.btn_Auftrag_AuftragZuruecksetzen.Location = new System.Drawing.Point(448, 424);
			this.btn_Auftrag_AuftragZuruecksetzen.Name = "btn_Auftrag_AuftragZuruecksetzen";
			this.btn_Auftrag_AuftragZuruecksetzen.Size = new System.Drawing.Size(80, 25);
			this.btn_Auftrag_AuftragZuruecksetzen.TabIndex = 19;
			this.btn_Auftrag_AuftragZuruecksetzen.Text = "Zurücksetzen";
			this.btn_Auftrag_AuftragZuruecksetzen.Click += new System.EventHandler(this.btn_Auftrag_AuftragZuruecksetzen_Click);
			// 
			// txt_Kraefte_Kfz_Kommentar
			// 
			this.txt_Kraefte_Kfz_Kommentar.Location = new System.Drawing.Point(0, 0);
			this.txt_Kraefte_Kfz_Kommentar.Name = "txt_Kraefte_Kfz_Kommentar";
			this.txt_Kraefte_Kfz_Kommentar.TabIndex = 0;
			this.txt_Kraefte_Kfz_Kommentar.Text = "";
			// 
			// dtg_EinsatzBetriebsstunden
			// 
			this.dtg_EinsatzBetriebsstunden.DataMember = "";
			this.dtg_EinsatzBetriebsstunden.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtg_EinsatzBetriebsstunden.Location = new System.Drawing.Point(0, 0);
			this.dtg_EinsatzBetriebsstunden.Name = "dtg_EinsatzBetriebsstunden";
			this.dtg_EinsatzBetriebsstunden.TabIndex = 0;
			// 
			// gbx_Kommentar
			// 
			this.gbx_Kommentar.Location = new System.Drawing.Point(0, 0);
			this.gbx_Kommentar.Name = "gbx_Kommentar";
			this.gbx_Kommentar.TabIndex = 0;
			this.gbx_Kommentar.TabStop = false;
			// 
			// tabpage_Fahrer
			// 
			this.tabpage_Fahrer.Location = new System.Drawing.Point(0, 0);
			this.tabpage_Fahrer.Name = "tabpage_Fahrer";
			this.tabpage_Fahrer.TabIndex = 0;
			// 
			// tabPage_EinsatzBetriebsstunden
			// 
			this.tabPage_EinsatzBetriebsstunden.Location = new System.Drawing.Point(0, 0);
			this.tabPage_EinsatzBetriebsstunden.Name = "tabPage_EinsatzBetriebsstunden";
			this.tabPage_EinsatzBetriebsstunden.TabIndex = 0;
			// 
			// dtg_Fahrer
			// 
			this.dtg_Fahrer.DataMember = "";
			this.dtg_Fahrer.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtg_Fahrer.Location = new System.Drawing.Point(0, 0);
			this.dtg_Fahrer.Name = "dtg_Fahrer";
			this.dtg_Fahrer.TabIndex = 0;
			// 
			// dtg_EinheitenHelferKFZ
			// 
			this.dtg_EinheitenHelferKFZ.DataMember = "";
			this.dtg_EinheitenHelferKFZ.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtg_EinheitenHelferKFZ.Location = new System.Drawing.Point(5, 20);
			this.dtg_EinheitenHelferKFZ.Name = "dtg_EinheitenHelferKFZ";
			this.dtg_EinheitenHelferKFZ.Size = new System.Drawing.Size(605, 335);
			this.dtg_EinheitenHelferKFZ.TabIndex = 25;
			this.dtg_EinheitenHelferKFZ.Navigate += new System.Windows.Forms.NavigateEventHandler(this.dtg_EinheitenHelferKFZ_Navigate);
			// 
			// txt_Name
			// 
			this.txt_Name.Location = new System.Drawing.Point(55, 15);
			this.txt_Name.Name = "txt_Name";
			this.txt_Name.Size = new System.Drawing.Size(140, 20);
			this.txt_Name.TabIndex = 21;
			this.txt_Name.Text = "";
			this.txt_Name.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Name.Leave += new System.EventHandler(this.txt_Name_Leave);
			// 
			// lbl_Name
			// 
			this.lbl_Name.Location = new System.Drawing.Point(5, 15);
			this.lbl_Name.Name = "lbl_Name";
			this.lbl_Name.Size = new System.Drawing.Size(55, 17);
			this.lbl_Name.TabIndex = 20;
			this.lbl_Name.Text = "Name:";
			// 
			// gbx_Eingabemaske
			// 
			this.gbx_Eingabemaske.BackColor = System.Drawing.Color.White;
			this.gbx_Eingabemaske.Controls.Add(this.txt_Name);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_Name);
			this.gbx_Eingabemaske.Location = new System.Drawing.Point(5, 5);
			this.gbx_Eingabemaske.Name = "gbx_Eingabemaske";
			this.gbx_Eingabemaske.Size = new System.Drawing.Size(610, 50);
			this.gbx_Eingabemaske.TabIndex = 26;
			this.gbx_Eingabemaske.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.dtg_EinheitenHelferKFZ);
			this.groupBox1.Location = new System.Drawing.Point(5, 55);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(615, 360);
			this.groupBox1.TabIndex = 27;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Einheiten/Helfer/KFZ";
			// 
			// usc_Module
			// 
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gbx_Eingabemaske);
			this.Controls.Add(this.btn_Auftrag_AuftragZuruecksetzen);
			this.Controls.Add(this.btn_Auftrag_AuftragSpeichern);
			this.Location = new System.Drawing.Point(6, 21);
			this.Name = "usc_Module";
			this.Size = new System.Drawing.Size(624, 456);
			((System.ComponentModel.ISupportInitialize)(this.dtg_EinsatzBetriebsstunden)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtg_Fahrer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtg_EinheitenHelferKFZ)).EndInit();
			this.gbx_Eingabemaske.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void dtg_EinheitenHelferKFZ_Navigate(object sender, System.Windows.Forms.NavigateEventArgs ne)
		{
		
		}

		
		#endregion

		#region Setze- Methode
		public void InitAlleSTE()
		{
		}

		private void Zuruecksetzen()
		{		
			this.txt_Name.Text = "";
			this.dtg_EinheitenHelferKFZ.DataSource = null;
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

		public bool FelderIstModifiziert
		{
			get
			{
				return(this._b_FelderModifiziert);
			}
		}

		public void LadeModul(Cdv_Modul pin_modul)
		{
			this.txt_Name.Text = pin_modul.Modulname;
			this._modul = pin_modul;
			Cdv_Einheit[] einheiten = this._stEK.HoleEinheitenZumModul(pin_modul);
			if(einheiten != null)
			{
				DataTable dtableDaten = this.ErstelleTabelleFuerDaten();
				IEnumerator ie = einheiten.GetEnumerator();
				while(ie.MoveNext())
				{
					Cdv_Einheit einheit = (Cdv_Einheit) ie.Current;
					this.LadeEinheit(einheit, dtableDaten);
					Cdv_Helfer[] helfermenge = this._stEK.HoleHelferZurEinheit(einheit.ID);
					if(helfermenge != null)
					{
						IEnumerator ieHelfer = helfermenge.GetEnumerator();
						while(ieHelfer.MoveNext())
						{
							Cdv_Helfer helfer = (Cdv_Helfer) ieHelfer.Current;
							this.LadeHelfer(helfer, dtableDaten);
						}
					}	
					
					int[] iaKFZIDs = einheit.KfzKraefteIDMenge;
					if (iaKFZIDs!=null)
					{
						IEnumerator ieKFZ = iaKFZIDs.GetEnumerator();
						while(ieKFZ.MoveNext())
						{
							int iID = (int) ieKFZ.Current;
							Cdv_KFZ kfz = this._stEK.HoleKfz(iID);
							if(kfz != null)
								this.LadeKfz(kfz, dtableDaten);
						}
					}
				}
				
				this.dtg_EinheitenHelferKFZ.DataSource = dtableDaten;
			}
			this._b_FelderModifiziert = false;
		}

		#region event handler

		private void btn_Auftrag_AuftragSpeichern_Click(object sender, System.EventArgs e)
		{
			if(CPopUp.SpeichernOhneUeberschreiben() == DialogResult.OK)
			{
				if(!this.EingabevalidierungModul())
					return;
				Cdv_Modul modul = new Cdv_Modul(this.txt_Name.Text);
				if(this._modul != null)
					modul.ID = this._modul.ID;
				this._stEK.SpeichereModul(modul);
				this._b_FelderModifiziert = false;
				this._modul = null;
				this.Zuruecksetzen();
			}
		}

		private void btn_Auftrag_AuftragZuruecksetzen_Click(object sender, System.EventArgs e)
		{
			if(this._b_FelderModifiziert == true && (CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes))
			{
				this.Zuruecksetzen();
			}		
		}

		private void txt_Name_Leave(object sender, System.EventArgs e)
		{
			this.txt_ModulName_Validated_Kfz(null, null);
		}

		private void FelderModifiziert(object sender, System.EventArgs e)
		{
			this._b_FelderModifiziert = true;
		}

		#endregion

		#region Eingabevalidierung

		private bool EingabevalidierungModul()
		{
			if(this.ValidiereModulName())
				return(true);
			this.txt_ModulName_Validated_Kfz(null, null);
			return(false);
		}

		private bool ValidiereModulName()
		{
			return(this.txt_Name.Text.Length > 0);
		}

		private void txt_ModulName_Validated_Kfz(object sender, System.EventArgs e)
		{
			if(this.ValidiereModulName())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_Name, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_Name, "Bitte geben Sie einen Modulnamen ein");
			}
		}

		#endregion

		#region datagrid

		private void LadeKfz(Cdv_KFZ pin_kfz, DataTable dtableDaten)
		{
			object[] obj_tabellezeile = new object[] {   pin_kfz.ID.ToString(),
														 // Name
														 pin_kfz.Funkrufname,
														 // Helferstatus
														 pin_kfz.Kraeftestatus.ToString(),
														 // Typ
														 "Kfz"
													 };
			dtableDaten.Rows.Add(obj_tabellezeile);
		}

		private void LadeHelfer(Cdv_Helfer pin_helfer, DataTable dtableDaten)
		{
			object[] obj_tabellezeile = new object[] {   pin_helfer.ID.ToString(),
														 // Name
														 pin_helfer.Personendaten.Name,
														 // Helferstatus
														 pin_helfer.Helferstatus.ToString(),
														 // Typ
														 "Helfer"
													 };
			dtableDaten.Rows.Add(obj_tabellezeile);
		}

		private void LadeEinheit(Cdv_Einheit pin_einheit, DataTable dtableDaten)
		{
			object[] obj_tabellezeile = new object[] {   pin_einheit.ID.ToString(),
														 // Name
														 pin_einheit.Name,
														 // Helferstatus
														 pin_einheit.Kraeftestatus.ToString(),
														 // Typ
														 "Einheit"
													 };
			dtableDaten.Rows.Add(obj_tabellezeile);
		}

		private DataTable ErstelleTabelleFuerDaten()
		{
			DataColumn[] dcol_a_Person = 
			{								
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_ID", "ID", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Name", "Name", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Kraeftestatus", "Kräftestatus", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Typ", "Kraeftetyp", "System.String")
			};			
			DataTable dtable_Person = Cpr_EK_AllgFkt.ErstellenEinerDataTable("dtable_Personen", dcol_a_Person);
			return dtable_Person;
		}

		#endregion

	}
}