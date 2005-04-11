using System;
using System.Windows.Forms;

namespace pELS.GUI.PopUp
{
	#region Dokumentation
	/**
	Erl�uterung:
	PopUps f�r die SBEs
	/// <summary>
	/// Die Klasse PopUp stellt s�mtliche PopUps und Meldungen bereit, die bei der Arbeit
	/// mit dem ELS auftreten k�nnen. Somit wird gew�hrleistet, dass gleiche Meldungen auch
	/// gleich aussehen. Die R�ckgabewerte m�ssen dann in der Logik der einzelnen SBEs aus-
	/// gewertet werden.
	/// </summary>

	erstellt von:	H�tte					am: 18.November.2004	
	review von:								am:
	getestet von:							am:

	aktuelle Version: 0.1

	History:
	- 18.11		- erstellt mit	Zur�cksetzenEingaben();
								SpeichernMitUeberschreiben();
								MeldenVonFalscherEingabe();
								MeldenVonSystemfehler();
								FragenSpeichernBeiBeendenProgramm() 
	
	Hinweise/Bekannte Bugs:
	- 
	**/
				
	#region letzte �nderungen
	/**
	erstellt von: H�tte						am: 18.November.2004
	ge�ndert von:							am:
	ge�ndert von:							am:
	ge�ndert von:							am:
	ge�ndert von:							am:
	ge�ndert von:		 					am:	
	ge�ndert von:		 					am:
	ge�ndert von:		 					am:	
	ge�ndert von:		 					am:	
	review von:								am:
	getestet von:							am:
	usw.
	**/
	#endregion

	#region History/Hinweise/Bekannte Bugs:
	/**
	- 
	**/
	#endregion
	#endregion
		
	public class CPopUp
	{
		/// <summary>
		/// "Unwiederufliche" stellt eine MessageBox zur Nachfrage, ob die gew�nschte
		/// �nderungen wirklich durchgef�hrt werden sollen. Ein Click auf OK f�hrt die �nderungen aus
		/// CANCEL bricht ab und geht zum Zustand Bearbeiten zur�ck.
		/// Returnwerte sind "DialogResult.OK" oder "DialogResult.Cancel"
		/// </summary>		
		public static DialogResult UnwiederuflicheEingabe()
		{
			DialogResult pout_dr_result;
			pout_dr_result = MessageBox.Show("Diese Aktion ist unwiederruflich! Soll sie wirklich ausgef�hrt werden?",
				"Unwiederrufliche Eingabe!", 
				MessageBoxButtons.YesNo, 
				MessageBoxIcon.Question);

			return pout_dr_result;			
		}
		/// <summary>
		/// "Zur�cksetzenEingabe" stellt eine MessageBox zur Nachfrage, ob die gemachten
		/// �nderungen gel�scht werden sollen. Ein Click auf OK l�scht die �nderungen
		/// CANCEL bricht das L�schen ab und geht zum Zustand Bearbeiten zur�ck.
		/// Returnwerte sind "DialogResult.OK" oder "DialogResult.Cancel"
		/// </summary>		
		public static DialogResult ZuruecksetzenEingaben()
		{
			DialogResult pout_dr_result;
			// alter Text von H�tte:
			//						pout_dr_result = MessageBox.Show("Die von Ihnen gemachten Eingaben werden verworfen? "
			//				+ "Nicht gespeicherte Daten gehen dabei verloren.\nOK...Verwerfen\nCancel...Verwerfen Abbrechen", 
			//				"Eingaben verwerfen?", 
			pout_dr_result = MessageBox.Show("Sollen die von Ihnen gemachten Eingaben verworfen werden?",
				"Eingaben verwerfen?", 
				MessageBoxButtons.YesNo, 
				MessageBoxIcon.Question);

			return pout_dr_result;			
		}
		/// <summary>
		/// Wie ZuruecksetzenEingaben(). Der Unterschied ist, dass man seinen eigenen Text im Dialog darstellen lassen
		/// kann.
		/// </summary>		
		public static DialogResult ZuruecksetzenEingaben(string pin_Text)
		{
			DialogResult pout_dr_result;
			// alter Text von H�tte:
			//						pout_dr_result = MessageBox.Show("Die von Ihnen gemachten Eingaben werden verworfen? "
			//				+ "Nicht gespeicherte Daten gehen dabei verloren.\nOK...Verwerfen\nCancel...Verwerfen Abbrechen", 
			//				"Eingaben verwerfen?", 
			pout_dr_result = MessageBox.Show(pin_Text + "Sollen die von Ihnen gemachten Eingaben verworfen werden?",
				"Eingaben verwerfen?", 
				MessageBoxButtons.YesNo, 
				MessageBoxIcon.Question);

			return pout_dr_result;			
		}

