using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadSection3 : MonoBehaviour
{
    public GameDataSection3 data;

    public Sneaker.Core.FittingStation FittingStation1;
    public Sneaker.Core.FittingStation FittingStation2;
    public Sneaker.Core.FittingStation FittingStation3;
    public Sneaker.Core.FittingStation FittingStation4;
    public Sneaker.Core.FittingStation FittingStation5;
    public Sneaker.Core.FittingStation FittingStation6;
    public Sneaker.Core.FittingStation FittingStation7;
    public Sneaker.Core.FittingStation FittingStation8;
    public Sneaker.Core.FittingStation FittingStation9;
    public Sneaker.Core.FittingStation FittingStation10;
    public Sneaker.Core.FittingStation FittingStation11;
    public Sneaker.Core.FittingStation FittingStation12;
    public Sneaker.Core.FittingStation FittingStation13;
    public Sneaker.Core.FittingStation FittingStation14;
    public Sneaker.Core.FittingStation FittingStation15;
    public Sneaker.Core.FittingStation FittingStation16;

    public Sneaker.Core.StoreExpansion Station1;
    public Sneaker.Core.StoreExpansion Station2;

    public Sneaker.Core.Racks racks;

    private void Awake()
    {
        data = SaveManagerSection3.Load();
        LoadGame();
    }
    public void SaveGame()
    {
        data.FittingStation1 = FittingStation1.MaxMoneyNeededToUnlock;
        data.FittingStation2 = FittingStation2.MaxMoneyNeededToUnlock;
        data.FittingStation3 = FittingStation3.MaxMoneyNeededToUnlock;
        data.FittingStation4 = FittingStation4.MaxMoneyNeededToUnlock;
        data.FittingStation5 = FittingStation5.MaxMoneyNeededToUnlock;
        data.FittingStation6 = FittingStation6.MaxMoneyNeededToUnlock;
        data.FittingStation7 = FittingStation7.MaxMoneyNeededToUnlock;
        data.FittingStation8 = FittingStation8.MaxMoneyNeededToUnlock;
        data.FittingStation9 = FittingStation9.MaxMoneyNeededToUnlock;
        data.FittingStation10 = FittingStation10.MaxMoneyNeededToUnlock;
        data.FittingStation11 = FittingStation11.MaxMoneyNeededToUnlock;
        data.FittingStation12 = FittingStation12.MaxMoneyNeededToUnlock;
        data.FittingStation13 = FittingStation13.MaxMoneyNeededToUnlock;
        data.FittingStation14 = FittingStation14.MaxMoneyNeededToUnlock;
        data.FittingStation15 = FittingStation15.MaxMoneyNeededToUnlock;
        data.FittingStation16 = FittingStation16.MaxMoneyNeededToUnlock;

        data.Station1 = Station1.MaxMoneyNeededToUnlock;
        data.Station2 = Station2.MaxMoneyNeededToUnlock;
        data.rack = racks.MaxMoneyNeededToUnlock;
        SaveManagerSection3.Save(data);
        print("SECTION 3 DATA SAVED");
    }
    public void LoadGame()
    {
        FittingStation1.MaxMoneyNeededToUnlock = data.FittingStation1;
        FittingStation2.MaxMoneyNeededToUnlock = data.FittingStation2;
        FittingStation3.MaxMoneyNeededToUnlock = data.FittingStation3;
        FittingStation4.MaxMoneyNeededToUnlock = data.FittingStation4;
        FittingStation5.MaxMoneyNeededToUnlock = data.FittingStation5;
        FittingStation6.MaxMoneyNeededToUnlock = data.FittingStation6;
        FittingStation7.MaxMoneyNeededToUnlock = data.FittingStation7;
        FittingStation8.MaxMoneyNeededToUnlock = data.FittingStation8;
        FittingStation9.MaxMoneyNeededToUnlock = data.FittingStation9;
        FittingStation10.MaxMoneyNeededToUnlock = data.FittingStation10;
        FittingStation11.MaxMoneyNeededToUnlock = data.FittingStation11;
        FittingStation12.MaxMoneyNeededToUnlock = data.FittingStation12;
        FittingStation13.MaxMoneyNeededToUnlock = data.FittingStation13;
        FittingStation14.MaxMoneyNeededToUnlock = data.FittingStation14;
        FittingStation15.MaxMoneyNeededToUnlock = data.FittingStation15;
        FittingStation16.MaxMoneyNeededToUnlock = data.FittingStation16;

        Station1.MaxMoneyNeededToUnlock = data.Station1;
        Station2.MaxMoneyNeededToUnlock = data.Station2;
        racks.MaxMoneyNeededToUnlock = data.rack;
        print("SECTION 3 DATA LOADED");
    }
}
