/*Diese wird benutzt um ein Datenbankschmema beim Anlegen einer neuen datenbank zu Erzeugen zu erzeugen*/
/*project.ELS - alexG - 29.03.2005*/


/* Sequenz die für die Eindeutigkeit aller Datensätze sorgt */
CREATE SEQUENCE "Pelsindex_ID_seq"
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 1
  CACHE 1;
ALTER TABLE "Pelsindex_ID_seq" OWNER TO "UserNameToReplace";

/* Die Reihenfolge der Tabellen darf aufgrund der Constraints nicht verändert werden */
CREATE TABLE "Ortsverbaende"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "Name" text NOT NULL,
  "Ortsbeauftragter" text,
  "Erreichbarkeit" text,
  "Landesverband" text,
  "PLZ" text,
  "Ort" text,
  "Strasse" text,
  "Hausnummer" text,
  "GF_Bereich" text,
  "GF_PLZ" text,
  "GF_Ort" text,
  "GF_Strasse" text,
  "GF_Hausnummer" text,
  CONSTRAINT "Primary_Key" PRIMARY KEY ("ID")
)
WITHOUT OIDS;
ALTER TABLE "Ortsverbaende" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Ortsverbaende" TO "UserNameToReplace";
GRANT ALL ON TABLE "Ortsverbaende" TO public;
COMMENT ON TABLE "Ortsverbaende" IS 'Verwaltet die Ortsverbaende mit dem enpsrechenden Geschäftsführerbereich';
COMMENT ON COLUMN "Ortsverbaende"."GF_Bereich" IS 'GF -> Geschäftsfuehrer';
COMMENT ON COLUMN "Ortsverbaende"."GF_PLZ" IS 'GF -> Geschäftsfuehrer';
COMMENT ON COLUMN "Ortsverbaende"."GF_Ort" IS 'GF -> Geschäftsfuehrer';
COMMENT ON COLUMN "Ortsverbaende"."GF_Strasse" IS 'GF -> Geschäftsfuehrer';
COMMENT ON COLUMN "Ortsverbaende"."GF_Hausnummer" IS 'GF -> Geschäftsfuehrer';

CREATE TABLE "Module"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "Modulname" text NOT NULL,
  "EinsatzschwerpunktID" int4,
  CONSTRAINT "Module_pkey" PRIMARY KEY ("ID")
)
WITHOUT OIDS;
ALTER TABLE "Module" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Module" TO "UserNameToReplace";
GRANT ALL ON TABLE "Module" TO public;
COMMENT ON COLUMN "Module"."EinsatzschwerpunktID" IS 'Fremdschlüssel auf ID der EinsatzschwerpuntkMenge';

CREATE TABLE "EtbEintraege"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "Erstelldatum" timestamp,
  "Benutzername" text,
  "IstSystemereignis" bool,
  "Systemereignisart" int4,
  "ErscheintInEtb" bool DEFAULT true,
  "Beschreibung" text,
  CONSTRAINT "ETBEintraege_pkey" PRIMARY KEY ("ID")
)
WITHOUT OIDS;
ALTER TABLE "EtbEintraege" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "EtbEintraege" TO "UserNameToReplace";
GRANT ALL ON TABLE "EtbEintraege" TO public;

CREATE TABLE "EtbEintragsKommentare"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "EtbEintragID" int4 NOT NULL,
  "Erstelldatum" timestamp NOT NULL,
  "ErscheintInEtb" bool NOT NULL DEFAULT false,
  "Kommentar_Autor" text,
  "Kommentar_Text" text,
  CONSTRAINT "ETBEintragsKommentare_pkey" PRIMARY KEY ("ID"),
  CONSTRAINT "EtbEintraege_ID_fkey" FOREIGN KEY ("EtbEintragID") REFERENCES "EtbEintraege" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITHOUT OIDS;
ALTER TABLE "EtbEintragsKommentare" OWNER TO "UserNameToReplace";

CREATE TABLE "Benutzer"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "Systemrolle" int4 NOT NULL,
  "Benutzername" text NOT NULL,
  CONSTRAINT "Benutzer_pkey" PRIMARY KEY ("ID")
)
WITHOUT OIDS;
ALTER TABLE "Benutzer" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Benutzer" TO "UserNameToReplace";
GRANT ALL ON TABLE "Benutzer" TO public;
COMMENT ON COLUMN "Benutzer"."Systemrolle" IS 'Aufzälung vom Typ Tdv_Systemrolle';

