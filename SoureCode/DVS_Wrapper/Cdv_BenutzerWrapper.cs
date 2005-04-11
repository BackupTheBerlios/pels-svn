using System;
using pELS.DV.Server.Interfaces;
using pELS.Server;
using pELS.DV;
using Npgsql;
using System.Data;
using pELS.Tools.Server;

namespace pELS.DV.Server.Wrapper
{
	/// <summary>
	/// Implementation & Test: alexG 28.02.05
	/// </summary>
	public class Cdv_BenutzerWrapper: Cdv_WrapperBase
	{

		#region Variablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonCdv_WrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion

		#region Konstruktor
		private Cdv_BenutzerWrapper()
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
				_obj_instanzVonCdv_WrapperBase = new Cdv_BenutzerWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_WrapperBase;				 
		}	
		#endregion

		#region Virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Benutzer))
				throw new ArgumentNullException("Falsches Objekt an Cdv_BenutzerWrapper übergeben. Cdv_Benutzer wurde erwartet! Methode:Cdv_BenutzerWrapper.NeuerEintrag");
			String str_INSERTAnfrage = "insert into \"Benutzer\"("
				+ "\"Benutzername\", "
				+ "\"Systemrolle\") values("
				+ "'" + CMethoden.KonvertiereStringFuerDB(((Cdv_Benutzer) pin_ob).Benutzername) + "'" +", "
				+ "'" + Convert.ToInt32(((Cdv_Benutzer) pin_ob).Systemrolle) + "'" + ")";

			// Anfrage an Cdv_DB übermitteln
			return(db.AusfuehrenInsertAnfrage(str_INSERTAnfrage));	
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Benutzer))
				throw new ArgumentNullException("Falsches Objekt an Cdv_BenutzerWrapper übergeben. Cdv_Benutzer wurde erwartet! Methode:Cdv_BenutzerWrapper.NeuerEintrag");
			String str_UPDATEAnfrage = "update \"Benutzer\" set "
				+ "\"Benutzername\"= " + "'" + CMethoden.KonvertiereStringFuerDB(((Cdv_Benutzer) pin_ob).Benutzername) + "'" + ", "
				+ "\"Systemrolle\"= " + "'" + Convert.ToInt32(((Cdv_Benutzer) pin_ob).Systemrolle) + "' "
				+ "where \"ID\"=" + ((Cdv_Benutzer)pin_ob).ID.ToString();

			// Anfrage an Cdv_DB weiterleiten
			return(db.AusfuehrenUpdateAnfrage(str_UPDATEAnfrage));
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_Benutzer;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage			
			String str_SELECTAnfrage = "select * from \"Benutzer\"";
			dreader_Benutzer = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_Benutzer[] Benutzer_anfrageergebnisse = new Cdv_Benutzer[i_anzahlZeilen];
			int i = 0;

			while(dreader_Benutzer.Read())
			{
				Benutzer_anfrageergebnisse[i] = new Cdv_Benutzer();
				Benutzer_anfrageergebnisse[i].ID = dreader_Benutzer.GetInt32(dreader_Benutzer.GetOrdinal("ID"));
				Benutzer_anfrageergebnisse[i].Benutzername = CMethoden.KonvertiereStringAusDB(dreader_Benutzer.GetString(dreader_Benutzer.GetOrdinal("Benutzername")));
				Benutzer_anfrageergebnisse[i].Systemrolle = (Tdv_Systemrolle) dreader_Benutzer.GetInt32(dreader_Benutzer.GetOrdinal("Systemrolle"));
				i++;
			}

			return(Benutzer_anfrageergebnisse);
		}
		
		
		
		
		
			#endregion
		}

}
