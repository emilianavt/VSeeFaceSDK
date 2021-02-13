using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSeeFace {
    public class VSF_MainCamera : MonoBehaviour
    {
        [Tooltip("When enabled, the clear color and clear flags of the camera will not be set automatically to optimize it for transparent rendering.")]
        public bool disableSetup = false;

        [HideInInspector]
        public ICameraManager cameraManager = null;
        private Camera cam;
        private bool haveRenderTexture = false;

        void GetCam() {
            if (cam == null)
                cam = GetComponent<Camera>();
        }
        
        void CheckConditions() {
            if (cam.targetTexture != null && !haveRenderTexture) {
                cam.enabled = false;
                cameraManager.UpdateCameraState(cam, false);
                haveRenderTexture = false;
                return;
            }
            if (cam.targetTexture == null && haveRenderTexture) {
                cam.enabled = true;
                cameraManager.UpdateCameraState(cam, true);
                haveRenderTexture = false;
                return;
            }
        }

        void OnDisable() {
            GetCam();
            if (cam == null || cameraManager == null)
                return;
            cam.enabled = false;
            cameraManager.UpdateCameraState(cam, false);
        }

        void OnEnable() {
            GetCam();
            if (cam == null || cameraManager == null)
                return;
            cam.enabled = true;
            cameraManager.UpdateCameraState(cam, true);
            CheckConditions();
        }

        void LateUpdate() {
            GetCam();
            if (cam == null || cameraManager == null)
                return;
            CheckConditions();
            if (disableSetup || !cam.enabled)
                return;
            cameraManager.SetupCamera(cam);
        }
    }
}