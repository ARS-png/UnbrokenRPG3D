using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    //не доступно из других скриптов, но доступно в инспекторе

    [SerializeField] float distance = 5;

    [SerializeField] float rotationSpeed = 2f;

    [SerializeField] float minVerticleAngle = -45;
    [SerializeField] float maxVerticleAngle = 45;

    [SerializeField] Vector2 framingOffset;

    float rotationX;
    float rotationY;
   


    private void LateUpdate()
    {
        if (followTarget == null) // чтообы после смерти персонажа не возникало ошибок
        {
            return;
        }
        
        rotationX -= rotationSpeed * Input.GetAxis("Mouse Y");
        rotationX = Mathf.Clamp(rotationX, minVerticleAngle, maxVerticleAngle);

        rotationY += rotationSpeed * Input.GetAxis("Mouse X"); // вращение вокруг оси Oy   
        //полуаем смещение курсора
        //добавляем это смещение к камере
        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0f);

        var focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);


        transform.position = focusPosition - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;

        if (Input.GetKey(KeyCode.Z)) // ну пока что вот так 
        {
            distance = Mathf.Lerp(distance,4, 1f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.X)) // ну пока что вот так 
        {
            distance = Mathf.Lerp(distance, 9, 1f * Time.deltaTime);
        }
    }

    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0); // свойство в гет
}
