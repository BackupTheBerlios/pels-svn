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
	public class usc_Kfz : System.Windows.Forms.UserControl
	{
		#region eigene Variablen
		/// <summary>
		/// gibt an, ob bereits Eingaben geschehen sind
		/// </summary>
		protected bool _b_FelderModifiziert = false;

		private Cdv_Einheit _einheit = null;

		private bool _bFahrerAusgewaehlt = false;

		private int _iFahrerHelferID = -1;

		private Cst_EK _stEK;

		private Cdv_KFZ _kfz = null;

		protected System.Windows.Forms.ErrorProvider ep_Eingabe = new System.Windows.Forms.ErrorProvider();
		#endregion
	
		#region graphische Variablen

		private System.Windows.Forms.Button btn_Speichern;
		private System.Windows.Forms.Button btn_Zuruecksetzen;
		private System.Windows.Forms.GroupBox gbx_Eingabemaske;
		private System.Windows.Forms.TextBox txt_Funkrufname;
		private System.Windows.Forms.Label lbl_Funkrufname;
		private System.Windows.Forms.TextBox txt_KfzTyp;
		private System.Windows.Forms.Label lbl_KfzTyp;
		private System.Windows.Forms.TextBox txt_Kennzeichen;
		private System.Windows.Forms.Label lbl_Kennzeichnen;
		private System.Windows.Forms.GroupBox gbx_Kommentar;
		private System.Windows.Forms.TextBox txt_Kommentar;
		private System.Windows.Forms.TabControl tabControltabctrl_Kfz;
		private System.Windows.Forms.TabPage tabPage_Fahrer;
		private System.Windows.Forms.DataGrid dtg_Fahrer;
		private System.Windows.Forms.ComboBox cmb_KrfStatus;
		private System.Windows.Forms.Label label1;
		public System.ComponentModel.Container components = null;

		#endregion

		#region Konstruktor & Destruktor
		public usc_Kfz(Cst_EK pin_stEK)
		{
			this._stEK = pin_stEK;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.InitAlleSTE();
			this.ep_Eingabe.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
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
			this.btn_Speichern = new System.Windows.Forms.Button();
			this.btn_Zuruecksetzen = new System.Windows.Forms.Button();
			this.gbx_Eingabemaske = new System.Windows.Forms.GroupBox();
			this.gbx_Kommentar = new System.Windows.Forms.GroupBox();
			this.txt_Kommentar = new System.Windows.Forms.TextBox();
			this.txt_Funkrufname = new System.Windows.Forms.TextBox();
			this.lbl_Funkrufname = new System.Windows.Forms.Label();
			this.txt_KfzTyp = new System.Windows.Forms.TextBox();
			this.lbl_KfzTyp = new System.Windows.Forms.Label();
			this.txt_Kennzeichen = new System.Windows.Forms.TextBox();
			this.lbl_Kennzeichnen = new System.Windows.Forms.Label();
			this.tabControltabctrl_Kfz = new System.Windows.Forms.TabControl();
			this.tabPage_Fahrer = new System.Windows.Forms.TabPage();
			this.dtg_Fahrer = new System.Windows.Forms.DataGrid();
			this.cmb_KrfStatus = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.gbx_Eingabemaske.SuspendLayout();
			this.gbx_Kommentar.SuspendLayout();
			this.tabControltabctrl_Kfz.SuspendLayout();
			this.tabPage_Fahrer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtg_Fahrer)).BeginInit();
			this.SuspendLayout();
			// 
			// btn_Speichern
			// 
			this.btn_Speichern.Location = new System.Drawing.Point(525, 425);
			this.btn_Speichern.Name = "btn_Speichern";
			this.btn_Speichern.Size = new System.Drawing.Size(90, 25);
			this.btn_Speichern.TabIndex = 17;
			this.btn_Speichern.Text = "Speichern";
			this.btn_Speichern.Click += new System.EventHandler(this.btn_Speichern_Click);
			// 
			// btn_Zuruecksetzen
			// 
			this.btn_Zuruecksetzen.Location = new System.Drawing.Point(430, 425);
			this.btn_Zuruecksetzen.Name = "btn_Zuruecksetzen";
			this.btn_Zuruecksetzen.Size = new System.Drawing.Size(90, 25);
			this.btn_Zuruecksetzen.TabIndex = 18;
			this.btn_Zuruecksetzen.Text = "Zurücksetzen";
			this.btn_Zuruecksetzen.Click += new System.EventHandler(this.btn_Zuruecksetzen_Click);
			// 
			// gbx_Eingabemaske
			// 
			this.gbx_Eingabemaske.BackColor = System.Drawing.Color.White;
			this.gbx_Eingabemaske.Controls.Add(this.cmb_KrfStatus);
			this.gbx_Eingabemaske.Controls.Add(this.label1);
			this.gbx_Eingabemaske.Controls.Add(this.gbx_Kommentar);
			this.gbx_Eingabemaske.Controls.Add(this.txt_Funkrufname);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_Funkrufname);
			this.gbx_Eingabemaske.Controls.Add(this.txt_KfzTyp);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_KfzTyp);
			this.gbx_Eingabemaske.Controls.Add(this.txt_Kennzeichen);
			this.gbx_Eingabemaske.Controls.Add(this.lbl_Kennzeichnen);
			this.gbx_Eingabemaske.Location = new System.Drawing.Point(5, 5);
			this.gbx_Eingabemaske.Name = "gbx_Eingabemaske";
			this.gbx_Eingabemaske.Size = new System.Drawing.Size(615, 115);
			this.gbx_Eingabemaske.TabIndex = 19;
			this.gbx_Eingabemaske.TabStop = false;
			// 
			// gbx_Kommentar
			// 
			this.gbx_Kommentar.Controls.Add(this.txt_Kommentar);
			this.gbx_Kommentar.Location = new System.Drawing.Point(235, 10);
			this.gbx_Kommentar.Name = "gbx_Kommentar";
			this.gbx_Kommentar.Size = new System.Drawing.Size(370, 85);
			this.gbx_Kommentar.TabIndex = 14;
			this.gbx_Kommentar.TabStop = false;
			this.gbx_Kommentar.Text = "Kommentar";
			// 
			// txt_Kommentar
			// 
			this.txt_Kommentar.Location = new System.Drawing.Point(10, 15);
			this.txt_Kommentar.Multiline = true;
			this.txt_Kommentar.Name = "txt_Kommentar";
			this.txt_Kommentar.Size = new System.Drawing.Size(345, 65);
			this.txt_Kommentar.TabIndex = 13;
			this.txt_Kommentar.Text = "";
			this.txt_Kommentar.TextChanged += new System.EventHandler(this.FelderModifiziert);
			// 
			// txt_Funkrufname
			// 
			this.txt_Funkrufname.Location = new System.Drawing.Point(85, 35);
			this.txt_Funkrufname.Name = "txt_Funkrufname";
			this.txt_Funkrufname.Size = new System.Drawing.Size(140, 20);
			this.txt_Funkrufname.TabIndex = 13;
			this.txt_Funkrufname.Text = "";
			this.txt_Funkrufname.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Funkrufname.Leave += new System.EventHandler(this.txt_Funkrufname_Leave);
			// 
			// lbl_Funkrufname
			// 
			this.lbl_Funkrufname.Location = new System.Drawing.Point(10, 35);
			this.lbl_Funkrufname.Name = "lbl_Funkrufname";
			this.lbl_Funkrufname.Size = new System.Drawing.Size(75, 20);
			this.lbl_Funkrufname.TabIndex = 12;
			this.lbl_Funkrufname.Text = "Funkrufname:";
			// 
			// txt_KfzTyp
			// 
			this.txt_KfzTyp.Location = new System.Drawing.Point(85, 55);
			this.txt_KfzTyp.Name = "txt_KfzTyp";
			this.txt_KfzTyp.Size = new System.Drawing.Size(140, 20);
			this.txt_KfzTyp.TabIndex = 11;
			this.txt_KfzTyp.Text = "";
			this.txt_KfzTyp.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_KfzTyp.Leave += new System.EventHandler(this.txt_KfzTyp_Leave);
			// 
			// lbl_KfzTyp
			// 
			this.lbl_KfzTyp.Location = new System.Drawing.Point(10, 55);
			this.lbl_KfzTyp.Name = "lbl_KfzTyp";
			this.lbl_KfzTyp.Size = new System.Drawing.Size(55, 20);
			this.lbl_KfzTyp.TabIndex = 10;
			this.lbl_KfzTyp.Text = "Kfz Typ:";
			// 
			// txt_Kennzeichen
			// 
			this.txt_Kennzeichen.Location = new System.Drawing.Point(85, 15);
			this.txt_Kennzeichen.Name = "txt_Kennzeichen";
			this.txt_Kennzeichen.Size = new System.Drawing.Size(140, 20);
			this.txt_Kennzeichen.TabIndex = 9;
			this.txt_Kennzeichen.Text = "";
			this.txt_Kennzeichen.TextChanged += new System.EventHandler(this.FelderModifiziert);
			this.txt_Kennzeichen.Leave += new System.EventHandler(this.txt_Kennzeichen_Leave);
			// 
			// lbl_Kennzeichnen
			// 
			this.lbl_Kennzeichnen.Location = new System.Drawing.Point(10, 15);
			this.lbl_Kennzeichnen.Name = "lbl_Kennzeichnen";
			this.lbl_Kennzeichnen.Size = new System.Drawing.Size(75, 20);
			this.lbl_Kennzeichnen.TabIndex = 8;
			this.lbl_Kennzeichnen.Text = "Kennzeichen:";
			// 
			// tabControltabctrl_Kfz
			// 
			this.tabControltabctrl_Kfz.Controls.Add(this.tabPage_Fahrer);
			this.tabControltabctrl_Kfz.Location = new System.Drawing.Point(5, 135);
			this.tabControltabctrl_Kfz.Name = "tabControltabctrl_Kfz";
			this.tabControltabctrl_Kfz.SelectedIndex = 0;
			this.tabControltabctrl_Kfz.Size = new System.Drawing.Size(615, 285);
			this.tabControltabctrl_Kfz.TabIndex = 20;
			// 
			// tabPage_Fahrer
			// 
			this.tabPage_Fahrer.Controls.Add(this.dtg_Fahrer);
			this.tabPage_Fahrer.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Fahrer.Name = "tabPage_Fahrer";
			this.tabPage_Fahrer.Size = new System.Drawing.Size(607, 259);
			this.tabPage_Fahrer.TabIndex = 0;
			this.tabPage_Fahrer.Text = "Fahrer";
			// 
			// dtg_Fahrer
			// 
			this.dtg_Fahrer.DataMember = "";
			this.dtg_Fahrer.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtg_Fahrer.Location = new System.Drawing.Point(5, 5);
			this.dtg_Fahrer.Name = "dtg_Fahrer";
			this.dtg_Fahrer.Size = new System.Drawing.Size(595, 290);
			this.dtg_Fahrer.TabIndex = 0;
			this.dtg_Fahrer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtg_Fahrer_MouseUp);
			this.dtg_Fahrer.Leave += new System.EventHandler(this.dtg_Fahrer_Leave);
			// 
			// cmb_KrfStatus
			// 
			this.cmb_KrfStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_KrfStatus.Location = new System.Drawing.Point(85, 75);
			this.cmb_KrfStatus.Name = "cmb_KrfStatus";
			this.cmb_KrfStatus.Size = new System.Drawing.Size(140, 21);
			this.cmb_KrfStatus.TabIndex = 83;
			this.cmb_KrfStatus.Leave += new System.EventHandler(this.txt_cmb_KrfStatus_Validated_Index);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 75);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 15);
			this.label1.TabIndex = 82;
			this.label1.Text = "Krf.Status:";
			// 
			// usc_Kfz
			// 
			this.Controls.Add(this.tabControltabctrl_Kfz);
			this.Controls.Add(this.gbx_Eingabemaske);
			this.Controls.Add(this.btn_Zuruecksetzen);
			this.Controls.Add(this.btn_Speichern);
			this.Enabled = false;
			this.Location = new System.Drawing.Point(6, 21);
			this.Name = "usc_Kfz";
			this.Size = new System.Drawing.Size(624, 456);
			this.gbx_Eingabemaske.ResumeLayout(false);
			this.gbx_Kommentar.ResumeLayout(false);
			this.tabControltabctrl_Kfz.ResumeLayout(false);
			this.tabPage_Fahrer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtg_Fahrer)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		#endregion

		#region Setze- Methode
		//
		private void Zuruecksetzen()
		{
			this.txt_Funkrufname.Text = "";
			this.txt_Kennzeichen.Text = "";
			this.txt_KfzTyp.Text = "";
			this.txt_Kommentar.Text = "";
			this.dtg_Fahrer.DataSource = null;
			this.Enabled = false;
			this._b_FelderModifiziert = false;
			this._kfz = null;
			this._iFahrerHelferID = -1;
			this._bFahrerAusgewaehlt = false;
			this._einheit = null;
			this.cmb_KrfStatus.SelectedIndex=-1;
		}

		public void InitAlleSTE()
		{
			SetzeKraefteStatus();
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

		#endregion

		#region Eingabeprüfung Kfz

		private bool EingabevalidierungKfz()
		{
			if(this.ValidiereKennzeichenKfz() && this.ValidiereFunkrufnameKfz()
				&& this.ValidiereKfzTypKfz() && this.ValidiereFahrerKfz() && ValidiereKraefteStatus())
				return(true);
			this.txt_Funkrufname_Validated_Kfz(null, null);
			this.txt_Kennzeichen_Validated_Kfz(null, null);
			this.txt_KfzTyp_Validated_Kfz(null, null);
			this.txt_cmb_KrfStatus_Validated_Index(null,null);
			this.dtg_Fahrer_Validated_Kfz(null, null);
			return(false);
		}

		private bool ValidiereFahrerKfz()
		{
			return(this._bFahrerAusgewaehlt);
		}

		private void dtg_Fahrer_Validated_Kfz(object sender, System.EventArgs e)
		{
			if(this.ValidiereFahrerKfz())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.dtg_Fahrer, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.dtg_Fahrer, "Bitte wählen Sie einen Fahrer aus");
			}
		}

		private bool ValidiereKfzTypKfz()
		{
			return(this.txt_KfzTyp.Text.Length > 0);
		}


		private bool ValidiereKraefteStatus()
		{
			return(this.cmb_KrfStatus.SelectedIndex != -1);
		}


		private void txt_cmb_KrfStatus_Validated_Index(object sender, System.EventArgs e)
		{
			if (this.ValidiereKraefteStatus())
			{
				ep_Eingabe.SetError(this.cmb_KrfStatus,"");
			}
			else
			{
				ep_Eingabe.SetError(this.cmb_KrfStatus,"Wählen Sie bitte einen Kräftestatus aus");
			}
		}

		private void txt_KfzTyp_Validated_Kfz(object sender, System.EventArgs e)
		{
			if(this.ValidiereKfzTypKfz())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_KfzTyp, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_KfzTyp, "Bitte geben Sie ein Kfz Typ ein");
			}
		}

		private bool ValidiereFunkrufnameKfz()
		{
			return(this.txt_Funkrufname.Text.Length > 0);
		}

		private void txt_Funkrufname_Validated_Kfz(object sender, System.EventArgs e)
		{
			if(this.ValidiereFunkrufnameKfz())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_Funkrufname, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_Funkrufname, "Bitte geben Sie ein Funkrufname ein");
			}
		}

		private bool ValidiereKennzeichenKfz()
		{
			return(this.txt_Kennzeichen.Text.Length > 0);
		}

		private void txt_Kennzeichen_Validated_Kfz(object sender, System.EventArgs e)
		{
			if(this.ValidiereKennzeichenKfz())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_Kennzeichen, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_Kennzeichen, "Bitte geben Sie ein Kennzeichen ein");
			}
		}

		private void txt_Kennzeichen_Leave(object sender, System.EventArgs e)
		{
			this.txt_Kennzeichen_Validated_Kfz(null, null);
		}

		private void txt_Funkrufname_Leave(object sender, System.EventArgs e)
		{
			this.txt_Funkrufname_Validated_Kfz(null, null);
		}

		private void txt_KfzTyp_Leave(object sender, System.EventArgs e)
		{
			this.txt_KfzTyp_Validated_Kfz(null, null);
		}

		private void dtg_Fahrer_Leave(object sender, System.EventArgs e)
		{
			this.dtg_Fahrer_Validated_Kfz(null, null);
		}

		#endregion

		#region event handler

		private void FelderModifiziert(object sender, EventArgs e)
		{
			this._b_FelderModifiziert = true;
		}

		private void dtg_Fahrer_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			DataGrid myGrid = (DataGrid) sender;
			DataGrid.HitTestInfo myHitInfo = myGrid.HitTest(e.X, e.Y);
			if((myHitInfo.Row < 0) || (myHitInfo.Column != -1))
				return;
			try
			{
				object o = myGrid[myHitInfo.Row, 0];
				this._iFahrerHelferID = Int32.Parse(o.ToString());
				this._bFahrerAusgewaehlt = true;
				this.ep_Eingabe.SetError(this.dtg_Fahrer, "");
				this._b_FelderModifiziert = true;
			}
			catch
			{
				this.ep_Eingabe.SetError(this.dtg_Fahrer, "Helfer konnte nicht geladen werden!");
				this._iFahrerHelferID = -1;
				this._bFahrerAusgewaehlt = false;
			}			
		}

		private void btn_Speichern_Click(object sender, System.EventArgs e)
		{
			if(CPopUp.SpeichernOhneUeberschreiben() == DialogResult.OK)
			{
				if(!this.EingabevalidierungKfz())
					return;
				Cdv_KFZ myKfz;
				if(this._kfz == null)
				{
					myKfz = new Cdv_KFZ();
					myKfz.Kraeftestatus = this._einheit.Kraeftestatus;
					myKfz.EinsatzschwerpunktID = this._einheit.EinsatzschwerpunktID;
				}
				else
					myKfz = this._kfz;
				myKfz.Kennzeichen = this.txt_Kennzeichen.Text;
				myKfz.KfzTyp = this.txt_KfzTyp.Text;
				myKfz.Kommentar.Autor = this._stEK.Einstellung.Benutzer.Benutzername;
				myKfz.Kommentar.Text = this.txt_Kommentar.Text;
				myKfz.Funkrufname = this.txt_Funkrufname.Text;
				myKfz.Kraeftestatus = (Tdv_Kraeftestatus) this.cmb_KrfStatus.SelectedIndex;
				myKfz.EinsatzschwerpunktID = this._einheit.EinsatzschwerpunktID;
				myKfz.FahrerHelferID = this._iFahrerHelferID;
				int iIDNeu = this._stEK.SpeichereKfz(myKfz);
				if(this._kfz == null)
				{
					int[] iKfzIDs = null;
					myKfz.ID = iIDNeu;
					iKfzIDs = new int[1];
					iKfzIDs[0] = myKfz.ID;
					this._einheit.KfzKraefteIDMenge = iKfzIDs;
				}
				///ToDO: Ist das Nötig? oder kann das auch in den If-Zweig?
				this._stEK.SpeichereEinheit(this._einheit);
				this.Zuruecksetzen();

			}
		}
		private void btn_Zuruecksetzen_Click(object sender, System.EventArgs e)
		{
			if(CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes)
			{
				this.Zuruecksetzen();
			}
		}

		#endregion

		#region Neues Kfz Anlegen

		public void NeuesKfzAnlegenStart(Cdv_Einheit pin_einheit)
		{
			this._einheit = pin_einheit;
			Cdv_Helfer[] helferMenge = this._stEK.HoleHelferZurEinheit(pin_einheit.ID);
			if(helferMenge != null)
				this.LadePersonen(helferMenge);
		}

		#region datagrid

		private DataTable ErstelleTabelleFuerPersonen()
		{
			DataColumn[] dcol_a_Person = 
			{								
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_ID", "ID", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Name", "Name", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Vorname", "Vorname", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_GebDat", "Geburtsdatum", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Strasse", "Str./Nr.", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_PLZOrt", "PLZ/Ort", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Erreichbarkeit", "Erreichbarkeit", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Faehigkeiten", "Faehigkeiten", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Helferstatus", "Helferstatus", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_KommentarAutor", "Kommentarautor", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_KommentarText", "Kommentartext", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_LetzteVerpflegung", "Letzte Verpflegung", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Position", "Position", "System.String")
			};			
			DataTable dtable_Person = Cpr_EK_AllgFkt.ErstellenEinerDataTable("dtable_Personen", dcol_a_Person);
			return dtable_Person;
		}

		public void LadeFahrer(Cdv_Helfer[] pin_Helfer, int pin_FahrerZumAuswaehlen)
		{
			//TODO: Fuelle Datagrid
			DataTable dtable_Person = ErstelleTabelleFuerPersonen();
			int iZeile = -1;
			bool bGefunden = false;

			foreach(Cdv_Helfer helfer in pin_Helfer)
			{
				if(!bGefunden)
					iZeile++;
				// ID			
				object[] obj_tabellezeile = new object[] {  helfer.ID.ToString(),
															 // Name
															 helfer.Personendaten.Name,
															 // Vorname
															 helfer.Personendaten.Vorname,
															 // GebDatum
															 helfer.Personendaten.GebDatum.ToString(),
															 // Strasse + Nummer
															 helfer.Personendaten.Anschrift.Strasse + " " + helfer.Personendaten.Anschrift.Hausnummer,
															 // PLZ/ORT
															 helfer.Personendaten.Anschrift.PLZ + " " + helfer.Personendaten.Anschrift.Ort,
															 // Erreichbarkeit
															 helfer.Erreichbarkeit,
															 // Faehigkeit
															 helfer.Faehigkeiten,
															 // Helferstatus
															 helfer.Helferstatus.ToString(),
															 //Kommentarautor
															 helfer.Kommentar.Autor,
															 // Kommentartext
															 helfer.Kommentar.Text,
															 // LetzteVerpflegung
															 helfer.LetzteVerfplegung.ToString(),
															 // Position
															 helfer.Position.ToString()
														 };
				
				if(helfer.ID == pin_FahrerZumAuswaehlen)
					bGefunden = true;
				dtable_Person.Rows.Add(obj_tabellezeile);
			}
			// Tabelle dem Datagrid zuordnen
			this.dtg_Fahrer.DataSource = dtable_Person;	
			this.dtg_Fahrer.Select(iZeile);
		}

		public void LadePersonen(Cdv_Helfer[] pin_Helfer)
		{
			//TODO: Fuelle Datagrid
			DataTable dtable_Person = ErstelleTabelleFuerPersonen();


			foreach(Cdv_Helfer helfer in pin_Helfer)
			{
				
		
				// ID			
				object[] obj_tabellezeile = new object[] {  helfer.ID.ToString(),
															 // Name
															 helfer.Personendaten.Name,
															 // Vorname
															 helfer.Personendaten.Vorname,
															 // GebDatum
															 helfer.Personendaten.GebDatum.ToString(),
															 // Strasse + Nummer
															 helfer.Personendaten.Anschrift.Strasse + " " + helfer.Personendaten.Anschrift.Hausnummer,
															 // PLZ/ORT
															 helfer.Personendaten.Anschrift.PLZ + " " + helfer.Personendaten.Anschrift.Ort,
															 // Erreichbarkeit
															 helfer.Erreichbarkeit,
															 // Faehigkeit
															 helfer.Faehigkeiten,
															 // Helferstatus
															 helfer.Helferstatus.ToString(),
															 //Kommentarautor
															 helfer.Kommentar.Autor,
															 // Kommentartext
															 helfer.Kommentar.Text,
															 // LetzteVerpflegung
															 helfer.LetzteVerfplegung.ToString(),
															 // Position
															 helfer.Position.ToString()
														 };
				
				
				dtable_Person.Rows.Add(obj_tabellezeile);
			}
			// Tabelle dem Datagrid zuordnen
			this.dtg_Fahrer.DataSource = dtable_Person;	
		}

		#endregion

		#endregion

		#region Getter

		public bool FelderIstModifiziert
		{
			get
			{
				return(this._b_FelderModifiziert);
			}
		}

		private int HoleFahrerHelferZeile(int pin_HelferID)
		{
			
			return(0);
		}

		#endregion

		#region Lade Kfz

		public void LadeKfz(Cdv_KFZ pin_kfz, Cdv_Einheit pin_einheit)
		{
			this.txt_Funkrufname.Text = pin_kfz.Funkrufname;
			this.txt_Kennzeichen.Text = pin_kfz.Kennzeichen;
			this.txt_KfzTyp.Text = pin_kfz.KfzTyp;
			this.txt_Kommentar.Text = pin_kfz.Kommentar.Text;
			this._einheit = pin_einheit;
			this.cmb_KrfStatus.SelectedIndex=(int)pin_kfz.Kraeftestatus;
			this._kfz = pin_kfz;
			int iFahrerHelferID = pin_kfz.FahrerHelferID;
			DataTable dtable_fahrer = this.ErstelleTabelleFuerPersonen();
			Cdv_Helfer[] helferMenge = this._stEK.HoleHelferZurEinheit(pin_einheit.ID);
			if(helferMenge != null)
			{
				this.LadeFahrer(helferMenge, pin_kfz.FahrerHelferID);
			}
		}

		#endregion

		#region Dynamisches anpassen von Daten

		public void AktualisiereFahrer()
		{
			if(this._einheit != null && this._kfz != null)
			{
				this.dtg_Fahrer.DataSource = null;
				DataTable dtable_fahrer = this.ErstelleTabelleFuerPersonen();
				Cdv_Helfer[] helferMenge = this._stEK.HoleHelferZurEinheit(_einheit.ID);
				if(helferMenge != null)
				{
					this.LadeFahrer(helferMenge, _kfz.FahrerHelferID);
				}
			}
		}

		#endregion
	}
}
