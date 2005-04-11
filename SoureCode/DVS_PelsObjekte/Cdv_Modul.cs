using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über ein Modul.
	/// 
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   24.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Modul : Cdv_pELSObject
	{

		#region Klassenvariablen

		//Name des Moduls
		private string _str_modulname = null;

		//Der Einsatzschwerpunkt, an dem dieses Modul zugeordnet ist
		private int _i_einsatzschwerpunktID = 0;

		#endregion
		#region Konstruktoren
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Modul()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes
		/// </summary>
		/// <param name="pin_Modulname">Name des Moduls</param>
		public Cdv_Modul(string pin_Modulname)
		{
			this.Modulname = pin_Modulname;			
		}
		#endregion

		#region Properties

		public string Modulname
		{
			get{return this._str_modulname ;}
			set{this._str_modulname = value;}
		}

		public int EinsatzschwerpunktID
		{
			get{return this._i_einsatzschwerpunktID ;}
			set{this._i_einsatzschwerpunktID= value;}
		}

		#endregion
	}
}
