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

        [Header("Leveling System")]
        public GameObject customerUI;
        public TextMeshProUGUI currentLevelCount;
        public TextMeshProUGUI lastLevelCount;
        public TextMeshProUGUI CustomerCount;
        public Slider levelSilder;

        public int currentServedCount;
        public int maxCustomerToServe;
        public int Level;
        void Start()
        {

        }

        // Update is called once per frame
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
        }
        public void levelingSystem()
        {
            CustomerCount.text = currentServedCount + " / " + maxCustomerToServe;
            levelSilder.maxValue = maxCustomerToServe;
            levelSilder.value = currentServedCount;
        }

        public void incresementOfCustomerServed()
        {
            currentServedCount++;
        }
    }
}
