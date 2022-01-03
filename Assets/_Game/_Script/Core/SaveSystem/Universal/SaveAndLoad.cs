using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    private Sneaker.Core.GameManager gm;
    public GameData data;

    public GameObject section2;
    public GameObject section3;

    public GameObject LockedSection2;
    public GameObject LockedSection3;

    public Sneaker.Core.Tutorial tutorial;
    public Sneaker.Core.SectionUnlocker unlocker;
    private void Awake()
    {
        gm = FindObjectOfType<Sneaker.Core.GameManager>();
        data = SaveManager.Load();
        //LoadGame();

    }

    public void SaveGame()
    {
        data.Level = gm.Level;
        data.totalMoney = gm.MaxMoney;
        data.currentCustomerCount = gm.currentServedCount;
        data.Section1 = unlocker.Unlocked[0];
        data.Section2 = unlocker.Unlocked[1];

        if (section2.activeSelf)
            data.isSection2Unlocked = true;

        if (section3.activeSelf)
            data.isSection3Unlocked = true;

        data.cinematic1 = tutorial.cinematic1;
        data.cinematic2 = tutorial.cinematic2;
        data.finalTutorialOver = tutorial.tutorial2Over;
        data.Tutorial1 = tutorial.CustomerServing;
        data.Tutorial2 = tutorial.FittingStationUnlocked; ;


        SaveManager.Save(data);

        print("UNIVERSAL DATA SAVED");
    }
    public void LoadGame()
    {
        gm.Level = data.Level;
        gm.MaxMoney = data.totalMoney;
        gm.currentServedCount = data.currentCustomerCount;
        tutorial.cinematic1 = data.cinematic1;
        tutorial.cinematic2 = data.cinematic2;
        unlocker.Unlocked[0] = data.Section1;
        unlocker.Unlocked[1] = data.Section2;
        tutorial.tutorial2Over = data.finalTutorialOver;
        tutorial.CustomerServing = data.Tutorial1;
        tutorial.FittingStationUnlocked = data.Tutorial2;
        if (data.isSection2Unlocked)
        {
            section2.SetActive(true);
            LockedSection2.SetActive(false);
        }

        if (data.isSection3Unlocked)
        {
            section3.SetActive(true);
            LockedSection3.SetActive(false);
        }


        print("UNIVERSAL DATA LOADED");
    }
}
