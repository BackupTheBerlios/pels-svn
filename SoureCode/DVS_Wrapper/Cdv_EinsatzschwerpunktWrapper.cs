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
	/// Beginn der Implementierung 23.02.05 - alexG
	/// Abschluss der Implementierung: 01.03.05
	/// test & debug: 02.03.05 - Hütte
	/// </summary>


	public class Cdv_EinsatzschwerpunktWrapper: Cdv_WrapperBase
	{
		#region Variablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonCdv_WrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion
		#region Konstruktor
		private Cdv_EinsatzschwerpunktWrapper()
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
				_obj_instanzVonCdv_WrapperBase = new Cdv_EinsatzschwerpunktWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_WrapperBase;				 
		}	
		#endregion
		#region virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Einsatzschwerpunkt))
				throw new ArgumentNullException("Falsches Objekt an Cdv_EinsatzschwerpunktWrapper übergeben. Cdv_Einsatzschwerpunkt wurde erwartet! Methode:Cdv_EinsatzschwerpunktWrapper.NeuerEintrag");
			// Objekt umcasten nach Cdv_Einsatzschwerpunkt
			Cdv_Einsatzschwerpunkt myESP = pin_ob as Cdv_Einsatzschwerpunkt;
			int i_ESP_ID;
			
			// Insertanfrage
			String str_INSERTAnfrage = "insert into \"Einsatzschwerpunkte\"("
				+ "\"Bezeichnung\", "
				+ "\"Lage_Autor\", "
				+ "\"Lage_Text\", "
				+ "\"Prioritaet\", "
				+ "\"EinsatzID\", "
				+ "\"EinsatzleiterHelferID\") values("
				//Belegen der Werte
				+ "'" + CMethoden.KonvertiereStringFuerDB(myESP.Bezeichnung) + "', "
				+ "'" + CMethoden.KonvertiereStringFuerDB(myESP.Lage.Autor)+ "', "
				+ "'" + CMethoden.KonvertiereStringFuerDB(myESP.Lage.Text) + "', "
				+ "'" + (int) myESP.Prioritaet + "', "
				+ "'" + myESP.EinsatzID + "', "
				+ "'" + myESP.EinsatzleiterHelferID + "');";
			i_ESP_ID = db.AusfuehrenInsertAnfrage(str_INSERTAnfrage);			

			return i_ESP_ID;
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)		
		{
			if(!(pin_ob is Cdv_Einsatzschwerpunkt))
				throw new ArgumentNullException("Falsches Objekt an Cdv_EinsatzschwerpunktWrapper übergeben. Cdv_Einsatzschwerpunkt wurde erwartet! Methode:Cdv_EinsatzschwerpunktWrapper.AktualisiereEintrag");
			// Objekt umcasten nach Cdv_Einsatzschwerpunkt
			Cdv_Einsatzschwerpunkt myESP = pin_ob as Cdv_Einsatzschwerpunkt;			
			
			// Update auf Einsatzschwerpunkte
			String str_UPDATEAnfrage = "update\"Einsatzschwerpunkte\" set "
				+ "\"Bezeichnung\" ='"+CMethoden.KonvertiereStringFuerDB(myESP.Bezeichnung)+"', "
				+ "\"Lage_Autor\" ='"+CMethoden.KonvertiereStringFuerDB(myESP.Lage.Autor)+"', "
				+ "\"Lage_Text\" ='" + CMethoden.KonvertiereStringFuerDB(myESP.Lage.Text)+"', "
				+ "\"Prioritaet\"= '" + (int) myESP.Prioritaet + "', "
				+ "\"EinsatzID\" = '" + myESP.EinsatzID + "', "
				+ "\"EinsatzleiterHelferID\" ='" + myESP.EinsatzleiterHelferID + "' ";
				
			str_UPDATEAnfrage += " where \"ID\"='"+myESP.ID+"';";

			return(db.AusfuehrenUpdateAnfrage(str_UPDATEAnfrage));
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt 
			NpgsqlDataReader dreader_esp_erg;
			
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen = 0;
			// Select anfrage
			String str_SELECTAnfrage = "Select * from \"Einsatzschwerpunkte\"";
			// Zugriff auf DB
			dreader_esp_erg = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			
			// Objekte-Behälter für die Ergebnisse

			Cdv_Einsatzschwerpunkt[] esp_erg = new Cdv_Einsatzschwerpunkt[i_anzahlZeilen];
			
			int i = 0;
			//alle Attribute belegen
			while(dreader_esp_erg.Read())
			{
				esp_erg[i] = new Cdv_Einsatzschwerpunkt();
				esp_erg[i].ID = dreader_esp_erg.GetInt32(dreader_esp_erg.GetOrdinal("ID"));
				esp_erg[i].Bezeichnung = CMethoden.KonvertiereStringAusDB(dreader_esp_erg.GetString(dreader_esp_erg.GetOrdinal("Bezeichnung")));
				esp_erg[i].Lage.Autor = CMethoden.KonvertiereStringAusDB(dreader_esp_erg.GetString(dreader_esp_erg.GetOrdinal("Lage_Autor")));
				esp_erg[i].Lage.Text = CMethoden.KonvertiereStringAusDB(dreader_esp_erg.GetString(dreader_esp_erg.GetOrdinal("Lage_Text")));
				esp_erg[i].Prioritaet = dreader_esp_erg.GetInt32(dreader_esp_erg.GetOrdinal("Prioritaet"));
				esp_erg[i].EinsatzID = dreader_esp_erg.GetInt32(dreader_esp_erg.GetOrdinal("EinsatzID"));
				esp_erg[i].EinsatzleiterHelferID = dreader_esp_erg.GetInt32(dreader_esp_erg.GetOrdinal("EinsatzleiterHelferID"));		
				i++;
			}
			return(esp_erg);
		}
		#endregion
				
	}
}
