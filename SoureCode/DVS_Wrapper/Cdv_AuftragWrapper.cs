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

			StringBuilder strQuery = new StringBuilder("insert into \"Auftraege\"(", 300);

			//das entsprechende Query wird zusammengebaut:
			strQuery.Append("\"Text\", \"Abfassungsdatum\", \"Uebermittlungsdatum\", \"Absender\", \"Uebermittlungsart\", \"IstUebermittelt\", \"BearbeiterID\", \"LaufendeNummer\", \"Ausfuehrungszeitpunkt\", \"SpaetesterEZP\", \"IstBefehl\", \"WirdNachverfolgt\", \"EmpfaengerBenutzerID\", \"IstInToDoListe\", ");
			if (pin_ob is Cdv_Erkundungsbefehl) 
			{
				strQuery.Append("\"EB_Befehlsart\", ");
				//				str_INSERTAnfrage.Append("");
			}
			//Muss immer gesetzt werden: erlaubt auf der DB-Tabelle die Unterscheidung des Typs
			strQuery.Append("\"IstErkundungsbefehl\") values('");
			strQuery.Append(CMethoden.KonvertiereStringFuerDB(auftrag.Text));
			strQuery.Append("', '");
			strQuery.Append(CMethoden.KonvertiereDatumFuerDB(auftrag.Abfassungsdatum));
			strQuery.Append("', '");
			strQuery.Append(CMethoden.KonvertiereDatumFuerDB(auftrag.Uebermittlungsdatum));
			strQuery.Append("', '");
			strQuery.Append(CMethoden.KonvertiereStringFuerDB(auftrag.Absender));
			strQuery.Append("', '");
			strQuery.Append((int)auftrag.Uebermittlungsart);
			strQuery.Append("','");
			strQuery.Append(auftrag.IstUebermittelt);
			strQuery.Append("','");
			strQuery.Append(auftrag.BearbeiterBenutzerID);
			strQuery.Append("','");
			strQuery.Append(auftrag.LaufendeNummer);
			strQuery.Append("','");
			strQuery.Append(CMethoden.KonvertiereDatumFuerDB(auftrag.Ausfuehrungszeitpunkt));
			strQuery.Append("','");
			strQuery.Append(CMethoden.KonvertiereDatumFuerDB(auftrag.SpaetesterErfuellungszeitpunkt));
			strQuery.Append("','");
			strQuery.Append(auftrag.IstBefehl);
			strQuery.Append("','");
			strQuery.Append(auftrag.WirdNachverfolgt);
			strQuery.Append("','");
			strQuery.Append(auftrag.EmpfaengerBenutzerID);
			strQuery.Append("','");
			strQuery.Append(auftrag.IstInToDoListe);
			strQuery.Append("',");
			if (pin_ob is Cdv_Erkundungsbefehl) 
			{
				strQuery.Append("'");
				strQuery.Append((int) erkundungsbefehl.BefehlsArt);
				//Hier wird auf die Datenbank der typ geschrieben -> IstErkundungsbefehl
				strQuery.Append("','");
				strQuery.Append(true);
				strQuery.Append("'");
			}
			else			
			{	//Hier wird auf die Datenbank der typ geschrieben -> IstErkundungsbefehl = false
				strQuery.Append("'");
				strQuery.Append(false);
				strQuery.Append("'");
			}
			//schließende Klammer der Anfrage
			strQuery.Append(");");

			i_IDdesAuftrages=(db.AusfuehrenInsertAnfrage(strQuery.ToString()));
			if(auftrag.EmpfaengerMengeKraftID != null)
			{ //getestet
				StringBuilder str_INSERTAnfrageEmpfaenger = new StringBuilder("");
				for (Int32 i_tmp1=0; i_tmp1<auftrag.EmpfaengerMengeKraftID.Length; i_tmp1++)
				{
					strQuery.Append("insert into \"Empfaenger_Auftrag\"(\"AuftragsID\",\"KraftID\") values ('");
					strQuery.Append( i_IDdesAuftrages );
					strQuery.Append("', '");
					strQuery.Append(auftrag.EmpfaengerMengeKraftID[i_tmp1]);
					strQuery.Append("');");
					db.AusfuehrenInsertAnfrage(str_INSERTAnfrageEmpfaenger.ToString());
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
			StringBuilder strQuery;			//AnfrageQuery zum Update des Objekts auf der DB
			StringBuilder str_INSERTAnfrageEmpfaenger; //AnfrageQuery zur Verknüpfung von Anfragen und Empfängern auf der DB
			//Anfrage 
			strQuery = new StringBuilder("update \"Auftraege\" set ", 300);
			strQuery.Append("\"Text\"='");
			strQuery.Append(CMethoden.KonvertiereStringFuerDB(auftrag.Text));
			strQuery.Append("', ");
			strQuery.Append("\"Abfassungsdatum\"='");
			strQuery.Append(CMethoden.KonvertiereDatumFuerDB(auftrag.Abfassungsdatum));
			strQuery.Append("', ");
			strQuery.Append("\"Uebermittlungsdatum\"='");
			strQuery.Append(CMethoden.KonvertiereDatumFuerDB(auftrag.Uebermittlungsdatum));
			strQuery.Append("', ");
			strQuery.Append("\"Absender\"='");
			strQuery.Append(CMethoden.KonvertiereStringFuerDB(auftrag.Absender));
			strQuery.Append("', ");
			strQuery.Append("\"Uebermittlungsart\"='");
			strQuery.Append((int)auftrag.Uebermittlungsart);
			strQuery.Append("',");
			strQuery.Append("\"IstUebermittelt\"='");
			strQuery.Append(auftrag.IstUebermittelt);
			strQuery.Append("',");
			strQuery.Append("\"BearbeiterID\"='");
			strQuery.Append(auftrag.BearbeiterBenutzerID);
			strQuery.Append("',");
			strQuery.Append("\"LaufendeNummer\"='");
			strQuery.Append(auftrag.LaufendeNummer);
			strQuery.Append("',");
			strQuery.Append("\"Ausfuehrungszeitpunkt\"='");
			strQuery.Append(CMethoden.KonvertiereDatumFuerDB(auftrag.Ausfuehrungszeitpunkt));
			strQuery.Append("',");
			strQuery.Append("\"SpaetesterEZP\"='");
			strQuery.Append(CMethoden.KonvertiereDatumFuerDB(auftrag.SpaetesterErfuellungszeitpunkt));
			strQuery.Append("',");
			strQuery.Append("\"IstBefehl\"='");
			strQuery.Append(auftrag.IstBefehl);
			strQuery.Append("',");
			strQuery.Append("\"WirdNachverfolgt\"='");
			strQuery.Append( auftrag.WirdNachverfolgt);
			strQuery.Append("', ");
			strQuery.Append( "\"EmpfaengerBenutzerID\"='" );
			strQuery.Append( Convert.ToInt32(auftrag.EmpfaengerBenutzerID));
			strQuery.Append( "', ");
			strQuery.Append( "\"IstInToDoListe\"='" );
			strQuery.Append( auftrag.IstInToDoListe);
			strQuery.Append( "'");
			if (pin_ob is Cdv_Erkundungsbefehl) 
			{
				strQuery.Append(", ");
				strQuery.Append("\"IstErkundungsbefehl\"='");
				strQuery.Append(true);
				strQuery.Append("',");
				strQuery.Append("\"EB_Befehlsart\"='");
				strQuery.Append((int)erkundungsbefehl.BefehlsArt);
				strQuery.Append("'");
				strQuery.Append("");
			}
			strQuery.Append(" where \"ID\"=");
			strQuery.Append(auftrag.ID);
			
			
			if (auftrag.EmpfaengerMengeKraftID != null)
			{	
				//erstmal löschen aller Einträge zu  diesem Auftrag/Erkundungsbefehl
				string str_delete = "delete from \"Empfaenger_Auftrag\" where \"AuftragsID\"="+auftrag.ID;
				db.AusfuehrenDeleteAnfrage(str_delete);

				//nun reinschreiben aller neuen Informationen
				for (Int32 i_tmp1=0; i_tmp1<auftrag.EmpfaengerMengeKraftID.Length; i_tmp1++)
				{
					str_INSERTAnfrageEmpfaenger = new StringBuilder("insert into \"Empfaenger_Auftrag\"", 100); 
					str_INSERTAnfrageEmpfaenger.Append("(");
					str_INSERTAnfrageEmpfaenger.Append("\"AuftragsID\",");
					str_INSERTAnfrageEmpfaenger.Append("\"KraftID\"");
					str_INSERTAnfrageEmpfaenger.Append(") values (");
					str_INSERTAnfrageEmpfaenger.Append("'");
					str_INSERTAnfrageEmpfaenger.Append( auftrag.ID );
					str_INSERTAnfrageEmpfaenger.Append("'");
					str_INSERTAnfrageEmpfaenger.Append("," );
					str_INSERTAnfrageEmpfaenger.Append("'");
					str_INSERTAnfrageEmpfaenger.Append( auftrag.EmpfaengerMengeKraftID[i_tmp1] );
					str_INSERTAnfrageEmpfaenger.Append("')");
					db.AusfuehrenInsertAnfrage(str_INSERTAnfrageEmpfaenger.ToString());
				}
			}
			//Abschließen des UpdateAbfrageString
			strQuery.Append( ";");
			return db.AusfuehrenUpdateAnfrage(strQuery.ToString());
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
