using System.Linq;

namespace CityMap.scripts
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using Mapbox.Unity.MeshGeneration.Data;

	public class DynamicFeatureManager : Singleton<DynamicFeatureManager> 
	{

		public Dictionary<GameObject, VectorFeatureUnity> featureDictionary;

		protected DynamicFeatureManager ()
		{
			featureDictionary = new Dictionary<GameObject, VectorFeatureUnity>();
		}

		public void updateMeshes() {
			List<GameObject> gameObjectList = featureDictionary.Keys.ToList();
			for (int i = 0; i < gameObjectList.Count; i++) 
			{
				updateMesh(gameObjectList[i], featureDictionary[gameObjectList[i]]);
			}
		}
			
		private void updateMesh(GameObject go, VectorFeatureUnity feature) {

			string cityString = UIDataManager.Instance.cityString;
			if ((feature.Properties.ContainsKey ("City")) && (feature.Properties ["City"].Equals(cityString))) {

				// Update Heights
				float hf = 0;
				float _scale = 0.00002044166f;
				float _heightMultiplier = 10;
				float minHeight = 0;

				string heightDataKey = UIDataManager.Instance.MonthKeys[UIDataManager.Instance.TimeIndex];
				if (feature.Properties.ContainsKey(heightDataKey))
				{
					if (float.TryParse(feature.Properties[heightDataKey].ToString(), out hf))
					{
						hf *= _scale;
						hf *= _heightMultiplier;
						if (feature.Properties.ContainsKey("min_height"))
						{
							minHeight = float.Parse(feature.Properties["min_height"].ToString()) * _scale;
							hf -= minHeight;
						}
					}
				}
				
				Mesh mesh = go.GetComponent<MeshFilter>().mesh;
				Vector3[] vertices = mesh.vertices;

				for (int i = 0; i < vertices.Length; i++)
				{
					if (vertices [i].y > minHeight) // Only recalculate height if vertex isn't on ground 
					{
						vertices [i] = new Vector3 (vertices [i].x, hf, vertices [i].z);
					}
				}
				mesh.vertices = vertices;
				mesh.RecalculateBounds();

				// Update Materials
				MeshRenderer meshRenderer = go.GetComponent<MeshRenderer> ();
				DynamicMaterialModifier.Instance.modifyMaterial(feature.Properties, meshRenderer.materials);
			}
		}
	}
}

