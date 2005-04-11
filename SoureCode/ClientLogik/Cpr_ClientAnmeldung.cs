using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// zum testen
using pELS.DV;
// benötigt für: Popups
using pELS.GUI.PopUp;


#region Dokumentation
	/**
	Erläuterung:	
	
	erstellt von:	Xiao		am: 16.Feb.2004
	

	aktuelle Version: 0.1 alpha

	History/Hinweise/Bekannte Bugs:
	- TODO: die Eingabeprüfung, Mechanismus der Fehlerausgabe
	**/
#endregion

namespace pELS.Client
{

	public class Cpr_ClientAnmeldung : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lbl_ClientAnmeldung_benutzername;
		private System.Windows.Forms.TextBox txt_ClientAnmeldung_benutzername;
		private System.Windows.Forms.Label lbl_ClientAnmeldung_systemrolle;
		private System.Windows.Forms.ComboBox cmb_ClientAnmeldung_waehleSystemrolle;
		private System.Windows.Forms.Button btn_ClientAnmeldung_anmelden;
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		#region eigene Instanzvariable
		private Cst_Client _stClient = null;
		private System.Windows.Forms.Button btn_ClientAnmeldung_abbrechen;
		private System.Windows.Forms.ComboBox cmb_ClientAnmeldung_Benutzermenge;
		private System.Windows.Forms.Label lbl_ClientAnmeldung_BenutzernameAuswahl;
		private System.Windows.Forms.RadioButton rbtn_ClientAnmeldung_BenutzerNeu;
		private System.Windows.Forms.RadioButton rbtn_ClientAnmeldung_BenutzerVorhanden;	
		
		private System.Windows.Forms.GroupBox groupBox1;	
		// teste, ob Cpr_ClientAnmeldung erfolgreich abgelaufen ist.
		// _b_ok: Anmeldung korrekt oder nicht
		// _anwendungSchliessen: folgt aus _b_ok, wenn Anmeldung erfolgreich, 
		private bool _b_ok = true;
		private bool _anwendungSchliessen = true;
		#endregion	

		#region SETs und GETs
		public bool ok
		{
			get{return _b_ok;}
		}
		public bool anwendungSchliessen
		{
			get{return _anwendungSchliessen;}
		}
		#endregion

