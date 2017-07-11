using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int distance;
    int coins;
    int gems;

    float speed;
    int orientation;

    float angle;

    float timeIncrease;
    
	void Start ()
    {
        distance = 0;
        coins = 0;
        gems = 0;

        speed = 0.02f;
        orientation = 0;

        angle = 0;

        timeIncrease = 0;
    }
	
	void Update ()
    {
        timeIncrease += Time.deltaTime;
        if(timeIncrease >= 5f)
        {
            speed += 0.02f;
            timeIncrease = 0;
        }

		if(Input.GetMouseButtonDown(0))
        {
            if (orientation == 0) orientation = 1;
            else orientation = 0;
        }

        angle += speed * 100;

        if (orientation == 0)
        {
            transform.rotation = Quaternion.Euler(angle, 0, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
        }
        else
        {
            transform.rotation = Quaternion.Euler(angle, -90, 0);
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        }
    }

    public void AddDistance()
    {
        distance++;
    }

    public void AddCoin()
    {
        coins++;
    }

    public void AddGem()
    {
        gems++;
    }

    public int GetDistance() { return distance; }
    public int GetCoins() { return coins; }
    public int GetGems() { return gems; }
}
