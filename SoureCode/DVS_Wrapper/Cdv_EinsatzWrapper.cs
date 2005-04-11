using System;
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
			String str_INSERTAnfrage = "insert into \"Einsaetze\"("
				+ "\"Bezeichnung\", "
				+ "\"Einsatzort\", "
				+ "\"EinsatzVon\", "
				+ "\"EinsatzBis\", "
				+ "\"ArtDerHilfeleistung\", "
				+ "\"Einsatzbericht\", "
				+ "\"Kostenabrechnung\", "
				+ "\"Erfahrungsbericht\", "
				+ "\"Pressemitteilung\", "
				+ "\"Kostenerstattung\", "
				+ "\"Haftungsfreistellung\", "
				+ "\"IhkBescheinigung\", "
				+ "\"Kommentar_Text\", "
				+ "\"Kommentar_Autor\""
				+ ") values("
				+ "'" + CMethoden.KonvertiereStringFuerDB(ceEinsatz.Bezeichnung) + "', "
				+ "'" + CMethoden.KonvertiereStringFuerDB(ceEinsatz.Einsatzort) + "', "
				+ "'" + CMethoden.KonvertiereDatumFuerDB(ceEinsatz.VonDatum) + "', "
				+ "'" + CMethoden.KonvertiereDatumFuerDB(ceEinsatz.BisDatum) + "',"
				+ "'" + CMethoden.KonvertiereStringFuerDB(ceEinsatz.ArtDerHilfeleistung) + "', "
				+ "'" + ceEinsatz.EinsatzberichtGefertigt + "', "
				+ "'" + ceEinsatz.KostenabrechnungGefertigt + "', "
				+ "'" + ceEinsatz.ErfahrungsberichtGeschrieben + "', "
				+ "'" + ceEinsatz.PressemitteilungGeschrieben + "', "
				+ "'" + ceEinsatz.KostenerstattungKontrolliert + "', "
				+ "'" + ceEinsatz.HaftungsfreistellungVorhanden + "', "
				+ "'" + ceEinsatz.IhkBescheinigungVorhanden + "', "
				+ "'" + ceEinsatz.Beschreibung.Text + "', "
				+ "'" + ceEinsatz.Beschreibung.Autor + "')";

			 return db.AusfuehrenInsertAnfrage(str_INSERTAnfrage);
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{				
			if(!(pin_ob is Cdv_Einsatz))
				throw new ArgumentNullException("Falsches Objekt an Cdv_EinsatzWrapper übergeben. Cdv_Einsatz wurde erwartet! Methode:Cdv_EinsatzWrapper.AktualisiereEintrag");
			// Objekt umcasten nach Cdv_Einsatz
			Cdv_Einsatz ceEinsatz = pin_ob as Cdv_Einsatz;
			// Anfrage
			string myQ = "update \"Einsaetze\" set"
				+ "\"Bezeichnung\"='" + CMethoden.KonvertiereStringFuerDB(ceEinsatz.Bezeichnung) + "', "
				+ "\"Einsatzort\"='" + CMethoden.KonvertiereStringFuerDB(ceEinsatz.Einsatzort) + "', "
				+ "\"EinsatzVon\"='" + CMethoden.KonvertiereDatumFuerDB(ceEinsatz.VonDatum) + "', "
				+ "\"EinsatzBis\"='" + CMethoden.KonvertiereDatumFuerDB(ceEinsatz.BisDatum) + "', "
				+ "\"ArtDerHilfeleistung\"='" + CMethoden.KonvertiereStringFuerDB(ceEinsatz.ArtDerHilfeleistung) + "', "
				+ "\"Kostenabrechnung\"='" + ceEinsatz.KostenabrechnungGefertigt + "', "
				+ "\"Erfahrungsbericht\"='" + ceEinsatz.ErfahrungsberichtGeschrieben + "', "
				+ "\"Pressemitteilung\"='" + ceEinsatz.PressemitteilungGeschrieben + "', "
				+ "\"Kostenerstattung\"='" + ceEinsatz.KostenerstattungKontrolliert + "', "
				+ "\"Einsatzbericht\"='" + ceEinsatz.EinsatzberichtGefertigt + "', "
				+ "\"Haftungsfreistellung\"='" + ceEinsatz.HaftungsfreistellungVorhanden + "', "
				+ "\"Kommentar_Text\"='" + ceEinsatz.Beschreibung.Text + "', "
				+ "\"Kommentar_Autor\"='" + ceEinsatz.Beschreibung.Autor + "', "
				+ "\"IhkBescheinigung\"='" + ceEinsatz.IhkBescheinigungVorhanden + "' "
				+ "where \"ID\"=" + ceEinsatz.ID;

			return db.AusfuehrenUpdateAnfrage(myQ);
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
