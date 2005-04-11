using System;

namespace pELS.Tools.Server
{
	/// <summary>
	/// Diese Klasse fast alle für den Server benötigten Konstanten zusammen
	/// </summary>
	public class CKonstanten
	{
		private CKonstanten()
		{
		}
		/// <summary>
		/// Verzeichnis in welchem alle Dlls der PortalLogiken liegen
		/// </summary>
		public static string _str_PortalLogikPfad = AppDomain.CurrentDomain.BaseDirectory + @"PortalLogik";
		/// <summary>
		/// Name der Serverkonfigurationsdatei
		/// </summary>
		public static string _str_ServerConfigPfad = AppDomain.CurrentDomain.BaseDirectory + @"pELS-Server.config";
		/// <summary>
		/// RemotingPfad, auf welchem der ObjektManager angesprochen werden kann
		/// </summary>
		public static string _str_ObjektManager_RemotePfad = "ObjektManager";
		public static string _str_PfadZuPelsDbSchemaDatei = AppDomain.CurrentDomain.BaseDirectory + @"pgadmin\default.sql";

		public static string _str_DefaultRegistryDatei = AppDomain.CurrentDomain.BaseDirectory + @"pgadmin\pELS_odbc_default.reg";
		public static string _str_AktuelleRegistryDatei = AppDomain.CurrentDomain.BaseDirectory + @"pgadmin\pELS_odbc.reg";
		
	}
}
