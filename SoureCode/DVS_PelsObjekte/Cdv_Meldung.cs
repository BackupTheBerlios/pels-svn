using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über eine Meldung sowie alle von Cdv_Mitteilung eingeerbten Eigenschaften.
	/// 
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   15.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Meldung : Cdv_Mitteilung
	{
		#region Klassenvariablen

		//Kategorie einer Meldung
		private Tdv_MeldungsKategorie _Tdv_MeldungsKategorie_Kategorie = Tdv_MeldungsKategorie.Sofort;

		#endregion
		#region Konstruktoren
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Meldung()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes
		/// </summary>
		/// <param name="pin_Text">Text der Meldung</param>
		/// <param name="pin_Absender">Absender der Meldung</param>
		/// <param name="pin_Kategorie">Tdv_Meldungskategorie der Meldung</param>
		/// <param name="pin_istFreieMeldung">true, wenn freie Meldung, sonst false</param>
		/// <param name="pin_ZeigeInToDoListe">true-> Auftrag wird in der ToDoListe des Empfaengers Angezeigt (falls EmpfaengerBenutzerID gesetzt)</param>
		public Cdv_Meldung(string pin_Text, string pin_Absender, Tdv_Uebermittlungsart pin_Uebermittlungsart,  Tdv_MeldungsKategorie pin_Kategorie, bool pin_ZeigeInToDoListe, int pin_BearbeiterID)
		{
			this.Text = pin_Text;
			this.Absender = pin_Absender;
			this.Kategorie = pin_Kategorie;
			this.Uebermittlungsart = pin_Uebermittlungsart;
			this.IstInToDoListe = pin_ZeigeInToDoListe;
			this.BearbeiterBenutzerID = pin_BearbeiterID;		
		}
		#endregion
		#region Properties
		public Tdv_MeldungsKategorie Kategorie
		{
			get{return this._Tdv_MeldungsKategorie_Kategorie;}
			set{this._Tdv_MeldungsKategorie_Kategorie = value;}
		}
		#endregion


	}
}
