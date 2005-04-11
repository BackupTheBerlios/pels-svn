using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;

namespace pELS.Tools
{
	/// <summary>
	/// Summary description for SplashScreen.
	/// </summary>
	public class Cpr_SplashScreen : System.Windows.Forms.Form
	{
		
		#region eigene Vairablen
		/* Threading 
		 */
		// das Splash screen
		static Cpr_SplashScreen _frm_Splash = null; 
		// Thread für das Splash screen
		static Thread _oThread = null;				
		// Endscheidet, ob das Splash Screen für den Server oder für das Client ist
		static bool _istServer;		

		/* Eín- und Ausblenden(Verblassen) des Splash Screen (Fade in and out). 
		 * */
		// Wert zum Einblenden
		private double _d_TransparenzInkrement = .05;	
		// Wert zum Ausblenden
		private double _d_TransparenzDekrement = .08;	
		// Das Zeitinterval zwischen Uhrticks
		private const int TIMER_INTERVAL = 50;

		/* Status und progress bar
		 * */
		// Der Status, zeigt an, in welchem Prozess sich das Programm befindet
		static string _str_Status;	
		// Wert des progresses
		private double _d_Ausfuehrungsfraktion = 0;
		// Der aktuelle Rechteck der Füllung des Progress bar
		private Rectangle _rectangle_Progress;	

		/* Progress smoothing
		 * */
		// das Letzte Fraction
		private double _d_LetzteAusfuehrungsfraktion = 0.0;	
		// Inkrement des Prozents pro Uhrticks
		private double _d_PBInkrementPerTimerInterval = .015;	

		/* Self-calibration support
		 * */
		// Ob es sich hier um den ersten Aufruf der Methode SetzeStatus(...) handelt
		// Wenn es bereits Eintrag in Registry vorhanden ist, ist false. sonst true;
		private bool _b_DieErsteEinfuehrung = false;	
		// Der Zeitpunkt des ersten Aufruf der Methode SetzeStatus(..)
		private DateTime _date_Start;			
		// Ob der Startzeitpunkt _date_Start gesetzt wurde
		private bool _b_DTSet = false;			
		// Die Zeiten der Ausführung aller Codes, die zwischen Aufrufen der Methode SetzeStatus() liegen,
		// werden in Array gespeichert. _i_Index ist für das Durchlaufen aller dieser Zeiten zuständig
		private int _i_Index = 1;				
		// jede 50ms tickt einmal die Uhr, dieser Wert ist für das wievielte Ticks
		private int _i_AktuellesTicks = 0;		

		private ArrayList _arraylist_DasLetzteFraktionProtokol;	// Alle Fractions
		// Jede verstrichene Zeit  des Codes zwischen zwei Aufrufen der Methode SetzeStatus() in Millisekunden
		// Das Array wird bei dem Beenden des Splash Screen ins Registry gepeichert, damit es
		// beim nächsten Aufruf abgelesen und aus ihm die relevanten Werte berechnet werden können.
		private ArrayList _arraylist_AktuelleTimes = new ArrayList();	

		
		/* Variablename des Programms in Registry
		 * */
		private const string REG_KEY_INITIALIZATION = "Initialization";		// 
		// Registry Variablename, diese Variable enthält Wert von Inkrement
		private const string REGVALUE_PB_MILISECOND_INCREMENT = "Inkrement";
		// Registry Variablename, diese Variable enthält Werte von Prozent der Ausführung der Codes,
		// die die Methode SetzeStatus(...) aufrufen
		private const string REGVALUE_PB_PERCENTS = "Prozentzahlen";			

		/* Konstant
		 * */
		private const string HINTERGRUNDBILD = "Splash.jpg";

		#endregion

		private System.Windows.Forms.Label lbl_Splash_Status;
		private System.Windows.Forms.Timer tmr_Splash_Time;
		// der Progress bar (nicht winforms progress, sondern 
		//	ein selbst Erstellter)
		private System.Windows.Forms.Panel pnl_Splash_Status;
		private System.Windows.Forms.Label lbl_Splash_VerbleibendeZeit; 
		private System.ComponentModel.IContainer components;

		#region Constructor
		
