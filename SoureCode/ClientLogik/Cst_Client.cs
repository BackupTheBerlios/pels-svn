using System;
using System.Windows.Forms;
// ben�tigt f�r: Bild laden
using System.Drawing;
// ben�tigt f�r: ArrayList
using System.Collections;
// ben�tigt f�r: Laden von Assemblies
using System.Reflection;

using pELS;
// ben�tigt f�r: Cdv_XXX
using pELS.DV;
// ben�tigt f�r: die Verwaltung des aktuellen Benutzers
using pELS.Tools.Client;
// ben�tigt f�r: Holen aller Benutzer
using pELS.APS.Server.Interface;
// ben�tigt f�r: Allgemeine Funktionen
using pELS.Client.PortalLogik_allgFkt;
// ben�tigt f�r: Popups
using pELS.GUI.PopUp;
// ben�tigt f�r ISbe;
using pELS.GUI.Interface;
// ben�tigt f�r: Splash Screen
using pELS.Tools;


namespace pELS.Client
{
	#region Dokumentation
	/**
	Erl�uterung:	
	
	erstellt von:	Xiao		am: 16.Feb.2004
	weiter entwickelt von: H�tte am 03.03.05 und 04.03.05

	aktuelle Version: 0.2 alpha

	History/Hinweise/Bekannte Bugs:
	- TODO: die Eingabepr�fung, Mechanismus der Fehlerausgabe
	**/
	#endregion

	public class Cst_Client : System.Windows.Forms.Form, IReportRequested
	{
		#region Instanzvariablen
		 
		private static int _IndexOfReports = -1;
		// Liste zum Verwalten unserer Softwarebauelemente				
		private ArrayList _arl_softwarebauelemente = new ArrayList();

		// Verzeichnis indem sich die Softwarebauelemente befinden m�ssen
		private string  _str_sbeVerzeichnis = System.IO.Directory.GetCurrentDirectory()+@"\SBE";			
		private string _str_ConfigDateiName = "pELS-Client.config";
		
		// enth�lt den aktuellen Benutzer und Server IP, Port
		private Cst_Einstellung _Einstellung; 
		// die Cap_Klasse f�r Client
		// private IClient _apClient;
		// die GUI f�r Client
		private Cpr_Client _prClient = null;
		// enth�lt alle Systembenutzer
		private Cdv_Benutzer[] _benutzerMenge; 
		// enth�lt allgemeine Fkts, die von mehreren Portalen verwendet werden
		private Cst_allgFkt _st_allgFkt;
		//public pELS.Events.ReportRequestedEventHandler _ev_ReportRequestedEventHandler;
		//private event pELS.Tools.Client.ReportRequestedEventHandler myEvent;

		#endregion

		#region SETs, GETs
		public Cdv_Benutzer[] alleBenutzer
		{
			get{ return _benutzerMenge;}
		}
		public Cst_Einstellung Einstellung
		{
			get{ return (_Einstellung);}
			set{ _Einstellung = value; }
		}
		public string ConfigDateiName
		{
			get{return this._str_ConfigDateiName;}
		}
		#endregion

