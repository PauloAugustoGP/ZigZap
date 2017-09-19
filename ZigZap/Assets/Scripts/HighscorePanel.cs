using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscorePanel : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int direction;

    bool changePanel = false;

	// Update is called once per frame
	void Update ()
    {
		if(changePanel)
        {
            transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime * direction),
                                             transform.position.y,
                                             transform.position.z);

            if(direction > 0 && transform.position.x >= 335.0f)
            {
                transform.position = new Vector3(335.0f,
                                             transform.position.y,
                                             transform.position.z);
                changePanel = false;
            }
            else if (direction < 0 && transform.position.x <= -335.0f)
            {
                transform.position = new Vector3(-335.0f,
                                             transform.position.y,
                                             transform.position.z);
                changePanel = false;
            }
        }
	}

    public void EnablePanel()
    {
        if (direction > 0) direction = -1;
        else direction = 1;

        changePanel = true;
    }
}
