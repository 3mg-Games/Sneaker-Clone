using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Sneaker.Core
{
    public class StoreExpansion : MonoBehaviour
    {

        private Sneaker.Core.GameManager gm;
        public Sneaker.Core.StoreExpansion NextExpansion;
        public TextMeshPro ExpansionPrice;
        public Collider[] ColliderToDestroy;
        public GameObject LockedArea, UnlockedArea;
        public GameObject MoneyIcon, Border;

        public int MaxMoneyNeededToUnlock;
        public int MoneyReduceSpeed;

        public bool isExpansion;
        private bool isExpanded;
        void Start()
        {
            if(NextExpansion !=null && !NextExpansion.isExpansion)
            {
                ExpansionPrice.gameObject.SetActive(false);
                MoneyIcon.SetActive(false);
                Border.SetActive(false);
            }

            gm = FindObjectOfType<Sneaker.Core.GameManager>();
            if (!isExpansion)
            {
                LockedArea.SetActive(true);
                UnlockedArea.SetActive(false);
            }
            if (isExpansion)
            {
                LockedArea.SetActive(false);
                UnlockedArea.SetActive(true);
                for (int i=0;i<= ColliderToDestroy.Length - 1; i++)
                {
                    Destroy(ColliderToDestroy[i]);
                }
            }
        }


        void Update()
        {
            CheckExpansionCondition();
            ExpansionPrice.text = "$" + MaxMoneyNeededToUnlock.ToString();

            
        }

        void CheckExpansionCondition()
        {

            if (NextExpansion != null && NextExpansion.isExpansion)
            {
                ExpansionPrice.gameObject.SetActive(true);
                MoneyIcon.SetActive(true);
                Border.SetActive(true);
            }
            if (MaxMoneyNeededToUnlock <= 0)
                isExpansion = true;

            if(isExpansion && !isExpanded)
            {
                FindObjectOfType<SAVE>().Save = 0;
                FindObjectOfType<AudioManager>().source.PlayOneShot(FindObjectOfType<AudioManager>().Unlock);
                FindObjectOfType<SAVE>().Save = 0;
                LockedArea.SetActive(false);
                UnlockedArea.SetActive(true);
                for (int i = 0; i <= ColliderToDestroy.Length - 1; i++)
                {
                    Destroy(ColliderToDestroy[i]);
                }
                isExpanded = true;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if(NextExpansion != null&& NextExpansion.isExpansion)
                {
                    if (collision.gameObject.GetComponent<Sneaker.Movement._PlayerMovment>().direction.magnitude <= 0)
                    {
                        if (MaxMoneyNeededToUnlock > 0 && gm.MaxMoney >= MoneyReduceSpeed && !isExpansion)
                        {
                            MaxMoneyNeededToUnlock -= MoneyReduceSpeed;
                            gm.MaxMoney -= MoneyReduceSpeed;
                            gm.MoneyCounterSpeed = MoneyReduceSpeed;
                        }
                    }
                }

                if(NextExpansion == null)
                    {
                        if (collision.gameObject.GetComponent<Sneaker.Movement._PlayerMovment>().direction.magnitude <= 0)
                        {
                            if (MaxMoneyNeededToUnlock > 0 && gm.MaxMoney >= MoneyReduceSpeed && !isExpansion)
                            {
                                MaxMoneyNeededToUnlock -= MoneyReduceSpeed;
                                gm.MaxMoney -= MoneyReduceSpeed;
                            gm.MoneyCounterSpeed = MoneyReduceSpeed;
                        }
                        }
                    }

                
            }
        }
    }
}
