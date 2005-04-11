using System;

// TODO: löschen
using System.Windows.Forms;

namespace PortalLogik_MAT
{
	// benötigt für: Cap_PortalLogik
	using pELS.Server;
	// benötigt für: IPortalLogik_XXX
	using pELS.APS.Server.Interface;
	// benötigt für: pELS.DV.Cdv_XXX
	using pELS.DV;
	// benötigt für: ObjectManager
	using pELS.DV.Server.ObjectManager;
	// benötigt für: IVerwaltung
	using pELS.DV.Server.ObjectManager.Interfaces;
	// benötigt für: pels-Objecte
	using pELS.DV.Server.Interfaces;
	using pELS;



	// benötigt für: remotable objects
	using System.Runtime.Remoting;
	// benötigt für: TypeFilterLevel
	using System.Runtime.Serialization.Formatters;
	// benötigt für: ChannelServices
	using System.Runtime.Remoting.Channels;
	// benötigt für: TcpChannel
	using System.Runtime.Remoting.Channels.Tcp;
	// benötigt für: IDictionary
	using System.Collections;


	/// <summary>
	/// Summary description for PortalLogik_MAT.
	/// </summary>
	public class CPortalLogik_MAT : Cap_PortalLogik, IPortalLogik_MAT
	{
		public CPortalLogik_MAT(int pin_OMPort, string pin_URL, int pin_Port) : 
			base(pin_OMPort, pin_URL, pin_Port)
		{
		}

		#region Cap_PortalLogik members
		protected override void SetzeRemotingPfad()
		{
			this._Pfad = "PortalMAT";
		}

		public override void StartePortalLogik()
		{
		}
		#endregion
		#region IPortalLogik_MAT Members

		// Hole- Methoden
		public Cdv_Einsatzschwerpunkt[] HoleAlleESP()
		{
			// Falls der Code woandershin kopiert werden soll, steht in Kommentar "//nummer"
			// die dort zu änderende Stelle und Häufigkeit. (Um das Hin-und Hertesten zu vermeiden)-xiao 
			IVerwaltung verw_verwaltung = _ObjektManager.Einsatzschwerpunkte; //1  			
			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();

			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_Einsatzschwerpunkt[] pout_espMenge = new Cdv_Einsatzschwerpunkt[ipoa.Length];	//3
				ipoa.CopyTo(pout_espMenge,0); //1
				return pout_espMenge; //1
			}//TODO: Fehldermeldungen
			else return null;
		}
	
