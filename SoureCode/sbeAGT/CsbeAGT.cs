using System;
using System.Drawing;
using System.Windows.Forms;


namespace pELS.GUI.AGT
{
	/// <summary>
	/// Implementation von alexG
	/// </summary>
public class CsbeAGT :  pELS.GUI.Interface.Isbe
{
#region Klassenvariablen
	
//Hält den Namen der Icon Datei fest
private string _strIconName = "agt.JPG";

//Hier wird die Beschriftung unterhalb des Icons festgehalten
private string _strSbeName = "AGT Einsatz";

//hier wird die Klassenvariable gehalten, die die User Control enthält
private usc_AGT _usc_AGT = new usc_AGT();

//dateiname des AGT-Inaktiv-Textes
private string	_strAGTInaktiv = "agti.loog";
#endregion

#region Konstruktoren & Destruktoren
	public CsbeAGT()
	{
		this._usc_AGT.SetzeRollenRechte(pELS.Rollensystem.i_aktuelleRolle);
		
	}	
	#endregion
			
#region Funktionalität
	/// <summary>
	/// public void LadeAGTInaktivText()
	/// Sollte eigentlich im Konstruktor von CsbeMAT geladen werden.
	/// Leider ist der Konstruktor nicht ausführbar, wenn das da drin steht
	/// </summary>
	
	public void LadeAGTInaktivText()
	{
		//Einmaliges laden der RichTextBox für den inaktiven AGToperator
		try
		{
			System.IO.FileStream myfile = System.IO.File.OpenRead(_strAGTInaktiv);		
		}
		catch (System.IO.FileLoadException ex)
		{
			MessageBox.Show(_strAGTInaktiv +"konnte nicht gelesen werden"
				+"\n sbeAGTKonstruktor\n"
				+ex.Message);
		}	
	}
	#endregion
	
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
		_usc_AGT.SetzeRollenRechte(pin_i_aktuelleRolle);
	}


	// TODO: Sichern alle Eingaben veranlassen und wenn alles erfolgreich true zurückgeben, sonst false
	public bool CloseSbeUserControl()
	{
		return true;
	}

	public System.Windows.Forms.UserControl GetSbeUserControl()
	{
			return this._usc_AGT;
	}

#endregion
}
}
