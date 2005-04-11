//using System;
//using System.Threading;
//using System.Drawing;
//using System.Windows.Forms;
//using System.Collections;
//using System.Data;
//using pELS.DV;
//
//// benötigt für: Cst_Einstellung
//using pELS.Tools.Client;
//// benötigt für: Cst_PortalLogik
//using pELS.Client;
//// benötigt für: IPortalLogik_XXX
//using pELS.APS.Server.Interface;
//
//namespace pELS.Client.EK
//{
//	/// <summary>
//	/// Summary description for CsbeEK.
//	/// Implementation bis 15.03: Steini
//	/// </summary>
//	public class Cst_EK: Cst_PortalLogik, pELS.GUI.Interface.Isbe
//	{ 
//		#region benötige Datenmengen
//		private Cdv_Einsatzschwerpunkt[] _EinsatzSchwerpunktmenge;
//		private Cdv_Modul[] _Modulmenge;
//		private Cdv_Einheit[] _EinheitMenge;
//		private Cdv_Helfer[] _HelferMenge;
//		private Cdv_KFZ[] _KFZMenge;
//		private Cdv_Einsatz _Einsatz;
//		public bool _b_DatenGeaendert;
//
//		#endregion
//
//		#region Allgemeines Teil
//		//Hält den Namen der Icon Datei fest
//		private string _strIconName = "ek.bmp";
//		//Hier wird die Beschriftung unterhalb des Icons festgehalten
//		private string _strSbeName = "Einsatzkräfte";
//		//hier wird die Klassenvariable gehalten, die die User Control enthält
//
//		private Cpr_usc_EK _usc_EK;			
//		private IPortalLogik_EK _PortalLogikEK;
//
//		#region Eventshandler - Events
//		private	pELS.Events.UpdateEventHandler _ueh_Einheiten;
//		private	pELS.Events.UpdateEventHandler _ueh_Einsatzschwerpunkte;
//		private pELS.Events.UpdateEventHandler _ueh_Helfer;
//		#endregion
//
//		#region Eventhandler - Methoden
//
//		private void BehandleEventHelfer(pELS.Events.UpdateEventArgs pin_e)
//		{
//			_EinheitMenge = this._PortalLogikEK.HoleEinheiten();
//			//fuelletrv_Einsatzmanager();
//		}	
//
//		private void BehandleEventEinheiten(pELS.Events.UpdateEventArgs pin_e)
//		{
//			_EinheitMenge = this._PortalLogikEK.HoleEinheiten();
//			//fuelletrv_Einsatzmanager();
//		}		
//
//		private void BehandleEventESP(pELS.Events.UpdateEventArgs pin_e)
//		{
//			_EinsatzSchwerpunktmenge = this._PortalLogikEK.HoleEinsatzschwerpunkte();
//			_b_DatenGeaendert = true;
//			
//			 this._usc_EK.Testme("MY SUPERTEST!");
//			
//		}		
//		#endregion
//
//		public Cst_EK(Cst_Einstellung pin_Einstellungen): base (pin_Einstellungen)
//		{
//			//Schaffe mir doch mal bitte eine Präsentationsschicht :-) -> Danke!
//			_usc_EK= new Cpr_usc_EK(this);
//			_b_DatenGeaendert = false;
//			// INIT Proxyobjekte
//			this._PortalLogikEK = (IPortalLogik_EK) this._PortalLogik;
//			AktualisiereEinsatzschwerpunktmenge();
//			AktualisiereModulnmenge();
//			AktualisiereEinheitMenge();
//			AktualisiereHelferMenge();
//			AktualisiereKFZmenge();
//			this.AktualisiereEinsatz();
////			fuelletrv_Einsatzmanager();
////			this.FuelleEinsatzscherpunkteBezeichnung();
////			this.FuelleEinsatzschwerpunktEinsatzleiter();
////			this.FuellePersonenEinheiten();
//
//			#region UpdateEvents
//			
//			_ueh_Einheiten = pELS.Events.UpdateEventAdapter.Create(
//				new pELS.Events.UpdateEventHandler(this.BehandleEventEinheiten));
//			
//
//			_ueh_Einsatzschwerpunkte = pELS.Events.UpdateEventAdapter.Create(
//				new pELS.Events.UpdateEventHandler(this.BehandleEventESP));
//			this._Portal_Update.RegistriereFuerEinsatzschwerpunkte(_ueh_Einsatzschwerpunkte);
//
//			this._ueh_Helfer = pELS.Events.UpdateEventAdapter.Create(
//				new pELS.Events.UpdateEventHandler(this.BehandleEventHelfer));
//			this._Portal_Update.RegistriereFuerHelfer(this._ueh_Helfer);
//			
//			#endregion
//		}
//		
//		#endregion
//		
//		#region Isbe Members
//		public Image GetSbeImage()
//		{
//			System.Reflection.Assembly asm_Sbe;
//			//Informationen über die ausführende Assembly sammeln
//			asm_Sbe = System.Reflection.Assembly.GetExecutingAssembly();
//			//Liefere Name der Assembly als AssemblyName
//			System.Reflection.AssemblyName asm_SbeName = asm_Sbe.GetName();
//			//Speichere den dll Namen im String
//			string strAssemblyName = asm_SbeName.Name;
//			//Erstelle ein Stream, aus dem die Icon Daten gelesen werden
//			System.IO.Stream s = asm_Sbe.GetManifestResourceStream(strAssemblyName + "." + _strIconName);
//			//Lese die Icon Daten aus dem Stream
//			Image myImage = Image.FromStream(s);
//			//Gebe myImage zurück
//			return(myImage);
//		}
//
//		public String GetSbeName()
//		{			
//			return this._strSbeName;
//		}
//
//		public void SetzeRollenRechte(int pin_i_aktuelleRolle)
//		{
//			_usc_EK.SetzeRollenRechte(pin_i_aktuelleRolle);
//		}
//
//		public System.Windows.Forms.UserControl GetSbeUserControl()
//		{
//			
//			return this._usc_EK;
//		}
//		
//		// TODO: Sichern alle Eingaben veranlassen und wenn alles erfolgreich true zurückgeben, sonst false
//		public bool CloseSbeUserControl()
//		{
//			return true;
//		}
//		#endregion
//
//		#region Cst_PortalLogik - abstract Members
//		override protected void SetzePortalTyp()
//		{
//			this._PortalTyp = typeof(IPortalLogik_EK);
//		}
//
//		override protected void SetzeRemotingPfad()
//		{
//			this._Pfad = "PortalEK";
//		}
//
//
//		#endregion
//
//		#region Laden/Füllen der Mengen
//		private void AktualisiereEinsatzschwerpunktmenge()
//		{
//			_EinsatzSchwerpunktmenge= _PortalLogikEK.HoleEinsatzschwerpunkte();
//		}
//
//		private void AktualisiereEinheitMenge()
//		{
//			_EinheitMenge= _PortalLogikEK.HoleEinheiten();
//		}
//
//		private void AktualisiereHelferMenge()
//		{
//			_HelferMenge = _PortalLogikEK.HoleHelfer();
//		}
//
//
//		private void AktualisiereModulnmenge()
//		{
//			_Modulmenge= _PortalLogikEK.HoleModule();
//		}
//
//		private void AktualisiereKFZmenge()
//		{
//			_KFZMenge= _PortalLogikEK.HoleKFZ();
//		}
//
//		private void AktualisiereEinsatz()
//		{
//			this._Einsatz = this._PortalLogikEK.HoleEinsatz();
//		}
//
//		#endregion
//
//		#region ALLES RUND UM DEN TREEVIEW UND SEIN FÜLLEN
//
//
//		/// <summary>
//		/// Füllt die Einheiten mit Helfer, KFZ und Material der Einheit
//		/// </summary>
//		/// <param name="myeinheit">die darzustellende Einheit</param>
//		/// <returns>Gibt einen Oberknoten zurück, der die Strukturieren Informationen enhält.</returns>
////		private TreeNode FülleEinheitMitDaten(Cdv_Einheit myeinheit)
////		{
////			TreeNode trn_Einheit = new TreeNode();
////			TreeNode trn_Helfer = new TreeNode();
////			TreeNode trn_KFZ = new TreeNode();
////			TreeNode trn_Material = new TreeNode();
////			
////			#region Lade Helfer zu Einheit
////			trn_Helfer.Text="Helfer";
////			if (myeinheit.HelferIDMenge!=null)
////			{
////				foreach (int myHelferID in myeinheit.HelferIDMenge)
////				{
////					TreeNode trn_myHelfer = new TreeNode();
////					Cdv_Helfer myHelfer=new Cdv_Helfer();
////					myHelfer=(Cdv_Helfer)_PortalLogikEK.HoleHelferMitID(myHelferID);
////					trn_myHelfer.Text=myHelfer.Personendaten.Name+","+myHelfer.Personendaten.Vorname;
////					trn_myHelfer.Tag=new Cst_EK_TreeviewTag();
////					(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Eintrag=myHelfer.ID;					
////					(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_Helfer;
////					(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Type=myHelfer.GetType();
////					trn_Helfer.Nodes.Add(trn_myHelfer);
////				}
////				trn_Einheit.Nodes.Add(trn_Helfer);
////			}
//			#endregion
//
//			#region Lade KFZ zur Einheit
////			trn_KFZ.Text="KFZ";
////			trn_KFZ.Tag= new Cst_EK_TreeviewTag();
//////			(trn_KFZ.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_abstrakt_Fahrzeuge;
////
////			if (myeinheit.KfzKraefteIDMenge!=null)
////			{
////				foreach (int myKFZID in myeinheit.KfzKraefteIDMenge)
////				{
////					TreeNode trn_myKfz = new TreeNode();
////					Cdv_KFZ myKfz=new Cdv_KFZ();
////					myKfz=(Cdv_KFZ)_PortalLogikEK.HoleKfzMitID(myKFZID);
////					trn_myKfz.Text=myKfz.KfzTyp+" "+myKfz.Kennzeichen;
////					trn_myKfz.Tag=new Cst_EK_TreeviewTag();
////					(trn_myKfz.Tag as Cst_EK_TreeviewTag).Eintrag=myKfz.ID;					
////					(trn_myKfz.Tag as Cst_EK_TreeviewTag).Type=myKfz.GetType();
////					(trn_myKfz.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_Fahrzeuge;
////					trn_KFZ.Nodes.Add(trn_myKfz);
////				}
////				trn_Einheit.Nodes.Add(trn_KFZ);
////			}
////			#endregion
////
////			#region Lade Material zu Einheit
//////			trn_Material.Text="Material";
//////			if (myeinheit.MaterialIDMenge!=null)
//////			{
//////				//TODO: ID anpassen, Material hat jetzt eine ID auf den Besitzer
//////				foreach (int myMaterialID in myeinheit.MaterialIDMenge)
//////				{
//////					TreeNode trn_myMaterial = new TreeNode();
//////					Cdv_Material myMaterial=new Cdv_Material();
//////					myMaterial=(Cdv_Material)_PortalLogikEK.HoleMaterialMitID(myMaterialID);
//////					trn_myMaterial.Text=myMaterial.Menge.ToString()+" "+myMaterial.Bezeichnung+" "+myMaterial.Art;
//////					trn_myMaterial.Tag=new Cst_EK_TreeviewTag();
//////					(trn_myMaterial.Tag as Cst_EK_TreeviewTag).Eintrag=myMaterial.ID;					
//////					(trn_myMaterial.Tag as Cst_EK_TreeviewTag).Type=myMaterial.GetType();
//////					(trn_myMaterial.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_Material;
//////					trn_Material.Nodes.Add(trn_myMaterial);
//////				}
//////				trn_Einheit.Nodes.Add(trn_Material);
//////			}
////
////			#endregion
////
////			return trn_Einheit;
////		}
//
//
//		/// <summary>
//		/// Lädt die Einheiten zu einem Modul.. und führt für jede einzelne Einheit FülleEinheit mit Daten aus.
//		/// Fügt diese unter den Rückgabeknoten hinzu.
//		/// </summary>
//		/// <param name="ModulID">ID eines Moduls</param>
//		/// <returns></returns>
////		private TreeNode EinheitenZuModul(int ModulID)
////		{
////			TreeNode trn_Einheiten = new TreeNode();
////			trn_Einheiten.Tag = new Cst_EK_TreeviewTag();
////			(trn_Einheiten.Tag as Cst_EK_TreeviewTag).Kontextmenue= _usc_EK.ctx_abstrakt_Einheiten;
////			foreach (Cdv_Einheit myeinheit in _EinheitMenge)
////			{
////				if (myeinheit.ModulID==ModulID)
////				{
////					TreeNode trn_Einheit = new TreeNode();
////					trn_Einheit.Text=myeinheit.Name;
////					trn_Einheit.Tag= new Cst_EK_TreeviewTag();
////					(trn_Einheit.Tag as Cst_EK_TreeviewTag).Eintrag=myeinheit.ID;
////					(trn_Einheit.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_Einheiten;
////					(trn_Einheit.Tag as Cst_EK_TreeviewTag).Type=myeinheit.GetType();
////					trn_Einheit=FuegeUnterknotenHinzu(trn_Einheit,FülleEinheitMitDaten(myeinheit));
////					trn_Einheiten.Nodes.Add(trn_Einheit);
////				}
////			}
////			return (trn_Einheiten);
////		}
////
//
//		/// <summary>
//		/// Lädt alle Helfer die einem Modul zugeordnet sind. 
//		/// Fügt diese unter den Rückgabeknoten hinzu.
//		/// </summary>
//		/// <param name="ModulID"></param>
//		/// <returns></returns>
////		private TreeNode HelferZuModul(int ModulID)
////		{
////			TreeNode trn_Helfer = new TreeNode();
////			trn_Helfer.Tag=new Cst_EK_TreeviewTag();
////			(trn_Helfer.Tag as Cst_EK_TreeviewTag).Kontextmenue= _usc_EK.ctx_abstrakt_Helfer;
////			foreach (Cdv_Helfer myHelfer in _HelferMenge)
////			{
////				if (myHelfer.ModulID==ModulID)
////				{
////					TreeNode trn_myHelfer = new TreeNode();
////					trn_myHelfer.Text=myHelfer.Personendaten.Name+","+myHelfer.Personendaten.Vorname;
////					trn_myHelfer.Tag= new Cst_EK_TreeviewTag();
////					(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Eintrag=myHelfer.ID;
////					(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Type= myHelfer.GetType();
////					(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_Helfer;
////					trn_Helfer.Nodes.Add(trn_myHelfer);
////				}
////			}
////			return (trn_Helfer);
////		}
////
//
//		/// <summary>
//		/// Lädt alle KFZ zu einem Modul und fügt diese unterhalb des Rückgabeknotens hinzu.
//		/// </summary>
//		/// <param name="ModulID">Gültig ModulID</param>
//		/// <returns></returns>
////		private TreeNode KFZZuModul(int ModulID)
////		{
////			TreeNode trn_KFZ = new TreeNode();
////			trn_KFZ.Tag=new Cst_EK_TreeviewTag();
////			(trn_KFZ.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_abstrakt_Fahrzeuge;
////			foreach (Cdv_KFZ myKFZ in _KFZMenge)
////			{
////				if (myKFZ.ModulID==ModulID)
////				{
////					TreeNode trn_myKFZ = new TreeNode();
////					trn_myKFZ.Text=myKFZ.KfzTyp+' '+myKFZ.Kennzeichen;
////					trn_myKFZ.Tag= new Cst_EK_TreeviewTag();
////					(trn_myKFZ.Tag as Cst_EK_TreeviewTag).Eintrag=myKFZ.ID;
////					(trn_myKFZ.Tag as Cst_EK_TreeviewTag).Type=myKFZ.GetType();
////					(trn_myKFZ.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_Fahrzeuge;
////					trn_KFZ.Nodes.Add(trn_myKFZ);
////				}
////			}
////			return (trn_KFZ);
////		}
//
//
//		/// <summary>
//		/// Fügt die Unterknoten von NodeWithSubNodes unter ParentNode hinzu.
//		/// </summary>
//		/// <param name="ParentNode">Neuer Oberknoten</param>
//		/// <param name="NodeWithSubNodes">Knoten unter dem die an Partennode zu setzen Knoten hängen</param>
//		/// <returns></returns>
//		private TreeNode FuegeUnterknotenHinzu(TreeNode ParentNode, TreeNode NodeWithSubNodes)
//		{
//			int i_tmp1;
//			for (i_tmp1=0; i_tmp1<NodeWithSubNodes.Nodes.Count;i_tmp1++)
//			{
//				ParentNode.Nodes.Add(NodeWithSubNodes.Nodes[i_tmp1]);
//			}
//			//ParentNode.Nodes.Add(NodeWithSubNodes);
//			return (ParentNode);
//		}
//
//
//		/// <summary>
//		/// Läd alle Module zu einem ESP und führt jeweils EinheitenZuModul, HelferZuModul und KFZzuModul aus
//		/// Fügt diese strukturiert unterhalb eines Oberknotens zurück
//		/// </summary>
//		/// <param name="ESPID">ID eines Einsatzschwerpunktes</param>
//		/// <returns>Oberknoten mit darunterhängender Struktur</returns>
////		private TreeNode ModuleZuESP(int ESPID)
////		{
////			TreeNode trn_module = new TreeNode();
////			trn_module.Tag= new Cst_EK_TreeviewTag();
////			(trn_module.Tag as Cst_EK_TreeviewTag).Kontextmenue= _usc_EK.ctx_abstrakt_Module;
////			foreach (Cdv_Modul myModul in _Modulmenge)
////			{
////				if (myModul.EinsatzschwerpunktID==ESPID)
////				{
////					TreeNode trn_myTreeNode= new TreeNode();
////					trn_myTreeNode.Text=myModul.Modulname;
////					trn_myTreeNode.Tag=new Cst_EK_TreeviewTag();
////					(trn_myTreeNode.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_Module;
////					(trn_myTreeNode.Tag as Cst_EK_TreeviewTag).Eintrag=myModul.ID;
////					(trn_myTreeNode.Tag as Cst_EK_TreeviewTag).Type=myModul.GetType();
////
////					TreeNode trn_Einheiten = new TreeNode();
////					trn_Einheiten.Text="Einheiten";
////					trn_Einheiten.Tag= new Cst_EK_TreeviewTag();
////					(trn_Einheiten.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_abstrakt_Einheiten;
////
////					trn_Einheiten=FuegeUnterknotenHinzu(trn_Einheiten,EinheitenZuModul(myModul.ID));
////					trn_myTreeNode.Nodes.Add(trn_Einheiten);
////
////					TreeNode trn_Helfer = new TreeNode();
////					trn_Helfer.Text="Helfer";
////					trn_Helfer.Tag=new Cst_EK_TreeviewTag();
////					(trn_Helfer.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_abstrakt_Helfer;
////
////					trn_Helfer=FuegeUnterknotenHinzu(trn_Helfer, HelferZuModul(myModul.ID));
////					trn_myTreeNode.Nodes.Add(trn_Helfer);
////
////					TreeNode trn_KFZ = new TreeNode();
////					trn_KFZ.Text="KFZ";
////					trn_KFZ.Tag= new Cst_EK_TreeviewTag();
////					(trn_KFZ.Tag as Cst_EK_TreeviewTag).Kontextmenue= _usc_EK.ctx_abstrakt_Fahrzeuge;					
////					
////					trn_KFZ=FuegeUnterknotenHinzu(trn_KFZ,KFZZuModul(myModul.ID));
////					trn_myTreeNode.Nodes.Add(trn_KFZ);
////
////					trn_module.Nodes.Add(trn_myTreeNode);
////				}
////			}
////			return trn_module;
////		}
////
////
////		/// <summary>
////		/// Füllt den Einsatzmanager mit den "Strukturen" Einsatzschwerpunkte, Einheiten, Module, (Material)
////		/// </summary>
//		public void fuelletrv_Einsatzmanager()
//		{
////			TreeNode trn_Einsatzmangerbaum = new TreeNode();
////			trn_Einsatzmangerbaum.Nodes.Clear();
////			TreeNode trn_Einsatzschwerpunke= new TreeNode("Einsatzschwerpunkte",18,18);
////			trn_Einsatzschwerpunke.Tag = new Cst_EK_TreeviewTag();
////			(trn_Einsatzschwerpunke.Tag as Cst_EK_TreeviewTag).Kontextmenue= _usc_EK.ctx_abstrakt_Einsatzschwerpunkte;
////			
////			
////			Cdv_Einsatzschwerpunkt[] myEinsatzschwerpunkte = _EinsatzSchwerpunktmenge;
////			
////			for( int tmp1=0; tmp1<myEinsatzschwerpunkte.GetLength(0); tmp1++)
////			{
////				TreeNode trn_myEinsatzschwerpunktnode= new TreeNode();
////				trn_myEinsatzschwerpunktnode.Text = myEinsatzschwerpunkte[tmp1].Bezeichnung;
////				trn_myEinsatzschwerpunktnode.Tag  = new Cst_EK_TreeviewTag();
////				(trn_myEinsatzschwerpunktnode.Tag as Cst_EK_TreeviewTag).Eintrag=myEinsatzschwerpunkte[tmp1];
////				(trn_myEinsatzschwerpunktnode.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_Einsatzschwerpunkte;
////				(trn_myEinsatzschwerpunktnode.Tag as Cst_EK_TreeviewTag).Type=myEinsatzschwerpunkte[tmp1].GetType();
////				trn_myEinsatzschwerpunktnode=FuegeUnterknotenHinzu(trn_myEinsatzschwerpunktnode,(ModuleZuESP(myEinsatzschwerpunkte[tmp1].ID)));
////				trn_Einsatzschwerpunke.Nodes.Add(trn_myEinsatzschwerpunktnode);
////			}
////
////			TreeNode trn_Module= new TreeNode("Module",18,18);
////			trn_Module.Tag = new Cst_EK_TreeviewTag();
////			(trn_Module.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_abstrakt_Module;
////		
////			Cdv_Modul[] myModule = _Modulmenge;
////
////			for( int tmp1=0; tmp1<myModule.GetLength(0); tmp1++)
////			{
////				TreeNode trn_myModul= new TreeNode();
////				trn_myModul.Text = myModule[tmp1].Modulname;
////				trn_myModul.Tag  = new Cst_EK_TreeviewTag();
////				(trn_myModul.Tag as Cst_EK_TreeviewTag).Eintrag=myModule[tmp1];
////				(trn_myModul.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_Module;
////				(trn_myModul.Tag as Cst_EK_TreeviewTag).Type=myModule[tmp1].GetType();
////				
////				trn_myModul=FuegeUnterknotenHinzu(trn_myModul,(EinheitenZuModul(myModule[tmp1].ID)));
////				trn_Module.Nodes.Add(trn_myModul);
////			}
////
////			
////			TreeNode trn_Einheiten= new TreeNode("Einheiten",18,18);
////			trn_Einheiten.Tag = new Cst_EK_TreeviewTag();
////			(trn_Einheiten.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_abstrakt_Einheiten;
////		
////			Cdv_Einheit[] myEinheiten = _EinheitMenge;
////
////			for( int tmp1=0; tmp1<myEinheiten.GetLength(0); tmp1++)
////			{
////				TreeNode trn_myEinheit= new TreeNode();
////				trn_myEinheit.Text = myEinheiten[tmp1].Name;
////				trn_myEinheit.Tag  = new Cst_EK_TreeviewTag();
////				(trn_myEinheit.Tag as Cst_EK_TreeviewTag).Eintrag=myEinheiten[tmp1];
////				(trn_myEinheit.Tag as Cst_EK_TreeviewTag).Kontextmenue=_usc_EK.ctx_Module;
////				(trn_myEinheit.Tag as Cst_EK_TreeviewTag).Type=myEinheiten[tmp1].GetType();
////				
////				trn_myEinheit=FuegeUnterknotenHinzu(trn_myEinheit,FülleEinheitMitDaten(myEinheiten[tmp1]));
////				trn_Einheiten.Nodes.Add(trn_myEinheit);
////			}
////
////			TreeNode trn_Helfer = new TreeNode("Helfer", 18, 18);
////			trn_Helfer.Tag = new Cst_EK_TreeviewTag();
////			(trn_Helfer.Tag as Cst_EK_TreeviewTag).Kontextmenue = this._usc_EK.ctx_abstrakt_Helfer;
////			Cdv_Helfer[] myHelfer = this._HelferMenge;
////			IEnumerator ie = myHelfer.GetEnumerator();
////			while(ie.MoveNext())
////			{
////				Cdv_Helfer helfer = (Cdv_Helfer) ie.Current;
////				TreeNode trn_myHelfer = new TreeNode();
////				trn_myHelfer.Text = helfer.ToString();
////				trn_myHelfer.Tag = new Cst_EK_TreeviewTag();
////				(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Eintrag = helfer;
////				(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Kontextmenue = this._usc_EK.ctx_abstrakt_Helfer;
////				(trn_myHelfer.Tag as Cst_EK_TreeviewTag).Type = helfer.GetType();
////
////				trn_Helfer.Nodes.Add(trn_myHelfer);
////			}
////
////			trn_Einsatzmangerbaum.Nodes.Add(trn_Einsatzschwerpunke);
////			trn_Einsatzmangerbaum.Nodes.Add(trn_Module);
////			trn_Einsatzmangerbaum.Nodes.Add(trn_Einheiten);
////			trn_Einsatzmangerbaum.Nodes.Add(trn_Helfer);
////
////			this._usc_EK.trv_Einsatzmanager.BeginUpdate();
////			this._usc_EK.trv_Einsatzmanager.Nodes.Clear();
////			foreach (TreeNode myNode in trn_Einsatzmangerbaum.Nodes)
////			{
////				_usc_EK.trv_Einsatzmanager.Nodes.Add(myNode);
////			}
////			_usc_EK.trv_Einsatzmanager.EndUpdate();
//		}		
////	
//////		public void FuelleEinsatzscherpunkteBezeichnung()
//////		{
////			IEnumerator ie = this._EinsatzSchwerpunktmenge.GetEnumerator();
////			this._usc_EK.cmb_Einsatzschwerpunkte_BezeichnungAuswahl.Items.Clear();
////			this._usc_EK.cmb_Einsatzschwerpunkte_BezeichnungAuswahl.Items.Add("<neuer Einsatzschwerpunkt>");
////			while(ie.MoveNext())
////			{
////				Cdv_Einsatzschwerpunkt esp = (Cdv_Einsatzschwerpunkt) ie.Current;
////				this._usc_EK.cmb_Einsatzschwerpunkte_BezeichnungAuswahl.Items.Add(esp);
////			}
////			this._usc_EK.cmb_Einsatzschwerpunkte_BezeichnungAuswahl.Text = "";
////		}
//
////		public void FuellePersonenEinheiten()
////		{
////			DataColumn[] dcol_a_Personen = 
////			{				
////				
////				this._usc_EK.ErstellenEinerDataColumn("dcol_Helfer_ID", "ID", "System.String"),
////				this._usc_EK.ErstellenEinerDataColumn("dcol_Helfer_Name", "Name", "System.String"),
////				this._usc_EK.ErstellenEinerDataColumn("dcol_Helfer_Vorname", "Vorname", "System.String")
////			};			
////			DataTable dtable_Personen = this._usc_EK.ErstellenEinerDataTable("dtable_Personen", dcol_a_Personen);
////			IEnumerator ie = this._HelferMenge.GetEnumerator();
////			while(ie.MoveNext())
////			{
////				Cdv_Helfer helfer = (Cdv_Helfer) ie.Current;
////				object[] obj_Row = new object[] {
////													helfer.ID.ToString(),
////													helfer.Personendaten.Name,
////													helfer.Personendaten.Vorname
////												};
////				dtable_Personen.Rows.Add(obj_Row);
////			}
////			this._usc_EK.dgrid_Kraefte_Einheit_Personen.DataSource = dtable_Personen;
////		}
//
////		public void FuelleEinsatzschwerpunktEinsatzleiter()
////		{
////			IEnumerator ie = this._HelferMenge.GetEnumerator();
////			this._usc_EK.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.Items.Clear();
////			while(ie.MoveNext())
////			{
////				Cdv_Helfer helfer = (Cdv_Helfer) ie.Current;
////				this._usc_EK.cmb_Einsatzschwerpunkte_Einsatzschwerpunkt_Einsatzleiter.Items.Add(helfer);
////			}
////		}
//
//		public Cdv_Helfer LeiterZuESP(Cdv_Einsatzschwerpunkt pin_esp)
//		{
//			Cdv_Helfer helfer = null;
//			IEnumerator ie = this._HelferMenge.GetEnumerator();
//			while(ie.MoveNext())
//			{
//				helfer = (Cdv_Helfer) ie.Current;
//				if(helfer.ID == pin_esp.EinsatzleiterHelferID)
//					return(helfer);
//			}
//			return(null);
//		}
//
//		public void SpeichereESP(Cdv_Einsatzschwerpunkt pin_esp, bool bIstNeu)
//		{
////			if(bIstNeu)
////				pin_esp.EinsatzID = this._Einsatz.ID;
////			this._PortalLogikEK.SpeichereESP(pin_esp);
////			this.AktualisiereEinsatzschwerpunktmenge();
////			//TODO: Optimieren!!!!
////			this.fuelletrv_Einsatzmanager();
////			this.FuelleEinsatzscherpunkteBezeichnung();
//		}
//
//		public void ErstelleESP(Cdv_Einsatzschwerpunkt pin_esp)
//		{
//			pin_esp.EinsatzID = this._Einsatz.ID;
//			pin_esp.Bezeichnung = "Neuer ESP";
//			this._PortalLogikEK.SpeichereESP(pin_esp);
//		}
//
//		public void SpeichereHelfer(Cdv_Helfer pin_helfer)
//		{
//			this._PortalLogikEK.SpeichereHelfer(pin_helfer);
//		}
//	
//		#endregion
//	}
//}
