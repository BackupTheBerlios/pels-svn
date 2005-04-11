using System;
//ben�tigt f�r: Image
using System.Drawing;
// ben�tigt f�r ArrayList
using System.Collections;

namespace pELS.Client.Etb
{
	// ben�tigt f�r Cst_PortalLogik
	using pELS.Client;
	// ben�tigt f�r: Interface_Portale
	using pELS.APS.Server.Interface;
	// ben�tigt f�r: Cst_Einstellung
	using pELS.Tools.Client;
	// ben�tigt f�r: pELS-Objekte
	using pELS.DV;

	/// <summary>
	/// angefangen by alexG 10.03.05
	/// </summary>
	public class Cst_Etb : Cst_PortalLogik, pELS.GUI.Interface.Isbe
	{
		#region Instanzvariablen
		//H�lt den Namen der Icon Datei fest
		private string _str_iconName = @"SBEImages\etb.jpg";
		//Hier wird die Beschriftung unterhalb des Icons festgehalten
		private string _str_sbeName = "ETB";
		//hier wird die Klassenvariable gehalten, die die User Control enth�lt
		private Cpr_usc_Etb _usc_Etb;

		// das proxy-objekt der Klasse Cap_Etb
		private IPortalLogik_Etb _PortalLogikEtb;

		#region pELS Objekte
		//H�lt die Menge der EtbEintraege zwischen
		public Cdv_EtbEintrag[] _myEtbEintragMenge;
		//H�lt die Menge der Systemereignisse zwischen
		public Cdv_Systemereignis[] _mySystemereignisMenge;
		//H�lt die Menge der Systemereignisse zwischen
		public Cdv_EtbEintragKommentar[] _myEtbKommentarMenge;
		#endregion

		#region pELS Eventhandler
		/// <summary>
		/// Eventhandler f�r ETB-Eintr�ge
		/// </summary>
		private pELS.Events.UpdateEventHandler _ueh_EtbEintraege;
		/// <summary>
		/// Eventhandler f�r Kommentare
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Kommentare;
		#endregion

		bool UpdateErlaubt = true;

		#endregion

		#region Cst_PortalLogik Members
		override protected void SetzeRemotingPfad()
		{
			this._Pfad = "PortalEtb";
		}
		
		override protected void SetzePortalTyp()
		{
			this._PortalTyp = typeof(IPortalLogik_Etb);
		}
		#endregion

		#region Isbe Members
		//Erzeuge ein enstprechende Image Objekt und liefere es zur�ck
		public System.Drawing.Image GetSbeImage()
		{
			System.Reflection.Assembly asm_Sbe;
			//Informationen �ber die ausf�hrende Assembly sammeln
			asm_Sbe = System.Reflection.Assembly.GetExecutingAssembly();
			//Liefere Name der Assembly als AssemblyName
			System.Reflection.AssemblyName asm_SbeName = asm_Sbe.GetName();
			//Speichere den dll Namen im String
			string strAssemblyName = asm_SbeName.Name;
			//Hole Icon aus Datei			
			Image myImage = Image.FromFile(_str_iconName);
			//Gebe myImage zur�ck
			return(myImage);
		}
		
		// Gibt den Namen des SBEs zur�ck
		public String GetSbeName()
		{			
			return this._str_sbeName;
		}
	
		// Gibt das UserControll zur�ck das rechts im Hauptfenster angezeigt wird
		public System.Windows.Forms.UserControl GetSbeUserControl()
		{
			return this._usc_Etb;
		}



		//startet die Anpassung des sbes an die �bergebene Rolle
		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
		{
			_usc_Etb.SetzeRollenRechte(pin_i_aktuelleRolle);			
		}

		#endregion

