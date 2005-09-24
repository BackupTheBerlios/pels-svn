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
	/// Implementation: alexG 01.03.05
	/// Debug & Tests: 01.03.05 Hütte
	/// </summary>
	public class Cdv_AnforderungWrapper: Cdv_WrapperBase
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
				_obj_instanzVonWrapperBase = new Cdv_AnforderungWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonWrapperBase;
		}
		#endregion
		#region Konstruktor
		private Cdv_AnforderungWrapper()
		{
			this.db = Cdv_DB.HoleInstanz();
			_i_anzahlReferenzen = 0;
		}
		#endregion
		#region virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Anforderung))
				throw new ArgumentNullException("Falsches Objekt an Cdv_AnforderungWrapper übergeben. Cdv_Anforderung wurde erwartet! Methode:Cdv_AnforderungWrapper.NeuerEintrag");
			// Objekt umcasten nach Cdv_Anforderung
			Cdv_Anforderung myAnf = pin_ob as Cdv_Anforderung;
			// Insertanfrage
			StringBuilder strQuery = new StringBuilder("insert into \"Anforderungen\"(", 300);
			strQuery.Append("\"GutID\", \"Menge\", \"Status\", \"AnforderndeKraftID\", \"Anforderungsdatum\", \"Zufuehrungsdatum\", \"Zweck\", \"Kommentar_Autor\", \"Kommentar_Text\", \"TGA\") values('" );
			strQuery.Append(myAnf.GutID );
			strQuery.Append("', '" );
			strQuery.Append(CMethoden.KonvertiereRealFuerDB(myAnf.Menge) );
			strQuery.Append("', '" );
			strQuery.Append((int) myAnf.AnforderungsStatus );
			strQuery.Append("', '" );
			strQuery.Append(myAnf.AnforderndeKraftID );
			strQuery.Append("', '" );
			strQuery.Append(CMethoden.KonvertiereDatumFuerDB(myAnf.Anforderungsdatum));
			strQuery.Append("', '" );
			strQuery.Append(CMethoden.KonvertiereDatumFuerDB(myAnf.Zufuehrungsdatum));
			strQuery.Append("', '" );
			strQuery.Append(CMethoden.KonvertiereStringFuerDB(myAnf.Zweck));
			strQuery.Append("', '" );
			strQuery.Append(CMethoden.KonvertiereStringFuerDB(myAnf.Kommentar.Autor));
			strQuery.Append("', '" );
			strQuery.Append(CMethoden.KonvertiereStringFuerDB(myAnf.Kommentar.Text));
			strQuery.Append("', '" );
			strQuery.Append(myAnf.IstTeilgueteranforderung );
			strQuery.Append("')");
			return db.AusfuehrenInsertAnfrage(strQuery.ToString());
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Anforderung))
				throw new ArgumentNullException("Falsches Objekt an Cdv_AnforderungWrapper übergeben. Cdv_Anforderung wurde erwartet! Methode:Cdv_AnforderungWrapper.AktualisiereEintrag");
			// Objekt umcasten nach Cdv_Anforderung
			Cdv_Anforderung myAnf = pin_ob as Cdv_Anforderung;
			// Anfrage

			StringBuilder strQuery = new StringBuilder("update \"Anforderungen\" set", 300);
			strQuery.Append("\"GutID\"='");
			strQuery.Append(myAnf.GutID);
			strQuery.Append("', \"Menge\"= '");
			strQuery.Append(CMethoden.KonvertiereRealFuerDB(myAnf.Menge));
			strQuery.Append("', \"Status\"= '");
			strQuery.Append((int) myAnf.AnforderungsStatus);
			strQuery.Append("', \"AnforderndeKraftID\"='");
			strQuery.Append(myAnf.AnforderndeKraftID);
			strQuery.Append("', \"Anforderungsdatum\"='");
			strQuery.Append(CMethoden.KonvertiereDatumFuerDB(myAnf.Anforderungsdatum));
			strQuery.Append("', \"Zufuehrungsdatum\"='");
			strQuery.Append(CMethoden.KonvertiereDatumFuerDB(myAnf.Zufuehrungsdatum));
			strQuery.Append("', \"Zweck\"='");
			strQuery.Append(CMethoden.KonvertiereStringFuerDB(myAnf.Zweck));
			strQuery.Append("', \"Kommentar_Autor\"='");
			strQuery.Append(CMethoden.KonvertiereStringFuerDB(myAnf.Kommentar.Autor));
			strQuery.Append("', \"Kommentar_Text\"='");
			strQuery.Append(CMethoden.KonvertiereStringFuerDB(myAnf.Kommentar.Text));
			strQuery.Append("', \"TGA\"= '");
			strQuery.Append(myAnf.IstTeilgueteranforderung);
			strQuery.Append("' where \"ID\"=");
			strQuery.Append(myAnf.ID);
			return db.AusfuehrenUpdateAnfrage(strQuery.ToString());
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_anforderung_anfrageergebnisse;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage
			String str_SELECTAnfrage = "Select * from \"Anforderungen\"";
			// Zugriff auf DB
			dreader_anforderung_anfrageergebnisse = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_Anforderung[] anforderung_anfrageergebnisse = new Cdv_Anforderung[i_anzahlZeilen];
			int i = 0;

			while(dreader_anforderung_anfrageergebnisse.Read())
			{
				anforderung_anfrageergebnisse[i] = new Cdv_Anforderung();
				anforderung_anfrageergebnisse[i].ID = dreader_anforderung_anfrageergebnisse.GetInt32(dreader_anforderung_anfrageergebnisse.GetOrdinal("ID"));
				anforderung_anfrageergebnisse[i].GutID = dreader_anforderung_anfrageergebnisse.GetInt32(dreader_anforderung_anfrageergebnisse.GetOrdinal("GutID"));				
				anforderung_anfrageergebnisse[i].AnforderndeKraftID = dreader_anforderung_anfrageergebnisse.GetInt32(dreader_anforderung_anfrageergebnisse.GetOrdinal("AnforderndeKraftID"));
				anforderung_anfrageergebnisse[i].Anforderungsdatum = dreader_anforderung_anfrageergebnisse.GetDateTime(dreader_anforderung_anfrageergebnisse.GetOrdinal("Anforderungsdatum"));									
				anforderung_anfrageergebnisse[i].Zufuehrungsdatum = dreader_anforderung_anfrageergebnisse.GetDateTime(dreader_anforderung_anfrageergebnisse.GetOrdinal("Zufuehrungsdatum"));
				anforderung_anfrageergebnisse[i].Zweck = CMethoden.KonvertiereStringAusDB(dreader_anforderung_anfrageergebnisse.GetString(dreader_anforderung_anfrageergebnisse.GetOrdinal("Zweck")));
				anforderung_anfrageergebnisse[i].AnforderungsStatus = (Tdv_AnforderungsStatus) dreader_anforderung_anfrageergebnisse.GetInt32(dreader_anforderung_anfrageergebnisse.GetOrdinal("Status"));
				anforderung_anfrageergebnisse[i].Kommentar.Autor = CMethoden.KonvertiereStringAusDB(dreader_anforderung_anfrageergebnisse.GetString(dreader_anforderung_anfrageergebnisse.GetOrdinal("Kommentar_Autor"))); 
				anforderung_anfrageergebnisse[i].Kommentar.Text = CMethoden.KonvertiereStringAusDB(dreader_anforderung_anfrageergebnisse.GetString(dreader_anforderung_anfrageergebnisse.GetOrdinal("Kommentar_Text"))); 
				anforderung_anfrageergebnisse[i].Menge = dreader_anforderung_anfrageergebnisse.GetFloat(dreader_anforderung_anfrageergebnisse.GetOrdinal("Menge"));
				anforderung_anfrageergebnisse[i].IstTeilgueteranforderung = dreader_anforderung_anfrageergebnisse.GetBoolean(dreader_anforderung_anfrageergebnisse.GetOrdinal("TGA"));
				i++;
			}
			return anforderung_anfrageergebnisse;	
		}

		#endregion
	}
}
