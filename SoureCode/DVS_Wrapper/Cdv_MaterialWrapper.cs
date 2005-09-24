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
	/// Zusammenfassung für Cdv_MaterialWrapper.
	/// Implementiert: 28.02.05 Steini
	/// Debug & Tests: 28.02.05 alexG
	/// Debug & Tests: 01.03.05 Hütte
	/// </summary>
	
	public class Cdv_MaterialWrapper: Cdv_WrapperBase
	{ 
		#region Variablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonCdv_WrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion

		#region Konstruktor
		private Cdv_MaterialWrapper()
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
				_obj_instanzVonCdv_WrapperBase = new Cdv_MaterialWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_WrapperBase;				 
		}	
		#endregion

		#region virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Material))
				throw new ArgumentNullException("Falsches Objekt an Cdv_MaterialWrapper übergeben. Cdv_Material wurde erwartet! Methode:Cdv_MaterialWrapper.NeuerEintrag");
			Cdv_Material material = pin_ob as Cdv_Material;
			StringBuilder strQuery;
			
			//das entsprechende Query wird zusammengebaut:
			strQuery = new StringBuilder("insert into \"Gueter\"(", 300);
			strQuery.Append( "\"Bezeichnung\", \"Menge\", \"Lagerort\", \"Art\", \"IstMaterial\", \"AktuellerBesitzerID\", \"EigentuemerID\" ) values('");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(material.Bezeichnung) );
			strQuery.Append("', '");
			strQuery.Append( material.Menge );
			strQuery.Append("', '");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(material.Lagerort) );
			strQuery.Append("', '");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(material.Art) );
			strQuery.Append("', '");
			strQuery.Append( true );
			strQuery.Append("', '");
			strQuery.Append( material.AktuellerBesitzerKraftID );
			strQuery.Append("', '");
			strQuery.Append( material.EigentuemerKraftID );
			strQuery.Append("');");

			return(db.AusfuehrenInsertAnfrage(strQuery.ToString()));
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Material))
				throw new ArgumentNullException("Falsches Objekt an Cdv_MaterialWrapper übergeben. Cdv_Material wurde erwartet! Methode:Cdv_MaterialWrapper.AktualisiereEintrag");
			Cdv_Material material = pin_ob as Cdv_Material;
			StringBuilder strQuery;
			
			//das entsprechende Query wird zusammengebaut:
			strQuery = new StringBuilder("update \"Gueter\" set ", 300);
			strQuery.Append( "\"Bezeichnung\"='");
			strQuery.Append( material.Bezeichnung );
			strQuery.Append("', \"Menge\"=");
			strQuery.Append( material.Menge );
			strQuery.Append(", \"Lagerort\"='");
			strQuery.Append(CMethoden.KonvertiereStringFuerDB(material.Lagerort) );
			strQuery.Append("', \"Art\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(material.Art) );
			strQuery.Append("', \"AktuellerBesitzerID\"=");
			strQuery.Append(material.AktuellerBesitzerKraftID);
			strQuery.Append(", \"EigentuemerID\"=");
			strQuery.Append( material.EigentuemerKraftID);
			strQuery.Append(" where \"ID\"=");
			strQuery.Append( material.ID);
			strQuery.Append(" ;");
			
			return db.AusfuehrenUpdateAnfrage(strQuery.ToString());
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_material_erg;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage
			String str_SELECTAnfrage = "Select * from \"Gueter\" where \"IstMaterial\" = true";
			// Zugriff auf DB
			dreader_material_erg = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_Material[] material = new Cdv_Material[i_anzahlZeilen];
			int i = 0;
			
			while(dreader_material_erg.Read())
			{
				material[i] = new Cdv_Material();
				material[i].ID = dreader_material_erg.GetInt32(dreader_material_erg.GetOrdinal("ID"));
				material[i].Bezeichnung = CMethoden.KonvertiereStringAusDB(dreader_material_erg.GetString(dreader_material_erg.GetOrdinal("Bezeichnung")));
				material[i].Menge = dreader_material_erg.GetFloat(dreader_material_erg.GetOrdinal("Menge"));
				material[i].Lagerort = CMethoden.KonvertiereStringAusDB(dreader_material_erg.GetString(dreader_material_erg.GetOrdinal("Lagerort")));
				material[i].Art = CMethoden.KonvertiereStringAusDB(dreader_material_erg.GetString(dreader_material_erg.GetOrdinal("Art")));
				material[i].AktuellerBesitzerKraftID = dreader_material_erg.GetInt32(dreader_material_erg.GetOrdinal("AktuellerBesitzerID"));
				material[i].EigentuemerKraftID = dreader_material_erg.GetInt32(dreader_material_erg.GetOrdinal("EigentuemerID"));
				i++;
			}
			return material;
		}
		#endregion
	}

}
