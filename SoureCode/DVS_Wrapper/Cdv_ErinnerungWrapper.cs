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
	/// Summary description for Cdv_ErinnerungWrapper.
	/// Implementiert & getestet: alexG
	/// </summary>
	public class Cdv_ErinnerungWrapper: Cdv_WrapperBase
	{
		#region Variablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonCdv_WrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion

		#region Konstruktor
		private Cdv_ErinnerungWrapper()
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
				_obj_instanzVonCdv_WrapperBase = new Cdv_ErinnerungWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_WrapperBase;				 
		}	
		#endregion


		#region virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Erinnerung))
				throw new ArgumentNullException("Falsches Objekt an Cdv_ErinnerungWrapper übergeben. Cdv_Erinnerung wurde erwartet! Methode:Cdv_ErinnerungWrapper.NeuerEintrag");
			Cdv_Erinnerung Erinnerung = pin_ob as Cdv_Erinnerung;
			StringBuilder strQuery;
			//das entsprechende Query wird zusammengebaut:
			strQuery = new StringBuilder("insert into \"Erinnerungen\"(", 200);
			strQuery.Append( "\"TerminID\", ");
			strQuery.Append( "\"Zeitpunkt\", ");
			strQuery.Append( "\"Text\") values('");
			strQuery.Append( Erinnerung.TerminID);
			strQuery.Append( "', '");
			strQuery.Append( CMethoden.KonvertiereDatumFuerDB(Erinnerung.Zeitpunkt));
			strQuery.Append( "', '");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Erinnerung.Erinnerungstext));
			strQuery.Append( "')");
			return(db.AusfuehrenInsertAnfrage(strQuery.ToString()));	
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Erinnerung))
				throw new ArgumentNullException("Falsches Objekt an Cdv_ErinnerungWrapper übergeben. Cdv_Erinnerung wurde erwartet! Methode:Cdv_ErinnerungWrapper.AktualisiereEintrag");
			// Objekt umcasten nach Cdv_Erinnerung
			Cdv_Erinnerung Erinnerung = pin_ob as Cdv_Erinnerung;
			// Anfrage
			StringBuilder strQuery = new StringBuilder("update \"Erinnerungen\" set", 300);
			strQuery.Append( "\"TerminID\"='");
			strQuery.Append( Erinnerung.TerminID);
			strQuery.Append( "', \"Zeitpunkt\"='" );
			strQuery.Append( CMethoden.KonvertiereDatumFuerDB(Erinnerung.Zeitpunkt));
			strQuery.Append( "', \"Text\"='" );
			strQuery.Append(CMethoden.KonvertiereStringFuerDB(Erinnerung.Erinnerungstext));
			strQuery.Append( "' where \"ID\"=" );
			strQuery.Append( Erinnerung.ID);
			return db.AusfuehrenUpdateAnfrage(strQuery.ToString());
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_Erinnerung_erg;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage
			String str_SELECTAnfrage = "Select * from \"Erinnerungen\"";
			// Zugriff auf DB
			dreader_Erinnerung_erg = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_Erinnerung[] Erinnerung_erg = new Cdv_Erinnerung[i_anzahlZeilen];
			int i = 0;

			while(dreader_Erinnerung_erg.Read())
			{	
				Erinnerung_erg[i] = new Cdv_Erinnerung();
				Erinnerung_erg[i].ID = dreader_Erinnerung_erg.GetInt32(dreader_Erinnerung_erg.GetOrdinal("ID"));
				Erinnerung_erg[i].TerminID = dreader_Erinnerung_erg.GetInt32(dreader_Erinnerung_erg.GetOrdinal("TerminID"));
				Erinnerung_erg[i].Zeitpunkt = dreader_Erinnerung_erg.GetDateTime(dreader_Erinnerung_erg.GetOrdinal("Zeitpunkt"));
				//Erinnerung_erg[i].IstWarnmeldung = dreader_Erinnerung_erg.GetBoolean(dreader_Erinnerung_erg.GetOrdinal("istWarnmeldung"));				
				Erinnerung_erg[i].Erinnerungstext = CMethoden.KonvertiereStringAusDB(dreader_Erinnerung_erg.GetString(dreader_Erinnerung_erg.GetOrdinal("Text")));				
				i++;
			}
			return Erinnerung_erg;
		}
		#endregion
	}
}
