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
	/// Zusammenfassung für HelferWrapper.
	/// ACHTUNG!!! ES WURDE ERST INSERT IMPLEMENTIERT! (Steini, 01.03.2004, 16:40)
	/// </summary>


	public class Cdv_HelferWrapper: Cdv_WrapperBase
	{
		#region Variablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonCdv_WrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion

		#region Konstruktor
		private Cdv_HelferWrapper()
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
				_obj_instanzVonCdv_WrapperBase = new Cdv_HelferWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_WrapperBase;				 
		}	
		#endregion

		#region virtuelle Methoden
		
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Helfer))
				throw new ArgumentNullException("Falsches Objekt an Cdv_HelferWrapper übergeben. Cdv_Helfer wurde erwartet! Methode:Cdv_HelferWrapper.NeuerEintrag");
			Cdv_Helfer Helfer = pin_ob as Cdv_Helfer;
			StringBuilder strQuery;			
			//das entsprechende Query wird zusammengebaut:
			strQuery = new StringBuilder("insert into \"Helfer\"(", 500);
			strQuery.Append( "\"Name\", \"Vorname\", \"GebDatum\", \"Zusatzinfo\", \"Erreichbarkeit\", \"Kommentar_Autor\", \"Kommentar_Text\", \"Kraeftestatus\", \"ModulID\", \"PLZ\", \"Ort\", \"Strasse\", \"Hausnummer\", \"Position\", \"OVID\", \"LetzteVerpflegung\", \"Faehigkeiten\", \"Helferstatus\", \"IstFuehrungskraftVonModul\", \"EinsatzschwerpunkID\") values('");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Name) );
			strQuery.Append("','");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Vorname) );
			strQuery.Append("','");
			strQuery.Append( CMethoden.KonvertiereDatumFuerDB(Helfer.Personendaten.GebDatum) );
			strQuery.Append("','");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.ZusatzInfo) );
			strQuery.Append("','");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Erreichbarkeit) );
			strQuery.Append("','");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Kommentar.Autor) );
			strQuery.Append("','");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Kommentar.Text) );
			strQuery.Append("','");
			strQuery.Append( (int) Helfer.Kraeftestatus );
			strQuery.Append("','");
			strQuery.Append( Helfer.ModulID );
			strQuery.Append("','");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.PLZ) );
			strQuery.Append("','");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.Ort) );
			strQuery.Append("','");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.Strasse) );
			strQuery.Append("','");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.Hausnummer) );
			strQuery.Append("','");
			strQuery.Append( (int)Helfer.Position );
			strQuery.Append("','");
			strQuery.Append( Helfer.OVID );
			strQuery.Append("','");
			strQuery.Append( CMethoden.KonvertiereDatumFuerDB(Helfer.LetzteVerfplegung) );
			strQuery.Append("','");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Faehigkeiten) );
			strQuery.Append("','");
			strQuery.Append( (int)Helfer.Helferstatus );
			strQuery.Append("','");
			strQuery.Append( Helfer.istFuehrungskraftVonModul );
			strQuery.Append("','");
			strQuery.Append( Helfer.EinsatzschwerpunktID );
			strQuery.Append("');");

			return(db.AusfuehrenInsertAnfrage(strQuery.ToString()));
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Helfer))
				throw new ArgumentNullException("Falsches Objekt an Cdv_HelferWrapper übergeben. Cdv_Helfer wurde erwartet! Methode:Cdv_HelferWrapper.AktualisiereEintrag");
			Cdv_Helfer Helfer = pin_ob as Cdv_Helfer;
			StringBuilder strQuery;
			
			//das entsprechende Query wird zusammengebaut:
			strQuery = new StringBuilder("update \"Helfer\" set ", 500);
			strQuery.Append( "\"Name\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Name) );
			strQuery.Append("', \"Vorname\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Vorname) );
			strQuery.Append("', \"GebDatum\"='");
			strQuery.Append( CMethoden.KonvertiereDatumFuerDB(Helfer.Personendaten.GebDatum) );
			strQuery.Append("', \"Zusatzinfo\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.ZusatzInfo) );
			strQuery.Append("', \"Erreichbarkeit\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Erreichbarkeit) );
			strQuery.Append("', \"Kommentar_Autor\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Kommentar.Autor) );
			strQuery.Append("', \"Kommentar_Text\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Kommentar.Text) );
			strQuery.Append("', \"Kraeftestatus\"='");
			strQuery.Append((int) Helfer.Kraeftestatus); 
			strQuery.Append("', \"ModulID\"='");
			strQuery.Append( Helfer.ModulID );
			strQuery.Append("', \"PLZ\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.PLZ) );
			strQuery.Append("', \"Ort\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.Ort) );
			strQuery.Append("', \"Strasse\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.Strasse) );
			strQuery.Append("', \"Hausnummer\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.Hausnummer) );
			strQuery.Append("', \"Position\"='");
			strQuery.Append( (int)Helfer.Position); 
			strQuery.Append("', \"OVID\"='");
			strQuery.Append( Helfer.OVID );
			strQuery.Append("', \"LetzteVerpflegung\"='");
			strQuery.Append( CMethoden.KonvertiereDatumFuerDB(Helfer.LetzteVerfplegung) );
			strQuery.Append("', \"Faehigkeiten\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(Helfer.Faehigkeiten) );
			strQuery.Append("', \"Helferstatus\"='");
			strQuery.Append((int) Helfer.Helferstatus); 
			strQuery.Append("', \"IstFuehrungskraftVonModul\"='");
			strQuery.Append( Helfer.istFuehrungskraftVonModul );
			strQuery.Append("', \"EinsatzschwerpunkID\"='");
			strQuery.Append( Helfer.EinsatzschwerpunktID );
			strQuery.Append("' where \"ID\"=");
			strQuery.Append( Helfer.ID);
			return db.AusfuehrenUpdateAnfrage(strQuery.ToString());
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_Helfer_erg;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage
			String str_SELECTAnfrage = "Select * from \"Helfer\"";
			// Zugriff auf DB
			dreader_Helfer_erg = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_Helfer[] Helfer = new Cdv_Helfer[i_anzahlZeilen];
			int i = 0;
			
			while(dreader_Helfer_erg.Read())
			{
				Helfer[i] = new Cdv_Helfer();
				Helfer[i].ID								=dreader_Helfer_erg.GetInt32(dreader_Helfer_erg.GetOrdinal("ID"));
				Helfer[i].Personendaten.Name				=CMethoden.KonvertiereStringAusDB(dreader_Helfer_erg.GetString(dreader_Helfer_erg.GetOrdinal("Name")));
				Helfer[i].Personendaten.Vorname				=CMethoden.KonvertiereStringAusDB(dreader_Helfer_erg.GetString(dreader_Helfer_erg.GetOrdinal("Vorname")));
				Helfer[i].Personendaten.GebDatum			=dreader_Helfer_erg.GetDateTime(dreader_Helfer_erg.GetOrdinal("GebDatum"));
				Helfer[i].Personendaten.ZusatzInfo			=CMethoden.KonvertiereStringAusDB(dreader_Helfer_erg.GetString(dreader_Helfer_erg.GetOrdinal("Zusatzinfo")));
				Helfer[i].Erreichbarkeit					=CMethoden.KonvertiereStringAusDB(dreader_Helfer_erg.GetString(dreader_Helfer_erg.GetOrdinal("Erreichbarkeit")));
				Helfer[i].Kommentar.Autor					=CMethoden.KonvertiereStringAusDB(dreader_Helfer_erg.GetString(dreader_Helfer_erg.GetOrdinal("Kommentar_Autor")));
				Helfer[i].Kommentar.Text					=CMethoden.KonvertiereStringAusDB(dreader_Helfer_erg.GetString(dreader_Helfer_erg.GetOrdinal("Kommentar_Text")));
				Helfer[i].Kraeftestatus						= (Tdv_Kraeftestatus) dreader_Helfer_erg.GetInt32(dreader_Helfer_erg.GetOrdinal("Kraeftestatus"));
				Helfer[i].ModulID							=dreader_Helfer_erg.GetInt32(dreader_Helfer_erg.GetOrdinal("ModulID"));
				Helfer[i].Personendaten.Anschrift.PLZ		=CMethoden.KonvertiereStringAusDB(dreader_Helfer_erg.GetString(dreader_Helfer_erg.GetOrdinal("PLZ")));
				Helfer[i].Personendaten.Anschrift.Ort		=CMethoden.KonvertiereStringAusDB(dreader_Helfer_erg.GetString(dreader_Helfer_erg.GetOrdinal("Ort")));
				Helfer[i].Personendaten.Anschrift.Strasse	=CMethoden.KonvertiereStringAusDB(dreader_Helfer_erg.GetString(dreader_Helfer_erg.GetOrdinal("Strasse")));
				Helfer[i].Personendaten.Anschrift.Hausnummer=CMethoden.KonvertiereStringAusDB(dreader_Helfer_erg.GetString(dreader_Helfer_erg.GetOrdinal("Hausnummer")));
				Helfer[i].Position							=(Tdv_Position) dreader_Helfer_erg.GetInt32(dreader_Helfer_erg.GetOrdinal("Position"));
				Helfer[i].OVID								=dreader_Helfer_erg.GetInt32(dreader_Helfer_erg.GetOrdinal("OVID"));
				Helfer[i].LetzteVerfplegung					=dreader_Helfer_erg.GetDateTime(dreader_Helfer_erg.GetOrdinal("LetzteVerpflegung"));
				Helfer[i].Faehigkeiten						=CMethoden.KonvertiereStringAusDB(dreader_Helfer_erg.GetString(dreader_Helfer_erg.GetOrdinal("Faehigkeiten")));
				Helfer[i].Helferstatus						=(Tdv_Helferstatus) dreader_Helfer_erg.GetInt32(dreader_Helfer_erg.GetOrdinal("Helferstatus"));
				Helfer[i].istFuehrungskraftVonModul			=dreader_Helfer_erg.GetBoolean(dreader_Helfer_erg.GetOrdinal("IstFuehrungskraftVonModul"));
				Helfer[i].EinsatzschwerpunktID				=dreader_Helfer_erg.GetInt32(dreader_Helfer_erg.GetOrdinal("EinsatzschwerpunkID"));

				i++;
			}
			return Helfer;
		}
		#endregion
	}
}
