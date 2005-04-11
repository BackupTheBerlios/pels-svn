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
	/// Zusammenfassung für Cdv_VerbrauchsgutWrapper.
	/// Implementiert: 28.02.05 Steini
	/// Debug & Tests: 28.02.05 alexG
	/// Debug & Tests: 01.03.05 Hütte
	/// </summary>
	

	public class Cdv_VerbrauchsgutWrapper: Cdv_WrapperBase
	{
		#region Variablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonCdv_WrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion

		#region Konstruktor
		private Cdv_VerbrauchsgutWrapper()
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
				_obj_instanzVonCdv_WrapperBase = new Cdv_VerbrauchsgutWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_WrapperBase;				 
		}	
		#endregion

		#region virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Verbrauchsgut))
				throw new ArgumentNullException("Falsches Objekt an Cdv_VerbrauchsgutWrapper übergeben. Cdv_Verbrauchsgut wurde erwartet! Methode:Cdv_VerbrauchsgutWrapper.NeuerEintrag");
			Cdv_Verbrauchsgut material = pin_ob as Cdv_Verbrauchsgut;
			String str_INSERTAnfrage;
			
			//das entsprechende Query wird zusammengebaut:
			str_INSERTAnfrage = "insert into \"Gueter\"("
				+ "\"Bezeichnung\", "
				+ "\"Menge\", "
				+ "\"Lagerort\", "
				+ "\"Art\", "
				+ "\"IstMaterial\", "
				+ "\"SpaetesterWbzpkt\""
				+") values("
				+"'"+ CMethoden.KonvertiereStringFuerDB(material.Bezeichnung) +"',"
				+"'"+ CMethoden.KonvertiereRealFuerDB(material.Menge) +"',"
				+"'"+ CMethoden.KonvertiereStringFuerDB(material.Lagerort) +"',"
				+"'"+ CMethoden.KonvertiereStringFuerDB(material.Art) +"',"
				+"'"+ false +"',"
				+"'"+ CMethoden.KonvertiereDatumFuerDB(material.SpaetesterWiederbeschaffungszeitpunkt)+"'"
				+");";

			return(db.AusfuehrenInsertAnfrage(str_INSERTAnfrage));
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Verbrauchsgut))
				throw new ArgumentNullException("Falsches Objekt an Cdv_VerbrauchsgutWrapper übergeben. Cdv_Verbrauchsgut wurde erwartet! Methode:Cdv_VerbrauchsgutWrapper.AktualisiereEintrag");
			Cdv_Verbrauchsgut material = pin_ob as Cdv_Verbrauchsgut;
			string myQ;
			
			//das entsprechende Query wird zusammengebaut:
			myQ = "update \"Gueter\" set "
				+ "\"Bezeichnung\"="
				+"'"+ material.Bezeichnung +"', "
				+ "\"Menge\"="
				+""+ CMethoden.KonvertiereRealFuerDB(material.Menge) +", "
				+ "\"Lagerort\"="
				+"'"+CMethoden.KonvertiereStringFuerDB(material.Lagerort) +"', "
				+ "\"Art\"="
				+"'"+ CMethoden.KonvertiereStringFuerDB(material.Art) +"', "
				+ "\"SpaetesterWbzpkt\"="
				+"'"+ CMethoden.KonvertiereDatumFuerDB(material.SpaetesterWiederbeschaffungszeitpunkt) +"' "
				+"where \"ID\"="
				+"'"+ material.ID +"' "
				+";";
			
			return db.AusfuehrenUpdateAnfrage(myQ);
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_material_erg;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage
			String str_SELECTAnfrage = "Select * from \"Gueter\" where \"IstMaterial\" = false";
			// Zugriff auf DB
			dreader_material_erg = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_Verbrauchsgut[] material = new Cdv_Verbrauchsgut[i_anzahlZeilen];
			int i = 0;
			
			while(dreader_material_erg.Read())
			{
				material[i] = new Cdv_Verbrauchsgut();
				material[i].ID = dreader_material_erg.GetInt32(dreader_material_erg.GetOrdinal("ID"));
				material[i].Bezeichnung = CMethoden.KonvertiereStringAusDB(dreader_material_erg.GetString(dreader_material_erg.GetOrdinal("Bezeichnung")));
				material[i].Menge = dreader_material_erg.GetFloat(dreader_material_erg.GetOrdinal("Menge"));
				material[i].Lagerort = CMethoden.KonvertiereStringAusDB(dreader_material_erg.GetString(dreader_material_erg.GetOrdinal("Lagerort")));
				material[i].Art = CMethoden.KonvertiereStringAusDB(dreader_material_erg.GetString(dreader_material_erg.GetOrdinal("Art")));
				material[i].SpaetesterWiederbeschaffungszeitpunkt = dreader_material_erg.GetDateTime(dreader_material_erg.GetOrdinal("SpaetesterWbzpkt"));
				i++;
			}
			return material;
		}
		#endregion
	}	
}

