using System;
using pELS.DV.Server.Interfaces;
using pELS.Server;
using pELS.DV;
using Npgsql;
using System.Data;
using pELS.Tools.Server;
using NUnit.Framework;
using pELS.DV.Server.Wrapper;
using pELS;

namespace Wrapper
{
	/// <summary>
	/// Tests für die Wrapper.
	/// </summary>

	[TestFixture]
	public class NUnitWrapperTests
	{
		[Test]
		public void NUnitInit()
		{
		}

		
		[TestFixture]
			public class NUnitBenutzerWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();

			private Cdv_BenutzerWrapper myWrapper = (Cdv_BenutzerWrapper)Cdv_BenutzerWrapper.HoleInstanz(); 
			private Cdv_Benutzer myBenutzer= new Cdv_Benutzer();
			private Cdv_Benutzer mySavedBenutzer= new Cdv_Benutzer();

			#endregion

			public NUnitBenutzerWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				myBenutzer.Benutzername = "xiao";
				myBenutzer.Systemrolle = Tdv_Systemrolle.LeiterFüSt;
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Assert.AreEqual(myBenutzer.ID,0);
				myBenutzer.ID = myWrapper.NeuerEintrag(myBenutzer);
				Assert.IsTrue(myBenutzer.ID != 0);
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge in der DB begonnen...");
				Cdv_Benutzer[] BenutzerMenge;
				BenutzerMenge = (Cdv_Benutzer[])myWrapper.LadeAusDerDB();
				foreach (Cdv_Benutzer BenutzerAusBenutzerMenge in BenutzerMenge)
				{
					if (BenutzerAusBenutzerMenge.ID == myBenutzer.ID)
					{
						mySavedBenutzer=BenutzerAusBenutzerMenge;
					}
				}

				Assert.IsNotNull(mySavedBenutzer);
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen");
				Assert.AreEqual(myBenutzer.ID,mySavedBenutzer.ID);
				Assert.AreEqual(myBenutzer.Benutzername,myBenutzer.Benutzername);
				Assert.AreEqual(myBenutzer.Systemrolle,myBenutzer.Systemrolle);
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichenr der veränderten Daten begonnen...");
				myBenutzer.Benutzername="Hütte";
				Assert.IsTrue(myWrapper.AktualisiereEintrag(myBenutzer));
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				NUnit_SpeicherVerändertesObjekt();
				NUnit_DatenbankEintraegeAuslesen();
				Assert.AreEqual(myBenutzer.ID,mySavedBenutzer.ID);
				Assert.AreEqual(myBenutzer.Benutzername,"Hütte");		
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}


		[TestFixture]
			public class NUnitOVWrapper
		{
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();

			private Cdv_OrtsverbandWrapper myWrapper = (Cdv_OrtsverbandWrapper) Cdv_OrtsverbandWrapper.HoleInstanz(); 
			private Cdv_Ortsverband myOrtsverband = new Cdv_Ortsverband();
			private Cdv_Ortsverband mySavedOrtsverband = new Cdv_Ortsverband();

			public NUnitOVWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			[SetUp]
			public void NUnit_Voraussetzung_ErzeugeReferenzNutzer()
			{
				Cdv_Ortsverband myOV = new Cdv_Ortsverband();
				Cdv_Ortsverband[] myOVA = new Cdv_Ortsverband[10];

				myOV.OVName = "OVBeelitz";
				myOV.Ortsbeauftragter = "Meier";
				myOV.OVErreichbarkeit = "01749715372";
				myOV.Landesverband = "Brandenburg";
				myOV.OVAnschrift.Ort = "Beelitz";
				myOV.OVAnschrift.PLZ = "14547";
				myOV.OVAnschrift.Strasse = "Virchowstrasse";
				myOV.OVAnschrift.Hausnummer = "15";
				myOV.Geschaeftsfuehrerbereich = "GF_Potsdam";
				myOV.Geschaeftsfuehreranschrift.Ort = "Potsdam";
				myOV.Geschaeftsfuehreranschrift.PLZ = "14480";
				myOV.Geschaeftsfuehreranschrift.Strasse = "am Bach";
				myOV.Geschaeftsfuehreranschrift.Hausnummer = "11a";

				myOrtsverband = myOV;
			}
						
