using System;
using pELS.DV.Server.ObjectManager.Verwaltung;
using pELS.DV.Server.ObjectManager.Interfaces;

namespace pELS.DV.Server.ObjectManager
{
	/// <summary>
	/// Implementiert den Object Manager. Dieser verwaltet
	/// Objektlisten und erlaubt es, die Objekte persistent
	/// zu halten mit Hilfe der Speichern Methoden der
	/// entsprechenden Mengen. Object Manager ist als Singleton
	/// implementiert
	/// </summary>
	public class Cdv_ObjMgr: MarshalByRefObject, IObjectManager
	{

		#region Instanzvariablen
		/// <summary>
		/// Menge der Anforderungen
		/// </summary>
		protected Cdv_Verwaltung _cvAnforderungsMenge;
		/// <summary>
		/// Menge aller Einsätze
		/// </summary>
		protected Cdv_Verwaltung _cvEinsatzMenge;
		/// <summary>
		/// Menge aller Ortsverbände
		/// </summary>
		protected Cdv_Verwaltung _cvOVMenge;
		/// <summary>
		/// Menge aller Einsatzschwerpunkte
		/// </summary>
		protected Cdv_Verwaltung _cvEinsatzschwerpunktMenge;
		/// <summary>
		/// Menge aller Personen
		/// </summary>
		//protected Cdv_Verwaltung _cvPersonenMenge;
		/// <summary>
		/// Menge aller Helfer
		/// </summary>
		protected Cdv_Verwaltung _cvHelferMenge;
		/// <summary>
		/// Menge aller Benutzer
		/// </summary>
		protected Cdv_Verwaltung _cvBenutzerMenge;
		/// <summary>
		/// Menge aller Aufträge
		/// </summary>
		protected Cdv_Verwaltung _cvAuftragsMenge;
		/// <summary>
		/// Menge aller Einheiten
		/// </summary>
		protected Cdv_Verwaltung _cvEinheitMenge;
		/// <summary>
		/// Menge aller Erkundungsergebnisse
		/// </summary>
		//protected Cdv_Verwaltung _cvErkundungsergebnissMenge;
		/// <summary>
		/// Menge aller Kfz
		/// </summary>
		protected Cdv_Verwaltung _cvKfzMenge;
		/// <summary>
		/// Materialmenge
		/// </summary>
		protected Cdv_Verwaltung _cvMaterialMenge;
		/// <summary>
		/// Materialübergabemenge
		/// </summary>
		protected Cdv_Verwaltung _cvMatrialuebergabeMenge;
		/// <summary>
		/// Menge aller Meldungen
		/// </summary>
		protected Cdv_Verwaltung _cvMeldungMenge;
		/// <summary>
		/// Menge aller Moduln
		/// </summary>
		protected Cdv_Verwaltung _cvModulMenge;
		/// <summary>
		/// Menge aller Termine
		/// </summary>
		protected Cdv_Verwaltung _cvTerminMenge;
		/// <summary>
		/// Menge aller Verbrauchsgüter
		/// </summary>
		protected Cdv_Verwaltung _cvVerbrauchsgutMenge;
		/// <summary>
		/// Menge aller ETB Einträge
		/// </summary>
		protected Cdv_Verwaltung _cvEtbEintragMenge;
		/// <summary>
		/// Menge aller Kommentare zu EtbEinträgen
		/// </summary>
		protected Cdv_Verwaltung _cvEtbEintragKommentarMenge;
		/// <summary>
		/// Referenzzähler auf das ObjectManager Objekt
		/// </summary>
		protected static int _i_ReferenzZaehler = 0;
		/// <summary>
		/// Die einzige Instanz des ObjectManagers
		/// </summary>
		protected static Cdv_ObjMgr _omgr_ObjectManager = null;
		#endregion