CREATE TABLE "Termine"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "Betreff" text NOT NULL,
  "FuerID" int4 NOT NULL,
  "VonID" int4 NOT NULL,
  "IstWichtig" bool NOT NULL DEFAULT false,
  "ZeitVon" timestamp,
  "ZeitBis" timestamp,
  "WirdErinnert" bool NOT NULL DEFAULT false,
  "IstInToDoListe" bool NOT NULL DEFAULT false,
  CONSTRAINT "Termin_pkey" PRIMARY KEY ("ID"),
  CONSTRAINT "Termin_FuerID_fkey" FOREIGN KEY ("FuerID") REFERENCES "Benutzer" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT,
  CONSTRAINT "Termin_VonID_fkey" FOREIGN KEY ("VonID") REFERENCES "Benutzer" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITHOUT OIDS;
ALTER TABLE "Termine" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Termine" TO "UserNameToReplace";
GRANT ALL ON TABLE "Termine" TO public;
COMMENT ON COLUMN "Termine"."FuerID" IS 'Fremdschlüssel auf ID der \'Benutzer\' Tabelle';
COMMENT ON COLUMN "Termine"."VonID" IS 'Fremdschlüssel auf ID der \'Benutzer\' Tabelle';
COMMENT ON COLUMN "Termine"."ZeitVon" IS 'Beginn des Termins';
COMMENT ON COLUMN "Termine"."ZeitBis" IS 'Ende des Termins';

CREATE TABLE "Erinnerungen"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "TerminID" int4 NOT NULL,
  "Zeitpunkt" timestamp NOT NULL,
  "Text" text,
  CONSTRAINT "Erinnerung_pkey" PRIMARY KEY ("ID"),
  CONSTRAINT "Erinnerung_TerminID_fkey" FOREIGN KEY ("TerminID") REFERENCES "Termine" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITHOUT OIDS;
ALTER TABLE "Erinnerungen" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Erinnerungen" TO "UserNameToReplace";
GRANT ALL ON TABLE "Erinnerungen" TO public;
COMMENT ON TABLE "Erinnerungen" IS 'In dieser Tabelle werden Erinnerungen verwaltet.';

CREATE TABLE "Einsaetze"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "Einsatzort" text NOT NULL,
  "EinsatzVon" timestamp NOT NULL,
  "EinsatzBis" timestamp,
  "Bezeichnung" text NOT NULL,
  "Kostenabrechnung" bool,
  "Erfahrungsbericht" bool,
  "Pressemitteilung" bool,
  "Kostenerstattung" bool,
  "Einsatzbericht" bool,
  "Haftungsfreistellung" bool,
  "IhkBescheinigung" bool,
  "ArtDerHilfeleistung" text,
  "Kommentar_Text" text,
  "Kommentar_Autor" text,
  CONSTRAINT "Einsaetze_pkey" PRIMARY KEY ("ID")
)
WITHOUT OIDS;
ALTER TABLE "Einsaetze" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Einsaetze" TO "UserNameToReplace";
GRANT ALL ON TABLE "Einsaetze" TO public;
COMMENT ON TABLE "Einsaetze" IS 'Hier werden Einsätze verwaltet.';

CREATE TABLE "Einsatzschwerpunkte"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "Bezeichnung" text NOT NULL,
  "Prioritaet" int4,
  "EinsatzleiterHelferID" int4,
  "Lage_Text" text,
  "Lage_Autor" text,
  "EinsatzID" int4 NOT NULL,
  CONSTRAINT "Einsatzschwerpunkte_pkey" PRIMARY KEY ("ID"),
  CONSTRAINT "Einsaetze_fkey" FOREIGN KEY ("EinsatzID") REFERENCES "Einsaetze" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITHOUT OIDS;
ALTER TABLE "Einsatzschwerpunkte" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Einsatzschwerpunkte" TO "UserNameToReplace";
GRANT ALL ON TABLE "Einsatzschwerpunkte" TO public;
COMMENT ON TABLE "Einsatzschwerpunkte" IS 'Verwaltet Informationen zu Einsatzschwerpunkten.';
COMMENT ON COLUMN "Einsatzschwerpunkte"."EinsatzleiterHelferID" IS 'Fremdschlüssel auf ID der Helfer-Tabelle';
COMMENT ON COLUMN "Einsatzschwerpunkte"."EinsatzID" IS 'Fremdschlüssel auf ID der \'Einsaetze\'-Tabelle';

