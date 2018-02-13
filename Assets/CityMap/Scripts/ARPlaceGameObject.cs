using System;
using System.Collections.Generic;

namespace UnityEngine.XR.iOS
{
	public class ARPlaceGameObject : MonoBehaviour
	{
		public Transform m_HitTransform;

		bool HitTestWithResultType(ARPoint point, ARHitTestResultType resultTypes)
		{
			List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface().HitTest(point, resultTypes);
			if (hitResults.Count > 0) {
				foreach (var hitResult in hitResults) {
					Debug.Log("Got hit!");
					m_HitTransform.position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
					m_HitTransform.rotation = UnityARMatrixOps.GetRotation(hitResult.worldTransform);
					Debug.Log(string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));
					return true;
				}
			}
			return false;
		}

		// Update is called once per frame
		void Update () {
			if (UIDataManager.Instance.isMapRepositionable && m_HitTransform != null)
			{

				ARPoint point = new ARPoint {
					x = 0.5,
					y = 0.5
				};	

				// prioritize reults types
				ARHitTestResultType[] resultTypes = {
					ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
					// if you want to use infinite planes use this:
					ARHitTestResultType.ARHitTestResultTypeExistingPlane,
					ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
					ARHitTestResultType.ARHitTestResultTypeFeaturePoint
				}; 

				foreach (ARHitTestResultType resultType in resultTypes) {
					if (HitTestWithResultType (point, resultType)) {
						Debug.Log ("Hit Test True");
						return;
					}
				}
			}
		}

	}
}

