using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using pELS.DV;

// benötigt für: Cst_Einstellung
using pELS.Tools.Client;
// benötigt für: Cst_PortalLogik
using pELS.Client;
// benötigt für: IPortalLogik_XXX
using pELS.APS.Server.Interface;

namespace pELS.Client.EK
{
	/// <summary>
	/// Summary description for CsbeEK.
	/// Implementation bis 15.03: Steini
	/// </summary>
	public class Cst_EK: Cst_PortalLogik, pELS.GUI.Interface.Isbe
	{ 

		
		#region eigene Variablen
		// TODO: dies soll später wieder weg
		private Cpr_usc_EK_xiao _usc_EK;
		//		private Cpr_usc_EK _usc_EK;			
		private IPortalLogik_EK _PortalLogikEK;

		#region benötige Datenmengen
		private Cdv_Einsatzschwerpunkt[] _EinsatzSchwerpunktmenge;
		private Cdv_Modul[] _Modulmenge;
		private Cdv_Einheit[] _EinheitMenge;
		private Cdv_Helfer[] _HelferMenge;
		private Cdv_KFZ[] _KFZMenge;
		private Cdv_Einsatz _Einsatz;
		private Cdv_Ortsverband[] _OrtsverbandMenge;
		private Cdv_Material[] _Materialmenge;
		private Cdv_Verbrauchsgut[] _Verbrauchsgueter;
		public bool _b_DatenGeaendert;
		//private Cdv

		#endregion

		#region EventDelegate
		
		private delegate void AktualisiereGUIDelegate();
		private delegate void AktualisiereTVHelferDelegate(int[] pin_IDs);

		#endregion
	
		#endregion

		#region ISBE
		//Hält den Namen der Icon Datei fest
		private string _strIconName = @"SBEImages\ek.jpg";
		//Hier wird die Beschriftung unterhalb des Icons festgehalten
		private string _strSbeName = "Einsatzkräfte";
		//hier wird die Klassenvariable gehalten, die die User Control enthält
		public Image GetSbeImage()
		{
			System.Reflection.Assembly asm_Sbe;
			//Informationen über die ausführende Assembly sammeln
			asm_Sbe = System.Reflection.Assembly.GetExecutingAssembly();
			//Liefere Name der Assembly als AssemblyName
			System.Reflection.AssemblyName asm_SbeName = asm_Sbe.GetName();
			//Speichere den dll Namen im String
			string strAssemblyName = asm_SbeName.Name;
			//Hole Icon
			Image myImage = Image.FromFile(_strIconName);
			//Gebe myImage zurück
			return(myImage);
		}

		public String GetSbeName()
		{			
			return this._strSbeName;
		}

		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
		{
			_usc_EK.SetzeRollenRechte(pin_i_aktuelleRolle);
		}

		public System.Windows.Forms.UserControl GetSbeUserControl()
		{
			
			return this._usc_EK;
		}
		
		// TODO: Sichern alle Eingaben veranlassen und wenn alles erfolgreich true zurückgeben, sonst false
		public bool CloseSbeUserControl()
		{
			return true;
		}

		
		#endregion

		#region Constructor
		public Cst_EK(Cst_Einstellung pin_Einstellungen): base (pin_Einstellungen)
		{
			
			_b_DatenGeaendert = false;
			// INIT Proxyobjekte
			this._PortalLogikEK = (IPortalLogik_EK) this._PortalLogik;

			AktualisiereEinsatzschwerpunktmenge();
			AktualisiereModulnmenge();
			AktualisiereEinheitMenge();
			AktualisiereHelferMenge();
			AktualisiereKFZmenge();
			this.AktualisiereOrtsverbandMenge();
			this.AktualisiereEinsatz();
			this.AktualisiereMaterial();		

			_usc_EK = new Cpr_usc_EK_xiao(this);

			#region UpdateEvents
			
			_ueh_Einheiten = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventEinheiten));
			this._Portal_Update.RegistriereFuerEinheit(_ueh_Einheiten);

