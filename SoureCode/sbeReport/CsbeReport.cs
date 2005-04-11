using System;
using System.Drawing;


namespace pELS.GUI.Report
{
	/// <summary>
	/// Summary description for CsbeReport.
	/// Ist erste Implementation (Referenz) zu Isbe-Interface
	/// </summary>
	public class CsbeReport :  pELS.GUI.Interface.Isbe
	{

		//Hält den Namen der Icon Datei fest
		private string _str_iconName = "icon.jpg";
		//Hier wird die Beschriftung unterhalb des Icons festgehalten
		private string _str_sbeName = "Reports";
		//hier wird die Klassenvariable gehalten, die die User Control enthält
		private usc_Report _usc_Report = new usc_Report();
	

		public CsbeReport()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Isbe Members

		public Image GetSbeImage()
		{
			System.Reflection.Assembly asm_sbe;
			//Informationen über die ausführende Assembly sammeln
			asm_sbe = System.Reflection.Assembly.GetExecutingAssembly();
			//Liefere Name der Assembly als AssemblyName
			System.Reflection.AssemblyName asm_sbeName = asm_sbe.GetName();
			//Speichere den dll Namen im String
			string strAssemblyName = asm_sbeName.Name;
			//Erstelle ein Stream, aus dem die Icon Daten gelesen werden
			System.IO.Stream s = asm_sbe.GetManifestResourceStream(strAssemblyName + "." + _str_iconName);
			//Lese die Icon Daten aus dem Stream
			Image im_bild = Image.FromStream(s);
			//Gebe myImage zurück
			return(im_bild);
		}

		public String GetSbeName()
		{			
			return this._str_sbeName;
		}

		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
		{
			_usc_Report.SetzeRollenRechte(pin_i_aktuelleRolle);
		}



		public System.Windows.Forms.UserControl GetSbeUserControl()
		{
			
			return this._usc_Report;
		}
		
		// TODO: Sichern alle Eingaben veranlassen und wenn alles erfolgreich true zurückgeben, sonst false
		public bool CloseSbeUserControl()
		{
			return true;
		}
		#endregion
	}
}
