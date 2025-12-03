using System.Collections.Generic;
using Unity.Cinemachine;

using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Dictionary<string, CinemachineVirtualCameraBase> camerasDict = new Dictionary<string, CinemachineVirtualCameraBase>();


    private void Start()
    {
        CinemachineVirtualCameraBase[] cinemachineVirtualCameras = FindObjectsByType<CinemachineVirtualCameraBase>
            (FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var camera in cinemachineVirtualCameras)
        {
            camerasDict[camera.gameObject.name] = camera;
        }
    }

    private void OnEnable()
    {
        GameEventsManager.instance.cameraEvents.onCameraViewChange += ChangeViewCamera;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.cameraEvents.onCameraViewChange -= ChangeViewCamera; 
    }


    private void ChangeViewCamera(string cameraName)
    {
        foreach (var camera in camerasDict)
        {
            camera.Value.Priority = 0;
        }

        camerasDict[cameraName].gameObject.SetActive(true);
        camerasDict[cameraName].Priority = 10;
    }
}
