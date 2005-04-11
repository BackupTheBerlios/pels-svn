using System;
using pELS.DV.Server.Interfaces;
using pELS.Server;
using pELS.DV;
using Npgsql;
using System.Collections;
using System.Data;


namespace pELS.DV.Server.Wrapper
{
	using pELS.Server;
	using pELS.DV;
	/// <summary>
	/// Zusammenfassung für Cdv_OrtsverbandWrapper.
	/// </summary>
	public class Cdv_OrtsverbandWrapper: Wrapper.Cdv_WrapperBase
	{
		#region Variablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonCdv_WrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion

		private Cdv_OrtsverbandWrapper()
		{
			_i_anzahlReferenzen = 0;
			this.db = Cdv_DB.HoleInstanz();
		}
		/// <summary>
		/// Pseudo-Konstruktor: Wenn noch keine Instanz existiert wird eine
		/// erstellt, sonst nicht.
		/// </summary>
		/// <returns>Die Referenz auf die Instanz des Wrapper</returns>
		public static Cdv_WrapperBase HoleInstanz()
		{
			// Instanz erstellen, wenn noch nicht vorhanden
			if (_obj_instanzVonCdv_WrapperBase == null)
				_obj_instanzVonCdv_WrapperBase = new Cdv_OrtsverbandWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_WrapperBase;				 
		}	
			
		public override int NeuerEintrag(pELS.DV.Server.Interfaces.IPelsObject pin_ob)
		{		
			if(!(pin_ob is Cdv_Ortsverband))
				throw new ArgumentNullException("Falsches Objekt an Cdv_OrtsverbandWrapper übergeben. Cdv_Ortsverband wurde erwartet! Methode:Cdv_OrtsverbandWrapper.NeuerEintrag");
			int pout_i_neueID = -1;						
			// Anfrage zusammenstellen
			String str_INSERTAnfrage = "insert into \"Ortsverbaende\"("
				+ "\"Name\", " 
				+ "\"Ortsbeauftragter\", "
				+ "\"Erreichbarkeit\", "
				+ "\"Landesverband\", "
				+ "\"PLZ\", "
				+ "\"Ort\", "
				+ "\"Strasse\", "
				+ "\"Hausnummer\", "
				+ "\"GF_Bereich\", "
				+ "\"GF_PLZ\", "
				+ "\"GF_Ort\", "
				+ "\"GF_Strasse\", "
				+ "\"GF_Hausnummer\") values("
				+ "'" + ((Cdv_Ortsverband)pin_ob).OVName + "', "
				+ "'" + ((Cdv_Ortsverband)pin_ob).Ortsbeauftragter + "', "
				+ "'" + ((Cdv_Ortsverband)pin_ob).OVErreichbarkeit + "', "
				+ "'" + ((Cdv_Ortsverband)pin_ob).Landesverband + "', "
				+ "'" + ((Cdv_Ortsverband)pin_ob).OVAnschrift.PLZ + "', "
				+ "'" + ((Cdv_Ortsverband)pin_ob).OVAnschrift.Ort + "', "
				+ "'" + ((Cdv_Ortsverband)pin_ob).OVAnschrift.Strasse + "', "
				+ "'" + ((Cdv_Ortsverband)pin_ob).OVAnschrift.Hausnummer + "', "
				+ "'" + ((Cdv_Ortsverband)pin_ob).Geschaeftsfuehrerbereich + "', "
				+ "'" + ((Cdv_Ortsverband)pin_ob).Geschaeftsfuehreranschrift.PLZ + "', "
				+ "'" + ((Cdv_Ortsverband)pin_ob).Geschaeftsfuehreranschrift.Ort + "', "
				+ "'" + ((Cdv_Ortsverband)pin_ob).Geschaeftsfuehreranschrift.Strasse + "', "
				+ "'" + ((Cdv_Ortsverband)pin_ob).Geschaeftsfuehreranschrift.Hausnummer + "')";

			// Anfrage an Cdv_DB übermitteln
			pout_i_neueID = db.AusfuehrenInsertAnfrage(str_INSERTAnfrage);					
			return pout_i_neueID;
		}

