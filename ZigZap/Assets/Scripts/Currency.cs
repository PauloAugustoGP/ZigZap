using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField]int type;

    float angle;
    float pos;

    bool up;
	
	void Update ()
    {
        angle += 1;
        if (angle == 361) angle = 1;

        if (up)
            pos += 0.01f;
        else
            pos -= 0.01f;

        if (pos == 0.04f) up = false;
        else if (pos == -0.04f) up = true;

        if(type == 0)
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + angle, transform.rotation.z);
        else
            transform.rotation = Quaternion.Euler(90, transform.rotation.y + angle, 0);

        transform.position = new Vector3(transform.position.x, transform.position.y + pos, transform.position.z);
	}

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "Player")
        {
            if(type == 0)
                c.GetComponent<Player>().AddCoin();
            else
                c.GetComponent<Player>().AddGem();

            Destroy(gameObject);
        }
    }
}
