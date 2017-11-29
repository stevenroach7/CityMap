using System;
using UnityEngine;
using System.Collections.Generic;
using Mapbox.Unity.MeshGeneration.Data;
using Mapbox.Unity.MeshGeneration.Modifiers;
using System.Linq;

public class DynamicFeatureManager : Singleton<DynamicFeatureManager>
{

	public Dictionary<MeshData, VectorFeatureUnity> FeatureDictionary;

	protected DynamicFeatureManager ()
	{
		FeatureDictionary = new Dictionary<MeshData, VectorFeatureUnity>();
	}
		
	public void updateMeshData() 
	{
//		HeightModifier heightModifier = new HeightModifier();
		HeightModifier heightModifier = ScriptableObject.CreateInstance<HeightModifier>();
//		Debug.Log(FeatureDictionary.Count);
		foreach (MeshData md in FeatureDictionary.Keys.ToList()) {
			heightModifier.Run(FeatureDictionary[md], md);
		}
	}

}


