using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed, zoomSpeed;
    public float borderSize;

    public float zoomMin, zoomMax;

    void Update()
    {
        UpdateHorizontalPosition();
        UpdateVerticalPosition();
    }

    void UpdateHorizontalPosition()
    {
        Vector3 mouse = Input.mousePosition;

        if (0 < mouse.x && mouse.x < borderSize)
        {
            transform.position -= new Vector3(cameraSpeed, 0, 0);
        }
        else if (Screen.width > mouse.x && mouse.x > Screen.width - borderSize)
        {
            transform.position += new Vector3(cameraSpeed, 0, 0);
        }
        if (0 < mouse.y && mouse.y < borderSize)
        {
            transform.position -= new Vector3(0, 0, cameraSpeed);
        }
        else if (Screen.height > mouse.y && mouse.y > Screen.height - borderSize)
        {
            transform.position += new Vector3(0, 0, cameraSpeed);
        }
    }

    void UpdateVerticalPosition()
    {
        float wheelPosition = Input.GetAxis("Mouse ScrollWheel");
        float currentZoom = Mathf.Clamp(transform.position.y - zoomSpeed * wheelPosition, zoomMin, zoomMax);


        transform.position = new Vector3(transform.position.x, currentZoom, transform.position.z);
    }
}
