using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace pELS.Client.EK
{
	/// <summary>
	/// Zusammenfassung für Cpr_EK_AllgFkt.
	/// </summary>
	public class Cpr_EK_AllgFkt
	{
		public Cpr_EK_AllgFkt()
		{
			//
			// TODO: Fügen Sie hier die Konstruktorlogik hinzu
			//
		}

		#region Datagrid

		public static DataColumn ErstellenEinerDataColumn(string pin_str_Name, string pin_str_Caption, string pin_str_Type) 
		{
			// Type der Spalte generieren
			System.Type type_meinType = Type.GetType(pin_str_Type);						
			if (type_meinType == null)
			{
				return null;
			}
			// Neue Spalte erstellen 
			DataColumn pout_dcol_Spalte = new DataColumn(pin_str_Name, type_meinType);
			pout_dcol_Spalte.ReadOnly = true; 
			pout_dcol_Spalte.ColumnName = pin_str_Caption;			
			
			return pout_dcol_Spalte;
		}

		// Tabelle für ein DataGrid erstellen
		public static DataTable ErstellenEinerDataTable(string pin_str_Name, DataColumn[] pin_dcol_a_Spalten) 
		{			
			DataTable pout_dtable_meineTabelle = new DataTable(pin_str_Name);

			pout_dtable_meineTabelle.Columns.AddRange(pin_dcol_a_Spalten);		
			return pout_dtable_meineTabelle;
		}

		#endregion

	}



}