		/// <summary>
		/// "LoeschenElement" stellt eine MessageBox zur Nachfrage, ob das gew�hlte Objekt
		/// gel�scht werden sollen. Ein Click auf Yes l�scht die �nderungen
		/// No bricht das L�schen ab und geht zum Zustand Bearbeiten zur�ck.
		/// Returnwerte sind "DialogResult.Yes" oder "DialogResult.No"
		/// 
		/// </summary>
		public static DialogResult LoeschenElement()
		{
			DialogResult pout_dr_result;			
			pout_dr_result = MessageBox.Show("Soll das markierte Element wirklich unwiederruflich gel�scht werden",
				"Element L�schen?", 
				MessageBoxButtons.YesNo, 
				MessageBoxIcon.Question);

			return pout_dr_result;			
		}

		/// <summary>
		/// "SpeichernOhneUeberschreiben" fragt nach, ob neue Daten, in die Datenbank
		/// geschrieben werden sollen, ohne dabei andere zu ver�ndern.
		/// Beispiel: Hinzuf�gen eines Kommentars
		/// Mit OK werden die Daten in die DB geschrieben. CANCEL kehrt zum Zustand 
		/// Bearbeiten zur�ck. Das Speichern wird abgebrochen.
		/// Returnwerte sind "DialogResult.OK" oder "DialogResult.Cancel"
		/// </summary>
		public static DialogResult SpeichernOhneUeberschreiben()
		{
			DialogResult pout_dr_result;
			pout_dr_result = MessageBox.Show("Sollen die eingegebene Daten in die Datenbank geschrieben werden?",
				"Daten in Datenbank schreiben?", 
				MessageBoxButtons.OKCancel, 
				MessageBoxIcon.Question);

			return pout_dr_result;
		}
		/// <summary>
		/// "SpeichernMitUeberschreiben" fragt nach, ob Daten, die schon im System stehen
		/// wirklich �berschrieben werden sollen. Beispiel: �nderung von Kr�ftedaten.
		/// Mit OK werden die Daten in der DB �berschrieben. CANCEL kehrt zum Zustand 
		/// Bearbeiten zur�ck. Das Speichern wird abgebrochen.
		/// Returnwerte sind "DialogResult.OK" oder "DialogResult.Cancel"
		/// </summary>
		public static DialogResult SpeichernMitUeberschreiben()
		{
			DialogResult pout_dr_result;
			pout_dr_result = MessageBox.Show("Mit dem Speichern �berschreiben Sie Daten in der Datenbank. "
				+ "Die bestehenden Daten werden ersetzt.\n OK...Speichern und �berschreiben\nCancel...Speichern abbrechen", 
				"Daten �berschreiben?", 
				MessageBoxButtons.OKCancel, 
				MessageBoxIcon.Question);

			return pout_dr_result;
		}
		/// <summary>
		/// "MeldenVonFalschenEingabe" wei�t darauf hin, dass das Speichern nicht stattfinden kann,
		/// da Fehler bei Eingaben aufgetreten sind. Die Fehlerhaften Eingaben sollen in der GUI mit 
		/// Hilfe eines Errorproviders markiert werden. 
		/// Returnwert ist immer "DialogResult.OK".
		/// </summary>
		/// <returns></returns>
		public static DialogResult MeldenVonFalscherEingabe()
		{
			DialogResult pout_dr_result;
			pout_dr_result = MessageBox.Show("Die markierten Eingaben enthalten Fehler. Korrigieren Sie die Fehler "
				+ "und versuchen Sie es erneut.", 
				"Eingabefehler", 
				MessageBoxButtons.OK, 
				MessageBoxIcon.Information);

			return pout_dr_result;
		}
		/// <summary>
		/// "MeldenVonSystemfehler" bringt eine Message hervor, wenn das Speichern aufgrund eines System-
		/// fehlers misslungen ist. Beispiel: Konnektivit�t zur Datenbank fehlt, Speicherplatz zu gering...
		/// Returnwert ist "DialogResult.Retry" f�r einen erneuten Speicherversuch oder "DialogResult.Cancel"
		/// f�r das Abbrechen des Speicherns und zur�ckkehren in den Zustand Bearbeiten.
		/// </summary>		
		public static DialogResult MeldenVonSystemfehler()
		{
			DialogResult pout_dr_result;
			pout_dr_result = MessageBox.Show("Beim Speichern der Daten ist ein Sysemfehleraufgetreten. "
				+ "Retry...Speichern erneut versuchen\n Cancel...Speichern abbrechen.", 
				"Systemfehler beim Speichern", 
				MessageBoxButtons.RetryCancel, 
				MessageBoxIcon.Warning);

			return pout_dr_result;
		}
		/// <summary>
		/// "FragenSpeichernBeiBeendenProgramm" erfragt, ob nicht gespeicherte �nderungen gespeichert oder
		/// verworfen werden sollen. Alternativ kann das Beenden abgebrochen werden.
		/// Bei YES muss gespeichert werden (mit Eingabepr�fung etc.) Bei NO wird das Programm einfach 
		/// beendet. Mit CANCEL gelangt man in den Zustand Bearbeiten, das Programm wird nicht beendet.
		/// Returnwert ist "DialogResult.Yes", "DialogResult.No" oder "DialogResult.Cancel".
		/// </summary>
		public static DialogResult FragenSpeichernBeiBeendenProgramm()
		{
			DialogResult pout_dr_result;
			pout_dr_result = MessageBox.Show("Es liegen noch ungespeicherte Daten vor. Sollen "
				+ "die Daten gespeichert werden?\n Yes...Daten speichern und beenden\n "
				+ "No...Daten verwerfen und beenden\n Cancel...Beenden abbrechen", 
				"Nicht gespeicherte Daten", 
				MessageBoxButtons.YesNoCancel, 
				MessageBoxIcon.Information);

			return pout_dr_result;
		}

