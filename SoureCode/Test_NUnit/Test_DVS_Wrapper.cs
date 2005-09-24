using System;
using NUnit.Framework;

using pELS.Server;
using pELS.DV.Server;
using pELS.DV.Server.Interfaces;


using pELS.DV.Server.ObjectManager;
using pELS.DV.Server.ObjectManager.Verwaltung;
using pELS.DV.Server.Wrapper;
using pELS.DV;


namespace pELS.Test.Unittest
{
	[TestFixture]
	public class NUnitWrapperTests
	{
		
		[TestFixture]
		public class NUnitBenutzerWrapper
		{
			private Cdv_BenutzerWrapper _Wrapper;
			private Cdv_Benutzer _referenceObject = null;
			private Cdv_Benutzer _comparisonObject = null;

			[SetUp]
			public void SetUp()
			{
				Cdv_DB DB = Cdv_DB.HoleInstanz();
				bool success = DB.VerbindeMitDB("postgres", "postgres", "127.0.0.1", "5432", "schuppe1", "0");
				Assert.IsTrue(true);
				_Wrapper = (Cdv_BenutzerWrapper)Cdv_BenutzerWrapper.HoleInstanz();
			}
	
			[Test]
			public void NUnit_NewEntry()
			{
				_referenceObject = null;
				_comparisonObject = null;
				
				_referenceObject= CreateRefObject();
				Assert.AreEqual(_referenceObject.ID,0);
				
				_referenceObject.ID = _Wrapper.NeuerEintrag(_referenceObject);
				Assert.IsTrue(_referenceObject.ID != 0);

				_comparisonObject = _Wrapper.LoadObject(_referenceObject.ID);

				Assert.IsNotNull(_comparisonObject);
				Assert.IsTrue(this.CompareObjects(_referenceObject, _comparisonObject));

				Assert.IsTrue(_Wrapper.RemoveObject(_referenceObject.ID));
			}

			[Test]
			public void NUnit_LoadAllEntries()
			{
				_referenceObject = null;
				_comparisonObject = null;
				
				_referenceObject= CreateRefObject();
				Assert.AreEqual(_referenceObject.ID,0);
				
				_referenceObject.ID = _Wrapper.NeuerEintrag(_referenceObject);
				Assert.IsTrue(_referenceObject.ID != 0);

				_comparisonObject = _Wrapper.LoadObject(_referenceObject.ID);

				Assert.IsNotNull(_comparisonObject);
				Assert.IsTrue(this.CompareObjects(_referenceObject, _comparisonObject));

				Assert.IsTrue(_Wrapper.RemoveObject(_referenceObject.ID));
			}

			//			[Test]
//			public void NUnit_DatenbankEintraegeAuslesen()
//			{
//				Console.WriteLine("Auslesen der Einträge in der DB begonnen...");
//				Cdv_Benutzer[] BenutzerMenge;
//				BenutzerMenge = (Cdv_Benutzer[])_Wrapper.LadeAusDerDB();
//				foreach (Cdv_Benutzer BenutzerAusBenutzerMenge in BenutzerMenge)
//				{
//					if (BenutzerAusBenutzerMenge.ID == _referenceObject.ID)
//					{
//						mySavedBenutzer=BenutzerAusBenutzerMenge;
//					}
//				}
//
//				Assert.IsNotNull(mySavedBenutzer);
//				Console.WriteLine("Auslesen der Einträge beendet.");
//			}
//
//			
//			[Test]
//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
//			{
//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen");
//				Assert.AreEqual(_referenceObject.ID,mySavedBenutzer.ID);
//				Assert.AreEqual(_referenceObject.Benutzername,_referenceObject.Benutzername);
//				Assert.AreEqual(_referenceObject.Systemrolle,_referenceObject.Systemrolle);
//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
//			}	
//
//			
//			public void NUnit_SpeicherVerändertesObjekt()
//			{
//				Console.WriteLine("Speichenr der veränderten Daten begonnen...");
//				_referenceObject.Benutzername="Hütte";
//				Assert.IsTrue(_Wrapper.AktualisiereEintrag(_referenceObject));
//				Console.WriteLine("Speichern der veränderten Daten beendet.");
//			}
//
//			
//			[Test]
//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
//			{
//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
//				NUnit_SpeicherVerändertesObjekt();
//				NUnit_DatenbankEintraegeAuslesen();
//				Assert.AreEqual(_referenceObject.ID,mySavedBenutzer.ID);
//				Assert.AreEqual(_referenceObject.Benutzername,"Hütte");		
//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
//			}
//
			private Cdv_Benutzer CreateRefObject() 
			{
				Cdv_Benutzer pout;
				pout = new Cdv_Benutzer();
				pout.Benutzername = "Max";
				pout.Systemrolle = Tdv_Systemrolle.LeiterFüSt;
				return pout;
			}
			
			private bool CompareObjects(Cdv_Benutzer obj1, Cdv_Benutzer obj2)
			{
				bool pout = true;
				if (obj1.Benutzername != obj2.Benutzername) pout = false;
				if (obj1.ID != obj2.ID) pout = false;
				if (obj1.Systemrolle != obj2.Systemrolle) pout = false;
				// is it reasonable to compare ids? should not have changed, right?
				if (obj1.Version != obj2.Version) pout = false;

				return pout;
			}
			

		}


