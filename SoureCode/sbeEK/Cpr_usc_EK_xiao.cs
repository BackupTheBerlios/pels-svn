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

	public class Cpr_usc_EK_xiao : System.Windows.Forms.UserControl
	{

		#region eigene Variablen
		private Cst_EK _Cst_EK;
		/// <summary>
		/// gibt an, ob bereits Eingaben geschehen sind
		/// </summary>
		protected bool _b_FelderModifiziert = false;


		#region Alle User- Controls Alle User- Controls
		private usc_Einheit _Einheit;
		private usc_Einsatzschwerpunkte _ESP;
		private usc_Helfer _Helfer;
		private usc_Kfz _Kfz;
		private usc_Module _Module;
		private usc_Ortsverband _OV;
		#endregion

		private TreeNode _trn_QuellKnoten = null;
		private TreeNode _trn_ZielKnoten = null;

		public ArrayList _TreeNodeReferenzen = new ArrayList();

		#endregion
		
		#region graphische Variablen

		public System.Windows.Forms.CheckBox cbx_Auftrag_IstUebermittelt;
		public System.Windows.Forms.TreeView trv_Einsatzmanager;
		private System.Windows.Forms.TabControl tabctrl_EK;
		private System.Windows.Forms.TabPage tabPage_Helfer;
		private System.Windows.Forms.TabPage tabpage_Einheit;
		private System.Windows.Forms.TabPage tabPage_Kfz;
		public System.Windows.Forms.TabPage tabPage_Einsatzschwerpunkt;
		private System.Windows.Forms.GroupBox gbx_Import;
		private System.Windows.Forms.TabPage tabPage_Hauptseite;
		public System.Windows.Forms.ContextMenu ctx_Helfer;
		private System.Windows.Forms.MenuItem mi_ueberschrift_Helfer;
		private System.Windows.Forms.MenuItem menuItem6;
		public System.Windows.Forms.ContextMenu ctx_Material;
		private System.Windows.Forms.MenuItem mi_ueberschrift_Material;
		private System.Windows.Forms.MenuItem menuItem8;
		public System.Windows.Forms.ContextMenu ctx_Module;
		private System.Windows.Forms.MenuItem mi_ueberschrift_Module;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.OpenFileDialog ofd_Kraefte_Importieren;
		public System.Windows.Forms.ContextMenu ctx_abstrakt_Fahrzeuge;
		private System.Windows.Forms.MenuItem mi_ueberschrift_abstrakt_Fahrzeuge;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem mI_abstrakt_Fahrzeuge_NeuesKFZAnlegen;
		public System.Windows.Forms.ContextMenu ctx_abstrakt_Einsatzschwerpunkte;
		private System.Windows.Forms.MenuItem mi_ueberschrift_abstrakt_einsatzschwerpunkte;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem mI_abstrakt_ESP_NeuenESPanlegen;
		public System.Windows.Forms.ContextMenu ctx_Einsatzschwerpunkte;
		private System.Windows.Forms.MenuItem mi_ueberschrift_Einsatzschwerpunkte;
		private System.Windows.Forms.MenuItem menuItem3;
		public System.Windows.Forms.ContextMenu ctx_abstrakt_Module;
		private System.Windows.Forms.MenuItem mi_ueberschrift_abstrakt_Module;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem mI_abstrakt_Modul_NeuesModulAnlegen;
		private System.Windows.Forms.MenuItem mI_abstrakt_Modul_WurdeVerpflegt;
		private System.Windows.Forms.MenuItem mI_abstrakt_Modul_KraeftestatusSetzen;
		private System.Windows.Forms.MenuItem menuItem41;
		public System.Windows.Forms.ContextMenu ctx_Fahrzeuge;
		private System.Windows.Forms.MenuItem mi_ueberschrift_Fahrzeuge;
		private System.Windows.Forms.MenuItem menuItem12;
		public System.Windows.Forms.ContextMenu ctx_abstrakt_Einheiten;
		private System.Windows.Forms.MenuItem mi_ueberschrift_abstrakt_Einheiten;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.MenuItem mI_abstrakt_Einheiten_NeueEinheitAnlegen;
		public System.Windows.Forms.ContextMenu ctx_abstrakt_Helfer;
		private System.Windows.Forms.MenuItem mi_ueberschrift_abstrakt_Helfer;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem mI_abstrakt_Helfer_HelferAnlegen;
		public System.Windows.Forms.ContextMenu ctx_abstrakt_Material;
		private System.Windows.Forms.MenuItem mi_ueberschrift_abstrakt_Material;
		private System.Windows.Forms.MenuItem menuItem37;
		public System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.ErrorProvider ep_Eingabe;
		public System.Windows.Forms.ContextMenu contextMenu2;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem13;
		public System.Windows.Forms.ContextMenu ctx_Einheiten;
		private System.Windows.Forms.MenuItem mi_ueberschrift_Einheit;
		private System.Windows.Forms.MenuItem menuItem20;
		private System.Windows.Forms.TabPage tabPage_Modul;
		private System.Windows.Forms.ImageList iml_TreeViewBilderListe;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cmb_Importliste;
		private System.Windows.Forms.Button btn_Import;
		private System.Windows.Forms.TabPage tabPage_OV;
		public System.Windows.Forms.Button btn_Aktualisieren;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.MenuItem mI_abstrakt_Einheiten_NeueEinheitAnlegen_edit;
		private System.Windows.Forms.MenuItem mI_abstrakt_Einheiten_NeueEinheitAnlegen_creatempty;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem mI_abstrakt_Helfer_Helfer_anlegen_1;
		private System.Windows.Forms.MenuItem mI_abstrakt_Helfer_Helfer_anlegen_5;
		private System.Windows.Forms.MenuItem mI_abstrakt_Helfer_Helfer_anlegen_10;
		private System.Windows.Forms.HelpProvider pelsHelp;
		
		private System.ComponentModel.IContainer components;

		#endregion
	
		#region Konstruktor & Destruktor
		/// <summary>
		/// Konstruktor
		/// </summary>
		public Cpr_usc_EK_xiao(Cst_EK pin_stEK)
		{
			this._Cst_EK = pin_stEK;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.InitAlleSTE();
			_b_FelderModifiziert = false;
			this.FuelleEinsatzmanager();
			// Hilfe festlegen
			SetzeHilfe();

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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Cpr_usc_EK_xiao));
			this.cbx_Auftrag_IstUebermittelt = new System.Windows.Forms.CheckBox();
			this.trv_Einsatzmanager = new System.Windows.Forms.TreeView();
			this.iml_TreeViewBilderListe = new System.Windows.Forms.ImageList(this.components);
			this.tabctrl_EK = new System.Windows.Forms.TabControl();
			this.tabPage_Hauptseite = new System.Windows.Forms.TabPage();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btn_Import = new System.Windows.Forms.Button();
			this.gbx_Import = new System.Windows.Forms.GroupBox();
			this.cmb_Importliste = new System.Windows.Forms.ComboBox();
			this.btn_Aktualisieren = new System.Windows.Forms.Button();
			this.tabPage_Helfer = new System.Windows.Forms.TabPage();
			this.tabpage_Einheit = new System.Windows.Forms.TabPage();
			this.tabPage_Kfz = new System.Windows.Forms.TabPage();
			this.tabPage_Einsatzschwerpunkt = new System.Windows.Forms.TabPage();
			this.tabPage_Modul = new System.Windows.Forms.TabPage();
			this.tabPage_OV = new System.Windows.Forms.TabPage();
			this.ctx_Helfer = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_Helfer = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.ctx_Material = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_Material = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.ctx_Module = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_Module = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.ofd_Kraefte_Importieren = new System.Windows.Forms.OpenFileDialog();
			this.ctx_abstrakt_Fahrzeuge = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_abstrakt_Fahrzeuge = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Fahrzeuge_NeuesKFZAnlegen = new System.Windows.Forms.MenuItem();
			this.ctx_abstrakt_Einsatzschwerpunkte = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_abstrakt_einsatzschwerpunkte = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_ESP_NeuenESPanlegen = new System.Windows.Forms.MenuItem();
			this.ctx_Einsatzschwerpunkte = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_Einsatzschwerpunkte = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.ctx_abstrakt_Module = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_abstrakt_Module = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Modul_NeuesModulAnlegen = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Modul_WurdeVerpflegt = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Modul_KraeftestatusSetzen = new System.Windows.Forms.MenuItem();
			this.menuItem41 = new System.Windows.Forms.MenuItem();
			this.ctx_Fahrzeuge = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_Fahrzeuge = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.ctx_abstrakt_Einheiten = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_abstrakt_Einheiten = new System.Windows.Forms.MenuItem();
			this.menuItem18 = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Einheiten_NeueEinheitAnlegen = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Einheiten_NeueEinheitAnlegen_edit = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Einheiten_NeueEinheitAnlegen_creatempty = new System.Windows.Forms.MenuItem();
			this.ctx_abstrakt_Helfer = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_abstrakt_Helfer = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Helfer_HelferAnlegen = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Helfer_Helfer_anlegen_1 = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Helfer_Helfer_anlegen_5 = new System.Windows.Forms.MenuItem();
			this.mI_abstrakt_Helfer_Helfer_anlegen_10 = new System.Windows.Forms.MenuItem();
			this.ctx_abstrakt_Material = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_abstrakt_Material = new System.Windows.Forms.MenuItem();
			this.menuItem37 = new System.Windows.Forms.MenuItem();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.ep_Eingabe = new System.Windows.Forms.ErrorProvider();
			this.contextMenu2 = new System.Windows.Forms.ContextMenu();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.ctx_Einheiten = new System.Windows.Forms.ContextMenu();
			this.mi_ueberschrift_Einheit = new System.Windows.Forms.MenuItem();
			this.menuItem20 = new System.Windows.Forms.MenuItem();
			this.pelsHelp = new System.Windows.Forms.HelpProvider();
			this.tabctrl_EK.SuspendLayout();
			this.tabPage_Hauptseite.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.gbx_Import.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbx_Auftrag_IstUebermittelt
			// 
			this.cbx_Auftrag_IstUebermittelt.Location = new System.Drawing.Point(0, 0);
			this.cbx_Auftrag_IstUebermittelt.Name = "cbx_Auftrag_IstUebermittelt";
			this.cbx_Auftrag_IstUebermittelt.TabIndex = 0;
			// 
			// trv_Einsatzmanager
			// 
			this.trv_Einsatzmanager.AllowDrop = true;
			this.trv_Einsatzmanager.HotTracking = true;
			this.trv_Einsatzmanager.ImageList = this.iml_TreeViewBilderListe;
			this.trv_Einsatzmanager.ItemHeight = 14;
			this.trv_Einsatzmanager.Location = new System.Drawing.Point(5, 10);
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
			this.trv_Einsatzmanager.Size = new System.Drawing.Size(440, 460);
			this.trv_Einsatzmanager.TabIndex = 20;
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
			// tabctrl_EK
			// 
			this.tabctrl_EK.Controls.Add(this.tabPage_Hauptseite);
			this.tabctrl_EK.Controls.Add(this.tabPage_Helfer);
			this.tabctrl_EK.Controls.Add(this.tabpage_Einheit);
			this.tabctrl_EK.Controls.Add(this.tabPage_Kfz);
			this.tabctrl_EK.Controls.Add(this.tabPage_Einsatzschwerpunkt);
			this.tabctrl_EK.Controls.Add(this.tabPage_Modul);
			this.tabctrl_EK.Controls.Add(this.tabPage_OV);
			this.tabctrl_EK.Location = new System.Drawing.Point(5, 5);
			this.tabctrl_EK.Name = "tabctrl_EK";
			this.tabctrl_EK.SelectedIndex = 0;
			this.tabctrl_EK.Size = new System.Drawing.Size(637, 499);
			this.tabctrl_EK.TabIndex = 21;
			// 
			// tabPage_Hauptseite
			// 
			this.tabPage_Hauptseite.Controls.Add(this.button1);
			this.tabPage_Hauptseite.Controls.Add(this.groupBox1);
			this.tabPage_Hauptseite.Controls.Add(this.btn_Aktualisieren);
			this.tabPage_Hauptseite.Controls.Add(this.trv_Einsatzmanager);
			this.tabPage_Hauptseite.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Hauptseite.Name = "tabPage_Hauptseite";
			this.tabPage_Hauptseite.Size = new System.Drawing.Size(629, 473);
			this.tabPage_Hauptseite.TabIndex = 0;
			this.tabPage_Hauptseite.Text = "Einsatzmanager";
			this.tabPage_Hauptseite.Click += new System.EventHandler(this.tabPage_Hauptseite_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(450, 55);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(115, 25);
			this.button1.TabIndex = 25;
			this.button1.Text = "button1";
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.groupBox1.Controls.Add(this.btn_Import);
			this.groupBox1.Controls.Add(this.gbx_Import);
			this.groupBox1.Enabled = false;
			this.groupBox1.Location = new System.Drawing.Point(450, 345);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(175, 115);
			this.groupBox1.TabIndex = 24;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Kräfte Importieren";
			// 
			// btn_Import
			// 
			this.btn_Import.Location = new System.Drawing.Point(90, 80);
			this.btn_Import.Name = "btn_Import";
			this.btn_Import.Size = new System.Drawing.Size(80, 25);
			this.btn_Import.TabIndex = 24;
			this.btn_Import.Text = "Importieren";
			// 
			// gbx_Import
			// 
			this.gbx_Import.Controls.Add(this.cmb_Importliste);
			this.gbx_Import.Location = new System.Drawing.Point(10, 20);
			this.gbx_Import.Name = "gbx_Import";
			this.gbx_Import.Size = new System.Drawing.Size(160, 55);
			this.gbx_Import.TabIndex = 23;
			this.gbx_Import.TabStop = false;
			this.gbx_Import.Text = "Auswählen";
			// 
			// cmb_Importliste
			// 
			this.cmb_Importliste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_Importliste.Location = new System.Drawing.Point(5, 20);
			this.cmb_Importliste.Name = "cmb_Importliste";
			this.cmb_Importliste.Size = new System.Drawing.Size(150, 21);
			this.cmb_Importliste.TabIndex = 22;
			// 
			// btn_Aktualisieren
			// 
			this.btn_Aktualisieren.Location = new System.Drawing.Point(450, 10);
			this.btn_Aktualisieren.Name = "btn_Aktualisieren";
			this.btn_Aktualisieren.Size = new System.Drawing.Size(115, 30);
			this.btn_Aktualisieren.TabIndex = 21;
			this.btn_Aktualisieren.Text = "<< Aktualisieren";
			this.btn_Aktualisieren.Click += new System.EventHandler(this.btn_Aktualisieren_Click);
			// 
			// tabPage_Helfer
			// 
			this.tabPage_Helfer.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Helfer.Name = "tabPage_Helfer";
			this.tabPage_Helfer.Size = new System.Drawing.Size(629, 473);
			this.tabPage_Helfer.TabIndex = 1;
			this.tabPage_Helfer.Text = "Helfer";
			// 
			// tabpage_Einheit
			// 
			this.tabpage_Einheit.Location = new System.Drawing.Point(4, 22);
			this.tabpage_Einheit.Name = "tabpage_Einheit";
			this.tabpage_Einheit.Size = new System.Drawing.Size(629, 473);
			this.tabpage_Einheit.TabIndex = 2;
			this.tabpage_Einheit.Text = "Einheit";
			// 
			// tabPage_Kfz
			// 
			this.tabPage_Kfz.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Kfz.Name = "tabPage_Kfz";
			this.tabPage_Kfz.Size = new System.Drawing.Size(629, 473);
			this.tabPage_Kfz.TabIndex = 3;
			this.tabPage_Kfz.Text = "KFZ";
			// 
			// tabPage_Einsatzschwerpunkt
			// 
			this.tabPage_Einsatzschwerpunkt.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Einsatzschwerpunkt.Name = "tabPage_Einsatzschwerpunkt";
			this.tabPage_Einsatzschwerpunkt.Size = new System.Drawing.Size(629, 473);
			this.tabPage_Einsatzschwerpunkt.TabIndex = 4;
			this.tabPage_Einsatzschwerpunkt.Text = "Einsatzschwerpunkt";
			// 
			// tabPage_Modul
			// 
			this.tabPage_Modul.Location = new System.Drawing.Point(4, 22);
			this.tabPage_Modul.Name = "tabPage_Modul";
			this.tabPage_Modul.Size = new System.Drawing.Size(629, 473);
			this.tabPage_Modul.TabIndex = 5;
			this.tabPage_Modul.Text = "Modul";
			// 
			// tabPage_OV
			// 
			this.tabPage_OV.Location = new System.Drawing.Point(4, 22);
			this.tabPage_OV.Name = "tabPage_OV";
			this.tabPage_OV.Size = new System.Drawing.Size(629, 473);
			this.tabPage_OV.TabIndex = 6;
			this.tabPage_OV.Text = "Ortsverband";
			// 
			// ctx_Helfer
			// 
			this.ctx_Helfer.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.mi_ueberschrift_Helfer,
																					   this.menuItem6,
																					   this.menuItem15});
			this.ctx_Helfer.Popup += new System.EventHandler(this.ctx_Helfer_Popup);
			// 
			// mi_ueberschrift_Helfer
			// 
			this.mi_ueberschrift_Helfer.Enabled = false;
			this.mi_ueberschrift_Helfer.Index = 0;
			this.mi_ueberschrift_Helfer.Text = "Helfer";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 1;
			this.menuItem6.Text = "-";
			// 
			// menuItem15
			// 
			this.menuItem15.Index = 2;
			this.menuItem15.Text = "wurde soeben verpflegt";
			// 
			// ctx_Material
			// 
			this.ctx_Material.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mi_ueberschrift_Material,
																						 this.menuItem8});
			// 
			// mi_ueberschrift_Material
			// 
			this.mi_ueberschrift_Material.Enabled = false;
			this.mi_ueberschrift_Material.Index = 0;
			this.mi_ueberschrift_Material.Text = "Material";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 1;
			this.menuItem8.Text = "-";
			// 
			// ctx_Module
			// 
			this.ctx_Module.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.mi_ueberschrift_Module,
																					   this.menuItem16});
			// 
			// mi_ueberschrift_Module
			// 
			this.mi_ueberschrift_Module.Enabled = false;
			this.mi_ueberschrift_Module.Index = 0;
			this.mi_ueberschrift_Module.Text = "Modul";
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 1;
			this.menuItem16.Text = "-";
			// 
			// ofd_Kraefte_Importieren
			// 
			this.ofd_Kraefte_Importieren.Filter = "pELS Dateien| *.els|Alle Dateien|*.*";
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
			// ctx_abstrakt_Einsatzschwerpunkte
			// 
			this.ctx_abstrakt_Einsatzschwerpunkte.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																											 this.mi_ueberschrift_abstrakt_einsatzschwerpunkte,
																											 this.menuItem2,
																											 this.mI_abstrakt_ESP_NeuenESPanlegen});
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
			this.mI_abstrakt_ESP_NeuenESPanlegen.Click += new System.EventHandler(this.mI_abstrakt_ESP_NeuenESPanlegen_Click_1);
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
			this.mI_abstrakt_Modul_NeuesModulAnlegen.Click += new System.EventHandler(this.mI_abstrakt_Modul_NeuesModulAnlegen_Click_1);
			// 
			// mI_abstrakt_Modul_WurdeVerpflegt
			// 
			this.mI_abstrakt_Modul_WurdeVerpflegt.Index = 3;
			this.mI_abstrakt_Modul_WurdeVerpflegt.Text = "wurde verpflegt (alle Helfer)";
			this.mI_abstrakt_Modul_WurdeVerpflegt.Click += new System.EventHandler(this.mI_abstrakt_Modul_WurdeVerpflegt_Click);
			// 
			// mI_abstrakt_Modul_KraeftestatusSetzen
			// 
			this.mI_abstrakt_Modul_KraeftestatusSetzen.Enabled = false;
			this.mI_abstrakt_Modul_KraeftestatusSetzen.Index = 4;
			this.mI_abstrakt_Modul_KraeftestatusSetzen.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																												  this.menuItem41});
			this.mI_abstrakt_Modul_KraeftestatusSetzen.Text = "Kräftestaus setzen (alle Kräfte)";
			// 
			// menuItem41
			// 
			this.menuItem41.Index = 0;
			this.menuItem41.Text = "TDV_Kräftestatus....";
			// 
			// ctx_Fahrzeuge
			// 
			this.ctx_Fahrzeuge.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.mi_ueberschrift_Fahrzeuge,
																						  this.menuItem12});
			// 
			// mi_ueberschrift_Fahrzeuge
			// 
			this.mi_ueberschrift_Fahrzeuge.Enabled = false;
			this.mi_ueberschrift_Fahrzeuge.Index = 0;
			this.mi_ueberschrift_Fahrzeuge.Text = "Fahrzeug";
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 1;
			this.menuItem12.Text = "-";
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
			this.mI_abstrakt_Einheiten_NeueEinheitAnlegen.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																													 this.mI_abstrakt_Einheiten_NeueEinheitAnlegen_edit,
																													 this.mI_abstrakt_Einheiten_NeueEinheitAnlegen_creatempty});
			this.mI_abstrakt_Einheiten_NeueEinheitAnlegen.Text = "Neue Einheit anlegen";
			// 
			// mI_abstrakt_Einheiten_NeueEinheitAnlegen_edit
			// 
			this.mI_abstrakt_Einheiten_NeueEinheitAnlegen_edit.Index = 0;
			this.mI_abstrakt_Einheiten_NeueEinheitAnlegen_edit.Text = "und editieren";
			this.mI_abstrakt_Einheiten_NeueEinheitAnlegen_edit.Click += new System.EventHandler(this.mI_abstrakt_Einheiten_NeueEinheitAnlegen_edit_Click);
			// 
			// mI_abstrakt_Einheiten_NeueEinheitAnlegen_creatempty
			// 
			this.mI_abstrakt_Einheiten_NeueEinheitAnlegen_creatempty.Index = 1;
			this.mI_abstrakt_Einheiten_NeueEinheitAnlegen_creatempty.Text = "mit EF und stellv.EF";
			this.mI_abstrakt_Einheiten_NeueEinheitAnlegen_creatempty.Click += new System.EventHandler(this.mI_abstrakt_Einheiten_NeueEinheitAnlegen_creatempty_Click);
			// 
			// ctx_abstrakt_Helfer
			// 
			this.ctx_abstrakt_Helfer.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																								this.mi_ueberschrift_abstrakt_Helfer,
																								this.menuItem4,
																								this.mI_abstrakt_Helfer_HelferAnlegen});
			// 
			// mi_ueberschrift_abstrakt_Helfer
			// 
			this.mi_ueberschrift_abstrakt_Helfer.Enabled = false;
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
																											 this.mI_abstrakt_Helfer_Helfer_anlegen_1,
																											 this.mI_abstrakt_Helfer_Helfer_anlegen_5,
																											 this.mI_abstrakt_Helfer_Helfer_anlegen_10});
			this.mI_abstrakt_Helfer_HelferAnlegen.Text = "Helfer anlegen";
			// 
			// mI_abstrakt_Helfer_Helfer_anlegen_1
			// 
			this.mI_abstrakt_Helfer_Helfer_anlegen_1.Index = 0;
			this.mI_abstrakt_Helfer_Helfer_anlegen_1.Text = "1";
			this.mI_abstrakt_Helfer_Helfer_anlegen_1.Click += new System.EventHandler(this.mI_abstrakt_Helfer_Helfer_anlegen_1_Click);
			// 
			// mI_abstrakt_Helfer_Helfer_anlegen_5
			// 
			this.mI_abstrakt_Helfer_Helfer_anlegen_5.Index = 1;
			this.mI_abstrakt_Helfer_Helfer_anlegen_5.Text = "5";
			// 
			// mI_abstrakt_Helfer_Helfer_anlegen_10
			// 
			this.mI_abstrakt_Helfer_Helfer_anlegen_10.Index = 2;
			this.mI_abstrakt_Helfer_Helfer_anlegen_10.Text = "10";
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
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem5,
																						 this.menuItem7});
			// 
			// menuItem1
			// 
			this.menuItem1.Enabled = false;
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Einheiten";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.Text = "-";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 2;
			this.menuItem7.Text = "Neue Einheit anlegen";
			// 
			// ep_Eingabe
			// 
			this.ep_Eingabe.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.ep_Eingabe.ContainerControl = this;
			// 
			// contextMenu2
			// 
			this.contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem9,
																						 this.menuItem11,
																						 this.menuItem13});
			// 
			// menuItem9
			// 
			this.menuItem9.Enabled = false;
			this.menuItem9.Index = 0;
			this.menuItem9.Text = "Einheiten";
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 1;
			this.menuItem11.Text = "-";
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 2;
			this.menuItem13.Text = "Neue Einheit anlegen";
			// 
			// ctx_Einheiten
			// 
			this.ctx_Einheiten.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.mi_ueberschrift_Einheit,
																						  this.menuItem20});
			// 
			// mi_ueberschrift_Einheit
			// 
			this.mi_ueberschrift_Einheit.Enabled = false;
			this.mi_ueberschrift_Einheit.Index = 0;
			this.mi_ueberschrift_Einheit.Text = "Einheit";
			// 
			// menuItem20
			// 
			this.menuItem20.Index = 1;
			this.menuItem20.Text = "-";
			// 
			// Cpr_usc_EK_xiao
			// 
			this.Controls.Add(this.tabctrl_EK);
			this.Location = new System.Drawing.Point(6, 21);
			this.Name = "Cpr_usc_EK_xiao";
			this.Size = new System.Drawing.Size(650, 530);
			this.tabctrl_EK.ResumeLayout(false);
			this.tabPage_Hauptseite.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.gbx_Import.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		#endregion
		
		#region Setze- Methode
		// Initialisiere und lade alle User-Controls. 
		private void InitAlleSTE()
		{
			// Lade alle SBEs 
			this._ESP = new usc_Einsatzschwerpunkte(this._Cst_EK);
			this._Einheit = new usc_Einheit(this._Cst_EK);
			this._Helfer = new usc_Helfer(this._Cst_EK);
			this._Kfz = new usc_Kfz(this._Cst_EK);
			this._Module = new usc_Module(this._Cst_EK);
			this._OV = new usc_Ortsverband(this._Cst_EK);

			this.tabpage_Einheit.Controls.Add(this._Einheit);
			this.tabPage_Einsatzschwerpunkt.Controls.Add(this._ESP);
			this.tabPage_Helfer.Controls.Add(this._Helfer);
			this.tabPage_Kfz.Controls.Add(this._Kfz);
			this.tabPage_Modul.Controls.Add(this._Module);
			this.tabPage_OV.Controls.Add(this._OV);
		}
		private void SetzeImportliste()
		{
			this.cmb_Importliste.Items.Add("Helfer");
			this.cmb_Importliste.Items.Add("Einheit");
			this.cmb_Importliste.Items.Add("KFZ");
			this.cmb_Importliste.Items.Add("Module");
			this.cmb_Importliste.SelectedIndex = 0;
		}



		#endregion

		#region treeview
		/// <summary>
		/// Füllt die Einheiten mit Helfer, KFZ und Material der Einheit
		/// </summary>
		/// <param name="myeinheit">die darzustellende Einheit</param>
		/// <returns>Gibt einen Oberknoten zurück, der die Strukturieren Informationen enhält.</returns>
		private TreeNode FülleEinheitMitDaten(Cdv_Einheit pin_myeinheit)
		{

			TreeNode trn_Einheit = new TreeNode();
			TreeNode trn_Helfer = new TreeNode();
			TreeNode trn_KFZ = new TreeNode();
			TreeNode trn_Material = new TreeNode();
			
			#region Lade Helfer zu Einheit
			trn_Helfer.Text="Helfer";
			trn_Helfer.SelectedImageIndex = 18;
			trn_Helfer.ImageIndex = 18;
			if (pin_myeinheit.HelferIDMenge!=null)
			{
				// Durlaufen aller HelferIDs der Einheit
				// Lade alle Helfer mit diesen IDs in Knoten, 
				// Füge sie in Treeview ein
				foreach (int myHelferID in pin_myeinheit.HelferIDMenge)
				{
					TreeNode trn_HelferUnterknoten = new TreeNode();
					trn_HelferUnterknoten.SelectedImageIndex = 18;
					trn_HelferUnterknoten.ImageIndex = 18;
					Cdv_Helfer myHelfer=new Cdv_Helfer();
					myHelfer= this._Cst_EK.HoleHelfer(myHelferID);
					trn_HelferUnterknoten.Text=myHelfer.Personendaten.Name+","+myHelfer.Personendaten.Vorname;
					trn_HelferUnterknoten.Tag=new Cst_EK_TreeviewTag();
					(trn_HelferUnterknoten.Tag as Cst_EK_TreeviewTag).Eintrag=myHelfer;		
					this._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(myHelfer.ID,trn_HelferUnterknoten));
					(trn_HelferUnterknoten.Tag as Cst_EK_TreeviewTag).Kontextmenue= ctx_Helfer;
					(trn_HelferUnterknoten.Tag as Cst_EK_TreeviewTag).Type=myHelfer.GetType();
					trn_Helfer.Nodes.Add(trn_HelferUnterknoten);
				}
			}
			trn_Einheit.Nodes.Add(trn_Helfer);
			#endregion

			#region Lade KFZ zur Einheit
			// TODO: Den Code wieder zum Vorschein zu bringen. Den habe ich rauskommentiert, da kfz auf der DVS nicht geladen
			//			werden kann.


			trn_KFZ.Text="KFZ";
			trn_KFZ.SelectedImageIndex = 17;
			trn_KFZ.ImageIndex = 17;
			trn_KFZ.Tag= new Cst_EK_TreeviewTag();
			(trn_KFZ.Tag as Cst_EK_TreeviewTag).Kontextmenue = ctx_abstrakt_Fahrzeuge;

			if (pin_myeinheit.KfzKraefteIDMenge!=null)
			{
				foreach (int myKFZID in pin_myeinheit.KfzKraefteIDMenge)
				{
					TreeNode trn_myKfz = new TreeNode();
					trn_myKfz.SelectedImageIndex = 17;
					trn_myKfz.ImageIndex = 17;
					Cdv_KFZ myKfz=new Cdv_KFZ();
					myKfz=this._Cst_EK.HoleKfz(myKFZID);
					trn_myKfz.Text=myKfz.KfzTyp+" "+myKfz.Kennzeichen;
					trn_myKfz.Tag=new Cst_EK_TreeviewTag();
					(trn_myKfz.Tag as Cst_EK_TreeviewTag).Eintrag=myKfz;
					this._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(myKfz.ID,trn_myKfz));
					(trn_myKfz.Tag as Cst_EK_TreeviewTag).Type=myKfz.GetType();
					(trn_myKfz.Tag as Cst_EK_TreeviewTag).Kontextmenue=ctx_Fahrzeuge;
					trn_KFZ.Nodes.Add(trn_myKfz);
				}
			}
			trn_Einheit.Nodes.Add(trn_KFZ);
			#endregion

			#region Lade Material zu Einheit
			trn_Material.Text="Material";
			trn_Material.SelectedImageIndex = 20;
			trn_Material.ImageIndex = 20;
			Cdv_Material[] materialMenge = this._Cst_EK.HoleAlleMaterialZuEinheit(pin_myeinheit.ID);
			if(materialMenge != null)
			{
				IEnumerator ie = materialMenge.GetEnumerator();
				while(ie.MoveNext())
				{
					Cdv_Material myMaterial = (Cdv_Material) ie.Current;
					TreeNode trn_myMaterial = new TreeNode();
					trn_myMaterial.Text=myMaterial.Menge.ToString()+" "+myMaterial.Bezeichnung+" "+myMaterial.Art;
					trn_myMaterial.Tag=new Cst_EK_TreeviewTag();
					(trn_myMaterial.Tag as Cst_EK_TreeviewTag).Eintrag=myMaterial;	
					this._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(myMaterial.ID,trn_myMaterial));
					(trn_myMaterial.Tag as Cst_EK_TreeviewTag).Type=myMaterial.GetType();
					(trn_myMaterial.Tag as Cst_EK_TreeviewTag).Kontextmenue=this.ctx_Material;
					trn_Material.Nodes.Add(trn_myMaterial);
				}
			}
			trn_Einheit.Nodes.Add(trn_Material);
			#endregion

			return trn_Einheit;
		}

		/// <summary>
		/// Lädt alle KFZ zu einem Modul und fügt diese unterhalb des Rückgabeknotens hinzu.
		/// </summary>
		/// <param name="ModulID">Gültig ModulID</param>
		/// <returns></returns>
		private TreeNode KFZZuModul(int ModulID)
		{
			TreeNode trn_KFZ = new TreeNode();
			trn_KFZ.Tag=new Cst_EK_TreeviewTag();
			(trn_KFZ.Tag as Cst_EK_TreeviewTag).Kontextmenue=ctx_abstrakt_Fahrzeuge;
			foreach (Cdv_KFZ myKFZ in this._Cst_EK.AlleKFZ)
			{
				if (myKFZ.ModulID==ModulID)
				{
					TreeNode trn_myKFZ = new TreeNode();
					trn_myKFZ.SelectedImageIndex = 17;
					trn_myKFZ.ImageIndex = 17;
					trn_myKFZ.Text=myKFZ.KfzTyp+' '+myKFZ.Kennzeichen;
					trn_myKFZ.Tag= new Cst_EK_TreeviewTag();
					(trn_myKFZ.Tag as Cst_EK_TreeviewTag).Eintrag=myKFZ;
					this._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(myKFZ.ID,trn_myKFZ));
					(trn_myKFZ.Tag as Cst_EK_TreeviewTag).Type=myKFZ.GetType();
					(trn_myKFZ.Tag as Cst_EK_TreeviewTag).Kontextmenue=ctx_Fahrzeuge;
					trn_KFZ.Nodes.Add(trn_myKFZ);
				}
			}
			return (trn_KFZ);
		}


		/// <summary>
		/// Lädt die Einheiten zu einem Modul.. und führt für jede einzelne Einheit FülleEinheit mit Daten aus.
		/// Fügt diese unter den Rückgabeknoten hinzu.
		/// </summary>
		/// <param name="pin_ModulID">ID eines Moduls</param>
		/// <returns></returns>
		private TreeNode EinheitenZuModul(int pin_ModulID)
		{
			TreeNode trn_Einheiten = new TreeNode();
			trn_Einheiten.Tag = new Cst_EK_TreeviewTag();
			(trn_Einheiten.Tag as Cst_EK_TreeviewTag).Kontextmenue= ctx_abstrakt_Einheiten;
			foreach (Cdv_Einheit myeinheit in this._Cst_EK.AlleEinheiten)
			{
				if (myeinheit.ModulID==pin_ModulID)
				{
					TreeNode trn_Einheit = new TreeNode();
					trn_Einheit.SelectedImageIndex = 16;
					trn_Einheit.ImageIndex = 16;
					trn_Einheit.Text=myeinheit.Name;
					trn_Einheit.Tag= new Cst_EK_TreeviewTag();
					(trn_Einheit.Tag as Cst_EK_TreeviewTag).Eintrag=myeinheit;
					this._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(myeinheit.ID,trn_Einheit));
					(trn_Einheit.Tag as Cst_EK_TreeviewTag).Kontextmenue=ctx_Einheiten;
					(trn_Einheit.Tag as Cst_EK_TreeviewTag).Type=myeinheit.GetType();
					trn_Einheit=FuegeUnterknotenHinzu(trn_Einheit,FülleEinheitMitDaten(myeinheit));
					trn_Einheiten.Nodes.Add(trn_Einheit);
				}
			}
			return (trn_Einheiten);
		}


		/// <summary>
		/// Fügt die Unterknoten von NodeWithSubNodes unter ParentNode hinzu.
		/// </summary>
		/// <param name="ParentNode">Neuer Oberknoten</param>
		/// <param name="NodeWithSubNodes">Knoten unter dem die an Partennode zu setzen Knoten hängen</param>
		/// <returns></returns>
		private TreeNode FuegeUnterknotenHinzu(TreeNode ParentNode, TreeNode NodeWithSubNodes)
		{
			int i_tmp1;
			for (i_tmp1=0; i_tmp1<NodeWithSubNodes.Nodes.Count;i_tmp1++)
			{
				ParentNode.Nodes.Add(NodeWithSubNodes.Nodes[i_tmp1]);
			}
			//ParentNode.Nodes.Add(NodeWithSubNodes);
			return (ParentNode);
		}

		/// <summary>
		/// Lädt alle Helfer die einem Modul zugeordnet sind. 
		/// Fügt diese unter den Rückgabeknoten hinzu.
		/// </summary>
		/// <param name="pin_ModulID"></param>
		/// <returns></returns>
		private TreeNode HelferZuModul(int pin_ModulID)
		{
			TreeNode trn_Helfer = new TreeNode();
			trn_Helfer.Tag=new Cst_EK_TreeviewTag();
			(trn_Helfer.Tag as Cst_EK_TreeviewTag).Kontextmenue= ctx_abstrakt_Helfer;
			foreach (Cdv_Helfer myHelfer in this._Cst_EK.AlleHelfer)
			{
				if (myHelfer.ModulID==pin_ModulID)
				{
					TreeNode trn_myHelfer = new TreeNode();
					trn_myHelfer.SelectedImageIndex = 18;
					trn_myHelfer.ImageIndex = 18;
					trn_myHelfer.Text=myHelfer.Personendaten.Name+","+myHelfer.Personendaten.Vorname;
					trn_myHelfer.Tag= new Cst_EK_TreeviewTag();
					(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Eintrag=myHelfer;
					this._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(myHelfer.ID,trn_myHelfer));
					(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Type= myHelfer.GetType();
					(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Kontextmenue=ctx_Helfer;
					trn_Helfer.Nodes.Add(trn_myHelfer);
				}
			}
			return (trn_Helfer);
		}

		/// <summary>
		/// Läd alle Module zu einem ESP und führt jeweils EinheitenZuModul, HelferZuModul und KFZzuModul aus
		/// Fügt diese strukturiert unterhalb eines Oberknotens zurück
		/// </summary>
		/// <param name="ESPID">ID eines Einsatzschwerpunktes</param>
		/// <returns>Oberknoten mit darunterhängender Struktur</returns>
		/// 
		private TreeNode ModuleZuESP(int ESPID)
		{
			TreeNode trn_module = new TreeNode();
			trn_module.Tag= new Cst_EK_TreeviewTag();
			(trn_module.Tag as Cst_EK_TreeviewTag).Kontextmenue= ctx_abstrakt_Module;
			foreach (Cdv_Modul myModul in this._Cst_EK.AlleModule)
			{
				if (myModul.EinsatzschwerpunktID==ESPID)
				{
					TreeNode trn_myTreeNode= new TreeNode();
					trn_myTreeNode.Text=myModul.Modulname;
					trn_myTreeNode.SelectedImageIndex = 16;
					trn_myTreeNode.ImageIndex = 16;
					trn_myTreeNode.Tag=new Cst_EK_TreeviewTag();
					(trn_myTreeNode.Tag as Cst_EK_TreeviewTag).Kontextmenue=ctx_Module;
					(trn_myTreeNode.Tag as Cst_EK_TreeviewTag).Eintrag=myModul;
					this._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(myModul.ID,trn_myTreeNode));

					trn_myTreeNode=FuegeUnterknotenHinzu(trn_myTreeNode,EinheitenZuModul(myModul.ID));
					//trn_myTreeNode.Nodes.Add(trn_Einheiten);

					trn_module.Nodes.Add(trn_myTreeNode);
				}
			}
			return trn_module;
		}

		/// <summary>
		/// Füllt den Einsatzmanager mit den "Strukturen" Einsatzschwerpunkte, Einheiten, Module, (Material)
		/// </summary>
		public void FuelleEinsatzmanager()
		{
			TreeNode trn_Einsatzmangerbaum = new TreeNode();
			trn_Einsatzmangerbaum.Nodes.Clear();
			TreeNode trn_Einsatzschwerpunke= new TreeNode("Einsatzschwerpunkte",19,19);
			trn_Einsatzschwerpunke.Tag = new Cst_EK_TreeviewTag();
			(trn_Einsatzschwerpunke.Tag as Cst_EK_TreeviewTag).Kontextmenue= ctx_abstrakt_Einsatzschwerpunkte;
				
			Cdv_Einsatzschwerpunkt[] myEinsatzschwerpunkte = this._Cst_EK.AlleEinsatzschwerpunkte;
				
			for( int tmp1=0; tmp1<myEinsatzschwerpunkte.GetLength(0); tmp1++)
			{
				TreeNode trn_myEinsatzschwerpunktnode= new TreeNode();
				trn_myEinsatzschwerpunktnode.SelectedImageIndex = 19;
				trn_myEinsatzschwerpunktnode.ImageIndex = 19;
				trn_myEinsatzschwerpunktnode.Text = myEinsatzschwerpunkte[tmp1].Bezeichnung;
				trn_myEinsatzschwerpunktnode.Tag  = new Cst_EK_TreeviewTag();
				(trn_myEinsatzschwerpunktnode.Tag as Cst_EK_TreeviewTag).Eintrag=myEinsatzschwerpunkte[tmp1];
				this._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(myEinsatzschwerpunkte[tmp1].ID,trn_myEinsatzschwerpunktnode));
				(trn_myEinsatzschwerpunktnode.Tag as Cst_EK_TreeviewTag).Kontextmenue=ctx_Einsatzschwerpunkte;
				(trn_myEinsatzschwerpunktnode.Tag as Cst_EK_TreeviewTag).Type=myEinsatzschwerpunkte[tmp1].GetType();
				trn_myEinsatzschwerpunktnode=FuegeUnterknotenHinzu(trn_myEinsatzschwerpunktnode,(ModuleZuESP(myEinsatzschwerpunkte[tmp1].ID)));
				trn_Einsatzschwerpunke.Nodes.Add(trn_myEinsatzschwerpunktnode);
			}
	
			TreeNode trn_Module= new TreeNode("Module",16,16);
			trn_Module.Tag = new Cst_EK_TreeviewTag();
			(trn_Module.Tag as Cst_EK_TreeviewTag).Kontextmenue=ctx_abstrakt_Module;
			
			// Damit kein Exception ausgeworfen werden, wenn auf der DB kein Datensatz vorhanden ist
			Cdv_Modul[] myModule = new Cdv_Modul[0];
			myModule = this._Cst_EK.AlleModule;
	
			for( int tmp1=0; tmp1<myModule.GetLength(0); tmp1++)
			{
				TreeNode trn_myModul= new TreeNode();
				trn_myModul.SelectedImageIndex = 16;
				trn_myModul.ImageIndex = 16;
				trn_myModul.Text = myModule[tmp1].Modulname;
				trn_myModul.Tag  = new Cst_EK_TreeviewTag();
				(trn_myModul.Tag as Cst_EK_TreeviewTag).Eintrag=myModule[tmp1];
				this._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(myModule[tmp1].ID,trn_myModul));

				(trn_myModul.Tag as Cst_EK_TreeviewTag).Kontextmenue=ctx_Module;
				(trn_myModul.Tag as Cst_EK_TreeviewTag).Type=myModule[tmp1].GetType();
					
				trn_myModul=FuegeUnterknotenHinzu(trn_myModul,(EinheitenZuModul(myModule[tmp1].ID)));
				trn_Module.Nodes.Add(trn_myModul);
			}
	
				
			TreeNode trn_Einheiten= new TreeNode("Einheiten",16,16);
			trn_Einheiten.Tag = new Cst_EK_TreeviewTag();
			(trn_Einheiten.Tag as Cst_EK_TreeviewTag).Kontextmenue=ctx_abstrakt_Einheiten;
			
			Cdv_Einheit[] myEinheiten = this._Cst_EK.AlleEinheiten;
	
			for( int tmp1=0; tmp1<myEinheiten.GetLength(0); tmp1++)
			{
				TreeNode trn_myEinheit= new TreeNode();
				trn_myEinheit.SelectedImageIndex = 16;
				trn_myEinheit.ImageIndex = 16;
				trn_myEinheit.Text = myEinheiten[tmp1].Name;
				trn_myEinheit.Tag  = new Cst_EK_TreeviewTag();
				(trn_myEinheit.Tag as Cst_EK_TreeviewTag).Eintrag=myEinheiten[tmp1];
				this._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(myEinheiten[tmp1].ID,trn_myEinheit));
				(trn_myEinheit.Tag as Cst_EK_TreeviewTag).Kontextmenue=ctx_Einheiten ;
				(trn_myEinheit.Tag as Cst_EK_TreeviewTag).Type=myEinheiten[tmp1].GetType();
					
				trn_myEinheit=FuegeUnterknotenHinzu(trn_myEinheit,FülleEinheitMitDaten(myEinheiten[tmp1]));
				trn_Einheiten.Nodes.Add(trn_myEinheit);
			}
	
			TreeNode trn_Helfer = new TreeNode("Helfer", 18, 18);
			trn_Helfer.Tag = new Cst_EK_TreeviewTag();
			(trn_Helfer.Tag as Cst_EK_TreeviewTag).Kontextmenue = ctx_abstrakt_Helfer;
			Cdv_Helfer[] myHelfer = this._Cst_EK.AlleHelfer;
			IEnumerator ie = myHelfer.GetEnumerator();
			while(ie.MoveNext())
			{
				Cdv_Helfer helfer = (Cdv_Helfer) ie.Current;
				TreeNode trn_myHelfer = new TreeNode();
				trn_myHelfer.SelectedImageIndex = 18;
				trn_myHelfer.ImageIndex = 18;
				trn_myHelfer.Text = helfer.ToString();
				trn_myHelfer.Tag = new Cst_EK_TreeviewTag();
				(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Eintrag = helfer;
				this._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(helfer.ID,trn_myHelfer));
				(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Kontextmenue = this.ctx_Helfer;
				(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Type = helfer.GetType();
	
				trn_Helfer.Nodes.Add(trn_myHelfer);
			}
	
			trn_Einsatzmangerbaum.Nodes.Add(trn_Einsatzschwerpunke);
			trn_Einsatzmangerbaum.Nodes.Add(trn_Module);
			trn_Einsatzmangerbaum.Nodes.Add(trn_Einheiten);
			trn_Einsatzmangerbaum.Nodes.Add(trn_Helfer);
	
			trv_Einsatzmanager.BeginUpdate();
			trv_Einsatzmanager.Nodes.Clear();
			foreach (TreeNode myNode in trn_Einsatzmangerbaum.Nodes)
			{
				trv_Einsatzmanager.Nodes.Add(myNode);
			}
			trv_Einsatzmanager.EndUpdate();
		}		


		#endregion

		#region Funktionalität
		
		private void SetzeHilfe()
		{
			this.pelsHelp.HelpNamespace = _Cst_EK.Einstellung.Helpfile;
			this.pelsHelp.SetShowHelp(this,true);
			this.pelsHelp.SetHelpKeyword(this,"Einsatz und Kräfte");

		}
		
		public void VerarbeiteUnterknoten(TreeNode pin_trn_Wurzel, ContextMenu pin_ctx_Menue)
		{
			//			int _i_Index = pin_trn_Wurzel.ImageIndex;
			//			switch(_i_Index % 5)
			//			{
			//				case 0:
			//					//pin_trn_Wurzel.Tag = this.ctx_Einheiten;
			//					break;
			//				case 1:
			//					//pin_trn_Wurzel.Tag = this.ctx_Material;
			//					break;
			//				case 2:
			//					//pin_trn_Wurzel.Tag = this.ctx_Helfer;
			//					break;
			//				case 3:
			//					//pin_trn_Wurzel.Tag = this.ctx_Einsatzschwerpunkte;
			//					break;
			//				case 4:
			//					//pin_trn_Wurzel.Tag = this.ctx_Material;
			//					break;
			//			}
			//			TreeNodeCollection tnc_Knoten = pin_trn_Wurzel.Nodes;
			//			IEnumerator ie = tnc_Knoten.GetEnumerator();
			//			while(ie.MoveNext())
			//			{
			//				TreeNode trn_Aktuell = (TreeNode) ie.Current;
			//				VerarbeiteUnterknoten(trn_Aktuell, pin_ctx_Menue);
			//			}
		}
		#endregion

		#region event handler
		#region zu ändern(Namenskonvention usw.)

		public void btn_Aktualisieren_Click(object sender, System.EventArgs e)
		{
			_TreeNodeReferenzen.Clear();
			trv_Einsatzmanager.Nodes.Clear();
			this.FuelleEinsatzmanager();
			//MessageBox.Show("trv sollte fertig sein");
		}
		private void mI_ctx_Helfer_als_verpflegt_Kennzeichnen_Click(object sender, System.EventArgs e)
		{ //F160
			//Cpr_usc_EK
			//Hierbei soll ein Dialog geöffnet werden, in dem angegeben werden kann, 
			// ob der Helfer jetzt oder zu einem früheren Zeitpunkt Verpflegt wird
			// Bei bestätigung wird 
			// VerpflegeHelfer(Cdv_Helfer pin_helfer, Date pin_datum) auf STS aufgerufen
		}

			
		// TODO 1. Daten vom gewählten Einsatzschwerpunkt laden inklusive Erkundungsdetails
	
		// TODO: Reaktion auf geöffnete Datei

		private void btn_Modul_erstellen_Click(object sender, System.EventArgs e)
		{
			//TODO:
			//neues Menue aufmachen (aufpoppen), dort namen abfragen und dann in
			//in die Liste der bestehenden Module aufnehmen (this.cbx_Module_Modul)
			//
		}


		#endregion

		// Der folgende üble Hack kommt von Steini.
		// Er ermittelt für einen Treeview aus den aktuellen Mauskoordinaten den zugehörigen Node
		// Dies klappt egal ob rechte,linke oder mittlere Maustaste genutzt werden.
		private void trv_Einsatzmanager_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			
			if(this._b_FelderModifiziert && e.Clicks == 2)
				return;
			TreeNode trn_AusgewaehlterKnoten;
			trn_AusgewaehlterKnoten = trv_Einsatzmanager.GetNodeAt(e.X,e.Y);
			if ((trn_AusgewaehlterKnoten != null) && (trv_Einsatzmanager.SelectedNode != trn_AusgewaehlterKnoten))
			{
				trv_Einsatzmanager.SelectedNode = trn_AusgewaehlterKnoten;
			}
			//Beim doppelklick auf ein Item, dieses anzeigen
			//			if(e.Clicks == 2)	
			//if(e.Clicks == 2 && trn_AusgewaehlterKnoten.GetNodeCount() == 0)
			if(e.Clicks == 2 && e.Button == MouseButtons.Left)	
			{
				Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trn_AusgewaehlterKnoten.Tag;
				if(tag == null || tag.Type == null || tag.Eintrag == null)
					return;
				if(tag.Eintrag is Cdv_Einsatzschwerpunkt)
				{
					if(((this._ESP.FelderIstModifiziert == true) && 
						(pELS.GUI.PopUp.CPopUp.ZuruecksetzenEingaben("Daten auf der Seite Einsatzschwerpunkt wurden geändert.") == DialogResult.Yes ))
						|| (this._ESP.FelderIstModifiziert == false))
					{
						Cdv_Einsatzschwerpunkt esp = (Cdv_Einsatzschwerpunkt) tag.Eintrag;
						this._ESP.LadeESP(esp);
						this.tabctrl_EK.SelectedTab = this.tabPage_Einsatzschwerpunkt;
					}
					
					
					return;					
				}
				if(tag.Eintrag is Cdv_Helfer)
				{
					if(((this._Helfer.FelderIstModifiziert == true) && 
						(pELS.GUI.PopUp.CPopUp.ZuruecksetzenEingaben("Daten auf der Seite Helfer wurden geändert.") == DialogResult.Yes ))
						|| (this._Helfer.FelderIstModifiziert == false))
					{
						Cdv_Helfer helfer = (Cdv_Helfer) tag.Eintrag;
						this._Helfer.LadeHelfer(helfer);
						int i_tabindex = this.tabctrl_EK.TabPages.IndexOf(this.tabPage_Helfer);
						this.tabctrl_EK.SelectedIndex = i_tabindex;							
					}
					
					return;
				}
				if(tag.Eintrag is Cdv_Einheit)
				{
					if(((this._Einheit.FelderIstModifziert == true) && 
						(pELS.GUI.PopUp.CPopUp.ZuruecksetzenEingaben("Daten auf der Seite Einheit wurden geändert.") == DialogResult.Yes ))
						|| (this._Einheit.FelderIstModifziert == false))
					{
						Cdv_Einheit einheit = (Cdv_Einheit) tag.Eintrag;
						this._Einheit.LadeEinheit(einheit);
						int i_tabindex = this.tabctrl_EK.TabPages.IndexOf(this.tabpage_Einheit);
						this.tabctrl_EK.SelectedIndex = i_tabindex;							
					}
					
					return;				
				}
				if(tag.Eintrag is Cdv_KFZ)
				{
					if(((this._Kfz.FelderIstModifiziert) && 
						(pELS.GUI.PopUp.CPopUp.ZuruecksetzenEingaben("Daten auf der Seite KFZ wurden geändert.") == DialogResult.Yes))
						|| (!this._Kfz.FelderIstModifiziert))
					{
						Cdv_KFZ kfz = (Cdv_KFZ) tag.Eintrag;
						Cst_EK_TreeviewTag tagEinheit = (Cst_EK_TreeviewTag) trn_AusgewaehlterKnoten.Parent.Parent.Tag;
						Cdv_Einheit einheit = (Cdv_Einheit) tagEinheit.Eintrag;
						this._Kfz.LadeKfz(kfz, einheit);
						this._Kfz.Enabled = true;
						int i_tabindex = this.tabctrl_EK.TabPages.IndexOf(this.tabPage_Kfz);
						this.tabctrl_EK.SelectedIndex = i_tabindex;
					}
					return;
				}
				if(tag.Eintrag is Cdv_Modul)
				{
					if(((this._Module.FelderIstModifiziert) &&
						(pELS.GUI.PopUp.CPopUp.ZuruecksetzenEingaben("Daten auf der Seite Modul wurden geändert.") == DialogResult.Yes))
						|| (!this._Module.FelderIstModifiziert))
					{
						Cdv_Modul modul = (Cdv_Modul) tag.Eintrag;
						this._Module.LadeModul(modul);
						int i_tabindex = this.tabctrl_EK.TabPages.IndexOf(this.tabPage_Modul);
						this.tabctrl_EK.SelectedIndex = i_tabindex;
					}
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

		private void mI_abstrakt_Fahrzeuge_NeuesKFZAnlegen_Click(object sender, System.EventArgs e)
		{
			TreeNode trnKnoten = this.trv_Einsatzmanager.SelectedNode.Parent;
			Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trnKnoten.Tag;
			Cdv_Einheit einheit = (Cdv_Einheit) tag.Eintrag;
			this._Kfz.NeuesKfzAnlegenStart(einheit);
			this._Kfz.Enabled = true;
			this.tabctrl_EK.SelectedTab = this.tabPage_Kfz;
		}
		
		#endregion

		#region Drag'n Drop Implementation

		private void trv_Einsatzmanager_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			e.Effect = System.Windows.Forms.DragDropEffects.Copy | DragDropEffects.Move;
		}

		private void trv_Einsatzmanager_DragLeave(object sender, System.EventArgs e)
		{
		
		}

		private void trv_Einsatzmanager_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
			TreeNode trn_Knoten = (TreeNode) e.Item;
			int _i_Index = trn_Knoten.Index;
			Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trn_Knoten.Tag;
			if((trn_Knoten.Nodes.Count == 0) || (tag.Eintrag != null && tag.Eintrag is Cdv_Einheit)
				|| (tag.Eintrag != null && tag.Eintrag is Cdv_Modul))
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
				this._Cst_EK.ErmittleZuordnungDND(this._trn_QuellKnoten, this._trn_ZielKnoten);
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
			if((_trn_Knoten = this.VerarbeiteWurzel(pin_i_X, pin_i_Y, this.trv_Einsatzmanager.Nodes[3])) != null)
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
				e.Effect = this._Cst_EK.ErmittleSymbolDND((Cst_EK_TreeviewTag) this._trn_QuellKnoten.Tag, (Cst_EK_TreeviewTag) _trn_Knoten.Tag);
			}
		}
		#endregion

		#region unknown methoden 
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

		private void mI_abstrakt_Helfer_StatusSetzenAuf_Click(object sender, System.EventArgs e)
		{
		
		}

		private void mI_abstrakt_Einheiten_NeueEinheitAnlegen_Click(object sender, System.EventArgs e)
		{
		
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

		#region für dynamische Datenaktualisierung

		#region TreeView Aktualisierung

		#region modul

		public void EntferneHelferVonQuelle(Cdv_Helfer pin_helfer, Cdv_Einheit pin_einheit)
		{
			//modul
			TreeNode trnModule = this.trv_Einsatzmanager.Nodes[1];
			IEnumerator ieModule = trnModule.Nodes.GetEnumerator();
			while(ieModule.MoveNext())
			{
				TreeNode trnModul = (TreeNode) ieModule.Current;
				Cdv_Modul modul = (Cdv_Modul) ((Cst_EK_TreeviewTag) trnModul.Tag).Eintrag;
				if(modul.ID == pin_einheit.ModulID)
				{
					IEnumerator ieEinheiten = trnModul.Nodes.GetEnumerator();
					while(ieEinheiten.MoveNext())
					{
						TreeNode trnEinheit = (TreeNode) ieEinheiten.Current;
						Cdv_Einheit einheit = (Cdv_Einheit) ((Cst_EK_TreeviewTag) trnEinheit.Tag).Eintrag;
						if(einheit.ID == pin_einheit.ID)
						{
							TreeNode trnHelfer = trnEinheit.Nodes[0];
							IEnumerator ieHelfer = trnHelfer.Nodes.GetEnumerator();
							while(ieHelfer.MoveNext())
							{
								TreeNode trnH = (TreeNode) ieHelfer.Current;
								Cdv_Helfer helfer = (Cdv_Helfer) ((Cst_EK_TreeviewTag) trnH.Tag).Eintrag;
								if(helfer.ID == pin_helfer.ID)
									trnHelfer.Nodes.Remove(trnH);
							}
						}
					}
				}
			}
			//einheit
		}

		public void FuegeHelferZurEinheit(Cdv_Einheit pin_einheit, Cdv_Helfer pin_helfer)
		{
			TreeNode trnModule = this.trv_Einsatzmanager.Nodes[1];
			IEnumerator ieModule = trnModule.Nodes.GetEnumerator();
			while(ieModule.MoveNext())
			{
				TreeNode trnModul = (TreeNode) ieModule.Current;
				Cst_EK_TreeviewTag tagModul = (Cst_EK_TreeviewTag) trnModul.Tag;
				Cdv_Modul modul = (Cdv_Modul) tagModul.Eintrag;
				if(modul.ID == pin_einheit.ModulID)
				{
					IEnumerator ieEinheiten = trnModul.Nodes.GetEnumerator();
					while(ieEinheiten.MoveNext())
					{
						TreeNode trnEinheit = (TreeNode) ieEinheiten.Current;
						Cst_EK_TreeviewTag tagEinheit = (Cst_EK_TreeviewTag) trnEinheit.Tag;
						Cdv_Einheit einheit = (Cdv_Einheit) tagEinheit.Eintrag;
						if(einheit.ID == pin_einheit.ID)
						{
							TreeNode trn_myHelfer = new TreeNode();
							trn_myHelfer.SelectedImageIndex = 18;
							trn_myHelfer.ImageIndex = 18;
							trn_myHelfer.Text = pin_helfer.Personendaten.Name + "," + pin_helfer.Personendaten.Vorname;
							trn_myHelfer.Tag = new Cst_EK_TreeviewTag();
							(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Eintrag = pin_helfer;
							(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Kontextmenue = this.ctx_Helfer;
							(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Type = pin_helfer.GetType();
							trnEinheit.Nodes[0].Nodes.Add(trn_myHelfer);	
							break;
						}
					}
				}
			}
		}

		public void EnfterneEinheitVomModul(Cdv_Einheit pin_einheit, Cdv_Modul pin_modul)
		{
			TreeNode trnModule = this.trv_Einsatzmanager.Nodes[1];
			IEnumerator ieModule = trnModule.Nodes.GetEnumerator();
			while(ieModule.MoveNext())
			{
				TreeNode trnM = (TreeNode) ieModule.Current;
				Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trnM.Tag;
				Cdv_Modul modul = (Cdv_Modul) tag.Eintrag;
				if(modul.ID == pin_einheit.ModulID)
				{
					//TreeNode trnEinheiten = trnM.Nodes;
					IEnumerator ieEinheiten = trnM.Nodes.GetEnumerator();
					while(ieEinheiten.MoveNext())
					{
						TreeNode trnEinheit = (TreeNode) ieEinheiten.Current;
						Cdv_Einheit einheit = (Cdv_Einheit) ((Cst_EK_TreeviewTag) trnEinheit.Tag).Eintrag;
						if(einheit.ID == pin_einheit.ID)
						{
							this.trv_Einsatzmanager.Nodes.Remove(trnEinheit);
							break;
						}
					}
				}
			}
		}

		public void AktualisiereModulVonEinheit(Cdv_Einheit pin_einheit)
		{
			TreeNode trnModule = this.trv_Einsatzmanager.Nodes[1];
			IEnumerator ieModule = trnModule.Nodes.GetEnumerator();
			while(ieModule.MoveNext())
			{
				TreeNode trnM = (TreeNode) ieModule.Current;
				Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trnM.Tag;
				Cdv_Modul modulAlt = (Cdv_Modul) tag.Eintrag;
				if(modulAlt.ID == pin_einheit.ModulID)
				{
					Cdv_Modul modulNeu = this._Cst_EK.HoleModul(pin_einheit.ModulID);
					trnM.Text = modulNeu.Modulname;
					tag.Eintrag = modulNeu;
					trnM.Tag = tag;
					TreeNode trn_myModul= new TreeNode();
					trn_myModul.SelectedImageIndex = 16;
					trn_myModul.ImageIndex = 16;
					trn_myModul.Text = pin_einheit.Name;
					trn_myModul.Tag  = new Cst_EK_TreeviewTag();
					(trn_myModul.Tag as Cst_EK_TreeviewTag).Eintrag=pin_einheit;
					(trn_myModul.Tag as Cst_EK_TreeviewTag).Kontextmenue=this.ctx_Einheiten;
					(trn_myModul.Tag as Cst_EK_TreeviewTag).Type=pin_einheit.GetType();
					trn_myModul=FuegeUnterknotenHinzu(trn_myModul,(ErstelleEinheitKnoten(pin_einheit)));
					trnM.Nodes.Add(trn_myModul);
					break;
				}
			}
		}

		#endregion

		#region kfz

		public void AktualisiereModulVonKfz(Cdv_Einheit pin_einheit, Cdv_KFZ pin_kfz)
		{
			int iModulID = pin_einheit.ModulID;
			TreeNode trnModule = this.trv_Einsatzmanager.Nodes[1];
			TreeNodeCollection tncModule = trnModule.Nodes;
			IEnumerator ieModule = tncModule.GetEnumerator();
			while(ieModule.MoveNext())
			{
				TreeNode trnKnoten = (TreeNode) ieModule.Current;
				Cdv_Modul modul = (Cdv_Modul) ((Cst_EK_TreeviewTag) trnKnoten.Tag).Eintrag;
				if(modul.ID == iModulID)
				{
					IEnumerator ie = trnKnoten.Nodes.GetEnumerator();
					while(ie.MoveNext())
					{
						TreeNode trnE = (TreeNode) ie.Current;
						Cdv_Einheit einheit = (Cdv_Einheit) ((Cst_EK_TreeviewTag) trnE.Tag).Eintrag;
						if(einheit.ID == pin_einheit.ID)
						{
							TreeNode trnKfz = trnE.Nodes[1];
							IEnumerator ieKfz = trnKfz.Nodes.GetEnumerator();
							bool bGefunden  = false;
							while(ieKfz.MoveNext())
							{
								TreeNode trnK = (TreeNode) ieKfz.Current;
								Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trnK.Tag;
								Cdv_KFZ kfz = (Cdv_KFZ) tag.Eintrag;
								if(kfz.ID == pin_kfz.ID)
								{
									tag.Eintrag = pin_kfz;
									trnK.Tag = tag;
									trnK.Text = pin_kfz.KfzTyp + " " + pin_kfz.Kennzeichen;
									bGefunden = true;
									break;
								}
							}
							if(!bGefunden)
							{
								TreeNode trn_myKfz = new TreeNode();
								trn_myKfz.SelectedImageIndex = 17;
								trn_myKfz.ImageIndex = 17;
								Cdv_KFZ myKfz=pin_kfz;
								trn_myKfz.Text=myKfz.KfzTyp+" "+myKfz.Kennzeichen;
								trn_myKfz.Tag=new Cst_EK_TreeviewTag();
								(trn_myKfz.Tag as Cst_EK_TreeviewTag).Eintrag=myKfz;					
								(trn_myKfz.Tag as Cst_EK_TreeviewTag).Type=myKfz.GetType();
								(trn_myKfz.Tag as Cst_EK_TreeviewTag).Kontextmenue=ctx_Fahrzeuge;
								trnKfz.Nodes.Add(trn_myKfz);
							}
							bGefunden = false;
						}
					}
				}
			}
		}

		#endregion

		#region einheit

		private Cdv_Helfer EinheitEnthaeltHelfer(int pin_helferID, TreeNode pin_trnKnoten)
		{
			IEnumerator ie = pin_trnKnoten.Nodes.GetEnumerator();
			while(ie.MoveNext())
			{
				TreeNode trnH = (TreeNode) ie.Current;
				Cdv_Helfer helfer = (Cdv_Helfer) ((Cst_EK_TreeviewTag) trnH.Tag).Eintrag;
				if(pin_helferID == helfer.ID)
					return(helfer);
			}
			return(null);
		}

		public void AktualisiereHelferVonEinheit(Cdv_Einheit pin_einheit, TreeNode pin_trnKnoten)
		{
			int[] iaIDs = pin_einheit.HelferIDMenge;
			if(iaIDs == null)
				return;
			ArrayList alIDs = new ArrayList(iaIDs);
			TreeNode trnHelfer = pin_trnKnoten.Nodes[0];
			bool bHelferGefunden = false;
			bool bHelferZumLoeschen = false;
			TreeNode trnH = null;
			Cdv_Helfer helfer = null;
			for(int i = 0; i < iaIDs.Length; i++)
			{
				if((helfer = this.EinheitEnthaeltHelfer(iaIDs[i], trnHelfer)) != null)
				{
					bHelferGefunden = true;
					bHelferZumLoeschen = false;
				}
				if(helfer != null && !alIDs.Contains(helfer.ID))
				{
					bHelferZumLoeschen = true;
				}
				if(!bHelferGefunden)
				{
					helfer = this._Cst_EK.HoleHelfer(iaIDs[i]);
					TreeNode trn_myHelfer = new TreeNode();
					trn_myHelfer.SelectedImageIndex = 18;
					trn_myHelfer.ImageIndex = 18;
					trn_myHelfer.Text = helfer.Personendaten.Name + "," + helfer.Personendaten.Vorname;
					trn_myHelfer.Tag = new Cst_EK_TreeviewTag();
					(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Eintrag = helfer;
					(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Kontextmenue = this.ctx_Helfer;
					(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Type = helfer.GetType();
					trnHelfer.Nodes.Add(trn_myHelfer);
				}
				else
				{

				}
				if(bHelferZumLoeschen)
				{
					if(trnH != null)
						trnHelfer.Nodes.Remove(trnH);
				}
				bHelferGefunden = false;
				bHelferZumLoeschen = false;
				helfer = null;
			}
		}

		private Cdv_KFZ EinheitEnthaeltKfz(int pin_kfzID, TreeNode pin_trnKnoten)
		{
			IEnumerator ie = pin_trnKnoten.Nodes.GetEnumerator();
			while(ie.MoveNext())
			{
				TreeNode trnH = (TreeNode) ie.Current;
				Cdv_KFZ kfz = (Cdv_KFZ) ((Cst_EK_TreeviewTag) trnH.Tag).Eintrag;
				if(pin_kfzID == kfz.ID)
					return(kfz);
			}
			return(null);
		}

		public void AktualisiereKfzVonEinehit(Cdv_Einheit pin_einheit, TreeNode pin_trnKnoten)
		{
			int[] iaIDs = pin_einheit.KfzKraefteIDMenge;
			if(iaIDs == null)
				return;
			ArrayList alIDs = new ArrayList(iaIDs);
			TreeNode trnKfz = pin_trnKnoten.Nodes[1];
			bool bKfzGefunden = false;
			bool bKfzZumLoeschen = false;
			TreeNode trnK = null;
			Cdv_KFZ kfz = null;
			for(int i = 0; i < iaIDs.Length; i++)
			{
				if((kfz = this.EinheitEnthaeltKfz(iaIDs[i], trnKfz)) != null)
				{
					bKfzGefunden = true;
					bKfzZumLoeschen = false;
				}
				if(kfz != null && !alIDs.Contains(kfz.ID))
				{
					bKfzZumLoeschen = true;
				}
				if(!bKfzGefunden)
				{
					kfz = this._Cst_EK.HoleKfz(iaIDs[i]);
					TreeNode trn_myKFZ = new TreeNode();
					trn_myKFZ.SelectedImageIndex = 17;
					trn_myKFZ.ImageIndex = 17;
					trn_myKFZ.Text=kfz.KfzTyp+' '+kfz.Kennzeichen;
					trn_myKFZ.Tag= new Cst_EK_TreeviewTag();
					(trn_myKFZ.Tag as Cst_EK_TreeviewTag).Eintrag=kfz;
					(trn_myKFZ.Tag as Cst_EK_TreeviewTag).Type=kfz.GetType();
					(trn_myKFZ.Tag as Cst_EK_TreeviewTag).Kontextmenue=ctx_Fahrzeuge;
					trnKfz.Nodes.Add(trn_myKFZ);
				}
				if(bKfzZumLoeschen)
				{
					if(trnK != null)
						trnKfz.Nodes.Remove(trnK);
				}
				bKfzGefunden = false;
				bKfzZumLoeschen = false;
				kfz = null;
			}
		}

		#endregion

		#region helfer

		public void AktualisiereModulVonEinheit(Cdv_Einheit pin_einheit, TreeNode pin_NeuerKnoten, Cdv_Helfer pin_helfer)
		{
			int iModulID = pin_einheit.ModulID;
			TreeNode trnModule = this.trv_Einsatzmanager.Nodes[1];
			TreeNodeCollection tncModule = trnModule.Nodes;
			IEnumerator ieModule = tncModule.GetEnumerator();
			while(ieModule.MoveNext())
			{
				TreeNode trnKnoten = (TreeNode) ieModule.Current;
				Cdv_Modul modul = (Cdv_Modul) ((Cst_EK_TreeviewTag) trnKnoten.Tag).Eintrag;
				if(modul.ID == iModulID)
				{
					IEnumerator ie = trnKnoten.Nodes.GetEnumerator();
					while(ie.MoveNext())
					{
						TreeNode trnE = (TreeNode) ie.Current;
						Cdv_Einheit einheit = (Cdv_Einheit) ((Cst_EK_TreeviewTag) trnE.Tag).Eintrag;
						if(einheit.ID == pin_einheit.ID)
						{
							TreeNode trnHelfer = trnE.Nodes[0];
							IEnumerator ieHelfer = trnHelfer.Nodes.GetEnumerator();
							while(ieHelfer.MoveNext())
							{
								TreeNode trnH = (TreeNode) ieHelfer.Current;
								Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trnH.Tag;
								Cdv_Helfer helfer = (Cdv_Helfer) tag.Eintrag;
								if(helfer.ID == pin_helfer.ID)
								{
									tag.Eintrag = pin_helfer;
									trnH.Tag = tag;
									trnH.Text = pin_helfer.Personendaten.Name + "," + pin_helfer.Personendaten.Vorname;
								}
							}
						}
					}
				}
			}
		}

		public void AktualisiereEinheitVomHelfer(Cdv_Helfer pin_helfer)
		{
			TreeNode trnEinheiten = this.trv_Einsatzmanager.Nodes[2];
			TreeNodeCollection tncEinheiten = trnEinheiten.Nodes;
			IEnumerator ieEinheiten = tncEinheiten.GetEnumerator();
			while(ieEinheiten.MoveNext())
			{
				TreeNode trnKnoten = (TreeNode) ieEinheiten.Current;
				TreeNode trnHelfer = trnKnoten.Nodes[0];
				TreeNodeCollection tncHelfer = trnHelfer.Nodes;
				IEnumerator ieHelfer = tncHelfer.GetEnumerator();
				while(ieHelfer.MoveNext())
				{
					TreeNode trnH = (TreeNode) ieHelfer.Current;
					Cst_EK_TreeviewTag tagH = (Cst_EK_TreeviewTag) trnH.Tag;
					Cdv_Helfer helfer = (Cdv_Helfer) tagH.Eintrag;
					if(helfer.ID == pin_helfer.ID)
					{
						tagH.Eintrag = pin_helfer;
						trnH.Tag = tagH;
						trnH.Text = pin_helfer.Personendaten.Name + "," + pin_helfer.Personendaten.Vorname;
						this.AktualisiereModulVonEinheit((Cdv_Einheit) ((Cst_EK_TreeviewTag) trnKnoten.Tag).Eintrag,
							trnKnoten, pin_helfer);
					}
				}
			}
		}

		#endregion

		#region TreeView Aktualisierung NEU!!!!!!!

		public void AktualisiereHelferTV(int[] pin_IDs)
		{
			int HelferID;
			for(int i = 0; i < pin_IDs.Length; i++)
			{
				HelferID=pin_IDs[i];
				bool bGefunden = false;
				foreach(Cst_EK_TreeviewReferenceItem item in this._TreeNodeReferenzen)
				{
					if(item.PelsObjectID == HelferID)
					{
						Cdv_Helfer helfer = this._Cst_EK.HoleHelfer(HelferID);
						(item.TreeNodeReferenziert.Tag as Cst_EK_TreeviewTag).Eintrag = helfer;
						item.TreeNodeReferenziert.Text = helfer.Personendaten.Name + "," + helfer.Personendaten.Vorname;
						bGefunden = true;
					}
				}
				if(!bGefunden)
				{
					Cdv_Helfer h = this._Cst_EK.HoleHelfer(HelferID);
					TreeNode trn_myHelfer = new TreeNode();
					trn_myHelfer.SelectedImageIndex = 18;
					trn_myHelfer.ImageIndex = 18;
					trn_myHelfer.Text = h.ToString();
					trn_myHelfer.Tag = new Cst_EK_TreeviewTag();
					(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Eintrag = h;
					(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Kontextmenue = this.ctx_Helfer;
					(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Type = h.GetType();
					Cst_EK_TreeviewReferenceItem refItem = new Cst_EK_TreeviewReferenceItem(h.ID, trn_myHelfer);
					this._TreeNodeReferenzen.Add(refItem);		
					this.trv_Einsatzmanager.Nodes[3].Nodes.Add(trn_myHelfer);
				}
			}
		}

		public void AktualisiereTVKfz(int[] pin_IDs)
		{
			int KfzID;
			for(int i = 0; i < pin_IDs.Length; i++)
			{
				KfzID=pin_IDs[i];
				bool bGefunden = false;
				foreach(Cst_EK_TreeviewReferenceItem item in this._TreeNodeReferenzen)
				{
					if(item.PelsObjectID == KfzID)
					{
						Cdv_KFZ kfz = this._Cst_EK.HoleKfz(KfzID);
						(item.TreeNodeReferenziert.Tag as Cst_EK_TreeviewTag).Eintrag = kfz;
						item.TreeNodeReferenziert.Text = kfz.KfzTyp + " " + kfz.Kennzeichen;
						//this.AktualisiereTVEinheitHelfer(einheit,einheitenAlt,einheitenNeu);
						bGefunden = true;
					}
				}
				if(!bGefunden)
				{
					Cdv_KFZ kfz = this._Cst_EK.HoleKfz(KfzID);
					TreeNode trn_myKfz = new TreeNode();
					trn_myKfz.SelectedImageIndex = 17;
					trn_myKfz.ImageIndex = 17;
					trn_myKfz.Text = kfz.KfzTyp + " " + kfz.Kennzeichen;
					trn_myKfz.Tag = new Cst_EK_TreeviewTag();
					(trn_myKfz.Tag as Cst_EK_TreeviewTag).Eintrag = kfz;
					(trn_myKfz.Tag as Cst_EK_TreeviewTag).Kontextmenue = this.ctx_Fahrzeuge;
					(trn_myKfz.Tag as Cst_EK_TreeviewTag).Type = kfz.GetType();
					Cdv_Einheit einheit = this._Cst_EK.HoleEinheitZumKfz(KfzID);
					ArrayList myTmpReferenz = new ArrayList(this._TreeNodeReferenzen);
					foreach(Cst_EK_TreeviewReferenceItem item in myTmpReferenz)
					{
						if(item.PelsObjectID == einheit.ID)
						{
							TreeNode trn_Temp = (TreeNode) trn_myKfz.Clone();
							Cst_EK_TreeviewReferenceItem refItem = new Cst_EK_TreeviewReferenceItem(kfz.ID, trn_Temp);
							this._TreeNodeReferenzen.Add(refItem);
							item.TreeNodeReferenziert.Nodes[1].Nodes.Add(trn_Temp);
						}
					}
					
				}
			}
		}

		public void AktualisiereTVEinheitKfz(Cdv_Einheit pin_einheit, Cdv_Einheit[] einheitenAlt, Cdv_Einheit[] einheitenNeu)
		{
			Cdv_Einheit einheitAlt = null;
			Cdv_Einheit einheitNeu = null;
			foreach(Cdv_Einheit einheit in einheitenAlt)
			{
				if(einheit.ID == pin_einheit.ID)
				{
					einheitAlt = einheit;
					break;
				}
			}
			foreach(Cdv_Einheit einheit in einheitenNeu)
			{
				if(einheit.ID == pin_einheit.ID)
				{
					einheitNeu = einheit;
					break;
				}
			}
			if(einheitAlt == null || einheitNeu == null)
				return;
			int[] iaIDsAlt = einheitAlt.KfzKraefteIDMenge;
			int[] iaIDsNeu = einheitNeu.KfzKraefteIDMenge;
			if(iaIDsAlt == null && iaIDsNeu == null)
				return;
			int iNewLen = 0;
			int iOldLen = 0;
			ArrayList arrayListAlt = null;
			ArrayList arrayListNeu = null;
			if(iaIDsNeu == null)
				arrayListNeu = new ArrayList();
			else
			{
				arrayListAlt= new ArrayList(iaIDsAlt);
				iNewLen = iaIDsNeu.Length;
			}
			if(iaIDsAlt == null)
				arrayListAlt = new ArrayList();
			else
			{
				arrayListNeu= new ArrayList(iaIDsNeu);
				iOldLen = iaIDsAlt.Length;
			}
			if(iNewLen == iOldLen)
				return;
			if(iOldLen > iNewLen)
			{
				foreach(int KfzID in iaIDsAlt)	
				{
					if (!arrayListNeu.Contains(KfzID))
					{
						ArrayList myTmpReferenz = new ArrayList(this._TreeNodeReferenzen);
						foreach(Cst_EK_TreeviewReferenceItem item in myTmpReferenz)
						{
							if(item.PelsObjectID == KfzID)
							{
								item.TreeNodeReferenziert.Remove();
								this._TreeNodeReferenzen.Remove(item);
								this._Cst_EK.SetzeKfzIDMengeFuerEinheit(pin_einheit, iaIDsNeu);
							}
						}					
					}
				}
			}
			if(iOldLen < iNewLen)
			{
				foreach(int KfzID in iaIDsNeu)
				{
					if(!arrayListAlt.Contains(KfzID))
					{
						ArrayList myTmpReferenz = new ArrayList(this._TreeNodeReferenzen);
						foreach(Cst_EK_TreeviewReferenceItem item in myTmpReferenz)
						{
							if(item.PelsObjectID == pin_einheit.ID)
							{
								Cdv_KFZ kfz = this._Cst_EK.HoleKfz(KfzID);
								TreeNode trn_myKfz = new TreeNode();
								trn_myKfz.SelectedImageIndex = 17;
								trn_myKfz.ImageIndex = 17;
								trn_myKfz.Text = kfz.KfzTyp + " " + kfz.Kennzeichen;
								trn_myKfz.Tag = new Cst_EK_TreeviewTag();
								(trn_myKfz.Tag as Cst_EK_TreeviewTag).Eintrag = kfz;
								(trn_myKfz.Tag as Cst_EK_TreeviewTag).Kontextmenue = this.ctx_Fahrzeuge;
								(trn_myKfz.Tag as Cst_EK_TreeviewTag).Type = kfz.GetType();
								Cst_EK_TreeviewReferenceItem refItem = new Cst_EK_TreeviewReferenceItem(kfz.ID, trn_myKfz);
								this._TreeNodeReferenzen.Add(refItem);		
								item.TreeNodeReferenziert.Nodes[1].Nodes.Add(trn_myKfz);
								this._Cst_EK.SetzeKfzIDMengeFuerEinheit(pin_einheit, iaIDsNeu);								
							}
						}					
					}
				}
			}
		}

		public void AktualisiereTVEinheitHelfer(Cdv_Einheit pin_einheit, Cdv_Einheit[] einheitenAlt, Cdv_Einheit[] einheitenNeu)
		{
			Cdv_Einheit einheitAlt = null;
			Cdv_Einheit einheitNeu = null;
			foreach(Cdv_Einheit einheit in einheitenAlt)
			{
				if(einheit.ID == pin_einheit.ID)
				{
					einheitAlt = einheit;
					break;
				}
			}
			foreach(Cdv_Einheit einheit in einheitenNeu)
			{
				if(einheit.ID == pin_einheit.ID)
				{
					einheitNeu = einheit;
					break;
				}
			}
			if(einheitAlt == null || einheitNeu == null)
				return;
			int[] iaIDsAlt = einheitAlt.HelferIDMenge;
			int[] iaIDsNeu = einheitNeu.HelferIDMenge;
			ArrayList arrayListAlt= new ArrayList(iaIDsAlt);
			ArrayList arrayListNeu= new ArrayList(iaIDsNeu);
			if(iaIDsAlt.Length == iaIDsNeu.Length)
				return;
			if(iaIDsAlt.Length > iaIDsNeu.Length)
			{
				foreach(int HelferID in iaIDsAlt)	
				{
					if (!arrayListNeu.Contains(HelferID))
					{
						ArrayList myTmpReferenz = new ArrayList(this._TreeNodeReferenzen);
						foreach(Cst_EK_TreeviewReferenceItem item in myTmpReferenz)
						{
							if(item.PelsObjectID == HelferID)
							{
								item.TreeNodeReferenziert.Remove();
								this._TreeNodeReferenzen.Remove(item);
								this._Cst_EK.SetzeHelferIDMengeFuerEinheit(pin_einheit, iaIDsNeu);
							}
						}					
					}
				}
			}
			if(iaIDsAlt.Length < iaIDsNeu.Length)
			{
				foreach(int HelferID in iaIDsNeu)
				{
					if(!arrayListAlt.Contains(HelferID))
					{
						ArrayList myTmpReferenz = new ArrayList(this._TreeNodeReferenzen);
						foreach(Cst_EK_TreeviewReferenceItem item in myTmpReferenz)
						{
							if(item.PelsObjectID == pin_einheit.ID)
							{
								Cdv_Helfer h = this._Cst_EK.HoleHelfer(HelferID);
								TreeNode trn_myHelfer = new TreeNode();
								trn_myHelfer.SelectedImageIndex = 18;
								trn_myHelfer.ImageIndex = 18;
								trn_myHelfer.Text = h.Personendaten.Name + "," + h.Personendaten.Vorname;
								trn_myHelfer.Tag = new Cst_EK_TreeviewTag();
								(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Eintrag = h;
								(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Kontextmenue = this.ctx_Helfer;
								(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Type = h.GetType();
								Cst_EK_TreeviewReferenceItem refItem = new Cst_EK_TreeviewReferenceItem(h.ID, trn_myHelfer);
								this._TreeNodeReferenzen.Add(refItem);		
								item.TreeNodeReferenziert.Nodes[0].Nodes.Add(trn_myHelfer);
								this._Cst_EK.SetzeHelferIDMengeFuerEinheit(pin_einheit, iaIDsNeu);
								
							}
						}					
					}
				}
			}
		}

		private  void EntferneAlleUnterknoten(TreeNode pin_trnBase)
		{
			foreach(TreeNode TrnSubNode in pin_trnBase.Nodes)
			{
				if (TrnSubNode.Nodes.Count>0)
				{
					EntferneAlleUnterknoten(TrnSubNode);
				}
				else
				{
					ArrayList NodeListe=new ArrayList(this._TreeNodeReferenzen);
					foreach(Cst_EK_TreeviewReferenceItem Item in NodeListe)
					{
						if(Item.TreeNodeReferenziert == TrnSubNode)
							this._TreeNodeReferenzen.Remove(TrnSubNode);
					}
				}
			}
		}

		private void FuegeAlleUnterknotenHinzu(TreeNode pin_trnBase)
		{
			foreach(TreeNode trnKnoten in pin_trnBase.Nodes)
			{
				if(trnKnoten.Nodes.Count > 0)
					FuegeAlleUnterknotenHinzu(trnKnoten);
				else
				{
					Cst_EK_TreeviewReferenceItem item = new Cst_EK_TreeviewReferenceItem();
					item.TreeNodeReferenziert = trnKnoten;
					Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trnKnoten.Tag;
					Cdv_pELSObject ipo = (Cdv_pELSObject) tag.Eintrag;
					item.PelsObjectID = ipo.ID;
					this._TreeNodeReferenzen.Add(item);
				}
			}
		}

		private void AktualisiereDatencacheEinheit(TreeNode pin_trnEinheit, bool pin_bLoeschen)
		{
			if(pin_bLoeschen)
			{
				EntferneAlleUnterknoten(pin_trnEinheit);
				ArrayList NodeListe = new ArrayList(this._TreeNodeReferenzen);
				foreach(Cst_EK_TreeviewReferenceItem item in NodeListe)
				{
					if(item.TreeNodeReferenziert == pin_trnEinheit)
						this._TreeNodeReferenzen.Remove(item);
				}
			}
			else
			{
				Cdv_Einheit einheit = (Cdv_Einheit) ((Cst_EK_TreeviewTag) pin_trnEinheit.Tag).Eintrag;
				Cst_EK_TreeviewReferenceItem refItem = new Cst_EK_TreeviewReferenceItem(einheit.ID, pin_trnEinheit);
				this._TreeNodeReferenzen.Add(refItem);
				FuegeAlleUnterknotenHinzu(pin_trnEinheit);
			}
		}

		private TreeNode ErstelleEinheitKnoten(Cdv_Einheit pin_einheit)
		{
			TreeNode trn_Einheit = new TreeNode();
			trn_Einheit.SelectedImageIndex = 16;
			trn_Einheit.ImageIndex = 16;
			trn_Einheit.Text=pin_einheit.Name;
			trn_Einheit.Tag= new Cst_EK_TreeviewTag();
			(trn_Einheit.Tag as Cst_EK_TreeviewTag).Eintrag=pin_einheit;
			this._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(pin_einheit.ID,trn_Einheit));
			(trn_Einheit.Tag as Cst_EK_TreeviewTag).Kontextmenue=this.ctx_abstrakt_Einheiten;
			(trn_Einheit.Tag as Cst_EK_TreeviewTag).Type=pin_einheit.GetType();
			trn_Einheit=FuegeUnterknotenHinzu(trn_Einheit,FülleEinheitMitDaten(pin_einheit));
			return (trn_Einheit);
		}
		
		public void OrdneEinheitModulHinzu(Cdv_Einheit pin_einheit, int pin_ModulID)
		{
			TreeNode trnModule = this.trv_Einsatzmanager.Nodes[1];
			IEnumerator ieModule = trnModule.Nodes.GetEnumerator();
			while(ieModule.MoveNext())
			{
				TreeNode trnM = (TreeNode) ieModule.Current;
				Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trnM.Tag;
				Cdv_Modul modul = (Cdv_Modul) tag.Eintrag;
				if(modul.ID == pin_ModulID)
				{
					TreeNode trnEinheit = ErstelleEinheitKnoten(pin_einheit);
					//AktualisiereDatencacheEinheit(trnEinheit, false);
					trnM.Nodes.Add(trnEinheit);
				}
			}
		}

		public void OrdneEinheitModulImESPHinzu(Cdv_Einheit pin_einheit, int pin_ModulID, int pin_EspID)
		{
			TreeNode trnEsp = this.trv_Einsatzmanager.Nodes[0];
			IEnumerator ieEsp = trnEsp.Nodes.GetEnumerator();
			while(ieEsp.MoveNext())
			{
				TreeNode trnE = (TreeNode) ieEsp.Current;
				Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trnE.Tag;
				Cdv_Einsatzschwerpunkt esp = (Cdv_Einsatzschwerpunkt) tag.Eintrag;
				if(esp.ID == pin_EspID)
				{
					TreeNodeCollection trnModule = trnE.Nodes;
					//TreeNode trnModule = trnE.Nodes[0].Nodes;
					IEnumerator ieModule = trnModule.GetEnumerator();
					while(ieModule.MoveNext())
					{
						TreeNode trnM = (TreeNode) ieModule.Current;
						Cst_EK_TreeviewTag tagM = (Cst_EK_TreeviewTag) trnM.Tag;
						Cdv_Modul modul = (Cdv_Modul) tagM.Eintrag;
						if(modul.ID == pin_ModulID)
						{
							TreeNode trnEinheit = ErstelleEinheitKnoten(pin_einheit);
							//AktualisiereDatencacheEinheit(trnEinheit, false);
							trnM.Nodes.Add(trnEinheit);
						}					
					}
				}
			}
		}

		public void EntferneEinheitVomModul(Cdv_Einheit pin_einheit, int pin_ModulID)
		{
			TreeNode trnModule = this.trv_Einsatzmanager.Nodes[1];
			IEnumerator ieModule = trnModule.Nodes.GetEnumerator();
			while(ieModule.MoveNext())
			{
				TreeNode trnM = (TreeNode) ieModule.Current;
				Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trnM.Tag;
				Cdv_Modul modul = (Cdv_Modul) tag.Eintrag;
				if(modul.ID == pin_ModulID)
				{
					IEnumerator ieEinheiten = trnM.Nodes.GetEnumerator();
					while(ieEinheiten.MoveNext())
					{
						TreeNode trnEinheit = (TreeNode) ieEinheiten.Current;
						Cdv_Einheit einheit = (Cdv_Einheit) ((Cst_EK_TreeviewTag) trnEinheit.Tag).Eintrag;
						if(einheit.ID == pin_einheit.ID)
						{
							this.trv_Einsatzmanager.Nodes.Remove(trnEinheit);
							//AktualisiereDatencacheEinheit(trnEinheit, true);
							break;
						}
					}
				}
			}
		}

		public void EntferneEinheitVomModulImESP(Cdv_Einheit pin_einheit, int pin_ModulID, int pin_espID)
		{
			TreeNode trnEsp = this.trv_Einsatzmanager.Nodes[0];
			IEnumerator ieEsp = trnEsp.Nodes.GetEnumerator();
			while(ieEsp.MoveNext())
			{
				TreeNode trnE = (TreeNode) ieEsp.Current;
				Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trnE.Tag;
				Cdv_Einsatzschwerpunkt esp = (Cdv_Einsatzschwerpunkt) tag.Eintrag;
				if(esp.ID == pin_espID)
				{
					TreeNodeCollection trnModule = trnE.Nodes;
					//TreeNode trnModule = trnE.Nodes[0].Nodes;
					IEnumerator ieModule = trnModule.GetEnumerator();
					while(ieModule.MoveNext())
					{
						TreeNode trnM = (TreeNode) ieModule.Current;
						Cst_EK_TreeviewTag tagM = (Cst_EK_TreeviewTag) trnM.Tag;
						Cdv_Modul modul = (Cdv_Modul) tagM.Eintrag;
						if(modul.ID == pin_ModulID)
						{
							IEnumerator ieEinheiten = trnM.Nodes.GetEnumerator();
							while(ieEinheiten.MoveNext())
							{
								TreeNode trnEinheit = (TreeNode) ieEinheiten.Current;
								Cdv_Einheit einheit = (Cdv_Einheit) ((Cst_EK_TreeviewTag) trnEinheit.Tag).Eintrag;
								if(einheit.ID == pin_einheit.ID)
								{
									this.trv_Einsatzmanager.Nodes.Remove(trnEinheit);
									//		AktualisiereDatencacheEinheit(trnEinheit, true);
									break;
								}
							}							
						}					
					}
				}
			}
		}

		public bool AktualisiereTVEinheitModul(Cdv_Einheit pin_einheit, Cdv_Einheit[] einheitenAlt, Cdv_Einheit[] einheitenNeu)
		{
			Cdv_Einheit einheitAlt = null;
			Cdv_Einheit einheitNeu = null;
			foreach(Cdv_Einheit einheit in einheitenAlt)
			{
				if(einheit.ID == pin_einheit.ID)
				{
					einheitAlt = einheit;
					break;
				}
			}
			foreach(Cdv_Einheit einheit in einheitenNeu)
			{
				if(einheit.ID == pin_einheit.ID)
				{
					einheitNeu = einheit;
					break;
				}
			}
			if(einheitAlt == null || einheitNeu == null)
				return(false);
			if(einheitAlt.ModulID == 0 && einheitNeu.ModulID != 0)
			{
				OrdneEinheitModulHinzu(einheitNeu, einheitNeu.ModulID);
				if(einheitNeu.EinsatzschwerpunktID != 0)
					OrdneEinheitModulImESPHinzu(einheitNeu, einheitNeu.ModulID, einheitNeu.EinsatzschwerpunktID);
			}
			if(einheitAlt.ModulID != 0 && einheitNeu.ModulID != 0)
			{
				OrdneEinheitModulHinzu(einheitNeu, einheitNeu.ModulID);
				EntferneEinheitVomModul(einheitAlt, einheitAlt.ModulID);
				if(einheitAlt.EinsatzschwerpunktID != 0)
					EntferneEinheitVomModulImESP(einheitAlt, einheitAlt.ModulID, einheitAlt.EinsatzschwerpunktID);
				if(einheitNeu.EinsatzschwerpunktID != 0)
					OrdneEinheitModulImESPHinzu(einheitNeu, einheitNeu.ModulID, einheitNeu.EinsatzschwerpunktID);
			}
			return(true);
		}

		public void AktualisiereTVEinheit(int[] pin_IDs)
		{
			int EinheitID;
			Cdv_Einheit[] einheitenAlt = (Cdv_Einheit[]) this._Cst_EK.AlleEinheiten.Clone();
			Cdv_Einheit[] einheitenNeu = (Cdv_Einheit[]) this._Cst_EK.HoleAlleEinheiten();	
			for(int i = 0; i < pin_IDs.Length; i++)
			{
				EinheitID=pin_IDs[i];
				bool bGefunden = false;
				bool bModulModifiziert = false;
				ArrayList alTemp = new ArrayList(this._TreeNodeReferenzen);
				foreach(Cst_EK_TreeviewReferenceItem item in alTemp)
				{
					if(item.PelsObjectID == EinheitID)
					{
						Cdv_Einheit einheit = this._Cst_EK.HoleEinheit(EinheitID);
						(item.TreeNodeReferenziert.Tag as Cst_EK_TreeviewTag).Eintrag = einheit;
						item.TreeNodeReferenziert.Text = einheit.Name;
						this.AktualisiereTVEinheitHelfer(einheit,einheitenAlt,einheitenNeu);
						this.AktualisiereTVEinheitKfz(einheit, einheitenAlt, einheitenNeu);
						if(!bModulModifiziert)
							bModulModifiziert = this.AktualisiereTVEinheitModul(einheit, einheitenAlt, einheitenNeu);
						bGefunden = true;
						//break;
					}
				}
				if(!bGefunden)
				{
					Cdv_Einheit einheit = this._Cst_EK.HoleEinheit(EinheitID);
					TreeNode trn_myEinheit = this.FülleEinheitMitDaten(einheit);
					trn_myEinheit.SelectedImageIndex = 16;
					trn_myEinheit.ImageIndex = 16;
					trn_myEinheit.Text = einheit.Name;
					trn_myEinheit.Tag = new Cst_EK_TreeviewTag();
					(trn_myEinheit.Tag as Cst_EK_TreeviewTag).Eintrag = einheit;
					(trn_myEinheit.Tag as Cst_EK_TreeviewTag).Kontextmenue = this.ctx_Einheiten;
					(trn_myEinheit.Tag as Cst_EK_TreeviewTag).Type = einheit.GetType();
					Cst_EK_TreeviewReferenceItem refItem = new Cst_EK_TreeviewReferenceItem(einheit.ID, trn_myEinheit);
					this._TreeNodeReferenzen.Add(refItem);
					this.trv_Einsatzmanager.Nodes[2].Nodes.Add(trn_myEinheit);
					if(einheit.ModulID != 0)
					{
						ArrayList myTmpReferenz = new ArrayList(this._TreeNodeReferenzen);
						foreach(Cst_EK_TreeviewReferenceItem item in myTmpReferenz)
						{
							if(item.PelsObjectID == einheit.ModulID)
							{
								TreeNode trn_Temp = (TreeNode) trn_myEinheit.Clone();
								Cst_EK_TreeviewReferenceItem refItemNeu = new Cst_EK_TreeviewReferenceItem(einheit.ID, trn_Temp);
								this._TreeNodeReferenzen.Add(refItemNeu);
								item.TreeNodeReferenziert.Nodes[1].Nodes.Add(trn_Temp);
							}
						}
					}
				}
			}	
			this._Cst_EK.AktualisiereEinheiten();
			this._Cst_EK.AktualisiereModule();
		}

		private bool AktualisiereTVModulESP(Cdv_Modul pin_modul, Cdv_Modul[] moduleAlt, Cdv_Modul[] moduleNeu)
		{
			Cdv_Modul modulAlt = null;
			Cdv_Modul modulNeu = null;
			foreach(Cdv_Modul modul in moduleAlt)
			{
				if(modul.ID == pin_modul.ID)
				{
					modulAlt = modul;
					break;
				}
			}
			foreach(Cdv_Modul modul in moduleNeu)
			{
				if(modul.ID == pin_modul.ID)
				{
					modulNeu = modul;
					break;
				}
			}
			if(modulAlt == null || modulNeu == null)
				return(false);
			if(modulAlt.EinsatzschwerpunktID == 0 && modulNeu.EinsatzschwerpunktID != 0)
			{
				OrdneModulImESPHinzu(modulNeu, modulNeu.EinsatzschwerpunktID);
			}
			if(modulAlt.EinsatzschwerpunktID != 0 && modulNeu.EinsatzschwerpunktID != 0)
			{
				OrdneModulImESPHinzu(modulNeu, modulNeu.EinsatzschwerpunktID);
				EntferneModulVonESP(modulAlt, modulAlt.EinsatzschwerpunktID);
			}
			return(true);
		}

		private void OrdneModulImESPHinzu(Cdv_Modul pin_modul, int pin_iEspID)
		{
			TreeNode trnEsp = this.trv_Einsatzmanager.Nodes[0];
			IEnumerator ieEsp = trnEsp.Nodes.GetEnumerator();
			while(ieEsp.MoveNext())
			{
				TreeNode trnE = (TreeNode) ieEsp.Current;
				Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trnE.Tag;
				Cdv_Einsatzschwerpunkt esp = (Cdv_Einsatzschwerpunkt) tag.Eintrag;
				if(esp.ID == pin_iEspID)
				{
					TreeNode trnModul = ErstelleModulKnoten(pin_modul);
					//AktualisiereDatencacheModul(trnModul, false);
					trnE.Nodes.Add(trnModul);
				}
			}
		}

		private void EntferneModulVonESP(Cdv_Modul pin_modul, int pin_iEspID)
		{
			TreeNode trnEsp = this.trv_Einsatzmanager.Nodes[0];
			IEnumerator ieEsp = trnEsp.Nodes.GetEnumerator();
			while(ieEsp.MoveNext())
			{
				TreeNode trnE = (TreeNode) ieEsp.Current;
				Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trnE.Tag;
				Cdv_Einsatzschwerpunkt esp = (Cdv_Einsatzschwerpunkt) tag.Eintrag;
				if(esp.ID == pin_iEspID)
				{
					TreeNodeCollection trnModule = trnE.Nodes;
					//TreeNode trnModule = trnE.Nodes[0].Nodes;
					IEnumerator ieModule = trnModule.GetEnumerator();
					while(ieModule.MoveNext())
					{
						TreeNode trnM = (TreeNode) ieModule.Current;
						Cst_EK_TreeviewTag tagM = (Cst_EK_TreeviewTag) trnM.Tag;
						Cdv_Modul modul = (Cdv_Modul) tagM.Eintrag;
						if(modul.ID == pin_modul.ID)
						{
							this.trv_Einsatzmanager.Nodes.Remove(trnM);
							//	AktualisiereDatencacheModul(trnM, true);
							break;
						}
					}
				}
			}
		}

		private TreeNode ErstelleModulKnoten(Cdv_Modul myModul)
		{
			TreeNode trn_myTreeNode= new TreeNode();
			trn_myTreeNode.Text=myModul.Modulname;
			trn_myTreeNode.SelectedImageIndex = 16;
			trn_myTreeNode.ImageIndex = 16;
			trn_myTreeNode.Tag=new Cst_EK_TreeviewTag();
			(trn_myTreeNode.Tag as Cst_EK_TreeviewTag).Kontextmenue=this.ctx_abstrakt_Module;
			(trn_myTreeNode.Tag as Cst_EK_TreeviewTag).Eintrag=myModul;
			this._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(myModul.ID,trn_myTreeNode));
			(trn_myTreeNode.Tag as Cst_EK_TreeviewTag).Type=myModul.GetType();
			trn_myTreeNode = FuegeUnterknotenHinzu(trn_myTreeNode, EinheitenZuModul(myModul.ID));

			return trn_myTreeNode;
		}

		private void AktualisiereDatencacheModul(TreeNode pin_trnBase, bool pin_bLoeschen)
		{
			if(pin_bLoeschen)
			{
				EntferneAlleUnterknoten(pin_trnBase);
				ArrayList NodeListe = new ArrayList(this._TreeNodeReferenzen);
				foreach(Cst_EK_TreeviewReferenceItem item in NodeListe)
				{
					if(item.TreeNodeReferenziert == pin_trnBase)
						this._TreeNodeReferenzen.Remove(item);
				}					
			}
			else
			{

			}
		}

		public void AktualisiereTVModul(int[] pin_IDs)
		{
			int ModulID;
			Cdv_Modul[] moduleAlt = (Cdv_Modul[]) this._Cst_EK.AlleModule.Clone();
			Cdv_Modul[] moduleNeu = (Cdv_Modul[]) this._Cst_EK.HoleAlleModule();	
			for(int i = 0; i < pin_IDs.Length; i++)
			{
				ModulID=pin_IDs[i];
				bool bGefunden = false;
				bool bEspAktualisiert = false;
				ArrayList alTemp = new ArrayList(this._TreeNodeReferenzen);
				foreach(Cst_EK_TreeviewReferenceItem item in alTemp)
				{
					if(item.PelsObjectID == ModulID)
					{
						Cdv_Modul modul = this._Cst_EK.HoleModul(ModulID);
						(item.TreeNodeReferenziert.Tag as Cst_EK_TreeviewTag).Eintrag = modul;
						item.TreeNodeReferenziert.Text = modul.Modulname;
						if(!bEspAktualisiert)
							bEspAktualisiert = AktualisiereTVModulESP(modul, moduleAlt, moduleNeu);
						bGefunden = true;
						//break;
					}
				}
				if(!bGefunden)
				{
					Cdv_Modul modul = this._Cst_EK.HoleModul(ModulID);
					TreeNode trn_Modul = new TreeNode();
					trn_Modul.SelectedImageIndex = 16;
					trn_Modul.ImageIndex = 16;
					trn_Modul.Text = modul.Modulname;
					trn_Modul.Tag = new Cst_EK_TreeviewTag();
					(trn_Modul.Tag as Cst_EK_TreeviewTag).Eintrag = modul;
					(trn_Modul.Tag as Cst_EK_TreeviewTag).Kontextmenue = this.ctx_Module;
					(trn_Modul.Tag as Cst_EK_TreeviewTag).Type = modul.GetType();
					Cst_EK_TreeviewReferenceItem refItem = new Cst_EK_TreeviewReferenceItem(modul.ID, trn_Modul);
					this._TreeNodeReferenzen.Add(refItem);
					this.trv_Einsatzmanager.Nodes[1].Nodes.Add(trn_Modul);
					if(modul.EinsatzschwerpunktID != 0)
					{
						ArrayList myTmpReferenz = new ArrayList(this._TreeNodeReferenzen);
						foreach(Cst_EK_TreeviewReferenceItem item in myTmpReferenz)
						{
							if(item.PelsObjectID == modul.EinsatzschwerpunktID)
							{
								TreeNode trn_Temp = (TreeNode) trn_Modul.Clone();
								Cst_EK_TreeviewReferenceItem refItemNeu = new Cst_EK_TreeviewReferenceItem(modul.ID, trn_Temp);
								this._TreeNodeReferenzen.Add(refItemNeu);
								item.TreeNodeReferenziert.Nodes.Add(trn_Temp);
							}
						}
					}
				}
			}	
			this._Cst_EK.AktualisiereEsps();
			this._Cst_EK.AktualisiereModule();
		}

		public void AktualisiereTVESP(int[] pin_IDs)
		{
			int espID;
			for(int i = 0; i < pin_IDs.Length; i++)
			{
				espID=pin_IDs[i];
				bool bGefunden = false;
				foreach(Cst_EK_TreeviewReferenceItem item in this._TreeNodeReferenzen)
				{
					if(item.PelsObjectID == espID)
					{
						Cdv_Einsatzschwerpunkt esp = this._Cst_EK.HoleESP(espID);
						(item.TreeNodeReferenziert.Tag as Cst_EK_TreeviewTag).Eintrag = esp;
						item.TreeNodeReferenziert.Text = esp.Bezeichnung;
						bGefunden = true;
					}
				}
				if(!bGefunden)
				{
					Cdv_Einsatzschwerpunkt esp = this._Cst_EK.HoleESP(espID);
					TreeNode trn_myESP = new TreeNode();
					trn_myESP.SelectedImageIndex = 19;
					trn_myESP.ImageIndex = 19;
					trn_myESP.Text = esp.Bezeichnung;
					trn_myESP.Tag = new Cst_EK_TreeviewTag();
					(trn_myESP.Tag as Cst_EK_TreeviewTag).Eintrag = esp;
					(trn_myESP.Tag as Cst_EK_TreeviewTag).Kontextmenue = this.ctx_Einsatzschwerpunkte;
					(trn_myESP.Tag as Cst_EK_TreeviewTag).Type = esp.GetType();
					Cst_EK_TreeviewReferenceItem refItem = new Cst_EK_TreeviewReferenceItem(esp.ID, trn_myESP);
					this._TreeNodeReferenzen.Add(refItem);		
					this.trv_Einsatzmanager.Nodes[0].Nodes.Add(trn_myESP);
				}
			}
		}

		#endregion

		#endregion

		/// <summary>
		/// Wenn die Menge aller Helfer verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AktualisiereHelfer()
		{
			this._Einheit.AktualisiereHelfer();
		}
		/// <summary>
		/// Wenn die Menge aller OV verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>		
		public void AktualisiereOV()
		{
			this._Einheit.AktualisiereOV();
		}
		/// <summary>
		/// Wenn die Menge aller Material verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>		
		public void AktualisiereMaterial()
		{
			this._Einheit.AktualisiereMaterial();
		}

		public void AktualisiereOVHelfer()
		{
			this._Helfer.AktualisiereOV();
		}

		public void AktualisiereLeiterESP()
		{
			this._ESP.AktualisiereLeiter();
		}

		public void AktualisiereFahrerKfz()
		{
			this._Kfz.AktualisiereFahrer();
		}
		#endregion

		private void tabPage_Hauptseite_Click(object sender, System.EventArgs e)
		{
		
		}

		public void ESPBezeichnungFuellen(Cdv_Einsatzschwerpunkt[] pin_espMenge)
		{
			
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			int count=0;
			this.trv_Einsatzmanager.BeginUpdate();
			foreach (object obj in this._TreeNodeReferenzen)
			{
				count++;
				if (obj is Cst_EK_TreeviewReferenceItem)
				{
					((obj as Cst_EK_TreeviewReferenceItem).TreeNodeReferenziert).Text=count.ToString();
				}
			}
			this.trv_Einsatzmanager.EndUpdate();

		}

		private void mI_ctx_Helferverpflegung_Click(object sender, System.EventArgs e)
		{
			if (((this.trv_Einsatzmanager.SelectedNode.Tag as Cst_EK_TreeviewTag).Type)==typeof(Cdv_Helfer))
			{
				((this.trv_Einsatzmanager.SelectedNode.Tag as Cst_EK_TreeviewTag).Eintrag as Cdv_Helfer).LetzteVerfplegung=DateTime.Now;
				this._Cst_EK.SpeichereHelfer(((this.trv_Einsatzmanager.SelectedNode.Tag as Cst_EK_TreeviewTag).Eintrag as Cdv_Helfer));
			}
		}

		private void mi_Module_HelferWurdenVerpflegt_Click(object sender, System.EventArgs e)
		{
			if (((this.trv_Einsatzmanager.SelectedNode.Tag as Cst_EK_TreeviewTag).Type)==typeof(Cdv_Modul))
			{
				this._Cst_EK.GeneriereSystemereignisVerpflegung((this.trv_Einsatzmanager.SelectedNode.Tag as Cst_EK_TreeviewTag).Eintrag as Cdv_Modul);
			}
		}

		private void mI_abstrakt_ESP_NeuenESPanlegen_Click_1(object sender, System.EventArgs e)
		{
			Cdv_Einsatzschwerpunkt myESP = new Cdv_Einsatzschwerpunkt("Neuer ESP",this._Cst_EK.Einsatz.ID);
			this._ESP.LadeESP(myESP);
			int i_tabindex = this.tabctrl_EK.TabPages.IndexOf(this.tabPage_Einsatzschwerpunkt);
			this.tabctrl_EK.SelectedIndex = i_tabindex;		
		}

		private void mI_abstrakt_Modul_NeuesModulAnlegen_Click_1(object sender, System.EventArgs e)
		{
			Cdv_Modul myModul = new Cdv_Modul("Neues Modul");
			this._Module.LadeModul(myModul);
			int i_tabindex = this.tabctrl_EK.TabPages.IndexOf(this.tabPage_Einsatzschwerpunkt);
			this.tabctrl_EK.SelectedIndex = i_tabindex;		
		}

		private void mI_abstrakt_Einheiten_NeueEinheitAnlegen_edit_Click(object sender, System.EventArgs e)
		{
			Cdv_Einheit myEinheit = new Cdv_Einheit("Neue Einheit",Tdv_Kraeftestatus.Angefordert,"HEROS",0,0,1,1);
			this._Einheit.LadeEinheit(myEinheit);
			int i_tabindex = this.tabctrl_EK.TabPages.IndexOf(this.tabpage_Einheit);
			this.tabctrl_EK.SelectedIndex = i_tabindex;		

		}

		private void mI_abstrakt_Einheiten_NeueEinheitAnlegen_creatempty_Click(object sender, System.EventArgs e)
		{
			Cdv_Helfer myHelferEF = new Cdv_Helfer("Einheit","Führer",Tdv_Kraeftestatus.Angefordert,Tdv_Helferstatus.AktiverHelfer);
			myHelferEF=this._Cst_EK.SpeichereundReturniereHelfer(myHelferEF);

			Cdv_Helfer myHelferStellEF = new Cdv_Helfer("Einheit","Führer 2",Tdv_Kraeftestatus.Angefordert,Tdv_Helferstatus.AktiverHelfer);
			myHelferStellEF=this._Cst_EK.SpeichereundReturniereHelfer(myHelferStellEF);

			Cdv_Einheit myEinheit = new Cdv_Einheit("Neue Einheit",Tdv_Kraeftestatus.Angefordert,"HEROS",myHelferEF.ID,myHelferStellEF.ID,1,1);
			myEinheit.HelferIDMenge=new int[2];
			myEinheit.HelferIDMenge[0]=myHelferEF.ID;
			myEinheit.HelferIDMenge[1]=myHelferStellEF.ID;

			this._Cst_EK.SpeichereEinheit(myEinheit);
		}

		private void ctx_Helfer_Popup(object sender, System.EventArgs e)
		{
		
		}

		private void mI_Helfer_WurdeVerpflegt_Click(object sender, System.EventArgs e)
		{
			if ((sender as TreeNode).Tag is Cst_EK_TreeviewTag)
			{
				if (((sender as TreeNode).Tag as Cst_EK_TreeviewTag).Eintrag is Cdv_Helfer)
				{
					(((sender as TreeNode).Tag as Cst_EK_TreeviewTag).Eintrag as Cdv_Helfer).LetzteVerfplegung=DateTime.Now;
					this._Cst_EK.SpeichereHelfer((((sender as TreeNode).Tag as Cst_EK_TreeviewTag).Eintrag as Cdv_Helfer));
				}
			}
		}

		private void mI_abstrakt_Helfer_Helfer_anlegen_1_Click(object sender, System.EventArgs e)
		{
			Cdv_Helfer myHelfer = new Cdv_Helfer("Helfer","Anonton",Tdv_Kraeftestatus.Angefordert,Tdv_Helferstatus.AktiverHelfer);
			this._Helfer.LadeHelfer(myHelfer);
			int i_tabindex = this.tabctrl_EK.TabPages.IndexOf(this.tabPage_Helfer);
			this.tabctrl_EK.SelectedIndex = i_tabindex;		
		}
	}
}


