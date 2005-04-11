using System;
using System.Threading;
using System.Collections;
using pELS.DV;
using pELS.DV.Server.ObjectManager.Interfaces;
using pELS.DV.Server.Interfaces;
// benötigt für: Eventbenachrichtung bei Änderungen
using pELS.Events;
// TODO: löschen
using System.Windows.Forms;

namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Die Klasse CVerwaltung übernimmt das Verwaltung der Anfragen, die an
	/// Objekte eines speziellen Typs gerichtet sind (z.B. für Anforderungen).
	/// Die Klasse CVerwaltung ist möglichst allgemein gehalten, sie leitet die
	/// Anfragen an spezielle Klassen, die die entsprechenden Mengen verwalten 
	/// (diese basieren auf der Klasse RemoteDirectoryBase). CVerwaltung implementiert
	/// 4 allgemeine Methoden: Holen, HolenAlle, 2 * Speichern. Dazu kommt noch ein
	/// Property LadenErfolgreich, dass den Ladezustand liefert (ok oder fehlgechlagen).
	/// Man kann mit Hilfe der Methode DatenErneutLaden versuchen, die Daten aus der 
	/// Datenbank erneut auszulesen.
	/// </summary>
	public class Cdv_Verwaltung: MarshalByRefObject, IVerwaltung
	{

		#region Instanzvariablen
		/// <summary>
		/// Hält die Referenz, auf die enstprechende Dictionary, die Instanzen
		/// des entsprechenden Typs verwalten
		/// </summary>
		private Cdv_PelsObjectDB _db_Liste;
		/// <summary>
		/// Deutet an, ob der Ladervorgang erfolgreich war (true) oder nicht (false)
		/// </summary>
		private bool _bLadenErfolgreich;
		/// <summary>
		/// Ein Mutex Objekt zur Synchronisation der Speichervorgänge
		/// </summary>
		private Mutex _mtxSpeichernMutex;
		#endregion

		#region Konstruktor
		/// <summary>
		/// Konfiguriert die Instanz mit dem Parameter pin_cpoDB, lädt alle
		/// Daten von der DB
		/// </summary>
		/// <param name="pin_cpoDB">Bestimmt den Typ, den es zu verwalten gibt</param>
		public Cdv_Verwaltung(Cdv_PelsObjectDB pin_cpoDB)
		{
			this._mtxSpeichernMutex = new Mutex(false);
			_db_Liste = pin_cpoDB;
			this._bLadenErfolgreich = true;
			try
			{
				this._db_Liste.LadeAusDB();
			}
			catch(Exception)
			{
				this._bLadenErfolgreich = false;
			}
		}
		#endregion

		#region Methoden
		/// <summary>
		/// liefert ein Objekt des entsprechenden Typs zurück
		/// </summary>
		/// <param name="pin_iPK">Primärschlüssel des Objektes</param>
		/// <returns>Das gesuchte Objekt wird zurückgeliefert. Null, wenn Suche
		/// erfolglos</returns>
		public IPelsObject Holen(int pin_iPK)
		{
			if(!this._bLadenErfolgreich)
				throw new Exception("Daten konnten nicht geladen werden!");
			return(this._db_Liste[pin_iPK]);
		}



		/// <summary>
		/// Speichert das entsprechende Objekt in der Datenbank, mit Hilfe von 
		/// Wrappern
		/// </summary>
		/// <param name="pin_ob">Das Obiekt, dass gespeichert werden soll</param>
		/// <returns>Liefert das gespeicherte Objekt zurück</returns>
		public IPelsObject Speichern(IPelsObject pin_ob)
		{
			this._mtxSpeichernMutex.WaitOne();
			if(!this._bLadenErfolgreich)
				throw new Exception("Daten konnten nicht gespeichert werden!");
			IPelsObject ipo = null;
			try
			{
				ipo = this._db_Liste.Speichern(pin_ob);
				// melde die ObjectID des neuen pELS-Objekts
				int[] ObjektIDMenge = new int[1];
				ObjektIDMenge[0] = pin_ob.ID;
				StarteAlleDelegates(ObjektIDMenge);
			}
			catch(Exception e)
			{
				throw e;
			}
			finally
			{
				this._mtxSpeichernMutex.ReleaseMutex();
			}
			return(ipo);
		}

		/// <summary>
		/// Speichert alle Objekte, die im Parameter pin_ipoa übergeben worden sind. Ruft intern
		/// die Speichern Methode für jedes einzelnes Objekt auf.
		/// </summary>
		/// <param name="pin_ipoa">Objekte, die gespeichert werden sollen</param>
		/// <returns>Liefert die gespeicherten Objekte zurück</returns>
		public IPelsObject[] Speichern(IPelsObject[] pin_ipoa)
		{
			this._mtxSpeichernMutex.WaitOne();
			if(!this._bLadenErfolgreich)
				throw new Exception("Daten konnten nicht gespeichert werden!");
			IPelsObject[] ipoa = null;
			try
			{
				ipoa = this._db_Liste.Speichern(pin_ipoa);
				// melde die ObjectID des neuen pELS-Objekts
				int[] ObjektIDMenge = new int[ipoa.Length];
				for(int i = 0; i < ipoa.Length; i++)
				{
					ObjektIDMenge[i] = ipoa[i].ID;
				}
				StarteAlleDelegates(ObjektIDMenge);
				// TODO: alle Delegates starten
			}
			catch(Exception e)
			{
				throw e;
			}
			finally
			{
				this._mtxSpeichernMutex.ReleaseMutex();
			}
			return(ipoa);
		}

		/// <summary>
		/// Holt alle Objekte des entsprechenden Typs, die der ObjectManager verwaltet
		/// </summary>
		/// <returns>Menge der Objekten</returns>
		public IPelsObject[] HolenAlle()
		{
			if(!this._bLadenErfolgreich)
				throw new Exception("Daten konnten nicht geladen werden!");
			IPelsObject[] mytmparray = this._db_Liste.HolenAlle();
			return mytmparray;//(this._db_Liste.HolenAlle());
		}

		/// <summary>
		/// Diese Methode versucht, Daten aus der Datenbank erneut auszulesen, aber nur im Fall,
		/// wenn der Lesevorgasng im Konstruktur fehlgeschlagen ist. 
		/// </summary>
		public void DatenErneutLaden()
		{
			this._mtxSpeichernMutex.WaitOne();
			if(this._bLadenErfolgreich)
				return;
			try
			{
				this._db_Liste.Clear();
				this._db_Liste.LadeAusDB();
				this._bLadenErfolgreich = true;
			}
			catch(Exception e)
			{
				this._bLadenErfolgreich = false;
				throw e;
			}
			finally
			{
				this._mtxSpeichernMutex.ReleaseMutex();
			}
		}

		/// <summary>
		/// Dieses Property erlaubt es nachzuschauen, ob die Daten aus der Datenbank
		/// korrekt ausgelesen werden konnten
		/// </summary>
		public bool LadenErfolgreich
		{
			get
			{
				return(this._bLadenErfolgreich);
			}
		}

		private pELS.Events.UpdateEventHandler _DelegateAufAlleUpdateEventHandler = null;
		public void RegistriereFuerAenderungsEvents(pELS.Events.UpdateEventHandler pin_Delegate)
		{
			_DelegateAufAlleUpdateEventHandler += pin_Delegate;
		}


		/// <summary>
		/// führt alle registrierten Delegates aus
		/// </summary>
		/// <param name="pin_ObjektIDMenge">Parameter für Event</param>
		private void StarteAlleDelegates(int[] pin_ObjektIDMenge)
		{
			if (_DelegateAufAlleUpdateEventHandler == null) return;
			// gehe durch alle registrierten Delegates
			foreach(Delegate tmpDel in _DelegateAufAlleUpdateEventHandler.GetInvocationList())
			{
				// und informiere diese
				UpdateEventsThreadContainer tmp = new 
					UpdateEventsThreadContainer(
					ref _DelegateAufAlleUpdateEventHandler,
					pin_ObjektIDMenge,
					(UpdateEventHandler) tmpDel);
				tmp.StarteRemoteEvent();
			}
		}
		#endregion

		public override object InitializeLifetimeService()
		{
			return(null);
		}
	}

	class UpdateEventsThreadContainer
	{
		/// <summary>
		/// delegate welches Informationen über alle registrierten Delegates hält
		/// </summary>
		private UpdateEventHandler _DelegateAufAlle;
		/// <summary>
		/// benötigt, um fehlerfrei ungültige Delegates zu entfernen
		/// </summary>
		private static Mutex _Mutex = new Mutex(false);
		/// <summary>
		/// Parameter, welche beim Event mit übergeben werden
		/// </summary>
		private UpdateEventArgs _UpdateEventArgs; 
		/// <summary>
		/// der aktuelle Delegate, der ausgeführt werden soll
		/// </summary>
		private UpdateEventHandler _AktuellerUpdateEventHandler;
		
		/// <summary>
		/// sammelt alle benötigten Konstruktoren
		/// </summary>
		/// <param name="pin_DelegateAufAlleUpdateEventHandler"></param>
		/// <param name="pin_ObjektIDMenge"></param>
		/// <param name="pin_AktuellerUpdateEventHandler"></param>
		public UpdateEventsThreadContainer(
			ref UpdateEventHandler pin_DelegateAufAlleUpdateEventHandler,
			int[] pin_ObjektIDMenge, 
			UpdateEventHandler pin_AktuellerUpdateEventHandler)
		{
			_DelegateAufAlle = pin_DelegateAufAlleUpdateEventHandler;
			_UpdateEventArgs = new UpdateEventArgs(pin_ObjektIDMenge);
			_AktuellerUpdateEventHandler = pin_AktuellerUpdateEventHandler;
		}
		/// <summary>
		/// informiert den aktuellen Delegate
		/// </summary>
		public void StarteRemoteEvent()
		{
			try
			{
				_AktuellerUpdateEventHandler(_UpdateEventArgs);
			}
				// falls dieser nicht informiert werden kann, entferne ihn aus der Liste
			catch
			{
				EntferneUngueltigenDelegate(_AktuellerUpdateEventHandler);
			}
		}

		/// <summary>
		/// entfernt ungültige Delegates aus der Liste der registrierten Delegates
		/// </summary>
		/// <param name="pin_UpdateEventHandler"></param>
		private void EntferneUngueltigenDelegate(UpdateEventHandler pin_UpdateEventHandler)
		{
			// warten bis Mutex frei ist
			_Mutex.WaitOne();
			try
			{
				// suche den aktuellen Delegate in der Liste aller Delegates
				foreach(Delegate tmpDel in _DelegateAufAlle.GetInvocationList())
				{
					if (tmpDel == pin_UpdateEventHandler)
						// entferne ihn
						_DelegateAufAlle -= pin_UpdateEventHandler;
				}
			}
			catch
			{
			}
			// gib Mutex wieder frei
			_Mutex.ReleaseMutex();

		}
	}

}
