using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pELS.GUI.ToDo
{
	#region Dokumentation
	/**				aktuelle Version: 1.0 Schuppe
	INFO:
		- implementiert die ToDo-Liste
		
		erstellt von:	Schuppe			am: 27.11.04

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
	public class usc_TODO : System.Windows.Forms.UserControl
	{
		#region graphische Elemente der Klasse
		private System.Windows.Forms.Panel _pnl_TreeViewHalter;
		private System.Windows.Forms.TreeView _trv_ToDoListe;

		private System.ComponentModel.Container components = null;
		#endregion

		#region funktionale Elemente der Klasse
		// identifizieren die obersten Knoten der TreeView
		TreeNode _tn_Termine;
		TreeNode _tn_Meldungen;
		TreeNode _tn_Auftraege;
		// speichert Verweise auf die eigentlichen Termine bzw. Mitteilungen
		ArrayList _arl_Termine;
		ArrayList _arl_Meldungen;
		ArrayList _arl_Auftraege;
		#endregion

		#region Konstruktoren & Destruktoren
		public usc_TODO()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call
			InitialisiereTreeView();

			SetzeGroesseAufPortal();

			ErzeugeBeispielwerte();
		}

		private void InitialisiereTreeView()
		{
			// füge die obersten Knoten hinzu
			_tn_Termine = new TreeNode("Termine");
			_tn_Meldungen = new TreeNode("Meldungen");
			_tn_Auftraege = new TreeNode("Aufträge");
			_trv_ToDoListe.Nodes.Add(_tn_Termine);
			_trv_ToDoListe.Nodes.Add(_tn_Meldungen);
			_trv_ToDoListe.Nodes.Add(_tn_Auftraege);
			// initialisiert ArrayLists
			_arl_Termine = new ArrayList();
			_arl_Meldungen = new ArrayList();
			_arl_Auftraege = new ArrayList();
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
			this._trv_ToDoListe = new System.Windows.Forms.TreeView();
			this._pnl_TreeViewHalter.SuspendLayout();
			this.SuspendLayout();
			// 
			// _pnl_TreeViewHalter
			// 
			this._pnl_TreeViewHalter.Controls.Add(this._trv_ToDoListe);
			this._pnl_TreeViewHalter.Location = new System.Drawing.Point(8, 8);
			this._pnl_TreeViewHalter.Name = "_pnl_TreeViewHalter";
			this._pnl_TreeViewHalter.Size = new System.Drawing.Size(96, 512);
			this._pnl_TreeViewHalter.TabIndex = 0;
			// 
			// _trv_ToDoListe
			// 
			this._trv_ToDoListe.ImageIndex = -1;
			this._trv_ToDoListe.Location = new System.Drawing.Point(8, 8);
			this._trv_ToDoListe.Name = "_trv_ToDoListe";
			this._trv_ToDoListe.SelectedImageIndex = -1;
			this._trv_ToDoListe.Size = new System.Drawing.Size(80, 496);
			this._trv_ToDoListe.Sorted = true;
			this._trv_ToDoListe.TabIndex = 1;
			this._trv_ToDoListe.DoubleClick += new System.EventHandler(this._trv_ToDoListe_DoubleClick);
			// 
			// usc_TODO
			// 
			this.Controls.Add(this._pnl_TreeViewHalter);
			this.Name = "usc_TODO";
			this.Size = new System.Drawing.Size(112, 530);
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
			this._trv_ToDoListe.Size = new System.Drawing.Size(629, 496);
		}



		#region Termin, Meldung und Auftragsverwaltung für usc_ToDo
		// beschreibt die Informationen eines Termins, welche in der ToDo-Liste angezeigt werden
		public struct struct_Termin
		{
			private string _str_text;
			public string str_text
			{
				get { return _str_text;}
			}
			private DateTime _date_zeitpunkt;
			public DateTime date_zeitpunkt
			{
				get { return _date_zeitpunkt;}
			}
			public string str_KnotenText
			{
				get 
				{
					return date_zeitpunkt.ToString() + " " + str_text;
				}
			}
			//TODO: hier muss der Typ festgelegt werden,
			//mit welchem der Verweis auf den eigentlichen Termin gespeichert wird
			// Schuppe
			private string _str_VerweisAufTermin;
			public string str_VerweisAufTermin
			{
				get { return _str_VerweisAufTermin;}
			}

			public struct_Termin(DateTime Zeitpunkt, string Text, string VerweisAufTermin)
			{
				_date_zeitpunkt = Zeitpunkt;
				_str_text = Text;
				_str_VerweisAufTermin = VerweisAufTermin;
			}
		}

		// fügt einen Termin hinzu
		public void FuegeTerminHinzu(struct_Termin pin_Termin)
		{
			TreeNode _tnNeuerTermin  = new TreeNode(pin_Termin.str_KnotenText);
			_tn_Termine.Nodes.Add(_tnNeuerTermin);
			_arl_Termine.Add(pin_Termin);
		}

		// entfernt einen Termin
		public void EntferneTermin(struct_Termin pin_Termin)
		{
			// wandere durch alle Knotenelement
			for(int _i_Zaehler = 0; _i_Zaehler < _tn_Termine.Nodes.Count; _i_Zaehler++)
			{
				// vergleiche Knotentext 
				if (_tn_Termine.Nodes[_i_Zaehler].Text == pin_Termin.str_KnotenText)
				{
					_tn_Termine.Nodes.RemoveAt(_i_Zaehler);
					// beende Schleife
					break;
				}
			}
			// entferne Termin-Struct aus der ArrayList
			_arl_Termine.Remove(pin_Termin);
		}

		// liefert Informationen zu einen Termin mit Hilfe des Knotentextes
		// falls kein entsprechender Termin gefunden wurde, wird ein leerer Termin zurückgegeben
		public struct_Termin GetTermin(string Knotentext)
		{
			for(int _i_Position = 0; _i_Position < _arl_Termine.Count; _i_Position++)
			{
				struct_Termin pout_structTermin_Termin = (struct_Termin) _arl_Termine[_i_Position];
				if (pout_structTermin_Termin.str_KnotenText == Knotentext)
					return pout_structTermin_Termin;
			}
			return new struct_Termin(new DateTime(0,0,0,0,0,0), "", "");
		}
		// öffnet einen Dialog mit den Termindaten
		private void OeffneTermin(struct_Termin pin_Termin)
		{
			frm_TerminAnzeige _frm_Termin = new frm_TerminAnzeige();
			//TODO: laden der Informationen in das Formular.USC...
			// füge das Formular zum Anzeigen von Terminen dem Oberformular hinzu
			this.FindForm().AddOwnedForm(_frm_Termin);
			_frm_Termin.ShowDialog();
		}


		
		// beschreibt die Informationen einer Meldung, welche in der ToDo-Liste angezeigt werden
		public struct struct_Meldung
		{
			private string _str_text;
			public string str_text
			{
				get { return _str_text;}
			}
			private DateTime _date_abfassungsdatum;
			public DateTime date_abfassungsdatum
			{
				get { return _date_abfassungsdatum;}
			}
			public string str_KnotenText
			{
				get 
				{
					return _date_abfassungsdatum.ToString() + " " + str_text;
				}
			}
			//TODO: hier muss der Typ festgelegt werden,
			//mit welchem der Verweis auf den eigentlichen Termin gespeichert wird
			// Schuppe
			private string _str_VerweisAufMeldung;
			public string str_VerweisAufMeldung
			{
				get { return _str_VerweisAufMeldung;}
			}

			public struct_Meldung(DateTime Abfassungsdatum, string Text, string VerweisAufMeldung)
			{
				_date_abfassungsdatum = Abfassungsdatum;
				_str_text = Text;
				_str_VerweisAufMeldung = VerweisAufMeldung;
			}
		}

		// fügt eine Meldung hinzu
		public void FuegeMeldungHinzu(struct_Meldung pin_Meldung)
		{
			TreeNode _tnNeueMeldung  = new TreeNode(pin_Meldung.str_KnotenText);
			_tn_Meldungen.Nodes.Add(_tnNeueMeldung);
			_arl_Meldungen.Add(pin_Meldung);
		}

		// entfernt eine Meldung
		public void EntferneMeldung(struct_Meldung pin_Meldung)
		{
			// wandere durch alle Knotenelement
			for(int _i_Zaehler = 0; _i_Zaehler < _tn_Meldungen.Nodes.Count; _i_Zaehler++)
			{
				// vergleiche Knotentext 
				if (_tn_Meldungen.Nodes[_i_Zaehler].Text == pin_Meldung.str_KnotenText)
				{
					_tn_Meldungen.Nodes.RemoveAt(_i_Zaehler);
					// beende Schleife
					break;
				}
			}
			_arl_Meldungen.Remove(pin_Meldung);
		}

		// liefert Informationen zu einer Meldung mit Hilfe des Knotentextes
		// falls keine entsprechende Meldung gefunden wurde, wird eine leere Meldung zurückgegeben
		public struct_Meldung GetMeldung(string Knotentext)
		{
			for(int _i_Position = 0; _i_Position < _arl_Meldungen.Count; _i_Position++)
			{
				struct_Meldung pout_structMeldung_Meldung = (struct_Meldung) _arl_Meldungen[_i_Position];
				if (pout_structMeldung_Meldung.str_KnotenText == Knotentext)
					return pout_structMeldung_Meldung;
			}
			return new struct_Meldung(new DateTime(0,0,0,0,0,0), "", "");
		}
		
		// öffnet einen Dialog mit den Meldungsdaten
		private void OeffneMeldung(struct_Meldung pin_Meldung)
		{
			frm_MeldungsAnzeige _frm_Meldung = new frm_MeldungsAnzeige();
			//TODO: laden der Informationen in das Formular.USC...
			// füge das Formular zum Anzeigen von Terminen dem Oberformular hinzu
			this.FindForm().AddOwnedForm(_frm_Meldung);
			_frm_Meldung.ShowDialog();
		}


		
		// beschreibt die Informationen eines Auftrags, welche in der ToDo-Liste angezeigt werden
		public struct struct_Auftrag
		{
			private string _str_text;
			public string str_text
			{
				get { return _str_text;}
			}
			private DateTime _date_abfassungsdatum;
			public DateTime date_abfassungsdatum
			{
				get { return _date_abfassungsdatum;}
			}
			public string str_KnotenText
			{
				get 
				{
					return _date_abfassungsdatum.ToString() + " " + str_text;
				}
			}
			//TODO: hier muss der Typ festgelegt werden,
			//mit welchem der Verweis auf den eigentlichen Termin gespeichert wird
			// Schuppe
			private string _str_VerweisAufAuftrag;
			public string str_VerweisAufAuftrag
			{
				get { return _str_VerweisAufAuftrag;}
			}

			public struct_Auftrag(DateTime Abfassungsdatum, string Text, string VerweisAufAuftrag)
			{
				_date_abfassungsdatum = Abfassungsdatum;
				_str_text = Text;
				_str_VerweisAufAuftrag = VerweisAufAuftrag;
			}
		}

		// fügt eine Meldung hinzu
		public void FuegeAuftragHinzu(struct_Auftrag pin_Auftrag)
		{
			TreeNode _tnNeuerAuftrag  = new TreeNode(pin_Auftrag.str_KnotenText);
			_tn_Auftraege.Nodes.Add(_tnNeuerAuftrag);
			_arl_Auftraege.Add(pin_Auftrag);
		}

		// entfernt einen Auftrag
		public void EntferneAuftrag(struct_Auftrag pin_Auftrag)
		{
			// wandere durch alle Knotenelement
			for(int _i_Zaehler = 0; _i_Zaehler < _tn_Auftraege.Nodes.Count; _i_Zaehler++)
			{
				// vergleiche Knotentext 
				if (_tn_Auftraege.Nodes[_i_Zaehler].Text == pin_Auftrag.str_KnotenText)
				{
					_tn_Auftraege.Nodes.RemoveAt(_i_Zaehler);
					// beende Schleife
					break;
				}
			}
			_arl_Auftraege.Remove(pin_Auftrag);
		}

		// liefert Informationen zu einem Auftrag mit Hilfe des Knotentextes
		// falls kein entsprechender Auftrag gefunden wurde, wird ein leerer Auftrag zurückgegeben
		public struct_Auftrag GetAuftrag(string Knotentext)
		{
			for(int _i_Position = 0; _i_Position < _arl_Auftraege.Count; _i_Position++)
			{
				struct_Auftrag pout_structAuftrag_Auftrag = (struct_Auftrag) _arl_Auftraege[_i_Position];
				if (pout_structAuftrag_Auftrag.str_KnotenText == Knotentext)
					return pout_structAuftrag_Auftrag;
			}
			return new struct_Auftrag(new DateTime(0,0,0,0,0,0), "", "");
		}
		
		// öffnet einen Dialog mit den Meldungsdaten
		private void OeffneAuftrag(struct_Auftrag pin_Auftrag)
		{
			frm_AuftragsAnzeige _frm_Auftrag = new frm_AuftragsAnzeige();
			//TODO: laden der Informationen in das Formular.USC...
			// füge das Formular zum Anzeigen von Terminen dem Oberformular hinzu
			this.FindForm().AddOwnedForm(_frm_Auftrag);
			_frm_Auftrag.ShowDialog();
		}


		#endregion
		
		private void ErzeugeBeispielwerte()
		{
			struct_Termin _termin;
			_termin = new struct_Termin(new DateTime(2004, 12, 22, 10, 0, 0), "Kaffee kochen", "Termin1");
			FuegeTerminHinzu(_termin);
			_termin = new struct_Termin(new DateTime(2004, 12, 24, 11, 0, 0), "Weihnachtsbaum schmücken", "Termin1");
			FuegeTerminHinzu(_termin);

			struct_Meldung _meldung; 
			_meldung = new struct_Meldung(new DateTime(2004, 10, 22, 09, 12, 54), "Alles ok im Keller", "Meldung1");
			FuegeMeldungHinzu(_meldung);

			struct_Auftrag _auftrag;
			_auftrag = new struct_Auftrag(new DateTime(2004, 10, 22, 08, 17, 17), "Überprüfe Keller", "Auftrag1");
			FuegeAuftragHinzu(_auftrag);
			_auftrag = new struct_Auftrag(new DateTime(2004, 10, 22, 08, 19, 33), "Überprüfe Dachgeschoss", "Auftrag1");
			FuegeAuftragHinzu(_auftrag);
		}

		// öffnet das ausgewählte Element
		// außer es handelt sich um die Root-Elemente 'Termine 'Aufträge' oder 'Meldungen'
		private void _trv_ToDoListe_DoubleClick(object sender, System.EventArgs e)
		{
			// suche den Knoten, der aktuell ausgewählt ist
			for(int _i_Zaehler = 0; _i_Zaehler < _tn_Termine.Nodes.Count; _i_Zaehler++)
			{
				if (_tn_Termine.Nodes[_i_Zaehler].IsSelected)
				{
					// wenn gefunden, dann zeige detaillierte Informationen an
					OeffneTermin(GetTermin(_tn_Termine.Nodes[_i_Zaehler].Text));
				}
			}
			// suche den Knoten, der aktuell ausgewählt ist
			for(int _i_Zaehler = 0; _i_Zaehler < _tn_Meldungen.Nodes.Count; _i_Zaehler++)
			{
				if (_tn_Meldungen.Nodes[_i_Zaehler].IsSelected)
				{
					// wenn gefunden, dann zeige detaillierte Informationen an
					OeffneMeldung(GetMeldung(_tn_Meldungen.Nodes[_i_Zaehler].Text));
				}
			}
			// suche den Knoten, der aktuell ausgewählt ist
			for(int _i_Zaehler = 0; _i_Zaehler < _tn_Auftraege.Nodes.Count; _i_Zaehler++)
			{
				if (_tn_Auftraege.Nodes[_i_Zaehler].IsSelected)
				{
					// wenn gefunden, dann zeige detaillierte Informationen an
					OeffneAuftrag(GetAuftrag(_tn_Auftraege.Nodes[_i_Zaehler].Text));
				}
			}
		}


	}
}
