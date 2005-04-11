using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pELS.Client.MAT
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
	public class usc_AuftraegeNachverfolgen: System.Windows.Forms.UserControl
	{
		#region Variablen
		/// </summary>
		/// gibt an, ob bereits Eingaben geschehen sind
		/// </summary>
		protected bool _b_FelderModifiziert = false;
		public bool b_FelderModifiziert
		{
			get { return _b_FelderModifiziert;}
			set { _b_FelderModifiziert = value;}
		}

		/// <summary>
		/// Referenz auf das entsprechende Element der Steuerungsschicht
		/// </summary>
		private Cst_MAT _stMAT;

		#endregion

		#region graphische Variablen
		private System.Windows.Forms.GroupBox grp_Nachverfolgung;
		private System.Windows.Forms.Label lbl_Nachverfolgung_Anleitung;
		private System.Windows.Forms.DataGrid dtg_AuftraegeNachverfolgen;
		private System.Windows.Forms.Button btn_Nachverfolgen_Speichern;
		private System.Windows.Forms.DateTimePicker dtp_Nachverfolgung_Ausfuehrungszeitpunkt;
		private System.Windows.Forms.Label lbl_Nachverfolgung_Ausfuehrungszeitpunkt;
		public System.Windows.Forms.CheckBox cbx_AusfuehrungsdatumJetzt;

		public System.ComponentModel.Container components = null;
		#endregion

		#region Konstruktor & Destruktor
		public usc_AuftraegeNachverfolgen(Cst_MAT pin_stMAT)
		{
			this._stMAT = pin_stMAT;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			this.LadeNachzuverfolgendeAuftraege();	
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
			this.grp_Nachverfolgung = new System.Windows.Forms.GroupBox();
			this.lbl_Nachverfolgung_Anleitung = new System.Windows.Forms.Label();
			this.dtg_AuftraegeNachverfolgen = new System.Windows.Forms.DataGrid();
			this.btn_Nachverfolgen_Speichern = new System.Windows.Forms.Button();
			this.dtp_Nachverfolgung_Ausfuehrungszeitpunkt = new System.Windows.Forms.DateTimePicker();
			this.lbl_Nachverfolgung_Ausfuehrungszeitpunkt = new System.Windows.Forms.Label();
			this.cbx_AusfuehrungsdatumJetzt = new System.Windows.Forms.CheckBox();
			this.grp_Nachverfolgung.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtg_AuftraegeNachverfolgen)).BeginInit();
			this.SuspendLayout();
			// 
			// grp_Nachverfolgung
			// 
			this.grp_Nachverfolgung.BackColor = System.Drawing.SystemColors.Window;
			this.grp_Nachverfolgung.Controls.Add(this.lbl_Nachverfolgung_Anleitung);
			this.grp_Nachverfolgung.Location = new System.Drawing.Point(5, 5);
			this.grp_Nachverfolgung.Name = "grp_Nachverfolgung";
			this.grp_Nachverfolgung.Size = new System.Drawing.Size(615, 56);
			this.grp_Nachverfolgung.TabIndex = 14;
			this.grp_Nachverfolgung.TabStop = false;
			this.grp_Nachverfolgung.Text = "Nachverfolgung von Aufträgen";
			// 
			// lbl_Nachverfolgung_Anleitung
			// 
			this.lbl_Nachverfolgung_Anleitung.Location = new System.Drawing.Point(8, 25);
			this.lbl_Nachverfolgung_Anleitung.Name = "lbl_Nachverfolgung_Anleitung";
			this.lbl_Nachverfolgung_Anleitung.Size = new System.Drawing.Size(512, 15);
			this.lbl_Nachverfolgung_Anleitung.TabIndex = 15;
			this.lbl_Nachverfolgung_Anleitung.Text = "Wählen Sie einen Auftrag aus, geben Sie den Ausführungszeitpunkt an und klicken S" +
				"ie auf Speichern.";
			// 
			// dtg_AuftraegeNachverfolgen
			// 
			this.dtg_AuftraegeNachverfolgen.DataMember = "";
			this.dtg_AuftraegeNachverfolgen.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtg_AuftraegeNachverfolgen.Location = new System.Drawing.Point(5, 55);
			this.dtg_AuftraegeNachverfolgen.Name = "dtg_AuftraegeNachverfolgen";
			this.dtg_AuftraegeNachverfolgen.PreferredColumnWidth = 120;
			this.dtg_AuftraegeNachverfolgen.Size = new System.Drawing.Size(614, 344);
			this.dtg_AuftraegeNachverfolgen.TabIndex = 0;
			// 
			// btn_Nachverfolgen_Speichern
			// 
			this.btn_Nachverfolgen_Speichern.Location = new System.Drawing.Point(525, 430);
			this.btn_Nachverfolgen_Speichern.Name = "btn_Nachverfolgen_Speichern";
			this.btn_Nachverfolgen_Speichern.Size = new System.Drawing.Size(80, 23);
			this.btn_Nachverfolgen_Speichern.TabIndex = 3;
			this.btn_Nachverfolgen_Speichern.Text = "Speichern";
			this.btn_Nachverfolgen_Speichern.Click += new System.EventHandler(this.btn_Nachverfolgen_Speichern_Click);
			// 
			// dtp_Nachverfolgung_Ausfuehrungszeitpunkt
			// 
			this.dtp_Nachverfolgung_Ausfuehrungszeitpunkt.CustomFormat = "dd.MM.yyyy - HH:mm";
			this.dtp_Nachverfolgung_Ausfuehrungszeitpunkt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp_Nachverfolgung_Ausfuehrungszeitpunkt.Location = new System.Drawing.Point(490, 400);
			this.dtp_Nachverfolgung_Ausfuehrungszeitpunkt.MinDate = new System.DateTime(2004, 11, 2, 0, 0, 0, 0);
			this.dtp_Nachverfolgung_Ausfuehrungszeitpunkt.Name = "dtp_Nachverfolgung_Ausfuehrungszeitpunkt";
			this.dtp_Nachverfolgung_Ausfuehrungszeitpunkt.Size = new System.Drawing.Size(120, 20);
			this.dtp_Nachverfolgung_Ausfuehrungszeitpunkt.TabIndex = 2;
			// 
			// lbl_Nachverfolgung_Ausfuehrungszeitpunkt
			// 
			this.lbl_Nachverfolgung_Ausfuehrungszeitpunkt.Location = new System.Drawing.Point(305, 405);
			this.lbl_Nachverfolgung_Ausfuehrungszeitpunkt.Name = "lbl_Nachverfolgung_Ausfuehrungszeitpunkt";
			this.lbl_Nachverfolgung_Ausfuehrungszeitpunkt.Size = new System.Drawing.Size(120, 15);
			this.lbl_Nachverfolgung_Ausfuehrungszeitpunkt.TabIndex = 31;
			this.lbl_Nachverfolgung_Ausfuehrungszeitpunkt.Text = "Ausführungszeitpunkt:";
			// 
			// cbx_AusfuehrungsdatumJetzt
			// 
			this.cbx_AusfuehrungsdatumJetzt.Location = new System.Drawing.Point(435, 405);
			this.cbx_AusfuehrungsdatumJetzt.Name = "cbx_AusfuehrungsdatumJetzt";
			this.cbx_AusfuehrungsdatumJetzt.Size = new System.Drawing.Size(44, 16);
			this.cbx_AusfuehrungsdatumJetzt.TabIndex = 1;
			this.cbx_AusfuehrungsdatumJetzt.Text = "jetzt";
			this.cbx_AusfuehrungsdatumJetzt.CheckedChanged += new System.EventHandler(this.cbx_AusfuehrungsdatumJetzt_CheckedChanged);
			// 
			// usc_AuftraegeNachverfolgen
			// 
			this.Controls.Add(this.cbx_AusfuehrungsdatumJetzt);
			this.Controls.Add(this.btn_Nachverfolgen_Speichern);
			this.Controls.Add(this.dtp_Nachverfolgung_Ausfuehrungszeitpunkt);
			this.Controls.Add(this.lbl_Nachverfolgung_Ausfuehrungszeitpunkt);
			this.Controls.Add(this.dtg_AuftraegeNachverfolgen);
			this.Controls.Add(this.grp_Nachverfolgung);
			this.Location = new System.Drawing.Point(6, 21);
			this.Name = "usc_AuftraegeNachverfolgen";
			this.Size = new System.Drawing.Size(624, 456);
			this.grp_Nachverfolgung.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtg_AuftraegeNachverfolgen)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		#endregion

		#region Lade-, Zuruecksetzen Methode
		private void SetzeEvents()
		{
			this.cbx_AusfuehrungsdatumJetzt.CheckedChanged += new EventHandler(this.FelderModifiziert);
		}
	
		/// </summary>
		/// Lade alle Auftraege, die nachzuverfolgen sind, und mit diesen Daten füllt
		/// die Tabelle aus.
		/// </summary>
		public void LadeNachzuverfolgendeAuftraege()
		{
//			Cdv_Auftrag[] auftragsmenge = this._stMAT.NachzuverfolgendeAuftraege;	
//			Cdv_Erkundungsbefehl[] befehlsmenge = this._stMAT.NachzuverfolgendeBefehle;
//			if((auftragsmenge.Length + befehlsmenge.Length) != 0)
//				this.SetzeDatagridAuftraegeNachverfolgen(auftragsmenge,befehlsmenge);
//			else
//				lbl_Nachverfolgung_Anleitung.Text = "Zur Zeit ist keinen Auftrag nachzuverfolgen.";
			Cdv_Auftrag[] auftragsmenge = this._stMAT.NachzuverfolgendeAuftraege;	
			if(auftragsmenge.Length != 0)
				this.SetzeDatagridAuftraegeNachverfolgen(auftragsmenge);
			else
				lbl_Nachverfolgung_Anleitung.Text = "Zur Zeit ist keinen Auftrag nachzuverfolgen.";
		}
		
		public DataTable ErstelleDatagridAuftraegeNachverfolgen()
		{
			// Spalten generieren
			DataColumn[] dcol_a_AuftraegeNachverfolgen = 
			{				
				
				ErstellenEinerDataColumn("dcol_Termine_laufendeNummer", "laufendeNummer", "System.String"),
				ErstellenEinerDataColumn("dcol_Termine_Absender", "Absender", "System.String"),
				ErstellenEinerDataColumn("dcol_Termine_Empfaenger", "Empfänger", "System.String"),
				ErstellenEinerDataColumn("dcol_Termine_Auftragstext", "Auftragstext", "System.String"),
				ErstellenEinerDataColumn("dcol_Termine_Abfassungsdatum", "Abfassungsdatum", "System.String"),
				ErstellenEinerDataColumn("dcol_Termine_IstBefehl", "Ist Befehl", "System.Boolean"),
				ErstellenEinerDataColumn("dcol_Termine_Befehlsart", "Befehlsart", "System.String"),
				ErstellenEinerDataColumn("dcol_Termine_IstUebermittelt", "Ist Übermittelt", "System.Boolean"),				
				ErstellenEinerDataColumn("dcol_Termine_Uebermittlungsdatum", "Uebermittlungsdatum", "System.String"),				
				ErstellenEinerDataColumn("dcol_Termine_BearbeiterBenutzer", "Bearbeiter Benutzer", "System.String"),
				ErstellenEinerDataColumn("dcol_Termine_Uebermittlungsart", "Übermittlungsart", "System.String"),
				// Wenn der Name "ID" verändert werden soll, muss in der Methode "HoleAusgewaehlteAuftragsID"
				ErstellenEinerDataColumn("dcol_Termine_ID", "ID", "System.String")
				
			};

			// nach unserem Prinzip "pro Nachricht mehr Daten" werden
			// die IDs zusammengepackt und nach unteren Schichten 
			// versendet. Dort werden die Objekte mit diesen IDs 
			// ausgesucht, und zurückgesendet.
			int[] i_EmpfaengerMengeKraftIDsMenge;
	
			// TODO: Implementierungsentscheidung noch zu treffen
			// 1. in Datagrid wurden statt Bezeichungen von Kraft vorläufig Ids ausgegeben, 
			//    weil es noch optimale ideen fehlen, die folgende Probleme beseitigen können:
			//    a) Implementationsaufwand
			//    b) Entwurfsentscheidung: Kraft hat keine Oberbezeichnung
			//    c) Speicherplatzaufwand: 
			//    d) Nachrichtensaustauschsaufwand
			DataTable dtable_AuftraegeNachverfolgen = ErstellenEinerDataTable("dtable_AuftraegeNachverfolgen", dcol_a_AuftraegeNachverfolgen);
			return dtable_AuftraegeNachverfolgen;
		}

		private void SetzeDatagridAuftraegeNachverfolgen(Cdv_Auftrag[] pin_auftragsmenge)
		{	
			DataTable dtable_AuftraegeNachverfolgen = this.ErstelleDatagridAuftraegeNachverfolgen();
			#region auftragsmenge
			foreach(Cdv_Auftrag a in pin_auftragsmenge)
			{
				// ACHTUNG (von Schuppe): es gibt keine ESP mehr für Aufträge
				//				Cdv_Einsatzschwerpunkt esp = this._stMAT.HoleESP(a.EinsatzschwerpunktID);
				Cdv_Benutzer bearbeiterBenutzer = this._stMAT.HoleBenutzer(a.BearbeiterBenutzerID);
				
				#region Hole Empfänger
				string str_EmpfaengerMengeKraft = "";
				string tmp_Name = "";
				string tmp_Typ = "";
				Cdv_Einheit einheit;
				Cdv_KFZ kfz;
				Cdv_Helfer helfer;
				foreach(int empfaengerID in a.EmpfaengerMengeKraftID)
				{
					einheit = null;
					kfz = null;
					helfer = null;

					einheit = this._stMAT.HoleEinheit(empfaengerID);
					if(einheit != null)
					{
						tmp_Name = einheit.Name;
						tmp_Typ = "Einheit:";
					}
					else
					{
						kfz = this._stMAT.HoleKFZ(empfaengerID);
						if(kfz != null)
						{
							tmp_Name = kfz.Kennzeichen;
							tmp_Typ = "KFZ:";
						}
						else
						{
							helfer = this._stMAT.HoleHelfer(empfaengerID);
							if(helfer != null)
							{
								tmp_Name = helfer.Personendaten.Vorname  + " " + helfer.Personendaten.Name;
								tmp_Typ = "Helfer:";
							}
						}
					}
					if(tmp_Name.CompareTo("") != 0)
					{
						 str_EmpfaengerMengeKraft = str_EmpfaengerMengeKraft + tmp_Typ + "(" + tmp_Name + ")" + "#";				
					}
				}
				#endregion

				// Befehlsart
				string str_Befehlsart = "";
				if(a.IstBefehl) str_Befehlsart = (a as Cdv_Erkundungsbefehl).BefehlsArt.ToString();
				

				// Uebermittlungsdatum
				string str_uebermittlungsdatum = "";
				if(a.IstUebermittelt == true)  str_uebermittlungsdatum = a.Uebermittlungsdatum.ToString();
																		
				object[] obj_tabellezeile = new object[] {  
															 // Laufende Nummer
															 a.LaufendeNummer.ToString(),
															 //Absender
															 a.Absender,
															 // Empfaenger
															 str_EmpfaengerMengeKraft,
															 // Auftragstext
															 a.Text,
															 // Abfassungsdatum
															 a.Abfassungsdatum.ToString(),
															 // ist Befehl
															 a.IstBefehl,
															 // auftrag hat keine Befehlsart
															 str_Befehlsart,
															 // ist uebermittelt
															 a.IstUebermittelt,
															 // Uebermittlungsdatum
															 str_uebermittlungsdatum,
															 // Bearbeiter Benutzer
															 bearbeiterBenutzer.Benutzername,
															 // Uebermittlungsart
															 a.Uebermittlungsart.ToString(),
															// AuftragsID
															a.ID.ToString()
														 };
				
				
				dtable_AuftraegeNachverfolgen.Rows.Add(obj_tabellezeile);

			}
			#endregion

	// Tabelle dem Datagrid zuordnen
			dtg_AuftraegeNachverfolgen.DataSource = dtable_AuftraegeNachverfolgen;	
		}

		private void Zuruecksetzen()
		{
			if(this._b_FelderModifiziert == true)
				this.dtp_Nachverfolgung_Ausfuehrungszeitpunkt.Value = DateTime.Now;
		}
		#region Datagrid Methoden
		// Spalten für eine Tabelle erstellen
		private DataColumn ErstellenEinerDataColumn(string pin_str_Name, string pin_str_Caption, string pin_str_Type) 
		{
			// Type der Spalte generieren
			System.Type type_meinType = Type.GetType(pin_str_Type);						
			if (type_meinType == null)
			{
				//TODO: Löschen MessageBox
				MessageBox.Show("type = null");
				return null;
			}
			// Neue Spalte erstellen 
			DataColumn pout_dcol_Spalte = new DataColumn(pin_str_Name, type_meinType);
			pout_dcol_Spalte.ReadOnly = true; 
			pout_dcol_Spalte.ColumnName = pin_str_Caption;			
			
			return pout_dcol_Spalte;
		}

		// Tabelle für ein DataGrid erstellen
		private DataTable ErstellenEinerDataTable(string pin_str_Name, DataColumn[] pin_dcol_a_Spalten) 
		{			
			DataTable pout_dtable_meineTabelle = new DataTable(pin_str_Name);

			pout_dtable_meineTabelle.Columns.AddRange(pin_dcol_a_Spalten);		
			return pout_dtable_meineTabelle;
		}


		#endregion
		#endregion 


		#region Event Handler

		private void btn_AuftraegeNachverfolgen_Aktualisieren_Click(object sender, System.EventArgs e)
		{
			this.LadeNachzuverfolgendeAuftraege();	
		
		}

		private void btn_Nachverfolgen_Speichern_Click(object sender, System.EventArgs e)
		{

			CurrencyManager xCM = (CurrencyManager)this.dtg_AuftraegeNachverfolgen.BindingContext
				[this.dtg_AuftraegeNachverfolgen.DataSource, this.dtg_AuftraegeNachverfolgen.DataMember];
			// Hole die aktuelle Zeile
			DataRow aktZeile = ((DataRowView)xCM.Current).Row;
	
			// Hole die ID des ausgewählten Auftrags
			DataTable dtable_AuftraegeNachverfolgen = this.dtg_AuftraegeNachverfolgen.DataSource as DataTable;
			// Voraussetzung: Column von IDS muss "ID" heißen
			// Index der Spalte, in der sich alle IDs befindet
			int i_IndexOfID = dtable_AuftraegeNachverfolgen.Columns.IndexOf("ID");
			// ID des ausgewaehlten Auftrags in String
			string tmp_ID = aktZeile.ItemArray[i_IndexOfID].ToString();
			// ID in Integer
			int i_AuftragsID = int.Parse(tmp_ID);

			// Lösche die Zeile aus dem Datagrid
			(this.dtg_AuftraegeNachverfolgen.DataSource as DataTable).Rows.Remove(aktZeile);
			
			// Überschreibe den Auftrag 
			Cdv_Auftrag auftrag = this._stMAT.HoleAuftrag(i_AuftragsID);
			
			auftrag.WirdNachverfolgt = false;
			auftrag.Ausfuehrungszeitpunkt = this.dtp_Nachverfolgung_Ausfuehrungszeitpunkt.Value;
			this._stMAT.SpeichereAuftrag(auftrag);
			this.Zuruecksetzen();
	}

		private void cbx_AusfuehrungsdatumJetzt_CheckedChanged(object sender, System.EventArgs e)
		{
			if (cbx_AusfuehrungsdatumJetzt.Checked==true)
			{
				//auf aktuelle Zeit zurück setzen
				dtp_Nachverfolgung_Ausfuehrungszeitpunkt.Value = DateTime.Now;
				// DateTimePicker ausgrauen
				dtp_Nachverfolgung_Ausfuehrungszeitpunkt.Enabled = false;
			}
			else
			{
				//DateTimePicker wieder aktivieren
				dtp_Nachverfolgung_Ausfuehrungszeitpunkt.Enabled = true;
			}
		}
		/// <summary>
		/// event, welches bei allen Eingabeelementen registriert ist
		/// und eine vorgenommene Änderung registriert
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FelderModifiziert(object sender, System.EventArgs e)
		{
			_b_FelderModifiziert = true;
		}


		#endregion

		#region dynamische Daten-Akualisierung
		/// <summary>
		/// Wenn die Menge aller Aufträge verändert wird, soll diese
		/// Funktion aufgerufen werden, damit die Gui akualisiert wird
		/// </summary>
		public void AktualisiereAuftrag()
		{
			this.LadeNachzuverfolgendeAuftraege();
		}
		
		#endregion

	}
}