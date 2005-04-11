using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pELS.Client.Funk
{
	/// <summary>
	/// Summary description for usc_Verschicken.
	/// </summary>
	public class usc_Auftrag_Verschicken : usc_Auftrag
	{

		/// <summary>
		///  beinhaltet den Zustand des zuletzt gecheckten Knoten aus dem EmpfängerTreeView
		/// </summary>
		private bool LetzterTVKnotenZustand;


		public usc_Auftrag_Verschicken(Cst_Funk pin_Cst_Funk) : base(pin_Cst_Funk)
		{
			SetzeTreeView();
		}

		override protected void SetzeGUIElemente()
		{
			this.btn_AuftragDrucken.Visible = true;
			this.gbx_Abfassung.Enabled = false;
			this.gbx_Ausfuehrung.Enabled = false;
			this.cbx_IstUebermittelt.Enabled = true;
			this.cbx_IstUebermittelt.Checked = true;
			this.txt_Auftragstext.Enabled = false;
			this.cmb_Benutzerempfaenger.Enabled = false;
			this.cbx_IstUebermittelt.Visible = true;
			this.cbx_IstUebermittelt.Checked = true;
			this.cmb_Befehlsart.Enabled = false;
			this.cbx_AbfassungsdatumJetzt.Visible = false;
			this.cbx_AusfuehrungszeitpunktJetzt.Visible = false;
			this.cbx_spaetesterErfuellungszeitpunktJetzt.Visible = false;
			this.cbx_nachverfolgen.Enabled = false;

			ep_Eingabe.SetIconAlignment(cmb_Befehlsart, ErrorIconAlignment.MiddleLeft);
			ep_Eingabe.SetIconAlignment(txt_Absender, ErrorIconAlignment.MiddleLeft);
			ep_Eingabe.SetIconAlignment(cmb_Uebermittlungsart, ErrorIconAlignment.MiddleLeft);
			ep_Eingabe.SetIconAlignment(cbx_IstUebermittelt, ErrorIconAlignment.MiddleRight);
			ep_Eingabe.SetIconAlignment(txt_Auftragstext, ErrorIconAlignment.MiddleRight);
			ep_Eingabe.SetIconAlignment(tvw_AuftragsEmpfaenger, ErrorIconAlignment.MiddleRight);
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

		/// <summary>
		/// setze die eventHandler für den TreeView so, dass keine Häkchen verändert werden können
		/// </summary>
		private void SetzeTreeView()
		{
			this.tvw_AuftragsEmpfaenger.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvw_AuftragsEmpfaenger_AfterCheck);
			this.tvw_AuftragsEmpfaenger.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvw_AuftragsEmpfaenger_BeforeCheck);

		}
		#region events
		/// <summary>
		/// löst das Speichern eines Auftrags aus, und leert aller Eingabefelder
		/// Die Überschreibung ist nötig, damit hier die Basismethode Zurueckstzen() aufgerufen wird
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		override protected void btn_AuftragSpeichern_Click(object sender, System.EventArgs e)
		{
			if (b_FelderModifiziert)
				if (Eingabevalidierung())
				{
					SpeichereAuftrag();
					base.Zuruecksetzen();
				}
				else
				{
					pELS.GUI.PopUp.CPopUp.MeldenVonFalscherEingabe();
				}
		}
		
		/// <summary>
		///  setzt den letzten Zustand von Checked eines TreeView-Knotens falls 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvw_AuftragsEmpfaenger_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
				if(e.Action != TreeViewAction.Unknown)
				{
					e.Node.Checked = LetzterTVKnotenZustand;
				}
		}

		/// <summary>
		///  speichert den letzten Zustand von Checked eines TreeView-Knotens falls 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvw_AuftragsEmpfaenger_BeforeCheck(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
				if(e.Action != TreeViewAction.Unknown)
				{
					LetzterTVKnotenZustand = e.Node.Checked;
				}
		}

		#endregion
	}
}