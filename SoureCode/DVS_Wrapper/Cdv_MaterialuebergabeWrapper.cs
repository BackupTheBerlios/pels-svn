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
	///  Implementation: 01.03.05 alexG
	///  test & debug: 02.03.05 Hütte
	/// </summary>
	
	public class Cdv_MaterialuebergabeWrapper: Cdv_WrapperBase
	{		
		#region Klassenvariablen
		private static Cdv_WrapperBase _obj_instanzVonWrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion
		#region statische Methoden
		public static Cdv_WrapperBase HoleInstanz()
		{
			// Instanz erstellen, wenn noch nicht vorhanden
			if (_obj_instanzVonWrapperBase == null)
				_obj_instanzVonWrapperBase = new Cdv_MaterialuebergabeWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonWrapperBase;
		}
		#endregion
		#region Konstruktor
		private Cdv_MaterialuebergabeWrapper()
		{
			this.db = Cdv_DB.HoleInstanz();
			_i_anzahlReferenzen = 0;
		}
		#endregion
		#region virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Materialuebergabe))
				throw new ArgumentNullException("Falsches Objekt an Cdv_MaterialuebergabeWrapper übergeben. Cdv_Materialuebergabe wurde erwartet! Methode:Cdv_MaterialuebergabeWrapper.NeuerEintrag");
			// Objekt umcasten nach Cdv_Materialuebergabe
			Cdv_Materialuebergabe matu = pin_ob as Cdv_Materialuebergabe;
			// Insertanfrage
			StringBuilder strQuery = new StringBuilder("insert into \"Materialuebergaben\"(", 200);
			//Abbilden der Eigenschaften von Materialuebergabe
			strQuery.Append( "\"Datum\", \"AllgBemerkung_Autor\", \"AllgBemerkung_Text\", \"VerleiherID\", \"EmpfaengerID\", \"GutID\", \"Menge\") values('"); 
				strQuery.Append( CMethoden.KonvertiereDatumFuerDB(matu.Datum));
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(matu.AllgBemerkungen.Autor));
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(matu.AllgBemerkungen.Text));
			strQuery.Append( "', '" );
			strQuery.Append( matu.VerleiherKraftID );
			strQuery.Append( "', '" );
			strQuery.Append( matu.EmpfaengerKraftID );
			strQuery.Append( "', '" );
			strQuery.Append( matu.UebergabepostenGutID );
			strQuery.Append( "', '" );
			strQuery.Append( matu.Menge );
			strQuery.Append( "');");
			return db.AusfuehrenInsertAnfrage(strQuery.ToString());
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Materialuebergabe))
				throw new ArgumentNullException("Falsches Objekt an Cdv_MaterialuebergabeWrapper übergeben. Cdv_Materialuebergabe wurde erwartet! Methode:Cdv_MaterialuebergabeWrapper.AktualisiereEintrag");
			// Objekt umcasten nach Cdv_Materialuebergabe
			Cdv_Materialuebergabe matu = pin_ob as Cdv_Materialuebergabe;
			//UpdateAnfrage
			StringBuilder strQuery = new StringBuilder("update \"Materialuebergaben\" set ", 300);
				strQuery.Append( "\"Datum\" ='");
					strQuery.Append( CMethoden.KonvertiereDatumFuerDB(matu.Datum));
					strQuery.Append( "', \"AllgBemerkung_Autor\"='");
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(matu.AllgBemerkungen.Autor));
					strQuery.Append( "', \"AllgBemerkung_Text\"='" );
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(matu.AllgBemerkungen.Text));
					strQuery.Append( "', \"VerleiherID\"='" );
					strQuery.Append( matu.VerleiherKraftID );
					strQuery.Append( "', \"EmpfaengerID\"='" );
					strQuery.Append( matu.EmpfaengerKraftID );
					strQuery.Append( "', \"GutID\"='" );
					strQuery.Append( matu.UebergabepostenGutID );
					strQuery.Append( "', \"Menge\"='" );
					strQuery.Append( matu.Menge );
					strQuery.Append( "' WHERE \"ID\"='");
					strQuery.Append(matu.ID);
					strQuery.Append("';");
			return db.AusfuehrenUpdateAnfrage(strQuery.ToString());
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt 
			NpgsqlDataReader dreader_matu_erg;
			
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen = 0;
			// Select anfrage
			String str_SELECTAnfrage = "Select * from \"Materialuebergaben\"";
			// Zugriff auf DB
			dreader_matu_erg = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			
			// Objekte-Behälter für die Ergebnisse

			Cdv_Materialuebergabe[] matu_erg = new Cdv_Materialuebergabe[i_anzahlZeilen];
			
			int i = 0;
			//alle Attribute belegen
			while(dreader_matu_erg.Read())
			{
				//neues Objekt anlegen
				matu_erg[i] = new Cdv_Materialuebergabe();
				matu_erg[i].ID = dreader_matu_erg.GetInt32(dreader_matu_erg.GetOrdinal("ID"));
				matu_erg[i].Datum = dreader_matu_erg.GetDateTime(dreader_matu_erg.GetOrdinal("Datum"));
				matu_erg[i].VerleiherKraftID = dreader_matu_erg.GetInt32(dreader_matu_erg.GetOrdinal("VerleiherID"));
				matu_erg[i].EmpfaengerKraftID = dreader_matu_erg.GetInt32(dreader_matu_erg.GetOrdinal("EmpfaengerID"));
				matu_erg[i].UebergabepostenGutID = dreader_matu_erg.GetInt32(dreader_matu_erg.GetOrdinal("GutID"));
				matu_erg[i].AllgBemerkungen.Autor = CMethoden.KonvertiereStringAusDB(dreader_matu_erg.GetString(dreader_matu_erg.GetOrdinal("AllgBemerkung_Autor")));
				matu_erg[i].AllgBemerkungen.Text = CMethoden.KonvertiereStringAusDB(dreader_matu_erg.GetString(dreader_matu_erg.GetOrdinal("AllgBemerkung_Text")));
				matu_erg[i].Menge = dreader_matu_erg.GetInt32(dreader_matu_erg.GetOrdinal("Menge"));
				i++;
			}
			return matu_erg;
		}
		#endregion
	}

}