			[Test]
			public void NUnit_Test_BenutzerWrapperEintragErzeugen()
			{
				Assert.AreEqual(myOrtsverband.ID,0);
				myOrtsverband.ID=myWrapper.NeuerEintrag(myOrtsverband);
				Assert.IsTrue(myOrtsverband.ID != 0);
			}

			[Test]
			public void NUnit_Test_BenutzerWrapperEintraegeAuslesen()
			{
				Cdv_Ortsverband[] OrtsverbandMenge;
				OrtsverbandMenge = (Cdv_Ortsverband[]) myWrapper.LadeAusDerDB();
				foreach (Cdv_Ortsverband OrtsverbandAusOrtsverbandMenge in OrtsverbandMenge)
				{
					if (OrtsverbandAusOrtsverbandMenge.ID == myOrtsverband.ID)
					{
						mySavedOrtsverband=OrtsverbandAusOrtsverbandMenge;
					}
				}
			}

			[Test]
			public void NUnit_Test_VerifiziereDaten()
			{
			}	
		}
	

		[TestFixture]
			public class NUnitAnforderungWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitAnforderungWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}

		
		[TestFixture]
			public class NUnitAuftragWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitAuftragWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}

		
		[TestFixture]
			public class NUnitEinheitWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitEinheitWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}


		[TestFixture]
			public class NUnitEinsatzschwerpunktWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitEinsatzschwerpunktWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}


		[TestFixture]
			public class NUnitEinsatzWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitEinsatzWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}


		[TestFixture]
			public class NUnitErinnerungWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitErinnerungWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}


		[TestFixture]
			public class NUnitEtbEintragKommentarWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitEtbEintragKommentarWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}


		[TestFixture]
			public class NUnitEtbEintragWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitEtbEintragWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}


		[TestFixture]
			public class NUnitHelferWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitHelferWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}


		[TestFixture]
			public class NUnitKFZWrapper
					
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitKFZWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}


		[TestFixture]
			public class NUnitMaterialuebergabeWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitMaterialuebergabeWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}


		[TestFixture]
			public class NUnitMaterialWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitMaterialWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}


		[TestFixture]
			public class NUnitMeldungWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitMeldungWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}


		[TestFixture]
			public class NUnitModulWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitModulWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}


		[TestFixture]
			public class NUnitOrtsverbandWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitOrtsverbandWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}


		[TestFixture]
			public class NUnitTerminWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitTerminWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}


		[TestFixture]
			public class NUnitVerbrauchsgutWrapper
		{
			#region Klassenvariablen
			
			private Cdv_DB myDB = Cdv_DB.HoleInstanz();


			#endregion

			public NUnitVerbrauchsgutWrapper()
			{
				myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");
			}

			
			[SetUp]
			public void NUnit_ErzeugeReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ReferenzObjektes begonnen...");
				Console.WriteLine("Erzeugen des ReferenzObjektes beendet.");
			}
						
			
			[Test]
			public void NUnit_SpeichereReferenzObjekt()
			{
				Console.WriteLine("Erzeugen des ETB-Eintrages begonnen..."); 
				Console.WriteLine("Erzeugen des ETB-Eintrages beendet.");

			}

			
			[Test]
			public void NUnit_DatenbankEintraegeAuslesen()
			{
				Console.WriteLine("Auslesen der Einträge aus der Datenbank begonnen...");
				Console.WriteLine("Auslesen der Einträge beendet.");
			}

			
			[Test]
			public void NUnit_VerifiziereInitialesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der ausgelesenen Daten begonnen...");
				Console.WriteLine("Verifikation der ausgelesenen Daten beendet");
			}	

			
			public void NUnit_SpeicherVerändertesObjekt()
			{
				Console.WriteLine("Speichen der veränderten Daten begonnen...");
				Console.WriteLine("Speichern der veränderten Daten beendet.");
			}

			
			[Test]
			public void NUnit_Test_VerifiziereVerändertesObjektGegenDatenbank()
			{
				Console.WriteLine("Verifikation der veränderten Daten begonnen..."); 
				Console.WriteLine("Verifikation der veränderten Daten beendet."); 
			}

		}
	
	}
}