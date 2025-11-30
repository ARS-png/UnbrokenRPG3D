using System;
using UnityEngine;

public class CameraEvents
{
    public event Action<string> onCameraViewChange;

    public void ChangeCamera(string targetName) => onCameraViewChange?.Invoke(targetName);

}
