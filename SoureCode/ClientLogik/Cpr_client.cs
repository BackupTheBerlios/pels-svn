using System;
// benötigt für: Bild laden
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using pELS.GUI.Interface;
using pELS.Tools.Client;
// benötigt für: IEnumerator
using System.Collections;


//using System.IO;
using System.Reflection;

#region Dokumentation

/**
Erläuterung:
muss noch gemacht werden

erstellt von:	Xiao, AlexG, Quecky		am: 1.November.2004
geändert von:	Quecky					am:	10.November.2004		
geändert von:	Quecky					am: 17.November.2004 
geändert von:	Schuppe					am: 1.Dezember.2004 
  review von:			am:
getestet von:			am:
usw.

aktuelle Version: 0.1 alpha

History/Hinweise/Bekannte Bugs:
- Wie komme ich nach dem TypeCast an die im Interface beschriebenen Methoden
- 10.11 Namenskonventionen durchgesetzt
- 17.11 Namenskonventionen + Bug bei Rollenwechsel zu AGT Operator beseitigt
- 1.12	Fenster wird zentriert auf den Bildschirm geladen
- 1.12	Panelgröße von pnl_icons verändert, so dass nur noch der vertikale Scrollbar erscheint

- es wird immer das falsche UC geladen
**/
#endregion


namespace pELS.Client
{
	/// <summary>
	/// Zusammenfassung für pels.
	/// </summary>
	public class Cpr_Client : System.Windows.Forms.Form
	{
		#region Klassenvariablen

		//Hauptmenü
		private System.Windows.Forms.MainMenu mm_pELS;
		private System.Windows.Forms.MenuItem mI_Datei;
		private System.Windows.Forms.MenuItem mI_Datei_Beenden;
		private System.Windows.Forms.MenuItem mI_Rollenwechsel;
		private System.Windows.Forms.MenuItem mI_Hilfe;
		private System.Windows.Forms.Panel pnl_Icons;		
		public System.Windows.Forms.GroupBox gbx_Softwarebauelement;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList iml_Toolbar;
		private System.ComponentModel.Container ctr_components = null;
		private System.Windows.Forms.HelpProvider pelsHelp;
		private System.Windows.Forms.MenuItem mI_Hilfe_About;
		private System.Windows.Forms.MenuItem mI_Hilfe_Dokumentation;
		private Cst_Client _stClient;
		private System.Windows.Forms.MenuItem mI_Hilfe_Homepage;
		
		
		
		#endregion

	

