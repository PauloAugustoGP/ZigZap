using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField] List<Material> skins;
    List<int> skinEnable;

    MeshRenderer mesh;
    
	void Start ()
    {
        mesh = GetComponent<MeshRenderer>();

        int currentSkin = PlayerPrefs.GetInt("CurrentSkin");
        mesh.material = skins[currentSkin];

        PlayerPrefs.SetInt("Skin0", 1);
        skinEnable = new List<int>();

		for(int i = 0; i < skins.Count; i++)
        {
            string name = "Skin" + i;
            skinEnable.Add(PlayerPrefs.GetInt(name));
        }
	}
	
	public int GetSkinValue(int index)
    {
        return skinEnable[index];
    }

    public void GetNewSkin()
    {
        int count = 0;

        for (int i = 0; i < skinEnable.Count; i++)
        {
            if (skinEnable[i] == 0)
                count++;
        }

        if (count == 0)
            return;

        int newSkin = Random.Range(0, count);

        for (int i = 0; i < skinEnable.Count; i++)
        {
            if (skinEnable[i] == 0 && newSkin == 0)
            {
                skinEnable[i] = 1;
                PlayerPrefs.SetInt("Skin"+i, 1);
                return;
            }
            else
                newSkin--;
        }
    }

    public void EquipSkin(int index)
    {
        PlayerPrefs.SetInt("CurrentSkin", index);
        mesh.material = skins[index];
    }
}