			_ueh_Einsatzschwerpunkte = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventESP));
			this._Portal_Update.RegistriereFuerEinsatzschwerpunkte(_ueh_Einsatzschwerpunkte);

			this._ueh_Helfer = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventHelfer));
			this._Portal_Update.RegistriereFuerHelfer(this._ueh_Helfer);

			this._ueh_OV = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventOV));
			this._Portal_Update.RegistriereFuerOrtsverband(this._ueh_OV);

			this._ueh_Material = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventMaterial));
			this._Portal_Update.RegistriereFuerMaterial(this._ueh_Material);

			this._ueh_Kfz = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventKfz));
			this._Portal_Update.RegistriereFuerKfZ(this._ueh_Kfz);

			this._ueh_Modul = pELS.Events.UpdateEventAdapter.Create(
				new pELS.Events.UpdateEventHandler(this.BehandleEventModul));
			this._Portal_Update.RegistriereFuerModul(_ueh_Modul);
			
			#endregion
		}
		

		#endregion

		#region  Events für dynamische Datenakualisierung

		#region Eventshandler - Events
		private	pELS.Events.UpdateEventHandler _ueh_Einheiten;
		private	pELS.Events.UpdateEventHandler _ueh_Einsatzschwerpunkte;
		private pELS.Events.UpdateEventHandler _ueh_Helfer;
		private pELS.Events.UpdateEventHandler _ueh_OV;
		private pELS.Events.UpdateEventHandler _ueh_Material;
		private pELS.Events.UpdateEventHandler _ueh_Kfz;
		private pELS.Events.UpdateEventHandler _ueh_Modul;
		#endregion

		#region Eventhandler für dynamische Datenakualisierung- Methoden

		private void BehandleEventModul(pELS.Events.UpdateEventArgs pin_e)
		{
			//this._Modulmenge = this._PortalLogikEK.HoleModule();
			object[] oaParams = {
									pin_e.IDMenge
								};
			this._usc_EK.BeginInvoke(new AktualisiereTVHelferDelegate(this._usc_EK.AktualisiereTVModul), oaParams);		
		}

		private void BehandleEventKfz(pELS.Events.UpdateEventArgs pin_e)
		{
			this._KFZMenge = this._PortalLogikEK.HoleKFZ();	
			object[] oaParams = {
									pin_e.IDMenge
								};
			this._usc_EK.BeginInvoke(new AktualisiereTVHelferDelegate(this._usc_EK.AktualisiereTVKfz), oaParams);
		}

		private void BehandleEventHelfer(pELS.Events.UpdateEventArgs pin_e)
		{
			_EinheitMenge = this._PortalLogikEK.HoleAlleEinheiten();
			object[] oaParams = {
									pin_e.IDMenge
								};
			this._usc_EK.BeginInvoke(new AktualisiereTVHelferDelegate(this._usc_EK.AktualisiereHelferTV), oaParams);
			this._usc_EK.BeginInvoke(new AktualisiereGUIDelegate(this._usc_EK.AktualisiereHelfer));
			this._usc_EK.BeginInvoke(new AktualisiereGUIDelegate(this._usc_EK.AktualisiereLeiterESP));
			this._usc_EK.BeginInvoke(new AktualisiereGUIDelegate(this._usc_EK.AktualisiereFahrerKfz));
		}	

		private void BehandleEventEinheiten(pELS.Events.UpdateEventArgs pin_e)
		{
			object[] oaParams = {
									pin_e.IDMenge
								};
			this._usc_EK.BeginInvoke(new AktualisiereTVHelferDelegate(this._usc_EK.AktualisiereTVEinheit), oaParams);
			//_EinheitMenge = this._PortalLogikEK.HoleAlleEinheiten();
		}		

		private void BehandleEventESP(pELS.Events.UpdateEventArgs pin_e)
		{
			_EinsatzSchwerpunktmenge = this._PortalLogikEK.HoleEinsatzschwerpunkte();
			//this._usc_EK.BeginInvoke(new AktualisiereGUIDelegate(this._usc_EK.FuelleEinsatzmanager));
			object[] oaParams = {
									pin_e.IDMenge
								};
			this._usc_EK.BeginInvoke(new AktualisiereTVHelferDelegate(this._usc_EK.AktualisiereTVESP), oaParams);
			_b_DatenGeaendert = true;
		}		

		private void BehandleEventOV(pELS.Events.UpdateEventArgs pin_e)
		{
			_OrtsverbandMenge = this._PortalLogikEK.HoleAlleOrtsverbaende();
			this._usc_EK.BeginInvoke(new AktualisiereGUIDelegate(this._usc_EK.AktualisiereOVHelfer));
			this._usc_EK.BeginInvoke(new AktualisiereGUIDelegate(this._usc_EK.AktualisiereOV));
		}
		private void BehandleEventMaterial(pELS.Events.UpdateEventArgs pin_e)
		{
			_Materialmenge = this._PortalLogikEK.HoleAlleMaterial();
			this._usc_EK.BeginInvoke(new AktualisiereGUIDelegate(this._usc_EK.AktualisiereMaterial));
		}
		#endregion

		#endregion

		#region Cst_PortalLogik - abstract Members
		override protected void SetzePortalTyp()
		{
			this._PortalTyp = typeof(IPortalLogik_EK);
		}

		override protected void SetzeRemotingPfad()
		{
			this._Pfad = "PortalEK";
		}


		#endregion

		#region Laden/Füllen der Mengen
		private void AktualisiereEinsatzschwerpunktmenge()
		{
			_EinsatzSchwerpunktmenge= _PortalLogikEK.HoleEinsatzschwerpunkte();
		}

		private void AktualisiereEinheitMenge()
		{
			_EinheitMenge= _PortalLogikEK.HoleAlleEinheiten();
		}

		private void AktualisiereHelferMenge()
		{
			_HelferMenge = _PortalLogikEK.HoleAlleHelfer();
		}

		private void AktualisiereOrtsverbandMenge()
		{
			this._OrtsverbandMenge = this._PortalLogikEK.HoleAlleOrtsverbaende();
		}

		private void AktualisiereModulnmenge()
		{
			_Modulmenge= _PortalLogikEK.HoleModule();
		}

		private void AktualisiereKFZmenge()
		{
			_KFZMenge= _PortalLogikEK.HoleKFZ();
		}

		private void AktualisiereEinsatz()
		{
			this._Einsatz = this._PortalLogikEK.HoleEinsatz();
		}
		private void AktualisiereMaterial()
		{
			this._Materialmenge = this._PortalLogikEK.HoleAlleMaterial();
		}
		
		private void AktualisiereVerbrauchsgueter()
		{
			this._Verbrauchsgueter = this._PortalLogikEK.HoleAlleVerbrauchsgueter();
		}
		public Cdv_Helfer HoleHelfer(int pin_ID)
		{
			return this._PortalLogikEK.HoleHelfer(pin_ID);
		}
		public Cdv_Einsatzschwerpunkt HoleESP(int pin_ID)
		{
			return(this._PortalLogikEK.HoleESP(pin_ID));
		}
		public Cdv_KFZ HoleKfz(int pin_ID)
		{
			return this._PortalLogikEK.HoleKfz(pin_ID);
		}
		public Cdv_Einheit HoleEinheit(int pin_ID)
		{
			return this._PortalLogikEK.HoleEinheit(pin_ID);
		}
		public Cdv_Ortsverband HoleOV(int pin_ID)
		{
			Cdv_Ortsverband pout_OV;
			if((pout_OV = this._PortalLogikEK.HoleOV(pin_ID)) == null)
				pout_OV = new Cdv_Ortsverband ("Keine Daten wurden geladen");
			return pout_OV;
			
		}
		public Cdv_Modul[] HoleAlleModule()
		{
			return(this._PortalLogikEK.HoleModule());
		}
		public Cdv_Modul HoleModul(int pin_ID)
		{
			return(this._PortalLogikEK.HoleModul(pin_ID));
		}
		public Cdv_Einheit HoleEinheitZumKfz(int pin_ID)
		{
			return(this._PortalLogikEK.HoleEinheitZumKfz(pin_ID));
		}
		public Cdv_Benutzer HoleAktBenutzer()
		{
			return this._Einstellung.Benutzer;
		}
		public Cdv_Helfer[] HoleHelferZurEinheit(int pin_EinheitID)
		{
			return(this._PortalLogikEK.HoleHelferZurEinheit(pin_EinheitID));
		}

		public Cdv_Ortsverband[] HoleAlleOrtsverbaende()
		{
			//return this._PortalLogikEK.
			return(this._PortalLogikEK.HoleAlleOrtsverbaende());
		}

		public Cdv_Erkundungsergebnis[] HoleErkundungsergebnisseZuESP(int pin_ID)
		{
			return this._PortalLogikEK.HoleAlleErkundungsergebnisseZuESP(pin_ID);
		}

		public Cdv_Einheit[] HoleAlleEinheiten()
		{
			return(this._PortalLogikEK.HoleAlleEinheiten());
		}

		public Cdv_Erkundungsergebnis[] HoleAlleErkundungsergebnisse()
		{
			return(this._PortalLogikEK.HoleAlleErkundungsergebnisse());
		}
		public Cdv_Material[] HoleAlleMaterialZuEinheit(int pin_EinheitsID)
		{
			Cdv_Material[] pout_Materialmenge;
			if((pout_Materialmenge = this._PortalLogikEK.HoleAlleMaterialZuEinheit(pin_EinheitsID)) == null)
				pout_Materialmenge = new Cdv_Material[0];
			return pout_Materialmenge;
		}
		
		public Cdv_Einheit[] HoleEinheitenZumModul(Cdv_Modul pin_modul)
		{
			return(this._PortalLogikEK.HoleEinheitenZumModul(pin_modul));
		}

		public Cdv_Material[] HoleMaterialZumHelfer(int pin_ID)
		{
			return(this._PortalLogikEK.HoleMaterialZumHelfer(pin_ID));
		}
		#endregion

		#region Funktionalität
		public void GeneriereSystemereignisVerpflegung(Cdv_Modul pin_modul)
		{
			int ModulID=pin_modul.ID;
			foreach(Cdv_Helfer myHelfer in this._HelferMenge)
			{
				if (ModulID== myHelfer.ModulID)
				{
					myHelfer.LetzteVerfplegung=DateTime.Now;
					this.SpeichereHelfer(myHelfer);
				}
			}

		}

		public void SpeichereESP(Cdv_Einsatzschwerpunkt pin_esp)
		{
			this._PortalLogikEK.SpeichereESP(pin_esp);
			//this.AktualisiereEinsatzschwerpunktmenge();
		}

		public int SpeichereKfz(Cdv_KFZ pin_kfz)
		{
			return(this._PortalLogikEK.SpeichereKfz(pin_kfz));
		}

		public void SpeichereEinheit(Cdv_Einheit pin_Einheit)
		{
			this._PortalLogikEK.SpeichereEinheit(pin_Einheit);
		}

		public void ErstelleESP(Cdv_Einsatzschwerpunkt pin_esp)
		{
			//			if(bIstNeu)
			//				pin_esp.EinsatzID = this._Einsatz.ID;
			//			this._PortalLogikEK.SpeichereESP(pin_esp);
			//			this.AktualisiereEinsatzschwerpunktmenge();
			//TODO: Optimieren!!!!
			//this.fuelletrv_Einsatzmanager();
			//this.FuelleEinsatzscherpunkteBezeichnung();
		}

		public void SpeichereHelfer(Cdv_Helfer pin_helfer)
		{
			this._PortalLogikEK.SpeichereHelfer(pin_helfer);
		}

		public Cdv_Helfer SpeichereundReturniereHelfer(Cdv_Helfer pin_helfer)
		{
			return this._PortalLogikEK.SpeichereHelfer(pin_helfer);
		}

		public void SpeichereOV(Cdv_Ortsverband pin_OV)
		{
			this._PortalLogikEK.SpeichereOV(pin_OV);
		}

		public void SpeichereModul(Cdv_Modul pin_modul)
		{
			this._PortalLogikEK.SpeichereModul(pin_modul);
		}
		#endregion

		#region get- Properties

		public Cdv_Einsatzschwerpunkt[] AlleEinsatzschwerpunkte
		{
			get
			{
				this.AktualisiereEinsatzschwerpunktmenge();
				return this._EinsatzSchwerpunktmenge;}
		}
		
		public Cdv_Modul[] AlleModule
		{
			get
			{
				//this.AktualisiereModulnmenge();
				return this._Modulmenge;}
		}

		public Cdv_Einheit[] AlleEinheiten
		{
			get
			{
				//this.AktualisiereEinheitMenge();
				return this._EinheitMenge;}
		}
		public Cdv_Einheit[] AlleEinheitenMitAktualisieren
		{
			get
			{
				this.AktualisiereEinheitMenge();
				return this._EinheitMenge;}
		}
		public Cdv_Helfer[] AlleHelfer
		{
			get
			{
				this.AktualisiereHelferMenge();
				return this._HelferMenge;
			}
		}
		public Cdv_KFZ[] AlleKFZ
		{
			get
			{
				this.AktualisiereKFZmenge();
				return this._KFZMenge;}
		}
		public Cdv_Einsatz Einsatz
		{
			get
			{
				this.AktualisiereEinsatz();
				return this._Einsatz;}
		}
		public Cdv_Ortsverband[] AlleOVs
		{
			get
			{
				this.AktualisiereOrtsverbandMenge();
				return(this._OrtsverbandMenge);
			}
		}
		
		public Cdv_Material[] AlleMaterial
		{
			get
			{
				this.AktualisiereMaterial();
				return this._Materialmenge;}
		}
	
		public Cdv_Verbrauchsgut[] AlleVerbrauchsgueter
		{
			get
			{
				this.AktualisiereVerbrauchsgueter();
				return this._Verbrauchsgueter;}
		}
		#endregion

		#region DnD Support für Präsentationsschicht

		private void EntferneKfzVonEinheit(Cdv_KFZ pin_kfz, Cdv_Einheit pin_einheit)
		{
			int[] iaKfzIDs = pin_einheit.KfzKraefteIDMenge;
			int[] iaKfzIDsNeu = null;
			ArrayList alIDsTemp = new ArrayList();
			if(iaKfzIDs != null)
			{
				foreach(int iKfzID in iaKfzIDs)
				{
					if(iKfzID != pin_kfz.ID)
						alIDsTemp.Add(iKfzID);
				}
				iaKfzIDsNeu = new int[alIDsTemp.Count];
				alIDsTemp.CopyTo(iaKfzIDsNeu);
			}
			pin_einheit.KfzKraefteIDMenge = iaKfzIDsNeu;
			foreach (Cdv_Einheit myeinheit in this._EinheitMenge)
			{
				if (myeinheit.ID==pin_einheit.ID)
				{
					myeinheit.KfzKraefteIDMenge=pin_einheit.KfzKraefteIDMenge;
					break;
				}
			}
			this.SpeichereEinheit(pin_einheit);
		}

		private void EntferneHelferVonEinheit(Cdv_Helfer pin_helfer, Cdv_Einheit pin_einheit)
		{
			int[] iaHelferIDs = pin_einheit.HelferIDMenge;
			int[] iaHelferIDsNeu = null;
			ArrayList alIDsTemp = new ArrayList();
			if(iaHelferIDs != null)
			{
				foreach(int iHelferID in iaHelferIDs)
				{
					if(iHelferID != pin_helfer.ID)
						alIDsTemp.Add(iHelferID);
				}
				iaHelferIDsNeu = new int[alIDsTemp.Count];
				alIDsTemp.CopyTo(iaHelferIDsNeu);
			}
			pin_einheit.HelferIDMenge = iaHelferIDsNeu;
			foreach (Cdv_Einheit myeinheit in this._EinheitMenge)
			{
				if (myeinheit.ID==pin_einheit.ID)
				{
					myeinheit.HelferIDMenge=pin_einheit.HelferIDMenge;
					break;
				}
			}
			this.SpeichereEinheit(pin_einheit);
		}

		public void SetzeKfzIDMengeFuerEinheit(Cdv_Einheit pin_einheit, int[] pin_IDs)
		{
			foreach(Cdv_Einheit einheit in this._EinheitMenge)
			{
				if(einheit.ID == pin_einheit.ID)
				{
					einheit.KfzKraefteIDMenge = pin_IDs;
					break;
				}
			}
		}

		public void SetzeHelferIDMengeFuerEinheit(Cdv_Einheit pin_einheit, int[] pin_IDs)
		{
			foreach(Cdv_Einheit einheit in this._EinheitMenge)
			{
				if(einheit.ID == pin_einheit.ID)
				{
					einheit.HelferIDMenge = pin_IDs;
					break;
				}
			}
		}

		private void OrdneKfzZurEinheit(Cdv_KFZ pin_kfz, Cdv_Einheit pin_einheit)
		{
			int[] iaKfzIDs = pin_einheit.KfzKraefteIDMenge;
			int iLen = 1;
			if(iaKfzIDs != null)
				iLen = iaKfzIDs.Length + 1;
			int[] iaKfzIDsNeu = new int[iLen];
			iaKfzIDsNeu[0] = pin_kfz.ID;
			for(int i = 1; i <= iLen - 1; i++)
				iaKfzIDsNeu[i] = iaKfzIDs[i - 1];
			pin_einheit.KfzKraefteIDMenge = iaKfzIDsNeu;
			foreach (Cdv_Einheit myeinheit in this._EinheitMenge)
			{
				if (myeinheit.ID==pin_einheit.ID)
				{
					myeinheit.KfzKraefteIDMenge=pin_einheit.KfzKraefteIDMenge;
					break;
				}
			}
			this.SpeichereEinheit(pin_einheit);
		}

		private void OrdneHelferZurEinheit(Cdv_Helfer pin_helfer, Cdv_Einheit pin_einheit)
		{
			int[] iaHelferIDs = pin_einheit.HelferIDMenge;
			int iLen = 1;
			if(iaHelferIDs != null)
				iLen = iaHelferIDs.Length + 1;
			int[] iaHelferIDsNeu = new int[iLen];
			iaHelferIDsNeu[0] = pin_helfer.ID;
			for(int i = 1; i <= iLen - 1; i++)
				iaHelferIDsNeu[i] = iaHelferIDs[i - 1];
			pin_einheit.HelferIDMenge = iaHelferIDsNeu;
			foreach (Cdv_Einheit myeinheit in this._EinheitMenge)
			{
				if (myeinheit.ID==pin_einheit.ID)
				{
					myeinheit.HelferIDMenge=pin_einheit.HelferIDMenge;
					break;
				}
			}
			this.SpeichereEinheit(pin_einheit);
		}

		private void OrdneEinheitZumModul(Cdv_Einheit pin_einheit, Cdv_Modul pin_modul)
		{
			pin_einheit.ModulID = pin_modul.ID;
			this.SpeichereEinheit(pin_einheit);
		}

		private void OrdneModulZumESP(Cdv_Modul pin_modul, Cdv_Einsatzschwerpunkt pin_esp)
		{
			pin_modul.EinsatzschwerpunktID = pin_esp.ID;
			this.SpeichereModul(pin_modul);
		}

		private void OrdneEinheitZumESP(Cdv_Einheit pin_einheit, Cdv_Einsatzschwerpunkt pin_esp)
		{
			pin_einheit.EinsatzschwerpunktID = pin_esp.ID;
			this.SpeichereEinheit(pin_einheit);
		}

		private void OrdneHelferZumESP(Cdv_Helfer pin_helfer, Cdv_Einsatzschwerpunkt pin_esp)
		{
			pin_helfer.EinsatzschwerpunktID = pin_esp.ID;
			this.SpeichereHelfer(pin_helfer);
		}

		private void EntferneHelferKnoten(Cdv_Helfer pin_helfer, TreeNode trnKnoten)
		{
			Cdv_Einheit einheit = (Cdv_Einheit) ((Cst_EK_TreeviewTag) trnKnoten.Tag).Eintrag;
			ArrayList alHelferNeu = new ArrayList();
			TreeNode trnAlleHelfer = trnKnoten.Nodes[0];
			TreeNodeCollection tnc = trnAlleHelfer.Nodes;
			IEnumerator ie = tnc.GetEnumerator();
			while(ie.MoveNext())
			{
				TreeNode trn = (TreeNode) ie.Current;
				Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trn.Tag;
				Cdv_Helfer helfer = (Cdv_Helfer) tag.Eintrag;
				if(helfer != null)
				{
					if(helfer == pin_helfer)
					{
						trnKnoten.Nodes.Remove(trn);
					}
					else
						alHelferNeu.Add(helfer.ID);
				}
			}
			int[] iaHelferIDs = new int[alHelferNeu.Count];
			alHelferNeu.CopyTo(iaHelferIDs);
			einheit.HelferIDMenge = iaHelferIDs;
			this.SpeichereEinheit(einheit);
		}

		private void EntferneKfzKnoten(Cdv_KFZ pin_kfz, TreeNode trnKnoten)
		{
			Cdv_Einheit einheit = (Cdv_Einheit) ((Cst_EK_TreeviewTag) trnKnoten.Tag).Eintrag;
			ArrayList alKFZNeu = new ArrayList();
			TreeNode trnAlleKFZ = trnKnoten.Nodes[1];
			TreeNodeCollection tnc = trnAlleKFZ.Nodes;
			IEnumerator ie = tnc.GetEnumerator();
			while(ie.MoveNext())
			{
				TreeNode trn = (TreeNode) ie.Current;
				Cst_EK_TreeviewTag tag = (Cst_EK_TreeviewTag) trn.Tag;
				Cdv_KFZ kfz = (Cdv_KFZ) tag.Eintrag;
				if(kfz != null)
				{
					if(kfz.Kennzeichen == pin_kfz.Kennzeichen)
					{
						trnKnoten.Nodes.Remove(trn);
					}
					else
						alKFZNeu.Add(kfz.ID);
				}
			}
			int[] iaKFZIDs = new int[alKFZNeu.Count];
			alKFZNeu.CopyTo(iaKFZIDs);
			einheit.KfzKraefteIDMenge = iaKFZIDs;
			this.SpeichereEinheit(einheit);
			this._usc_EK.AktualisiereModulVonKfz(einheit, pin_kfz);
		}

		public void ErmittleZuordnungDND(TreeNode pin_nodeQuelle, TreeNode pin_nodeZiel)
		{
			Cst_EK_TreeviewTag tagQuelle = (Cst_EK_TreeviewTag) pin_nodeQuelle.Tag;
			Cst_EK_TreeviewTag tagZiel = (Cst_EK_TreeviewTag) pin_nodeZiel.Tag;
			if((tagZiel.Eintrag != null) && (tagQuelle.Eintrag != null))
			{
				//zuordnung Helfer -> Einheit
				if(tagQuelle.Eintrag is Cdv_Helfer)
				{
					if(tagZiel.Eintrag is Cdv_Einheit)
					{
						if(pin_nodeQuelle.Parent.Parent != null)
						{
							Cst_EK_TreeviewTag einheitTag = (Cst_EK_TreeviewTag) pin_nodeQuelle.Parent.Parent.Tag;
							ArrayList tmpList= new ArrayList(this._usc_EK._TreeNodeReferenzen);
							IEnumerator ieHelfer = this._usc_EK._TreeNodeReferenzen.GetEnumerator();
							while(ieHelfer.MoveNext())
							{
								Cst_EK_TreeviewReferenceItem item = (Cst_EK_TreeviewReferenceItem) ieHelfer.Current;
								int iHelferID = (tagQuelle.Eintrag as Cdv_Helfer).ID;
								if (item.PelsObjectID == iHelferID)
								{
									this._usc_EK.trv_Einsatzmanager.Nodes.Remove(item.TreeNodeReferenziert);
									this._usc_EK._TreeNodeReferenzen.Remove(item);
									ieHelfer = this._usc_EK._TreeNodeReferenzen.GetEnumerator();
								}
							}
							tmpList= new ArrayList(this._usc_EK._TreeNodeReferenzen);
							foreach (Cst_EK_TreeviewReferenceItem item in tmpList)
							{
								int iEinheitID = (tagZiel.Eintrag as Cdv_Einheit).ID;
								if (item.PelsObjectID == iEinheitID)
								{
									Cdv_Helfer helfer = (Cdv_Helfer) tagQuelle.Eintrag;
									int iHelferID = (tagQuelle.Eintrag as Cdv_Helfer).ID;
									TreeNode tmpNode=(TreeNode)pin_nodeQuelle.Clone();
									item.TreeNodeReferenziert.Nodes[0].Nodes.Add(tmpNode);
									this._usc_EK._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(iHelferID,tmpNode));
								}
							}
							this.EntferneHelferVonEinheit((Cdv_Helfer) tagQuelle.Eintrag, (Cdv_Einheit) einheitTag.Eintrag);
						}
						else
						{
							ArrayList tmpList= new ArrayList(this._usc_EK._TreeNodeReferenzen);
							foreach (Cst_EK_TreeviewReferenceItem item in tmpList)
							{
								int iEinheitID = (tagZiel.Eintrag as Cdv_Einheit).ID;
								if (item.PelsObjectID == iEinheitID)
								{
									int iHelferID = (tagQuelle.Eintrag as Cdv_Helfer).ID;
									TreeNode tmpNode=(TreeNode)pin_nodeQuelle.Clone();
									item.TreeNodeReferenziert.Nodes[0].Nodes.Add(tmpNode);
									this._usc_EK._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(iHelferID,tmpNode));
								}
							}					
						}
						this.OrdneHelferZurEinheit((Cdv_Helfer) tagQuelle.Eintrag, (Cdv_Einheit) tagZiel.Eintrag);
					}
				}
				//zuordnung Einheit -> Modul
				if(tagQuelle.Eintrag is Cdv_Einheit)
				{
					if(tagZiel.Eintrag is Cdv_Modul)
					{
						Cdv_Einheit einheitZumSpeichern = (Cdv_Einheit) tagQuelle.Eintrag;
						Cdv_Modul modulZumZuordnen = (Cdv_Modul) tagZiel.Eintrag;
						Cdv_Einheit einheit = new Cdv_Einheit();
						einheit.Betriebsverbrauch = einheitZumSpeichern.Betriebsverbrauch;
						einheit.EinheitenfuehrerHelferID = einheitZumSpeichern.EinheitenfuehrerHelferID;
						einheit.EinsatzschwerpunktID = modulZumZuordnen.EinsatzschwerpunktID;
						einheit.Erreichbarkeit = einheitZumSpeichern.Erreichbarkeit;
						einheit.Funkrufname = einheitZumSpeichern.Funkrufname;
						einheit.GST = einheitZumSpeichern.GST;
						einheit.HelferIDMenge = einheitZumSpeichern.HelferIDMenge;
						einheit.ID = einheitZumSpeichern.ID;
						einheit.IstStaerke = einheitZumSpeichern.IstStaerke;
						einheit.KfzKraefteIDMenge = einheitZumSpeichern.KfzKraefteIDMenge;
						einheit.Kommentar = einheitZumSpeichern.Kommentar;
						einheit.Kraeftestatus = einheitZumSpeichern.Kraeftestatus;
						einheit.ModulID = modulZumZuordnen.ID;
						einheit.Name = einheitZumSpeichern.Name;
						einheit.OVID = einheitZumSpeichern.OVID;
						einheit.SollStaerke = einheitZumSpeichern.SollStaerke;
						einheit.StellvertreterHelferID = einheitZumSpeichern.StellvertreterHelferID;
						einheit.Version = einheitZumSpeichern.Version;
						SpeichereEinheit(einheit);
						return;
					}
				}
				//zuordnung Modul -> ESP
				if(tagQuelle.Eintrag is Cdv_Modul)
				{
					if(tagZiel.Eintrag is Cdv_Einsatzschwerpunkt)
					{
						//this.OrdneModulZumESP((Cdv_Modul) tagQuelle.Eintrag, (Cdv_Einsatzschwerpunkt) tagZiel.Eintrag);
						//return;
						Cdv_Modul m = new Cdv_Modul();
						Cdv_Modul modul = (Cdv_Modul) tagQuelle.Eintrag;
						m.ID = modul.ID;
						m.Modulname = modul.Modulname;
						m.Version = modul.Version;
						Cdv_Einsatzschwerpunkt esp = (Cdv_Einsatzschwerpunkt) tagZiel.Eintrag;
						m.EinsatzschwerpunktID = esp.ID;
						SpeichereModul(m);
						foreach(Cdv_Einheit myEinheit in this.AlleEinheiten)
						{
							if (myEinheit.ModulID==m.ID)
							{
								myEinheit.EinsatzschwerpunktID=m.EinsatzschwerpunktID;
								SpeichereEinheit(myEinheit);
							}
						}
						return;
					}
				}
				//zuordnung Kfz -> Einheit
				if(tagQuelle.Eintrag is Cdv_KFZ)
				{
					if(tagZiel.Eintrag is Cdv_Einheit)
					{
						if(pin_nodeQuelle.Parent.Parent != null)
						{
							Cst_EK_TreeviewTag einheitTag = (Cst_EK_TreeviewTag) pin_nodeQuelle.Parent.Parent.Tag;
							ArrayList tmpList= new ArrayList(this._usc_EK._TreeNodeReferenzen);
							IEnumerator ieKfz = this._usc_EK._TreeNodeReferenzen.GetEnumerator();
							while(ieKfz.MoveNext())
							{
								Cst_EK_TreeviewReferenceItem item = (Cst_EK_TreeviewReferenceItem) ieKfz.Current;
								int iKfzID = (tagQuelle.Eintrag as Cdv_KFZ).ID;
								if (item.PelsObjectID == iKfzID)
								{
									this._usc_EK.trv_Einsatzmanager.Nodes.Remove(item.TreeNodeReferenziert);
									this._usc_EK._TreeNodeReferenzen.Remove(item);
									ieKfz = this._usc_EK._TreeNodeReferenzen.GetEnumerator();
								}
							}
							tmpList= new ArrayList(this._usc_EK._TreeNodeReferenzen);
							foreach (Cst_EK_TreeviewReferenceItem item in tmpList)
							{
								int iEinheitID = (tagZiel.Eintrag as Cdv_Einheit).ID;
								if (item.PelsObjectID == iEinheitID)
								{
									int iKfzID = (tagQuelle.Eintrag as Cdv_KFZ).ID;
									TreeNode tmpNode=(TreeNode)pin_nodeQuelle.Clone();
									item.TreeNodeReferenziert.Nodes[1].Nodes.Add(tmpNode);
									this._usc_EK._TreeNodeReferenzen.Add(new Cst_EK_TreeviewReferenceItem(iKfzID,tmpNode));
								}
							}
							this.EntferneKfzVonEinheit((Cdv_KFZ) tagQuelle.Eintrag, (Cdv_Einheit) einheitTag.Eintrag);
						}
						this.OrdneKfzZurEinheit((Cdv_KFZ) tagQuelle.Eintrag, (Cdv_Einheit) tagZiel.Eintrag);
					}
				}
			}
		}

		
		public void EntferneAlleUnterknoten(TreeNode Currentnode)
		{
			foreach(TreeNode TrnSubNode in Currentnode.Nodes)
			{
				if (TrnSubNode.Nodes.Count>0)
				{
					EntferneAlleUnterknoten(TrnSubNode);
				}
				else
				{
					ArrayList NodeListe=new ArrayList(this._usc_EK._TreeNodeReferenzen);
					foreach(Cst_EK_TreeviewReferenceItem Item in NodeListe)
					{
						if(Item.TreeNodeReferenziert == TrnSubNode)
							this._usc_EK._TreeNodeReferenzen.Remove(TrnSubNode);
					}
				}
			}
		}
		
		public DragDropEffects ErmittleSymbolDND(Cst_EK_TreeviewTag pin_tagQuelle, Cst_EK_TreeviewTag pin_tagZiel)
		{
			if(pin_tagZiel.Eintrag == null)
				return(DragDropEffects.None);
			else
			{
				Type tQuelle = pin_tagQuelle.Type;
				Type tKnoten = pin_tagZiel.Type;
				if(tKnoten == tQuelle)
					return(DragDropEffects.None);
				//Modul -> ESP
				if((pin_tagZiel.Eintrag is Cdv_Einsatzschwerpunkt) && (pin_tagQuelle.Eintrag is Cdv_Modul))
					return(DragDropEffects.Copy | DragDropEffects.Move);
				//Helfer -> Einheit
				if((pin_tagZiel.Eintrag is Cdv_Einheit) && (pin_tagQuelle.Eintrag is Cdv_Helfer))
					return(DragDropEffects.Copy | DragDropEffects.Move);
				//Kfz -> Einheit
				if((pin_tagZiel.Eintrag is Cdv_Einheit) && (pin_tagQuelle.Eintrag is Cdv_KFZ))
					return(DragDropEffects.Copy | DragDropEffects.Move);
				//Einheit -> Modul
				if((pin_tagZiel.Eintrag is Cdv_Modul) && (pin_tagQuelle.Eintrag is Cdv_Einheit))
					return(DragDropEffects.Copy | DragDropEffects.Move);
//				if((pin_tagZiel.Eintrag is Cdv_Einsatzschwerpunkt) || (pin_tagZiel.Eintrag is Cdv_Einheit)
//					|| (pin_tagZiel.Eintrag is Cdv_Modul))
//					return(DragDropEffects.Copy | DragDropEffects.Move);
			}			
			return(DragDropEffects.None);
		}

		public void AktualisiereEinheiten()
		{
			this._EinheitMenge = this._PortalLogikEK.HoleAlleEinheiten();
		}

		public void AktualisiereModule()
		{
			this._Modulmenge = this._PortalLogikEK.HoleModule();
		}

		public void AktualisiereEsps()
		{
			this._EinsatzSchwerpunktmenge = this._PortalLogikEK.HoleEinsatzschwerpunkte();
		}

		#endregion

		#region zu löschen


		//TODO: Diese Methode zu löschen. Sie ist jetzt noch da, weil Cpr_usc_EK ist nicht verändert,(jetzt wird
		// an Cst_usc_EK_xiao.cs gearbeitet)
		// und sie diese Methode noch braucht zum Compilieren. 
		/// <summary>
		/// Füllt den Einsatzmanager mit den "Strukturen" Einsatzschwerpunkte, Einheiten, Module, (Material)
		/// </summary>
		public void fuelletrv_Einsatzmanager()
		{
		}		

	
		public Cdv_Helfer LeiterZuESP(Cdv_Einsatzschwerpunkt pin_esp)
		{
			Cdv_Helfer helfer = null;
			IEnumerator ie = this._HelferMenge.GetEnumerator();
			while(ie.MoveNext())
			{
				helfer = (Cdv_Helfer) ie.Current;
				if(helfer.ID == pin_esp.EinsatzleiterHelferID)
					return(helfer);
			}
			return(null);
		}
		#endregion

	}
}
