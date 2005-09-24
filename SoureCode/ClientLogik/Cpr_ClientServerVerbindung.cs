using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace pELS.Client
{
	/// <summary>
	/// Summary description for Cst_ClientServerVerbindung.
	/// </summary>
	public class Cpr_ClientServerVerbindung : System.Windows.Forms.Form
	{
		#region Variablen
		// Cst_Client
		private Cst_Client _stClient = null;
		private System.Windows.Forms.GroupBox grp_ClientAnmeldung_Serverinfos;
		private System.Windows.Forms.Label lbl_Serverport;
		private System.Windows.Forms.TextBox txt_Servername;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_Serverport;
		private System.Windows.Forms.Button btn_ClientServerVerbindung_abbrechen;
		private System.Windows.Forms.Button btn_ClientServerVerbindung_anmelden;
		private bool _anwendungSchliessen = true;		// teste, ob Cpr_ClientServerVerbindung erfolgreich abgelaufen ist.
		private string _letzerBenutzer = String.Empty;
		private string _letzeIP = String.Empty;
		private string _letzerPort = String.Empty;
		
		private System.ComponentModel.Container components = null;
		#endregion
		#region GETs und SETs
		public bool anwendungSchliessen
		{
			get{return _anwendungSchliessen;}
		}
		#endregion

		public Cpr_ClientServerVerbindung(Cst_Client pin_stClient)
		{			
			InitializeComponent();
			// Zur Übergabe der Serverkonvigurationsdaten muss stClient bekannt sein
			_stClient = pin_stClient;
			this.LadeLetzteEinstellungenAusDatei();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Cpr_ClientServerVerbindung));
			this.grp_ClientAnmeldung_Serverinfos = new System.Windows.Forms.GroupBox();
			this.lbl_Serverport = new System.Windows.Forms.Label();
			this.txt_Servername = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txt_Serverport = new System.Windows.Forms.TextBox();
			this.btn_ClientServerVerbindung_abbrechen = new System.Windows.Forms.Button();
			this.btn_ClientServerVerbindung_anmelden = new System.Windows.Forms.Button();
			this.grp_ClientAnmeldung_Serverinfos.SuspendLayout();
			this.SuspendLayout();
			// 
			// grp_ClientAnmeldung_Serverinfos
			// 
			this.grp_ClientAnmeldung_Serverinfos.Controls.Add(this.lbl_Serverport);
			this.grp_ClientAnmeldung_Serverinfos.Controls.Add(this.txt_Servername);
			this.grp_ClientAnmeldung_Serverinfos.Controls.Add(this.label1);
			this.grp_ClientAnmeldung_Serverinfos.Controls.Add(this.txt_Serverport);
			this.grp_ClientAnmeldung_Serverinfos.Location = new System.Drawing.Point(8, 16);
			this.grp_ClientAnmeldung_Serverinfos.Name = "grp_ClientAnmeldung_Serverinfos";
			this.grp_ClientAnmeldung_Serverinfos.Size = new System.Drawing.Size(310, 70);
			this.grp_ClientAnmeldung_Serverinfos.TabIndex = 19;
			this.grp_ClientAnmeldung_Serverinfos.TabStop = false;
			this.grp_ClientAnmeldung_Serverinfos.Text = "Serverinformationen";
			// 
			// lbl_Serverport
			// 
			this.lbl_Serverport.Location = new System.Drawing.Point(10, 40);
			this.lbl_Serverport.Name = "lbl_Serverport";
			this.lbl_Serverport.Size = new System.Drawing.Size(85, 20);
			this.lbl_Serverport.TabIndex = 13;
			this.lbl_Serverport.Text = "Serverport";
			// 
			// txt_Servername
			// 
			this.txt_Servername.Location = new System.Drawing.Point(110, 20);
			this.txt_Servername.Name = "txt_Servername";
			this.txt_Servername.Size = new System.Drawing.Size(190, 20);
			this.txt_Servername.TabIndex = 1;
			this.txt_Servername.Text = "127.0.0.1";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94, 20);
			this.label1.TabIndex = 10;
			this.label1.Text = "Servername o. IP";
			// 
			// txt_Serverport
			// 
			this.txt_Serverport.Location = new System.Drawing.Point(110, 40);
			this.txt_Serverport.Name = "txt_Serverport";
			this.txt_Serverport.Size = new System.Drawing.Size(50, 20);
			this.txt_Serverport.TabIndex = 2;
			this.txt_Serverport.Text = "9001";
			// 
			// btn_ClientServerVerbindung_abbrechen
			// 
			this.btn_ClientServerVerbindung_abbrechen.Location = new System.Drawing.Point(240, 96);
			this.btn_ClientServerVerbindung_abbrechen.Name = "btn_ClientServerVerbindung_abbrechen";
			this.btn_ClientServerVerbindung_abbrechen.Size = new System.Drawing.Size(80, 25);
			this.btn_ClientServerVerbindung_abbrechen.TabIndex = 4;
			this.btn_ClientServerVerbindung_abbrechen.Text = "&Abbrechen";
			this.btn_ClientServerVerbindung_abbrechen.Click += new System.EventHandler(this.btn_ClientServerVerbindung_abbrechen_Click);
			// 
			// btn_ClientServerVerbindung_anmelden
			// 
			this.btn_ClientServerVerbindung_anmelden.Location = new System.Drawing.Point(152, 96);
			this.btn_ClientServerVerbindung_anmelden.Name = "btn_ClientServerVerbindung_anmelden";
			this.btn_ClientServerVerbindung_anmelden.Size = new System.Drawing.Size(80, 25);
			this.btn_ClientServerVerbindung_anmelden.TabIndex = 3;
			this.btn_ClientServerVerbindung_anmelden.Text = "A&nmelden";
			this.btn_ClientServerVerbindung_anmelden.Click += new System.EventHandler(this.btn_ClientServerVerbindung_anmelden_Click);
			// 
			// Cpr_ClientServerVerbindung
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(328, 125);
			this.Controls.Add(this.btn_ClientServerVerbindung_abbrechen);
			this.Controls.Add(this.btn_ClientServerVerbindung_anmelden);
			this.Controls.Add(this.grp_ClientAnmeldung_Serverinfos);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Cpr_ClientServerVerbindung";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Konfigurieren der Verbindung zum Server";
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Cpr_ClientServerVerbindung_KeyPress);
			this.grp_ClientAnmeldung_Serverinfos.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btn_ClientServerVerbindung_anmelden_Click(object sender, System.EventArgs e)
		{
			_stClient.SetzeServerKonfig(txt_Servername.Text, txt_Serverport.Text);
			this._anwendungSchliessen = false;
			this.SpeichereLetzteEinstellungenInDatei();
			this.Close();
		}

		private void btn_ClientServerVerbindung_abbrechen_Click(object sender, System.EventArgs e)
		{
			this._anwendungSchliessen = true;
			this.Close();
		}


		private bool LadeLetzteEinstellungenAusDatei()
		{
			pELS.Tools.XMLZugriff myXMLconfig = new pELS.Tools.XMLZugriff();
			try
			{
				myXMLconfig.LadeDatei(_stClient.ConfigDateiName);			
				myXMLconfig.WaehleKnoten("pELS/pELS-Client/Address");
				txt_Servername.Text = myXMLconfig.HoleKnotenAttribut(0,"IP");
				txt_Serverport.Text = myXMLconfig.HoleKnotenAttribut(0, "Port");				
				return true;
			}
			catch
			{
				return false;			
			}
		}

		private void SpeichereLetzteEinstellungenInDatei()
		{
			pELS.Tools.XMLZugriff myXMLconfig = new pELS.Tools.XMLZugriff();
			try
			{
				myXMLconfig.LadeDatei(_stClient.ConfigDateiName);			
				myXMLconfig.WaehleKnoten("pELS/pELS-Client/Address");
				myXMLconfig.SetzeKnotenAttribut(0, "IP", txt_Servername.Text);
				myXMLconfig.SetzeKnotenAttribut(0, "Port", txt_Serverport.Text);				
				//myXMLconfig.SpeichereDatei(_stClient.ConfigDateiName);				
			}
			catch (System.Exception ex)
			{
				//MessageBox.Show("Speichern der Daten nicht möglich.\n"+ex.Message+"\nCpr_ClientServerVerbindung.SpeichereLetzteEinstellungenInDatei()");
				//hier wird einfach nix gemacht			
			}
		}

		private void Cpr_ClientServerVerbindung_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
			{
				_stClient.SetzeServerKonfig(txt_Servername.Text, txt_Serverport.Text);
				this._anwendungSchliessen = false;
				this.SpeichereLetzteEinstellungenInDatei();
				this.Close();
			}

		}

	}
}
