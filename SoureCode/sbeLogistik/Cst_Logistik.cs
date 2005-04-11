using System;
//ben�tigt f�r: Image
using System.Drawing;
// ben�tigt f�r ArrayList
using System.Collections;

namespace pELS.Client.Logistik
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
	/// Summary description for Cst_Logistik.
	/// </summary>
	public class Cst_Logistik : Cst_PortalLogik, pELS.GUI.Interface.Isbe
	{
		#region Variablen
		#region ISBE
		/// <summary>
		/// H�lt den Namen der Icon Datei fest
		/// </summary>
		private string _strIconName = @"SBEImages\logistik.JPG";
		/// <summary>
		/// Hier wird die Beschriftung unterhalb des Icons festgehalten
		/// </summary>
		private string _strSbeName = "Logistik";
		#endregion

		#region Cst_Logistik allg.
		/// <summary>
		/// hier wird die Klassenvariable gehalten, die die User Control enth�lt
		/// </summary>
		private Cpr_usc_Logistik _usc_Logistik;
		/// <summary>
		/// das proxy-objekt der Klasse Cap_Funk 
		/// </summary>
		private IPortalLogik_Logistik _PortalLogikLogistik;
		#endregion

		#region EventHandler
		/// <summary>
		/// eventhandler f�r Einheiten
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Einheiten;
		/// <summary>
		/// eventhandler f�r KFZ
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_KFZ;
		/// <summary>
		/// eventhandler f�r Helfer
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Helfer;
		/// <summary>
		/// eventhandler f�r Material
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Material;
		/// <summary>
		/// eventhandler f�r Verbrauchsg�ter
		/// </summary>
		private	pELS.Events.UpdateEventHandler _ueh_Verbrauchsgueter;
		/// <summary>
		/// Delegate f�r die Aktualisierung der TreeViews durch den UI-Thread
		/// </summary>
		private delegate void UpdateTreeDelegate(); 
		#endregion

		#region pELSObjekt-Speicher
		/// <summary>
		/// enth�lt alle Einheiten
		/// </summary>
		public ArrayList _AlleEinheiten;
		/// <summary>
		/// enth�lt alle Helfer
		/// </summary>
		public ArrayList _AlleHelfer;
		/// <summary>
		/// enth�lt alle KFZ
		/// </summary>
		public ArrayList _AlleKFZ;
		/// <summary>
		/// enth�lt alle Verbrauchsg�ter
		/// </summary>
		public ArrayList _AlleVerbrauchsgueter;
		/// <summary>
		/// enth�lt alle Materialien
		/// </summary>
		public ArrayList _AlleMaterialien;
		
		#endregion

		#endregion

		#region Konstruktor
		public Cst_Logistik(Cst_Einstellung pin_Einstellung) : 
			base(pin_Einstellung)
		{
			// initialisiere Proxyobjekt
			this._PortalLogikLogistik = (IPortalLogik_Logistik) this._PortalLogik;

			#region UpdateEvent registrieren
			// registriere f�r Einheiten
			_ueh_Einheiten = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventEinheiten));
			this._Portal_Update.RegistriereFuerEinheit(_ueh_Einheiten);
			// registriere f�r Helfer
			_ueh_Helfer = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventHelfer));
			this._Portal_Update.RegistriereFuerHelfer(_ueh_Helfer);
			// registriere f�r KFZ
			_ueh_KFZ = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventKFZ));
			this._Portal_Update.RegistriereFuerKfZ(_ueh_KFZ);
			// registriere f�r Material
			_ueh_Material = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventMaterial));
			this._Portal_Update.RegistriereFuerMaterial(_ueh_Material);
			// registriere f�r Verbrauchsg�ter
			_ueh_Verbrauchsgueter = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventVerbrauchsgut));
			this._Portal_Update.RegistriereFuerVerbrauchsgut(_ueh_Verbrauchsgueter);
			#endregion

			InitialisiereStartwerte();
			// INIT Gui
			this._usc_Logistik = new Cpr_usc_Logistik(this);
		}
		#endregion

		#region Cst_Logistik members
		/// <summary>
		/// L�dt beim initialisieren alle Meldungen, Auftr�ge und Termine
		/// </summary>
		private void InitialisiereStartwerte()
		{
			_AlleEinheiten = new ArrayList(_PortalLogikLogistik.HoleAlleEinheiten());
			_AlleHelfer = new ArrayList(_PortalLogikLogistik.HoleAlleHelfer());
			_AlleKFZ = new ArrayList(_PortalLogikLogistik.HoleAlleKFZ());
			_AlleMaterialien = new ArrayList(_PortalLogikLogistik.HoleAlleMaterialien());
			_AlleVerbrauchsgueter = new ArrayList(_PortalLogikLogistik.HoleAlleVerbrauchsgueter());
		}

		/// <summary>
		/// ermittelt mit Hilfe der �bergebenen IDs die entsprechenden Objekte
		/// und gibt diese zur�ck
		/// </summary>
		/// <param name="pin_EmpfaengerIDMenge">IDs</param>
		/// <param name="pout_einheitenMenge">Menge aller Einheiten</param>
		/// <param name="pout_helfermenge">Menge aller Helfer</param>
		/// <param name="pout_kfzMenge">Menge aller KFZs</param>
		public void ID2Kraft(
			int[] pin_EmpfaengerIDMenge, 
			out ArrayList pout_einheitenMenge, 
			out ArrayList pout_helfermenge, 
			out ArrayList pout_kfzMenge)
		{
			pout_einheitenMenge = null;
			pout_helfermenge = null;
			pout_kfzMenge = null;
			if(pin_EmpfaengerIDMenge.Length != 0)
			{
				ArrayList EinheitenMenge = new ArrayList();
				ArrayList HelferMenge = new ArrayList();
				ArrayList KFZMenge = new ArrayList();
				foreach(int ID in pin_EmpfaengerIDMenge)
				{
					foreach(Cdv_Einheit Einheit in _AlleEinheiten)
					{
						if(Einheit.ID == ID)
							EinheitenMenge.Add(Einheit);
					}
					foreach(Cdv_Helfer Helfer in _AlleHelfer)
					{
						if(Helfer.ID == ID)
							HelferMenge.Add(Helfer);
					}
					foreach(Cdv_KFZ KFZ in _AlleKFZ)
					{
						if(KFZ.ID == ID)
							KFZMenge.Add(KFZ);
					}
				}
				pout_einheitenMenge = EinheitenMenge;
				pout_helfermenge = HelferMenge;
				pout_kfzMenge = KFZMenge;
			}
		}


		/// <summary>
		/// liefert das Gut mit der entsprechenden ID
		/// liefert NULL, falls kein Objekt gefunden wurde
		/// </summary>
		/// <param name="pin_ID"></param>
		/// <returns></returns>
		public Cdv_Gut ID2Gut(int pin_ID)
		{
			foreach(Cdv_Gut Gut in _AlleMaterialien)
			{
				if(Gut.ID == pin_ID)
					return Gut;
			}
			foreach(Cdv_Gut Gut in _AlleVerbrauchsgueter)
			{
				if(Gut.ID == pin_ID)
					return Gut;
			}
			return null;
		}

		private void BehandleEventEinheiten(pELS.Events.UpdateEventArgs pin_e)
		{
			Cdv_Einheit[] tmp = _PortalLogikLogistik.HoleAlleEinheiten();
			_AlleEinheiten = new ArrayList(tmp);
			_usc_Logistik.BeginInvoke(new UpdateTreeDelegate(_usc_Logistik.AktualisiereTreeViews));
		}

		private void BehandleEventHelfer(pELS.Events.UpdateEventArgs pin_e)
		{
			Cdv_Helfer[] tmp = _PortalLogikLogistik.HoleAlleHelfer();
			_AlleHelfer = new ArrayList(tmp);
			_usc_Logistik.BeginInvoke(new UpdateTreeDelegate(_usc_Logistik.AktualisiereTreeViews));
		}
		
		private void BehandleEventKFZ(pELS.Events.UpdateEventArgs pin_e)
		{
			Cdv_KFZ[] tmp = _PortalLogikLogistik.HoleAlleKFZ();
			_AlleKFZ = new ArrayList(tmp);
			_usc_Logistik.BeginInvoke(new UpdateTreeDelegate(_usc_Logistik.AktualisiereTreeViews));
		}

		private void BehandleEventMaterial(pELS.Events.UpdateEventArgs pin_e)
		{
			
			Cdv_Material[] tmp = _PortalLogikLogistik.HoleAlleMaterialien();
			_AlleMaterialien = new ArrayList(tmp);
			_usc_Logistik.LadeMaterial();
		}
		
		private void BehandleEventVerbrauchsgut(pELS.Events.UpdateEventArgs pin_e)
		{
			Cdv_Verbrauchsgut[] tmp = _PortalLogikLogistik.HoleAlleVerbrauchsgueter();
			_AlleVerbrauchsgueter = new ArrayList(tmp);
			_usc_Logistik.LadeVerbrauchsgueter();
		}


		public void SpeichereGut(Cdv_Gut pin_Gut)
		{
			this._PortalLogikLogistik.SpeichereGut(pin_Gut);

			if (pin_Gut is Cdv_Verbrauchsgut)
			{
				this.WerfeSystemereignis(pin_Gut as Cdv_Verbrauchsgut);
			}
		}

		public void SpeichereMaterialuebergabe(Cdv_Materialuebergabe pin_Materialuebergabe)
		{
			this._PortalLogikLogistik.MaterialUebergabe(pin_Materialuebergabe);
			this.WerfeSystemereignis(pin_Materialuebergabe);
		}

		public ArrayList HoleAlleMaterialien(int pin_BesitzerID)
		{
			int[] MatIDs = _PortalLogikLogistik.HoleAlleMaterialIDs(pin_BesitzerID);
			ArrayList pout_Materialien = new ArrayList();
			foreach(Cdv_Material Material in _AlleMaterialien)
			{
				foreach(int ID in MatIDs)
				{
					if (Material.ID == ID) pout_Materialien.Add(Material);
				}
			}
			return pout_Materialien;
		}

		private void WerfeSystemereignis(Cdv_Materialuebergabe pin_MatUeber)
		{
			#region Allgemeiner Beschreibungstext
			// Allgemeine Beschreibung wobei die <Felder> noch �berschieben werden
			string str_Beschreibung = "Es wurden <Menge> <Bezeichnung> von <Verleiher> an <Empf�nger> �bergeben.";
			if (pin_MatUeber.AllgBemerkungen.Text != String.Empty)
				str_Beschreibung += "\n" + pin_MatUeber.AllgBemerkungen.Text;
			#endregion
		
			#region Verleiher und Empf�nger in Text umwandeln
			bool b_VerleiherGefunden = false;
			bool b_EmpfaengerGefunden = false;

			foreach(Cdv_Einheit en in _AlleEinheiten)
			{
				if(pin_MatUeber.VerleiherKraftID == en.ID)
				{
					str_Beschreibung = str_Beschreibung.Replace("<Verleiher>", en.Name);
					b_VerleiherGefunden = true;
				}
				if(pin_MatUeber.EmpfaengerKraftID == en.ID)
				{
					str_Beschreibung = str_Beschreibung.Replace("<Empf�nger>", en.Name);
					b_EmpfaengerGefunden = true;
				}
			}
			if (!b_EmpfaengerGefunden && !b_VerleiherGefunden)
				foreach(Cdv_Helfer he in _AlleHelfer)
				{
					if(pin_MatUeber.VerleiherKraftID == he.ID)
					{
						str_Beschreibung = str_Beschreibung.Replace("<Verleiher>", he.Personendaten.Vorname + " " + he.Personendaten.Name);
						b_VerleiherGefunden = true;
					}
					if(pin_MatUeber.EmpfaengerKraftID == he.ID)
					{
						str_Beschreibung = str_Beschreibung.Replace("<Empf�nger>", he.Personendaten.Vorname + " " + he.Personendaten.Name);
						b_EmpfaengerGefunden = true;
					}
				}
			if (!b_EmpfaengerGefunden && !b_VerleiherGefunden)
				foreach(Cdv_KFZ kfz in _AlleKFZ)
				{
					if(pin_MatUeber.VerleiherKraftID == kfz.ID)
					{
						str_Beschreibung = str_Beschreibung.Replace("<Verleiher>", kfz.Funkrufname + "(" + kfz.Kennzeichen + ")");
						b_VerleiherGefunden = true;
					}
					if(pin_MatUeber.EmpfaengerKraftID == kfz.ID)
					{
						str_Beschreibung = str_Beschreibung.Replace("<Empf�nger>", kfz.Funkrufname + "(" + kfz.Kennzeichen + ")");
						b_EmpfaengerGefunden = true;
					}
				}
			#endregion
			
			#region Materialinformationen in Text umwandeln
			foreach(Cdv_Material mat in _AlleMaterialien)
				if (mat.ID == pin_MatUeber.UebergabepostenGutID)
				{
					str_Beschreibung = str_Beschreibung.Replace("<Menge>", mat.Menge.ToString());
					str_Beschreibung = str_Beschreibung.Replace("<Bezeichnung>", mat.Bezeichnung);
					break;
				}
			#endregion

			#region Systemereignis speichern
			Cdv_Systemereignis mySyserg = new Cdv_Systemereignis(Einstellung.Benutzer.Benutzername, 
				DateTime.Now,
				str_Beschreibung,
				Tdv_SystemereignisArt.Material�bergabe,
				false);
			this._Portal_AllgFkt.WerfeSystemereignis(mySyserg);
			#endregion
		}
		private void WerfeSystemereignis(Cdv_Verbrauchsgut pin_Verbrauchsgut)
		{
			#region Beschreibungstext
			string str_Beschreibung = "Der Lagerebestand von " + pin_Verbrauchsgut.Bezeichnung + "(" + pin_Verbrauchsgut.Art + ") am Lagerort " + pin_Verbrauchsgut.Lagerort + " wurde ver�ndert.\n"
				+ "Der neue Bestand betr�gt " + pin_Verbrauchsgut.Menge + " und sp�tester Wiederbeschaffungszeitpunkt ist der " + pin_Verbrauchsgut.SpaetesterWiederbeschaffungszeitpunkt.ToString() + " Uhr.";
			#endregion

			#region Systemereignis speichern
			Cdv_Systemereignis mySyserg = new Cdv_Systemereignis(Einstellung.Benutzer.Benutzername, 
				DateTime.Now,
				str_Beschreibung,
				Tdv_SystemereignisArt.G�terankunft,
				false);
			this._Portal_AllgFkt.WerfeSystemereignis(mySyserg);
			#endregion
		}

		#endregion

		#region Cst_PortalLogik members
		override protected void SetzeRemotingPfad()
		{
			this._Pfad = "PortalLogistik";
		}
		
		override protected void SetzePortalTyp()
		{
			this._PortalTyp = typeof(IPortalLogik_Logistik);
		}

		#endregion


		#region Isbe Members

		public Image GetSbeImage()
		{
			System.Reflection.Assembly asm_Sbe;
			//Informationen �ber die ausf�hrende Assembly sammeln
			asm_Sbe = System.Reflection.Assembly.GetExecutingAssembly();
			//Liefere Name der Assembly als AssemblyName
			System.Reflection.AssemblyName asm_SbeName = asm_Sbe.GetName();
			//Speichere den dll Namen im String
			string strAssemblyName = asm_SbeName.Name;
			//Erstelle ein Stream, aus dem die Icon Daten gelesen werden
			//Hole Icon
			Image myImage = Image.FromFile(_strIconName);
			//Gebe myImage zur�ck
			return(myImage);
		}

		public String GetSbeName()
		{			
			return this._strSbeName;
		}

		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
		{
			_usc_Logistik.SetzeRollenRechte(pin_i_aktuelleRolle);
		}


		public System.Windows.Forms.UserControl GetSbeUserControl()
		{
			
			return this._usc_Logistik;
		}
		
		#endregion

	}
}