		#region Konstruktor
		/// <summary>
		/// 
		/// </summary>		
		public Cst_Client()
		{
			//_ev_ReportRequestedEventHandler = pELS.Events.ReportRequestedEventAdapter.Create(new pELS.Events.ReportRequestedEventHandler(BehandleReportRequestedEvent));
			this._Einstellung = new Cst_Einstellung();
			this._Einstellung.O_Cst_Client = this;

			#region Kanal auf der Client-Seite �ffnen
			// Creates a proxy for the well-known object 
			// indicated by the specified type and URL.
			try
			{
				IDictionary prop = new Hashtable();
				prop["name"] = "tcppELSClient";
				prop["port"] = 9099;
				System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(new 
					System.Runtime.Remoting.Channels.Tcp.TcpChannel(prop, null, null));
			}
			catch (Exception e)
			{
				throw e;
			}
			#endregion

			bool b_MitServerVerbunden = false;
			while (!b_MitServerVerbunden)
			{
				#region Serververbindungsdaten einlesen
				// Konfiguration der Verbindung zum Server
				Cpr_ClientServerVerbindung _prClientServerVerbindung = new Cpr_ClientServerVerbindung(this);
				Application.Run(_prClientServerVerbindung);
				// Bei Abbruch in Cpr_ClientServerVerbindung: Programm schlie�en
				if(_prClientServerVerbindung.anwendungSchliessen) Environment.Exit(0);
				#endregion		
				try
				{
					// hierbei wird zum ersten Mal eine Verbindung mit dem Server
					// aufgebaut. 
					_st_allgFkt = new Cst_allgFkt(_Einstellung);
					// ist dies erfolgreich, so wird normal mit dem Programmablauf fortgefahren
					b_MitServerVerbunden = true;
				}
				catch
				{
					//schl�gt dies fehl, dann lass den Benutzer nochmal die Daten eingeben
					CPopUp.KeineVerbindungZumServer();
				}
			}
			#region pr�fe allg. Vorraussetzungen
			// pr�fe serverseitige Vorraussetzungen
			if (!_st_allgFkt.PruefeAllgVorraussetzungen())
			{
				CPopUp.NichtAlleServervorrausstzungenErfuellt();
				Environment.Exit(0);
			}
			#endregion
			#region Benutzeranmeldung
			// Lade Benutzermenge aus DB
			this._benutzerMenge = this._st_allgFkt.HoleAlleBenutzer();
			// Anmeldungsfenster
			Cpr_ClientAnmeldung _prClientAnmeldung = new Cpr_ClientAnmeldung(this);
			

			// Das ClientAnmeldungsfenster funktioniert bei mir(xiao) immer
			// noch nicht. Einfachheit halber benutze ich den Pseudocode daunten.
			// Und zur Zeit da es noch keine L�sung dazu gibt, brauche ich diesen Code
			// zur Implementierung. 10.03.2005

			//this.ShowDialog();

			Application.DoEvents();
			//_prClientAnmeldung.ShowMe(this);
			_prClientAnmeldung.ShowDialog();	

			//MessageBox.Show(_prClientAnmeldung.Modal.ToString());
			// Zeige GUI bis Anmeldung ok, oder Abgebrochen wurde			
			while (!_prClientAnmeldung.ok)
				_prClientAnmeldung.ShowDialog();
						
			// Pruefe, ob bei ClientAnmeldung alles in Ordnung ging
			// wenn nicht, dann breche ab
			if(_prClientAnmeldung.anwendungSchliessen) Environment.Exit(0);

			// Das ClientAnmeldungsfenster funktioniert bei mir(xiao) immer
			// noch nicht. Einfachheit halber benutze ich den Pseudocode daunten.
			// Und zur Zeit da es noch keine L�sung dazu gibt, brauche ich diesen Code
			// zur Implementierung.10.03.2005

//			if(this._benutzerMenge.Length != 0)
//			{
//				Cdv_Benutzer tempBenutzer = this._benutzerMenge[0];	
//				MessageBox.Show("Benutzername:" + tempBenutzer.Benutzername+"\nSystemrolle:" + tempBenutzer.Systemrolle.ToString());
//				_Einstellung.Benutzer = tempBenutzer;
//			}
//			else
//			{
//				Cdv_Benutzer tempBenutzer = new Cdv_Benutzer("DefaultUser", Tdv_Systemrolle.LeiterF�St);
//				MessageBox.Show("Benutzername:" + tempBenutzer.Benutzername+"\nSystemrolle:" + tempBenutzer.Systemrolle.ToString());
//				_Einstellung.Benutzer = tempBenutzer;			
//			}
			#endregion

			// Client initialisieren
			_prClient = new Cpr_Client(this);
			_prClient.SetzeBenutzer(_Einstellung.Benutzer.Benutzername, _Einstellung.Benutzer.Systemrolle);
			// SBEs laden
			this.LadenAllerAssemblies();

			// zeige das erste SBE an
			if (_arl_softwarebauelemente.Count > 0)	StarteSBE(0);			

			// Alles ist fertig -> Anwendung starten
			Application.Run(_prClient);
			Environment.Exit(0);			
		}
		#endregion		

