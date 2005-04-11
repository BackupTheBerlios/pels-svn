using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über ein Material sowie alle von Gut eingeerbten Eigenschaften.
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   15.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Material : Cdv_Gut
	{

		#region Klassenvariablen
		// aktueller Besitzer des Materials
		private int _i_aktuellerBesitzerKraftID =0;

		//Eigentümer des Materials
		private int _i_EigentuemerKraftID=0;
		#endregion

		#region Konstruktoren
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Material()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes
		/// </summary>
		/// <param name="pin_Bezeichnung">bezeichnet das Gut</param>		
		/// <param name="pin_EigentuemerKraftID">Bezeichnet den Eigentümer</param>
		public Cdv_Material(string pin_Bezeichnung, int pin_EigentuemerKraftID)
		{
			this.Bezeichnung = pin_Bezeichnung;			
			this.EigentuemerKraftID = pin_EigentuemerKraftID;
			
			//Hier wird davon ausgegangen, dass beim Erzeugen 
			//erstmal der Eigentuemer auch der Besitzer ist
			this.AktuellerBesitzerKraftID =pin_EigentuemerKraftID;
		}
		#endregion
		#region Properties
		public int AktuellerBesitzerKraftID
		{
			get{return this._i_aktuellerBesitzerKraftID ;}
			set{this._i_aktuellerBesitzerKraftID = value;}
		}

		public int EigentuemerKraftID
		{
			get{return this._i_EigentuemerKraftID ;}
			set{this._i_EigentuemerKraftID = value;}
		}
		#endregion
	}
}
