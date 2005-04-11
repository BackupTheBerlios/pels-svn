using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
	// benötigt für: Exportieren des Datenbestand
using System.IO;

namespace pELS.Server
{
	using pELS.DV;
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Cpr_frm_Server : System.Windows.Forms.Form
	{
		#region GUI-Variablen
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.Button btn_ZeigeServerKonfig;
		private System.Windows.Forms.Button btn_NeuerEinsatz;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		#endregion
		#region globale Variablen
		private static Cpr_frm_Einsatzdaten _frm_Einsatzdaten;
		#endregion
		private System.Windows.Forms.MenuItem mI_Server;
		private System.Windows.Forms.MenuItem mI_Server_neu_starten;
		private System.Windows.Forms.MenuItem mI_Server_Konfiguration;
		private System.Windows.Forms.MenuItem mI_Server_herunterfahren;
		private System.Windows.Forms.MenuItem mI_Einsatz;
		private System.Windows.Forms.MenuItem Einsatz_neu_anlegen;
		private System.Windows.Forms.MenuItem mI_Einsatz_daten_bearbeiten;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem mI_Hilfe;
		private System.Windows.Forms.MenuItem mI_Hilfe_Homepage;
		private System.Windows.Forms.MenuItem mI_Hilfe_Info;
		private System.Windows.Forms.Button btn_Serverherunterfahren;
		private System.Windows.Forms.Button btn_Oberflaeche_verstecken;
		private System.Windows.Forms.MenuItem mI_Server_verstecken;
		private System.Windows.Forms.MenuItem mI_Hilfe_Doku;
		private System.Windows.Forms.Label lbl_warten;


		//		static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args) 
		//		{
		//			if (args.IsTerminating)
		//			{
		//				MessageBox.Show(@"Schluss, Aus, es ist vorbei", 
		//					"Fehlermeldung", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//
		//			}
		//			Exception e = (Exception) args.ExceptionObject;
		//			MessageBox.Show(@"Ein Fehler ist im System aufgetreten. Sollte sich dieser 
		//				Fehler wiederholen, wenden Sie sich bitte an die pELS-Projektgruppe" + 
		//				args.ExceptionObject.ToString(), 
		//				"Fehlermeldung", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//		}

		private Cst_Server _Cst_Server = null;

		/// <summary>
		/// erstellt Objekt der Klasse Cpr_frm_Server
		/// </summary>
		/// <param name="pin_Cst_Server">Referenz auf das entsprechende STS-Objekt</param>
		public Cpr_frm_Server(Cst_Server pin_Cst_Server)
		{
			_Cst_Server = pin_Cst_Server;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			// initialisiere Form_Einsatzdaten
			this.FindForm().AddOwnedForm(_frm_Einsatzdaten);
			ErzeugeNotifyIconMenue();

			//			AppDomain.CurrentDomain.UnhandledException +=new 
			//				UnhandledExceptionEventHandler(UnhandledExceptionHandler);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Cpr_frm_Server));
			this.btn_ZeigeServerKonfig = new System.Windows.Forms.Button();
			this.btn_NeuerEinsatz = new System.Windows.Forms.Button();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.mI_Server = new System.Windows.Forms.MenuItem();
			this.mI_Server_neu_starten = new System.Windows.Forms.MenuItem();
			this.mI_Server_verstecken = new System.Windows.Forms.MenuItem();
			this.mI_Server_Konfiguration = new System.Windows.Forms.MenuItem();
			this.mI_Server_herunterfahren = new System.Windows.Forms.MenuItem();
			this.mI_Einsatz = new System.Windows.Forms.MenuItem();
			this.Einsatz_neu_anlegen = new System.Windows.Forms.MenuItem();
			this.mI_Einsatz_daten_bearbeiten = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.mI_Hilfe = new System.Windows.Forms.MenuItem();
			this.mI_Hilfe_Doku = new System.Windows.Forms.MenuItem();
			this.mI_Hilfe_Homepage = new System.Windows.Forms.MenuItem();
			this.mI_Hilfe_Info = new System.Windows.Forms.MenuItem();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.btn_Serverherunterfahren = new System.Windows.Forms.Button();
			this.btn_Oberflaeche_verstecken = new System.Windows.Forms.Button();
			this.lbl_warten = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btn_ZeigeServerKonfig
			// 
			this.btn_ZeigeServerKonfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btn_ZeigeServerKonfig.Location = new System.Drawing.Point(0, 32);
			this.btn_ZeigeServerKonfig.Name = "btn_ZeigeServerKonfig";
			this.btn_ZeigeServerKonfig.Size = new System.Drawing.Size(280, 32);
			this.btn_ZeigeServerKonfig.TabIndex = 0;
			this.btn_ZeigeServerKonfig.Text = "Server Konfiguration";
			this.btn_ZeigeServerKonfig.Click += new System.EventHandler(this.ZeigeServerkonfiguration);
			// 
			// btn_NeuerEinsatz
			// 
			this.btn_NeuerEinsatz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btn_NeuerEinsatz.Location = new System.Drawing.Point(0, 64);
			this.btn_NeuerEinsatz.Name = "btn_NeuerEinsatz";
			this.btn_NeuerEinsatz.Size = new System.Drawing.Size(280, 32);
			this.btn_NeuerEinsatz.TabIndex = 1;
			this.btn_NeuerEinsatz.Text = "Einsatzdaten bearbeiten";
			this.btn_NeuerEinsatz.Click += new System.EventHandler(this.ZeigeEinsatzdaten);
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mI_Server,
																					 this.mI_Einsatz,
																					 this.menuItem8,
																					 this.mI_Hilfe});
			this.mainMenu.RightToLeft = System.Windows.Forms.RightToLeft.No;
			// 
			// mI_Server
			// 
			this.mI_Server.Index = 0;
			this.mI_Server.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mI_Server_neu_starten,
																					  this.mI_Server_verstecken,
																					  this.mI_Server_Konfiguration,
																					  this.mI_Server_herunterfahren});
			this.mI_Server.Text = "&Server";
			// 
			// mI_Server_neu_starten
			// 
			this.mI_Server_neu_starten.Index = 0;
			this.mI_Server_neu_starten.Text = "&neu starten";
			this.mI_Server_neu_starten.Click += new System.EventHandler(this.Starten);
			// 
			// mI_Server_verstecken
			// 
			this.mI_Server_verstecken.Index = 1;
			this.mI_Server_verstecken.Text = "Oberfläche &verstecken";
			this.mI_Server_verstecken.Click += new System.EventHandler(this.VersteckeServerFenster);
			// 
			// mI_Server_Konfiguration
			// 
			this.mI_Server_Konfiguration.Index = 2;
			this.mI_Server_Konfiguration.Text = "&Konfiguration";
			this.mI_Server_Konfiguration.Click += new System.EventHandler(this.ZeigeServerkonfiguration);
			// 
			// mI_Server_herunterfahren
			// 
			this.mI_Server_herunterfahren.Index = 3;
			this.mI_Server_herunterfahren.Text = "&Beenden";
			this.mI_Server_herunterfahren.Click += new System.EventHandler(this.Beenden);
			// 
			// mI_Einsatz
			// 
			this.mI_Einsatz.Index = 1;
			this.mI_Einsatz.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.Einsatz_neu_anlegen,
																					   this.mI_Einsatz_daten_bearbeiten});
			this.mI_Einsatz.Text = "Einsatz";
			// 
			// Einsatz_neu_anlegen
			// 
			this.Einsatz_neu_anlegen.Index = 0;
			this.Einsatz_neu_anlegen.Text = "neuen Einsatz anlegen";
			this.Einsatz_neu_anlegen.Click += new System.EventHandler(this.Einsatz_neu_anlegen_Click);
			// 
			// mI_Einsatz_daten_bearbeiten
			// 
			this.mI_Einsatz_daten_bearbeiten.Index = 1;
			this.mI_Einsatz_daten_bearbeiten.Text = "aktuelle Einsatzdaten";
			this.mI_Einsatz_daten_bearbeiten.Click += new System.EventHandler(this.ZeigeEinsatzdaten);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 2;
			this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem9,
																					  this.menuItem10});
			this.menuItem8.Text = "Datenbestand";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 0;
			this.menuItem9.Text = "Exportieren aller Daten zum aktuellen Einsatz";
			this.menuItem9.Click += new System.EventHandler(this.ExportiereDatenbestand);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 1;
			this.menuItem10.Text = "Importieren aller Daten zum aktuellen Einsatz";
			this.menuItem10.Click += new System.EventHandler(this.ImportiereDatenbestand);
			// 
			// mI_Hilfe
			// 
			this.mI_Hilfe.Index = 3;
			this.mI_Hilfe.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mI_Hilfe_Doku,
																					 this.mI_Hilfe_Homepage,
																					 this.mI_Hilfe_Info});
			this.mI_Hilfe.Text = "?";
			// 
			// mI_Hilfe_Doku
			// 
			this.mI_Hilfe_Doku.Index = 0;
			this.mI_Hilfe_Doku.Text = "Dokumentation";
			// 
			// mI_Hilfe_Homepage
			// 
			this.mI_Hilfe_Homepage.Index = 1;
			this.mI_Hilfe_Homepage.Text = "Homepage";
			this.mI_Hilfe_Homepage.Click += new System.EventHandler(this.mI_Hilfe_Homepage_Click);
			// 
			// mI_Hilfe_Info
			// 
			this.mI_Hilfe_Info.Index = 2;
			this.mI_Hilfe_Info.Text = "Info";
			this.mI_Hilfe_Info.Click += new System.EventHandler(this.mI_Hilfe_Info_Click);
			// 
			// notifyIcon
			// 
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "project ELS";
			this.notifyIcon.Click += new System.EventHandler(this.ZeigeServerFenster);
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "pELS-Dateien| *.pels";
			this.openFileDialog.Title = "Importieren eines kompletten Datenbestands";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "pELS-Dateien|*.pels|Dateien für THWin|*.csv";
			this.saveFileDialog.Title = "Speichern des kompletten Datenbestands";
			// 
			// btn_Serverherunterfahren
			// 
			this.btn_Serverherunterfahren.BackColor = System.Drawing.Color.LightSteelBlue;
			this.btn_Serverherunterfahren.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btn_Serverherunterfahren.ForeColor = System.Drawing.Color.MidnightBlue;
			this.btn_Serverherunterfahren.Location = new System.Drawing.Point(0, 96);
			this.btn_Serverherunterfahren.Name = "btn_Serverherunterfahren";
			this.btn_Serverherunterfahren.Size = new System.Drawing.Size(280, 32);
			this.btn_Serverherunterfahren.TabIndex = 4;
			this.btn_Serverherunterfahren.Text = "Server beenden";
			this.btn_Serverherunterfahren.Click += new System.EventHandler(this.Beenden);
			// 
			// btn_Oberflaeche_verstecken
			// 
			this.btn_Oberflaeche_verstecken.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btn_Oberflaeche_verstecken.Location = new System.Drawing.Point(0, 0);
			this.btn_Oberflaeche_verstecken.Name = "btn_Oberflaeche_verstecken";
			this.btn_Oberflaeche_verstecken.Size = new System.Drawing.Size(280, 32);
			this.btn_Oberflaeche_verstecken.TabIndex = 5;
			this.btn_Oberflaeche_verstecken.Text = "Server verstecken";
			this.btn_Oberflaeche_verstecken.Click += new System.EventHandler(this.VersteckeServerFenster);
			// 
			// lbl_warten
			// 
			this.lbl_warten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_warten.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbl_warten.Location = new System.Drawing.Point(48, 32);
			this.lbl_warten.Name = "lbl_warten";
			this.lbl_warten.Size = new System.Drawing.Size(176, 64);
			this.lbl_warten.TabIndex = 6;
			this.lbl_warten.Text = "Processing!        Bitte warten...";
			this.lbl_warten.Visible = false;
			// 
			// Cpr_frm_Server
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.LightSteelBlue;
			this.ClientSize = new System.Drawing.Size(274, 128);
			this.Controls.Add(this.lbl_warten);
			this.Controls.Add(this.btn_Oberflaeche_verstecken);
			this.Controls.Add(this.btn_Serverherunterfahren);
			this.Controls.Add(this.btn_NeuerEinsatz);
			this.Controls.Add(this.btn_ZeigeServerKonfig);
			this.ForeColor = System.Drawing.Color.MidnightBlue;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mainMenu;
			this.Name = "Cpr_frm_Server";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "pELS - Server";
			this.ResumeLayout(false);

		}
		#endregion

		//		/// <summary>
		//		/// The main entry point for the application.
		//		/// </summary>
		//		[STAThread]

		/// <summary>
		/// erzeugt das NotifyIconMenü
		/// </summary>
		private void ErzeugeNotifyIconMenue()
		{
			ContextMenu notifyIcon_Menue= new ContextMenu();
			MenuItem menuItem;
			// Eintrag zum SystemTray-Icon hinzufügen
			menuItem = 
				new MenuItem("Server-Fenster anzeigen", 
				new EventHandler(ZeigeServerFenster));
			notifyIcon_Menue.MenuItems.Add(menuItem);
			// Eintrag zum SystemTray-Icon hinzufügen
			menuItem = 
				new MenuItem("Serverkonfiguration", 
				new EventHandler(ZeigeServerkonfiguration));
			notifyIcon_Menue.MenuItems.Add(menuItem);
			// Eintrag zum SystemTray-Icon hinzufügen
			menuItem = 
				new MenuItem("Einsatzdaten bearbeiten", 
				new EventHandler(ZeigeEinsatzdaten));
			notifyIcon_Menue.MenuItems.Add(menuItem);
			// Eintrag zum SystemTray-Icon hinzufügen
			menuItem = 
				new MenuItem("Server herunterfahren", 
				new EventHandler(Beenden));
			notifyIcon_Menue.MenuItems.Add(menuItem);

			notifyIcon.ContextMenu = notifyIcon_Menue;
		}

		/// <summary>
		///  versteckt das Fenster
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VersteckeServerFenster(object sender, System.EventArgs e)
		{
			this.notifyIcon.Visible  = true;
			this.ShowInTaskbar = false;
			this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			this.WindowState = FormWindowState.Minimized;
		}

		/// <summary>
		/// zeigt das Fenster
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ZeigeServerFenster(object sender, System.EventArgs e)
		{
			this.ShowInTaskbar = true;
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.WindowState = FormWindowState.Normal;
			this.notifyIcon.Visible  = false;
		}


		/// <summary>
		/// lädt die gespeicherte Serverkonfiguration 
		/// und zeigt diese im ServerKonfigurationsfernster an
		/// speichert die neue Auswahl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ZeigeServerkonfiguration(object sender, System.EventArgs e)
		{
			Cpr_frm_ServerKonfiguration frm_serverkonfig = new Cpr_frm_ServerKonfiguration();
			this.FindForm().AddOwnedForm(frm_serverkonfig);
			frm_serverkonfig.ShowDialog();
			if (frm_serverkonfig.EingabeErfolgreich)
				this.Starten(null, null);

		}

	
		/// <summary>
		/// zeigt das Fenster für Einsatzdaten
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ZeigeEinsatzdaten(object sender, System.EventArgs e)
		{
			_frm_Einsatzdaten = new Cpr_frm_Einsatzdaten(ref _Cst_Server);
			this.FindForm().AddOwnedForm(_frm_Einsatzdaten);
			_frm_Einsatzdaten.ShowDialog();
		}

		/// <summary>
		/// startet das Exportieren des Datenbestands
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExportiereDatenbestand(object sender, System.EventArgs e)
		{
			if (saveFileDialog.ShowDialog() == DialogResult.OK) 
			{
				_Cst_Server.ExportiereDatenbestand(saveFileDialog.FileName);
				this.EingabeSperre();
			}
		}
	
		/// <summary>
		/// Startet das Importieren des Datenbestands
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImportiereDatenbestand(object sender, System.EventArgs e)
		{
			if(openFileDialog.ShowDialog() == DialogResult.OK)
			{
				_Cst_Server.ImportiereDatenbestand(openFileDialog.FileName);
				this.EingabeSperre();
			}
		}




		protected override void OnClosing(CancelEventArgs e)
		{
			DialogResult dr_beenden = new DialogResult();
			dr_beenden = MessageBox.Show("Möchten Sie den Server wirklich beenden?\n\n"
				+"Angemeldete Clients verlieren nicht gespeicherte Daten",
				"Server wirklich beenden?",
				MessageBoxButtons.YesNo, 
				MessageBoxIcon.Question);
			if(dr_beenden == DialogResult.No)
			{				
				e.Cancel = true;
			}
			else
			{
				//Nur damit nicht nach Programmende noch das Notify Icon rumhängt
				notifyIcon.Visible=false;
			}

			base.OnClosing (e);
		}



		/// <summary>
		/// startet die Serverroutine
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Starten(object sender, System.EventArgs e)
		{
			_Cst_Server.StarteServerRoutine();
		}
		
		/// <summary>
		/// startet das Beenden der Serverroutine
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Beenden(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		/// <summary>
		/// Verhindert und erlaubt Eingaben während und nach Backups
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void EingabeSperre()
		{
			this.lbl_warten.Visible = !this.lbl_warten.Visible; 
			this.Enabled = !this.Enabled;
		}

		private void mI_Hilfe_Homepage_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.els.sigmadelta.de");
		}

		private void mI_Hilfe_Info_Click(object sender, System.EventArgs e)
		{					
			pELS.GUI.PopUp.CPopUp.ZeigeInfo();
		}

		private void Einsatz_neu_anlegen_Click(object sender, System.EventArgs e)
		{	
			Cpr_frm_neuerEinsatz frm_neuerEinsatz = new Cpr_frm_neuerEinsatz();			
			frm_neuerEinsatz.ShowDialog();			
			if(frm_neuerEinsatz.EingabeErfolgreich)
			{
				//GUI sperren
				this.EingabeSperre();
							
				//neuen einsatz anlegen
				this._Cst_Server.neuerEinsatz(frm_neuerEinsatz.neuerDBName);
				//Server neu starten
				this.Starten(null, null);				
				
			
				//GUI wieder frei schalten
				this.EingabeSperre();
				
			}	
		}

	}

}
