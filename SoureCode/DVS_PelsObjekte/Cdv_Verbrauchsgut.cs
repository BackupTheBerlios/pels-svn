using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über ein Verbrauchsgut sowie alle aus Cdv_Gut eingeerbten Eigenschaften.
	/// 
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   24.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Verbrauchsgut : Cdv_Gut
	{

		#region Klassenvariablen
		//Der Zeitpunkt zudem das Material Verbraucht sein dürfte
		private DateTime _date_spaetesterWiederbeschaffungszeitpunkt = DateTime.Parse("01.01.1800");
		#endregion
		#region Konsruktoren
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Verbrauchsgut()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes
		/// </summary>
		/// <param name="pin_Bezeichnung">bezeichnet das Gut!</param>
		public Cdv_Verbrauchsgut(string pin_Bezeichnung)
		{
			this.Bezeichnung = pin_Bezeichnung;
		}

		#endregion

		#region Properties
		public DateTime SpaetesterWiederbeschaffungszeitpunkt
		{
			get{return this._date_spaetesterWiederbeschaffungszeitpunkt;}
			set{this._date_spaetesterWiederbeschaffungszeitpunkt = value;}
		}
		#endregion
	}
}