CREATE TABLE "Auftraege"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "Text" text NOT NULL,
  "Abfassungsdatum" timestamp NOT NULL,
  "Uebermittlungsdatum" timestamp,
  "Absender" text NOT NULL,
  "Uebermittlungsart" int4 NOT NULL,
  "IstUebermittelt" bool NOT NULL,
  "BearbeiterID" int4,
  "Ausfuehrungszeitpunkt" timestamp,
  "SpaetesterEZP" timestamp,
  "IstBefehl" bool NOT NULL,
  "WirdNachverfolgt" bool NOT NULL,
  "IstErkundungsbefehl" bool NOT NULL,
  "EB_Befehlsart" int4,
  "EmpfaengerBenutzerID" int4,
  "IstInToDoListe" bool NOT NULL DEFAULT false,
  "LaufendeNummer" int4,
  CONSTRAINT "Auftraege_pkey" PRIMARY KEY ("ID"),
  CONSTRAINT "Auftraege_BearbeiterID" FOREIGN KEY ("BearbeiterID") REFERENCES "Benutzer" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITHOUT OIDS;
ALTER TABLE "Auftraege" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Auftraege" TO "UserNameToReplace";
GRANT ALL ON TABLE "Auftraege" TO public;
COMMENT ON TABLE "Auftraege" IS 'Hier werden Aufträge & Erkundungsbefehle abgebildet.
Die Felder
EB_* werden nur gesetzt, wenn es sich um einen Erkundungsbefehl handelt.
Zusätzlich wird die Information über den Typ noch im Feld \'istErkundungsbefehl\' festgehalten
Zusätzlich wird in der Tabelle \'Empfaenger_Auftrag\' noch die Zuordnung von Aufträgen zu Kräften verwaltet';
COMMENT ON COLUMN "Auftraege"."Uebermittlungsart" IS 'Ist im System eine Aufzählung vom Typ Tdv_Uebermittlungsart';
COMMENT ON COLUMN "Auftraege"."IstUebermittelt" IS 'TRUE, wenn Erkundungsbefehl. FALSE, wenn Auftrag.';
COMMENT ON COLUMN "Auftraege"."BearbeiterID" IS 'Fremdschlüssel auf ID in Benutzer-Tabelle.
Wird auf BearbeiterBenutzerID abgebildet einer Mitteilung abgebildet.';
COMMENT ON COLUMN "Auftraege"."IstBefehl" IS 'Ein Auftrag kann auch ein Befehl sein. Dies ist unabhängig von der Frage ob es sich dabei im einen Erkundungsbefehl handelt.
Bzw. Erkundungsbefehle sollten da imme rein \'True\' haben';
COMMENT ON COLUMN "Auftraege"."WirdNachverfolgt" IS '..klar...';
COMMENT ON COLUMN "Auftraege"."IstErkundungsbefehl" IS 'Gibt an, ob es sich dabei um einen Erkundungsbefehl handelt oder nicht.
true = Erkundungsbefehl
false= Auftrag';
COMMENT ON COLUMN "Auftraege"."EB_Befehlsart" IS 'Aufzählung im System vom Typ Tdv_BefehlsArt';

CREATE TABLE "Meldungen"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "Text" text NOT NULL,
  "Uebermittlungsdatum" timestamp,
  "Absender" text NOT NULL,
  "Uebermittlungsart" int4 NOT NULL,
  "IstUebermittelt" bool DEFAULT false,
  "BearbeiterID" int4,
  "Kategorie" int4 NOT NULL,
  "Abfassungsdatum" timestamp,
  "EO_Bezeichnung" text,
  "EO_Erkundungsdatum" Timestamp,
  "EO_Haustyp" text,
  "EO_Bauart" int4,
  "EO_Heizung" text,
  "EO_Wasserversorgung" bool,
  "EO_Elektroversorgung" bool,
  "EO_Abwasserentsorgung" bool,
  "EO_Anschrift_PLZ" text,
  "EO_Anschrift_Ort" text,
  "EO_Anschrift_Strasse" text,
  "EO_Anschrift_Hausnummer" text,
  "EO_Keller_Vorhanden" bool,
  "EO_Keller_Prozentsatz" int2,
  "EE_EinsatzschwerpunktID" int4,
  "EE_Erkunder" text,
  "IstErkundungsergebnis" bool NOT NULL DEFAULT false,
  "EmpfaengerBenutzerID" int4,
  "IstInToDoListe" bool NOT NULL DEFAULT false,
  "LaufendeNummer" int4,
  CONSTRAINT "Meldung_pkey" PRIMARY KEY ("ID"),
  CONSTRAINT "Meldungen_BearbeiterID_fkey" FOREIGN KEY ("BearbeiterID") REFERENCES "Benutzer" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITHOUT OIDS;
