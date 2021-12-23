using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Clone.Core
{    

    public class Racks : MonoBehaviour
    {
        public int ClothIDNumber;

        public List<GameObject> RackCloths = new List<GameObject>();
        public TextMeshPro RackPrice;

        public Transform ClothStackingOnRack;
        public GameObject StoreGraphics;
        public GameObject PurchaseGraphics;
        public GameObject ClothToGivePlayer;

        public float giveItemToPlayer;
        public float waitTimer = 1;
        public float YPosAfterUnlock;
        
        public int MoneyReduceSpeed;
        public int MaxMoneyNeededToUnlock;

        public bool isPlayerNear;
        public bool isRackClosed;
        public bool isRackOpen;
        public bool Unlock;
        public bool isPlayerOnClosedRack;

        [Header("Logic For Store")]
        public bool Rack0;        
        public bool Rack1;        
        public bool Rack2;        
        public bool Rack3;


        private Clone.Control.PlayerControl playerControl;
        private Clone.Core.PlayerStackingAndUnstacking PlayerStackingAndUnstacking;
        private Clone.Core.GameManager gm;
        private Clone.Core._LevelManager levelManager;
        
        void Start()
        {
            gm = FindObjectOfType<Clone.Core.GameManager>();
            playerControl = FindObjectOfType<Clone.Control.PlayerControl>();
            PlayerStackingAndUnstacking = FindObjectOfType<Clone.Core.PlayerStackingAndUnstacking>();
            levelManager = GetComponentInParent<Clone.Core._LevelManager>();

            giveItemToPlayer = waitTimer;
            if (isRackClosed)
            {
                RackPrice.text = "$" + MaxMoneyNeededToUnlock.ToString();
                StoreGraphics.SetActive(false);
                PurchaseGraphics.SetActive(true);
            }
        }

        
        void Update()
        {
            if (!isRackClosed && !isRackOpen)
                whenPlayerIsOnRack();

            if (MaxMoneyNeededToUnlock <= 0 && isRackClosed)
                isRackClosed = false;

            if (MaxMoneyNeededToUnlock <= 0 && !Unlock)
                Unlock = true;

            if (Unlock)
                MaxMoneyNeededToUnlock = 0;
            
            if (!isRackClosed && isRackOpen)
                GiveItemToPlayer();

            removeClothFromRack();
            CheckStore();
        }

        public void CheckStore()
        {
            levelManager.isRackOpen_0 = Rack0;
            levelManager.isRackOpen_1 = Rack1;
            levelManager.isRackOpen_2 = Rack2;
            levelManager.isRackOpen_3 = Rack3;
        }

        public void whenPlayerIsOnRack()
        {
            RackPrice.gameObject.SetActive(false);
            StoreGraphics.SetActive(true);
            //PurchaseGraphics.GetComponent<MeshRenderer>().enabled = false;
            Destroy(PurchaseGraphics);
            //transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
            //transform.localPosition = new Vector3(transform.localPosition.x, YPosAfterUnlock, transform.localPosition.z);
            CheckStore();
            isRackOpen = true;
        }
        public void GiveItemToPlayer()
        {

            if (isPlayerNear && PlayerStackingAndUnstacking.ClothObject.Count <= gm.PlayerClothCollectionLimit-1 && RackCloths.Count >0)
            {
                giveItemToPlayer -= Time.deltaTime;
                if (giveItemToPlayer <= 0)
                {
                    playerControl.GetComponent<Clone.Core.PlayerStackingAndUnstacking>().addClothToStack(ClothToGivePlayer, ClothIDNumber);                    
                    isPlayerNear = false;
                    Destroy(RackCloths[0].gameObject);
                    giveItemToPlayer = waitTimer;                   
                }
            }
        }

        void removeClothFromRack()
        {
            foreach (Transform T in ClothStackingOnRack.transform)
            {
                if (!RackCloths.Contains(T.gameObject))
                {
                    RackCloths.Add(T.gameObject);
                }
            }
            if (RackCloths.Count > 0)
            {
                for(int i = 0; i <= RackCloths.Count - 1; i++)
                {
                    if (RackCloths[i] == null)
                        RackCloths.Remove(RackCloths[i]);
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if(collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Clone.Movement._PlayerMovment>().direction.magnitude <= 0)
            {
                isPlayerNear = true;
            }



            if (collision.gameObject.CompareTag("Player") && isRackClosed && collision.gameObject.GetComponent<Clone.Movement._PlayerMovment>().direction.magnitude <= 0)
            {
                isPlayerOnClosedRack = true;
                if(gm.MaxMoney >= MaxMoneyNeededToUnlock && MaxMoneyNeededToUnlock > 0)
                {
                    MaxMoneyNeededToUnlock -= MoneyReduceSpeed;
                    gm.MaxMoney -= MoneyReduceSpeed;
                    RackPrice.text = "$" + MaxMoneyNeededToUnlock.ToString();
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (isPlayerNear)
                isPlayerNear = false;

            if (isPlayerOnClosedRack)
                isPlayerOnClosedRack = false;
        }

    }
}
