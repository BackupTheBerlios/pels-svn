using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;

namespace pELS.Server
{
	/// <summary>
	/// Summary description for SplashScreen.
	/// </summary>
	public class Cpr_SplashScreen : System.Windows.Forms.Form
	{
		
		#region eigene Vairablen
		// Threading
		static Cpr_SplashScreen _frm_Splash = null; // das Splash screen
		static Thread _oThread = null;				// Thread für das Splash screen

		// Eín- und Ausblenden(Verblassen) des Splash Screen (Fade in and out).
		private double _d_TransparenzInkrement = .05;	// Wert zum Einblenden
		private double _d_TransparenzDekrement = .08;	// Wert zum Ausblenden
		private const int TIMER_INTERVAL = 50;

		// Status und progress bar
		static string _str_Status;	// Der Status, zeigt an, was das Programm gerade tut
		private double _d_CompletionFraction = 0;	// Wert des progresses
		private Rectangle _rectangle_Progress;	// Der aktuelle Rechteck der Füllung des Progress bar 

		// Progress smoothing
		private double _d_LastCompletionFraction = 0.0;	// das Letzte Fraction
		private double _d_PBInkrementPerTimerInterval = .015;	// Inkrement des Prozents pro Uhrticks

		// Self-calibration support
		private bool _b_FirstLaunch = false;	// Wenn es bereits Eintrag in Registry vorhanden ist, ist false
												// sonst true;
		private DateTime _date_Start;			// Der Zeitpunkt des Starten des Splash screens
		private bool _b_DTSet = false;			// Ob der Startzeitpunkt gesetzt wurde
		private int _i_Index = 1;				// 
		private int _i_AktuellesTicks = 0;		// jede 50ms tickt einmal die Uhr, dieser Wert ist für
												// das wievielte Ticks
		private ArrayList _arraylist_PreviousCompletionFraction;	// Alle Fractions
		private ArrayList _arraylist_AktuelleTimes = new ArrayList();	// Jede verstrichene Zeit  des Codes zwischen 
																		// zwei SetzeStatus Methoden in Millisekunden

		// Variablename des Programms in Registry
		private const string REG_KEY_INITIALIZATION = "Initialization";		// 
		private const string REGVALUE_PB_MILISECOND_INCREMENT = "Increment";	// Werte beim Inkrement
		private const string REGVALUE_PB_PERCENTS = "Percents";					// Prozentwerte
		#endregion