ALTER TABLE "Meldungen" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Meldungen" TO "UserNameToReplace";
GRANT ALL ON TABLE "Meldungen" TO public;
COMMENT ON TABLE "Meldungen" IS 'Verwaltet Meldungen und Erkundungsergebnisse.
Das Flag: \'istErkundungsergebnis\' gibt an, welcher Typ sich hinter dem Eintrag verbirgt.
Wenn es ein Erkundungsergebnis ist, werden alle Felder die mit EE_* oder EO_* beginnen belegt';
COMMENT ON COLUMN "Meldungen"."Uebermittlungsart" IS 'Aufzählung vom Typ \'Tdv_Uebermittlungsart\'';
COMMENT ON COLUMN "Meldungen"."BearbeiterID" IS 'Fremdschlüssel auf ID der \'Benutzer\'-Tabelle';
COMMENT ON COLUMN "Meldungen"."Kategorie" IS 'Aufzählung vom Typ Tdv_Kategorie';
COMMENT ON COLUMN "Meldungen"."EE_EinsatzschwerpunktID" IS 'Fremdschlüssel auf ID in der \'Einsatzschwerpunkte\' Tabelle';
COMMENT ON COLUMN "Meldungen"."EE_Erkunder" IS 'Erkunder eines Erkundungsergebnisses';

CREATE TABLE "Empfaenger_Auftrag"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "AuftragsID" int4 NOT NULL,
  "KraftID" int4 NOT NULL,
  CONSTRAINT "Empfaenger_Auftrag_pkey" PRIMARY KEY ("ID"),
  CONSTRAINT "Empfaenger_Auftrag_AuftragsID_fkey" FOREIGN KEY ("AuftragsID") REFERENCES "Auftraege" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITHOUT OIDS;
ALTER TABLE "Empfaenger_Auftrag" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Empfaenger_Auftrag" TO "UserNameToReplace";
GRANT ALL ON TABLE "Empfaenger_Auftrag" TO public;
COMMENT ON TABLE "Empfaenger_Auftrag" IS 'Hier wird die Zuordnung zwischen Aufträgen und deren Empfängern gehalten.
Empfänger können Helfer, Einheiten oder Kfz\'s sein';
COMMENT ON COLUMN "Empfaenger_Auftrag"."AuftragsID" IS 'Fremdschlüssel auf ID der \'Auftraege\'-Tabelle';
COMMENT ON COLUMN "Empfaenger_Auftrag"."KraftID" IS 'Fremdschlüssel auf ID der Tabellen \'Helfer\', \'Einheiten\' oder \'Kfz\'';

CREATE TABLE "Empfaenger_Meldung"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "MeldungsID" int4 NOT NULL,
  "KraftID" int4 NOT NULL,
  CONSTRAINT "Empfaenger_Meldung_pkey" PRIMARY KEY ("ID"),
  CONSTRAINT "Empfaenger_Meldung_MeldungsID_fkey" FOREIGN KEY ("MeldungsID") REFERENCES "Meldungen" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITHOUT OIDS;

ALTER TABLE "Empfaenger_Meldung" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Empfaenger_Meldung" TO "UserNameToReplace";
GRANT ALL ON TABLE "Empfaenger_Meldung" TO public;
COMMENT ON TABLE "Empfaenger_Meldung" IS 'Hier wird die Zuordnung von Meldungen zu Empfaengern verwaltet.
Empfänger können Helfer, Einheiten oder Kfz\'s sein';
COMMENT ON COLUMN "Empfaenger_Meldung"."MeldungsID" IS 'Fremdschlüssel auf ID der \'Meldungen\' Tabelle';
COMMENT ON COLUMN "Empfaenger_Meldung"."KraftID" IS 'Fremdschlüssel auf die ID der \'Helfer\', \'Einheiten\' oder \'Kfz\' Tabelle';

