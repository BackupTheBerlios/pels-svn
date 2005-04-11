using System;
using pELS.DV.Server;
using pELS.Server;
using pELS.DV;

namespace Interface
{
	/// <summary>
	/// Summary description for IPortalLogik_MAT.
	/// </summary>
	public interface IPortalLogik_MAT
	{
		// einen Termin speichern
		public IPelsObject SpeichereTermin(Cdv_Termin pin_termin);
		// eine THW-interne MeldungsID vergeben
		private Cdv_Meldung VergebeMeldungsID(Cdv_Meldung pin_meldung);
	}
}
