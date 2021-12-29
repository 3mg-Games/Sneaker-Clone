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

        [Header("Level Up")]
        public GameObject LevelUpBannaer;
        public GameObject cash;
        public TextMeshProUGUI LevelCount;
        public TextMeshProUGUI MoneyAdded;

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
            LevelUpBannaer.SetActive(false);
            bonusUI.SetActive(false);
            lockedSectionIcon.SetActive(false);
            maxCustomerToServe = MaxCustomerCount();
        }

        void Update()
        {
            customUI();
            moneyCounter();
            levelStatus();
            stackingClothLimit();
            if (!GameplayPause)
                levelingSystem();
        }

        public void stackingClothLimit()
        {
            if(Level ==0 || Level <= 0)
            {
                PlayerClothCollectionLimit = 8;
            }
            if (Level > 0)
            {
                PlayerClothCollectionLimit = 40;
            }
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
                currentLevelCount.text = (Level + 2).ToString();
                lastLevelCount.text = (Level + 1).ToString();
            }
            if (Level >= 1)
            {
                currentLevelCount.text = (Level + 2).ToString();
                lastLevelCount.text = (Level+1).ToString();
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
                cash.SetActive(false);
                LevelUpBannaer.SetActive(true);
                MoneyAdded.text = "Unlocked";
                LevelCount.text = (Level + 1).ToString();
            }
            else
            {
                bonusUI.GetComponent<bonusUI>().amount = bonus();
                bonusUI.SetActive(true);
                LevelUpBannaer.SetActive(true);
                cash.SetActive(true);
                MoneyAdded.text = "+"+bonus().ToString();
                LevelCount.text = (Level + 1).ToString();
            }
            
            Level += 1;            
            GetComponent<SectionUnlocker>().unlockSection();
            currentServedCount -= maxCustomerToServe;
            maxCustomerToServe = MaxCustomerCount();
            FindObjectOfType<Tutorial>().resetUI();
            levelUpdateButton.GetComponent<Animator>().Play("OUT");
            
        }
        int custCount;
        public int MaxCustomerCount()
        {
            if (Level == 0)
                custCount = 11;
            if (Level == 1)
                custCount = 48;
            if (Level == 2)
                custCount = 80;
            if (Level == 3)
                custCount = 90;
            if (Level == 4)
                custCount = 60;
            if (Level == 5)
                custCount = 70;
            if (Level == 6)
                custCount = 60;
            if (Level == 7)
                custCount = 50;
            if (Level == 8)
                custCount = 40;
            if (Level == 9)
                custCount = 60;
            if (Level == 10)
                custCount = 80;
            if (Level >= 11)
                custCount = 110;

            return custCount;
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
