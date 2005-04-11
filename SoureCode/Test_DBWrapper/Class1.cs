using System;
using pELS.DV.Server;
using pELS.DV;
using pELS.DV.Server.Interfaces;
using pELS.DV.Server.Wrapper;

namespace pELS.Server
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class TestDB_Wrapper
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Cdv_DB myDB = Cdv_DB.HoleInstanz();
			myDB.VerbindeMitDB("THW", "pels", "192.168.222.100", "5432", "pELS_DB", "0");

			//Cdv_EinsatzWrapper myWrapper = (Cdv_EinsatzWrapper)Cdv_EinsatzWrapper.HoleInstanz();
			//Cdv_BenutzerWrapper myWrapper = (Cdv_BenutzerWrapper)Cdv_BenutzerWrapper.HoleInstanz();
			//Cdv_AuftragWrapper myWrapper = (Cdv_AuftragWrapper)Cdv_AuftragWrapper.HoleInstanz();
			//Cdv_EinsatzschwerpunktWrapper myWrapper = (Cdv_EinsatzschwerpunktWrapper)Cdv_EinsatzschwerpunktWrapper.HoleInstanz();
			//Cdv_ModulWrapper myWrapper = (Cdv_ModulWrapper)Cdv_ModulWrapper.HoleInstanz();
			//Cdv_MeldungWrapper myWrapper = (Cdv_MeldungWrapper)Cdv_MeldungWrapper.HoleInstanz();
			//Cdv_TerminWrapper myWrapper = (Cdv_TerminWrapper)Cdv_TerminWrapper.HoleInstanz();
			//Cdv_ErinnerungWrapper myWrapper = (Cdv_ErinnerungWrapper)Cdv_ErinnerungWrapper.HoleInstanz();
			//Cdv_ErkundungsergebnisWrapper myWrapper = (Cdv_ErkundungsergebnisWrapper)Cdv_ErkundungsergebnisWrapper.HoleInstanz();
			//Cdv_OrtsverbandWrapper myWrapper = (Cdv_OrtsverbandWrapper)Cdv_OrtsverbandWrapper.HoleInstanz();
			//Cdv_MaterialWrapper myWrapper = (Cdv_MaterialWrapper)Cdv_MaterialWrapper.HoleInstanz();
			//Cdv_VerbrauchsgutWrapper myWrapper = (Cdv_VerbrauchsgutWrapper)Cdv_VerbrauchsgutWrapper.HoleInstanz();
			//Cdv_KFZWrapper myWrapper = (Cdv_KFZWrapper)Cdv_KFZWrapper.HoleInstanz();
			//Cdv_KFZWrapper myWrapper = (Cdv_KFZWrapper) Cdv_KFZWrapper.HoleInstanz();
			//Cdv_EinheitWrapper myWrapper = (Cdv_EinheitWrapper) Cdv_EinheitWrapper.HoleInstanz();
			//Cdv_MaterialuebergabeWrapper myWrapper = (Cdv_MaterialuebergabeWrapper) Cdv_MaterialuebergabeWrapper.HoleInstanz();
			//Cdv_HelferWrapper myWrapper = (Cdv_HelferWrapper) Cdv_HelferWrapper.HoleInstanz();
			//Cdv_EtbEintragWrapper myWrapper = (Cdv_EtbEintragWrapper) Cdv_EtbEintragWrapper.HoleInstanz();
			//Cdv_EtbEintragKommentarWrapper myWrapper = (Cdv_EtbEintragKommentarWrapper) Cdv_EtbEintragKommentarWrapper.HoleInstanz();
			
			#region testing OV
//			Cdv_Ortsverband myOV = new Cdv_Ortsverband();
//			Cdv_Ortsverband[] myOVA = new Cdv_Ortsverband[10];
//
//			myOV.OVName = "OVBeelitz";
//			myOV.Ortsbeauftragter = "Meier";
//			myOV.OVErreichbarkeit = "01749715372";
//			myOV.Landesverband = "Brandenburg";
//			myOV.OVAnschrift.Ort = "Beelitz";
//			myOV.OVAnschrift.PLZ = "14547";
//			myOV.OVAnschrift.Strasse = "Virchowstrasse";
//			myOV.OVAnschrift.Hausnummer = "15";
//			myOV.Geschaeftsfuehrerbereich = "GF_Potsdam";
//			myOV.Geschaeftsfuehreranschrift.Ort = "Potsdam";
//			myOV.Geschaeftsfuehreranschrift.PLZ = "14480";
//			myOV.Geschaeftsfuehreranschrift.Strasse = "am Bach";
//			myOV.Geschaeftsfuehreranschrift.Hausnummer = "11a";
//			Console.WriteLine(myWrapper.NeuerEintrag(myOV));
//
//			myOV.ID = 2;
//			myOVA = (Cdv_Ortsverband[])myWrapper.LadeAusDerDB();
//			foreach(Cdv_Ortsverband ov in myOVA)
//				Console.WriteLine("ID: "+ov.ID+"Name: "+ov.OVName+"Erreicharkeit"+ov.OVErreichbarkeit+"GFBereich"+ ov.Geschaeftsfuehrerbereich);
			#endregion
			#region testing Einsatz
//			Cdv_Einsatz[] myEinsatzMenge = new Cdv_Einsatz[10];
//			Cdv_Einsatz myEinsatz = new Cdv_Einsatz();
//			myEinsatz.Bezeichnung= "noerdlicher Stuetzpunkt";
//			myEinsatz.Einsatzort = "Berlin";
//			myEinsatz.VonDatum = Convert.ToDateTime("2007-02-22 10:54:00");
//			myEinsatz.BisDatum = Convert.ToDateTime("2007-04-04 10:10:10");			
//			myEinsatz.ID = myWrapper.NeuerEintrag(myEinsatz);
//			Console.WriteLine(myEinsatz.ID);
//			myEinsatzMenge = (Cdv_Einsatz[])myWrapper.LadeAusDerDB();
//			foreach (Cdv_Einsatz eins in myEinsatzMenge)
//			{
//				Console.WriteLine("ID: "+eins.ID+" Bezeichnung: "+ eins.Bezeichnung+" Ort: "+eins.Einsatzort+" VonDatum: "+eins.VonDatum+" Bisdatum: "+eins.BisDatum);
//			}
//			myEinsatz.Einsatzort = "HPI3";
//			myEinsatz.VonDatum = DateTime.Now;			
//			Console.WriteLine(myWrapper.AktualisiereEintrag(myEinsatz));
//			myEinsatzMenge = (Cdv_Einsatz[])myWrapper.LadeAusDerDB();
//			foreach (Cdv_Einsatz eins in myEinsatzMenge)
//			{
//				Console.WriteLine("Bezeichnung: "+ eins.Bezeichnung+" Ort: "+eins.Einsatzort+" VonDatum: "+eins.VonDatum+" Bisdatum: "+eins.BisDatum);
//			}
			#endregion
			#region testing Benutzer
