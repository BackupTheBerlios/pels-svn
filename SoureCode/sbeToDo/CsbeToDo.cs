using System;
using System.Drawing;


namespace pELS.GUI.ToDo
{
	/// <summary>
	/// Summary description for CsbeMAT.
	/// Ist erste Implementation (Referenz) zu Isbe-Interface
	/// </summary>
	public class CsbeToDo :  pELS.GUI.Interface.Isbe
	{

		//Hält den Namen der Icon Datei fest
		private string _strIconName = "icon.jpg";
		//Hier wird die Beschriftung unterhalb des Icons festgehalten
		private string _strSbeName = "ToDo Liste";
		//hier wird die Klassenvariable gehalten, die die User Control enthält
		private usc_TODO _usc_ToDo = new usc_TODO();
	

		public CsbeToDo()
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
			// TODO: Muss noch implementiert werden
			//_usc_ToDo.SetzeRollenRechte(pin_i_aktuelleRolle);
		}



		public System.Windows.Forms.UserControl GetSbeUserControl()
		{
			
			return this._usc_ToDo;
		}

		// TODO: Sichern alle Eingaben veranlassen und wenn alles erfolgreich true zurückgeben, sonst false
		public bool CloseSbeUserControl()
		{
			return true;
		}
		#endregion
	}
}
