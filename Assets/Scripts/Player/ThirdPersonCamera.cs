using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform lookAt;
    public Transform camTransform;
    public float distance = 9.0f;
    public float minZoom;
    public float maxZoom;

    private float currentX = 0.0f;
    private float currentY = 45.0f;

    Camera cam;

    private void Start()
    {
        camTransform = transform;
        cam = GetComponent<Camera>();
        minZoom = 3f;
        maxZoom = 75f;
    }

    private void Update()
    {
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {      
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);

        if (Input.touchSupported)
        {
            if (Input.touchCount == 2)
            {

                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);
                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                float oldTouchDistance = Vector2.Distance (tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance (tZero.position, tOne.position);

                float deltaDistance = oldTouchDistance - currentTouchDistance;
                Zoom (deltaDistance, 0.1f);
            }
        }
        else
        {

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Zoom(scroll, 15f);
        }
    }

    
    
    void Zoom(float deltaMagnitudeDiff, float speed)
    {
        distance+=deltaMagnitudeDiff*speed;
        distance=Mathf.Clamp(distance,minZoom,maxZoom);
    }
}
