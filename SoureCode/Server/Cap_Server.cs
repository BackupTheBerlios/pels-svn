using System;
// benötigt für: ArrayList
using System.Collections;
// benötigt für: ObjectHandle
using System.Runtime.Remoting;
// benötigt für: ActivationAttributes
using System.Runtime.Remoting.Activation;
// benötigt für: Exportieren des Datenbestand
using System.IO;
// benötigt für: Wrapperzugriff auf Einsatzwrapper
using pELS.DV.Server.Wrapper;
// benötigt für: das Laden der PortalLogik-Assembly
using System.Reflection;
// benötigt für: IVerwaltung
using pELS.DV.Server.ObjectManager.Interfaces;
// benötigt für: pels-Objecte
using pELS.DV.Server.Interfaces;
//benötigt für: istPortFrei
using pELS.Tools.Server;
//benötigt für: Splash Screen
using pELS.Tools;

// Quecky: Folgendes benötige ich für Datenim- und export
using Npgsql;


//TODO: alle löschen
//using System.Windows.Forms;
// benötigt für: ChannelServices
using System.Runtime.Remoting.Channels;
// benötigt für: TcpChannel
using System.Runtime.Remoting.Channels.Tcp;
// benötigt für: TypeFilterLevel
using System.Runtime.Serialization.Formatters;

namespace pELS.Server
{	
	using pELS.DV;
	using pELS.Tools;
	using pELS.DV.Server.ObjectManager;
	/// <summary>
	/// CAP des Servers
	/// diese Klasse initialiert alle benötigten Funktionalitäten für
	/// den Server
	/// Sie ist das Herzstück des Servers
	/// </summary>
	public class Cap_Server// : System.Windows.Forms.Form
	{
		#region Instanzvariablen
		/// <summary>
		/// beinhaltet Verweise auf alle AppDomains in den PortalLogiken liegen
		/// </summary>
		ArrayList _arl_PortalLogik_AppDomains = new ArrayList();
		/// <summary>
		/// Referenz auf den ObjectManager
		/// </summary>
		private static Cdv_ObjMgr _ObjectManager = null;		
		/// <summary>
		/// Referenz auf die Datenbankanbindung, zur Initialisierung
		/// </summary>
		private static Cdv_DB _cdv_DB;
		/// <summary>
		/// zum Laden und Speichern der Einsatzdaten
		/// </summary>
		private static IVerwaltung _verw_einsatzverwaltung;
		/// <summary>
		/// der Server hat bereits einen eigenen Kanal geöffnet
		/// </summary>
		private static bool _KanalOffen = false;
		/// <summary>
		/// Kanal, über welchen mit dem Server kommuniziert werden kann
		/// </summary>
		private TcpChannel _ServerKanal;
		/// <summary>
		/// speichert den Wert für die interne Portnummer
		/// an der z.B. der ObjektManager erreichbar ist
		/// </summary>
		private static int _Interner_RemotePort = _LetztePortNummer;
		/// <summary>
		/// speichert die letzte Portnummer, auf dem versucht wurde
		/// einen Kanal zu vergeben
		/// </summary>
		private static int _LetztePortNummer = 9000;
		/// <summary>
		/// speichert Referenz auf ein statisches Objekt vom Typ CPortalWaechter
		/// </summary>
		private static CPortalWaechter _PortalWaechter = new CPortalWaechter();
		/// <summary>
		/// beinhaltet Informationen zur Serverkonfiguration
		/// </summary>
		private Cdv_Serverkonfiguration _Serverkonfiguration = null;
		/// <summary>
		/// Prozesscontainer für Hintergrundprozesse
		/// </summary>
		public System.Diagnostics.Process BackupProzess;
		/// <summary>
		/// Verzeichnis indem sich die Backuptools befinden
		/// </summary>
		private string _str_pgAdminPfad = Environment.CurrentDirectory + @"\pgadmin\";


		/// <summary>
		/// Entscheidet ob das SplashScreen gezeigt werden soll
		/// </summary>
		private bool _ZeigeSplashScreen = true;
		
		#endregion
		
		
		#region SETs und GETs
		public Cdv_Serverkonfiguration Serverkonfiguration
		{
			get
			{
				if (_Serverkonfiguration == null)
				{
					_Serverkonfiguration = this.LadeServerkonfiguration();
				}
				return _Serverkonfiguration;
			}
			set
			{
				this.SpeichereServerkonfiguration(value);
			}
		}

		#endregion

		[STAThread]
		static void Main() 
		{
			Cap_Server _Cap_Server = new Cap_Server();
			Cst_Server _Cst_Server = new Cst_Server(ref _Cap_Server);
		}

