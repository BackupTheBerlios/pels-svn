using System;

namespace pELS.Client
{
	/// <summary>
	/// Summary description for IReportRequested.
	/// </summary>
	public interface IReportRequested
	{	
		/// <summary>
		/// Dient zur Behandlung des Events aus ToolsClient\Cst_Einstellung.cs 
		/// Es wird in Cst_Client implementiert und ebenso in Cst_Report, um dort
		/// Aktionen ausführen zu können
		/// </summary>
		/// <param name="pin_mitteilung"></param>
		void BehandleReportRequestedEvent(object pin_mitteilung);
	}	
}
