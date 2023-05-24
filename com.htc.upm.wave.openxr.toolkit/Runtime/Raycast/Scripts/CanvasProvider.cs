// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System.Collections.Generic;
using UnityEngine;

namespace Wave.OpenXR.Toolkit.Raycast
{
    public static class CanvasProvider
    {
		const string LOG_TAG = "Wave.OpenXR.Toolkit.Raycast.CanvasProvider";
		private static void DEBUG(string msg) { Debug.Log(LOG_TAG + " " + msg); }

		private static List<Canvas> s_TargetCanvases = new List<Canvas>();

		public static bool RegisterTargetCanvas(Canvas canvas)
		{
			if (canvas != null && !s_TargetCanvases.Contains(canvas))
			{
				DEBUG("RegisterTargetCanvas() " + canvas.gameObject.name);
				s_TargetCanvases.Add(canvas);
				return true;
			}

			return false;
		}
		public static bool RemoveTargetCanvas(Canvas canvas)
		{
			if (canvas != null && s_TargetCanvases.Contains(canvas))
			{
				DEBUG("RemoveTargetCanvas() " + canvas.gameObject.name);
				s_TargetCanvases.Remove(canvas);
				return true;
			}

			return false;
		}
		public static Canvas[] GetTargetCanvas() { return s_TargetCanvases.ToArray(); }
	}
}