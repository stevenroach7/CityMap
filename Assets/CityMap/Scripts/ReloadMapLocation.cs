using Mapbox.Utils;

namespace CityMap.scripts
{
	using Mapbox.Geocoding;
	using UnityEngine.UI;
	using Mapbox.Unity.Map;
	using UnityEngine;
	using System.Collections.Generic;

	public class ReloadMapLocation : MonoBehaviour
	{
		Camera _camera;
		Vector3 _cameraStartPos;
		AbstractMap _map;

		[SerializeField]
		Dropdown _locationDropdown;
	
		void Awake()
		{
			_camera = Camera.main;
			_cameraStartPos = _camera.transform.position;
			_map = FindObjectOfType<AbstractMap>();
			_locationDropdown.onValueChanged.AddListener(Reload);
		}

		void Reload(int value)
		{
			_camera.transform.position = _cameraStartPos;
			MapLocation mapLocation = MapLocationManager.Instance._mapLocationList[value];

			UIDataManager.Instance.cityString = mapLocation._cityString;

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
			_map.Initialize(mapCenterLatLong, mapLocation._zoom);
		}
	}
}
