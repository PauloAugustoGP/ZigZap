using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]GameObject target;
    
	void Update ()
    {
        transform.position = new Vector3(target.transform.position.x + 2, target.transform.position.y + 4, target.transform.position.z - 3);
        transform.LookAt(target.transform);
	}
}