//			Cdv_Benutzer myBenutzer = new Cdv_Benutzer();
//			Cdv_Benutzer[] myBenutzerMenge = new Cdv_Benutzer[10];
//
//			myBenutzer.Benutzername = "xiao";
//			myBenutzer.Systemrolle = Tdv_Systemrolle.LeiterFuest;
//			Console.WriteLine(myWrapper.NeuerEintrag(myBenutzer));

//			myBenutzer.ID = 3;
//			myBenutzerMenge = (Cdv_Benutzer[])myWrapper.LadeAusDerDB();
//			foreach(Cdv_Benutzer b in myBenutzerMenge)
//			{Console.WriteLine("Benutzername: "+b.Benutzername + "\tSystemrolle: "+b.Systemrolle.ToString());}
			
			//Console.WriteLine(myWrapper.AktualisiereEintrag(myBenutzer));
			#endregion
			#region testing Auftrag & Erkundungsbefehl
//			Cdv_Auftrag myAuftrag = new Cdv_Auftrag();
//			Cdv_Erkundungsbefehl myEB = new Cdv_Erkundungsbefehl();
//			
//			myAuftrag.BearbeiterBenutzerID = 1;
//			myAuftrag.Abfassungsdatum = DateTime.Now;
//			Console.WriteLine(myWrapper.NeuerEintrag(myAuftrag));
//			
//			//Teste: Schreibe Erkundungsbefehl
//			myEB.BearbeiterBenutzerID = 1;
//			Console.WriteLine(myWrapper.NeuerEintrag(myEB));
//
//			//Teste Auftrag Update
//			myAuftrag = new Cdv_Auftrag();
//			myAuftrag.ID = 10;
//			myAuftrag.BearbeiterBenutzerID = 2;
//			myAuftrag.IstUebermittelt = true;
//			myAuftrag.Text = "Hier wurde aktualisiert2";
//			myAuftrag.Absender = "AlexG";
//			Console.WriteLine(myWrapper.AktualisiereEintrag(myAuftrag));
//
//			//Teste Erkundungsbefehl Update
//			myEB = new Cdv_Erkundungsbefehl();
//			myEB.ID = 10;
//			myEB.BearbeiterBenutzerID = 1;
//			myEB.Text = "EB Update";
//			myEB.Absender = "los";
//			myEB.BefehlsArt = Tdv_BefehlArt.Transport;
//			myEB.WirdNachverfolgt = false;
//			Console.WriteLine(myWrapper.AktualisiereEintrag(myEB));
//
//			//Teste Auftrag LadeAusDerDB()
//
//			//myAuftrag.EinsatzschwerpunktID= 0;
//			//Console.WriteLine(myWrapper.NeuerEintrag(myAuftrag));												
//			
//			//myEB = new Cdv_Erkundungsbefehl();
//			Console.WriteLine(myWrapper.NeuerEintrag(myEB));
//			//Auftragsdaten belegen
//			myAuftrag.Text = "Das ist der Text";
//			myAuftrag.Abfassungsdatum = DateTime.Parse("01.01.2000");
//			myAuftrag.Uebermittlungsart = Tdv_Uebermittlungsart.Fax;
//			myAuftrag.IstUebermittelt = true;
//			myAuftrag.Absender = "Ich bin der absender";
//
//			myAuftrag.Ausfuehrungszeitpunkt = DateTime.Now; 
//			myAuftrag.SpaetesterErfuellungszeitpunkt = DateTime.Parse("04.04.2005");
//			myAuftrag.IstBefehl = false;
//			myAuftrag.WirdNachverfolgt = true;
//
//			IPelsObject[] objMenge = myWrapper.LadeAusDerDB();
//			Console.WriteLine("Hier kommen zuerst die Aufträge:");
//			foreach(Cdv_Auftrag au in objMenge)
//			{
//				if (au is Cdv_Erkundungsbefehl)
//					Console.WriteLine("Ich bin ein Erkundungsbefehl! \n Befehlsart: "+((Cdv_Erkundungsbefehl) au).BefehlsArt.ToString());
//				Console.WriteLine("ID:"+au.ID+" Text: "+au.Text+" Absender: "+au.Absender+" IstBefehl:"+au.IstBefehl);				
//			}				
//
//			//Teste: aktualisiere Erkundungsbefehl mit EmpfaengerMengeKraftID
//			//myEB = new Cdv_Erkundungsbefehl();
//			myEB.ID = 17;
//			myEB.BearbeiterBenutzerID = 1;
//			myEB.EmpfaengerMengeKraftID = new int[2];
//			myEB.EmpfaengerMengeKraftID[0] = 2;
//			myEB.EmpfaengerMengeKraftID[1] = 2;
//			Console.WriteLine(myWrapper.AktualisiereEintrag(myEB));
//
//			//Lade alle aus der DB mit evtl. den Empängern
//			objMenge = myWrapper.LadeAusDerDB();
//			foreach(Cdv_Auftrag au in objMenge)
//			{
//				if (au is Cdv_Erkundungsbefehl)
//				{
//					Console.WriteLine("Ich bin ein Erkundungsbefehl! \n Befehlsart: "+((Cdv_Erkundungsbefehl) au).BefehlsArt.ToString());					
//				}
//				Console.WriteLine("ID:"+au.ID+" Text: "+au.Text+" Absender: "+au.Absender+" IstBefehl:"+au.IstBefehl);				
//				if(au.EmpfaengerMengeKraftID != null)
//				{ 
//					foreach(int eintrag in au.EmpfaengerMengeKraftID)
//						Console.WriteLine("Empfänger: "+ eintrag);					
//				}
//			}

			#endregion
			#region testing Erkundungsbefehl
//			Cdv_Erkundungsbefehl myErkundungsbefehl = new Cdv_Erkundungsbefehl();						
//			myErkundungsbefehl.Ausfuehrungszeitpunkt = DateTime.Now;
//			myErkundungsbefehl.Text = ("Das ist der Text einer Mitteilung");
//			Console.WriteLine(myWrapper.NeuerEintrag(myErkundungsbefehl));												
			#endregion
			#region testing Einsatzschwerpunkt
