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
	/// Erste Implementation ?
	/// Überarbeitete Implementation: alexG 01.03.2005
	/// Debug & Tests: 01.03.05 Hütte
	/// </summary>
	
	public class Cdv_ModulWrapper: Cdv_WrapperBase
	{

		#region Variablen
		// singleton-Instanz und ihr Refernzzähler
		private static Cdv_WrapperBase _obj_instanzVonCdv_WrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion

		#region Konstruktor
		private Cdv_ModulWrapper()
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
				_obj_instanzVonCdv_WrapperBase = new Cdv_ModulWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonCdv_WrapperBase;				 
		}	
		#endregion

		#region Virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			// Ist korrekter Objekttyp.
			if (pin_ob is Cdv_Modul)
			{
				String str_INSERTAnfrage= "insert into \"Module\"("
					+ "\"Modulname\""
					+ ", \"EinsatzschwerpunktID\") values("
					+ "'" + CMethoden.KonvertiereStringFuerDB (((Cdv_Modul)pin_ob).Modulname) + "'" 
					+ ", '" + ((Cdv_Modul)pin_ob).EinsatzschwerpunktID + "'" + ")";				
				
				// Anfrage an Cdv_DB übermitteln
				return(db.AusfuehrenInsertAnfrage(str_INSERTAnfrage));	
			}
			else
			{
				//bei Falschem Objekttyp excetion werfen.
				throw(new ArgumentNullException("Falsches Objekt an Cdv_ModulWrapper übergeben. Modul wurde erwartet!"));
			}
		}
			
		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Modul))
				throw new ArgumentNullException("Falsches Objekt an Cdv_ModulWrapper übergeben. Modul wurde erwartet!");
			String str_UPDATEAnfrage = "update \"Module\" set "
				+ "\"Modulname\"= " + "'" + CMethoden.KonvertiereStringFuerDB(((Cdv_Modul) pin_ob).Modulname) + "'" + " ,"
				+ "\"EinsatzschwerpunktID\"= " + "'" + ((Cdv_Modul) pin_ob).EinsatzschwerpunktID+ "'" + ""
				+ "where \"ID\"=" + ((Cdv_Modul) pin_ob).ID;

			// Anfrage an Cdv_DB weiterleiten
			return(db.AusfuehrenUpdateAnfrage(str_UPDATEAnfrage));
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt
			NpgsqlDataReader dreader_Module;
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen;
			// Select anfrage			
			String str_SELECTAnfrage = "select * from \"Module\"";
			dreader_Module = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			// Objekte-Behälter für die Ergebnisse
			Cdv_Modul[] Module_anfrageergebnisse = new Cdv_Modul[i_anzahlZeilen];
			int i = 0;

			while(dreader_Module.Read())
			{
				Module_anfrageergebnisse[i] = new Cdv_Modul();
				Module_anfrageergebnisse[i].ID = dreader_Module.GetInt32(dreader_Module.GetOrdinal("ID"));
				Module_anfrageergebnisse[i].Modulname = CMethoden.KonvertiereStringAusDB(dreader_Module.GetString(dreader_Module.GetOrdinal("Modulname")));
				Module_anfrageergebnisse[i].EinsatzschwerpunktID = dreader_Module.GetInt32(dreader_Module.GetOrdinal ("EinsatzschwerpunktID"));
				i++;
			}

			return(Module_anfrageergebnisse);
		}
		
		#endregion
	}	
}