CREATE TABLE "Helfer"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "Name" text NOT NULL,
  "Vorname" text NOT NULL,
  "GebDatum" date,
  "Zusatzinfo" text,
  "Erreichbarkeit" text,
  "Kommentar_Autor" text,
  "Kommentar_Text" text,
  "Kraeftestatus" int4 NOT NULL,
  "ModulID" int4,
  "PLZ" text,
  "Ort" text,
  "Strasse" text,
  "Hausnummer" text,
  "Position" int4,
  "OVID" int4,
  "LetzteVerpflegung" timestamp,
  "Faehigkeiten" text,
  "Helferstatus" int4 NOT NULL,
  "IstFuehrungskraftVonModul" bool,
  "EinsatzschwerpunkID" int4,
  CONSTRAINT "Helfer_pkey" PRIMARY KEY ("ID")
)
WITHOUT OIDS;
ALTER TABLE "Helfer" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Helfer" TO "UserNameToReplace";
GRANT ALL ON TABLE "Helfer" TO public;
COMMENT ON TABLE "Helfer" IS 'Hier werden alle Helfer gespeichert';
COMMENT ON COLUMN "Helfer"."Name" IS 'Helfer->Person->Name';
COMMENT ON COLUMN "Helfer"."Vorname" IS 'Helfer->Person->Vorname';
COMMENT ON COLUMN "Helfer"."GebDatum" IS 'Helfer->Person->Geburtsdatum';
COMMENT ON COLUMN "Helfer"."Zusatzinfo" IS 'Helfer->Person->Zusatzinfo';
COMMENT ON COLUMN "Helfer"."Erreichbarkeit" IS 'Helfer-> Erreichbarkeit';
COMMENT ON COLUMN "Helfer"."Kommentar_Autor" IS 'Helfer->Kommentar-> Autor
Eingeerbt von Kraft';
COMMENT ON COLUMN "Helfer"."Kommentar_Text" IS 'Helfer->Kommentar-> Text
Eingeerbt von Kraft';
COMMENT ON COLUMN "Helfer"."Kraeftestatus" IS 'Ist eine Aufzählung vom Typ Tdv_Kraeftestatus
Eingeerbt von Kraft';
COMMENT ON COLUMN "Helfer"."ModulID" IS 'Fremdschlüssel auf ID in Tabelle \'Module\'';
COMMENT ON COLUMN "Helfer"."PLZ" IS 'Helfer-> Person-> Anschrift ->PLZ';
COMMENT ON COLUMN "Helfer"."Ort" IS 'Helfer-> Person-> Anschrift ->Ort';
COMMENT ON COLUMN "Helfer"."Strasse" IS 'Helfer-> Person-> Anschrift ->Strasse';
COMMENT ON COLUMN "Helfer"."Hausnummer" IS 'Helfer-> Person-> Anschrift -> Hausnummer';
COMMENT ON COLUMN "Helfer"."Position" IS 'Aufzählung von Tdv_Position (nur Helfer)';
COMMENT ON COLUMN "Helfer"."OVID" IS 'Fremdschlüssel auf ID der \'Ortsverbaende\'-Tabelle';
COMMENT ON COLUMN "Helfer"."Faehigkeiten" IS 'Freier Text als Info über besondere Fähigkeiten eines Helfers';
COMMENT ON COLUMN "Helfer"."Helferstatus" IS 'Aufzählung vom Typ Tdv_Helferstatus';
COMMENT ON COLUMN "Helfer"."IstFuehrungskraftVonModul" IS 'Flag wird gesetzt, wenn der Helfer die Führungskraft in dem Modul ist, dem er zugeordnet wurde';
COMMENT ON COLUMN "Helfer"."EinsatzschwerpunkID" IS 'Fremdschlüssel auf die ID der \'Einsatzschwerpunkte\' Tabelle.

Gibt an, welchem ESP der Helfer momentan zugeordnet ist';

CREATE TABLE "Einheiten"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "Name" text NOT NULL,
  "Kraeftestatus" int4 NOT NULL,
  "ModulID" int4,
  "Funkrufname" text NOT NULL,
  "EinheitsfuehrerID" int4 NOT NULL,
  "StellvertreterID" int4,
  "OVID" int4,
  "Geschaeftsstelle" text,
  "Sollstaerke" int4 NOT NULL,
  "Iststaerke" int4 NOT NULL,
  "Erreichbarkeit" text,
  "Betriebsverbrauch" text,
  "EinsatzschwerpunktID" int4,
  "Kommentar_Autor" text,
  "Kommentar_Text" text,
CONSTRAINT "Einheit_pkey" PRIMARY KEY ("ID"),
  CONSTRAINT "Einheit_EinheitsfuehrerID_fkey" FOREIGN KEY ("EinheitsfuehrerID") REFERENCES "Helfer" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT,
  CONSTRAINT "Einheit_StellvertreterID_fkey" FOREIGN KEY ("StellvertreterID") REFERENCES "Helfer" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT)
