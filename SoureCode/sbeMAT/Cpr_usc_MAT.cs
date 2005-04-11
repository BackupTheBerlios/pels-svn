using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;


using pELS.DV;
using pELS.GUI.PopUp;
using pELS.GUI;
using pELS.Client;
using pELS.Tools.Client;

namespace pELS.Client.MAT
{
	#region Dokumentation
	/**
		Erläuterung:
		Implementiert die GUI zum Softwarebauelement MAT

		erstellt von:   Xiao, AlexG, Quecky		am: 1.November.2004
		geändert von:	AlexG					am: 7.Novemver.2004				
		geändert von:	AlexG	 				am:	12.11.04
		geändert von:	Steini					am:	16.11.04 
		geändert von:	Quecky					am: 17.November.2004 
		geändert von:	Quecky					am: 18.November.2004 
		geändert von:	Hütte					am: 21.November.2004 
		geändert von:	Hütte					am: 25.November.2004 
		geändert von:	Quecky					am: 25.November.2004 
		geändert von:	Hütte					am: 26.November.2004 
		geändert von:	Hütte					am: 28.November.2004 
		review von:								am:
		getestet von:							am:

		aktuelle Version: 0.2

		History:
		- 17.11		- Fehler beim zurücksetzten am Datum behoben, Doku angelegt
					- ZÜM F40 noch nicht ganz vollständig umgesetzt 
		- 18.11		- ZÜM F40 und F50 umgesetzt
					- Rollensystem fertig
		- 21.11		- Methoden zum Zurücksetzen der Tabs MeldungErfassen, MeldungAbfassen, AutragEingeben
					- Tab TerminErstellen fertig.
		- 22.11		- Beim Zurücksetzen: MeldungsArt wird nicht mehr verändert
		- 25.11		- Aufträge nachverfolgen
		- 25.11		- Korrektes Einbinden der PopUps zum Verwerfen und Speichern
		- 26.11		- Druck-Button mit Drucken TabPage
		- 28.11		- Datagrid Beispielfüllung

	
		Hinweise/Bekannte Bugs:
		- Rollenrechte sind nicht getestet
		**/
					

	#region History/Hinweise/Bekannte Bugs:
	/**	
	
		**/
	#endregion
	
	#endregion

	public class Cpr_usc_MAT : System.Windows.Forms.UserControl
	{
		#region Klassenvariablen

		private System.Windows.Forms.TabPage tabpage_Auftrag;
		private System.Windows.Forms.TabPage tabpage_MeldungErfassen;
		private System.Windows.Forms.TabPage tabpage_MeldungAbfassen;
		private System.Windows.Forms.TabPage tabpage_Termin;
		private System.Windows.Forms.TextBox txt_MeldungAbfassen_Erkundungsobjekt;
		private System.Windows.Forms.TabPage tabpage_Drucken;
		
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TabControl tabctrl_MAT;
		private bool b_tabPageDruckenSchließen = false;
		#endregion



		#region Instanzvairable
		private Cst_MAT _stMAT = null;
		private usc_Meldung _meldungAbfassen;
		private usc_Meldung _meldungErfassen;
		private usc_Auftrag _auftrag;
		private usc_Termin _termin;
		private System.Windows.Forms.TabPage tabpage_AuftraegeNachverfolgen;
		private System.Windows.Forms.HelpProvider pelsHelp;
		private usc_AuftraegeNachverfolgen _auftraegeNachverfolgen;
		#endregion
		
