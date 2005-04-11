using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;


namespace pELS.Client.ToDo
{
	#region Dokumentation
	/**				aktuelle Version: 1.0 Schuppe
	INFO:
		- implementiert die ToDo-Liste
		
		erstellt von:	Schuppe			am: 27.11.04
		geändert von:	Hütte			am: 09.03.05

	**/
	#region Member-Doku
	/**		

	**/
	#endregion			

	#region letzte Änderungen
	/**
	erstellt von:	Schuppe					am: 27.11.2004
	geändert von:	Schuppe					am: 27.11.2004				
	review von:		Quecky					am:	29.11.2004
	geändert von:	Hütte					am: 09.03.05
	getestet von:							am:
	**/
	#endregion

	#region History/Hinweise/Bekannte Bugs:
	/**
	History:



	Hinweise/Bekannte Bugs:
	- Rollenrechte sind nicht implementiert

	**/
	#endregion

	#endregion	

	// benötigt für: PopUp
	using pELS.GUI.PopUp;
	// benötigt für: pELS-Objekte
	using pELS.DV;
	// benötigt für: pels-Objecte
	using pELS.DV.Server.Interfaces;

	public class Cpr_usc_TODO : System.Windows.Forms.UserControl
	{
		#region graphische Elemente der Klasse
		private System.Windows.Forms.Panel _pnl_TreeViewHalter;
		private System.Windows.Forms.TreeView _trv_ToDoListe;
		private System.Windows.Forms.Button btn_trvPlus;
		private System.Windows.Forms.Button btn_trvMinus;
		private System.ComponentModel.Container components = null;
		#endregion

		#region funktionale Elemente der Klasse
		private Cst_ToDo _st_ToDo;
		/// <summary>
		///  identifizieren den Oberknoten Termine
		/// </summary>
		TreeNode _tn_Termine;
		/// <summary>
		///  identifizieren den Oberknoten Meldungen
		/// </summary>
		TreeNode _tn_Meldungen;
		/// <summary>
		///  identifizieren den Oberknoten Aufträge
		/// </summary>
		TreeNode _tn_Auftraege;

		private ArrayList _ar_TermineZumErinnern = new ArrayList();
		private System.Windows.Forms.HelpProvider pelsHelp;
	
		public ArrayList Ar_TermineZumErinnern
		{
			get{return _ar_TermineZumErinnern;}
			set{_ar_TermineZumErinnern = value;}
		}

		private System.Windows.Forms.Timer _timer_ErinnerungsTimer = new Timer();
		#endregion

		#region Konstruktoren & Destruktoren
		public Cpr_usc_TODO(Cst_ToDo pin_Cst_ToDo)
		{
			_st_ToDo = pin_Cst_ToDo;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			// Treeviewknoten initialisieren			
			InitialisiereTreeView();
			SetzeGroesseAufPortal();
			AktualisiereTreeView();

			// Hilfe festlegen
			SetzeHilfe();

		}