//			//ESP anlegen	
//			Cdv_Einsatzschwerpunkt myESP= new Cdv_Einsatzschwerpunkt();
//			//Auftragsdaten belegen
//			myESP.Bezeichnung = "mein dritter ESP";
//			myESP.Prioritaet = 8;
//			myESP.Lage = new Cdv_Kommentar();
//			myESP.Lage.Text ="Ich bin der LageText";
//			myESP.Lage.Autor="Author der Lage";			
//			myESP.EinsatzleiterHelferID = 7;
//			
//			
//			Console.WriteLine(myESP.ID = myWrapper.NeuerEintrag(myESP));	
			
//			// UPDATE
//			Cdv_Einsatzschwerpunkt myESP= new Cdv_Einsatzschwerpunkt();
//			//Auftragsdaten belegen
//			myESP.ID = 276;
//			myESP.Bezeichnung = "Olleys ESP";
//			myESP.Prioritaet = 8;
//			myESP.Lage = new Cdv_Kommentar();
//			myESP.Lage.Text ="Dye em BLACK!";
//			myESP.Lage.Autor="Olley";
//			myESP.EinsatzleiterHelferID = 6;
//
//		
//			Console.WriteLine(myWrapper.AktualisiereEintrag(myESP));	
//
//			// SELECT		
//			Cdv_Einsatzschwerpunkt[] myESPMenge = (Cdv_Einsatzschwerpunkt[])myWrapper.LadeAusDerDB();
//			foreach(Cdv_Einsatzschwerpunkt esp in myESPMenge)
//			{
//				Console.WriteLine("ID: "+esp.ID+" Bez: "+esp.Bezeichnung+" Prio: "+esp.Prioritaet.ToString()+"\n"
//					+"Lage: "+esp.Lage.Text+ " Autor: "+esp.Lage.Autor+" ESPLeiter: "+esp.EinsatzleiterHelferID);
//				Console.WriteLine("\n");
//			}

			#endregion			
			#region testing Modul
//			//modul anlegen	
//			Cdv_Modul myModul= new Cdv_Modul();
//			//Moduldaten belegen
//			myModul.Modulname ="Steinis erstes Modul";
//			//Was wird mit der KräfteMenge
//			myModul.EinsatzschwerpunktID = 20;
//
//			Console.WriteLine(myWrapper.NeuerEintrag(myModul));
																					
//			//update testen
//			Cdv_Modul myModul = new Cdv_Modul();
//			myModul.ID = 18;
//			myModul.Modulname = "updateName";
//			Console.WriteLine(myWrapper.AktualisiereEintrag(myModul));															
//
//			//laden aller vorhandenen Module
//			Cdv_Modul[] myModulMenge = (Cdv_Modul[]) myWrapper.LadeAusDerDB();
//			foreach(Cdv_Modul m in myModulMenge)
//			{
//				Console.WriteLine(m.ID + " " + m.Modulname + " " + m.EinsatzschwerpunktID);
//			}
			
			#endregion
			#region testing Meldung
//			Cdv_Meldung myM = new Cdv_Meldung();
//			myM.BearbeiterBenutzerID = 1;
//			myM.Kategorie = Tdv_MeldungsKategorie.Staatsnot;
//			myM.Text = "Ich bin der Meldungstext";
//			myM.Abfassungsdatum = DateTime.MinValue;
//			myM.Uebermittlungsdatum = DateTime.MinValue;
//			myM.Absender = "Ich bin der Absender1";
//			myM.Uebermittlungsart = Tdv_Uebermittlungsart.Kurier;
//			myM.IstUebermittelt = false;
//			myM.Abfassungsdatum = DateTime.Now;
//			//myM.BearbeiterBenutzerID = 2;			
//			myM.ID = myWrapper.NeuerEintrag(myM);
//			Console.WriteLine(myM.ID);	
//			Cdv_Meldung[] myMeldgMenge = (Cdv_Meldung[])myWrapper.LadeAusDerDB();
//			foreach (Cdv_Meldung m in myMeldgMenge)
//			{
//				Console.WriteLine("ID: "+ m.ID+" Absender: "+m.Absender+" text: "+m.Text+" Kategorie:"+m.Kategorie.ToString()+"istuebermittelt: "+m.IstUebermittelt);
//			}
//			//Bereits geschriebene Meldg verändern und updaten();			
//			myM.BearbeiterBenutzerID = 1;
//			myM.IstUebermittelt = true;
//			myM.Kategorie = Tdv_MeldungsKategorie.Blitz;
//			myM.Text = "Ich bin der Meldungstext2";
//			myM.Abfassungsdatum = DateTime.Now;
//			myM.Uebermittlungsdatum = DateTime.Now;
//			myM.Absender = "Ich bin der Absender2";
//			myM.Uebermittlungsart = Tdv_Uebermittlungsart.Funk;											
//			Console.WriteLine(myWrapper.AktualisiereEintrag(myM));
//			//Alle aus der DB auslesen:			
//			myMeldgMenge = (Cdv_Meldung[])myWrapper.LadeAusDerDB();
//			foreach (Cdv_Meldung m in myMeldgMenge)
//			{
//				Console.WriteLine("ID: "+ m.ID+" Absender: "+m.Absender+" text: "+m.Text+" Kategorie:"+m.Kategorie.ToString()+"istuebermittelt: "+m.IstUebermittelt);
//			}
//	
			#endregion
			#region testing Termin
//			Cdv_Termin myTermin = new Cdv_Termin();		
//			Cdv_Erinnerung myErinnerung = new Cdv_Erinnerung();
//			myErinnerung.Erinnerungstext = "Olley";
//			myErinnerung.IstWarnmeldung = true;
//			myTermin.Erinnerung = myErinnerung;
//			myTermin.Betreff ="Mein Termin";
//			myTermin.ZeitVon = DateTime.Now;
//			myTermin.ZeitBis = DateTime.Parse("04.04.2005");
//			myTermin.ErstelltVonBenutzerID = 6;
//			myTermin.ErstelltFuerBenutzerID = 8;
//			myTermin.IstWichtig = true;
//			Console.WriteLine(myWrapper.NeuerEintrag(myTermin));
//			Cdv_Termin myTermin = new Cdv_Termin();	
//			Cdv_Erinnerung myErinnerung = new Cdv_Erinnerung();
//			myErinnerung.Erinnerungstext = "Olley Erinnerung";
//			myErinnerung.Zeitpunkt = DateTime.Parse("05.05.2005");
//			myErinnerung.IstWarnmeldung = true;
//			myTermin.Erinnerung = myErinnerung;		
//			myTermin.ID = 280;
//			myTermin.Betreff ="Mein aktuellisierter Termin. Olley";
//			myTermin.ZeitVon = DateTime.Now;
//			myTermin.ZeitBis = DateTime.Parse("04.04.2005");
//			myTermin.ErstelltVonBenutzerID = 6;
//			myTermin.ErstelltFuerBenutzerID = 8;			
//			Console.WriteLine(myWrapper.AktualisiereEintrag(myTermin));
//			Cdv_Termin[] myTerminMenge = (Cdv_Termin[]) myWrapper.LadeAusDerDB();
//			foreach(Cdv_Termin t in myTerminMenge)
//				Console.WriteLine("ID: "+t.ID+" Betreff: "+t.Betreff+" Von: "+t.ZeitVon+" Bis: "+t.ZeitBis+" Von: " +t.ErstelltVonBenutzerID+" Für: "+t.ErstelltFuerBenutzerID+" wichtig: "+t.IstWichtig);


			#endregion
			#region testing Erinnerung
