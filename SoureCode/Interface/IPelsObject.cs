using System;

namespace pELS.DV.Server.Interfaces
{
	/// <summary>
	/// Das Interface, dass Pels Objekte implementieren sollen. Es erlaubt das
	/// ID und die Version des Objektes zu setzen. Nur Objekte, die exzplizit NICHT
	/// verwaltet werden, können die Implementation des Interfaces weglassen
	/// </summary>
	public interface IPelsObject
	{
		int ID{get;set;}
		int Version{get;set;}
	}
}


