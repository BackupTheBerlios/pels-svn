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
	/// Implementiert: 01.03.05 alexG
	/// Test & Debug: 02.03.05 Hütte
	/// </summary>

	public class Cdv_EinheitWrapper: Cdv_WrapperBase
	{
		#region Klassenvariablen
		private static Cdv_WrapperBase _obj_instanzVonWrapperBase;
		private static int _i_anzahlReferenzen;
		#endregion
		#region statische Methoden
		public static Cdv_WrapperBase HoleInstanz()
		{
			// Instanz erstellen, wenn noch nicht vorhanden
			if (_obj_instanzVonWrapperBase == null)
				_obj_instanzVonWrapperBase = new Cdv_EinheitWrapper();
			// Referenzen hochzählen
			_i_anzahlReferenzen++;
			// Instanz zurückgeben
			return _obj_instanzVonWrapperBase;
		}
		#endregion
		#region Konstruktor
		private Cdv_EinheitWrapper()
		{
			this.db = Cdv_DB.HoleInstanz();
			_i_anzahlReferenzen = 0;
		}
		#endregion
		#region virtuelle Methoden
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Einheit))
				throw new ArgumentNullException("Falsches Objekt an Cdv_EinheitWrapper übergeben. Cdv_Einheit wurde erwartet! Methode:Cdv_EinheitWrapper.NeuerEintrag");
			//zusätzliche Variablen:
			int i_EinheitsID;

			// Objekt umcasten nach Cdv_Einheit
			Cdv_Einheit myEinheit = pin_ob as Cdv_Einheit;
			// Insertanfrage

			StringBuilder strQuery = new StringBuilder("insert into \"Einheiten\"(", 500);
			//Abbilden der von Kraft eingeerbten Attribute
			strQuery.Append("\"Kraeftestatus\", \"EinsatzschwerpunktID\", \"Kommentar_Autor\", \"Kommentar_Text\", \"ModulID\", \"Name\", \"Funkrufname\", \"Erreichbarkeit\", \"Sollstaerke\", \"Iststaerke\", \"Geschaeftsstelle\", \"OVID\", \"EinheitsfuehrerID\", \"StellvertreterID\", \"Betriebsverbrauch\") values('");
			strQuery.Append( (int) myEinheit.Kraeftestatus );
			strQuery.Append( "', '" );
			strQuery.Append( myEinheit.EinsatzschwerpunktID );
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myEinheit.Kommentar.Autor) );
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myEinheit.Kommentar.Text) );
			strQuery.Append( "', '" );
			strQuery.Append( myEinheit.ModulID );
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myEinheit.Name) );
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myEinheit.Funkrufname) );
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myEinheit.Erreichbarkeit) );
			strQuery.Append( "', '" );
			strQuery.Append( myEinheit.SollStaerke );
			strQuery.Append( "', '" );
			strQuery.Append( myEinheit.IstStaerke );
			strQuery.Append( "', '" );
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myEinheit.GST) );
			strQuery.Append( "', '" );
			strQuery.Append( myEinheit.OVID );
			strQuery.Append( "', '" );
			strQuery.Append( myEinheit.EinheitenfuehrerHelferID );
			strQuery.Append( "', '" );
			strQuery.Append( myEinheit.StellvertreterHelferID );
			strQuery.Append( "', '" );
			strQuery.Append( myEinheit.Betriebsverbrauch );
			strQuery.Append( "')");
			i_EinheitsID = db.AusfuehrenInsertAnfrage(strQuery.ToString());
			//einheit ist eingefügt

			#region Mengen abbilden
			//HelferMengeID

			if(myEinheit.HelferIDMenge != null)
			{	//einheit enthält helferMenge
				for (int i=0; i<myEinheit.HelferIDMenge.Length; i++)
				{
					string str_InstertAnfrageHelfer = "insert into \"Helfer_Einheit\""
						+"(\"HelferID\", \"EinheitID\") values ('"+myEinheit.HelferIDMenge[i]+"','"+i_EinheitsID+"');";					
					db.AusfuehrenInsertAnfrage(str_InstertAnfrageHelfer);
				}
			}

			//KfzKreafteIDMenge
			if(myEinheit.KfzKraefteIDMenge != null)
			{	//einheit enthält helferMenge
				for (int i=0; i<myEinheit.KfzKraefteIDMenge.Length; i++)
				{
					string str_InstertAnfrageHelfer = "insert into \"Kfz_Einheit\""
						+"(\"KfzID\", \"EinheitID\") values ('"+myEinheit.KfzKraefteIDMenge[i]+"','"+i_EinheitsID+"');";					
					db.AusfuehrenInsertAnfrage(str_InstertAnfrageHelfer);
				}
			}

			#endregion
			return i_EinheitsID;
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			if(!(pin_ob is Cdv_Einheit))
				throw new ArgumentNullException("Falsches Objekt an Cdv_EinheitWrapper übergeben. Cdv_Einheit wurde erwartet! Methode:Cdv_EinheitWrapper.AktualisiereEintrag");
			// Objekt umcasten nach Cdv_Anforderung
			Cdv_Einheit myEinheit = pin_ob as Cdv_Einheit;
			// Update

			StringBuilder strQuery = new StringBuilder("update \"Einheiten\" set", 300);
			strQuery.Append( " \"Kraeftestatus\"='");
			strQuery.Append( (int) myEinheit.Kraeftestatus);
			strQuery.Append( "', \"EinsatzschwerpunktID\"='");
			strQuery.Append( myEinheit.EinsatzschwerpunktID);
			strQuery.Append( "', \"Kommentar_Autor\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myEinheit.Kommentar.Autor));
			strQuery.Append( "', \"Kommentar_Text\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myEinheit.Kommentar.Text));
			strQuery.Append( "', \"ModulID\"='");
			strQuery.Append( myEinheit.ModulID);
			strQuery.Append( "', \"Name\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myEinheit.Name));
			strQuery.Append( "', \"Funkrufname\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myEinheit.Funkrufname));
			strQuery.Append( "', \"Erreichbarkeit\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myEinheit.Erreichbarkeit));
			strQuery.Append( "', \"Sollstaerke\"='");
			strQuery.Append( myEinheit.SollStaerke);
			strQuery.Append( "', \"Iststaerke\"='");
			strQuery.Append( myEinheit.IstStaerke);
			strQuery.Append( "', \"Geschaeftsstelle\"='");
			strQuery.Append( CMethoden.KonvertiereStringFuerDB(myEinheit.GST));
			strQuery.Append( "', \"OVID\"='");
			strQuery.Append( myEinheit.OVID);
			strQuery.Append( "', \"EinheitsfuehrerID\"='");
			strQuery.Append( myEinheit.EinheitenfuehrerHelferID);
			strQuery.Append( "', \"StellvertreterID\"='");
			strQuery.Append( myEinheit.StellvertreterHelferID);
			strQuery.Append( "', \"Betriebsverbrauch\"='");
			strQuery.Append( myEinheit.Betriebsverbrauch);
			strQuery.Append("' where \"ID\"= '");
			strQuery.Append(myEinheit.ID);
			strQuery.Append("';");
			if (db.AusfuehrenUpdateAnfrage(strQuery.ToString()))
			{ 
				//Erstmal alle verbindlichkeiten in den Entsprechenden Tabellen löschen:
				string str_DeleteHelferEinheit = "Delete from \"Helfer_Einheit\" where \"EinheitID\"='"+myEinheit.ID+"';";
				string str_DeleteKfzEinheit = "Delete from \"Kfz_Einheit\" where \"EinheitID\"='"+myEinheit.ID+"';";
				//string str_DeleteMaterialEinheit = "Delete from \"Material_Einheit\" where \"EinheitID\"='"+myEinheit.ID+"';";
				//string str_DeletefehlenfesMaterialEinheit = "Delete from \"FehlendesMaterial_Einheit\" where \"EinheitID\"='"+myEinheit.ID+"';";
				db.AusfuehrenDeleteAnfrage(str_DeleteHelferEinheit);
				db.AusfuehrenDeleteAnfrage(str_DeleteKfzEinheit);
				//db.AusfuehrenDeleteAnfrage(str_DeleteMaterialEinheit);
				//db.AusfuehrenDeleteAnfrage(str_DeletefehlenfesMaterialEinheit);

				//jetzt wieder ein insert der evtl. Verbindlichkeiten
				#region Mengen abbilden
				//HelferMengeID
				if(myEinheit.HelferIDMenge != null)
				{	//einheit enthält helferMenge
					for (int i=0; i<myEinheit.HelferIDMenge.Length; i++)
					{
						string str_InstertAnfrageHelfer = "insert into \"Helfer_Einheit\""
							+"(\"HelferID\", \"EinheitID\") values ('"+myEinheit.HelferIDMenge[i]+"','"+myEinheit.ID+"');";					
						db.AusfuehrenInsertAnfrage(str_InstertAnfrageHelfer);
					}
				}

				//KfzKraefteIDMenge
				if(myEinheit.KfzKraefteIDMenge != null)
				{	//einheit enthält helferMenge
					for (int i=0; i<myEinheit.KfzKraefteIDMenge.Length; i++)
					{
						string str_InstertAnfrageHelfer = "insert into \"Kfz_Einheit\""
							+"(\"KfzID\", \"EinheitID\") values ('"+myEinheit.KfzKraefteIDMenge[i]+"','"+myEinheit.ID+"');";					
						db.AusfuehrenInsertAnfrage(str_InstertAnfrageHelfer);
					}
				}
				#endregion
			}		
			else
				return false;

			return true;
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			// Reader, der Daten aufnimmt 
			NpgsqlDataReader dreader_einheit_erg;
			
			// Zum initialisieren des Pels-Objekt-Arrays
			int i_anzahlZeilen = 0;
			// Select anfrage
			String str_SELECTAnfrage = "Select * from \"Einheiten\"";
			// Zugriff auf DB
			dreader_einheit_erg = db.AusfuehrenSelectAnfrage(str_SELECTAnfrage, out i_anzahlZeilen);
			
			// Objekte-Behälter für die Ergebnisse

			Cdv_Einheit[] einheit_erg = new Cdv_Einheit[i_anzahlZeilen];
			
			int i = 0;
			//alle Attribute belegen
			while(dreader_einheit_erg.Read())
			{
				//erstmal alle eigentlichen Attribute belegen
				einheit_erg[i] = new Cdv_Einheit();
				einheit_erg[i].ID = dreader_einheit_erg.GetInt32(dreader_einheit_erg.GetOrdinal("ID"));
				//alle von Kraft eingeerbten Eigenschaften
				einheit_erg[i].Kraeftestatus = (Tdv_Kraeftestatus)dreader_einheit_erg.GetInt32(dreader_einheit_erg.GetOrdinal("Kraeftestatus"));
				einheit_erg[i].EinsatzschwerpunktID = dreader_einheit_erg.GetInt32(dreader_einheit_erg.GetOrdinal("EinsatzschwerpunktID"));
				einheit_erg[i].ModulID = dreader_einheit_erg.GetInt32(dreader_einheit_erg.GetOrdinal("ModulID"));
				einheit_erg[i].Kommentar.Autor = CMethoden.KonvertiereStringAusDB(dreader_einheit_erg.GetString(dreader_einheit_erg.GetOrdinal("Kommentar_Autor")));
				einheit_erg[i].Kommentar.Text = CMethoden.KonvertiereStringAusDB(dreader_einheit_erg.GetString(dreader_einheit_erg.GetOrdinal("Kommentar_Text")));
				//alle zur Einheit gehörenden eigenschaften
				einheit_erg[i].Name = CMethoden.KonvertiereStringAusDB(dreader_einheit_erg.GetString(dreader_einheit_erg.GetOrdinal("Name")));
				einheit_erg[i].Funkrufname = CMethoden.KonvertiereStringAusDB(dreader_einheit_erg.GetString(dreader_einheit_erg.GetOrdinal("Funkrufname")));
				einheit_erg[i].Erreichbarkeit = CMethoden.KonvertiereStringAusDB(dreader_einheit_erg.GetString(dreader_einheit_erg.GetOrdinal("Erreichbarkeit")));
				einheit_erg[i].SollStaerke = (uint) dreader_einheit_erg.GetInt32(dreader_einheit_erg.GetOrdinal("Sollstaerke"));
				einheit_erg[i].IstStaerke = (uint) dreader_einheit_erg.GetInt32(dreader_einheit_erg.GetOrdinal("Iststaerke"));
				einheit_erg[i].GST = CMethoden.KonvertiereStringAusDB(dreader_einheit_erg.GetString(dreader_einheit_erg.GetOrdinal("Geschaeftsstelle")));
				einheit_erg[i].OVID = dreader_einheit_erg.GetInt32(dreader_einheit_erg.GetOrdinal("OVID"));
				einheit_erg[i].EinheitenfuehrerHelferID = dreader_einheit_erg.GetInt32(dreader_einheit_erg.GetOrdinal("EinheitsfuehrerID"));
				einheit_erg[i].StellvertreterHelferID = dreader_einheit_erg.GetInt32(dreader_einheit_erg.GetOrdinal("StellvertreterID"));
				einheit_erg[i].Betriebsverbrauch = CMethoden.KonvertiereStringAusDB(dreader_einheit_erg.GetString(dreader_einheit_erg.GetOrdinal("Betriebsverbrauch")));

				#region Einlesen der IDMengen
				//platzhalter für Strings in C# ?
				string str_SelectAnfrageBegin = "Select * from \"";
				string str_SelectAnfrageEnde = "\" where \"EinheitID\"="+einheit_erg[i].ID+";";

				//KfzKraefteIDMenge
				int i_AnzahlKfzZeilen=0;
				NpgsqlDataReader dreader_kfzeinheit_erg =db.AusfuehrenSelectAnfrage(str_SelectAnfrageBegin+"Kfz_Einheit"+str_SelectAnfrageEnde, out i_AnzahlKfzZeilen);
				if(i_AnzahlKfzZeilen >0)
				{
					einheit_erg[i].KfzKraefteIDMenge = new int[i_AnzahlKfzZeilen];
					int i_tmp=0;
					while(dreader_kfzeinheit_erg.Read())
					{
						einheit_erg[i].KfzKraefteIDMenge[i_tmp] = dreader_kfzeinheit_erg.GetInt32(dreader_kfzeinheit_erg.GetOrdinal("KfzID"));
						i_tmp++;
					}					
				}
				
				//HelferIDMenge
				int i_AnzahlHelferZeilen=0;
				NpgsqlDataReader dreader_helfereinheit_erg =db.AusfuehrenSelectAnfrage(str_SelectAnfrageBegin+"Helfer_Einheit"+str_SelectAnfrageEnde, out i_AnzahlHelferZeilen);
				if(i_AnzahlHelferZeilen >0)
				{
					einheit_erg[i].HelferIDMenge = new int[i_AnzahlHelferZeilen];
					int i_tmp=0;
					while(dreader_helfereinheit_erg.Read())
					{
						einheit_erg[i].HelferIDMenge[i_tmp] = dreader_helfereinheit_erg.GetInt32(dreader_helfereinheit_erg.GetOrdinal("HelferID"));
						i_tmp++;
					}					
				}

				#endregion

				i++;
			}
			return(einheit_erg);
		}
		#endregion
	}
}
