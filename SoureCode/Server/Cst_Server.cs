using System;
using System.Collections;
using System.Windows.Forms;
// benötigt für: dns
using System.Net;
	// benötigt für: Exportieren des Datenbestand
using System.IO;
// benötigit für: pels_Objekte
using pELS.DV.Server.Interfaces;
using pELS.APS.Server.Interface;
using PortalLogik_ToDo;

namespace pELS.Server
{
	using pELS.DV;

	/// <summary>
	/// STS des Servers
	/// </summary>
	public class Cst_Server
	{
		private Cpr_frm_Server _Cpr_Server;
		private Cdv_Einsatz _Einsatz = null;		
		/// <summary>
		///  gibt die aktuellen Einsatzdaten zurück
		/// </summary>
		public Cdv_Einsatz Einsatz
		{
			get 
			{
				if (_Einsatz != null) return _Einsatz;
				else
				{
					_Einsatz = _Cap_Server.LadeEinsatzdaten();
					return _Einsatz;
				}
			}
		}

		private Cap_Server _Cap_Server = null;

		/// <summary>
		/// startet das PRS-Objekt
		/// </summary>
		/// <param name="pin_Cap_Server"></param>
		public Cst_Server(ref Cap_Server pin_Cap_Server)
		{
			_Cap_Server = pin_Cap_Server;
			_Cap_Server.StarteServerRoutine();
			_Cpr_Server = new Cpr_frm_Server(this);
			Application.Run(_Cpr_Server);
		}

		/// <summary>
		/// Überprüft den Backupprozess
		/// </summary>
		private void UeberpruefeBeendetenProzess(object obj, EventArgs e)
		{
			// Sperren wieder freigeben
			_Cpr_Server.EingabeSperre();
			// Überprüfen des Exitcodes, ob Erfolgreich abgeschlossen
			switch (_Cap_Server.BackupProzess.ExitCode)
			{
				case 0: 
				{
					MessageBox.Show("Backup/Restore erfolgreich erstellt.");
					break;
				}
				case 1:
				{
					MessageBox.Show("Fehler im Datenbankmanagementsystem. Überprüfen sie die Datenbankverbindung und versuchen sie es erneut.");
					break;
				}
				case -1073741510:
				{
					MessageBox.Show("Abbruch durch Benutzer");
					break;
				}
				default:
				{
					MessageBox.Show("Backup/Restore konnte aufgrund eines unbekannten Fehlers nicht durchgeführt werden");
					break;
				}
			}
		}
		
		/// <summary>
		/// liest das ConfigFile aus
		/// </summary>
		/// <returns>Objekt mit der aktuellen Serverkonfiguration zurück</returns>
		public Cdv_Serverkonfiguration LadeServerkonfiguration()
		{
			return _Cap_Server.LadeServerkonfiguration();
		}

		/// <summary>
		/// speichert die Serverkonfiguration im ConfigFile
		/// </summary>
		/// <param name="pin_Serverkonfiguration"></param>
		/// <returns></returns>
		public bool SpeichereServerkonfiguration(Cdv_Serverkonfiguration pin_Serverkonfiguration)
		{
			return _Cap_Server.SpeichereServerkonfiguration(pin_Serverkonfiguration);
		}

		/// <summary>
		/// speichert die Einsatzdaten
		/// </summary>
		/// <param name="pin_Einsatz"></param>
		/// <returns></returns>
		public IPelsObject SpeichereEinsatzdaten(Cdv_Einsatz pin_Einsatz)
		{
			return _Cap_Server.SpeichereEinsatzdaten(pin_Einsatz);
		}

		/// <summary>
		/// stößt das Exportieren der Einsatzdaten an
		/// </summary>
		/// <param name="pin_FileName">Name der Exportdatei</param>
		/// <returns></returns>
		public bool ExportiereDatenbestand(String pin_FileName)
		{
			if (pin_FileName.EndsWith("csv")) _Cpr_Server.EingabeSperre();
			bool gestartet = _Cap_Server.ExportiereDatenbestand(pin_FileName);
			if (_Cap_Server.BackupProzess != null) _Cap_Server.BackupProzess.Exited += new EventHandler(UeberpruefeBeendetenProzess);
			return gestartet;
		}

		/// <summary>
		/// stößt das Importieren der Einsatzdaten an
		/// </summary>
		/// <param name="pin_FileName">Name der Importdatei</param>
		/// <returns></returns>
		public bool ImportiereDatenbestand(string pin_FileName)
		{
			bool gestartet = _Cap_Server.ImportiereDatenbestand(pin_FileName);
			_Cap_Server.BackupProzess.Exited += new EventHandler(UeberpruefeBeendetenProzess);
			return gestartet;
		}

		/// <summary>
		/// stößt das Starten der Serverroutine an
		/// </summary>
		public void StarteServerRoutine()
		{
			// beende Serverroutine, falls schon eine gestartet wurde
			_Cap_Server.BeendeServerRoutine();
			// starte Serverroutine
			_Cap_Server.StarteServerRoutine();
		}

		/// <summary>
		/// stößt das Beenden der Serverroutine an
		/// </summary>
		public void Beenden()
		{
			_Cap_Server.BeendeServerRoutine();
			_Cap_Server.Beenden();
		}

		/// <summary>
		/// holt die eigenen IP-Addressen aus allen verfügbaren Netzen
		/// </summary>
		/// <returns>stringArray mit IPs</returns>
		public static string[] HoleIPAddressListe()
		{
			// lese alle verfügbaren Netzwerkaddressen
			ArrayList _arl_tmp = new ArrayList();
			foreach(IPAddress ip in Dns.GetHostByName(Dns.GetHostName()).AddressList)
			{
				_arl_tmp.Add(Convert.ToString(ip));
			}
			// nach string[] kopieren
			int _Laenge = _arl_tmp.Count;
			string[] pout_StringList = new string[_Laenge];
			for (int i = 0; i < _Laenge; i++)
			{
				pout_StringList[i] = (string)_arl_tmp[i];
			}
			// ausgeben
			return pout_StringList;
			
		}

		public void neuerEinsatz(string pin_name)
		{
			_Cap_Server.ErstelleNeuePelsDatenbank(pin_name);
			
			pELS.Tools.RegistryZugriff myRegistryZugriff = new pELS.Tools.RegistryZugriff(pELS.Tools.Server.CKonstanten._str_DefaultRegistryDatei, pELS.Tools.Server.CKonstanten._str_AktuelleRegistryDatei);
			myRegistryZugriff.AktualisiereOdbcKonfiguration(pin_name);
			
			this._Einsatz = new Cdv_Einsatz();
			this.Einsatz.Bezeichnung = "Bitte eintragen";
			this.Einsatz.Einsatzort = "Bitte eintragen";		
			this.Einsatz.Beschreibung.Text = "Bitte eintragen";
		}
	}
}