		/// <summary>
		/// Constructor
		/// </summary>
		public Cpr_SplashScreen()
		{
			InitializeComponent();
			// Einstellung der Transparenz
			this.Opacity = .00;

			tmr_Splash_Time.Interval = TIMER_INTERVAL;
			tmr_Splash_Time.Start();

			//TODO: Das Backgroundbild ist evtl. neu zu ersetzen
			this.SetzeHintergrundbild();
			this.ClientSize = this.BackgroundImage.Size;

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.lbl_Splash_Status = new System.Windows.Forms.Label();
			this.pnl_Splash_Status = new System.Windows.Forms.Panel();
			this.lbl_Splash_VerbleibendeZeit = new System.Windows.Forms.Label();
			this.tmr_Splash_Time = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// lbl_Splash_Status
			// 
			this.lbl_Splash_Status.BackColor = System.Drawing.Color.White;
			this.lbl_Splash_Status.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lbl_Splash_Status.Location = new System.Drawing.Point(0, 215);
			this.lbl_Splash_Status.Name = "lbl_Splash_Status";
			this.lbl_Splash_Status.Size = new System.Drawing.Size(360, 20);
			this.lbl_Splash_Status.TabIndex = 0;
			this.lbl_Splash_Status.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
			// 
			// pnl_Splash_Status
			// 
			this.pnl_Splash_Status.BackColor = System.Drawing.Color.White;
			this.pnl_Splash_Status.Location = new System.Drawing.Point(0, 180);
			this.pnl_Splash_Status.Name = "pnl_Splash_Status";
			this.pnl_Splash_Status.Size = new System.Drawing.Size(360, 25);
			this.pnl_Splash_Status.TabIndex = 1;
			this.pnl_Splash_Status.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Splash_Status_Paint);
			this.pnl_Splash_Status.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
			// 
			// lbl_Splash_VerbleibendeZeit
			// 
			this.lbl_Splash_VerbleibendeZeit.BackColor = System.Drawing.Color.White;
			this.lbl_Splash_VerbleibendeZeit.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.lbl_Splash_VerbleibendeZeit.Location = new System.Drawing.Point(0, 160);
			this.lbl_Splash_VerbleibendeZeit.Name = "lbl_Splash_VerbleibendeZeit";
			this.lbl_Splash_VerbleibendeZeit.Size = new System.Drawing.Size(360, 15);
			this.lbl_Splash_VerbleibendeZeit.TabIndex = 2;
			this.lbl_Splash_VerbleibendeZeit.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
			// 
			// tmr_Splash_Time
			// 
			this.tmr_Splash_Time.Tick += new System.EventHandler(this.tmr_Splash_Time_Tick);
			// 
			// Cpr_SplashScreen
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(360, 240);
			this.Controls.Add(this.lbl_Splash_VerbleibendeZeit);
			this.Controls.Add(this.pnl_Splash_Status);
			this.Controls.Add(this.lbl_Splash_Status);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Cpr_SplashScreen";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SplashScreen";
			this.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
			this.ResumeLayout(false);

		}
		#endregion
		#endregion


		#region static Methoden
		// ************* Static Methods *************** //

		// Erzeuge ein Thread, das für die Initialisierung des Splash Screen und
		// dessen Ausführung zuständig ist.
		static public void ZeigeSplashScreen(bool pin_istServer)
		{
			// Make sure it's only launched once.
			// Es kann sein, dass dieser Code hier von mehreren Threads des Programms
			// zugegriffen werden, muss daher Sichergestellt werden, dass _frm_Splash nur einmal initialisiert wird.
			if( _frm_Splash != null )
				return;
			// unterscheide, ob es sich um Server oder Client handelt
			_istServer = pin_istServer;
			// Erzeuge ein Thread, in dem das Splash Screen initialisiert und ausgeführt wird.
			_oThread = new Thread( new ThreadStart(Cpr_SplashScreen.ZeigeForm));
			_oThread.IsBackground = true;
			_oThread.ApartmentState = ApartmentState.STA;
			_oThread.Start();
		}

		// Die Initialisierung und Ausführung des Splash Screen. Dies ist im Übergeordneten Programm hier
		// im eigenen Thread passiert.
		static private void ZeigeForm()
		{
			_frm_Splash = new Cpr_SplashScreen();
			Application.Run(_frm_Splash);
		}

