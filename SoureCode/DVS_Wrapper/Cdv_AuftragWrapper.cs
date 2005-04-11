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
	/// Implementation start: 22.02.05 , alexG
	/// Implementation erstellt: 24.02.05, Giftzwerg
	/// Test: 25.02.05, alexG bis auf die mit TODO gekennzeichneten Pfade!!!!!!!
	/// Test & Debug: 28.02.05 alexG
	/// Update: EmpfaengerBenutzerID & IstInToDoListe hinzugefügt - alexG 05.03.05
	/// </summary>
	public class Cdv_AuftragWrapper: Cdv_WrapperBase
	{
		#region Variablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonCdv_WrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion

		#region Konstruktor
		private Cdv_AuftragWrapper()
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
				_obj_instanzVonCdv_WrapperBase = new Cdv_AuftragWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_WrapperBase;				 
		}	
		#endregion

		#region virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			int i_IDdesAuftrages;
			if (!((pin_ob is Cdv_Auftrag)||(pin_ob is Cdv_Erkundungsbefehl))) throw(new ArgumentNullException("Falsches Objekt an Cdv_AuftragWrapper übergeben. Cdv_Auftrag oder Cdv_Erkundungsbefehl wurde erwartet! Methode:Cdv_AuftragWrapper.NeuerEintrag")); 
			Cdv_Auftrag auftrag = pin_ob as Cdv_Auftrag;
			Cdv_Erkundungsbefehl erkundungsbefehl = pin_ob as Cdv_Erkundungsbefehl;

			String str_INSERTAnfrage;
			
			//das entsprechende Query wird zusammengebaut:
			str_INSERTAnfrage = "insert into \"Auftraege\"("
				+"\"Text\", "
				+"\"Abfassungsdatum\", "
				+"\"Uebermittlungsdatum\", "
				+"\"Absender\", "
				+"\"Uebermittlungsart\", "
				+"\"IstUebermittelt\", "
				+"\"BearbeiterID\", "
				+"\"LaufendeNummer\", "
				+"\"Ausfuehrungszeitpunkt\", "
				+"\"SpaetesterEZP\", "
				+"\"IstBefehl\", "
				+"\"WirdNachverfolgt\", "
				+ "\"EmpfaengerBenutzerID\", "
				+ "\"IstInToDoListe\", ";
			if (pin_ob is Cdv_Erkundungsbefehl) 
			{
				str_INSERTAnfrage=str_INSERTAnfrage					
					+"\"EB_Befehlsart\", "
					+"";
			}
			//Muss immer gesetzt werden: erlaubt auf der DB-Tabelle die Unterscheidung des Typs
			str_INSERTAnfrage += "\"IstErkundungsbefehl\"";
			
			str_INSERTAnfrage=str_INSERTAnfrage
			+") values("
				+"'"+CMethoden.KonvertiereStringFuerDB(auftrag.Text)+"', "
				+"'"+CMethoden.KonvertiereDatumFuerDB(auftrag.Abfassungsdatum)+"', "
				+"'"+CMethoden.KonvertiereDatumFuerDB(auftrag.Uebermittlungsdatum)+"', "
				+"'"+CMethoden.KonvertiereStringFuerDB(auftrag.Absender)+"', "
				+"'"+(int)auftrag.Uebermittlungsart+"',"
				+"'"+auftrag.IstUebermittelt+"',"
				+"'"+auftrag.BearbeiterBenutzerID+"',"
				+"'"+auftrag.LaufendeNummer+"',"
				+"'"+CMethoden.KonvertiereDatumFuerDB(auftrag.Ausfuehrungszeitpunkt)+"',"
				+"'"+CMethoden.KonvertiereDatumFuerDB(auftrag.SpaetesterErfuellungszeitpunkt)+"',"
				+"'"+auftrag.IstBefehl+"',"
				+"'"+auftrag.WirdNachverfolgt+"',"
				+"'"+auftrag.EmpfaengerBenutzerID+"',"
				+"'"+auftrag.IstInToDoListe+"',";	

			if (pin_ob is Cdv_Erkundungsbefehl) 
			{
				str_INSERTAnfrage=str_INSERTAnfrage
					+"'"+(int) erkundungsbefehl.BefehlsArt+"',"
					//Hier wird auf die Datenbank der typ geschrieben -> IstErkundungsbefehl
					+"'"+true+"'"
					+"";
			}
			else			
			{	//Hier wird auf die Datenbank der typ geschrieben -> IstErkundungsbefehl = false
				str_INSERTAnfrage += "'"+false+"'";
			}
			//schließende Klammer der Anfrage
			str_INSERTAnfrage += ");";

			i_IDdesAuftrages=(db.AusfuehrenInsertAnfrage(str_INSERTAnfrage));
			if(auftrag.EmpfaengerMengeKraftID != null)
			{ //getestet
				string str_INSERTAnfrageEmpfaenger="";
				for (Int32 i_tmp1=0; i_tmp1<auftrag.EmpfaengerMengeKraftID.Length; i_tmp1++)
				{
					str_INSERTAnfrageEmpfaenger="insert into \"Empfaenger_Auftrag\""+ 
						"("+
						"\"AuftragsID\","+
						"\"KraftID\"" +
						") values ("
						+"'"+ i_IDdesAuftrages +"'"
						+"," 
						+"'"+ auftrag.EmpfaengerMengeKraftID[i_tmp1] +"'"
						+");";
					db.AusfuehrenInsertAnfrage(str_INSERTAnfrageEmpfaenger);
				}
			}
			return i_IDdesAuftrages;
		}

		
		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			// Objekt umcasten nach Cdv_Auftrag oder Cdv_Erkundungsbefehl
			if (!((pin_ob is Cdv_Auftrag)||(pin_ob is Cdv_Erkundungsbefehl))) throw(new ArgumentNullException("Falsches Objekt an Cdv_AuftragWrapper übergeben. Cdv_Auftrag oder Cdv_Erkundungsbefehl wurde erwartet! Methode:Cdv_AuftragWrapper.AktualisiereEintrag")); 
			Cdv_Auftrag auftrag = pin_ob as Cdv_Auftrag;
			Cdv_Erkundungsbefehl erkundungsbefehl = pin_ob as Cdv_Erkundungsbefehl;
			string str_UPDATEAnfrage;			//AnfrageQuery zum Update des Objekts auf der DB
			string str_INSERTAnfrageEmpfaenger; //AnfrageQuery zur Verknüpfung von Anfragen und Empfängern auf der DB

			//Anfrage 
			str_UPDATEAnfrage = "update \"Auftraege\" set "
				+"\"Text\"='"+CMethoden.KonvertiereStringFuerDB(auftrag.Text)+"', "
				+"\"Abfassungsdatum\"='"+CMethoden.KonvertiereDatumFuerDB(auftrag.Abfassungsdatum)+"', "
				+"\"Uebermittlungsdatum\"='"+CMethoden.KonvertiereDatumFuerDB(auftrag.Uebermittlungsdatum)+"', "
				+"\"Absender\"='"+CMethoden.KonvertiereStringFuerDB(auftrag.Absender)+"', "
				+"\"Uebermittlungsart\"='"+(int)auftrag.Uebermittlungsart+"',"
				+"\"IstUebermittelt\"='"+auftrag.IstUebermittelt+"',"
				+"\"BearbeiterID\"='"+auftrag.BearbeiterBenutzerID+"',"
				+"\"LaufendeNummer\"='"+auftrag.LaufendeNummer+"',"
				+"\"Ausfuehrungszeitpunkt\"='"+CMethoden.KonvertiereDatumFuerDB(auftrag.Ausfuehrungszeitpunkt)+"',"
				+"\"SpaetesterEZP\"='"+CMethoden.KonvertiereDatumFuerDB(auftrag.SpaetesterErfuellungszeitpunkt)+"',"
				+"\"IstBefehl\"='"+auftrag.IstBefehl+"',"
				+"\"WirdNachverfolgt\"='"+ auftrag.WirdNachverfolgt+"', "
				+ "\"EmpfaengerBenutzerID\"='" + Convert.ToInt32(auftrag.EmpfaengerBenutzerID)+ "', "
				+ "\"IstInToDoListe\"='" + auftrag.IstInToDoListe+ "'";	
			if (pin_ob is Cdv_Erkundungsbefehl) 
			{
				str_UPDATEAnfrage=str_UPDATEAnfrage
					+", "
					+"\"IstErkundungsbefehl\"='"+true+"',"
					+"\"EB_Befehlsart\"='"+(int)erkundungsbefehl.BefehlsArt+"'"
					+"";
			}
			str_UPDATEAnfrage += " where \"ID\"="+auftrag.ID;					
			
			
			if (auftrag.EmpfaengerMengeKraftID != null)
			{	
				//erstmal löschen aller Einträge zu  diesem Auftrag/Erkundungsbefehl
				string str_delete = "delete from \"Empfaenger_Auftrag\" where \"AuftragsID\"="+auftrag.ID;
				db.AusfuehrenDeleteAnfrage(str_delete);

				//nun reinschreiben aller neuen Informationen
				for (Int32 i_tmp1=0; i_tmp1<auftrag.EmpfaengerMengeKraftID.Length; i_tmp1++)
				{
					str_INSERTAnfrageEmpfaenger="insert into \"Empfaenger_Auftrag\""+ 
						"("+
						"\"AuftragsID\","+
						"\"KraftID\"" +
						") values ("
						+"'"+ auftrag.ID +"'"
						+"," 
						+"'"+ auftrag.EmpfaengerMengeKraftID[i_tmp1] +"')";

					db.AusfuehrenInsertAnfrage(str_INSERTAnfrageEmpfaenger);
				}
			}
			//Abschließen des UpdateAbfrageString
			str_UPDATEAnfrage += ";";
			return db.AusfuehrenUpdateAnfrage(str_UPDATEAnfrage);
		}

		
		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt (ACHTUNG: Nimmt Auftraege und Erkundungsbefehle auf!)
			NpgsqlDataReader dreader_auftrag_erg;
			NpgsqlDataReader dreader_empfaenger_erg;
			
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen, i_anzahlZeilen2 = 0;
			// Select 
			String str_SELECTAnfrage = "Select * from \"Auftraege\"";
			// Zugriff auf DB
			dreader_auftrag_erg = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			
			// Objekte-Behälter für die Ergebnisse

			Cdv_Auftrag[] auftraege_erg = new Cdv_Auftrag[i_anzahlZeilen];
			
			int i = 0;

			while(dreader_auftrag_erg.Read())
			{
				if(dreader_auftrag_erg.GetBoolean(dreader_auftrag_erg.GetOrdinal("IstErkundungsbefehl")))
					auftraege_erg[i] = new Cdv_Erkundungsbefehl();
				else
					auftraege_erg[i] = new Cdv_Auftrag();

				auftraege_erg[i].Abfassungsdatum=dreader_auftrag_erg.GetDateTime(dreader_auftrag_erg.GetOrdinal("Abfassungsdatum"));
				auftraege_erg[i].Absender=CMethoden.KonvertiereStringAusDB(dreader_auftrag_erg.GetString(dreader_auftrag_erg.GetOrdinal("Absender")));
				auftraege_erg[i].Ausfuehrungszeitpunkt=dreader_auftrag_erg.GetDateTime(dreader_auftrag_erg.GetOrdinal("Ausfuehrungszeitpunkt"));
				auftraege_erg[i].BearbeiterBenutzerID=dreader_auftrag_erg.GetInt32(dreader_auftrag_erg.GetOrdinal("BearbeiterID"));
				auftraege_erg[i].LaufendeNummer=dreader_auftrag_erg.GetInt32(dreader_auftrag_erg.GetOrdinal("LaufendeNummer"));
				auftraege_erg[i].ID=dreader_auftrag_erg.GetInt32(dreader_auftrag_erg.GetOrdinal("ID"));
				
				//erstellen der EmpfängerKräfteIDMenge!!!!
				i_anzahlZeilen2 = 0;
				dreader_empfaenger_erg=db.AusfuehrenSelectAnfrage(("SELECT * FROM \"Empfaenger_Auftrag\" where \"AuftragsID\"="+auftraege_erg[i].ID),out i_anzahlZeilen2);
				auftraege_erg[i].EmpfaengerMengeKraftID = new int[i_anzahlZeilen2];
				int i_tmp1=0;
				
				while (dreader_empfaenger_erg.Read())
				{
					auftraege_erg[i].EmpfaengerMengeKraftID[i_tmp1]=dreader_empfaenger_erg.GetInt32(dreader_empfaenger_erg.GetOrdinal("KraftID"));
					i_tmp1++;
				}
		
				auftraege_erg[i].IstBefehl=dreader_auftrag_erg.GetBoolean(dreader_auftrag_erg.GetOrdinal("IstBefehl"));
				auftraege_erg[i].IstUebermittelt=dreader_auftrag_erg.GetBoolean(dreader_auftrag_erg.GetOrdinal("IstUebermittelt"));
				auftraege_erg[i].SpaetesterErfuellungszeitpunkt=dreader_auftrag_erg.GetDateTime(dreader_auftrag_erg.GetOrdinal("SpaetesterEZP"));
				auftraege_erg[i].Text=CMethoden.KonvertiereStringAusDB(dreader_auftrag_erg.GetString(dreader_auftrag_erg.GetOrdinal("Text")));
				auftraege_erg[i].Uebermittlungsart=(Tdv_Uebermittlungsart) dreader_auftrag_erg.GetInt32(dreader_auftrag_erg.GetOrdinal("Uebermittlungsart"));
				auftraege_erg[i].Uebermittlungsdatum=dreader_auftrag_erg.GetDateTime(dreader_auftrag_erg.GetOrdinal("Uebermittlungsdatum"));
				auftraege_erg[i].WirdNachverfolgt=dreader_auftrag_erg.GetBoolean(dreader_auftrag_erg.GetOrdinal("WirdNachverfolgt"));
				auftraege_erg[i].EmpfaengerBenutzerID = dreader_auftrag_erg.GetInt32(dreader_auftrag_erg.GetOrdinal("EmpfaengerBenutzerID"));
				auftraege_erg[i].IstInToDoListe = dreader_auftrag_erg.GetBoolean(dreader_auftrag_erg.GetOrdinal("IstInToDoListe"));

				if(auftraege_erg[i] is Cdv_Erkundungsbefehl)
				{
					((Cdv_Erkundungsbefehl) auftraege_erg[i]).BefehlsArt= (Tdv_BefehlArt) dreader_auftrag_erg.GetInt32(dreader_auftrag_erg.GetOrdinal("EB_Befehlsart"));
				}

				i++;
			}
			return (IPelsObject[]) auftraege_erg;
		}
		#endregion
	}
}
