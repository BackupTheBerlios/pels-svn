using System.Collections;
using System;
using System.Reflection;
using System.Runtime.Remoting;
using pELS.DV;
using pELS.DV.Server.ObjectManager.Interfaces;
using pELS.DV.Server.Interfaces;
using pELS.DV.Server;
using pELS.DV.Server.Wrapper;


namespace pELS.DV.Server.ObjectManager.Verwaltung
{
	/// <summary>
	/// Liefert die Basisfunktionalität der Dictionary Base, ist
	/// auf CPelsObject typisiert. Die Klassen, die von dieser Klasse
	/// erben, überschreiben nur die Methoden OnInsert, OnRemove, OnValidate und OnSet,
	/// um die Typprüfung durchführen zu können
	/// </summary>
	public class Cdv_PelsObjectDB: Cdv_RemoteDictionaryBase
	{
		#region Variablen

		#endregion

		#region Konstruktor
		public Cdv_PelsObjectDB()
		{
		}
		#endregion

		#region Implementation der Methoden
		/// <summary>
		/// Liefert alle Schlüssel, die in der Menge vorkommen
		/// </summary>
		public ICollection Keys  
		{
			get  
			{
				return( Dictionary.Keys );
			}
		}

		/// <summary>
		/// Liefert alle Werte, die in der Menge vorkommen
		/// </summary>
		public ICollection Values  
		{
			get  
			{
				return( Dictionary.Values );
			}
		}

		/// <summary>
		/// Hier wird der [] Operator überladen, somit ist
		/// der Zugriff myVar[1] möglich. Liefert null zurück
		/// falls iKey kein gültiger Schlüssel ist
		/// </summary>
		public Cdv_pELSObject this[ int iKey ]  
		{
			get  
			{
				return( (Cdv_pELSObject) Dictionary[iKey] );
			}
			set  
			{
				Dictionary[iKey] = value;
			}
		}

		/// <summary>
		/// Fügt ein Element vom Typ CPelsObject in die Menge hinzu
		/// </summary>
		/// <param name="iKey">Schlüsselwert</param>
		/// <param name="value">Das Objekt, das hinzugefügt werden soll</param>
		public void Add( int iKey, Cdv_pELSObject value )  
		{
			Dictionary.Add( iKey, value );
		}

		/// <summary>
		/// Prüft, ob ein Obiekt mit dem Schlüssel iKey in der Menge vorhanden ist
		/// </summary>
		/// <param name="iKey">Schlüsselwert</param>
		/// <returns></returns>
		public bool Contains(int iKey)
		{
			return(Dictionary.Contains(iKey));
		}

		/// <summary>
		/// Löscht ein Element mit dem Schlüssel iKey aus der Menge
		/// </summary>
		/// <param name="iKey">Schlüsselwert</param>
		public void Remove(int iKey)
		{
			Dictionary.Remove(iKey);
		}

		/// <summary>
		/// Wird vor dem Hinzufügen eines Elements aufgerufen, um die Prüfung des
		/// Typs durchzuführen
		/// </summary>
		/// <param name="key">Schlüsselwert</param>
		/// <param name="value">Obiekt, das hinzugefügt werden soll</param>
		protected override void OnInsert(Object key, Object value)
		{
			int iKey;
			Cdv_pELSObject cpoObject;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_pELSObject)
				cpoObject = (Cdv_pELSObject) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}
		
		/// <summary>
		/// Wird vor dem Löschen eines Elements aufgerufen, um die Prüfung des
		/// Typs durchzuführen
		/// </summary>
		/// <param name="key">Schlüsselwert</param>
		/// <param name="value">Obiekt, das hinzugefügt werden soll</param>
		protected override void OnRemove(Object key, Object value)
		{
			int iKey;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");			
		}
		/// <summary>
		/// Wird vor dem Aktualisieren eines Elements aufgerufen, um die Prüfung des
		/// Typs durchzuführen
		/// </summary>
		/// <param name="key">Schlüsselwert</param>
		/// <param name="newValue">Neue Werte der Variable</param>
		/// <param name="oldValue">Alte Werte der Variable</param>
		protected override void OnSet(Object key, Object oldValue, Object newValue)
		{
			int iKey;
			Cdv_pELSObject cpoObject;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(newValue is Cdv_pELSObject)
				cpoObject = (Cdv_pELSObject) newValue;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}

