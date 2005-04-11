using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace pELS.Client
{
	#region Dokumentation
	/**
	Erläuterung:	
	
	erstellt von:	Xiao		am: 16.Feb.2004
	

	aktuelle Version: 0.1 alpha

	History/Hinweise/Bekannte Bugs:
	- Dies ist eine Testversion
	**/
	#endregion
	public class Cap_Client : System.Windows.Forms.Form
	{

		#region main
		[STAThread]
		static void Main() 
		{
			Cap_Client _apClient = new Cap_Client();
			Cst_Client _stClient = new Cst_Client(_apClient);
			Application.Run();
		}
		#endregion
				
		private System.ComponentModel.Container components = null;
		
		#region Konstruktor
		public Cap_Client()
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

			//
			// TODO: Fügen Sie den Konstruktorcode nach dem Aufruf von InitializeComponent hinzu
			//
		}
		#endregion
		
		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
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

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.Size = new System.Drawing.Size(300,300);
			this.Text = "Cap_Client";
		}
		#endregion
	}
}