		#region Konstruktoren & Destruktoren
		public Cpr_usc_MAT(Cst_MAT pin_MAT)
		{
			// Dieser Aufruf ist für den Windows Form-Designer erforderlich.

			InitializeComponent();
			//			FuellenDatagridTermine();
			//			FuellenDatagridAuftraegeNachverfolgen();
			
			// Initialisiere _stMAT 
			this._stMAT = pin_MAT;
			
			// Alle Methoden, die mit _stMAt zu tun haben, folgen hier
			// Initialisiere alle Steuerelemente am Starten von MAT
			_meldungAbfassen = new usc_Meldung (pin_MAT, true);
			_meldungErfassen = new usc_Meldung (pin_MAT, false);
			_auftrag = new usc_Auftrag(pin_MAT);
			_termin = new usc_Termin(pin_MAT);
			_auftraegeNachverfolgen = new usc_AuftraegeNachverfolgen(pin_MAT);

			InitAlleTabs();
			// Hilfe festlegen
			SetzeHilfe();
		}

	
		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Vom Komponenten-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabctrl_MAT = new System.Windows.Forms.TabControl();
			this.tabpage_MeldungAbfassen = new System.Windows.Forms.TabPage();
			this.tabpage_Auftrag = new System.Windows.Forms.TabPage();
			this.tabpage_AuftraegeNachverfolgen = new System.Windows.Forms.TabPage();
			this.tabpage_MeldungErfassen = new System.Windows.Forms.TabPage();
			this.tabpage_Termin = new System.Windows.Forms.TabPage();
			this.txt_MeldungAbfassen_Erkundungsobjekt = new System.Windows.Forms.TextBox();
			this.tabpage_Drucken = new System.Windows.Forms.TabPage();
			this.pelsHelp = new System.Windows.Forms.HelpProvider();
			this.tabctrl_MAT.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabctrl_MAT
			// 
			this.tabctrl_MAT.Controls.Add(this.tabpage_MeldungAbfassen);
			this.tabctrl_MAT.Controls.Add(this.tabpage_Auftrag);
			this.tabctrl_MAT.Controls.Add(this.tabpage_AuftraegeNachverfolgen);
			this.tabctrl_MAT.Controls.Add(this.tabpage_MeldungErfassen);
			this.tabctrl_MAT.Controls.Add(this.tabpage_Termin);
			this.tabctrl_MAT.Location = new System.Drawing.Point(5, 5);
			this.tabctrl_MAT.Name = "tabctrl_MAT";
			this.tabctrl_MAT.SelectedIndex = 0;
			this.tabctrl_MAT.Size = new System.Drawing.Size(645, 525);
			this.tabctrl_MAT.TabIndex = 4;
			// 
			// tabpage_MeldungAbfassen
			// 
			this.tabpage_MeldungAbfassen.Location = new System.Drawing.Point(4, 22);
			this.tabpage_MeldungAbfassen.Name = "tabpage_MeldungAbfassen";
			this.tabpage_MeldungAbfassen.Size = new System.Drawing.Size(637, 499);
			this.tabpage_MeldungAbfassen.TabIndex = 3;
			this.tabpage_MeldungAbfassen.Text = "Meldung abfassen";
			// 
			// tabpage_Auftrag
			// 
			this.tabpage_Auftrag.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Auftrag.Name = "tabpage_Auftrag";
			this.tabpage_Auftrag.Size = new System.Drawing.Size(637, 499);
			this.tabpage_Auftrag.TabIndex = 0;
			this.tabpage_Auftrag.Text = "Auftrag eingeben";
			// 
			// tabpage_AuftraegeNachverfolgen
			// 
			this.tabpage_AuftraegeNachverfolgen.Location = new System.Drawing.Point(4, 22);
			this.tabpage_AuftraegeNachverfolgen.Name = "tabpage_AuftraegeNachverfolgen";
			this.tabpage_AuftraegeNachverfolgen.Size = new System.Drawing.Size(637, 499);
			this.tabpage_AuftraegeNachverfolgen.TabIndex = 5;
			this.tabpage_AuftraegeNachverfolgen.Text = "Auftraege nachverfolgen";
			// 
			// tabpage_MeldungErfassen
			// 
			this.tabpage_MeldungErfassen.Location = new System.Drawing.Point(4, 22);
			this.tabpage_MeldungErfassen.Name = "tabpage_MeldungErfassen";
			this.tabpage_MeldungErfassen.Size = new System.Drawing.Size(637, 499);
			this.tabpage_MeldungErfassen.TabIndex = 2;
			this.tabpage_MeldungErfassen.Text = "Meldung erfassen";
			// 
			// tabpage_Termin
			// 
			this.tabpage_Termin.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Termin.Name = "tabpage_Termin";
			this.tabpage_Termin.Size = new System.Drawing.Size(637, 499);
			this.tabpage_Termin.TabIndex = 4;
			this.tabpage_Termin.Text = "Termin erstellen";
			// 
			// txt_MeldungAbfassen_Erkundungsobjekt
			// 
			this.txt_MeldungAbfassen_Erkundungsobjekt.Location = new System.Drawing.Point(0, 0);
			this.txt_MeldungAbfassen_Erkundungsobjekt.Name = "txt_MeldungAbfassen_Erkundungsobjekt";
			this.txt_MeldungAbfassen_Erkundungsobjekt.TabIndex = 0;
			this.txt_MeldungAbfassen_Erkundungsobjekt.Text = "";
			// 
			// tabpage_Drucken
			// 
			this.tabpage_Drucken.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Drucken.Name = "tabpage_Drucken";
			this.tabpage_Drucken.Size = new System.Drawing.Size(637, 499);
			this.tabpage_Drucken.TabIndex = 5;
			this.tabpage_Drucken.Text = "Drucken";
			// 
			// Cpr_usc_MAT
			// 
			this.Controls.Add(this.tabctrl_MAT);
			this.Name = "Cpr_usc_MAT";
			this.Size = new System.Drawing.Size(650, 530);
			this.tabctrl_MAT.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region get-, set Methoden
		
