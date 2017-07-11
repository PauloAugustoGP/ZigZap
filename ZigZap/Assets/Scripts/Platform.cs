using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    bool done;

	void Update ()
    {
        if (done) Destroy(gameObject, 10);
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            c.GetComponent<Player>().AddDistance();
            GetComponentInParent<Level>().DecreaseQuant();
            GetComponentInParent<Level>().SetupCheckpoint(transform.position);
            done = true;
        }
    }
}
