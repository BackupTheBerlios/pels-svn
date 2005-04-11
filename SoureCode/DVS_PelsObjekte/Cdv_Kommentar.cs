using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als Objekt zur Laufzeit und enthält die Informationen 
	/// über einen Kommentar.
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   24.02.2005
	/// 
	/// Hinweis: Objekte der Klasse treten nur als Instanzvariablen von pELSObjekten auf
	/// </summary>
	[Serializable]
	public class Cdv_Kommentar
	{
		#region Klassenvariablen
		//Autor
		private string _str_Autor = null;

		//Inhalt
		private string _str_Text = null;
		#endregion

		public Cdv_Kommentar()
		{
		
		}

		#region Properties
		public string Text
		{
			get {return this._str_Text;}
			set {this._str_Text= value;}
		}

		public string Autor
		{
			get {return this._str_Autor;}
			set {this._str_Autor= value;}
		}
		#endregion
	}
}
