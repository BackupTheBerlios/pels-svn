using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using Npgsql;


namespace pELS.Server
{
	/// <summary>
	/// Diese Klasse realisiert den Zugriff auf die Datenbank.
	/// Der Serverkontroller instanziiert Cdv_DB und setzt die 
	/// Verbindungsparameter.
	/// </summary>
	public class Cdv_DB
	{
		
		#region Variablen	
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_DB _obj_instanzVonCdv_DB;
		private static int _i_anzahlReferenzen;		
		
		// Variablen für die Anfragebearbeitung		
		private NpgsqlConnection _con_dBVerbindung = new NpgsqlConnection();
		private NpgsqlCommand _com_dBCommand = new NpgsqlCommand();
		#endregion

		#region Instanz- und Verbindungsmanagement
		/// <summary>
		/// Privater Konstuktor, um Singleton-Pattern zu realisieren
		/// </summary>
		private Cdv_DB()
		{
			_i_anzahlReferenzen = 0;			
		}

		/// <summary>
		/// Pseudo-Konstruktor: Wenn noch keine Instanz existiert wird eine
		/// erstellt, sonst nicht.
		/// </summary>
		/// <returns>Die Referenz auf die Instanz einer Klasse zum DB-Zugriff</returns>
		public static Cdv_DB HoleInstanz()
		{
			// Instanz erstellen, wenn noch nicht vorhanden
			if (_obj_instanzVonCdv_DB == null)
				_obj_instanzVonCdv_DB = new Cdv_DB();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_DB;				 
		}		
		
		/// <summary>
		/// Trennt die evtl. bestehende Datenbankverbindung.
		/// Erstellt den Connectionstring und öffnet die Verbindung.
		/// zur Datenbank. 
		/// Bei Änderungen an der DB-Verbindung muss ebenfallsdiese 
		/// Methode aufgerufen werden. 
		/// </summary>
		/// <param>Anfrage</param>
		/// <returns>Gibt FALSE zurück, wenn Verbindung nicht aufgebaut werden konnte</returns>
		public bool VerbindeMitDB( String pin_str_userID,
			String pin_str_password,
			String pin_str_host,
			String pin_str_port,
			String pin_str_database,
			String pin_str_lifetime		)
		{			
			// Wenn Verbindung nicht geschlossen (!0), dann schließen
			if (_con_dBVerbindung.State != 0) SchliesseDBVerbindung();
			// ConnectionString erstellen
			String str_connectionString =	"User ID="	+ pin_str_userID	+ "; " + 
				"Password=" + pin_str_password	+ "; " +
				"Host="		+ pin_str_host		+ "; " + 
				"Port="		+ pin_str_port		+ "; " + 
				"Database=" + pin_str_database	+ "; " + 
				"Lifetime=" + pin_str_lifetime	+ "; " + 
				"encoding=UNICODE";

			try
			{
				// ConnectionString der Connection zuweisen, Connection öffnen
				_con_dBVerbindung.ConnectionString = str_connectionString;
				_con_dBVerbindung.Open();
				return true;
			}
			catch (System.Exception e)  
			{
				// Wenn Öffnen schief geht, false zurückgeben
				Console.WriteLine(e);
				return false;
			}
		}


