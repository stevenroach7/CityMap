using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityMap.scripts
{
	public class RepositionMapManager : Singleton<RepositionMapManager> 
	{

		[SerializeField]
		public Text moveDeviceAroundText;

		[SerializeField]
		public Button placeMapButton;

		[SerializeField]
		Toggle repositionMapToggle;

		protected RepositionMapManager() 
		{
			 
		}

		void Awake() {
			placeMapButton.onClick.AddListener(onPlaceMapButtonPressed);	
			repositionMapToggle.onValueChanged.AddListener(reloadToggle);
		}
			
		private void reloadToggle(bool value)
		{
			UIDataManager.Instance.isMapRepositionable = value;
		}

		private void onPlaceMapButtonPressed() {
			UIDataManager.Instance.isMapRepositionable = false;
			placeMapButton.gameObject.SetActive(false);
			repositionMapToggle.gameObject.SetActive(true);
		}

	}
}
