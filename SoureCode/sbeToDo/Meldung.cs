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
		- kapselt die UserControl usc_Meldung und ermöglicht das Betrachten
		  einer zu ladenen Meldung
		
	**/
	#region Member-Doku
	/**		

	**/
	#endregion			

	#region letzte Änderungen
	/**
	erstellt von: Schuppe					am: 27.11.2004
	geändert von: Schuppe					am: 27.11.2004				
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

	public class Cpr_frm_MeldungsAnzeige : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		#region Windows Designer Variablen
		private System.Windows.Forms.Button btn_ZurKenntnisGenommen;
		private System.Windows.Forms.Button btn_WeiterhinAnzeigen;		
		private System.ComponentModel.Container components = null;
		#endregion

		#region Instanzvariablen
		private Cpr_usc_Meldung _usc_Meldung;
		private Cst_ToDo _st_ToDo;
		private Cpr_usc_TODO _pr_frm_ToDo;
		private Cdv_Meldung _aktuelleMeldung;
		#endregion 

		#region Konstruktor Destruktor			
		public Cpr_frm_MeldungsAnzeige(Cst_ToDo pin_stToDo, Cpr_usc_TODO pin_prToDo)
		{
			_st_ToDo = pin_stToDo;
			_pr_frm_ToDo = pin_prToDo;
			//
			// Required for Windows Form Designer support
			//
			this._usc_Meldung = new pELS.Client.ToDo.Cpr_usc_Meldung(_st_ToDo, _pr_frm_ToDo);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			///
			this.btn_ZurKenntnisGenommen = new System.Windows.Forms.Button();
			this.btn_WeiterhinAnzeigen = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _usc_Meldung
			// 
			this._usc_Meldung.b_FelderModifiziert = false;
			this._usc_Meldung.Location = new System.Drawing.Point(0, 0);
			this._usc_Meldung.Name = "_usc_Meldung";
			this._usc_Meldung.TabIndex = 0;
			// 
			// btn_ZurKenntnisGenommen
			// 
			this.btn_ZurKenntnisGenommen.Location = new System.Drawing.Point(432, 456);
			this.btn_ZurKenntnisGenommen.Name = "btn_ZurKenntnisGenommen";
			this.btn_ZurKenntnisGenommen.Size = new System.Drawing.Size(184, 23);
			this.btn_ZurKenntnisGenommen.TabIndex = 11;
			this.btn_ZurKenntnisGenommen.Text = "Zur &Kenntnis genommen";
			this.btn_ZurKenntnisGenommen.Click += new System.EventHandler(this.btn_ZurKenntnisGenommen_Click);
			// 
			// btn_WeiterhinAnzeigen
			// 
			this.btn_WeiterhinAnzeigen.Location = new System.Drawing.Point(264, 456);
			this.btn_WeiterhinAnzeigen.Name = "btn_WeiterhinAnzeigen";
			this.btn_WeiterhinAnzeigen.Size = new System.Drawing.Size(160, 23);
			this.btn_WeiterhinAnzeigen.TabIndex = 12;
			this.btn_WeiterhinAnzeigen.Text = "&Weiterhin Anzeigen";
			this.btn_WeiterhinAnzeigen.Click += new System.EventHandler(this.btn_WeiterhinAnzeigen_Click);
			// 
			// Cpr_frm_MeldungsAnzeige
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(624, 485);
			this.Controls.Add(this.btn_WeiterhinAnzeigen);
			this.Controls.Add(this.btn_ZurKenntnisGenommen);
			this.Controls.Add(this._usc_Meldung);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Cpr_frm_MeldungsAnzeige";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Meldung";
			this.ResumeLayout(false);

		}
		#endregion

		#region Methoden

		public void LadeMeldung(Cdv_Meldung pin_Meldung)
		{
			_aktuelleMeldung = pin_Meldung;
			this._usc_Meldung.LadeMeldung(pin_Meldung);
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
			_st_ToDo.EntferneMeldungAusToDoListe(_aktuelleMeldung);
//			_pr_frm_ToDo.EntferneAktuellMarkiertenNodeAusTreeview();
			this.Close();
		}
		#endregion

	}
}