//			Cdv_Erinnerung myE = new Cdv_Erinnerung();
//			myE.Erinnerungstext = "Ich bin der Erinnerungstext";
//			myE.IstWarnmeldung = true;
//			myE.TerminID = 6;
//			myE.Zeitpunkt = DateTime.Now;
//			Console.WriteLine(myWrapper.NeuerEintrag(myE));

//			Cdv_Erinnerung myE = new Cdv_Erinnerung();
//			myE.ID = 2;
//			myE.Erinnerungstext = "Ich bin der aktualisierte Erinnerungstext";
//			myE.IstWarnmeldung = true;
//			myE.TerminID = 7;
//			myE.Zeitpunkt = DateTime.Now;
//			Console.WriteLine(myWrapper.AktualisiereEintrag(myE));
//
//			Cdv_Erinnerung[] myEMenge = (Cdv_Erinnerung[]) myWrapper.LadeAusDerDB();
//			foreach( Cdv_Erinnerung eri in myEMenge)
//				Console.WriteLine("ID:"+eri.ID+" Text: "+eri.Erinnerungstext+"TerminID: "+eri.TerminID+" Zeitpunkt: "+eri.Zeitpunkt+" IstWarnmeldung: "+eri.IstWarnmeldung);
			#endregion
			#region testing Erkundungsergebnis
			//Insert
//			Cdv_Erkundungsergebnis erkerg = new Cdv_Erkundungsergebnis();
//			erkerg.Erkunder = "alexG";
//			erkerg.EinsatzschwerpunkID = 1;
//			erkerg.
//			erkerg.Erkundungsobjekt.Bauart = Tdv_Bauart.massivhaus;
//			Console.WriteLine(myWrapper.NeuerEintrag(erkerg));
			#endregion
			#region testing Material
//			Insert
//			Cdv_Material myMat = new Cdv_Material();
//			myMat.EigentuemerKraftID = 6;
//			myMat.AktuellerBesitzerKraftID = 7;
//			myMat.Bezeichnung = "das ist das materal";
//			Console.WriteLine(myWrapper.NeuerEintrag(myMat));
//
//			Update
//			myMat = new Cdv_Material();
//			myMat.ID = 10;
//			myMat.EigentuemerKraftID = 6;
//			myMat.AktuellerBesitzerKraftID = 6;
//			myMat.Menge = 2;
//			myMat.Bezeichnung = "Das ist das Materal";
//			Console.WriteLine(myWrapper.AktualisiereEintrag(myMat));
//
//			lade aus DB
//			Cdv_Material[] myMatMenge = new Cdv_Material[10];
//			myMatMenge = (Cdv_Material[]) myWrapper.LadeAusDerDB();
//			foreach(Cdv_Material m in myMatMenge)
//			{	Console.WriteLine("ID: "+m.ID+ " Bezeichnung: "+m.Bezeichnung+" Eigentümer: "+m.EigentuemerKraftID+" Besitzer: "+m.AktuellerBesitzerKraftID); }			
			#endregion
			#region testing Verbauchsgueter
			//Insert
			//			Cdv_Verbrauchsgut myV = new Cdv_Verbrauchsgut();
			//			myV.Bezeichnung ="donot";
			//			myV.Art = "Schoko";
			//			myV.Lagerort = "Tasche recht";
			//			myV.Menge = 200f;
			//			myV.SpaetesterWiederbeschaffungszeitpunk = DateTime.Now;
			//			Console.WriteLine(myWrapper.NeuerEintrag(myV));

			//Update
			//			Cdv_Verbrauchsgut myV = new Cdv_Verbrauchsgut();
			//			myV.ID = 15;
			//			myV.Bezeichnung ="ich bin aktualisiert";
			//			myV.Art = "benzin";
			//			myV.Lagerort = "Halle 2";
			//			myV.Menge = 2.2f;
			//			myV.SpaetesterWiederbeschaffungszeitpunk= DateTime.Now;			
			//			Console.WriteLine(myWrapper.AktualisiereEintrag(myV));

			//Lade aus DB
			//			Cdv_Verbrauchsgut[] myVMenge = (Cdv_Verbrauchsgut[]) myWrapper.LadeAusDerDB();
			//			foreach( Cdv_Verbrauchsgut v in myVMenge)
			//				Console.WriteLine("ID:"+v.ID+" Bezeichnung: "+v.Bezeichnung+"\nMenge: "+v.Menge+" Lagerort: "+v.Lagerort+" Art: "+ v.Art+"\nsWBZ: "+v.SpaetesterWiederbeschaffungszeitpunk);

			#endregion
			#region testing KFZ
			//Insert
//			Cdv_KFZ myKfz = new Cdv_KFZ();
//			myKfz.Kraeftestatus = 0;
//			myKfz.Einsatzbetriebsstunden = 10000;
//			myKfz.EinsatzKm = 400000;
//			myKfz.EinsatzschwerpunktID = 20;
//			myKfz.ModulID = 17;
//			myKfz.FahrerHelferID = 2;
//			myKfz.Funkrufname = "Welt!";
//			myKfz.Kennzeichen = "THW-83171";
//
//			Console.WriteLine(myWrapper.NeuerEintrag(myKfz));						
						
//			//Update
//			Cdv_KFZ myKfz = new Cdv_KFZ();
//			myKfz.ID = 28;
//			myKfz.Funkrufname = "Aktualisierter Funkrufname";
//			myKfz.Kennzeichen = "wurde geklaut";
//			myKfz.KfzTyp = "Kampfpanzer Leopard 2A6 mit Klimaanlage";
//			myKfz.FahrerHelferID = 1;
//					
//			Console.WriteLine(myWrapper.AktualisiereEintrag(myKfz));
//
//			//Lade aus DB
//			Cdv_KFZ[] myVMenge = (Cdv_KFZ[]) myWrapper.LadeAusDerDB();
//			foreach( Cdv_KFZ k in myVMenge)
//				Console.WriteLine("ID:"+k.ID+" Typ: "+k.KfzTyp+" Status: "+k.Kraeftestatus+"\nFahrerID: "+k.FahrerHelferID+" EinsatzKm: "+k.EinsatzKm+" Betriebs_h: "+ k.Einsatzbetriebsstunden+"\nFunkrufname: "+k.Funkrufname);

			#endregion
			#region testing einheit
			// INSERT
