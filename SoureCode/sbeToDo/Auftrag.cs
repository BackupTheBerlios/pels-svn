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
		- kapselt die UserControl usc_Auftrag und ermöglicht das Betrachten
		  eines zu ladenen Auftrags
		
	**/
	#region Member-Doku
	/**		

	**/
	#endregion			

	#region letzte Änderungen
	/**
	erstellt von: Schuppe					am: 27.11.2004
	geändert von: Schuppe					am: 27.11.2004				
	geändert von: Schuppe					am:	 1.12.2004				
	review von:								am:
	getestet von:							am:
	**/
	#endregion

	#region History/Hinweise/Bekannte Bugs:
	/**
	History:
		1.12	_usc_Auftrag wird von Hand auf Visible gesetzt
					wg. unbekanntem Bug funktionierte dies vorher nicht korrekt


	Hinweise/Bekannte Bugs:

	**/
	#endregion

	#endregion	

	// benötigt für: pELS-Objekte
	using pELS.DV;

	public class frm_AuftragsAnzeige : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		#region Windows Designer Variablen
		private System.Windows.Forms.Button btn_WeiterhinAnzeigen;
		private System.Windows.Forms.Button btn_ZurKenntnisGenommen;
		private System.ComponentModel.Container components = null;
		#endregion

		#region Instanzvariablen		
		private Cdv_Auftrag _aktuellerAuftrag;
		private pELS.Client.ToDo.usc_Auftrag _usc_Auftrag;
		private Cst_ToDo _st_ToDo;
		private Cpr_usc_TODO _pr_frm_ToDo;

		#endregion

		#region Konstruktor und Destruktor
		public frm_AuftragsAnzeige(Cst_ToDo pin_stToDo, Cpr_usc_TODO pin_prToDo)
		{
			_st_ToDo = pin_stToDo;
			_pr_frm_ToDo = pin_prToDo;
			//
			// Required for Windows Form Designer support
			//
			this._usc_Auftrag = new pELS.Client.ToDo.usc_Auftrag(_st_ToDo, _pr_frm_ToDo);
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
			this.btn_WeiterhinAnzeigen = new System.Windows.Forms.Button();
			this.btn_ZurKenntnisGenommen = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _usc_Auftrag
			// 
			this._usc_Auftrag.b_FelderModifiziert = false;
			this._usc_Auftrag.Location = new System.Drawing.Point(0, 0);
			this._usc_Auftrag.Name = "_usc_Auftrag";
			this._usc_Auftrag.TabIndex = 0;
			// 
			// btn_WeiterhinAnzeigen
			// 
			this.btn_WeiterhinAnzeigen.Location = new System.Drawing.Point(264, 472);
			this.btn_WeiterhinAnzeigen.Name = "btn_WeiterhinAnzeigen";
			this.btn_WeiterhinAnzeigen.Size = new System.Drawing.Size(160, 23);
			this.btn_WeiterhinAnzeigen.TabIndex = 14;
			this.btn_WeiterhinAnzeigen.Text = "&Weiterhin Anzeigen";
			this.btn_WeiterhinAnzeigen.Click += new System.EventHandler(this.btn_WeiterhinAnzeigen_Click);
			// 
			// btn_ZurKenntnisGenommen
			// 
			this.btn_ZurKenntnisGenommen.Location = new System.Drawing.Point(432, 472);
			this.btn_ZurKenntnisGenommen.Name = "btn_ZurKenntnisGenommen";
			this.btn_ZurKenntnisGenommen.Size = new System.Drawing.Size(184, 23);
			this.btn_ZurKenntnisGenommen.TabIndex = 13;
			this.btn_ZurKenntnisGenommen.Text = "Zur &Kenntnis genommen";
			this.btn_ZurKenntnisGenommen.Click += new System.EventHandler(this.btn_ZurKenntnisGenommen_Click);
			// 
			// frm_AuftragsAnzeige
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(624, 503);
			this.Controls.Add(this.btn_WeiterhinAnzeigen);
			this.Controls.Add(this.btn_ZurKenntnisGenommen);
			this.Controls.Add(this._usc_Auftrag);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "frm_AuftragsAnzeige";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Auftrag";
			this.ResumeLayout(false);

		}
		#endregion

		#region Methoden
		/// <summary>
		/// lädt den übergebenen Auftrag in die GUI
		/// </summary>
		/// <param name="pin_Auftrag"></param>
		public void LadeAuftrag(Cdv_Auftrag pin_Auftrag)
		{
			_aktuellerAuftrag = pin_Auftrag;
			this._usc_Auftrag.LadeAuftrag(pin_Auftrag);
		}


		#endregion

		#region GUI Eventhandler
		// das angezeigte Fenster wird geschlossen
		private void btn_WeiterhinAnzeigen_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		// das angezeigte Fenster wird geschlossen
		// die angezeigten Informationen werden aus der ToDo-Liste entfernt
		private void btn_ZurKenntnisGenommen_Click(object sender, System.EventArgs e)
		{
			_st_ToDo.EntferneAuftragAusToDoListe(_aktuellerAuftrag);
//			_pr_frm_ToDo.EntferneAktuellMarkiertenNodeAusTreeview();
			this.Close();
		}
		#endregion
	}

	}
