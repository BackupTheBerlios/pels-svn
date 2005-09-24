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
			StringBuilder strQuery = new StringBuilder("insert into \"Kfz\"(", 300);
			strQuery.Append( "\"Kennzeichen\", \"Funkrufname\", \"Typ\", \"FahrerID\", \"Einsatzbetriebsstunden\", \"Einsatzkm\", \"Kraeftestatus\", \"EinsatzschwerpunktID\", \"Kommentar_Autor\", \"Kommentar_Text\", \"ModulID\") values('" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myKFZ.Kennzeichen) );
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myKFZ.Funkrufname) );
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myKFZ.KfzTyp) );
			strQuery.Append( "', '" );
			strQuery.Append( myKFZ.FahrerHelferID);
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereRealFuerDB(myKFZ.Einsatzbetriebsstunden) );
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereRealFuerDB(myKFZ.EinsatzKm) );
			strQuery.Append( "', '" );
			strQuery.Append( (int) myKFZ.Kraeftestatus );
			strQuery.Append( "', '" );
			strQuery.Append( myKFZ.EinsatzschwerpunktID);
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myKFZ.Kommentar.Autor) );
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myKFZ.Kommentar.Text) );
			strQuery.Append( "', '" );
			strQuery.Append( myKFZ.ModulID );
			strQuery.Append( "')");
			return db.AusfuehrenInsertAnfrage(strQuery.ToString());
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_KFZ))
				throw new ArgumentNullException("Falsches Objekt an Cdv_KFZWrapper übergeben. Cdv_KFZ wurde erwartet! Methode:Cdv_KFZWrapper.AktualisiereEintrag");
			// Objekt umcasten nach Cdv_KFZ
			Cdv_KFZ myKfz = pin_ob as Cdv_KFZ;
			// Anfrage
			StringBuilder strQuery = new StringBuilder("update \"Kfz\" set", 300);
				strQuery.Append( "\"Kennzeichen\"='" );
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(myKfz.Kennzeichen) );
					strQuery.Append( "', \"Funkrufname\"='" );
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(myKfz.Funkrufname) );
					strQuery.Append( "', \"Typ\"='" );
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(myKfz.KfzTyp) );
					strQuery.Append( "', \"FahrerID\"='" );
					strQuery.Append( myKfz.FahrerHelferID );
					strQuery.Append( "', \"Einsatzbetriebsstunden\"='" );
					strQuery.Append( CMethoden.KonvertiereRealFuerDB(myKfz.Einsatzbetriebsstunden) );
					strQuery.Append( "', \"Einsatzkm\"='" );
					strQuery.Append( CMethoden.KonvertiereRealFuerDB(myKfz.EinsatzKm) );
					strQuery.Append( "', \"Kraeftestatus\"='" );
					strQuery.Append( (int) myKfz.Kraeftestatus );
					strQuery.Append( "', \"EinsatzschwerpunktID\"='" );
					strQuery.Append(myKfz.EinsatzschwerpunktID );
					strQuery.Append( "', \"Kommentar_Autor\"='" );
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(myKfz.Kommentar.Autor) );
					strQuery.Append( "', \"Kommentar_Text\"='" );
					strQuery.Append( CMethoden.KonvertiereStringFuerDB(myKfz.Kommentar.Text) );
					strQuery.Append( "', \"ModulID\"='" );
					strQuery.Append( myKfz.ModulID );
					strQuery.Append( "' where \"ID\"="); 
					strQuery.Append( myKfz.ID);

			return db.AusfuehrenUpdateAnfrage(strQuery.ToString());
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