		/// <summary>
		/// Macht eine zusätzliche Verbindung zur Datenbank auf 
		/// (unabhängig von bestehenden Verbindungen und 
		/// fängt differenziert Fehler ab und gibt diese als 
		/// String zurück		
		/// </summary>
		/// <param name="pin_str_userID"></param>
		/// <param name="pin_str_password"></param>
		/// <param name="pin_str_host"></param>
		/// <param name="pin_str_port"></param>
		/// <param name="pin_str_database"></param>
		/// <param name="pin_str_lifetime"></param>
		public string testeDB(	  String pin_str_userID,
			String pin_str_password,
			String pin_str_host,
			String pin_str_port,
			String pin_str_database,
			String pin_str_lifetime		)
		{
			//Erstellen des Connection String
			String str_connectionString =	"User ID="	+ pin_str_userID	+ "; " + 
				"Password=" + pin_str_password	+ "; " +
				"Host="		+ pin_str_host		+ "; " + 
				"Port="		+ pin_str_port		+ "; " + 
				"Database=" + pin_str_database	+ "; " + 
				"Lifetime=" + pin_str_lifetime	+ "; " + 
				"encoding=UNICODE";
		
			NpgsqlConnection my_con_dBVerbindung = new NpgsqlConnection();
			try
			{
				my_con_dBVerbindung.ConnectionString = str_connectionString;
				my_con_dBVerbindung.Open();
				my_con_dBVerbindung.Close();
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			
			return "";
		}
		
		
		/// <summary>
		/// Schließt die bestehende Datenbankverbindung.		
		/// </summary>
		/// /// <returns>Gibt FALSE zurück, wenn Verbindung nicht geschlossen werden konnte</returns>
		public bool SchliesseDBVerbindung()
		{
			try
			{
				// wenn Connection nicht geschlossen, dann schließen
				if (_con_dBVerbindung.State != 0) _con_dBVerbindung.Close();
				return true;				
			}
			catch
			{
				// Schließen fehlgeschlagen -> false zurückgeben
				return false;
			}
		}
		#endregion
		
		#region Anfragenmangement

		/// <summary>
		/// Diese Methode soll DDL (Data Definition Language) Anfragen auf der Datenbank aufführen
		/// </summary>
		/// <param name="pin_str_Anfrage">
		///	Enthält die komplette Anfrage. Zudem muss der Nutzer der Methode Exceptions abfangen,
		///	die z.b. bei Zugriffsverletzungen auftreten können
		///	Die	DDL Anfrage muss konform zur Syntax von PostgreSQL 8.0.1 sein.
		/// </param>
		public void AusfuehrenDDLAnfrage(String pin_str_Anfrage)
		{
			// Transaktion beginnen			
			if (_con_dBVerbindung.State != ConnectionState.Open)
				throw new Exception("Es besteht keine Verbindung zum Server");						
			else
			{
				_com_dBCommand = _con_dBVerbindung.CreateCommand();
				_com_dBCommand.CommandText = pin_str_Anfrage;
					_com_dBCommand.ExecuteNonQuery();
			}																							
		}

		
		/// <summary>
		/// Übernimmt INSERT-Anfragen an die Datenbank, führt sie aus und gibt die ID
		/// des eingefügten Tupels aus der Tabelle zurück.
		/// </summary>
		/// <param name="pin_str_Anfrage">
		///		SQL-INSERT-Anfrage, die bearbeitet werden soll. 
		///		Anfrage darf nur auf genau einer Tabelle arbeiten und muss
		///		konform zur Syntax von PostgreSQL 8.0.1 sein.
		/// </param>
		/// <returns>
		///		Die ID des mit pin_str_Anfrage eingefügten Tupels wird zurückgegeben.
		///		Beim Auftreten eines Fehlers wird Exception geworfen zurückgegeben.
		/// </returns>
		public int AusfuehrenInsertAnfrage(String pin_str_Anfrage)
		{
			int pout_i_lastID;
			object obj_lastID;
			String str_selectLastIDAnfrage = "select currval('public.\"Pelsindex_ID_seq\"')";
			// Transaktion beginnen
			NpgsqlTransaction trans_transaktion;
			if (_con_dBVerbindung.State == ConnectionState.Open)
				trans_transaktion = _con_dBVerbindung.BeginTransaction();
			else throw new Exception("Datenbankverbindung nicht offen");	
			try
			{				
				// Command mit INSERT-Anfrage, Connection und Transaktion belegen
				_com_dBCommand = new NpgsqlCommand(pin_str_Anfrage, _con_dBVerbindung, trans_transaktion);
				// Command ausführen
				_com_dBCommand.ExecuteNonQuery();
				// Command ist jetzt die Anfrage nach der LastID
				_com_dBCommand.CommandText = str_selectLastIDAnfrage;
				// Command ausführen -> LastID kommt als Objekt zurück
				obj_lastID = _com_dBCommand.ExecuteScalar();
				// Transaktion abschließen
				trans_transaktion.Commit();
				// LastID von Object nach int konvertieren
				pout_i_lastID = Convert.ToInt32(obj_lastID.ToString());
				return pout_i_lastID;
			}
			catch(Exception e)
			{
				// wenn etwas schief geht, Rollback versuchen. Fehlermeldung
				try 
				{
					trans_transaktion.Rollback();
				}
				catch {}	
				throw e;					
			}					
		}
		

		/// <summary>
		/// Übernimmt Delete-Anfragen an die Datenbank, führt sie aus und gibt die ID
		/// des eingefügten Tupels aus der Tabelle zurück.
		/// </summary>
		/// <param name="pin_str_Anfrage">
		///		SQL-Delete-Anfrage, die bearbeitet werden soll. 
		///		Anfrage darf nur auf genau einer Tabelle arbeiten und muss
		///		konform zur Syntax von PostgreSQL 8.0.1 sein.
		/// </param>
		/// <returns>
		///		true bei gelungenem Löschen
		/// </returns>
		public bool AusfuehrenDeleteAnfrage(String pin_str_Anfrage)
		{
			// Transaktion beginnen
			NpgsqlTransaction trans_transaktion;
			if (_con_dBVerbindung.State == ConnectionState.Open)
				trans_transaktion = _con_dBVerbindung.BeginTransaction();
			else throw new Exception("Datenbankverbindung nicht offen");	
			try
			{				
				// Command mit DELETE-Anfrage, Connection und Transaktion belegen
				_com_dBCommand = new NpgsqlCommand(pin_str_Anfrage, _con_dBVerbindung, trans_transaktion);
				// Command ausführen
				_com_dBCommand.ExecuteNonQuery();
				// Transaktion abschließen
				trans_transaktion.Commit();
				return true; 
			}
			catch(Exception e)
			{
				// wenn etwas schief geht, Rollback versuchen. Fehlermeldung
				try 
				{
					trans_transaktion.Rollback();
				}
				catch {}	
				throw e;					
			}					
		}
		

		/// <summary>
		/// Übernimmt UPDATE-Anfragen an die Datenbank, führt sie aus und gibt eine
		/// Erfolgsmeldung zurück.
		/// </summary>
		/// <param name="pin_str_Anfrage">
		///		SQL-UPDATE-Anfrage, die bearbeitet werden soll. 
		///		Anfrage muss konform zur Syntax von PostgreSQL 8.0.1 sein.
		/// </param>
		/// <returns>
		///		true, wenn Anfrage erfolgreich bearbeitet wurde.
		///		Wenn Anfrage nicht erfolgreich bearbeitet wurde, Exception werfen.
		/// </returns>
		public bool AusfuehrenUpdateAnfrage(String pin_str_Anfrage)
		{									
			// Transaktion beginnen 
			NpgsqlTransaction trans_transaktion;
			if (_con_dBVerbindung.State == ConnectionState.Open)
				trans_transaktion = _con_dBVerbindung.BeginTransaction();
			else throw new Exception("Datenbankverbindung nicht offen");
			
			try
			{				
				// Command mit UPDATE-Anfrage, Connection und Transaktion belegen
				_com_dBCommand = new NpgsqlCommand(pin_str_Anfrage, _con_dBVerbindung, trans_transaktion);
				// Command ausführen
				_com_dBCommand.ExecuteNonQuery();
				// Transaktion abschließen
				trans_transaktion.Commit();
				return true;
			}
			catch (Exception e)
			{
				// wenn etwas schief geht, Rollback versuchen. false als Fehlermeldung.
				try 
				{					
					trans_transaktion.Rollback();
				}
				catch {}						
				throw e;
			}						
		}

		/// <summary>
		/// Übernimmt SELECT-Anfragen an die Datenbank, führt sie aus und gibt die 
		/// angefragten Daten zurück.
		/// </summary>
		/// <param name="pin_str_Anfrage">
		///		SQL-SELECT-Anfrage, die bearbeitet werden soll. 
		///		Anfrage muss konform zur Syntax von PostgreSQL 8.0.1 sein.
		///		</param>
		/// <returns>
		///		Die angefragten Daten in einem DataSet
		///	</returns>		
		///	
		public NpgsqlDataReader AusfuehrenSelectAnfrage(String pin_str_Anfrage, out int pout_i_anzahlZeilen)
		{
			// Datareader nimmt die Anfrage ergebnisse auf
			NpgsqlDataReader pout_dreader_anfrageergebnisse;

			pout_i_anzahlZeilen = 0;
			// Transaktion beginnen
			NpgsqlTransaction trans_transaktion;
			if (_con_dBVerbindung.State == ConnectionState.Open)
				trans_transaktion = _con_dBVerbindung.BeginTransaction();
			else throw new Exception("Datenbankverbindung nicht offen");
			try
			{
				// Command mit SELECT-Anfrage, Connection und Transaktion belegen
				_com_dBCommand = new NpgsqlCommand(pin_str_Anfrage, _con_dBVerbindung, trans_transaktion);
				// Command ausführen -> Datareader füllen
				pout_dreader_anfrageergebnisse = _com_dBCommand.ExecuteReader();
				NpgsqlDataReader dreader_copy  = _com_dBCommand.ExecuteReader();
				


				trans_transaktion.Commit();
				//hier eine beschissene Lsg., die eine Kopie des Dadareaders anlegt und durchzählt									
					while(dreader_copy.Read()) 
							{pout_i_anzahlZeilen++;}


				return pout_dreader_anfrageergebnisse;
			}
			catch (Exception e)
			{				
				// Wenn etwas schief geht, Exception werfen				
				trans_transaktion.Commit();
				throw e;				
			}				
		}
		
		/// <summary>
		/// Quecky: Bin mir noch nicht sicher ob ich die Methode brauche...
		/// Diese Methode wird verwendet um Änderungen am Datenbankschema vozunehmen.
		/// Aufgerufen wird sie beim Import kompletten Einsätzen und bei der Initialisierung.
		/// </summary>
		/// <param name="pin_str_Anfrage">
		///		SQL-UPDATE-Anfrage, die bearbeitet werden soll. 
		///		Anfrage muss konform zur Syntax von PostgreSQL 8.0.1 sein.
		/// </param>
		/// <returns>
		///		true, wenn Anfrage erfolgreich bearbeitet wurde.
		///		Wenn Anfrage nicht erfolgreich bearbeitet wurde, Exception werfen.
		/// </returns>
		public bool AusfuehrenAnfrage(String pin_str_Anfrage)
		{									
			
			if (_con_dBVerbindung.State != ConnectionState.Open)
				throw new Exception("Datenbankverbindung nicht offen");
			
			try
			{				
				// Command mit UPDATE-Anfrage, Connection und Transaktion belegen
				_com_dBCommand = new NpgsqlCommand(pin_str_Anfrage, _con_dBVerbindung);
				// Command ausführen
				_com_dBCommand.ExecuteNonQuery();
				return true;
			}
			catch (Exception e)
			{
				throw e;
			}						
		}
		#endregion


		 
	}
}