		private void InitialisiereTreeView()
		{		
			// füge die obersten Knoten hinzu
			_tn_Termine = new TreeNode("Termine");
			_tn_Meldungen = new TreeNode("Meldungen");
			_tn_Auftraege = new TreeNode("Aufträge");
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing  )
			{
				if( components != null )
					components.Dispose();
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
			this._pnl_TreeViewHalter = new System.Windows.Forms.Panel();
			this.btn_trvMinus = new System.Windows.Forms.Button();
			this.btn_trvPlus = new System.Windows.Forms.Button();
			this._trv_ToDoListe = new System.Windows.Forms.TreeView();
			this.pelsHelp = new System.Windows.Forms.HelpProvider();
			this._pnl_TreeViewHalter.SuspendLayout();
			this.SuspendLayout();
			// 
			// _pnl_TreeViewHalter
			// 
			this._pnl_TreeViewHalter.Controls.Add(this.btn_trvMinus);
			this._pnl_TreeViewHalter.Controls.Add(this.btn_trvPlus);
			this._pnl_TreeViewHalter.Controls.Add(this._trv_ToDoListe);
			this._pnl_TreeViewHalter.Location = new System.Drawing.Point(8, 8);
			this._pnl_TreeViewHalter.Name = "_pnl_TreeViewHalter";
			this._pnl_TreeViewHalter.Size = new System.Drawing.Size(280, 512);
			this._pnl_TreeViewHalter.TabIndex = 0;
			// 
			// btn_trvMinus
			// 
			this.btn_trvMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btn_trvMinus.Location = new System.Drawing.Point(80, 16);
			this.btn_trvMinus.Name = "btn_trvMinus";
			this.btn_trvMinus.Size = new System.Drawing.Size(72, 23);
			this.btn_trvMinus.TabIndex = 10;
			this.btn_trvMinus.Text = "Zuklappen";
			this.btn_trvMinus.Click += new System.EventHandler(this.btn_trvMinus_Click);
			// 
			// btn_trvPlus
			// 
			this.btn_trvPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btn_trvPlus.Location = new System.Drawing.Point(8, 16);
			this.btn_trvPlus.Name = "btn_trvPlus";
			this.btn_trvPlus.Size = new System.Drawing.Size(72, 23);
			this.btn_trvPlus.TabIndex = 1;
			this.btn_trvPlus.Text = "Aufklappen";
			this.btn_trvPlus.Click += new System.EventHandler(this.btn_trvPlus_Click);
			// 
			// _trv_ToDoListe
			// 
			this._trv_ToDoListe.ImageIndex = -1;
			this._trv_ToDoListe.Indent = 20;
			this._trv_ToDoListe.Location = new System.Drawing.Point(8, 40);
			this._trv_ToDoListe.Name = "_trv_ToDoListe";
			this._trv_ToDoListe.SelectedImageIndex = -1;
			this._trv_ToDoListe.Size = new System.Drawing.Size(176, 464);
			this._trv_ToDoListe.Sorted = true;
			this._trv_ToDoListe.TabIndex = 2;
			this._trv_ToDoListe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._trv_ToDoListe_KeyPress);
			this._trv_ToDoListe.DoubleClick += new System.EventHandler(this._trv_ToDoListe_DoubleClick);
			// 
			// Cpr_usc_TODO
			// 
			this.Controls.Add(this._pnl_TreeViewHalter);
			this.Name = "Cpr_usc_TODO";
			this.Size = new System.Drawing.Size(296, 530);
			this._pnl_TreeViewHalter.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		// passt die Größe des UserControl der ToDo-Liste so an,
		// dass sie an die Seite der pELS-Applikation gestellt werden kann
		public void SetzeGroesseAufSeitenelement()
		{
			this.Size = new System.Drawing.Size(112, 530);
			this._pnl_TreeViewHalter.Size = new System.Drawing.Size(96, 512);
			this._trv_ToDoListe.Size = new System.Drawing.Size(80, 496);
		}

		// passt die Größe des UserControl der ToDo-Liste so an,
		// dass sie als Portal eingesetzt werden kann
		public void SetzeGroesseAufPortal()
		{
			this.Size = new System.Drawing.Size(650, 530);
			this._pnl_TreeViewHalter.Size = new System.Drawing.Size(638, 512);			
			this._trv_ToDoListe.Size = new System.Drawing.Size(629, 459);
		}		

		private void SetzeHilfe()
		{
			this.pelsHelp.HelpNamespace = _st_ToDo.Einstellung.Helpfile;
			this.pelsHelp.SetShowHelp(this,true);
			this.pelsHelp.SetHelpKeyword(this,"ToDo Liste");

		}

