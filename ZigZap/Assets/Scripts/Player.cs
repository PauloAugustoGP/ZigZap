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

    //bool jumping;
    public bool dead;
    bool running;

    public int extraLife;

    Rigidbody rb;
    Vector3 forward;
    Vector3 left;
    
	void Start ()
    {
        distance = 0;
        coins = 0;
        gems = 0;

        maxSpeed = 0.0f;
        speed = 0f;
        aceleration = 1;
        orientation = 0;
        running = false;

        extraLife = 1;

        angle = 0;

        timeIncrease = 0;

        rb = GetComponent<Rigidbody>();

        forward = transform.forward.normalized;
        left = transform.right.normalized * -1.0f;
    }
	
	void Update ()
    {
        if (transform.position.y < -0.5f && !dead)
        {
            dead = true;

            int total = PlayerPrefs.GetInt("TotalCoins");
            PlayerPrefs.SetInt("TotalCoins", total + coins);
        }

        if (!dead && running)
        {
            speed += aceleration * Time.deltaTime;
            if (speed > maxSpeed) speed = maxSpeed;

            timeIncrease += Time.deltaTime;
            if (timeIncrease >= 5f)
            {
                maxSpeed += 1;
                timeIncrease = 0;
            }

            if (Input.touchCount > 0)
                CheckTouch();

            angle += speed;

            if (orientation == 0)
            {
                transform.rotation = Quaternion.Euler(angle, 0, 0);
                rb.velocity = forward * speed;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, angle);
                rb.velocity = left * speed;
            }

            rb.AddForce(Vector3.down*100);
        }
    }

    void CheckTouch()
    {
        Touch touch = Input.GetTouch(0);

        switch(touch.phase)
        {
            case TouchPhase.Ended:
                {
                    Orientation();
                }break;
        }
    }

    /*void Jump()
    {
        if (!jumping)
        {
            rb.AddForce(new Vector3(0, 250, 0));
            jumping = true;
        }
    }*/

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

    public void DoubleCoin()
    {
        coins *= 2;
    }

    public void AddGem()
    {
        gems++;
    }
    

    public void ResetPlayer(Vector3 newPosition, int newOrientation)
    {
        transform.position = new Vector3(newPosition.x, newPosition.y + 0.75f, newPosition.z);
        
        transform.rotation = Quaternion.identity;

        orientation = newOrientation;

        rb.velocity = Vector3.zero;

        dead = false;
        running = false;
        speed = 0f;

        Invoke("ResumeRun", 2.0f);
    }

    public int GetOrientation() { return orientation; }
    public int GetDistance() { return distance; }
    public int GetCoins() { return coins; }
    public int GetGems() { return gems; }
    public bool IsRunning() { return running; }

    public void StartRun()
    {
        maxSpeed = 4;
        running = true;
    }

    public void StopRun()
    {
        running = false;
    }

    public void ResumeRun()
    {
        running = true;
    }

    /*void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == "Platform")
            jumping = false;
    }*/
}
