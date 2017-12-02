using Mapbox.Json;
using UnityEngine;

namespace CityMap.scripts
{
	using System;
	using System.Collections.Generic;

	public class MapLocationManager : Singleton<MapLocationManager>
	{
		public Dictionary<string, MapLocation> _mapLocationDict;

		protected MapLocationManager ()
		{
			_mapLocationDict = createMapLocationDict ();
		}

		private Dictionary<string, MapLocation> createMapLocationDict() {

			Dictionary<string, MapLocation> mapLocationDict = new Dictionary<string, MapLocation>();

			MapLocation saintPaulMap = new MapLocation();
			saintPaulMap._cityString = "Saint Paul";
			saintPaulMap._latLongString = "44.9504, -93.1015";
			saintPaulMap._zoom = 13;
			saintPaulMap._tileSize = (float) 0.1;
			saintPaulMap._westTiles = 2;
			saintPaulMap._northTiles = 1;
			saintPaulMap._eastTiles = 2;
			saintPaulMap._southTiles = 2;
			saintPaulMap._minMaxDict = jsonToMinMaxDict("Assets/CityMap/Data/SaintPaulMinMax.json");
			mapLocationDict["Saint Paul"] = saintPaulMap;

			MapLocation seattleMap = new MapLocation();
			seattleMap._cityString = "Seattle";
			seattleMap._latLongString = "47.6038, -122.3301";
			seattleMap._zoom = 13;
			seattleMap._tileSize = (float) 0.1;
			seattleMap._westTiles = 2;
			seattleMap._northTiles = 5;
			seattleMap._eastTiles = 2;
			seattleMap._southTiles = 3;
			seattleMap._minMaxDict = jsonToMinMaxDict("Assets/CityMap/Data/SeattleMinMax.json");
			mapLocationDict["Seattle"] = seattleMap;


			MapLocation minneapolisMap = new MapLocation();
			minneapolisMap._cityString = "Minneapolis";
			minneapolisMap._latLongString = "44.9773, -93.2655";
			minneapolisMap._zoom = 13;
			minneapolisMap._tileSize = (float) 0.1;
			minneapolisMap._westTiles = 1;
			minneapolisMap._northTiles = 2;
			minneapolisMap._eastTiles = 2;
			minneapolisMap._southTiles = 3;
			minneapolisMap._minMaxDict = jsonToMinMaxDict("Assets/CityMap/Data/MinneapolisMinMax.json");
			mapLocationDict["Minneapolis"] = minneapolisMap;


			MapLocation detroitMap = new MapLocation();
			detroitMap._cityString = "Detroit";
			detroitMap._latLongString = "42.3487, -83.0567";
			detroitMap._zoom = 13;
			detroitMap._tileSize = (float) 0.1;
			detroitMap._westTiles = 2;
			detroitMap._northTiles = 4;
			detroitMap._eastTiles = 3;
			detroitMap._southTiles = 2;
			detroitMap._minMaxDict = jsonToMinMaxDict("Assets/CityMap/Data/DetroitMinMax.json");
			mapLocationDict["Detroit"] = detroitMap;
			return mapLocationDict;
		}


		private Dictionary<string, Dictionary<string, float>> jsonToMinMaxDict(string jsonPath) {
			string jsonText = System.IO.File.ReadAllText(jsonPath);
			return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, float>>>(jsonText);
		}




	}
}

