using System;
using System.Windows.Forms;


namespace pELS.Events
{
	/// <summary>
	/// Parameter, welcher beim Auslösen eines UpdateEvents übergeben wird
	/// </summary>
	[Serializable]	
	public class UpdateEventArgs : EventArgs 
	{  
		// enthält die IDs aller Objekte, die verändert wurden
		private int[] _IDMenge;
		public int[] IDMenge
		{
			get { return _IDMenge;}
		}
		public UpdateEventArgs(int[] pin_IDMenge) 
		{
			this._IDMenge = pin_IDMenge;
		}

	}

	/// <summary>
	/// event handler für ein UpdateEvent
	/// </summary>
	public delegate void UpdateEventHandler(UpdateEventArgs e);
	/// <summary>
	/// diese Klasse dient als Zwischenstück zur Kommunikation
	/// zwischen Server und Client
	/// somit muss der Server nicht das Interface der jeweiligen Clientklasse, die
	/// benachrichtigt werden soll, kennen
	/// see http://blogs.msdn.com/csharpfaq/archive/2004/04/28/122691.aspx for further infos
	/// </summary>
	public class UpdateEventAdapter : MarshalByRefObject
	{
		/// <summary>
		/// delegate auf die Zielmethode
		/// </summary>
		private UpdateEventHandler _Ziel;
		/// <summary>
		/// privater Konstruktor
		/// </summary>
		/// <param name="target">delegate auf die Zielmethode</param>
		private UpdateEventAdapter(UpdateEventHandler pin_Ziel)
		{
			this._Ziel += pin_Ziel;
		}


		// setzen der Lease-time für remotable-objects dieser AppDomain
		public override Object InitializeLifetimeService()
		{
			// Lifetime ist unendlich
			return null;
		}


		/// <summary>
		/// diese Methode wird aufgerufen, wenn das Event ausgelöst wurde
		/// </summary>
		/// <param name="pin_ChangeEventArgs">Event Parameter</param>
		public void UpdateEvent(UpdateEventArgs pin_UpdateEventArgs)
		{
			// starte die Zielmethode
			this._Ziel(pin_UpdateEventArgs);
		}

		/// <summary>
		/// erzeugt ein neues Objekt dieser Klasse
		/// </summary>
		/// <param name="target">delegate auf die Zielmethode</param>
		/// <returns>delegate auf this.UpdateEvent</returns>
		public static UpdateEventHandler Create(UpdateEventHandler pin_Ziel)
		{			
			UpdateEventAdapter shim = new UpdateEventAdapter(pin_Ziel);
			UpdateEventHandler pout = new UpdateEventHandler(shim.UpdateEvent);
			return pout;
		}
	}

	

}