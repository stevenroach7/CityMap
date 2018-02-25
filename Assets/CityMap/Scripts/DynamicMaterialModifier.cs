using System;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMaterialModifier : Singleton<DynamicMaterialModifier> 
{

	protected DynamicMaterialModifier() {}

	public void modifyMaterial(Dictionary<string, object> properties, Material[] materials) 
	{
		string timeString = UIDataManager.Instance.MonthKeys[UIDataManager.Instance.TimeIndex];
		string heightDataKey = UIDataManager.Instance.MonthKeys[UIDataManager.Instance.TimeIndex];
		string sideColorDataKey;
		if ((float.Parse (timeString.Substring (0, 4))) > 2005) 
		{
			sideColorDataKey = "2010-minorityPercent";
		} 
		else 
		{
			sideColorDataKey = "2000-minorityPercent";
		}

		float minDataValue = 0;
		float maxDataValue = 100;

		float dataValue = minDataValue;
		Material sideMaterial = materials[1];

		if (properties.ContainsKey(sideColorDataKey) && properties.ContainsKey(heightDataKey)) 
		{
			if (float.TryParse(properties[sideColorDataKey].ToString (), out dataValue)) 
			{
				float colorPercent = (dataValue - minDataValue) / (maxDataValue - minDataValue);
				LABColor minColorVal = new LABColor(0f, 25f, -100f);
				LABColor maxColorVal = new LABColor(100f, 0f, 0f);
				LABColor sideMaterialColor = LABColor.Lerp (maxColorVal, minColorVal, colorPercent);
				sideMaterial.SetColor ("_Color", sideMaterialColor.ToColor());
			}
		} 
		else 
		{
//			materials[0] = new Material(sideMaterial.shader);
//			// TODO: set top material to missing data material
//			materials[0].SetColor("_Color", new Color(229f / 255f, 117f / 255f, 52f / 255f, 1f));
//			materials [0].mainTexture = sideMaterial.mainTexture;
//			materials = new Material[0];
		}
	}
}

