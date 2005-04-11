using System;
//Messgebox
using System.Windows.Forms;

namespace pELS.APS.Server.Interface
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
	

	
	
	public class CPortalLogik_allgFkt: Cap_PortalLogik, IPortalLogik_allgFkt
	{
		public CPortalLogik_allgFkt(int pin_OMPort, string pin_URL, int pin_Port) : 
			base(pin_OMPort, pin_URL, pin_Port)
		{
		}

		#region Cap_PortalLogik members
		protected override void SetzeRemotingPfad()
		{
			this._Pfad = "PortalAllgFkt";
		}

		public override void StartePortalLogik()
		{
		}
		#endregion


		#region IPortalLogik_allgFkt members
		public Cdv_Benutzer[] HoleAlleBenutzer()
		{	
			IVerwaltung verw_benutzerverwaltung = _ObjektManager.Benutzer;
			IPelsObject[] pout_ipoa = verw_benutzerverwaltung.HolenAlle();
			
			if (pout_ipoa.Length != 0)
			{
				Cdv_Benutzer[] benutzer = new Cdv_Benutzer[pout_ipoa.Length];
				pout_ipoa.CopyTo(benutzer,0);
				//				zum Testen
				//				MessageBox.Show("Nach Init einheiten:"+einheiten[0].Funkrufname);
				return benutzer;
			}
			else return null;
		}

		public Cdv_Benutzer SpeichereBenutzer(Cdv_Benutzer pin_neuerBenutzer)
		{
			Cdv_Benutzer pout_benutzer = (Cdv_Benutzer)_ObjektManager.Benutzer.Speichern(pin_neuerBenutzer);
			return pout_benutzer;
		}
		#endregion


		public int FindeBenutzerID(string pin_Benutzername)
		{
			/* TODO
			Cdv_Benutzer[] benutzermenge = (Cdv_Benutzer[])this._ObjektManager.Benutzer.HolenAlle();	
			int i_ID =-1;
			for(int i=0; i<benutzermenge.Length; i++)
			{
				if(benutzermenge[i].Benutzername.CompareTo(pin_Benutzername) == 0)
					i_ID = benutzermenge[i].ID;
			}
			*/
			//return i_ID;
			return 1;
		}

		//Hier für ETB Systemereignisse werfen
		public void WerfeSystemereignis(Cdv_Systemereignis pin_syserg)
		{
			_ObjektManager.EtbEintraege.Speichern(pin_syserg);		
		}
		

		public bool PruefeAllgVorraussetzungen()
		{
			try
			{
				// prüfe, ob ein Einsatz vorhanden ist
				IPelsObject[] Einsaetze = this._ObjektManager.Einsaetze.HolenAlle();
				if (Einsaetze == null)
					return false;
				else 
				{
					if (Einsaetze.Length == 0)
						return false;
				}
				return true;
			}
			catch
			{
				return false;
			}

		}

	}
}
