// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;

namespace Wave.OpenXR.Toolkit.Raycast
{
    public class TargetCanvas : MonoBehaviour
    {
		const string LOG_TAG = "Wave.OpenXR.Toolkit.Raycast.TargetCanvas";
		private void DEBUG(string msg) { Debug.Log(LOG_TAG + " " + gameObject.name + ", " + msg); }

		Canvas m_Canvas = null;
		private void Awake()
		{
			m_Canvas = GetComponent<Canvas>();
		}
		private void OnEnable()
		{
			DEBUG("OnEnable()");
			if (m_Canvas != null)
			{
				DEBUG("OnEnable() RegisterTargetCanvas.");
				CanvasProvider.RegisterTargetCanvas(m_Canvas);
			}
		}
		private void OnDisable()
		{
			DEBUG("OnDisable()");
			if (m_Canvas != null)
			{
				DEBUG("OnDisable() RemoveTargetCanvas.");
				CanvasProvider.RemoveTargetCanvas(m_Canvas);
			}
		}

		Canvas[] s_ChildrenCanvas = null;
		private void Update()
		{
			Canvas[] canvases = GetComponentsInChildren<Canvas>();
			if (canvases != null && canvases.Length > 0) // find children canvas
			{
				s_ChildrenCanvas = canvases;

				for (int i = 0; i < s_ChildrenCanvas.Length; i++)
					CanvasProvider.RegisterTargetCanvas(s_ChildrenCanvas[i]);

				return;
			}
			if (s_ChildrenCanvas != null && s_ChildrenCanvas.Length > 0) // remove old children canvas
			{
				for (int i = 0; i < s_ChildrenCanvas.Length; i++)
					CanvasProvider.RemoveTargetCanvas(s_ChildrenCanvas[i]);

				s_ChildrenCanvas = null;
			}
		}
	}
}