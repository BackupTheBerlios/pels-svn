using System;
using pELS.DV;

namespace pELS.Tools.Client
{
	/// <summary>
	/// Dieses Event wird geworfen, wenn von einem SBE zum SBE Report gewechselt werden soll,
	/// um z.B. einen Auftrag oder eine Meldung zu drucken. Passend zu diesem Event gibt es 
	/// eine Interface "IReportRequested", welches von Cst_Client implementiert wird. Hierdurch
	/// wird ein Eventhandler für dieses Event bereitgestellt
	/// </summary>
	public delegate void ReportRequestedEventHandler(object pin_mitteilung);
	
	/// <summary>
	/// Zusammenfassung für Cst_Einstellung.
	/// 
	/// #Add: Hier wird nun die aktuelle Rolle gehalten. alexG 05.03.
	/// </summary>
	public class Cst_Einstellung
	{
		
		private Cdv_Benutzer _Benutzer = new Cdv_Benutzer();
		private string _ServerIP = "127.0.0.1";
		private string _ServerPort = "9001";			
		private object _o_Cst_Client;
		private string _helpFile = AppDomain.CurrentDomain.BaseDirectory + @"Hilfe\pELS.chm";

		public Cst_Einstellung()
		{
			 this._Benutzer = new Cdv_Benutzer();			 
		}

		#region Properties
		public Cdv_Benutzer Benutzer
		{
			get{return this._Benutzer;}
			set{this._Benutzer = value;}
		}

		public string ServerIP
		{
			get{return this._ServerIP;}
			set{this._ServerIP = value;}
		}

		public string ServerPort
		{
			get{return this._ServerPort;}
			set{this._ServerPort = value;}
		}

		
		public object O_Cst_Client
		{
			get{return _o_Cst_Client;}
			set{_o_Cst_Client = value;}
		}

		public string Helpfile
		{
			get{return _helpFile;}
		}

		#endregion
		#region Funktionalität


		#endregion
	

	}
}
