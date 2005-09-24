using System;
using System.Text;
using pELS.DV.Server.Interfaces;
using pELS.DV;
using pELS.Server;
using System.Data;
using Npgsql;
using pELS.Tools.Server;

namespace pELS.DV.Server.Wrapper
{
	/// <summary>
	/// Summary description for Cdv_EtbEintragWrapper.
	/// </summary>
	public class Cdv_EtbEintragWrapper : Cdv_WrapperBase
	{

		#region Variablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonCdv_WrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion

		#region Konstruktor
		private Cdv_EtbEintragWrapper()
		{
			_i_anzahlReferenzen = 0;
			this.db = Cdv_DB.HoleInstanz();
		}
		#endregion

		#region Statische Methoden
		public static Cdv_WrapperBase HoleInstanz()
		{
			// Instanz erstellen, wenn noch nicht vorhanden
			if (_obj_instanzVonCdv_WrapperBase == null)
				_obj_instanzVonCdv_WrapperBase = new Cdv_EtbEintragWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_WrapperBase;				 
		}	
		#endregion


		#region virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{			
			if(!(pin_ob is Cdv_EtbEintrag))
				throw new ArgumentNullException("Falsches Objekt an Cdv_EtbEintragWrapper übergeben. Cdv_EtbEintrag wurde erwartet! Methode: Cdv_EtbEintragWrapper.NeuerEintrag");
			Cdv_EtbEintrag etbE = pin_ob as Cdv_EtbEintrag;
			StringBuilder strQuery;
			
			strQuery = new StringBuilder("insert into \"EtbEintraege\"(", 300);
			strQuery.Append( "\"Erstelldatum\", ");
			strQuery.Append( "\"Benutzername\", ");
			strQuery.Append( "\"Beschreibung\", ");
			//Da wird der Typ abgebildet
			strQuery.Append( "\"IstSystemereignis\"");
			if(etbE is Cdv_Systemereignis)
			{
				strQuery.Append(", \"Systemereignisart\", \"ErscheintInEtb\"");
			}
			strQuery.Append(") values('");
			strQuery.Append( CMethoden.KonvertiereDatumFuerDB(etbE.ErstellDatum) );
			strQuery.Append( "', '");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(etbE.Benutzername));
			strQuery.Append( "', '");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(etbE.Beschreibung));
			strQuery.Append( "', ");
			if(etbE is Cdv_Systemereignis)
			{
				strQuery.Append("'" );
				strQuery.Append( true );
				strQuery.Append( "', '");
				strQuery.Append( (int) ((Cdv_Systemereignis)etbE).Systemereignisart );
				strQuery.Append( "', '");
				strQuery.Append( ((Cdv_Systemereignis) etbE).ErscheintInEtb );
				strQuery.Append( "');");

			}
			else 
			{
				strQuery.Append( "'" );
				strQuery.Append( false );
				strQuery.Append( "');");
			}
			//das entsprechende Query wird zusammengebaut:
			return db.AusfuehrenInsertAnfrage(strQuery.ToString());	
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_EtbEintrag))
				throw new ArgumentNullException("Falsches Objekt an Cdv_EtbEintragWrapper übergeben. Cdv_EtbEintrag wurde erwartet! Methode: Cdv_EtbEintragWrapper.NeuerEintrag");
			else
			{
				if(!(pin_ob is Cdv_Systemereignis))
					throw new Exception	("Ungültiger Aufruf: EtbEintraege duerfen nicht nachtraeglich veraendert werden. \n Methode: Cdv_EtbEintragWrapper.AktualisiereEintrag");
				else
				{// hier wird nur der Wert: 'erscheintInEtb' geschrieben, da andere Werte nicht verändert(updated) werden dürfen
					string str_UpdateAnfrage = "update \"EtbEintraege\" set "
						+"\"ErscheintInEtb\" = "+ ((Cdv_Systemereignis)pin_ob).ErscheintInEtb
						+" WHERE \"ID\"="+pin_ob.ID+";";					
					return (db.AusfuehrenUpdateAnfrage(str_UpdateAnfrage));
				}
			}
		

			
		}

		public override IPelsObject[] LadeAusDerDB()
		{						
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_etbE_erg;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage
			String str_SELECTAnfrage = "Select * from \"EtbEintraege\"";
			// Zugriff auf DB
			dreader_etbE_erg = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_EtbEintrag[] etbE_erg = new Cdv_EtbEintrag[i_anzahlZeilen];
			int i = 0;
			
			while(dreader_etbE_erg.Read())
			{	
				string str_Benutzername, str_Beschreibung;
				DateTime date_erstellDatum;
				date_erstellDatum = dreader_etbE_erg.GetDateTime(dreader_etbE_erg.GetOrdinal("Erstelldatum"));
				str_Benutzername = CMethoden.KonvertiereStringAusDB(dreader_etbE_erg.GetString(dreader_etbE_erg.GetOrdinal("Benutzername")));
				str_Beschreibung = CMethoden.KonvertiereStringAusDB(dreader_etbE_erg.GetString(dreader_etbE_erg.GetOrdinal("BEschreibung")));

				if(dreader_etbE_erg.GetBoolean(dreader_etbE_erg.GetOrdinal("IstSystemereignis")))
				{
					Tdv_SystemereignisArt syserg =	(Tdv_SystemereignisArt) dreader_etbE_erg.GetInt32(dreader_etbE_erg.GetOrdinal("Systemereignisart"));
					bool b_erscheintInEtb = dreader_etbE_erg.GetBoolean(dreader_etbE_erg.GetOrdinal("ErscheintInEtb"));
					etbE_erg[i] = new Cdv_Systemereignis(str_Benutzername,date_erstellDatum,str_Beschreibung,syserg,b_erscheintInEtb);
				}
				else
					etbE_erg[i] = new Cdv_EtbEintrag(str_Benutzername, date_erstellDatum, str_Beschreibung);				
				
				etbE_erg[i].ID = dreader_etbE_erg.GetInt32(dreader_etbE_erg.GetOrdinal("ID"));		
				
				//TODO: alle Kommentare nochmal auslesen
				i++;
			}
			return etbE_erg;				
		}
		#endregion

	}
}
