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
			saintPaulMap._tileSize = (float) 0.1;
			saintPaulMap._westTiles = 2;
			saintPaulMap._northTiles = 1;
			saintPaulMap._eastTiles = 2;
			saintPaulMap._southTiles = 2;
			mapLocationList.Add(saintPaulMap);

			MapLocation seattleMap = new MapLocation();
			seattleMap._latLongString = "47.6038, -122.3301";
			seattleMap._zoom = 13;
			seattleMap._tileSize = (float) 0.1;
			seattleMap._westTiles = 2;
			seattleMap._northTiles = 5;
			seattleMap._eastTiles = 2;
			seattleMap._southTiles = 3;
			mapLocationList.Add(seattleMap);

			MapLocation minneapolisMap = new MapLocation();
			minneapolisMap._latLongString = "44.9773, -93.2655";
			minneapolisMap._zoom = 13;
			minneapolisMap._tileSize = (float) 0.1;
			minneapolisMap._westTiles = 1;
			minneapolisMap._northTiles = 2;
			minneapolisMap._eastTiles = 2;
			minneapolisMap._southTiles = 3;
			mapLocationList.Add(minneapolisMap);

			MapLocation detroitMap = new MapLocation();
			detroitMap._latLongString = "42.3487, -83.0567";
			detroitMap._zoom = 13;
			detroitMap._tileSize = (float) 0.1;
			detroitMap._westTiles = 2;
			detroitMap._northTiles = 4;
			detroitMap._eastTiles = 3;
			detroitMap._southTiles = 2;
			mapLocationList.Add(detroitMap);

			return mapLocationList;
		}
	}
}

