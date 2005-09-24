using System;
using System.Text;
using System.Collections;

// benötigt für: ChannelServices
using System.Runtime.Remoting.Channels;
// benötigt für: TcpChannel
using System.Runtime.Remoting.Channels.Tcp;
// benötigt für: TypeFilterLevel
using System.Runtime.Serialization.Formatters;

namespace pELS.Tools.Server
{
	/// <summary>
	/// Diese Klasse stellt verschiedenste Funktionalitäten durch static-Methoden
	/// zur Verfügung, die von allen Klassen genutzt werden können
	/// </summary>
	public class CMethoden
	{
		public CMethoden()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// Konvertiert ein DateTime-Objekt in einen String, so wie er für 
		/// die Datenbank nutzbar ist
		/// </summary>
		/// <param name="pin_date_datum">ein DateTime-Objekt</param>
		/// <returns>String, der das Datum in DB-konformen Format enthält</returns>
		public static String KonvertiereDatumFuerDB(DateTime pin_date_datum)
		{
			StringBuilder pout_str_konvertiertesDatum;
			//Es soll unbedingt das Datum 4stellig gespeichert werden:				
			string str_year = pin_date_datum.Year.ToString();
			//Darum wird der String hier vorne künstlich auf 4 Stellen verlängert
			while(str_year.Length <= 3)
			{	str_year= "0"+ str_year; }			

			pout_str_konvertiertesDatum = new StringBuilder(str_year, 150);
			pout_str_konvertiertesDatum.Append( "-");
				pout_str_konvertiertesDatum.Append( pin_date_datum.Month.ToString() );
				pout_str_konvertiertesDatum.Append( "-");
				pout_str_konvertiertesDatum.Append( pin_date_datum.Day.ToString() );
				pout_str_konvertiertesDatum.Append( " ");
				pout_str_konvertiertesDatum.Append( pin_date_datum.ToLongTimeString());

			return pout_str_konvertiertesDatum.ToString();
		}

		/// <summary>
		/// Ist vorgesehen für Sonerzeichen, muss noch implementiert werden.
		/// Wird von Wrapper Benutzt, um Strings in den DB Format zu konvertieren
		/// </summary>
		/// <param name="pin_Str_String">String zum konvertieren</param>
		/// <returns>String im DB Format</returns>
		//public static byte[] KonvertiereStringFuerDB(String pin_Str_String)
		public static string KonvertiereStringFuerDB(string pin_Str_String)
		{		
			if (pin_Str_String==null) return ""; else return(pin_Str_String);
		}
		
		/// <summary>
		/// Ist vorgesehen für Sonerzeichen, muss noch implementiert werden.
		/// Wird von Wrapper Benutzt, um Strings aus der DB zu konvertieren
		/// </summary>
		/// <param name="pin_Str_String">String zum konvertieren</param>
		/// <returns>Konvertiereter String</returns>
		public static String KonvertiereStringAusDB(string pin_Str_String)
		{
			if (pin_Str_String==null) return ""; else return(pin_Str_String);
		}

		public static String KonvertiereRealFuerDB(float pin_zahl)
		{
			#region Auskommentierter Code von Iro

//			//Was ein beschissener Hack!
//			//Warum funktioniert die string.replace methode hier nicht intuitiv bzw. gar nicht
//			//Warum muss man so nen Müll 2005 immer noch selbst implementieren
//			//by alexG
//			string pout_str_konvertiert;
//			int i_position = (pin_zahl.ToString()).IndexOf(",",0);
//			if(i_position>0)
//			{
//				pout_str_konvertiert = (pin_zahl.ToString()).Substring(0,i_position);
//				pout_str_konvertiert += ".";				
//				pout_str_konvertiert += (pin_zahl.ToString()).Substring(i_position+1);
//				return pout_str_konvertiert;
//			}
//			else
//				return pin_zahl.ToString();
			#endregion
			
			return (pin_zahl.ToString()).Replace(",",".");
			
		}


		/// <summary>
		/// überprüft, ob an dem übergebenen Port ein Kanal erzeugt werden kann
		/// </summary>
		/// <param name="pin_Portnummer"></param>
		/// <returns>
		/// TRUE, wenn ein Kanal erzeugt werden kann
		/// FALSE, wenn kein Kanal erzeugt werden kann
		/// </returns>
		public static bool IstPortFrei(int pin_Portnummer)
		{
			if (pin_Portnummer > 20000) 
				throw new Exception("CMethoden.IstPortFrei(): es konnte kein Port unter 20.000 vergeben werden!");
			try
			{
				TcpChannel _testChannel = new TcpChannel(pin_Portnummer);
				ChannelServices.RegisterChannel(_testChannel);
				ChannelServices.UnregisterChannel(_testChannel);
				return true;
			}
			catch
			{
				return false;
			}
		}


		/// <summary>
		/// initialisiert einen Kanal auf dem entsprechenden Port
		/// </summary>
		/// <param name="pin_Portnummer"></param>
		/// <returns>der erstellte Kanal</returns>
		public static TcpChannel InitialisiereKanal(int pin_Portnummer)
		{
			// Creating a custom formatter for a TcpChannel sink chain.
			BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
			provider.TypeFilterLevel = TypeFilterLevel.Full;
			// Creating the IDictionary to set the port on the channel instance.
			IDictionary props = new Hashtable();
			props["port"] = pin_Portnummer;
			// Pass the properties for the port setting and the server provider in the server chain argument. (Client remains null here.)
			TcpChannel _pout_neuer_Kanal = new TcpChannel(props, null, provider);
			ChannelServices.RegisterChannel(_pout_neuer_Kanal);
			return _pout_neuer_Kanal;
		}
	}
}
