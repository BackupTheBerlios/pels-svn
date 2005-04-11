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
	/// Zusammenfassung für Cdv_TerminWrapper.
	/// Implementiert & getestet by alexG 28.05.05
	/// Update: IstInToDoListe hinzugefügt - alexG 05.03.05
	/// </summary>
	
	
	public class Cdv_TerminWrapper: Cdv_WrapperBase
	{
		#region Variablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonCdv_WrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion

		#region Konstruktor
		private Cdv_TerminWrapper()
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
				_obj_instanzVonCdv_WrapperBase = new Cdv_TerminWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_WrapperBase;				 
		}	
		#endregion


		#region virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Termin))
				throw new ArgumentNullException("Falsches Objekt an Cdv_TerminWrapper übergeben. Cdv_Termin wurde erwartet! Methode:Cdv_TerminWrapper.NeuerEintrag");
			Cdv_Termin termin = pin_ob as Cdv_Termin;
			int iRetVal = 0;
			String str_INSERTAnfrage;
			
			//das entsprechende Query wird zusammengebaut:
			str_INSERTAnfrage = "insert into \"Termine\"("
				+ "\"Betreff\", "
				+ "\"ZeitVon\", "
				+ "\"ZeitBis\", "
				+ "\"FuerID\", "
				+ "\"VonID\", "
				+ "\"WirdErinnert\", "
				+ "\"IstInToDoListe\","
				+ "\"IstWichtig\") values("
				+ "'" + CMethoden.KonvertiereStringFuerDB(termin.Betreff)+ "', "
				+ "'" + CMethoden.KonvertiereDatumFuerDB(termin.ZeitVon)+ "', "
				+ "'" + CMethoden.KonvertiereDatumFuerDB(termin.ZeitBis)+ "', "
				+ "'" + termin.ErstelltFuerBenutzerID+ "', "
				+ "'" + termin.ErstelltVonBenutzerID+ "', "
				+ "'" + termin.WirdErinnert + "', "
				+ "'" + termin.IstInToDoListe + "', "
				+ "'" + termin.IstWichtig+ "')"				;
			iRetVal = db.AusfuehrenInsertAnfrage(str_INSERTAnfrage);
			termin.ID = iRetVal;
			if(termin.WirdErinnert)
			{
				String str_INSERTTerminAnfrage = "insert into \"Erinnerungen\"("
					+ "\"TerminID\", "
					+ "\"Zeitpunkt\", "
					+ "\"Text\") values("
					+"'" + termin.ID + "', "
					+"'" + CMethoden.KonvertiereDatumFuerDB(termin.Erinnerung.Zeitpunkt) + "', "
					+"'" + CMethoden.KonvertiereStringFuerDB(termin.Erinnerung.Erinnerungstext) + "')";
				iRetVal = db.AusfuehrenInsertAnfrage(str_INSERTTerminAnfrage);
				termin.Erinnerung.ID = iRetVal;
			}
			return(iRetVal);
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Termin))
				throw new ArgumentNullException("Falsches Objekt an Cdv_TerminWrapper übergeben. Cdv_Termin wurde erwartet! Methode:Cdv_TerminWrapper.AktualisiereEintrag");
			// Objekt umcasten nach Cdv_Termin
			Cdv_Termin termin = pin_ob as Cdv_Termin;
			bool bRetVal = false;
			// Anfrage
			string myQ = "update \"Termine\" set"
				+ "\"Betreff\"='" + CMethoden.KonvertiereStringFuerDB(termin.Betreff) + "', "
				+ "\"ZeitVon\"='" + CMethoden.KonvertiereDatumFuerDB(termin.ZeitVon)+ "', "
				+ "\"ZeitBis\"='" + CMethoden.KonvertiereDatumFuerDB(termin.ZeitBis)+ "', "
				+ "\"FuerID\"='" +termin.ErstelltFuerBenutzerID+ "', "
				+ "\"VonID\"='" + termin.ErstelltVonBenutzerID+ "', "
				+ "\"IstWichtig\"='" + termin.IstWichtig+ "', "		
				+ "\"WirdErinnert\"='" + termin.WirdErinnert + "', "
				+ "\"IstInToDoListe\"='" + termin.IstInToDoListe+ "' "
				+ "where \"ID\"=" + termin.ID;

			bRetVal = db.AusfuehrenUpdateAnfrage(myQ);
			if(termin.WirdErinnert && bRetVal)
			{
				string str_ErinnerungUpdate = "update \"Erinnerungen\" set"
					+ "\"Zeitpunkt\"='" + CMethoden.KonvertiereDatumFuerDB(termin.Erinnerung.Zeitpunkt) + "', "
					+ "\"Text\"='" + CMethoden.KonvertiereStringFuerDB(termin.Erinnerung.Erinnerungstext) + "' "
					+ "where \"TerminID\"=" + termin.ID;

				bRetVal = db.AusfuehrenUpdateAnfrage(str_ErinnerungUpdate);
			}
			return(bRetVal);
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_termin_erg;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage
			String str_SELECTAnfrage = "Select * from \"Termine\"";
			// Zugriff auf DB
			dreader_termin_erg = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_Termin[] termin_erg = new Cdv_Termin[i_anzahlZeilen];
			int i = 0;

			while(dreader_termin_erg.Read())
			{
				termin_erg[i] = new Cdv_Termin();
				termin_erg[i].ID = dreader_termin_erg.GetInt32(dreader_termin_erg.GetOrdinal("ID"));
				termin_erg[i].Betreff =CMethoden.KonvertiereStringAusDB(dreader_termin_erg.GetString(dreader_termin_erg.GetOrdinal("Betreff")));
				termin_erg[i].ZeitVon = dreader_termin_erg.GetDateTime(dreader_termin_erg.GetOrdinal("ZeitVon"));
				termin_erg[i].ZeitBis = dreader_termin_erg.GetDateTime(dreader_termin_erg.GetOrdinal("ZeitBis"));
				termin_erg[i].ErstelltFuerBenutzerID = dreader_termin_erg.GetInt32(dreader_termin_erg.GetOrdinal("FuerID"));
				termin_erg[i].ErstelltVonBenutzerID = dreader_termin_erg.GetInt32(dreader_termin_erg.GetOrdinal("VonID"));
				termin_erg[i].IstWichtig = dreader_termin_erg.GetBoolean(dreader_termin_erg.GetOrdinal("IstWichtig"));				
				termin_erg[i].IstInToDoListe = dreader_termin_erg.GetBoolean(dreader_termin_erg.GetOrdinal("IstInToDoListe"));
				termin_erg[i].WirdErinnert = dreader_termin_erg.GetBoolean(dreader_termin_erg.GetOrdinal("WirdErinnert"));
				if(termin_erg[i].WirdErinnert)
				{
					String str_SELECTErinnerungAnfrage = "select * from \"Erinnerungen\" where \"TerminID\"=" + termin_erg[i].ID;
					int iCount;
					NpgsqlDataReader dreader_erg = db.AusfuehrenSelectAnfrage(str_SELECTErinnerungAnfrage, out iCount);
					while(dreader_erg.Read())
					{
						Cdv_Erinnerung erg = new Cdv_Erinnerung();
						erg.TerminID = dreader_erg.GetInt32(dreader_erg.GetOrdinal("TerminID"));
						erg.Zeitpunkt = dreader_erg.GetDateTime(dreader_erg.GetOrdinal("Zeitpunkt"));
						//erg.IstWarnmeldung = dreader_erg.GetBoolean(dreader_erg.GetOrdinal("istWarnmeldung"));
						erg.Erinnerungstext = CMethoden.KonvertiereStringAusDB(dreader_erg.GetString(dreader_erg.GetOrdinal("Text")));
						termin_erg[i].Erinnerung = erg;
					}
				}
				i++;
			}
			return termin_erg;
		}
		#endregion
	}
}
