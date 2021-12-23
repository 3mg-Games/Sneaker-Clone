using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Clone.Core
{
    public class GameManager : MonoBehaviour
    {
        public int MaxMoney;
        public int PlayerClothCollectionLimit = 10;

        public bool GameplayPause;


        [Header("Leveling System")]
        public GameObject customerUI;
        public GameObject levelUpdateButton;
        public TextMeshProUGUI currentLevelCount;
        public TextMeshProUGUI lastLevelCount;
        public TextMeshProUGUI CustomerCount;
        public Slider levelSilder;

        public int currentServedCount;
        public float levelIncrementalSpeed;
        public int maxCustomerToServe;
        public int Level;
        void Start()
        {
            levelUpdateButton.SetActive(false);
        }

        void Update()
        {
            levelStatus();
            levelingSystem();
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
        }


        float levelIncrementor;

        public void levelingSystem()
        {
            CustomerCount.text = currentServedCount + " / " + maxCustomerToServe;
            levelSilder.maxValue = maxCustomerToServe;


            if(levelIncrementor <= currentServedCount)
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
            Level += 1;
            GetComponent<SectionUnlocker>().unlockSection();
            currentServedCount -= maxCustomerToServe;
            maxCustomerToServe = maxCustomerToServe * 2;
            levelUpdateButton.GetComponent<Animator>().Play("OUT");
        }
    }
}
