﻿using System.Collections.Generic;

namespace CityMap.scripts
{
	using System;

	public struct MapLocation
	{
		public string _cityString;
		public string _latLongString;
		public int _zoom;
		public float _tileSize;
		public int _westTiles;
		public int _northTiles;
		public int _eastTiles;
		public int _southTiles;
		public Dictionary<string, Dictionary<string, float>> _housingValueMinMaxDict;
		public Dictionary<string, Dictionary<string, float>> _minorityPercentMinMaxDict;
	}
}

