using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using pELS.DV;
// benötigt für: Cst_Einstellung
using pELS.Tools.Client;
// benötigt für: Cst_PortalLogik
using pELS.Client;
// benötigt für: IPortalLogik_XXX
using pELS.APS.Server.Interface;
using pELS.GUI.PopUp;

//// benötigt für: Initialisierung des Proxy-Objekts von IPortalLogik_allgFkt
//using pELS.Client.PortalLogik_allgFkt;

namespace pELS.Client.EK
{
	#region Dokumentation
	/**
	Erläuterung:
	Implementiert die GUI zum Softwarebauelement Einsatz und Kräfte

	erstellt von:	Hütte					am: 23.11.04
	geändert von:	Steini & Michal			am: 24.11.04
	geändert von:	Steini					am: 25.11.04
	geändert von:	alexG					am: 25.11.04
	geändert von:	Michal					am: 25.11.04
	geändert von:	Steini					am: 26.11.04
	geändert von:	Steini					am: 27.11.04
	geändert von:	Steini					am: 28.11.04
	review von:		Schuppe					am: 29.11.04
	geändert von:	Steini					am: 29.11.04
	getestet von:							am:

	aktuelle Version: 0.9

	History:
	- 23.11		- Tabs erstellt Use-Cases zugeordnet
				- Beginn: Einsatzschwerpunkte implementieren
	- 24.11     - trv_Einsatzmanager angefangen.
	- 25.11		- F150, F155, F160 GUI implementiert (alexG)
	- 25.11		- Grundlagen von Drag'n Drop implementiert
	- 27.11		- Review von Schuppe
	Hinweise/Bekannte Bugs:
	
	**/
				
	#region letzte Änderungen
	/**
		private void trv_Einsatzmanager_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		ermittelt den zu selektierenden Knoten bei den jeweiligen Mausevents. 		
	**/
	#endregion

	#region History/Hinweise/Bekannte Bugs:
	/**
	- Rollenrechte müssen implementiert werden
	**/
	#endregion
	
	#endregion
	public class Cpr_usc_EK : System.Windows.Forms.UserControl
	{
		#region Klassenvariablen
		private System.Windows.Forms.TabControl tabctrl_EK;
		private System.Windows.Forms.TabPage tabpage_Einsatzschwerpunkte;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox gbx_Einsatzschwerpunkte_Einsatzschwerpunkt;
		private System.Windows.Forms.Label lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung;
		private System.Windows.Forms.Label lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Lage;
		private System.Windows.Forms.Label lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter;
		public System.Windows.Forms.ComboBox cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter;
		private System.Windows.Forms.ComboBox cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet;
		private System.Windows.Forms.Label lbl_Einsatzschwerpunkte_Einsatzschwerpunkte_Prioritaet;
		private System.Windows.Forms.Button btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen;
		private System.Windows.Forms.Button btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern;
		private System.Windows.Forms.Button btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen;
		public System.Windows.Forms.TreeView trv_Einsatzmanager;
		private System.ComponentModel.IContainer components;
		private TreeNode _trn_QuellKnoten = null;
		private System.Windows.Forms.TabPage tabpage_Kraefte;
		private System.Windows.Forms.Label lbl_Einsatzschwerpunkte_BezeichnungAuswahl;
		public System.Windows.Forms.ComboBox cmb_Einsatzschwerpunkte_BezeichnungAuswahl;
		private System.Windows.Forms.TextBox txt_Einsatzschwerpunkte_Lage;
		private System.Windows.Forms.Button btn_Einsatzschwerpunkte_Verwerfen;
		private System.Windows.Forms.Button btn_Einsatzschwerpunkte_Speichern;
		private System.Windows.Forms.GroupBox gbx_Einsatzschwerpunkte_Auswahl;
		private System.Windows.Forms.TextBox txt_Einsatzschwerpuntke_Bezeichnung;
		private System.Windows.Forms.Label lblTemp;
		private System.Windows.Forms.DataGrid dgrid_Einsatzschwerpunkte_Erkundungsergebnisse;
		private System.Windows.Forms.Label lbl_Einsatzschwerpunkte_Erkundungsergebnisse;
		private System.Windows.Forms.OpenFileDialog ofd_Kraefte_Importieren;
		private System.Windows.Forms.TabControl tabctrl_Kraefte;
		private System.Windows.Forms.TabPage tabpage_Einheit;
		private System.Windows.Forms.TabPage tabpage_Helfer;
		private System.Windows.Forms.TabPage tabpage_Kfz;
		private System.Windows.Forms.ImageList iml_TreeViewBilderListe;
		private System.Windows.Forms.Label lbl_Kraefte_Helfer_NameVorname;
		private System.Windows.Forms.TextBox txt_Kraefte_Helfer_Name;
		private System.Windows.Forms.TextBox txt_Kraefte_Helfer_Vorname;
		private System.Windows.Forms.Label lbl_Kraefte_Helfer_GebDatum;
		private System.Windows.Forms.DateTimePicker dtp_Kraefte_Helfer_Geburtsdatum;
		private System.Windows.Forms.Label lbl_Kraefte_Helfer_Anschrift;
		private System.Windows.Forms.TextBox txt_Kraefte_Helfer_Anschrift;
		private System.Windows.Forms.Label lbl_Kraefte_Helfer_PosStat;
		private System.Windows.Forms.ComboBox cmb_Kraefte_Helfer_Position;
		private System.Windows.Forms.ComboBox cmb_Kraefte_Helfer_Status;
		private System.Windows.Forms.Button btn_Kraefte_Helfer_Importieren;
		private System.Windows.Forms.Label lbl_Kraefte_Helfer_OV;
		private System.Windows.Forms.Label lbl_Kraefte_Helfer_Person;
		private System.Windows.Forms.TextBox txt_Kraefte_Helfer_OV;
		private System.Windows.Forms.DataGrid dgrid_Kraefte_Helfer_Person;
		private System.Windows.Forms.Label lbl_Kraefte_Helfet_Arbeitszeiten;
		private System.Windows.Forms.DataGrid dgrid_Kraefte_Helfer_Arbeitszeiten;
		private System.Windows.Forms.TextBox txt_Kraefte_Helfer_Erreichbarkeit;
		private System.Windows.Forms.TextBox txt_Kraefte_Helfer_Zusatz;
		private System.Windows.Forms.Label lbl_Kraefte_Helfer_Zusatz;
		private System.Windows.Forms.Label lbl_Kraefte_Helfer_Erreichbarkeit;
		private System.Windows.Forms.GroupBox gbx_Kraefte_Helfer_DatenSpeichern;
		private System.Windows.Forms.Button btn_Kraefte_Helfer_DatenSpeichern_Verwerfen;
		private System.Windows.Forms.Button btn_Kraefte_Helfer_DatenSpeichern_Speichern;
		private System.Windows.Forms.TextBox txt_Kraefte_Kfz_Funkrufname;
		private System.Windows.Forms.Label lbl_Kraefte_Kfz_Funkrufname;
		private System.Windows.Forms.TextBox txt_Kraefte_Kfz_KfzTyp;
		private System.Windows.Forms.Label lbl_Kraefte_Kfz_KfzTyp;
		private System.Windows.Forms.Label lbl_Kraefte_Kfz_Fahrer;
		private System.Windows.Forms.TextBox txt_Kraefte_Kfz_Kennzeichen;
		private System.Windows.Forms.Label lbl_Kraefte_Kfz_Kennzeichen;
		private System.Windows.Forms.DataGrid dgrid_Kraefte_Kfz_Fahrer;
		private System.Windows.Forms.DataGrid dataGrid2;
		private System.Windows.Forms.Label lbl_Kraefte_Kfz_Betrieb;
		private System.Windows.Forms.Label lbl_Kraefte_Kfz_Kommentar;
		private System.Windows.Forms.TextBox txt_Kraefte_Kfz_Kommentar;
		private System.Windows.Forms.GroupBox gbx_Kraefte_Kfz_DatenSpeichern;
		private System.Windows.Forms.Button btn__Kraefte_Kfz_DatenSpeichern_Speichern;
		private System.Windows.Forms.Button btn_Kraefte_Kfz_DatenSpeichern_Verwerfen;
		private System.Windows.Forms.Button btn_Kraefte_Kfz_Importieren;
		private System.Windows.Forms.Label lbl_Kraefte_Einheit_Funk;
		private System.Windows.Forms.TextBox txt_Kraefte_Einheit_Funkrufname;
		private System.Windows.Forms.Label lbl_Kraefte_Einheit_SollStaerke;
		private System.Windows.Forms.TextBox txt_Kraefte_Einheit_SollStaerke;
		private System.Windows.Forms.Label lbl_Kraefte_Einheit_IstStaerke;
		private System.Windows.Forms.TextBox txt_Kraefte_Einheit_IstStaerke;
		private System.Windows.Forms.Label lbl_Kraefte_Einheit_Name;
		private System.Windows.Forms.TextBox txt_Kraefte_Einheit_Name;
		private System.Windows.Forms.Label lbl_Kraefte_Einheit_gst;
		private System.Windows.Forms.Label lbl_Kraefte_Einheit_OV;
		private System.Windows.Forms.TextBox txt_Kraefte_Einheit_gst;
		private System.Windows.Forms.TextBox txt_Kraefte_Einheit_OV;
		private System.Windows.Forms.Label lbl_Kraefte_Einheit_Erreichbarkeit;
		private System.Windows.Forms.TextBox txt_Kraefte_Einheit_Erreichbarkeit;
		private System.Windows.Forms.Label lbl_Kraefte_Einheit_Personen;
		public System.Windows.Forms.DataGrid dgrid_Kraefte_Einheit_Personen;
		private System.Windows.Forms.DataGrid dgrid_Kraefte_Einheit_Material;
		private System.Windows.Forms.Label lbl_Kraefte_Einheit_Material;
		private System.Windows.Forms.DataGrid dgrid_Kraefte_Einheit_Verbrauchsgueter;
		private System.Windows.Forms.Label lbl_Kraefte_Einheit_Verbrauchsgueter;
		private System.Windows.Forms.GroupBox gbx_Kraefte_Einheit_DatenSpeichern;
		private System.Windows.Forms.Button btn_Kraefte_Einheit_DatenSpeichern_Speichern;
		private System.Windows.Forms.Button btn_Kraefte_Einheit_DatenSpeichern_Verwerfen;
		private System.Windows.Forms.Button btn_Kraefte_Einheit_Importieren;
		private System.Windows.Forms.TabPage tabctrl_Module;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		public System.Windows.Forms.ContextMenu ctx_abstrakt_Einsatzschwerpunkt;
		private System.Windows.Forms.MenuItem mi_Ueberschrift_Einsatzschwerpunkte;
		private System.Windows.Forms.MenuItem menuItem2;
		public System.Windows.Forms.ContextMenu ctx_Einsatzschwerpunkt;
		private System.Windows.Forms.MenuItem mi_Ueberschrift_Einsatzschwerpunkt;
		private System.Windows.Forms.MenuItem menuItem3;
		public System.Windows.Forms.ContextMenu ctx_abstrakt_Helfer;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem4;
		public System.Windows.Forms.ContextMenu ctx_Helfer;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		public System.Windows.Forms.ContextMenu ctx_abstrakt_Material;
		public System.Windows.Forms.ContextMenu ctx_Material;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		public System.Windows.Forms.ContextMenu ctx_abstrakt_Fahrzeuge;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		public System.Windows.Forms.ContextMenu ctx_Fahrzeuge;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
		public System.Windows.Forms.ContextMenu ctx_abstrakt_Module;
		public System.Windows.Forms.ContextMenu ctx_Module;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		public System.Windows.Forms.ContextMenu ctx_abstrakt_Einheiten;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItem20;
		public System.Windows.Forms.ContextMenu ctx_abstrakt_Einsatzschwerpunkte;
		private System.Windows.Forms.MenuItem mi_ueberschrift_abstrakt_einsatzschwerpunkte;
		public System.Windows.Forms.ContextMenu ctx_Einsatzschwerpunkte;
		private System.Windows.Forms.MenuItem mi_ueberschrift_Einsatzschwerpunkte;
		private System.Windows.Forms.MenuItem mi_ueberschrift_abstrakt_Helfer;
		private System.Windows.Forms.MenuItem mi_ueberschrift_Helfer;
		private System.Windows.Forms.MenuItem mi_ueberschrift_Material;
		private System.Windows.Forms.MenuItem mi_ueberschrift_abstrakt_Fahrzeuge;
		private System.Windows.Forms.MenuItem mi_ueberschrift_Fahrzeuge;
		private System.Windows.Forms.MenuItem mi_ueberschrift_abstrakt_Module;
		private System.Windows.Forms.MenuItem mi_ueberschrift_Module;
		private System.Windows.Forms.MenuItem mi_ueberschrift_abstrakt_Einheiten;
		public System.Windows.Forms.ContextMenu ctx_Einheiten;
		private System.Windows.Forms.MenuItem mi_ueberschrift_Einheit;
		private System.Windows.Forms.MenuItem mi_ueberschrift_abstrakt_Material;
		public System.Windows.Forms.Button btn_Aktualisieren;
		private TreeNode _trn_ZielKnoten = null;
		#endregion

		private System.Windows.Forms.MenuItem menuItem23;
		private System.Windows.Forms.MenuItem menuItem24;
		private System.Windows.Forms.MenuItem menuItem25;
		private System.Windows.Forms.MenuItem menuItem26;
		private System.Windows.Forms.MenuItem menuItem27;
		private System.Windows.Forms.MenuItem menuItem28;
		private System.Windows.Forms.MenuItem menuItem29;
		private System.Windows.Forms.MenuItem menuItem30;
		private System.Windows.Forms.MenuItem menuItem31;
		private System.Windows.Forms.MenuItem menuItem32;
		private System.Windows.Forms.MenuItem menuItem33;
		private System.Windows.Forms.MenuItem menuItem34;
		private System.Windows.Forms.MenuItem menuItem35;
		private System.Windows.Forms.MenuItem menuItem36;
		private System.Windows.Forms.MenuItem menuItem37;
		private System.Windows.Forms.MenuItem menuItem41;
		private System.Windows.Forms.MenuItem menuItem44;
		private System.Windows.Forms.MenuItem mI_abstrakt_ESP_NeuenESPanlegen;
		private System.Windows.Forms.MenuItem mI_abstrakt_Modul_NeuesModulAnlegen;
		private System.Windows.Forms.MenuItem mI_abstrakt_Modul_WurdeVerpflegt;
		private System.Windows.Forms.MenuItem mI_abstrakt_Modul_KraeftestatusSetzen;
		private System.Windows.Forms.MenuItem mI_abstrakt_Helfer_HelferAnlegen;
		private System.Windows.Forms.MenuItem mI_abstrakt_Helfer_WurdeVerpflegt;
		private System.Windows.Forms.MenuItem mI_abstrakt_Helfer_StatusSetzenAuf;
		private System.Windows.Forms.MenuItem mI_abstrakt_Einheiten_NeueEinheitAnlegen;
		private System.Windows.Forms.MenuItem mI_abstrakt_Fahrzeuge_NeuesKFZAnlegen;
		private System.Windows.Forms.Timer tmr_ESPTimer;
		private System.Windows.Forms.ErrorProvider ep_Eingabe;
		private bool bEditierungsModus;
		private System.Windows.Forms.TabPage tabpage_Kraefte_Helfer;
		private System.Windows.Forms.TabPage tabpage_Kraefte_Einheit;
		private System.Windows.Forms.TabPage tabpage_Kraefte_Kfz;
		private System.Windows.Forms.TabPage tabpage_Kraefte_Module;

		#region Konstruktoren & Destruktoren

		private Cst_EK _Cst_EK;


		public Cpr_usc_EK(Cst_EK myST)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			_Cst_EK=myST;
			this.bEditierungsModus = false;
			// TODO: Add any initialization after the InitComponent call

		}

