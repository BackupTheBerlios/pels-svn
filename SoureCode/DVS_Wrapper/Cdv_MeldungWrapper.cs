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
			
			String str_INSERTAnfrage;
			
			//das entsprechende Query wird zusammengebaut:
				str_INSERTAnfrage = "insert into \"Meldungen\"("
					+ "\"Text\", "
					+ "\"Abfassungsdatum\", "
					+ "\"Uebermittlungsdatum\", "
					+ "\"Absender\", "
					+ "\"Uebermittlungsart\", "
					+ "\"IstUebermittelt\", "
					+ "\"BearbeiterID\", "	
					+ "\"LaufendeNummer\", "					
					+ "\"Kategorie\", "
					+ "\"EmpfaengerBenutzerID\", "
					+ "\"IstInToDoListe\"";
					//Wenn die Meldung ein Erkundungsergebnis ist
					if(meldg is Cdv_Erkundungsergebnis)
					{
						str_INSERTAnfrage +=  ", \"EE_Erkunder\", "
										+ "\"EE_EinsatzschwerpunktID\", "				
										//Hier wird der Inhalt des Erkundungsobjektes abgebildet
										+ "\"EO_Bezeichnung\", "
										+ "\"EO_Erkundungsdatum\", "
										+ "\"EO_Haustyp\", "
										+ "\"EO_Bauart\", "
										+ "\"EO_Heizung\", "
										+ "\"EO_Wasserversorgung\", "
										+ "\"EO_Elektroversorgung\", "
										+ "\"EO_Abwasserentsorgung\", "
										//Hier wird der Keller abgebildet
										+ "\"EO_Keller_Vorhanden\", "
										+ "\"EO_Keller_Prozentsatz\", "
										//Hier wird die Anschrift abgebildet
										+ "\"EO_Anschrift_Strasse\", "
										+ "\"EO_Anschrift_Hausnummer\", "
										+ "\"EO_Anschrift_PLZ\", "
										+ "\"EO_Anschrift_Ort\", "
										//Hier wird das Wissen kodiert, ob es sich um eine Meldung oder ein Erkundungsergebnis handelt
										+ "\"IstErkundungsergebnis\"";
					}				
			str_INSERTAnfrage += ") values("
					+ "'" + CMethoden.KonvertiereStringFuerDB(meldg.Text)+ "', "
					+ "'" + CMethoden.KonvertiereDatumFuerDB(meldg.Abfassungsdatum)+ "', "
					+ "'" + CMethoden.KonvertiereDatumFuerDB(meldg.Uebermittlungsdatum)+ "', "
					+ "'" + CMethoden.KonvertiereStringFuerDB(meldg.Absender)+ "', "
					+ "'" + Convert.ToInt32(meldg.Uebermittlungsart)+ "', "
					+ "'" + meldg.IstUebermittelt+ "', "
					+ "'" + meldg.BearbeiterBenutzerID+ "', "	
					+ "'" + meldg.LaufendeNummer+ "', "	
					+ "'" + Convert.ToInt32(meldg.Kategorie)+"', "
					+ "'" + Convert.ToInt32(meldg.EmpfaengerBenutzerID)+"', "
					+ "'" + meldg.IstInToDoListe+"'";
			//Wenn die Meldung ein Erkundungsergebnis ist werden hier die Werte belegt
			if(meldg is Cdv_Erkundungsergebnis)
			{
				str_INSERTAnfrage += ", '" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkunder)+ "', "
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
					//Hier wird der Keller eines Erkundungsobjektes abgebildet
					+ "'" + erkerg.Erkundungsobjekt.Keller.Vorhanden+ "', "
					+ "'" + erkerg.Erkundungsobjekt.Keller.Prozentsatz+ "', "
					//Hier wird der Anschrift des Erkundungsobjektes abgebildet
					+ "'" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Strasse)+ "', "
					+ "'" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Hausnummer)+ "', "
					+ "'" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.PLZ)+ "', "
					+ "'" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Ort)+ "', "
					//Hier wird das wissen abgebildet, dass es sich um einen Erkundungsergebnis handelt
					+ "'" + true + "'";
			
			}
			str_INSERTAnfrage +=");";
			i_IDderMeldung = db.AusfuehrenInsertAnfrage(str_INSERTAnfrage);
			
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
			string myQ = "update \"Meldungen\" set "
				+ "\"Text\"='" +CMethoden.KonvertiereStringFuerDB(meldg.Text) + "', "
				+ "\"Abfassungsdatum\"='" + CMethoden.KonvertiereDatumFuerDB(meldg.Abfassungsdatum)+ "', "
				+ "\"Uebermittlungsdatum\"='" + CMethoden.KonvertiereDatumFuerDB(meldg.Uebermittlungsdatum)+ "', "
				+ "\"Absender\"='" +CMethoden.KonvertiereStringFuerDB(meldg.Absender)+ "', "
				+ "\"Uebermittlungsart\"='" + Convert.ToInt32(meldg.Uebermittlungsart)+ "', "
				+ "\"IstUebermittelt\"='" + meldg.IstUebermittelt+ "', "
				+ "\"BearbeiterID\"='" + meldg.BearbeiterBenutzerID+ "', "
				+ "\"LaufendeNummer\"='" + meldg.LaufendeNummer+ "', "
				+ "\"Kategorie\"='" + Convert.ToInt32(meldg.Kategorie)+ "', "
				+ "\"EmpfaengerBenutzerID\"='" + Convert.ToInt32(meldg.EmpfaengerBenutzerID)+ "', "
				+ "\"IstInToDoListe\"='" + meldg.IstInToDoListe+ "'";

			//Hier wird zusätzlich codiert, falls die meldung ein Erkundungergebnis ist
			if(meldg is Cdv_Erkundungsergebnis)
			{								
				myQ += ", \"EE_Erkunder\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkunder)+ "', "
					+ "\"EE_EinsatzschwerpunktID\"='" + erkerg.EinsatzschwerpunkID+ "', "
					//Hier wird der Inhalt des Erkundungsobjektes abgebildet
					+ "\"EO_Bezeichnung\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Bezeichnung)+ "', "
					+ "\"EO_Erkundungsdatum\"='" + CMethoden.KonvertiereDatumFuerDB(erkerg.Erkundungsobjekt.Erkundungsdatum)+ "', "
					+ "\"EO_Haustyp\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Haustyp)+ "', "
					+ "\"EO_Bauart\"='" + (int) erkerg.Erkundungsobjekt.Bauart+ "', "
					+ "\"EO_Heizung\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Heizung)+ "', "
					+ "\"EO_Wasserversorgung\"='" + erkerg.Erkundungsobjekt.Wasserversorgung+ "', "
					+ "\"EO_Elektroversorgung\"='" + erkerg.Erkundungsobjekt.Elektroversorgung+ "', "
					+ "\"EO_Abwasserentsorgung\"='" + erkerg.Erkundungsobjekt.Abwasserentsorgung+ "', "
					//Hier wird der Keller abgebildet
					+ "\"EO_Keller_Vorhanden\"='" + erkerg.Erkundungsobjekt.Keller.Vorhanden+ "', "
					+ "\"EO_Keller_Prozentsatz\"='" + erkerg.Erkundungsobjekt.Keller.Prozentsatz+ "', "
					//Hier wird die Anschrift abgebildet
					+ "\"EO_Anschrift_Strasse\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Strasse)+ "', "
					+ "\"EO_Anschrift_Hausnummer\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Hausnummer)+ "', "
					+ "\"EO_Anschrift_PLZ\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.PLZ)+ "', "
					+ "\"EO_Anschrift_Ort\"='" + CMethoden.KonvertiereStringFuerDB(erkerg.Erkundungsobjekt.Anschrift.Ort)+ "', "
					+ "\"IstErkundungsergebnis\"='" + true + "'";
			}			
			myQ +=" where \"ID\"=" + meldg.ID+";";
			
			if(db.AusfuehrenUpdateAnfrage(myQ))
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
