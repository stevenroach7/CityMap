namespace Mapbox.Examples
{
	using Mapbox.Geocoding;
	using UnityEngine.UI;
	using Mapbox.Unity.Map;
	using UnityEngine;
	using System.Collections.Generic;

	public class ReloadMap1 : MonoBehaviour
	{
		Camera _camera;
		Vector3 _cameraStartPos;
		AbstractMap _map;

//		[SerializeField]
//		ForwardGeocodeUserInput _forwardGeocoder; // TODO: Use this to switch between cities maybe?

		[SerializeField]
		Slider _timeSlider;

		void Awake()
		{
			_camera = Camera.main;
			_cameraStartPos = _camera.transform.position;
			_map = FindObjectOfType<AbstractMap>();
//			_forwardGeocoder.OnGeocoderResponse += ForwardGeocoder_OnGeocoderResponse;
			_timeSlider.onValueChanged.AddListener(Reload);
		}

		void ForwardGeocoder_OnGeocoderResponse(ForwardGeocodeResponse response)
		{
			_camera.transform.position = _cameraStartPos;
			_map.Initialize(response.Features[0].Center, (int)_timeSlider.value);
		}

		void Reload(float value)
		{
			_camera.transform.position = _cameraStartPos;

			Dictionary<string, float> uiData = new Dictionary<string, float>();
			uiData["timeIndex"] = value;

			_map.Initialize(_map.CenterLatitudeLongitude, _map.Zoom, uiData);
		}
	}
}