		public Cst_MAT stMAT
		{
			get{return this._stMAT;}
			set{this._stMAT = value;}
		}

		#endregion

		private void InitAlleTabs()
		{
			//TODO: noch zwei tabs
			this.tabpage_MeldungAbfassen.Controls.Add(this._meldungAbfassen);
			this.tabpage_MeldungErfassen.Controls.Add(this._meldungErfassen);
			this.tabpage_Auftrag.Controls.Add(this._auftrag);
			this.tabpage_AuftraegeNachverfolgen.Controls.Add(this._auftraegeNachverfolgen);
			this.tabpage_Termin.Controls.Add(this._termin);

		}

		private void SetzeHilfe()
		{
			this.pelsHelp.HelpNamespace = _stMAT.Einstellung.Helpfile;
			this.pelsHelp.SetShowHelp(this,true);
			this.pelsHelp.SetHelpKeyword(this,"Aufträge");

		}
		#region Setzen der Rollenrechte
		//TODO: impelementation ist noch unvollständig.
		//Test steht noch aus.

		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
		{
			// akualisiere den aktuellen Benutzer aller user-Controls
			this.AktualisiereAktBenutzer();

			Tdv_Systemrolle rolle = (Tdv_Systemrolle)pin_i_aktuelleRolle;
			this.tabctrl_MAT.TabPages.Clear();
			
			switch (rolle)
			{
					//Haben alle die gleichen Rechte (ALLE)
				case Tdv_Systemrolle.Zugführer: 
				case Tdv_Systemrolle.Zugtruppführer:
				case Tdv_Systemrolle.Führungsgehilfe:
				{
					//F40
					this.tabctrl_MAT.TabPages.Add(this.tabpage_MeldungAbfassen);
					//F50
					this.tabctrl_MAT.TabPages.Add(this.tabpage_MeldungErfassen);
					//F140
					this.tabctrl_MAT.TabPages.Add(this.tabpage_Auftrag);
					//F145
					this.tabctrl_MAT.TabPages.Add(this.tabpage_AuftraegeNachverfolgen);
					//F220
					this.tabctrl_MAT.TabPages.Add(this.tabpage_Termin);
					break;
				}

					//Haben alle die gleichen Rechte (F40,F140,F145,F220)
				case Tdv_Systemrolle.Einsatzleiter:
				case Tdv_Systemrolle.LeiterFüSt:
				case Tdv_Systemrolle.LeiterStab:
				{	
					//F40
					this.tabctrl_MAT.TabPages.Add(this.tabpage_MeldungAbfassen);
					//F140
					this.tabctrl_MAT.TabPages.Add(this.tabpage_Auftrag);
					//F145
					this.tabctrl_MAT.TabPages.Add(this.tabpage_AuftraegeNachverfolgen);
					//F220
					this.tabctrl_MAT.TabPages.Add(this.tabpage_Termin);
					break;
				}

					//Haben alle die gleichen Rechte (F145,F220)
				case Tdv_Systemrolle.Sichter :
				{	
					//F145
					this.tabctrl_MAT.TabPages.Add(this.tabpage_AuftraegeNachverfolgen);
					//F220
					this.tabctrl_MAT.TabPages.Add(this.tabpage_Termin);
					break;
				}

					//Haben alle die gleichen Rechte (F50,F145,F220)
				case Tdv_Systemrolle.Fernmelder :
				{	
					//F50
					this.tabctrl_MAT.TabPages.Add(this.tabpage_MeldungErfassen);
					//F145
					this.tabctrl_MAT.TabPages.Add(this.tabpage_AuftraegeNachverfolgen);
					//F220
					this.tabctrl_MAT.TabPages.Add(this.tabpage_Termin);
					break;
				}
				
					//Haben alle die gleichen Rechte (F40,F145,F220)
				case Tdv_Systemrolle.S1:
				case Tdv_Systemrolle.S2:
				case Tdv_Systemrolle.S4: 
				case Tdv_Systemrolle.S5: 
				case Tdv_Systemrolle.S6:
				{	
					//F40
					this.tabctrl_MAT.TabPages.Add(this.tabpage_MeldungAbfassen);
					//F145
					this.tabctrl_MAT.TabPages.Add(this.tabpage_AuftraegeNachverfolgen);
					//F220
					this.tabctrl_MAT.TabPages.Add(this.tabpage_Termin);
					break;
				}

					//Haben alle die gleichen Rechte (F40,F140,F145,F220)
				case Tdv_Systemrolle.S3 :
				{	
					//F40
					this.tabctrl_MAT.TabPages.Add(this.tabpage_MeldungAbfassen);
					//F140
					this.tabctrl_MAT.TabPages.Add(this.tabpage_Auftrag);
					//F145
					this.tabctrl_MAT.TabPages.Add(this.tabpage_AuftraegeNachverfolgen);
					//F220
					this.tabctrl_MAT.TabPages.Add(this.tabpage_Termin);
					break;
				}
				

				default:	break;
			}
		}