		#region Konstruktoren und Destruktor
		public Cpr_Client(Cst_Client pin_stClient)
		{
			// Erforderlich für die Windows Form-Designerunterstützung
			InitializeComponent();
			_stClient = pin_stClient;
			LadeBenutzerRollen();
			// Setze Helpfile
			this.pelsHelp.HelpNamespace = _stClient.Einstellung.Helpfile;
			this.pelsHelp.SetHelpKeyword(this, "Client");
			this.pelsHelp.SetShowHelp(this, true);
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (ctr_components != null) 
				{
					ctr_components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Cpr_Client));
			this.mm_pELS = new System.Windows.Forms.MainMenu();
			this.mI_Datei = new System.Windows.Forms.MenuItem();
			this.mI_Datei_Beenden = new System.Windows.Forms.MenuItem();
			this.mI_Rollenwechsel = new System.Windows.Forms.MenuItem();
			this.mI_Hilfe = new System.Windows.Forms.MenuItem();
			this.mI_Hilfe_Dokumentation = new System.Windows.Forms.MenuItem();
			this.mI_Hilfe_Homepage = new System.Windows.Forms.MenuItem();
			this.mI_Hilfe_About = new System.Windows.Forms.MenuItem();
			this.gbx_Softwarebauelement = new System.Windows.Forms.GroupBox();
			this.iml_Toolbar = new System.Windows.Forms.ImageList(this.components);
			this.pnl_Icons = new System.Windows.Forms.Panel();
			this.pelsHelp = new System.Windows.Forms.HelpProvider();
			this.SuspendLayout();
			// 
			// mm_pELS
			// 
			this.mm_pELS.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mI_Datei,
																					this.mI_Rollenwechsel,
																					this.mI_Hilfe});
			// 
			// mI_Datei
			// 
			this.mI_Datei.Index = 0;
			this.mI_Datei.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mI_Datei_Beenden});
			this.mI_Datei.Text = "Programm";
			// 
			// mI_Datei_Beenden
			// 
			this.mI_Datei_Beenden.Index = 0;
			this.mI_Datei_Beenden.Text = "Beenden";
			this.mI_Datei_Beenden.Click += new System.EventHandler(this.mI_Datei_Beenden_Click);
			// 
			// mI_Rollenwechsel
			// 
			this.mI_Rollenwechsel.Index = 1;
			this.mI_Rollenwechsel.RadioCheck = true;
			this.mI_Rollenwechsel.Text = "Rollenwechsel";
			// 
			// mI_Hilfe
			// 
			this.mI_Hilfe.Index = 2;
			this.mI_Hilfe.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mI_Hilfe_Dokumentation,
																					 this.mI_Hilfe_Homepage,
																					 this.mI_Hilfe_About});
			this.mI_Hilfe.Text = "?";
			// 
			// mI_Hilfe_Dokumentation
			// 
			this.mI_Hilfe_Dokumentation.Index = 0;
			this.mI_Hilfe_Dokumentation.Text = "Dokumentation";
			this.mI_Hilfe_Dokumentation.Click += new System.EventHandler(this.mI_Hilfe_Dokumentation_Click);
			// 
			// mI_Hilfe_Homepage
			// 
			this.mI_Hilfe_Homepage.Index = 1;
			this.mI_Hilfe_Homepage.Text = "Homepage";
			this.mI_Hilfe_Homepage.Click += new System.EventHandler(this.mI_Hilfe_Homepage_Click);
			// 
			// mI_Hilfe_About
			// 
			this.mI_Hilfe_About.Index = 2;
			this.mI_Hilfe_About.Text = "Info";
			this.mI_Hilfe_About.Click += new System.EventHandler(this.mI_Hilfe_About_Click);
			// 
			// gbx_Softwarebauelement
			// 
			this.gbx_Softwarebauelement.Location = new System.Drawing.Point(88, 8);
			this.gbx_Softwarebauelement.Name = "gbx_Softwarebauelement";
			this.gbx_Softwarebauelement.Size = new System.Drawing.Size(650, 530);
			this.gbx_Softwarebauelement.TabIndex = 1;
			this.gbx_Softwarebauelement.TabStop = false;
			// 
			// iml_Toolbar
			// 
			this.iml_Toolbar.ImageSize = new System.Drawing.Size(16, 16);
			this.iml_Toolbar.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// pnl_Icons
			// 
			this.pnl_Icons.AutoScroll = true;
			this.pnl_Icons.Location = new System.Drawing.Point(10, 15);
			this.pnl_Icons.Name = "pnl_Icons";
			this.pnl_Icons.Size = new System.Drawing.Size(70, 0);
			this.pnl_Icons.TabIndex = 0;
			// 
			// Cpr_Client
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(746, 547);
			this.Controls.Add(this.gbx_Softwarebauelement);
			this.Controls.Add(this.pnl_Icons);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mm_pELS;
			this.Name = "Cpr_Client";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "project.ELS";
			this.Load += new System.EventHandler(this.pels_Load);
			this.ResumeLayout(false);

		}
		#endregion
	
		#region Funktionalität
	

		/// <summary>
		/// lädt alle definierten Systemrollen und stellt diese im 
		/// Menü dar
		/// jeder Eintrag wird mit einem Eventhandler verknüpft
		/// </summary>
		private void LadeBenutzerRollen()
		{
			// erstelle einen Menüeintrag für den Benutzernamen
			MenuItem MI_BenutzerName = new MenuItem("Benutzer: " + "Benutzername");
			MI_BenutzerName.Enabled = false;
			MI_BenutzerName.BarBreak = true;
			this.mm_pELS.MenuItems[1].MenuItems.Add(MI_BenutzerName);

			foreach(pELS.Tdv_Systemrolle sr in 
				Enum.GetValues(typeof(pELS.Tdv_Systemrolle)))
			{
				MenuItem tmpMI = this.mm_pELS.MenuItems[1].MenuItems.Add(sr.ToString());
				tmpMI.Click +=new EventHandler(this.mI_Rollenwechsel_Click);
			}
		}

		/// <summary>
		/// setzt im Menü die übergebene Rolle
		/// </summary>
		/// <param name="pin_Systemrolle"></param>
		public void SetzeBenutzer(string pin_BenutzerName, Tdv_Systemrolle pin_Systemrolle)
		{

			this.mm_pELS.MenuItems[1].MenuItems[0].Text = "Benutzer: " + pin_BenutzerName;

			foreach(MenuItem MI in 
				this.mm_pELS.MenuItems[1].MenuItems)
			{
				// setze so, dass keine Rolle ausgewählt ist
				MI.Checked = false;
				if (MI.Text == pin_Systemrolle.ToString())
				{
					// wähle den aktuellen Menüpunkt aus
					MI.Checked = true;
					break;
				}
			}
		}


		// Dynamisches vergrößern des Auswahlfenster und einfügen der Icons und Beschriftung 
		public void ErweitereAuswahlliste(string pin_str_Beschriftung, Image pin_im_Bild, int pin_i_Index)
		{
			// Konstanten zur Angabe der Icongröße
			const int i_IconGroesse = 50;
			const int i_LabelHoehe = 20;
			const int i_LabelBreite = 70;

			// Vergrößern die Auswahlliste, um ein weiteres Icon einzuladen
			this.pnl_Icons.Height += i_IconGroesse + i_LabelHoehe;
			

			// Deklarieren des Icons
			Button btn_Icon = new System.Windows.Forms.Button();
			Label lbl_Icon = new System.Windows.Forms.Label();

			// Hinzufügen des Icons zur Liste
			this.pnl_Icons.Controls.Add(btn_Icon);
			this.pnl_Icons.Controls.Add(lbl_Icon);
			

		
			// Eigenschaften des Icons
			btn_Icon.Name = pin_i_Index.ToString();
			btn_Icon.Image = pin_im_Bild;
			btn_Icon.TabIndex = this.pnl_Icons.Controls.Count;			
			btn_Icon.Size = new System.Drawing.Size(i_IconGroesse, i_IconGroesse);
			btn_Icon.Location = new System.Drawing.Point (0,pnl_Icons.Height - i_LabelHoehe - i_IconGroesse);
			lbl_Icon.Text = pin_str_Beschriftung;
			lbl_Icon.Height = i_LabelHoehe;
			lbl_Icon.Width = i_LabelBreite;
			lbl_Icon.Location = new System.Drawing.Point(0, btn_Icon.Location.Y + i_IconGroesse);

			// Eventhandler falls ein Icon gedrückt wird
			btn_Icon.Click += new System.EventHandler(this.btn_Icon_Click);		
		}
		

	
		#endregion

		// Der Haupteinstiegspunkt für die Anwendung.
		#region Eventhandler	

		// Laden aller SBEs beim Systemstart
		private void pels_Load(object sender, System.EventArgs e)
		{
//			this.LadenAllerAssemblies();
//			LadenDerAktuellenTermine();
		}
		


		
	
		// Rollenwechsel				
		private void mI_Rollenwechsel_Click(object sender, System.EventArgs e)
		{
			foreach (MenuItem mi in (this.mI_Rollenwechsel).MenuItems)
			{
				if (mi.IsParent) 
				{
					// Enumarator auf alle Untermenüitems
					IEnumerator ie = (mi.MenuItems).GetEnumerator();
					while(ie.MoveNext())
					{
						((MenuItem)ie.Current).Checked = false;
					}
				}
				else mi.Checked = false;

			}
			(sender as MenuItem).Checked = true;

			//Hier wird der Rollenwechsel durch dei ST-Schicht angestoßen
			_stClient.Rollenwechsel((sender as MenuItem).Text);
		}

		
		// Ausgewähltes Softwarebauelement starten
		private void btn_Icon_Click(object sender, System.EventArgs e)
		{
			int i_index = Int32.Parse((sender as System.Windows.Forms.Button).Name);
			//Button zu aktuell ausgewähltem SBE hervorheben
			this.gewaehltesSBEhervorheben(sender, e);
			_stClient.StarteSBE(i_index);
		}

		//Button zu aktuell ausgewähltem SBE hervorheben (über änderung des Flatstyle)
		private void gewaehltesSBEhervorheben(object sender, System.EventArgs e)
		{
			//erst in der Aufzählung auf Standard zurück setzen
			foreach (Object o in this.pnl_Icons.Controls)
			{
				if(o is Button)
				{
					(o as Button).FlatStyle = FlatStyle.Standard;
				}
			}
			//gedrückten Button auf hervorheben
			(sender as System.Windows.Forms.Button).FlatStyle = FlatStyle.Flat;

		}

		// Beenden des Programms
		// Dabei Abfragen, ob alle Portale beendet werden können
		private void mI_Datei_Beenden_Click(object sender, System.EventArgs e)
		{
			_stClient.Beenden();
		}

		// Hilfedatei öffnen
		private void mI_Hilfe_Dokumentation_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(this, _stClient.Einstellung.Helpfile);
		}

		//Programminformationen anzeigen
		private void mI_Hilfe_About_Click(object sender, System.EventArgs e)
		{
			//TODO: pels.gui.popup.cpopup.zeigeInfo();
			pELS.GUI.PopUp.CPopUp.ZeigeInfo();
			
		}


		private void mI_Hilfe_Homepage_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start("http://els.sigmadelta.de");
		}

		#endregion



	}
}
