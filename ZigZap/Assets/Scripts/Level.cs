using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] GameObject floor;
    [SerializeField] GameObject floorInitial;

    Vector3 current;

    int quant;

    Vector3 checkpointPosition;
    int checkpointOrientation;
    int checkpoint;
    

    void Start()
    {
        quant = 0;
        checkpoint = 0;

        checkpointPosition = new Vector3(0, 0, 0);

        current = new Vector3(0f, 0f, 0f);
        Instantiate(floorInitial, transform).transform.position = current;
        quant++;

        current = new Vector3(current.x, 0f, current.z + 3f);
        Instantiate(floor, transform).transform.position = current;
        quant++;
    }
	
	void Update ()
    {
		while(quant < 20)
        {
            int rand = Random.Range(0, 100);

            if(rand <= 49)
            {
                current = new Vector3(current.x, 0f, current.z + 3f);
                Instantiate(floor, transform).transform.position = current;
            }
            else
            {
                current = new Vector3(current.x - 3, 0f, current.z);
                Instantiate(floor, transform).transform.position = current;
            }

            quant++;
        }
	}

    public void DecreaseQuant()
    {
        quant--;
    }

    public void SetupCheckpoint(Vector3 newPosition)
    {
        checkpoint++;

        if (checkpoint == 10)
        {
            checkpointPosition = newPosition;
            checkpointOrientation = GameObject.FindWithTag("Player").GetComponent<Player>().GetOrientation();
            checkpoint = 0;
        }
    }

    public Vector3 GetCheckpointPosition() { return checkpointPosition; }
    public int GetCheckpointOrientation() { return checkpointOrientation; }
}
