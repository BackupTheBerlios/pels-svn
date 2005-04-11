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
			String str_INSERTAnfrage;			
			//das entsprechende Query wird zusammengebaut:
			str_INSERTAnfrage = "insert into \"Helfer\"("
				+ "\"Name\", "
				+ "\"Vorname\", "
				+ "\"GebDatum\", "
				+ "\"Zusatzinfo\", "
				+ "\"Erreichbarkeit\", "
				+ "\"Kommentar_Autor\", "
				+ "\"Kommentar_Text\", "
				+ "\"Kraeftestatus\", "
				+ "\"ModulID\", "
				+ "\"PLZ\", "
				+ "\"Ort\", "
				+ "\"Strasse\", "
				+ "\"Hausnummer\", "
				+ "\"Position\", "
				+ "\"OVID\", "
				+ "\"LetzteVerpflegung\", "
				+ "\"Faehigkeiten\", "
				+ "\"Helferstatus\", "
				+ "\"IstFuehrungskraftVonModul\", "
				+ "\"EinsatzschwerpunkID\""
				+") values("
				+"'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Name) +"',"
				+"'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Vorname) +"',"
				+"'"+ CMethoden.KonvertiereDatumFuerDB(Helfer.Personendaten.GebDatum) +"',"
				+"'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.ZusatzInfo) +"',"
				+"'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Erreichbarkeit) +"',"
				+"'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Kommentar.Autor) +"',"
				+"'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Kommentar.Text) +"',"
				+"'"+ (int) Helfer.Kraeftestatus +"',"
				+"'"+ Helfer.ModulID +"',"
				+"'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.PLZ) +"',"
				+"'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.Ort) +"',"
				+"'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.Strasse) +"',"
				+"'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.Hausnummer) +"',"
				+"'"+ (int)Helfer.Position +"',"
				+"'"+ Helfer.OVID +"',"
				+"'"+ CMethoden.KonvertiereDatumFuerDB(Helfer.LetzteVerfplegung) +"',"
				+"'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Faehigkeiten) +"',"
				+"'"+ (int)Helfer.Helferstatus +"',"
				+"'"+ Helfer.istFuehrungskraftVonModul +"',"
				+"'"+ Helfer.EinsatzschwerpunktID +"'"
				+");";

			return(db.AusfuehrenInsertAnfrage(str_INSERTAnfrage));
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Helfer))
				throw new ArgumentNullException("Falsches Objekt an Cdv_HelferWrapper übergeben. Cdv_Helfer wurde erwartet! Methode:Cdv_HelferWrapper.AktualisiereEintrag");
			Cdv_Helfer Helfer = pin_ob as Cdv_Helfer;
			string myQ;
			
			//das entsprechende Query wird zusammengebaut:
			myQ = "update \"Helfer\" set "
				+ "\"Name\"="
				+ "'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Name) +"', "
				+ "\"Vorname\"="
				+ "'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Vorname) +"', "
				+ "\"GebDatum\"="
				+ "'"+ CMethoden.KonvertiereDatumFuerDB(Helfer.Personendaten.GebDatum) +"', "
				+ "\"Zusatzinfo\"="
				+ "'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.ZusatzInfo) +"', "
				+ "\"Erreichbarkeit\"="
				+ "'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Erreichbarkeit) +"', "
				+ "\"Kommentar_Autor\"="
				+ "'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Kommentar.Autor) +"', "
				+ "\"Kommentar_Text\"="
				+ "'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Kommentar.Text) +"', "
				+ "\"Kraeftestatus\"="
				+ "'"+(int) Helfer.Kraeftestatus +"', "
				+ "\"ModulID\"="
				+ "'"+ Helfer.ModulID +"', "
				+ "\"PLZ\"="
				+ "'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.PLZ) +"', "
				+ "\"Ort\"="
				+ "'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.Ort) +"', "
				+ "\"Strasse\"="
				+ "'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.Strasse) +"', "
				+ "\"Hausnummer\"="
				+ "'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Personendaten.Anschrift.Hausnummer) +"', "
				+ "\"Position\"="
				+ "'"+ (int)Helfer.Position +"', "
				+ "\"OVID\"="
				+ "'"+ Helfer.OVID +"', "
				+ "\"LetzteVerpflegung\"="
				+ "'"+ CMethoden.KonvertiereDatumFuerDB(Helfer.LetzteVerfplegung) +"', "
				+ "\"Faehigkeiten\"="
				+ "'"+ CMethoden.KonvertiereStringFuerDB(Helfer.Faehigkeiten) +"', "
				+ "\"Helferstatus\"="
				+ "'"+(int) Helfer.Helferstatus +"', "
				+ "\"IstFuehrungskraftVonModul\"="
				+ "'"+ Helfer.istFuehrungskraftVonModul +"', "
				+ "\"EinsatzschwerpunkID\"="
				+ "'"+ Helfer.EinsatzschwerpunktID +"'"
				+ " where \"ID\"="+ Helfer.ID;

			return db.AusfuehrenUpdateAnfrage(myQ);
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