		#region IReportRequested members
		/// <summary>
		/// Behandelt das Event ReportRequested welches in ToolsClient\Cst_Einstellungen.cs deklariert und
		/// von MAT und Funk gefeuert wird
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="pin_mitteilung"></param>
		public void BehandleReportRequestedEvent(object pin_mitteilung)
		{			
			// SBE Reports anzeigen
			StarteSBE(_IndexOfReports);	
			// Eine Funktion im SBE Reports aufrufen								
			((pELS.Client.IReportRequested)_arl_softwarebauelemente[_IndexOfReports]).BehandleReportRequestedEvent(pin_mitteilung);			
		}		
		#endregion
		
		#region Methoden		
		/// <summary>
		/// Liefert alle Systemrollen als Array von string zurueck,
		/// damit Cpr_ClientAnmeldung keine Klasse der DVS kennt.
		/// </summary>
		/// <returns>Systemrollen als Stringarray</returns>
		public string[] HoleSystemrollen()
		{
			Tdv_Systemrolle[] rollen =			
				(Tdv_Systemrolle[]) Enum.GetValues(typeof(Tdv_Systemrolle));

			string[] str_rollen = new string[rollen.Length]; 
			
			int i = 0;
			foreach(Tdv_Systemrolle r in rollen)
			{
				str_rollen[i] = r.ToString();
				i++;
			}	
			return str_rollen;
		}
		// Liefert alle Benutzernamen in Array von string zur�ck
		public string[] HoleAlleBenutzernamen()
		{ 						
			string[] pout_alleBenutzer = new string[this._benutzerMenge.Length];
			int i=0;
			foreach(Cdv_Benutzer b in this._benutzerMenge)
			{
				pout_alleBenutzer[i] = b.Benutzername;
				i++;
			}
			return pout_alleBenutzer;				
		}

		
		/// <summary>
		/// Pr�fe die G�ltigkeit der Parameter, gibt false oder true zur�ck.
		/// Im erfolgreichen Fall initialisiere den in dieser Klasse gehaltenen 
		/// Benutzer mit den �bergebenen Parametern.
		/// falls der Benutzer sich noch nie zuvor im System angemeldet hat,
		/// ist pin_istNeuerBenutzer true, sonst false
		/// Ist der Benutzer neu, muss er in die DB geschrieben werden und danach ins Array
		/// _benutzerMenge eingetragen werden.
		/// </summary>		
		public bool SetzeBenutzer(string pin_benutzername, string pin_systemrolle, bool pin_istNeuerBenutzer)
		{
			// Pruefe auf die Dopplung: Wenn der Benutzer neu ist, wird der benutzername auf
			// Dopplung gepr�ft. Dieser Benutzer darf sich nur anmelden, wenn sein Name
			// nicht bereits im System vorhanden ist.
			if(pin_istNeuerBenutzer)
			{
				foreach(Cdv_Benutzer b in this._benutzerMenge)
				// wenn ein gleicher Benutzername bereits vorhanden ist
					if(pin_benutzername.CompareTo(b.Benutzername) == 0)
					{						
						CPopUp.BenutzerExistiertBereits();						
						return false;
					}				
			}						
			
			// Systemrolle ermitteln
			Tdv_Systemrolle enm_systemrolle = 
				(Tdv_Systemrolle) Enum.Parse(typeof(Tdv_Systemrolle),(pin_systemrolle),true);
			// aktuellen Benutzer setzen				
			this._Einstellung.Benutzer.Benutzername = pin_benutzername;
			this._Einstellung.Benutzer.Systemrolle = enm_systemrolle;
			this._Einstellung.Benutzer.ID = HoleBenutzerID(pin_benutzername);
			// AktuelleRolle ist die des angemeldeten Benutzers
			this._Einstellung.Benutzer.Systemrolle = enm_systemrolle;
			
			// Wenn ein Neuer Benutzer angemeldet wird, Schreibe ihn in die Datenbank
			if (pin_istNeuerBenutzer)
			{							
				// Neuen Benutzer in die DB schreiben, zur�ck kommt der gleiche Benutzer mit seiner DB-ID
				_Einstellung.Benutzer = _st_allgFkt.SpeichereBenutzer(_Einstellung.Benutzer);
				
				
				// Den neuen Benutzer mit der neuen ID in _benutzerMenge schreiben
				// Arraylist nimmt zun�chst alle vorhandenen Benutzer auf
				ArrayList al_tempBenutzerMenge = new ArrayList();
				foreach(Cdv_Benutzer b in _benutzerMenge)
					al_tempBenutzerMenge.Add(b);
				// Dann hinzuf�gen des neuen Benutzers
				al_tempBenutzerMenge.Add(_Einstellung.Benutzer);

				_benutzerMenge = new Cdv_Benutzer[ _benutzerMenge.Length+1 ];
				al_tempBenutzerMenge.CopyTo(_benutzerMenge);
			}				
			return true;			
		}

