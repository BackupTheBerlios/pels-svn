using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace pELS.Server
{
	using pELS.DV;

	/// <summary>txt_Servername
	/// die Klasse stellt ein UI mit dem die Serverkonfiguration eingetragen werden kann.
	/// Dabei wird in Abhängigkeit vom verwendeten Konstruktor entweder die Server-GUI
	/// oder die Client-GUI angezeigt
	/// die Server-GUI zeigt eine Auswahl der möglichen IP-Adresse
	/// die Client-GUI erlaubt die Eingabe der Server-IP
	/// </summary>
	public class Cpr_frm_ServerKonfiguration : System.Windows.Forms.Form
	{
		#region GUI-Variablen
		private System.Windows.Forms.Button btn_schliessen;								
		private System.Windows.Forms.TextBox txt_Serverport;
		private System.Windows.Forms.Label lbl_Server_IPs;
		private System.Windows.Forms.GroupBox gbx_DB;
		private System.Windows.Forms.Label lbl_DB_Port;
		private System.Windows.Forms.Label lbl_DB_DBName;
		private System.Windows.Forms.Label lbl_DB_UserID;
		private System.Windows.Forms.Label lbl_DB_pw;		
		private System.Windows.Forms.TextBox txt_DB_Port;
		private System.Windows.Forms.TextBox txt_DB_Name;
		private System.Windows.Forms.TextBox txt_DB_UserID;
		private System.Windows.Forms.TextBox txt_DB_Passwort;
		private System.Windows.Forms.GroupBox gbx_Server;
		private System.Windows.Forms.RichTextBox txt_ServerIPs;
		private System.Windows.Forms.Label lbl_Server_Port;
		private System.Windows.Forms.Button btn_DB_Aendern_Verwerfen;
		private System.Windows.Forms.Button btn_ServerPort_aendern;
		private System.Windows.Forms.Button btn_DB_testen;
		private System.Windows.Forms.Label lbl_DB_Host;
		private System.Windows.Forms.TextBox txt_DB_Host;
		#endregion

		#region eigene Klassenvariablen
		bool _b_Port_geandert = false;
		bool _b_db_geaendert = false;
		bool _b_EingabeErfolgreich = false;
		string _str_merke_Serverport = String.Empty;
		string _str_merke_DB_Host = String.Empty;
		string _str_merke_DB_Port = String.Empty;
		string _str_merke_DB_Name = String.Empty;
		string _str_merke_DB_UserID = String.Empty;
		string _str_merke_DB_Passwort = String.Empty;
		string _str_merke_DB_Lifetime = String.Empty;
		#endregion



	
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Konstruktor für den Client-Zugriff
		/// Verwendung:
		/// 	Cpr_ServerKonfiguration _frm_serverKonfiguration = new Cpr_ServerKonfiguration();
		/// </summary>
		public Cpr_frm_ServerKonfiguration()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.LadeKonfigInGUI();			
						
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Cpr_frm_ServerKonfiguration));
			this.lbl_Server_IPs = new System.Windows.Forms.Label();
			this.btn_schliessen = new System.Windows.Forms.Button();
			this.lbl_Server_Port = new System.Windows.Forms.Label();
			this.txt_Serverport = new System.Windows.Forms.TextBox();
			this.gbx_DB = new System.Windows.Forms.GroupBox();
			this.txt_DB_Host = new System.Windows.Forms.TextBox();
			this.txt_DB_Passwort = new System.Windows.Forms.TextBox();
			this.txt_DB_UserID = new System.Windows.Forms.TextBox();
			this.txt_DB_Name = new System.Windows.Forms.TextBox();
			this.txt_DB_Port = new System.Windows.Forms.TextBox();
			this.lbl_DB_pw = new System.Windows.Forms.Label();
			this.lbl_DB_UserID = new System.Windows.Forms.Label();
			this.lbl_DB_DBName = new System.Windows.Forms.Label();
			this.lbl_DB_Port = new System.Windows.Forms.Label();
			this.lbl_DB_Host = new System.Windows.Forms.Label();
			this.gbx_Server = new System.Windows.Forms.GroupBox();
			this.txt_ServerIPs = new System.Windows.Forms.RichTextBox();
			this.btn_DB_Aendern_Verwerfen = new System.Windows.Forms.Button();
			this.btn_ServerPort_aendern = new System.Windows.Forms.Button();
			this.btn_DB_testen = new System.Windows.Forms.Button();
			this.gbx_DB.SuspendLayout();
			this.gbx_Server.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbl_Server_IPs
			// 
			this.lbl_Server_IPs.Location = new System.Drawing.Point(12, 28);
			this.lbl_Server_IPs.Name = "lbl_Server_IPs";
			this.lbl_Server_IPs.Size = new System.Drawing.Size(84, 20);
			this.lbl_Server_IPs.TabIndex = 4;
			this.lbl_Server_IPs.Text = "erreichbare IPs";
			// 
			// btn_schliessen
			// 
			this.btn_schliessen.Location = new System.Drawing.Point(8, 248);
			this.btn_schliessen.Name = "btn_schliessen";
			this.btn_schliessen.Size = new System.Drawing.Size(416, 23);
			this.btn_schliessen.TabIndex = 8;
			this.btn_schliessen.Text = "schließen && fortfahren";
			this.btn_schliessen.Click += new System.EventHandler(this.btn_schliessen_Click);
			// 
			// lbl_Server_Port
			// 
			this.lbl_Server_Port.Location = new System.Drawing.Point(16, 148);
			this.lbl_Server_Port.Name = "lbl_Server_Port";
			this.lbl_Server_Port.Size = new System.Drawing.Size(68, 16);
			this.lbl_Server_Port.TabIndex = 11;
			this.lbl_Server_Port.Text = "Port";
			// 
			// txt_Serverport
			// 
			this.txt_Serverport.Location = new System.Drawing.Point(96, 148);
			this.txt_Serverport.Name = "txt_Serverport";
			this.txt_Serverport.Size = new System.Drawing.Size(112, 20);
			this.txt_Serverport.TabIndex = 12;
			this.txt_Serverport.Text = "";
			// 
			// gbx_DB
			// 
			this.gbx_DB.Controls.Add(this.txt_DB_Host);
			this.gbx_DB.Controls.Add(this.txt_DB_Passwort);
			this.gbx_DB.Controls.Add(this.txt_DB_UserID);
			this.gbx_DB.Controls.Add(this.txt_DB_Name);
			this.gbx_DB.Controls.Add(this.txt_DB_Port);
			this.gbx_DB.Controls.Add(this.lbl_DB_pw);
			this.gbx_DB.Controls.Add(this.lbl_DB_UserID);
			this.gbx_DB.Controls.Add(this.lbl_DB_DBName);
			this.gbx_DB.Controls.Add(this.lbl_DB_Port);
			this.gbx_DB.Controls.Add(this.lbl_DB_Host);
			this.gbx_DB.Enabled = false;
			this.gbx_DB.Location = new System.Drawing.Point(224, 4);
			this.gbx_DB.Name = "gbx_DB";
			this.gbx_DB.Size = new System.Drawing.Size(204, 184);
			this.gbx_DB.TabIndex = 13;
			this.gbx_DB.TabStop = false;
			this.gbx_DB.Text = "Datenbankserver";
			// 
			// txt_DB_Host
			// 
			this.txt_DB_Host.Location = new System.Drawing.Point(96, 22);
			this.txt_DB_Host.Name = "txt_DB_Host";
			this.txt_DB_Host.Size = new System.Drawing.Size(96, 20);
			this.txt_DB_Host.TabIndex = 10;
			this.txt_DB_Host.Text = "<DB Host>";
			// 
			// txt_DB_Passwort
			// 
			this.txt_DB_Passwort.Location = new System.Drawing.Point(96, 150);
			this.txt_DB_Passwort.Name = "txt_DB_Passwort";
			this.txt_DB_Passwort.Size = new System.Drawing.Size(96, 20);
			this.txt_DB_Passwort.TabIndex = 9;
			this.txt_DB_Passwort.Text = "<Passwort>";
			// 
			// txt_DB_UserID
			// 
			this.txt_DB_UserID.Location = new System.Drawing.Point(96, 118);
			this.txt_DB_UserID.Name = "txt_DB_UserID";
			this.txt_DB_UserID.Size = new System.Drawing.Size(96, 20);
			this.txt_DB_UserID.TabIndex = 8;
			this.txt_DB_UserID.Text = "<UserID>";
			// 
			// txt_DB_Name
			// 
			this.txt_DB_Name.Location = new System.Drawing.Point(96, 86);
			this.txt_DB_Name.Name = "txt_DB_Name";
			this.txt_DB_Name.Size = new System.Drawing.Size(96, 20);
			this.txt_DB_Name.TabIndex = 7;
			this.txt_DB_Name.Text = "<DBName>";
			// 
			// txt_DB_Port
			// 
			this.txt_DB_Port.Location = new System.Drawing.Point(96, 54);
			this.txt_DB_Port.Name = "txt_DB_Port";
			this.txt_DB_Port.Size = new System.Drawing.Size(96, 20);
			this.txt_DB_Port.TabIndex = 6;
			this.txt_DB_Port.Text = "<Port>";
			// 
			// lbl_DB_pw
			// 
			this.lbl_DB_pw.Location = new System.Drawing.Point(12, 152);
			this.lbl_DB_pw.Name = "lbl_DB_pw";
			this.lbl_DB_pw.Size = new System.Drawing.Size(60, 16);
			this.lbl_DB_pw.TabIndex = 4;
			this.lbl_DB_pw.Text = "Passwort";
			// 
			// lbl_DB_UserID
			// 
			this.lbl_DB_UserID.Location = new System.Drawing.Point(12, 120);
			this.lbl_DB_UserID.Name = "lbl_DB_UserID";
			this.lbl_DB_UserID.Size = new System.Drawing.Size(60, 16);
			this.lbl_DB_UserID.TabIndex = 3;
			this.lbl_DB_UserID.Text = "Benutzer";
			// 
			// lbl_DB_DBName
			// 
			this.lbl_DB_DBName.Location = new System.Drawing.Point(12, 88);
			this.lbl_DB_DBName.Name = "lbl_DB_DBName";
			this.lbl_DB_DBName.Size = new System.Drawing.Size(60, 16);
			this.lbl_DB_DBName.TabIndex = 2;
			this.lbl_DB_DBName.Text = "DB Name";
			// 
			// lbl_DB_Port
			// 
			this.lbl_DB_Port.Location = new System.Drawing.Point(12, 56);
			this.lbl_DB_Port.Name = "lbl_DB_Port";
			this.lbl_DB_Port.Size = new System.Drawing.Size(60, 16);
			this.lbl_DB_Port.TabIndex = 1;
			this.lbl_DB_Port.Text = "Port";
			// 
			// lbl_DB_Host
			// 
			this.lbl_DB_Host.Location = new System.Drawing.Point(12, 24);
			this.lbl_DB_Host.Name = "lbl_DB_Host";
			this.lbl_DB_Host.Size = new System.Drawing.Size(60, 16);
			this.lbl_DB_Host.TabIndex = 0;
			this.lbl_DB_Host.Text = "Host";
			// 
			// gbx_Server
			// 
			this.gbx_Server.Controls.Add(this.txt_ServerIPs);
			this.gbx_Server.Controls.Add(this.lbl_Server_Port);
			this.gbx_Server.Controls.Add(this.txt_Serverport);
			this.gbx_Server.Controls.Add(this.lbl_Server_IPs);
			this.gbx_Server.Enabled = false;
			this.gbx_Server.Location = new System.Drawing.Point(0, 4);
			this.gbx_Server.Name = "gbx_Server";
			this.gbx_Server.Size = new System.Drawing.Size(220, 184);
			this.gbx_Server.TabIndex = 14;
			this.gbx_Server.TabStop = false;
			this.gbx_Server.Text = "Server Erreichbarkeit";
			// 
			// txt_ServerIPs
			// 
			this.txt_ServerIPs.Location = new System.Drawing.Point(96, 24);
			this.txt_ServerIPs.Name = "txt_ServerIPs";
			this.txt_ServerIPs.ReadOnly = true;
			this.txt_ServerIPs.Size = new System.Drawing.Size(112, 112);
			this.txt_ServerIPs.TabIndex = 13;
			this.txt_ServerIPs.Text = "<Servernamen>";
			// 
			// btn_DB_Aendern_Verwerfen
			// 
			this.btn_DB_Aendern_Verwerfen.Location = new System.Drawing.Point(228, 196);
			this.btn_DB_Aendern_Verwerfen.Name = "btn_DB_Aendern_Verwerfen";
			this.btn_DB_Aendern_Verwerfen.Size = new System.Drawing.Size(196, 20);
			this.btn_DB_Aendern_Verwerfen.TabIndex = 15;
			this.btn_DB_Aendern_Verwerfen.Text = "DB Anbindung ändern";
			this.btn_DB_Aendern_Verwerfen.Click += new System.EventHandler(this.btn_DB_Aendern_Verwerfen_Click);
			// 
			// btn_ServerPort_aendern
			// 
			this.btn_ServerPort_aendern.Location = new System.Drawing.Point(12, 196);
			this.btn_ServerPort_aendern.Name = "btn_ServerPort_aendern";
			this.btn_ServerPort_aendern.Size = new System.Drawing.Size(196, 20);
			this.btn_ServerPort_aendern.TabIndex = 16;
			this.btn_ServerPort_aendern.Text = "ServerPort ändern";
			this.btn_ServerPort_aendern.Click += new System.EventHandler(this.btn_ServerPort_aendern_Click);
			// 
			// btn_DB_testen
			// 
			this.btn_DB_testen.Location = new System.Drawing.Point(228, 220);
			this.btn_DB_testen.Name = "btn_DB_testen";
			this.btn_DB_testen.Size = new System.Drawing.Size(196, 20);
			this.btn_DB_testen.TabIndex = 17;
			this.btn_DB_testen.Text = "DB Anbindung testen";
			this.btn_DB_testen.Click += new System.EventHandler(this.btn_DB_testen_Click);
			// 
			// Cpr_frm_ServerKonfiguration
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(434, 274);
			this.Controls.Add(this.btn_DB_testen);
			this.Controls.Add(this.btn_ServerPort_aendern);
			this.Controls.Add(this.btn_DB_Aendern_Verwerfen);
			this.Controls.Add(this.gbx_Server);
			this.Controls.Add(this.gbx_DB);
			this.Controls.Add(this.btn_schliessen);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Cpr_frm_ServerKonfiguration";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Server-Konfiguration";
			this.gbx_DB.ResumeLayout(false);
			this.gbx_Server.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		#region Eventhandler
		/// <summary>
		/// speichert ggf. die Serverkonfiguration beim Verlassen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_schliessen_Click(object sender, System.EventArgs e)
		{
			bool cancel = false;
			#region testen dass die aktuelle Konfig eine richtige ist
			string str_nachricht = this.testeDB();
			if(str_nachricht!="")
			{
				MessageBox.Show("Es trat folgender Fehler beim Test der Datenbankkonfiguration auf:\n\n"
					+str_nachricht
					+"\n\nBitte überprüfen Sie die Einstellungen und versuchen Sie es erneut!",
					"Fehler beim Datenbanktest",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				cancel = true;
			}

			#endregion
			#region ggf. Änderungen speichern
			if(!cancel)
			{
				if(_b_db_geaendert || _b_Port_geandert)
				{
					if(this.DatenVeraendert())
					{
						string str_text = "Sie haben Änderungen an der Serverkonfiguration vorgenommen.\n"
							+	 "Nach dem Speichern der Änderungen, wird der Server automatisch neu gestartet.\n"
							+	 "Alle verbundenen Clients verlieren ihre Verbindung\n\n";
						if(_b_db_geaendert)
							str_text += "\nDie Datenbankanbindung muss neu initialisiert werden.";
						if(_b_Port_geandert)
							str_text += "\nDer Serverport wurde geändert.";
						DialogResult dr_speichern = new DialogResult();
						dr_speichern = MessageBox.Show(str_text,				
							"Änderungen übernehmen?", 
							MessageBoxButtons.YesNo, 
							MessageBoxIcon.Question);
						if(dr_speichern == DialogResult.Yes)
						{
							if(_b_db_geaendert)
								if(!this.SpeichereDBConfigAusGUI())						
									MessageBox.Show("Daten konnten nicht in "+pELS.Tools.Server.CKonstanten._str_ServerConfigPfad+" geschrieben werden.\nDatei beschädigt, nicht vorhanden oder schreibgeschützt");
							if(_b_Port_geandert)
								if(!this.SpeichereServerportAusGUI())						
									MessageBox.Show("Daten konnten nicht in "+pELS.Tools.Server.CKonstanten._str_ServerConfigPfad+" geschrieben werden.\nDatei beschädigt, nicht vorhanden oder schreibgeschützt");																					
						}
						else
						{
							cancel = true;
						}
					}
				}
				else
				{
					//EingabeErfolgreich wird außerhalb abgerufen um zu entscheiden ob der server neu gestartet werden soll
					//daher hier die Ausgabe-> nein, weil sich ja auch nix geändert hat -alexG
					this._b_EingabeErfolgreich = false;
				}
			}
			#endregion
			if(!cancel)
			{				
				this.Close();
			}
		}
		private void btn_ServerPort_aendern_Click(object sender, System.EventArgs e)
		{
			if(_b_Port_geandert)
			{
				this.gbx_Server.Enabled = false;
				//Zurücksetzen durch den gemerkten Port
				this.txt_Serverport.Text = _str_merke_Serverport;
				this._b_Port_geandert = false;
				this.btn_ServerPort_aendern.Text = "ServerPort ändern";
			}
			else
			{
				this.gbx_Server.Enabled = true;
				this._b_Port_geandert = true;
				this.btn_ServerPort_aendern.Text = "Port zurücksetzen";
			}
			
		}

		private void btn_DB_Aendern_Verwerfen_Click(object sender, System.EventArgs e)
		{
			if(_b_db_geaendert)
			{
				this.gbx_DB.Enabled = false;				
				//belegen der GUI mit den zwischengespeicherten Werten
				this.txt_DB_Host.Text = _str_merke_DB_Host;
				this.txt_DB_Port.Text = _str_merke_DB_Port;
				this.txt_DB_Name.Text = _str_merke_DB_Name;
				this.txt_DB_UserID.Text = _str_merke_DB_UserID;
				this.txt_DB_Passwort.Text = _str_merke_DB_Passwort;

				this.btn_DB_Aendern_Verwerfen.Text = "DB Anbindung ändern";
				_b_db_geaendert = false;			
			}
			else
			{
				this.gbx_DB.Enabled = true;
				this.btn_DB_Aendern_Verwerfen.Text ="DB Anbindung zurücksetzen";
				_b_db_geaendert = true;
			}
		
		}

		private void btn_DB_testen_Click(object sender, System.EventArgs e)
		{
			string str_nachricht = testeDB();
			if(str_nachricht=="")
				MessageBox.Show("Datenbankkonfiguration erfolgreich getestet");
			else
				MessageBox.Show("Es trat folgender Fehler beim Test der Datenbankkonfiguration auf:\n\n"
					+str_nachricht
					+"\n\nBitte überprüfen Sie die Einstellungen und versuchen Sie es erneut!",
					"Fehler beim Datenbanktest",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);


		}
		




		#endregion
		#region Funktionalität
		private void LadeKonfigInGUI()
		{
			//Laden der IPs						
			this.txt_ServerIPs.Lines = Cst_Server.HoleIPAddressListe();
			this.txt_ServerIPs.Text += "\r\n127.0.0.1";
			//Laden des Ports
			this.txt_Serverport.Text = this.LadePortAusDatei();
			this.LadeDbConfigAusDateiInGUI();
									
		}

		private string LadePortAusDatei()
		{
			pELS.Tools.XMLZugriff myXMLZugriff = new pELS.Tools.XMLZugriff();
			myXMLZugriff.LadeDatei(pELS.Tools.Server.CKonstanten._str_ServerConfigPfad);
			myXMLZugriff.WaehleKnoten("pELS/pELS-Server/Address");
			//Serverport global zwischenmerken
			_str_merke_Serverport =  myXMLZugriff.HoleKnotenAttribut(0,"Port");			
			return _str_merke_Serverport;
		}

		private string testeDB()
		{
			string str_nachricht = "";
			Cdv_DB db = Cdv_DB.HoleInstanz();
			str_nachricht = db.testeDB(txt_DB_UserID.Text,
				txt_DB_Passwort.Text,
				txt_DB_Host.Text,
				txt_DB_Port.Text,
				txt_DB_Name.Text,
				_str_merke_DB_Lifetime);
			return str_nachricht;

		}
		private void LadeDbConfigAusDateiInGUI()
		{
			pELS.Tools.XMLZugriff myXMLZugriff = new pELS.Tools.XMLZugriff();
			myXMLZugriff.LadeDatei(pELS.Tools.Server.CKonstanten._str_ServerConfigPfad);
			myXMLZugriff.WaehleKnoten("pELS/pELS-Server/DBConfig");
			//erst in eine globale Zwischenvariable auslesen und dann in die textboxen schreiben
			_str_merke_DB_Host = myXMLZugriff.HoleKnotenAttribut(0,"Host");
			_str_merke_DB_Port = myXMLZugriff.HoleKnotenAttribut(0,"Port");
			_str_merke_DB_Name = myXMLZugriff.HoleKnotenAttribut(0,"DBName");
			_str_merke_DB_UserID = myXMLZugriff.HoleKnotenAttribut(0,"UserID");
			_str_merke_DB_Passwort = myXMLZugriff.HoleKnotenAttribut(0,"PW");
			_str_merke_DB_Lifetime = myXMLZugriff.HoleKnotenAttribut(0,"Lifetime");
			//belegen der GUI mit den zwischengespeicherten Werten
			this.txt_DB_Host.Text = _str_merke_DB_Host;
			this.txt_DB_Port.Text = _str_merke_DB_Port;
			this.txt_DB_Name.Text = _str_merke_DB_Name;
			this.txt_DB_UserID.Text = _str_merke_DB_UserID;
			this.txt_DB_Passwort.Text = _str_merke_DB_Passwort;


		}

		//Prüfen aller gemerkten Daten gegen die GUI Inhalte
		private bool DatenVeraendert()
		{
			if(_str_merke_DB_Host != txt_DB_Host.Text)
				return true;
			if(_str_merke_DB_Port != txt_DB_Port.Text)
				return true;
			if(_str_merke_DB_Name != txt_DB_Name.Text)
				return true;
			if(_str_merke_DB_UserID != txt_DB_UserID.Text)
				return true;
			if(_str_merke_DB_Passwort != txt_DB_Passwort.Text)
				return true;
			if(_str_merke_Serverport != txt_Serverport.Text)
				return true;
			else
				return false;
		}


		private bool SpeichereDBConfigAusGUI()
		{
			pELS.Tools.XMLZugriff myXMLZugriff = new pELS.Tools.XMLZugriff();
			myXMLZugriff.LadeDatei(pELS.Tools.Server.CKonstanten._str_ServerConfigPfad);
			myXMLZugriff.WaehleKnoten("pELS/pELS-Server/DBConfig");
			myXMLZugriff.SetzeKnotenAttribut(0,"Host", this.txt_DB_Host.Text);
			myXMLZugriff.SetzeKnotenAttribut(0,"Port", this.txt_DB_Port.Text);
			myXMLZugriff.SetzeKnotenAttribut(0,"DBName",this.txt_DB_Name.Text);
			myXMLZugriff.SetzeKnotenAttribut(0,"UserID",this.txt_DB_UserID.Text);
			myXMLZugriff.SetzeKnotenAttribut(0,"PW",this.txt_DB_Passwort.Text);
			if ("" == myXMLZugriff.SpeichereDatei())
			{				
				pELS.Tools.RegistryZugriff myRegistryZugriff = new pELS.Tools.RegistryZugriff(pELS.Tools.Server.CKonstanten._str_DefaultRegistryDatei, pELS.Tools.Server.CKonstanten._str_AktuelleRegistryDatei);
				// Ändern der Konfiguration in der Registry
				bool b_erfolreich = myRegistryZugriff.AktualisiereOdbcKonfiguration(txt_DB_Name.Text, txt_DB_Host.Text, txt_DB_Port.Text, txt_DB_UserID.Text, txt_DB_Passwort.Text);
			
				_b_EingabeErfolgreich = b_erfolreich;
				return b_erfolreich;
			}
			else
			{
				_b_EingabeErfolgreich = false;
				return false;
			}
		}

		private bool SpeichereServerportAusGUI()
		{
			pELS.Tools.XMLZugriff myXMLZugriff = new pELS.Tools.XMLZugriff();
			myXMLZugriff.LadeDatei(pELS.Tools.Server.CKonstanten._str_ServerConfigPfad);
			myXMLZugriff.WaehleKnoten("pELS/pELS-Server/Address");
			myXMLZugriff.SetzeKnotenAttribut(0,"Port",this.txt_Serverport.Text);
			if ("" == myXMLZugriff.SpeichereDatei())
			{
				_b_EingabeErfolgreich = true;
				return true;
			}
			else
			{
				_b_EingabeErfolgreich = false;
				return false;
			}
		}

		#endregion



		#region Properties
		public bool EingabeErfolgreich
		{
			get{return this._b_EingabeErfolgreich;}
		}
		#endregion
	}

}
