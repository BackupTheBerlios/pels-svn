using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über ein Erkundungsergebnis.
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   24.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Erkundungsergebnis : Cdv_Meldung
	{

		#region Klassenvariablen
		//Der Mensch, der die Erkundung durchgeführt hat
		private string _str_Erkunder = null;
		
		//Erkundungsobjekt
		private Cdv_Erkundungsobjekt _Cdv_Erkundungsobjekt_Erkundungsobjekt = new Cdv_Erkundungsobjekt();

		//Zugehöriger Einsatzschwerpunkt
		private int _i_EinsatzschwerpunktID =0;

		#endregion
		#region Konstruktoren
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Erkundungsergebnis()
		{
		}
		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes
		/// 				
		/// Dont forget:
		/// Erkundungsergebnis beherbergt noch das Erkundungsobjekt!
		/// </summary>
		/// <param name="pin_Text">Text des Erkundungsergebis</param>
		/// <param name="pin_Absender">Absender dieser Meldung (über das Erkundungsergebnis) != Erkunder</param>	
		/// <param name="pin_Erkunder">Derjenige der vor Ort war</param>
		/// <param name="pin_EspID">Verweis auf den Einsatzschwerpunkt, bei dem erkundet wurde (kann auch 0 sein (notfalls!))</param>
		public Cdv_Erkundungsergebnis(string pin_Text, string pin_Absender, Tdv_Uebermittlungsart pin_Uebermittlungsart,  Tdv_MeldungsKategorie pin_Kategorie, bool pin_istFreieMeldung, bool pin_ZeigeInToDoListe, int pin_BearbeiterID, int pin_EinsatzschwerpunktID, string pin_Erkunder)
			: base(pin_Text, pin_Absender, pin_Uebermittlungsart,pin_Kategorie,pin_ZeigeInToDoListe,pin_BearbeiterID)
		{
			this.Erkunder = pin_Erkunder;
			this.EinsatzschwerpunkID = pin_EinsatzschwerpunktID;
		}

		#endregion

		#region Properties
		public string Erkunder
		{
			get{return this._str_Erkunder;}
			set{this._str_Erkunder = value;}
		}

		public Cdv_Erkundungsobjekt Erkundungsobjekt
		{
			get{return this._Cdv_Erkundungsobjekt_Erkundungsobjekt;}
			set{this._Cdv_Erkundungsobjekt_Erkundungsobjekt = value;}
		}

		public int EinsatzschwerpunkID
		{
			get{return this._i_EinsatzschwerpunktID;}
			set{this._i_EinsatzschwerpunktID = value;}
		}
		#endregion

		/// <summary>
		/// Hier werden die Erkundungsergebnisse aufgearbeitet um
		/// besser im entsprechenden Fenster angezeigt werden zu können
		/// </summary>
		/// <returns>einen String, der die optimale Länge zur Darstellung hat</returns>
		public override string ToString()
		{
			// Zusammebauen des Strings aus Nummer, Ekunder und wichtigste Infos über das EO
			string pout_str;
			pout_str = this.LaufendeNummer.ToString();
			pout_str += " - Erkundungsergebnis von " + this.Erkunder + " über ";
			pout_str += this.Erkundungsobjekt.Bezeichnung + " vom " + this.Erkundungsobjekt.Erkundungsdatum + "Uhr";
			return pout_str;
		}
	}
}
