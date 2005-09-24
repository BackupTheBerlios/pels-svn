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
	/// Summary description for Cdv_EtbEintragKommentarWrapper.
	/// </summary>
	public class Cdv_EtbEintragKommentarWrapper : Cdv_WrapperBase
	{

		#region Variablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonCdv_WrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion
		#region Konstruktor
		private Cdv_EtbEintragKommentarWrapper()
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
				_obj_instanzVonCdv_WrapperBase = new Cdv_EtbEintragKommentarWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_WrapperBase;				 
		}	
		#endregion

		#region virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{			
			if(!(pin_ob is Cdv_EtbEintragKommentar))
				throw new ArgumentNullException("Falsches Objekt an Cdv_EtbEintragKommentarWrapper übergeben. Cdv_EtbEintragKommentar wurde erwartet! Methode: Cdv_EtbEintragKommentarWrapper.NeuerEintrag");
			Cdv_EtbEintragKommentar etbK = pin_ob as Cdv_EtbEintragKommentar;
			StringBuilder strQuery;
						
			//das entsprechende Query wird zusammengebaut:
			strQuery = new StringBuilder("insert into \"EtbEintragsKommentare\"(", 300);
			strQuery.Append( "\"Erstelldatum\", ");
			strQuery.Append( "\"EtbEintragID\", ");
			strQuery.Append( "\"ErscheintInEtb\", ");
			strQuery.Append( "\"Kommentar_Autor\", ");
			strQuery.Append( "\"Kommentar_Text\") values(");
			strQuery.Append( "'" ); 
			strQuery.Append( CMethoden.KonvertiereDatumFuerDB(etbK.ErstellDatum) );
			strQuery.Append( "', '");
			strQuery.Append( etbK.EtbEintragID);
			strQuery.Append( "', '");
			strQuery.Append( etbK.ErscheintInEtb );
			strQuery.Append( "', '");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(etbK.Kommentar.Autor));
			strQuery.Append( "', ´'");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(etbK.Kommentar.Text));
			strQuery.Append( "');");
			return db.AusfuehrenInsertAnfrage(strQuery.ToString());	
		}
		/// <summary>
		/// Hier wird nur der Wert, 'erscheintInEtb' neu gesetzt,
		/// da alle anderen Werte als unveränderlich gelten
		/// </summary>
		/// <param name="pin_ob">Übergebener Eintragskommentar</param>
		/// <returns></returns>
		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			//Hier wird nur der Wert, 'erscheintInEtb' neu gesetzt
			if(!(pin_ob is Cdv_EtbEintragKommentar))
				throw new ArgumentNullException("Falsches Objekt an Cdv_EtbEintragKommentarWrapper übergeben.  Cdv_EtbEintragKommentar wurde erwartet! Methode: Cdv_EtbEintragKommentarWrapper.AktualisiereEintrag");
			
			Cdv_EtbEintragKommentar etbK = pin_ob as Cdv_EtbEintragKommentar;
			
			string str_UpdateAnfrage = "update \"EtbEintragsKommentare\" set"
				+" \"ErscheintInEtb\"='"+etbK.ErscheintInEtb+"'"
				+" WHERE \"ID\"='"+etbK.ID+"';";
			
			return(db.AusfuehrenUpdateAnfrage(str_UpdateAnfrage));			 
		}

		public override IPelsObject[] LadeAusDerDB()
		{						
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_etbK_erg;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage
			String str_SELECTAnfrage = "Select * from \"EtbEintragsKommentare\"";
			// Zugriff auf DB
			dreader_etbK_erg = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_EtbEintragKommentar[] etbK_erg = new Cdv_EtbEintragKommentar[i_anzahlZeilen];
			int i = 0;
			
			while(dreader_etbK_erg.Read())
			{					
				DateTime date_erstellDatum;				
				bool b_erscheintInEtb;
				int i_EintragsID;

				date_erstellDatum = dreader_etbK_erg.GetDateTime(dreader_etbK_erg.GetOrdinal("Erstelldatum"));
				i_EintragsID = dreader_etbK_erg.GetInt32(dreader_etbK_erg.GetOrdinal("EtbEintragID"));
				b_erscheintInEtb = dreader_etbK_erg.GetBoolean(dreader_etbK_erg.GetOrdinal("ErscheintInEtb"));

				etbK_erg[i] = new Cdv_EtbEintragKommentar(i_EintragsID, date_erstellDatum, b_erscheintInEtb);
				etbK_erg[i].ID = dreader_etbK_erg.GetInt32(dreader_etbK_erg.GetOrdinal("ID"));
				etbK_erg[i].Kommentar.Autor = CMethoden.KonvertiereStringAusDB(dreader_etbK_erg.GetString(dreader_etbK_erg.GetOrdinal("Kommentar_Autor")));
				etbK_erg[i].Kommentar.Text = CMethoden.KonvertiereStringAusDB(dreader_etbK_erg.GetString(dreader_etbK_erg.GetOrdinal("Kommentar_Text")));								
				
				//TODO: alle Kommentare nochmal auslesen
				i++;
			}
			return etbK_erg;			
		}
		#endregion
	}
}
