using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

// Benötigt für: Person Daten
using pELS.DV;

namespace pELS.Client.EK
{
	/// <summary>
	/// Summary description for usc_dtg_Personen.
	/// </summary>
	public class usc_dtg_Personen : System.Windows.Forms.UserControl
	{
		#region grafische Variablen
		private System.Windows.Forms.DataGrid dtg_Personen;
		protected System.Windows.Forms.ErrorProvider ep_Eingabe = new System.Windows.Forms.ErrorProvider();
		private Cdv_Helfer[] _Helfermenge;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion
		#region Konstruktor
		public usc_dtg_Personen()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			this.ep_Eingabe.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			// TODO: Add any initialization after the InitializeComponent call
			//			this.FuellePersonenEinheiten();

		}

		/// <summary>
		/// Summary description for usc_dtg_Personen.
		/// Die Breite und Hoehe dieses Datagrid Personen können eingestellt werden
		/// <param name="pin_Bereite">Die Breite des Datagrid</param>
		/// <param name="pin_Hoehe">Die Hoehe des Datagrid</param>
		/// </summary>
		public usc_dtg_Personen(int pin_Breite, int pin_Hoehe)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			//			this.FuellePersonenEinheiten();
			this.Size = new System.Drawing.Size(pin_Breite,pin_Hoehe);
			this.dtg_Personen.Size = new System.Drawing.Size(pin_Breite -15,pin_Hoehe -5);
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
			this.dtg_Personen = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dtg_Personen)).BeginInit();
			this.SuspendLayout();
			// 
			// dtg_Personen
			// 
			this.dtg_Personen.DataMember = "";
			this.dtg_Personen.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtg_Personen.Location = new System.Drawing.Point(0, 0);
			this.dtg_Personen.Name = "dtg_Personen";
			this.dtg_Personen.Size = new System.Drawing.Size(608, 255);
			this.dtg_Personen.TabIndex = 1;
			// 
			// usc_dtg_Personen
			// 
			this.Controls.Add(this.dtg_Personen);
			this.Name = "usc_dtg_Personen";
			this.Size = new System.Drawing.Size(608, 264);
			((System.ComponentModel.ISupportInitialize)(this.dtg_Personen)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#endregion

		#region Funktionalität

		public void SetzeEreignissMouseUp(MouseEventHandler pin_handler)
		{
			this.dtg_Personen.MouseUp += pin_handler;
		}

		private DataTable ErstelleTabelleFuerPersonen()
		{
			DataColumn[] dcol_a_Person = 
			{								
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_ID", "ID", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Name", "Name", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Vorname", "Vorname", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_GebDat", "Geburtsdatum", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Strasse", "Str./Nr.", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_PLZOrt", "PLZ/Ort", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Erreichbarkeit", "Erreichbarkeit", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Faehigkeiten", "Faehigkeiten", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Helferstatus", "Helferstatus", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_KommentarAutor", "Kommentarautor", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_KommentarText", "Kommentartext", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_LetzteVerpflegung", "Letzte Verpflegung", "System.String"),
				Cpr_EK_AllgFkt.ErstellenEinerDataColumn("dcol_Position", "Position", "System.String")
			};			
			DataTable dtable_Person = Cpr_EK_AllgFkt.ErstellenEinerDataTable("dtable_Personen", dcol_a_Person);
		//	this.dtg_Personen.DataSource = dtable_Person;
			return dtable_Person;

		}

		public void SetzteFehlerMeldung(string pin_fehler)
		{
			this.ep_Eingabe.SetError(this.dtg_Personen, pin_fehler);
		}

		public void LadePersonen(Cdv_Helfer[] pin_Helfer)
		{
			//TODO: Fuelle Datagrid
			DataTable dtable_Person = ErstelleTabelleFuerPersonen();


			foreach(Cdv_Helfer helfer in pin_Helfer)
			{
				
		
															// ID			
				object[] obj_tabellezeile = new object[] {  helfer.ID.ToString(),
															 // Name
															 helfer.Personendaten.Name,
															 // Vorname
															 helfer.Personendaten.Vorname,
															 // GebDatum
															 helfer.Personendaten.GebDatum.ToString(),
															 // Strasse + Nummer
															 helfer.Personendaten.Anschrift.Strasse + " " + helfer.Personendaten.Anschrift.Hausnummer,
															 // PLZ/ORT
															 helfer.Personendaten.Anschrift.PLZ + " " + helfer.Personendaten.Anschrift.Ort,
															 // Erreichbarkeit
															 helfer.Erreichbarkeit,
															 // Faehigkeit
															 helfer.Faehigkeiten,
															 // Helferstatus
															 helfer.Helferstatus.ToString(),
															 //Kommentarautor
															 helfer.Kommentar.Autor,
															 // Kommentartext
															 helfer.Kommentar.Text,
															 // LetzteVerpflegung
															 helfer.LetzteVerfplegung.ToString(),
															 // Position
															 helfer.Position.ToString()
														 };
				
				
				dtable_Person.Rows.Add(obj_tabellezeile);
			}
			// Tabelle dem Datagrid zuordnen
			this.dtg_Personen.DataSource = dtable_Person;	
			this._Helfermenge = pin_Helfer;
			
		}

		// Liefert den Helfer zurück, der aktuell ausgewählt wird.
		public Cdv_Helfer HoleAusgewaehltePerson()
		{
			int i_ID = HoleAusgewaehltePersonID();
			foreach(Cdv_Helfer helfer in this._Helfermenge)
			{
				if(helfer.ID == i_ID)	
					return helfer;
			}
			return null;
		}


		// Liefert die ID des aktuell aus Datagrid ausgewaehlten Auftrags
		private int HoleAusgewaehltePersonID()
		{
			// Voraussetzung: Column von IDS muss "ID" heißen
			DataTable dtable_Person = this.dtg_Personen.DataSource as DataTable;
			// Index der Spalte, in der sich alle IDs befindet
			int i_IndexOfID = dtable_Person.Columns.IndexOf("ID");
			// Index des ausgewaehlten Auftrags
			int ausgewaehlteZeile = this.dtg_Personen.CurrentRowIndex;
			// ID des ausgewaehlten Auftrags in String
			string tmp_ID = dtable_Person.Rows[ausgewaehlteZeile].ItemArray[i_IndexOfID].ToString();
			// ID in Integer
			int i_ID = int.Parse(tmp_ID);
	
			return i_ID;
		}

		#endregion

		#region get- Methoden
//		public DataGrid DataGrid_Personen
//		{
//			get{return this.dtg_Personen;}
//		}
		#endregion
	}
}