		#region Cpr_usc_ToDo members
		/// <summary>
		/// Löscht alle Knoten und stellt die drei Hauptknoten
		/// Meldungen, Aufträge und Termine wieder her
		/// </summary>
		private void LeerenTreeView()
		{
			_trv_ToDoListe.BeginUpdate();
			// Alle Knoten Löschen
			_trv_ToDoListe.Nodes.Clear();
			_tn_Meldungen.Nodes.Clear();
			_tn_Auftraege.Nodes.Clear();
			_tn_Termine.Nodes.Clear();
			// Hauptknoten hinzufügen
			_trv_ToDoListe.Nodes.Add(_tn_Meldungen);
			_trv_ToDoListe.Nodes.Add(_tn_Auftraege);
			_trv_ToDoListe.Nodes.Add(_tn_Termine);
			_trv_ToDoListe.EndUpdate();
		}	
		/// <summary>
		/// Wird aufgerufen, nachdem "Zur Kenntnis genommen" geclickt wurde und
		/// entfernt das angeschaute Item aus dem Baum. 
		/// </summary>
		public void EntferneAktuellMarkiertenNodeAusTreeview()
		{			
			_trv_ToDoListe.Nodes.Remove(_trv_ToDoListe.SelectedNode);
		}		
		/// <summary>
		/// Fügt eine Meldung in den entsprechenden Knoten im Treeview ein
		/// </summary>
		/// <param name="pin_Termin">einzufügende Meldung</param>
		private void FuegeMeldungHinzu(Cdv_Meldung pin_meldung)
		{
			// Neuen Knoten erzeugen, Meldung dort ablegen			
			TreeNode tnNeueMeldung  = new TreeNode();
			tnNeueMeldung.Text = pin_meldung.Text;            
			tnNeueMeldung.Tag = pin_meldung;
			// neuen Knoten einordnen
			_tn_Meldungen.Nodes.Add(tnNeueMeldung);
		}
		/// <summary>
		/// Fügt einen Auftrag in den entsprechenden Knoten im Treeview ein
		/// </summary>
		/// <param name="pin_Termin">einzufügender Auftrag</param>
		private void FuegeAuftragHinzu(Cdv_Auftrag pin_auftrag)
		{
			// Neuen Knoten erzeugen			
			TreeNode tnNeuerAuftrag  = new TreeNode();
			tnNeuerAuftrag.Text = pin_auftrag.Text;
			tnNeuerAuftrag.Tag = pin_auftrag;
			// neuen Knoten einordnen
			_tn_Auftraege.Nodes.Add(tnNeuerAuftrag);					
		}
		/// <summary>
		/// Fügt einen Termin in den entsprechenden Knoten im Treeview ein
		/// </summary>
		/// <param name="pin_Termin">einzufügender Termin</param>
		private void FuegeTerminHinzu(Cdv_Termin pin_termin)
		{
			// Neuen Knoten erzeugen			
			TreeNode tnNeuerTermin  = new TreeNode();
			// Wichtige Termine hervorheben
			if (pin_termin.IstWichtig) 
				tnNeuerTermin.Text = "!!! WICHTIG !!! ";
			tnNeuerTermin.Text += pin_termin.Betreff;

			tnNeuerTermin.Tag = pin_termin;
			// neuen Knoten einordnen
			_tn_Termine.Nodes.Add(tnNeuerTermin);		
		}
		/// <summary>
		/// Füllt den Treeview mit den Meldungen, Auftraegen u. Terminen des akt. Benutzers
		/// </summary>
		public void AktualisiereTreeView()
		{
			// Leert den TreeView
			LeerenTreeView();
			// Verhindert, dass die Treeviewansicht an den Hinzufügefortschritt angepasst wird
			_trv_ToDoListe.BeginUpdate();
			if (_st_ToDo.ToDoListeMeldungen != null)
				foreach(Cdv_Meldung m in _st_ToDo.ToDoListeMeldungen)
					FuegeMeldungHinzu(m);
			if (_st_ToDo.ToDoListeAuftraege != null)
				foreach(Cdv_Auftrag a in _st_ToDo.ToDoListeAuftraege)
					FuegeAuftragHinzu(a);
			if (_st_ToDo.ToDoListeTermine != null)
				foreach(Cdv_Termin t in _st_ToDo.ToDoListeTermine)
					FuegeTerminHinzu(t);
			// Ansicht wieder freischalten
			_trv_ToDoListe.EndUpdate();		
			_trv_ToDoListe.Refresh();
		}		
		/// <summary>
		/// Öffnet das jeweilige Formular und zeigt den angeklickten Eintrag der ToDo-Liste an
		/// </summary>
		/// <param name="pin_meldung">Anzuzeigende Meldung</param>
		private void OeffneListeneintrag(Cdv_Meldung pin_meldung)
		{
			Cpr_frm_MeldungsAnzeige _frm_Meldung = new Cpr_frm_MeldungsAnzeige(_st_ToDo, this);
			// füge das Formular zum Anzeigen von Meldungen dem Oberformular hinzu
			this.FindForm().AddOwnedForm(_frm_Meldung);
			_frm_Meldung.LadeMeldung(pin_meldung);
			_frm_Meldung.ShowDialog();
		}
		/// <summary>
		/// Öffnet das jeweilige Formular und zeigt den angeklickten Eintrag der ToDo-Liste an
		/// </summary>
		/// <param name="pin_auftrag">Anzuzeigender Auftrag</param>
		private void OeffneListeneintrag(Cdv_Auftrag pin_auftrag)
		{
			frm_AuftragsAnzeige _frm_Auftrag = new frm_AuftragsAnzeige(_st_ToDo, this);		
			// füge das Formular zum Anzeigen von Aufträgen dem Oberformular hinzu
			this.FindForm().AddOwnedForm(_frm_Auftrag);
			_frm_Auftrag.LadeAuftrag(pin_auftrag);
			_frm_Auftrag.ShowDialog();
		}
		/// <summary>
		/// Öffnet das jeweilige Formular und zeigt den angeklickten Eintrag der ToDo-Liste an
		/// </summary>
		/// <param name="pin_termin">Anzuzeigender Termin</param>
		private void OeffneListeneintrag(Cdv_Termin pin_termin)
		{
			frm_TerminAnzeige _frm_Termin = new frm_TerminAnzeige(_st_ToDo);			
			// füge das Formular zum Anzeigen von Terminen dem Oberformular hinzu
			this.FindForm().AddOwnedForm(_frm_Termin);
			_frm_Termin.FuelleTerminInFormular(pin_termin);
			_frm_Termin.ShowDialog();
		}
		

