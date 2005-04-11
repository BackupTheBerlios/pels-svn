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
	/// Implementation: 01.03.05 alexG
	/// Debug & Tests: 02.03.05 Hütte
	/// </summary>
	public class Cdv_KFZWrapper: Cdv_WrapperBase
	{
		#region Klassenvariablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonWrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion

		#region Konstruktor
		private Cdv_KFZWrapper()
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
				_obj_instanzVonWrapperBase = new Cdv_KFZWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonWrapperBase;
		}
		#endregion

		#region virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_KFZ))
				throw new ArgumentNullException("Falsches Objekt an Cdv_KFZWrapper übergeben. Cdv_KFZ wurde erwartet! Methode:Cdv_KFZWrapper.NeuerEintrag");
			// Objekt umcasten nach Cdv_Einsatz
			Cdv_KFZ myKFZ = pin_ob as Cdv_KFZ;
			// Insertanfrage
			String str_INSERTAnfrage = "insert into \"Kfz\"("
				+ "\"Kennzeichen\", "
				+ "\"Funkrufname\", "
				+ "\"Typ\", "
				+ "\"FahrerID\", "
				+ "\"Einsatzbetriebsstunden\", "
				+ "\"Einsatzkm\", "
				//Ab hier werden von Cdv_Kraft eingeerbte Eigenschaften abgebildet
				+ "\"Kraeftestatus\", "
				+ "\"EinsatzschwerpunktID\", "
				+ "\"Kommentar_Autor\", "
				+ "\"Kommentar_Text\", "
				+ "\"ModulID\") values("
				//Inhalte
				+ "'" + CMethoden.KonvertiereStringFuerDB(myKFZ.Kennzeichen) + "', "
				+ "'" + CMethoden.KonvertiereStringFuerDB(myKFZ.Funkrufname) + "', "
				+ "'" + CMethoden.KonvertiereStringFuerDB(myKFZ.KfzTyp) + "', "
				+ "'" + myKFZ.FahrerHelferID+ "', "
				+ "'" + CMethoden.KonvertiereRealFuerDB(myKFZ.Einsatzbetriebsstunden) + "', "
				+ "'" + CMethoden.KonvertiereRealFuerDB(myKFZ.EinsatzKm) + "', "
				//Ab hier werden von Cdv_Kraft eingeerbte Inhalte abgebildet
				+ "'" + (int) myKFZ.Kraeftestatus + "', "
				+ "'" + myKFZ.EinsatzschwerpunktID+ "', "
				+ "'" + CMethoden.KonvertiereStringFuerDB(myKFZ.Kommentar.Autor) + "', "
				+ "'" + CMethoden.KonvertiereStringFuerDB(myKFZ.Kommentar.Text) + "', "
				+ "'" + myKFZ.ModulID + "')";
			
			return db.AusfuehrenInsertAnfrage(str_INSERTAnfrage);
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_KFZ))
				throw new ArgumentNullException("Falsches Objekt an Cdv_KFZWrapper übergeben. Cdv_KFZ wurde erwartet! Methode:Cdv_KFZWrapper.AktualisiereEintrag");
			// Objekt umcasten nach Cdv_KFZ
			Cdv_KFZ myKfz = pin_ob as Cdv_KFZ;
			// Anfrage
			string myQ = "update \"Kfz\" set"
				+ "\"Kennzeichen\"='" + CMethoden.KonvertiereStringFuerDB(myKfz.Kennzeichen) + "', "
				+ "\"Funkrufname\"='" + CMethoden.KonvertiereStringFuerDB(myKfz.Funkrufname) + "', "
				+ "\"Typ\"='" + CMethoden.KonvertiereStringFuerDB(myKfz.KfzTyp) + "', "
				+ "\"FahrerID\"='" + myKfz.FahrerHelferID + "', "
				+ "\"Einsatzbetriebsstunden\"='" + CMethoden.KonvertiereRealFuerDB(myKfz.Einsatzbetriebsstunden) + "', "
				+ "\"Einsatzkm\"='" + CMethoden.KonvertiereRealFuerDB(myKfz.EinsatzKm) + "', "
				//Ab hier werden von Cdv_Kraft eingeerbte Eigenschaften abgebildet
				+ "\"Kraeftestatus\"='" + (int) myKfz.Kraeftestatus + "', "
				+ "\"EinsatzschwerpunktID\"='" +myKfz.EinsatzschwerpunktID + "', "
				+ "\"Kommentar_Autor\"='" + CMethoden.KonvertiereStringFuerDB(myKfz.Kommentar.Autor) + "', "
				+ "\"Kommentar_Text\"='" + CMethoden.KonvertiereStringFuerDB(myKfz.Kommentar.Text) + "', "
				+ "\"ModulID\"='" + myKfz.ModulID + "' "
				+ "where \"ID\"=" + myKfz.ID;

			return db.AusfuehrenUpdateAnfrage(myQ);
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_kfz_anfrageergebnisse;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage
			String str_SELECTAnfrage = "Select * from \"Kfz\"";
			// Zugriff auf DB
			dreader_kfz_anfrageergebnisse = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_KFZ[] kfz_anfrageergebnisse = new Cdv_KFZ[i_anzahlZeilen];
			int i = 0;

			while(dreader_kfz_anfrageergebnisse.Read())
			{
				//Zuerst werden die aus Kraft eingeerbten Attribute gemappt
				kfz_anfrageergebnisse[i] = new Cdv_KFZ();
				kfz_anfrageergebnisse[i].ID = dreader_kfz_anfrageergebnisse.GetInt32(dreader_kfz_anfrageergebnisse.GetOrdinal("ID"));
				kfz_anfrageergebnisse[i].Kraeftestatus = (Tdv_Kraeftestatus) dreader_kfz_anfrageergebnisse.GetInt32(dreader_kfz_anfrageergebnisse.GetOrdinal("Kraeftestatus"));
				kfz_anfrageergebnisse[i].EinsatzschwerpunktID = dreader_kfz_anfrageergebnisse.GetInt32(dreader_kfz_anfrageergebnisse.GetOrdinal("EinsatzschwerpunktID"));
				kfz_anfrageergebnisse[i].ModulID = dreader_kfz_anfrageergebnisse.GetInt32(dreader_kfz_anfrageergebnisse.GetOrdinal("ModulID"));
				kfz_anfrageergebnisse[i].Kommentar.Autor = CMethoden.KonvertiereStringAusDB(dreader_kfz_anfrageergebnisse.GetString(dreader_kfz_anfrageergebnisse.GetOrdinal("Kommentar_Autor")));
				kfz_anfrageergebnisse[i].Kommentar.Text = CMethoden.KonvertiereStringAusDB(dreader_kfz_anfrageergebnisse.GetString(dreader_kfz_anfrageergebnisse.GetOrdinal("Kommentar_Text")));
				//jetzt werden die KFZ spezifischen Attribute gemappt
				kfz_anfrageergebnisse[i].Kennzeichen = CMethoden.KonvertiereStringAusDB(dreader_kfz_anfrageergebnisse.GetString(dreader_kfz_anfrageergebnisse.GetOrdinal("Kennzeichen")));
				kfz_anfrageergebnisse[i].Funkrufname = CMethoden.KonvertiereStringAusDB(dreader_kfz_anfrageergebnisse.GetString(dreader_kfz_anfrageergebnisse.GetOrdinal("Funkrufname")));
				kfz_anfrageergebnisse[i].KfzTyp = CMethoden.KonvertiereStringAusDB(dreader_kfz_anfrageergebnisse.GetString(dreader_kfz_anfrageergebnisse.GetOrdinal("Typ")));
				kfz_anfrageergebnisse[i].FahrerHelferID = dreader_kfz_anfrageergebnisse.GetInt32(dreader_kfz_anfrageergebnisse.GetOrdinal("FahrerID"));
				kfz_anfrageergebnisse[i].Einsatzbetriebsstunden = dreader_kfz_anfrageergebnisse.GetFloat(dreader_kfz_anfrageergebnisse.GetOrdinal("Einsatzbetriebsstunden"));
				kfz_anfrageergebnisse[i].EinsatzKm = dreader_kfz_anfrageergebnisse.GetFloat(dreader_kfz_anfrageergebnisse.GetOrdinal("Einsatzkm"));
				i++;
			}
			return kfz_anfrageergebnisse;
		}
		#endregion
	}

}