		public override bool AktualisiereEintrag(pELS.DV.Server.Interfaces.IPelsObject pin_ob)
		{			
			if(!(pin_ob is Cdv_Ortsverband))
				throw new ArgumentNullException("Falsches Objekt an Cdv_OrtsverbandWrapper übergeben. Cdv_Ortsverband wurde erwartet! Methode:Cdv_OrtsverbandWrapper.AktualisiereEintrag");
			// Anfrage zusammenstellen
			String str_UPDATEAnfrage = "update \"Ortsverbaende\" set "
				+ "\"Name\"=' " + ((Cdv_Ortsverband)pin_ob).OVName + "', "
				+ "\"Ortsbeauftragter\"=' " + ((Cdv_Ortsverband)pin_ob).Ortsbeauftragter + "', "
				+ "\"Erreichbarkeit\"=' " + ((Cdv_Ortsverband)pin_ob).OVErreichbarkeit + "', "
				+ "\"Landesverband\"=' " + ((Cdv_Ortsverband)pin_ob).Landesverband + "', "
				+ "\"PLZ\"=' " + ((Cdv_Ortsverband)pin_ob).OVAnschrift.PLZ + "', "
				+ "\"Ort\"=' " + ((Cdv_Ortsverband)pin_ob).OVAnschrift.Ort + "', "
				+ "\"Strasse\"=' " + ((Cdv_Ortsverband)pin_ob).OVAnschrift.Strasse + "', "
				+ "\"Hausnummer\"=' " + ((Cdv_Ortsverband)pin_ob).OVAnschrift.Hausnummer + "', "
				+ "\"GF_Bereich\"=' " + ((Cdv_Ortsverband)pin_ob).Geschaeftsfuehrerbereich + "', "
				+ "\"GF_PLZ\"=' " + ((Cdv_Ortsverband)pin_ob).Geschaeftsfuehreranschrift.PLZ + "', "
				+ "\"GF_Ort\"=' " + ((Cdv_Ortsverband)pin_ob).Geschaeftsfuehreranschrift.Ort + "', "
				+ "\"GF_Strasse\"=' " + ((Cdv_Ortsverband)pin_ob).Geschaeftsfuehreranschrift.Strasse + "', "
				+ "\"GF_Hausnummer\"=' " + ((Cdv_Ortsverband)pin_ob).Geschaeftsfuehreranschrift.Hausnummer + "' "
				+ "where \"ID\"=" + ((Cdv_Ortsverband)pin_ob).ID.ToString();

			return db.AusfuehrenUpdateAnfrage(str_UPDATEAnfrage);
		}
	
		public override pELS.DV.Server.Interfaces.IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_ovs;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;						
			// Select Anfrage
			String str_SELECTAnfrage = "select * from \"Ortsverbaende\"";
			// Zugriff auf Datenbank
			dreader_ovs = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_Ortsverband[] ov_anfrageergebnisse = new Cdv_Ortsverband[i_anzahlZeilen];
			// Laufvariablen
			int i = 0;

			while (dreader_ovs.Read())
			{
				ov_anfrageergebnisse[i] = new Cdv_Ortsverband();
				ov_anfrageergebnisse[i].ID = dreader_ovs.GetInt32(dreader_ovs.GetOrdinal("ID"));
				ov_anfrageergebnisse[i].OVName = dreader_ovs.GetString(dreader_ovs.GetOrdinal("Name"));
				ov_anfrageergebnisse[i].Ortsbeauftragter = dreader_ovs.GetString(dreader_ovs.GetOrdinal("Ortsbeauftragter"));
				ov_anfrageergebnisse[i].OVErreichbarkeit = dreader_ovs.GetString(dreader_ovs.GetOrdinal("Erreichbarkeit"));
				ov_anfrageergebnisse[i].Landesverband = dreader_ovs.GetString(dreader_ovs.GetOrdinal("Landesverband"));
				ov_anfrageergebnisse[i].OVAnschrift.PLZ = dreader_ovs.GetString(dreader_ovs.GetOrdinal("PLZ"));
				ov_anfrageergebnisse[i].OVAnschrift.Ort = dreader_ovs.GetString(dreader_ovs.GetOrdinal("Ort"));
				ov_anfrageergebnisse[i].OVAnschrift.Strasse = dreader_ovs.GetString(dreader_ovs.GetOrdinal("Strasse"));
				ov_anfrageergebnisse[i].OVAnschrift.Hausnummer = dreader_ovs.GetString(dreader_ovs.GetOrdinal("Hausnummer"));
				ov_anfrageergebnisse[i].Geschaeftsfuehrerbereich = dreader_ovs.GetString(dreader_ovs.GetOrdinal("GF_Bereich"));
				ov_anfrageergebnisse[i].Geschaeftsfuehreranschrift.PLZ = dreader_ovs.GetString(dreader_ovs.GetOrdinal("GF_PLZ"));
				ov_anfrageergebnisse[i].Geschaeftsfuehreranschrift.Ort = dreader_ovs.GetString(dreader_ovs.GetOrdinal("GF_Ort"));
				ov_anfrageergebnisse[i].Geschaeftsfuehreranschrift.Strasse = dreader_ovs.GetString(dreader_ovs.GetOrdinal("GF_Strasse"));
				ov_anfrageergebnisse[i].Geschaeftsfuehreranschrift.Hausnummer = dreader_ovs.GetString(dreader_ovs.GetOrdinal("GF_Hausnummer"));
				i++;
			}
			return ov_anfrageergebnisse;
		}

		
		
		


	}
}
