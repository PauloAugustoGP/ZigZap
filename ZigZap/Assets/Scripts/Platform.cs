using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    bool done;

    [SerializeField]GameObject coin;
    [SerializeField]GameObject gem;

    [SerializeField]List<Transform> line1;
    [SerializeField]List<Transform> line2;
    [SerializeField]List<Transform> line3;

    void Start()
    {
        int rand = Random.Range(0, 100);

        if(rand < 50)
        {
            int currency = Random.Range(0, 100);
            
            if(currency < 2)
            {
                Instantiate(gem, line2[1].position, Quaternion.Euler(90, 0, 0)).transform.parent = transform;
            }
            else
            {
                Instantiate(coin, line1[Random.Range(0, line1.Count)].position, Quaternion.identity).transform.parent = transform;
                Instantiate(coin, line1[Random.Range(0, line1.Count)].position, Quaternion.identity).transform.parent = transform;
                Instantiate(coin, line1[Random.Range(0, line1.Count)].position, Quaternion.identity).transform.parent = transform;
            }
        }
    }

    void Update ()
    {
        if (done) Destroy(gameObject, 60);
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player" && !done)
        {
            c.GetComponent<Player>().AddDistance();
            GetComponentInParent<Level>().DecreaseQuant();
            GetComponentInParent<Level>().SetupCheckpoint(transform.position);
            done = true;
        }
    }
}
