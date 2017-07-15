using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField]Player target;
    [SerializeField]Level level;
    [SerializeField]UI ui;

    public bool extraAd;

	public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ShowExtraLifeAd()
    {
        extraAd = true;

        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");

                
                Vector3 pos = level.GetCheckpoint();
                target.transform.position = new Vector3(pos.x, pos.y + 1f, pos.z);
                target.transform.rotation = Quaternion.identity;
                target.dead = false;

                target.speed = 0f;

                ui.extraLife = 0;

                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
