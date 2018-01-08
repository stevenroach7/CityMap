namespace Mapbox.Unity.Map
{
	using UnityEngine;
	using Mapbox.Map;

	public class RangeTileProvider : AbstractTileProvider
	{
		
		[SerializeField]
		private int _west;
		public int West 
		{
			get 
			{
				return _west;
			}
			set 
			{
				_west = value;
			}
		}
		[SerializeField]
		private int _north;
		public int North
		{
			get 
			{
				return _north;
			}
			set 
			{
				_north = value;
			}
		}
		[SerializeField]
		private int _east;
		public int East
		{
			get 
			{
				return _east;
			}
			set 
			{
				_east = value;
			}
		}
		[SerializeField]
		private int _south;
		public int South
		{
			get 
			{
				return _south;
			}
			set 
			{
				_south = value;
			}
		}

		public override void OnInitialized()
		{
			var centerTile = TileCover.CoordinateToTileId(_map.CenterLatitudeLongitude, _map.AbsoluteZoom);
			AddTile(new UnwrappedTileId(_map.AbsoluteZoom, centerTile.X, centerTile.Y));
			for (int x = (int)(centerTile.X - _west); x <= (centerTile.X + _east); x++)
			{
				for (int y = (int)(centerTile.Y - _north); y <= (centerTile.Y + _south); y++)
				{
					AddTile(new UnwrappedTileId(_map.AbsoluteZoom, x, y));
				}
			}
		}
	}
}
