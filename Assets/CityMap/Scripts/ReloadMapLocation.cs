using Mapbox.Utils;
using System;
using Mapbox.Unity.MeshGeneration.Data;

namespace CityMap.scripts
{
	using Mapbox.Geocoding;
	using UnityEngine.UI;
	using Mapbox.Unity.Map;
	using UnityEngine;
	using System.Collections.Generic;

	public class ReloadMapLocation : MonoBehaviour
	{
//		Camera _camera;
//		Vector3 _cameraStartPos;
		AbstractMap _map;

		[SerializeField]
		Dropdown _locationDropdown;

		[SerializeField]
		Text _heightMinMaxText;

		[SerializeField]
		Text _colorMinMaxText;


		void Awake()
		{
//			_camera = Camera.main;
//			_cameraStartPos = _camera.transform.position;
			_map = FindObjectOfType<AbstractMap>();
			_locationDropdown.onValueChanged.AddListener(Reload);
		}

		void Reload(int value)
		{
//			_camera.transform.position = _cameraStartPos;

			string cityString = "";
			switch(value) { // Depends on dropdown options order
			case 0:
				cityString = "Saint Paul";
				break;
//			case 1: 
//				cityString = "Seattle";
//				break;
			case 1:
				cityString = "Minneapolis";
				break;
			}

			MapLocation mapLocation = MapLocationManager.Instance._mapLocationDict[cityString];
			UIDataManager.Instance.cityString = mapLocation._cityString;

			// Update Min Max Text 
			float maxHeight = mapLocation._housingValueMinMaxDict["max"][UIDataManager.Instance.MonthKeys[UIDataManager.Instance.TimeIndex]]; 
			float minHeight = mapLocation._housingValueMinMaxDict["min"][UIDataManager.Instance.MonthKeys[UIDataManager.Instance.TimeIndex]]; 
			_heightMinMaxText.text = "Min: " + String.Format("{0:0}", minHeight) + " Max: " + String.Format("{0:0}", maxHeight);


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

			float maxColor = mapLocation._minorityPercentMinMaxDict["max"][sideColorDataKey]; 
			float minColor = mapLocation._minorityPercentMinMaxDict["min"][sideColorDataKey]; 
			_colorMinMaxText.text = "Min: " + String.Format("{0:0.00}", minColor) + " Max: " + String.Format("{0:0.00}", maxColor);


			_map.UnityTileSize = mapLocation._tileSize;
			RangeTileProvider tileProvider = _map.TileProvider as RangeTileProvider;
			if (tileProvider != null) 
			{
				tileProvider.West = mapLocation._westTiles;
				tileProvider.North = mapLocation._northTiles;
				tileProvider.East = mapLocation._eastTiles;
				tileProvider.South = mapLocation._southTiles;
				_map.TileProvider = tileProvider;
			}
			string[] latLonSplit = mapLocation._latLongString.Split(',');
			Vector2d mapCenterLatLong = new Vector2d(double.Parse (latLonSplit[0]), double.Parse (latLonSplit[1])); 

			DynamicFeatureManager.Instance.featureDictionary = new Dictionary<GameObject, VectorFeatureUnity>();
			_map.Initialize(mapCenterLatLong, mapLocation._zoom);
		}
	}
}