		#endregion


		#region dynamische Daten-Akualisierung
		/// <summary>
		/// Wenn die Menge aller Einsatzschwerpunkte verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AkualisiereESP()
		{
			this._meldungErfassen.AkualisiereESP();
			this._meldungAbfassen.AkualisiereESP();

			
		}
		/// <summary>
		/// Wenn die Menge aller Benutzer verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>		
		public void AktualisiereBenutzer()
		{
			this._auftrag.AktualisiereBenutzer();
			this._meldungAbfassen.AktualisiereBenutzer();
			this._meldungErfassen.AktualisiereBenutzer();
			this._auftrag.AktualisiereBenutzer();
			
			
		}
		/// <summary>
		/// Wenn die Menge aller Einheiten, Helfer, oder Kfz verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AktualisiereEHK()
		{
			this._auftrag.AktualisiereEHK();
			this._meldungAbfassen.AktualisiereEHK();
			this._meldungErfassen.AktualisiereEHK();
		}

		/// <summary>
		/// Wenn die Menge aller Aufträge verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AktualisiereAuftrag()
		{
			this._auftraegeNachverfolgen.AktualisiereAuftrag();
		}

		/// <summary>
		/// Wenn die Systemrolle des aktuellen Benutzers verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AktualisiereAktBenutzer()
		{
			this._meldungAbfassen.AkualisiereAktBenutzer();
			this._meldungErfassen.AkualisiereAktBenutzer();
			this._auftrag.AkualisiereAktBenutzer();
			this._termin.AkualisiereAktBenutzer();
		}
	

		#endregion
	

	}		
}
