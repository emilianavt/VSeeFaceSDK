using UnityEngine;

public interface ICameraManager {
    void UpdateCameraState(Camera cam, bool active);
    void SetupCamera(Camera cam);
}