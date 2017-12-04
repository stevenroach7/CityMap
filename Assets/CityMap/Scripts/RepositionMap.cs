namespace CityMap.scripts
{
	using System;
	using UnityEngine;
	using UnityEngine.UI;

	public class RepositionMap : MonoBehaviour
	{

		[SerializeField]
		Toggle _repositionMapToggle;

		void Awake()
		{
			_repositionMapToggle.onValueChanged.AddListener(Reload);
		}

		void Reload(bool value)
		{
			UIDataManager.Instance.isMapRepositionable = value;
		}
	}
}