		#region Konstruktor
		public Cst_Etb(Cst_Einstellung pin_Einstellung) : 
			base(pin_Einstellung)
		{
			// INIT Proxyobjekte
			this._PortalLogikEtb = (IPortalLogik_Etb) this._PortalLogik;

			// INIT Gui
			this._usc_Etb = new Cpr_usc_Etb(this);

			#region F�r pELS-Events registrieren
			// registriere f�r ETB-Eintr�ge
			_ueh_EtbEintraege = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventEtbEintraege));
			this._Portal_Update.RegistriereFuerEtbEintraege(_ueh_EtbEintraege);
			// registriere f�r ETB-Eintragskommentare
			_ueh_Kommentare = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventKommentare));
			this._Portal_Update.RegistriereFuerEtbKommentare(_ueh_Kommentare);
			#endregion

			InitialisiereStartwerte();
		}
		#endregion

		#region Cst_Etb members
		/// <summary>
		/// F�llt alle Datenfelder mit den Startewerten
		/// </summary>
		private void InitialisiereStartwerte()
		{
			this.HoleAlleSystemereignisse();
			this.HoleAlleEtbZusatzEintraege();
			this.HolleAlleEtbKommentare();
		}
		
		/// <summary>
		/// Reagiert auf neue Eintrag ins ETB in der DV
		/// </summary>
		private void BehandleEventEtbEintraege(pELS.Events.UpdateEventArgs pin_e)
		{
			if (UpdateErlaubt)
			{	
				this.HoleAlleEtbZusatzEintraege();
				this.HoleAlleSystemereignisse();
			}
		}
		
		/// <summary>
		/// Reagiert auf neue Kommentare ins ETB in der DV
		/// </summary>
		private void BehandleEventKommentare(pELS.Events.UpdateEventArgs pin_e)
		{
			if (UpdateErlaubt)
			{	
				this.HolleAlleEtbKommentare();
			}
		}
		
		/// <summary>
		/// Erzeugt ein pdf welches alle Daten des ETBs enth�lt
		/// </summary>
		public bool ErzeugeEtb()
		{
			try
			{
				// Weiterleitung der zu verwendenden Vorlage an den Server und R�ckgabe des Reports als Stream
				System.IO.Stream _stream_Report = _PortalLogikEtb.ErzeugeEtb();
				// Auslesen des Streams
				System.IO.StreamReader input = new System.IO.StreamReader(_stream_Report, System.Text.Encoding.BigEndianUnicode);
				// Estellen eines PDFs zum Anzeigen
				System.IO.StreamWriter output = new System.IO.StreamWriter("ETB.pdf", false, System.Text.Encoding.BigEndianUnicode);
				// Umwandeln des Streams in PDF Datei
				output.Write(input.ReadToEnd());
				// Streams schlie�en
				input.Close();
				output.Close();
			
				return true;
			}
			catch(Exception)
			{
				System.Windows.Forms.MessageBox.Show("Fehler auf dem Server: Es konnte keine ODBC Verbindung zur Datenbank hergestellt werden.");
				return false;
			}
		}
		
		/// <summary>
		/// Holt alle Systemereignisse vom Server 
		/// speichert in die Interne Liste 
		/// und l�d diese in die GUI
		/// </summary>
		private void HoleAlleSystemereignisse()
		{
			//Vom Server alle SysErgs holen und in Auswahlliste speichern
			_mySystemereignisMenge = this._PortalLogikEtb.LadeSystemereignisse();
			_usc_Etb.LadeAlleSystemeregnisseInListe(_mySystemereignisMenge);
		}
		
		/// <summary>
		/// Markiert ein ausgew�hltes Systemereignis, so dass es im ETB angezeigt wird
		/// </summary>
		public void MarkiereSystemereignis(Cdv_EtbEintrag pin_neuMarkiertesSE)
		{
			// �ndere Markierung in der Datenbank
			UpdateErlaubt = false;
			_PortalLogikEtb.MarkiereSystemereignis(pin_neuMarkiertesSE);
			UpdateErlaubt = true;
			// Aktualisiere Liste
			this.BehandleEventEtbEintraege(null);
		}

		/// <summary>
		/// Holt alle EtbZusatzEintraege vom Server,
		/// speichert in die Interne Liste &
		/// l�d diese in die GUI 
		/// </summary>
		private void HoleAlleEtbZusatzEintraege()
		{
			//Vom Server alle etbEintraege holen und in Client Liste speichern
			_myEtbEintragMenge = this._PortalLogikEtb.LadeEtbEintraege();			
			this._usc_Etb.LadeAlleZusatzeintraegeInListe(_myEtbEintragMenge);
		}

		/// <summary>
		/// Schreibe Zusatzeintrag in die Datenbank und f�ge es zur GUI hinzu
		/// </summary>
		public void SpeichereEtbZusatzEintrag(Cdv_EtbEintrag pin_neuerZE)
		{
			// Objekt in die Datenbank schreiben, und verhindern das w�hrendessen Updates passieren
			UpdateErlaubt = false;
			Cdv_EtbEintrag neuerZeMitID = _PortalLogikEtb.SpeichereZusatzeintrag(pin_neuerZE);
			UpdateErlaubt = true;
			// Aktualisiere Liste
			this.BehandleEventEtbEintraege(null);
		}

		private void HolleAlleEtbKommentare()
		{
			//Vom Server alle EtbKommentare holen und in Client Liste speichern
			_myEtbKommentarMenge = this._PortalLogikEtb.LadeEtbKommentare();			
		}
		
		/// <summary>
		/// Schreibe Kommentar in die Datenbank und ins ETB
		/// </summary>
		public void SpeichereEtbEintragKommentar(Cdv_EtbEintragKommentar pin_neuerKommentar)
		{
			// Objekt in die Datenbank schreiben
			UpdateErlaubt = false;
			Cdv_EtbEintragKommentar neuerKommentarMitID = _PortalLogikEtb.SpeichereEintragKommentar(pin_neuerKommentar);
			UpdateErlaubt = true;
			// Aktualisiere Liste
			this.BehandleEventKommentare(null);
		}
		#endregion


	}
}
