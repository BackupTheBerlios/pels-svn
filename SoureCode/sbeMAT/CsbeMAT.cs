/*using System;
using System.Drawing;
using pELS.Client;

using pELS.Tools.Client;

namespace pELS.GUI.MAT
{
	/// <summary>
	/// Summary description for CsbeMAT.
	/// Ist erste Implementation (Referenz) zu Isbe-Interface
	/// </summary>
	public class CsbeMAT :  pELS.GUI.Interface.Isbe
	{

		//Hält den Namen der Icon Datei fest
		private string _strIconName = "icon.jpg";
		//Hier wird die Beschriftung unterhalb des Icons festgehalten
		private string _strSbeName = "Aufträge";
		//hier wird die Klassenvariable gehalten, die die User Control enthält
		private usc_MAT _usc_Mat = null;
	

		public CsbeMAT(Cst_Einstellung pin_Einstellung)
		{
			this._usc_Mat = new usc_MAT(pin_Einstellung);
		}
		#region Isbe Members

		public Image GetSbeImage()
		{
			System.Reflection.Assembly asm_Sbe;
			//Informationen über die ausführende Assembly sammeln
			asm_Sbe = System.Reflection.Assembly.GetExecutingAssembly();
			//Liefere Name der Assembly als AssemblyName
			System.Reflection.AssemblyName asm_SbeName = asm_Sbe.GetName();
			//Speichere den dll Namen im String
			string strAssemblyName = asm_SbeName.Name;
			//Erstelle ein Stream, aus dem die Icon Daten gelesen werden
			System.IO.Stream s = asm_Sbe.GetManifestResourceStream(strAssemblyName + "." + _strIconName);
			//Lese die Icon Daten aus dem Stream
			Image myImage = Image.FromStream(s);
			//Gebe myImage zurück
			return(myImage);
		}

		public String GetSbeName()
		{			
			return this._strSbeName;
		}

		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
		{
			_usc_Mat.SetzeRollenRechte(pin_i_aktuelleRolle);
		}

		public System.Windows.Forms.UserControl GetSbeUserControl()
		{
			
			return this._usc_Mat;
		}

		// TODO: Sichern alle Eingaben veranlassen und wenn alles erfolgreich true zurückgeben, sonst false
		public bool CloseSbeUserControl()
		{
			return true;
		}
		#endregion
	}
}
*/