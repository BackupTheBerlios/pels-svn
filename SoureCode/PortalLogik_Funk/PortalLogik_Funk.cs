using System;
// benötigt für: ArrayList
using System.Collections;


// TODO: löschen
using System.Windows.Forms;
namespace PortalLogik_Funk
{
	// benötigt für: Cap_PortalLogik
	using pELS.Server;
	// benötigt für: IPortalLogik_XXX
	using pELS.APS.Server.Interface;
	// benötigt für: pELS.DV.Cdv_XXX
	using pELS.DV;
	// benötigt für: ObjectManager
	using pELS.DV.Server.ObjectManager;

	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class CPortalLogik_Funk : Cap_PortalLogik, IPortalLogik_Funk, IPortalLogik_allgemeinMAT
	{
		public CPortalLogik_Funk(int pin_OMPort, string pin_URL, int pin_Port) : 
			base(pin_OMPort, pin_URL, pin_Port)
		{
		}
		#region Cap_PortalLogik members
		protected override void SetzeRemotingPfad()
		{
			this._Pfad = "PortalFunk";
		}

		public override void StartePortalLogik()
		{
		}
		#endregion
		#region IPortalLogik_Funk members
		// lädt alle noch nicht versendeten Mitteilungen 
		public Cdv_Meldung[] LadeAlleNichtVersendetenMeldungen()
		{
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.Meldungen.HolenAlle(); 
			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				// ArrayList, welche alle Mitteilungen enthält, die zurückgegeben werden
				ArrayList _tmpAL = new ArrayList();
				Cdv_Meldung[] tmpMeldungen = new Cdv_Meldung[ipoa.Length];
				ipoa.CopyTo(tmpMeldungen,0);
				// entferne alle bereits versendeten Meldungen
				for (int pos = (tmpMeldungen.Length - 1); pos >= 0; pos--)
				{
					if(!tmpMeldungen[pos].IstUebermittelt) _tmpAL.Add(tmpMeldungen[pos]);
				}

				// kopiere Einträge aus ArrayList nach Array
				Cdv_Meldung[] pout_Meldungen = new Cdv_Meldung[_tmpAL.Count];
				_tmpAL.CopyTo(pout_Meldungen);
				return pout_Meldungen;
			}
			else return null;
		}


		public Cdv_Auftrag[] LadeAlleNichtVersendetenAuftraege()
		{
			// IPelsObjektMenge nimmt alle Auftraege auf
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.Auftraege.HolenAlle();
			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				// ArrayList, welche alle Mitteilungen enthält, die zurückgegeben werden
				ArrayList _tmpAL = new ArrayList();				
				Cdv_Auftrag[] tmpAuftraege = new Cdv_Auftrag[ipoa.Length];
				ipoa.CopyTo(tmpAuftraege,0);
				// entferne alle bereits versendeten Aufträge
				for (int pos = (tmpAuftraege.Length - 1); pos >= 0; pos--)
				{
					if(!tmpAuftraege[pos].IstUebermittelt) _tmpAL.Add(tmpAuftraege[pos]);
				}
				// kopiere Einträge aus ArrayList nach Array
				Cdv_Auftrag[] pout_Auftraege = new Cdv_Auftrag[_tmpAL.Count];
				_tmpAL.CopyTo(pout_Auftraege);
				return pout_Auftraege;
			}
			else return null;
		}


		public Cdv_Einsatzschwerpunkt[] LadeAlleEinsatzschwerpunkte()
		{
			// konvertiere Ergebnis in das korrekte Format
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.Einsatzschwerpunkte.HolenAlle(); 
			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_Einsatzschwerpunkt[] pout_ESP = new Cdv_Einsatzschwerpunkt[ipoa.Length];
				ipoa.CopyTo(pout_ESP,0);
				return pout_ESP;
			}
			else return null;
		}

		public Cdv_Meldung LadeMeldung(int pin_pELSID)
		{
			pELS.DV.Server.Interfaces.IPelsObject ipoa = _ObjektManager.Meldungen.Holen(pin_pELSID); 
			Cdv_Meldung pout_Meldung = (Cdv_Meldung) ipoa;
			return pout_Meldung;
		}
		public Cdv_Auftrag LadeAuftrag(int pin_pELSID)
		{
			pELS.DV.Server.Interfaces.IPelsObject ipoa = _ObjektManager.Auftraege.Holen(pin_pELSID); 
			Cdv_Auftrag pout_Auftrag = (Cdv_Auftrag) ipoa;
			return pout_Auftrag;
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
			// konvertiere Ergebnis in das korrekte Format
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.Auftraege.HolenAlle(); 
			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_Auftrag[] pout_Auftraege = new Cdv_Auftrag[ipoa.Length];
				ipoa.CopyTo(pout_Auftraege,0);
				return pout_Auftraege;
			}
			else return null;
		}

		public Cdv_Meldung[] LadeMeldungen()
		{
			// konvertiere Ergebnis in das korrekte Format
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.Meldungen.HolenAlle(); 
			Cdv_Meldung[] pout_meldungen = new Cdv_Meldung[ipoa.Length];
			ipoa.CopyTo(pout_meldungen,0);

			return pout_meldungen;
		}

		public Cdv_Einheit[] HoleAlleEinheiten()
		{
			// konvertiere Ergebnis in das korrekte Format
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.Einheiten.HolenAlle(); 
			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_Einheit[] pout_Einheiten = new Cdv_Einheit[ipoa.Length];
				ipoa.CopyTo(pout_Einheiten,0);
				return pout_Einheiten;
			}
			else return null;
		}

		public Cdv_KFZ[] HoleAlleKFZ()
		{
			// konvertiere Ergebnis in das korrekte Format
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.Kfz.HolenAlle(); 
			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_KFZ[] pout_KFZ = new Cdv_KFZ[ipoa.Length];
				ipoa.CopyTo(pout_KFZ,0);
				return pout_KFZ;
			}
			else return null;
		}

		public Cdv_Helfer[] HoleAlleHelfer()
		{
			// konvertiere Ergebnis in das korrekte Format
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.Helfer.HolenAlle(); 
			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_Helfer[] pout_Helfer = new Cdv_Helfer[ipoa.Length];
				ipoa.CopyTo(pout_Helfer,0);
				return pout_Helfer;
			}
			else return null;
		}


		#endregion

	}
}









