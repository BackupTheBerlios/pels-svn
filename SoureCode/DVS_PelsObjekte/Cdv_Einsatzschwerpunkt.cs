using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über einen Einsatzschwerpunkt.
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   15.02.2005
	/// 	
	/// 03.03.2005 - die ID Menge von Moduln wurde rausgenommen (Die ESPs werden
	/// durch Module verwaltet). Michal & Alex
	/// </summary>
	[Serializable]
	public class Cdv_Einsatzschwerpunkt : Cdv_pELSObject
	{
		#region Klassenvariablen
		
		//Bezeichnung
		private string _str_Bezeichnung = null;
		
		//Lage
		private Cdv_Kommentar _ckom_Lage = new Cdv_Kommentar();

		//Einsatzleiter
		private int _i_einsatzleiterHelferID = 0;

		//Der zugehörige Einsatz
		private int _i_einsatzID = 0;
		
		//Priorität
		private int _i_Prioritaet = 0;
		#endregion

		#region Konstruktoren
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Einsatzschwerpunkt()
		{						
			
		}
		
		/// <summary>
		/// Nimmt die MussFelder und belegt diese beim Erzeugen des Objektes
		/// 
		/// Achtung:
		/// Wünschenswert wäre die Belegung der EinsatzleiterHelferID.
		/// Das ist der Mensch, der für den ESP verantwortlich ist.
		/// Taucht hier nicht im Konstruktor auf, um Deadlocks zu verhindern
		/// & flexibler Arbeiten zu können.
		/// </summary>
		/// <param name="pin_Bezeichnung">Bezeichnung (Name) des des ESP</param>
		/// <param name="pin_EinsatzID">Verweis auf den Einsatz zu dem dieser ESP gehört</param>
		public Cdv_Einsatzschwerpunkt(string pin_Bezeichnung, int pin_EinsatzID)
		{
			this.Bezeichnung = pin_Bezeichnung;
			this.EinsatzID = pin_EinsatzID;
		}
		#endregion

		#region Properties

		public string Bezeichnung
		{
			get{return this._str_Bezeichnung;}
			set{this._str_Bezeichnung= value;}
		}				
		public Cdv_Kommentar Lage
		{
			get{return this._ckom_Lage;}
			set{this._ckom_Lage = value;}
		}
		
		public int EinsatzleiterHelferID
		{
			get{return this._i_einsatzleiterHelferID;}
			set{this._i_einsatzleiterHelferID = value;}
		}

		public int Prioritaet
		{
			get{return this._i_Prioritaet;}
			set{this._i_Prioritaet = value;}
		}

		public int EinsatzID
		{
			get{return this._i_einsatzID;}
			set{this._i_einsatzID = value;}
		}
		#endregion

		public override string ToString()
		{
			return this.ID.ToString() + " " + this._str_Bezeichnung;
		}

	}
}