		// A static method to close the SplashScreen
		static public void CloseForm()
		{
			if( _frm_Splash != null && _frm_Splash.IsDisposed == false )
			{
				// Make it start going away.
				_frm_Splash._d_TransparenzInkrement = - _frm_Splash._d_TransparenzDekrement;
			}
			_oThread = null;	// we don't need these any more.
			_frm_Splash = null;
		}

		// A property returning the splash screen instance
		static public Cpr_SplashScreen SplashForm 
		{
			get{ return _frm_Splash;} 
		}


		// A static method to set the status and update the reference.
		static public void SetzeStatus(string pin_NeuesStatus)
		{
			
			SetzeStatus(pin_NeuesStatus,true);
		}

		// A static method to set the status and optionally update the reference.
		// This is useful if you are in a section of code that has a variable
		// set of status string updates.  In that case, don't set the reference.
		static public void SetzeStatus(string pin_newStatus,bool pin_setzeReference)
		{
			_str_Status = pin_newStatus;
			if( _frm_Splash == null )
				return;
			if( pin_setzeReference )
				_frm_Splash.SetReferenceInternal();
		}

		// Static method called from the initializing application to 
		// give the splash screen reference points.  Not needed if
		// you are using a lot of status strings.
		static public void SetReferencePoint()
		{
			if( _frm_Splash == null )
				return;
			_frm_Splash.SetReferenceInternal();

		}

		static public bool IstServer
		{
			get{return _istServer;}
			set{_istServer = value;}
		}
		#endregion

		#region private Methoden
		// ************ Private methods ************
		/// <summary>
		/// Setze das Hintergrundbild
		/// </summary>
		private void SetzeHintergrundbild()
		{
			Image im_bild;
			//default.jpg aus laden
			System.Reflection.Assembly asm_Assembly;
			//Informationen über die ausführende Assembly sammeln
			asm_Assembly = System.Reflection.Assembly.GetExecutingAssembly();
			//Hole Name der Assembly als AssemblyName
			System.Reflection.AssemblyName asm_SbeName = asm_Assembly.GetName();
			//Speichere den dll Namen im String
			string strAssemblyName = asm_SbeName.Name;
			//Erstelle ein Stream, aus dem die Hintergrundbild Daten gelesen werden
			System.IO.Stream s = asm_Assembly.GetManifestResourceStream(strAssemblyName + "." + HINTERGRUNDBILD);
			//Lese die Icon Daten aus dem Stream
			im_bild = Image.FromStream(s);
			this.BackgroundImage = im_bild;
		
		}


		/// <summary>
		/// Setze den Startzeitpunkt, falls dieser noch nicht gesetzt wird
		/// Speichere die verstrichene Zeit vom letzten Aufruf bis zum aktuellen Aufruf dieser Methode 
		/// Setze Instanzvariablen mit Werten aus Registry, 
		/// die für die Einstellung von Progress bar und die Berechnung der verbleibenden Zeit wichtig
		/// sind. 
		/// </summary>
		private void SetReferenceInternal()
		{
			// Entscheidet, Ob das Splash Screen neu gestartet wird
			if( _b_DTSet == false )
			{
				_b_DTSet = true;	// Beim ersten Starten wird der Startzeitpunkt gesetzt
				_date_Start = DateTime.Now;
				HoleInkrements();	// Initialisiere die notwendigen Daten
			}
			double d_MilliSekunden = VerstricheneMilliSekunden();
			// Wenn das Splash screen neu gestart wird, speichern die verstrichene Zeit vom Starten bis Beendigung
			// der Initialisierung der notwendigen Daten.
			// Sonst speichern die verstrichene Zeit der Ausführung des codes zwischen zwei Aufrufe dieser Methode
			_arraylist_AktuelleTimes.Add(d_MilliSekunden); 
			_d_LetzteAusfuehrungsfraktion = _d_Ausfuehrungsfraktion;
			// Prüfe ob _arraylist_DasLetzteFraktionProtokol Werte enthält, ist dies der Fall wird
			// es mit Hilfe _i_Index durchlaufen
			if( _arraylist_DasLetzteFraktionProtokol != null && _i_Index < _arraylist_DasLetzteFraktionProtokol.Count )
				_d_Ausfuehrungsfraktion = (double)_arraylist_DasLetzteFraktionProtokol[_i_Index++];
			else	
				// Wenn alle Werte in _arraylist_DasLetzteFraktionProtokol durchlaufen sind, bedeutet es mst. dass
				// das Splash screen seine auszuführende Zeit erreicht.
				_d_Ausfuehrungsfraktion = ( _i_Index > 0 )? 1: 0;
		}

