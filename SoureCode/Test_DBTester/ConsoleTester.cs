using System;
using pELS.DV.Server.Interfaces;
using pELS.DV.Server.ObjectManager;
using pELS.DV.Server.ObjectManager.Interfaces;
using pELS.DV;
using pELS.Server;

namespace DBTester
{
//	/// <summary>
//	/// Zusammenfassung für Class1.
//	/// </summary>
	class CDBTester
	{
//
//		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				Cdv_DB myDB = Cdv_DB.HoleInstanz();
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");

				IObjectManager om = Cdv_ObjMgr.HoleInstanz();//Activator.GetObject(...)
				
				IVerwaltung v = om.Einsaetze;
				Cdv_Einsatz a1 = new Cdv_Einsatz();
				a1.Einsatzort = "Bla1";
				a1.VonDatum = Convert.ToDateTime("2005-12-12 10:10:10");
				Cdv_Einsatz a2 = new Cdv_Einsatz();
				a2.Einsatzort = "Olley";
				a2.VonDatum = Convert.ToDateTime("2006-12-12 10:10:10");
				IPelsObject[] ipoaInsert = new IPelsObject[2];
				ipoaInsert[0] = a1;
				ipoaInsert[1] = a2;
				v.Speichern(ipoaInsert);
				a1.Einsatzort = "Beelitz";
				v.Speichern(a1);
				IPelsObject[] ipoa = v.HolenAlle();
				Cdv_Helfer h = new Cdv_Helfer();
				v.Speichern((IPelsObject) h);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