//			Cdv_Einheit myE = new Cdv_Einheit();
//			myE.Kraeftestatus = Tdv_Kraeftestatus.imEinsatz;
//			myE.Name = "Huettes 3. Einheit";
//			myE.Funkrufname = "BravoSierra";
//			myE.SollStaerke = 30;
//			myE.IstStaerke = 35;
//			myE.EinheitenfuehrerHelferID = 1;
//			myE.StellvertreterHelferID = 2;
//			myE.Betriebsverbrauch = "3liter/h";
//			myE.EinsatzschwerpunktID = 0;
//			myE.Erreichbarkeit = "01749715371";
//			// Fehlendes Material
//			myE.FehlendesMaterialIDMenge = new Int32[3];
//			myE.FehlendesMaterialIDMenge[0] = 9;
//			myE.FehlendesMaterialIDMenge[1] = 10;
//			myE.FehlendesMaterialIDMenge[2] = 11;
//			// Helfermenge
//			myE.HelferIDMenge = new Int32[2];
//			myE.HelferIDMenge[0] = 1;
//			myE.HelferIDMenge[1] = 2;
//			// Material
//			myE.MaterialIDMenge = new Int32[2];
//			myE.MaterialIDMenge[0] = 14;
//			myE.MaterialIDMenge[1] = 15;
//
//			Console.WriteLine(myWrapper.NeuerEintrag(myE));

//
//			// UPDATE
//			Cdv_Einheit myE = new Cdv_Einheit();
//			myE.ID = 120;
//			myE.Kraeftestatus = Tdv_Kraeftestatus.imEinsatz;
//			myE.Name = "Huettes TopEinheit";
//			myE.Funkrufname = "BravoSierra";
//			myE.SollStaerke = 200;
//			myE.IstStaerke = 500;
//			myE.EinheitenfuehrerHelferID = 1;
//			myE.StellvertreterHelferID = 2;
//			myE.Betriebsverbrauch = "5literh";
//			myE.EinsatzschwerpunktID = 0;
//			myE.Erreichbarkeit = "01749715371";
//			// Fehlendes Material
//			myE.FehlendesMaterialIDMenge = new Int32[3];
//			myE.FehlendesMaterialIDMenge[0] = 14;
//			myE.FehlendesMaterialIDMenge[1] = 10;
//			myE.FehlendesMaterialIDMenge[2] = 11;
//			// Helfermenge
//			myE.HelferIDMenge = new Int32[2];
//			myE.HelferIDMenge[0] = 1;
//			myE.HelferIDMenge[1] = 2;
//			// Material
//			myE.MaterialIDMenge = new Int32[2];
//			myE.MaterialIDMenge[0] = 14;
//			myE.MaterialIDMenge[1] = 15;
//
//			Console.WriteLine(myWrapper.AktualisiereEintrag(myE));
//
			// SELECT
//			Cdv_Einheit[] myEMenge = (Cdv_Einheit[]) myWrapper.LadeAusDerDB();
//			foreach(Cdv_Einheit e in myEMenge)
//			{
//				Console.WriteLine("ID: "+e.ID+" Name: "+e.Name+" SollSt: "+e.SollStaerke+" IstSt: "+e.IstStaerke+" EFuehrer: "+e.EinheitenfuehrerHelferID+"");
//				foreach(int j in e.HelferIDMenge)
//					Console.WriteLine(" Helfer: "+j);
//				Console.WriteLine("\n");
//			}
//					
			

			#endregion
			#region testing Materialuebergabe
