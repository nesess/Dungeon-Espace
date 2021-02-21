using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour,IUnityAdsListener
{
    string gameId = "3698289"; 
    string placement = "rewardedVideo";
    bool testMode = true;

    Player player;

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void ShowAd()
    {
        Advertisement.Show(placement);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
                // award 100 gems to player
                Debug.Log("100 gems awarded to you");
                player.increaseDiamondAmount(100);
                UIManager.Instance.OpenShop(player.getGemCount());
                break;
            case ShowResult.Skipped:
                Debug.Log("You skipped ad no gems awarded to you");
                break;
            case ShowResult.Failed:
                Debug.Log("Ad video failed to play");
                break;
        }
    }

    public void OnUnityAdsDidError(string message)
    {



    }

    public void OnUnityAdsDidStart(string placementId)
    {



    }

    public void OnUnityAdsReady(string placementId)
    {



    }

}
