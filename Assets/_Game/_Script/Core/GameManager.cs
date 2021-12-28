using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Sneaker.Core
{
    public class GameManager : MonoBehaviour
    {
        public int MaxMoney;
        private int CurrentMoney;
        public int PlayerClothCollectionLimit = 10;

        public bool GameplayPause;
        [Header("Custom UI")]
        public TextMeshProUGUI MoneyCount;
        public TextMeshProUGUI FittingStation;
        public TextMeshProUGUI SectionCount;
        public TextMeshProUGUI ProcessingStation;

        [Header("Leveling System")]
        public GameObject customerUI;
        public GameObject levelUpdateButton;
        public GameObject bonusUI;
        public GameObject lockedSectionIcon;
        public TextMeshProUGUI currentLevelCount;
        public TextMeshProUGUI lastLevelCount;
        public TextMeshProUGUI CustomerCount;

        public Slider levelSilder;

        public int currentServedCount;
        public float levelIncrementalSpeed;
        public int maxCustomerToServe;
        public int Level;

        public List<GameObject> FittingStationList = new List<GameObject>();
        public List<GameObject> SectionsList = new List<GameObject>();
        public List<GameObject> ProcessingStationList = new List<GameObject>();
        private int bonusAmount;
        void Start()
        {
            levelUpdateButton.SetActive(false);
            bonusUI.SetActive(false);
            lockedSectionIcon.SetActive(false);
        }

        void Update()
        {
            customUI();
            moneyCounter();
            levelStatus();
            if(!GameplayPause)
                levelingSystem();
        }

        public void customUI()
        {
            FittingStation.text = FittingStationList.Count.ToString();
            SectionCount.text = SectionsList.Count.ToString() + "/" + 3;
            ProcessingStation.text = ProcessingStationList.Count.ToString() + "/" + 2;
        }
        public void moneyCounter()
        {
            MaxMoney = (int)Mathf.Clamp(MaxMoney, 0f, Mathf.Infinity);

            if (MaxMoney < CurrentMoney)
                CurrentMoney -= 1;

            if (MaxMoney > CurrentMoney)
                CurrentMoney += 1;
            MoneyCount.text = CurrentMoney.ToString();
        }

        
        public void levelStatus()
        {
            if (Level == 0)
            {
                currentLevelCount.text = (Level + 1).ToString();
                lastLevelCount.text = (Level + 1).ToString();
            }
            if (Level >= 1)
            {
                currentLevelCount.text = (Level + 1).ToString();
                lastLevelCount.text = Level.ToString();
            }

            if(currentServedCount >= maxCustomerToServe)
            {
                levelUpdateButton.SetActive(true);
            }

            if (Level == 4 || Level == 8)
                lockedSectionIcon.SetActive(true);
        }


        float levelIncrementor;

        public void levelingSystem()
        {
            
            CustomerCount.text = currentServedCount + " / " + maxCustomerToServe;
            levelSilder.maxValue = maxCustomerToServe;


            if(levelIncrementor <= currentServedCount )
            {
                levelIncrementor += levelIncrementalSpeed * Time.deltaTime;
            }
            if (currentServedCount <= levelIncrementor)
            {
                levelIncrementor -= levelIncrementalSpeed * Time.deltaTime;
            }

            levelSilder.value = levelIncrementor;
        }

        public void incresementOfCustomerServed()
        {
            currentServedCount++;
        }
        public void increseLevel()
        {
            if (Level == 4 || Level == 8)
            {
                //bonusUI.GetComponent<bonusUI>().amount = bonus();
                lockedSectionIcon.SetActive(false);
                bonusUI.SetActive(false);
            }
            else
            {
                bonusUI.GetComponent<bonusUI>().amount = bonus();
                bonusUI.SetActive(true);
            }
            
            Level += 1;
            GetComponent<SectionUnlocker>().unlockSection();
            currentServedCount -= maxCustomerToServe;
            maxCustomerToServe = maxCustomerToServe * 2;
            levelUpdateButton.GetComponent<Animator>().Play("OUT");
            
        }

        public int bonus()
        {
            if (Level == 0)
                bonusAmount = 50;
            if (Level == 1)
                bonusAmount = 50;
            if (Level == 2)
                bonusAmount = 75;
            if (Level == 3)
                bonusAmount = 100;
            if (Level == 5)
                bonusAmount = 150;
            if (Level == 6)
                bonusAmount = 200;
            if (Level == 7)
                bonusAmount = 250;
            if (Level == 9)
                bonusAmount = 1000;
            if (Level == 10)
                bonusAmount = 1000;
            if (Level == 11)
                bonusAmount = 2000;
            if (Level == 12)
                bonusAmount = 2000;
            if (Level == 13)
                bonusAmount = 3000;
            if (Level >= 14)
                bonusAmount = 3000;

            return bonusAmount;
        }
    }
}