		public Cdv_Auftrag[] HoleNachzuverfolgendeAuftraege()
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Auftraege;  			
			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();

			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				ArrayList arrlist_tmpAuftraege = new ArrayList();	
				IEnumerator enm = ipoa.GetEnumerator();
				Cdv_Auftrag auftrag = new Cdv_Auftrag();
				while(enm.MoveNext())
				{
					auftrag = (Cdv_Auftrag)enm.Current;
					if(auftrag.WirdNachverfolgt == true)
						arrlist_tmpAuftraege.Add(auftrag);
				}
				//
				Cdv_Auftrag[] pout_AuftragsMenge = new Cdv_Auftrag[arrlist_tmpAuftraege.Count];	
				arrlist_tmpAuftraege.CopyTo(pout_AuftragsMenge,0); 
				return pout_AuftragsMenge; 				
			}
			else
				return null;
		}


		public Cdv_Einsatzschwerpunkt HoleESP(int pin_ID)
		{
			Cdv_Einsatzschwerpunkt esp = (Cdv_Einsatzschwerpunkt)_ObjektManager.Einsatzschwerpunkte.Holen(pin_ID);	
			return esp;
		}
		public Cdv_Benutzer HoleBenutzer(int pin_ID)
		{
			Cdv_Benutzer benutzer = (Cdv_Benutzer)_ObjektManager.Benutzer.Holen(pin_ID);
			return benutzer;
		}


		// Speichere- Methoden
		public Cdv_Termin SpeichereTermin(Cdv_Termin pin_termin)
		{
			//			MessageBox.Show("PortalLogik_MAT.SpeichereTermin(..): Startet ");
			Cdv_Termin pout_termin;
			pout_termin = (Cdv_Termin)_ObjektManager.Termine.Speichern(pin_termin);
			return pout_termin;
		}
		public void SpeichereMeldung(Cdv_Meldung pin_Meldung)
		{
			this._ObjektManager.Meldungen.Speichern(pin_Meldung);
		}
		
		public void SpeichereErkundungsergebnis(Cdv_Erkundungsergebnis pin_Erkundungsergebnis)
		{
			this._ObjektManager.Meldungen.Speichern(pin_Erkundungsergebnis);
		}
		public void SpeichereAuftrag(Cdv_Auftrag pin_Auftrag)
		{
			this._ObjektManager.Auftraege.Speichern(pin_Auftrag);
		}
		public void SpeichereErkundungsbefehl(Cdv_Erkundungsbefehl pin_Erkundungsbefehl)
		{
			this._ObjektManager.Erkundungsbefehle.Speichern(pin_Erkundungsbefehl);
		}

		
		#endregion

		#region IPortalLogik_allgemeinMAT Members

		public pELS.DV.Cdv_Mitteilung SpeichereMitteilung(pELS.DV.Cdv_Meldung pin_meldung)
		{
			Cdv_Meldung pout_neueMeldung = (Cdv_Meldung)_ObjektManager.Meldungen.Speichern(pin_meldung);
			return pout_neueMeldung;
		}

		public pELS.DV.Cdv_Mitteilung SpeichereMitteilung(pELS.DV.Cdv_Auftrag pin_auftrag)
		{
			Cdv_Auftrag pout_neuerAuftrag = (Cdv_Auftrag)_ObjektManager.Auftraege.Speichern(pin_auftrag);
			return pout_neuerAuftrag;
		}

		public Cdv_Auftrag[] LadeAuftraege()
		{			
			IPelsObject[] pout_ipoa = _ObjektManager.Auftraege.HolenAlle();				
			if (pout_ipoa != null)
			{
				Cdv_Auftrag[] auftraege = new Cdv_Auftrag[pout_ipoa.Length];
				pout_ipoa.CopyTo(auftraege, 0);
				return auftraege;
			}
			else return null;						
		}

		public Cdv_Meldung[] LadeMeldungen()
		{
			IPelsObject[] pout_ipoa = _ObjektManager.Meldungen.HolenAlle();				
			if (pout_ipoa != null)
			{
				Cdv_Meldung[] meldungen = new Cdv_Meldung[pout_ipoa.Length];
				pout_ipoa.CopyTo(meldungen, 0);
				return meldungen;
			}
			else return null;				
		}

		public Cdv_Einheit[] HoleAlleEinheiten()
		{
			IVerwaltung verw_einheitverwaltung = _ObjektManager.Einheiten;
			IPelsObject[] pout_ipoa = verw_einheitverwaltung.HolenAlle();

			if (pout_ipoa != null)
			{
				Cdv_Einheit[] einheiten = new Cdv_Einheit[pout_ipoa.Length];
				pout_ipoa.CopyTo(einheiten,0);
				return einheiten;
			}//TODO: Fehldermeldungen
			else return null;
		}

		public Cdv_KFZ[] HoleAlleKFZ()
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Kfz;
			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();
			if (ipoa != null)
			{
				Cdv_KFZ[] pout_kfzmenge = new Cdv_KFZ[ipoa.Length];
				ipoa.CopyTo(pout_kfzmenge,0);
				return pout_kfzmenge;
			}//TODO: Fehldermeldungen
			else return null;
		}

		public Cdv_Helfer[] HoleAlleHelfer()
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Helfer;
			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();
			if (ipoa != null)
			{
				Cdv_Helfer[] pout_helfermenge = new Cdv_Helfer[ipoa.Length];
				ipoa.CopyTo(pout_helfermenge,0);

				return pout_helfermenge;
			}//TODO: Fehldermeldungen
			else return null;
		}
		


		#endregion
	}
}