//			// INSERT
//			Cdv_Materialuebergabe myMU = new Cdv_Materialuebergabe();
//			myMU.AllgBemerkungen.Text = "Bemerkung";
//			myMU.AllgBemerkungen.Autor = "Autor";
//			myMU.Datum = DateTime.Now;
//			myMU.EmpfaengerKraftID = 2;
//			myMU.VerleiherKraftID = 120;
//			myMU.UebergabepostenGutID = 14;
//			
//			Console.WriteLine(myWrapper.NeuerEintrag(myMU));
//
//			// UPDATE
//			myMU = new Cdv_Materialuebergabe();
//			myMU.AllgBemerkungen.Text = "halloasdöfkjie";
//			myMU.AllgBemerkungen.Autor = "otto";
//			myMU.Datum = DateTime.Now;
//			myMU.EmpfaengerKraftID = 1;
//			myMU.VerleiherKraftID = 120;	
//			myMU.UebergabepostenGutID = 16;
//			myMU.ID = 229;
//
//			Console.WriteLine(myWrapper.AktualisiereEintrag(myMU));
////
//			// SELECT
//			Cdv_Materialuebergabe[] myMUMenge = (Cdv_Materialuebergabe[])myWrapper.LadeAusDerDB();
//			foreach(Cdv_Materialuebergabe m in myMUMenge)
//				Console.WriteLine("ID: "+m.ID+" Datum: "+m.Datum.ToString()+" Empf: "+m.EmpfaengerKraftID+"\n"
//					+"Verl: "+m.VerleiherKraftID+" Text: "+m.AllgBemerkungen.Text+" Gut: "+m.UebergabepostenGutID);

			#endregion			
			#region testing Meldungen & Erkundungsergebnisse
			
			//Insert eines Erkundungsergebnisses
			//			Cdv_Erkundungsergebnis erkerg = new Cdv_Erkundungsergebnis();
			//			erkerg.Text = "Alexander Grosskopf";
			//			erkerg.Absender = "steini";
			//			erkerg.BearbeiterBenutzerID = 8;
			//			erkerg.Abfassungsdatum = DateTime.Now;
			//			erkerg.Erkundungsobjekt.Bezeichnung ="EO_Bezeichnung";
			//			erkerg.Erkundungsobjekt.Erkundungsdatum = DateTime.Now;
			//			erkerg.Erkundungsobjekt.Haustyp = "Haustyp string";
			//			erkerg.Erkundungsobjekt.Bauart = Tdv_Bauart.fertighaus;
			//			erkerg.Erkundungsobjekt.Heizung = "Oelheizung";
			//			erkerg.Erkundungsobjekt.Abwasserentsorgung = true;
			//			erkerg.Erkundungsobjekt.Schaeden.Autor = "alex";
			//			erkerg.Erkundungsobjekt.Schaeden.Text = "michal";
			//			erkerg.Erkundungsobjekt.Anschrift.Strasse = "Strasse";
			//			erkerg.Erkundungsobjekt.Anschrift.Ort = "ort";
			//			erkerg.Erkundungsobjekt.Keller.Vorhanden = true;
			//			erkerg.Erkundungsobjekt.Keller.Prozentsatz = 33;
			//			erkerg.EinsatzschwerpunkID = 20;
			//			erkerg.EmpfaengerMengeKraftID = new int[3];
			//			erkerg.EmpfaengerMengeKraftID[0] = 20;
			//			erkerg.EmpfaengerMengeKraftID[1] = 21;
			//			erkerg.EmpfaengerMengeKraftID[2] = 22;
			//			Console.WriteLine(myWrapper.NeuerEintrag(erkerg));

			//Insert einer Meldung
			//			Cdv_Meldung meldg = new Cdv_Meldung();
			//			meldg.Text = "MeldungstextXXX";
			//			meldg.Absender = "AbsenderXXX";
			//			meldg.BearbeiterBenutzerID = 8;
			//			meldg.Abfassungsdatum = DateTime.Now;
			//			meldg.Uebermittlungsdatum = DateTime.Now;
			//			meldg.IstFreieMeldung = true;
			//			meldg.IstUebermittelt = true;
			//			meldg.EmpfaengerMengeKraftID = new int[3];
			//			meldg.EmpfaengerMengeKraftID[0] = 99;
			//			meldg.EmpfaengerMengeKraftID[1] = 98;
			//			meldg.EmpfaengerMengeKraftID[2] = 97;
			//			Console.WriteLine(myWrapper.NeuerEintrag(meldg));

			//Update einer Meldung
			//			Cdv_Meldung meldg = new Cdv_Meldung();
			//			meldg.ID = 253;
			//			meldg.Text = "update_Meldungstext";
			//			meldg.Absender = "update Absender";
			//			meldg.BearbeiterBenutzerID = 183;
			//			meldg.Abfassungsdatum = DateTime.Now;
			//			meldg.Uebermittlungsdatum = DateTime.Now;
			//			meldg.IstFreieMeldung = true;
			//			meldg.IstUebermittelt = true;
			//			meldg.EmpfaengerMengeKraftID = new int[3];
			//			meldg.EmpfaengerMengeKraftID[0] = 2;
			//			meldg.EmpfaengerMengeKraftID[1] = 21;			
			//			Console.WriteLine(myWrapper.AktualisiereEintrag(meldg));

			//Update eines Erkundungsergebnisses
			//			Cdv_Erkundungsergebnis erkerg = new Cdv_Erkundungsergebnis();
			//			erkerg.ID = 234;
			//			erkerg.Text = "Alexander Grosskopf";
			//			erkerg.Absender = "steini";
			//			erkerg.BearbeiterBenutzerID = 8;
			//			erkerg.Abfassungsdatum = DateTime.Now;
			//			erkerg.Erkundungsobjekt.Bezeichnung ="EO_Bezeichnung";
			//			erkerg.Erkundungsobjekt.Erkundungsdatum = DateTime.Now;
			//			erkerg.Erkundungsobjekt.Haustyp = "Haustyp string";
			//			erkerg.Erkundungsobjekt.Bauart = Tdv_Bauart.fertighaus;
			//			erkerg.Erkundungsobjekt.Heizung = "Oelheizung";
			//			erkerg.Erkundungsobjekt.Abwasserentsorgung = true;
			//			erkerg.Erkundungsobjekt.Schaeden.Autor = "alex";
			//			erkerg.Erkundungsobjekt.Schaeden.Text = "michal";
			//			erkerg.Erkundungsobjekt.Anschrift.Strasse = "Strasse";
			//			erkerg.Erkundungsobjekt.Anschrift.Ort = "ort";
			//			erkerg.Erkundungsobjekt.Keller.Vorhanden = true;
			//			erkerg.Erkundungsobjekt.Keller.Prozentsatz = 33;
			//			erkerg.EinsatzschwerpunkID = 20;
			//			erkerg.EmpfaengerMengeKraftID = new int[3];
			//			erkerg.EmpfaengerMengeKraftID[0] = 88;
			//			erkerg.EmpfaengerMengeKraftID[1] = 87;
			//			erkerg.EmpfaengerMengeKraftID[2] = 86;
			//			Console.WriteLine(myWrapper.AktualisiereEintrag(erkerg));


			//LadeAusDB()
			//			IPelsObject[] pelsOBjecte = myWrapper.LadeAusDerDB();
			//			foreach(Cdv_Meldung meldg in pelsOBjecte)
			//			{
			//				Console.WriteLine("ID = " +meldg.ID);
			//				Console.WriteLine("Abfassungsdatum = " +meldg.Abfassungsdatum);
			//				Console.WriteLine("Uebermittlungsdatum = " +meldg.Uebermittlungsdatum);
			//				Console.WriteLine("Absender: "+ meldg.Absender);
			//				Console.WriteLine("Text: "+ meldg.Text);
			//				Console.WriteLine("uebermittlungsart: "+ meldg.Uebermittlungsart.ToString ());
			//
			//				if(meldg.EmpfaengerMengeKraftID != null)
			//				{
			//					foreach(int e in meldg.EmpfaengerMengeKraftID)
			//					{
			//						Console.WriteLine("EmpfaengerKraftID = " +e );
			//					}
			//				}
			//
			//				if(meldg is Cdv_Erkundungsergebnis)
			//				{
			//					Console.WriteLine("Ich bin das Erkundungsergebnis ");										
			//					Console.WriteLine("BearbeiterBenutzerID: "+ ((Cdv_Erkundungsergebnis) meldg).BearbeiterBenutzerID);					
			//					Console.WriteLine("EO_Bezeichnung: "+ ((Cdv_Erkundungsergebnis) meldg).Erkundungsobjekt.Bezeichnung);
			//					Console.WriteLine("EO_Erkundungsdatum: "+ ((Cdv_Erkundungsergebnis) meldg).Erkundungsobjekt.Erkundungsdatum);
			//					Console.WriteLine("EO_Haustyp: "+ ((Cdv_Erkundungsergebnis) meldg).Erkundungsobjekt.Haustyp);
			//					Console.WriteLine("EO_Bauart: "+ ((Cdv_Erkundungsergebnis) meldg).Erkundungsobjekt.Bauart.ToString());
			//					Console.WriteLine("EO_Heizung: "+ ((Cdv_Erkundungsergebnis) meldg).Erkundungsobjekt.Heizung);
			//					Console.WriteLine("EO_Schaeden:   "+ ((Cdv_Erkundungsergebnis) meldg).Erkundungsobjekt.Schaeden.Autor+" -> "+((Cdv_Erkundungsergebnis) meldg).Erkundungsobjekt.Schaeden.Text);
			//					Console.WriteLine("EO_Anschrift: " + ((Cdv_Erkundungsergebnis) meldg).Erkundungsobjekt.Anschrift.Strasse+" "+((Cdv_Erkundungsergebnis) meldg).Erkundungsobjekt.Anschrift.Hausnummer);
			//					Console.WriteLine("\t" + ((Cdv_Erkundungsergebnis) meldg).Erkundungsobjekt.Anschrift.PLZ+" "+((Cdv_Erkundungsergebnis) meldg).Erkundungsobjekt.Anschrift.Ort);
			//					Console.WriteLine ("Keller: "+((Cdv_Erkundungsergebnis) meldg).Erkundungsobjekt.Keller.Vorhanden+" ("+((Cdv_Erkundungsergebnis) meldg).Erkundungsobjekt.Keller.Prozentsatz+")");
			//					Console.WriteLine ("EinsatzID: " +((Cdv_Erkundungsergebnis) meldg).EinsatzschwerpunkID);
			//				}
			//			}
			#endregion
			#region testing EtbEintrag
			//Insert