WITHOUT OIDS;
ALTER TABLE "Einheiten" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Einheiten" TO "UserNameToReplace";
GRANT ALL ON TABLE "Einheiten" TO public;
COMMENT ON TABLE "Einheiten" IS 'In dieser Tabelle werden Einheiten verwaltet.
Alle Informationen zu einer Einheit ergeben sich erst durch Bezugnahme auch auf die Tabellen

\'Helfer_Einheit\',  \'Material_Einheit\' & \'FehlendesMaterial_Einheit\'';
COMMENT ON COLUMN "Einheiten"."Name" IS 'Name der Einheit';
COMMENT ON COLUMN "Einheiten"."Kraeftestatus" IS 'Aufzählung vom Typ Tdv_Kraeftestatus';
COMMENT ON COLUMN "Einheiten"."ModulID" IS 'Fremdschlüssel auf ID der Tabelle Module.
Gibt an in welchem Modul die Einheit ggf. gerade gebunden/zugeordnet ist';
COMMENT ON COLUMN "Einheiten"."Funkrufname" IS 'Funkrufname der Einheit
';
COMMENT ON COLUMN "Einheiten"."EinheitsfuehrerID" IS 'Fremdschlüssel auf ID der Helfer-Tabelle';
COMMENT ON COLUMN "Einheiten"."StellvertreterID" IS 'Fremdschlüssel auf ID der Helfer-Tabelle';
COMMENT ON COLUMN "Einheiten"."OVID" IS 'Fremdschlüssel auf ID der \'Ortsverbaende\'-Tabelle';
COMMENT ON COLUMN "Einheiten"."Geschaeftsstelle" IS 'Wird auf Einheit.GST abgebildet.';
COMMENT ON COLUMN "Einheiten"."Sollstaerke" IS 'Sollstärke einer Einheit';
COMMENT ON COLUMN "Einheiten"."Iststaerke" IS 'Iststaerke einer Einheit';
COMMENT ON COLUMN "Einheiten"."Betriebsverbrauch" IS 'Vorsicht: ist ein Text';
COMMENT ON COLUMN "Einheiten"."EinsatzschwerpunktID" IS 'Fremdschlüssel auf ID der \'Einsatzschwerpunkte\'-Tabelle';

CREATE TABLE "Gueter"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "Bezeichnung" text NOT NULL,
  "Menge" float4,
  "Lagerort" text,
  "Art" text,
  "IstMaterial" bool NOT NULL DEFAULT false,
  "AktuellerBesitzerID" int4,
  "EigentuemerID" int4,
  "SpaetesterWbzpkt" timestamp,
  CONSTRAINT "Gueter_pkey" PRIMARY KEY ("ID")
)
WITHOUT OIDS;
ALTER TABLE "Gueter" OWNER TO "UserNameToReplace";
COMMENT ON TABLE "Gueter" IS 'In dieser Tabelle werden Güter verwaltet, die konkret entweder Material oder ein Verbrauchsgut sind';
COMMENT ON COLUMN "Gueter"."Bezeichnung" IS 'Bezeichnung eines Gutes';
COMMENT ON COLUMN "Gueter"."Menge" IS 'Menge eines Gutes';
COMMENT ON COLUMN "Gueter"."Lagerort" IS 'Lagerort eines Gutes';
COMMENT ON COLUMN "Gueter"."Art" IS 'Art eines Gutes (achtung: hier string)';
COMMENT ON COLUMN "Gueter"."IstMaterial" IS 'FALSE -> Verbrauchsgut,
TRUE-> Material';
COMMENT ON COLUMN "Gueter"."AktuellerBesitzerID" IS 'Nur bei Material.
Fremdschlüssel auf ID einer Kraft.
Kann also auf die Helfer, Einheit oder Kraft-Tabelle verweisen.';
COMMENT ON COLUMN "Gueter"."EigentuemerID" IS 'Nur bei Material.
Fremdschlüssel auf ID einer Kraft.
Kann also auf die Helfer, Einheit oder Kraft-Tabelle verweisen.';
COMMENT ON COLUMN "Gueter"."SpaetesterWbzpkt" IS 'Nur bei Verbrauchsgut (bzw. \'istMaterial\'=false)';

