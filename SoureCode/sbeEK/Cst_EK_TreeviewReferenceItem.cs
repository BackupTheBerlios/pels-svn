using System;

namespace pELS.Client.EK
{
	/// <summary>
	/// Summary description for Cst_EK_TreeviewTag.
	/// </summary>
	public class Cst_EK_TreeviewReferenceItem
	{
		#region Konstruktur
		public Cst_EK_TreeviewReferenceItem()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public Cst_EK_TreeviewReferenceItem(int ID, System.Windows.Forms.TreeNode TreeNode)
		{
			this.PelsObjectID=ID;
			this.TreeNodeReferenziert=TreeNode;
		}
		#endregion

		#region Properties
		public int PelsObjectID;
		public System.Windows.Forms.TreeNode TreeNodeReferenziert;
		#endregion

	 
	}
}