		private System.Windows.Forms.Label lbl_Splash_Status;
		private System.Windows.Forms.Label lbl_Splash_VerbleibendeZeit;
		private System.Windows.Forms.Timer tmr_Splash_Time;
		// der Progress bar (nicht winforms progress, sondern 
		//	ein selbst Erstellter)
		private System.Windows.Forms.Panel pnl_Splash_Status; 
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Cpr_SplashScreen));
			this.lbl_Splash_Status = new System.Windows.Forms.Label();
			this.pnl_Splash_Status = new System.Windows.Forms.Panel();
			this.lbl_Splash_VerbleibendeZeit = new System.Windows.Forms.Label();
			this.tmr_Splash_Time = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// lbl_Splash_Status
			// 
			this.lbl_Splash_Status.BackColor = System.Drawing.Color.Transparent;
			this.lbl_Splash_Status.Location = new System.Drawing.Point(112, 116);
			this.lbl_Splash_Status.Name = "lbl_Splash_Status";
			this.lbl_Splash_Status.Size = new System.Drawing.Size(279, 14);
			this.lbl_Splash_Status.TabIndex = 0;
			this.lbl_Splash_Status.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
			// 
			// pnl_Splash_Status
			// 
			this.pnl_Splash_Status.BackColor = System.Drawing.Color.Transparent;
			this.pnl_Splash_Status.Location = new System.Drawing.Point(112, 138);
			this.pnl_Splash_Status.Name = "pnl_Splash_Status";
			this.pnl_Splash_Status.Size = new System.Drawing.Size(279, 24);
			this.pnl_Splash_Status.TabIndex = 1;
			this.pnl_Splash_Status.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Splash_Status_Paint);
			this.pnl_Splash_Status.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
			// 
			// lbl_Splash_VerbleibendeZeit
			// 
			this.lbl_Splash_VerbleibendeZeit.BackColor = System.Drawing.Color.Transparent;
			this.lbl_Splash_VerbleibendeZeit.Location = new System.Drawing.Point(112, 169);
			this.lbl_Splash_VerbleibendeZeit.Name = "lbl_Splash_VerbleibendeZeit";
			this.lbl_Splash_VerbleibendeZeit.Size = new System.Drawing.Size(279, 16);
			this.lbl_Splash_VerbleibendeZeit.TabIndex = 2;
			this.lbl_Splash_VerbleibendeZeit.Text = "Time remaining";
			this.lbl_Splash_VerbleibendeZeit.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
			// 
			// tmr_Splash_Time
			// 
			this.tmr_Splash_Time.Tick += new System.EventHandler(this.tmr_Splash_Time_Tick);
			// 
			// Cpr_SplashScreen
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.LightGray;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(421, 221);
			this.Controls.Add(this.lbl_Splash_VerbleibendeZeit);
			this.Controls.Add(this.pnl_Splash_Status);
			this.Controls.Add(this.lbl_Splash_Status);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Cpr_SplashScreen";
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
		static public void ZeigeSplashScreen()
		{
			// Make sure it's only launched once.
			// Es kann sein, dass dieser Code hier von mehreren Threads des Programms
			// zugegriffen werden, muss daher Sichergestellt werden, dass _frm_Splash nur einmal initialisiert wird.
			if( _frm_Splash != null )
				return;
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
		static public void SetzeStatus(string newStatus)
		{
			SetzeStatus(newStatus, true);
		}

		// A static method to set the status and optionally update the reference.
		// This is useful if you are in a section of code that has a variable
		// set of status string updates.  In that case, don't set the reference.
		static public void SetzeStatus(string pin_newStatus, bool pin_setzeReference)
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
		#endregion

		// ************ Private methods ************

		// Internal method for setting reference points.
		private void SetReferenceInternal()
		{
			// Entscheidet, Ob das Splash Screen neu gestartet wird
			if( _b_DTSet == false )
			{
				_b_DTSet = true;	// Beim ersten Starten wird der Startzeitpunkt gesetzt
				_date_Start = DateTime.Now;
				ReadIncrements();	// Initialisiere die notwendigen Daten
			}
			double dblMilliseconds = ElapsedMilliSeconds();
			// Wenn das Splash screen neu gestart wird, speichern die verstrichene Zeit vom Starten bis Beendigung
			// der Initialisierung der notwendigen Daten.
			// Sonst speichern die verstrichene Zeit der Ausführung des codes zwischen zwei Aufrufe dieser Methode
			_arraylist_AktuelleTimes.Add(dblMilliseconds); 
			_d_LastCompletionFraction = _d_CompletionFraction;
			// Prüfe ob _arraylist_PreviousCompletionFraction Werte enthält, ist dies der Fall wird
			// es mit Hilfe _i_Index durchlaufen
			if( _arraylist_PreviousCompletionFraction != null && _i_Index < _arraylist_PreviousCompletionFraction.Count )
				_d_CompletionFraction = (double)_arraylist_PreviousCompletionFraction[_i_Index++];
			else	
				// Wenn alle Werte in _arraylist_PreviousCompletionFraction durchlaufen sind, bedeutet es mst. dass
				// das Splash screen seine auszuführende Zeit erreicht.
				_d_CompletionFraction = ( _i_Index > 0 )? 1: 0;
		}

		// Utility function to return elapsed Milliseconds since the 
		// SplashScreen was launched.
		private double ElapsedMilliSeconds()
		{
			TimeSpan ts = DateTime.Now - _date_Start;
			return ts.TotalMilliseconds;
		}

		// Function to read the checkpoint intervals from the previous invocation of the
		// splashscreen from the registry.
		private void ReadIncrements()
		{
			// Fragen Registry ab, ob drin Werte über Inkrement vorhanden sind, in diesem Fall werden
			// die Werte in einer Zeichenkette zurückgeholt, andernfalls wird der Wert über Inkrement der Dafaultwert sein.
			string str_PBIncrementPerTimerInterval = RegistryAccess.GetStringRegistryValue( REGVALUE_PB_MILISECOND_INCREMENT, "0.0015");
			double d_Result;
			// Wenn die vom Registry geholte Zeichenkette gültig ist, konvertieren diese in die Instanzvariable
			if( Double.TryParse(str_PBIncrementPerTimerInterval, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out d_Result) == true )
				_d_PBInkrementPerTimerInterval = d_Result;
			else	// Wenn die Zeichenkette nicht gültig ist, setze die Instanzvariable mit dafault wert. 
				_d_PBInkrementPerTimerInterval = .0015;

			// Fragen Registry ab, ob drin Werte über Perzent vorhanden sind,in diesem Fall werden
			// die Werte in einer Zeichenkette zurückgeholt, andernfalls wird die Information über Prozent leer sein.
			string str_PBPreviousPctComplete = RegistryAccess.GetStringRegistryValue( REGVALUE_PB_PERCENTS, "" );

			// Wenn die zurückgelieferte Zeichenkette nicht leer ist
			if( str_PBPreviousPctComplete != "" )
			{
				// Splitten die Zeichenkette in Array
				string [] str_Timesmenge = str_PBPreviousPctComplete.Split(null);
				_arraylist_PreviousCompletionFraction = new ArrayList();

				for(int i = 0; i < str_Timesmenge.Length; i++ )
				{
					// Konvertieren jedes String in der Array in einen Double Wert und speichen diesen in 
					// _arraylist_PreviousCompletionFraction
					// Falls während des Durchlaufs irgend ein String nicht gültig ist, wird 1.0 in
					// _arraylist_PreviousCompletionFraction gespeichert.
					double d_Val;
					if( Double.TryParse(str_Timesmenge[i], System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out d_Val) )
						_arraylist_PreviousCompletionFraction.Add(d_Val);
					else
						_arraylist_PreviousCompletionFraction.Add(1.0);
				}
			}// Wenn die zurückgelieferte Zeichenkette leer ist.
			else
			{
				_b_FirstLaunch = true;	// zeigt, dass noch nichts über "prozent" im Registry registriert wird
				lbl_Splash_VerbleibendeZeit.Text = "";	
			}
		}

		// Method to store the intervals (in percent complete) from the current invocation of
		// the splash screen to the registry.
		private void StoreIncrements()
		{
			string str_Prozent = "";
			double d_ElapsedMilliseconds = ElapsedMilliSeconds();
			for( int i = 0; i < _arraylist_AktuelleTimes.Count; i++ )
				str_Prozent += ((double)_arraylist_AktuelleTimes[i]/d_ElapsedMilliseconds).ToString("0.####", System.Globalization.NumberFormatInfo.InvariantInfo) + " ";

			RegistryAccess.SetStringRegistryValue( REGVALUE_PB_PERCENTS, str_Prozent );

			_d_PBInkrementPerTimerInterval = 1.0/(double)_i_AktuellesTicks;
			RegistryAccess.SetStringRegistryValue( REGVALUE_PB_MILISECOND_INCREMENT, _d_PBInkrementPerTimerInterval.ToString("#.000000", System.Globalization.NumberFormatInfo.InvariantInfo));
		}

		//********* Event Handlers ************

		// Tick Event handler for the Timer control.  Handle fade in and fade out.  Also
		// handle the smoothed progress bar.
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
			// _d_LastCompletionFraction und _d_CompletionFraction durch SetReferenceInternal() initialisiert
			// wird
			if( _b_FirstLaunch == false && _d_LastCompletionFraction < _d_CompletionFraction )
			{
				_d_LastCompletionFraction += _d_PBInkrementPerTimerInterval;
				// Einstellung des Panels
				int width = (int)Math.Floor(pnl_Splash_Status.ClientRectangle.Width * _d_LastCompletionFraction);
				int height = pnl_Splash_Status.ClientRectangle.Height;
				int x = pnl_Splash_Status.ClientRectangle.X;
				int y = pnl_Splash_Status.ClientRectangle.Y;
				if( width > 0 && height > 0 )
				{
					_rectangle_Progress = new Rectangle( x, y, width, height);
					pnl_Splash_Status.Invalidate(_rectangle_Progress);
					
					//int iSecondsLeft = 1 + (int)(TIMER_INTERVAL * ((1.0 - _d_LastCompletionFraction)/_d_PBInkrementPerTimerInterval)) / 1000;
					// Wieviel Fraktion es noch gibt = 100% - die Letzte Fraktion
					double d_VerbleibendeFraktion = 1.0 - _d_LastCompletionFraction;
					// Soviel Fraktion braucht wieviel mal zu inkrementiert werden
					double d_AnzahlDerInkrements = d_VerbleibendeFraktion / _d_PBInkrementPerTimerInterval;
					// Soviel Imkrements würde wieviel Zeit in mili Sekunde verbrauchen
					int i_BenoetigteMiliSekunden = (int)(TIMER_INTERVAL * d_AnzahlDerInkrements);
					// Umrechnen in Sekunden
					int i_Sekunden = i_BenoetigteMiliSekunden / 1000;
					// Addiere die letzte Sekunde zu verlassen des Programms
					int i_SekundeZuVerlassen = 1 + i_Sekunden;

					if( i_SekundeZuVerlassen == 1 )
						lbl_Splash_VerbleibendeZeit.Text = string.Format( "Verbleibende Zeit noch 1 Sekunde ");
					else
						lbl_Splash_VerbleibendeZeit.Text = string.Format( "Verbleibende Zeit noch {0} Sekunden ", i_SekundeZuVerlassen);

				}
			}
		}

		// Paint the portion of the panel invalidated during the tick event.
		private void pnl_Splash_Status_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if( _b_FirstLaunch == false && e.ClipRectangle.Width > 0 && _i_AktuellesTicks > 1 )
			{
				LinearGradientBrush brBackground = new LinearGradientBrush(_rectangle_Progress, Color.FromArgb(58, 96, 151), Color.FromArgb(181, 237, 254), LinearGradientMode.Horizontal);
				e.Graphics.FillRectangle(brBackground, _rectangle_Progress);
			}
		}

		// Close the form if they double click on it.
		private void SplashScreen_DoubleClick(object sender, System.EventArgs e)
		{
			CloseForm();
		}
	}


	#region Umgebungsvariablen in Registry,
	/// <summary>
	/// A class for managing registry access.
	/// </summary>
	public class RegistryAccess
	{
		private const string SOFTWARE_KEY = "Software";
		private const string COMPANY_NAME = "MyCompany";
		private const string APPLICATION_NAME = "MyApplication";

		// Method for retrieving a Registry Value.
		static public string GetStringRegistryValue(string pin_Key, string pin_defaultValue)
		{
			RegistryKey rk_Company;
			RegistryKey rk_Application;

			// Wenn es in Registry Werte unter den Variablennamen vorhanden sind, liefere sie zurück
			// Sonst liefere den Default Wert zurück
			rk_Company = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, false).OpenSubKey(COMPANY_NAME, false);
			if( rk_Company != null )
			{
				rk_Application = rk_Company.OpenSubKey(APPLICATION_NAME, true);
				if( rk_Application != null )
				{
					foreach(string str_Key in rk_Application.GetValueNames())
					{
						if( str_Key == pin_Key )
						{
							return (string)rk_Application.GetValue(str_Key);
						}
					}
				}
			}
			return pin_defaultValue;
		}

		// Method for storing a Registry Value.
		static public void SetStringRegistryValue(string pin_Key, string pin_stringValue)
		{
			RegistryKey rk_Software;
			RegistryKey rk_Company;
			RegistryKey rk_Application;

			rk_Software = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, true);
			rk_Company = rk_Software.CreateSubKey(COMPANY_NAME);
			if( rk_Company != null )
			{
				rk_Application = rk_Company.CreateSubKey(APPLICATION_NAME);
				if( rk_Application != null )
				{
					rk_Application.SetValue(pin_Key, pin_stringValue);
				}
			}
		}

		static public void LoescheKeyRegistry(string pin_Key)
		{
			RegistryKey rk_Software;
			RegistryKey rk_Company;
			RegistryKey rk_Application;

			rk_Company = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, true).OpenSubKey(COMPANY_NAME, true);
			if( rk_Company != null )
			{
				rk_Application = rk_Company.OpenSubKey(APPLICATION_NAME, true);
				if( rk_Application != null )
				{
					rk_Application.DeleteSubKey(pin_Key);
				}
			}
		}
		#endregion
	}
}
