using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Sneaker.Core
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

        public int LimitCountForCloth;
        
        public float giveItemToPlayer;
        public float waitTimer = 1;
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
        /* public bool Rack2;        
         public bool Rack3;*/

        public int ClothXCount;
        public int limitToGiveCloth;

        private Sneaker.Control.PlayerControl playerControl;
        private Sneaker.Core.PlayerStackingAndUnstacking PlayerStackingAndUnstacking;
        private Sneaker.Core.GameManager gm;
        private Sneaker.Core._LevelManager levelManager;
        private Sneaker.Core.AudioManager audioManager;
        
        void Start()
        {
            gm = FindObjectOfType<Sneaker.Core.GameManager>();
            playerControl = FindObjectOfType<Sneaker.Control.PlayerControl>();
            PlayerStackingAndUnstacking = FindObjectOfType<Sneaker.Core.PlayerStackingAndUnstacking>();
            levelManager = GetComponentInParent<Sneaker.Core._LevelManager>();
            audioManager = FindObjectOfType<Sneaker.Core.AudioManager>();
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
            {
                GetComponent<Sneaker.Control._ClothRegenrator>().spwanOnce();
                isRackClosed = false;
            }

            if (MaxMoneyNeededToUnlock <= 0 && !Unlock)
                Unlock = true;

            if (Unlock)
                MaxMoneyNeededToUnlock = 0;
            
            if (!isRackClosed && isRackOpen)
                GiveItemToPlayer();

            removeClothFromRack();
            CheckStore();
        }
        public bool section3;
        public void CheckStore()
        {
            if (section3)
            {
                if (!levelManager.OpenRacks.Contains(ClothIDNumber))
                    levelManager.OpenRacks.Add(ClothIDNumber);                
            }
            if (!section3)
            {
                if (levelManager.isRackOpen0 && Unlock)
                {
                    /*levelManager.isRackOpen_0 = Rack0;*/
                    if (!levelManager.OpenRacks.Contains(ClothIDNumber))
                        levelManager.OpenRacks.Add(ClothIDNumber);
                }
                if (levelManager.isRackOpen1 && Unlock)
                {
                    /*levelManager.isRackOpen_1 = Rack1;*/
                    if (!levelManager.OpenRacks.Contains(ClothIDNumber))
                        levelManager.OpenRacks.Add(ClothIDNumber);
                }
            }



          
            //levelManager.isRackOpen_2 = Rack2;
            //levelManager.isRackOpen_3 = Rack3;
        }

        public void whenPlayerIsOnRack()
        {
            FindObjectOfType<Sneaker.Core.AudioManager>().source.PlayOneShot(FindObjectOfType<Sneaker.Core.AudioManager>().Unlock, 1f);
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

            if (isPlayerNear && PlayerStackingAndUnstacking.ClothObject.Count <= gm.PlayerClothCollectionLimit-1 && limitToGiveCloth<= gm.PlayerClothCollectionLimit -5 && RackCloths.Count >0)
            {
                giveItemToPlayer -= Time.deltaTime;
                if (giveItemToPlayer <= 0)
                {

                    playerControl.GetComponent<Sneaker.Core.PlayerStackingAndUnstacking>().addClothToStack(ClothToGivePlayer, ClothIDNumber);
                    ClothXCount++;
                    limitToGiveCloth++;
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
            if(collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Sneaker.Movement._PlayerMovment>().direction.magnitude <= 0)
            {
                if(ClothXCount<= LimitCountForCloth)
                    isPlayerNear = true;
            }

            if (collision.gameObject.CompareTag("Player") && isRackClosed && collision.gameObject.GetComponent<Sneaker.Movement._PlayerMovment>().direction.magnitude <= 0)
            {
                isPlayerOnClosedRack = true;
                if(gm.MaxMoney > MoneyReduceSpeed && MaxMoneyNeededToUnlock > 0)
                {
                    MaxMoneyNeededToUnlock -= MoneyReduceSpeed;
                    gm.MaxMoney -= MoneyReduceSpeed;
                    RackPrice.text = "$" + MaxMoneyNeededToUnlock.ToString();
                }
            }
        }

        public void reduce()
        {
            limitToGiveCloth--;
        }
        private void OnCollisionExit(Collision collision)
        {
            if (isPlayerNear)
                isPlayerNear = false;

            if (isPlayerOnClosedRack)
                isPlayerOnClosedRack = false;

            if (ClothXCount > 0)
                ClothXCount = 0;
        }

    }
}
