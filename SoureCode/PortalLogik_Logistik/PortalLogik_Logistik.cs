using System;
using System.Collections;

namespace PortalLogik_Logistik
{
	// benötigt für: Cap_PortalLogik
	using pELS.Server;
	// benötigt für: IPortalLogik_XXX
	using pELS.APS.Server.Interface;
	// benötigt für: pELS.DV.Cdv_XXX
	using pELS.DV;
	// benötigt für: ObjectManager
	using pELS.DV.Server.ObjectManager;
	
	/// <summary>
	/// Summary description for CportalLogik_Logistik.
	/// </summary>
	public class CPortalLogik_Logistik : Cap_PortalLogik, IPortalLogik_Logistik
	{
		public CPortalLogik_Logistik(int pin_OMPort, string pin_URL, int pin_Port) : 
			base(pin_OMPort, pin_URL, pin_Port)
		{	
		}

		#region Cap_PortalLogik members
		protected override void SetzeRemotingPfad()
		{
			this._Pfad = "PortalLogistik";
		}

		public override void StartePortalLogik()
		{
		}
		#endregion		

		#region IPortalLogik_Logistik Members

		public void SpeichereAnforderung(Cdv_Anforderung pin_Anforderung)
		{
			this._ObjektManager.Anforderungen.Speichern(pin_Anforderung);
		}

		public void SpeichereGut(Cdv_Gut pin_Gut)
		{
			Cdv_Verbrauchsgut tmpVG = pin_Gut as Cdv_Verbrauchsgut;
			Cdv_Material tmpM = pin_Gut as Cdv_Material;
			if(tmpM != null)
                this._ObjektManager.Material.Speichern(tmpM);
			else if (tmpVG != null)
				this._ObjektManager.Verbrauchsgueter.Speichern(tmpVG);
		}

		public void MaterialUebergabe(Cdv_Materialuebergabe pin_Materialuebergabe)
		{
			this._ObjektManager.Materialuebergaben.Speichern(pin_Materialuebergabe);
		}

		public void OrdneMaterialZuEinheit(Cdv_Material pin_Material, Cdv_Einheit pin_Einheit)
		{
			// TODO: IRO FRAGEN WIE DAS FUNKTIONIEREN SOLL !!
//			pin_Einheit.MaterialIDMenge
//			pin_Material.AktuellerBesitzerKraftID = pin_Einheit.ID;
			// TODO:  Add CPortalLogik_Logistik.OrdneMaterialZuEinheit implementation
		}

		public Cdv_Einheit[] HoleAlleEinheiten()
		{
			// konvertiere Ergebnis in das korrekte Format
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.Einheiten.HolenAlle(); 
			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_Einheit[] pout_Einheiten = new Cdv_Einheit[ipoa.Length];
				ipoa.CopyTo(pout_Einheiten,0);
				return pout_Einheiten;
			}
			else return null;
		}

		public Cdv_KFZ[] HoleAlleKFZ()
		{
			// konvertiere Ergebnis in das korrekte Format
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.Kfz.HolenAlle(); 
			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_KFZ[] pout_KFZ = new Cdv_KFZ[ipoa.Length];
				ipoa.CopyTo(pout_KFZ,0);
				return pout_KFZ;
			}
			else return null;
		}

		public Cdv_Helfer[] HoleAlleHelfer()
		{
			// konvertiere Ergebnis in das korrekte Format
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.Helfer.HolenAlle(); 
			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_Helfer[] pout_Helfer = new Cdv_Helfer[ipoa.Length];
				ipoa.CopyTo(pout_Helfer,0);
				return pout_Helfer;
			}
			else return null;
		}

		public Cdv_Verbrauchsgut[] HoleAlleVerbrauchsgueter()
		{
			// konvertiere Ergebnis in das korrekte Format
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.Verbrauchsgueter.HolenAlle(); 
			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_Verbrauchsgut[] pout_VB = new Cdv_Verbrauchsgut[ipoa.Length];
				ipoa.CopyTo(pout_VB,0);
				return pout_VB;
			}
			else return null;
		}

		public Cdv_Material[] HoleAlleMaterialien()
		{
			// konvertiere Ergebnis in das korrekte Format
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.Material.HolenAlle(); 
			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				Cdv_Material[] pout_Material = new Cdv_Material[ipoa.Length];
				ipoa.CopyTo(pout_Material,0);
				return pout_Material;
			}
			else return null;
		}

		public int[] HoleAlleMaterialIDs(int pin_BesitzerID)
		{
			// konvertiere Ergebnis in das korrekte Format
			pELS.DV.Server.Interfaces.IPelsObject[] ipoa = _ObjektManager.Material.HolenAlle(); 

			if(!(ipoa == null) || (ipoa.Length == 0))
			{
				ArrayList tmpMaterialien = new ArrayList();
				foreach(Cdv_Material Material in ipoa)
				{
					if(Material.AktuellerBesitzerKraftID == pin_BesitzerID)
						tmpMaterialien.Add(Material.ID);
				}
				int[] pout_MaterialID = new int[tmpMaterialien.Count];
				tmpMaterialien.CopyTo(pout_MaterialID,0);
				return pout_MaterialID;
			}
			else return null;
		}



		#endregion
	}
}
