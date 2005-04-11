using System;
// benötigt für: MethodInfo
using System.Reflection;
// benötigt für: CSharpCodeProvider
using Microsoft.CSharp;
//benötigt für: ICodeCompiler
using System.CodeDom.Compiler;
// benötigt für : StringBuilder
using System.Text;
// benötigt für: ArrayList
using System.Collections;

using System.Windows.Forms;

namespace pELS.Server
{
	using pELS.APS.Server.Interface;

	/// <summary>
	/// Die Klasse CPortalWaechter steuert den Zugang zu den einzelnen PortalLogiken
	/// diese können vom Client über einen Port auf dem Server angesprochen werden,
	/// müssen dann aber auf die jeweiligen Logiken weitergeleitet werden
	/// </summary>
	public class CPortalWaechter
	{
		private ArrayList _Decorators = new ArrayList();

		public CPortalWaechter()
		{
		}

		public void FuegeDecoratorHinzu(IPortalLogik_Decorator pin_PortalLogikDecorator)
		{
			_Decorators.Add(pin_PortalLogikDecorator);
		}

		public void EntferneAlleDecorators()
		{
			foreach(IPortalLogik_Decorator aktDecorator in _Decorators)
			{
				aktDecorator.Disconnect();
			}
			_Decorators.Clear();
		}
	}

	class CDecoratorDesigner
	{
		private CDecoratorDesigner()
		{
		}
		
		
		/// <summary>
		/// erstellt eine Decorator-Klasse 
		/// </summary>
		/// <param name="pin_Type">Typ der PortalLogik</param>
		/// <param name="pin_Klassenname">Name, welche die Klasse haben soll</param>
		/// <param name="pin_Port">Port, auf dem die PortalLogik erreichbar ist</param>
		/// <param name="pin_RemotingPfad">Pfad, auf dem dieses, als auch das PortalLogik-Objekt 
		/// erreichbar ist</param>
		/// <returns></returns>
		public static string CreateSourceCode(Type pin_Type, string pin_Klassenname, 
			int pin_Port, string pin_RemotingPfad)
		{
			// Bezeichnung für das Remote-Objekt
			// über welches die Aufrufe weitergeleitet ewrden
			string _RemoteObjekt = "remoteObject";
			// SourceCode, der erstellt und zurückgegeben wird
			string pout_SourceCode = "";
			// Interface, welches via Remoting zur Verfügung gestellt werden soll
			System.Type PortalInterface;

			#region SourceCode: Header
			// erstelle die using-Deklarationen für die Decorator-Klasse
			pout_SourceCode += 
				"using System;" + "\n" + 
				"// benötigt für: IPelsObject" + "\n" + 
				"using pELS.DV.Server.Interfaces;" + "\n" + 
				"// benötigt für: pELS-Objekte" + "\n" + 
				"using pELS.DV;" + "\n" + 
				"// benötigt für: Remoting" + "\n" +
				"using System.Runtime.Remoting;" + "\n" +
				"\n" +
				// setze den Namespace auf den gleichen wie alle Serverklassen
				"namespace pELS.Server" + "\n" +
				"{" + "\n";
			#endregion
			#region SourceCode: Body
			#region Klassenname und Vererbungs- bzw. Implementierungsbeziehungen
			pout_SourceCode += 
				"public class " + 
				pin_Klassenname +  
				" : MarshalByRefObject, IPortalLogik_Decorator "
				;
			// finde das zu publizierende Interface
			// ACHTUNG: hierfür ist es wichtig, dass alle Klassen PortalLogik_XXX
			// dieses Interface an der 2 Position stehen haben
			Type[] implementierteInterfaces = pin_Type.GetInterfaces();
			PortalInterface = implementierteInterfaces[1];
			pout_SourceCode += ", " + PortalInterface.FullName;
			pout_SourceCode += 
				"\n" + 
				"{" + "\n" + "\n";
			#endregion
			#region Konstruktor & Remoting-Methode
			// erstelle einen Konstruktor
			pout_SourceCode += 
				PortalInterface.FullName + " " + _RemoteObjekt + ";\n" + "\n";
			pout_SourceCode += 
				"public " + pin_Klassenname + "()\n" + 
				"{" + "\n" +
				"}" + "\n";
			// erstelle InitializeLifetimeService-Methode
			pout_SourceCode += 
				"public override Object InitializeLifetimeService()" + "\n" + 
				"{" + "\n" +
                "  return null;" + "\n" +
				"}" + "\n";
			// erstelle die Methode, welches dieses Decorator-Objekt publiziert
			// und sich zur PortalLogik verbindet
			pout_SourceCode += 
				"public void StarteRemotingObject(int pin_Port)" + "\n" + 
				"{" + "\n" + 
				"remoteObject = (" + PortalInterface.FullName + ") Activator.GetObject(" + "\n" + 
				"typeof(" + PortalInterface.FullName + ")," + "\n" + 
				"\"tcp://127.0.0.1:" + pin_Port.ToString() + "/" + pin_RemotingPfad + "\");" + "\n" + 
				"RemotingServices.Marshal(" + "\n" + 
				"this," + "\n" + 
				"\"" + pin_RemotingPfad + "\"," + "\n" +  
				"((Object)this).GetType());" + "\n" + 
				"}"+ "\n";

			pout_SourceCode += 
				"public void Disconnect()\n" + 
				"{" + "\n" +
				"RemotingServices.Disconnect(this);" + "\n" +
				"}" + "\n";

			#endregion

			#region Methoden
			// gehe durch alle implementierten Interfaces
			// mit Ausnahme des MarshalByRef-Interfaces (das erste)
			for(int ICount = 1; ICount < implementierteInterfaces.Length; ICount++)
			{
				// lese alle Methoden dieses Interfaces
				MethodInfo[] MI_Menge = implementierteInterfaces[ICount].GetMethods();
				// gehe über alle Methoden
				foreach (MethodInfo aktuellesMI in MI_Menge)
				{
					// Methodenname
					pout_SourceCode += 
						"public " + 
						aktuellesMI.ReturnType + " " + 
						aktuellesMI.Name + "(";
					ParameterInfo[] PI_Menge = aktuellesMI.GetParameters();
					bool SetzeKomma = false;
					// Methodenparameter
					foreach(ParameterInfo aktuellesPI in PI_Menge)
					{
						if (!SetzeKomma)
						{
							SetzeKomma = true;
						}
						else { pout_SourceCode += ", ";}
						// überprüfe, ob es sich um einen out-Parameter handelt
						if (aktuellesPI.IsOut)
						{
							// füge out hinzu
							pout_SourceCode += 
								" out "; 
							// entferne das "&"-Zeichen
							pout_SourceCode += 
								aktuellesPI.ParameterType.ToString() + " ";
							pout_SourceCode = pout_SourceCode.Remove(pout_SourceCode.Length - 2, 1);
						}
						else
						{
							// falls es kein out-Parameter ist, muss kein "&"-Zeichen entfernt werden
							pout_SourceCode += 
								aktuellesPI.ParameterType.ToString() + " ";
						}
						// Parametername 
						pout_SourceCode += 
							aktuellesPI.Name;

					}

					pout_SourceCode += 
						")" + "\n" +
						"{" + "\n";
					// falls Rückgabewert vorhanden, dann gebe diesen auch zurück
					if (aktuellesMI.ReturnType != typeof(void))
					{
						pout_SourceCode += 
							"return ";
					}
					// Methodeninhalt:
					// leitet Aufruf an PortalLogik weiter
					pout_SourceCode += _RemoteObjekt + "." + aktuellesMI.Name + "(";

					// Parameter für Remoting-Aufruf
					SetzeKomma = false;
					foreach(ParameterInfo aktuellesPI in PI_Menge)
					{
						if (!SetzeKomma)
						{
							SetzeKomma = true;
						}
						else { pout_SourceCode += ", ";}

						if (aktuellesPI.IsOut)
						{
							// füge out hinzu
							pout_SourceCode += 
								" out "; 
							// entferne das "&"-Zeichen
							
						}
						
						// Parametername 
						pout_SourceCode += 
							aktuellesPI.Name;





					}

					pout_SourceCode += 
						");" + "\n" + 
						"}" + "\n";

				}
			}
			#endregion

			#endregion
			#region SourceCode: Ende
			pout_SourceCode += 
				"}" + "\n" +
				"}" + "\n";
			#endregion
			// ACHTUNG: dies ist erforderlich, da der SourceCode sonst nicht kompiliert
			// werden kann
			pout_SourceCode = pout_SourceCode.Replace("System.Void", "void");
//			MessageBox.Show(pout_SourceCode);
			return pout_SourceCode;
		}

