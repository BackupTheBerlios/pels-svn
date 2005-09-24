using System;
using System.Text;
using pELS.DV.Server.Interfaces;
using pELS.Server;
using pELS.DV;
using Npgsql;
using System.Data;
using pELS.Tools.Server;

namespace pELS.DV.Server.Wrapper
{
	/// <summary>
	/// Implementation & Test: alexG 28.02.05
	/// </summary>
	public class Cdv_BenutzerWrapper: Cdv_WrapperBase
	{

		#region Variablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonCdv_WrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion

		#region Konstruktor
		private Cdv_BenutzerWrapper()
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
				_obj_instanzVonCdv_WrapperBase = new Cdv_BenutzerWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_WrapperBase;				 
		}	
		#endregion

		#region Virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Benutzer))
				throw new ArgumentNullException("Falsches Objekt an Cdv_BenutzerWrapper übergeben. Cdv_Benutzer wurde erwartet! Methode:Cdv_BenutzerWrapper.NeuerEintrag");

			StringBuilder strQuery = new StringBuilder("insert into \"Benutzer\"(", 300);
			strQuery.Append( "\"Benutzername\", \"Systemrolle\") values('" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(((Cdv_Benutzer) pin_ob).Benutzername) );
			strQuery.Append( "', '" );
			strQuery.Append( Convert.ToInt32(((Cdv_Benutzer) pin_ob).Systemrolle) );
			strQuery.Append( "')");
			// Anfrage an Cdv_DB übermitteln
			return(db.AusfuehrenInsertAnfrage(strQuery.ToString()));	
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Benutzer))
				throw new ArgumentNullException("Falsches Objekt an Cdv_BenutzerWrapper übergeben. Cdv_Benutzer wurde erwartet! Methode:Cdv_BenutzerWrapper.NeuerEintrag");
			StringBuilder strQuery = new StringBuilder("update \"Benutzer\" set ", 200);
			strQuery.Append( "\"Benutzername\"= '" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(((Cdv_Benutzer) pin_ob).Benutzername) );
			strQuery.Append( "', ");
			strQuery.Append( "\"Systemrolle\"= " );
			strQuery.Append( "'" );
			strQuery.Append( Convert.ToInt32(((Cdv_Benutzer) pin_ob).Systemrolle) );
			strQuery.Append( "' where \"ID\"=" );
			strQuery.Append( ((Cdv_Benutzer)pin_ob).ID.ToString());
			// Anfrage an Cdv_DB weiterleiten
			return(db.AusfuehrenUpdateAnfrage(strQuery.ToString()));
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_Benutzer;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage			
			String str_SELECTAnfrage = "select * from \"Benutzer\"";
			dreader_Benutzer = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_Benutzer[] Benutzer_anfrageergebnisse = new Cdv_Benutzer[i_anzahlZeilen];
			int i = 0;

			while(dreader_Benutzer.Read())
			{
				Benutzer_anfrageergebnisse[i] = new Cdv_Benutzer();
				Benutzer_anfrageergebnisse[i].ID = dreader_Benutzer.GetInt32(dreader_Benutzer.GetOrdinal("ID"));
				Benutzer_anfrageergebnisse[i].Benutzername = CMethoden.KonvertiereStringAusDB(dreader_Benutzer.GetString(dreader_Benutzer.GetOrdinal("Benutzername")));
				Benutzer_anfrageergebnisse[i].Systemrolle = (Tdv_Systemrolle) dreader_Benutzer.GetInt32(dreader_Benutzer.GetOrdinal("Systemrolle"));
				i++;
			}

			return(Benutzer_anfrageergebnisse);
		}
		
		public Cdv_Benutzer LoadObject(int pin_ID)
		{
			Cdv_Benutzer pout = null; 
			// Reader, der Daten aufnimmt
			NpgsqlDataReader reader;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage			
			String strQuery = "select * from \"Benutzer\" where \"ID\" = " + pin_ID + ";";
			reader = db.AusfuehrenSelectAnfrage(strQuery, out i_anzahlZeilen);
			if ((i_anzahlZeilen == 1)  && reader.Read())
			{
				pout = new Cdv_Benutzer();
				pout.ID = reader.GetInt32(reader.GetOrdinal("ID"));
				pout.Benutzername = CMethoden.KonvertiereStringAusDB(reader.GetString(reader.GetOrdinal("Benutzername")));
				pout.Systemrolle = (Tdv_Systemrolle) reader.GetInt32(reader.GetOrdinal("Systemrolle"));
			}

			return pout;
		}
		
		public bool RemoveObject(int pin_ID)
		{
			bool pout;
			String strQuery = "delete from \"Benutzer\" where \"ID\" = " + pin_ID + ";";
			pout = db.AusfuehrenUpdateAnfrage(strQuery);
			return pout;
		}
		
				
				
		
		
		
		#endregion
	}

}