		#region Konstruktor
		public Cpr_ClientAnmeldung(Cst_Client pin_stClient)
		{
			// Erforderlich für die Windows Form-Designerunterstützung
			InitializeComponent();
			// Argument Cst_Client festhalten
			_stClient = pin_stClient;
			
			// Initialisieren der ComboBox
			// Benutzer laden
			InitBenutzermenge();
			// Systemrollen laden			
			InitWaehleSystemrolle();			
		}
		#endregion 

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
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

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Cpr_ClientAnmeldung));
			this.lbl_ClientAnmeldung_BenutzernameAuswahl = new System.Windows.Forms.Label();
			this.rbtn_ClientAnmeldung_BenutzerVorhanden = new System.Windows.Forms.RadioButton();
			this.rbtn_ClientAnmeldung_BenutzerNeu = new System.Windows.Forms.RadioButton();
			this.cmb_ClientAnmeldung_waehleSystemrolle = new System.Windows.Forms.ComboBox();
			this.lbl_ClientAnmeldung_systemrolle = new System.Windows.Forms.Label();
			this.txt_ClientAnmeldung_benutzername = new System.Windows.Forms.TextBox();
			this.lbl_ClientAnmeldung_benutzername = new System.Windows.Forms.Label();
			this.cmb_ClientAnmeldung_Benutzermenge = new System.Windows.Forms.ComboBox();
			this.btn_ClientAnmeldung_anmelden = new System.Windows.Forms.Button();
			this.btn_ClientAnmeldung_abbrechen = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbl_ClientAnmeldung_BenutzernameAuswahl
			// 
			this.lbl_ClientAnmeldung_BenutzernameAuswahl.Location = new System.Drawing.Point(10, 70);
			this.lbl_ClientAnmeldung_BenutzernameAuswahl.Name = "lbl_ClientAnmeldung_BenutzernameAuswahl";
			this.lbl_ClientAnmeldung_BenutzernameAuswahl.Size = new System.Drawing.Size(85, 20);
			this.lbl_ClientAnmeldung_BenutzernameAuswahl.TabIndex = 15;
			this.lbl_ClientAnmeldung_BenutzernameAuswahl.Text = "Benutzername:";
			// 
			// rbtn_ClientAnmeldung_BenutzerVorhanden
			// 
			this.rbtn_ClientAnmeldung_BenutzerVorhanden.Checked = true;
			this.rbtn_ClientAnmeldung_BenutzerVorhanden.Location = new System.Drawing.Point(10, 25);
			this.rbtn_ClientAnmeldung_BenutzerVorhanden.Name = "rbtn_ClientAnmeldung_BenutzerVorhanden";
			this.rbtn_ClientAnmeldung_BenutzerVorhanden.Size = new System.Drawing.Size(190, 15);
			this.rbtn_ClientAnmeldung_BenutzerVorhanden.TabIndex = 1;
			this.rbtn_ClientAnmeldung_BenutzerVorhanden.TabStop = true;
			this.rbtn_ClientAnmeldung_BenutzerVorhanden.Text = "Bereits im System angemeldet";
			this.rbtn_ClientAnmeldung_BenutzerVorhanden.CheckedChanged += new System.EventHandler(this.rbtn_ClientAnmeldung_BenutzerVorhanden_CheckedChanged);
			// 
			// rbtn_ClientAnmeldung_BenutzerNeu
			// 
			this.rbtn_ClientAnmeldung_BenutzerNeu.Location = new System.Drawing.Point(10, 40);
			this.rbtn_ClientAnmeldung_BenutzerNeu.Name = "rbtn_ClientAnmeldung_BenutzerNeu";
			this.rbtn_ClientAnmeldung_BenutzerNeu.Size = new System.Drawing.Size(105, 15);
			this.rbtn_ClientAnmeldung_BenutzerNeu.TabIndex = 2;
			this.rbtn_ClientAnmeldung_BenutzerNeu.Text = "Neuer Benutzer";
			this.rbtn_ClientAnmeldung_BenutzerNeu.CheckedChanged += new System.EventHandler(this.rbtn_ClientAnmeldung_BenutzerNeu_CheckedChanged);
			// 
			// cmb_ClientAnmeldung_waehleSystemrolle
			// 
			this.cmb_ClientAnmeldung_waehleSystemrolle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_ClientAnmeldung_waehleSystemrolle.Location = new System.Drawing.Point(110, 120);
			this.cmb_ClientAnmeldung_waehleSystemrolle.Name = "cmb_ClientAnmeldung_waehleSystemrolle";
			this.cmb_ClientAnmeldung_waehleSystemrolle.Size = new System.Drawing.Size(190, 21);
			this.cmb_ClientAnmeldung_waehleSystemrolle.TabIndex = 5;
			this.cmb_ClientAnmeldung_waehleSystemrolle.Visible = false;
			// 
			// lbl_ClientAnmeldung_systemrolle
			// 
			this.lbl_ClientAnmeldung_systemrolle.Location = new System.Drawing.Point(10, 120);
			this.lbl_ClientAnmeldung_systemrolle.Name = "lbl_ClientAnmeldung_systemrolle";
			this.lbl_ClientAnmeldung_systemrolle.Size = new System.Drawing.Size(85, 20);
			this.lbl_ClientAnmeldung_systemrolle.TabIndex = 2;
			this.lbl_ClientAnmeldung_systemrolle.Text = "Rolle:";
			this.lbl_ClientAnmeldung_systemrolle.Visible = false;
			// 
			// txt_ClientAnmeldung_benutzername
			// 
			this.txt_ClientAnmeldung_benutzername.Location = new System.Drawing.Point(110, 95);
			this.txt_ClientAnmeldung_benutzername.Name = "txt_ClientAnmeldung_benutzername";
			this.txt_ClientAnmeldung_benutzername.Size = new System.Drawing.Size(190, 20);
			this.txt_ClientAnmeldung_benutzername.TabIndex = 4;
			this.txt_ClientAnmeldung_benutzername.Text = "";
			this.txt_ClientAnmeldung_benutzername.Visible = false;
			// 
			// lbl_ClientAnmeldung_benutzername
			// 
			this.lbl_ClientAnmeldung_benutzername.Location = new System.Drawing.Point(10, 95);
			this.lbl_ClientAnmeldung_benutzername.Name = "lbl_ClientAnmeldung_benutzername";
			this.lbl_ClientAnmeldung_benutzername.Size = new System.Drawing.Size(85, 20);
			this.lbl_ClientAnmeldung_benutzername.TabIndex = 0;
			this.lbl_ClientAnmeldung_benutzername.Text = "Benutzername:";
			this.lbl_ClientAnmeldung_benutzername.Visible = false;
			// 
			// cmb_ClientAnmeldung_Benutzermenge
			// 
			this.cmb_ClientAnmeldung_Benutzermenge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_ClientAnmeldung_Benutzermenge.Location = new System.Drawing.Point(110, 70);
			this.cmb_ClientAnmeldung_Benutzermenge.Name = "cmb_ClientAnmeldung_Benutzermenge";
			this.cmb_ClientAnmeldung_Benutzermenge.Size = new System.Drawing.Size(190, 21);
			this.cmb_ClientAnmeldung_Benutzermenge.TabIndex = 3;
			this.cmb_ClientAnmeldung_Benutzermenge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Cpr_ClientAnmeldung_KeyPress);
			// 
			// btn_ClientAnmeldung_anmelden
			// 
			this.btn_ClientAnmeldung_anmelden.Location = new System.Drawing.Point(130, 150);
			this.btn_ClientAnmeldung_anmelden.Name = "btn_ClientAnmeldung_anmelden";
			this.btn_ClientAnmeldung_anmelden.Size = new System.Drawing.Size(80, 25);
			this.btn_ClientAnmeldung_anmelden.TabIndex = 6;
			this.btn_ClientAnmeldung_anmelden.Text = "&Anmelden";
			this.btn_ClientAnmeldung_anmelden.Click += new System.EventHandler(this.btn_ClientAnmeldung_anmelden_Click);
			// 
			// btn_ClientAnmeldung_abbrechen
			// 
			this.btn_ClientAnmeldung_abbrechen.Location = new System.Drawing.Point(220, 150);
			this.btn_ClientAnmeldung_abbrechen.Name = "btn_ClientAnmeldung_abbrechen";
			this.btn_ClientAnmeldung_abbrechen.Size = new System.Drawing.Size(80, 25);
			this.btn_ClientAnmeldung_abbrechen.TabIndex = 7;
			this.btn_ClientAnmeldung_abbrechen.Text = "A&bbrechen";
			this.btn_ClientAnmeldung_abbrechen.Click += new System.EventHandler(this.btn_ClientAnmeldung_abbrechen_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btn_ClientAnmeldung_anmelden);
			this.groupBox1.Controls.Add(this.lbl_ClientAnmeldung_benutzername);
			this.groupBox1.Controls.Add(this.lbl_ClientAnmeldung_BenutzernameAuswahl);
			this.groupBox1.Controls.Add(this.lbl_ClientAnmeldung_systemrolle);
			this.groupBox1.Controls.Add(this.txt_ClientAnmeldung_benutzername);
			this.groupBox1.Controls.Add(this.cmb_ClientAnmeldung_waehleSystemrolle);
			this.groupBox1.Controls.Add(this.cmb_ClientAnmeldung_Benutzermenge);
			this.groupBox1.Controls.Add(this.rbtn_ClientAnmeldung_BenutzerVorhanden);
			this.groupBox1.Controls.Add(this.rbtn_ClientAnmeldung_BenutzerNeu);
			this.groupBox1.Controls.Add(this.btn_ClientAnmeldung_abbrechen);
			this.groupBox1.Location = new System.Drawing.Point(2, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(310, 180);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Benutzeranmeldung";
			// 
			// Cpr_ClientAnmeldung
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(314, 188);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Cpr_ClientAnmeldung";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Benutzeranmeldung";
			this.TopMost = true;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Cpr_ClientAnmeldung_KeyPress);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Methoden
		private void InitBenutzermenge()
		{
			// Benutzermenge von Cst_Client holen
			Cdv_Benutzer[] benutzerMengeArray = _stClient.alleBenutzer;
			ArrayList arl_benutzerMenge = new ArrayList(benutzerMengeArray);

			// Benutzer in Combobox übernehmen
			if (benutzerMengeArray.Length != 0)
			{
				
				//letzten Benutzernamen für auswahl ermitteln
				Cdv_Benutzer letzterBenutzer = this.HoleLetzenBenutzerAusDatei();
				for(int i = benutzerMengeArray.Length-1; i>=0; i--)
				{
					if(letzterBenutzer.ID == benutzerMengeArray[i].ID)
					{	
						//Diesen Benutzer als erstes In die ComboBox eintragen						
						this.cmb_ClientAnmeldung_Benutzermenge.Items.Add(benutzerMengeArray[i]);
						//Benutzer aus Liste entfernen, damit er nicht 2 mal auftaucht
						arl_benutzerMenge.Remove(benutzerMengeArray[i]);
					}
				}
				// restliche Benutzer von ArrayList in Combobox übernehmen									
				foreach(Cdv_Benutzer b in arl_benutzerMenge)
				{ 
					this.cmb_ClientAnmeldung_Benutzermenge.Items.Add(b);

				}
				
				
				
				// Richtige Elemente in GUI zeigen
				this.lbl_ClientAnmeldung_benutzername.Visible = false;
				this.txt_ClientAnmeldung_benutzername.Visible = false;
				this.lbl_ClientAnmeldung_systemrolle.Visible = false;
				this.cmb_ClientAnmeldung_waehleSystemrolle.Visible = false;				
				this.lbl_ClientAnmeldung_BenutzernameAuswahl.Visible = true;
				this.cmb_ClientAnmeldung_Benutzermenge.Visible = true;	
				this.cmb_ClientAnmeldung_Benutzermenge.SelectedIndex = 0;
			}
			else
			{
				//this.cmb_ClientAnmeldung_Benutzermenge.SelectedIndex = 0;	
				this.cmb_ClientAnmeldung_Benutzermenge.SelectedIndex = -1;	
				rbtn_ClientAnmeldung_BenutzerVorhanden.Enabled = false;
				rbtn_ClientAnmeldung_BenutzerNeu.Checked = true;
			}
		}
		private void InitWaehleSystemrolle()
		{
			// ruft die Methode aus Cst_Client, 
			// erhaelt somit alle Systemrollen als Array von strings
			string[] str_rollen = _stClient.HoleSystemrollen();
			// Initialisiert die Liste des Combox 
			for(int i = 0; i < str_rollen.Length; i++)
			{
				this.cmb_ClientAnmeldung_waehleSystemrolle.Items.Add(str_rollen[i]);
			}

			// Am Initzustand soll das erste Element der Liste des ComboBox ausgewählt sein.
			if(this.cmb_ClientAnmeldung_waehleSystemrolle.Items.Count > 0)
				this.cmb_ClientAnmeldung_waehleSystemrolle.SelectedIndex = 0; 
			else
				this.cmb_ClientAnmeldung_waehleSystemrolle.Items.Add("keine Daten");			
		}
		
		#endregion

		public new System.Windows.Forms.DialogResult ShowMe(Form caller)
		{
			System.Windows.Forms.DialogResult myResult = new DialogResult();
			myResult=this.ShowDialog(caller);
			return myResult;
		}

		private void btn_ClientAnmeldung_anmelden_Click(object sender, System.EventArgs e)
		{
			// Nimmt die Eingabe auf
			// Benutzername
			bool b_istNeuerBenutzer = this.rbtn_ClientAnmeldung_BenutzerNeu.Checked;
			bool b_pruefeEingabe = false;
			string str_benutzer = "";
			string str_systemrolle = "";
			
			// Unterscheidung Neuer Benutzer -- vorhandener Benutzer
			if(b_istNeuerBenutzer == true)
			{
				// Eingaben auslesen
				str_benutzer = txt_ClientAnmeldung_benutzername.Text;				
				str_systemrolle = this.cmb_ClientAnmeldung_waehleSystemrolle.SelectedItem.ToString();								
			}
			else // vorhandener Benutzer
			{
				// Auswahl auslesen
				if(cmb_ClientAnmeldung_Benutzermenge.Items.Count !=0)
				{
					str_benutzer = this.cmb_ClientAnmeldung_Benutzermenge.SelectedItem.ToString();
					// Zu Auswahl die Systemrolle des Benutzers holen
					foreach(Cdv_Benutzer b in _stClient.alleBenutzer)
						if ( b.Benutzername.Equals(str_benutzer) ) str_systemrolle = b.Systemrolle.ToString();
				}

			}

			// Eingaben prüfen, wenn korrekt, wird gleich der aktuelle Benutzer gesetzt
			b_pruefeEingabe = _stClient.SetzeBenutzer(str_benutzer, str_systemrolle,b_istNeuerBenutzer);
			// Unterscheidung ob SetzeBenutzer erfolgreich war
			if(b_pruefeEingabe == false) 
			{
				// Anmeldung nicht erfolgreich, deswegen wiederholen
				this._b_ok = false;
				//Ist dieses Hide für Xiaos Bug verantwortlich ?
				//this.Hide();
			}
			else //Eingabe war richtig
			{
				// Anmeldung war erfolgreich, Anmeldefenster kann zu gemacht werden
				this._b_ok = true;	
				//angemeldeten benutzer in Config Datei speichern
				this.SpeichereLetztenBenutzerInDatei(this._stClient.Einstellung.Benutzer);
				// nicht die gesamte Anwendung schließen, da Anmeldung erfolgreich
				this._anwendungSchliessen = false;
				this.Close();
			}										
		}

		private void btn_ClientAnmeldung_abbrechen_Click(object sender, System.EventArgs e)
		{
			this._anwendungSchliessen = true;
			// Anmeldefenster nicht mehr hervorbringen
			this._b_ok = true;
			this.Close();
		}

		private void rbtn_ClientAnmeldung_BenutzerNeu_CheckedChanged(object sender, System.EventArgs e)
		{
			this.lbl_ClientAnmeldung_benutzername.Visible = true;
			this.txt_ClientAnmeldung_benutzername.Visible = true;
			this.cmb_ClientAnmeldung_waehleSystemrolle.Visible = true;
			this.lbl_ClientAnmeldung_systemrolle.Visible = true;
			this.lbl_ClientAnmeldung_BenutzernameAuswahl.Visible = false;
			this.cmb_ClientAnmeldung_Benutzermenge.Visible = false;
		}

		private void rbtn_ClientAnmeldung_BenutzerVorhanden_CheckedChanged(object sender, System.EventArgs e)
		{
			this.lbl_ClientAnmeldung_benutzername.Visible = false;
			this.txt_ClientAnmeldung_benutzername.Visible = false;
			this.lbl_ClientAnmeldung_systemrolle.Visible = false;
			this.cmb_ClientAnmeldung_waehleSystemrolle.Visible = false;
			this.lbl_ClientAnmeldung_BenutzernameAuswahl.Visible = true;
			this.cmb_ClientAnmeldung_Benutzermenge.Visible = true;
		}

		private void SpeichereLetztenBenutzerInDatei(Cdv_Benutzer pin_Benutzer)
		{
			pELS.Tools.XMLZugriff myXMLconfig = new pELS.Tools.XMLZugriff();
			try
			{
				myXMLconfig.LadeDatei(_stClient.ConfigDateiName);			
				myXMLconfig.WaehleKnoten("pELS/pELS-Client/Benutzer");
				myXMLconfig.SetzeKnotenAttribut(0, "Name", pin_Benutzer.Benutzername);
				myXMLconfig.SetzeKnotenAttribut(0, "ID", pin_Benutzer.ID.ToString());								
			}
			catch (System.Exception ex)
			{
				MessageBox.Show("Speichern der Daten nicht möglich.\n"+ex.Message+"\nCpr_ClientServerVerbindung.SpeichereLetzteEinstellungenInDatei()");
				//hier wird einfach nix gemacht			
			}
		}

		private Cdv_Benutzer HoleLetzenBenutzerAusDatei()
		{
			pELS.Tools.XMLZugriff myXMLconfig = new pELS.Tools.XMLZugriff();
			Cdv_Benutzer b = new Cdv_Benutzer();

			myXMLconfig.LadeDatei(_stClient.ConfigDateiName);			
			myXMLconfig.WaehleKnoten("pELS/pELS-Client/Benutzer");
			b.Benutzername = myXMLconfig.HoleKnotenAttribut(0, "Name");								
			b.ID = int.Parse(myXMLconfig.HoleKnotenAttribut(0, "ID"));								
			return b;
		}

		private void Cpr_ClientAnmeldung_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		}

		private void Cpr_ClientAnmeldung_KeyPress(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			btn_ClientAnmeldung_anmelden_Click(null, null);		
		}
	


	}
}
