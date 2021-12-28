using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Sneaker.Control
{
    public class ProcessingStationRack : MonoBehaviour
    {
        public int ClothIDNumber;

        public Sneaker.Control.ProcessingStation PS;
        public TextMeshPro RackPrice;
        
        public GameObject ClothToGivePlayer;
        public GameObject StoreGraphics;
        public GameObject PurchaseGraphics;

        public float giveItemToPlayer;
        public float waitTimer = 1;
        public int MoneyReduceSpeed;
        public int MaxMoneyNeededToUnlock;

        public bool isPlayerNear;
        public bool isRackClosed;
        public bool isRackOpen;
        public bool Unlock;
        public bool isPlayerOnClosedRack;

        public int ClothXCount;
        public int limitToGiveCloth;

        private Sneaker.Control.PlayerControl playerControl;
        private Sneaker.Core.PlayerStackingAndUnstacking PlayerStackingAndUnstacking;
        private Sneaker.Core.GameManager gm;
        private Sneaker.Core._LevelManager levelManager;
        private Sneaker.Core.AudioManager audioManager;
        // Start is called before the first frame update
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

        // Update is called once per frame
        void Update()
        {
            if (!isRackClosed && !isRackOpen)
                whenPlayerIsOnRack();

            if (MaxMoneyNeededToUnlock <= 0 && isRackClosed)
            {
                isPlayerNear = false;
                isRackClosed = false;
            }

            if (MaxMoneyNeededToUnlock <= 0 && !Unlock)
                Unlock = true;

            if (Unlock)
                MaxMoneyNeededToUnlock = 0;

            if (!isRackClosed && isRackOpen)
                GiveItemToPlayer();

            CheckStore();

            if (isRackOpen && !gm.ProcessingStationList.Contains(this.gameObject))
                gm.ProcessingStationList.Add(this.gameObject);
        }

        public void CheckStore()
        {
            if (levelManager.isRackOpen2 && Unlock)
            {
                /*levelManager.isRackOpen_0 = Rack0;*/
                if (!levelManager.OpenRacks.Contains(ClothIDNumber))
                    levelManager.OpenRacks.Add(ClothIDNumber);
            }
            if (levelManager.isRackOpen3 && Unlock)
            {
                /*levelManager.isRackOpen_1 = Rack1;*/
                if (!levelManager.OpenRacks.Contains(ClothIDNumber))
                    levelManager.OpenRacks.Add(ClothIDNumber);
            }
            //levelManager.isRackOpen_2 = Rack2;
            //levelManager.isRackOpen_3 = Rack3;
        }
        public void whenPlayerIsOnRack()
        {
            audioManager.source.PlayOneShot(audioManager.Unlock);
            RackPrice.gameObject.SetActive(false);
            StoreGraphics.SetActive(true);
            //PurchaseGraphics.GetComponent<MeshRenderer>().enabled = false;
            Destroy(PurchaseGraphics);
            //transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
            //transform.localPosition = new Vector3(transform.localPosition.x, YPosAfterUnlock, transform.localPosition.z);
            //CheckStore();
            isRackOpen = true;
        }
        public void GiveItemToPlayer()
        {

            if (isPlayerNear && PlayerStackingAndUnstacking.ClothObject.Count <= gm.PlayerClothCollectionLimit-1 && limitToGiveCloth<= gm.PlayerClothCollectionLimit -5 && PS.Cloth.Count >0)
            {
                giveItemToPlayer -= Time.deltaTime;
                if (giveItemToPlayer <= 0)
                {
                    playerControl.GetComponent<Sneaker.Core.PlayerStackingAndUnstacking>().addClothToStack(ClothToGivePlayer, ClothIDNumber);
                    limitToGiveCloth++;
                    isPlayerNear = false;
                    Destroy(PS.Cloth[PS.Cloth.Count-1].gameObject);
                    giveItemToPlayer = waitTimer;
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Sneaker.Movement._PlayerMovment>().direction.magnitude <= 0)
            {
                isPlayerNear = true;
            }
            if (collision.gameObject.CompareTag("Player") && isRackClosed && collision.gameObject.GetComponent<Sneaker.Movement._PlayerMovment>().direction.magnitude <= 0)
            {
                isPlayerOnClosedRack = true;
                if (gm.MaxMoney >= MoneyReduceSpeed && MaxMoneyNeededToUnlock > 0)
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
