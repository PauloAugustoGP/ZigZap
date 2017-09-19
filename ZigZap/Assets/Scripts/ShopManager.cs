using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject lootBoxPanel;
    [SerializeField] GameObject skinsPanel;
    [SerializeField] List<Button> avaiableSkins;

    [SerializeField] Text totalCoins;

    [SerializeField] SkinManager player;

    int total;

    void Start()
    {
        total = PlayerPrefs.GetInt("TotalCoins");
    }

    void Update ()
    {
        totalCoins.text = "" + total;

        for(int i = 0; i< avaiableSkins.Count; i++)
        {
            if (player.GetSkinValue(i) == 0 || i == PlayerPrefs.GetInt("CurrentSkin"))
                avaiableSkins[i].interactable = false;
            else
                avaiableSkins[i].interactable = true;
        }
	}

    public void BuySkin()
    {
        if(total > 1000)
        {
            total -= 1000;
            PlayerPrefs.SetInt("TotalCoins", total);

            player.GetNewSkin();
        }
    }

    public void BuyDiscount()
    {
        if (total > 900)
        {
            total -= 900;
            PlayerPrefs.SetInt("TotalCoins", total);

            GameObject.Find("GameManager").GetComponent<Manager>().ShowBonusCoinAd();

            player.GetNewSkin();
        }
    }

    public void ShowLootPanel()
    {
        lootBoxPanel.SetActive(true);
        skinsPanel.SetActive(false);
    }

    public void ShowSkinPanel()
    {
        lootBoxPanel.SetActive(false);
        skinsPanel.SetActive(true);
    }

    public void EquipSkin(int index)
    {
        player.EquipSkin(index);
    }
}
