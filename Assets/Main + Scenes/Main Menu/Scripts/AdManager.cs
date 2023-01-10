using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements; 

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    string placement = "rewardedVideo";

  

    // Start is called before the first frame update

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("3647054", true);

       
        
    }


    public void ShowAd(string p)
    {
        Advertisement.Show(p);
    }
   

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            int previousmoney = PlayerPrefs.GetInt("Wealth");
            PlayerPrefs.SetInt("Wealth", previousmoney + 40);
        }

        else if (showResult == ShowResult.Failed)
        {
            //On no!
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
    }
}
