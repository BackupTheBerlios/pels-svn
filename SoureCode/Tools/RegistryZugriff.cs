using System;
using System.IO;

namespace pELS.Tools
{
	/// <summary>
	/// Diese Klasse ermöglicht den Zugriff auf die Registry
	/// </summary>
	public class RegistryZugriff
	{
		private string _str_alteRegistry = String.Empty;
		private string _str_neueRegistry = String.Empty;

		public RegistryZugriff(string pin_default, string pin_zuaendern)
		{
			_str_alteRegistry = pin_default;
			_str_neueRegistry = pin_zuaendern;
		}

		/// <summary>
		/// Aktualisiert den Namen der Datenbank in der Registry
		/// </summary>
		/// <param name="pin_db">Name der neuen Datenbank</param>
		/// <returns>Bool ob die Änderung erfolgreich war</returns>
		public bool AktualisiereOdbcKonfiguration(string pin_db)
		{
			// Streams aus zum bearbeiten der Dateien
			StreamReader _alteRegistry = new StreamReader(_str_alteRegistry);
			StreamWriter _neueRegistry = new StreamWriter(_str_neueRegistry, false);

			// Zeilenweise die Default Registry auslesen und Änderungen durchführen
			string zeile = String.Empty;
			while ((zeile = _alteRegistry.ReadLine()) != null)
			{
				if (zeile.StartsWith("\"Database\""))
					_neueRegistry.WriteLine("\"Database\"=\"" + pin_db + "\"");
				else
					_neueRegistry.WriteLine(zeile);
			}

			// Schließe Streams
			_alteRegistry.Close();
			_neueRegistry.Close();

			return AktualisiereRegistry();
		}	
		
		/// <summary>
		/// Aktualisiert die Servrekonfiguration in der Registry
		/// </summary>
		public bool AktualisiereOdbcKonfiguration(string pin_db, string pin_server, string pin_port, string pin_benutzer, string pin_passwort)
		{
			// Streams aus zum bearbeiten der Dateien
			StreamReader _alteRegistry = new StreamReader(_str_alteRegistry);
			StreamWriter _neueRegistry = new StreamWriter(_str_neueRegistry, false);

			// Zeilenweise die Default Registry auslesen und Änderungen durchführen
			string zeile = String.Empty;
			while ((zeile = _alteRegistry.ReadLine()) != null)
			{
				if (zeile.StartsWith("\"Servername\""))
					_neueRegistry.WriteLine("\"Servername\"=\"" + pin_server + "\"");
				else if (zeile.StartsWith("\"Username\""))
					_neueRegistry.WriteLine("\"Username\"=\"" + pin_benutzer + "\"");
				else if (zeile.StartsWith("\"Password\""))
					_neueRegistry.WriteLine("\"Password\"=\"" + pin_passwort + "\"");
				else if (zeile.StartsWith("\"Database\""))
					_neueRegistry.WriteLine("\"Database\"=\"" + pin_db + "\"");
				else if (zeile.StartsWith("\"Port\""))
					_neueRegistry.WriteLine("\"Port\"=\"" + pin_port + "\"");
				else
					_neueRegistry.WriteLine(zeile);
			}

			// Schließe Streams
			_alteRegistry.Close();
			_neueRegistry.Close();
			
			return AktualisiereRegistry();
		}
	
		/// <summary>
		/// Startet einen Prozess zur Aktualisierung der Registry
		/// </summary>
		public bool AktualisiereRegistry()
		{
			// Starte Prozess, der die Registrierung erweitert
			System.Diagnostics.Process myRegedit = new System.Diagnostics.Process();
			myRegedit.StartInfo.FileName = "regedit.exe";
			myRegedit.StartInfo.Arguments = "\"" + _str_neueRegistry + "\"";
			myRegedit.Start();
			myRegedit.WaitForExit();
			return true;
		}
	}
}
