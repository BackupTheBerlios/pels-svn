using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pELS.GUI.AGT
{
	#region Dokumentation
	  
	 /**				aktuelle Version: 0.3 alexG
	INFO:
		- kapselt die UserControl zum erfassen einer AGT-Druck-Meldung im Einsatz
	
    Erläuterung:
	Implementiert die GUI zum Softwarebauelement AGT (Atemgeräteträger)
	Vorlage für die GUI ist die Atemschutzüberwachung.pdf des THW.

	erstellt von:	alexG					am: 18.November.2004
	geändert von:	alexG					am:  30.November.2004
	geändert von:   alexG					am:  1.Dezember.2004
	geändert von:							am:  
	review von:								am:
	getestet von:							am:

	aktuelle Version: 0.3 alpha

	History:
	
	1.12.	ade wirf nun event (EnabledChanged)
			In usc_AGT wird dieser als anlass genommen um
			die Werte in das Datatable zu schreiben
			Nur für Trupp1 implementiert -> Trupp2, STrupp analog			
	 
	30.11.
			Festlegung außerhalb der Namenskonvention
			ade = AGT_Druck_Eintrag
			
			//alles ums eingesetzte datagrid
			- region DataGrid
			
			//Einsatzdaten aus EingabeMaske Laden
			Einsatzdaten_Laden();
			
			MeldungsSteuerElementeLaden();
			//dabei werden Steuerelemente vom Typ AGT_Druck_Eintrag generiert
			// und plaziert und zwar auf den gbx für die Trupps
			
			//TODO:laufender einsatz als aktive TabPage wählen
			
	Hinweise/Bekannte Bugs:
	
	
	**/		
	#endregion
	public class usc_AGT : System.Windows.Forms.UserControl
	{
		#region Klassenvariablen
		private System.Windows.Forms.TabControl tabctrl_AGT;
		private System.Windows.Forms.Label lbl_AGT_Einsatz;
		private System.Windows.Forms.TabPage tabpage_AGT_Einatz_erstellen;
		private System.Windows.Forms.GroupBox gbx_AGT_Einsatz_erstellen_Header;
		private System.Windows.Forms.DateTimePicker dtp_AGT_Einsatz_Erstellen_Header_Datum;
		private System.Windows.Forms.Label lbl__AGT_Einsatz_Erstellen_Header_Datum;
		private System.Windows.Forms.ComboBox cbo_AGT_Einsatz_erstellen_Header_Einsatzschwerpunkt;
		private System.Windows.Forms.TabPage tabpage_AGTOperator_inaktiv;
		private System.Windows.Forms.RichTextBox rtb_AGTOperator_inaktiv;
		private System.Windows.Forms.GroupBox gbx_EinsatzTrupps_Trupp1;
		private System.Windows.Forms.ComboBox cbo_Einsatztrupp_Trupp1_Truppführer;
		private System.Windows.Forms.Label lbl_Trupp1_Funkrufname;
		private System.Windows.Forms.GroupBox gbx_PA_Nr;
		private System.Windows.Forms.GroupBox gbx_EinsatzTrupps_Trupp2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ComboBox cbo_Einsatztrupp_Trupp2_Truppmann3;
		private System.Windows.Forms.ComboBox cbo_Einsatztrupp_Trupp2_Truppmann2;
		private System.Windows.Forms.ComboBox cbo_Einsatztrupp_Trupp2_Truppmann1;
		private System.Windows.Forms.ComboBox cbo_Einsatztrupp_Trupp2_Truppführer;
		private System.Windows.Forms.Label lbl_Trupp2_Funkrufname;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.ComboBox cbo_Einsatztrupp_Sicherungstrupp_Truppführer;
		private System.Windows.Forms.Label lbl_Sicherungstrupp_Funkrufname;
		private System.Windows.Forms.Button btn_Einsatz_abbrechen;
		private System.Windows.Forms.CheckBox cbx_Sicherungstrupp;
		private System.Windows.Forms.CheckBox cbx_Trupp2;
		private System.Windows.Forms.CheckBox cbx_Trupp1;
		private System.Windows.Forms.Button btn_AGT_Einsatz_anlegen;
		private System.Windows.Forms.TabPage tabpage_AGT_aktueller_Einsatz;
		private System.Windows.Forms.Button btn_Einsatz_abschließen;
		private System.Windows.Forms.GroupBox gbx_allgemeine_Informationen;
		private System.Windows.Forms.GroupBox gbx_Einsatz_Trupp1;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp1_Truppfuehrer_DoppelPunkt;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp1_Funkrufname_Doppelpunkt;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp1_Funkrufname;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp1_Truppfuehrer;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp1_Druck_Doppelpunkt;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp1_Zeit_Doppelpunkt;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp1_Einsatzziel_Doppelpunkt;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp1_Rueckzug_Doppelpunkt;
		private System.Windows.Forms.DateTimePicker dtp_Einsatz_Trupp1_Einsatzziel_Doppelpunkt;
		private System.Windows.Forms.DateTimePicker dtp_Einsatz_Trupp1_Rueckzug_Doppelpunkt;
		private System.Windows.Forms.GroupBox gbx_Einsatz_Trupp1_Einsatzziel_Rückzug;
		private System.Windows.Forms.RichTextBox txt_Einsatz_Trupp1_Kommentar;
		private System.Windows.Forms.TabControl tabctrl_AGT_Einsatz;
		private System.Windows.Forms.TabPage tabpage_AGT_Einsatz_Trupp1;
		private System.Windows.Forms.TabPage tabpage_AGT_Einsatz_Trupp2;
		private System.Windows.Forms.TabPage tabpage_AGT_Einsatz_Sicherungtrupp;
		private System.Windows.Forms.GroupBox gbx_Einsatz_Trupp2;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp2_Truppfuehrer;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp2_Funkrufname;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp2_Funkrufname_Doppelpunkt;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp2_Truppfuehrer_DoppelPunkt;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp2_Druck_Doppelpunkt;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp2_Zeit_Doppelpunkt;
		private System.Windows.Forms.GroupBox gbx_Einsatz_Trupp2_Einsatzziel_Rückzug;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp2_Rueckzug_Doppelpunkt;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp2_Einsatzziel_Doppelpunkt;
		private System.Windows.Forms.DateTimePicker dtp_Einsatz_Trupp2_Rueckzug_Doppelpunkt;
		private System.Windows.Forms.DateTimePicker dtp_Einsatz_Trupp2_Einsatzziel_Doppelpunkt;
		private System.Windows.Forms.RichTextBox txt_Einsatz_Trupp2_Kommentar;
		private System.Windows.Forms.GroupBox gbx_Einsatz_Sicherungstrupp;
		private System.Windows.Forms.Label lbl_Einsatz_Sicherungstrupp_Funkrufname;
		private System.Windows.Forms.Label lbl_Einsatz_Sicherungstrupp_Funkrufname_Doppelpunkt;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp2_Sicherungstrupp_Truppfuehrer_DoppelPunkt;
		private System.Windows.Forms.Label lbl_Einsatz_Sicherungtrupp_Druck_Doppelpunkt;
		private System.Windows.Forms.Label lbl_Einsatz_Sicherungstrupp_Zeit_Doppelpunkt;
		private System.Windows.Forms.GroupBox gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug;
		private System.Windows.Forms.Label lbl_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt;
		private System.Windows.Forms.Label lbl_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt;
		private System.Windows.Forms.DateTimePicker dtp_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt;
		private System.Windows.Forms.DateTimePicker dtp_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt;
		private System.Windows.Forms.RichTextBox txt_Einsatz_Sicherungstrupp_Kommentar;
		private System.Windows.Forms.DataGrid dgrid_Einsatz_letzte_Informationen;
		private System.Windows.Forms.ComboBox cbo_Einsatztrupp_Trupp1_Truppmann3;
		private System.Windows.Forms.ComboBox cbo_Einsatztrupp_Trupp1_Truppmann2;
		private System.Windows.Forms.ComboBox cbo_Einsatztrupp_Trupp1_Truppmann1;
		private System.Windows.Forms.ComboBox cbo_Einsatztrupp_Sicherungstrupp_Truppmann1;
		private System.Windows.Forms.ComboBox cbo_Einsatztrupp_Sicherungstrupp_Truppmann3;
		private System.Windows.Forms.ComboBox cbo_Einsatztrupp_Sicherungstrupp_Truppmann2;
		private System.Windows.Forms.TextBox txt_Trupp1_PA_Nr_Truppmann2;
		private System.Windows.Forms.TextBox txt_Trupp1_PA_Nr_Truppmann1;
		private System.Windows.Forms.TextBox txt_Trupp1_PA_Nr_Truppfuehrer;
		private System.Windows.Forms.TextBox txt_Trupp1_Funkrufname;
		private System.Windows.Forms.TextBox txt_Trupp1_PA_Nr_Truppmann3;
		private System.Windows.Forms.TextBox txt_Trupp2_PA_Nr_Truppmann3;
		private System.Windows.Forms.TextBox txt_Trupp2_PA_Nr_Truppmann2;
		private System.Windows.Forms.TextBox txt_Trupp2_PA_Nr_Truppmann1;
		private System.Windows.Forms.TextBox txt_Trupp2_PA_Nr_Truppfuehrer;
		private System.Windows.Forms.TextBox txt_Trupp2_Funkrufname;
		private System.Windows.Forms.TextBox txt_Sicherungstrupp_PA_Nr_Truppmann3;
		private System.Windows.Forms.TextBox txt_Sicherungstrupp_PA_Nr_Truppmann2;
		private System.Windows.Forms.TextBox txt_Sicherungstrupp_PA_Nr_Truppmann1;
		private System.Windows.Forms.TextBox txt_Sicherungstrupp_PA_Nr_Truppfuehrer;
		private System.Windows.Forms.TextBox txt_Sicherungstrupp_Funkrufname;
		private System.Windows.Forms.Label lbl_AGT_Einsatz_erstellen_Header_Verantwortlicher_DoppelPunkt;
		private System.Windows.Forms.TextBox txt_AGT_Einsatz_erstellen_Header_Verantwortlicher;
		private System.Windows.Forms.Label lbl_aktueller_Einsatz__Verantwortlicher_DoppelPunkt;
		private System.Windows.Forms.Label lbl_aktueller_Einsatz_Verantwortlicher;
		private System.Windows.Forms.Label lbl_aktueller_Einsatz_Einsatzschwerpunkt_DoppelPunkt;
		private System.Windows.Forms.Label lbl_aktueller_Einsatz_Einsatzschwerpunkt;
		private System.Windows.Forms.Label lbl_aktueller_Einsatz_Datum;
		private System.Windows.Forms.CheckBox cbx_Einsatz_Trupp1_EinsatzZiel_DatumJetzt;
		private System.Windows.Forms.CheckBox cbx_Einsatz_Trupp1_Rückzug_DatumJetzt;
		private System.Windows.Forms.ContextMenu ctx_ZeitAktualisieren;
		private System.Windows.Forms.MenuItem mI_ZeitAktualisieren_Jetzt;
		private System.ComponentModel.IContainer components = null;
		private DateTimePicker _dtp_unterCuror;
		private AGT_Informationen _myAGT_Informationen;	

		private System.Windows.Forms.CheckBox cbx_Einsatz_Trupp2_Rueckzug_DatumJetzt;
		private System.Windows.Forms.CheckBox cbx_Einsatz_Trupp2_EinsatzZiel_DatumJetzt;
		private System.Windows.Forms.CheckBox cbx_Einsatz_Sicherungstrupp_Rueckzug_DatumJetzt;
		private System.Windows.Forms.CheckBox cbx_Einsatz_Sicherungstrupp_EinsatzZiel_DatumJetzt;
		private System.Windows.Forms.Label lbl_Einsatz_Sicherungstrupp_Truppfuehrer;
		private pELS.GUI.AGT.AGT_Druck_Eintrag ade_Trupp1_Start;
		private pELS.GUI.AGT.AGT_Druck_Eintrag ade_Trupp1_Anschliessen;
		private pELS.GUI.AGT.AGT_Druck_Eintrag ade_Trupp1_10min;
		private pELS.GUI.AGT.AGT_Druck_Eintrag ade_Trupp1_20min;
		private pELS.GUI.AGT.AGT_Druck_Eintrag ade_Trupp1_30min;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp1_Kommentar;
		private pELS.GUI.AGT.AGT_Druck_Eintrag ade_Trupp2_30min;
		private pELS.GUI.AGT.AGT_Druck_Eintrag ade_Trupp2_20min;
		private pELS.GUI.AGT.AGT_Druck_Eintrag ade_Trupp2_10min;
		private pELS.GUI.AGT.AGT_Druck_Eintrag ade_Trupp2_Anschliessen;
		private pELS.GUI.AGT.AGT_Druck_Eintrag ade_Trupp2_Start;
		private pELS.GUI.AGT.AGT_Druck_Eintrag ade_Sicherungstrupp_30min;
		private pELS.GUI.AGT.AGT_Druck_Eintrag ade_Sicherungstrupp_20min;
		private pELS.GUI.AGT.AGT_Druck_Eintrag ade_Sicherungstrupp_10min;
		private pELS.GUI.AGT.AGT_Druck_Eintrag ade_Sicherungstrupp_Anschliessen;
		private pELS.GUI.AGT.AGT_Druck_Eintrag ade_Sicherungstrupp_Start;
		private System.Windows.Forms.Label lbl_Einsatz_Trupp2_Kommentar;
		private System.Windows.Forms.Label lbl_Einsatz_Sicherungstrupp_Kommentar;
		private System.Windows.Forms.DataGridTableStyle dgts_AGT_Informationen;
		private System.Windows.Forms.GroupBox gbx_EinsatzTrupps;
		private System.Windows.Forms.GroupBox gbx_EinsatzTrupps_Sicherungstrupp;
		private System.Windows.Forms.Button btn_Zeige_AGT_Informationen;
		
		#endregion
		
		#region Konstruktoren & Destruktoren
		public usc_AGT()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();


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


		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabctrl_AGT = new System.Windows.Forms.TabControl();
			this.tabpage_AGT_Einatz_erstellen = new System.Windows.Forms.TabPage();
			this.gbx_EinsatzTrupps = new System.Windows.Forms.GroupBox();
			this.cbx_Trupp1 = new System.Windows.Forms.CheckBox();
			this.cbx_Trupp2 = new System.Windows.Forms.CheckBox();
			this.cbx_Sicherungstrupp = new System.Windows.Forms.CheckBox();
			this.gbx_EinsatzTrupps_Trupp1 = new System.Windows.Forms.GroupBox();
			this.gbx_PA_Nr = new System.Windows.Forms.GroupBox();
			this.txt_Trupp1_PA_Nr_Truppmann3 = new System.Windows.Forms.TextBox();
			this.txt_Trupp1_PA_Nr_Truppmann2 = new System.Windows.Forms.TextBox();
			this.txt_Trupp1_PA_Nr_Truppmann1 = new System.Windows.Forms.TextBox();
			this.txt_Trupp1_PA_Nr_Truppfuehrer = new System.Windows.Forms.TextBox();
			this.cbo_Einsatztrupp_Trupp1_Truppmann3 = new System.Windows.Forms.ComboBox();
			this.cbo_Einsatztrupp_Trupp1_Truppmann2 = new System.Windows.Forms.ComboBox();
			this.cbo_Einsatztrupp_Trupp1_Truppmann1 = new System.Windows.Forms.ComboBox();
			this.cbo_Einsatztrupp_Trupp1_Truppführer = new System.Windows.Forms.ComboBox();
			this.txt_Trupp1_Funkrufname = new System.Windows.Forms.TextBox();
			this.lbl_Trupp1_Funkrufname = new System.Windows.Forms.Label();
			this.gbx_EinsatzTrupps_Trupp2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txt_Trupp2_PA_Nr_Truppmann3 = new System.Windows.Forms.TextBox();
			this.txt_Trupp2_PA_Nr_Truppmann2 = new System.Windows.Forms.TextBox();
			this.txt_Trupp2_PA_Nr_Truppmann1 = new System.Windows.Forms.TextBox();
			this.txt_Trupp2_PA_Nr_Truppfuehrer = new System.Windows.Forms.TextBox();
			this.cbo_Einsatztrupp_Trupp2_Truppmann3 = new System.Windows.Forms.ComboBox();
			this.cbo_Einsatztrupp_Trupp2_Truppmann2 = new System.Windows.Forms.ComboBox();
			this.cbo_Einsatztrupp_Trupp2_Truppmann1 = new System.Windows.Forms.ComboBox();
			this.cbo_Einsatztrupp_Trupp2_Truppführer = new System.Windows.Forms.ComboBox();
			this.txt_Trupp2_Funkrufname = new System.Windows.Forms.TextBox();
			this.lbl_Trupp2_Funkrufname = new System.Windows.Forms.Label();
			this.gbx_EinsatzTrupps_Sicherungstrupp = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.txt_Sicherungstrupp_PA_Nr_Truppmann3 = new System.Windows.Forms.TextBox();
			this.txt_Sicherungstrupp_PA_Nr_Truppmann2 = new System.Windows.Forms.TextBox();
			this.txt_Sicherungstrupp_PA_Nr_Truppmann1 = new System.Windows.Forms.TextBox();
			this.txt_Sicherungstrupp_PA_Nr_Truppfuehrer = new System.Windows.Forms.TextBox();
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann3 = new System.Windows.Forms.ComboBox();
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann2 = new System.Windows.Forms.ComboBox();
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann1 = new System.Windows.Forms.ComboBox();
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppführer = new System.Windows.Forms.ComboBox();
			this.txt_Sicherungstrupp_Funkrufname = new System.Windows.Forms.TextBox();
			this.lbl_Sicherungstrupp_Funkrufname = new System.Windows.Forms.Label();
			this.gbx_AGT_Einsatz_erstellen_Header = new System.Windows.Forms.GroupBox();
			this.txt_AGT_Einsatz_erstellen_Header_Verantwortlicher = new System.Windows.Forms.TextBox();
			this.cbo_AGT_Einsatz_erstellen_Header_Einsatzschwerpunkt = new System.Windows.Forms.ComboBox();
			this.lbl_AGT_Einsatz_erstellen_Header_Verantwortlicher_DoppelPunkt = new System.Windows.Forms.Label();
			this.dtp_AGT_Einsatz_Erstellen_Header_Datum = new System.Windows.Forms.DateTimePicker();
			this.ctx_ZeitAktualisieren = new System.Windows.Forms.ContextMenu();
			this.mI_ZeitAktualisieren_Jetzt = new System.Windows.Forms.MenuItem();
			this.lbl__AGT_Einsatz_Erstellen_Header_Datum = new System.Windows.Forms.Label();
			this.btn_AGT_Einsatz_anlegen = new System.Windows.Forms.Button();
			this.tabpage_AGTOperator_inaktiv = new System.Windows.Forms.TabPage();
			this.rtb_AGTOperator_inaktiv = new System.Windows.Forms.RichTextBox();
			this.tabpage_AGT_aktueller_Einsatz = new System.Windows.Forms.TabPage();
			this.tabctrl_AGT_Einsatz = new System.Windows.Forms.TabControl();
			this.tabpage_AGT_Einsatz_Trupp1 = new System.Windows.Forms.TabPage();
			this.gbx_Einsatz_Trupp1 = new System.Windows.Forms.GroupBox();
			this.lbl_Einsatz_Trupp1_Kommentar = new System.Windows.Forms.Label();
			this.ade_Trupp1_30min = new pELS.GUI.AGT.AGT_Druck_Eintrag();
			this.ade_Trupp1_20min = new pELS.GUI.AGT.AGT_Druck_Eintrag();
			this.ade_Trupp1_10min = new pELS.GUI.AGT.AGT_Druck_Eintrag();
			this.ade_Trupp1_Anschliessen = new pELS.GUI.AGT.AGT_Druck_Eintrag();
			this.ade_Trupp1_Start = new pELS.GUI.AGT.AGT_Druck_Eintrag();
			this.lbl_Einsatz_Trupp1_Truppfuehrer = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Trupp1_Funkrufname = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Trupp1_Funkrufname_Doppelpunkt = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Trupp1_Truppfuehrer_DoppelPunkt = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Trupp1_Druck_Doppelpunkt = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Trupp1_Zeit_Doppelpunkt = new System.Windows.Forms.Label();
			this.gbx_Einsatz_Trupp1_Einsatzziel_Rückzug = new System.Windows.Forms.GroupBox();
			this.cbx_Einsatz_Trupp1_Rückzug_DatumJetzt = new System.Windows.Forms.CheckBox();
			this.lbl_Einsatz_Trupp1_Rueckzug_Doppelpunkt = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Trupp1_Einsatzziel_Doppelpunkt = new System.Windows.Forms.Label();
			this.dtp_Einsatz_Trupp1_Rueckzug_Doppelpunkt = new System.Windows.Forms.DateTimePicker();
			this.dtp_Einsatz_Trupp1_Einsatzziel_Doppelpunkt = new System.Windows.Forms.DateTimePicker();
			this.cbx_Einsatz_Trupp1_EinsatzZiel_DatumJetzt = new System.Windows.Forms.CheckBox();
			this.txt_Einsatz_Trupp1_Kommentar = new System.Windows.Forms.RichTextBox();
			this.tabpage_AGT_Einsatz_Trupp2 = new System.Windows.Forms.TabPage();
			this.gbx_Einsatz_Trupp2 = new System.Windows.Forms.GroupBox();
			this.lbl_Einsatz_Trupp2_Kommentar = new System.Windows.Forms.Label();
			this.ade_Trupp2_30min = new pELS.GUI.AGT.AGT_Druck_Eintrag();
			this.ade_Trupp2_20min = new pELS.GUI.AGT.AGT_Druck_Eintrag();
			this.ade_Trupp2_10min = new pELS.GUI.AGT.AGT_Druck_Eintrag();
			this.ade_Trupp2_Anschliessen = new pELS.GUI.AGT.AGT_Druck_Eintrag();
			this.ade_Trupp2_Start = new pELS.GUI.AGT.AGT_Druck_Eintrag();
			this.lbl_Einsatz_Trupp2_Truppfuehrer = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Trupp2_Funkrufname = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Trupp2_Funkrufname_Doppelpunkt = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Trupp2_Truppfuehrer_DoppelPunkt = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Trupp2_Druck_Doppelpunkt = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Trupp2_Zeit_Doppelpunkt = new System.Windows.Forms.Label();
			this.gbx_Einsatz_Trupp2_Einsatzziel_Rückzug = new System.Windows.Forms.GroupBox();
			this.cbx_Einsatz_Trupp2_Rueckzug_DatumJetzt = new System.Windows.Forms.CheckBox();
			this.cbx_Einsatz_Trupp2_EinsatzZiel_DatumJetzt = new System.Windows.Forms.CheckBox();
			this.lbl_Einsatz_Trupp2_Rueckzug_Doppelpunkt = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Trupp2_Einsatzziel_Doppelpunkt = new System.Windows.Forms.Label();
			this.dtp_Einsatz_Trupp2_Rueckzug_Doppelpunkt = new System.Windows.Forms.DateTimePicker();
			this.dtp_Einsatz_Trupp2_Einsatzziel_Doppelpunkt = new System.Windows.Forms.DateTimePicker();
			this.txt_Einsatz_Trupp2_Kommentar = new System.Windows.Forms.RichTextBox();
			this.tabpage_AGT_Einsatz_Sicherungtrupp = new System.Windows.Forms.TabPage();
			this.gbx_Einsatz_Sicherungstrupp = new System.Windows.Forms.GroupBox();
			this.lbl_Einsatz_Sicherungstrupp_Kommentar = new System.Windows.Forms.Label();
			this.ade_Sicherungstrupp_30min = new pELS.GUI.AGT.AGT_Druck_Eintrag();
			this.ade_Sicherungstrupp_20min = new pELS.GUI.AGT.AGT_Druck_Eintrag();
			this.ade_Sicherungstrupp_10min = new pELS.GUI.AGT.AGT_Druck_Eintrag();
			this.ade_Sicherungstrupp_Anschliessen = new pELS.GUI.AGT.AGT_Druck_Eintrag();
			this.ade_Sicherungstrupp_Start = new pELS.GUI.AGT.AGT_Druck_Eintrag();
			this.lbl_Einsatz_Sicherungstrupp_Truppfuehrer = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Sicherungstrupp_Funkrufname = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Sicherungstrupp_Funkrufname_Doppelpunkt = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Trupp2_Sicherungstrupp_Truppfuehrer_DoppelPunkt = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Sicherungtrupp_Druck_Doppelpunkt = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Sicherungstrupp_Zeit_Doppelpunkt = new System.Windows.Forms.Label();
			this.gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug = new System.Windows.Forms.GroupBox();
			this.lbl_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt = new System.Windows.Forms.Label();
			this.lbl_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt = new System.Windows.Forms.Label();
			this.dtp_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt = new System.Windows.Forms.DateTimePicker();
			this.dtp_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt = new System.Windows.Forms.DateTimePicker();
			this.cbx_Einsatz_Sicherungstrupp_EinsatzZiel_DatumJetzt = new System.Windows.Forms.CheckBox();
			this.cbx_Einsatz_Sicherungstrupp_Rueckzug_DatumJetzt = new System.Windows.Forms.CheckBox();
			this.txt_Einsatz_Sicherungstrupp_Kommentar = new System.Windows.Forms.RichTextBox();
			this.gbx_allgemeine_Informationen = new System.Windows.Forms.GroupBox();
			this.btn_Zeige_AGT_Informationen = new System.Windows.Forms.Button();
			this.lbl_aktueller_Einsatz_Datum = new System.Windows.Forms.Label();
			this.lbl_aktueller_Einsatz_Einsatzschwerpunkt = new System.Windows.Forms.Label();
			this.lbl_aktueller_Einsatz_Einsatzschwerpunkt_DoppelPunkt = new System.Windows.Forms.Label();
			this.lbl_aktueller_Einsatz_Verantwortlicher = new System.Windows.Forms.Label();
			this.lbl_aktueller_Einsatz__Verantwortlicher_DoppelPunkt = new System.Windows.Forms.Label();
			this.dgrid_Einsatz_letzte_Informationen = new System.Windows.Forms.DataGrid();
			this.dgts_AGT_Informationen = new System.Windows.Forms.DataGridTableStyle();
			this.btn_Einsatz_abschließen = new System.Windows.Forms.Button();
			this.btn_Einsatz_abbrechen = new System.Windows.Forms.Button();
			this.lbl_AGT_Einsatz = new System.Windows.Forms.Label();
			this.tabctrl_AGT.SuspendLayout();
			this.tabpage_AGT_Einatz_erstellen.SuspendLayout();
			this.gbx_EinsatzTrupps.SuspendLayout();
			this.gbx_EinsatzTrupps_Trupp1.SuspendLayout();
			this.gbx_PA_Nr.SuspendLayout();
			this.gbx_EinsatzTrupps_Trupp2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.gbx_EinsatzTrupps_Sicherungstrupp.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.gbx_AGT_Einsatz_erstellen_Header.SuspendLayout();
			this.tabpage_AGTOperator_inaktiv.SuspendLayout();
			this.tabpage_AGT_aktueller_Einsatz.SuspendLayout();
			this.tabctrl_AGT_Einsatz.SuspendLayout();
			this.tabpage_AGT_Einsatz_Trupp1.SuspendLayout();
			this.gbx_Einsatz_Trupp1.SuspendLayout();
			this.gbx_Einsatz_Trupp1_Einsatzziel_Rückzug.SuspendLayout();
			this.tabpage_AGT_Einsatz_Trupp2.SuspendLayout();
			this.gbx_Einsatz_Trupp2.SuspendLayout();
			this.gbx_Einsatz_Trupp2_Einsatzziel_Rückzug.SuspendLayout();
			this.tabpage_AGT_Einsatz_Sicherungtrupp.SuspendLayout();
			this.gbx_Einsatz_Sicherungstrupp.SuspendLayout();
			this.gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug.SuspendLayout();
			this.gbx_allgemeine_Informationen.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Einsatz_letzte_Informationen)).BeginInit();
			this.SuspendLayout();
			// 
			// tabctrl_AGT
			// 
			this.tabctrl_AGT.Controls.Add(this.tabpage_AGT_Einatz_erstellen);
			this.tabctrl_AGT.Controls.Add(this.tabpage_AGTOperator_inaktiv);
			this.tabctrl_AGT.Controls.Add(this.tabpage_AGT_aktueller_Einsatz);
			this.tabctrl_AGT.Location = new System.Drawing.Point(0, 40);
			this.tabctrl_AGT.Name = "tabctrl_AGT";
			this.tabctrl_AGT.SelectedIndex = 0;
			this.tabctrl_AGT.Size = new System.Drawing.Size(648, 488);
			this.tabctrl_AGT.TabIndex = 0;
			// 
			// tabpage_AGT_Einatz_erstellen
			// 
			this.tabpage_AGT_Einatz_erstellen.Controls.Add(this.gbx_EinsatzTrupps);
			this.tabpage_AGT_Einatz_erstellen.Controls.Add(this.gbx_AGT_Einsatz_erstellen_Header);
			this.tabpage_AGT_Einatz_erstellen.Controls.Add(this.btn_AGT_Einsatz_anlegen);
			this.tabpage_AGT_Einatz_erstellen.Location = new System.Drawing.Point(4, 22);
			this.tabpage_AGT_Einatz_erstellen.Name = "tabpage_AGT_Einatz_erstellen";
			this.tabpage_AGT_Einatz_erstellen.Size = new System.Drawing.Size(640, 462);
			this.tabpage_AGT_Einatz_erstellen.TabIndex = 0;
			this.tabpage_AGT_Einatz_erstellen.Text = "Einsatz erstellen";
			// 
			// gbx_EinsatzTrupps
			// 
			this.gbx_EinsatzTrupps.Controls.Add(this.cbx_Trupp1);
			this.gbx_EinsatzTrupps.Controls.Add(this.cbx_Trupp2);
			this.gbx_EinsatzTrupps.Controls.Add(this.cbx_Sicherungstrupp);
			this.gbx_EinsatzTrupps.Controls.Add(this.gbx_EinsatzTrupps_Trupp1);
			this.gbx_EinsatzTrupps.Controls.Add(this.gbx_EinsatzTrupps_Trupp2);
			this.gbx_EinsatzTrupps.Controls.Add(this.gbx_EinsatzTrupps_Sicherungstrupp);
			this.gbx_EinsatzTrupps.Location = new System.Drawing.Point(8, 56);
			this.gbx_EinsatzTrupps.Name = "gbx_EinsatzTrupps";
			this.gbx_EinsatzTrupps.Size = new System.Drawing.Size(624, 368);
			this.gbx_EinsatzTrupps.TabIndex = 5;
			this.gbx_EinsatzTrupps.TabStop = false;
			this.gbx_EinsatzTrupps.Text = "EinsatzTrupps";
			// 
			// cbx_Trupp1
			// 
			this.cbx_Trupp1.Checked = true;
			this.cbx_Trupp1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbx_Trupp1.Enabled = false;
			this.cbx_Trupp1.Location = new System.Drawing.Point(8, 24);
			this.cbx_Trupp1.Name = "cbx_Trupp1";
			this.cbx_Trupp1.Size = new System.Drawing.Size(15, 15);
			this.cbx_Trupp1.TabIndex = 12;
			// 
			// cbx_Trupp2
			// 
			this.cbx_Trupp2.Location = new System.Drawing.Point(8, 136);
			this.cbx_Trupp2.Name = "cbx_Trupp2";
			this.cbx_Trupp2.Size = new System.Drawing.Size(15, 15);
			this.cbx_Trupp2.TabIndex = 11;
			this.cbx_Trupp2.CheckStateChanged += new System.EventHandler(this.cbx_Trupp2_CheckStateChanged);
			// 
			// cbx_Sicherungstrupp
			// 
			this.cbx_Sicherungstrupp.Checked = true;
			this.cbx_Sicherungstrupp.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbx_Sicherungstrupp.Enabled = false;
			this.cbx_Sicherungstrupp.Location = new System.Drawing.Point(8, 256);
			this.cbx_Sicherungstrupp.Name = "cbx_Sicherungstrupp";
			this.cbx_Sicherungstrupp.Size = new System.Drawing.Size(15, 15);
			this.cbx_Sicherungstrupp.TabIndex = 10;
			// 
			// gbx_EinsatzTrupps_Trupp1
			// 
			this.gbx_EinsatzTrupps_Trupp1.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_EinsatzTrupps_Trupp1.Controls.Add(this.gbx_PA_Nr);
			this.gbx_EinsatzTrupps_Trupp1.Controls.Add(this.cbo_Einsatztrupp_Trupp1_Truppmann3);
			this.gbx_EinsatzTrupps_Trupp1.Controls.Add(this.cbo_Einsatztrupp_Trupp1_Truppmann2);
			this.gbx_EinsatzTrupps_Trupp1.Controls.Add(this.cbo_Einsatztrupp_Trupp1_Truppmann1);
			this.gbx_EinsatzTrupps_Trupp1.Controls.Add(this.cbo_Einsatztrupp_Trupp1_Truppführer);
			this.gbx_EinsatzTrupps_Trupp1.Controls.Add(this.txt_Trupp1_Funkrufname);
			this.gbx_EinsatzTrupps_Trupp1.Controls.Add(this.lbl_Trupp1_Funkrufname);
			this.gbx_EinsatzTrupps_Trupp1.Location = new System.Drawing.Point(24, 24);
			this.gbx_EinsatzTrupps_Trupp1.Name = "gbx_EinsatzTrupps_Trupp1";
			this.gbx_EinsatzTrupps_Trupp1.Size = new System.Drawing.Size(568, 96);
			this.gbx_EinsatzTrupps_Trupp1.TabIndex = 2;
			this.gbx_EinsatzTrupps_Trupp1.TabStop = false;
			this.gbx_EinsatzTrupps_Trupp1.Text = "Trupp1";
			// 
			// gbx_PA_Nr
			// 
			this.gbx_PA_Nr.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_PA_Nr.Controls.Add(this.txt_Trupp1_PA_Nr_Truppmann3);
			this.gbx_PA_Nr.Controls.Add(this.txt_Trupp1_PA_Nr_Truppmann2);
			this.gbx_PA_Nr.Controls.Add(this.txt_Trupp1_PA_Nr_Truppmann1);
			this.gbx_PA_Nr.Controls.Add(this.txt_Trupp1_PA_Nr_Truppfuehrer);
			this.gbx_PA_Nr.Location = new System.Drawing.Point(12, 56);
			this.gbx_PA_Nr.Name = "gbx_PA_Nr";
			this.gbx_PA_Nr.Size = new System.Drawing.Size(544, 32);
			this.gbx_PA_Nr.TabIndex = 6;
			this.gbx_PA_Nr.TabStop = false;
			this.gbx_PA_Nr.Text = "PA-Nr.";
			// 
			// txt_Trupp1_PA_Nr_Truppmann3
			// 
			this.txt_Trupp1_PA_Nr_Truppmann3.Location = new System.Drawing.Point(496, 8);
			this.txt_Trupp1_PA_Nr_Truppmann3.Name = "txt_Trupp1_PA_Nr_Truppmann3";
			this.txt_Trupp1_PA_Nr_Truppmann3.Size = new System.Drawing.Size(40, 20);
			this.txt_Trupp1_PA_Nr_Truppmann3.TabIndex = 3;
			this.txt_Trupp1_PA_Nr_Truppmann3.Text = "";
			this.txt_Trupp1_PA_Nr_Truppmann3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt_Trupp1_PA_Nr_Truppmann2
			// 
			this.txt_Trupp1_PA_Nr_Truppmann2.Location = new System.Drawing.Point(344, 8);
			this.txt_Trupp1_PA_Nr_Truppmann2.Name = "txt_Trupp1_PA_Nr_Truppmann2";
			this.txt_Trupp1_PA_Nr_Truppmann2.Size = new System.Drawing.Size(40, 20);
			this.txt_Trupp1_PA_Nr_Truppmann2.TabIndex = 2;
			this.txt_Trupp1_PA_Nr_Truppmann2.Text = "";
			this.txt_Trupp1_PA_Nr_Truppmann2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt_Trupp1_PA_Nr_Truppmann1
			// 
			this.txt_Trupp1_PA_Nr_Truppmann1.Location = new System.Drawing.Point(208, 8);
			this.txt_Trupp1_PA_Nr_Truppmann1.Name = "txt_Trupp1_PA_Nr_Truppmann1";
			this.txt_Trupp1_PA_Nr_Truppmann1.Size = new System.Drawing.Size(40, 20);
			this.txt_Trupp1_PA_Nr_Truppmann1.TabIndex = 1;
			this.txt_Trupp1_PA_Nr_Truppmann1.Text = "";
			this.txt_Trupp1_PA_Nr_Truppmann1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt_Trupp1_PA_Nr_Truppfuehrer
			// 
			this.txt_Trupp1_PA_Nr_Truppfuehrer.Location = new System.Drawing.Point(72, 8);
			this.txt_Trupp1_PA_Nr_Truppfuehrer.Name = "txt_Trupp1_PA_Nr_Truppfuehrer";
			this.txt_Trupp1_PA_Nr_Truppfuehrer.Size = new System.Drawing.Size(40, 20);
			this.txt_Trupp1_PA_Nr_Truppfuehrer.TabIndex = 0;
			this.txt_Trupp1_PA_Nr_Truppfuehrer.Text = "";
			this.txt_Trupp1_PA_Nr_Truppfuehrer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cbo_Einsatztrupp_Trupp1_Truppmann3
			// 
			this.cbo_Einsatztrupp_Trupp1_Truppmann3.Location = new System.Drawing.Point(440, 32);
			this.cbo_Einsatztrupp_Trupp1_Truppmann3.Name = "cbo_Einsatztrupp_Trupp1_Truppmann3";
			this.cbo_Einsatztrupp_Trupp1_Truppmann3.Size = new System.Drawing.Size(116, 21);
			this.cbo_Einsatztrupp_Trupp1_Truppmann3.TabIndex = 5;
			this.cbo_Einsatztrupp_Trupp1_Truppmann3.Text = "3. Truppmann";
			// 
			// cbo_Einsatztrupp_Trupp1_Truppmann2
			// 
			this.cbo_Einsatztrupp_Trupp1_Truppmann2.Location = new System.Drawing.Point(288, 32);
			this.cbo_Einsatztrupp_Trupp1_Truppmann2.Name = "cbo_Einsatztrupp_Trupp1_Truppmann2";
			this.cbo_Einsatztrupp_Trupp1_Truppmann2.Size = new System.Drawing.Size(116, 21);
			this.cbo_Einsatztrupp_Trupp1_Truppmann2.TabIndex = 4;
			this.cbo_Einsatztrupp_Trupp1_Truppmann2.Text = "2. Truppman";
			// 
			// cbo_Einsatztrupp_Trupp1_Truppmann1
			// 
			this.cbo_Einsatztrupp_Trupp1_Truppmann1.Location = new System.Drawing.Point(152, 32);
			this.cbo_Einsatztrupp_Trupp1_Truppmann1.Name = "cbo_Einsatztrupp_Trupp1_Truppmann1";
			this.cbo_Einsatztrupp_Trupp1_Truppmann1.Size = new System.Drawing.Size(116, 21);
			this.cbo_Einsatztrupp_Trupp1_Truppmann1.TabIndex = 3;
			this.cbo_Einsatztrupp_Trupp1_Truppmann1.Text = "1. Truppmann";
			// 
			// cbo_Einsatztrupp_Trupp1_Truppführer
			// 
			this.cbo_Einsatztrupp_Trupp1_Truppführer.Location = new System.Drawing.Point(12, 32);
			this.cbo_Einsatztrupp_Trupp1_Truppführer.Name = "cbo_Einsatztrupp_Trupp1_Truppführer";
			this.cbo_Einsatztrupp_Trupp1_Truppführer.Size = new System.Drawing.Size(116, 21);
			this.cbo_Einsatztrupp_Trupp1_Truppführer.TabIndex = 2;
			this.cbo_Einsatztrupp_Trupp1_Truppführer.Text = "Truppführer";
			// 
			// txt_Trupp1_Funkrufname
			// 
			this.txt_Trupp1_Funkrufname.Location = new System.Drawing.Point(224, 8);
			this.txt_Trupp1_Funkrufname.Name = "txt_Trupp1_Funkrufname";
			this.txt_Trupp1_Funkrufname.Size = new System.Drawing.Size(120, 20);
			this.txt_Trupp1_Funkrufname.TabIndex = 1;
			this.txt_Trupp1_Funkrufname.Text = "";
			// 
			// lbl_Trupp1_Funkrufname
			// 
			this.lbl_Trupp1_Funkrufname.Location = new System.Drawing.Point(152, 12);
			this.lbl_Trupp1_Funkrufname.Name = "lbl_Trupp1_Funkrufname";
			this.lbl_Trupp1_Funkrufname.Size = new System.Drawing.Size(72, 16);
			this.lbl_Trupp1_Funkrufname.TabIndex = 0;
			this.lbl_Trupp1_Funkrufname.Text = "Funkrufname:";
			// 
			// gbx_EinsatzTrupps_Trupp2
			// 
			this.gbx_EinsatzTrupps_Trupp2.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_EinsatzTrupps_Trupp2.Controls.Add(this.groupBox3);
			this.gbx_EinsatzTrupps_Trupp2.Controls.Add(this.cbo_Einsatztrupp_Trupp2_Truppmann3);
			this.gbx_EinsatzTrupps_Trupp2.Controls.Add(this.cbo_Einsatztrupp_Trupp2_Truppmann2);
			this.gbx_EinsatzTrupps_Trupp2.Controls.Add(this.cbo_Einsatztrupp_Trupp2_Truppmann1);
			this.gbx_EinsatzTrupps_Trupp2.Controls.Add(this.cbo_Einsatztrupp_Trupp2_Truppführer);
			this.gbx_EinsatzTrupps_Trupp2.Controls.Add(this.txt_Trupp2_Funkrufname);
			this.gbx_EinsatzTrupps_Trupp2.Controls.Add(this.lbl_Trupp2_Funkrufname);
			this.gbx_EinsatzTrupps_Trupp2.Enabled = false;
			this.gbx_EinsatzTrupps_Trupp2.Location = new System.Drawing.Point(24, 136);
			this.gbx_EinsatzTrupps_Trupp2.Name = "gbx_EinsatzTrupps_Trupp2";
			this.gbx_EinsatzTrupps_Trupp2.Size = new System.Drawing.Size(568, 96);
			this.gbx_EinsatzTrupps_Trupp2.TabIndex = 7;
			this.gbx_EinsatzTrupps_Trupp2.TabStop = false;
			this.gbx_EinsatzTrupps_Trupp2.Text = "Trupp2";
			// 
			// groupBox3
			// 
			this.groupBox3.BackColor = System.Drawing.SystemColors.Window;
			this.groupBox3.Controls.Add(this.txt_Trupp2_PA_Nr_Truppmann3);
			this.groupBox3.Controls.Add(this.txt_Trupp2_PA_Nr_Truppmann2);
			this.groupBox3.Controls.Add(this.txt_Trupp2_PA_Nr_Truppmann1);
			this.groupBox3.Controls.Add(this.txt_Trupp2_PA_Nr_Truppfuehrer);
			this.groupBox3.Location = new System.Drawing.Point(12, 56);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(544, 32);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "PA-Nr.";
			// 
			// txt_Trupp2_PA_Nr_Truppmann3
			// 
			this.txt_Trupp2_PA_Nr_Truppmann3.Location = new System.Drawing.Point(496, 8);
			this.txt_Trupp2_PA_Nr_Truppmann3.Name = "txt_Trupp2_PA_Nr_Truppmann3";
			this.txt_Trupp2_PA_Nr_Truppmann3.Size = new System.Drawing.Size(40, 20);
			this.txt_Trupp2_PA_Nr_Truppmann3.TabIndex = 3;
			this.txt_Trupp2_PA_Nr_Truppmann3.Text = "";
			this.txt_Trupp2_PA_Nr_Truppmann3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt_Trupp2_PA_Nr_Truppmann2
			// 
			this.txt_Trupp2_PA_Nr_Truppmann2.Location = new System.Drawing.Point(344, 8);
			this.txt_Trupp2_PA_Nr_Truppmann2.Name = "txt_Trupp2_PA_Nr_Truppmann2";
			this.txt_Trupp2_PA_Nr_Truppmann2.Size = new System.Drawing.Size(40, 20);
			this.txt_Trupp2_PA_Nr_Truppmann2.TabIndex = 2;
			this.txt_Trupp2_PA_Nr_Truppmann2.Text = "";
			this.txt_Trupp2_PA_Nr_Truppmann2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt_Trupp2_PA_Nr_Truppmann1
			// 
			this.txt_Trupp2_PA_Nr_Truppmann1.Location = new System.Drawing.Point(208, 8);
			this.txt_Trupp2_PA_Nr_Truppmann1.Name = "txt_Trupp2_PA_Nr_Truppmann1";
			this.txt_Trupp2_PA_Nr_Truppmann1.Size = new System.Drawing.Size(40, 20);
			this.txt_Trupp2_PA_Nr_Truppmann1.TabIndex = 1;
			this.txt_Trupp2_PA_Nr_Truppmann1.Text = "";
			this.txt_Trupp2_PA_Nr_Truppmann1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt_Trupp2_PA_Nr_Truppfuehrer
			// 
			this.txt_Trupp2_PA_Nr_Truppfuehrer.Location = new System.Drawing.Point(72, 8);
			this.txt_Trupp2_PA_Nr_Truppfuehrer.Name = "txt_Trupp2_PA_Nr_Truppfuehrer";
			this.txt_Trupp2_PA_Nr_Truppfuehrer.Size = new System.Drawing.Size(40, 20);
			this.txt_Trupp2_PA_Nr_Truppfuehrer.TabIndex = 0;
			this.txt_Trupp2_PA_Nr_Truppfuehrer.Text = "";
			this.txt_Trupp2_PA_Nr_Truppfuehrer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cbo_Einsatztrupp_Trupp2_Truppmann3
			// 
			this.cbo_Einsatztrupp_Trupp2_Truppmann3.Location = new System.Drawing.Point(440, 32);
			this.cbo_Einsatztrupp_Trupp2_Truppmann3.Name = "cbo_Einsatztrupp_Trupp2_Truppmann3";
			this.cbo_Einsatztrupp_Trupp2_Truppmann3.Size = new System.Drawing.Size(116, 21);
			this.cbo_Einsatztrupp_Trupp2_Truppmann3.TabIndex = 5;
			this.cbo_Einsatztrupp_Trupp2_Truppmann3.Text = "3. Truppmann";
			// 
			// cbo_Einsatztrupp_Trupp2_Truppmann2
			// 
			this.cbo_Einsatztrupp_Trupp2_Truppmann2.Location = new System.Drawing.Point(288, 32);
			this.cbo_Einsatztrupp_Trupp2_Truppmann2.Name = "cbo_Einsatztrupp_Trupp2_Truppmann2";
			this.cbo_Einsatztrupp_Trupp2_Truppmann2.Size = new System.Drawing.Size(116, 21);
			this.cbo_Einsatztrupp_Trupp2_Truppmann2.TabIndex = 4;
			this.cbo_Einsatztrupp_Trupp2_Truppmann2.Text = "2. Truppman";
			// 
			// cbo_Einsatztrupp_Trupp2_Truppmann1
			// 
			this.cbo_Einsatztrupp_Trupp2_Truppmann1.Location = new System.Drawing.Point(152, 32);
			this.cbo_Einsatztrupp_Trupp2_Truppmann1.Name = "cbo_Einsatztrupp_Trupp2_Truppmann1";
			this.cbo_Einsatztrupp_Trupp2_Truppmann1.Size = new System.Drawing.Size(116, 21);
			this.cbo_Einsatztrupp_Trupp2_Truppmann1.TabIndex = 3;
			this.cbo_Einsatztrupp_Trupp2_Truppmann1.Text = "1. Truppmann";
			// 
			// cbo_Einsatztrupp_Trupp2_Truppführer
			// 
			this.cbo_Einsatztrupp_Trupp2_Truppführer.Location = new System.Drawing.Point(12, 32);
			this.cbo_Einsatztrupp_Trupp2_Truppführer.Name = "cbo_Einsatztrupp_Trupp2_Truppführer";
			this.cbo_Einsatztrupp_Trupp2_Truppführer.Size = new System.Drawing.Size(116, 21);
			this.cbo_Einsatztrupp_Trupp2_Truppführer.TabIndex = 2;
			this.cbo_Einsatztrupp_Trupp2_Truppführer.Text = "Truppführer";
			// 
			// txt_Trupp2_Funkrufname
			// 
			this.txt_Trupp2_Funkrufname.Location = new System.Drawing.Point(224, 8);
			this.txt_Trupp2_Funkrufname.Name = "txt_Trupp2_Funkrufname";
			this.txt_Trupp2_Funkrufname.Size = new System.Drawing.Size(120, 20);
			this.txt_Trupp2_Funkrufname.TabIndex = 1;
			this.txt_Trupp2_Funkrufname.Text = "";
			// 
			// lbl_Trupp2_Funkrufname
			// 
			this.lbl_Trupp2_Funkrufname.Location = new System.Drawing.Point(152, 12);
			this.lbl_Trupp2_Funkrufname.Name = "lbl_Trupp2_Funkrufname";
			this.lbl_Trupp2_Funkrufname.Size = new System.Drawing.Size(72, 16);
			this.lbl_Trupp2_Funkrufname.TabIndex = 0;
			this.lbl_Trupp2_Funkrufname.Text = "Funkrufname:";
			// 
			// gbx_EinsatzTrupps_Sicherungstrupp
			// 
			this.gbx_EinsatzTrupps_Sicherungstrupp.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_EinsatzTrupps_Sicherungstrupp.Controls.Add(this.groupBox4);
			this.gbx_EinsatzTrupps_Sicherungstrupp.Controls.Add(this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann3);
			this.gbx_EinsatzTrupps_Sicherungstrupp.Controls.Add(this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann2);
			this.gbx_EinsatzTrupps_Sicherungstrupp.Controls.Add(this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann1);
			this.gbx_EinsatzTrupps_Sicherungstrupp.Controls.Add(this.cbo_Einsatztrupp_Sicherungstrupp_Truppführer);
			this.gbx_EinsatzTrupps_Sicherungstrupp.Controls.Add(this.txt_Sicherungstrupp_Funkrufname);
			this.gbx_EinsatzTrupps_Sicherungstrupp.Controls.Add(this.lbl_Sicherungstrupp_Funkrufname);
			this.gbx_EinsatzTrupps_Sicherungstrupp.Location = new System.Drawing.Point(24, 256);
			this.gbx_EinsatzTrupps_Sicherungstrupp.Name = "gbx_EinsatzTrupps_Sicherungstrupp";
			this.gbx_EinsatzTrupps_Sicherungstrupp.Size = new System.Drawing.Size(568, 96);
			this.gbx_EinsatzTrupps_Sicherungstrupp.TabIndex = 8;
			this.gbx_EinsatzTrupps_Sicherungstrupp.TabStop = false;
			this.gbx_EinsatzTrupps_Sicherungstrupp.Text = "Sicherungstrupp";
			// 
			// groupBox4
			// 
			this.groupBox4.BackColor = System.Drawing.SystemColors.Window;
			this.groupBox4.Controls.Add(this.txt_Sicherungstrupp_PA_Nr_Truppmann3);
			this.groupBox4.Controls.Add(this.txt_Sicherungstrupp_PA_Nr_Truppmann2);
			this.groupBox4.Controls.Add(this.txt_Sicherungstrupp_PA_Nr_Truppmann1);
			this.groupBox4.Controls.Add(this.txt_Sicherungstrupp_PA_Nr_Truppfuehrer);
			this.groupBox4.Location = new System.Drawing.Point(12, 56);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(544, 32);
			this.groupBox4.TabIndex = 6;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "PA-Nr.";
			// 
			// txt_Sicherungstrupp_PA_Nr_Truppmann3
			// 
			this.txt_Sicherungstrupp_PA_Nr_Truppmann3.Location = new System.Drawing.Point(496, 8);
			this.txt_Sicherungstrupp_PA_Nr_Truppmann3.Name = "txt_Sicherungstrupp_PA_Nr_Truppmann3";
			this.txt_Sicherungstrupp_PA_Nr_Truppmann3.Size = new System.Drawing.Size(40, 20);
			this.txt_Sicherungstrupp_PA_Nr_Truppmann3.TabIndex = 3;
			this.txt_Sicherungstrupp_PA_Nr_Truppmann3.Text = "";
			this.txt_Sicherungstrupp_PA_Nr_Truppmann3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt_Sicherungstrupp_PA_Nr_Truppmann2
			// 
			this.txt_Sicherungstrupp_PA_Nr_Truppmann2.Location = new System.Drawing.Point(344, 8);
			this.txt_Sicherungstrupp_PA_Nr_Truppmann2.Name = "txt_Sicherungstrupp_PA_Nr_Truppmann2";
			this.txt_Sicherungstrupp_PA_Nr_Truppmann2.Size = new System.Drawing.Size(40, 20);
			this.txt_Sicherungstrupp_PA_Nr_Truppmann2.TabIndex = 2;
			this.txt_Sicherungstrupp_PA_Nr_Truppmann2.Text = "";
			this.txt_Sicherungstrupp_PA_Nr_Truppmann2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt_Sicherungstrupp_PA_Nr_Truppmann1
			// 
			this.txt_Sicherungstrupp_PA_Nr_Truppmann1.Location = new System.Drawing.Point(208, 8);
			this.txt_Sicherungstrupp_PA_Nr_Truppmann1.Name = "txt_Sicherungstrupp_PA_Nr_Truppmann1";
			this.txt_Sicherungstrupp_PA_Nr_Truppmann1.Size = new System.Drawing.Size(40, 20);
			this.txt_Sicherungstrupp_PA_Nr_Truppmann1.TabIndex = 1;
			this.txt_Sicherungstrupp_PA_Nr_Truppmann1.Text = "";
			this.txt_Sicherungstrupp_PA_Nr_Truppmann1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt_Sicherungstrupp_PA_Nr_Truppfuehrer
			// 
			this.txt_Sicherungstrupp_PA_Nr_Truppfuehrer.Location = new System.Drawing.Point(72, 8);
			this.txt_Sicherungstrupp_PA_Nr_Truppfuehrer.Name = "txt_Sicherungstrupp_PA_Nr_Truppfuehrer";
			this.txt_Sicherungstrupp_PA_Nr_Truppfuehrer.Size = new System.Drawing.Size(40, 20);
			this.txt_Sicherungstrupp_PA_Nr_Truppfuehrer.TabIndex = 0;
			this.txt_Sicherungstrupp_PA_Nr_Truppfuehrer.Text = "";
			this.txt_Sicherungstrupp_PA_Nr_Truppfuehrer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cbo_Einsatztrupp_Sicherungstrupp_Truppmann3
			// 
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann3.Location = new System.Drawing.Point(440, 32);
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann3.Name = "cbo_Einsatztrupp_Sicherungstrupp_Truppmann3";
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann3.Size = new System.Drawing.Size(116, 21);
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann3.TabIndex = 5;
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann3.Text = "3. Truppmann";
			// 
			// cbo_Einsatztrupp_Sicherungstrupp_Truppmann2
			// 
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann2.Location = new System.Drawing.Point(288, 32);
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann2.Name = "cbo_Einsatztrupp_Sicherungstrupp_Truppmann2";
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann2.Size = new System.Drawing.Size(116, 21);
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann2.TabIndex = 4;
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann2.Text = "2. Truppman";
			// 
			// cbo_Einsatztrupp_Sicherungstrupp_Truppmann1
			// 
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann1.Location = new System.Drawing.Point(152, 32);
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann1.Name = "cbo_Einsatztrupp_Sicherungstrupp_Truppmann1";
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann1.Size = new System.Drawing.Size(116, 21);
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann1.TabIndex = 3;
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann1.Text = "1. Truppmann";
			// 
			// cbo_Einsatztrupp_Sicherungstrupp_Truppführer
			// 
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppführer.Location = new System.Drawing.Point(12, 32);
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppführer.Name = "cbo_Einsatztrupp_Sicherungstrupp_Truppführer";
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppführer.Size = new System.Drawing.Size(116, 21);
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppführer.TabIndex = 2;
			this.cbo_Einsatztrupp_Sicherungstrupp_Truppführer.Text = "Truppführer";
			// 
			// txt_Sicherungstrupp_Funkrufname
			// 
			this.txt_Sicherungstrupp_Funkrufname.Location = new System.Drawing.Point(224, 8);
			this.txt_Sicherungstrupp_Funkrufname.Name = "txt_Sicherungstrupp_Funkrufname";
			this.txt_Sicherungstrupp_Funkrufname.Size = new System.Drawing.Size(120, 20);
			this.txt_Sicherungstrupp_Funkrufname.TabIndex = 1;
			this.txt_Sicherungstrupp_Funkrufname.Text = "";
			// 
			// lbl_Sicherungstrupp_Funkrufname
			// 
			this.lbl_Sicherungstrupp_Funkrufname.Location = new System.Drawing.Point(152, 12);
			this.lbl_Sicherungstrupp_Funkrufname.Name = "lbl_Sicherungstrupp_Funkrufname";
			this.lbl_Sicherungstrupp_Funkrufname.Size = new System.Drawing.Size(72, 16);
			this.lbl_Sicherungstrupp_Funkrufname.TabIndex = 0;
			this.lbl_Sicherungstrupp_Funkrufname.Text = "Funkrufname:";
			// 
			// gbx_AGT_Einsatz_erstellen_Header
			// 
			this.gbx_AGT_Einsatz_erstellen_Header.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_AGT_Einsatz_erstellen_Header.Controls.Add(this.txt_AGT_Einsatz_erstellen_Header_Verantwortlicher);
			this.gbx_AGT_Einsatz_erstellen_Header.Controls.Add(this.cbo_AGT_Einsatz_erstellen_Header_Einsatzschwerpunkt);
			this.gbx_AGT_Einsatz_erstellen_Header.Controls.Add(this.lbl_AGT_Einsatz_erstellen_Header_Verantwortlicher_DoppelPunkt);
			this.gbx_AGT_Einsatz_erstellen_Header.Controls.Add(this.dtp_AGT_Einsatz_Erstellen_Header_Datum);
			this.gbx_AGT_Einsatz_erstellen_Header.Controls.Add(this.lbl__AGT_Einsatz_Erstellen_Header_Datum);
			this.gbx_AGT_Einsatz_erstellen_Header.Location = new System.Drawing.Point(8, 8);
			this.gbx_AGT_Einsatz_erstellen_Header.Name = "gbx_AGT_Einsatz_erstellen_Header";
			this.gbx_AGT_Einsatz_erstellen_Header.Size = new System.Drawing.Size(624, 40);
			this.gbx_AGT_Einsatz_erstellen_Header.TabIndex = 1;
			this.gbx_AGT_Einsatz_erstellen_Header.TabStop = false;
			// 
			// txt_AGT_Einsatz_erstellen_Header_Verantwortlicher
			// 
			this.txt_AGT_Einsatz_erstellen_Header_Verantwortlicher.Location = new System.Drawing.Point(512, 14);
			this.txt_AGT_Einsatz_erstellen_Header_Verantwortlicher.Name = "txt_AGT_Einsatz_erstellen_Header_Verantwortlicher";
			this.txt_AGT_Einsatz_erstellen_Header_Verantwortlicher.Size = new System.Drawing.Size(104, 20);
			this.txt_AGT_Einsatz_erstellen_Header_Verantwortlicher.TabIndex = 22;
			this.txt_AGT_Einsatz_erstellen_Header_Verantwortlicher.Text = "";
			// 
			// cbo_AGT_Einsatz_erstellen_Header_Einsatzschwerpunkt
			// 
			this.cbo_AGT_Einsatz_erstellen_Header_Einsatzschwerpunkt.Items.AddRange(new object[] {
																									 "brennendes Haus"});
			this.cbo_AGT_Einsatz_erstellen_Header_Einsatzschwerpunkt.Location = new System.Drawing.Point(224, 12);
			this.cbo_AGT_Einsatz_erstellen_Header_Einsatzschwerpunkt.Name = "cbo_AGT_Einsatz_erstellen_Header_Einsatzschwerpunkt";
			this.cbo_AGT_Einsatz_erstellen_Header_Einsatzschwerpunkt.Size = new System.Drawing.Size(184, 21);
			this.cbo_AGT_Einsatz_erstellen_Header_Einsatzschwerpunkt.TabIndex = 21;
			this.cbo_AGT_Einsatz_erstellen_Header_Einsatzschwerpunkt.Text = "Wähle Einsatzschwerpunkt";
			// 
			// lbl_AGT_Einsatz_erstellen_Header_Verantwortlicher_DoppelPunkt
			// 
			this.lbl_AGT_Einsatz_erstellen_Header_Verantwortlicher_DoppelPunkt.Location = new System.Drawing.Point(416, 16);
			this.lbl_AGT_Einsatz_erstellen_Header_Verantwortlicher_DoppelPunkt.Name = "lbl_AGT_Einsatz_erstellen_Header_Verantwortlicher_DoppelPunkt";
			this.lbl_AGT_Einsatz_erstellen_Header_Verantwortlicher_DoppelPunkt.Size = new System.Drawing.Size(96, 16);
			this.lbl_AGT_Einsatz_erstellen_Header_Verantwortlicher_DoppelPunkt.TabIndex = 20;
			this.lbl_AGT_Einsatz_erstellen_Header_Verantwortlicher_DoppelPunkt.Text = "Verantwortlicher:";
			// 
			// dtp_AGT_Einsatz_Erstellen_Header_Datum
			// 
			this.dtp_AGT_Einsatz_Erstellen_Header_Datum.ContextMenu = this.ctx_ZeitAktualisieren;
			this.dtp_AGT_Einsatz_Erstellen_Header_Datum.CustomFormat = "dd.MM.yyyy : hh:mm";
			this.dtp_AGT_Einsatz_Erstellen_Header_Datum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_AGT_Einsatz_Erstellen_Header_Datum.Location = new System.Drawing.Point(64, 12);
			this.dtp_AGT_Einsatz_Erstellen_Header_Datum.MinDate = new System.DateTime(2004, 11, 2, 0, 0, 0, 0);
			this.dtp_AGT_Einsatz_Erstellen_Header_Datum.Name = "dtp_AGT_Einsatz_Erstellen_Header_Datum";
			this.dtp_AGT_Einsatz_Erstellen_Header_Datum.Size = new System.Drawing.Size(120, 20);
			this.dtp_AGT_Einsatz_Erstellen_Header_Datum.TabIndex = 19;
			// 
			// ctx_ZeitAktualisieren
			// 
			this.ctx_ZeitAktualisieren.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																								  this.mI_ZeitAktualisieren_Jetzt});
			// 
			// mI_ZeitAktualisieren_Jetzt
			// 
			this.mI_ZeitAktualisieren_Jetzt.Index = 0;
			this.mI_ZeitAktualisieren_Jetzt.Text = "Jetzt";
			// 
			// lbl__AGT_Einsatz_Erstellen_Header_Datum
			// 
			this.lbl__AGT_Einsatz_Erstellen_Header_Datum.Location = new System.Drawing.Point(16, 15);
			this.lbl__AGT_Einsatz_Erstellen_Header_Datum.Name = "lbl__AGT_Einsatz_Erstellen_Header_Datum";
			this.lbl__AGT_Einsatz_Erstellen_Header_Datum.Size = new System.Drawing.Size(48, 15);
			this.lbl__AGT_Einsatz_Erstellen_Header_Datum.TabIndex = 18;
			this.lbl__AGT_Einsatz_Erstellen_Header_Datum.Text = "Datum: ";
			// 
			// btn_AGT_Einsatz_anlegen
			// 
			this.btn_AGT_Einsatz_anlegen.Location = new System.Drawing.Point(528, 432);
			this.btn_AGT_Einsatz_anlegen.Name = "btn_AGT_Einsatz_anlegen";
			this.btn_AGT_Einsatz_anlegen.Size = new System.Drawing.Size(104, 24);
			this.btn_AGT_Einsatz_anlegen.TabIndex = 4;
			this.btn_AGT_Einsatz_anlegen.Text = "Einsatz anlegen";
			this.btn_AGT_Einsatz_anlegen.Click += new System.EventHandler(this.btn_AGT_Einsatz_starten_Click);
			// 
			// tabpage_AGTOperator_inaktiv
			// 
			this.tabpage_AGTOperator_inaktiv.Controls.Add(this.rtb_AGTOperator_inaktiv);
			this.tabpage_AGTOperator_inaktiv.Location = new System.Drawing.Point(4, 22);
			this.tabpage_AGTOperator_inaktiv.Name = "tabpage_AGTOperator_inaktiv";
			this.tabpage_AGTOperator_inaktiv.Size = new System.Drawing.Size(640, 462);
			this.tabpage_AGTOperator_inaktiv.TabIndex = 1;
			this.tabpage_AGTOperator_inaktiv.Text = "falsche Rolle";
			// 
			// rtb_AGTOperator_inaktiv
			// 
			this.rtb_AGTOperator_inaktiv.BackColor = System.Drawing.Color.RosyBrown;
			this.rtb_AGTOperator_inaktiv.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rtb_AGTOperator_inaktiv.Location = new System.Drawing.Point(48, 104);
			this.rtb_AGTOperator_inaktiv.Name = "rtb_AGTOperator_inaktiv";
			this.rtb_AGTOperator_inaktiv.Size = new System.Drawing.Size(528, 200);
			this.rtb_AGTOperator_inaktiv.TabIndex = 1;
			this.rtb_AGTOperator_inaktiv.Text = @"			Sie sind zur Zeit nicht als AGT Operator angemeldet.
		Nur wenn Sie die richtige Rolle haben, können Sie einen AGT Einsatz überwachen.




