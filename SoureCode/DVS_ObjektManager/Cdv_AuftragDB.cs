using System;
using System.Collections;
using pELS.DV;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Zusammenfassung für AuftragDB.
	/// </summary>
	public class Cdv_AuftragDB: Cdv_PelsObjectDB
	{
		/// <summary>
		/// speichert die laufende Nummer für Auftraege
		/// Erlaubt eine vergabe von fortlaufenden Auftragsnummern		
		/// </summary>
		int _i_LaufendeAuftragsNummer =1;

		public Cdv_AuftragDB()
		{}

		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_Auftrag caAuf;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_Auftrag)
				caAuf = (Cdv_Auftrag) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}

		/// <summary>
		/// ermittelt die höchste laufende Nummer der gespeicherten Meldungen
		/// und setzt die von der Verwaltung gehaltene lfd. Nr. um eins hoch
		/// </summary>
		/// <returns></returns>
		public override void LadeAusDB()
		{
			base.LadeAusDB ();

			IDictionaryEnumerator ide = this.GetEnumerator();
			// gehe durch alle Einträge
			while(ide.MoveNext())
			{
				int tmpNummer = ((Cdv_Mitteilung) ide.Value).LaufendeNummer;
				if (tmpNummer >= this._i_LaufendeAuftragsNummer)
					this._i_LaufendeAuftragsNummer = tmpNummer + 1;
			}
		}

		/// <summary>
		/// setzt die laufende Nummer für die zu speichernde Mitteilung
		/// falls es sich um eine neue Mitteilung handelt
		/// </summary>
		/// <param name="pin_object"></param>
		/// <returns></returns>
		public override pELS.DV.Server.Interfaces.IPelsObject Speichern(pELS.DV.Server.Interfaces.IPelsObject pin_object)
		{
			Cdv_Mitteilung tmpMitteilung = (Cdv_Mitteilung) pin_object;
			// prüfe ob neue Mitteilung
			if (tmpMitteilung.ID == 0)
			{
				tmpMitteilung.LaufendeNummer = this._i_LaufendeAuftragsNummer;
				this._i_LaufendeAuftragsNummer++;
			}
			return base.Speichern (tmpMitteilung);
		}

	}
}
