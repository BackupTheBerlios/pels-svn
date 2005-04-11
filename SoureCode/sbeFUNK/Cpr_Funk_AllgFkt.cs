using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using pELS.DV;


namespace pELS.Client.Funk
{
	/// <summary>
	/// Zusammenfassung f�r Cpr_Funk_AllgFkt.
	/// </summary>
	public class Cpr_Funk_AllgFkt
	{
		public Cpr_Funk_AllgFkt()
		{
			//
			// TODO: F�gen Sie hier die Konstruktorlogik hinzu
			//
		}

		public static ArrayList HoleAlleAusgewaehltenEmpfaengerIDs(TreeNodeCollection pin_TreeNode)
		{
			// neue leere Arraylist
			ArrayList pout_AL = new ArrayList();
			// gehe durch alle enthaltenen Knoten
			if (pin_TreeNode.Count != 0)
			{
				foreach(TreeNode tn in pin_TreeNode)
				{
					// gehe durch alle in diesem enthaltenen Knoten Knoten
					if (tn.Nodes != null)
					{
						ArrayList tmp = HoleAlleAusgewaehltenEmpfaengerIDs(tn.Nodes);
						if (tmp != null)
						{
							// f�ge R�ckgabewerte der aktuellen ArrayList hinzu
							pout_AL.AddRange(tmp);
						}
					}
					// pr�fe, ob ein Tag-Value existiert
					if (tn.Tag != null)
						// pr�fe, ob das Element ausgew�hlt wurde
						if(tn.Checked)
						{
							// hole die PelsObject.ID und speichere diese in der ArrayList
							pout_AL.Add(((Cdv_pELSObject) tn.Tag).ID);
						}
				}
			}
			else
			{
			}
			return pout_AL;
		}


		/// <summary>
		/// setzt H�kchen bei allen Elementen deren ID in der �bergebenen
		/// ID-Menge enthalten ist
		/// </summary>
		/// <param name="pin_TreeNode"></param>
		/// <param name="pin_IDMenge"></param>
		public static void SetzeAlleAusgewaehltenEmpfaenger(
			TreeNodeCollection pin_TreeNode, int[] pin_IDMenge)
		{
			if (pin_IDMenge != null)
				// gehe durch alle enthaltenen Knoten
				if (pin_TreeNode.Count != 0)
				{
					foreach(TreeNode tn in pin_TreeNode)
					{
						tn.Checked = false;
						// gehe durch alle in diesem enthaltenen Knoten Knoten
						if (tn.Nodes != null)
						{
							SetzeAlleAusgewaehltenEmpfaenger(tn.Nodes, pin_IDMenge);
						}
						// pr�fe, ob ein Tag-Value existiert
						if (tn.Tag != null)
							// pr�fe, ob das Element ausgew�hlt wurde
							foreach(int ID in pin_IDMenge)
							{
								if (((Cdv_pELSObject)tn.Tag).ID == ID)
								{
									tn.Checked = true;
								}
							}
					}
				}
		}
	












	}
}
