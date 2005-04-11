using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace pELS.Client.ToDo
{
	#region Dokumentation
	/**				aktuelle Version: 1.0 Schuppe
	INFO:
		- kapselt die UserControl usc_Termin und ermöglicht das Betrachten
		  eines zu ladenen Termins
		
	**/
	#region Member-Doku
	/**		

	**/
	#endregion			

	#region letzte Änderungen
	/**
	erstellt von: Schuppe					am: 27.11.2004
	geändert von: Schuppe					am: 27.11.2004	
	geändert von: Hütte						am: 09.03.2005			
	review von:								am:
	getestet von:							am:
	**/
	#endregion

	#region History/Hinweise/Bekannte Bugs:
	/**
	History:

	Hinweise/Bekannte Bugs:

	**/
	#endregion

	#endregion	

	// benötigt für: pELS-Objekte
	using pELS.DV;

	public class frm_TerminAnzeige : System.Windows.Forms.Form
	{
		#region Windows Designer Variablen
		private System.Windows.Forms.Button btn_WeiterhinAnzeigen;
		private System.Windows.Forms.Button btn_ZurKenntnisGenommen;
		private System.ComponentModel.Container components = null;
		#endregion
		
		#region Instanzvariablen
		private usc_Termin _usc_Termin;
		private Cdv_Termin _aktuellerTermin;
		private Cst_ToDo _st_ToDo;
		#endregion 
		
		#region Konstruktor Destruktor			
		public frm_TerminAnzeige(Cst_ToDo pin_stToDo)
		{
			_st_ToDo = pin_stToDo;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			SetzeUSCTermin();			
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._usc_Termin = new pELS.Client.ToDo.usc_Termin();
			this.btn_WeiterhinAnzeigen = new System.Windows.Forms.Button();
			this.btn_ZurKenntnisGenommen = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _usc_Termin
			// 
			this._usc_Termin.b_FelderModifiziert = false;
			this._usc_Termin.Location = new System.Drawing.Point(0, 0);
			this._usc_Termin.Name = "_usc_Termin";
			this._usc_Termin.Size = new System.Drawing.Size(632, 192);
			this._usc_Termin.TabIndex = 0;
			// 
			// btn_WeiterhinAnzeigen
			// 
			this.btn_WeiterhinAnzeigen.Location = new System.Drawing.Point(272, 200);
			this.btn_WeiterhinAnzeigen.Name = "btn_WeiterhinAnzeigen";
			this.btn_WeiterhinAnzeigen.Size = new System.Drawing.Size(160, 23);
			this.btn_WeiterhinAnzeigen.TabIndex = 14;
			this.btn_WeiterhinAnzeigen.Text = "&Weiterhin Anzeigen";
			this.btn_WeiterhinAnzeigen.Click += new System.EventHandler(this.btn_WeiterhinAnzeigen_Click);
			// 
			// btn_ZurKenntnisGenommen
			// 
			this.btn_ZurKenntnisGenommen.Location = new System.Drawing.Point(440, 200);
			this.btn_ZurKenntnisGenommen.Name = "btn_ZurKenntnisGenommen";
			this.btn_ZurKenntnisGenommen.Size = new System.Drawing.Size(184, 23);
			this.btn_ZurKenntnisGenommen.TabIndex = 13;
			this.btn_ZurKenntnisGenommen.Text = "Zur &Kenntnis genommen";
			this.btn_ZurKenntnisGenommen.Click += new System.EventHandler(this.btn_ZurKenntnisGenommen_Click);
			// 
			// frm_TerminAnzeige
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 229);
			this.Controls.Add(this.btn_WeiterhinAnzeigen);
			this.Controls.Add(this.btn_ZurKenntnisGenommen);
			this.Controls.Add(this._usc_Termin);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "frm_TerminAnzeige";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Termin";
			this.ResumeLayout(false);

		}
		#endregion

		#region Methoden
		/// <summary>
		/// setzt die Eingabemöglichkeiten der Maske
		/// damit keine Eingaben geschehen können, und nur betrachtet werden kann
		/// </summary>
		private void SetzeUSCTermin()
		{
			_usc_Termin.dtp_DatumBis.Enabled = false;
			_usc_Termin.dtp_DatumVon.Enabled = false;
			_usc_Termin.gbx_Befehl.Enabled = false;
			_usc_Termin.cbx_IstWichtig.Enabled = false;

			_usc_Termin.txt_Betreff.ReadOnly = true;

			_usc_Termin.btn_Speichern.Visible = false;
			_usc_Termin.btn_Zuruecksetzen.Visible = false;
		}

		/// <summary>
		/// Lädt die anzuzeigenen Informationen in die GUI
		/// </summary>
		/// <param name="pin_termin"></param>
		public void FuelleTerminInFormular(Cdv_Termin pin_termin)
		{
			// Aktueller Termin muss nachher zum Löschen bekannt sein
			_aktuellerTermin = pin_termin;
			// ErstelltFuer ist der aktuelle Benutzer
			_usc_Termin.lbl_ErstelltFuer.Text = _st_ToDo.Einstellung.Benutzer.Benutzername;
			// ZeitVon prüfen und eintragen
			if ( (pin_termin.ZeitVon > _usc_Termin.dtp_DatumVon.MinDate) && 
				 (pin_termin.ZeitVon < _usc_Termin.dtp_DatumVon.MaxDate) )
					_usc_Termin.dtp_DatumVon.Value = pin_termin.ZeitVon;
			// ZeitBis prüfen und eintragen
			if ( (pin_termin.ZeitBis > _usc_Termin.dtp_DatumBis.MinDate) && 
				 (pin_termin.ZeitBis < _usc_Termin.dtp_DatumBis.MaxDate) )
					_usc_Termin.dtp_DatumBis.Value = pin_termin.ZeitBis;	
			// Erinnerungszeit prüfen und eintragen
			if ( (pin_termin.Erinnerung.Zeitpunkt > _usc_Termin.dtp_ErinnernAm.MinDate) && 
				 (pin_termin.Erinnerung.Zeitpunkt < _usc_Termin.dtp_ErinnernAm.MaxDate) )
                _usc_Termin.dtp_ErinnernAm.Value = pin_termin.Erinnerung.Zeitpunkt;

			// Benutzer für "TerminErstelltVon" holen
			Cdv_Benutzer erstellerDesTermins = _st_ToDo.ID2Benutzer(pin_termin.ErstelltVonBenutzerID);
			if (erstellerDesTermins != null) _usc_Termin.lbl_ErstelltVon.Text = erstellerDesTermins.Benutzername;
			
			// Checkboxes und Betreff
			_usc_Termin.cbx_ErinnernAm.Checked = pin_termin.WirdErinnert;
			_usc_Termin.cbx_IstWichtig.Checked = pin_termin.IstWichtig;
			_usc_Termin.txt_Betreff.Text = pin_termin.Betreff;
		}
		#endregion 

		#region GUI Eventhandler
		/// <summary>
		/// Das angezeigte Fenster wird geschlossen, es wird nichts verändert.
		/// </summary>		
		private void btn_WeiterhinAnzeigen_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Das angezeigte Fenster wird geschlossen, die angezeigten Informationen 
		/// werden aus der ToDo-Liste entfernt -> Speichern in DB
		/// </summary>		
		private void btn_ZurKenntnisGenommen_Click(object sender, System.EventArgs e)
		{
			//TODO
//			_pr_frm_ToDo.EntferneAktuellMarkiertenNodeAusTreeview();
			_st_ToDo.EntferneTerminAusToDoListe(_aktuellerTermin);
			this.Close();
		}
		#endregion
	}
}