		#region Methoden für höhere Schichten
		/// <summary>
		/// ermittelt alle Empfänger und stellt diese dar
		/// </summary>
		/// <param name="pin_TreeNode"></param>
		/// <param name="pin_IDMenge"></param>
		public void LadeMitteilungsEmpfaenger(TreeView pin_TreeView,  int[] pin_EmpfaengerIDMenge)
		{
			#region Holen der Empfänger
			String[] einheitenMenge;
			String[] helfermenge;
			String[] kfzMenge;
			// Holen der Empfänger, speichern in die String-Arrays
			_st_ToDo.HoleEmpfaenger(pin_EmpfaengerIDMenge, out einheitenMenge, out helfermenge, out kfzMenge);
			#endregion
  		
			#region Darstellen in EmpfängerTreeview
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
			foreach(String s in einheitenMenge)
			{
				TreeNode neuerKnoten = new TreeNode(s);								
				pin_TreeView.Nodes[0].Nodes.Add(neuerKnoten);
				pin_TreeView.Nodes[0].Nodes[i_lauf].Checked = true;
				i_lauf++;
			}
			i_lauf = 0;
			// Knoten unter dem Oberknoten "Helfer"
			foreach(String s in helfermenge)
			{
				TreeNode neuerKnoten = new TreeNode(s);				
				pin_TreeView.Nodes[1].Nodes.Add(neuerKnoten);
				pin_TreeView.Nodes[1].Nodes[i_lauf].Checked = true;
				i_lauf++;
			}		
			i_lauf = 0;
			// Knoten unter dem Oberknoten "Kfz"
			foreach(String s in kfzMenge)
			{
				TreeNode neuerKnoten = new TreeNode(s);				
				pin_TreeView.Nodes[2].Nodes.Add(neuerKnoten);
				pin_TreeView.Nodes[2].Nodes[i_lauf].Checked = true;
				i_lauf++;
			}	
  						
			// Anpassen Treeview beenden
			pin_TreeView.ExpandAll();
			pin_TreeView.EndUpdate();
			#endregion
		}

		#endregion
		#endregion
		
		#region GUI Eventhandler
		/// <summary>
		/// Relalisiert, dass auch per Enter ein Item ausgewählt werden kann.
		/// Verweist dann auf den Eventhandler von DoubleClick
		/// </summary>		
		private void _trv_ToDoListe_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Enter))
				_trv_ToDoListe_DoubleClick(sender, e);
		}
		private void _trv_ToDoListe_DoubleClick(object sender, System.EventArgs e)
		{			
			if (_trv_ToDoListe.SelectedNode != null)
			{
				if (_trv_ToDoListe.SelectedNode.Tag is Cdv_Meldung)
					OeffneListeneintrag( (Cdv_Meldung)_trv_ToDoListe.SelectedNode.Tag );
				if (_trv_ToDoListe.SelectedNode.Tag is Cdv_Auftrag)
					OeffneListeneintrag( (Cdv_Auftrag)_trv_ToDoListe.SelectedNode.Tag );
				if (_trv_ToDoListe.SelectedNode.Tag is Cdv_Termin)
					OeffneListeneintrag( (Cdv_Termin)_trv_ToDoListe.SelectedNode.Tag );
			}
		}		
		/// <summary>
		/// expandiert alle Knoten eines TreeView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_trvPlus_Click(object sender, System.EventArgs e)
		{
			_trv_ToDoListe.ExpandAll();
		}
		/// <summary>
		/// kollabiert alle Knoten eines TreeView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_trvMinus_Click(object sender, System.EventArgs e)
		{
			_trv_ToDoListe.CollapseAll();
		}		
		#endregion


	}
}