		public static DialogResult BenutzerExistiertBereits()
		{
			DialogResult pout_dr_result;
			pout_dr_result = MessageBox.Show("Der Benutzername existiert bereits in der Datenbank, bitte w�hlen Sie einen anderen Namen.", 
				"Benutzername existiert schon.", 
				MessageBoxButtons.OK, 
				MessageBoxIcon.Information);

			return pout_dr_result;
		}

		public static void ZeigeInfo()
		{
			Form frm_info = new Cpr_frm_Info();
			frm_info.Show();
		}
 

		public static void KeineVerbindungZumServer()
		{
			MessageBox.Show("Es konnte keine Verbindung zum Server aufgebaut werden. \n"
				+ "Bitte �berpr�fen Sie, ob der Server gestartet wurde und \n"
				+ "ob Sie die korrekten Verbindungsdaten eingegeben haben.",
				"Keine Verbindung zum Server!",
				MessageBoxButtons.OK, 
				MessageBoxIcon.Error);
		}


		public static void NichtAlleServervorrausstzungenErfuellt()
		{
			MessageBox.Show("Es sind nicht alle serverseitigen Vorraussetzungen zum Betrieb des Systems erf�llt. \n "
				+ "Bitte �berpr�fen Sie, ob ein Einsatz angelegt wurde.\n " 
				+ "Bitte korrigieren Sie dies und starten den Client erneut.",
				"Nicht alle Vorraussetzungen erf�llt!",
				MessageBoxButtons.OK, 
				MessageBoxIcon.Error);
		}

	}
}