		/// <summary>
		/// Validiert das übergebene Objekt. Es wird geprüft, ob der Schlüssel
		/// und das Objekt des entsprechenden Typs sind
		/// </summary>
		/// <param name="key">Objekt, dass validiert werden soll</param>
		/// <param name="value">Schlüssel, der validiert werden soll</param>
		protected override void OnValidate(Object key, Object value)
		{
			int iKey;
			Cdv_pELSObject cpoObject;
			if((key is System.Int32) || (key is System.Int64))
				iKey = (int) key;
			else
				throw new ArgumentException("Ungültiger Schlüsselwert");
			if(value is Cdv_pELSObject)
				cpoObject = (Cdv_pELSObject) value;
			else
				throw new ArgumentException("Ungültiger Objekttyp");
		}

		/// <summary>
		/// Leitet die Speicheranfrage weiter an den Wrapper. Prüft, ob es sich
		/// um ein neues Obiekt handelt (ID == 0), oder um eine Obiekt, das 
		/// aktualisiert werdeb soll (ID != 0). Ein neues Obiekt wird in die Menge aufgenommen
		/// und in der DB gespeichert. Für ein existierendes Obiekt wird die Versionsnummer 
		/// geprüft. Das Obiekt wird nur dann aktualisiert, wenn das Obiekt in der Menge
		/// dieselbe Versionnnummer trägt.
		/// </summary>
		/// <param name="pin_object">Das Objekt, das gespeichert werden soll</param>
		/// <returns></returns>
		public virtual IPelsObject Speichern(IPelsObject pin_object)
		{
			//jetzt wird hier: nicht mehr der typ des reinkommenden Objektes bestimmt (wie früher)
			//					sondern der typ der jetzigen Klasse & daraus der Wrapper bestimmt

			Type tType = this.GetType();
			string sTemp = tType.Name;
			string sName = sTemp.Remove(sTemp.Length - 2, 2);
			
			//old
			//ermittle den Typ des Objektes pin_object
			//Type t = pin_object.GetType();
			
			//Erzeuge/Hole den passenden Wrapper, um das Objekt zu speichern. Die Wrapper MÜSSEN im Namensraum
			//"pELS.Server.DV.Wrapper" enthalten sein. Für eine Klasse muss der Wrapper denselben Namen tragen +
			//"Wrapper" (also für Cdv_Benutzer -> Cdv_BenutzerWrapper). Die Wrapper MÜSSEN in der Datei "Wrapper.dll"
			//enthalten sein. Ansonsten kann HoleInstanz den passenden Wrapper nicht erzeugen/holen
			Cdv_WrapperBase cwb = this.HoleWrapper("Wrapper.dll", "pELS.DV.Server.Wrapper." + sName + "Wrapper");


			//wenn ID == 0, dann ist es ein neues Objekt, dass in der DB noch nicht existiert
			if(pin_object.ID == 0)
			{
				try
				{
					//Validiere das Objekt, das hinzugefügt werden soll. Sollte das nicht erfolgreich sein, dann wird von der 
					//OnValidate Methode eine Exception geworfen
					this.OnValidate(0, pin_object);
					
					//Speichere das Objekt in der DB. Eine ID wird zurückgeliefert
					int iNewID = cwb.NeuerEintrag(pin_object);
					pin_object.ID = iNewID;
					//Füge das neue Objekt in die interne Menge hinzu
					this.Add(pin_object.ID, (Cdv_pELSObject) pin_object);	
				}
				catch(Exception e)
				{
					throw e;
				}
			}
			//Objekt ist schon vorhanden, also nur aktualisieren
			else
			{
				Cdv_pELSObject cpoObject = this[pin_object.ID];
				//sollte das Objekt an der Stelle, wo es zu finden sein sollte nicht existieren, dann
				//wird eine Exception geworfen
				if(cpoObject == null)
					throw new ArgumentException("Objekt nicht gefunden.");
				//prüfe die Versionsnummer. Aktualisiere nur dann, wenn diese
				//gleich sind.
				if(pin_object.ID == cpoObject.ID)
				{
					try
					{
						//derselbe Trick wie oben
						this.OnValidate(pin_object.ID, pin_object);
						// Muss nicht mehr, weil das oben schon zur verfügung steht!
						//Cdv_WrapperBase cwb = this.HoleWrapper("Wrapper.dll", "pELS.DV.Server.Wrapper." + t.Name + "Wrapper");
						//Aktualisiere die DB
						cwb.AktualisiereEintrag(pin_object);
					}
					catch(Exception e)
					{
						throw e;
					}
					//Aktualisiere die interne Menge
					pin_object.Version = ++cpoObject.Version;
//					cpoObject = (Cdv_pELSObject) pin_object;
					this[pin_object.ID] = (Cdv_pELSObject) pin_object;
					return(pin_object);
				}
				return(null);
			}
			return(pin_object);
		}



