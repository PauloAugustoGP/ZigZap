using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]Player target;
    
    [SerializeField]Transform cameraCharacter;

    [SerializeField] [Range(50, 200)] int speed;
    [SerializeField] [Range(10, 50)] int speedIdle;
    float angle = 0;

    void Update ()
    {
        transform.position = target.transform.position;

        if (target.IsRunning())
        {
            if (target.GetOrientation() == 0)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), speed * Time.deltaTime);
            else
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, -90, 0), speed * Time.deltaTime);
        }
        else
        {
            angle -= speedIdle * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, angle, 0);

            if (angle <= -360.0f) angle = 0;
        }

        cameraCharacter.LookAt(target.transform);
	}
}
