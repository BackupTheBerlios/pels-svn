using System;

//TODO: vielleicht das namespace noch umzubennen
namespace pELS
{
	public interface IClient
	{
		bool SpeichereBenutzer(string pin_Name, string pin_Systemrolle);
	}
}
