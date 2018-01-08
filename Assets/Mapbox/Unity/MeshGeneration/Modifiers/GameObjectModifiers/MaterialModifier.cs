namespace Mapbox.Unity.MeshGeneration.Modifiers
{
	using UnityEngine;
	using Mapbox.Unity.MeshGeneration.Components;
	using Mapbox.Unity.MeshGeneration.Data;
	using System;
	using CityMap.scripts;

	/// <summary>
	/// Texture Modifier is a basic modifier which simply adds a TextureSelector script to the features.
	/// Logic is all pushed into this TextureSelector mono behaviour to make it's easier to change it in runtime.
	/// </summary>
	[CreateAssetMenu(menuName = "Mapbox/Modifiers/Material Modifier")]
	public class MaterialModifier : GameObjectModifier
	{
		[SerializeField]
		private bool _projectMapImagery;
		[SerializeField]
		private MaterialList[] _materials;

		public override void Run(VectorEntity ve, UnityTile tile)
		{
			var min = Math.Min(_materials.Length, ve.MeshFilter.mesh.subMeshCount);
			var mats = new Material[min];

			if (!_projectMapImagery)
			{
				for (int i = 0; i < min; i++)
				{
					mats[i] = _materials[i].Materials[UnityEngine.Random.Range(0, _materials[i].Materials.Length)];
				}
			}
			else
			{
				Material sideMaterial = _materials[1].Materials[UnityEngine.Random.Range (0, _materials[1].Materials.Length)];
				Material topMaterial = new Material(sideMaterial.shader);
				mats = new Material[2] {
					topMaterial,
					sideMaterial
				};
					
				if (min > 0) 
				{
					mats[0].mainTexture = tile.GetRasterData ();
					mats[0].mainTextureScale = new Vector2 (1f, 1f);
				}
			}
			ve.MeshRenderer.materials = mats;

			if (min > 0) 
			{
				DynamicMaterialModifier.Instance.modifyMaterial(ve.Feature.Properties, ve.MeshRenderer.materials);
			}
		}
	}

	[Serializable]
	public class MaterialList
	{
		[SerializeField]
		public Material[] Materials;

		public MaterialList()
		{
			Materials = new Material[1];
		}
	}
}