CREATE TABLE "Kfz"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "Kraeftestatus" int4 NOT NULL,
  "ModulID" int4,
  "Funkrufname" text,
  "Kennzeichen" text NOT NULL,
  "Typ" text,
  "FahrerID" int4,
  "Einsatzkm" float4,
  "Einsatzbetriebsstunden" float4,
  "EinsatzschwerpunktID" int4,
  "Kommentar_Text" text,
  "Kommentar_Autor" text,
  CONSTRAINT "Kfz_pkey" PRIMARY KEY ("ID")
)
WITHOUT OIDS;
ALTER TABLE "Kfz" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Kfz" TO "UserNameToReplace";
GRANT ALL ON TABLE "Kfz" TO public;
COMMENT ON COLUMN "Kfz"."Kommentar_Text" IS 'Kommentar-> Text
Eingeerbt von Kraft';
COMMENT ON COLUMN "Kfz"."Kommentar_Autor" IS 'Kommentar-> Autor
Eingeerbt von Kraft';
COMMENT ON COLUMN "Kfz"."Kraeftestatus" IS 'Ist eine Aufzählung vom Typ Tdv_Kraeftestatus
Eingeerbt von Kraft';
COMMENT ON COLUMN "Kfz"."ModulID" IS 'Fremdschlüssel auf ID in Tabelle \'Module\'';
COMMENT ON COLUMN "Kfz"."Funkrufname" IS 'Funkrufname des Fahrzeugs';
COMMENT ON COLUMN "Kfz"."Typ" IS 'Typ des Fahrzeugs (Achtung: string)';
COMMENT ON COLUMN "Kfz"."FahrerID" IS 'Fremdschlüssel auf ID der \'Helfer\' Tabelle';
COMMENT ON COLUMN "Kfz"."EinsatzschwerpunktID" IS 'Fremdschlüssel auf die ID der \'Einsatzschwerpunkte\' Tabelle.
Gibt an, welchem ESP dieses Fahrzeug momentan zugeordnet ist';

CREATE TABLE "Kfz_Einheit"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "KfzID" int4 NOT NULL,
  "EinheitID" int4,
  CONSTRAINT "Kfz_Einheit_pkey" PRIMARY KEY ("ID"),
  CONSTRAINT "Kfz_Einheit_EinheitID_fkey" FOREIGN KEY ("EinheitID") REFERENCES "Einheiten" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT,
  CONSTRAINT "Kfz_Einheit_KfzID_fkey" FOREIGN KEY ("KfzID") REFERENCES "Kfz" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITHOUT OIDS;
ALTER TABLE "Kfz_Einheit" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Kfz_Einheit" TO "UserNameToReplace";
GRANT ALL ON TABLE "Kfz_Einheit" TO public;
COMMENT ON TABLE "Kfz_Einheit" IS 'Zuordnung der Kfz zu einer Einheit';
COMMENT ON COLUMN "Kfz_Einheit"."KfzID" IS 'Fremdschlüssel auf die ID der \'Kfz\' Tabelle';
COMMENT ON COLUMN "Kfz_Einheit"."EinheitID" IS 'Fremdschlüssel auf die ID der \'Einheiten\' Tabelle';

CREATE TABLE "Anforderungen"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "GutID" int4 NOT NULL,
  "Menge" float4,
  "Status" int4 NOT NULL DEFAULT 1,
  "AnforderndeKraftID" int4 NOT NULL,
  "Anforderungsdatum" timestamp NOT NULL,
  "Zufuehrungsdatum" timestamp,
  "Zweck" text,
  "Kommentar_Autor" text,
  "Kommentar_Text" text,
  "TGA" bool NOT NULL DEFAULT false,
  CONSTRAINT "Anforderungen_GutID_fkey" FOREIGN KEY ("GutID") REFERENCES   "Gueter" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITHOUT OIDS;
ALTER TABLE "Anforderungen" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Anforderungen" TO "UserNameToReplace";
GRANT ALL ON TABLE "Anforderungen" TO public;
COMMENT ON COLUMN "Anforderungen"."GutID" IS 'Ist ein Fremdschlüsse & verweist auf die ID eines Eintrags in der \'Gueter\' Tabelle';
COMMENT ON COLUMN "Anforderungen"."Menge" IS 'Übergebene Menge als Float';
COMMENT ON COLUMN "Anforderungen"."Status" IS 'Wird in pELS alsTdv_Anforderungsstatus gehalten ';
COMMENT ON COLUMN "Anforderungen"."AnforderndeKraftID" IS 'Ist ein Fremdschlüsse & verweist auf die ID eines Eintrags in entweder der Helfer-, Einheiten- oder Kfz-Tabelle
';
COMMENT ON COLUMN "Anforderungen"."TGA" IS 'Ist \'true\' wenn es sich um eine Teilgueteranforderung handelt. Sonst false.