		//		[TestFixture]
		//			public class NUnitOVWrapper
		//		{
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//			private Cdv_OrtsverbandWrapper myWrapper = (Cdv_OrtsverbandWrapper) Cdv_OrtsverbandWrapper.HoleInstanz(); 
		//			private Cdv_Ortsverband myOrtsverband = new Cdv_Ortsverband();
		//			private Cdv_Ortsverband mySavedOrtsverband = new Cdv_Ortsverband();
		//
		//			public NUnitOVWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			[SetUp]
		//			public void NUnit_Voraussetzung_ErzeugeReferenzNutzer()
		//			{
		//				Cdv_Ortsverband myOV = new Cdv_Ortsverband();
		//				Cdv_Ortsverband[] myOVA = new Cdv_Ortsverband[10];
		//
		//				myOV.OVName = "OVBeelitz";
		//				myOV.Ortsbeauftragter = "Meier";
		//				myOV.OVErreichbarkeit = "01749715372";
		//				myOV.Landesverband = "Brandenburg";
		//				myOV.OVAnschrift.Ort = "Beelitz";
		//				myOV.OVAnschrift.PLZ = "14547";
		//				myOV.OVAnschrift.Strasse = "Virchowstrasse";
		//				myOV.OVAnschrift.Hausnummer = "15";
		//				myOV.Geschaeftsfuehrerbereich = "GF_Potsdam";
		//				myOV.Geschaeftsfuehreranschrift.Ort = "Potsdam";
		//				myOV.Geschaeftsfuehreranschrift.PLZ = "14480";
		//				myOV.Geschaeftsfuehreranschrift.Strasse = "am Bach";
		//				myOV.Geschaeftsfuehreranschrift.Hausnummer = "11a";
		//
		//				myOrtsverband = myOV;
		//			}
		//						
		//			[Test]
		//			public void NUnit_Test_BenutzerWrapperEintragErzeugen()
		//			{
		//				Assert.AreEqual(myOrtsverband.ID,0);
		//				myOrtsverband.ID=myWrapper.NeuerEintrag(myOrtsverband);
		//				Assert.IsTrue(myOrtsverband.ID != 0);
		//			}
		//
		//			[Test]
		//			public void NUnit_Test_BenutzerWrapperEintraegeAuslesen()
		//			{
		//				Cdv_Ortsverband[] OrtsverbandMenge;
		//				OrtsverbandMenge = (Cdv_Ortsverband[]) myWrapper.LadeAusDerDB();
		//				foreach (Cdv_Ortsverband OrtsverbandAusOrtsverbandMenge in OrtsverbandMenge)
		//				{
		//					if (OrtsverbandAusOrtsverbandMenge.ID == myOrtsverband.ID)
		//					{
		//						mySavedOrtsverband=OrtsverbandAusOrtsverbandMenge;
		//					}
		//				}
		//			}
		//
		//			[Test]
		//			public void NUnit_Test_VerifiziereDaten()
		//			{
		//			}	
		//		}
		//	
		//
		//		[TestFixture]
		//			public class NUnitAnforderungWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitAnforderungWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//		
		//		[TestFixture]
		//			public class NUnitAuftragWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitAuftragWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//		
		//		[TestFixture]
		//			public class NUnitEinheitWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitEinheitWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//
		//		[TestFixture]
		//			public class NUnitEinsatzschwerpunktWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitEinsatzschwerpunktWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//
		//		[TestFixture]
		//			public class NUnitEinsatzWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitEinsatzWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//
		//		[TestFixture]
		//			public class NUnitErinnerungWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitErinnerungWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//
		//		[TestFixture]
		//			public class NUnitEtbEintragKommentarWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitEtbEintragKommentarWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//
		//		[TestFixture]
		//			public class NUnitEtbEintragWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitEtbEintragWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//
		//		[TestFixture]
		//			public class NUnitHelferWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitHelferWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//
		//		[TestFixture]
		//			public class NUnitKFZWrapper
		//					
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitKFZWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//
		//		[TestFixture]
		//			public class NUnitMaterialuebergabeWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitMaterialuebergabeWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//
		//		[TestFixture]
		//			public class NUnitMaterialWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitMaterialWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//
		//		[TestFixture]
		//			public class NUnitMeldungWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitMeldungWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//
		//		[TestFixture]
		//			public class NUnitModulWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitModulWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//
		//		[TestFixture]
		//			public class NUnitOrtsverbandWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitOrtsverbandWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//
		//		[TestFixture]
		//			public class NUnitTerminWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitTerminWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//
		//
		//		[TestFixture]
		//			public class NUnitVerbrauchsgutWrapper
		//		{
		//			#region Klassenvariablen
		//			
		//			private Cdv_DB myDB = Cdv_DB.HoleInstanz();
		//
		//
		//			#endregion
		//
		//			public NUnitVerbrauchsgutWrapper()
		//			{
		//				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
		//			}
		//
		//			
		//			[SetUp]
		//			public void NUnit_ErzeugeReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
		//				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
		//			}
		//						
		//			
		//			[Test]
		//			public void NUnit_SpeichereReferenzObjekt()
		//			{
		//				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
		//				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");
		//
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_DatenbankEintraegeAuslesen()
		//			{
		//				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
		//				Console.WriteLine("Auslesen der Einträge beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
		//				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
		//			}	
		//
		//			
		//			public void NUnit_SpeicherVerändertesObjekt()
		//			{
		//				Console.WriteLine("Speichen der veränderten Daten begonnen...");
		//				Console.WriteLine("Speichern der veränderten Daten beendet.");
		//			}
		//
		//			
		//			[Test]
		//			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
		//			{
		//				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
		//				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
		//			}
		//
		//		}
		//	
	}
}
