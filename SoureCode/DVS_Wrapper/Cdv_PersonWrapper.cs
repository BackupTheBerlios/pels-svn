using System;
using pELS.DV.Server.Interfaces;
namespace pELS.DV.Server.Wrapper
{
	/// <summary>
	/// Zusammenfassung für Cdv_PersonWrapper.
	/// </summary>
	

	public class Cdv_PersonWrapper: Cdv_WrapperBase
	{
		public override int NeuerEintrag(IPelsObject pin_ob)
		{
			Random r = new Random();
			return(r.Next(1,100));
		}

		public override bool AktualisiereEintrag(IPelsObject pin_ob)
		{
			return false;
		}

		public override IPelsObject[] LadeAusDerDB()
		{
			return(null);
		}
	}

}
