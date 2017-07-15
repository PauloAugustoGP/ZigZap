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
                Instantiate(gem, transform).transform.position = line2[1].position;
            }
            else
            {
                Instantiate(coin, transform).transform.position = line1[Random.Range(0, line1.Count)].position;
                Instantiate(coin, transform).transform.position = line2[Random.Range(0, line2.Count)].position;
                Instantiate(coin, transform).transform.position = line3[Random.Range(0, line3.Count)].position;
            }
        }
    }

    void Update ()
    {
        if (done) Destroy(gameObject, 60);
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