TGA bedeutet, dass es vorher eine Anforderung gab, die nun in 2 Anforderungen gesplittet wurde.';

CREATE TABLE "Material_Einheit"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "MaterialID" int4 NOT NULL,
  "EinheitID" int4 NOT NULL,
  CONSTRAINT "Material_Einheit_pkey" PRIMARY KEY ("ID"),
  CONSTRAINT "Material_Einheit_EinheitID_fkey" FOREIGN KEY ("EinheitID") REFERENCES "Einheiten" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT,
  CONSTRAINT "Material_Einheit_MaterialID_fkey" FOREIGN KEY ("MaterialID") REFERENCES "Gueter" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITHOUT OIDS;
ALTER TABLE "Material_Einheit" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Material_Einheit" TO "UserNameToReplace";
GRANT ALL ON TABLE "Material_Einheit" TO public;
COMMENT ON TABLE "Material_Einheit" IS 'Ist eine Zuordnungstabelle, die einer Einheit ihr Material zuordnet, wobei das Material überraschenderweise in der Gütertabelle zu finden ist!';
COMMENT ON COLUMN "Material_Einheit"."MaterialID" IS 'Fremdschlüssel auf ID der \'Material\'-Tabelle';
COMMENT ON COLUMN "Material_Einheit"."EinheitID" IS 'Fremdschlüssel auf ID der \'Einheiten\'-Tabelle';

CREATE TABLE "Helfer_Einheit"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "HelferID" int4 NOT NULL,
  "EinheitID" int4,
  CONSTRAINT "Helfer_Einheit_pkey" PRIMARY KEY ("ID"),
  CONSTRAINT "Helfer_Einheit_EinheitID_fkey" FOREIGN KEY ("EinheitID") REFERENCES "Einheiten" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT,
  CONSTRAINT "Helfer_Einheit_HelferID_fkey" FOREIGN KEY ("HelferID") REFERENCES "Helfer" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITHOUT OIDS;
ALTER TABLE "Helfer_Einheit" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Helfer_Einheit" TO "UserNameToReplace";
GRANT ALL ON TABLE "Helfer_Einheit" TO public;
COMMENT ON TABLE "Helfer_Einheit" IS 'Zuordnung der Helfer zu einer Einheit';
COMMENT ON COLUMN "Helfer_Einheit"."HelferID" IS 'Fremdschlüssel auf die ID der \'Helfer\' Tabelle';
COMMENT ON COLUMN "Helfer_Einheit"."EinheitID" IS 'Fremdschlüssel auf die ID der \'Einheiten\' Tabelle';

CREATE TABLE "Materialuebergaben"
(
  "ID" int4 NOT NULL DEFAULT nextval('public."Pelsindex_ID_seq"'::text),
  "Datum" timestamp NOT NULL,
  "EmpfaengerID" int4 NOT NULL,
  "VerleiherID" int4 NOT NULL,
  "Menge" int4 NOT NULL,
  "AllgBemerkung_Autor" text,
  "AllgBemerkung_Text" text,
  "GutID" int4 NOT NULL,
  CONSTRAINT "Materialuebergaben_pkey" PRIMARY KEY ("ID"),
  CONSTRAINT "Materialuebergaben_GutID_fkey" FOREIGN KEY ("GutID") REFERENCES "Gueter" ("ID") ON UPDATE RESTRICT ON DELETE RESTRICT
)
WITHOUT OIDS;
ALTER TABLE "Materialuebergaben" OWNER TO "UserNameToReplace";
GRANT ALL ON TABLE "Materialuebergaben" TO "UserNameToReplace";
GRANT ALL ON TABLE "Materialuebergaben" TO public;
COMMENT ON COLUMN "Materialuebergaben"."EmpfaengerID" IS 'Fremdschlüssel auf ID der Tabellen \'Helfer\', \'Einheiten\' oder \'Kfz\'';
COMMENT ON COLUMN "Materialuebergaben"."VerleiherID" IS 'Fremdschlüssel auf ID der Tabellen \'Helfer\', \'Einheiten\' oder \'Kfz\'';
COMMENT ON COLUMN "Materialuebergaben"."AllgBemerkung_Autor" IS 'Kommentar -> Autor';
COMMENT ON COLUMN "Materialuebergaben"."AllgBemerkung_Text" IS 'Kommentar -> Text';
COMMENT ON COLUMN "Materialuebergaben"."GutID" IS 'Fremdschlüssel auf ID der \'Gueter\' -Tabelle';
