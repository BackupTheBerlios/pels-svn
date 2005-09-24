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
	/// Bearbeitet von MIchal & Alex am 24.02.05
	/// getestet: 24.02.05, 14h -> alles ok
	/// 
	/// Änderung: Der Wrapper bildet nicht nur Meldungen sondern auch Erkundungsergebnisse ab
	/// Implementation|Test|Debug : alexG 02.03.05
	/// Update: EmpfaengerBenutzerID & IstInToDoListe hinzugefügt - alexG 05.03.05
	/// </summary>
	
	public class Cdv_MeldungWrapper: Cdv_WrapperBase
	{
		#region Variablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonCdv_WrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion

		#region Konstruktor
		private Cdv_MeldungWrapper()
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
				_obj_instanzVonCdv_WrapperBase = new Cdv_MeldungWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_WrapperBase;				 
		}	
		#endregion

		#region virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			if (!(pin_ob is Cdv_Meldung)) throw(new ArgumentNullException("Falsches Objekt an Cdv_MeldungWrapper übergeben. Cdv_Meldung oder Cdv_Erkundungsergebnis wurde erwartet! Methode:Cdv_ErkundungsergebnisWrapper.NeuerEintrag")); 
			
			int i_IDderMeldung;
			Cdv_Meldung meldg = pin_ob as Cdv_Meldung;
			Cdv_Erkundungsergebnis erkerg = pin_ob as Cdv_Erkundungsergebnis;
			
			StringBuilder strQuery;
			
			//das entsprechende Query wird zusammengebaut:
			strQuery = new StringBuilder("insert into \"Meldungen\"(", 600);
			strQuery.Append( "\"Text\", \"Abfassungsdatum\", \"Uebermittlungsdatum\", \"Absender\", \"Uebermittlungsart\", \"IstUebermittelt\", \"BearbeiterID\", \"LaufendeNummer\", \"Kategorie\", \"EmpfaengerBenutzerID\", \"IstInToDoListe\"");
			//Wenn die Meldung ein Erkundungsergebnis ist
			if(meldg is Cdv_Erkundungsergebnis)
			{
				strQuery.Append(", \"EE_Erkunder\", \"EE_EinsatzschwerpunktID\", \"EO_Bezeichnung\", \"EO_Erkundungsdatum\", \"EO_Haustyp\", \"EO_Bauart\", \"EO_Heizung\", \"EO_Wasserversorgung\", \"EO_Elektroversorgung\", \"EO_Abwasserentsorgung\", \"EO_Keller_Vorhanden\", \"EO_Keller_Prozentsatz\", \"EO_Anschrift_Strasse\", \"EO_Anschrift_Hausnummer\", \"EO_Anschrift_PLZ\", \"EO_Anschrift_Ort\", \"IstErkundungsergebnis\"");
			}				
			strQuery.Append(") values('" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(meldg.Text));
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereDatumFuerDB(meldg.Abfassungsdatum));
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereDatumFuerDB(meldg.Uebermittlungsdatum));
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(meldg.Absender));
			strQuery.Append( "', '" );
			strQuery.Append( Convert.ToInt32(meldg.Uebermittlungsart));
			strQuery.Append( "', '" );
			strQuery.Append( meldg.IstUebermittelt);
			strQuery.Append( "', '" );
			strQuery.Append( meldg.BearbeiterBenutzerID);
			strQuery.Append( "', '" );
			strQuery.Append( meldg.LaufendeNummer);
			strQuery.Append( "', '" );
			strQuery.Append( Convert.ToInt32(meldg.Kategorie));
			strQuery.Append("', '" );
			strQuery.Append( Convert.ToInt32(meldg.EmpfaengerBenutzerID));
			strQuery.Append("', '" );
			strQuery.Append( meldg.IstInToDoListe);
			strQuery.Append("'");
			//Wenn die Meldung ein Erkundungsergebnis ist werden hier die Werte belegt
			if(meldg is Cdv_Erkundungsergebnis)
			{
				strQuery.Append( ", '"); 
				strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkunder));
				strQuery.Append( "', '" );
				strQuery.Append( erkerg.EinsatzschwerpunkID);
				strQuery.Append( "', '" );
				strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Bezeichnung));
				strQuery.Append( "', '" );
				strQuery.Append( CMethoden.KonvertiereDatumFuerDB(erkerg.Erkundungsobjekt.Erkundungsdatum));
				strQuery.Append( "', '" );
				strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Haustyp));
				strQuery.Append( "', '" );
				strQuery.Append( (int) erkerg.Erkundungsobjekt.Bauart);
				strQuery.Append( "', '" );
				strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Heizung));
				strQuery.Append( "', '" );
				strQuery.Append( erkerg.Erkundungsobjekt.Wasserversorgung);
				strQuery.Append( "', '" );
				strQuery.Append( erkerg.Erkundungsobjekt.Elektroversorgung);
				strQuery.Append( "', '" );
				strQuery.Append( erkerg.Erkundungsobjekt.Abwasserentsorgung);
				strQuery.Append( "', '" );
				strQuery.Append( erkerg.Erkundungsobjekt.Keller.Vorhanden);
				strQuery.Append( "', '" );
				strQuery.Append( erkerg.Erkundungsobjekt.Keller.Prozentsatz );
				strQuery.Append( "', '" );
				strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Strasse));
				strQuery.Append( "', '" );
				strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Hausnummer));
				strQuery.Append( "', '" );
				strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.PLZ));
				strQuery.Append( "', '" );
				strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Ort));
				strQuery.Append( "', '" );
				strQuery.Append( true );
				strQuery.Append( "'");
			}
			strQuery.Append(");");
			i_IDderMeldung = db.AusfuehrenInsertAnfrage(strQuery.ToString());
			
			//Nun das Eintragen in die Tabelle Empfaenger_Meldung
			if(meldg.EmpfaengerMengeKraftID !=null)
				foreach(int KraftID in meldg.EmpfaengerMengeKraftID)
				{
					if(KraftID != 0)
					{
						string str_INSERT_Empfaenger_Meldung ="insert into \"Empfaenger_Meldung\" (\"MeldungsID\" , \"KraftID\") values('"+i_IDderMeldung+"','"+KraftID+"');";
						db.AusfuehrenInsertAnfrage(str_INSERT_Empfaenger_Meldung);
					}
				}
			
			return(i_IDderMeldung);	
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if (!(pin_ob is Cdv_Meldung)) throw(new ArgumentNullException("Falsches Objekt an Cdv_MeldungWrapper übergeben. Cdv_Meldung oder Cdv_Erkundungsergebnis wurde erwartet! Methode:Cdv_ErkundungsergebnisWrapper.AktualisiereEintrag")); 
			
			// Objekt umcasten nach Cdv_Einsatz
			Cdv_Meldung meldg = pin_ob as Cdv_Meldung;
			Cdv_Erkundungsergebnis erkerg = pin_ob as Cdv_Erkundungsergebnis;			

			// Anfrage
			StringBuilder strQuery = new StringBuilder("update \"Meldungen\" set ", 500);
				strQuery.Append( "\"Text\"='" );
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(meldg.Text) );
					strQuery.Append( "', \"Abfassungsdatum\"='" );
					strQuery.Append( CMethoden.KonvertiereDatumFuerDB(meldg.Abfassungsdatum));
					strQuery.Append( "', \"Uebermittlungsdatum\"='" );
					strQuery.Append( CMethoden.KonvertiereDatumFuerDB(meldg.Uebermittlungsdatum));
					strQuery.Append( "', \"Absender\"='" );
					strQuery.Append(CMethoden.KonvertiereStringFuerDB(meldg.Absender));
					strQuery.Append( "', \"Uebermittlungsart\"='" );
					strQuery.Append( Convert.ToInt32(meldg.Uebermittlungsart));
					strQuery.Append( "', \"IstUebermittelt\"='" );
					strQuery.Append( meldg.IstUebermittelt);
					strQuery.Append( "', \"BearbeiterID\"='"); 
					strQuery.Append( meldg.BearbeiterBenutzerID);
					strQuery.Append( "', \"LaufendeNummer\"='" );
					strQuery.Append( meldg.LaufendeNummer);
					strQuery.Append( "', \"Kategorie\"='" );
					strQuery.Append( Convert.ToInt32(meldg.Kategorie));
					strQuery.Append( "', \"EmpfaengerBenutzerID\"='" );
					strQuery.Append( Convert.ToInt32(meldg.EmpfaengerBenutzerID));
					strQuery.Append( "', \"IstInToDoListe\"='" );
					strQuery.Append( meldg.IstInToDoListe);
					strQuery.Append( "'");

			//Hier wird zusätzlich codiert, falls die meldung ein Erkundungergebnis ist
			if(meldg is Cdv_Erkundungsergebnis)
			{								
				strQuery.Append( ", \"EE_Erkunder\"='" );
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkunder));
					strQuery.Append( "', \"EE_EinsatzschwerpunktID\"='" );
					strQuery.Append( erkerg.EinsatzschwerpunkID);
					strQuery.Append( "', \"EO_Bezeichnung\"='" );
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Bezeichnung));
					strQuery.Append( "', \"EO_Erkundungsdatum\"='" );
					strQuery.Append( CMethoden.KonvertiereDatumFuerDB(erkerg.Erkundungsobjekt.Erkundungsdatum));
					strQuery.Append( "', \"EO_Haustyp\"='" );
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Haustyp));
					strQuery.Append( "', \"EO_Bauart\"='" );
					strQuery.Append( (int) erkerg.Erkundungsobjekt.Bauart);
					strQuery.Append( "', \"EO_Heizung\"='" );
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Heizung));
					strQuery.Append( "', \"EO_Wasserversorgung\"='" );
					strQuery.Append( erkerg.Erkundungsobjekt.Wasserversorgung);
					strQuery.Append( "', \"EO_Elektroversorgung\"='" );
					strQuery.Append( erkerg.Erkundungsobjekt.Elektroversorgung);
					strQuery.Append( "', \"EO_Abwasserentsorgung\"='" );
					strQuery.Append( erkerg.Erkundungsobjekt.Abwasserentsorgung);
					strQuery.Append( "', \"EO_Keller_Vorhanden\"='" );
					strQuery.Append( erkerg.Erkundungsobjekt.Keller.Vorhanden);
					strQuery.Append( "', \"EO_Keller_Prozentsatz\"='" );
					strQuery.Append( erkerg.Erkundungsobjekt.Keller.Prozentsatz);
					strQuery.Append( "', \"EO_Anschrift_Strasse\"='" );
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Strasse));
					strQuery.Append( "', \"EO_Anschrift_Hausnummer\"='" );
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Hausnummer));
					strQuery.Append( "', \"EO_Anschrift_PLZ\"='" );
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.PLZ));
					strQuery.Append( "', \"EO_Anschrift_Ort\"='" );
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Ort));
					strQuery.Append( "', \"IstErkundungsergebnis\"='" );
					strQuery.Append( true );
					strQuery.Append( "'");
			}			
			strQuery.Append(" where \"ID\"=" );
				strQuery.Append( meldg.ID);
				strQuery.Append(";");
			
			if(db.AusfuehrenUpdateAnfrage(strQuery.ToString()))
			{	
				//Erstmal löschen aller evtl noch vorhandenen Eintragungen
				db.AusfuehrenDeleteAnfrage("delete from \"Empfaenger_Meldung\" where \"MeldungsID\"='"+meldg.ID+"';");

				//Nun das Eintragen in die Tabelle Empfaenger_Meldung
				if(meldg.EmpfaengerMengeKraftID !=null)
					foreach(int KraftID in meldg.EmpfaengerMengeKraftID)
					{
						if(KraftID !=0)
						{
							string str_INSERT_Empfaenger_Meldung ="insert into \"Empfaenger_Meldung\" (\"MeldungsID\",\"KraftID\") values('"+meldg.ID+"','"+KraftID+"');";
							db.AusfuehrenInsertAnfrage(str_INSERT_Empfaenger_Meldung);
						}
					}
			
			}
			else return false;

			return true;
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_meldg_erg;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage
			String str_SELECTAnfrage = "Select * from \"Meldungen\"";
			// Zugriff auf DB
			dreader_meldg_erg = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_Meldung[] meldg_erg = new Cdv_Meldung[i_anzahlZeilen];
			int i = 0;

			while(dreader_meldg_erg.Read())
			{
				if(dreader_meldg_erg.GetBoolean(dreader_meldg_erg.GetOrdinal("istErkundungsergebnis")))
					meldg_erg[i] = new Cdv_Erkundungsergebnis();
				else
					meldg_erg[i] = new Cdv_Meldung();
				meldg_erg[i].ID = dreader_meldg_erg.GetInt32(dreader_meldg_erg.GetOrdinal("ID"));
				meldg_erg[i].Text =CMethoden.KonvertiereStringAusDB(dreader_meldg_erg.GetString(dreader_meldg_erg.GetOrdinal("Text")));
				meldg_erg[i].Abfassungsdatum = dreader_meldg_erg.GetDateTime(dreader_meldg_erg.GetOrdinal("Abfassungsdatum"));
				meldg_erg[i].Uebermittlungsdatum = dreader_meldg_erg.GetDateTime(dreader_meldg_erg.GetOrdinal("Uebermittlungsdatum"));
				meldg_erg[i].Absender =CMethoden.KonvertiereStringAusDB(dreader_meldg_erg.GetString(dreader_meldg_erg.GetOrdinal("Absender")));
				meldg_erg[i].Uebermittlungsart= (Tdv_Uebermittlungsart)dreader_meldg_erg.GetInt32(dreader_meldg_erg.GetOrdinal("Uebermittlungsart"));
				meldg_erg[i].IstUebermittelt = dreader_meldg_erg.GetBoolean(dreader_meldg_erg.GetOrdinal("IstUebermittelt"));
				meldg_erg[i].BearbeiterBenutzerID = dreader_meldg_erg.GetInt32(dreader_meldg_erg.GetOrdinal("BearbeiterID"));
				meldg_erg[i].Kategorie = (Tdv_MeldungsKategorie)dreader_meldg_erg.GetInt32(dreader_meldg_erg.GetOrdinal("Kategorie"));
				meldg_erg[i].LaufendeNummer=dreader_meldg_erg.GetInt32(dreader_meldg_erg.GetOrdinal("LaufendeNummer"));
				meldg_erg[i].EmpfaengerBenutzerID = dreader_meldg_erg.GetInt32(dreader_meldg_erg.GetOrdinal("EmpfaengerBenutzerID"));
				meldg_erg[i].IstInToDoListe = dreader_meldg_erg.GetBoolean(dreader_meldg_erg.GetOrdinal("IstInToDoListe"));
				
				//wenn es sich um ein Erkundungsergebnis handelt
				if(meldg_erg[i] is Cdv_Erkundungsergebnis)
				{					
					((Cdv_Erkundungsergebnis) meldg_erg[i]).Erkunder = CMethoden.KonvertiereStringAusDB(dreader_meldg_erg.GetString(dreader_meldg_erg.GetOrdinal("EE_Erkunder")));
					((Cdv_Erkundungsergebnis) meldg_erg[i]).EinsatzschwerpunkID = dreader_meldg_erg.GetInt32(dreader_meldg_erg.GetOrdinal("EE_EinsatzschwerpunktID"));
					//Auslesen der Informationen zu dem Erkundungsobjekt				
					((Cdv_Erkundungsergebnis) meldg_erg[i]).Erkundungsobjekt.Bezeichnung = CMethoden.KonvertiereStringAusDB(dreader_meldg_erg.GetString(dreader_meldg_erg.GetOrdinal("EO_Bezeichnung")));
					((Cdv_Erkundungsergebnis) meldg_erg[i]).Erkundungsobjekt.Erkundungsdatum = dreader_meldg_erg.GetDateTime(dreader_meldg_erg.GetOrdinal("EO_ErkundungsDatum"));
					((Cdv_Erkundungsergebnis) meldg_erg[i]).Erkundungsobjekt.Haustyp = CMethoden.KonvertiereStringAusDB(dreader_meldg_erg.GetString(dreader_meldg_erg.GetOrdinal("EO_Haustyp")));
					((Cdv_Erkundungsergebnis) meldg_erg[i]).Erkundungsobjekt.Bauart = (Tdv_Bauart) dreader_meldg_erg.GetInt32(dreader_meldg_erg.GetOrdinal("EO_Bauart"));
					//Keller
					((Cdv_Erkundungsergebnis) meldg_erg[i]).Erkundungsobjekt.Keller.Vorhanden = dreader_meldg_erg.GetBoolean(dreader_meldg_erg.GetOrdinal("EO_Keller_Vorhanden"));
					((Cdv_Erkundungsergebnis) meldg_erg[i]).Erkundungsobjekt.Keller.Prozentsatz = dreader_meldg_erg.GetInt16(dreader_meldg_erg.GetOrdinal("EO_Keller_Prozentsatz"));					
					//Anschrift
					((Cdv_Erkundungsergebnis) meldg_erg[i]).Erkundungsobjekt.Anschrift.Strasse = CMethoden.KonvertiereStringAusDB(dreader_meldg_erg.GetString(dreader_meldg_erg.GetOrdinal("EO_Anschrift_Strasse")));
					((Cdv_Erkundungsergebnis) meldg_erg[i]).Erkundungsobjekt.Anschrift.Hausnummer = CMethoden.KonvertiereStringAusDB(dreader_meldg_erg.GetString(dreader_meldg_erg.GetOrdinal("EO_Anschrift_Hausnummer")));
					((Cdv_Erkundungsergebnis) meldg_erg[i]).Erkundungsobjekt.Anschrift.Ort = CMethoden.KonvertiereStringAusDB(dreader_meldg_erg.GetString(dreader_meldg_erg.GetOrdinal("EO_Anschrift_Ort")));
					((Cdv_Erkundungsergebnis) meldg_erg[i]).Erkundungsobjekt.Anschrift.PLZ = CMethoden.KonvertiereStringAusDB(dreader_meldg_erg.GetString(dreader_meldg_erg.GetOrdinal("EO_Anschrift_PLZ")));
					//Versorgung & co
					((Cdv_Erkundungsergebnis) meldg_erg[i]).Erkundungsobjekt.Heizung = CMethoden.KonvertiereStringAusDB(dreader_meldg_erg.GetString(dreader_meldg_erg.GetOrdinal("EO_Heizung")));
					((Cdv_Erkundungsergebnis) meldg_erg[i]).Erkundungsobjekt.Elektroversorgung = dreader_meldg_erg.GetBoolean(dreader_meldg_erg.GetOrdinal("EO_Elektroversorgung"));
					((Cdv_Erkundungsergebnis) meldg_erg[i]).Erkundungsobjekt.Wasserversorgung = dreader_meldg_erg.GetBoolean(dreader_meldg_erg.GetOrdinal("EO_Wasserversorgung"));
					((Cdv_Erkundungsergebnis) meldg_erg[i]).Erkundungsobjekt.Abwasserentsorgung = dreader_meldg_erg.GetBoolean(dreader_meldg_erg.GetOrdinal("EO_Abwasserentsorgung"));																		
				}

				//Auslesen der Empfaenger einer Meldung
				int i_AnzahlZeilen_Empf=0;
				NpgsqlDataReader dreader_meldg_empf_erg = db.AusfuehrenSelectAnfrage("Select * from \"Empfaenger_Meldung\" where \"MeldungsID\"= "+meldg_erg[i].ID+";",out i_AnzahlZeilen_Empf);
				if(i_AnzahlZeilen_Empf > 0)
				{
					meldg_erg[i].EmpfaengerMengeKraftID = new int[i_AnzahlZeilen_Empf];
					int i_tmp=0;
					while(dreader_meldg_empf_erg.Read())
					{
						meldg_erg[i].EmpfaengerMengeKraftID[i_tmp] =dreader_meldg_empf_erg.GetInt32(dreader_meldg_empf_erg.GetOrdinal("KraftID"));
						i_tmp++;
					}
				}	
				i++;
			}

			return meldg_erg;
		}


		#endregion
	}
}
