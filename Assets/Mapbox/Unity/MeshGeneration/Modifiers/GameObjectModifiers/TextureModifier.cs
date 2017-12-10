using System.Collections.Generic;

namespace Mapbox.Unity.MeshGeneration.Modifiers
{
    using UnityEngine;
    using Mapbox.Unity.MeshGeneration.Components;
	using Mapbox.Unity.MeshGeneration.Data;
	using CityMap.scripts;

	/// <summary>
	/// Texture Modifier is a basic modifier which simply adds a TextureSelector script to the features.
	/// Logic is all pushed into this TextureSelector mono behaviour to make it's easier to change it in runtime.
	/// </summary>
	[CreateAssetMenu(menuName = "Mapbox/Modifiers/Texture Modifier")]
    public class TextureModifier : GameObjectModifier
    {
        [SerializeField]
        private bool _textureTop;
        [SerializeField]
        private bool _useSatelliteTexture;
        [SerializeField]
        private Material[] _topMaterials;

		[SerializeField]
        private bool _textureSides;
        [SerializeField]
        private Material[] _sideMaterials;

		[SerializeField]
		private float _minDataValue = 0;


		[SerializeField]
		private float _maxDataValue = 235;

		[SerializeField]
		private string _cityString = "";



		public override void Run(FeatureBehaviour fb, UnityTile tile)
        {
			var _meshRenderer = fb.gameObject.AddComponent<MeshRenderer>();

			string timeString = UIDataManager.Instance.MonthKeys[UIDataManager.Instance.TimeIndex];
			string sideColorDataKey;
			if ((float.Parse(timeString.Substring (0, 4))) > 2005)
			{
				sideColorDataKey = "2010-minorityPercent";
			} 
			else 
			{
				sideColorDataKey = "2000-minorityPercent";
			}

			if (_textureSides && _sideMaterials.Length > 0)
			{
				float dataValue = _minDataValue;
				Material sideMaterial = _sideMaterials[Random.Range (0, _sideMaterials.Length)];
				Material topMaterial = new Material(sideMaterial.shader);

				_cityString = UIDataManager.Instance.cityString;

				if ((fb.Data.Properties.ContainsKey("City")) && (fb.Data.Properties ["City"].Equals(_cityString))) 
				{

					Dictionary<string, Dictionary<string, float>> minMaxDict = MapLocationManager.Instance._mapLocationDict[UIDataManager.Instance.cityString]._minorityPercentMinMaxDict;
					_minDataValue = 0;
					_maxDataValue = 100;

					if (fb.Data.Properties.ContainsKey(sideColorDataKey)) 
					{
						if (float.TryParse (fb.Data.Properties [sideColorDataKey].ToString (), out dataValue)) 
						{
							float colorPercent = (dataValue - _minDataValue) / (_maxDataValue - _minDataValue);
							LABColor minColorVal = new LABColor(0f, 25f, -100f);
							LABColor maxColorVal = new LABColor (100f, 0f, 0f);
							LABColor sideMaterialColor = LABColor.Lerp(maxColorVal, minColorVal, colorPercent);
							sideMaterial.SetColor ("_Color", sideMaterialColor.ToColor());

                            // TODO: Add top material color
//                            Color topMaterialColor;
//                            if (colorPercent <= 0.5)
//                            {
//                                topMaterialColor = new Color(0f, 0.75f, 0f, 1f);
//                            }
//                            else
//                            {
//                                topMaterialColor = new Color(0.75f, 0f, 0f, 0f);
//                            }
//
//                            topMaterial.SetColor("_Color", topMaterialColor);
                        }
					} 
					else 
					{
						topMaterial.SetColor("_Color", Color.red);
					}

				} else 
				{
					sideMaterial.SetColor("_Color", Color.clear);
                    //topMaterial.SetColor("_Color", Color.clear);
                }
				_meshRenderer.materials = new Material[2]
				{
				topMaterial,
				sideMaterial
				};
			}
			else if (_textureTop)
			{
				_meshRenderer.materials = new Material[1]
			   {
				_topMaterials[Random.Range(0, _topMaterials.Length)]
			   };
			}

			if (_useSatelliteTexture && fb.Data.Properties.ContainsKey(sideColorDataKey))
			{
				var _tile = fb.gameObject.GetComponent<UnityTile>();
				var t = fb.transform;
				while (_tile == null && t.parent != null)
				{
					t = t.parent;
					_tile = t.GetComponent<UnityTile>();
				}

				_meshRenderer.materials[0].mainTexture = _tile.GetRasterData();
				_meshRenderer.materials[0].mainTextureScale = new Vector2(1f, 1f);
			}


			//var ts = fb.gameObject.AddComponent<TextureSelector>();
   //         ts.Initialize(fb, _textureTop, _useSatelliteTexture, _topMaterials, _textureSides, _sideMaterials);
        }
    }
}
