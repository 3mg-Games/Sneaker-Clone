using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Sneaker.Core
{
    public class FittingStation : MonoBehaviour
    {
        private Sneaker.Core.GameManager gm;

        public TextMeshPro StationPrice;
        public GameObject Mirror1, Mirror2, Circle1, Circle2,MoneySymbol;
        public ParticleSystem poofEffect;
        public int MaxMoneyNeededToUnlock;
        public int MoneyReduceSpeed;

        public bool isStationOpen;

        private bool isStoreOpened;
        private void Start()
        {
            gm = FindObjectOfType<Sneaker.Core.GameManager>();

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
                FindObjectOfType<AudioManager>().source.PlayOneShot(FindObjectOfType<AudioManager>().Unlock);

                if (poofEffect != null)
                    poofEffect.gameObject.SetActive(true);

                Mirror1.SetActive(true);
                Mirror2.SetActive(true);
                Circle1.SetActive(true);
                Circle2.SetActive(true);
                MoneySymbol.SetActive(false);
                StationPrice.gameObject.SetActive(false);
                if (!gm.FittingStationList.Contains(this.gameObject))
                {
                    gm.FittingStationList.Add(this.gameObject);
                }
                isStoreOpened = true;
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.GetComponent<Sneaker.Movement._PlayerMovment>().direction.magnitude <= 0)
                {
                    if (MaxMoneyNeededToUnlock > 0 && gm.MaxMoney >= MoneyReduceSpeed && !isStationOpen)
                    {
                        MaxMoneyNeededToUnlock -= MoneyReduceSpeed;
                        gm.MaxMoney -= MoneyReduceSpeed;                        
                    }
                }
            }
        }

    }
}