//			int anzahl = 10;
//			Cdv_EtbEintrag[] etbE_Menge = new Cdv_EtbEintrag[anzahl];
//			for (int i=0; i<10; i++)
//			{
//				etbE_Menge[i]= new Cdv_EtbEintrag("alex"+i.ToString(),DateTime.Now,"Hilfe!");				
//				etbE_Menge[i].ID =  myWrapper.NeuerEintrag(etbE_Menge[i]);
//				Console.WriteLine("neuer EtbEintrag. ID = " +etbE_Menge[i].ID);
//			}
//			
//			Cdv_Systemereignis[] syserg_Menge = new Cdv_Systemereignis[anzahl];
//			for (int i=0; i<10; i++)
//			{
//				syserg_Menge[i]= new Cdv_Systemereignis("alex"+i.ToString(),DateTime.Now,"Hilfe!", (Tdv_SystemereignisArt)i, true);								
//				syserg_Menge[i].ID =  myWrapper.NeuerEintrag(syserg_Menge[i]);
//				Console.WriteLine("neues Systemereignis. ID = " +syserg_Menge[i].ID);
//			}
//			
//			//Auslesen aus der DB
//			IPelsObject[] temp = myWrapper.LadeAusDerDB();
//			foreach(Cdv_EtbEintrag etbE in temp)
//			{
//				if(etbE is Cdv_Systemereignis)
//					Console.WriteLine("Ich bin ein Systemereignis");
//				else
//					Console.WriteLine("Ich bin ein Zusatzeintrag");
//				Console.WriteLine("ID: "+etbE.ID);
//				Console.WriteLine("Erstelldatum: "+etbE.ErstellDatum);
//				Console.WriteLine("Beschreibung: "+etbE.Beschreibung);
//				Console.WriteLine("BenutzerName: "+etbE.Benutzername);
//				if(etbE is Cdv_Systemereignis)
//				{
//					Console.WriteLine("SystemEreignisArt: "+((Cdv_Systemereignis) etbE).Systemereignisart);
//					Console.WriteLine("erscheintInEtb: "+((Cdv_Systemereignis) etbE).ErscheintInEtb);
//				}
//			
//			}
//			
//
//			
//			Cdv_EtbEintrag myEtbE = new Cdv_EtbEintrag();
//			myEtbE.Benutzername = "alexG";
//			myEtbE.Beschreibung = "Beschreibung des Eintrags";
//			myEtbE.ErstellDatum = DateTime.Now;
//			anzahl = 3;
//			myEtbE.EintragsKommentarMenge = new Cdv_EtbEintragKommentar[anzahl];
//			for (int i=0; i< anzahl; i++)
//			{
//				myEtbE.EintragsKommentarMenge[i] = new Cdv_EtbEintragKommentar();
//				myEtbE.EintragsKommentarMenge[i].ErscheintInEtb = true;
//				myEtbE.EintragsKommentarMenge[i].ErstellDatum = DateTime.Now;
//				myEtbE.EintragsKommentarMenge[i].Kommentar.Autor = "alexG_33";
//				myEtbE.EintragsKommentarMenge[i].Kommentar.Text = "kommentartext";				
//			}
//			Console.WriteLine(myWrapper.NeuerEintrag(myEtbE));
//
//			//Update
//			Cdv_Systemereignis syserg = new Cdv_Systemereignis("schuppe",DateTime.Now, "hallo", Tdv_SystemereignisArt.sonstiges,true);
//			syserg.ID = 1310;
//
//			Console.WriteLine(myWrapper.AktualisiereEintrag(syserg));	

			#endregion
			#region testing EtbEintragKommentar

//			//Insert
//			Cdv_EtbEintragKommentar etbK = new Cdv_EtbEintragKommentar(1, DateTime.Now, true);
//			etbK.Kommentar.Autor = "AlexG";
//			etbK.Kommentar.Text = "Text des Kommentars";
//			Console.WriteLine(myWrapper.NeuerEintrag(etbK));
//
//			//Update
//			etbK = new Cdv_EtbEintragKommentar(1, DateTime.Now, true);
//			etbK.ID = 1355;
//			etbK.Kommentar.Autor = "Schuppe";
//			etbK.Kommentar.Text = "Text des Kommentars";
//			etbK.ErscheintInEtb = false;
//			Console.WriteLine(myWrapper.AktualisiereEintrag(etbK));
//
//			//Lade aus der DB
//			IPelsObject[] temp = myWrapper.LadeAusDerDB();
//			foreach(Cdv_EtbEintragKommentar etbK2 in temp)
//			{
//				Console.WriteLine("\nID:\t\t"+etbK2.ID);
//				Console.WriteLine("EtbEintragsID:\t\t" +etbK2.EtbEintragID);
//				Console.WriteLine("Erstelldatum:\t\t"+etbK2.ErstellDatum);			
//				Console.WriteLine("ErscheintInEtb:\t\t"+etbK2.ErscheintInEtb);
//				Console.WriteLine("Kommentar_Autor:\t\t"+etbK2.Kommentar.Autor);
//				Console.WriteLine("Kommentar_Text:\t\t"+etbK2.Kommentar.Text);
//
//			}
			#endregion
			#region testing DB auslastung am Beispiel Cdv_Helfer

