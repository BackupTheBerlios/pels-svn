using System;

namespace pELS.Client
{
	public class Cdv_Benutzer
	{
		private string _benutzername;
		private Tdv_Systemrolle _systemrolle;

		public Cdv_Benutzer(string pin_strBenutzername, Tdv_Systemrolle pin_strSystemrolle)
		{
			this._benutzername = pin_strBenutzername;
			this._systemrolle = pin_strSystemrolle;
		}

		public string Benutzername
		{
			get{return (_benutzername);}
			set{_benutzername = value;}
		}

		public Tdv_Systemrolle Systemrolle
		{
			get{return (_systemrolle);}
			set{_systemrolle = value;}
		}
	}

	public enum Tdv_Systemrolle
	{
		leiterFueSt,
		leiterStab
	}
}
