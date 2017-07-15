using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int distance;
    int coins;
    int gems;

    float maxSpeed;
    public float speed;
    float aceleration;
    int orientation;

    float angle;

    float timeIncrease;

    Vector2 touchStart;
    float minDistanceY;

    bool jumping;
    public bool dead;

    Rigidbody rb;
    
	void Start ()
    {
        distance = 0;
        coins = 0;
        gems = 0;

        maxSpeed = 0.02f;
        speed = 0f;
        aceleration = 0.01f;
        orientation = 0;

        angle = 0;

        timeIncrease = 0;

        minDistanceY = 10f;

        rb = GetComponent<Rigidbody>();
    }
	
	void Update ()
    {
        if (transform.position.y < 0.5f)
            dead = true;

        if (!dead)
        {
            speed += aceleration * Time.deltaTime;
            if (speed > maxSpeed) speed = maxSpeed;

            timeIncrease += Time.deltaTime;
            if (timeIncrease >= 5f)
            {
                maxSpeed += 0.02f;
                timeIncrease = 0;
            }

            if (Input.touchCount > 0)
                checkTouch();

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
    }

    void checkTouch()
    {
        Touch touch = Input.GetTouch(0);

        switch(touch.phase)
        {
            case TouchPhase.Began:
                {
                    touchStart = touch.position;
                }break;

            case TouchPhase.Ended:
                {
                    float value = (new Vector3(0, touch.position.y, 0) - new Vector3(0, touchStart.y, 0)).magnitude;

                    if (value > minDistanceY)
                    {
                        if (Mathf.Sign(touch.position.y - touchStart.y) > 0f)
                            Jump();
                    }
                    else
                        Orientation();
                }break;
        }
    }

    void Jump()
    {
        if (!jumping)
        {
            rb.AddForce(new Vector3(0, 250, 0));
            jumping = true;
        }
    }

    void Orientation()
    {
        if (orientation == 0) orientation = 1;
        else orientation = 0;
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

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == "Platform")
            jumping = false;
    }

    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.name == "Platform")
            jumping = false;
    }
}
