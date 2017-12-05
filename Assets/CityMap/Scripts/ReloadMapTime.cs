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
		Camera _camera;
		Vector3 _cameraStartPos;

		[SerializeField]
		Slider _timeSlider;

		[SerializeField]
		Text _timeLabel;

		[SerializeField]
		GameObject _minValue;

		[SerializeField]
		GameObject _maxValue;

		[SerializeField]
		GameObject _legendBar;

        private Dictionary<string, string> _timeDisplayMap;


		void Awake()
		{
			_camera = Camera.main;
			_cameraStartPos = _camera.transform.position;
			_timeSlider.onValueChanged.AddListener(Reload);
            _timeDisplayMap = CreateTimeDisplayMap();
		}

		void Reload(float value)
		{
			UIDataManager.Instance.TimeIndex = (int) _timeSlider.value;

			// Update legend
			MapLocation mapLocation = MapLocationManager.Instance._mapLocationDict[UIDataManager.Instance.cityString];

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
				
			_timeLabel.text = UIDataManager.Instance.MonthKeys[UIDataManager.Instance.TimeIndex];
            // create a dictionary that maps from 01-2017 to January 2017 and displays full month string, look at example from UIdataManager
			_camera.transform.position = _cameraStartPos;
			DynamicFeatureManager.Instance.updateMeshes();
		}

        Dictionary<string, string> CreateTimeDisplayMap()
        {
            return null;
        }
	}
}
