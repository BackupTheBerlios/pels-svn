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
	using pELS.Server;
	using pELS.DV;
	/// <summary>
	/// Implementiert & getestet by Michal & alexG 24.02.05 14h
	/// </summary>
	public class Cdv_EinsatzWrapper: Cdv_WrapperBase
	{
		#region Klassenvariablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonWrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion

		#region Konstruktor
		private Cdv_EinsatzWrapper()
		{
			this.db = Cdv_DB.HoleInstanz();
			_i_anzahlReferenzen = 0;
		}
		#endregion

		#region statische Methoden
		public static Cdv_WrapperBase HoleInstanz()
		{
			// Instanz erstellen, wenn noch nicht vorhanden
			if (_obj_instanzVonWrapperBase == null)
				_obj_instanzVonWrapperBase = new Cdv_EinsatzWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonWrapperBase;
		}
		#endregion

		#region virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{	
			if(!(pin_ob is Cdv_Einsatz))
				throw new ArgumentNullException("Falsches Objekt an Cdv_EinsatzWrapper übergeben. Cdv_Einsatz wurde erwartet! Methode:Cdv_EinsatzWrapper.NeuerEintrag");
			// Objekt umcasten nach Cdv_Einsatz
			Cdv_Einsatz ceEinsatz = pin_ob as Cdv_Einsatz;
			// Insertanfrage
			StringBuilder strQuery = new StringBuilder("insert into \"Einsaetze\"(", 300);
			strQuery.Append( "\"Bezeichnung\", \"Einsatzort\", \"EinsatzVon\", \"EinsatzBis\", \"ArtDerHilfeleistung\", \"Einsatzbericht\", \"Kostenabrechnung\", \"Erfahrungsbericht\", \"Pressemitteilung\", \"Kostenerstattung\", \"Haftungsfreistellung\", \"IhkBescheinigung\", \"Kommentar_Text\", \"Kommentar_Autor\") values('" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(ceEinsatz.Bezeichnung) );
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(ceEinsatz.Einsatzort) );
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereDatumFuerDB(ceEinsatz.VonDatum) );
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereDatumFuerDB(ceEinsatz.BisDatum) );
			strQuery.Append( "','" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(ceEinsatz.ArtDerHilfeleistung) );
			strQuery.Append( "', '" );
			strQuery.Append( ceEinsatz.EinsatzberichtGefertigt );
			strQuery.Append( "', '" );
			strQuery.Append( ceEinsatz.KostenabrechnungGefertigt );
			strQuery.Append( "', '" );
			strQuery.Append( ceEinsatz.ErfahrungsberichtGeschrieben );
			strQuery.Append( "', '" );
			strQuery.Append( ceEinsatz.PressemitteilungGeschrieben );
			strQuery.Append( "', '" );
			strQuery.Append( ceEinsatz.KostenerstattungKontrolliert );
			strQuery.Append( "', '" );
			strQuery.Append( ceEinsatz.HaftungsfreistellungVorhanden );
			strQuery.Append( "', '" );
			strQuery.Append( ceEinsatz.IhkBescheinigungVorhanden );
			strQuery.Append( "', '" );
			strQuery.Append( ceEinsatz.Beschreibung.Text );
			strQuery.Append( "', '" );
			strQuery.Append( ceEinsatz.Beschreibung.Autor );
			strQuery.Append( "')");
			return db.AusfuehrenInsertAnfrage(strQuery.ToString());
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{				
			if(!(pin_ob is Cdv_Einsatz))
				throw new ArgumentNullException("Falsches Objekt an Cdv_EinsatzWrapper übergeben. Cdv_Einsatz wurde erwartet! Methode:Cdv_EinsatzWrapper.AktualisiereEintrag");
			// Objekt umcasten nach Cdv_Einsatz
			Cdv_Einsatz ceEinsatz = pin_ob as Cdv_Einsatz;
			// Anfrage
			StringBuilder strQuery = new StringBuilder("update \"Einsaetze\" set", 300);
			strQuery.Append( "\"Bezeichnung\"='" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(ceEinsatz.Bezeichnung) );
			strQuery.Append( "', \"Einsatzort\"='" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(ceEinsatz.Einsatzort) );
			strQuery.Append( "', \"EinsatzVon\"='" );
			strQuery.Append( CMethoden.KonvertiereDatumFuerDB(ceEinsatz.VonDatum) );
			strQuery.Append( "', \"EinsatzBis\"='" );
			strQuery.Append( CMethoden.KonvertiereDatumFuerDB(ceEinsatz.BisDatum) );
			strQuery.Append( "', \"ArtDerHilfeleistung\"='" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(ceEinsatz.ArtDerHilfeleistung) );
			strQuery.Append( "', \"Kostenabrechnung\"='" );
			strQuery.Append( ceEinsatz.KostenabrechnungGefertigt );
			strQuery.Append( "', \"Erfahrungsbericht\"='" );
			strQuery.Append( ceEinsatz.ErfahrungsberichtGeschrieben );
			strQuery.Append( "', \"Pressemitteilung\"='" );
			strQuery.Append( ceEinsatz.PressemitteilungGeschrieben );
			strQuery.Append( "', \"Kostenerstattung\"='" );
			strQuery.Append( ceEinsatz.KostenerstattungKontrolliert );
			strQuery.Append( "', \"Einsatzbericht\"='" );
			strQuery.Append( ceEinsatz.EinsatzberichtGefertigt );
			strQuery.Append( "', \"Haftungsfreistellung\"='" );
			strQuery.Append( ceEinsatz.HaftungsfreistellungVorhanden );
			strQuery.Append( "', \"Kommentar_Text\"='" );
			strQuery.Append( ceEinsatz.Beschreibung.Text );
			strQuery.Append( "', \"Kommentar_Autor\"='" );
			strQuery.Append( ceEinsatz.Beschreibung.Autor );
			strQuery.Append( "', \"IhkBescheinigung\"='" );
			strQuery.Append( ceEinsatz.IhkBescheinigungVorhanden );
			strQuery.Append( "' where \"ID\"=" );
			strQuery.Append( ceEinsatz.ID);
			return db.AusfuehrenUpdateAnfrage(strQuery.ToString());
		}

		
		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_einsatz_anfrageergebnisse;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage
			String str_SELECTAnfrage = "Select * from \"Einsaetze\"";
			// Zugriff auf DB
			dreader_einsatz_anfrageergebnisse = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_Einsatz[] einsatz_anfrageergebnisse = new Cdv_Einsatz[i_anzahlZeilen];
			int i = 0;

			while(dreader_einsatz_anfrageergebnisse.Read())
			{
				einsatz_anfrageergebnisse[i] = new Cdv_Einsatz();
				einsatz_anfrageergebnisse[i].ID = dreader_einsatz_anfrageergebnisse.GetInt32(dreader_einsatz_anfrageergebnisse.GetOrdinal("ID"));
				einsatz_anfrageergebnisse[i].Bezeichnung = CMethoden.KonvertiereStringAusDB(dreader_einsatz_anfrageergebnisse.GetString(dreader_einsatz_anfrageergebnisse.GetOrdinal("Bezeichnung")));
				einsatz_anfrageergebnisse[i].Einsatzort = CMethoden.KonvertiereStringAusDB(dreader_einsatz_anfrageergebnisse.GetString(dreader_einsatz_anfrageergebnisse.GetOrdinal("Einsatzort"))); 
				einsatz_anfrageergebnisse[i].VonDatum = dreader_einsatz_anfrageergebnisse.GetDateTime(dreader_einsatz_anfrageergebnisse.GetOrdinal("EinsatzVon"));									
				einsatz_anfrageergebnisse[i].BisDatum =	dreader_einsatz_anfrageergebnisse.GetDateTime(dreader_einsatz_anfrageergebnisse.GetOrdinal("EinsatzBis"));
				einsatz_anfrageergebnisse[i].ArtDerHilfeleistung = CMethoden.KonvertiereStringAusDB(dreader_einsatz_anfrageergebnisse.GetString(dreader_einsatz_anfrageergebnisse.GetOrdinal("ArtDerHilfeleistung"))); 
				einsatz_anfrageergebnisse[i].EinsatzberichtGefertigt = dreader_einsatz_anfrageergebnisse.GetBoolean(dreader_einsatz_anfrageergebnisse.GetOrdinal("Einsatzbericht"));
				einsatz_anfrageergebnisse[i].KostenabrechnungGefertigt = dreader_einsatz_anfrageergebnisse.GetBoolean(dreader_einsatz_anfrageergebnisse.GetOrdinal("Kostenabrechnung"));
				einsatz_anfrageergebnisse[i].ErfahrungsberichtGeschrieben = dreader_einsatz_anfrageergebnisse.GetBoolean(dreader_einsatz_anfrageergebnisse.GetOrdinal("Erfahrungsbericht"));
				einsatz_anfrageergebnisse[i].PressemitteilungGeschrieben = dreader_einsatz_anfrageergebnisse.GetBoolean(dreader_einsatz_anfrageergebnisse.GetOrdinal("Pressemitteilung"));
				einsatz_anfrageergebnisse[i].KostenerstattungKontrolliert = dreader_einsatz_anfrageergebnisse.GetBoolean(dreader_einsatz_anfrageergebnisse.GetOrdinal("Kostenerstattung"));
				einsatz_anfrageergebnisse[i].HaftungsfreistellungVorhanden = dreader_einsatz_anfrageergebnisse.GetBoolean(dreader_einsatz_anfrageergebnisse.GetOrdinal("Haftungsfreistellung"));
				einsatz_anfrageergebnisse[i].Beschreibung.Text = CMethoden.KonvertiereStringAusDB(dreader_einsatz_anfrageergebnisse.GetString(dreader_einsatz_anfrageergebnisse.GetOrdinal("Kommentar_Text")));
				einsatz_anfrageergebnisse[i].Beschreibung.Autor = CMethoden.KonvertiereStringAusDB(dreader_einsatz_anfrageergebnisse.GetString(dreader_einsatz_anfrageergebnisse.GetOrdinal("Kommentar_Autor")));
				einsatz_anfrageergebnisse[i].IhkBescheinigungVorhanden = dreader_einsatz_anfrageergebnisse.GetBoolean(dreader_einsatz_anfrageergebnisse.GetOrdinal("IhkBescheinigung"));
				i++;
			}
			return einsatz_anfrageergebnisse;			
		}
		#endregion
		



	}
}
