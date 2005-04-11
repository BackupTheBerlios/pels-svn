/*
using System;
using System.Drawing;

namespace pELS.Client.EK
{
	/// <summary>
	/// Summary description for CsbeEK.
	/// </summary>
	public class CsbeEK:  pELS.GUI.Interface.Isbe
	{ 
		//Hält den Namen der Icon Datei fest
		private string _strIconName = "icon.jpg";
		//Hier wird die Beschriftung unterhalb des Icons festgehalten
		private string _strSbeName = "Einsatzkräfte";
		//hier wird die Klassenvariable gehalten, die die User Control enthält
		private Cpr_usc_EK _usc_EK = new Cpr_usc_EK();
		
		public CsbeEK()
		{
			//
			// TODO: Add constructor logic here
			//
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
			_usc_EK.SetzeRollenRechte(pin_i_aktuelleRolle);
		}



		public System.Windows.Forms.UserControl GetSbeUserControl()
		{
			
			return this._usc_EK;
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