		/// <summary>
		/// Konstruktor der Cap_Server-Klasse
		/// startet die Serverroutine und 
		/// lädt das ConfigFile
		/// </summary>

		#region Konstruktor
		public Cap_Server()
		{
			
		}

		#endregion
		
		
		/// <summary>
		/// startet den Objektmanager
		/// </summary>
		private void InitialisiereObjektManager(int pin_Port)
		{
			// erzeuge ein neues Objekt vom Typ Cdv_ObjMgr
			_ObjectManager = Cdv_ObjMgr.HoleInstanz();

			//Hier das Starten aller Verwaltungen anstoßen
			_ObjectManager.InitialisiereAlleVerwaltungen();

			//Gebe alle nicht geladenen Verwaltungen an Nutzer weiter
			if(_ObjectManager.NichtGeladeneVerwaltungen != String.Empty)
				System.Windows.Forms.MessageBox.Show("Folgende Verwaltungen konnten nicht geladen werden:\n\n"+_ObjectManager.NichtGeladeneVerwaltungen+"\nVersichern Sie sich, dass die Datenbankverbindung korrekt ist und dass Sie auf eine gültige Einsatzdatenbank zugreifen. Sie können eine neue Einsatzdatenbank anlegen (Einsatz\\neuen Einsatz anlegen).\n Sie müssen den Server danach manuel neu starten (Server\\neu starten).\n\nOrt: pELS.Server.Cap_Server.InitialisiereObjektManager()","Fehler beim Laden des ObjektManagers",System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

			// publiziert dieses Objekt
			RemotingServices.Marshal(
				_ObjectManager, 
				CKonstanten._str_ObjektManager_RemotePfad, 
				typeof(Cdv_ObjMgr));				

			#region nur für Testzwecke
//			string connectionString = @"tcp://127.0.0.1:" + this._Serverkonfiguration.Port + "/" + CKonstanten._str_ObjektManager_RemotePfad;
//			Cdv_ObjMgr tmp = (Cdv_ObjMgr)Activator.GetObject(
//				typeof(Cdv_ObjMgr), 
//			connectionString);
//			tmp.Einsaetze.HolenAlle();
			#endregion					

		}
		
		
		private void InitialisiereCdv_DB()
		{
			// Instanz erstellen
			_cdv_DB = Cdv_DB.HoleInstanz();		
			// Configfile auslesen
			XMLZugriff XMLZugriffsObject = new XMLZugriff();
			XMLZugriffsObject.LadeDatei(CKonstanten._str_ServerConfigPfad);
			XMLZugriffsObject.WaehleKnoten("pELS/pELS-Server/DBConfig");
			#region Testen der Konfig Daten evtl. neu konfigurieren
			
			string str_verbindenErfolgreich = _cdv_DB.testeDB(
				XMLZugriffsObject.HoleKnotenAttribut(0, "UserID"),
				XMLZugriffsObject.HoleKnotenAttribut(0, "PW"),
				XMLZugriffsObject.HoleKnotenAttribut(0, "Host"),
				XMLZugriffsObject.HoleKnotenAttribut(0, "Port"),
				XMLZugriffsObject.HoleKnotenAttribut(0, "DBName"),
				XMLZugriffsObject.HoleKnotenAttribut(0, "Lifetime")   );

			if(str_verbindenErfolgreich != "")
			{
				System.Windows.Forms.MessageBox.Show("Folgender Fehler trat beim Verbinden mit der Datenbank auf:\n\n"
													+ str_verbindenErfolgreich 
													+ "\n\nKonfigurieren Sie die Datenbankanbindung bevor es weiter gehen kann.",
													"Fehler bei DB-Konfiguration",
													System.Windows.Forms.MessageBoxButtons.OK,
													System.Windows.Forms.MessageBoxIcon.Error);
				Cpr_frm_ServerKonfiguration konfig = new Cpr_frm_ServerKonfiguration();
				konfig.ShowDialog();
				//Wenn der nutzer es bis dahin immer noch nciht gepackt hat, dann soll er gehen
				if(!konfig.EingabeErfolgreich)
					Environment.Exit(2);
				else
				//Nochmal die Methode starten (rekursiv)
					this.InitialisiereCdv_DB();

			}
			#endregion
			else				
			{

				// DB-config-Daten zur Verbindung nutzen
				bool b_verbindenErfolgreich = _cdv_DB.VerbindeMitDB(
					XMLZugriffsObject.HoleKnotenAttribut(0, "UserID"),
					XMLZugriffsObject.HoleKnotenAttribut(0, "PW"),
					XMLZugriffsObject.HoleKnotenAttribut(0, "Host"),
					XMLZugriffsObject.HoleKnotenAttribut(0, "Port"),
					XMLZugriffsObject.HoleKnotenAttribut(0, "DBName"),
					XMLZugriffsObject.HoleKnotenAttribut(0, "Lifetime")   );
				// wenn was schief ging Exception werfen
				if (!b_verbindenErfolgreich) throw new Exception("Verbindung mit DB konnte nicht aufgebaut werden.");
			}
		}		
		#region Für die Benutzung von Splash Screen
		/// <summary>
		/// Initialisiere das Splash Screen
		/// </summary>
		private void InitSplashScreen()
		{
			if(this._ZeigeSplashScreen == true)
			{
				Cpr_SplashScreen.ZeigeSplashScreen(true); 
				System.Windows.Forms.Application.DoEvents();
			}
			else{}	
		}
		/// <summary>
		/// Sezte den Satus des Splash Screen, die zu übergebene Zeichenkette steht
		/// für den im Augenblick ausgeführten Prozess
		/// </summary>
		private void SetzeSplashScreenStatus(string pin_Prozess)
		{
			if(this._ZeigeSplashScreen == true)		
			{
				Cpr_SplashScreen.SetzeStatus(pin_Prozess);
			}
			else{}
		}
		/// <summary>
		/// Sezte den Satus des Splash Screen, die zu übergebene Zeichenkette steht
		/// für den im Augenblick ausgeführten Prozess. Der boolesche Wert
		/// wirkt auf progress bar aus. Diese Methode ist zu empfehlen, wenn an einer Stelle
		/// im Code zu viel SetzeSplashScreenStatus(...) aufgerufen werden, und in diesem Fall
		///	solle der boolesche Wert mit false dieser Methode übergeben werden.
		/// </summary>
		private void SetzeSplashScreenStatus(string pin_Prozess, bool pin_SetzeReferenz)
		{
			if(this._ZeigeSplashScreen == true)		
			{
				Cpr_SplashScreen.SetzeStatus(pin_Prozess,pin_SetzeReferenz);
			}
			else{}
		}
		/// <summary>
		/// Destroy das Splash Screen
		/// </summary>
	
		private void SchliesseSplashScreen()
		{
			Cpr_SplashScreen.CloseForm();
		}

		/// <summary>
		/// get, set Methode für das Attribut, das entscheidet, ob das Splash Screen angezeigt werden.
		/// </summary>
		public bool ZeigeSplashScreen
		{
			get{return this._ZeigeSplashScreen;}
			set{this._ZeigeSplashScreen = value;}
		}
		#endregion
			 	
		/// <summary>
		/// startet die Serverroutine, d.h.
		/// alle PortalLogiken werden aktiviert und via Remoting publiziert
		/// </summary>
		/// <returns></returns>
		public void StarteServerRoutine()
		{
			
			
			this.InitSplashScreen();
			this.SetzeSplashScreenStatus("StarteServerRoutine Beginn");
			

			#region Initialisiere Kanal
			this.SetzeSplashScreenStatus(" Initialisiere Kanal");

			// lade den Port aus der Serverkonfiguration
			_LetztePortNummer = Convert.ToInt16(Serverkonfiguration.Port);
			// bestimme einen freien Port
			while ((!_KanalOffen) && (!Tools.Server.CMethoden.IstPortFrei(_LetztePortNummer)))
			{
				_LetztePortNummer++;
			}
			if (!_KanalOffen)
			{
				// öffne Kanal auf dem alle Remotable Objects dieses Servers angesprochen werden können
				this._ServerKanal = Tools.Server.CMethoden.InitialisiereKanal(_LetztePortNummer);
				_Interner_RemotePort = _LetztePortNummer;
				_KanalOffen = true;
			}
			this.SetzeSplashScreenStatus("Setze Serverkonfiguration");
			// speichere den verwendeten Port in der Serverkonfiguration
			pELS.Tools.XMLZugriff myXMLZugriff = new pELS.Tools.XMLZugriff();
			myXMLZugriff.LadeDatei(pELS.Tools.Server.CKonstanten._str_ServerConfigPfad);
			myXMLZugriff.WaehleKnoten("pELS/pELS-Server/Address");
			myXMLZugriff.SetzeKnotenAttribut(0,"Port", _Interner_RemotePort.ToString());
			
			//Wenn die gewünschte Portnummer nicht vergeben werden konnte, dann Hinweis auf verwendeten Port ausgeben
			if(_LetztePortNummer.ToString() != Serverkonfiguration.Port)
				System.Windows.Forms.MessageBox.Show("Der Port "+Serverkonfiguration.Port+"konnte nicht für den pELS-Server reserviert werden.\n Gewählter Anmeldeport: "+_LetztePortNummer.ToString());
			#endregion			

			this.SetzeSplashScreenStatus("Initialisiere DB");
			InitialisiereCdv_DB();
			this.SetzeSplashScreenStatus("Initialisiere ObjektManager");
			InitialisiereObjektManager(_LetztePortNummer);
			
			

			// URL des Servers
			string ServerURL = "tcp://" + Serverkonfiguration.IP;
			// bezeichnet den Klassen- u. Dateinamen der PortalDecorators
			string PortalDecoratorName = "";

			// lese alle Dateien aus dem Verzeichnis, in welchem sich die
			// Dlls der PortalLogiken befinden
			System.IO.DirectoryInfo dir_sbeVerzeichnis = 
				new System.IO.DirectoryInfo(CKonstanten._str_PortalLogikPfad);
			// lese alle Dlls ein
			System.IO.FileInfo[] arl_alleAssemblies = dir_sbeVerzeichnis.GetFiles("*.dll");
			// gehe durch alle Assemblies
			this.SetzeSplashScreenStatus("Lade Assemblies:");
			foreach(System.IO.FileInfo fileInfo in arl_alleAssemblies)
			{		
				// lese alle in der Dll enhaltenen Typen ein
				Assembly _asm_Tmp = Assembly.LoadFrom(fileInfo.FullName);
				Type[] _AvailableTypes = _asm_Tmp.GetTypes();
				foreach(Type _AktuellerType in _AvailableTypes)
				{									
					this.SetzeSplashScreenStatus("Lade Assemblies:" + " " + _AktuellerType.ToString(),false);
					// überpüfen, ob aktueller Typ das benötigte Interface implementiert
					if (null != (_AktuellerType.GetInterface(
						// und nicht abstrakt ist (wg. Cap_portalLogik)
						(typeof(pELS.Server.IPortalLogik).FullName))))
						if ((!_AktuellerType.IsAbstract))
						{
							try
							{
								#region erstelle AppDomain
								// setze AppDomain-Eigenschaften
								AppDomainSetup adSetup = new AppDomainSetup();
								adSetup.ApplicationBase = Environment.CurrentDirectory;
								// erzeuge die AppDomain 
								AppDomain neuePortalLogikAppDomain = AppDomain.CreateDomain(fileInfo.FullName, null, adSetup);
								#endregion

								// erzeuge Objekt vom Typ Server_PortalLader
								ObjectHandle _PLhandle = neuePortalLogikAppDomain.CreateInstanceFrom(
									"Server_PortalLader.dll", 
									"pELS.Server" + "." + "CPortalLogikLader");
								// entpacke Objekt
								CPortalLogikLader _PortalLogikLader = (CPortalLogikLader)_PLhandle.Unwrap();
								// setze auf die nächste (wahrscheinlich) freie Portnummer
								_LetztePortNummer++;
								if (_PortalLogikLader.LadeAssembly(fileInfo.FullName, 
									_Interner_RemotePort, ServerURL, _LetztePortNummer))
								{
									// starte die PortalLogik
									_PortalLogikLader.StartePortalLogik();
									// hole den gewählten Port, auf dem die PortalLogik erreichbar ist
									_LetztePortNummer = _PortalLogikLader.LiefereRemotingPort();
									#region starte PortalDecorator
									// lege Namen für zu erzeugende Datei fest
									PortalDecoratorName = _AktuellerType.Name + "_Decorator";
									// überpüfe, ob die Ausgabedatei geschrieben werden kann
									// (falls DLL bereits erzeugt wurde und benutzt wird, kann sie nicht
									// mehr geschrieben werden)
									FileInfo PortalDecoratorFI = new FileInfo(PortalDecoratorName + ".dll");
									// falls sie nicht geschrieben werden kann, erzeuge sie nicht erneut
									if(PortalDecoratorFI.Exists)
									{
										try
										{
											// überprüfe Zugriffrechte auf diese Datei
											PortalDecoratorFI.Delete();
											// erzeuge Assembly für den Decorator
											CDecoratorDesigner.UebersetzeSourceCode(
												// Name der PortalLogik
												fileInfo.FullName,
												// Name der zu ereugenden Datei
												PortalDecoratorName + ".dll",
												// Pfad aller Dependencies
												AppDomain.CurrentDomain.BaseDirectory,
												// SourceCode
												CDecoratorDesigner.CreateSourceCode(
												_AktuellerType,
												PortalDecoratorName,
												_LetztePortNummer, 
												_PortalLogikLader.LieferePortalPfad()));
										}
										catch
										{
										}
									}
									else
									{
										// erzeuge Assembly für den Decorator
										CDecoratorDesigner.UebersetzeSourceCode(
											// Name der PortalLogik
											fileInfo.FullName,
											// Name der zu ereugenden Datei
											PortalDecoratorName + ".dll",
											// Pfad aller Dependencies
											AppDomain.CurrentDomain.BaseDirectory,
											// SourceCode
											CDecoratorDesigner.CreateSourceCode(
											_AktuellerType,
											PortalDecoratorName,
											_LetztePortNummer, 
											_PortalLogikLader.LieferePortalPfad()));

									}
									// erzeuge Objekt vom Typ des PortalDecorator
									ObjectHandle _PDhandle = AppDomain.CurrentDomain.CreateInstanceFrom(
										PortalDecoratorName + ".dll", 
										"pELS.Server" + "." + PortalDecoratorName);
									// entpacke Objekt
									IPortalLogik_Decorator _PortalLogikDecorator = (IPortalLogik_Decorator)_PDhandle.Unwrap();
									// starte den Decorator mit einer Verbindung zur PortalLogik
									// auf dem übergebenen Port
									_PortalLogikDecorator.StarteRemotingObject(_LetztePortNummer);
									// speichere Referenz auf den Decorator im PortalWaechter
									_PortalWaechter.FuegeDecoratorHinzu(_PortalLogikDecorator);
									#endregion
									// füge AppDomain zur ArrayList hinzu
									_arl_PortalLogik_AppDomains.Add(neuePortalLogikAppDomain);
								}
							}
							catch (System.Exception ex)
							{
								this.SetzeSplashScreenStatus("Fehler: "+ex.Message);
								System.Windows.Forms.MessageBox.Show("Fehler beim Laden der Assemblies: \n"+ex.Message+"\nOrt: pELS.Server.Cap_Server.StarteServerRoutine()");
							}						
						}
				}
			}
			this.SetzeSplashScreenStatus("ServerStarteRoutine Ende");
			this.SchliesseSplashScreen();			
		}

		/// <summary>
		/// entfernt alle AppDomains
		/// </summary>
		public void BeendeServerRoutine()
		{
			// entferne alle AppDomains mit den PortalLogiken
			foreach(AppDomain PortalLogikDomain in _arl_PortalLogik_AppDomains)
			{
				AppDomain.Unload(PortalLogikDomain);
			}
			_arl_PortalLogik_AppDomains.Clear();
			// gebe den reservierten ServerKanal frei
			try
			{
				ChannelServices.UnregisterChannel(this._ServerKanal);
				_KanalOffen = false;
			}
			catch
			{
				// der Kanal ist bereits freigeben
			}
			//gebe alle Resourcen der Decorators frei
			_PortalWaechter.EntferneAlleDecorators();
		}


		public void Beenden()
		{
			System.Windows.Forms.Application.Exit();
		}
		/// <summary>
		/// liest das ConfigFile aus
		/// </summary>
		/// <returns>Objekt mit der aktuellen Serverkonfiguration zurück</returns>
		public Cdv_Serverkonfiguration LadeServerkonfiguration()
		{
					
			XMLZugriff XMLZugriffsObject = new XMLZugriff();
			XMLZugriffsObject.LadeDatei(CKonstanten._str_ServerConfigPfad);
			XMLZugriffsObject.WaehleKnoten("pELS/pELS-Server/Address");
			string _str_IP = XMLZugriffsObject.HoleKnotenAttribut(0, "IP");
			string _str_Port = XMLZugriffsObject.HoleKnotenAttribut(0, "Port");
			
			Cdv_Serverkonfiguration pout_ServerKonfiguration = new Cdv_Serverkonfiguration();
			pout_ServerKonfiguration.IP = _str_IP;
			pout_ServerKonfiguration.Port = _str_Port;

			return pout_ServerKonfiguration;
		}

		/// <summary>
		/// speichert die Serverkonfiguration im ConfigFile
		/// </summary>
		/// <param name="pin_Serverkonfiguration"></param>
		/// <returns></returns>
		public bool SpeichereServerkonfiguration(Cdv_Serverkonfiguration pin_Serverkonfiguration)
		{
			XMLZugriff XMLZugriffsObject = new XMLZugriff();
			XMLZugriffsObject.LadeDatei(CKonstanten._str_ServerConfigPfad);
			XMLZugriffsObject.WaehleKnoten("pELS/pELS-Server/Address");
			XMLZugriffsObject.SetzeKnotenAttribut(0, "IP", 
				pin_Serverkonfiguration.IP);
			return true;
		}


		/// <summary>
		/// lädt die aktuellen Einsatzdaten
		/// </summary>
		/// <returns></returns>
		public Cdv_Einsatz LadeEinsatzdaten()
		{
			Cdv_Einsatz pout_leererEinsatz = new Cdv_Einsatz();									
			_verw_einsatzverwaltung = _ObjectManager.Einsaetze;
			IPelsObject[] ipoa = _verw_einsatzverwaltung.HolenAlle();
			//Cdv_Einsatz[] pout_Einsatz = (Cdv_Einsatz[])_verw_einsatzverwaltung.HolenAlle();
			if(ipoa.Length == 0)
				return(pout_leererEinsatz);
			else
				return((Cdv_Einsatz) ipoa[0]);	
		}
		

		/// <summary>
		/// Achtung! Die Methode wurde noch nicht getestet!!!
		/// Datenbank auf Initialzustand setzen
		/// </summary>
		public void ErstelleNeuePelsDatenbank(string pin_dbName)
		{
			#region Verbindung zur DefaultDB herstellen
			string str_tmp_angemeldeterNutzer;
			// Schließe Verbindung zur Datenbank, falls offen
			_cdv_DB.SchliesseDBVerbindung();
			// Öffne neue Verbindung zur der Default PostgreSQL Datenbank
			_cdv_DB = Cdv_DB.HoleInstanz();
			// Configfile auslesen
			XMLZugriff XMLZugriffsObject = new XMLZugriff();
			XMLZugriffsObject.LadeDatei(CKonstanten._str_ServerConfigPfad);
			XMLZugriffsObject.WaehleKnoten("pELS/pELS-Server/DBConfig");
			// DB-config-Daten zur Verbindung nutzen
			bool b_verbindenErfolgreich = _cdv_DB.VerbindeMitDB(
				XMLZugriffsObject.HoleKnotenAttribut(0, "UserID"),
				XMLZugriffsObject.HoleKnotenAttribut(0, "PW"),
				XMLZugriffsObject.HoleKnotenAttribut(0, "Host"),
				XMLZugriffsObject.HoleKnotenAttribut(0, "Port"),
				"template1",
				XMLZugriffsObject.HoleKnotenAttribut(0, "Lifetime")   );
			//merken des Benutzernamen um Ihn beim Erstellen zu ersetzen
			str_tmp_angemeldeterNutzer = XMLZugriffsObject.HoleKnotenAttribut(0, "UserID");
			// wenn was schief ging Exception werfen
			if (!b_verbindenErfolgreich) throw new Exception("Verbindung mit DB konnte nicht aufgebaut werden.");
			#endregion

			#region pELS_DB erstellen und damit verbinden
			// Löscht die Datenbank falls existent
			try
			{
				_cdv_DB.AusfuehrenDDLAnfrage("DROP DATABASE \"" + pin_dbName + "\";");
			}
			catch (Exception ex)
			{
				//nur den Fehler abfangen, dass man eine DB droppen will, die nicht existiert
				if(!ex.Message.StartsWith("ERROR: 3D000"))
					throw ex;
			}
			// Erstellt die neue Datenbank
			try
			{
				//Optimierungen bzw. der Syntax hier: http://pgsqld.active-venture.com/sql-createdatabase.html
				_cdv_DB.AusfuehrenDDLAnfrage("CREATE DATABASE \"" + pin_dbName +"\" TEMPLATE=\"template0\";");
			}
			catch (Exception ex)
			{
				//Fehler abfangen, und an Nutzer weiterreichen
				System.Windows.Forms.MessageBox.Show(ex.Message ,
													"Fehler beim Anlegen der Datenbank");

			}
				// Mit neuer DB verbinden
			this.InitialisiereCdv_DB();
			#endregion
		
			#region SQL Befehle zur DB Initialisierung aus Datei auslesen und an DB übertragen
			//Variablendeklaration
			StreamReader sqlStream = null;
			// Liest die Skripte aus einer Datei 
			try
			{
				sqlStream = new StreamReader(CKonstanten._str_PfadZuPelsDbSchemaDatei);
			}
			catch (Exception ex)
			{	//Hinweis auf Fehler geben
				System.Windows.Forms.MessageBox.Show("Folgender Fehler beim öffnen der Datei "+CKonstanten._str_PfadZuPelsDbSchemaDatei+" auf\n\n\n"+ex.Message,
													 "Fehler beim Lesen der Datei",
													 System.Windows.Forms.MessageBoxButtons.OK,
													System.Windows.Forms.MessageBoxIcon.Error);				 
			}

			// Zwischenspeicher für die einzelnen Befehlszeilen
			string str_Befehlszeile = String.Empty;
			string str_sqlBefehl = String.Empty;
			if(sqlStream !=null)
			{	// Datei Zeilenweise durchgehen
				while ((str_Befehlszeile = sqlStream.ReadLine()) != null)
				{
					if (str_Befehlszeile.StartsWith("//") || str_Befehlszeile.StartsWith("/*"))
					{
						//es handelt sich um einen Kommentar, der nicht betrachtet wird
					}
					else
					{
						//Anfügen und auch gleich den Richtigen Benutzer eintragen -> alexG
						str_sqlBefehl += str_Befehlszeile.Replace("UserNameToReplace",str_tmp_angemeldeterNutzer);
						if(str_sqlBefehl.EndsWith(";"))
						{							
							_cdv_DB.AusfuehrenDDLAnfrage(str_sqlBefehl);
							str_sqlBefehl = String.Empty;
						}
					}
					
				}
			}
			#endregion

		}
		
		/// <summary>
		/// Importieren der Einsatzdaten aus einer externen Datei
		/// </summary>
		/// <param name="pin_FileName">Name und Pfad der Datei, aus der importiert wird</param>
		/// <returns>Bool ob der Import funktioniert hat</returns>
		public bool ImportiereDatenbestand(string pin_FileName)
		{
			// Prozesscontainer in dem der pgAdmin ausgeführt wird
			InitialisiereHintergrundProzess();
			//Schaffe Zugriff auf Konfig
			XMLZugriff XMLZugriffsObject = new XMLZugriff();
			XMLZugriffsObject.LadeDatei(CKonstanten._str_ServerConfigPfad);
			XMLZugriffsObject.WaehleKnoten("pELS/pELS-Server/DBConfig");
			// Aufruf
			BackupProzess.StartInfo.FileName = _str_pgAdminPfad + "pg_restore.exe";
			BackupProzess.StartInfo.Arguments = " -i -h " + XMLZugriffsObject.HoleKnotenAttribut(0, "Host")
				+ " -p " + XMLZugriffsObject.HoleKnotenAttribut(0, "Port")
				+ " -U " + XMLZugriffsObject.HoleKnotenAttribut(0, "UserID")
				+ " -d \"" + XMLZugriffsObject.HoleKnotenAttribut(0, "DBName") + "\""
				+ " -c -v \"" + pin_FileName + "\"";
			// Starten des Prozesses
			BackupProzess.Start();
			// Automatisierte Eingabe des Passworts
			BackupProzess.StandardInput.WriteLine(XMLZugriffsObject.HoleKnotenAttribut(0, "PW"));
			return true;
		}

		/// <summary>
		/// Exportiern der kompletten Datenbank in eine Datei im csv oder pels Format
		/// mögliches Feature: zusätzlich xml
		/// </summary>
		/// <param name="pin_FileStream">wohin gehen die Daten</param>
		/// <returns></returns>
		public bool ExportiereDatenbestand(string pin_FileName)
		{
			// ausgewählte Exportformate
			bool pels = false;
			bool csv = false;
			
			// Überprüfen in welchem Format exportiert werden soll.
			switch (pin_FileName.Substring(pin_FileName.LastIndexOf(".") + 1).ToLower())
			{
				case "pels": 
				{
					// PgAdmin wird verwendet um die PostgreSQL Datenbank komplett zu exportieren
					pels = true;
					break;
				}
				case "csv":
				{
					// Export in einem csv Format für THWin
					csv = true;
					break;
				}
				default:
				{
					pin_FileName += ".pELS";
					pels = true;
					break;
				}

			}
			
			if (pels)
			{
				// Prozesscontainer in dem der pgAdmin ausgeführt wird
				InitialisiereHintergrundProzess();
				//Zugriff auf die Konfig
				XMLZugriff XMLZugriffsObject = new XMLZugriff();
				XMLZugriffsObject.LadeDatei(CKonstanten._str_ServerConfigPfad);
				XMLZugriffsObject.WaehleKnoten("pELS/pELS-Server/DBConfig");
				// Aufruf
				System.Windows.Forms.MessageBox.Show(XMLZugriffsObject.HoleKnotenAttribut(0, "DBName").ToString());
				
				BackupProzess.StartInfo.FileName = _str_pgAdminPfad + "pg_dump.exe";
				BackupProzess.StartInfo.Arguments = 
					" -i -h " + XMLZugriffsObject.HoleKnotenAttribut(0, "Host")
					+ " -p " + XMLZugriffsObject.HoleKnotenAttribut(0, "Port")
					+ " -U " + XMLZugriffsObject.HoleKnotenAttribut(0, "UserID")
					+ " -W -F c -c -v -f \"" + pin_FileName + "\""
					+ " \"" + XMLZugriffsObject.HoleKnotenAttribut(0, "DBName") + "\"";
				// Starten des Prozesses
				BackupProzess.Start();
				// Automatisierte Eingabe des Passworts
				BackupProzess.StandardInput.WriteLine(XMLZugriffsObject.HoleKnotenAttribut(0, "PW"));
				return true;
			}

			if (csv)
			{
				// Exportdatei
				StreamWriter _FileStream = new StreamWriter(pin_FileName, false);
			
				// Benötigte Parameter um die Daten aus der Datenbank zu holen
				Cdv_DB _obj_InstanzVonDB = Cdv_DB.HoleInstanz();
				NpgsqlDataReader _dr_TabellenNamen;
				NpgsqlDataReader _dr_Tabelle;
				string _str_Tabellenname = String.Empty;
				string _str_Anfrage = String.Empty;
				int _i_Datensaetze = 0;
			
				// Alle Tabellen in der Datenbankermitteln
				_str_Anfrage = "SELECT tablename FROM pg_tables WHERE schemaname='public' AND tablename!='spatial_ref_sys' AND tablename!='geometry_columns';";
				_dr_TabellenNamen = _obj_InstanzVonDB.AusfuehrenSelectAnfrage(_str_Anfrage, out _i_Datensaetze);
			
				// Schleife über alle Tabellen in der Datenbank			
				while(_dr_TabellenNamen.Read())
				{
					// Export einer Tabelle
					_str_Tabellenname  = _dr_TabellenNamen.GetValue(0).ToString();
					_str_Anfrage = "SELECT * FROM \"" + _str_Tabellenname + "\";";
					_dr_Tabelle = _obj_InstanzVonDB.AusfuehrenSelectAnfrage(_str_Anfrage, out _i_Datensaetze);
			
					// Schreiben des Tabellennamen
					_FileStream.Write(":" + _str_Tabellenname + ";");
					// Schreiben der Spaltennamen
					for (int _spalte=0; _spalte<_dr_Tabelle.FieldCount; _spalte++)
						_FileStream.Write(_dr_Tabelle.GetName(_spalte) + ";");
					_FileStream.WriteLine();

					// Auslesen und exportieren der Daten
					while (_dr_Tabelle.Read())
					{
						// Alle Werte einer Zeile in einem Array speichern
						object[] _obj_Values = new object[_dr_Tabelle.FieldCount];
						_dr_Tabelle.GetValues(_obj_Values);
			
						// Werte durch Semikolon getrennt speichern
						foreach(object _obj_Value in _obj_Values)
							_FileStream.Write(_obj_Value.ToString() + ";");
			
						// Zeilenumbruch zum Ende des Datensatzes
						_FileStream.WriteLine();
					}
				}
				// Schließen der Exportdatei
				_FileStream.Close();
				return true;
			}
			
			// Wenn man an dieser Stelle ist, wurde nichts exportiert
			return false;
		}

		/// <summary>
		/// Erstellt Prozess welcher Backup und Restore durchführen kann
		/// </summary>
		private void InitialisiereHintergrundProzess()
		{
			BackupProzess = new System.Diagnostics.Process();
			BackupProzess.StartInfo.RedirectStandardInput = true;
			BackupProzess.StartInfo.UseShellExecute = false;
			BackupProzess.EnableRaisingEvents = true;
		}
		
		/// <summary>
		/// speichert Änderungen in den Einsatzdaten
		/// + Wirft Systemereignis (Änderungen an den Einsatzdaten)
		/// </summary>
		/// <param name="pin_Einsatz"></param>
		/// <returns></returns>
		public IPelsObject SpeichereEinsatzdaten(Cdv_Einsatz pin_Einsatz)
		{			
			#region Systemereignis mit Einsatzdaten werfen
			string str_Beschreibung;
			str_Beschreibung = "Die Einsatzdaten wurden bearbeitet:";
			str_Beschreibung += "\nBezeichnung:\t"+pin_Einsatz.Bezeichnung;
			str_Beschreibung += "\nEinsatzort:\t"+pin_Einsatz.Einsatzort;
			str_Beschreibung += "\nVon:\t\t"+pin_Einsatz.VonDatum;
			str_Beschreibung += "\nBis:\t\t"+pin_Einsatz.BisDatum;
			// TODO: Wenn Flags geändert wurden muss das hier noch rein
			Cdv_Systemereignis mySysErg = new Cdv_Systemereignis("Server-Administrator",
																  DateTime.Now,
																  str_Beschreibung, 
																  Tdv_SystemereignisArt.Einsatzdaten, 
																  true);
			#endregion
			_ObjectManager.EtbEintraege.Speichern(mySysErg);
			return _verw_einsatzverwaltung.Speichern(pin_Einsatz);
		}
	}
}
