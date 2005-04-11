using System;

namespace pELS.DV
{
	/// <summary>
	/// Die Klasse diehnt als abstrakte Klasse und kann zur Laufzeit als
	/// 
	/// Cdv_Meldung oder Cdv_Auftrag auftreten.
	/// 
	/// Die Implementation basiert auf der DV-Loom.vsd
	/// 
	/// alexG   15.02.2005
	/// </summary>
	[Serializable]
	public abstract class Cdv_Mitteilung : Cdv_pELSObject
	{
		#region Klassenvariablen
		// laufende Nummer einer Mitteilung
		private int _int_laufendeNummer = 0;

		//Text der Mitteilung
		private string _str_Text = null;

		//Abfassungsdatum der Mitteilung
		private DateTime _date_Abfassungsdatum = DateTime.Parse("01.01.1800");

		//Übermittlungsdatum der Mitteilung
		private DateTime _date_Uebermittlungsdatum = DateTime.Parse("01.01.1800");

		//Absender der Mitteilung
		private string _str_Absender = null;

		//Übermittlungsart der Mitteilung
		private Tdv_Uebermittlungsart _Tdv_Uebermittlungsart_Uebermittlungsart = Tdv_Uebermittlungsart.Funk;

		//Ist diese Mitteilung schon übermittelt ?
		private bool _b_IstUebermittelt = false;

		//Wer bearbeitet das ganze
		private int  _i_BearbeiterBenutzerID = 0;
		
		//Kräftemenge die die Mitteilung empfangen seollentsprechend DV-Loom
		private int[] _i_EmpfaengerMengeKraftID = null;

		//Zusätzlicher Empfänger von Mitteilungen kann ein Benutzer im System sein -> für ToDo-Liste
		private int _EmpfaengerBenutzerID = 0;

		//Gibt an, ob eine Mitteilung in einer ToDo-Liste erscheinen soll oder nicht
		private bool _istInToDoListe = false;

		# endregion

		public Cdv_Mitteilung()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		#region Properties
		public int LaufendeNummer
		{
			set{this._int_laufendeNummer = value;}
			get{return this._int_laufendeNummer;}
		}

		public string Text
		{
			get{return this._str_Text;}
			set{this._str_Text = value;}
		}

		public DateTime Abfassungsdatum
		{
			get{return this._date_Abfassungsdatum;}
			set{this._date_Abfassungsdatum = value;}
		}

		public DateTime Uebermittlungsdatum
		{
			get{return this._date_Uebermittlungsdatum;}
			set{this._date_Uebermittlungsdatum = value;}
		}
		
		public string Absender
		{
			get{return this._str_Absender;}
			set{this._str_Absender = value;}
		}
		
		public Tdv_Uebermittlungsart Uebermittlungsart
		{
			get{return this._Tdv_Uebermittlungsart_Uebermittlungsart;}
			set{this._Tdv_Uebermittlungsart_Uebermittlungsart = value;}
		}

		public bool IstUebermittelt
		{
			get{return this._b_IstUebermittelt;}
			set{this._b_IstUebermittelt= value;}
		}
		
		public int BearbeiterBenutzerID
		{
			set{this._i_BearbeiterBenutzerID = value;}
			get{return this._i_BearbeiterBenutzerID;}
		}

		public int[] EmpfaengerMengeKraftID
		{
			set{this._i_EmpfaengerMengeKraftID= value;}
			get{return this._i_EmpfaengerMengeKraftID;}
		}

		public int EmpfaengerBenutzerID
		{
			set{this._EmpfaengerBenutzerID = value;}
			get{return this._EmpfaengerBenutzerID;}
		}

		public bool IstInToDoListe
		{
			set{this._istInToDoListe = value;}
			get{return this._istInToDoListe;}
		}
		#endregion

		/// <summary>
		/// Hier werden die Mitteilungen aufgearbeitet um
		/// besser im entsprechenden Fenster angezeigt werden zu können
		/// </summary>
		/// <returns>einen String, der die optimale Länge zur Darstellung hat</returns>
		public override string ToString()
		{
			//Maximale Länge
			int i_maxPoutStr = 75;
			// Zusammebauen des Strings aus Nummer, Absender und Anfang des Textes
			string pout_str;
			pout_str = this._int_laufendeNummer + " - (" + this._str_Absender + ")";
			pout_str += " - ";
			
			int i_index = 0;
			while (i_index < i_maxPoutStr && i_index < this._str_Text.Length)
			{
				pout_str += this._str_Text.Substring(i_index, 1);
				i_index++;
			}
			pout_str += "...";

			return pout_str;
		}
	}
}
