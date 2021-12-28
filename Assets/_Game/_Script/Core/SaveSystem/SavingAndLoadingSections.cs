using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sneaker.Control;
using Sneaker.Movement;

namespace Sneaker.Core
{
    public class SavingAndLoadingSections : MonoBehaviour
    {
        //Temp
        public Racks rack0;
        public Racks rack1;
        public ProcessingStationRack pRack0;
        public ProcessingStationRack pRack1;
        public StoreExpansion sExpansion0;
        public StoreExpansion sExpansion1;
        public StoreExpansion sExpansion2;
        public StoreExpansion sExpansion3;               
        private void Awake()
        {
            //loadGame();
        }
        public void loadGame()
        {
            rack0.MaxMoneyNeededToUnlock = 0;
            rack1.MaxMoneyNeededToUnlock = 0;
            sExpansion0.MaxMoneyNeededToUnlock = 0;
            sExpansion1.MaxMoneyNeededToUnlock = 0;
            
            if(pRack0 !=null && pRack1 != null)
            {
                pRack0.MaxMoneyNeededToUnlock = 0;
                pRack1.MaxMoneyNeededToUnlock = 0;
            }            
            
            if (sExpansion2 != null & sExpansion3 != null)
            {
                sExpansion2.MaxMoneyNeededToUnlock = 0;
                sExpansion3.MaxMoneyNeededToUnlock = 0;
            }            
        }
    }
}
