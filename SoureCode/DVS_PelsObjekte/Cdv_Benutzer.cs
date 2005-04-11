using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über einen Benutzer sowie alle von Cdv_pELSObject eingeerbten Eigenschaften.
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   15.02.2005
	/// </summary>
	[Serializable]
	public class Cdv_Benutzer : Cdv_pELSObject
	{

		#region Klassenvariablen
		//Benutzername
		private string _str_benutzername = null;

		//Systemrolle
		private Tdv_Systemrolle _Tdv_Systemrolle_systemrolle = Tdv_Systemrolle.Zugtruppführer;
		#endregion
		#region Konstruktoren
		/// <summary>
		/// Standard Konstruktor.
		/// Von der Nutzung wird ABGERATEN !
		/// 
		/// Alle Werte werden mit Standards initialisiert.
		/// Es ist nicht garantiert wie lange dieser noch unterstützt wird.
		/// </summary>
		public Cdv_Benutzer()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Nimmt alle MussFelder und belegt diese beim Erzeugen des Objektes
		/// </summary>
		/// <param name="pin_Benutzername">Name des Benutzers. Ist Systemweit eindeutig</param>
		/// <param name="pin_Rolle">aktuelle Tdv_Systemrolle des Benutzers</param>
		public Cdv_Benutzer(string pin_Benutzername, Tdv_Systemrolle pin_Rolle)
		{
			this._str_benutzername = pin_Benutzername;
			this._Tdv_Systemrolle_systemrolle = pin_Rolle;
						
		}
		#endregion
		public override string ToString()
		{
			return this.Benutzername;
		}

		#region Properties
		public string Benutzername
		{
			get{return this._str_benutzername;}
			set{this._str_benutzername = value;}
		}
		public Tdv_Systemrolle Systemrolle
		{
			get{return this._Tdv_Systemrolle_systemrolle ;}
			set{this._Tdv_Systemrolle_systemrolle = value;}
		}


		#endregion



	}
}