		public void SetzeServerKonfig(string pin_IP, string pin_Port)
		{
			this._Einstellung.ServerIP = pin_IP;
			this._Einstellung.ServerPort = pin_Port;
		}

		public Cdv_Benutzer IdentifiziereBenutzer()
		{
			return this._Einstellung.Benutzer;
		}
		/// <summary>
		/// gibt die ID eines Benutzers wieder, der schon in der DB steht.
		/// Dies ist wichtig, um nach der Auswahl des Benutzers in der Combobox 
		/// auch seine ID zu kennen.
		/// </summary>
		/// <param name="pin_Benutzername">Benutzername</param>
		/// <returns>0, wenn Benutzername nicht gefunden wurde, sonst die BenutzerID</returns>
		public int HoleBenutzerID(String pin_Benutzername)
		{
			int pout_benutzerID = 0;
			foreach(Cdv_Benutzer b in _benutzerMenge)
				if (b.Benutzername.Equals(pin_Benutzername)) pout_benutzerID = b.ID;
			return pout_benutzerID;
		}

// TODO: Wirklich ben�tigt?
		public bool PruefeVerbindungZumServer()
		{
//			Cst_ClientVerbindung client = new Cst_ClientVerbindung();
//			 
//			if ( client.InitRemotableObjekt(
//				Convert.ToInt16(this._Einstellung.ServerPort),
//				this._Einstellung.ServerIP))
//			{
//				this._apClient = client.proxyClient;
//				return true;
//			}
//			else
//			{
//				Console.WriteLine("Couldn't initialize remoting client!");
//				Console.ReadLine();
//				return false;
//			}
			//TODO
//         Initialisiere this._apClient;
//         Initialisiere this._apAllgFkt;

			return true;
		}		
		#endregion
		
