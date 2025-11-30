using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;

    private void LateUpdate()
    {
        transform.rotation = cam.transform.rotation;
    }
}