Hinweis:
Solange Sie noch AGT Einsatzsätze leiten wird die Funktionalität der Software nur eingeschränkt zur 
Verfügung stehen.  Bitte beachten Sie, dass Sie die Rolle nicht wechseln können, solange noch AGT-Einsätze laufen.




	UM IN DEN AGT MODUS ZU GELANGEN, WECHSELN SIE DIE ROLLE ÜBER DAS MENÜ!";
			// 
			// tabpage_AGT_aktueller_Einsatz
			// 
			this.tabpage_AGT_aktueller_Einsatz.Controls.Add(this.tabctrl_AGT_Einsatz);
			this.tabpage_AGT_aktueller_Einsatz.Controls.Add(this.gbx_allgemeine_Informationen);
			this.tabpage_AGT_aktueller_Einsatz.Controls.Add(this.btn_Einsatz_abschließen);
			this.tabpage_AGT_aktueller_Einsatz.Controls.Add(this.btn_Einsatz_abbrechen);
			this.tabpage_AGT_aktueller_Einsatz.Location = new System.Drawing.Point(4, 22);
			this.tabpage_AGT_aktueller_Einsatz.Name = "tabpage_AGT_aktueller_Einsatz";
			this.tabpage_AGT_aktueller_Einsatz.Size = new System.Drawing.Size(640, 462);
			this.tabpage_AGT_aktueller_Einsatz.TabIndex = 2;
			this.tabpage_AGT_aktueller_Einsatz.Text = "aktueller  Einsatz";
			// 
			// tabctrl_AGT_Einsatz
			// 
			this.tabctrl_AGT_Einsatz.Controls.Add(this.tabpage_AGT_Einsatz_Trupp1);
			this.tabctrl_AGT_Einsatz.Controls.Add(this.tabpage_AGT_Einsatz_Trupp2);
			this.tabctrl_AGT_Einsatz.Controls.Add(this.tabpage_AGT_Einsatz_Sicherungtrupp);
			this.tabctrl_AGT_Einsatz.Location = new System.Drawing.Point(8, 176);
			this.tabctrl_AGT_Einsatz.Name = "tabctrl_AGT_Einsatz";
			this.tabctrl_AGT_Einsatz.SelectedIndex = 0;
			this.tabctrl_AGT_Einsatz.Size = new System.Drawing.Size(624, 248);
			this.tabctrl_AGT_Einsatz.TabIndex = 4;
			// 
			// tabpage_AGT_Einsatz_Trupp1
			// 
			this.tabpage_AGT_Einsatz_Trupp1.Controls.Add(this.gbx_Einsatz_Trupp1);
			this.tabpage_AGT_Einsatz_Trupp1.Location = new System.Drawing.Point(4, 22);
			this.tabpage_AGT_Einsatz_Trupp1.Name = "tabpage_AGT_Einsatz_Trupp1";
			this.tabpage_AGT_Einsatz_Trupp1.Size = new System.Drawing.Size(616, 222);
			this.tabpage_AGT_Einsatz_Trupp1.TabIndex = 0;
			this.tabpage_AGT_Einsatz_Trupp1.Text = "Trupp1";
			// 
			// gbx_Einsatz_Trupp1
			// 
			this.gbx_Einsatz_Trupp1.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_Einsatz_Trupp1.Controls.Add(this.lbl_Einsatz_Trupp1_Kommentar);
			this.gbx_Einsatz_Trupp1.Controls.Add(this.ade_Trupp1_30min);
			this.gbx_Einsatz_Trupp1.Controls.Add(this.ade_Trupp1_20min);
			this.gbx_Einsatz_Trupp1.Controls.Add(this.ade_Trupp1_10min);
			this.gbx_Einsatz_Trupp1.Controls.Add(this.ade_Trupp1_Anschliessen);
			this.gbx_Einsatz_Trupp1.Controls.Add(this.ade_Trupp1_Start);
			this.gbx_Einsatz_Trupp1.Controls.Add(this.lbl_Einsatz_Trupp1_Truppfuehrer);
			this.gbx_Einsatz_Trupp1.Controls.Add(this.lbl_Einsatz_Trupp1_Funkrufname);
			this.gbx_Einsatz_Trupp1.Controls.Add(this.lbl_Einsatz_Trupp1_Funkrufname_Doppelpunkt);
			this.gbx_Einsatz_Trupp1.Controls.Add(this.lbl_Einsatz_Trupp1_Truppfuehrer_DoppelPunkt);
			this.gbx_Einsatz_Trupp1.Controls.Add(this.lbl_Einsatz_Trupp1_Druck_Doppelpunkt);
			this.gbx_Einsatz_Trupp1.Controls.Add(this.lbl_Einsatz_Trupp1_Zeit_Doppelpunkt);
			this.gbx_Einsatz_Trupp1.Controls.Add(this.gbx_Einsatz_Trupp1_Einsatzziel_Rückzug);
			this.gbx_Einsatz_Trupp1.Controls.Add(this.txt_Einsatz_Trupp1_Kommentar);
			this.gbx_Einsatz_Trupp1.Location = new System.Drawing.Point(8, 8);
			this.gbx_Einsatz_Trupp1.Name = "gbx_Einsatz_Trupp1";
			this.gbx_Einsatz_Trupp1.Size = new System.Drawing.Size(608, 208);
			this.gbx_Einsatz_Trupp1.TabIndex = 3;
			this.gbx_Einsatz_Trupp1.TabStop = false;
			// 
			// lbl_Einsatz_Trupp1_Kommentar
			// 
			this.lbl_Einsatz_Trupp1_Kommentar.Location = new System.Drawing.Point(48, 120);
			this.lbl_Einsatz_Trupp1_Kommentar.Name = "lbl_Einsatz_Trupp1_Kommentar";
			this.lbl_Einsatz_Trupp1_Kommentar.Size = new System.Drawing.Size(66, 12);
			this.lbl_Einsatz_Trupp1_Kommentar.TabIndex = 10;
			this.lbl_Einsatz_Trupp1_Kommentar.Text = "Kommentar";
			// 
			// ade_Trupp1_30min
			// 
			this.ade_Trupp1_30min.Beschriftung = "30min";
			this.ade_Trupp1_30min.Location = new System.Drawing.Point(400, 32);
			this.ade_Trupp1_30min.Name = "ade_Trupp1_30min";
			this.ade_Trupp1_30min.Size = new System.Drawing.Size(88, 78);
			this.ade_Trupp1_30min.TabIndex = 9;
			this.ade_Trupp1_30min.Zeit = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			this.ade_Trupp1_30min.EnabledChanged += new System.EventHandler(this.ade_Trupp1_EnabledChanged);
			// 
			// ade_Trupp1_20min
			// 
			this.ade_Trupp1_20min.Beschriftung = "20min";
			this.ade_Trupp1_20min.Location = new System.Drawing.Point(312, 32);
			this.ade_Trupp1_20min.Name = "ade_Trupp1_20min";
			this.ade_Trupp1_20min.Size = new System.Drawing.Size(88, 78);
			this.ade_Trupp1_20min.TabIndex = 8;
			this.ade_Trupp1_20min.Zeit = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			this.ade_Trupp1_20min.EnabledChanged += new System.EventHandler(this.ade_Trupp1_EnabledChanged);
			// 
			// ade_Trupp1_10min
			// 
			this.ade_Trupp1_10min.Beschriftung = "10min";
			this.ade_Trupp1_10min.Location = new System.Drawing.Point(224, 32);
			this.ade_Trupp1_10min.Name = "ade_Trupp1_10min";
			this.ade_Trupp1_10min.Size = new System.Drawing.Size(88, 78);
			this.ade_Trupp1_10min.TabIndex = 7;
			this.ade_Trupp1_10min.Zeit = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			this.ade_Trupp1_10min.EnabledChanged += new System.EventHandler(this.ade_Trupp1_EnabledChanged);
			// 
			// ade_Trupp1_Anschliessen
			// 
			this.ade_Trupp1_Anschliessen.Beschriftung = "Anschließen";
			this.ade_Trupp1_Anschliessen.Location = new System.Drawing.Point(136, 32);
			this.ade_Trupp1_Anschliessen.Name = "ade_Trupp1_Anschliessen";
			this.ade_Trupp1_Anschliessen.Size = new System.Drawing.Size(88, 78);
			this.ade_Trupp1_Anschliessen.TabIndex = 6;
			this.ade_Trupp1_Anschliessen.Zeit = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			this.ade_Trupp1_Anschliessen.EnabledChanged += new System.EventHandler(this.ade_Trupp1_EnabledChanged);
			// 
			// ade_Trupp1_Start
			// 
			this.ade_Trupp1_Start.Beschriftung = "Start";
			this.ade_Trupp1_Start.Location = new System.Drawing.Point(48, 32);
			this.ade_Trupp1_Start.Name = "ade_Trupp1_Start";
			this.ade_Trupp1_Start.Size = new System.Drawing.Size(88, 78);
			this.ade_Trupp1_Start.TabIndex = 5;
			this.ade_Trupp1_Start.Zeit = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			this.ade_Trupp1_Start.EnabledChanged += new System.EventHandler(this.ade_Trupp1_EnabledChanged);
			// 
			// lbl_Einsatz_Trupp1_Truppfuehrer
			// 
			this.lbl_Einsatz_Trupp1_Truppfuehrer.Location = new System.Drawing.Point(360, 8);
			this.lbl_Einsatz_Trupp1_Truppfuehrer.Name = "lbl_Einsatz_Trupp1_Truppfuehrer";
			this.lbl_Einsatz_Trupp1_Truppfuehrer.Size = new System.Drawing.Size(128, 16);
			this.lbl_Einsatz_Trupp1_Truppfuehrer.TabIndex = 4;
			this.lbl_Einsatz_Trupp1_Truppfuehrer.Text = "<Truppfuehrer>";
			// 
			// lbl_Einsatz_Trupp1_Funkrufname
			// 
			this.lbl_Einsatz_Trupp1_Funkrufname.Location = new System.Drawing.Point(160, 8);
			this.lbl_Einsatz_Trupp1_Funkrufname.Name = "lbl_Einsatz_Trupp1_Funkrufname";
			this.lbl_Einsatz_Trupp1_Funkrufname.Size = new System.Drawing.Size(120, 16);
			this.lbl_Einsatz_Trupp1_Funkrufname.TabIndex = 3;
			this.lbl_Einsatz_Trupp1_Funkrufname.Text = "<FUNKRUFNAME>";
			// 
			// lbl_Einsatz_Trupp1_Funkrufname_Doppelpunkt
			// 
			this.lbl_Einsatz_Trupp1_Funkrufname_Doppelpunkt.Location = new System.Drawing.Point(80, 8);
			this.lbl_Einsatz_Trupp1_Funkrufname_Doppelpunkt.Name = "lbl_Einsatz_Trupp1_Funkrufname_Doppelpunkt";
			this.lbl_Einsatz_Trupp1_Funkrufname_Doppelpunkt.Size = new System.Drawing.Size(80, 16);
			this.lbl_Einsatz_Trupp1_Funkrufname_Doppelpunkt.TabIndex = 2;
			this.lbl_Einsatz_Trupp1_Funkrufname_Doppelpunkt.Text = "Funkrufname:";
			// 
			// lbl_Einsatz_Trupp1_Truppfuehrer_DoppelPunkt
			// 
			this.lbl_Einsatz_Trupp1_Truppfuehrer_DoppelPunkt.Location = new System.Drawing.Point(288, 8);
			this.lbl_Einsatz_Trupp1_Truppfuehrer_DoppelPunkt.Name = "lbl_Einsatz_Trupp1_Truppfuehrer_DoppelPunkt";
			this.lbl_Einsatz_Trupp1_Truppfuehrer_DoppelPunkt.Size = new System.Drawing.Size(80, 16);
			this.lbl_Einsatz_Trupp1_Truppfuehrer_DoppelPunkt.TabIndex = 1;
			this.lbl_Einsatz_Trupp1_Truppfuehrer_DoppelPunkt.Text = "Truppführer:";
			// 
			// lbl_Einsatz_Trupp1_Druck_Doppelpunkt
			// 
			this.lbl_Einsatz_Trupp1_Druck_Doppelpunkt.Location = new System.Drawing.Point(8, 53);
			this.lbl_Einsatz_Trupp1_Druck_Doppelpunkt.Name = "lbl_Einsatz_Trupp1_Druck_Doppelpunkt";
			this.lbl_Einsatz_Trupp1_Druck_Doppelpunkt.Size = new System.Drawing.Size(40, 16);
			this.lbl_Einsatz_Trupp1_Druck_Doppelpunkt.TabIndex = 1;
			this.lbl_Einsatz_Trupp1_Druck_Doppelpunkt.Text = "Druck:";
			// 
			// lbl_Einsatz_Trupp1_Zeit_Doppelpunkt
			// 
			this.lbl_Einsatz_Trupp1_Zeit_Doppelpunkt.Location = new System.Drawing.Point(16, 76);
			this.lbl_Einsatz_Trupp1_Zeit_Doppelpunkt.Name = "lbl_Einsatz_Trupp1_Zeit_Doppelpunkt";
			this.lbl_Einsatz_Trupp1_Zeit_Doppelpunkt.Size = new System.Drawing.Size(26, 16);
			this.lbl_Einsatz_Trupp1_Zeit_Doppelpunkt.TabIndex = 0;
			this.lbl_Einsatz_Trupp1_Zeit_Doppelpunkt.Text = "Zeit:";
			// 
			// gbx_Einsatz_Trupp1_Einsatzziel_Rückzug
			// 
			this.gbx_Einsatz_Trupp1_Einsatzziel_Rückzug.Controls.Add(this.cbx_Einsatz_Trupp1_Rückzug_DatumJetzt);
			this.gbx_Einsatz_Trupp1_Einsatzziel_Rückzug.Controls.Add(this.lbl_Einsatz_Trupp1_Rueckzug_Doppelpunkt);
			this.gbx_Einsatz_Trupp1_Einsatzziel_Rückzug.Controls.Add(this.lbl_Einsatz_Trupp1_Einsatzziel_Doppelpunkt);
			this.gbx_Einsatz_Trupp1_Einsatzziel_Rückzug.Controls.Add(this.dtp_Einsatz_Trupp1_Rueckzug_Doppelpunkt);
			this.gbx_Einsatz_Trupp1_Einsatzziel_Rückzug.Controls.Add(this.dtp_Einsatz_Trupp1_Einsatzziel_Doppelpunkt);
			this.gbx_Einsatz_Trupp1_Einsatzziel_Rückzug.Controls.Add(this.cbx_Einsatz_Trupp1_EinsatzZiel_DatumJetzt);
			this.gbx_Einsatz_Trupp1_Einsatzziel_Rückzug.Location = new System.Drawing.Point(496, 104);
			this.gbx_Einsatz_Trupp1_Einsatzziel_Rückzug.Name = "gbx_Einsatz_Trupp1_Einsatzziel_Rückzug";
			this.gbx_Einsatz_Trupp1_Einsatzziel_Rückzug.Size = new System.Drawing.Size(104, 96);
			this.gbx_Einsatz_Trupp1_Einsatzziel_Rückzug.TabIndex = 4;
			this.gbx_Einsatz_Trupp1_Einsatzziel_Rückzug.TabStop = false;
			// 
			// cbx_Einsatz_Trupp1_Rückzug_DatumJetzt
			// 
			this.cbx_Einsatz_Trupp1_Rückzug_DatumJetzt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbx_Einsatz_Trupp1_Rückzug_DatumJetzt.Location = new System.Drawing.Point(80, 72);
			this.cbx_Einsatz_Trupp1_Rückzug_DatumJetzt.Name = "cbx_Einsatz_Trupp1_Rückzug_DatumJetzt";
			this.cbx_Einsatz_Trupp1_Rückzug_DatumJetzt.Size = new System.Drawing.Size(14, 14);
			this.cbx_Einsatz_Trupp1_Rückzug_DatumJetzt.TabIndex = 10;
			// 
			// lbl_Einsatz_Trupp1_Rueckzug_Doppelpunkt
			// 
			this.lbl_Einsatz_Trupp1_Rueckzug_Doppelpunkt.Location = new System.Drawing.Point(8, 56);
			this.lbl_Einsatz_Trupp1_Rueckzug_Doppelpunkt.Name = "lbl_Einsatz_Trupp1_Rueckzug_Doppelpunkt";
			this.lbl_Einsatz_Trupp1_Rueckzug_Doppelpunkt.Size = new System.Drawing.Size(56, 16);
			this.lbl_Einsatz_Trupp1_Rueckzug_Doppelpunkt.TabIndex = 8;
			this.lbl_Einsatz_Trupp1_Rueckzug_Doppelpunkt.Text = "Rückzug:";
			// 
			// lbl_Einsatz_Trupp1_Einsatzziel_Doppelpunkt
			// 
			this.lbl_Einsatz_Trupp1_Einsatzziel_Doppelpunkt.Location = new System.Drawing.Point(8, 8);
			this.lbl_Einsatz_Trupp1_Einsatzziel_Doppelpunkt.Name = "lbl_Einsatz_Trupp1_Einsatzziel_Doppelpunkt";
			this.lbl_Einsatz_Trupp1_Einsatzziel_Doppelpunkt.Size = new System.Drawing.Size(64, 16);
			this.lbl_Einsatz_Trupp1_Einsatzziel_Doppelpunkt.TabIndex = 7;
			this.lbl_Einsatz_Trupp1_Einsatzziel_Doppelpunkt.Text = "Einsatzziel:";
			// 
			// dtp_Einsatz_Trupp1_Rueckzug_Doppelpunkt
			// 
			this.dtp_Einsatz_Trupp1_Rueckzug_Doppelpunkt.ContextMenu = this.ctx_ZeitAktualisieren;
			this.dtp_Einsatz_Trupp1_Rueckzug_Doppelpunkt.CustomFormat = "hh:mm:ss";
			this.dtp_Einsatz_Trupp1_Rueckzug_Doppelpunkt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Einsatz_Trupp1_Rueckzug_Doppelpunkt.Location = new System.Drawing.Point(8, 72);
			this.dtp_Einsatz_Trupp1_Rueckzug_Doppelpunkt.Name = "dtp_Einsatz_Trupp1_Rueckzug_Doppelpunkt";
			this.dtp_Einsatz_Trupp1_Rueckzug_Doppelpunkt.ShowUpDown = true;
			this.dtp_Einsatz_Trupp1_Rueckzug_Doppelpunkt.Size = new System.Drawing.Size(72, 20);
			this.dtp_Einsatz_Trupp1_Rueckzug_Doppelpunkt.TabIndex = 9;
			this.dtp_Einsatz_Trupp1_Rueckzug_Doppelpunkt.Value = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			// 
			// dtp_Einsatz_Trupp1_Einsatzziel_Doppelpunkt
			// 
			this.dtp_Einsatz_Trupp1_Einsatzziel_Doppelpunkt.ContextMenu = this.ctx_ZeitAktualisieren;
			this.dtp_Einsatz_Trupp1_Einsatzziel_Doppelpunkt.CustomFormat = "hh:mm:ss";
			this.dtp_Einsatz_Trupp1_Einsatzziel_Doppelpunkt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Einsatz_Trupp1_Einsatzziel_Doppelpunkt.Location = new System.Drawing.Point(8, 24);
			this.dtp_Einsatz_Trupp1_Einsatzziel_Doppelpunkt.Name = "dtp_Einsatz_Trupp1_Einsatzziel_Doppelpunkt";
			this.dtp_Einsatz_Trupp1_Einsatzziel_Doppelpunkt.ShowUpDown = true;
			this.dtp_Einsatz_Trupp1_Einsatzziel_Doppelpunkt.Size = new System.Drawing.Size(72, 20);
			this.dtp_Einsatz_Trupp1_Einsatzziel_Doppelpunkt.TabIndex = 2;
			this.dtp_Einsatz_Trupp1_Einsatzziel_Doppelpunkt.Value = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			// 
			// cbx_Einsatz_Trupp1_EinsatzZiel_DatumJetzt
			// 
			this.cbx_Einsatz_Trupp1_EinsatzZiel_DatumJetzt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbx_Einsatz_Trupp1_EinsatzZiel_DatumJetzt.Location = new System.Drawing.Point(80, 24);
			this.cbx_Einsatz_Trupp1_EinsatzZiel_DatumJetzt.Name = "cbx_Einsatz_Trupp1_EinsatzZiel_DatumJetzt";
			this.cbx_Einsatz_Trupp1_EinsatzZiel_DatumJetzt.Size = new System.Drawing.Size(14, 14);
			this.cbx_Einsatz_Trupp1_EinsatzZiel_DatumJetzt.TabIndex = 7;
			// 
			// txt_Einsatz_Trupp1_Kommentar
			// 
			this.txt_Einsatz_Trupp1_Kommentar.Location = new System.Drawing.Point(48, 136);
			this.txt_Einsatz_Trupp1_Kommentar.Name = "txt_Einsatz_Trupp1_Kommentar";
			this.txt_Einsatz_Trupp1_Kommentar.Size = new System.Drawing.Size(440, 64);
			this.txt_Einsatz_Trupp1_Kommentar.TabIndex = 4;
			this.txt_Einsatz_Trupp1_Kommentar.Text = "";
			// 
			// tabpage_AGT_Einsatz_Trupp2
			// 
			this.tabpage_AGT_Einsatz_Trupp2.Controls.Add(this.gbx_Einsatz_Trupp2);
			this.tabpage_AGT_Einsatz_Trupp2.Location = new System.Drawing.Point(4, 22);
			this.tabpage_AGT_Einsatz_Trupp2.Name = "tabpage_AGT_Einsatz_Trupp2";
			this.tabpage_AGT_Einsatz_Trupp2.Size = new System.Drawing.Size(616, 222);
			this.tabpage_AGT_Einsatz_Trupp2.TabIndex = 1;
			this.tabpage_AGT_Einsatz_Trupp2.Text = "Trupp2";
			// 
			// gbx_Einsatz_Trupp2
			// 
			this.gbx_Einsatz_Trupp2.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_Einsatz_Trupp2.Controls.Add(this.lbl_Einsatz_Trupp2_Kommentar);
			this.gbx_Einsatz_Trupp2.Controls.Add(this.ade_Trupp2_30min);
			this.gbx_Einsatz_Trupp2.Controls.Add(this.ade_Trupp2_20min);
			this.gbx_Einsatz_Trupp2.Controls.Add(this.ade_Trupp2_10min);
			this.gbx_Einsatz_Trupp2.Controls.Add(this.ade_Trupp2_Anschliessen);
			this.gbx_Einsatz_Trupp2.Controls.Add(this.ade_Trupp2_Start);
			this.gbx_Einsatz_Trupp2.Controls.Add(this.lbl_Einsatz_Trupp2_Truppfuehrer);
			this.gbx_Einsatz_Trupp2.Controls.Add(this.lbl_Einsatz_Trupp2_Funkrufname);
			this.gbx_Einsatz_Trupp2.Controls.Add(this.lbl_Einsatz_Trupp2_Funkrufname_Doppelpunkt);
			this.gbx_Einsatz_Trupp2.Controls.Add(this.lbl_Einsatz_Trupp2_Truppfuehrer_DoppelPunkt);
			this.gbx_Einsatz_Trupp2.Controls.Add(this.lbl_Einsatz_Trupp2_Druck_Doppelpunkt);
			this.gbx_Einsatz_Trupp2.Controls.Add(this.lbl_Einsatz_Trupp2_Zeit_Doppelpunkt);
			this.gbx_Einsatz_Trupp2.Controls.Add(this.gbx_Einsatz_Trupp2_Einsatzziel_Rückzug);
			this.gbx_Einsatz_Trupp2.Controls.Add(this.txt_Einsatz_Trupp2_Kommentar);
			this.gbx_Einsatz_Trupp2.Location = new System.Drawing.Point(8, 8);
			this.gbx_Einsatz_Trupp2.Name = "gbx_Einsatz_Trupp2";
			this.gbx_Einsatz_Trupp2.Size = new System.Drawing.Size(608, 208);
			this.gbx_Einsatz_Trupp2.TabIndex = 4;
			this.gbx_Einsatz_Trupp2.TabStop = false;
			// 
			// lbl_Einsatz_Trupp2_Kommentar
			// 
			this.lbl_Einsatz_Trupp2_Kommentar.Location = new System.Drawing.Point(48, 120);
			this.lbl_Einsatz_Trupp2_Kommentar.Name = "lbl_Einsatz_Trupp2_Kommentar";
			this.lbl_Einsatz_Trupp2_Kommentar.Size = new System.Drawing.Size(66, 12);
			this.lbl_Einsatz_Trupp2_Kommentar.TabIndex = 15;
			this.lbl_Einsatz_Trupp2_Kommentar.Text = "Kommentar";
			// 
			// ade_Trupp2_30min
			// 
			this.ade_Trupp2_30min.Beschriftung = "30min";
			this.ade_Trupp2_30min.Location = new System.Drawing.Point(400, 32);
			this.ade_Trupp2_30min.Name = "ade_Trupp2_30min";
			this.ade_Trupp2_30min.Size = new System.Drawing.Size(88, 78);
			this.ade_Trupp2_30min.TabIndex = 14;
			this.ade_Trupp2_30min.Zeit = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			this.ade_Trupp2_30min.EnabledChanged += new System.EventHandler(this.ade_Trupp2_EnabledChanged);
			// 
			// ade_Trupp2_20min
			// 
			this.ade_Trupp2_20min.Beschriftung = "20min";
			this.ade_Trupp2_20min.Location = new System.Drawing.Point(312, 32);
			this.ade_Trupp2_20min.Name = "ade_Trupp2_20min";
			this.ade_Trupp2_20min.Size = new System.Drawing.Size(88, 78);
			this.ade_Trupp2_20min.TabIndex = 13;
			this.ade_Trupp2_20min.Zeit = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			this.ade_Trupp2_20min.EnabledChanged += new System.EventHandler(this.ade_Trupp2_EnabledChanged);
			// 
			// ade_Trupp2_10min
			// 
			this.ade_Trupp2_10min.Beschriftung = "10min";
			this.ade_Trupp2_10min.Location = new System.Drawing.Point(224, 32);
			this.ade_Trupp2_10min.Name = "ade_Trupp2_10min";
			this.ade_Trupp2_10min.Size = new System.Drawing.Size(88, 78);
			this.ade_Trupp2_10min.TabIndex = 12;
			this.ade_Trupp2_10min.Zeit = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			this.ade_Trupp2_10min.EnabledChanged += new System.EventHandler(this.ade_Trupp2_EnabledChanged);
			// 
			// ade_Trupp2_Anschliessen
			// 
			this.ade_Trupp2_Anschliessen.Beschriftung = "Anschließen";
			this.ade_Trupp2_Anschliessen.Location = new System.Drawing.Point(136, 32);
			this.ade_Trupp2_Anschliessen.Name = "ade_Trupp2_Anschliessen";
			this.ade_Trupp2_Anschliessen.Size = new System.Drawing.Size(88, 78);
			this.ade_Trupp2_Anschliessen.TabIndex = 11;
			this.ade_Trupp2_Anschliessen.Zeit = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			this.ade_Trupp2_Anschliessen.EnabledChanged += new System.EventHandler(this.ade_Trupp2_EnabledChanged);
			// 
			// ade_Trupp2_Start
			// 
			this.ade_Trupp2_Start.Beschriftung = "Start";
			this.ade_Trupp2_Start.Location = new System.Drawing.Point(48, 32);
			this.ade_Trupp2_Start.Name = "ade_Trupp2_Start";
			this.ade_Trupp2_Start.Size = new System.Drawing.Size(88, 78);
			this.ade_Trupp2_Start.TabIndex = 10;
			this.ade_Trupp2_Start.Zeit = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			this.ade_Trupp2_Start.EnabledChanged += new System.EventHandler(this.ade_Trupp2_EnabledChanged);
			// 
			// lbl_Einsatz_Trupp2_Truppfuehrer
			// 
			this.lbl_Einsatz_Trupp2_Truppfuehrer.Location = new System.Drawing.Point(360, 8);
			this.lbl_Einsatz_Trupp2_Truppfuehrer.Name = "lbl_Einsatz_Trupp2_Truppfuehrer";
			this.lbl_Einsatz_Trupp2_Truppfuehrer.Size = new System.Drawing.Size(128, 16);
			this.lbl_Einsatz_Trupp2_Truppfuehrer.TabIndex = 4;
			this.lbl_Einsatz_Trupp2_Truppfuehrer.Text = "<Truppfuehrer>";
			// 
			// lbl_Einsatz_Trupp2_Funkrufname
			// 
			this.lbl_Einsatz_Trupp2_Funkrufname.Location = new System.Drawing.Point(160, 8);
			this.lbl_Einsatz_Trupp2_Funkrufname.Name = "lbl_Einsatz_Trupp2_Funkrufname";
			this.lbl_Einsatz_Trupp2_Funkrufname.Size = new System.Drawing.Size(120, 16);
			this.lbl_Einsatz_Trupp2_Funkrufname.TabIndex = 3;
			this.lbl_Einsatz_Trupp2_Funkrufname.Text = "<FUNKRUFNAME>";
			// 
			// lbl_Einsatz_Trupp2_Funkrufname_Doppelpunkt
			// 
			this.lbl_Einsatz_Trupp2_Funkrufname_Doppelpunkt.Location = new System.Drawing.Point(80, 8);
			this.lbl_Einsatz_Trupp2_Funkrufname_Doppelpunkt.Name = "lbl_Einsatz_Trupp2_Funkrufname_Doppelpunkt";
			this.lbl_Einsatz_Trupp2_Funkrufname_Doppelpunkt.Size = new System.Drawing.Size(80, 16);
			this.lbl_Einsatz_Trupp2_Funkrufname_Doppelpunkt.TabIndex = 2;
			this.lbl_Einsatz_Trupp2_Funkrufname_Doppelpunkt.Text = "Funkrufname:";
			// 
			// lbl_Einsatz_Trupp2_Truppfuehrer_DoppelPunkt
			// 
			this.lbl_Einsatz_Trupp2_Truppfuehrer_DoppelPunkt.Location = new System.Drawing.Point(288, 8);
			this.lbl_Einsatz_Trupp2_Truppfuehrer_DoppelPunkt.Name = "lbl_Einsatz_Trupp2_Truppfuehrer_DoppelPunkt";
			this.lbl_Einsatz_Trupp2_Truppfuehrer_DoppelPunkt.Size = new System.Drawing.Size(80, 16);
			this.lbl_Einsatz_Trupp2_Truppfuehrer_DoppelPunkt.TabIndex = 1;
			this.lbl_Einsatz_Trupp2_Truppfuehrer_DoppelPunkt.Text = "Truppführer:";
			// 
			// lbl_Einsatz_Trupp2_Druck_Doppelpunkt
			// 
			this.lbl_Einsatz_Trupp2_Druck_Doppelpunkt.Location = new System.Drawing.Point(8, 53);
			this.lbl_Einsatz_Trupp2_Druck_Doppelpunkt.Name = "lbl_Einsatz_Trupp2_Druck_Doppelpunkt";
			this.lbl_Einsatz_Trupp2_Druck_Doppelpunkt.Size = new System.Drawing.Size(40, 16);
			this.lbl_Einsatz_Trupp2_Druck_Doppelpunkt.TabIndex = 1;
			this.lbl_Einsatz_Trupp2_Druck_Doppelpunkt.Text = "Druck:";
			// 
			// lbl_Einsatz_Trupp2_Zeit_Doppelpunkt
			// 
			this.lbl_Einsatz_Trupp2_Zeit_Doppelpunkt.Location = new System.Drawing.Point(16, 76);
			this.lbl_Einsatz_Trupp2_Zeit_Doppelpunkt.Name = "lbl_Einsatz_Trupp2_Zeit_Doppelpunkt";
			this.lbl_Einsatz_Trupp2_Zeit_Doppelpunkt.Size = new System.Drawing.Size(26, 16);
			this.lbl_Einsatz_Trupp2_Zeit_Doppelpunkt.TabIndex = 0;
			this.lbl_Einsatz_Trupp2_Zeit_Doppelpunkt.Text = "Zeit:";
			// 
			// gbx_Einsatz_Trupp2_Einsatzziel_Rückzug
			// 
			this.gbx_Einsatz_Trupp2_Einsatzziel_Rückzug.Controls.Add(this.cbx_Einsatz_Trupp2_Rueckzug_DatumJetzt);
			this.gbx_Einsatz_Trupp2_Einsatzziel_Rückzug.Controls.Add(this.cbx_Einsatz_Trupp2_EinsatzZiel_DatumJetzt);
			this.gbx_Einsatz_Trupp2_Einsatzziel_Rückzug.Controls.Add(this.lbl_Einsatz_Trupp2_Rueckzug_Doppelpunkt);
			this.gbx_Einsatz_Trupp2_Einsatzziel_Rückzug.Controls.Add(this.lbl_Einsatz_Trupp2_Einsatzziel_Doppelpunkt);
			this.gbx_Einsatz_Trupp2_Einsatzziel_Rückzug.Controls.Add(this.dtp_Einsatz_Trupp2_Rueckzug_Doppelpunkt);
			this.gbx_Einsatz_Trupp2_Einsatzziel_Rückzug.Controls.Add(this.dtp_Einsatz_Trupp2_Einsatzziel_Doppelpunkt);
			this.gbx_Einsatz_Trupp2_Einsatzziel_Rückzug.Location = new System.Drawing.Point(496, 104);
			this.gbx_Einsatz_Trupp2_Einsatzziel_Rückzug.Name = "gbx_Einsatz_Trupp2_Einsatzziel_Rückzug";
			this.gbx_Einsatz_Trupp2_Einsatzziel_Rückzug.Size = new System.Drawing.Size(104, 96);
			this.gbx_Einsatz_Trupp2_Einsatzziel_Rückzug.TabIndex = 4;
			this.gbx_Einsatz_Trupp2_Einsatzziel_Rückzug.TabStop = false;
			// 
			// cbx_Einsatz_Trupp2_Rueckzug_DatumJetzt
			// 
			this.cbx_Einsatz_Trupp2_Rueckzug_DatumJetzt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbx_Einsatz_Trupp2_Rueckzug_DatumJetzt.Location = new System.Drawing.Point(80, 72);
			this.cbx_Einsatz_Trupp2_Rueckzug_DatumJetzt.Name = "cbx_Einsatz_Trupp2_Rueckzug_DatumJetzt";
			this.cbx_Einsatz_Trupp2_Rueckzug_DatumJetzt.Size = new System.Drawing.Size(14, 14);
			this.cbx_Einsatz_Trupp2_Rueckzug_DatumJetzt.TabIndex = 12;
			// 
			// cbx_Einsatz_Trupp2_EinsatzZiel_DatumJetzt
			// 
			this.cbx_Einsatz_Trupp2_EinsatzZiel_DatumJetzt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbx_Einsatz_Trupp2_EinsatzZiel_DatumJetzt.Location = new System.Drawing.Point(80, 24);
			this.cbx_Einsatz_Trupp2_EinsatzZiel_DatumJetzt.Name = "cbx_Einsatz_Trupp2_EinsatzZiel_DatumJetzt";
			this.cbx_Einsatz_Trupp2_EinsatzZiel_DatumJetzt.Size = new System.Drawing.Size(14, 14);
			this.cbx_Einsatz_Trupp2_EinsatzZiel_DatumJetzt.TabIndex = 11;
			// 
			// lbl_Einsatz_Trupp2_Rueckzug_Doppelpunkt
			// 
			this.lbl_Einsatz_Trupp2_Rueckzug_Doppelpunkt.Location = new System.Drawing.Point(8, 56);
			this.lbl_Einsatz_Trupp2_Rueckzug_Doppelpunkt.Name = "lbl_Einsatz_Trupp2_Rueckzug_Doppelpunkt";
			this.lbl_Einsatz_Trupp2_Rueckzug_Doppelpunkt.Size = new System.Drawing.Size(56, 16);
			this.lbl_Einsatz_Trupp2_Rueckzug_Doppelpunkt.TabIndex = 8;
			this.lbl_Einsatz_Trupp2_Rueckzug_Doppelpunkt.Text = "Rückzug:";
			// 
			// lbl_Einsatz_Trupp2_Einsatzziel_Doppelpunkt
			// 
			this.lbl_Einsatz_Trupp2_Einsatzziel_Doppelpunkt.Location = new System.Drawing.Point(8, 8);
			this.lbl_Einsatz_Trupp2_Einsatzziel_Doppelpunkt.Name = "lbl_Einsatz_Trupp2_Einsatzziel_Doppelpunkt";
			this.lbl_Einsatz_Trupp2_Einsatzziel_Doppelpunkt.Size = new System.Drawing.Size(64, 16);
			this.lbl_Einsatz_Trupp2_Einsatzziel_Doppelpunkt.TabIndex = 7;
			this.lbl_Einsatz_Trupp2_Einsatzziel_Doppelpunkt.Text = "Einsatzziel:";
			// 
			// dtp_Einsatz_Trupp2_Rueckzug_Doppelpunkt
			// 
			this.dtp_Einsatz_Trupp2_Rueckzug_Doppelpunkt.ContextMenu = this.ctx_ZeitAktualisieren;
			this.dtp_Einsatz_Trupp2_Rueckzug_Doppelpunkt.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtp_Einsatz_Trupp2_Rueckzug_Doppelpunkt.Location = new System.Drawing.Point(8, 72);
			this.dtp_Einsatz_Trupp2_Rueckzug_Doppelpunkt.Name = "dtp_Einsatz_Trupp2_Rueckzug_Doppelpunkt";
			this.dtp_Einsatz_Trupp2_Rueckzug_Doppelpunkt.ShowUpDown = true;
			this.dtp_Einsatz_Trupp2_Rueckzug_Doppelpunkt.Size = new System.Drawing.Size(72, 20);
			this.dtp_Einsatz_Trupp2_Rueckzug_Doppelpunkt.TabIndex = 9;
			// 
			// dtp_Einsatz_Trupp2_Einsatzziel_Doppelpunkt
			// 
			this.dtp_Einsatz_Trupp2_Einsatzziel_Doppelpunkt.ContextMenu = this.ctx_ZeitAktualisieren;
			this.dtp_Einsatz_Trupp2_Einsatzziel_Doppelpunkt.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtp_Einsatz_Trupp2_Einsatzziel_Doppelpunkt.Location = new System.Drawing.Point(8, 24);
			this.dtp_Einsatz_Trupp2_Einsatzziel_Doppelpunkt.Name = "dtp_Einsatz_Trupp2_Einsatzziel_Doppelpunkt";
			this.dtp_Einsatz_Trupp2_Einsatzziel_Doppelpunkt.ShowUpDown = true;
			this.dtp_Einsatz_Trupp2_Einsatzziel_Doppelpunkt.Size = new System.Drawing.Size(72, 20);
			this.dtp_Einsatz_Trupp2_Einsatzziel_Doppelpunkt.TabIndex = 2;
			// 
			// txt_Einsatz_Trupp2_Kommentar
			// 
			this.txt_Einsatz_Trupp2_Kommentar.Location = new System.Drawing.Point(48, 136);
			this.txt_Einsatz_Trupp2_Kommentar.Name = "txt_Einsatz_Trupp2_Kommentar";
			this.txt_Einsatz_Trupp2_Kommentar.Size = new System.Drawing.Size(440, 64);
			this.txt_Einsatz_Trupp2_Kommentar.TabIndex = 4;
			this.txt_Einsatz_Trupp2_Kommentar.Text = "";
			// 
			// tabpage_AGT_Einsatz_Sicherungtrupp
			// 
			this.tabpage_AGT_Einsatz_Sicherungtrupp.Controls.Add(this.gbx_Einsatz_Sicherungstrupp);
			this.tabpage_AGT_Einsatz_Sicherungtrupp.Location = new System.Drawing.Point(4, 22);
			this.tabpage_AGT_Einsatz_Sicherungtrupp.Name = "tabpage_AGT_Einsatz_Sicherungtrupp";
			this.tabpage_AGT_Einsatz_Sicherungtrupp.Size = new System.Drawing.Size(616, 222);
			this.tabpage_AGT_Einsatz_Sicherungtrupp.TabIndex = 2;
			this.tabpage_AGT_Einsatz_Sicherungtrupp.Text = "Sicherungstrupp";
			// 
			// gbx_Einsatz_Sicherungstrupp
			// 
			this.gbx_Einsatz_Sicherungstrupp.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_Einsatz_Sicherungstrupp.Controls.Add(this.lbl_Einsatz_Sicherungstrupp_Kommentar);
			this.gbx_Einsatz_Sicherungstrupp.Controls.Add(this.ade_Sicherungstrupp_30min);
			this.gbx_Einsatz_Sicherungstrupp.Controls.Add(this.ade_Sicherungstrupp_20min);
			this.gbx_Einsatz_Sicherungstrupp.Controls.Add(this.ade_Sicherungstrupp_10min);
			this.gbx_Einsatz_Sicherungstrupp.Controls.Add(this.ade_Sicherungstrupp_Anschliessen);
			this.gbx_Einsatz_Sicherungstrupp.Controls.Add(this.ade_Sicherungstrupp_Start);
			this.gbx_Einsatz_Sicherungstrupp.Controls.Add(this.lbl_Einsatz_Sicherungstrupp_Truppfuehrer);
			this.gbx_Einsatz_Sicherungstrupp.Controls.Add(this.lbl_Einsatz_Sicherungstrupp_Funkrufname);
			this.gbx_Einsatz_Sicherungstrupp.Controls.Add(this.lbl_Einsatz_Sicherungstrupp_Funkrufname_Doppelpunkt);
			this.gbx_Einsatz_Sicherungstrupp.Controls.Add(this.lbl_Einsatz_Trupp2_Sicherungstrupp_Truppfuehrer_DoppelPunkt);
			this.gbx_Einsatz_Sicherungstrupp.Controls.Add(this.lbl_Einsatz_Sicherungtrupp_Druck_Doppelpunkt);
			this.gbx_Einsatz_Sicherungstrupp.Controls.Add(this.lbl_Einsatz_Sicherungstrupp_Zeit_Doppelpunkt);
			this.gbx_Einsatz_Sicherungstrupp.Controls.Add(this.gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug);
			this.gbx_Einsatz_Sicherungstrupp.Controls.Add(this.txt_Einsatz_Sicherungstrupp_Kommentar);
			this.gbx_Einsatz_Sicherungstrupp.Location = new System.Drawing.Point(8, 8);
			this.gbx_Einsatz_Sicherungstrupp.Name = "gbx_Einsatz_Sicherungstrupp";
			this.gbx_Einsatz_Sicherungstrupp.Size = new System.Drawing.Size(608, 208);
			this.gbx_Einsatz_Sicherungstrupp.TabIndex = 5;
			this.gbx_Einsatz_Sicherungstrupp.TabStop = false;
			// 
			// lbl_Einsatz_Sicherungstrupp_Kommentar
			// 
			this.lbl_Einsatz_Sicherungstrupp_Kommentar.Location = new System.Drawing.Point(48, 120);
			this.lbl_Einsatz_Sicherungstrupp_Kommentar.Name = "lbl_Einsatz_Sicherungstrupp_Kommentar";
			this.lbl_Einsatz_Sicherungstrupp_Kommentar.Size = new System.Drawing.Size(66, 12);
			this.lbl_Einsatz_Sicherungstrupp_Kommentar.TabIndex = 16;
			this.lbl_Einsatz_Sicherungstrupp_Kommentar.Text = "Kommentar";
			// 
			// ade_Sicherungstrupp_30min
			// 
			this.ade_Sicherungstrupp_30min.Beschriftung = "30min";
			this.ade_Sicherungstrupp_30min.Location = new System.Drawing.Point(400, 32);
			this.ade_Sicherungstrupp_30min.Name = "ade_Sicherungstrupp_30min";
			this.ade_Sicherungstrupp_30min.Size = new System.Drawing.Size(88, 78);
			this.ade_Sicherungstrupp_30min.TabIndex = 14;
			this.ade_Sicherungstrupp_30min.Zeit = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			this.ade_Sicherungstrupp_30min.EnabledChanged += new System.EventHandler(this.ade_Sicherungstrupp_EnabledChanged);
			// 
			// ade_Sicherungstrupp_20min
			// 
			this.ade_Sicherungstrupp_20min.Beschriftung = "20min";
			this.ade_Sicherungstrupp_20min.Location = new System.Drawing.Point(312, 32);
			this.ade_Sicherungstrupp_20min.Name = "ade_Sicherungstrupp_20min";
			this.ade_Sicherungstrupp_20min.Size = new System.Drawing.Size(88, 78);
			this.ade_Sicherungstrupp_20min.TabIndex = 13;
			this.ade_Sicherungstrupp_20min.Zeit = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			this.ade_Sicherungstrupp_20min.EnabledChanged += new System.EventHandler(this.ade_Sicherungstrupp_EnabledChanged);
			// 
			// ade_Sicherungstrupp_10min
			// 
			this.ade_Sicherungstrupp_10min.Beschriftung = "10min";
			this.ade_Sicherungstrupp_10min.Location = new System.Drawing.Point(224, 32);
			this.ade_Sicherungstrupp_10min.Name = "ade_Sicherungstrupp_10min";
			this.ade_Sicherungstrupp_10min.Size = new System.Drawing.Size(88, 78);
			this.ade_Sicherungstrupp_10min.TabIndex = 12;
			this.ade_Sicherungstrupp_10min.Zeit = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			this.ade_Sicherungstrupp_10min.EnabledChanged += new System.EventHandler(this.ade_Sicherungstrupp_EnabledChanged);
			// 
			// ade_Sicherungstrupp_Anschliessen
			// 
			this.ade_Sicherungstrupp_Anschliessen.Beschriftung = "Anschließen";
			this.ade_Sicherungstrupp_Anschliessen.Location = new System.Drawing.Point(136, 32);
			this.ade_Sicherungstrupp_Anschliessen.Name = "ade_Sicherungstrupp_Anschliessen";
			this.ade_Sicherungstrupp_Anschliessen.Size = new System.Drawing.Size(88, 78);
			this.ade_Sicherungstrupp_Anschliessen.TabIndex = 11;
			this.ade_Sicherungstrupp_Anschliessen.Zeit = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			this.ade_Sicherungstrupp_Anschliessen.EnabledChanged += new System.EventHandler(this.ade_Sicherungstrupp_EnabledChanged);
			// 
			// ade_Sicherungstrupp_Start
			// 
			this.ade_Sicherungstrupp_Start.Beschriftung = "Start";
			this.ade_Sicherungstrupp_Start.Location = new System.Drawing.Point(48, 32);
			this.ade_Sicherungstrupp_Start.Name = "ade_Sicherungstrupp_Start";
			this.ade_Sicherungstrupp_Start.Size = new System.Drawing.Size(88, 78);
			this.ade_Sicherungstrupp_Start.TabIndex = 10;
			this.ade_Sicherungstrupp_Start.Zeit = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			this.ade_Sicherungstrupp_Start.EnabledChanged += new System.EventHandler(this.ade_Sicherungstrupp_EnabledChanged);
			// 
			// lbl_Einsatz_Sicherungstrupp_Truppfuehrer
			// 
			this.lbl_Einsatz_Sicherungstrupp_Truppfuehrer.Location = new System.Drawing.Point(360, 8);
			this.lbl_Einsatz_Sicherungstrupp_Truppfuehrer.Name = "lbl_Einsatz_Sicherungstrupp_Truppfuehrer";
			this.lbl_Einsatz_Sicherungstrupp_Truppfuehrer.Size = new System.Drawing.Size(128, 16);
			this.lbl_Einsatz_Sicherungstrupp_Truppfuehrer.TabIndex = 4;
			this.lbl_Einsatz_Sicherungstrupp_Truppfuehrer.Text = "<Truppfuehrer>";
			// 
			// lbl_Einsatz_Sicherungstrupp_Funkrufname
			// 
			this.lbl_Einsatz_Sicherungstrupp_Funkrufname.Location = new System.Drawing.Point(160, 8);
			this.lbl_Einsatz_Sicherungstrupp_Funkrufname.Name = "lbl_Einsatz_Sicherungstrupp_Funkrufname";
			this.lbl_Einsatz_Sicherungstrupp_Funkrufname.Size = new System.Drawing.Size(120, 16);
			this.lbl_Einsatz_Sicherungstrupp_Funkrufname.TabIndex = 3;
			this.lbl_Einsatz_Sicherungstrupp_Funkrufname.Text = "<FUNKRUFNAME>";
			// 
			// lbl_Einsatz_Sicherungstrupp_Funkrufname_Doppelpunkt
			// 
			this.lbl_Einsatz_Sicherungstrupp_Funkrufname_Doppelpunkt.Location = new System.Drawing.Point(80, 8);
			this.lbl_Einsatz_Sicherungstrupp_Funkrufname_Doppelpunkt.Name = "lbl_Einsatz_Sicherungstrupp_Funkrufname_Doppelpunkt";
			this.lbl_Einsatz_Sicherungstrupp_Funkrufname_Doppelpunkt.Size = new System.Drawing.Size(80, 16);
			this.lbl_Einsatz_Sicherungstrupp_Funkrufname_Doppelpunkt.TabIndex = 2;
			this.lbl_Einsatz_Sicherungstrupp_Funkrufname_Doppelpunkt.Text = "Funkrufname:";
			// 
			// lbl_Einsatz_Trupp2_Sicherungstrupp_Truppfuehrer_DoppelPunkt
			// 
			this.lbl_Einsatz_Trupp2_Sicherungstrupp_Truppfuehrer_DoppelPunkt.Location = new System.Drawing.Point(288, 8);
			this.lbl_Einsatz_Trupp2_Sicherungstrupp_Truppfuehrer_DoppelPunkt.Name = "lbl_Einsatz_Trupp2_Sicherungstrupp_Truppfuehrer_DoppelPunkt";
			this.lbl_Einsatz_Trupp2_Sicherungstrupp_Truppfuehrer_DoppelPunkt.Size = new System.Drawing.Size(80, 16);
			this.lbl_Einsatz_Trupp2_Sicherungstrupp_Truppfuehrer_DoppelPunkt.TabIndex = 1;
			this.lbl_Einsatz_Trupp2_Sicherungstrupp_Truppfuehrer_DoppelPunkt.Text = "Truppführer:";
			// 
			// lbl_Einsatz_Sicherungtrupp_Druck_Doppelpunkt
			// 
			this.lbl_Einsatz_Sicherungtrupp_Druck_Doppelpunkt.Location = new System.Drawing.Point(8, 53);
			this.lbl_Einsatz_Sicherungtrupp_Druck_Doppelpunkt.Name = "lbl_Einsatz_Sicherungtrupp_Druck_Doppelpunkt";
			this.lbl_Einsatz_Sicherungtrupp_Druck_Doppelpunkt.Size = new System.Drawing.Size(40, 16);
			this.lbl_Einsatz_Sicherungtrupp_Druck_Doppelpunkt.TabIndex = 1;
			this.lbl_Einsatz_Sicherungtrupp_Druck_Doppelpunkt.Text = "Druck:";
			// 
			// lbl_Einsatz_Sicherungstrupp_Zeit_Doppelpunkt
			// 
			this.lbl_Einsatz_Sicherungstrupp_Zeit_Doppelpunkt.Location = new System.Drawing.Point(16, 76);
			this.lbl_Einsatz_Sicherungstrupp_Zeit_Doppelpunkt.Name = "lbl_Einsatz_Sicherungstrupp_Zeit_Doppelpunkt";
			this.lbl_Einsatz_Sicherungstrupp_Zeit_Doppelpunkt.Size = new System.Drawing.Size(26, 16);
			this.lbl_Einsatz_Sicherungstrupp_Zeit_Doppelpunkt.TabIndex = 0;
			this.lbl_Einsatz_Sicherungstrupp_Zeit_Doppelpunkt.Text = "Zeit:";
			// 
			// gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug
			// 
			this.gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug.Controls.Add(this.lbl_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt);
			this.gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug.Controls.Add(this.lbl_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt);
			this.gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug.Controls.Add(this.dtp_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt);
			this.gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug.Controls.Add(this.dtp_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt);
			this.gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug.Controls.Add(this.cbx_Einsatz_Sicherungstrupp_EinsatzZiel_DatumJetzt);
			this.gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug.Controls.Add(this.cbx_Einsatz_Sicherungstrupp_Rueckzug_DatumJetzt);
			this.gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug.Location = new System.Drawing.Point(496, 104);
			this.gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug.Name = "gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug";
			this.gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug.Size = new System.Drawing.Size(104, 96);
			this.gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug.TabIndex = 4;
			this.gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug.TabStop = false;
			// 
			// lbl_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt
			// 
			this.lbl_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt.Location = new System.Drawing.Point(8, 56);
			this.lbl_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt.Name = "lbl_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt";
			this.lbl_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt.Size = new System.Drawing.Size(56, 16);
			this.lbl_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt.TabIndex = 8;
			this.lbl_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt.Text = "Rückzug:";
			// 
			// lbl_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt
			// 
			this.lbl_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt.Location = new System.Drawing.Point(8, 8);
			this.lbl_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt.Name = "lbl_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt";
			this.lbl_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt.Size = new System.Drawing.Size(64, 16);
			this.lbl_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt.TabIndex = 7;
			this.lbl_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt.Text = "Einsatzziel:";
			// 
			// dtp_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt
			// 
			this.dtp_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt.ContextMenu = this.ctx_ZeitAktualisieren;
			this.dtp_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtp_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt.Location = new System.Drawing.Point(8, 72);
			this.dtp_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt.Name = "dtp_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt";
			this.dtp_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt.ShowUpDown = true;
			this.dtp_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt.Size = new System.Drawing.Size(72, 20);
			this.dtp_Einsatz_Sicherungstrupp_Rueckzug_Doppelpunkt.TabIndex = 9;
			// 
			// dtp_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt
			// 
			this.dtp_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt.ContextMenu = this.ctx_ZeitAktualisieren;
			this.dtp_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtp_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt.Location = new System.Drawing.Point(8, 24);
			this.dtp_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt.Name = "dtp_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt";
			this.dtp_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt.ShowUpDown = true;
			this.dtp_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt.Size = new System.Drawing.Size(72, 20);
			this.dtp_Einsatz_Sicherungstrupp_Einsatzziel_Doppelpunkt.TabIndex = 2;
			// 
			// cbx_Einsatz_Sicherungstrupp_EinsatzZiel_DatumJetzt
			// 
			this.cbx_Einsatz_Sicherungstrupp_EinsatzZiel_DatumJetzt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbx_Einsatz_Sicherungstrupp_EinsatzZiel_DatumJetzt.Location = new System.Drawing.Point(80, 24);
			this.cbx_Einsatz_Sicherungstrupp_EinsatzZiel_DatumJetzt.Name = "cbx_Einsatz_Sicherungstrupp_EinsatzZiel_DatumJetzt";
			this.cbx_Einsatz_Sicherungstrupp_EinsatzZiel_DatumJetzt.Size = new System.Drawing.Size(14, 14);
			this.cbx_Einsatz_Sicherungstrupp_EinsatzZiel_DatumJetzt.TabIndex = 13;
			// 
			// cbx_Einsatz_Sicherungstrupp_Rueckzug_DatumJetzt
			// 
			this.cbx_Einsatz_Sicherungstrupp_Rueckzug_DatumJetzt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbx_Einsatz_Sicherungstrupp_Rueckzug_DatumJetzt.Location = new System.Drawing.Point(80, 72);
			this.cbx_Einsatz_Sicherungstrupp_Rueckzug_DatumJetzt.Name = "cbx_Einsatz_Sicherungstrupp_Rueckzug_DatumJetzt";
			this.cbx_Einsatz_Sicherungstrupp_Rueckzug_DatumJetzt.Size = new System.Drawing.Size(14, 14);
			this.cbx_Einsatz_Sicherungstrupp_Rueckzug_DatumJetzt.TabIndex = 14;
			// 
			// txt_Einsatz_Sicherungstrupp_Kommentar
			// 
			this.txt_Einsatz_Sicherungstrupp_Kommentar.Location = new System.Drawing.Point(48, 136);
			this.txt_Einsatz_Sicherungstrupp_Kommentar.Name = "txt_Einsatz_Sicherungstrupp_Kommentar";
			this.txt_Einsatz_Sicherungstrupp_Kommentar.Size = new System.Drawing.Size(440, 64);
			this.txt_Einsatz_Sicherungstrupp_Kommentar.TabIndex = 4;
			this.txt_Einsatz_Sicherungstrupp_Kommentar.Text = "";
			// 
			// gbx_allgemeine_Informationen
			// 
			this.gbx_allgemeine_Informationen.BackColor = System.Drawing.SystemColors.Window;
			this.gbx_allgemeine_Informationen.Controls.Add(this.btn_Zeige_AGT_Informationen);
			this.gbx_allgemeine_Informationen.Controls.Add(this.lbl_aktueller_Einsatz_Datum);
			this.gbx_allgemeine_Informationen.Controls.Add(this.lbl_aktueller_Einsatz_Einsatzschwerpunkt);
			this.gbx_allgemeine_Informationen.Controls.Add(this.lbl_aktueller_Einsatz_Einsatzschwerpunkt_DoppelPunkt);
			this.gbx_allgemeine_Informationen.Controls.Add(this.lbl_aktueller_Einsatz_Verantwortlicher);
			this.gbx_allgemeine_Informationen.Controls.Add(this.lbl_aktueller_Einsatz__Verantwortlicher_DoppelPunkt);
			this.gbx_allgemeine_Informationen.Controls.Add(this.dgrid_Einsatz_letzte_Informationen);
			this.gbx_allgemeine_Informationen.Location = new System.Drawing.Point(8, 12);
			this.gbx_allgemeine_Informationen.Name = "gbx_allgemeine_Informationen";
			this.gbx_allgemeine_Informationen.Size = new System.Drawing.Size(624, 160);
			this.gbx_allgemeine_Informationen.TabIndex = 2;
			this.gbx_allgemeine_Informationen.TabStop = false;
			this.gbx_allgemeine_Informationen.Text = "Informationen";
			// 
			// btn_Zeige_AGT_Informationen
			// 
			this.btn_Zeige_AGT_Informationen.Location = new System.Drawing.Point(595, 35);
			this.btn_Zeige_AGT_Informationen.Name = "btn_Zeige_AGT_Informationen";
			this.btn_Zeige_AGT_Informationen.Size = new System.Drawing.Size(24, 16);
			this.btn_Zeige_AGT_Informationen.TabIndex = 26;
			this.btn_Zeige_AGT_Informationen.Text = "all";
			this.btn_Zeige_AGT_Informationen.Click += new System.EventHandler(this.btn_Zeige_AGT_Informationen_Click);
			// 
			// lbl_aktueller_Einsatz_Datum
			// 
			this.lbl_aktueller_Einsatz_Datum.Location = new System.Drawing.Point(424, 16);
			this.lbl_aktueller_Einsatz_Datum.Name = "lbl_aktueller_Einsatz_Datum";
			this.lbl_aktueller_Einsatz_Datum.Size = new System.Drawing.Size(112, 16);
			this.lbl_aktueller_Einsatz_Datum.TabIndex = 25;
			this.lbl_aktueller_Einsatz_Datum.Text = "<Date>";
			// 
			// lbl_aktueller_Einsatz_Einsatzschwerpunkt
			// 
			this.lbl_aktueller_Einsatz_Einsatzschwerpunkt.Location = new System.Drawing.Point(256, 32);
			this.lbl_aktueller_Einsatz_Einsatzschwerpunkt.Name = "lbl_aktueller_Einsatz_Einsatzschwerpunkt";
			this.lbl_aktueller_Einsatz_Einsatzschwerpunkt.Size = new System.Drawing.Size(208, 16);
			this.lbl_aktueller_Einsatz_Einsatzschwerpunkt.TabIndex = 24;
			this.lbl_aktueller_Einsatz_Einsatzschwerpunkt.Text = "<ESP>";
			// 
			// lbl_aktueller_Einsatz_Einsatzschwerpunkt_DoppelPunkt
			// 
			this.lbl_aktueller_Einsatz_Einsatzschwerpunkt_DoppelPunkt.Location = new System.Drawing.Point(256, 16);
			this.lbl_aktueller_Einsatz_Einsatzschwerpunkt_DoppelPunkt.Name = "lbl_aktueller_Einsatz_Einsatzschwerpunkt_DoppelPunkt";
			this.lbl_aktueller_Einsatz_Einsatzschwerpunkt_DoppelPunkt.Size = new System.Drawing.Size(120, 16);
			this.lbl_aktueller_Einsatz_Einsatzschwerpunkt_DoppelPunkt.TabIndex = 23;
			this.lbl_aktueller_Einsatz_Einsatzschwerpunkt_DoppelPunkt.Text = "Einsatzschwerpunkt:";
			// 
			// lbl_aktueller_Einsatz_Verantwortlicher
			// 
			this.lbl_aktueller_Einsatz_Verantwortlicher.Location = new System.Drawing.Point(88, 32);
			this.lbl_aktueller_Einsatz_Verantwortlicher.Name = "lbl_aktueller_Einsatz_Verantwortlicher";
			this.lbl_aktueller_Einsatz_Verantwortlicher.Size = new System.Drawing.Size(136, 16);
			this.lbl_aktueller_Einsatz_Verantwortlicher.TabIndex = 22;
			this.lbl_aktueller_Einsatz_Verantwortlicher.Text = "<Verantwortlicher>";
			// 
			// lbl_aktueller_Einsatz__Verantwortlicher_DoppelPunkt
			// 
			this.lbl_aktueller_Einsatz__Verantwortlicher_DoppelPunkt.Location = new System.Drawing.Point(88, 16);
			this.lbl_aktueller_Einsatz__Verantwortlicher_DoppelPunkt.Name = "lbl_aktueller_Einsatz__Verantwortlicher_DoppelPunkt";
			this.lbl_aktueller_Einsatz__Verantwortlicher_DoppelPunkt.Size = new System.Drawing.Size(96, 16);
			this.lbl_aktueller_Einsatz__Verantwortlicher_DoppelPunkt.TabIndex = 21;
			this.lbl_aktueller_Einsatz__Verantwortlicher_DoppelPunkt.Text = "Verantwortlicher:";
			// 
			// dgrid_Einsatz_letzte_Informationen
			// 
			this.dgrid_Einsatz_letzte_Informationen.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgrid_Einsatz_letzte_Informationen.CaptionVisible = false;
			this.dgrid_Einsatz_letzte_Informationen.DataMember = "";
			this.dgrid_Einsatz_letzte_Informationen.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.dgrid_Einsatz_letzte_Informationen.FlatMode = true;
			this.dgrid_Einsatz_letzte_Informationen.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgrid_Einsatz_letzte_Informationen.Location = new System.Drawing.Point(3, 53);
			this.dgrid_Einsatz_letzte_Informationen.Name = "dgrid_Einsatz_letzte_Informationen";
			this.dgrid_Einsatz_letzte_Informationen.ReadOnly = true;
			this.dgrid_Einsatz_letzte_Informationen.RowHeadersVisible = false;
			this.dgrid_Einsatz_letzte_Informationen.Size = new System.Drawing.Size(618, 104);
			this.dgrid_Einsatz_letzte_Informationen.TabIndex = 0;
			this.dgrid_Einsatz_letzte_Informationen.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																														   this.dgts_AGT_Informationen});
			// 
			// dgts_AGT_Informationen
			// 
			this.dgts_AGT_Informationen.DataGrid = this.dgrid_Einsatz_letzte_Informationen;
			this.dgts_AGT_Informationen.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgts_AGT_Informationen.MappingName = "";
			this.dgts_AGT_Informationen.PreferredColumnWidth = 80;
			this.dgts_AGT_Informationen.PreferredRowHeight = 12;
			this.dgts_AGT_Informationen.ReadOnly = true;
			this.dgts_AGT_Informationen.RowHeaderWidth = 40;
			// 
			// btn_Einsatz_abschließen
			// 
			this.btn_Einsatz_abschließen.Enabled = false;
			this.btn_Einsatz_abschließen.Location = new System.Drawing.Point(512, 432);
			this.btn_Einsatz_abschließen.Name = "btn_Einsatz_abschließen";
			this.btn_Einsatz_abschließen.Size = new System.Drawing.Size(120, 24);
			this.btn_Einsatz_abschließen.TabIndex = 1;
			this.btn_Einsatz_abschließen.Text = "Einsatz abschließen";
			// 
			// btn_Einsatz_abbrechen
			// 
			this.btn_Einsatz_abbrechen.Location = new System.Drawing.Point(336, 432);
			this.btn_Einsatz_abbrechen.Name = "btn_Einsatz_abbrechen";
			this.btn_Einsatz_abbrechen.Size = new System.Drawing.Size(144, 24);
			this.btn_Einsatz_abbrechen.TabIndex = 0;
			this.btn_Einsatz_abbrechen.Text = "Einsatz abbrechen";
			this.btn_Einsatz_abbrechen.Click += new System.EventHandler(this.btn_Einsatz_abbrechen_Click);
			// 
			// lbl_AGT_Einsatz
			// 
			this.lbl_AGT_Einsatz.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbl_AGT_Einsatz.Location = new System.Drawing.Point(208, 8);
			this.lbl_AGT_Einsatz.Name = "lbl_AGT_Einsatz";
			this.lbl_AGT_Einsatz.Size = new System.Drawing.Size(136, 23);
			this.lbl_AGT_Einsatz.TabIndex = 1;
			this.lbl_AGT_Einsatz.Text = "AGT Einsatz";
			// 
			// usc_AGT
			// 
			this.Controls.Add(this.lbl_AGT_Einsatz);
			this.Controls.Add(this.tabctrl_AGT);
			this.Name = "usc_AGT";
			this.Size = new System.Drawing.Size(650, 530);
			this.tabctrl_AGT.ResumeLayout(false);
			this.tabpage_AGT_Einatz_erstellen.ResumeLayout(false);
			this.gbx_EinsatzTrupps.ResumeLayout(false);
			this.gbx_EinsatzTrupps_Trupp1.ResumeLayout(false);
			this.gbx_PA_Nr.ResumeLayout(false);
			this.gbx_EinsatzTrupps_Trupp2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.gbx_EinsatzTrupps_Sicherungstrupp.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.gbx_AGT_Einsatz_erstellen_Header.ResumeLayout(false);
			this.tabpage_AGTOperator_inaktiv.ResumeLayout(false);
			this.tabpage_AGT_aktueller_Einsatz.ResumeLayout(false);
			this.tabctrl_AGT_Einsatz.ResumeLayout(false);
			this.tabpage_AGT_Einsatz_Trupp1.ResumeLayout(false);
			this.gbx_Einsatz_Trupp1.ResumeLayout(false);
			this.gbx_Einsatz_Trupp1_Einsatzziel_Rückzug.ResumeLayout(false);
			this.tabpage_AGT_Einsatz_Trupp2.ResumeLayout(false);
			this.gbx_Einsatz_Trupp2.ResumeLayout(false);
			this.gbx_Einsatz_Trupp2_Einsatzziel_Rückzug.ResumeLayout(false);
			this.tabpage_AGT_Einsatz_Sicherungtrupp.ResumeLayout(false);
			this.gbx_Einsatz_Sicherungstrupp.ResumeLayout(false);
			this.gbx_Einsatz_Sicherungstrupp_Einsatzziel_Rückzug.ResumeLayout(false);
			this.gbx_allgemeine_Informationen.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Einsatz_letzte_Informationen)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Funktionalität

		private void Einsatzdaten_Laden()
		{
			TabelleAnlegen();
			
			//allgemeine Informationen für dein Einsatz dem Header der
			//Page "Einsatz anlegen" in "aktueller Einsatz laden"
			this.lbl_aktueller_Einsatz_Verantwortlicher.Text = this.txt_AGT_Einsatz_erstellen_Header_Verantwortlicher.Text;
			this.lbl_aktueller_Einsatz_Einsatzschwerpunkt.Text = this.cbo_AGT_Einsatz_erstellen_Header_Einsatzschwerpunkt.Text;
			this.lbl_aktueller_Einsatz_Datum.Text = this.dtp_AGT_Einsatz_Erstellen_Header_Datum.Text.ToString();

			//Truppspezifische Informationen für Trupp1, Trupp2
			// und Sicherungstrupp aus dem Eingabefeldern Lesen
			//Trupp1
			this.lbl_Einsatz_Trupp1_Truppfuehrer.Text = this.cbo_Einsatztrupp_Trupp1_Truppführer.Text;
			this.lbl_Einsatz_Trupp1_Funkrufname.Text = this.txt_Trupp1_Funkrufname.Text;
			//Trupp2
			this.lbl_Einsatz_Trupp2_Truppfuehrer.Text = this.cbo_Einsatztrupp_Trupp2_Truppführer.Text;
			this.lbl_Einsatz_Trupp2_Funkrufname.Text = this.txt_Trupp2_Funkrufname.Text;
			//Sicherungstrupp
			this.lbl_Einsatz_Sicherungstrupp_Truppfuehrer.Text = this.cbo_Einsatztrupp_Sicherungstrupp_Truppführer.Text;
			this.lbl_Einsatz_Sicherungstrupp_Funkrufname.Text = this.txt_Sicherungstrupp_Funkrufname.Text;

		}



	
		



		#endregion
	
		#region Setzen der Rollenrechte
		//TODO: impelementation ist noch unvollständig.
		//Test steht noch aus.
		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
		{
			if(pin_i_aktuelleRolle == (int) Rollensystem.Tenm_Rolle.AGTOperator)				
			{// Funktionalität aktivieren
			
				
				//Hinweis auf falsche Rolle raus schmeißen
				this.tabctrl_AGT.TabPages.Remove(this.tabpage_AGTOperator_inaktiv);								
				//über diesen Button können kann ein Einsatz angelegt werden
				this.btn_AGT_Einsatz_anlegen.Enabled = true;

			}
			else
			{//Funktionalität deaktivieren
								
				//enthält hinweis auf den AGT Modus
				this.tabctrl_AGT.TabPages.Clear();
				//Hinweis auf die falsche Rolle als erstes Tab festlegen
				this.tabctrl_AGT.TabPages.Add(this.tabpage_AGTOperator_inaktiv);
				this.tabctrl_AGT.TabPages.Add(this.tabpage_AGT_Einatz_erstellen);
				//über diesen Button können kann ein Einsatz angelegt werden
				this.btn_AGT_Einsatz_anlegen.Enabled = false; 
				
				
			}
		}

		#endregion

		#region Events
		private void btn_AGT_Einsatz_starten_Click(object sender, System.EventArgs e)
		{
			//TabPage vorsichtshalber raus nehmen
			this.tabctrl_AGT.TabPages.Remove(this.tabpage_AGT_aktueller_Einsatz);
			
			//TabPage zu TabControl hinzufügen
			this.tabctrl_AGT.TabPages.Add(this.tabpage_AGT_aktueller_Einsatz);
									
			//Button Einsatz starten deaktivieren
			this.btn_AGT_Einsatz_anlegen.Enabled = false;

			//Wenn Trupp2 auch mit in den Einsatz geht, dann Tapppage freischalten
			
			//Zuerstmal entfernen (weil standardmäßig drin)
			this.tabctrl_AGT_Einsatz.TabPages.Remove(this.tabpage_AGT_Einsatz_Trupp2);

			if (this.cbx_Trupp2.CheckState == CheckState.Checked)
			{
				//erstmal den sicherungstrupp rausnehmen
				this.tabctrl_AGT_Einsatz.TabPages.Remove(this.tabpage_AGT_Einsatz_Sicherungtrupp);	
				//Trupp2 einfügen
				this.tabctrl_AGT_Einsatz.TabPages.Add(this.tabpage_AGT_Einsatz_Trupp2);
				//sicherungstrupp wieder einfügen
				this.tabctrl_AGT_Einsatz.TabPages.Add(this.tabpage_AGT_Einsatz_Sicherungtrupp);				
			}
			else
				//Trupp2 raus nehmen
				this.tabctrl_AGT_Einsatz.TabPages.Remove(this.tabpage_AGT_Einsatz_Trupp2);

			//Einsatzdaten aus EingabeMaske Laden
			Einsatzdaten_Laden();
			

			
			
			//TODO:laufender einsatz als aktive TabPage wählen

		}

		private void btn_Einsatz_abbrechen_Click(object sender, System.EventArgs e)
		{
			//TabPage von Tabcontrol entfernen
			this.tabctrl_AGT.TabPages.Remove(this.tabpage_AGT_aktueller_Einsatz);
			//Button Einsatz starten aktivieren
			this.btn_AGT_Einsatz_anlegen.Enabled= true;
			
		}

		private void cbx_Trupp2_CheckStateChanged(object sender, System.EventArgs e)
		{
			//Mit dieser checkbox, kann man das Feld für den 2. Trupp aktivieren
			if (cbx_Trupp2.CheckState == CheckState.Checked)
			{
				gbx_EinsatzTrupps_Trupp2.Enabled = true;
			}
			else
				gbx_EinsatzTrupps_Trupp2.Enabled = false;
		}

		private void btn_Eintragen_Click(object sender, System.EventArgs e)
		{
			TabelleAnlegen();
		}

		private void mI_ZeitAktualisieren_Jetzt_Click(object sender, System.EventArgs e)
		{
			//Die Uhrzeit des aktuell unter dem Cursor 
			//befindlichen DateTimePicker wird aktualisiert
			this._dtp_unterCuror.Value = DateTime.Now;
		}
		private void dtp_Enter(object sender, System.EventArgs e)
		{
			//der sender wird ermittelt und somit das aktuell
			// unter dem Cursor liegende Datumsfeld
			this._dtp_unterCuror = (DateTimePicker) sender;
			//MessageBox.Show((sender.GetType()).ToString());
		
		}
		private void btn_Zeige_AGT_Informationen_Click(object sender, System.EventArgs e)
		{
			if( this._myAGT_Informationen == null)
				this._myAGT_Informationen = new AGT_Informationen();

			//Beschriftung der Form auf den Titel des Einsatzschwerpunktes setzen
			this._myAGT_Informationen.Beschriftung = this.lbl_aktueller_Einsatz_Einsatzschwerpunkt.Text;
			//Datenverknüfung
			this._myAGT_Informationen.Inhalt = (DataTable) this.dgrid_Einsatz_letzte_Informationen.DataSource;
			this._myAGT_Informationen.Show();			
		}

		

		#region Eventhandler der AGT Druck Einträge (in Tabelle)
		#region Trupp1
		private void ade_Trupp1_EnabledChanged(object sender, System.EventArgs e)
		{
			//Wird die HandlerMethode für alle EnabledChanged events der ade's
			
			if( ((AGT_Druck_Eintrag) sender).Enabled == false)
			{
				AGT_Druck_Eintrag ade_tmp = (AGT_Druck_Eintrag) sender;
				
				//neuen Tabelleneintrag erstellen
				object[] obj_Trupp1 = new object[] {ade_tmp.Zeit.ToShortTimeString(),
													   "Trupp1",
													   (string)this.lbl_Einsatz_Trupp1_Truppfuehrer.Text,
													   (string)this.lbl_Einsatz_Trupp1_Funkrufname.Text,
													   (string) ade_tmp.Druck,
													   (string) this.txt_Einsatz_Trupp1_Kommentar.Text,
				};
				//datatable auslesen					
				DataTable dtp_tmp = (DataTable)this.dgrid_Einsatz_letzte_Informationen.DataSource;
				//Datatable modifizieren
				dtp_tmp.Rows.Add(obj_Trupp1);
				//datatable wieder reinschreiben
				this.dgrid_Einsatz_letzte_Informationen.DataSource = dtp_tmp;
				//debug
				//this.dgrid_Einsatz_letzte_Informationen.Refresh();
				
			}
		}
			#endregion
		#region Trupp2
		private void ade_Trupp2_EnabledChanged(object sender, System.EventArgs e)
		{
			//Wird die HandlerMethode für alle EnabledChanged events der ade's
			
			if( ((AGT_Druck_Eintrag) sender).Enabled == false)
			{
				AGT_Druck_Eintrag ade_tmp = (AGT_Druck_Eintrag) sender;
				
				//neuen Tabelleneintrag erstellen
				object[] obj_Trupp2 = new object[] {ade_tmp.Zeit.ToShortTimeString(),
													   "Trupp2",
													   (string)this.lbl_Einsatz_Trupp2_Truppfuehrer.Text,
													   (string)this.lbl_Einsatz_Trupp2_Funkrufname.Text,
													   (string) ade_tmp.Druck,
													   (string) this.txt_Einsatz_Trupp2_Kommentar.Text,
				};
				//datatable auslesen					
				DataTable dtp_tmp = (DataTable)this.dgrid_Einsatz_letzte_Informationen.DataSource;
				//Datatable modifizieren
				dtp_tmp.Rows.Add(obj_Trupp2);
				//datatable wieder reinschreiben
				this.dgrid_Einsatz_letzte_Informationen.DataSource = dtp_tmp;
				//debug
				//this.dgrid_Einsatz_letzte_Informationen.Refresh();
				
			}
		}
		#endregion
		#region Sicherungstrupp
		private void ade_Sicherungstrupp_EnabledChanged(object sender, System.EventArgs e)
		{
			//Wird die HandlerMethode für alle EnabledChanged events der ade's
			
			if( ((AGT_Druck_Eintrag) sender).Enabled == false)
			{
				AGT_Druck_Eintrag ade_tmp = (AGT_Druck_Eintrag) sender;
				
				//neuen Tabelleneintrag erstellen
				object[] obj_Sicherungstrupp = new object[] {ade_tmp.Zeit.ToShortTimeString(),
													   "Sicherungstrupp",
													   (string)this.lbl_Einsatz_Sicherungstrupp_Truppfuehrer.Text,
													   (string)this.lbl_Einsatz_Sicherungstrupp_Funkrufname.Text,
													   (string) ade_tmp.Druck,
													   (string) this.txt_Einsatz_Sicherungstrupp_Kommentar.Text,
				};
				//datatable auslesen					
				DataTable dtp_tmp = (DataTable)this.dgrid_Einsatz_letzte_Informationen.DataSource;
				//Datatable modifizieren
				dtp_tmp.Rows.Add(obj_Sicherungstrupp);
				//datatable wieder reinschreiben
				this.dgrid_Einsatz_letzte_Informationen.DataSource = dtp_tmp;
				//debug
				//this.dgrid_Einsatz_letzte_Informationen.Refresh();
				
			}
		}
		#endregion
		#endregion


		#endregion
		
		# region DataGrid
					
		#region Übernommener Code aus usc_MAT
		private DataTable ErstellenEinerDataTable(string pin_str_Name, DataColumn[] pin_dcol_a_Spalten) 
		{			
			DataTable pout_dtable_meineTabelle = new DataTable(pin_str_Name);
			pout_dtable_meineTabelle.Columns.AddRange(pin_dcol_a_Spalten);				
			return pout_dtable_meineTabelle;
		}

		private DataColumn ErstellenEinerDataColumn(string pin_str_Name, string pin_str_Caption, string pin_str_Type) 
		{
			// Type der Spalte generieren
			System.Type type_meinType = Type.GetType(pin_str_Type);						
			if (type_meinType == null) return null;
			// Neue Spalte erstellen 
			DataColumn pout_dcol_Spalte = new DataColumn(pin_str_Name, type_meinType);
			pout_dcol_Spalte.ReadOnly = true; 
			pout_dcol_Spalte.ColumnName = pin_str_Caption;			
			
			
			return pout_dcol_Spalte;
		}

				
		#endregion

		//Tabelle Modellieren
		private void TabelleAnlegen()
		{	
			// Spalten generieren
			DataColumn[] dcol_agt_Spalten = 
			{				
				ErstellenEinerDataColumn("dcol_agt_Einsatz_Zeitpunkt", "Zeitpunkt", "System.String"),
				ErstellenEinerDataColumn("dcol_agt_Einsatz_Trupp", "Trupp", "System.String"),			
				ErstellenEinerDataColumn("dcol_agt_Einsatz_Truppfuehrer", "Truppführer", "System.String"),			
				ErstellenEinerDataColumn("dcol_agt_Einsatz_Funkrufname", "Funkrufname", "System.String"),			
				ErstellenEinerDataColumn("dcol_agt_Einsatz_Druck", "Druckwerte", "System.String"),			
				ErstellenEinerDataColumn("dcol_agt_Einsatz_Kommentar", "Kommentar", "System.String"),	
			};

			// Tabelle mit den Spalten generieren
			DataTable dtable_agt_Einsatz = ErstellenEinerDataTable("dtable_agt_Einsazt", dcol_agt_Spalten);
			
			// Laden der daten für die Tabelle aus den Inhalten der Tabpage "Einsatz erstellen"			
			object[] obj_FuerZeile1 = new object[] {DateTime.Now.ToShortTimeString(),"","","","","-> Einsatz erstellt"};
			
			object[] obj_Trupp1 = new object[] {DateTime.Now.ToShortTimeString(),
												   "Trupp1",
												   this.cbo_Einsatztrupp_Trupp1_Truppführer.Text,
												   this.txt_Trupp1_Funkrufname.Text,
												   "",
												   this.cbo_Einsatztrupp_Trupp1_Truppführer.Text
												   +", "+this.cbo_Einsatztrupp_Trupp1_Truppmann1.Text
												   +", "+this.cbo_Einsatztrupp_Trupp1_Truppmann2.Text
												   +", "+this.cbo_Einsatztrupp_Trupp1_Truppmann3.Text
											   };

			object[] obj_Trupp2= new object[] {DateTime.Now.ToShortTimeString(),
												  "Trupp2",
												  this.cbo_Einsatztrupp_Trupp2_Truppführer.Text,
												  this.txt_Trupp2_Funkrufname.Text,
												  "",
												  this.cbo_Einsatztrupp_Trupp2_Truppführer.Text
												  +", "+this.cbo_Einsatztrupp_Trupp2_Truppmann1.Text
												  +", "+this.cbo_Einsatztrupp_Trupp2_Truppmann2.Text
												  +", "+this.cbo_Einsatztrupp_Trupp2_Truppmann3.Text
											  };
			
			object[] obj_Trupp3= new object[] {DateTime.Now.ToShortTimeString(),
												  "Sicherungstrupp",
												  this.cbo_Einsatztrupp_Sicherungstrupp_Truppführer.Text,
												  this.txt_Sicherungstrupp_Funkrufname.Text,
												  "",
												  this.cbo_Einsatztrupp_Sicherungstrupp_Truppführer.Text
												  +", "+this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann1.Text
												  +", "+this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann2.Text
												  +", "+this.cbo_Einsatztrupp_Sicherungstrupp_Truppmann3.Text
											  };
			

			
			// Zeilen zur Tabelle hinzufügen
			dtable_agt_Einsatz.Rows.Add(obj_FuerZeile1);
			dtable_agt_Einsatz.Rows.Add(obj_Trupp1);
			if(this.cbx_Trupp2.CheckState == CheckState.Checked)
				dtable_agt_Einsatz.Rows.Add(obj_Trupp2);
			dtable_agt_Einsatz.Rows.Add(obj_Trupp3);
			
			// Tabelle dem Datagrid zuordnen
			dgrid_Einsatz_letzte_Informationen.DataSource = dtable_agt_Einsatz;		
		}

		#endregion




		







	
	}
}
