using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using Facebook.Unity;
public class GASetup : MonoBehaviour
{
    private void Awake()
    {
        FB.Init();
    }
    private void Start()
    {
        GameAnalytics.Initialize();
    }

    public void roomUnlocked(Transform t)
    {
        GameAnalytics.NewDesignEvent("Room Unlocked: " + t.name);
        print("SECTION DATA SENT TO _GAME ANALYTICS_");
    }
    public void LevelComplete(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, level.ToString("D4"));
        print("LEVEL COMPLETE DATA SENT TO _GAME ANALYTICS_");
    }
}