		/// <summary>
		/// Liefere die verstrichene Zeit vom ersten Aufruf des SetzeStatus() Methode bis zum aktuellen Zeitpunkt
		/// in Mili Sekunden
		/// </summary>
		private double VerstricheneMilliSekunden()
		{
			TimeSpan ts = DateTime.Now - _date_Start;
			return ts.TotalMilliseconds;
		}


		/// <summary>
		/// Funktion zum Holen die checkpoint intervals vom letzten Aufruf dieses Splash Screen
		/// aus dem Registry
		/// </summary>
		private void HoleInkrements()
		{
			// Fragen Registry ab, ob drin Werte über Inkrement vorhanden sind, in diesem Fall werden
			// die Werte in einer Zeichenkette zurückgeholt, andernfalls wird der Wert über Inkrement der Dafaultwert sein.
			string str_PBIncrementPerTimerInterval = RegistryAccess.GetStringRegistryValue( REGVALUE_PB_MILISECOND_INCREMENT, "0.0015", _istServer);
			double d_Result;
			// Wenn die vom Registry geholte Zeichenkette gültig ist, konvertieren diese in die Instanzvariable
			if( Double.TryParse(str_PBIncrementPerTimerInterval, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out d_Result) == true )
				_d_PBInkrementPerTimerInterval = d_Result;
			else	// Wenn die Zeichenkette nicht gültig ist, setze die Instanzvariable mit dafault wert. 
				_d_PBInkrementPerTimerInterval = .0015;

			// Fragen Registry ab, ob drin Werte über Perzent vorhanden sind,in diesem Fall werden
			// die Werte in einer Zeichenkette zurückgeholt, andernfalls wird die Information über Prozent leer sein.
			string str_PBPreviousPctComplete = RegistryAccess.GetStringRegistryValue( REGVALUE_PB_PERCENTS, "" ,_istServer);

			// Wenn die zurückgelieferte Zeichenkette nicht leer ist
			if( str_PBPreviousPctComplete != "" )
			{
				// Splitten die Zeichenkette in Array
				string [] str_Timesmenge = str_PBPreviousPctComplete.Split(null);
				_arraylist_DasLetzteFraktionProtokol = new ArrayList();

				for(int i = 0; i < str_Timesmenge.Length; i++ )
				{
					// Konvertieren jedes String in der Array in einen Double Wert und speichen diesen in 
					// _arraylist_DasLetzteFraktionProtokol
					// Falls während des Durchlaufs irgend ein String nicht gültig ist, wird 1.0 in
					// _arraylist_DasLetzteFraktionProtokol gespeichert.
					double d_Val;
					if( Double.TryParse(str_Timesmenge[i], System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out d_Val) )
						_arraylist_DasLetzteFraktionProtokol.Add(d_Val);
					else
						_arraylist_DasLetzteFraktionProtokol.Add(1.0);
				}
			}// Wenn die zurückgelieferte Zeichenkette leer ist.
			else
			{
				_b_DieErsteEinfuehrung = true;	// zeigt, dass noch nichts über "prozent" im Registry registriert wird
				lbl_Splash_VerbleibendeZeit.Text = "";	
			}
		}


		/// </summary>
		/// Funktion zum registrieren die checkpoint intervals vom letzten Aufruf dieses Splash Screen
		/// ins Registry
		/// </summary>
		private void StoreIncrements()
		{
			string str_Prozent = "";
			double d_VerstricheneMilliSekunden = VerstricheneMilliSekunden();
			for( int i = 0; i < _arraylist_AktuelleTimes.Count; i++ )
				str_Prozent += ((double)_arraylist_AktuelleTimes[i]/d_VerstricheneMilliSekunden).ToString("0.####", System.Globalization.NumberFormatInfo.InvariantInfo) + " ";

			RegistryAccess.SetStringRegistryValue( REGVALUE_PB_PERCENTS, str_Prozent, _istServer );

			_d_PBInkrementPerTimerInterval = 1.0/(double)_i_AktuellesTicks;
			RegistryAccess.SetStringRegistryValue( REGVALUE_PB_MILISECOND_INCREMENT, _d_PBInkrementPerTimerInterval.ToString("#.000000", System.Globalization.NumberFormatInfo.InvariantInfo),_istServer);
		}
		#endregion

