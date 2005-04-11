using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pELS.Client.Funk
{
	/// <summary>
	/// Summary description for usc_Meldung_Verschicken.
	/// </summary>
	public class usc_Meldung_Verschicken : usc_Meldung
	{

		/// <summary>
		///  beinhaltet den Zustand des zuletzt gecheckten Knoten aus dem EmpfängerTreeView
		/// </summary>
		private bool LetzterTVKnotenZustand;

		public usc_Meldung_Verschicken(Cst_Funk pin_Cst_Funk) : base(pin_Cst_Funk)
		{
			SetzeTreeView();
		}

		override protected void SetzeGUIElemente()
		{
			this.gbx_Meldungstyp.Enabled = false;
			this.gbx_Abfassung.Enabled = false;
			this.cbx_IstUebermittelt.Enabled = true;
			this.cbx_IstUebermittelt.Checked = true;
			this.gbx_Erkundung.Enabled = false;
			this.btn_Drucken.Visible = true;
			this.cmb_Benutzerempfaenger.Enabled = false;
			this.txt_Meldungstext.ReadOnly = true;
			this.cbx_IstUebermittelt.Checked = true;

			ep_Eingabe.SetIconAlignment(cmb_Meldungskategorie, ErrorIconAlignment.MiddleLeft);
			ep_Eingabe.SetIconAlignment(txt_Absender, ErrorIconAlignment.MiddleLeft);
			ep_Eingabe.SetIconAlignment(cmb_Uebermittlungsart, ErrorIconAlignment.MiddleLeft);
			ep_Eingabe.SetIconAlignment(cbx_IstUebermittelt, ErrorIconAlignment.MiddleRight);
			ep_Eingabe.SetIconAlignment(txt_Meldungstext, ErrorIconAlignment.MiddleRight);
			ep_Eingabe.SetIconAlignment(tvw_Empfaenger, ErrorIconAlignment.MiddleRight);
			ep_Eingabe.SetIconAlignment(txt_Erkunder, ErrorIconAlignment.MiddleRight);
			ep_Eingabe.SetIconAlignment(txt_Erkundungsobjekt, ErrorIconAlignment.MiddleRight);
		}

		/// <summary>
		/// setzt alle Eingabefelder zurück in den Ausgangszustand
		/// </summary>
		override public void Zuruecksetzen()
		{
			// Übermittlung
			this.cmb_Uebermittlungsart.SelectedIndex = 0;
			this.cbx_IstUebermittelt.Checked = false;
			this.cbx_UebermittlungszeitpunktJetzt.Checked = false;
			this.dtp_Uebermittlungsdatum.Value = DateTime.Now;
		}

		private void SetzeTreeView()
		{
			this.tvw_Empfaenger.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvw_MeldungsEmpfaenger_AfterCheck);
			this.tvw_Empfaenger.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvw_MeldungsEmpfaenger_BeforeCheck);
		}

		/// <summary>
		/// löst das Speichern eines Auftrags aus, und leert aller Eingabefelder
		/// Die Überschreibung ist nötig, damit hier die Basismethode Zurueckstzen() aufgerufen wird
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		override protected void btn_Speichern_Click(object sender, System.EventArgs e)
		{
			if (b_FelderModifiziert)
			{
				if (b_FelderModifiziert)
					if (Eingabevalidierung())
					{
						SpeichereMeldung();
						base.Zuruecksetzen();		
					}
					else
					{
						pELS.GUI.PopUp.CPopUp.MeldenVonFalscherEingabe();
					}
			}
		}
		/// <summary>
		///  setzt den letzten Zustand von Checked eines TreeView-Knotens falls 
		///  EmpfaengerTreeViewModifizierbar == false
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvw_MeldungsEmpfaenger_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
				if(e.Action != TreeViewAction.Unknown)
				{
					e.Node.Checked = LetzterTVKnotenZustand;
				}
		}

		/// <summary>
		///  speichert den letzten Zustand von Checked eines TreeView-Knotens falls 
		///  EmpfaengerTreeViewModifizierbar == false
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvw_MeldungsEmpfaenger_BeforeCheck(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
				if(e.Action != TreeViewAction.Unknown)
				{
					LetzterTVKnotenZustand = e.Node.Checked;
				}
		}


	}
}
