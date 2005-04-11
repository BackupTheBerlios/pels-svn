using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pELS.Client.Logistik
{
	using pELS.DV;

	public class Cpr_usc_Logistik : System.Windows.Forms.UserControl
	{
		#region Graphische Variablen

		private System.Windows.Forms.TabPage tabpage_GEuB;
		private System.Windows.Forms.TabPage tabpage_AEuB;
		private System.Windows.Forms.TabPage tabpage_MaterialZuordnung;
		private System.Windows.Forms.TabControl tabctrl_Logistik;

		private System.ComponentModel.Container components = null;
		#endregion

		#region funktionale Variablen

		private Cst_Logistik _st_Logistik;
		/// <summary>
		/// User Control für Güter-Eingeben-und-Erfassen
		/// </summary>
		private Cpr_usc_GEuB _usc_GEuB;
		/// <summary>
		/// User Control für Anforderungen-Eingeben-und-Erfassen
		/// </summary>
		private Cpr_usc_AEuB _usc_AEuB;
		private System.Windows.Forms.HelpProvider pelsHelp;
		/// <summary>
		/// User Control für Materialübergaben
		/// </summary>
		private Cpr_usc_Material _usc_Material;

		#endregion

		#region Konstruktoren & Destruktoren
		public Cpr_usc_Logistik(Cst_Logistik pin_st_Logistik)
		{
			_st_Logistik = pin_st_Logistik;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			// lade alle USCs
			LadeUSCs();
			// Hilfe festlegen
			SetzeHilfe();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
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
			this.tabctrl_Logistik = new System.Windows.Forms.TabControl();
			this.tabpage_GEuB = new System.Windows.Forms.TabPage();
			this.tabpage_MaterialZuordnung = new System.Windows.Forms.TabPage();
			this.tabpage_AEuB = new System.Windows.Forms.TabPage();
			this.pelsHelp = new System.Windows.Forms.HelpProvider();
			this.tabctrl_Logistik.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabctrl_Logistik
			// 
			this.tabctrl_Logistik.Controls.Add(this.tabpage_GEuB);
			this.tabctrl_Logistik.Controls.Add(this.tabpage_MaterialZuordnung);
			this.tabctrl_Logistik.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabctrl_Logistik.Location = new System.Drawing.Point(0, 0);
			this.tabctrl_Logistik.Name = "tabctrl_Logistik";
			this.tabctrl_Logistik.SelectedIndex = 0;
			this.tabctrl_Logistik.Size = new System.Drawing.Size(650, 530);
			this.tabctrl_Logistik.TabIndex = 0;
			// 
			// tabpage_GEuB
			// 
			this.tabpage_GEuB.Location = new System.Drawing.Point(4, 22);
			this.tabpage_GEuB.Name = "tabpage_GEuB";
			this.tabpage_GEuB.Size = new System.Drawing.Size(642, 504);
			this.tabpage_GEuB.TabIndex = 0;
			this.tabpage_GEuB.Text = "Güter erfassen und bearbeiten";
			// 
			// tabpage_MaterialZuordnung
			// 
			this.tabpage_MaterialZuordnung.Location = new System.Drawing.Point(4, 22);
			this.tabpage_MaterialZuordnung.Name = "tabpage_MaterialZuordnung";
			this.tabpage_MaterialZuordnung.Size = new System.Drawing.Size(642, 504);
			this.tabpage_MaterialZuordnung.TabIndex = 6;
			this.tabpage_MaterialZuordnung.Text = "Materialzuordnung";
			// 
			// tabpage_AEuB
			// 
			this.tabpage_AEuB.Location = new System.Drawing.Point(4, 22);
			this.tabpage_AEuB.Name = "tabpage_AEuB";
			this.tabpage_AEuB.Size = new System.Drawing.Size(642, 504);
			this.tabpage_AEuB.TabIndex = 1;
			this.tabpage_AEuB.Text = "Anforderungen erfassen und bearbeiten";
			// 
			// Cpr_usc_Logistik
			// 
			this.Controls.Add(this.tabctrl_Logistik);
			this.Name = "Cpr_usc_Logistik";
			this.Size = new System.Drawing.Size(650, 530);
			this.tabctrl_Logistik.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#endregion

		#region LadeXXX
		/// <summary>
		/// lädt alle USCs in dieses USC
		/// </summary>
		private void LadeUSCs()
		{
			// lade Güter-Erfassen-und-Bearbeiten
			_usc_GEuB = new Cpr_usc_GEuB(_st_Logistik, this);
			_usc_GEuB.Location = new Point(1,1);
			tabpage_GEuB.Controls.Add(_usc_GEuB);
			// lade Anforderungen-Erfassen-und-Bearbeiten
			_usc_AEuB = new Cpr_usc_AEuB();
			_usc_AEuB.Location = new Point(1,1);
			tabpage_AEuB.Controls.Add(_usc_AEuB);
			// lade MaterialZuordnung
			_usc_Material = new Cpr_usc_Material(_st_Logistik, this);
			_usc_Material.Location = new Point(1,1);
			tabpage_MaterialZuordnung.Controls.Add(_usc_Material);
		}


		/// <summary>
		/// lädt alle Materialmengen in allen USCs neu
		/// </summary>
		public void LadeMaterial()
		{
			_usc_GEuB.LadeGueterGrid("Material");
		}
		/// <summary>
		/// lädt alle Verbrauchsgütermengen in allen USCs neu
		/// </summary>
		public void LadeVerbrauchsgueter()
		{
			_usc_GEuB.LadeGueterGrid("Verbrauchsgut");
		}

		#endregion

		#region SetzeXXX
		/// <summary>
		/// Überträgt die aktuellen Einheiten, KFZ und Helfer in alle TreeViews
		/// </summary>
		public void AktualisiereTreeViews()
		{
			SetzeTreeViewKraft(_usc_GEuB.tvw_Besitzer);
			SetzeTreeViewKraft(_usc_GEuB.tvw_Eigentuemer);
			SetzeTreeViewKraft(_usc_Material.tvw_aktuellerBesitzer);
			SetzeTreeViewKraft(_usc_Material.tvw_neuerBesitzer);
	
		}
		/// <summary>
		/// überträgt die aktuellen Einheiten, KFZ und Helfer in ein TreeView-Element
		/// </summary>
		/// <param name="pin_TreeView">das zu modifizierende TreeView</param>
		public void SetzeTreeViewKraft(TreeView pin_TreeView)
		{
			pin_TreeView.BeginUpdate();
			pin_TreeView.Nodes.Clear();
			// alle mögliche Kräftetypen
			string[] str_typmenge = new string[3];
			str_typmenge[0] = "Einheiten";
			str_typmenge[1] = "Helfer";
			str_typmenge[2] = "KFZ";
			
			// Knoten auf der obersten Hierachieebene erstellen
			for(int i=0; i<str_typmenge.Length; i++)
			{
				pin_TreeView.Nodes.Add(str_typmenge[i]);
			}
			// Knoten unter dem Oberknoten "Einheit"
			foreach(Cdv_Einheit Einheit in this._st_Logistik._AlleEinheiten)
			{
				TreeNode neuerKnoten = new TreeNode();
				neuerKnoten.Text = Einheit.ToString();
				neuerKnoten.Tag = Einheit;
				pin_TreeView.Nodes[0].Nodes.Add(neuerKnoten);
			}

			foreach(Cdv_Helfer Helfer in this._st_Logistik._AlleHelfer)
			{
				TreeNode neuerKnoten = new TreeNode();
				neuerKnoten.Text = Helfer.ToString();
				neuerKnoten.Tag = Helfer;
				pin_TreeView.Nodes[1].Nodes.Add(neuerKnoten);
			}
			pin_TreeView.EndUpdate();

			foreach(Cdv_KFZ KFZ in this._st_Logistik._AlleKFZ)
			{
				TreeNode neuerKnoten = new TreeNode();
				neuerKnoten.Text = KFZ.ToString();
				neuerKnoten.Tag = KFZ;
				pin_TreeView.Nodes[2].Nodes.Add(neuerKnoten);
			}

		}


		#endregion

		#region LadeXXX
		/// <summary>
		/// ermittelt alle Empfänger und stellt diese dar
		/// </summary>
		/// <param name="pin_TreeView"></param>
		/// <param name="pin_EmpfaengerIDMenge"></param>
		/// <returns>markierte TreeNodes</returns>
		public TreeNode[] LadeTreeViewKraefte(TreeView pin_TreeView,  int[] pin_EmpfaengerIDMenge)
		{
			#region Holen der Empfänger
			ArrayList einheitenMenge;
			ArrayList helfermenge;
			ArrayList kfzMenge;
			// Holen der Empfänger, speichern in die String-Arrays
			_st_Logistik.ID2Kraft(pin_EmpfaengerIDMenge, out einheitenMenge, out helfermenge, out kfzMenge);
			#endregion
  		
			#region Darstellen in EmpfängerTreeview
			ArrayList pout_tmpTreeNodes = new ArrayList();
			// Anpassen Treeview beginnen
			pin_TreeView.BeginUpdate();
  			
			pin_TreeView.Nodes.Clear();
			// alle mögliche Kräftetypen
			string[] str_typmenge = new string[3];
			str_typmenge[0] = "Einheiten";
			str_typmenge[1] = "Helfer";
			str_typmenge[2] = "Kfz";
			// Laufindex
			int i_lauf = 0;
  			
			// Knoten auf der obersten Hierachieebene erstellen
			for(int i=0; i < str_typmenge.Length; i++)
			{
				pin_TreeView.Nodes.Add(str_typmenge[i]);
			}
			// Knoten unter dem Oberknoten "Einheit"
			foreach(Cdv_Einheit Einheit in einheitenMenge)
			{
				TreeNode neuerKnoten = new TreeNode(Einheit.ToString());								
				neuerKnoten.Tag = Einheit;
				pin_TreeView.Nodes[0].Nodes.Add(neuerKnoten);
				pin_TreeView.Nodes[0].Nodes[i_lauf].Checked = true;
				i_lauf++;
				pout_tmpTreeNodes.Add(neuerKnoten);
			}
			i_lauf = 0;
			// Knoten unter dem Oberknoten "Helfer"
			foreach(Cdv_Helfer Helfer in helfermenge)
			{
				TreeNode neuerKnoten = new TreeNode(Helfer.ToString());								
				neuerKnoten.Tag = Helfer;
				pin_TreeView.Nodes[1].Nodes.Add(neuerKnoten);
				pin_TreeView.Nodes[1].Nodes[i_lauf].Checked = true;
				i_lauf++;
				pout_tmpTreeNodes.Add(neuerKnoten);
			}		
			i_lauf = 0;
			// Knoten unter dem Oberknoten "Kfz"
			foreach(Cdv_KFZ KFZ in kfzMenge)
			{
				TreeNode neuerKnoten = new TreeNode(KFZ.ToString());								
				neuerKnoten.Tag = KFZ;
				pin_TreeView.Nodes[2].Nodes.Add(neuerKnoten);
				pin_TreeView.Nodes[2].Nodes[i_lauf].Checked = true;
				i_lauf++;
				pout_tmpTreeNodes.Add(neuerKnoten);
			}	
  						
			// Anpassen Treeview beenden
			pin_TreeView.ExpandAll();
			pin_TreeView.EndUpdate();
			TreeNode[] pout_TreeNodes = new TreeNode[pout_tmpTreeNodes.Count];
			pout_tmpTreeNodes.CopyTo(pout_TreeNodes);
			return pout_TreeNodes;
			#endregion
		}

		#endregion

		#region ISBE
		/// <summary>
		/// passt die GUI an die aktuelle Rolle an
		/// </summary>
		/// <param name="pin_i_aktuelleRolle"></param>
		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
		{
			
			Tdv_Systemrolle rolle = (Tdv_Systemrolle)pin_i_aktuelleRolle;
			this.tabctrl_Logistik.TabPages.Clear();
			
			switch (rolle)
			{
					//Haben alle die kompletten Rechte
				case Tdv_Systemrolle.Zugführer: 
				case Tdv_Systemrolle.Zugtruppführer:
				case Tdv_Systemrolle.LeiterStab:
				case Tdv_Systemrolle.Führungsgehilfe:
				case Tdv_Systemrolle.S4: 
				{
					//F170, F178, F200
					this.tabctrl_Logistik.TabPages.Add(this.tabpage_GEuB);
					//F172, F174, F176, F178
					this.tabctrl_Logistik.TabPages.Add(this.tabpage_AEuB);
					//F210
					this.tabctrl_Logistik.TabPages.Add(this.tabpage_MaterialZuordnung);
					break;
				}
				case Tdv_Systemrolle.Einsatzleiter:
				case Tdv_Systemrolle.LeiterFüSt:
				{
					//F170, F178, F200
					this.tabctrl_Logistik.TabPages.Add(this.tabpage_GEuB);
					//F172, F174, F176, F178
					this.tabctrl_Logistik.TabPages.Add(this.tabpage_AEuB);
					break;
				}
				case Tdv_Systemrolle.Fernmelder :
				case Tdv_Systemrolle.Sichter :
				case Tdv_Systemrolle.S1:
				case Tdv_Systemrolle.S2:
				case Tdv_Systemrolle.S5: 
				case Tdv_Systemrolle.S6:
				case Tdv_Systemrolle.S3 :
				default:	break;
			}
		}

		#endregion

		private void SetzeHilfe()
		{
			this.pelsHelp.HelpNamespace = _st_Logistik.Einstellung.Helpfile;
			this.pelsHelp.SetShowHelp(this,true);
			this.pelsHelp.SetHelpKeyword(this,"Logistik");

		}
	}
}