		#region event handlers
		//********* Event Handlers ************

		/// <summary>
		/// Uhrtick Eventhandler für Timer Control.
		/// Behandelt das Ein- und Ausblenden sowie der smoothed progress
		/// </summary>
		private void tmr_Splash_Time_Tick(object sender, System.EventArgs e)
		{
			lbl_Splash_Status.Text = _str_Status;
			// Einstellung der Transparenzeingenschaft
			// Ob dieser Wert initialisiert wurde
			if( _d_TransparenzInkrement > 0 )
			{
				_i_AktuellesTicks++;
				if( this.Opacity < 1 )
					this.Opacity += _d_TransparenzInkrement;
			}
				//
			else
			{
				if( this.Opacity > 0 )
					this.Opacity += _d_TransparenzInkrement;
				else
				{
						// Wenn nicht , rechne neue Werte und registriere diese in REgistry
						StoreIncrements();
						this.Close();
						Debug.WriteLine("Called this.Close()");
				}
			} 
			// Wenn Werte über "Prozent" im Registry vorhanden sind, und die Werte von
			// _d_LetzteAusfuehrungsfraktion und _d_Ausfuehrungsfraktion durch SetReferenceInternal() initialisiert
			// wird
			if( _b_DieErsteEinfuehrung == false && _d_LetzteAusfuehrungsfraktion < _d_Ausfuehrungsfraktion )
			{
				_d_LetzteAusfuehrungsfraktion += _d_PBInkrementPerTimerInterval;
				// Einstellung des Panels
				int width = (int)Math.Floor(pnl_Splash_Status.ClientRectangle.Width * _d_LetzteAusfuehrungsfraktion);
				int height = pnl_Splash_Status.ClientRectangle.Height;
				int x = pnl_Splash_Status.ClientRectangle.X;
				int y = pnl_Splash_Status.ClientRectangle.Y;
				if( width > 0 && height > 0 )
				{
					_rectangle_Progress = new Rectangle( x, y, width, height);
					pnl_Splash_Status.Invalidate(_rectangle_Progress);
					
					//int iSecondsLeft = 1 + (int)(TIMER_INTERVAL * ((1.0 - _d_LetzteAusfuehrungsfraktion)/_d_PBInkrementPerTimerInterval)) / 1000;
					// Wieviel Fraktion es noch gibt = 100% - die Letzte Fraktion
					double d_VerbleibendeFraktion = 1.0 - _d_LetzteAusfuehrungsfraktion;
					// Soviel Fraktion braucht wieviel mal zu inkrementiert werden
					double d_AnzahlDerInkrements = d_VerbleibendeFraktion / _d_PBInkrementPerTimerInterval;
					// Soviel Imkrements würde wieviel Zeit in mili Sekunde verbrauchen
					int i_BenoetigteMiliSekunden = (int)(TIMER_INTERVAL * d_AnzahlDerInkrements);
					// Umrechnen in Sekunden
					int i_Sekunden = i_BenoetigteMiliSekunden / 1000;
					// Addiere die letzte Sekunde zu verlassen des Programms
					int i_SekundeZuVerlassen = 1 + i_Sekunden;

					if( i_SekundeZuVerlassen == 1 )
						lbl_Splash_VerbleibendeZeit.Text = string.Format( "Noch 1 Sekunde ");
					else
						lbl_Splash_VerbleibendeZeit.Text = string.Format( "Noch {0} Sekunden ", i_SekundeZuVerlassen);

				}
			}
		}


