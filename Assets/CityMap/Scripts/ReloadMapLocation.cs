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
		GameObject _minValue;

		[SerializeField]
		GameObject _maxValue;

		[SerializeField]
		GameObject _legendBar;

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
			case 1: 
				cityString = "Seattle";
				break;
			case 2:
				cityString = "Minneapolis";
				break;
			case 3: 
				cityString = "Saint Louis";
				break;
			}

			MapLocation mapLocation = MapLocationManager.Instance._mapLocationDict[cityString];
			UIDataManager.Instance.cityString = mapLocation._cityString;

			float maxHeight = mapLocation._minMaxDict["max"][UIDataManager.Instance.MonthKeys[UIDataManager.Instance.TimeIndex]]; 
			int maxHeightRoundedUp = (int) Math.Round(maxHeight / 10, MidpointRounding.AwayFromZero) * 10;
			float barYScale = maxHeightRoundedUp / 4880f;

			Transform barTransform = (Transform)_legendBar.GetComponent(typeof(Transform));
			if (barTransform != null) 
			{
				Vector3 tempScale = barTransform.localScale;
				tempScale.y = barYScale;
				barTransform.localScale = tempScale;
				Vector3 tempPosition = barTransform.position;
				tempPosition.y = tempScale.y / 2;
				barTransform.position = tempPosition;
			}
				
			TextMesh maxTextMesh = (TextMesh) _maxValue.GetComponent(typeof(TextMesh));
			if (maxTextMesh != null) 
			{
				maxTextMesh.text = maxHeightRoundedUp.ToString();
			}

			Transform maxValTransform = (Transform)_maxValue.GetComponent(typeof(Transform));
			if (maxValTransform != null && barYScale > 0) 
			{
				Vector3 tempPosition = maxValTransform.position;
				tempPosition.y = barYScale + 0.00595737704f;
				maxValTransform.position = tempPosition;
			}

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
