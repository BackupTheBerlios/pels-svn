using pELS;
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

	// benötigt für: remotable objects
using System.Runtime.Remoting;
	// benötigt für: TypeFilterLevel
using System.Runtime.Serialization.Formatters;
	// benötigt für: ChannelServices
using System.Runtime.Remoting.Channels;
	// benötigt für: TcpChannel
//using System.Runtime.Remoting.Channels.Tcp;
	// benötigt für: IDictionary
using System.Collections;
using System.Windows.Forms;

namespace PortalLogik_EK
{


	/// <summary>
	/// Summary description for PortalLogik_EK
	/// </summary>
	public class CPortalLogik_EK : Cap_PortalLogik, IPortalLogik_EK
	{
		public CPortalLogik_EK(int pin_OMPort, string pin_URL, int pin_Port) : 
			base(pin_OMPort, pin_URL, pin_Port)
		{
		}

		
		#region Cap_PortalLogik members
		protected override void SetzeRemotingPfad()
		{
			this._Pfad = "PortalEK";
		}

		public override void StartePortalLogik()
		{
		}
		#endregion

		#region IPortalLogik_EK Members
		
		#region "Laden"
		public Cdv_Einheit HoleEinheitZumKfz(int pin_ID)
		{
			//IVerwaltung verw_verwaltung_kfz = this._ObjektManager.Kfz;
			IVerwaltung verw_verwaltung = this._ObjektManager.Einheiten;
			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();
			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				IEnumerator ie = ipoa.GetEnumerator();
				while(ie.MoveNext())
				{
					Cdv_Einheit e = (Cdv_Einheit) ie.Current;
					int[] iaIDs = e.KfzKraefteIDMenge;
					if(iaIDs == null)
						continue;
					IEnumerator ieKfz = iaIDs.GetEnumerator();
					while(ieKfz.MoveNext())
					{
						int iID = (int) ieKfz.Current;
						if(iID == pin_ID)
							return(e);
					}
				}
			}
			return(null);
		}

		public Cdv_Einsatzschwerpunkt HoleESP(int pin_ID)
		{
			IVerwaltung verw_verwaltung = this._ObjektManager.Einsatzschwerpunkte;
			IPelsObject esp = verw_verwaltung.Holen(pin_ID);
			return((Cdv_Einsatzschwerpunkt) esp);
		}

		public Cdv_Modul HoleModul(int pin_ID)
		{
			IVerwaltung verw_verwaltung = this._ObjektManager.Moduln;
			IPelsObject modul = verw_verwaltung.Holen(pin_ID);
			return((Cdv_Modul) modul);
		}

		public Cdv_Einsatzschwerpunkt[] HoleEinsatzschwerpunkte()
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Einsatzschwerpunkte;  			
			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();

			if (!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_Einsatzschwerpunkt[] pout_espMenge = new Cdv_Einsatzschwerpunkt[ipoa.Length];
				ipoa.CopyTo(pout_espMenge,0);
				return pout_espMenge;
			}//TODO: Fehldermeldungen
			else return null;
		}
		