		/// <summary>
		/// Clean up any resources being used.
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

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Cpr_usc_EK));
			this.tabctrl_EK = new System.Windows.Forms.TabControl();
			this.tabpage_Kraefte = new System.Windows.Forms.TabPage();
			this.tabctrl_Kraefte = new System.Windows.Forms.TabControl();
			this.tabpage_Helfer = new System.Windows.Forms.TabPage();
			this.gbx_Kraefte_Helfer_DatenSpeichern = new System.Windows.Forms.GroupBox();
			this.btn_Kraefte_Helfer_DatenSpeichern_Speichern = new System.Windows.Forms.Button();
			this.btn_Kraefte_Helfer_DatenSpeichern_Verwerfen = new System.Windows.Forms.Button();
			this.txt_Kraefte_Helfer_Erreichbarkeit = new System.Windows.Forms.TextBox();
			this.txt_Kraefte_Helfer_Zusatz = new System.Windows.Forms.TextBox();
			this.lbl_Kraefte_Helfer_Zusatz = new System.Windows.Forms.Label();
			this.lbl_Kraefte_Helfer_Erreichbarkeit = new System.Windows.Forms.Label();
			this.dgrid_Kraefte_Helfer_Arbeitszeiten = new System.Windows.Forms.DataGrid();
			this.lbl_Kraefte_Helfet_Arbeitszeiten = new System.Windows.Forms.Label();
			this.txt_Kraefte_Helfer_OV = new System.Windows.Forms.TextBox();
			this.dgrid_Kraefte_Helfer_Person = new System.Windows.Forms.DataGrid();
			this.lbl_Kraefte_Helfer_Person = new System.Windows.Forms.Label();
			this.lbl_Kraefte_Helfer_OV = new System.Windows.Forms.Label();
			this.cmb_Kraefte_Helfer_Status = new System.Windows.Forms.ComboBox();
			this.cmb_Kraefte_Helfer_Position = new System.Windows.Forms.ComboBox();
			this.lbl_Kraefte_Helfer_PosStat = new System.Windows.Forms.Label();
			this.txt_Kraefte_Helfer_Anschrift = new System.Windows.Forms.TextBox();
			this.lbl_Kraefte_Helfer_Anschrift = new System.Windows.Forms.Label();
			this.dtp_Kraefte_Helfer_Geburtsdatum = new System.Windows.Forms.DateTimePicker();
			this.lbl_Kraefte_Helfer_GebDatum = new System.Windows.Forms.Label();
			this.txt_Kraefte_Helfer_Vorname = new System.Windows.Forms.TextBox();
			this.txt_Kraefte_Helfer_Name = new System.Windows.Forms.TextBox();
			this.lbl_Kraefte_Helfer_NameVorname = new System.Windows.Forms.Label();
			this.btn_Kraefte_Helfer_Importieren = new System.Windows.Forms.Button();
			this.tabpage_Einheit = new System.Windows.Forms.TabPage();
			this.gbx_Kraefte_Einheit_DatenSpeichern = new System.Windows.Forms.GroupBox();
			this.btn_Kraefte_Einheit_DatenSpeichern_Speichern = new System.Windows.Forms.Button();
			this.btn_Kraefte_Einheit_DatenSpeichern_Verwerfen = new System.Windows.Forms.Button();
			this.btn_Kraefte_Einheit_Importieren = new System.Windows.Forms.Button();
			this.dgrid_Kraefte_Einheit_Verbrauchsgueter = new System.Windows.Forms.DataGrid();
			this.lbl_Kraefte_Einheit_Verbrauchsgueter = new System.Windows.Forms.Label();
			this.dgrid_Kraefte_Einheit_Material = new System.Windows.Forms.DataGrid();
			this.lbl_Kraefte_Einheit_Material = new System.Windows.Forms.Label();
			this.dgrid_Kraefte_Einheit_Personen = new System.Windows.Forms.DataGrid();
			this.lbl_Kraefte_Einheit_Personen = new System.Windows.Forms.Label();
			this.txt_Kraefte_Einheit_Erreichbarkeit = new System.Windows.Forms.TextBox();
			this.lbl_Kraefte_Einheit_Erreichbarkeit = new System.Windows.Forms.Label();
			this.txt_Kraefte_Einheit_OV = new System.Windows.Forms.TextBox();
			this.txt_Kraefte_Einheit_gst = new System.Windows.Forms.TextBox();
			this.lbl_Kraefte_Einheit_OV = new System.Windows.Forms.Label();
			this.lbl_Kraefte_Einheit_gst = new System.Windows.Forms.Label();
			this.txt_Kraefte_Einheit_Name = new System.Windows.Forms.TextBox();
			this.lbl_Kraefte_Einheit_Name = new System.Windows.Forms.Label();
			this.txt_Kraefte_Einheit_IstStaerke = new System.Windows.Forms.TextBox();
			this.lbl_Kraefte_Einheit_IstStaerke = new System.Windows.Forms.Label();
			this.txt_Kraefte_Einheit_SollStaerke = new System.Windows.Forms.TextBox();
			this.lbl_Kraefte_Einheit_SollStaerke = new System.Windows.Forms.Label();
			this.txt_Kraefte_Einheit_Funkrufname = new System.Windows.Forms.TextBox();
			this.lbl_Kraefte_Einheit_Funk = new System.Windows.Forms.Label();
			this.tabpage_Kfz = new System.Windows.Forms.TabPage();
			this.gbx_Kraefte_Kfz_DatenSpeichern = new System.Windows.Forms.GroupBox();
			this.btn__Kraefte_Kfz_DatenSpeichern_Speichern = new System.Windows.Forms.Button();
			this.btn_Kraefte_Kfz_DatenSpeichern_Verwerfen = new System.Windows.Forms.Button();
			this.btn_Kraefte_Kfz_Importieren = new System.Windows.Forms.Button();
			this.txt_Kraefte_Kfz_Kommentar = new System.Windows.Forms.TextBox();
			this.lbl_Kraefte_Kfz_Kommentar = new System.Windows.Forms.Label();
			this.dataGrid2 = new System.Windows.Forms.DataGrid();
			this.lbl_Kraefte_Kfz_Betrieb = new System.Windows.Forms.Label();
			this.dgrid_Kraefte_Kfz_Fahrer = new System.Windows.Forms.DataGrid();
			this.txt_Kraefte_Kfz_Funkrufname = new System.Windows.Forms.TextBox();
			this.lbl_Kraefte_Kfz_Funkrufname = new System.Windows.Forms.Label();
			this.txt_Kraefte_Kfz_KfzTyp = new System.Windows.Forms.TextBox();
			this.lbl_Kraefte_Kfz_KfzTyp = new System.Windows.Forms.Label();
			this.lbl_Kraefte_Kfz_Fahrer = new System.Windows.Forms.Label();
			this.txt_Kraefte_Kfz_Kennzeichen = new System.Windows.Forms.TextBox();
			this.lbl_Kraefte_Kfz_Kennzeichen = new System.Windows.Forms.Label();
			this.tabctrl_Module = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.label4 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tabpage_Einsatzschwerpunkte = new System.Windows.Forms.TabPage();
			this.gbx_Einsatzschwerpunkte_Auswahl = new System.Windows.Forms.GroupBox();
			this.lbl_Einsatzschwerpunkte_BezeichnungAuswahl = new System.Windows.Forms.Label();
			this.cmb_Einsatzschwerpunkte_BezeichnungAuswahl = new System.Windows.Forms.ComboBox();
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt = new System.Windows.Forms.GroupBox();
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen = new System.Windows.Forms.Button();
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern = new System.Windows.Forms.Button();
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen = new System.Windows.Forms.Button();
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet = new System.Windows.Forms.ComboBox();
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkte_Prioritaet = new System.Windows.Forms.Label();
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter = new System.Windows.Forms.ComboBox();
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter = new System.Windows.Forms.Label();
			this.txt_Einsatzschwerpunkte_Lage = new System.Windows.Forms.TextBox();
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Lage = new System.Windows.Forms.Label();
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung = new System.Windows.Forms.Label();
			this.txt_Einsatzschwerpuntke_Bezeichnung = new System.Windows.Forms.TextBox();
			this.btn_Einsatzschwerpunkte_Verwerfen = new System.Windows.Forms.Button();
			this.btn_Einsatzschwerpunkte_Speichern = new System.Windows.Forms.Button();
			this.lblTemp = new System.Windows.Forms.Label();
			this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse = new System.Windows.Forms.DataGrid();
			this.lbl_Einsatzschwerpunkte_Erkundungsergebnisse = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.trv_Einsatzmanager = new System.Windows.Forms.TreeView();
			this.iml_TreeViewBilderListe = new System.Windows.Forms.ImageList(this.components);
			this.ofd_Kraefte_Importieren = new System.Windows.Forms.OpenFileDialog();
			this.ctx_abstrakt_Einsatzschwerpunkte = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_abstrakt_einsatzschwerpunkte = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_ESP_NeuenESPanlegen = new System.Windows.Forms.MenuItem();
			this.ctx_Einsatzschwerpunkte = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_Einsatzschwerpunkte = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.ctx_abstrakt_Helfer = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_abstrakt_Helfer = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Helfer_HelferAnlegen = new System.Windows.Forms.MenuItem();
			this.menuItem23 = new System.Windows.Forms.MenuItem();
			this.menuItem24 = new System.Windows.Forms.MenuItem();
			this.menuItem25 = new System.Windows.Forms.MenuItem();
			this.menuItem26 = new System.Windows.Forms.MenuItem();
			this.menuItem27 = new System.Windows.Forms.MenuItem();
			this.menuItem28 = new System.Windows.Forms.MenuItem();
			this.menuItem29 = new System.Windows.Forms.MenuItem();
			this.menuItem30 = new System.Windows.Forms.MenuItem();
			this.menuItem31 = new System.Windows.Forms.MenuItem();
			this.menuItem32 = new System.Windows.Forms.MenuItem();
			this.menuItem33 = new System.Windows.Forms.MenuItem();
			this.menuItem34 = new System.Windows.Forms.MenuItem();
			this.menuItem35 = new System.Windows.Forms.MenuItem();
			this.menuItem36 = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Helfer_WurdeVerpflegt = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Helfer_StatusSetzenAuf = new System.Windows.Forms.MenuItem();
			this.menuItem44 = new System.Windows.Forms.MenuItem();
			this.ctx_Helfer = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_Helfer = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.ctx_abstrakt_Material = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_abstrakt_Material = new System.Windows.Forms.MenuItem();
			this.menuItem37 = new System.Windows.Forms.MenuItem();
			this.ctx_Material = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_Material = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.ctx_abstrakt_Fahrzeuge = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_abstrakt_Fahrzeuge = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Fahrzeuge_NeuesKFZAnlegen = new System.Windows.Forms.MenuItem();
			this.ctx_Fahrzeuge = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_Fahrzeuge = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.ctx_abstrakt_Module = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_abstrakt_Module = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Modul_NeuesModulAnlegen = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Modul_WurdeVerpflegt = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Modul_KraeftestatusSetzen = new System.Windows.Forms.MenuItem();
			this.menuItem41 = new System.Windows.Forms.MenuItem();
			this.ctx_Module = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_Module = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.ctx_abstrakt_Einheiten = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_abstrakt_Einheiten = new System.Windows.Forms.MenuItem();
			this.menuItem18 = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Einheiten_NeueEinheitAnlegen = new System.Windows.Forms.MenuItem();
			this.ctx_Einheiten = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_Einheit = new System.Windows.Forms.MenuItem();
			this.menuItem20 = new System.Windows.Forms.MenuItem();
			this.btn_Aktualisieren = new System.Windows.Forms.Button();
			this.tmr_ESPTimer = new System.Windows.Forms.Timer(this.components);
			this.ep_Eingabe = new System.Windows.Forms.ErrorProvider();
			this.tabpage_Kraefte_Helfer = new System.Windows.Forms.TabPage();
			this.tabpage_Kraefte_Einheit = new System.Windows.Forms.TabPage();
			this.tabpage_Kraefte_Kfz = new System.Windows.Forms.TabPage();
			this.tabpage_Kraefte_Module = new System.Windows.Forms.TabPage();
			this.tabctrl_EK.SuspendLayout();
			this.tabpage_Kraefte.SuspendLayout();
			this.tabctrl_Kraefte.SuspendLayout();
			this.tabpage_Helfer.SuspendLayout();
			this.gbx_Kraefte_Helfer_DatenSpeichern.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Kraefte_Helfer_Arbeitszeiten)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Kraefte_Helfer_Person)).BeginInit();
			this.tabpage_Einheit.SuspendLayout();
			this.gbx_Kraefte_Einheit_DatenSpeichern.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Kraefte_Einheit_Verbrauchsgueter)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Kraefte_Einheit_Material)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Kraefte_Einheit_Personen)).BeginInit();
			this.tabpage_Kfz.SuspendLayout();
			this.gbx_Kraefte_Kfz_DatenSpeichern.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Kraefte_Kfz_Fahrer)).BeginInit();
			this.tabctrl_Module.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.tabpage_Einsatzschwerpunkte.SuspendLayout();
			this.gbx_Einsatzschwerpunkte_Auswahl.SuspendLayout();
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse)).BeginInit();
			this.SuspendLayout();
			// 
			// tabctrl_EK
			// 
			this.tabctrl_EK.Controls.Add(this.tabpage_Kraefte);
			this.tabctrl_EK.Controls.Add(this.tabpage_Einsatzschwerpunkte);
			this.tabctrl_EK.Controls.Add(this.tabpage_Kraefte_Helfer);
			this.tabctrl_EK.Controls.Add(this.tabpage_Kraefte_Einheit);
			this.tabctrl_EK.Controls.Add(this.tabpage_Kraefte_Kfz);
			this.tabctrl_EK.Controls.Add(this.tabpage_Kraefte_Module);
			this.tabctrl_EK.Location = new System.Drawing.Point(5, 5);
			this.tabctrl_EK.Multiline = true;
			this.tabctrl_EK.Name = "tabctrl_EK";
			this.tabctrl_EK.SelectedIndex = 0;
			this.tabctrl_EK.Size = new System.Drawing.Size(421, 525);
			this.tabctrl_EK.TabIndex = 5;
			// 
			// tabpage_Kraefte
			// 
			this.tabpage_Kraefte.Controls.Add(this.tabctrl_Kraefte);
			this.tabpage_Kraefte.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Kraefte.Name = "tabpage_Kraefte";
			this.tabpage_Kraefte.Size = new System.Drawing.Size(413, 499);
			this.tabpage_Kraefte.TabIndex = 2;
			this.tabpage_Kraefte.Text = "Kräfte";
			// 
			// tabctrl_Kraefte
			// 
			this.tabctrl_Kraefte.Controls.Add(this.tabpage_Helfer);
			this.tabctrl_Kraefte.Controls.Add(this.tabpage_Einheit);
			this.tabctrl_Kraefte.Controls.Add(this.tabpage_Kfz);
			this.tabctrl_Kraefte.Controls.Add(this.tabctrl_Module);
			this.tabctrl_Kraefte.Location = new System.Drawing.Point(10, 8);
			this.tabctrl_Kraefte.Name = "tabctrl_Kraefte";
			this.tabctrl_Kraefte.SelectedIndex = 0;
			this.tabctrl_Kraefte.Size = new System.Drawing.Size(400, 480);
			this.tabctrl_Kraefte.TabIndex = 2;
			// 
			// tabpage_Helfer
			// 
			this.tabpage_Helfer.Controls.Add(this.gbx_Kraefte_Helfer_DatenSpeichern);
			this.tabpage_Helfer.Controls.Add(this.txt_Kraefte_Helfer_Erreichbarkeit);
			this.tabpage_Helfer.Controls.Add(this.txt_Kraefte_Helfer_Zusatz);
			this.tabpage_Helfer.Controls.Add(this.lbl_Kraefte_Helfer_Zusatz);
			this.tabpage_Helfer.Controls.Add(this.lbl_Kraefte_Helfer_Erreichbarkeit);
			this.tabpage_Helfer.Controls.Add(this.dgrid_Kraefte_Helfer_Arbeitszeiten);
			this.tabpage_Helfer.Controls.Add(this.lbl_Kraefte_Helfet_Arbeitszeiten);
			this.tabpage_Helfer.Controls.Add(this.txt_Kraefte_Helfer_OV);
			this.tabpage_Helfer.Controls.Add(this.dgrid_Kraefte_Helfer_Person);
			this.tabpage_Helfer.Controls.Add(this.lbl_Kraefte_Helfer_Person);
			this.tabpage_Helfer.Controls.Add(this.lbl_Kraefte_Helfer_OV);
			this.tabpage_Helfer.Controls.Add(this.cmb_Kraefte_Helfer_Status);
			this.tabpage_Helfer.Controls.Add(this.cmb_Kraefte_Helfer_Position);
			this.tabpage_Helfer.Controls.Add(this.lbl_Kraefte_Helfer_PosStat);
			this.tabpage_Helfer.Controls.Add(this.txt_Kraefte_Helfer_Anschrift);
			this.tabpage_Helfer.Controls.Add(this.lbl_Kraefte_Helfer_Anschrift);
			this.tabpage_Helfer.Controls.Add(this.dtp_Kraefte_Helfer_Geburtsdatum);
			this.tabpage_Helfer.Controls.Add(this.lbl_Kraefte_Helfer_GebDatum);
			this.tabpage_Helfer.Controls.Add(this.txt_Kraefte_Helfer_Vorname);
			this.tabpage_Helfer.Controls.Add(this.txt_Kraefte_Helfer_Name);
			this.tabpage_Helfer.Controls.Add(this.lbl_Kraefte_Helfer_NameVorname);
			this.tabpage_Helfer.Controls.Add(this.btn_Kraefte_Helfer_Importieren);
			this.tabpage_Helfer.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Helfer.Name = "tabpage_Helfer";
			this.tabpage_Helfer.Size = new System.Drawing.Size(392, 454);
			this.tabpage_Helfer.TabIndex = 1;
			this.tabpage_Helfer.Text = "Helfer";
			// 
			// gbx_Kraefte_Helfer_DatenSpeichern
			// 
			this.gbx_Kraefte_Helfer_DatenSpeichern.Controls.Add(this.btn_Kraefte_Helfer_DatenSpeichern_Speichern);
			this.gbx_Kraefte_Helfer_DatenSpeichern.Controls.Add(this.btn_Kraefte_Helfer_DatenSpeichern_Verwerfen);
			this.gbx_Kraefte_Helfer_DatenSpeichern.Location = new System.Drawing.Point(190, 380);
			this.gbx_Kraefte_Helfer_DatenSpeichern.Name = "gbx_Kraefte_Helfer_DatenSpeichern";
			this.gbx_Kraefte_Helfer_DatenSpeichern.Size = new System.Drawing.Size(200, 56);
			this.gbx_Kraefte_Helfer_DatenSpeichern.TabIndex = 25;
			this.gbx_Kraefte_Helfer_DatenSpeichern.TabStop = false;
			this.gbx_Kraefte_Helfer_DatenSpeichern.Text = "Daten Speichern";
			// 
			// btn_Kraefte_Helfer_DatenSpeichern_Speichern
			// 
			this.btn_Kraefte_Helfer_DatenSpeichern_Speichern.Location = new System.Drawing.Point(112, 20);
			this.btn_Kraefte_Helfer_DatenSpeichern_Speichern.Name = "btn_Kraefte_Helfer_DatenSpeichern_Speichern";
			this.btn_Kraefte_Helfer_DatenSpeichern_Speichern.TabIndex = 27;
			this.btn_Kraefte_Helfer_DatenSpeichern_Speichern.Text = "Speichern";
			this.btn_Kraefte_Helfer_DatenSpeichern_Speichern.Click += new System.EventHandler(this.btn_Kraefte_Helfer_DatenSpeichern_Speichern_Click);
			// 
			// btn_Kraefte_Helfer_DatenSpeichern_Verwerfen
			// 
			this.btn_Kraefte_Helfer_DatenSpeichern_Verwerfen.Location = new System.Drawing.Point(15, 20);
			this.btn_Kraefte_Helfer_DatenSpeichern_Verwerfen.Name = "btn_Kraefte_Helfer_DatenSpeichern_Verwerfen";
			this.btn_Kraefte_Helfer_DatenSpeichern_Verwerfen.TabIndex = 26;
			this.btn_Kraefte_Helfer_DatenSpeichern_Verwerfen.Text = "Verwerfen";
			this.btn_Kraefte_Helfer_DatenSpeichern_Verwerfen.Click += new System.EventHandler(this.btn_Kraefte_Helfer_DatenSpeichern_Verwerfen_Click);
			// 
			// txt_Kraefte_Helfer_Erreichbarkeit
			// 
			this.txt_Kraefte_Helfer_Erreichbarkeit.Location = new System.Drawing.Point(95, 165);
			this.txt_Kraefte_Helfer_Erreichbarkeit.Multiline = true;
			this.txt_Kraefte_Helfer_Erreichbarkeit.Name = "txt_Kraefte_Helfer_Erreichbarkeit";
			this.txt_Kraefte_Helfer_Erreichbarkeit.Size = new System.Drawing.Size(276, 64);
			this.txt_Kraefte_Helfer_Erreichbarkeit.TabIndex = 24;
			this.txt_Kraefte_Helfer_Erreichbarkeit.Text = "";
			this.txt_Kraefte_Helfer_Erreichbarkeit.Leave += new System.EventHandler(this.txt_Kraefte_Helfer_Erreichbarkeit_Leave);
			// 
			// txt_Kraefte_Helfer_Zusatz
			// 
			this.txt_Kraefte_Helfer_Zusatz.Location = new System.Drawing.Point(105, 230);
			this.txt_Kraefte_Helfer_Zusatz.Multiline = true;
			this.txt_Kraefte_Helfer_Zusatz.Name = "txt_Kraefte_Helfer_Zusatz";
			this.txt_Kraefte_Helfer_Zusatz.Size = new System.Drawing.Size(276, 64);
			this.txt_Kraefte_Helfer_Zusatz.TabIndex = 23;
			this.txt_Kraefte_Helfer_Zusatz.Text = "";
			// 
			// lbl_Kraefte_Helfer_Zusatz
			// 
			this.lbl_Kraefte_Helfer_Zusatz.Location = new System.Drawing.Point(15, 220);
			this.lbl_Kraefte_Helfer_Zusatz.Name = "lbl_Kraefte_Helfer_Zusatz";
			this.lbl_Kraefte_Helfer_Zusatz.Size = new System.Drawing.Size(72, 23);
			this.lbl_Kraefte_Helfer_Zusatz.TabIndex = 22;
			this.lbl_Kraefte_Helfer_Zusatz.Text = "Zusatz Info.";
			// 
			// lbl_Kraefte_Helfer_Erreichbarkeit
			// 
			this.lbl_Kraefte_Helfer_Erreichbarkeit.Location = new System.Drawing.Point(5, 165);
			this.lbl_Kraefte_Helfer_Erreichbarkeit.Name = "lbl_Kraefte_Helfer_Erreichbarkeit";
			this.lbl_Kraefte_Helfer_Erreichbarkeit.Size = new System.Drawing.Size(88, 23);
			this.lbl_Kraefte_Helfer_Erreichbarkeit.TabIndex = 21;
			this.lbl_Kraefte_Helfer_Erreichbarkeit.Text = "Erreichbarkeit";
			// 
			// dgrid_Kraefte_Helfer_Arbeitszeiten
			// 
			this.dgrid_Kraefte_Helfer_Arbeitszeiten.DataMember = "";
			this.dgrid_Kraefte_Helfer_Arbeitszeiten.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgrid_Kraefte_Helfer_Arbeitszeiten.Location = new System.Drawing.Point(208, 304);
			this.dgrid_Kraefte_Helfer_Arbeitszeiten.Name = "dgrid_Kraefte_Helfer_Arbeitszeiten";
			this.dgrid_Kraefte_Helfer_Arbeitszeiten.Size = new System.Drawing.Size(176, 72);
			this.dgrid_Kraefte_Helfer_Arbeitszeiten.TabIndex = 19;
			this.dgrid_Kraefte_Helfer_Arbeitszeiten.Visible = false;
			// 
			// lbl_Kraefte_Helfet_Arbeitszeiten
			// 
			this.lbl_Kraefte_Helfet_Arbeitszeiten.Location = new System.Drawing.Point(208, 288);
			this.lbl_Kraefte_Helfet_Arbeitszeiten.Name = "lbl_Kraefte_Helfet_Arbeitszeiten";
			this.lbl_Kraefte_Helfet_Arbeitszeiten.Size = new System.Drawing.Size(80, 23);
			this.lbl_Kraefte_Helfet_Arbeitszeiten.TabIndex = 18;
			this.lbl_Kraefte_Helfet_Arbeitszeiten.Text = "Arbeitszeiten";
			this.lbl_Kraefte_Helfet_Arbeitszeiten.Visible = false;
			// 
			// txt_Kraefte_Helfer_OV
			// 
			this.txt_Kraefte_Helfer_OV.Location = new System.Drawing.Point(95, 140);
			this.txt_Kraefte_Helfer_OV.Name = "txt_Kraefte_Helfer_OV";
			this.txt_Kraefte_Helfer_OV.Size = new System.Drawing.Size(144, 20);
			this.txt_Kraefte_Helfer_OV.TabIndex = 13;
			this.txt_Kraefte_Helfer_OV.Text = "";
			this.txt_Kraefte_Helfer_OV.Leave += new System.EventHandler(this.txt_Kraefte_Helfer_OV_Leave);
			// 
			// dgrid_Kraefte_Helfer_Person
			// 
			this.dgrid_Kraefte_Helfer_Person.DataMember = "";
			this.dgrid_Kraefte_Helfer_Person.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgrid_Kraefte_Helfer_Person.Location = new System.Drawing.Point(20, 300);
			this.dgrid_Kraefte_Helfer_Person.Name = "dgrid_Kraefte_Helfer_Person";
			this.dgrid_Kraefte_Helfer_Person.Size = new System.Drawing.Size(369, 72);
			this.dgrid_Kraefte_Helfer_Person.TabIndex = 12;
			// 
			// lbl_Kraefte_Helfer_Person
			// 
			this.lbl_Kraefte_Helfer_Person.Location = new System.Drawing.Point(20, 280);
			this.lbl_Kraefte_Helfer_Person.Name = "lbl_Kraefte_Helfer_Person";
			this.lbl_Kraefte_Helfer_Person.Size = new System.Drawing.Size(64, 23);
			this.lbl_Kraefte_Helfer_Person.TabIndex = 11;
			this.lbl_Kraefte_Helfer_Person.Text = "Person";
			// 
			// lbl_Kraefte_Helfer_OV
			// 
			this.lbl_Kraefte_Helfer_OV.Location = new System.Drawing.Point(5, 140);
			this.lbl_Kraefte_Helfer_OV.Name = "lbl_Kraefte_Helfer_OV";
			this.lbl_Kraefte_Helfer_OV.Size = new System.Drawing.Size(80, 23);
			this.lbl_Kraefte_Helfer_OV.TabIndex = 10;
			this.lbl_Kraefte_Helfer_OV.Text = "Ortsverband";
			// 
			// cmb_Kraefte_Helfer_Status
			// 
			this.cmb_Kraefte_Helfer_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Kraefte_Helfer_Status.Items.AddRange(new object[] {
																		   "Aktiver Helfer",
																		   "Reservehelfer",
																		   "Althelfer",
																		   "Junghelfer"});
			this.cmb_Kraefte_Helfer_Status.Location = new System.Drawing.Point(250, 60);
			this.cmb_Kraefte_Helfer_Status.Name = "cmb_Kraefte_Helfer_Status";
			this.cmb_Kraefte_Helfer_Status.Size = new System.Drawing.Size(124, 21);
			this.cmb_Kraefte_Helfer_Status.TabIndex = 9;
			this.cmb_Kraefte_Helfer_Status.Leave += new System.EventHandler(this.cmb_Kraefte_Helfer_Status_Leave);
			// 
			// cmb_Kraefte_Helfer_Position
			// 
			this.cmb_Kraefte_Helfer_Position.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Kraefte_Helfer_Position.Items.AddRange(new object[] {
																			 "Zugführer",
																			 "Einfacher Helfer",
																			 "Führer",
																			 "Unterführer",
																			 "Gruppenführer",
																			 "Truppenhelfer"});
			this.cmb_Kraefte_Helfer_Position.Location = new System.Drawing.Point(95, 60);
			this.cmb_Kraefte_Helfer_Position.Name = "cmb_Kraefte_Helfer_Position";
			this.cmb_Kraefte_Helfer_Position.Size = new System.Drawing.Size(144, 21);
			this.cmb_Kraefte_Helfer_Position.TabIndex = 8;
			this.cmb_Kraefte_Helfer_Position.Leave += new System.EventHandler(this.cmb_Kraefte_Helfer_Position_Leave);
			// 
			// lbl_Kraefte_Helfer_PosStat
			// 
			this.lbl_Kraefte_Helfer_PosStat.Location = new System.Drawing.Point(5, 60);
			this.lbl_Kraefte_Helfer_PosStat.Name = "lbl_Kraefte_Helfer_PosStat";
			this.lbl_Kraefte_Helfer_PosStat.Size = new System.Drawing.Size(88, 23);
			this.lbl_Kraefte_Helfer_PosStat.TabIndex = 7;
			this.lbl_Kraefte_Helfer_PosStat.Text = "Position, Status";
			// 
			// txt_Kraefte_Helfer_Anschrift
			// 
			this.txt_Kraefte_Helfer_Anschrift.Location = new System.Drawing.Point(95, 85);
			this.txt_Kraefte_Helfer_Anschrift.Multiline = true;
			this.txt_Kraefte_Helfer_Anschrift.Name = "txt_Kraefte_Helfer_Anschrift";
			this.txt_Kraefte_Helfer_Anschrift.Size = new System.Drawing.Size(276, 56);
			this.txt_Kraefte_Helfer_Anschrift.TabIndex = 6;
			this.txt_Kraefte_Helfer_Anschrift.Text = "";
			this.txt_Kraefte_Helfer_Anschrift.Leave += new System.EventHandler(this.txt_Kraefte_Helfer_Anschrift_Leave);
			// 
			// lbl_Kraefte_Helfer_Anschrift
			// 
			this.lbl_Kraefte_Helfer_Anschrift.Location = new System.Drawing.Point(5, 85);
			this.lbl_Kraefte_Helfer_Anschrift.Name = "lbl_Kraefte_Helfer_Anschrift";
			this.lbl_Kraefte_Helfer_Anschrift.Size = new System.Drawing.Size(88, 23);
			this.lbl_Kraefte_Helfer_Anschrift.TabIndex = 5;
			this.lbl_Kraefte_Helfer_Anschrift.Text = "Anschrift";
			// 
			// dtp_Kraefte_Helfer_Geburtsdatum
			// 
			this.dtp_Kraefte_Helfer_Geburtsdatum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtp_Kraefte_Helfer_Geburtsdatum.Location = new System.Drawing.Point(95, 35);
			this.dtp_Kraefte_Helfer_Geburtsdatum.Name = "dtp_Kraefte_Helfer_Geburtsdatum";
			this.dtp_Kraefte_Helfer_Geburtsdatum.Size = new System.Drawing.Size(144, 20);
			this.dtp_Kraefte_Helfer_Geburtsdatum.TabIndex = 4;
			this.dtp_Kraefte_Helfer_Geburtsdatum.Leave += new System.EventHandler(this.dtp_Kraefte_Helfer_Geburtsdatum_Leave);
			// 
			// lbl_Kraefte_Helfer_GebDatum
			// 
			this.lbl_Kraefte_Helfer_GebDatum.Location = new System.Drawing.Point(5, 35);
			this.lbl_Kraefte_Helfer_GebDatum.Name = "lbl_Kraefte_Helfer_GebDatum";
			this.lbl_Kraefte_Helfer_GebDatum.TabIndex = 3;
			this.lbl_Kraefte_Helfer_GebDatum.Text = "Geburtsdatum";
			// 
			// txt_Kraefte_Helfer_Vorname
			// 
			this.txt_Kraefte_Helfer_Vorname.Location = new System.Drawing.Point(250, 15);
			this.txt_Kraefte_Helfer_Vorname.Name = "txt_Kraefte_Helfer_Vorname";
			this.txt_Kraefte_Helfer_Vorname.Size = new System.Drawing.Size(124, 20);
			this.txt_Kraefte_Helfer_Vorname.TabIndex = 2;
			this.txt_Kraefte_Helfer_Vorname.Text = "";
			this.txt_Kraefte_Helfer_Vorname.Leave += new System.EventHandler(this.txt_Kraefte_Helfer_Vorname_Leave);
			// 
			// txt_Kraefte_Helfer_Name
			// 
			this.txt_Kraefte_Helfer_Name.Location = new System.Drawing.Point(95, 15);
			this.txt_Kraefte_Helfer_Name.Name = "txt_Kraefte_Helfer_Name";
			this.txt_Kraefte_Helfer_Name.Size = new System.Drawing.Size(144, 20);
			this.txt_Kraefte_Helfer_Name.TabIndex = 1;
			this.txt_Kraefte_Helfer_Name.Text = "";
			this.txt_Kraefte_Helfer_Name.Leave += new System.EventHandler(this.txt_Kraefte_Helfer_Name_Leave);
			// 
			// lbl_Kraefte_Helfer_NameVorname
			// 
			this.lbl_Kraefte_Helfer_NameVorname.Location = new System.Drawing.Point(5, 10);
			this.lbl_Kraefte_Helfer_NameVorname.Name = "lbl_Kraefte_Helfer_NameVorname";
			this.lbl_Kraefte_Helfer_NameVorname.Size = new System.Drawing.Size(88, 23);
			this.lbl_Kraefte_Helfer_NameVorname.TabIndex = 0;
			this.lbl_Kraefte_Helfer_NameVorname.Text = "Name, Vorname";
			this.lbl_Kraefte_Helfer_NameVorname.Click += new System.EventHandler(this.lbl_Kraefte_Helfer_NameVorname_Click);
			// 
			// btn_Kraefte_Helfer_Importieren
			// 
			this.btn_Kraefte_Helfer_Importieren.Location = new System.Drawing.Point(20, 400);
			this.btn_Kraefte_Helfer_Importieren.Name = "btn_Kraefte_Helfer_Importieren";
			this.btn_Kraefte_Helfer_Importieren.TabIndex = 1;
			this.btn_Kraefte_Helfer_Importieren.Text = "Importieren";
			this.btn_Kraefte_Helfer_Importieren.Click += new System.EventHandler(this.btn_Kraefte_Importieren_Click);
			// 
			// tabpage_Einheit
			// 
			this.tabpage_Einheit.Controls.Add(this.gbx_Kraefte_Einheit_DatenSpeichern);
			this.tabpage_Einheit.Controls.Add(this.btn_Kraefte_Einheit_Importieren);
			this.tabpage_Einheit.Controls.Add(this.dgrid_Kraefte_Einheit_Verbrauchsgueter);
			this.tabpage_Einheit.Controls.Add(this.lbl_Kraefte_Einheit_Verbrauchsgueter);
			this.tabpage_Einheit.Controls.Add(this.dgrid_Kraefte_Einheit_Material);
			this.tabpage_Einheit.Controls.Add(this.lbl_Kraefte_Einheit_Material);
			this.tabpage_Einheit.Controls.Add(this.dgrid_Kraefte_Einheit_Personen);
			this.tabpage_Einheit.Controls.Add(this.lbl_Kraefte_Einheit_Personen);
			this.tabpage_Einheit.Controls.Add(this.txt_Kraefte_Einheit_Erreichbarkeit);
			this.tabpage_Einheit.Controls.Add(this.lbl_Kraefte_Einheit_Erreichbarkeit);
			this.tabpage_Einheit.Controls.Add(this.txt_Kraefte_Einheit_OV);
			this.tabpage_Einheit.Controls.Add(this.txt_Kraefte_Einheit_gst);
			this.tabpage_Einheit.Controls.Add(this.lbl_Kraefte_Einheit_OV);
			this.tabpage_Einheit.Controls.Add(this.lbl_Kraefte_Einheit_gst);
			this.tabpage_Einheit.Controls.Add(this.txt_Kraefte_Einheit_Name);
			this.tabpage_Einheit.Controls.Add(this.lbl_Kraefte_Einheit_Name);
			this.tabpage_Einheit.Controls.Add(this.txt_Kraefte_Einheit_IstStaerke);
			this.tabpage_Einheit.Controls.Add(this.lbl_Kraefte_Einheit_IstStaerke);
			this.tabpage_Einheit.Controls.Add(this.txt_Kraefte_Einheit_SollStaerke);
			this.tabpage_Einheit.Controls.Add(this.lbl_Kraefte_Einheit_SollStaerke);
			this.tabpage_Einheit.Controls.Add(this.txt_Kraefte_Einheit_Funkrufname);
			this.tabpage_Einheit.Controls.Add(this.lbl_Kraefte_Einheit_Funk);
			this.tabpage_Einheit.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Einheit.Name = "tabpage_Einheit";
			this.tabpage_Einheit.Size = new System.Drawing.Size(392, 454);
			this.tabpage_Einheit.TabIndex = 0;
			this.tabpage_Einheit.Text = "Einheit";
			// 
			// gbx_Kraefte_Einheit_DatenSpeichern
			// 
			this.gbx_Kraefte_Einheit_DatenSpeichern.Controls.Add(this.btn_Kraefte_Einheit_DatenSpeichern_Speichern);
			this.gbx_Kraefte_Einheit_DatenSpeichern.Controls.Add(this.btn_Kraefte_Einheit_DatenSpeichern_Verwerfen);
			this.gbx_Kraefte_Einheit_DatenSpeichern.Location = new System.Drawing.Point(185, 395);
			this.gbx_Kraefte_Einheit_DatenSpeichern.Name = "gbx_Kraefte_Einheit_DatenSpeichern";
			this.gbx_Kraefte_Einheit_DatenSpeichern.Size = new System.Drawing.Size(200, 56);
			this.gbx_Kraefte_Einheit_DatenSpeichern.TabIndex = 27;
			this.gbx_Kraefte_Einheit_DatenSpeichern.TabStop = false;
			this.gbx_Kraefte_Einheit_DatenSpeichern.Text = "Daten Speichern";
			// 
			// btn_Kraefte_Einheit_DatenSpeichern_Speichern
			// 
			this.btn_Kraefte_Einheit_DatenSpeichern_Speichern.Location = new System.Drawing.Point(112, 20);
			this.btn_Kraefte_Einheit_DatenSpeichern_Speichern.Name = "btn_Kraefte_Einheit_DatenSpeichern_Speichern";
			this.btn_Kraefte_Einheit_DatenSpeichern_Speichern.TabIndex = 27;
			this.btn_Kraefte_Einheit_DatenSpeichern_Speichern.Text = "Speichern";
			this.btn_Kraefte_Einheit_DatenSpeichern_Speichern.Click += new System.EventHandler(this.btn_Kraefte_Einheit_DatenSpeichern_Speichern_Click);
			// 
			// btn_Kraefte_Einheit_DatenSpeichern_Verwerfen
			// 
			this.btn_Kraefte_Einheit_DatenSpeichern_Verwerfen.Location = new System.Drawing.Point(15, 20);
			this.btn_Kraefte_Einheit_DatenSpeichern_Verwerfen.Name = "btn_Kraefte_Einheit_DatenSpeichern_Verwerfen";
			this.btn_Kraefte_Einheit_DatenSpeichern_Verwerfen.TabIndex = 26;
			this.btn_Kraefte_Einheit_DatenSpeichern_Verwerfen.Text = "Verwerfen";
			this.btn_Kraefte_Einheit_DatenSpeichern_Verwerfen.Click += new System.EventHandler(this.btn_Kraefte_Einheit_DatenSpeichern_Verwerfen_Click);
			// 
			// btn_Kraefte_Einheit_Importieren
			// 
			this.btn_Kraefte_Einheit_Importieren.Location = new System.Drawing.Point(15, 415);
			this.btn_Kraefte_Einheit_Importieren.Name = "btn_Kraefte_Einheit_Importieren";
			this.btn_Kraefte_Einheit_Importieren.TabIndex = 26;
			this.btn_Kraefte_Einheit_Importieren.Text = "Importieren";
			this.btn_Kraefte_Einheit_Importieren.Click += new System.EventHandler(this.btn_Kraefte_Einheit_Importieren_Click);
			// 
			// dgrid_Kraefte_Einheit_Verbrauchsgueter
			// 
			this.dgrid_Kraefte_Einheit_Verbrauchsgueter.DataMember = "";
			this.dgrid_Kraefte_Einheit_Verbrauchsgueter.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgrid_Kraefte_Einheit_Verbrauchsgueter.Location = new System.Drawing.Point(10, 320);
			this.dgrid_Kraefte_Einheit_Verbrauchsgueter.Name = "dgrid_Kraefte_Einheit_Verbrauchsgueter";
			this.dgrid_Kraefte_Einheit_Verbrauchsgueter.Size = new System.Drawing.Size(370, 70);
			this.dgrid_Kraefte_Einheit_Verbrauchsgueter.TabIndex = 19;
			// 
			// lbl_Kraefte_Einheit_Verbrauchsgueter
			// 
			this.lbl_Kraefte_Einheit_Verbrauchsgueter.Location = new System.Drawing.Point(10, 295);
			this.lbl_Kraefte_Einheit_Verbrauchsgueter.Name = "lbl_Kraefte_Einheit_Verbrauchsgueter";
			this.lbl_Kraefte_Einheit_Verbrauchsgueter.TabIndex = 18;
			this.lbl_Kraefte_Einheit_Verbrauchsgueter.Text = "Verbrauchsgüter";
			// 
			// dgrid_Kraefte_Einheit_Material
			// 
			this.dgrid_Kraefte_Einheit_Material.DataMember = "";
			this.dgrid_Kraefte_Einheit_Material.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgrid_Kraefte_Einheit_Material.Location = new System.Drawing.Point(10, 225);
			this.dgrid_Kraefte_Einheit_Material.Name = "dgrid_Kraefte_Einheit_Material";
			this.dgrid_Kraefte_Einheit_Material.Size = new System.Drawing.Size(370, 70);
			this.dgrid_Kraefte_Einheit_Material.TabIndex = 17;
			// 
			// lbl_Kraefte_Einheit_Material
			// 
			this.lbl_Kraefte_Einheit_Material.Location = new System.Drawing.Point(10, 200);
			this.lbl_Kraefte_Einheit_Material.Name = "lbl_Kraefte_Einheit_Material";
			this.lbl_Kraefte_Einheit_Material.TabIndex = 16;
			this.lbl_Kraefte_Einheit_Material.Text = "Material";
			// 
			// dgrid_Kraefte_Einheit_Personen
			// 
			this.dgrid_Kraefte_Einheit_Personen.DataMember = "";
			this.dgrid_Kraefte_Einheit_Personen.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgrid_Kraefte_Einheit_Personen.Location = new System.Drawing.Point(10, 130);
			this.dgrid_Kraefte_Einheit_Personen.Name = "dgrid_Kraefte_Einheit_Personen";
			this.dgrid_Kraefte_Einheit_Personen.Size = new System.Drawing.Size(370, 70);
			this.dgrid_Kraefte_Einheit_Personen.TabIndex = 15;
			// 
			// lbl_Kraefte_Einheit_Personen
			// 
			this.lbl_Kraefte_Einheit_Personen.Location = new System.Drawing.Point(10, 110);
			this.lbl_Kraefte_Einheit_Personen.Name = "lbl_Kraefte_Einheit_Personen";
			this.lbl_Kraefte_Einheit_Personen.TabIndex = 14;
			this.lbl_Kraefte_Einheit_Personen.Text = "Personen";
			// 
			// txt_Kraefte_Einheit_Erreichbarkeit
			// 
			this.txt_Kraefte_Einheit_Erreichbarkeit.Location = new System.Drawing.Point(250, 60);
			this.txt_Kraefte_Einheit_Erreichbarkeit.Multiline = true;
			this.txt_Kraefte_Einheit_Erreichbarkeit.Name = "txt_Kraefte_Einheit_Erreichbarkeit";
			this.txt_Kraefte_Einheit_Erreichbarkeit.Size = new System.Drawing.Size(130, 45);
			this.txt_Kraefte_Einheit_Erreichbarkeit.TabIndex = 13;
			this.txt_Kraefte_Einheit_Erreichbarkeit.Text = "";
			// 
			// lbl_Kraefte_Einheit_Erreichbarkeit
			// 
			this.lbl_Kraefte_Einheit_Erreichbarkeit.Location = new System.Drawing.Point(200, 60);
			this.lbl_Kraefte_Einheit_Erreichbarkeit.Name = "lbl_Kraefte_Einheit_Erreichbarkeit";
			this.lbl_Kraefte_Einheit_Erreichbarkeit.Size = new System.Drawing.Size(45, 23);
			this.lbl_Kraefte_Einheit_Erreichbarkeit.TabIndex = 12;
			this.lbl_Kraefte_Einheit_Erreichbarkeit.Text = "Kontakt";
			// 
			// txt_Kraefte_Einheit_OV
			// 
			this.txt_Kraefte_Einheit_OV.Location = new System.Drawing.Point(65, 85);
			this.txt_Kraefte_Einheit_OV.Name = "txt_Kraefte_Einheit_OV";
			this.txt_Kraefte_Einheit_OV.Size = new System.Drawing.Size(125, 20);
			this.txt_Kraefte_Einheit_OV.TabIndex = 11;
			this.txt_Kraefte_Einheit_OV.Text = "";
			// 
			// txt_Kraefte_Einheit_gst
			// 
			this.txt_Kraefte_Einheit_gst.Location = new System.Drawing.Point(65, 60);
			this.txt_Kraefte_Einheit_gst.Name = "txt_Kraefte_Einheit_gst";
			this.txt_Kraefte_Einheit_gst.Size = new System.Drawing.Size(125, 20);
			this.txt_Kraefte_Einheit_gst.TabIndex = 10;
			this.txt_Kraefte_Einheit_gst.Text = "";
			// 
			// lbl_Kraefte_Einheit_OV
			// 
			this.lbl_Kraefte_Einheit_OV.Location = new System.Drawing.Point(10, 85);
			this.lbl_Kraefte_Einheit_OV.Name = "lbl_Kraefte_Einheit_OV";
			this.lbl_Kraefte_Einheit_OV.Size = new System.Drawing.Size(55, 23);
			this.lbl_Kraefte_Einheit_OV.TabIndex = 9;
			this.lbl_Kraefte_Einheit_OV.Text = "OV";
			// 
			// lbl_Kraefte_Einheit_gst
			// 
			this.lbl_Kraefte_Einheit_gst.Location = new System.Drawing.Point(10, 60);
			this.lbl_Kraefte_Einheit_gst.Name = "lbl_Kraefte_Einheit_gst";
			this.lbl_Kraefte_Einheit_gst.Size = new System.Drawing.Size(55, 23);
			this.lbl_Kraefte_Einheit_gst.TabIndex = 8;
			this.lbl_Kraefte_Einheit_gst.Text = "GST";
			// 
			// txt_Kraefte_Einheit_Name
			// 
			this.txt_Kraefte_Einheit_Name.Location = new System.Drawing.Point(65, 35);
			this.txt_Kraefte_Einheit_Name.Name = "txt_Kraefte_Einheit_Name";
			this.txt_Kraefte_Einheit_Name.Size = new System.Drawing.Size(125, 20);
			this.txt_Kraefte_Einheit_Name.TabIndex = 7;
			this.txt_Kraefte_Einheit_Name.Text = "";
			// 
			// lbl_Kraefte_Einheit_Name
			// 
			this.lbl_Kraefte_Einheit_Name.Location = new System.Drawing.Point(10, 35);
			this.lbl_Kraefte_Einheit_Name.Name = "lbl_Kraefte_Einheit_Name";
			this.lbl_Kraefte_Einheit_Name.Size = new System.Drawing.Size(55, 23);
			this.lbl_Kraefte_Einheit_Name.TabIndex = 6;
			this.lbl_Kraefte_Einheit_Name.Text = "Name";
			// 
			// txt_Kraefte_Einheit_IstStaerke
			// 
			this.txt_Kraefte_Einheit_IstStaerke.Location = new System.Drawing.Point(250, 35);
			this.txt_Kraefte_Einheit_IstStaerke.Name = "txt_Kraefte_Einheit_IstStaerke";
			this.txt_Kraefte_Einheit_IstStaerke.Size = new System.Drawing.Size(130, 20);
			this.txt_Kraefte_Einheit_IstStaerke.TabIndex = 5;
			this.txt_Kraefte_Einheit_IstStaerke.Text = "";
			// 
			// lbl_Kraefte_Einheit_IstStaerke
			// 
			this.lbl_Kraefte_Einheit_IstStaerke.Location = new System.Drawing.Point(200, 35);
			this.lbl_Kraefte_Einheit_IstStaerke.Name = "lbl_Kraefte_Einheit_IstStaerke";
			this.lbl_Kraefte_Einheit_IstStaerke.Size = new System.Drawing.Size(45, 23);
			this.lbl_Kraefte_Einheit_IstStaerke.TabIndex = 4;
			this.lbl_Kraefte_Einheit_IstStaerke.Text = "Ist";
			// 
			// txt_Kraefte_Einheit_SollStaerke
			// 
			this.txt_Kraefte_Einheit_SollStaerke.Location = new System.Drawing.Point(250, 10);
			this.txt_Kraefte_Einheit_SollStaerke.Name = "txt_Kraefte_Einheit_SollStaerke";
			this.txt_Kraefte_Einheit_SollStaerke.Size = new System.Drawing.Size(130, 20);
			this.txt_Kraefte_Einheit_SollStaerke.TabIndex = 3;
			this.txt_Kraefte_Einheit_SollStaerke.Text = "";
			// 
			// lbl_Kraefte_Einheit_SollStaerke
			// 
			this.lbl_Kraefte_Einheit_SollStaerke.Location = new System.Drawing.Point(200, 10);
			this.lbl_Kraefte_Einheit_SollStaerke.Name = "lbl_Kraefte_Einheit_SollStaerke";
			this.lbl_Kraefte_Einheit_SollStaerke.Size = new System.Drawing.Size(45, 23);
			this.lbl_Kraefte_Einheit_SollStaerke.TabIndex = 2;
			this.lbl_Kraefte_Einheit_SollStaerke.Text = "Soll";
			// 
			// txt_Kraefte_Einheit_Funkrufname
			// 
			this.txt_Kraefte_Einheit_Funkrufname.Location = new System.Drawing.Point(65, 10);
			this.txt_Kraefte_Einheit_Funkrufname.Name = "txt_Kraefte_Einheit_Funkrufname";
			this.txt_Kraefte_Einheit_Funkrufname.Size = new System.Drawing.Size(125, 20);
			this.txt_Kraefte_Einheit_Funkrufname.TabIndex = 1;
			this.txt_Kraefte_Einheit_Funkrufname.Text = "";
			// 
			// lbl_Kraefte_Einheit_Funk
			// 
			this.lbl_Kraefte_Einheit_Funk.Location = new System.Drawing.Point(8, 10);
			this.lbl_Kraefte_Einheit_Funk.Name = "lbl_Kraefte_Einheit_Funk";
			this.lbl_Kraefte_Einheit_Funk.Size = new System.Drawing.Size(56, 23);
			this.lbl_Kraefte_Einheit_Funk.TabIndex = 0;
			this.lbl_Kraefte_Einheit_Funk.Text = "Funk";
			// 
			// tabpage_Kfz
			// 
			this.tabpage_Kfz.Controls.Add(this.gbx_Kraefte_Kfz_DatenSpeichern);
			this.tabpage_Kfz.Controls.Add(this.btn_Kraefte_Kfz_Importieren);
			this.tabpage_Kfz.Controls.Add(this.txt_Kraefte_Kfz_Kommentar);
			this.tabpage_Kfz.Controls.Add(this.lbl_Kraefte_Kfz_Kommentar);
			this.tabpage_Kfz.Controls.Add(this.dataGrid2);
			this.tabpage_Kfz.Controls.Add(this.lbl_Kraefte_Kfz_Betrieb);
			this.tabpage_Kfz.Controls.Add(this.dgrid_Kraefte_Kfz_Fahrer);
			this.tabpage_Kfz.Controls.Add(this.txt_Kraefte_Kfz_Funkrufname);
			this.tabpage_Kfz.Controls.Add(this.lbl_Kraefte_Kfz_Funkrufname);
			this.tabpage_Kfz.Controls.Add(this.txt_Kraefte_Kfz_KfzTyp);
			this.tabpage_Kfz.Controls.Add(this.lbl_Kraefte_Kfz_KfzTyp);
			this.tabpage_Kfz.Controls.Add(this.lbl_Kraefte_Kfz_Fahrer);
			this.tabpage_Kfz.Controls.Add(this.txt_Kraefte_Kfz_Kennzeichen);
			this.tabpage_Kfz.Controls.Add(this.lbl_Kraefte_Kfz_Kennzeichen);
			this.tabpage_Kfz.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Kfz.Name = "tabpage_Kfz";
			this.tabpage_Kfz.Size = new System.Drawing.Size(392, 454);
			this.tabpage_Kfz.TabIndex = 2;
			this.tabpage_Kfz.Text = "Kfz";
			// 
			// gbx_Kraefte_Kfz_DatenSpeichern
			// 
			this.gbx_Kraefte_Kfz_DatenSpeichern.Controls.Add(this.btn__Kraefte_Kfz_DatenSpeichern_Speichern);
			this.gbx_Kraefte_Kfz_DatenSpeichern.Controls.Add(this.btn_Kraefte_Kfz_DatenSpeichern_Verwerfen);
			this.gbx_Kraefte_Kfz_DatenSpeichern.Location = new System.Drawing.Point(180, 385);
			this.gbx_Kraefte_Kfz_DatenSpeichern.Name = "gbx_Kraefte_Kfz_DatenSpeichern";
			this.gbx_Kraefte_Kfz_DatenSpeichern.Size = new System.Drawing.Size(200, 56);
			this.gbx_Kraefte_Kfz_DatenSpeichern.TabIndex = 27;
			this.gbx_Kraefte_Kfz_DatenSpeichern.TabStop = false;
			this.gbx_Kraefte_Kfz_DatenSpeichern.Text = "Daten Speichern";
			// 
			// btn__Kraefte_Kfz_DatenSpeichern_Speichern
			// 
			this.btn__Kraefte_Kfz_DatenSpeichern_Speichern.Location = new System.Drawing.Point(112, 20);
			this.btn__Kraefte_Kfz_DatenSpeichern_Speichern.Name = "btn__Kraefte_Kfz_DatenSpeichern_Speichern";
			this.btn__Kraefte_Kfz_DatenSpeichern_Speichern.TabIndex = 27;
			this.btn__Kraefte_Kfz_DatenSpeichern_Speichern.Text = "Speichern";
			this.btn__Kraefte_Kfz_DatenSpeichern_Speichern.Click += new System.EventHandler(this.btn__Kraefte_Kfz_DatenSpeichern_Speichern_Click);
			// 
			// btn_Kraefte_Kfz_DatenSpeichern_Verwerfen
			// 
			this.btn_Kraefte_Kfz_DatenSpeichern_Verwerfen.Location = new System.Drawing.Point(15, 20);
			this.btn_Kraefte_Kfz_DatenSpeichern_Verwerfen.Name = "btn_Kraefte_Kfz_DatenSpeichern_Verwerfen";
			this.btn_Kraefte_Kfz_DatenSpeichern_Verwerfen.TabIndex = 26;
			this.btn_Kraefte_Kfz_DatenSpeichern_Verwerfen.Text = "Verwerfen";
			this.btn_Kraefte_Kfz_DatenSpeichern_Verwerfen.Click += new System.EventHandler(this.btn_Kraefte_Kfz_DatenSpeichern_Verwerfen_Click);
			// 
			// btn_Kraefte_Kfz_Importieren
			// 
			this.btn_Kraefte_Kfz_Importieren.Location = new System.Drawing.Point(12, 400);
			this.btn_Kraefte_Kfz_Importieren.Name = "btn_Kraefte_Kfz_Importieren";
			this.btn_Kraefte_Kfz_Importieren.TabIndex = 26;
			this.btn_Kraefte_Kfz_Importieren.Text = "Importieren";
			this.btn_Kraefte_Kfz_Importieren.Click += new System.EventHandler(this.btn_Kraefte_Kfz_Importieren_Click);
			// 
			// txt_Kraefte_Kfz_Kommentar
			// 
			this.txt_Kraefte_Kfz_Kommentar.Location = new System.Drawing.Point(16, 320);
			this.txt_Kraefte_Kfz_Kommentar.Multiline = true;
			this.txt_Kraefte_Kfz_Kommentar.Name = "txt_Kraefte_Kfz_Kommentar";
			this.txt_Kraefte_Kfz_Kommentar.Size = new System.Drawing.Size(360, 56);
			this.txt_Kraefte_Kfz_Kommentar.TabIndex = 12;
			this.txt_Kraefte_Kfz_Kommentar.Text = "";
			// 
			// lbl_Kraefte_Kfz_Kommentar
			// 
			this.lbl_Kraefte_Kfz_Kommentar.Location = new System.Drawing.Point(16, 300);
			this.lbl_Kraefte_Kfz_Kommentar.Name = "lbl_Kraefte_Kfz_Kommentar";
			this.lbl_Kraefte_Kfz_Kommentar.TabIndex = 11;
			this.lbl_Kraefte_Kfz_Kommentar.Text = "Kommentar";
			// 
			// dataGrid2
			// 
			this.dataGrid2.DataMember = "";
			this.dataGrid2.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid2.Location = new System.Drawing.Point(16, 215);
			this.dataGrid2.Name = "dataGrid2";
			this.dataGrid2.Size = new System.Drawing.Size(360, 72);
			this.dataGrid2.TabIndex = 10;
			// 
			// lbl_Kraefte_Kfz_Betrieb
			// 
			this.lbl_Kraefte_Kfz_Betrieb.Location = new System.Drawing.Point(16, 195);
			this.lbl_Kraefte_Kfz_Betrieb.Name = "lbl_Kraefte_Kfz_Betrieb";
			this.lbl_Kraefte_Kfz_Betrieb.Size = new System.Drawing.Size(192, 23);
			this.lbl_Kraefte_Kfz_Betrieb.TabIndex = 9;
			this.lbl_Kraefte_Kfz_Betrieb.Text = "Einsatz Betreibsstunden/Kilometer";
			// 
			// dgrid_Kraefte_Kfz_Fahrer
			// 
			this.dgrid_Kraefte_Kfz_Fahrer.DataMember = "";
			this.dgrid_Kraefte_Kfz_Fahrer.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgrid_Kraefte_Kfz_Fahrer.Location = new System.Drawing.Point(16, 110);
			this.dgrid_Kraefte_Kfz_Fahrer.Name = "dgrid_Kraefte_Kfz_Fahrer";
			this.dgrid_Kraefte_Kfz_Fahrer.Size = new System.Drawing.Size(360, 72);
			this.dgrid_Kraefte_Kfz_Fahrer.TabIndex = 8;
			// 
			// txt_Kraefte_Kfz_Funkrufname
			// 
			this.txt_Kraefte_Kfz_Funkrufname.Location = new System.Drawing.Point(104, 40);
			this.txt_Kraefte_Kfz_Funkrufname.Name = "txt_Kraefte_Kfz_Funkrufname";
			this.txt_Kraefte_Kfz_Funkrufname.TabIndex = 7;
			this.txt_Kraefte_Kfz_Funkrufname.Text = "";
			// 
			// lbl_Kraefte_Kfz_Funkrufname
			// 
			this.lbl_Kraefte_Kfz_Funkrufname.Location = new System.Drawing.Point(16, 40);
			this.lbl_Kraefte_Kfz_Funkrufname.Name = "lbl_Kraefte_Kfz_Funkrufname";
			this.lbl_Kraefte_Kfz_Funkrufname.Size = new System.Drawing.Size(72, 23);
			this.lbl_Kraefte_Kfz_Funkrufname.TabIndex = 6;
			this.lbl_Kraefte_Kfz_Funkrufname.Text = "Funkrufname";
			// 
			// txt_Kraefte_Kfz_KfzTyp
			// 
			this.txt_Kraefte_Kfz_KfzTyp.Location = new System.Drawing.Point(104, 64);
			this.txt_Kraefte_Kfz_KfzTyp.Name = "txt_Kraefte_Kfz_KfzTyp";
			this.txt_Kraefte_Kfz_KfzTyp.TabIndex = 5;
			this.txt_Kraefte_Kfz_KfzTyp.Text = "";
			// 
			// lbl_Kraefte_Kfz_KfzTyp
			// 
			this.lbl_Kraefte_Kfz_KfzTyp.Location = new System.Drawing.Point(16, 64);
			this.lbl_Kraefte_Kfz_KfzTyp.Name = "lbl_Kraefte_Kfz_KfzTyp";
			this.lbl_Kraefte_Kfz_KfzTyp.Size = new System.Drawing.Size(56, 23);
			this.lbl_Kraefte_Kfz_KfzTyp.TabIndex = 4;
			this.lbl_Kraefte_Kfz_KfzTyp.Text = "Kfz Typ";
			// 
			// lbl_Kraefte_Kfz_Fahrer
			// 
			this.lbl_Kraefte_Kfz_Fahrer.Location = new System.Drawing.Point(16, 88);
			this.lbl_Kraefte_Kfz_Fahrer.Name = "lbl_Kraefte_Kfz_Fahrer";
			this.lbl_Kraefte_Kfz_Fahrer.Size = new System.Drawing.Size(56, 23);
			this.lbl_Kraefte_Kfz_Fahrer.TabIndex = 2;
			this.lbl_Kraefte_Kfz_Fahrer.Text = "Fahrer";
			// 
			// txt_Kraefte_Kfz_Kennzeichen
			// 
			this.txt_Kraefte_Kfz_Kennzeichen.Location = new System.Drawing.Point(104, 16);
			this.txt_Kraefte_Kfz_Kennzeichen.Name = "txt_Kraefte_Kfz_Kennzeichen";
			this.txt_Kraefte_Kfz_Kennzeichen.TabIndex = 1;
			this.txt_Kraefte_Kfz_Kennzeichen.Text = "";
			// 
			// lbl_Kraefte_Kfz_Kennzeichen
			// 
			this.lbl_Kraefte_Kfz_Kennzeichen.Location = new System.Drawing.Point(16, 16);
			this.lbl_Kraefte_Kfz_Kennzeichen.Name = "lbl_Kraefte_Kfz_Kennzeichen";
			this.lbl_Kraefte_Kfz_Kennzeichen.Size = new System.Drawing.Size(72, 23);
			this.lbl_Kraefte_Kfz_Kennzeichen.TabIndex = 0;
			this.lbl_Kraefte_Kfz_Kennzeichen.Text = "Kennzeichen";
			// 
			// tabctrl_Module
			// 
			this.tabctrl_Module.Controls.Add(this.groupBox1);
			this.tabctrl_Module.Controls.Add(this.button3);
			this.tabctrl_Module.Controls.Add(this.dataGrid1);
			this.tabctrl_Module.Controls.Add(this.label4);
			this.tabctrl_Module.Controls.Add(this.comboBox1);
			this.tabctrl_Module.Controls.Add(this.label3);
			this.tabctrl_Module.Controls.Add(this.textBox1);
			this.tabctrl_Module.Controls.Add(this.label2);
			this.tabctrl_Module.Location = new System.Drawing.Point(4, 22);
			this.tabctrl_Module.Name = "tabctrl_Module";
			this.tabctrl_Module.Size = new System.Drawing.Size(392, 454);
			this.tabctrl_Module.TabIndex = 3;
			this.tabctrl_Module.Text = "Module";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Location = new System.Drawing.Point(180, 390);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 56);
			this.groupBox1.TabIndex = 29;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Daten Speichern";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(112, 20);
			this.button1.Name = "button1";
			this.button1.TabIndex = 27;
			this.button1.Text = "Speichern";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(15, 20);
			this.button2.Name = "button2";
			this.button2.TabIndex = 26;
			this.button2.Text = "Verwerfen";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(10, 405);
			this.button3.Name = "button3";
			this.button3.TabIndex = 28;
			this.button3.Text = "Importieren";
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(15, 90);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(365, 285);
			this.dataGrid1.TabIndex = 17;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(20, 70);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(185, 23);
			this.label4.TabIndex = 16;
			this.label4.Text = "Einheiten/Helfer/KFZ";
			this.label4.Click += new System.EventHandler(this.label4_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Items.AddRange(new object[] {
														   "Einfacher Helfer",
														   "Truppenführer",
														   "Gruppenführer",
														   "Unterführer",
														   "Zugführer",
														   "Führer",
														   "Atemgeräteträger"});
			this.comboBox1.Location = new System.Drawing.Point(115, 40);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(140, 21);
			this.comboBox1.TabIndex = 11;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(20, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 23);
			this.label3.TabIndex = 10;
			this.label3.Text = "Position, Status";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(115, 15);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(140, 20);
			this.textBox1.TabIndex = 9;
			this.textBox1.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(20, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 23);
			this.label2.TabIndex = 8;
			this.label2.Text = "Name";
			// 
			// tabpage_Einsatzschwerpunkte
			// 
			this.tabpage_Einsatzschwerpunkte.Controls.Add(this.gbx_Einsatzschwerpunkte_Auswahl);
			this.tabpage_Einsatzschwerpunkte.Controls.Add(this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt);
			this.tabpage_Einsatzschwerpunkte.Controls.Add(this.label1);
			this.tabpage_Einsatzschwerpunkte.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Einsatzschwerpunkte.Name = "tabpage_Einsatzschwerpunkte";
			this.tabpage_Einsatzschwerpunkte.Size = new System.Drawing.Size(413, 499);
			this.tabpage_Einsatzschwerpunkte.TabIndex = 0;
			this.tabpage_Einsatzschwerpunkte.Text = "Einsatzschwerpunkte";
			// 
			// gbx_Einsatzschwerpunkte_Auswahl
			// 
			this.gbx_Einsatzschwerpunkte_Auswahl.Controls.Add(this.lbl_Einsatzschwerpunkte_BezeichnungAuswahl);
			this.gbx_Einsatzschwerpunkte_Auswahl.Controls.Add(this.cmb_Einsatzschwerpunkte_BezeichnungAuswahl);
			this.gbx_Einsatzschwerpunkte_Auswahl.Location = new System.Drawing.Point(5, 5);
			this.gbx_Einsatzschwerpunkte_Auswahl.Name = "gbx_Einsatzschwerpunkte_Auswahl";
			this.gbx_Einsatzschwerpunkte_Auswahl.Size = new System.Drawing.Size(400, 45);
			this.gbx_Einsatzschwerpunkte_Auswahl.TabIndex = 2;
			this.gbx_Einsatzschwerpunkte_Auswahl.TabStop = false;
			this.gbx_Einsatzschwerpunkte_Auswahl.Text = "Einsatzschwerpunkt wählen";
			// 
			// lbl_Einsatzschwerpunkte_BezeichnungAuswahl
			// 
			this.lbl_Einsatzschwerpunkte_BezeichnungAuswahl.Location = new System.Drawing.Point(10, 20);
			this.lbl_Einsatzschwerpunkte_BezeichnungAuswahl.Name = "lbl_Einsatzschwerpunkte_BezeichnungAuswahl";
			this.lbl_Einsatzschwerpunkte_BezeichnungAuswahl.Size = new System.Drawing.Size(70, 16);
			this.lbl_Einsatzschwerpunkte_BezeichnungAuswahl.TabIndex = 2;
			this.lbl_Einsatzschwerpunkte_BezeichnungAuswahl.Text = "Bezeichnung";
			// 
			// cmb_Einsatzschwerpunkte_BezeichnungAuswahl
			// 
			this.cmb_Einsatzschwerpunkte_BezeichnungAuswahl.Location = new System.Drawing.Point(80, 17);
			this.cmb_Einsatzschwerpunkte_BezeichnungAuswahl.Name = "cmb_Einsatzschwerpunkte_BezeichnungAuswahl";
			this.cmb_Einsatzschwerpunkte_BezeichnungAuswahl.Size = new System.Drawing.Size(210, 21);
			this.cmb_Einsatzschwerpunkte_BezeichnungAuswahl.TabIndex = 0;
			this.cmb_Einsatzschwerpunkte_BezeichnungAuswahl.SelectedIndexChanged += new System.EventHandler(this.cmb_Einsatzschwerpunkte_BezeichnungAuswahl_SelectedIndexChanged);
			// 
			// gbx_Einsatzschwerpunkte_Einsatzschwerpunkt
			// 
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkte_Prioritaet);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.txt_Einsatzschwerpunkte_Lage);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Lage);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.txt_Einsatzschwerpuntke_Bezeichnung);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.btn_Einsatzschwerpunkte_Verwerfen);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.btn_Einsatzschwerpunkte_Speichern);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.lblTemp);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Controls.Add(this.lbl_Einsatzschwerpunkte_Erkundungsergebnisse);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Enabled = false;
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Location = new System.Drawing.Point(5, 55);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Name = "gbx_Einsatzschwerpunkte_Einsatzschwerpunkt";
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Size = new System.Drawing.Size(400, 440);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.TabIndex = 1;
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.TabStop = false;
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Text = "Einsatzschwerpunkt";
			// 
			// btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen
			// 
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen.Location = new System.Drawing.Point(460, 75);
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen.Name = "btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen";
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen.Size = new System.Drawing.Size(155, 35);
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen.TabIndex = 11;
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen.Text = "Erkundungsergebnisse hinzufügen";
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_ErkundungsergebnisseHinzufuegen.Visible = false;
			// 
			// btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern
			// 
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern.Location = new System.Drawing.Point(460, 115);
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern.Name = "btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern";
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern.Size = new System.Drawing.Size(155, 23);
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern.TabIndex = 10;
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern.Text = "Priorität / Lage ändern";
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_PrioritaetLageAendern.Visible = false;
			// 
			// btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen
			// 
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen.Location = new System.Drawing.Point(460, 115);
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen.Name = "btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen";
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen.Size = new System.Drawing.Size(155, 23);
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen.TabIndex = 9;
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen.Text = "Schwerpunkt anlegen";
			this.btn_Einsatzschwerpunkte_Einsatzschwerpunkt_NeuAnlegen.Visible = false;
			// 
			// cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet
			// 
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet.Items.AddRange(new object[] {
																									   "1",
																									   "2",
																									   "3",
																									   "4",
																									   "5",
																									   "6",
																									   "7"});
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet.Location = new System.Drawing.Point(340, 30);
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet.Name = "cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet";
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet.Size = new System.Drawing.Size(40, 21);
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet.TabIndex = 8;
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet.Leave += new System.EventHandler(this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet_Leave);
			// 
			// lbl_Einsatzschwerpunkte_Einsatzschwerpunkte_Prioritaet
			// 
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkte_Prioritaet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkte_Prioritaet.Location = new System.Drawing.Point(290, 35);
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkte_Prioritaet.Name = "lbl_Einsatzschwerpunkte_Einsatzschwerpunkte_Prioritaet";
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkte_Prioritaet.Size = new System.Drawing.Size(45, 15);
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkte_Prioritaet.TabIndex = 7;
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkte_Prioritaet.Text = "Priorität";
			// 
			// cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter
			// 
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.Items.AddRange(new object[] {
																										  "Kraft 1",
																										  "Kraft 2",
																										  "Kraft 3",
																										  "Kraft 4"});
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.Location = new System.Drawing.Point(80, 40);
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.Name = "cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter";
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.Size = new System.Drawing.Size(210, 21);
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.TabIndex = 6;
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.Leave += new System.EventHandler(this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter_Leave);
			// 
			// lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter
			// 
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.Location = new System.Drawing.Point(10, 45);
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.Name = "lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter";
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.Size = new System.Drawing.Size(70, 15);
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.TabIndex = 5;
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.Text = "Einsatzleiter";
			// 
			// txt_Einsatzschwerpunkte_Lage
			// 
			this.txt_Einsatzschwerpunkte_Lage.Location = new System.Drawing.Point(80, 65);
			this.txt_Einsatzschwerpunkte_Lage.Multiline = true;
			this.txt_Einsatzschwerpunkte_Lage.Name = "txt_Einsatzschwerpunkte_Lage";
			this.txt_Einsatzschwerpunkte_Lage.Size = new System.Drawing.Size(305, 85);
			this.txt_Einsatzschwerpunkte_Lage.TabIndex = 3;
			this.txt_Einsatzschwerpunkte_Lage.Text = "Lagebeschreibung";
			this.txt_Einsatzschwerpunkte_Lage.Leave += new System.EventHandler(this.txt_Einsatzschwerpunkte_Lage_Leave);
			// 
			// lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Lage
			// 
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Lage.Location = new System.Drawing.Point(10, 70);
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Lage.Name = "lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Lage";
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Lage.Size = new System.Drawing.Size(60, 16);
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Lage.TabIndex = 2;
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Lage.Text = "Lage";
			// 
			// lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung
			// 
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung.Location = new System.Drawing.Point(10, 25);
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung.Name = "lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung";
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung.Size = new System.Drawing.Size(70, 16);
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung.TabIndex = 1;
			this.lbl_Einsatzschwerpunkte_Einsatzschwerpunkt_Bezeichnung.Text = "Bezeichnung";
			// 
			// txt_Einsatzschwerpuntke_Bezeichnung
			// 
			this.txt_Einsatzschwerpuntke_Bezeichnung.Location = new System.Drawing.Point(80, 20);
			this.txt_Einsatzschwerpuntke_Bezeichnung.Name = "txt_Einsatzschwerpuntke_Bezeichnung";
			this.txt_Einsatzschwerpuntke_Bezeichnung.Size = new System.Drawing.Size(210, 20);
			this.txt_Einsatzschwerpuntke_Bezeichnung.TabIndex = 0;
			this.txt_Einsatzschwerpuntke_Bezeichnung.Text = "";
			this.txt_Einsatzschwerpuntke_Bezeichnung.Leave += new System.EventHandler(this.txt_Einsatzschwerpuntke_Bezeichnung_Leave);
			// 
			// btn_Einsatzschwerpunkte_Verwerfen
			// 
			this.btn_Einsatzschwerpunkte_Verwerfen.Location = new System.Drawing.Point(40, 400);
			this.btn_Einsatzschwerpunkte_Verwerfen.Name = "btn_Einsatzschwerpunkte_Verwerfen";
			this.btn_Einsatzschwerpunkte_Verwerfen.Size = new System.Drawing.Size(130, 30);
			this.btn_Einsatzschwerpunkte_Verwerfen.TabIndex = 11;
			this.btn_Einsatzschwerpunkte_Verwerfen.Text = "Änderungen verwerfen";
			this.btn_Einsatzschwerpunkte_Verwerfen.Click += new System.EventHandler(this.btn_Einsatzschwerpunkte_Verwerfen_Click);
			// 
			// btn_Einsatzschwerpunkte_Speichern
			// 
			this.btn_Einsatzschwerpunkte_Speichern.Location = new System.Drawing.Point(225, 400);
			this.btn_Einsatzschwerpunkte_Speichern.Name = "btn_Einsatzschwerpunkte_Speichern";
			this.btn_Einsatzschwerpunkte_Speichern.Size = new System.Drawing.Size(130, 30);
			this.btn_Einsatzschwerpunkte_Speichern.TabIndex = 10;
			this.btn_Einsatzschwerpunkte_Speichern.Text = "Änderungen speichern";
			this.btn_Einsatzschwerpunkte_Speichern.Click += new System.EventHandler(this.btn_Einsatzschwerpunkte_Speichern_Click);
			// 
			// lblTemp
			// 
			this.lblTemp.Location = new System.Drawing.Point(80, 255);
			this.lblTemp.Name = "lblTemp";
			this.lblTemp.Size = new System.Drawing.Size(255, 35);
			this.lblTemp.TabIndex = 1;
			this.lblTemp.Text = "Hier sind nachher Erkundungsergebnisse tabellarisch dargestellt";
			// 
			// dgrid_Einsatzschwerpunkte_Erkundungsergebnisse
			// 
			this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse.DataMember = "";
			this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse.Location = new System.Drawing.Point(5, 170);
			this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse.Name = "dgrid_Einsatzschwerpunkte_Erkundungsergebnisse";
			this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse.Size = new System.Drawing.Size(390, 220);
			this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse.TabIndex = 0;
			// 
			// lbl_Einsatzschwerpunkte_Erkundungsergebnisse
			// 
			this.lbl_Einsatzschwerpunkte_Erkundungsergebnisse.Location = new System.Drawing.Point(5, 155);
			this.lbl_Einsatzschwerpunkte_Erkundungsergebnisse.Name = "lbl_Einsatzschwerpunkte_Erkundungsergebnisse";
			this.lbl_Einsatzschwerpunkte_Erkundungsergebnisse.Size = new System.Drawing.Size(150, 20);
			this.lbl_Einsatzschwerpunkte_Erkundungsergebnisse.TabIndex = 12;
			this.lbl_Einsatzschwerpunkte_Erkundungsergebnisse.Text = "Erkundungsergebnisse";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(536, 456);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "f60 un f70";
			// 
			// trv_Einsatzmanager
			// 
			this.trv_Einsatzmanager.AllowDrop = true;
			this.trv_Einsatzmanager.HotTracking = true;
			this.trv_Einsatzmanager.ImageList = this.iml_TreeViewBilderListe;
			this.trv_Einsatzmanager.ItemHeight = 14;
			this.trv_Einsatzmanager.Location = new System.Drawing.Point(430, 30);
			this.trv_Einsatzmanager.Name = "trv_Einsatzmanager";
			this.trv_Einsatzmanager.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																						   new System.Windows.Forms.TreeNode("Einsatzschwerpunkte", 18, 18, new System.Windows.Forms.TreeNode[] {
																																																	new System.Windows.Forms.TreeNode("Haus A", 8, 8, new System.Windows.Forms.TreeNode[] {
																																																																							  new System.Windows.Forms.TreeNode("B1 Cottbus (_/2/4=6)", 1, 1, new System.Windows.Forms.TreeNode[] {
																																																																																																	  new System.Windows.Forms.TreeNode("Helfer (6)", 7, 7, new System.Windows.Forms.TreeNode[] {
																																																																																																																									new System.Windows.Forms.TreeNode("GF Kafka, Franz", 7, 7),
																																																																																																																									new System.Windows.Forms.TreeNode("TrFü Chatwin, Bruce", 7, 7),
																																																																																																																									new System.Windows.Forms.TreeNode("McCullers, Carson", 2, 2),
																																																																																																																									new System.Windows.Forms.TreeNode("Maugham Somerest, W.", 2, 2),
																																																																																																																									new System.Windows.Forms.TreeNode("Eco, Umberto", 2, 2),
																																																																																																																									new System.Windows.Forms.TreeNode("Nooteboom, Cees", 2, 2)}),
																																																																																																	  new System.Windows.Forms.TreeNode("Fahrzeuge (2)", 1, 1, new System.Windows.Forms.TreeNode[] {
																																																																																																																									   new System.Windows.Forms.TreeNode("GKW1 (Iveco)", 1, 1),
																																																																																																																									   new System.Windows.Forms.TreeNode("Anhänger 8t", 1, 1)}),
																																																																																																	  new System.Windows.Forms.TreeNode("Material & Geräte (1)", 12, 12, new System.Windows.Forms.TreeNode[] {
																																																																																																																												 new System.Windows.Forms.TreeNode("NEA 5KVA", 12, 12)})})}),
																																																	new System.Windows.Forms.TreeNode("Verfügbare Einheiten/Module", 18, 18),
																																																	new System.Windows.Forms.TreeNode("Ruhende Einheiten/Module", 18, 18),
																																																	new System.Windows.Forms.TreeNode("Angeforderte Einheiten", 18, 18)}),
																						   new System.Windows.Forms.TreeNode("Einheiten/Module", 15, 15, new System.Windows.Forms.TreeNode[] {
																																																 new System.Windows.Forms.TreeNode("B1 Cottbus (_/2/4=6)", 1, 1, new System.Windows.Forms.TreeNode[] {
																																																																										 new System.Windows.Forms.TreeNode("Helfer (6)", 7, 7, new System.Windows.Forms.TreeNode[] {
																																																																																																	   new System.Windows.Forms.TreeNode("GF Kafka, Franz", 7, 7),
																																																																																																	   new System.Windows.Forms.TreeNode("TrFü Chatwin, Bruce", 7, 7),
																																																																																																	   new System.Windows.Forms.TreeNode("McCullers, Carson", 2, 2),
																																																																																																	   new System.Windows.Forms.TreeNode("Maugham Somerest, W.", 2, 2),
																																																																																																	   new System.Windows.Forms.TreeNode("Eco, Umberto", 2, 2),
																																																																																																	   new System.Windows.Forms.TreeNode("Nooteboom, Cees", 2, 2)}),
																																																																										 new System.Windows.Forms.TreeNode("Fahrzeuge (2)", 1, 1, new System.Windows.Forms.TreeNode[] {
																																																																																																		  new System.Windows.Forms.TreeNode("GKW1 (Iveco)", 1, 1),
																																																																																																		  new System.Windows.Forms.TreeNode("Anhänger 8t", 1, 1)}),
																																																																										 new System.Windows.Forms.TreeNode("Material & Geräte (1)", 15, 15, new System.Windows.Forms.TreeNode[] {
																																																																																																					new System.Windows.Forms.TreeNode("NAE 5KVA", 11, 11)})}),
																																																 new System.Windows.Forms.TreeNode("1.TZ Frankfurt Oder ", 5, 5, new System.Windows.Forms.TreeNode[] {
																																																																										 new System.Windows.Forms.TreeNode("B1 1.TZ Frankfurt Oder", 0, 0, new System.Windows.Forms.TreeNode[] {
																																																																																																				   new System.Windows.Forms.TreeNode("Helfer", 2, 2),
																																																																																																				   new System.Windows.Forms.TreeNode("Fahrzeuge", 5, 5),
																																																																																																				   new System.Windows.Forms.TreeNode("Material & Geräte", 19, 19)}),
																																																																										 new System.Windows.Forms.TreeNode("ZTr 1.TZ Frankfurt Oder", 10, 10, new System.Windows.Forms.TreeNode[] {
																																																																																																					  new System.Windows.Forms.TreeNode("Helfer", 2, 2),
																																																																																																					  new System.Windows.Forms.TreeNode("Fahrzeuge", 11, 11),
																																																																																																					  new System.Windows.Forms.TreeNode("Material & Geräte", 19, 19)})})}),
																						   new System.Windows.Forms.TreeNode("Material", 19, 19, new System.Windows.Forms.TreeNode[] {
																																														 new System.Windows.Forms.TreeNode("Schaufeln (Bestand: 15)", 4, 4)})});
			this.trv_Einsatzmanager.Size = new System.Drawing.Size(213, 455);
			this.trv_Einsatzmanager.TabIndex = 6;
			this.trv_Einsatzmanager.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trv_Einsatzmanager_MouseDown);
			this.trv_Einsatzmanager.DragOver += new System.Windows.Forms.DragEventHandler(this.trv_Einsatzmanager_DragOver);
			this.trv_Einsatzmanager.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trv_Einsatzmanager_AfterSelect);
			this.trv_Einsatzmanager.DragEnter += new System.Windows.Forms.DragEventHandler(this.trv_Einsatzmanager_DragEnter);
			this.trv_Einsatzmanager.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.trv_Einsatzmanager_ItemDrag);
			this.trv_Einsatzmanager.DragLeave += new System.EventHandler(this.trv_Einsatzmanager_DragLeave);
			this.trv_Einsatzmanager.DragDrop += new System.Windows.Forms.DragEventHandler(this.trv_Einsatzmanager_DragDrop);
			// 
			// iml_TreeViewBilderListe
			// 
			this.iml_TreeViewBilderListe.ImageSize = new System.Drawing.Size(15, 15);
			this.iml_TreeViewBilderListe.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_TreeViewBilderListe.ImageStream")));
			this.iml_TreeViewBilderListe.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ofd_Kraefte_Importieren
			// 
			this.ofd_Kraefte_Importieren.Filter = "pELS Dateien| *.els|Alle Dateien|*.*";
			// 
			// ctx_abstrakt_Einsatzschwerpunkte
			// 
			this.ctx_abstrakt_Einsatzschwerpunkte.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																											 this.mi_ueberschrift_abstrakt_einsatzschwerpunkte,
																											 this.menuItem2,
																											 this.mI_abstrakt_ESP_NeuenESPanlegen});
			this.ctx_abstrakt_Einsatzschwerpunkte.Popup += new System.EventHandler(this.ctx_abstrakt_Einsatzschwerpunkt_Popup);
			// 
			// mi_ueberschrift_abstrakt_einsatzschwerpunkte
			// 
			this.mi_ueberschrift_abstrakt_einsatzschwerpunkte.Enabled = false;
			this.mi_ueberschrift_abstrakt_einsatzschwerpunkte.Index = 0;
			this.mi_ueberschrift_abstrakt_einsatzschwerpunkte.Text = "Einsatzschwerpunkte";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "-";
			// 
			// mI_abstrakt_ESP_NeuenESPanlegen
			// 
			this.mI_abstrakt_ESP_NeuenESPanlegen.Index = 2;
			this.mI_abstrakt_ESP_NeuenESPanlegen.Text = "Neuen ESP anlegen";
			this.mI_abstrakt_ESP_NeuenESPanlegen.Click += new System.EventHandler(this.mI_abstrakt_ESP_NeuenESPanlegen_Click);
			// 
			// ctx_Einsatzschwerpunkte
			// 
			this.ctx_Einsatzschwerpunkte.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																									this.mi_ueberschrift_Einsatzschwerpunkte,
																									this.menuItem3});
			// 
			// mi_ueberschrift_Einsatzschwerpunkte
			// 
			this.mi_ueberschrift_Einsatzschwerpunkte.Enabled = false;
			this.mi_ueberschrift_Einsatzschwerpunkte.Index = 0;
			this.mi_ueberschrift_Einsatzschwerpunkte.Text = "Einsatzschwerpunkt";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "-";
			// 
			// ctx_abstrakt_Helfer
			// 
			this.ctx_abstrakt_Helfer.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																								this.mi_ueberschrift_abstrakt_Helfer,
																								this.menuItem4,
																								this.mI_abstrakt_Helfer_HelferAnlegen,
																								this.mI_abstrakt_Helfer_WurdeVerpflegt,
																								this.mI_abstrakt_Helfer_StatusSetzenAuf});
			// 
			// mi_ueberschrift_abstrakt_Helfer
			// 
			this.mi_ueberschrift_abstrakt_Helfer.Index = 0;
			this.mi_ueberschrift_abstrakt_Helfer.Text = "Helfer";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.Text = "-";
			// 
			// mI_abstrakt_Helfer_HelferAnlegen
			// 
			this.mI_abstrakt_Helfer_HelferAnlegen.Index = 2;
			this.mI_abstrakt_Helfer_HelferAnlegen.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																											 this.menuItem23,
																											 this.menuItem24,
																											 this.menuItem25,
																											 this.menuItem26,
																											 this.menuItem27,
																											 this.menuItem28,
																											 this.menuItem29,
																											 this.menuItem30,
																											 this.menuItem31,
																											 this.menuItem32,
																											 this.menuItem33,
																											 this.menuItem34,
																											 this.menuItem35,
																											 this.menuItem36});
			this.mI_abstrakt_Helfer_HelferAnlegen.Text = "Helfer anlegen";
			this.mI_abstrakt_Helfer_HelferAnlegen.Click += new System.EventHandler(this.mI_abstrakt_Helfer_HelferAnlegen_Click);
			// 
			// menuItem23
			// 
			this.menuItem23.Index = 0;
			this.menuItem23.Text = "1";
			// 
			// menuItem24
			// 
			this.menuItem24.Index = 1;
			this.menuItem24.Text = "2";
			// 
			// menuItem25
			// 
			this.menuItem25.Index = 2;
			this.menuItem25.Text = "3";
			// 
			// menuItem26
			// 
			this.menuItem26.Index = 3;
			this.menuItem26.Text = "4";
			// 
			// menuItem27
			// 
			this.menuItem27.Index = 4;
			this.menuItem27.Text = "5";
			// 
			// menuItem28
			// 
			this.menuItem28.Index = 5;
			this.menuItem28.Text = "6";
			// 
			// menuItem29
			// 
			this.menuItem29.Index = 6;
			this.menuItem29.Text = "7";
			// 
			// menuItem30
			// 
			this.menuItem30.Index = 7;
			this.menuItem30.Text = "8";
			// 
			// menuItem31
			// 
			this.menuItem31.Index = 8;
			this.menuItem31.Text = "9";
			// 
			// menuItem32
			// 
			this.menuItem32.Index = 9;
			this.menuItem32.Text = "10";
			// 
			// menuItem33
			// 
			this.menuItem33.Index = 10;
			this.menuItem33.Text = "11";
			// 
			// menuItem34
			// 
			this.menuItem34.Index = 11;
			this.menuItem34.Text = "12";
			// 
			// menuItem35
			// 
			this.menuItem35.Index = 12;
			this.menuItem35.Text = "20";
			// 
			// menuItem36
			// 
			this.menuItem36.Index = 13;
			this.menuItem36.Text = "50";
			// 
			// mI_abstrakt_Helfer_WurdeVerpflegt
			// 
			this.mI_abstrakt_Helfer_WurdeVerpflegt.Index = 3;
			this.mI_abstrakt_Helfer_WurdeVerpflegt.Text = "wurden soeben verpflegt";
			this.mI_abstrakt_Helfer_WurdeVerpflegt.Click += new System.EventHandler(this.mI_abstrakt_Helfer_WurdeVerpflegt_Click);
			// 
			// mI_abstrakt_Helfer_StatusSetzenAuf
			// 
			this.mI_abstrakt_Helfer_StatusSetzenAuf.Index = 4;
			this.mI_abstrakt_Helfer_StatusSetzenAuf.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																											   this.menuItem44});
			this.mI_abstrakt_Helfer_StatusSetzenAuf.Text = "Status setzen auf";
			this.mI_abstrakt_Helfer_StatusSetzenAuf.Click += new System.EventHandler(this.mI_abstrakt_Helfer_StatusSetzenAuf_Click);
			// 
			// menuItem44
			// 
			this.menuItem44.Index = 0;
			this.menuItem44.Text = "TDV_ ....";
			// 
			// ctx_Helfer
			// 
			this.ctx_Helfer.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.mi_ueberschrift_Helfer,
																					   this.menuItem6});
			// 
			// mi_ueberschrift_Helfer
			// 
			this.mi_ueberschrift_Helfer.Index = 0;
			this.mi_ueberschrift_Helfer.Text = "Helfer";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 1;
			this.menuItem6.Text = "-";
			// 
			// ctx_abstrakt_Material
			// 
			this.ctx_abstrakt_Material.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																								  this.mi_ueberschrift_abstrakt_Material,
																								  this.menuItem37});
			// 
			// mi_ueberschrift_abstrakt_Material
			// 
			this.mi_ueberschrift_abstrakt_Material.Enabled = false;
			this.mi_ueberschrift_abstrakt_Material.Index = 0;
			this.mi_ueberschrift_abstrakt_Material.Text = "Material";
			// 
			// menuItem37
			// 
			this.menuItem37.Index = 1;
			this.menuItem37.Text = "-";
			// 
			// ctx_Material
			// 
			this.ctx_Material.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mi_ueberschrift_Material,
																						 this.menuItem8});
			// 
			// mi_ueberschrift_Material
			// 
			this.mi_ueberschrift_Material.Index = 0;
			this.mi_ueberschrift_Material.Text = "Material";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 1;
			this.menuItem8.Text = "-";
			// 
			// ctx_abstrakt_Fahrzeuge
			// 
			this.ctx_abstrakt_Fahrzeuge.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																								   this.mi_ueberschrift_abstrakt_Fahrzeuge,
																								   this.menuItem10,
																								   this.mI_abstrakt_Fahrzeuge_NeuesKFZAnlegen});
			// 
			// mi_ueberschrift_abstrakt_Fahrzeuge
			// 
			this.mi_ueberschrift_abstrakt_Fahrzeuge.Enabled = false;
			this.mi_ueberschrift_abstrakt_Fahrzeuge.Index = 0;
			this.mi_ueberschrift_abstrakt_Fahrzeuge.Text = "Fahrzeuge";
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 1;
			this.menuItem10.Text = "-";
			// 
			// mI_abstrakt_Fahrzeuge_NeuesKFZAnlegen
			// 
			this.mI_abstrakt_Fahrzeuge_NeuesKFZAnlegen.Index = 2;
			this.mI_abstrakt_Fahrzeuge_NeuesKFZAnlegen.Text = "Neues KFZ anlegen";
			this.mI_abstrakt_Fahrzeuge_NeuesKFZAnlegen.Click += new System.EventHandler(this.mI_abstrakt_Fahrzeuge_NeuesKFZAnlegen_Click);
			// 
			// ctx_Fahrzeuge
			// 
			this.ctx_Fahrzeuge.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.mi_ueberschrift_Fahrzeuge,
																						  this.menuItem12});
			// 
			// mi_ueberschrift_Fahrzeuge
			// 
			this.mi_ueberschrift_Fahrzeuge.Index = 0;
			this.mi_ueberschrift_Fahrzeuge.Text = "Fahrzeug";
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 1;
			this.menuItem12.Text = "-";
			// 
			// ctx_abstrakt_Module
			// 
			this.ctx_abstrakt_Module.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																								this.mi_ueberschrift_abstrakt_Module,
																								this.menuItem14,
																								this.mI_abstrakt_Modul_NeuesModulAnlegen,
																								this.mI_abstrakt_Modul_WurdeVerpflegt,
																								this.mI_abstrakt_Modul_KraeftestatusSetzen});
			// 
			// mi_ueberschrift_abstrakt_Module
			// 
			this.mi_ueberschrift_abstrakt_Module.Enabled = false;
			this.mi_ueberschrift_abstrakt_Module.Index = 0;
			this.mi_ueberschrift_abstrakt_Module.Text = "Module";
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 1;
			this.menuItem14.Text = "-";
			// 
			// mI_abstrakt_Modul_NeuesModulAnlegen
			// 
			this.mI_abstrakt_Modul_NeuesModulAnlegen.Index = 2;
			this.mI_abstrakt_Modul_NeuesModulAnlegen.Text = "Neues Modul anlegen";
			this.mI_abstrakt_Modul_NeuesModulAnlegen.Click += new System.EventHandler(this.mI_abstrakt_Modul_NeuesModulAnlegen_Click);
			// 
			// mI_abstrakt_Modul_WurdeVerpflegt
			// 
			this.mI_abstrakt_Modul_WurdeVerpflegt.Index = 3;
			this.mI_abstrakt_Modul_WurdeVerpflegt.Text = "wurde verpflegt (alle Helfer)";
			this.mI_abstrakt_Modul_WurdeVerpflegt.Click += new System.EventHandler(this.mI_abstrakt_Modul_WurdeVerpflegt_Click);
			// 
			// mI_abstrakt_Modul_KraeftestatusSetzen
			// 
			this.mI_abstrakt_Modul_KraeftestatusSetzen.Index = 4;
			this.mI_abstrakt_Modul_KraeftestatusSetzen.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																												  this.menuItem41});
			this.mI_abstrakt_Modul_KraeftestatusSetzen.Text = "Kräftestaus setzen (alle Kräfte)";
			this.mI_abstrakt_Modul_KraeftestatusSetzen.Click += new System.EventHandler(this.mI_abstrakt_Modul_KraeftestatusSetzen_Click);
			// 
			// menuItem41
			// 
			this.menuItem41.Index = 0;
			this.menuItem41.Text = "TDV_Kräftestatus....";
			// 
			// ctx_Module
			// 
			this.ctx_Module.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.mi_ueberschrift_Module,
																					   this.menuItem16});
			// 
			// mi_ueberschrift_Module
			// 
			this.mi_ueberschrift_Module.Index = 0;
			this.mi_ueberschrift_Module.Text = "Modul";
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 1;
			this.menuItem16.Text = "-";
			// 
			// ctx_abstrakt_Einheiten
			// 
			this.ctx_abstrakt_Einheiten.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																								   this.mi_ueberschrift_abstrakt_Einheiten,
																								   this.menuItem18,
																								   this.mI_abstrakt_Einheiten_NeueEinheitAnlegen});
			// 
			// mi_ueberschrift_abstrakt_Einheiten
			// 
			this.mi_ueberschrift_abstrakt_Einheiten.Enabled = false;
			this.mi_ueberschrift_abstrakt_Einheiten.Index = 0;
			this.mi_ueberschrift_abstrakt_Einheiten.Text = "Einheiten";
			// 
			// menuItem18
			// 
			this.menuItem18.Index = 1;
			this.menuItem18.Text = "-";
			// 
			// mI_abstrakt_Einheiten_NeueEinheitAnlegen
			// 
			this.mI_abstrakt_Einheiten_NeueEinheitAnlegen.Index = 2;
			this.mI_abstrakt_Einheiten_NeueEinheitAnlegen.Text = "Neue Einheit anlegen";
			this.mI_abstrakt_Einheiten_NeueEinheitAnlegen.Click += new System.EventHandler(this.mI_abstrakt_Einheiten_NeueEinheitAnlegen_Click);
			// 
			// ctx_Einheiten
			// 
			this.ctx_Einheiten.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.mi_ueberschrift_Einheit,
																						  this.menuItem20});
			// 
			// mi_ueberschrift_Einheit
			// 
			this.mi_ueberschrift_Einheit.Index = 0;
			this.mi_ueberschrift_Einheit.Text = "Einheit";
			this.mi_ueberschrift_Einheit.Click += new System.EventHandler(this.menuItem19_Click);
			// 
			// menuItem20
			// 
			this.menuItem20.Index = 1;
			this.menuItem20.Text = "-";
			// 
			// btn_Aktualisieren
			// 
			this.btn_Aktualisieren.Location = new System.Drawing.Point(435, 490);
			this.btn_Aktualisieren.Name = "btn_Aktualisieren";
			this.btn_Aktualisieren.Size = new System.Drawing.Size(205, 30);
			this.btn_Aktualisieren.TabIndex = 7;
			this.btn_Aktualisieren.Text = "Aktualisieren";
			this.btn_Aktualisieren.Click += new System.EventHandler(this.btn_Aktualisieren_Click);
			// 
			// tmr_ESPTimer
			// 
			this.tmr_ESPTimer.Enabled = true;
			this.tmr_ESPTimer.Tick += new System.EventHandler(this.tmr_ESPTimer_Tick);
			// 
			// ep_Eingabe
			// 
			this.ep_Eingabe.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.ep_Eingabe.ContainerControl = this;
			// 
			// tabpage_Kraefte_Helfer
			// 
			this.tabpage_Kraefte_Helfer.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Kraefte_Helfer.Name = "tabpage_Kraefte_Helfer";
			this.tabpage_Kraefte_Helfer.Size = new System.Drawing.Size(413, 499);
			this.tabpage_Kraefte_Helfer.TabIndex = 3;
			this.tabpage_Kraefte_Helfer.Text = "Helfer";
			// 
			// tabpage_Kraefte_Einheit
			// 
			this.tabpage_Kraefte_Einheit.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Kraefte_Einheit.Name = "tabpage_Kraefte_Einheit";
			this.tabpage_Kraefte_Einheit.Size = new System.Drawing.Size(413, 499);
			this.tabpage_Kraefte_Einheit.TabIndex = 4;
			this.tabpage_Kraefte_Einheit.Text = "Einheit";
			// 
			// tabpage_Kraefte_Kfz
			// 
			this.tabpage_Kraefte_Kfz.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Kraefte_Kfz.Name = "tabpage_Kraefte_Kfz";
			this.tabpage_Kraefte_Kfz.Size = new System.Drawing.Size(413, 499);
			this.tabpage_Kraefte_Kfz.TabIndex = 5;
			this.tabpage_Kraefte_Kfz.Text = "Kfz";
			// 
			// tabpage_Kraefte_Module
			// 
			this.tabpage_Kraefte_Module.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Kraefte_Module.Name = "tabpage_Kraefte_Module";
			this.tabpage_Kraefte_Module.Size = new System.Drawing.Size(413, 499);
			this.tabpage_Kraefte_Module.TabIndex = 6;
			this.tabpage_Kraefte_Module.Text = "Module";
			// 
			// Cpr_usc_EK
			// 
			this.AllowDrop = true;
			this.Controls.Add(this.btn_Aktualisieren);
			this.Controls.Add(this.tabctrl_EK);
			this.Controls.Add(this.trv_Einsatzmanager);
			this.Name = "Cpr_usc_EK";
			this.Size = new System.Drawing.Size(650, 530);
			this.Load += new System.EventHandler(this.usc_EK_Load);
			this.tabctrl_EK.ResumeLayout(false);
			this.tabpage_Kraefte.ResumeLayout(false);
			this.tabctrl_Kraefte.ResumeLayout(false);
			this.tabpage_Helfer.ResumeLayout(false);
			this.gbx_Kraefte_Helfer_DatenSpeichern.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Kraefte_Helfer_Arbeitszeiten)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Kraefte_Helfer_Person)).EndInit();
			this.tabpage_Einheit.ResumeLayout(false);
			this.gbx_Kraefte_Einheit_DatenSpeichern.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Kraefte_Einheit_Verbrauchsgueter)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Kraefte_Einheit_Material)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Kraefte_Einheit_Personen)).EndInit();
			this.tabpage_Kfz.ResumeLayout(false);
			this.gbx_Kraefte_Kfz_DatenSpeichern.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Kraefte_Kfz_Fahrer)).EndInit();
			this.tabctrl_Module.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.tabpage_Einsatzschwerpunkte.ResumeLayout(false);
			this.gbx_Einsatzschwerpunkte_Auswahl.ResumeLayout(false);
			this.gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Einsatzschwerpunkte_Erkundungsergebnisse)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Eingabevalidierung

		#region Eingabevalidierung ESP
		
		private bool EingabevalidierungESP()
		{
			if(this.ValidiereBezeichnungESP() && this.ValidiereLageESP() && this.ValidierePrioESP() && this.ValidiereEinsatzleiterESP())
				return(true);
			this.txt_Bezeichnung_Validated_ESP(null, null);
			this.txt_Lage_Validated_ESP(null, null);
			this.cmb_Prio_Validated_ESP(null, null);
			this.cmb_Einsatzleiter_Validated_ESP(null, null);
			return(false);
		}

		private bool ValidiereEinsatzleiterESP()
		{
			return(this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.SelectedItem is Cdv_Helfer);
		}

		private void cmb_Einsatzleiter_Validated_ESP(object sender, System.EventArgs e)
		{
			if(ValidiereEinsatzleiterESP())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter, "Bitte wählen Sie einen Einsatzleiter aus");
			}
		}		

		private bool ValidierePrioESP()
		{
			int iPrio;
			try
			{
				iPrio = Int32.Parse(this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet.Text);
			}
			catch
			{
				return(false);
			}
			if(iPrio > 0 && iPrio < 8)
				return(true);
			return(false);
		}

		private bool ValidiereLageESP()
		{
			return(this.txt_Einsatzschwerpunkte_Lage.Text.Length > 0);
		}

		private bool ValidiereBezeichnungESP()
		{
			return(this.txt_Einsatzschwerpuntke_Bezeichnung.Text.Length > 0);
		}

		private void cmb_Prio_Validated_ESP(object sender, System.EventArgs e)
		{
			if(ValidierePrioESP())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet, "Bitte geben Sie eine Priorität ein");
			}
		}

		private void txt_Bezeichnung_Validated_ESP(object sender, System.EventArgs e)
		{
			if(ValidiereBezeichnungESP())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_Einsatzschwerpuntke_Bezeichnung, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_Einsatzschwerpuntke_Bezeichnung, "Bitte geben Sie eine Bezeichnung ein");
			}
		}

		private void txt_Lage_Validated_ESP(object sender, System.EventArgs e)
		{
			if(ValidiereLageESP())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_Einsatzschwerpunkte_Lage, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_Einsatzschwerpunkte_Lage, "Bitte geben Sie eine Lage ein");
			}
		}

		#endregion

		#region Eingabevalidierung Helfer
		
		private bool EingabevalidierungHelfer()
		{
			if(this.ValidiereNameHelfer() && this.ValidiereVornameHelfer() && this.ValidierePositionHelfer()
				&& this.ValidiereStatusHelfer() && this.ValidiereGeburtsdatumHelfer() && this.ValidiereAnschriftHelfer()
				&& this.ValidiereOVHelfer() && this.ValidiereErreichbarkeitHelfer())
				return(true);
			this.txt_Helfer_Name_Validated_ESP(null, null);
			this.txt_Helfer_Vorname_Validated_ESP(null, null);
			this.cmb_Helfer_Position_Validated_ESP(null, null);
			this.cmb_Helfer_Status_Validated_ESP(null, null);
			this.dtp_Helfer_Geburtsdatum_Validated_ESP(null, null);
			this.txt_Helfer_Anschrift_Validated_ESP(null, null);
			this.txt_Helfer_OV_Validated_ESP(null, null);
			this.txt_Helfer_Erreichbarkeit_Validated_ESP(null, null);
			return(false);
		}

		private bool ValidiereErreichbarkeitHelfer()
		{
			return(this.txt_Kraefte_Helfer_Erreichbarkeit.Text.Length > 0);
		}

		private void txt_Helfer_Erreichbarkeit_Validated_ESP(object sender, System.EventArgs e)
		{
			if(this.ValidiereErreichbarkeitHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_Kraefte_Helfer_Erreichbarkeit, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_Kraefte_Helfer_Erreichbarkeit, "Bitte geben sie die Helfererreichbarkeit an");
			}
		}
		
		private bool ValidiereOVHelfer()
		{
			return(this.txt_Kraefte_Helfer_OV.Text.Length > 0);
		}

		private void txt_Helfer_OV_Validated_ESP(object sender, System.EventArgs e)
		{
			if(this.ValidiereOVHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_Kraefte_Helfer_OV, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_Kraefte_Helfer_OV, "Bitte geben sie ein Ortsverband an");
			}
		}

		private string[] ParseAnschrift(string pin_StrasseHausnummer, string pin_PLZOrt)
		{
			string[] szReturnVal = new string[4];
			try
			{
				szReturnVal[0] = pin_StrasseHausnummer.Substring(0,pin_StrasseHausnummer.LastIndexOf(" "));
				szReturnVal[1] = pin_StrasseHausnummer.Substring(pin_StrasseHausnummer.LastIndexOf(" ") + 1);
				szReturnVal[2] = pin_PLZOrt.Substring(0, pin_PLZOrt.LastIndexOf(" "));
				szReturnVal[3] = pin_PLZOrt.Substring(pin_PLZOrt.LastIndexOf(" ") + 1);
			}
			catch
			{

				return(null);
			}
			return(szReturnVal);
		}

		private bool ValidiereAnschriftHelfer()
		{
			string[] pszaLines = this.txt_Kraefte_Helfer_Anschrift.Lines;
			int iLines = pszaLines.Length;
			if(iLines != 2)
				return(false);
			string[] pszaParsed = this.ParseAnschrift(pszaLines[0], pszaLines[1]);
			if(pszaParsed == null)
				return(false);
			return(true);
		}

		private void txt_Helfer_Anschrift_Validated_ESP(object sender, System.EventArgs e)
		{
			if(this.ValidiereAnschriftHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_Kraefte_Helfer_Anschrift, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_Kraefte_Helfer_Anschrift, "Bitte geben sie ein Anschrift an. Format:\n1 Zeile:Strasse Hausnummer\n2 Zeile:PLZ Ort");
			}
		}

		private bool ValidiereGeburtsdatumHelfer()
		{
			return(this.dtp_Kraefte_Helfer_Geburtsdatum.Value < DateTime.Now);
		}

		private void dtp_Helfer_Geburtsdatum_Validated_ESP(object sender, System.EventArgs e)
		{
			if(this.ValidiereGeburtsdatumHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.dtp_Kraefte_Helfer_Geburtsdatum, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.dtp_Kraefte_Helfer_Geburtsdatum, "Bitte wählen Sie ein gültiges Geburtsdatum aus");
			}
		}

		private bool ValidiereStatusHelfer()
		{
			return(this.cmb_Kraefte_Helfer_Status.Text.Length > 0);
		}

		private void cmb_Helfer_Status_Validated_ESP(object sender, System.EventArgs e)
		{
			if(this.ValidiereStatusHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.cmb_Kraefte_Helfer_Status, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.cmb_Kraefte_Helfer_Status, "Bitte wählen Sie einen Helferstatus aus");
			}
		}

		private bool ValidierePositionHelfer()
		{
			return(this.cmb_Kraefte_Helfer_Position.Text.Length > 0);
		}

		private void cmb_Helfer_Position_Validated_ESP(object sender, System.EventArgs e)
		{
			if(this.ValidierePositionHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.cmb_Kraefte_Helfer_Position, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.cmb_Kraefte_Helfer_Position, "Bitte wählen Sie eine Helferposition aus");
			}
		}

		private bool ValidiereVornameHelfer()
		{
			return(this.txt_Kraefte_Helfer_Vorname.Text.Length > 0);
		}

		private void txt_Helfer_Vorname_Validated_ESP(object sender, System.EventArgs e)
		{
			if(this.ValidiereVornameHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_Kraefte_Helfer_Vorname, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_Kraefte_Helfer_Vorname, "Bitte geben Sie ein Helfervorname ein");
			}
		}

		private bool ValidiereNameHelfer()
		{
			return(this.txt_Kraefte_Helfer_Name.Text.Length > 0);
		}

		private void txt_Helfer_Name_Validated_ESP(object sender, System.EventArgs e)
		{
			if(this.ValidiereNameHelfer())
			{
				// Clear the error, if any, in the error provider.
				ep_Eingabe.SetError(this.txt_Kraefte_Helfer_Name, "");
			}
			else
			{
				// Set the error if the name is not valid.
				ep_Eingabe.SetError(this.txt_Kraefte_Helfer_Name, "Bitte geben Sie ein Helfername ein");
			}
		}

		#endregion

		#endregion

		#region Funktionalität
		public void VerarbeiteUnterknoten(TreeNode pin_trn_Wurzel, ContextMenu pin_ctx_Menue)
		{
			int _i_Index = pin_trn_Wurzel.ImageIndex;
			switch(_i_Index % 5)
			{
				case 0:
					//pin_trn_Wurzel.Tag = this.ctx_Einheiten;
					break;
				case 1:
					//pin_trn_Wurzel.Tag = this.ctx_Material;
					break;
				case 2:
					//pin_trn_Wurzel.Tag = this.ctx_Helfer;
					break;
				case 3:
					//pin_trn_Wurzel.Tag = this.ctx_Einsatzschwerpunkte;
					break;
				case 4:
					//pin_trn_Wurzel.Tag = this.ctx_Material;
					break;
			}
			TreeNodeCollection tnc_Knoten = pin_trn_Wurzel.Nodes;
			IEnumerator ie = tnc_Knoten.GetEnumerator();
			while(ie.MoveNext())
			{
				TreeNode trn_Aktuell = (TreeNode) ie.Current;
				VerarbeiteUnterknoten(trn_Aktuell, pin_ctx_Menue);
			}
		}

		#endregion

		#region Eventhandler

		private void btn_Modul_erstellen_Click(object sender, System.EventArgs e)
		{
			//TODO:
			//neues Menue aufmachen (aufpoppen), dort namen abfragen und dann in
			//in die Liste der bestehenden Module aufnehmen (this.cbx_Module_Modul)
			//
		}

		// Der folgende üble Hack kommt von Steini.
		// Er ermittelt für einen Treeview aus den aktuellen Mauskoordinaten den zugehörigen Node
		// Dies klappt egal ob rechte,linke oder mittlere Maustaste genutzt werden.
		private void trv_Einsatzmanager_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(this.bEditierungsModus && e.Clicks == 2)
				return;
			TreeNode trn_AusgewaehlterKnoten;
			trn_AusgewaehlterKnoten = trv_Einsatzmanager.GetNodeAt(e.X,e.Y);
			if ((trn_AusgewaehlterKnoten != null) && (trv_Einsatzmanager.SelectedNode != trn_AusgewaehlterKnoten))
			{
				trv_Einsatzmanager.SelectedNode = trn_AusgewaehlterKnoten;
			}
			//Beim doppelklick auf ein Item, dieses anzeigen
			if(e.Clicks == 2)	
			//if(e.Clicks == 2 && trn_AusgewaehlterKnoten.GetNodeCount() == 0)
			{
				string str_SystemTyp;
				Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trn_AusgewaehlterKnoten.Tag;
				if(tag.Type == null || tag.Eintrag == null)
					return;
				str_SystemTyp = tag.Type.ToString();
				int iIndex = str_SystemTyp.LastIndexOf('.');
				string str_Typ = str_SystemTyp.Substring(++iIndex);
				if(str_Typ.Equals("Cdv_Einsatzschwerpunkt"))
				{
					Cdv_Einsatzschwerpunkt esp = (Cdv_Einsatzschwerpunkt) tag.Eintrag;
					this.LadeESPDaten(esp);
					this.bEditierungsModus = true;
					return;
				}
			}
		}

		private void trv_Einsatzmanager_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			//Ermitteln des obersten Knotens.
			//Iteratives Aufsteigen im Baum, bis zur Wurzel.
			//Olley
			string str_Eltern = e.Node.Text;	
			TreeNode trn_ElternKnoten = e.Node.Parent;
			while(trn_ElternKnoten != null)
			{
				str_Eltern = trn_ElternKnoten.Text;
				trn_ElternKnoten = trn_ElternKnoten.Parent;
			}	
			if (e.Node.Tag is Cst_EK_TreeviewTag)
			{
				if ((e.Node.Tag as Cst_EK_TreeviewTag).Kontextmenue!=null)
				{
					trv_Einsatzmanager.ContextMenu=((e.Node.Tag as Cst_EK_TreeviewTag).Kontextmenue as ContextMenu);
				}
			}
			
		}

		private void usc_EK_Load(object sender, System.EventArgs e)
		{
//			this.trv_Einsatzmanager.Nodes.Clear();
//			this.trv_Einsatzmanager.Nodes.Add();
			

//			VerarbeiteUnterknoten(this.trv_Einsatzmanager.Nodes[0], this.ctx_Einsatzschwerpunkte);
//			VerarbeiteUnterknoten(this.trv_Einsatzmanager.Nodes[1], this.ctx_Einheiten);
//			VerarbeiteUnterknoten(this.trv_Einsatzmanager.Nodes[2], this.ctx_Material);
		}
		#region Drag'n Drop Implementation
		private void trv_Einsatzmanager_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
			TreeNode trn_Knoten = (TreeNode) e.Item;
			int _i_Index = trn_Knoten.Index;
			if(trn_Knoten.Nodes.Count == 0)
			{
				this._trn_QuellKnoten = trn_Knoten;
				this.DoDragDrop(_trn_QuellKnoten,DragDropEffects.Copy | DragDropEffects.Move);
			}
		}

		private void trv_Einsatzmanager_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if(e.Data.GetDataPresent("System.Windows.Forms.TreeNode",false))
			{
				this._trn_ZielKnoten = this.FindeKnoten(e.X, e.Y);
				//DnD Logik HIER implementieren
				this._trn_ZielKnoten = this._trn_QuellKnoten = null;
			}
		}

		protected TreeNode VerarbeiteWurzel(int pin_i_X, int pin_i_Y, TreeNode pin_trn_Knoten)
		{
			Point _pnt_Punkt = new Point(pin_i_X, pin_i_Y);
			_pnt_Punkt = this.trv_Einsatzmanager.PointToClient(_pnt_Punkt);
			if(pin_trn_Knoten.Bounds.Contains(_pnt_Punkt))
				return(pin_trn_Knoten);
			TreeNodeCollection tnc_KinderKnoten = pin_trn_Knoten.Nodes;
			IEnumerator inm_Iterator = tnc_KinderKnoten.GetEnumerator();
			while(inm_Iterator.MoveNext())
			{
				TreeNode _trn_AktuellerKnoten = (TreeNode) inm_Iterator.Current;
				if(_trn_AktuellerKnoten.Bounds.Contains(_pnt_Punkt))
					return(_trn_AktuellerKnoten);
				TreeNode trn_InnerKnoten = VerarbeiteWurzel(pin_i_X, pin_i_Y, _trn_AktuellerKnoten);
				if(trn_InnerKnoten != null)
					return(trn_InnerKnoten);
			}
			return(null);
		}

		protected TreeNode FindeKnoten(int pin_i_X, int pin_i_Y)
		{
			TreeNode _trn_Knoten = null;
			if((_trn_Knoten = this.VerarbeiteWurzel(pin_i_X, pin_i_Y, this.trv_Einsatzmanager.Nodes[0]))!= null)
				return(_trn_Knoten);
			if((_trn_Knoten = this.VerarbeiteWurzel(pin_i_X, pin_i_Y, this.trv_Einsatzmanager.Nodes[1])) != null)
				return(_trn_Knoten);
			if((_trn_Knoten = this.VerarbeiteWurzel(pin_i_X, pin_i_Y, this.trv_Einsatzmanager.Nodes[2])) != null)
				return(_trn_Knoten);
			return(_trn_Knoten);
		}

		private void trv_Einsatzmanager_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if(e.Data.GetDataPresent("System.Windows.Forms.TreeNode",false))
			{
				TreeNode _trn_Knoten = this.FindeKnoten(e.X, e.Y);
				this.trv_Einsatzmanager.SelectedNode = _trn_Knoten;
				_trn_Knoten.ExpandAll();
			}
		}

		private void trv_Einsatzmanager_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			e.Effect = System.Windows.Forms.DragDropEffects.Copy | DragDropEffects.Move;
		}

		private void trv_Einsatzmanager_DragLeave(object sender, System.EventArgs e)
		{
			
		}
		#endregion

		private void btn_Einsatzschwerpunkte_Verwerfen_Click(object sender, System.EventArgs e)
		{
			if (CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes)
			{
				EingebfelderLeeren_Einsatzschwerpunkt();
				gbx_Einsatzschwerpunkte_Auswahl.Enabled = true;			
				gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Enabled = false;
				this.ep_Eingabe.SetError(this.txt_Einsatzschwerpuntke_Bezeichnung, "");
				this.ep_Eingabe.SetError(this.txt_Einsatzschwerpunkte_Lage, "");
				this.ep_Eingabe.SetError(this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter, "");
				this.ep_Eingabe.SetError(this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet, "");
				this.bEditierungsModus = false;
			}
		}

		private void btn_Einsatzschwerpunkte_Speichern_Click(object sender, System.EventArgs e)
		{
			// Unterscheidung ob neuer Einsatzschwerpunkt oder schon vorhandener
			bool bNeuerESP = false;
			DialogResult _dr_speichern = new DialogResult();
			if (cmb_Einsatzschwerpunkte_BezeichnungAuswahl.Text == "<neuer Einsatzschwerpunkt>")
			{
				bNeuerESP = true;
				_dr_speichern = CPopUp.SpeichernOhneUeberschreiben();
			}
			else _dr_speichern = CPopUp.SpeichernMitUeberschreiben();
			
			if ( _dr_speichern == DialogResult.OK)
			{
				if(!this.EingabevalidierungESP())
					return;
				Cdv_Einsatzschwerpunkt esp = null;
				if(bNeuerESP)
					esp = new Cdv_Einsatzschwerpunkt();
				else
					esp = (Cdv_Einsatzschwerpunkt) this.cmb_Einsatzschwerpunkte_BezeichnungAuswahl.SelectedItem;
				esp.Bezeichnung = this.txt_Einsatzschwerpuntke_Bezeichnung.Text;
				esp.EinsatzleiterHelferID = ((Cdv_Helfer)this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.SelectedItem).ID;
				esp.Lage.Text = this.txt_Einsatzschwerpunkte_Lage.Text;
				esp.Lage.Autor = this._Cst_EK.Einstellung.Benutzer.Benutzername;
				esp.Prioritaet = Int32.Parse(this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet.Text);
//XYT				this._Cst_EK.SpeichereESP(esp, bNeuerESP);
				EingebfelderLeeren_Einsatzschwerpunkt();
				gbx_Einsatzschwerpunkte_Auswahl.Enabled = true;			
				gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Enabled = false;
				this.bEditierungsModus = false;
			}
		}

		// TODO 1. Daten vom gewählten Einsatzschwerpunkt laden inklusive Erkundungsdetails
		private void cmb_Einsatzschwerpunkte_BezeichnungAuswahl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			gbx_Einsatzschwerpunkte_Auswahl.Enabled = false;			
			gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Enabled = true;

			if ((cmb_Einsatzschwerpunkte_BezeichnungAuswahl.Text != "<neuer Einsatzschwerpunkt>")
				&& (this.cmb_Einsatzschwerpunkte_BezeichnungAuswahl.Text != ""))
			{
				txt_Einsatzschwerpuntke_Bezeichnung.Text = cmb_Einsatzschwerpunkte_BezeichnungAuswahl.Text;
				Cdv_Einsatzschwerpunkt esp = (Cdv_Einsatzschwerpunkt) this.cmb_Einsatzschwerpunkte_BezeichnungAuswahl.SelectedItem;
				this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet.Text = esp.Prioritaet.ToString();
				this.txt_Einsatzschwerpunkte_Lage.Text = esp.Lage.Text;
				this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.SelectedItem = this._Cst_EK.LeiterZuESP(esp);
			}
			this.bEditierungsModus = true;
		}
	
		private void EingebfelderLeeren_Einsatzschwerpunkt()
		{
			cmb_Einsatzschwerpunkte_BezeichnungAuswahl.SelectedItem = null;
			txt_Einsatzschwerpuntke_Bezeichnung.Text = "";
			txt_Einsatzschwerpunkte_Lage.Text = "";
			cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.SelectedItem = null;
			cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet.SelectedItem = null;
		}

		private void EingabefelderLeeren_Kraefte_Helfer()
		{
			this.txt_Kraefte_Helfer_Anschrift.Text = "";
			this.txt_Kraefte_Helfer_Erreichbarkeit.Text = "";
			this.txt_Kraefte_Helfer_Name.Text = "";
			this.txt_Kraefte_Helfer_OV.Text = "";
			this.txt_Kraefte_Helfer_Vorname.Text = "";
			this.txt_Kraefte_Helfer_Zusatz.Text = "";
			this.dtp_Kraefte_Helfer_Geburtsdatum.Text = "";
			this.cmb_Kraefte_Helfer_Position.SelectedItem = null;
			this.cmb_Kraefte_Helfer_Status.SelectedItem = null;
		}

		private void EingabefelderLeeren_Kraefte_Einheit()
		{
			this.txt_Kraefte_Einheit_Erreichbarkeit.Text = "";
			this.txt_Kraefte_Einheit_Funkrufname.Text = "";
			this.txt_Kraefte_Einheit_gst.Text = "";
			this.txt_Kraefte_Einheit_IstStaerke.Text = "";
			this.txt_Kraefte_Einheit_Name.Text = "";
			this.txt_Kraefte_Einheit_OV.Text = "";
			this.txt_Kraefte_Einheit_SollStaerke.Text = "";
		}

		private void EingabefelderLeeren_Kraefte_Kfz()
		{
			this.txt_Kraefte_Kfz_Funkrufname.Text = "";
			this.txt_Kraefte_Kfz_Kennzeichen.Text = "";
			this.txt_Kraefte_Kfz_KfzTyp.Text = "";
			this.txt_Kraefte_Kfz_Kommentar.Text = "";
		}

		// TODO: Reaktion auf geöffnete Datei
		private void btn_Kraefte_Importieren_Click(object sender, System.EventArgs e)
		{
			ofd_Kraefte_Importieren.ShowDialog();
		}

		private void ctx_Einheiten_Popup(object sender, System.EventArgs e)
		{
			
		}

		private void btn_Kraefte_Helfer_DatenSpeichern_Verwerfen_Click(object sender, System.EventArgs e)
		{
			if(CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes)
			{
				this.EingabefelderLeeren_Kraefte_Helfer();
				this.ep_Eingabe.SetError(this.txt_Kraefte_Helfer_Vorname, "");
				this.ep_Eingabe.SetError(this.txt_Kraefte_Helfer_Name, "");
				this.ep_Eingabe.SetError(this.cmb_Kraefte_Helfer_Status, "");
				this.ep_Eingabe.SetError(this.cmb_Kraefte_Helfer_Position, "");
				this.ep_Eingabe.SetError(this.dtp_Kraefte_Helfer_Geburtsdatum, "");
				this.ep_Eingabe.SetError(this.txt_Kraefte_Helfer_OV, "");
				this.ep_Eingabe.SetError(this.txt_Kraefte_Helfer_Erreichbarkeit, "");
				this.ep_Eingabe.SetError(this.txt_Kraefte_Helfer_Anschrift, "");
			}
		}

		private void btn_Kraefte_Helfer_DatenSpeichern_Speichern_Click(object sender, System.EventArgs e)
		{
			if(CPopUp.SpeichernOhneUeberschreiben() == DialogResult.OK)
			{
				if(!this.EingabevalidierungHelfer())
					return;
				Cdv_Helfer helfer = new Cdv_Helfer();
				helfer.Erreichbarkeit = this.txt_Kraefte_Helfer_Erreichbarkeit.Text;
				helfer.Personendaten.Name = this.txt_Kraefte_Helfer_Name.Text;
				helfer.Personendaten.Vorname = this.txt_Kraefte_Helfer_Vorname.Text;
				helfer.Personendaten.GebDatum = this.dtp_Kraefte_Helfer_Geburtsdatum.Value;
				string[] pszaLines = this.txt_Kraefte_Helfer_Anschrift.Lines;
				string[] pszaParsed = this.ParseAnschrift(pszaLines[0], pszaLines[1]);
				helfer.Personendaten.Anschrift.Ort = pszaParsed[3];
				helfer.Personendaten.Anschrift.PLZ = pszaParsed[2];
				helfer.Personendaten.Anschrift.Hausnummer = pszaParsed[1];
				helfer.Personendaten.Anschrift.Strasse = pszaParsed[0];
				helfer.Helferstatus = (Tdv_Helferstatus) this.cmb_Kraefte_Helfer_Status.SelectedIndex;
				helfer.Position = (Tdv_Position) this.cmb_Kraefte_Helfer_Position.SelectedIndex;
				helfer.Personendaten.ZusatzInfo = this.txt_Kraefte_Helfer_Zusatz.Text;
				if(helfer.Personendaten.ZusatzInfo.Length == 0)
					helfer.Personendaten.ZusatzInfo = "keine Zusatzinfos";
				this._Cst_EK.SpeichereHelfer(helfer);
				this.EingabefelderLeeren_Kraefte_Helfer();
			}
		}

		private void btn_Kraefte_Einheit_Importieren_Click(object sender, System.EventArgs e)
		{
			ofd_Kraefte_Importieren.ShowDialog();
		}

		private void btn_Kraefte_Einheit_DatenSpeichern_Verwerfen_Click(object sender, System.EventArgs e)
		{
			if(CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes)
			{
				this.EingabefelderLeeren_Kraefte_Einheit();
			}
		}

		private void btn_Kraefte_Einheit_DatenSpeichern_Speichern_Click(object sender, System.EventArgs e)
		{
			if(CPopUp.SpeichernOhneUeberschreiben() == DialogResult.OK)
			{
				this.EingabefelderLeeren_Kraefte_Einheit();
			}
		}

		private void btn_Kraefte_Kfz_Importieren_Click(object sender, System.EventArgs e)
		{
			ofd_Kraefte_Importieren.ShowDialog();
		}

		private void btn_Kraefte_Kfz_DatenSpeichern_Verwerfen_Click(object sender, System.EventArgs e)
		{
			if(CPopUp.ZuruecksetzenEingaben() == DialogResult.Yes)
			{
				this.EingabefelderLeeren_Kraefte_Kfz();
			}
		}

		private void btn__Kraefte_Kfz_DatenSpeichern_Speichern_Click(object sender, System.EventArgs e)
		{
			if(CPopUp.SpeichernOhneUeberschreiben() == DialogResult.OK)
			{
				this.EingabefelderLeeren_Kraefte_Kfz();
			}
		}


		#endregion

		#region Setzen der Rollenrechte
		//TODO: Alles
		//Test steht noch aus.
		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
		{///Info: F155,F150,F160 haben die gleichen 
			///		  Rechte und können gemeinsam durch entfernen der
			///		  tabpage_Module gesperrt werden. (alexG)
		}
		#endregion

		private void mI_ctx_Helfer_als_verpflegt_Kennzeichnen_Click(object sender, System.EventArgs e)
		{ //F160
			//Cpr_usc_EK
			//Hierbei soll ein Dialog geöffnet werden, in dem angegeben werden kann, 
			// ob der Helfer jetzt oder zu einem früheren Zeitpunkt Verpflegt wird
			// Bei bestätigung wird 
			// VerpflegeHelfer(Cdv_Helfer pin_helfer, Date pin_datum) auf STS aufgerufen
		}

		private void label4_Click(object sender, System.EventArgs e)
		{
		
		}

		private void ctx_Einsatzschwerpunkte_Popup(object sender, System.EventArgs e)
		{
		
		}

		private void ctx_abstrakt_Einsatzschwerpunkt_Popup(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem19_Click(object sender, System.EventArgs e)
		{
		
		}

		public void btn_Aktualisieren_Click(object sender, System.EventArgs e)
		{
			//MessageBox.Show("TEST!");
			trv_Einsatzmanager.Nodes.Clear();
			_Cst_EK.fuelletrv_Einsatzmanager();
			
		}

		public void Testme(string mytext)
		{
			MessageBox.Show(mytext);
			//this.btn_Aktualisieren_Click(btn_Aktualisieren,null);
		}

		private void mI_abstrakt_ESP_NeuenESPanlegen_Click(object sender, System.EventArgs e)
		{
			Cdv_Einsatzschwerpunkt esp = new Cdv_Einsatzschwerpunkt();
			this._Cst_EK.ErstelleESP(esp);
		}

		private void mI_abstrakt_Modul_NeuesModulAnlegen_Click(object sender, System.EventArgs e)
		{
		
		}

		private void mI_abstrakt_Modul_WurdeVerpflegt_Click(object sender, System.EventArgs e)
		{
		
		}

		private void mI_abstrakt_Modul_KraeftestatusSetzen_Click(object sender, System.EventArgs e)
		{
		
		}

		private void mI_abstrakt_Helfer_HelferAnlegen_Click(object sender, System.EventArgs e)
		{
		
		}

		private void mI_abstrakt_Helfer_WurdeVerpflegt_Click(object sender, System.EventArgs e)
		{
		
		}

		private void mI_abstrakt_Helfer_StatusSetzenAuf_Click(object sender, System.EventArgs e)
		{
		
		}

		private void mI_abstrakt_Fahrzeuge_NeuesKFZAnlegen_Click(object sender, System.EventArgs e)
		{
		
		}

		private void mI_abstrakt_Einheiten_NeueEinheitAnlegen_Click(object sender, System.EventArgs e)
		{
		
		}

		private void tmr_ESPTimer_Tick(object sender, System.EventArgs e)
		{
			if (this._Cst_EK._b_DatenGeaendert) 
			{
				this._Cst_EK._b_DatenGeaendert = false;
				btn_Aktualisieren_Click(this,null);
			}
		}

		private void LadeESPDaten(Cdv_Einsatzschwerpunkt pin_esp)
		{
			gbx_Einsatzschwerpunkte_Auswahl.Enabled = false;			
			gbx_Einsatzschwerpunkte_Einsatzschwerpunkt.Enabled = true;
			this.tabctrl_EK.SelectedTab = this.tabpage_Einsatzschwerpunkte;
			this.cmb_Einsatzschwerpunkte_BezeichnungAuswahl.SelectedItem = pin_esp;
			this.txt_Einsatzschwerpuntke_Bezeichnung.Text = pin_esp.Bezeichnung;
			this.txt_Einsatzschwerpunkte_Lage.Text = pin_esp.Lage.Text;
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet.Text = pin_esp.Prioritaet.ToString();
			this.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.SelectedItem = this._Cst_EK.LeiterZuESP(pin_esp);
		}

		private void txt_Einsatzschwerpuntke_Bezeichnung_Leave(object sender, System.EventArgs e)
		{
			this.txt_Bezeichnung_Validated_ESP(null,null);
		}

		private void txt_Einsatzschwerpunkte_Lage_Leave(object sender, System.EventArgs e)
		{
			this.txt_Lage_Validated_ESP(null, null);
		}

		private void cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Prioritaet_Leave(object sender, System.EventArgs e)
		{
			this.cmb_Prio_Validated_ESP(null, null);
		}

		private void cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter_Leave(object sender, System.EventArgs e)
		{
			this.cmb_Einsatzleiter_Validated_ESP(null, null);
		}

		private void txt_Kraefte_Helfer_Vorname_Leave(object sender, System.EventArgs e)
		{
			this.txt_Helfer_Vorname_Validated_ESP(null, null);
		}

		private void txt_Kraefte_Helfer_Name_Leave(object sender, System.EventArgs e)
		{
			this.txt_Helfer_Name_Validated_ESP(null, null);
		}

		private void cmb_Kraefte_Helfer_Position_Leave(object sender, System.EventArgs e)
		{
			this.cmb_Helfer_Position_Validated_ESP(null, null);
		}

		private void cmb_Kraefte_Helfer_Status_Leave(object sender, System.EventArgs e)
		{
			this.cmb_Helfer_Status_Validated_ESP(null, null);
		}

		private void dtp_Kraefte_Helfer_Geburtsdatum_Leave(object sender, System.EventArgs e)
		{
			this.dtp_Helfer_Geburtsdatum_Validated_ESP(null, null);
		}

		private void txt_Kraefte_Helfer_Anschrift_Leave(object sender, System.EventArgs e)
		{
			this.txt_Helfer_Anschrift_Validated_ESP(null, null);
		}

		private void txt_Kraefte_Helfer_OV_Leave(object sender, System.EventArgs e)
		{
			this.txt_Helfer_OV_Validated_ESP(null, null);
		}

		private void txt_Kraefte_Helfer_Erreichbarkeit_Leave(object sender, System.EventArgs e)
		{
			this.txt_Helfer_Erreichbarkeit_Validated_ESP(null, null);
		}

		#region Datagrid

		public DataColumn ErstellenEinerDataColumn(string pin_str_Name, string pin_str_Caption, string pin_str_Type) 
		{
			// Type der Spalte generieren
			System.Type type_meinType = Type.GetType(pin_str_Type);						
			if (type_meinType == null)
			{
				return null;
			}
			// Neue Spalte erstellen 
			DataColumn pout_dcol_Spalte = new DataColumn(pin_str_Name, type_meinType);
			pout_dcol_Spalte.ReadOnly = true; 
			pout_dcol_Spalte.ColumnName = pin_str_Caption;			
			
			return pout_dcol_Spalte;
		}

		// Tabelle für ein DataGrid erstellen
		public DataTable ErstellenEinerDataTable(string pin_str_Name, DataColumn[] pin_dcol_a_Spalten) 
		{			
			DataTable pout_dtable_meineTabelle = new DataTable(pin_str_Name);

			pout_dtable_meineTabelle.Columns.AddRange(pin_dcol_a_Spalten);		
			return pout_dtable_meineTabelle;
		}

		private void lbl_Kraefte_Helfer_NameVorname_Click(object sender, System.EventArgs e)
		{
		
		}
		#region Datagrid Personen (Einheiten)
		
		

		#endregion

		#endregion
	}
}