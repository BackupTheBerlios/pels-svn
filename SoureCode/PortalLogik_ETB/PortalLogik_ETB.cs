using System;
using System.Collections;

// Benötigt für CrystalReports
using CrystalDecisions.CrystalReports.Engine;

namespace PortalLogik_ETB
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
	/// Summary description for PortalLogik_ETB
	/// </summary>
	public class CPortalLogik_ETB : Cap_PortalLogik, IPortalLogik_Etb
	{
		public CPortalLogik_ETB(int pin_OMPort, string pin_URL, int pin_Port) : 
			base(pin_OMPort, pin_URL, pin_Port)
		{			
		}

		#region Cap_PortalLogik members
		protected override void SetzeRemotingPfad()
		{
			this._Pfad = "PortalETB";
		}

		public override void StartePortalLogik()
		{
		}
		#endregion

		#region IPortalLogik_ETB members

		/// <summary>
		/// Diese Funktion lädt alle Systemereignisse.
		/// <returns name="pout_Systemereignisse">Menge aller Systemereignisse die bisher geworfen wurde.</returns>
		/// </summary>
		public System.IO.Stream ErzeugeEtb()
		{
			// Erstellen eines Reports
			ReportDocument pout_Report = new ReportDocument();
			// Lade Reportvorlage			
			pout_Report.Load(System.IO.Directory.GetCurrentDirectory() + @"\ReportVorlagen\ETB.rpt");
			Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
			// muss als Stream übertragen werden, da ReportDocuments nicht Serializable sind
			return pout_Report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
		}

		/// <summary>
		/// Diese Funktion lädt alle Systemereignisse.
		/// <returns name="pout_Systemereignisse">Menge aller Systemereignisse die bisher geworfen wurde.</returns>
		/// </summary>
		public Cdv_Systemereignis[] LadeSystemereignisse()
		{
			// hole alle Systemereignisse
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.EtbEintraege.HolenAlle(); 
			
			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_EtbEintrag[] tmpEtbEintraege = new Cdv_EtbEintrag[ipoa.Length];
				ipoa.CopyTo(tmpEtbEintraege,0);
			
				// Finde die Systemereignisse
				ArrayList _tmpAL = new ArrayList();
				for (int pos = 0; pos < tmpEtbEintraege.Length; pos++)
				{
					if (tmpEtbEintraege[pos].GetType().ToString() == "pELS.DV.Cdv_Systemereignis") _tmpAL.Add(tmpEtbEintraege[pos]);
				}

				// kopiere Einträge aus ArrayList nach Array
				Cdv_Systemereignis[] pout_Systemereignisse = new Cdv_Systemereignis[_tmpAL.Count];
				_tmpAL.CopyTo(pout_Systemereignisse);

				//nach Datum sortieren
				pout_Systemereignisse = (Cdv_Systemereignis[]) this.SortiereNachDatum(pout_Systemereignisse);
				return pout_Systemereignisse;
			}
			else
				return null;
		}

		/// <summary>
		/// Diese Funktion gibt alle EtbEintraege zurueck, die nicht Systemereignisse sind.
		/// </summary>
		/// <returns>Menge aller EtbZusatzeintraege</returns>
		public Cdv_EtbEintrag[] LadeEtbEintraege()
		{
			// hole alle EtbEintraege
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.EtbEintraege.HolenAlle(); 
			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_EtbEintrag[] tmpEtbEintraege = new Cdv_EtbEintrag[ipoa.Length];
				ipoa.CopyTo(tmpEtbEintraege,0);
			
				//Schmeiße alle Systemereignisse raus
				ArrayList _tmpAL = new ArrayList();
				for (int pos = 0; pos < tmpEtbEintraege.Length; pos++)
				{
					if (tmpEtbEintraege[pos].GetType().ToString() != "pELS.DV.Cdv_Systemereignis") _tmpAL.Add(tmpEtbEintraege[pos]);
				}

				// kopiere Einträge aus ArrayList nach Array
				Cdv_EtbEintrag[] pout_EtbEintraege = new Cdv_EtbEintrag[_tmpAL.Count];
				_tmpAL.CopyTo(pout_EtbEintraege);

				//nach Datum sortieren
				pout_EtbEintraege = this.SortiereNachDatum(pout_EtbEintraege);
				return pout_EtbEintraege;
			}
			else 
				return null;
		}
		
		/// <summary>
		/// Holt alle EtbEintragKommentare und liefert sie in einem Array zurück
		/// </summary>
		/// <returns></returns>
		public Cdv_EtbEintragKommentar[] LadeEtbKommentare()
		{
			// hole alle EtbEintragKommenare
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.EtbKommentare.HolenAlle(); 
			if(!(ipoa == null) || (ipoa.Length == 0))				
			{
					 Cdv_EtbEintragKommentar[] pout_EtbKommentare = new Cdv_EtbEintragKommentar[ipoa.Length];
					 ipoa.CopyTo(pout_EtbKommentare,0);
					 return pout_EtbKommentare;
			}
			else
				return null;
		}

		#region In aktueller Version nicht verwendet, siehe IPortalLogik_ETB
		/// <summary>
		/// Liefert ein Systemereignis auf Grundlage der ID zurück
		/// </summary>
		/// <param name="pin_ID">ID des Systemereignisses</param>
		/// <returns>Das gesuchte Systemereignis</returns>
		public Cdv_Systemereignis LadeSystemereignis(int pin_ID)
		{
			pELS.DV.Server.Interfaces.IPelsObject ipo = _ObjektManager.EtbEintraege.Holen(pin_ID);
			Cdv_Systemereignis pout_syserg = (Cdv_Systemereignis) ipo;			
			return pout_syserg;
		}

		/// <summary>
		/// Liefert einen EtbEintrag auf Grundlage der ID zurück
		/// </summary>
		/// <param name="pin_ID">ID des EtbEintrags</param>
		/// <returns>der gesuchte EtbEIntrag</returns>
		public Cdv_EtbEintrag LadeEtbEintrag(int pin_ID)
		{
			pELS.DV.Server.Interfaces.IPelsObject ipo = _ObjektManager.EtbEintraege.Holen(pin_ID);
			Cdv_EtbEintrag pout_eintrag = (Cdv_EtbEintrag) ipo;			

			return pout_eintrag;
		}

		/// <summary>
		/// Liefert einen EtbKommentar auf Grundlage der ID zurück
		/// </summary>
		/// <param name="pin_ID">ID des EtbEintragKommentar</param>
		/// <returns>der gesuchte Kommentar</returns>
		public Cdv_EtbEintragKommentar LadeEtbKommentar(int pin_ID)
		{
			pELS.DV.Server.Interfaces.IPelsObject ipo = _ObjektManager.EtbKommentare.Holen(pin_ID);
			Cdv_EtbEintragKommentar pout_etbK = (Cdv_EtbEintragKommentar) ipo;
			return pout_etbK;		
		}
		#endregion

		/// <summary>
		/// Schreibt ein neu markiertes Systemereignis in die Datenbank
		/// </summary>
		/// <param name="pin_ID">neuer ETB Eintrag</param>
		/// <returns>nichts</returns>
		public void MarkiereSystemereignis(Cdv_EtbEintrag pin_markiertesSE)
		{
			_ObjektManager.EtbEintraege.Speichern(pin_markiertesSE);
		}
		
		/// <summary>
		/// Speichert einen neuen Zusatzeintrag in die Datenbank
		/// </summary>
		/// <param name="pin_neuerZeOhneID">neuer ETB Eintrag</param>
		/// <returns>Objekt mit ID</returns>
		public Cdv_EtbEintrag SpeichereZusatzeintrag(Cdv_EtbEintrag pin_neuerZeOhneID)
		{
			Cdv_EtbEintrag pout_ZeMitID = (Cdv_EtbEintrag) _ObjektManager.EtbEintraege.Speichern(pin_neuerZeOhneID);
			return pout_ZeMitID;
		}

		/// <summary>
		/// Speichert einen neuen Kommentar zu einem Zusatzeintrag in die Datenbank
		/// </summary>
		/// <param name="pin_ID">neuer Kommentar</param>
		/// <returns>Objekt mit ID</returns>
		public Cdv_EtbEintragKommentar SpeichereEintragKommentar(Cdv_EtbEintragKommentar pin_neuerKommentar)
		{
			Cdv_EtbEintragKommentar pout_KommentarMitID = (Cdv_EtbEintragKommentar) _ObjektManager.EtbKommentare.Speichern(pin_neuerKommentar);
			return pout_KommentarMitID;
		}
		#endregion

		#region Sortieren
		//Implementation des Comparers für EtbEintraege
		private class EtbEDateComparer : IComparer
		{
			public int Compare(object x,object y)
			{
				//evtl. ein try-catch drum !?
				if((x as Cdv_EtbEintrag).ErstellDatum < (y as Cdv_EtbEintrag).ErstellDatum)
					return -1;
				if((x as Cdv_EtbEintrag).ErstellDatum == (y as Cdv_EtbEintrag).ErstellDatum)
					return 0;
				else
					// entspricht der Aussage:
					// if((x as Cdv_EtbEintrag).ErstellDatum > (y as Cdv_EtbEintrag).ErstellDatum)					
					return 1;				
			}
		}

		//Implementation des Comparers für EtbEintragKommentare
		private class EtbKDateComparer : IComparer
		{
			public int Compare(object x,object y)
			{
				//evtl. ein try-catch drum !?
				if((x as Cdv_EtbEintragKommentar).ErstellDatum < (y as Cdv_EtbEintragKommentar).ErstellDatum)
					return -1;
				if((x as Cdv_EtbEintragKommentar).ErstellDatum == (y as Cdv_EtbEintragKommentar).ErstellDatum)
					return 0;
				else
					// entspricht der Aussage:
					// if((x as Cdv_EtbEintragKommentar).ErstellDatum > (y as Cdv_EtbEintragKommentar).ErstellDatum)					
					return 1;				
			}
		}

		/// <summary>
		/// Sortiert EtbEintraege (und auch Systemereignisse) nach dem 
		/// Erstelldatum.
		/// </summary>
		/// <param name="pin_etbEs">zu Sortierendes Array</param>
		/// <returns>Sortiertes Array</returns>
		private Cdv_EtbEintrag[] SortiereNachDatum(Cdv_EtbEintrag[] pin_etbEs)
		{
			IComparer MeinVergleicher = new EtbEDateComparer();
			Array.Sort(pin_etbEs, MeinVergleicher);

			return pin_etbEs;
		}

		private Cdv_EtbEintragKommentar[] SortiereNachDatum(Cdv_EtbEintragKommentar[] pin_etbKs)
		{
			IComparer MeinVergleicher = new EtbKDateComparer();
			Array.Sort(pin_etbKs, MeinVergleicher);

			return pin_etbKs;
		}
		#endregion
	}
}