		/// <summary>
		/// Paint die Portion von Panel invalidated während tick event
		/// </summary>
		private void pnl_Splash_Status_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if( _b_DieErsteEinfuehrung == false && e.ClipRectangle.Width > 0 && _i_AktuellesTicks > 1 )
			{
				LinearGradientBrush brBackground = new LinearGradientBrush(_rectangle_Progress, Color.FromArgb(58, 96, 151), Color.FromArgb(181, 237, 254), LinearGradientMode.Horizontal);
				e.Graphics.FillRectangle(brBackground, _rectangle_Progress);
			}
		}


		/// <summary>
		/// Schließe die Splash Screen Form, wenn darauf doppelt geclickt wird
		/// </summary>
		private void SplashScreen_DoubleClick(object sender, System.EventArgs e)
		{
			CloseForm();
		}
		#endregion 
	}

	#region Umgebungsvariablen in Registry,
	/// <summary>
	/// die Klasse für das Management von registry access.
	/// </summary>
	public class RegistryAccess
	{
		private const string SOFTWARE_KEY = "Software";
		private const string COMPANY_NAME = "pELSGroup";
		private const string APPLICATION_NAME = "PELS";
		private const string APPLICATION_SERVER = "Server";
		private const string APPLICATION_CLIENT = "Client";

		/// <summary>
		/// An Hand des übergebenen Parameter liefert den Namen der Variable aus Registry zurück,
		/// die relevanten Informationen enthält.  
		/// </summary>
		static public string HoleSBENamen(bool pin_istServer)
		{
			if(pin_istServer == true)
				return APPLICATION_SERVER;
			else
				return APPLICATION_CLIENT;
		}

		/// <summary>
		/// Methode für Abfrage
		/// </summary>
		static public string GetStringRegistryValue(string pin_Key, string pin_defaultValue,bool pin_istServer)
		{
			RegistryKey rk_Company;
			RegistryKey rk_Application;
			RegistryKey rk_SBEName;
			string str_SBEName = HoleSBENamen(pin_istServer);

			// Wenn es in Registry Werte unter den Variablennamen vorhanden sind, liefere sie zurück
			// Sonst liefere den Default Wert zurück
			rk_Company = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, false).OpenSubKey(COMPANY_NAME, false);
			if( rk_Company != null )
			{
				rk_Application = rk_Company.OpenSubKey(APPLICATION_NAME, true);				
				if( rk_Application != null )
				{
					rk_SBEName = rk_Application.OpenSubKey(str_SBEName, true);				

					if(rk_SBEName != null)
					{
						foreach(string str_Key in rk_SBEName.GetValueNames())
						{
							if( str_Key == pin_Key )
							{
								return (string)rk_SBEName.GetValue(str_Key);
							}
						}
					}
				}
			}
			return pin_defaultValue;
		}

		/// <summary>
		/// Methode für Registrierung
		/// </summary>
		static public void SetStringRegistryValue(string pin_Key, string pin_stringValue, bool pin_istServer)
		{
			RegistryKey rk_Software;
			RegistryKey rk_Company;
			RegistryKey rk_Application;
			RegistryKey rk_SBEName;
			string str_SBEName = HoleSBENamen(pin_istServer);

			rk_Software = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, true);
			rk_Company = rk_Software.CreateSubKey(COMPANY_NAME);
			if( rk_Company != null )
			{
				rk_Application = rk_Company.CreateSubKey(APPLICATION_NAME);
				if( rk_Application != null )
				{
					rk_SBEName = rk_Application.CreateSubKey(str_SBEName);
					rk_SBEName.SetValue(pin_Key, pin_stringValue);
				}
			}
		}
		/// <summary>
		/// Methode für Löschen
		/// </summary>
		static public void LoescheKeyRegistry(string pin_Key, bool pin_istServer)
		{
			
			RegistryKey rk_Company;
			RegistryKey rk_Application;
			RegistryKey rk_SBEName;
			string str_SBEName = HoleSBENamen(pin_istServer);
	
			rk_Company = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, true).OpenSubKey(COMPANY_NAME, true);
			if( rk_Company != null )
			{
				rk_Application = rk_Company.OpenSubKey(APPLICATION_NAME, true);
				if( rk_Application != null )
				{
					rk_SBEName = rk_Application.OpenSubKey(str_SBEName,true);
					rk_SBEName.DeleteSubKey(pin_Key);
				}
			}
		}
		#endregion
	}
}