		#region Konstruktor
		private Cdv_ObjMgr()
		{
			this._cvAnforderungsMenge = new Cdv_Verwaltung(new Cdv_AnforderungDB());
			this._cvEinsatzMenge = new Cdv_Verwaltung(new Cdv_EinsatzDB());
			this._cvOVMenge = new Cdv_Verwaltung(new Cdv_OrtsverbandDB());
			this._cvEinsatzschwerpunktMenge = new Cdv_Verwaltung(new Cdv_EinsatzschwerpunktDB());
			//this._cvPersonenMenge = new Cdv_Verwaltung(new Cdv_PersonDB());
			this._cvHelferMenge = new Cdv_Verwaltung(new Cdv_HelferDB());
			this._cvBenutzerMenge = new Cdv_Verwaltung(new Cdv_BenutzerDB());
			this._cvAuftragsMenge = new Cdv_Verwaltung(new Cdv_AuftragDB());
			this._cvEinheitMenge = new Cdv_Verwaltung(new Cdv_EinheitDB());
			//this._cvErkundungsergebnissMenge = new Cdv_Verwaltung(new Cdv_ErkundungsergebnisDB());
			this._cvKfzMenge = new Cdv_Verwaltung(new Cdv_KFZDB());
			this._cvMaterialMenge = new Cdv_Verwaltung(new Cdv_MaterialDB());
			this._cvMatrialuebergabeMenge = new Cdv_Verwaltung(new Cdv_MaterialuebergabeDB());
			this._cvMeldungMenge = new Cdv_Verwaltung(new Cdv_MeldungDB());
			this._cvModulMenge = new Cdv_Verwaltung(new Cdv_ModulDB());
			this._cvTerminMenge = new Cdv_Verwaltung(new Cdv_TerminDB());
			this._cvVerbrauchsgutMenge = new Cdv_Verwaltung(new Cdv_VerbrauchsgutDB());
			this._cvEtbEintragMenge = new Cdv_Verwaltung(new Cdv_EtbEintragDB());
			this._cvEtbEintragKommentarMenge = new Cdv_Verwaltung(new Cdv_EtbEintragKommentarDB());
		}
		#endregion

		#region Methoden und Properties
		public IVerwaltung Anforderungen
		{
			get
			{
				return(this._cvAnforderungsMenge);
			}
		}

		public IVerwaltung Einsaetze
		{
			get
			{
				return(this._cvEinsatzMenge);
			}
		}

		public IVerwaltung Ortsverbaende
		{
			get
			{
				return(this._cvOVMenge);
			}
		}

		public IVerwaltung Einsatzschwerpunkte
		{
			get
			{
				return(this._cvEinsatzschwerpunktMenge);
			}
		}	

//		public IVerwaltung Personen
//		{
//			get
//			{
//				return(this._cvPersonenMenge);
//			}
//		}

		public IVerwaltung Helfer
		{
			get
			{
				return(this._cvHelferMenge);
			}
		}

		public IVerwaltung Benutzer
		{
			get
			{
				return(this._cvBenutzerMenge);
			}
		}

		public IVerwaltung Auftraege
		{
			get
			{
				return(this._cvAuftragsMenge);
			}
		}

		public IVerwaltung Erkundungsbefehle
		{
			get
			{
				return(this._cvAuftragsMenge);
			}
		}

		public IVerwaltung Einheiten
		{
			get
			{
				return(this._cvEinheitMenge);
			}
		}

//		public IVerwaltung Erkundungsergebnisse
//		{
//			get
//			{
//				return(this._cvErkundungsergebnissMenge);
//			}
//		}

		public IVerwaltung Kfz
		{
			get
			{
				return(this._cvKfzMenge);
			}
		}

		public IVerwaltung Material
		{
			get
			{
				return(this._cvMaterialMenge);
			}
		}

		public IVerwaltung Materialuebergaben
		{
			get
			{
				return(this._cvMatrialuebergabeMenge);
			}
		}

		public IVerwaltung Meldungen
		{
			get
			{
				return(this._cvMeldungMenge);
			}
		}

		public IVerwaltung Moduln
		{
			get
			{
				return(this._cvModulMenge);
			}
		}

		public IVerwaltung Termine
		{
			get
			{
				return(this._cvTerminMenge);
			}
		}

		public IVerwaltung Verbrauchsgueter
		{
			get
			{
				return(this._cvVerbrauchsgutMenge);
			}
		}

		public IVerwaltung EtbEintraege
		{
			get
			{
				return(this._cvEtbEintragMenge);
			}
		}

		public IVerwaltung EtbKommentare
		{
			get
			{
				return(this._cvEtbEintragKommentarMenge);
			}
		}
		
		

		public static Cdv_ObjMgr HoleInstanz()
		{
			if(_omgr_ObjectManager == null)
				_omgr_ObjectManager = new Cdv_ObjMgr();
			_i_ReferenzZaehler++;
			return(_omgr_ObjectManager);
		}

		public override object InitializeLifetimeService()
		{
			return(null);
		}

		#endregion
	}
}
