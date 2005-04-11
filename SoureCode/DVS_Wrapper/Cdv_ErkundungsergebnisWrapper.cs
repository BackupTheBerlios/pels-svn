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
	/// Zusammenfassung für ErkundungsergebnisWrapper.
	/// To be continued- alexG
	/// </summary>
	
	public class Cdv_ErkundungsergebnisWrapper: Cdv_WrapperBase
	{
		#region Variablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonCdv_WrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion

		#region Konstruktor
		private Cdv_ErkundungsergebnisWrapper()
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
				_obj_instanzVonCdv_WrapperBase = new Cdv_ErkundungsergebnisWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_WrapperBase;				 
		}	
		#endregion


		#region virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Erkundungsergebnis))
				throw new ArgumentNullException("Falsches Objekt an Cdv_ErkundungsergebnisWrapper übergeben. Cdv_Erkundungsergebnis wurde erwartet! Methode:Cdv_ErkundungsergebnisWrapper.NeuerEintrag");
			Cdv_Erkundungsergebnis erkerg = pin_ob as Cdv_Erkundungsergebnis;
			String str_INSERTAnfrage;
			
			//das entsprechende Query wird zusammengebaut:
			str_INSERTAnfrage = "insert into \"Erkundungsergebnisse\"("
				+ "\"Erkunder\", "
				+ "\"EinsatzschwerpunktID\", "				
				//Hier wird der Inhalt des Erkundungsobjektes abgebildet
				+ "\"EO_Bezeichnung\", "
				+ "\"EO_ErkundungsDatum\", "
				+ "\"EO_Haustyp\", "
				+ "\"EO_Bauart\", "
				+ "\"EO_Heizung\", "
				+ "\"EO_Wasserversorgung\", "
				+ "\"EO_Elektroversorgung\", "
				+ "\"EO_Abwasserentsorgung\", "
				//Hier wird der Kommentarinhalt des Schadens abgebildet
				+ "\"EO_Schaeden_Autor\", "
				+ "\"EO_Schaeden_Text\", "
				//Hier wird der Keller abgebildet
				+ "\"EO_Keller_vorhanden\", "
				+ "\"EO_Keller_Prozentsatz\", "
				//Hier wird die Anschrift abgebildet
				+ "\"EO_Anschrift_Strasse\", "
				+ "\"EO_Anschrift_Hausnummer\", "
				+ "\"EO_Anschrift_PLZ\", "
				+ "\"EO_Anschrift_Ort\") values("
				+ "'" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkunder)+ "', "
				+ "'" + erkerg.EinsatzschwerpunkID+ "', "				
				//Hier wird der Inhalt des Erkundungsobjektes abgebildet
				+ "'" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Bezeichnung)+ "', "
				+ "'" + CMethoden.KonvertiereDatumFuerDB(erkerg.Erkundungsobjekt.Erkundungsdatum)+ "', "
				+ "'" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Haustyp)+ "', "
				+ "'" + (int) erkerg.Erkundungsobjekt.Bauart+ "', "
				+ "'" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Heizung)+ "', "
				+ "'" + erkerg.Erkundungsobjekt.Wasserversorgung+ "', "
				+ "'" + erkerg.Erkundungsobjekt.Elektroversorgung+ "', "
				+ "'" + erkerg.Erkundungsobjekt.Abwasserentsorgung+ "', "
				//Hier wird der Kommentarinhalt des Schadens abgebildet
				+ "'" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Schaeden.Autor)+ "', "
				+ "'" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Schaeden.Text)+ "', "
				//Hier wird der Keller eines Erkundungsobjektes abgebildet
				+ "'" + erkerg.Erkundungsobjekt.Keller.Vorhanden+ "', "
				+ "'" + erkerg.Erkundungsobjekt.Keller.Prozentsatz+ "', "
				//Hier wird der Anschrift des Erkundungsobjektes abgebildet
				+ "'" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Strasse)+ "', "
				+ "'" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Hausnummer)+ "', "
				+ "'" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.PLZ)+ "', "
				+ "'" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Ort)+ "')";
			return(db.AusfuehrenInsertAnfrage(str_INSERTAnfrage));	
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Erkundungsergebnis))
				throw new ArgumentNullException("Falsches Objekt an Cdv_ErkundungsergebnisWrapper übergeben. Cdv_Erkundungsergebnis wurde erwartet! Methode:Cdv_ErkundungsergebnisWrapper.AktualisiereEintrag");
			//Objekt als Cdv_Erkundungsergebnis casten
			Cdv_Erkundungsergebnis erkerg = pin_ob as Cdv_Erkundungsergebnis;
			String str_UpdateAnfrage;
			
			//das entsprechende Query wird zusammengebaut:
			str_UpdateAnfrage = "update \"Erkundungsergebnisse\" set "
				+ "\"Erkunder\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkunder)+ "', "
				+ "\"EinsatzschwerpunktID\"='" + erkerg.EinsatzschwerpunkID+ "', "
				//Hier wird der Inhalt des Erkundungsobjektes abgebildet
				+ "\"EO_Bezeichnung\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Bezeichnung)+ "', "
				+ "\"EO_ErkundungsDatum\"='" + CMethoden.KonvertiereDatumFuerDB(erkerg.Erkundungsobjekt.Erkundungsdatum)+ "', "
				+ "\"EO_Haustyp\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Haustyp)+ "', "
				+ "\"EO_Bauart\"='" + (int) erkerg.Erkundungsobjekt.Bauart+ "', "
				+ "\"EO_Heizung\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Heizung)+ "', "
				+ "\"EO_Wasserversorgung\"='" + erkerg.Erkundungsobjekt.Wasserversorgung+ "', "
				+ "\"EO_Elektroversorgung\"='" + erkerg.Erkundungsobjekt.Elektroversorgung+ "', "
				+ "\"EO_Abwasserentsorgung\"='" + erkerg.Erkundungsobjekt.Abwasserentsorgung+ "', "
				//Hier wird der Kommentarinhalt des Schadens abgebildet
				+ "\"EO_Schaeden_Autor\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Schaeden.Autor)+ "', "
				+ "\"EO_Schaeden_Text\"'" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Schaeden.Text)+ "', "
				//Hier wird der Keller abgebildet
				+ "\"EO_Keller_vorhanden\"='" + erkerg.Erkundungsobjekt.Keller.Vorhanden+ "', "
				+ "\"EO_Keller_Prozentsatz\"='" + erkerg.Erkundungsobjekt.Keller.Prozentsatz+ "', "
				//Hier wird die Anschrift abgebildet
				+ "\"EO_Anschrift_Strasse\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Strasse)+ "', "
				+ "\"EO_Anschrift_Hausnummer\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Hausnummer)+ "', "
				+ "\"EO_Anschrift_PLZ\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.PLZ)+ "', "
				+ "\"EO_Anschrift_Ort\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Ort)+ "' "
				+"where \"ID\"="+erkerg.ID+";";

			return db.AusfuehrenUpdateAnfrage(str_UpdateAnfrage);
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_erkerg;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage
			String str_SELECTAnfrage = "Select * from \"Erkundungsergebnisse\"";
			// Zugriff auf DB
			dreader_erkerg = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_Erkundungsergebnis[] erkerg = new Cdv_Erkundungsergebnis[i_anzahlZeilen];
			int i = 0;

			while(dreader_erkerg.Read())
			{
				erkerg[i] = new Cdv_Erkundungsergebnis();
				erkerg[i].ID = dreader_erkerg.GetInt32(dreader_erkerg.GetOrdinal("ID"));
				erkerg[i].Erkunder = CMethoden.KonvertiereStringAusDB(dreader_erkerg.GetString(dreader_erkerg.GetOrdinal("Erkunder")));
				erkerg[i].EinsatzschwerpunkID = dreader_erkerg.GetInt32(dreader_erkerg.GetOrdinal("EinsatzschwerpunktID"));
				//Auslesen der Informationen zu dem Erkundungsobjekt				
				erkerg[i].Erkundungsobjekt.Bezeichnung = CMethoden.KonvertiereStringAusDB(dreader_erkerg.GetString(dreader_erkerg.GetOrdinal("EO_Bezeichnung")));
				erkerg[i].Erkundungsobjekt.Erkundungsdatum = dreader_erkerg.GetDateTime(dreader_erkerg.GetOrdinal("EO_ErkundungsDatum"));
				erkerg[i].Erkundungsobjekt.Haustyp = CMethoden.KonvertiereStringAusDB(dreader_erkerg.GetString(dreader_erkerg.GetOrdinal("EO_Haustyp")));
				erkerg[i].Erkundungsobjekt.Bauart = (Tdv_Bauart) dreader_erkerg.GetInt32(dreader_erkerg.GetOrdinal("EO_Bauart"));
				//Keller
				erkerg[i].Erkundungsobjekt.Keller.Vorhanden = dreader_erkerg.GetBoolean(dreader_erkerg.GetOrdinal("EO_Keller_vorhanden"));
				erkerg[i].Erkundungsobjekt.Keller.Prozentsatz = dreader_erkerg.GetInt32(dreader_erkerg.GetOrdinal("EO_Keller_Prozentsatz"));
				//schaeden:Kommentar
				erkerg[i].Erkundungsobjekt.Schaeden.Autor = CMethoden.KonvertiereStringAusDB(dreader_erkerg.GetString(dreader_erkerg.GetOrdinal("EO_Schaeden_Autor")));
				erkerg[i].Erkundungsobjekt.Schaeden.Text = CMethoden.KonvertiereStringAusDB(dreader_erkerg.GetString(dreader_erkerg.GetOrdinal("EO_Schaeden_Text")));
				//Anschrift
				erkerg[i].Erkundungsobjekt.Anschrift.Strasse = CMethoden.KonvertiereStringAusDB(dreader_erkerg.GetString(dreader_erkerg.GetOrdinal("EO_Anschrift_Strasse")));
				erkerg[i].Erkundungsobjekt.Anschrift.Hausnummer = CMethoden.KonvertiereStringAusDB(dreader_erkerg.GetString(dreader_erkerg.GetOrdinal("EO_Anschrift_Hausnummer")));
				erkerg[i].Erkundungsobjekt.Anschrift.Ort = CMethoden.KonvertiereStringAusDB(dreader_erkerg.GetString(dreader_erkerg.GetOrdinal("EO_Anschrift_Ort")));
				erkerg[i].Erkundungsobjekt.Anschrift.PLZ = CMethoden.KonvertiereStringAusDB(dreader_erkerg.GetString(dreader_erkerg.GetOrdinal("EO_Anschrift_PLZ")));
				//Versorgung & co
				erkerg[i].Erkundungsobjekt.Heizung = CMethoden.KonvertiereStringAusDB(dreader_erkerg.GetString(dreader_erkerg.GetOrdinal("EO_Heizung")));
				erkerg[i].Erkundungsobjekt.Elektroversorgung = dreader_erkerg.GetBoolean(dreader_erkerg.GetOrdinal("EO_Elektroversorgung"));
				erkerg[i].Erkundungsobjekt.Wasserversorgung = dreader_erkerg.GetBoolean(dreader_erkerg.GetOrdinal("EO_Wasserversorgung"));
				erkerg[i].Erkundungsobjekt.Abwasserentsorgung = dreader_erkerg.GetBoolean(dreader_erkerg.GetOrdinal("EO_Abwasserentsorgung"));								
				i++;
			}
			return erkerg;			
		}
		#endregion
	}
}
