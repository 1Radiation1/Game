using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInputHandler : MonoBehaviour
{
    public Camera target;

    public float roll = 0.0f;

    float sumX = 0.0f, sumY = 0.0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X"), mouseY = Input.GetAxis("Mouse Y");

        sumX = (sumX + mouseX) % 360.0f;

        sumY += mouseY;

        if (sumY > 90.0f)
        {
            sumY = 90.0f;
        }
        else if (sumY < -90.0f)
        {
            sumY = -90.0f;
        }

        target.transform.rotation = Quaternion.Euler(0.0f, 0.0f, roll) * Quaternion.Euler(-sumY, sumX, 0.0f);
    }
}
