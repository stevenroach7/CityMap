namespace CityMap.scripts
{
	using System;
	using System.Collections.Generic;

	public class MapLocationManager
	{
		public List<MapLocation> _mapLocationList;

		public MapLocationManager ()
		{
			_mapLocationList = createMapLocationList ();
		}

		private List<MapLocation> createMapLocationList() {

			List<MapLocation> mapLocationList = new List<MapLocation>();

			MapLocation saintPaulMap = new MapLocation();
			saintPaulMap._latLongString = "44.9504, -93.1015";
			saintPaulMap._zoom = 13;
			saintPaulMap._tileSize = 0.1;
			saintPaulMap._westTiles = 2;
			saintPaulMap._northTiles = 1;
			saintPaulMap._eastTiles = 2;
			saintPaulMap._southTiles = 2;
			mapLocationList.Add(saintPaulMap);

			MapLocation seattleMap = new MapLocation();
			seattleMap._latLongString = "47.6038, -122.3301";
			seattleMap._zoom = 13;
			seattleMap._tileSize = 0.1;
			seattleMap._westTiles = 2;
			seattleMap._northTiles = 5;
			seattleMap._eastTiles = 2;
			seattleMap._southTiles = 3;
			mapLocationList.Add(seattleMap);

			return mapLocationList;
		}
	}
}

