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
using UnityEngine.InputSystem;
using UnityEngine.XR;

namespace Wave.OpenXR.Toolkit.Raycast
{
    public class HandRaycastPointer : RaycastPointer
    {
        const string LOG_TAG = "Wave.OpenXR.Toolkit.Raycast.HandRaycastPointer";
        void DEBUG(string msg) { Debug.Log(LOG_TAG + (IsLeft ? " Left " : " Right ") + msg); }
        void INTERVAL(string msg) { if (printIntervalLog) { DEBUG(msg); } }

        #region Inspector
        public bool IsLeft = false;

        [Tooltip("To apply poses on the raycast pointer.")]
        [SerializeField]
        private bool m_UsePose = true;
        public bool UsePose { get { return m_UsePose; } set { m_UsePose = value; } }

        [SerializeField]
        private InputActionReference m_AimPose = null;
        public InputActionReference AimPose { get { return m_AimPose; } set { m_AimPose = value; } }
        bool getAimTracked(InputActionReference actionReference)
        {
            bool tracked = false;

            if (OpenXRHelper.VALIDATE(actionReference, out string value))
            {
#if USE_INPUT_SYSTEM_POSE_CONTROL // Scripting Define Symbol added by using OpenXR Plugin 1.6.0.
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.InputSystem.XR.PoseState))
#else
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.XR.OpenXR.Input.Pose))
#endif
                {
#if USE_INPUT_SYSTEM_POSE_CONTROL // Scripting Define Symbol added by using OpenXR Plugin 1.6.0.
                    tracked = actionReference.action.ReadValue<UnityEngine.InputSystem.XR.PoseState>().isTracked;
#else
                    tracked = actionReference.action.ReadValue<UnityEngine.XR.OpenXR.Input.Pose>().isTracked;
#endif
                    INTERVAL("getAimTracked(" + tracked + ")");
                }
            }
            else
            {
                INTERVAL("getAimTracked() invalid input: " + value);
            }

            return tracked;
        }
        InputTrackingState getAimTrackingState(InputActionReference actionReference)
        {
            InputTrackingState state = InputTrackingState.None;

            if (OpenXRHelper.VALIDATE(actionReference, out string value))
            {
#if USE_INPUT_SYSTEM_POSE_CONTROL // Scripting Define Symbol added by using OpenXR Plugin 1.6.0.
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.InputSystem.XR.PoseState))
#else
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.XR.OpenXR.Input.Pose))
#endif
                {
#if USE_INPUT_SYSTEM_POSE_CONTROL // Scripting Define Symbol added by using OpenXR Plugin 1.6.0.
                    state = actionReference.action.ReadValue<UnityEngine.InputSystem.XR.PoseState>().trackingState;
#else
                    state = actionReference.action.ReadValue<UnityEngine.XR.OpenXR.Input.Pose>().trackingState;
#endif
                    INTERVAL("getAimTrackingState(" + state + ")");
                }
            }
            else
            {
                INTERVAL("getAimTrackingState() invalid input: " + value);
            }

            return state;
        }
        Vector3 getAimPosition(InputActionReference actionReference)
        {
            var position = Vector3.zero;

            if (OpenXRHelper.VALIDATE(actionReference, out string value))
            {
#if USE_INPUT_SYSTEM_POSE_CONTROL // Scripting Define Symbol added by using OpenXR Plugin 1.6.0.
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.InputSystem.XR.PoseState))
#else
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.XR.OpenXR.Input.Pose))
#endif
                {
#if USE_INPUT_SYSTEM_POSE_CONTROL // Scripting Define Symbol added by using OpenXR Plugin 1.6.0.
                    position = actionReference.action.ReadValue<UnityEngine.InputSystem.XR.PoseState>().position;
#else
                    position = actionReference.action.ReadValue<UnityEngine.XR.OpenXR.Input.Pose>().position;
#endif
                    INTERVAL("getAimPosition(" + position.x.ToString() + ", " + position.y.ToString() + ", " + position.z.ToString() + ")");
                }
            }
            else
            {
                INTERVAL("getAimPosition() invalid input: " + value);
            }

            return position;
        }
        Quaternion getAimRotation(InputActionReference actionReference)
        {
            var rotation = Quaternion.identity;

            if (OpenXRHelper.VALIDATE(actionReference, out string value))
            {
#if USE_INPUT_SYSTEM_POSE_CONTROL // Scripting Define Symbol added by using OpenXR Plugin 1.6.0.
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.InputSystem.XR.PoseState))
#else
                if (actionReference.action.activeControl.valueType == typeof(UnityEngine.XR.OpenXR.Input.Pose))
