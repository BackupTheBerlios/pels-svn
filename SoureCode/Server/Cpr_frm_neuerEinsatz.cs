using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using pELS.Tools;

namespace pELS.Server
{
	/// <summary>
	/// Summary description for Cpr_frm_neuerEinsatz.
	/// </summary>
	public class Cpr_frm_neuerEinsatz : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox gbx_DB_Name_festlegen;
		private System.Windows.Forms.Label lbl_Erklaerung;
		private System.Windows.Forms.TextBox txt_DBName;
		private System.Windows.Forms.Label lbl_DBNamen_eingeben;
		private System.Windows.Forms.Label lbl_vorsicht;
		private System.Windows.Forms.Button btn_neueDB_anlegen;
		private System.Windows.Forms.Label lbl_Hinweis;
		private System.Windows.Forms.Button btn_abbrechen;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region eigene Variablen
		//eigene Variable (merkt sich den Erfolg)
		private bool _eingabeErfolgreich = false;
		private string _neuerDBName = String.Empty;
		#endregion

		public Cpr_frm_neuerEinsatz()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Cpr_frm_neuerEinsatz));
			this.btn_abbrechen = new System.Windows.Forms.Button();
			this.gbx_DB_Name_festlegen = new System.Windows.Forms.GroupBox();
			this.lbl_vorsicht = new System.Windows.Forms.Label();
			this.lbl_Hinweis = new System.Windows.Forms.Label();
			this.lbl_DBNamen_eingeben = new System.Windows.Forms.Label();
			this.txt_DBName = new System.Windows.Forms.TextBox();
			this.lbl_Erklaerung = new System.Windows.Forms.Label();
			this.btn_neueDB_anlegen = new System.Windows.Forms.Button();
			this.gbx_DB_Name_festlegen.SuspendLayout();
			this.SuspendLayout();
			// 
			// btn_abbrechen
			// 
			this.btn_abbrechen.Location = new System.Drawing.Point(264, 216);
			this.btn_abbrechen.Name = "btn_abbrechen";
			this.btn_abbrechen.Size = new System.Drawing.Size(80, 25);
			this.btn_abbrechen.TabIndex = 1;
			this.btn_abbrechen.Text = "&Abbrechen";
			this.btn_abbrechen.Click += new System.EventHandler(this.btn_abbrechen_Click);
			// 
			// gbx_DB_Name_festlegen
			// 
			this.gbx_DB_Name_festlegen.Controls.Add(this.lbl_vorsicht);
			this.gbx_DB_Name_festlegen.Controls.Add(this.lbl_Hinweis);
			this.gbx_DB_Name_festlegen.Controls.Add(this.lbl_DBNamen_eingeben);
			this.gbx_DB_Name_festlegen.Controls.Add(this.txt_DBName);
			this.gbx_DB_Name_festlegen.Controls.Add(this.lbl_Erklaerung);
			this.gbx_DB_Name_festlegen.Location = new System.Drawing.Point(8, 8);
			this.gbx_DB_Name_festlegen.Name = "gbx_DB_Name_festlegen";
			this.gbx_DB_Name_festlegen.Size = new System.Drawing.Size(472, 200);
			this.gbx_DB_Name_festlegen.TabIndex = 3;
			this.gbx_DB_Name_festlegen.TabStop = false;
			this.gbx_DB_Name_festlegen.Text = "Datenbankname";
			// 
			// lbl_vorsicht
			// 
			this.lbl_vorsicht.Location = new System.Drawing.Point(16, 160);
			this.lbl_vorsicht.Name = "lbl_vorsicht";
			this.lbl_vorsicht.Size = new System.Drawing.Size(376, 32);
			this.lbl_vorsicht.TabIndex = 4;
			this.lbl_vorsicht.Text = "Vorsicht: Sollte der Name bereits im DBMS vorhanden sein, wird die alte Datenbank" +
				" überschrieben. Informationen gehen dabei verloren.";
			// 
			// lbl_Hinweis
			// 
			this.lbl_Hinweis.Location = new System.Drawing.Point(16, 64);
			this.lbl_Hinweis.Name = "lbl_Hinweis";
			this.lbl_Hinweis.Size = new System.Drawing.Size(432, 40);
			this.lbl_Hinweis.TabIndex = 3;
			this.lbl_Hinweis.Text = "Als Datenbankserver wird das aktuelle DatenbankManagementsystem verwendet. Alle T" +
				"abellen etc. werden per Skript für die von Ihnen angegebene Datenbank für den mo" +
				"mentan angemeldeten Benutzer erstellt.";
			// 
			// lbl_DBNamen_eingeben
			// 
			this.lbl_DBNamen_eingeben.Location = new System.Drawing.Point(16, 128);
			this.lbl_DBNamen_eingeben.Name = "lbl_DBNamen_eingeben";
			this.lbl_DBNamen_eingeben.Size = new System.Drawing.Size(336, 16);
			this.lbl_DBNamen_eingeben.TabIndex = 2;
			this.lbl_DBNamen_eingeben.Text = "Geben Sie den Namen der neu zu erstellenenden Datenbank an:";
			// 
			// txt_DBName
			// 
			this.txt_DBName.Location = new System.Drawing.Point(360, 126);
			this.txt_DBName.Name = "txt_DBName";
			this.txt_DBName.Size = new System.Drawing.Size(104, 20);
			this.txt_DBName.TabIndex = 1;
			this.txt_DBName.Text = "";
			// 
			// lbl_Erklaerung
			// 
			this.lbl_Erklaerung.Location = new System.Drawing.Point(16, 24);
			this.lbl_Erklaerung.Name = "lbl_Erklaerung";
			this.lbl_Erklaerung.Size = new System.Drawing.Size(432, 32);
			this.lbl_Erklaerung.TabIndex = 0;
			this.lbl_Erklaerung.Text = "Sie können hier eine neue Datenbank für einen neuen Einsatz auf Ihrem Datenbankse" +
				"rver anlegen.";
			// 
			// btn_neueDB_anlegen
			// 
			this.btn_neueDB_anlegen.Location = new System.Drawing.Point(360, 216);
			this.btn_neueDB_anlegen.Name = "btn_neueDB_anlegen";
			this.btn_neueDB_anlegen.Size = new System.Drawing.Size(120, 25);
			this.btn_neueDB_anlegen.TabIndex = 4;
			this.btn_neueDB_anlegen.Text = "Datenbank anlegen";
			this.btn_neueDB_anlegen.Click += new System.EventHandler(this.btn_neueDB_anlegen_Click);
			// 
			// Cpr_frm_neuerEinsatz
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(488, 245);
			this.Controls.Add(this.btn_neueDB_anlegen);
			this.Controls.Add(this.gbx_DB_Name_festlegen);
			this.Controls.Add(this.btn_abbrechen);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Cpr_frm_neuerEinsatz";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "neuen Einsatz anlegen";
			this.gbx_DB_Name_festlegen.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Eventhandler
		private void btn_neueDB_anlegen_Click(object sender, System.EventArgs e)
		{
			if(this.txt_DBName.Text == "")
				MessageBox.Show("Sie müssen einen Namen für die DB angeben","Namenangeben",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
			else
			{
				Cdv_DB dbInstanz = Cdv_DB.HoleInstanz();
				if(SchreibeDbNamenInDatei())
				{									
					_neuerDBName = txt_DBName.Text;
					_eingabeErfolgreich = true;
					this.Close();
				}				
			}
		}

		private void btn_abbrechen_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		#endregion
		#region Funktionalität
		private bool SchreibeDbNamenInDatei()
		{
			XMLZugriff myXML = new XMLZugriff();
			myXML.LadeDatei(pELS.Tools.Server.CKonstanten._str_ServerConfigPfad);
			myXML.WaehleKnoten("pELS/pELS-Server/DBConfig");
			myXML.SetzeKnotenAttribut(0,"DBName", this.txt_DBName.Text);
			if(myXML.SpeichereDatei() == "")
				return true;
			else
			{
				DialogResult dr_auswahl;
				dr_auswahl = MessageBox.Show("\nDer Vorgang kann so nicht fortgesetzt werden."
					+"\nStellen Sie sicher, dass die Datei '"
					+ pELS.Tools.Server.CKonstanten._str_ServerConfigPfad
					+"' vorhanden, nicht beschädigt oder schreibgeschützt ist.",
					"Änderungen konnten nicht geschrieben werden",
					MessageBoxButtons.RetryCancel,
					MessageBoxIcon.Error);
			
				if(dr_auswahl == DialogResult.Retry)
					return this.SchreibeDbNamenInDatei();
				else
					return false;		
			}
		}

		#endregion
		#region Properties
		public bool EingabeErfolgreich
		{
			get{ return this._eingabeErfolgreich;}
		}

		public string neuerDBName
		{
			get{return this._neuerDBName;}
		}
					
	#endregion
	

		
	}
}