//		public Cdv_Einheit[] HoleAlleEinheiten()
//		{
//			IVerwaltung verw_verwaltung = _ObjektManager.Einheiten;  			
//			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();
//
//			if (ipoa != null)
//			{
//				Cdv_Einheit[] pout_einheitenMenge = new Cdv_Einheit[ipoa.Length];
//				ipoa.CopyTo(pout_einheitenMenge,0);
//				return pout_einheitenMenge;
//			}//TODO: Fehldermeldungen
//			else return null;
//		}

		public Cdv_Helfer[] HoleAlleHelfer()
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Helfer;  			
			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();

			if (!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_Helfer[] pout_espMenge = new Cdv_Helfer[ipoa.Length];
				ipoa.CopyTo(pout_espMenge,0);
				return pout_espMenge;
			}//TODO: Fehldermeldungen
			else return null;
		}
		
		public Cdv_KFZ[] HoleKFZ()
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Kfz;  			
			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();

			if (!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_KFZ[] pout_espMenge = new Cdv_KFZ[ipoa.Length];
				ipoa.CopyTo(pout_espMenge,0);
				return pout_espMenge;
			}//TODO: Fehldermeldungen
			else return null;
		}

		public Cdv_Material[] HoleAlleMaterial()
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Material;
			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();

			if (!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_Material[] pout_espMenge = new Cdv_Material[ipoa.Length];
				ipoa.CopyTo(pout_espMenge,0);
				return pout_espMenge;
			}//TODO: Fehldermeldungen
			else return null;
		}

		public Cdv_Material[] HoleMaterialZumHelfer(int pin_ID)
		{
			Cdv_Material[] Materialmenge = this.HoleAlleMaterial();
			if(!(Materialmenge == null) || (Materialmenge.Length == 0))
			{
				ArrayList alMaterialZuHelfer = new ArrayList();
				foreach(Cdv_Material material in Materialmenge)
				{
					if(material.AktuellerBesitzerKraftID == pin_ID ||
						material.EigentuemerKraftID == pin_ID)
					{
						alMaterialZuHelfer.Add(material);
					}
				}
				Cdv_Material[] pout_MaterialZuHelfer = new Cdv_Material[alMaterialZuHelfer.Count];
				alMaterialZuHelfer.CopyTo(pout_MaterialZuHelfer,0);
				return pout_MaterialZuHelfer;
			}
			else
				return(null);
		}

		public Cdv_Material[] HoleAlleMaterialZuEinheit(int pin_EinheitID)
		{
			Cdv_Material[] Materialmenge = this.HoleAlleMaterial();
			if(!(Materialmenge == null) || (Materialmenge.Length == 0))
			{
				ArrayList arrlist_MaterialZuEinheit = new ArrayList();
				foreach(Cdv_Material material in Materialmenge)
				{
					if(material.AktuellerBesitzerKraftID == pin_EinheitID ||
						material.EigentuemerKraftID == pin_EinheitID)
					{
						arrlist_MaterialZuEinheit.Add(material);
					}
				}
				Cdv_Material[] pout_MaterialZuEinheit = new Cdv_Material[arrlist_MaterialZuEinheit.Count];
				arrlist_MaterialZuEinheit.CopyTo(pout_MaterialZuEinheit,0);
				return pout_MaterialZuEinheit;
			}
			else return Materialmenge;	
		}

		public Cdv_Modul[] HoleModule()	
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Moduln;  			
			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();

			if (!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_Modul[] pout_espMenge = new Cdv_Modul[ipoa.Length];
				ipoa.CopyTo(pout_espMenge,0);
				return pout_espMenge;
			}//TODO: Fehldermeldungen
			else return null;
		}

		public Cdv_Einheit[] HoleAlleEinheiten()	
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Einheiten;
			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();

			if (!(ipoa == null) || (ipoa.Length == 0))
			{	
				Cdv_Einheit[] pout_espMenge = new Cdv_Einheit[ipoa.Length];
				ipoa.CopyTo(pout_espMenge,0);
				return pout_espMenge;
			}//TODO: Fehldermeldungen
			else return null;
		}

		public Cdv_Einheit HoleEinheit(int pin_EinheitsID)
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Einheiten ;
			IPelsObject PelsObject = verw_verwaltung.Holen(pin_EinheitsID );
			return (Cdv_Einheit) PelsObject;
		
		}
		public Cdv_Helfer HoleHelfer(int pin_HelferID)	
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Helfer;
			IPelsObject HelferAlsPelsObject = verw_verwaltung.Holen(pin_HelferID);
			return (Cdv_Helfer) HelferAlsPelsObject;
		}

		public Cdv_KFZ HoleKfz(int pin_KfzID)
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Kfz;
			IPelsObject KfzAlsPelsObject = verw_verwaltung.Holen(pin_KfzID);
			return (Cdv_KFZ) KfzAlsPelsObject;		
		}
		
		public Cdv_Material HoleMaterial(int pin_MaterialID)
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Material;
			IPelsObject MaterialAlsPelsObject = verw_verwaltung.Holen(pin_MaterialID);
			return (Cdv_Material) MaterialAlsPelsObject;		
		}

		public Cdv_Einsatz HoleEinsatz()
		{
			IVerwaltung verw_verwaltung = this._ObjektManager.Einsaetze;
			IPelsObject[] EinsaetzeAlsPelsObjekte = verw_verwaltung.HolenAlle();
			if(!(EinsaetzeAlsPelsObjekte== null) || (EinsaetzeAlsPelsObjekte.Length == 0))
			{
				Cdv_Einsatz[] einsatzMenge = new Cdv_Einsatz[EinsaetzeAlsPelsObjekte.Length];
				EinsaetzeAlsPelsObjekte.CopyTo(einsatzMenge,0);
				return(einsatzMenge[0]);				
			}
			return(null);
		}
		public Cdv_Ortsverband HoleOV(int pin_ID)
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Ortsverbaende;
			IPelsObject OVAlsPelsObject = verw_verwaltung.Holen(pin_ID);
			return (Cdv_Ortsverband) OVAlsPelsObject;		
		}
		public Cdv_Ortsverband[] HoleAlleOrtsverbaende()
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Ortsverbaende; //1  			
			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();

			if (!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_Ortsverband[] pout_OVMenge = new Cdv_Ortsverband[ipoa.Length];	//3
				ipoa.CopyTo(pout_OVMenge,0); //1
				return pout_OVMenge; //1
			}//TODO: Fehldermeldungen
			else return null;
	
		}

		public Cdv_Erkundungsergebnis[] HoleAlleErkundungsergebnisse()
		{
			IVerwaltung verw_verwaltung = _ObjektManager.Meldungen; //1  			
			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();
			if (!(ipoa == null) || (ipoa.Length == 0))
			{
				ArrayList Erkundungsergebnismenge = new ArrayList();
				IEnumerator ie = ipoa.GetEnumerator();
				while(ie.MoveNext())
				{
					if(ie.Current is Cdv_Erkundungsergebnis)
					{
						Cdv_Erkundungsergebnis erk = (Cdv_Erkundungsergebnis) ie.Current;
						Erkundungsergebnismenge.Add(erk);
					}
				}
				Cdv_Erkundungsergebnis[] pout_erkMenge = new Cdv_Erkundungsergebnis[Erkundungsergebnismenge.Count];
				Erkundungsergebnismenge.CopyTo(pout_erkMenge);
				return pout_erkMenge; //1
			}//TODO: Fehldermeldungen
			else return null;
		}

		public Cdv_Erkundungsergebnis[] HoleAlleErkundungsergebnisseZuESP(int pin_ID)
		{
			// TODO: Wie kann man Erkundungsergebnisse laden?

			// Falls der Code woandershin kopiert werden soll, steht in Kommentar "//nummer"
			// die dort zu änderende Stelle und Häufigkeit. (Um das Hin-und Hertesten zu vermeiden)-xiao 
			IVerwaltung verw_verwaltung = _ObjektManager.Meldungen; //1  			
			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();
			if (!(ipoa == null) || (ipoa.Length == 0))
			{
				ArrayList Erkundungsergebnismenge = new ArrayList();
				IEnumerator ie = ipoa.GetEnumerator();
				while(ie.MoveNext())
				{
					if(ie.Current is Cdv_Erkundungsergebnis)
					{
						Cdv_Erkundungsergebnis erk = (Cdv_Erkundungsergebnis) ie.Current;
						if(erk.EinsatzschwerpunkID == pin_ID)
							Erkundungsergebnismenge.Add(erk);
					}
				}
				Cdv_Erkundungsergebnis[] pout_erkMenge = new Cdv_Erkundungsergebnis[Erkundungsergebnismenge.Count];
				Erkundungsergebnismenge.CopyTo(pout_erkMenge);
				return pout_erkMenge; //1
			}//TODO: Fehldermeldungen
			else return null;
//			return null;

		}

		public Cdv_Verbrauchsgut[] HoleAlleVerbrauchsgueter()
		{
			// Falls der Code woandershin kopiert werden soll, steht in Kommentar "//nummer"
			// die dort zu änderende Stelle und Häufigkeit. (Um das Hin-und Hertesten zu vermeiden)-xiao 
			IVerwaltung verw_verwaltung = _ObjektManager.Verbrauchsgueter; //1  			
			IPelsObject[] ipoa = verw_verwaltung.HolenAlle();

			if (!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_Verbrauchsgut[] pout_VerbrauchsgueterMenge = new Cdv_Verbrauchsgut[ipoa.Length];	//3
				ipoa.CopyTo(pout_VerbrauchsgueterMenge,0); //1
				return pout_VerbrauchsgueterMenge; //1
			}//TODO: Fehldermeldungen
			else return null;
		}

		public Cdv_Helfer[] HoleHelferZurEinheit(int pin_EinheitID)
		{
			IVerwaltung verw_verwaltung = this._ObjektManager.Einheiten;
			IVerwaltung verw_verwaltungH = this._ObjektManager.Helfer;
			Cdv_Einheit einheit = (Cdv_Einheit) verw_verwaltung.Holen(pin_EinheitID);
			ArrayList alHelfer = new ArrayList();
			Cdv_Helfer[] pout_Helfer = null;
			if(einheit != null)
			{
				int [] iaIDs = einheit.HelferIDMenge;
				if (iaIDs != null)
				{
					IEnumerator ie = iaIDs.GetEnumerator();
					while(ie.MoveNext())
					{
						Cdv_Helfer helfer = (Cdv_Helfer) verw_verwaltungH.Holen((int) ie.Current);
						if(helfer != null)
							alHelfer.Add(helfer);
					}
					pout_Helfer = new Cdv_Helfer[alHelfer.Count];
					alHelfer.CopyTo(pout_Helfer);
				}
			}
			return(pout_Helfer);
		}

		public Cdv_Einheit[] HoleEinheitenZumModul(Cdv_Modul pin_modul)
		{
			IVerwaltung verw_verwaltung = this._ObjektManager.Einheiten;
			ArrayList alEinheiten = new ArrayList();
			Cdv_Einheit[] out_einheiten = null;
			IPelsObject[] einheiten = verw_verwaltung.HolenAlle();
			if(!(einheiten == null) || (einheiten.Length == 0))
			{
				IEnumerator ie = einheiten.GetEnumerator();
				while(ie.MoveNext())
				{
					Cdv_Einheit e = (Cdv_Einheit) ie.Current;
					if(e.ModulID == pin_modul.ID)
						alEinheiten.Add(e);
				}
				out_einheiten = new Cdv_Einheit[alEinheiten.Count];
				alEinheiten.CopyTo(out_einheiten);
			}
			return(out_einheiten);
		}
		
		#endregion

		#region "Speichern"

		public Cdv_Einsatzschwerpunkt SpeichereESP(Cdv_Einsatzschwerpunkt pin_ESP)
		{
			IVerwaltung ver_verwaltung = this._ObjektManager.Einsatzschwerpunkte;
			return((Cdv_Einsatzschwerpunkt) ver_verwaltung.Speichern(pin_ESP));
		}


		public Cdv_Helfer SpeichereHelfer(Cdv_Helfer pin_helfer)
		{
			IVerwaltung ver_verwaltung = this._ObjektManager.Helfer;
			return((Cdv_Helfer) ver_verwaltung.Speichern(pin_helfer));
		}
		public void SpeichereOV(Cdv_Ortsverband pin_OV)
		{
			IVerwaltung ver_verwaltung = this._ObjektManager.Ortsverbaende;
			ver_verwaltung.Speichern(pin_OV);
		}

		public void SpeichereEinheit(Cdv_Einheit pin_Einheit)
		{
			IVerwaltung ver_verwaltung = this._ObjektManager.Einheiten;
			ver_verwaltung.Speichern(pin_Einheit);
		}

		public int SpeichereKfz(Cdv_KFZ pin_kfz)
		{
			IVerwaltung verw_verwaltung = this._ObjektManager.Kfz;
			Cdv_KFZ myKfz = (Cdv_KFZ) verw_verwaltung.Speichern(pin_kfz);
			return(myKfz.ID);
		}

		public void SpeichereModul(Cdv_Modul pin_modul)
		{
			IVerwaltung verw_verwaltung = this._ObjektManager.Moduln;
			verw_verwaltung.Speichern(pin_modul);
		}

		#endregion

		#endregion
	}
}