#endif
                {
#if USE_INPUT_SYSTEM_POSE_CONTROL // Scripting Define Symbol added by using OpenXR Plugin 1.6.0.
                    rotation = actionReference.action.ReadValue<UnityEngine.InputSystem.XR.PoseState>().rotation;
#else
                    rotation = actionReference.action.ReadValue<UnityEngine.XR.OpenXR.Input.Pose>().rotation;
#endif
                    INTERVAL("getAimRotation(" + rotation.x.ToString() + ", " + rotation.y.ToString() + ", " + rotation.z.ToString() + ", " + rotation.w.ToString() + ")");
                }
            }
            else
            {
                INTERVAL("getAimRotation() invalid input: " + value);
            }

            return rotation;
        }

        [SerializeField]
        private InputActionReference m_PinchStrength = null;
        public InputActionReference PinchStrength { get => m_PinchStrength; set => m_PinchStrength = value; }
        float getStrength(InputActionReference actionReference)
        {
            float strength = 0;

            if (OpenXRHelper.VALIDATE(actionReference, out string value))
            {
                if (actionReference.action.activeControl.valueType == typeof(float))
                {
                    strength = actionReference.action.ReadValue<float>();
                    INTERVAL("getStrength(" + strength + ")");
                }
            }
            else
            {
                INTERVAL("getStrength() invalid input: " + value);
            }

            return strength;
        }

        [Tooltip("Pinch strength to trigger events.")]
        [SerializeField]
        private float m_PinchThreshold = .5f;
        public float PinchThreshold { get { return m_PinchThreshold; } set { m_PinchThreshold = value; } }

        [SerializeField]
        private bool m_AlwaysEnable = false;
        public bool AlwaysEnable { get { return m_AlwaysEnable; } set { m_AlwaysEnable = value; } }
        #endregion

        protected override void Update()
        {
            base.Update();

            validPose = getAimTracked(m_AimPose);
            trackingStatus = getAimTrackingState(m_AimPose);
            var origin = getAimPosition(m_AimPose);
            var rotation = getAimRotation(m_AimPose);
            strength = getStrength(m_PinchStrength);

            if (!IsInteractable()) { return; }

            if (m_UsePose)
            {
                transform.localPosition = origin;
                transform.localRotation = rotation;
            }
        }

        bool validPose = false;
        InputTrackingState trackingStatus = InputTrackingState.None;
        private bool IsInteractable()
        {
            bool enabled = RaycastSwitch.Hand.Enabled;
            bool positionTracked = ((trackingStatus & InputTrackingState.Position) != 0);
            bool rotationTracked = ((trackingStatus & InputTrackingState.Rotation) != 0);

            m_Interactable = (m_AlwaysEnable || enabled)
                && validPose
                && positionTracked
                && rotationTracked;

            if (printIntervalLog)
            {
                DEBUG("IsInteractable() m_Interactable: " + m_Interactable
                    + ", enabled: " + enabled
                    + ", validPose: " + validPose
                    + ", positionTracked: " + positionTracked
                    + ", rotationTracked: " + rotationTracked
                    + ", m_AlwaysEnable: " + m_AlwaysEnable);
            }

            return m_Interactable;
        }

        #region RaycastImpl Actions overrides
        bool eligibleForClick = false;
        float strength = 0;
        protected override bool OnDown()
        {
            if (!eligibleForClick)
            {
                bool down = strength > m_PinchThreshold;
                if (down)
                {
                    eligibleForClick = true;
                    return true;
                }
            }

            return false;
        }
        protected override bool OnHold()
        {
            bool hold = strength > m_PinchThreshold;
            if (!hold)
                eligibleForClick = false;
            return hold;
        }
        #endregion
    }
}
