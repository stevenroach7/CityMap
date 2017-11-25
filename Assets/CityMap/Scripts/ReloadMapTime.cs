namespace Mapbox.Examples
{
	using Mapbox.Geocoding;
	using UnityEngine.UI;
	using Mapbox.Unity.Map;
	using UnityEngine;

	public class ReloadMapTime : MonoBehaviour
	{
		Camera _camera;
		Vector3 _cameraStartPos;
		AbstractMap _map;

		[SerializeField]
		Slider _timeSlider;

		void Awake()
		{
			_camera = Camera.main;
			_cameraStartPos = _camera.transform.position;
			_map = FindObjectOfType<AbstractMap>();
			_timeSlider.onValueChanged.AddListener(Reload);
		}

		void Reload(float value)
		{
			UIDataManager.Instance.TimeIndex = (int) _timeSlider.value;
			_camera.transform.position = _cameraStartPos;
			_map.Initialize(_map.CenterLatitudeLongitude, _map.Zoom);
		}
	}
}