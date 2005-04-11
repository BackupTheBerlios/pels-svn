using System;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace pELS.Tools
{
	/// <summary>
	/// erlaubt den einfachen Zugriff auf XML-dateien
	/// </summary>
	public class XMLZugriff
	{
		private string _str_XMLDateiName = "datei.xml";
		private XmlDocument _XMLDatei = new XmlDocument();
		private XmlNodeList _KnotenListe = null;		
		

		
		public XMLZugriff()
		{

		}

		
		/// <summary>
		/// lädt die angegebene XML-Datei
		/// </summary>
		/// <param name="pin_Filename"></param>
		/// <returns>gibt bei einem Fehler zurück ""</returns>
		public string LadeDatei(string pin_Dateiname)
		{	
			this._str_XMLDateiName = pin_Dateiname;
			XmlTextReader reader =null;
			try
			{
				reader = new XmlTextReader(pin_Dateiname);
				_XMLDatei.Load(reader);
				reader.Close();											
				return "";
			}
			catch (XmlException e)
			{
				if(reader != null)
					reader.Close();				
				ErstelleStandardConfigDatei(pin_Dateiname);
				return LadeDatei(pin_Dateiname);
			}
			catch (System.IO.FileNotFoundException ex)
			{
				ErstelleStandardConfigDatei(pin_Dateiname);
				return LadeDatei(pin_Dateiname);
			}
			catch (Exception ex)
			{
				if(reader != null)
					reader.Close();
				return ex.Message;
			}

		}

		/// <summary>
		/// Erstellt eine Standard pELS-Config Datei die sowohl für
		/// pELS-Server als auch für pELS-Client geeignet ist.
		/// </summary>
		/// <param name="pin_Dateiname">Name der zu erstellenden Datei</param>
		private void ErstelleStandardConfigDatei(string pin_Dateiname)
		{
			string str_standard_XML_config;
			#region Belegen der str_standard_XML_config
			str_standard_XML_config = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
			str_standard_XML_config += "\n<pELS>"
				+   "\n<pELS-Server>"
				+	"\n <Address Port=\"9001\"> </Address>"
				+  "\n <DBConfig UserID=\"unbekannt\" PW=\"unbekannt\" Host=\"127.0.0.1\" Port=\"5432\" DBName=\"template1\" Lifetime=\"0\"></DBConfig>"
				+  "\n</pELS-Server>"
				+  "\n<pELS-Client>"
				+	"\n<Address IP=\"127.0.0.1\" Port=\"9001\"> </Address>"
				+  "\n<Benutzer Name=\"iro\" ID=\"1833\">  </Benutzer>"
				+  "\n</pELS-Client>"
				+  "\n</pELS>";
			#endregion
			System.IO.StreamWriter ws =null;
			try
			{
				ws = new StreamWriter(pin_Dateiname,false);
				ws.Write(str_standard_XML_config);
				ws.Close();

			}
			catch (System.UnauthorizedAccessException ex)
			{	//Kein Zugriffsrecht für die Datei -> kann zum Abbruch des Programms führen
				if(ws != null)
					ws.Close();
				System.Windows.Forms.DialogResult dr_abbrechen;
				dr_abbrechen = System.Windows.Forms.MessageBox.Show("Die Datei "+pin_Dateiname+ " kann nicht geschrieben werden"
					+"\n Versichern Sie sich, dass Sie die Rechte dafür besitzen und starten Sie erneut"
					+"\n\n"+ex.Message, 
					"Schreibfehler: "+pin_Dateiname, 
					System.Windows.Forms.MessageBoxButtons.RetryCancel, 
					System.Windows.Forms.MessageBoxIcon.Error);

				if(dr_abbrechen == System.Windows.Forms.DialogResult.Retry)
				{
					//nochmal versuchen
					this.ErstelleStandardConfigDatei(pin_Dateiname);
				}
				else
				{
					Environment.Exit(3);
				}																						
			}
			catch (System.IO.IOException ex)
			{	
				if(ws != null)
					ws.Close();
				System.Windows.Forms.DialogResult dr_abbrechen;
				dr_abbrechen = System.Windows.Forms.MessageBox.Show("Zugriff auf "+pin_Dateiname+ " nicht möglich"					
																	+"\n\n"+ex.Message, 
																    "Schreibfehler: "+pin_Dateiname, 
																     System.Windows.Forms.MessageBoxButtons.RetryCancel, 
																	 System.Windows.Forms.MessageBoxIcon.Error);

				if(dr_abbrechen == System.Windows.Forms.DialogResult.Retry)
				{
					//nochmal versuchen
					this.ErstelleStandardConfigDatei(pin_Dateiname);
				}
				else
				{				
					Environment.Exit(3);
				}

			}
			catch (Exception ex)
			{ //Nicht betrachtete Sonstige Fehler
				if(ws != null)
					ws.Close();
				System.Windows.Forms.MessageBox.Show("Zugriff auf "+pin_Dateiname+ " nicht möglich"											
					+"\n\n"+ex.Message, 
					"Schreibfehler: "+pin_Dateiname, 
					System.Windows.Forms.MessageBoxButtons.RetryCancel, 
					System.Windows.Forms.MessageBoxIcon.Error);
				Environment.Exit(3);
			}



		}

		/// <summary>
		/// wählt eine Liste von Knoten
		/// </summary>
		/// <param name="pin_Path">
		/// definiert die Elemente welche zur Liste hinzugefügt werden
		/// </param>
		/// <returns>
		/// gibt TRUE zurück, wenn Elemente gefunden werden
		/// und FALSE, falls keine gefunden wurden
		/// </returns>
		public bool WaehleKnoten(string pin_Pfad)
		{
			try
			{
				_KnotenListe = _XMLDatei.SelectNodes(pin_Pfad);
				if ((_KnotenListe != null) && _KnotenListe.Count != 0 ) return true;
				else return false;

			}
			catch (XPathException)
			{
				return false;
			}
		}
		

		/// <summary>
		/// gibt die Anzahl der ausgewählten Knoten
		/// </summary>
		public int KnotenAnzahl
		{
			get
			{
				return _KnotenListe.Count;
			}
		}


		/// <summary>
		/// gibt die Anzahl der Attribute der ausgewählten Knoten
		/// </summary>
		public int AttributAnzahl
		{
			get
			{
				if (_KnotenListe.Count == 0) return 0;
				else return _KnotenListe[0].Attributes.Count;
			}
		}

		/// <summary>
		/// gibt den Wert eines Attributes eines Knotens
		/// </summary>
		/// <param name="pin_Knotennummer">Position des Knotens in der Liste</param>
		/// <param name="pin_AttributName"></param>
		/// <returns></returns>
		public string HoleKnotenAttribut(int pin_Knotennummer, string pin_AttributName)
		{
			XmlNode tmpNode = _KnotenListe.Item(pin_Knotennummer);
			if (tmpNode != null)
			{
				XmlAttribute tmpAttr = tmpNode.Attributes[pin_AttributName];
				if (tmpAttr != null) return tmpAttr.Value;
				else return "";
			}
			else return "";
		}


		/// <summary>
		/// gibt den Wert eines Attributes eines Knotens
		/// </summary>
		/// <param name="pin_Knotennummer">Position des Knotens in der Liste</param>
		/// <param name="pin_AttributNummer">Position des Attributs in der Liste der ausgewählten Knoten</param>
		/// <returns></returns>
		public string HoleKnotenAttribut(int pin_Knotennummer, int pin_AttributNummer)
		{
			XmlNode tmpNode = _KnotenListe.Item(pin_Knotennummer);
			if (tmpNode != null)
			{
				if (pin_AttributNummer < tmpNode.Attributes.Count)
				{
					XmlAttribute tmpAttr = tmpNode.Attributes[pin_AttributNummer];
					if (tmpAttr != null) return tmpAttr.Value;
					else return "";
				}
				else return "";
			}
			else return "";
		}


		public string SpeichereDatei(string pin_Dateiname)
		{	
			try
			{
				StreamWriter sw = new StreamWriter(pin_Dateiname);
				_XMLDatei.Save(sw);
				sw.Close();
				return "";
			}
			catch (XmlException e)
			{
				return e.ToString();
			}
			catch (UnauthorizedAccessException ex)
			{
				//Tritt immer dann auf, wenn die datei shcreibgeschützt ist
				return ex.ToString();
			}
		}

		public string SpeichereDatei()
		{	
			return this.SpeichereDatei(_str_XMLDateiName);
		}


		public string SetzeKnotenAttribut(int pin_Knotennummer, string pin_AttributName, string pin_Wert)
		{
			XmlNode tmpNode = _KnotenListe.Item(pin_Knotennummer);
			if (tmpNode != null)
			{
				XmlAttribute tmpAttr = tmpNode.Attributes[pin_AttributName];
				if (tmpAttr != null)
				{
					tmpAttr.Value = pin_Wert;
				}
				else 
				{
					XmlElement tmpXmlElement = (XmlElement) tmpNode;
					tmpXmlElement.SetAttribute(pin_AttributName, pin_Wert);
				}
				this.SpeichereDatei();
				return "";
			}
			else return "Knoten nicht gefunden";
		}

	}
}
