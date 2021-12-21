using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Clone.Core
{
    public class StoreExpansion : MonoBehaviour
    {

        private Clone.Core.GameManager gm;
        public Clone.Core.StoreExpansion NextExpansion;
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

            gm = FindObjectOfType<Clone.Core.GameManager>();
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
                if (collision.gameObject.GetComponent<Clone.Movement._PlayerMovment>().direction.magnitude <= 0)
                {
                    if (MaxMoneyNeededToUnlock >= 10 && gm.MaxMoney >= MaxMoneyNeededToUnlock && !isExpansion)
                    {
                        MaxMoneyNeededToUnlock -= MoneyReduceSpeed;
                        gm.MaxMoney -= MoneyReduceSpeed;
                    }
                }
            }
        }
    }
}