		/// <summary>
		/// Speichert alle Objekte, die im Parameter pin_ipoa übergeben worden sind. Ruft intern
		/// die Speichern Methode für jedes einzelnes Objekt auf.
		/// </summary>
		/// <param name="pin_ipoa">Objekte, die gespeichert werden sollen</param>
		/// <returns>Liefert die gespeicherten Objekte zurück</returns>
		virtual public IPelsObject[] Speichern(IPelsObject[] pin_ipoa)
		{
			int i = 0;
			//Wir liefern uns einen Enumerator um durch die Menge iterieren zu können
			IEnumerator ie = pin_ipoa.GetEnumerator();
			//Diese Menge wird zurückgeliefert
			IPelsObject[] ipoaNew = new IPelsObject[pin_ipoa.Length];
			try
			{
				//Iteration durch die Menge
				while(ie.MoveNext())
				{
					//ermittle das aktuelle Objekt
					IPelsObject ipoCurrent = (IPelsObject) ie.Current;
					//Hier wird das aktuelle Objekt gespeichert und gleichzeitig in die
					//Ausgansgmenge gespeichert
					ipoaNew[i] = this.Speichern(ipoCurrent);
					i++;
				}
			}
			catch(Exception e)
			{
				throw e;
			}
			return(ipoaNew);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		virtual public IPelsObject[] HolenAlle()
		{
			int i = 0;
			IPelsObject[] ipoaNew = new IPelsObject[this.Count];
			IDictionaryEnumerator ide = this.GetEnumerator();
			while(ide.MoveNext())
			{
				ipoaNew[i] = ((IPelsObject) ide.Value);
				i++;
			}
			return(ipoaNew);
		}

		/// <summary>
		/// Diese Methode ruft die statische Methode der Wrapperklasse "HoleInstanz"
		/// um das Wrapper Objekt zu holen. 
		/// </summary>
		/// <param name="pin_szDLL">Die Assembly, wo der Wrapper zu Hause ist</param>
		/// <param name="pin_szKlasse">Die Wrapperklasse, die geladen werden soll</param>
		/// <returns></returns>
		private Cdv_WrapperBase HoleWrapper(string pin_szDLL, string pin_szKlasse)
		{
			Cdv_WrapperBase cwb = null;
			try
			{
				//Lade Assembly
				Assembly a = Assembly.LoadFrom(pin_szDLL);
				//Hole die Typinformationen über die Wrapperklasse
				Type t = a.GetType(pin_szKlasse);
				//Hole die Informationen über die Methode "HoleInstanz"
				MethodInfo mi = t.GetMethod("HoleInstanz");
				//Rufe die Methode HoleInstanz auf
				cwb = (Cdv_WrapperBase) mi.Invoke(null, null);
			}
			catch(Exception e)
			{
				throw e;
			}
			return(cwb);
		}

		/// <summary>
		/// Holt alle Objekte des entsprechenden Typs, die der ObjectManager verwaltet
		/// </summary>
		/// <returns>Menge der Objekten</returns>
		public virtual void LadeAusDB()
		{
			//Der Trick von der Speichern Methode. Diesmal müssen wir zusätzlich die "DB" vom Typ loswerden, mit
			//String.Remove
			Type tType = this.GetType();
			string sTemp = tType.Name;
			string sName = sTemp.Remove(sTemp.Length - 2, 2);
			try
			{
				Cdv_WrapperBase cwb = this.HoleWrapper("Wrapper.dll", "pELS.DV.Server.Wrapper." + sName + "Wrapper");
				IPelsObject[] ipoa = cwb.LadeAusDerDB();
				//Speichere alle Objekte in der internen Menge
				if(ipoa != null)
				{
					IEnumerator ie = ipoa.GetEnumerator();
					while(ie.MoveNext())
					{
						IPelsObject ipo = (IPelsObject) ie.Current;
						ipo.Version = 1;
						this.Add(ipo.ID,(Cdv_pELSObject) ipo);
					}
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());
				Console.ReadLine();
				throw e;
			}
		}
		#endregion
	}
}
