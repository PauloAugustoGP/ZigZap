using UnityEngine;
using System.Collections;

public class AccelerometerInput : MonoBehaviour
{
    void Update()
    {
        Vector3 newLocation;

        if (GetComponent<Player>().GetOrientation() == 0)
            newLocation = new Vector3(transform.position.x + Input.acceleration.x * 5, transform.position.y, transform.position.z);
        else
            newLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z + Input.acceleration.x * 5);

        transform.position = Vector3.Lerp(transform.position, newLocation, 5 * Time.deltaTime);
    }
}