//			Cdv_Helfer myHelfer = new Cdv_Helfer();
//			myHelfer.Personendaten.GebDatum= DateTime.Now;
//			myHelfer.Personendaten.Anschrift.PLZ = "12559";
//			DateTime startzeit = DateTime.Now;
//			
//			for(int i=0; i<1000; i++)
//			{
//				DateTime vorher = DateTime.Now;
//				TimeSpan zeitspanne;
//				myHelfer.Personendaten.Name = "Helfername "+i.ToString();
//				myHelfer.Personendaten.Vorname = "Helfervorname " +i.ToString();
//				myHelfer.Personendaten.GebDatum.AddYears(i);
//				myHelfer.Personendaten.ZusatzInfo = "Zusatzinfo "+i.ToString();
//				myHelfer.EinsatzschwerpunktID = i;				
//				myHelfer.Erreichbarkeit = "Tel: 0049 0 "+i.ToString();	
//
//				zeitspanne = DateTime.Now - vorher;
//				Console.WriteLine(myWrapper.NeuerEintrag(myHelfer));
//				Console.WriteLine(zeitspanne.Milliseconds);
//
//			}
//			Console.WriteLine("Gesamtlaufzeit: "+ ((TimeSpan)(startzeit-DateTime.Now)).Milliseconds);


			#endregion

			
			#region Lasttest 1000
			
			//Hinweis: das einfügen von 2000 Datensätzen in die DB dauerte satte 50 min !!

//			Cdv_Helfer ref_Helfer = new Cdv_Helfer("test1","test1",Tdv_Kraeftestatus.Angefordert,Tdv_Helferstatus.AktiverHelfer);
//			ref_Helfer.ID = (Cdv_HelferWrapper.HoleInstanz()).NeuerEintrag(ref_Helfer);			
//
//			Cdv_Benutzer ref_benutzer = new Cdv_Benutzer("alexg",Tdv_Systemrolle.Zugtruppführer);
//			ref_benutzer.ID = (Cdv_BenutzerWrapper.HoleInstanz()).NeuerEintrag(ref_benutzer);
//
//			Cdv_Einsatz ref_einsatz = new Cdv_Einsatz("Ich bin der Referenzeinsatz", "HPI", DateTime.Now);			
//			ref_einsatz.ID = (Cdv_EinsatzWrapper.HoleInstanz()).NeuerEintrag(ref_einsatz);
//
//			int anzahl = 1000;
//			//OVs
//
//			Console.WriteLine(DateTime.Now.ToLongTimeString());
//			for(int i =1; i<=anzahl; i++)
//			{
//				
//				(Cdv_OrtsverbandWrapper.HoleInstanz()).NeuerEintrag(new Cdv_Ortsverband("Ortsverband"+i));
//				(Cdv_EinsatzWrapper.HoleInstanz()).NeuerEintrag(new Cdv_Einsatz("Einsatz"+i,"HPI"+anzahl.ToString(),DateTime.Now));
//				(Cdv_BenutzerWrapper.HoleInstanz()).NeuerEintrag(new Cdv_Benutzer("Benutzername"+i,Tdv_Systemrolle.Führungsgehilfe));
//				(Cdv_AuftragWrapper.HoleInstanz()).NeuerEintrag(new Cdv_Auftrag("Auftragstext"+i,DateTime.Now,"benutzer"+i, Tdv_Uebermittlungsart.Kurier,true,true,ref_benutzer.ID));
//				int espID = (Cdv_EinsatzschwerpunktWrapper.HoleInstanz()).NeuerEintrag(new Cdv_Einsatzschwerpunkt("Ortsverband"+i, ref_einsatz.ID));
//				(Cdv_MeldungWrapper.HoleInstanz()).NeuerEintrag(new Cdv_Meldung("Meldungstext"+i,"absendername"+i,Tdv_Uebermittlungsart.Kurier, Tdv_MeldungsKategorie.Staatsnot, true, ref_benutzer.ID));
//				int terminID = (Cdv_TerminWrapper.HoleInstanz()).NeuerEintrag(new Cdv_Termin("Betreff"+i, ref_benutzer.ID, ref_benutzer.ID, true));
//				(Cdv_ErinnerungWrapper.HoleInstanz()).NeuerEintrag(new Cdv_Erinnerung(terminID, ((DateTime) DateTime.Now).AddMinutes((Convert.ToDouble(i)))));
//				int helferID = (Cdv_HelferWrapper.HoleInstanz()).NeuerEintrag(new Cdv_Helfer("Helfername"+i,"Helfervorname"+i, Tdv_Kraeftestatus.Ruht, Tdv_Helferstatus.AltHelfer));
//				(Cdv_KFZWrapper.HoleInstanz()).NeuerEintrag(new Cdv_KFZ("B-MW "+i,"Funkrufname"+i, Tdv_Kraeftestatus.ImEinsatz));
//				(Cdv_EinheitWrapper.HoleInstanz()).NeuerEintrag(new Cdv_Einheit("Einheit Nr. "+i, Tdv_Kraeftestatus.Angefordert, "Funk E"+i, ref_Helfer.ID, helferID,anzahl, i));
//				int GutID1 =(Cdv_MaterialWrapper.HoleInstanz()).NeuerEintrag(new Cdv_Material("Schaufel", helferID));
//				int GutID2 =(Cdv_VerbrauchsgutWrapper.HoleInstanz()).NeuerEintrag(new Cdv_Verbrauchsgut("Benzin "+i+"L"));
//				int etbE_ID = (Cdv_EtbEintragWrapper.HoleInstanz()).NeuerEintrag(new Cdv_EtbEintrag("Benutzer"+i, DateTime.Now,"Ich bin die Beschreibung Nr. "+i));
//				(Cdv_EtbEintragKommentarWrapper.HoleInstanz()).NeuerEintrag(new Cdv_EtbEintragKommentar(etbE_ID,DateTime.Now, true));
//				(Cdv_AnforderungWrapper.HoleInstanz()).NeuerEintrag(new Cdv_Anforderung(GutID1, Tdv_AnforderungsStatus.Neu, helferID, DateTime.Now, false));
//				(Cdv_ModulWrapper.HoleInstanz()).NeuerEintrag(new Cdv_Modul("Modul Nr. "+i));
//				(Cdv_MaterialuebergabeWrapper.HoleInstanz()).NeuerEintrag(new Cdv_Materialuebergabe(DateTime.Now, ref_Helfer.ID, helferID, GutID2, i));
//				
//				//Damit keiner denkt es passiert nix
//				Console.Write(".");
//
//			}
//			Console.WriteLine(DateTime.Now.ToLongTimeString());


			#endregion
   						
			
			Console.WriteLine("done");			
			Console.ReadLine();
		}
	}
}
