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
    private void Awake()
    {
        gm = FindObjectOfType<Sneaker.Core.GameManager>();
        data = SaveManager.Load();
        LoadGame();

    }
    public void SaveGame()
    {
        data.Level = gm.Level;
        data.totalMoney = gm.MaxMoney;
        data.currentCustomerCount = gm.currentServedCount;

        if (section2.activeSelf)
            data.isSection2Unlocked = true;

        if (section3.activeSelf)
            data.isSection3Unlocked = true;

        SaveManager.Save(data);

        print("UNIVERSAL DATA SAVED");
    }
    public void LoadGame()
    {
        gm.Level = data.Level;
        gm.MaxMoney = data.totalMoney;
        gm.currentServedCount = data.currentCustomerCount;

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
