using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


namespace pELS.GUI.AGT
{
	/// <summary>
	/// Summary description for AGT_Informationen.
	/// </summary>
	public class AGT_Informationen : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dgrid_Daten;
		private System.Windows.Forms.Button btn_abbrechen;
		private System.Windows.Forms.Button btn_drucken;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AGT_Informationen()
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
			this.dgrid_Daten = new System.Windows.Forms.DataGrid();
			this.btn_abbrechen = new System.Windows.Forms.Button();
			this.btn_drucken = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Daten)).BeginInit();
			this.SuspendLayout();
			// 
			// dgrid_Daten
			// 
			this.dgrid_Daten.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dgrid_Daten.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgrid_Daten.CaptionVisible = false;
			this.dgrid_Daten.DataMember = "";
			this.dgrid_Daten.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgrid_Daten.Location = new System.Drawing.Point(0, 0);
			this.dgrid_Daten.Name = "dgrid_Daten";
			this.dgrid_Daten.RowHeadersVisible = false;
			this.dgrid_Daten.Size = new System.Drawing.Size(480, 432);
			this.dgrid_Daten.TabIndex = 0;
			// 
			// btn_abbrechen
			// 
			this.btn_abbrechen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_abbrechen.Location = new System.Drawing.Point(208, 440);
			this.btn_abbrechen.Name = "btn_abbrechen";
			this.btn_abbrechen.Size = new System.Drawing.Size(96, 24);
			this.btn_abbrechen.TabIndex = 1;
			this.btn_abbrechen.Text = "abbrechen";
			this.btn_abbrechen.Click += new System.EventHandler(this.btn_abbrechen_Click);
			// 
			// btn_drucken
			// 
			this.btn_drucken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_drucken.Enabled = false;
			this.btn_drucken.Location = new System.Drawing.Point(352, 440);
			this.btn_drucken.Name = "btn_drucken";
			this.btn_drucken.Size = new System.Drawing.Size(96, 24);
			this.btn_drucken.TabIndex = 2;
			this.btn_drucken.Text = "drucken";
			// 
			// AGT_Informationen
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(480, 469);
			this.ControlBox = false;
			this.Controls.Add(this.btn_drucken);
			this.Controls.Add(this.btn_abbrechen);
			this.Controls.Add(this.dgrid_Daten);
			this.Name = "AGT_Informationen";
			((System.ComponentModel.ISupportInitialize)(this.dgrid_Daten)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void btn_abbrechen_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}


		#region spezielle Properties
		
		public DataTable Inhalt
		{
			set{ this.dgrid_Daten.DataSource = value;}
			get{ return (DataTable)this.dgrid_Daten.DataSource;}
		}
		
		public string Beschriftung
		{
			set{this.Text = value;}
			get{ return this.Text;}
		}
		#endregion
	}
}
