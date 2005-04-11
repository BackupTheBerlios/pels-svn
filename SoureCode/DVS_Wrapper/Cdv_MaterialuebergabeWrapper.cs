using System;
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
			String str_INSERTAnfrage = "insert into \"Materialuebergaben\"("
			//Abbilden der Eigenschaften von Materialuebergabe
				+ "\"Datum\", "
				+ "\"AllgBemerkung_Autor\", "
				+ "\"AllgBemerkung_Text\", "
				+ "\"VerleiherID\", "
				+ "\"EmpfaengerID\", "
				+ "\"GutID\", "
				+ "\"Menge\") values("
			//Belegen der Eigenschaftswerte mit Inhalten
				+ "'" + CMethoden.KonvertiereDatumFuerDB(matu.Datum)+ "', "
				+ "'" + CMethoden.KonvertiereStringFuerDB(matu.AllgBemerkungen.Autor)+ "', "
				+ "'" + CMethoden.KonvertiereStringFuerDB(matu.AllgBemerkungen.Text)+ "', "
				+ "'" + matu.VerleiherKraftID + "', "
				+ "'" + matu.EmpfaengerKraftID + "', "
				+ "'" + matu.UebergabepostenGutID + "', "
				+ "'" + matu.Menge + "');";
			return db.AusfuehrenInsertAnfrage(str_INSERTAnfrage);
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Materialuebergabe))
				throw new ArgumentNullException("Falsches Objekt an Cdv_MaterialuebergabeWrapper übergeben. Cdv_Materialuebergabe wurde erwartet! Methode:Cdv_MaterialuebergabeWrapper.AktualisiereEintrag");
			// Objekt umcasten nach Cdv_Materialuebergabe
			Cdv_Materialuebergabe matu = pin_ob as Cdv_Materialuebergabe;
			//UpdateAnfrage
			string str_UpdateAnfrage = "update \"Materialuebergaben\" set "
				+ "\"Datum\" ='"+ CMethoden.KonvertiereDatumFuerDB(matu.Datum)+ "', "
				+ "\"AllgBemerkung_Autor\"='"+ CMethoden.KonvertiereStringFuerDB(matu.AllgBemerkungen.Autor)+ "', "
				+ "\"AllgBemerkung_Text\"='" + CMethoden.KonvertiereStringFuerDB(matu.AllgBemerkungen.Text)+ "', "
				+ "\"VerleiherID\"='" + matu.VerleiherKraftID + "', "
				+ "\"EmpfaengerID\"='" + matu.EmpfaengerKraftID + "', "
				+ "\"GutID\"='" + matu.UebergabepostenGutID + "', "
				+ "\"Menge\"='" + matu.Menge + "'";
			str_UpdateAnfrage += " WHERE \"ID\"='"+matu.ID+"';";
			return db.AusfuehrenUpdateAnfrage(str_UpdateAnfrage);
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