		#region SBEs laden 
		// Durchsuchen eines Standardverzeichnisses und Laden alle gefundenen Softwarebauelemente
		private void LadenAllerAssemblies()
		{
			Cpr_SplashScreen.ZeigeSplashScreen(false);
			Application.DoEvents();
			// Laden aller Assemblies
			System.IO.DirectoryInfo dir_sbeVerzeichnis = new System.IO.DirectoryInfo(_str_sbeVerzeichnis);
			//TODO: hier noch etwas feilen! mglw aotputfile-namen �ndern.
			System.IO.FileInfo[] arl_alleAssemblies = dir_sbeVerzeichnis.GetFiles("*sbe*.dll");
			Cpr_SplashScreen.SetzeStatus("Lade Assemblies");
			foreach(System.IO.FileInfo asm_Sbe in arl_alleAssemblies)
			{		
				this.LadeAssembly(asm_Sbe.FullName ,true);
			}
			Cpr_SplashScreen.CloseForm();
		}				
		private void LadeAssembly (string str_assemblyName, bool b_SenderIsToList)
		{   
			// Laden der Assembly mit Fehlerbehandlung
			Assembly asm_aktuellesSbe = null;
			try
			{
				asm_aktuellesSbe = Assembly.LoadFrom(str_assemblyName);
			}
			catch 
			{
				MessageBox.Show("Die Datei " + str_assemblyName + " konnte nicht geladen werden.", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}			
			// Laden aller Typeninformationen
			Type[] TypesOfComp = asm_aktuellesSbe.GetTypes();			
			// �berpr�fen ob die Assembly das GUI-Interface implementiert
			foreach(Type t in TypesOfComp)
			{			
			Cpr_SplashScreen.SetzeStatus("Lade" + " " + t.ToString());			
				if (null != t.GetInterface((typeof(pELS.GUI.Interface.Isbe).FullName)))
				{
					Object obj_Csbe = null;
					// Instanzieren des gefundenen SBEs mit Fehlerbehandlung
					try 
					{					
						//MessageBox.Show(Cst_Client.IdentifiziereBenutzer().Benutzername.ToString());
						Cst_Einstellung argOne = this.Einstellung;
						object[] args = {argOne};

						obj_Csbe = Activator.CreateInstance(t,args);						
					}
					catch(Exception e)
					{
//						MessageBox.Show("Message: "+ e.Message+"\n"
//							+"Source: "+ e.Source+"\n"
//							+"Target: "+ e.TargetSite+"\n"
//							+"Stack: "+ e.StackTrace);
//						if (e.InnerException != null)
//						{
//							MessageBox.Show("IMessage: "+ e.InnerException.Message+"\n"
//								+"ISource: "+ e.InnerException.Source+"\n"
//								+"ITarget: "+ e.InnerException.TargetSite+"\n"
//								+"IStack: "+ e.InnerException.StackTrace);
//						}
						MessageBox.Show("Die Datei " + str_assemblyName + " konnte nicht geladen werden.", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						Console.WriteLine(e.ToString(),Console.Error);
						return;
					}					
					// �berpr�fen ob SBE bereits geladen wurde, wenn ja abbruch
					foreach(Object obj_oldCsbe in this._arl_softwarebauelemente)
						if (obj_Csbe.ToString().Equals(obj_oldCsbe.ToString())) return;
					Cpr_SplashScreen.SetzeStatus("Hinzuf�gen" + " " + "SBEs");								
					// SBE zur Liste hinzuf�gen
					this._arl_softwarebauelemente.Add(obj_Csbe);					
					
					// Position an der das SBE geladen wurde
					int i_index = this._arl_softwarebauelemente.IndexOf(obj_Csbe);
					
					// Laden der Beschriftung f�r die Auswahlliste
					string str_beschriftung;
					try
					{
						str_beschriftung	= ((obj_Csbe as pELS.GUI.Interface.Isbe).GetSbeName());
					}
					catch (System.Exception ex)
					{						
						MessageBox.Show(obj_Csbe.ToString()+" konnte seinen Namen nicht nennen\n"+ex.Message);
						str_beschriftung	= "default"+obj_Csbe.ToString();
					}
								
					// Laden des Bildes f�r die Auswahlliste
					Image im_bild;
					try
					{
						// Versuche das Icon von SBE zu beziehen
						im_bild = ((obj_Csbe as pELS.GUI.Interface.Isbe).GetSbeImage());				
					}
					catch	(System.Exception ex)
					{
						//default.jpg aus laden
						//MessageBox.Show("Es konnte kein spezifisches Bild f�r "+ str_beschriftung + " geladen werden.\n"+ex.Message);
						// hier das default.jpg statt dessen laden
						System.Reflection.Assembly asm_Assembly;
						//Informationen �ber die ausf�hrende Assembly sammeln
						asm_Assembly = System.Reflection.Assembly.GetExecutingAssembly();
						//Hole Name der Assembly als AssemblyName
						System.Reflection.AssemblyName asm_SbeName = asm_Assembly.GetName();
						//Speichere den dll Namen im String
						string strAssemblyName = asm_SbeName.Name;
						//Erstelle ein Stream, aus dem die Icon Daten gelesen werden
						System.IO.Stream s = asm_Assembly.GetManifestResourceStream(strAssemblyName + ".default.JPG");
						//Lese die Icon Daten aus dem Stream
						im_bild = Image.FromStream(s);
					}

					// Hinzuf�ge des SBE-Icons in die Auswahlliste
					_prClient.ErweitereAuswahlliste(str_beschriftung, im_bild, i_index);
					if (str_beschriftung.Equals("Reports")) 
						_IndexOfReports = i_index;	
				}
			}					
		}

		// starten der SBEs
		public void StarteSBE(int pin_i_index)
		{   	
			// Sbe bestimmen
			object obj_Csbe = _arl_softwarebauelemente[pin_i_index];
			// Sbe laden
			UserControl uc_Csbe = (obj_Csbe as pELS.GUI.Interface.Isbe).GetSbeUserControl();
			//geladene usc entfernen
			_prClient.gbx_Softwarebauelement.Controls.Clear();
			// ausgew�hltes Sbe anzeigen
			_prClient.gbx_Softwarebauelement.Controls.Add(uc_Csbe);
			
		}
		#endregion
	
		#region Rollenwechsel
		public void Rollenwechsel(string pin_Rolle)
		{
			//merke alte Rolle:
			Tdv_Systemrolle alteRolle = _Einstellung.Benutzer.Systemrolle;

			switch (pin_Rolle) 
			{
				case "Zugf�hrer"		: {_Einstellung.Benutzer.Systemrolle = Tdv_Systemrolle.Zugf�hrer; break;}
				case "Zugtruppf�hrer"	: {_Einstellung.Benutzer.Systemrolle = Tdv_Systemrolle.Zugtruppf�hrer; break;}
				case "Einsatzleiter"	: {_Einstellung.Benutzer.Systemrolle = Tdv_Systemrolle.Einsatzleiter;break;}
				case "LeiterF�St"		: {_Einstellung.Benutzer.Systemrolle = Tdv_Systemrolle.LeiterF�St;break;}
				case "F�hrungsgehilfe"	: {_Einstellung.Benutzer.Systemrolle = Tdv_Systemrolle.F�hrungsgehilfe;break;}
				case "Sichter"			: {_Einstellung.Benutzer.Systemrolle = Tdv_Systemrolle.Sichter;break;}
				case "Fernmelder"		: {_Einstellung.Benutzer.Systemrolle = Tdv_Systemrolle.Fernmelder;break;}
				case "S1"				: {_Einstellung.Benutzer.Systemrolle = Tdv_Systemrolle.S1;break;}
				case "S2"				: {_Einstellung.Benutzer.Systemrolle = Tdv_Systemrolle.S2;break;}
				case "S3"				: {_Einstellung.Benutzer.Systemrolle = Tdv_Systemrolle.S3;break;}
				case "S4"				: {_Einstellung.Benutzer.Systemrolle = Tdv_Systemrolle.S4;break;}
				case "S5"				: {_Einstellung.Benutzer.Systemrolle = Tdv_Systemrolle.S5;break;}
				case "S6"				: {_Einstellung.Benutzer.Systemrolle = Tdv_Systemrolle.S6;break;}
				case "LeiterStab"		: {_Einstellung.Benutzer.Systemrolle = Tdv_Systemrolle.LeiterStab;break;}
				default					:break;
			}
			
			//Nur starten,wenn die Rolle sich wirklich ge�ndert hat
			if(alteRolle != _Einstellung.Benutzer.Systemrolle)
			{
				
				//Speichern der neuen Rolle auf dem Server	
				this._st_allgFkt.apAllgFkt.SpeichereBenutzer(Einstellung.Benutzer);

				//neue Systemrolle an alle Softwarebauelemnte senden
				foreach (object obj_sbe in this._arl_softwarebauelemente)
				{
					//DEBUG: Ausgeben lassen wegen der Geschwindigkeit
					//MessageBox.Show("Mache Update von "+ obj_sbe.GetType().ToString());
					(obj_sbe as Isbe).SetzeRollenRechte((int) _Einstellung.Benutzer.Systemrolle);				
				}													
			
				// Systemereignis werfen
				Cdv_Systemereignis sysErg = new Cdv_Systemereignis(
					_Einstellung.Benutzer.Benutzername,
					System.DateTime.Now,
					"Der Benutzer " + _Einstellung.Benutzer.Benutzername + " hat die Rolle " + _Einstellung.Benutzer.Systemrolle + " angenommen.",
                    Tdv_SystemereignisArt.Statuswechsel,
					false);
				_st_allgFkt.apAllgFkt.WerfeSystemereignis(sysErg);
			}
		}
		#endregion

		public void Beenden()
		{
			Application.Exit();
//			foreach(object obj_Csbe in _arl_softwarebauelemente)
//			{
//				if ((obj_Csbe as pELS.GUI.Interface.Isbe).CloseSbeUserControl() == false) return;
//			}
//			this.Close();
		}

		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Cst_Client));
			// 
			// Cst_Client
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Cst_Client";

		}

		#region main
		[STAThread]
		static void Main() 
		{
			Cst_Client stClient = new Cst_Client();
			Application.Run();
		}
		#endregion
	}
}
