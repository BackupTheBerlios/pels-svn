using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace pELS.GUI.PopUp
{
	/// <summary>
	/// Summary description for Cpr_frm_Info.
	/// </summary>
	public class Cpr_frm_Info : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btn_schliessen;
		private System.Windows.Forms.PictureBox pbx_Icon;
		private System.Windows.Forms.Label lbl_version;
		private System.Windows.Forms.Label lbl_Homepage;
		private System.Windows.Forms.LinkLabel llbl_Homepage;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region eigene Variablen
		private string _version = "0.9 beta";
		private string _homepage = "www.els.sigmadelta.de";
		private string _bezeichnung = "project.ELS";
		private System.Windows.Forms.Label lbl_Programm;
		private System.Windows.Forms.Label lbl_version_inhalt;
		private System.Windows.Forms.Label lbl_Programm_Bezeichnung;
		private System.Windows.Forms.Label lbl_beschreibung;
		private string _beschreibung = "Das project.ELS (EinsatzLeitSoftware) ist das Ergebnis einer Kooperation von Studenten des Hasso-Plattner-Instituts und des Instituts für Informatik an der Universität Potsdam. Alle Rechte sind der Universität Potsdam vorbehalten.";
		#endregion

		public Cpr_frm_Info()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.LadeWerte();			
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Cpr_frm_Info));
			this.btn_schliessen = new System.Windows.Forms.Button();
			this.pbx_Icon = new System.Windows.Forms.PictureBox();
			this.lbl_version = new System.Windows.Forms.Label();
			this.lbl_Homepage = new System.Windows.Forms.Label();
			this.llbl_Homepage = new System.Windows.Forms.LinkLabel();
			this.lbl_Programm = new System.Windows.Forms.Label();
			this.lbl_version_inhalt = new System.Windows.Forms.Label();
			this.lbl_Programm_Bezeichnung = new System.Windows.Forms.Label();
			this.lbl_beschreibung = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btn_schliessen
			// 
			this.btn_schliessen.Location = new System.Drawing.Point(104, 152);
			this.btn_schliessen.Name = "btn_schliessen";
			this.btn_schliessen.TabIndex = 0;
			this.btn_schliessen.Text = "schließen";
			this.btn_schliessen.Click += new System.EventHandler(this.btn_schliessen_Click);
			// 
			// pbx_Icon
			// 
			this.pbx_Icon.Image = ((System.Drawing.Image)(resources.GetObject("pbx_Icon.Image")));
			this.pbx_Icon.Location = new System.Drawing.Point(8, 8);
			this.pbx_Icon.Name = "pbx_Icon";
			this.pbx_Icon.Size = new System.Drawing.Size(60, 60);
			this.pbx_Icon.TabIndex = 1;
			this.pbx_Icon.TabStop = false;
			// 
			// lbl_version
			// 
			this.lbl_version.Location = new System.Drawing.Point(80, 32);
			this.lbl_version.Name = "lbl_version";
			this.lbl_version.Size = new System.Drawing.Size(70, 16);
			this.lbl_version.TabIndex = 2;
			this.lbl_version.Text = "Version: ";
			// 
			// lbl_Homepage
			// 
			this.lbl_Homepage.Location = new System.Drawing.Point(80, 48);
			this.lbl_Homepage.Name = "lbl_Homepage";
			this.lbl_Homepage.Size = new System.Drawing.Size(70, 16);
			this.lbl_Homepage.TabIndex = 3;
			this.lbl_Homepage.Text = "Homepage: ";
			// 
			// llbl_Homepage
			// 
			this.llbl_Homepage.Location = new System.Drawing.Point(152, 48);
			this.llbl_Homepage.Name = "llbl_Homepage";
			this.llbl_Homepage.Size = new System.Drawing.Size(130, 16);
			this.llbl_Homepage.TabIndex = 4;
			this.llbl_Homepage.TabStop = true;
			this.llbl_Homepage.Text = "www.els.sigmadelta.de";
			// 
			// lbl_Programm
			// 
			this.lbl_Programm.Location = new System.Drawing.Point(80, 16);
			this.lbl_Programm.Name = "lbl_Programm";
			this.lbl_Programm.Size = new System.Drawing.Size(70, 16);
			this.lbl_Programm.TabIndex = 5;
			this.lbl_Programm.Text = "Programm: ";
			// 
			// lbl_version_inhalt
			// 
			this.lbl_version_inhalt.Location = new System.Drawing.Point(152, 32);
			this.lbl_version_inhalt.Name = "lbl_version_inhalt";
			this.lbl_version_inhalt.Size = new System.Drawing.Size(130, 16);
			this.lbl_version_inhalt.TabIndex = 6;
			this.lbl_version_inhalt.Text = "<version>";
			// 
			// lbl_Programm_Bezeichnung
			// 
			this.lbl_Programm_Bezeichnung.Location = new System.Drawing.Point(152, 16);
			this.lbl_Programm_Bezeichnung.Name = "lbl_Programm_Bezeichnung";
			this.lbl_Programm_Bezeichnung.Size = new System.Drawing.Size(130, 16);
			this.lbl_Programm_Bezeichnung.TabIndex = 7;
			this.lbl_Programm_Bezeichnung.Text = "<bezeichnung>";
			// 
			// lbl_beschreibung
			// 
			this.lbl_beschreibung.Location = new System.Drawing.Point(8, 80);
			this.lbl_beschreibung.Name = "lbl_beschreibung";
			this.lbl_beschreibung.Size = new System.Drawing.Size(280, 64);
			this.lbl_beschreibung.TabIndex = 8;
			this.lbl_beschreibung.Text = "<beschreibung>";
			// 
			// Cpr_frm_Info
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 181);
			this.Controls.Add(this.lbl_beschreibung);
			this.Controls.Add(this.lbl_Programm_Bezeichnung);
			this.Controls.Add(this.lbl_version_inhalt);
			this.Controls.Add(this.lbl_Programm);
			this.Controls.Add(this.llbl_Homepage);
			this.Controls.Add(this.lbl_Homepage);
			this.Controls.Add(this.lbl_version);
			this.Controls.Add(this.pbx_Icon);
			this.Controls.Add(this.btn_schliessen);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Cpr_frm_Info";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "p.ELS Info";
			this.ResumeLayout(false);

		}
		#endregion

		private void btn_schliessen_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void LadeWerte()
		{
			this.lbl_version_inhalt.Text = this._version;
			this.llbl_Homepage.Text = this._homepage;
			this.lbl_Programm_Bezeichnung.Text = this._bezeichnung;
			this.lbl_beschreibung.Text = this._beschreibung;
		}
	}
}
