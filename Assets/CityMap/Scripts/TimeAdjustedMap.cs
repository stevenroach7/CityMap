﻿namespace Mapbox.Unity.Map
{
	using System;
	using Mapbox.Unity.Utilities;
	using Utils;
	using UnityEngine;
	using Mapbox.Map;
	using System.Collections.Generic;

	public class TimeAdjustedMap : AbstractMap
	{
		
		[SerializeField]
		int _timeIndex = 0;

		public override void Initialize(Vector2d latLon, int zoom, Dictionary<string, float> uiData)
		{
			_worldHeightFixed = false;
			_centerLatitudeLongitude = latLon;
			_zoom = zoom;

			var referenceTileRect = Conversions.TileBounds(TileCover.CoordinateToTileId(_centerLatitudeLongitude, _zoom));
			_centerMercator = referenceTileRect.Center;

			// TODO: Initialize Visualizer with Time index

			_worldRelativeScale = (float)(_unityTileSize / referenceTileRect.Size.x);
			_mapVisualizer.Initialize(this, _fileSouce, uiData);
			_tileProvider.Initialize(this);

			SendInitialized();
		}
	}
}