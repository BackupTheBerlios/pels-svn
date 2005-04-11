using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

#region Dokumentation
	/**				aktuelle Version: 0.8 alexG
	INFO:
		- kapselt die UserControl zum erfassen einer AGT-Druck-Meldung im Einsatz
		
	**/
#region Member-Doku
	/**		
	Besonderheit ->	es existieren die Properties:
		public string Druck
		public DateTime Zeit
		public bool Eingetragen
		
	Diese ensprechen dem Informationellen zustand des Objektes, soweitfür außenstehende Interessant.
	Dabei kann nur gesetzt werden (wenn geändert, dann hier ändern)
#endregion 
	**/
#endregion			

#region letzte Änderungen
	/**
	erstellt von: alexG						am: 30.11.2004
	geändert von:							am: 
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

namespace pELS.GUI.AGT
{
	/// <summary>
	/// Summary description for AGT_Druck_Eintrag.
	/// </summary>
	public class AGT_Druck_Eintrag : System.Windows.Forms.UserControl
	{
	
		#region Klassenattribute
		private System.Windows.Forms.GroupBox gbx_Rahmen;
		private System.Windows.Forms.TextBox txt_Druck;
		private System.Windows.Forms.CheckBox cbx_eintragen;
		private System.Windows.Forms.DateTimePicker dtp_Zeit;
		private System.Windows.Forms.ContextMenu ctx_Zeit;
		private System.Windows.Forms.MenuItem mI_jetzt;
		private System.Windows.Forms.ContextMenu ctx_Freischalten;
		private System.Windows.Forms.MenuItem mI_freischalten;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region 2 Konstruktoren, 1 Destruktor
		public AGT_Druck_Eintrag()
		{
			
			InitializeComponent();
		}
		public AGT_Druck_Eintrag(string pin_Name)
		{			
			InitializeComponent();
			this.gbx_Rahmen.Text = pin_Name;    		

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
			this.gbx_Rahmen = new System.Windows.Forms.GroupBox();
			this.cbx_eintragen = new System.Windows.Forms.CheckBox();
			this.dtp_Zeit = new System.Windows.Forms.DateTimePicker();
			this.ctx_Zeit = new System.Windows.Forms.ContextMenu();
			this.mI_jetzt = new System.Windows.Forms.MenuItem();
			this.txt_Druck = new System.Windows.Forms.TextBox();
			this.ctx_Freischalten = new System.Windows.Forms.ContextMenu();
			this.mI_freischalten = new System.Windows.Forms.MenuItem();
			this.gbx_Rahmen.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbx_Rahmen
			// 
			this.gbx_Rahmen.Controls.Add(this.cbx_eintragen);
			this.gbx_Rahmen.Controls.Add(this.dtp_Zeit);
			this.gbx_Rahmen.Controls.Add(this.txt_Druck);
			this.gbx_Rahmen.Location = new System.Drawing.Point(0, 0);
			this.gbx_Rahmen.Name = "gbx_Rahmen";
			this.gbx_Rahmen.Size = new System.Drawing.Size(88, 78);
			this.gbx_Rahmen.TabIndex = 3;
			this.gbx_Rahmen.TabStop = false;
			// 
			// cbx_eintragen
			// 
			this.cbx_eintragen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cbx_eintragen.Location = new System.Drawing.Point(8, 60);
			this.cbx_eintragen.Name = "cbx_eintragen";
			this.cbx_eintragen.Size = new System.Drawing.Size(72, 14);
			this.cbx_eintragen.TabIndex = 2;
			this.cbx_eintragen.Text = "eintragen";
			this.cbx_eintragen.CheckedChanged += new System.EventHandler(this.cbx_eintragen_CheckedChanged);
			// 
			// dtp_Zeit
			// 
			this.dtp_Zeit.ContextMenu = this.ctx_Zeit;
			this.dtp_Zeit.CustomFormat = "hh:mm:ss";
			this.dtp_Zeit.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtp_Zeit.Location = new System.Drawing.Point(8, 40);
			this.dtp_Zeit.Name = "dtp_Zeit";
			this.dtp_Zeit.ShowUpDown = true;
			this.dtp_Zeit.Size = new System.Drawing.Size(72, 20);
			this.dtp_Zeit.TabIndex = 1;
			this.dtp_Zeit.Value = new System.DateTime(2004, 11, 29, 11, 11, 11, 0);
			// 
			// ctx_Zeit
			// 
			this.ctx_Zeit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mI_jetzt});
			// 
			// mI_jetzt
			// 
			this.mI_jetzt.Index = 0;
			this.mI_jetzt.Text = "jetzt";
			this.mI_jetzt.Click += new System.EventHandler(this.mI_jetzt_Click);
			// 
			// txt_Druck
			// 
			this.txt_Druck.Location = new System.Drawing.Point(8, 16);
			this.txt_Druck.Name = "txt_Druck";
			this.txt_Druck.Size = new System.Drawing.Size(72, 20);
			this.txt_Druck.TabIndex = 0;
			this.txt_Druck.Text = "";
			this.txt_Druck.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// ctx_Freischalten
			// 
			this.ctx_Freischalten.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							 this.mI_freischalten});
			// 
			// mI_freischalten
			// 
			this.mI_freischalten.Index = 0;
			this.mI_freischalten.Text = "freischalten";
			this.mI_freischalten.Click += new System.EventHandler(this.mI_freischalten_Click);
			// 
			// AGT_Druck_Eintrag
			// 
			this.Controls.Add(this.gbx_Rahmen);
			this.Name = "AGT_Druck_Eintrag";
			this.Size = new System.Drawing.Size(88, 78);
			this.gbx_Rahmen.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties
		public string Druck
		{
			get{return this.txt_Druck.Text;}
		}

		public DateTime Zeit
		{
			set { this.dtp_Zeit.Value = value;}
			get { return this.dtp_Zeit.Value;}
		}

		public bool Eingetragen
		{
			get{return this.cbx_eintragen.Checked;}
		}

		public string Beschriftung
		{
			get{ return this.gbx_Rahmen.Text;}
			set{ this.gbx_Rahmen.Text = value;}

		}
		#endregion

		#region Events
		//Kontextmenue setzt Wert auf sofort
		private void mI_jetzt_Click(object sender, System.EventArgs e)
		{
			this.dtp_Zeit.Value = DateTime.Now;
		}

		//Werte unverändderlich machen
		private void cbx_eintragen_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.cbx_eintragen.CheckState == CheckState.Checked)
			{
				//Die Groupbox und damit alle elemente deaktivieren
				this.gbx_Rahmen.Enabled = false;
				//Kontextmenue zum wieder frei schalten adden
				this.ContextMenu = this.ctx_Freischalten;

				//Hässlicher code und diehnt auch nur dem Zweck einen
				//Event an das ContainerObjekt zu schicken
				this.Enabled = false;
				this.Enabled = true;
			}

		}

		//Freischalten der Groupbox
		private void mI_freischalten_Click(object sender, System.EventArgs e)
		{
			//GroupBox aktivieren
			this.gbx_Rahmen.Enabled = true;
			//häckchen raus nehmen
			this.cbx_eintragen.Checked = false;
			//Contextmenue für Groupbox sperren
			this.ContextMenu = null;
		}

		#endregion
		


	}
}
