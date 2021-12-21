using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Clone.Core
{
    public class FittingStation : MonoBehaviour
    {
        private Clone.Core.GameManager gm;

        public TextMeshPro StationPrice;
        public GameObject Mirror1, Mirror2, Circle1, Circle2,MoneySymbol;
        public ParticleSystem poofEffect;
        public int MaxMoneyNeededToUnlock;
        public int MoneyReduceSpeed;

        public bool isStationOpen;

        private bool isStoreOpened;
        private void Start()
        {
            gm = FindObjectOfType<Clone.Core.GameManager>();

            if (!isStationOpen)
            {
                if(poofEffect != null)
                    poofEffect.gameObject.SetActive(false);

                Mirror1.SetActive(false);
                Mirror2.SetActive(false);
                Circle1.SetActive(false);
                Circle2.SetActive(false);
                MoneySymbol.SetActive(true);
                StationPrice.gameObject.SetActive(true);
            }
        }
        private void Update()
        {
            CheckStoreCondition();
            StationPrice.text = "$" + MaxMoneyNeededToUnlock.ToString();
        }


        void CheckStoreCondition()
        {
            if (MaxMoneyNeededToUnlock <= 0)
                isStationOpen = true;

            if(!isStoreOpened && isStationOpen)
            {
                if (poofEffect != null)
                    poofEffect.gameObject.SetActive(true);

                Mirror1.SetActive(true);
                Mirror2.SetActive(true);
                Circle1.SetActive(true);
                Circle2.SetActive(true);
                MoneySymbol.SetActive(false);
                StationPrice.gameObject.SetActive(false);
                isStoreOpened = true;
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.GetComponent<Clone.Movement._PlayerMovment>().direction.magnitude <= 0)
                {
                    if (MaxMoneyNeededToUnlock >= 10 && gm.MaxMoney >= MaxMoneyNeededToUnlock && !isStationOpen)
                    {
                        MaxMoneyNeededToUnlock -= MoneyReduceSpeed;
                        gm.MaxMoney -= MoneyReduceSpeed;                        
                    }
                }
            }
        }

    }
}
