namespace CityMap.scripts
{
	using Mapbox.Geocoding;
	using UnityEngine.UI;
	using Mapbox.Unity.Map;
	using UnityEngine;
	using System;
    using System.Collections.Generic;

    public class ReloadMapTime : MonoBehaviour
	{
//		Camera _camera;
//		Vector3 _cameraStartPos;

		[SerializeField]
		Slider _timeSlider;

		[SerializeField]
		Text _timeLabel;

		[SerializeField]
		Text _heightMinMaxText;

		[SerializeField]
		Text _colorMinMaxText;

		[SerializeField]
		Text _colorDataSourceText;

        private Dictionary<string, string> _timeDisplayMap;


		void Awake()
		{
//			_camera = Camera.main;
//			_cameraStartPos = _camera.transform.position;
			_timeSlider.onValueChanged.AddListener(Reload);
		}

		void Reload(float value)
		{
			UIDataManager.Instance.TimeIndex = (int) _timeSlider.value;

			MapLocation mapLocation = MapLocationManager.Instance._mapLocationDict[UIDataManager.Instance.cityString];

			// Update Min Max Text 
			float maxHeight = mapLocation._housingValueMinMaxDict["max"][UIDataManager.Instance.MonthKeys[UIDataManager.Instance.TimeIndex]]; 
			float minHeight = mapLocation._housingValueMinMaxDict["min"][UIDataManager.Instance.MonthKeys[UIDataManager.Instance.TimeIndex]]; 
			_heightMinMaxText.text = "Min: " + String.Format("{0:0}", minHeight) + " Max: " + String.Format("{0:0}", maxHeight);


			string timeString = UIDataManager.Instance.MonthKeys[UIDataManager.Instance.TimeIndex];
			string sideColorDataKey;
			if ((float.Parse(timeString.Substring (0, 4))) > 2005)
			{
				sideColorDataKey = "2010-minorityPercent";
				_colorDataSourceText.text = "Data Source Census 2010";
			} 
			else 
			{
				sideColorDataKey = "2000-minorityPercent";
				_colorDataSourceText.text = "Data Source Census 2000";
			}

			float maxColor = mapLocation._minorityPercentMinMaxDict["max"][sideColorDataKey]; 
			float minColor = mapLocation._minorityPercentMinMaxDict["min"][sideColorDataKey]; 
			_colorMinMaxText.text = "Min: " + String.Format("{0:0.00}", minColor) + " Max: " + String.Format("{0:0.00}", maxColor);
				
			_timeLabel.text = UIDataManager.Instance.MonthKeys[UIDataManager.Instance.TimeIndex];
            // TODO: create a dictionary that maps from 01-2017 to January 2017 and displays full month string, look at example from UIdataManager
//			_camera.transform.position = _cameraStartPos;

			DynamicFeatureManager.Instance.updateMeshes();
		}
	}
}