		public static void UebersetzeSourceCode(string pin_PortalDateiname, 
			string pin_AusgabeDateiname,
			string pin_DepenciesDLLPfad,
			string pin_SourceCode)
		{
			// setze Compiler-Parameter
			Microsoft.CSharp.CSharpCodeProvider provider = 
				new CSharpCodeProvider();
			ICodeCompiler compiler = provider.CreateCompiler();
			CompilerParameters compilerparams = new CompilerParameters();
			compilerparams.GenerateExecutable = false;
			compilerparams.GenerateInMemory = false;
			compilerparams.OutputAssembly = pin_AusgabeDateiname;
			compilerparams.IncludeDebugInformation = false;

			#region Einlesen aller benötigten Dlls
			compilerparams.CompilerOptions = 
					@"/r:System.Runtime.Remoting.dll ";

			// Einlesen des Verzeichnisses
			System.IO.DirectoryInfo dir_sbeVerzeichnis = 
				new System.IO.DirectoryInfo(
				AppDomain.CurrentDomain.BaseDirectory);
			// lesen aller Dlls
			System.IO.FileInfo[] arl_alleAssemblies = dir_sbeVerzeichnis.GetFiles("*.dll");
			foreach(System.IO.FileInfo fileInfo in arl_alleAssemblies)
			{
					compilerparams.CompilerOptions = 
						compilerparams.CompilerOptions + 
						@"/r:" + fileInfo.Name + " ";
			}
			#endregion
			// kompiliere und speichere Compiler-Ergebnisse
			CompilerResults results = 
				compiler.CompileAssemblyFromSource(compilerparams, pin_SourceCode);
			// baue Fehlermeldung
			if (results.Errors.HasErrors)
			{
				StringBuilder errors = new StringBuilder("Compiler Errors :\r\n");
				foreach (CompilerError error in results.Errors )
				{
					errors.AppendFormat("Line {0},{1}\t: {2}\n", 
						error.Line, error.Column, error.ErrorText);
				}
//				MessageBox.Show(errors.ToString());
				throw new Exception(errors.ToString());
				Environment.Exit(1);
			}
		}
	}
}
