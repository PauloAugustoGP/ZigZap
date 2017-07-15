using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]GameObject gamePanel;
    [SerializeField]Text distance;
    [SerializeField]Text coins;
    [SerializeField]Text gems;

    [SerializeField]GameObject adsPanel;
    [SerializeField]Text counter;
    [SerializeField]Image counterImage;

    [SerializeField]Player target;

    [SerializeField]Manager manager;

    float timer = 5;
    public int extraLife = 1;
    
	void Update ()
    {
        distance.text = "" + target.GetDistance();
        coins.text = "" + target.GetCoins();
        gems.text = "" + target.GetGems();
        Debug.Log(extraLife);
        if(target.dead)
        {
            if (extraLife == 1)
            {
                gamePanel.SetActive(false);
                adsPanel.SetActive(true);

                timer -= Time.deltaTime;
                counter.text = "" + (int)timer;

                counterImage.fillAmount = timer / 5;

                if (manager.extraAd)
                {
                    timer = 0;

                    gamePanel.SetActive(true);
                    adsPanel.SetActive(false);
                }
                else
                {
                    if (timer <= 0)
                    {
                        manager.ChangeScene("Menu");
                    }
                }
            }
            else
            {
                manager.ChangeScene("Menu");
            }
        }
	}
}
