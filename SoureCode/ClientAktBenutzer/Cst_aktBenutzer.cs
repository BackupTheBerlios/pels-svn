using System;

namespace BenutzerTest
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Benutzer
	{

		public static string _Benutzer;

		public Benutzer()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static string IdentifiziereBenutzer()
		{
			return _Benutzer;
		}

		public bool SetzeBenutzer(string pin_benutzername, string pin_systemrolle)
		{
			return true;
		}
	
	}
}
