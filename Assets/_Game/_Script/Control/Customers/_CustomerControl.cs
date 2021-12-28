using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneaker.Control {
    public class _CustomerControl : MonoBehaviour
    {        
        [HideInInspector]public Sneaker.Movement._CustomerMovement move;
        public int NeedItemCode;

        public GameObject Money;


        public float MoneyDropOffset;
        public float waitTimer;
        public float timerToTakeItemFromPlayer;

        public bool isTradeStarted;        
        public bool isTradingGoingOn;
        public bool isPlayerNear;
        public bool clothTookFromPlayer;
        public bool isTradeComplete;
        [HideInInspector] public bool moneySpwanned;
        void Start()
        {
            timerToTakeItemFromPlayer = waitTimer;
            move = GetComponent<Sneaker.Movement._CustomerMovement>();            
        }

        private float t = 0.5f;
        public void CompleteTrading()
        {
            if (clothTookFromPlayer && t >= 0)
                t -= Time.deltaTime;
            if (t <= 0)
            {
                t = 0;
                isTradingGoingOn = true;
            }
        }


        bool itemCodeGenrator;
        float itemCodeGenratorTimer = 1f;
        void Update()
        {


            CompleteTrading();
            takeCloth();
            MoneyThrow();

            if (move.levelManager.OpenRacks.Count > 0 && !itemCodeGenrator && itemCodeGenratorTimer >= 0)                
            {
                itemCodeGenratorTimer -= Time.deltaTime;
                if (itemCodeGenratorTimer <= 0)
                {
                    CutomerRandomNeedItemGenrator();
                    itemCodeGenrator = true;
                }
            }



        }

        void MoneyThrow()
        {
            if (isTradingGoingOn && !moneySpwanned)
            {
                if (NeedItemCode == move.levelManager.Rack0.ClothIDNumber)
                {
                    move.levelManager.Rack0.limitToGiveCloth--;
                    Instantiate(move.levelManager.Money, new Vector3(transform.position.x, transform.position.y + MoneyDropOffset, transform.position.z), Quaternion.identity);
                }

                if (move.levelManager.Rack1 != null && NeedItemCode == move.levelManager.Rack1.ClothIDNumber)
                {
                    move.levelManager.Rack1.limitToGiveCloth--;
                    Instantiate(move.levelManager.Money1, new Vector3(transform.position.x, transform.position.y + MoneyDropOffset, transform.position.z), Quaternion.identity);
                }

                if (move.levelManager.PStationRack0 != null && NeedItemCode == move.levelManager.PStationRack0.ClothIDNumber)
                {
                    move.levelManager.PStationRack0.limitToGiveCloth--;
                    Instantiate(move.levelManager.Money1, new Vector3(transform.position.x, transform.position.y + MoneyDropOffset, transform.position.z), Quaternion.identity);
                }

                if (move.levelManager.PStationRack1 != null && NeedItemCode == move.levelManager.PStationRack1.ClothIDNumber)
                {
                    move.levelManager.PStationRack1.limitToGiveCloth--;
                    Instantiate(move.levelManager.Money1, new Vector3(transform.position.x, transform.position.y + MoneyDropOffset, transform.position.z), Quaternion.identity);
                }

                StartCoroutine(activateAnim(0.2f));
                moneySpwanned = true;
            }
        }
        IEnumerator activateAnim(float t)
        {
            yield return new WaitForSeconds(t);            
            move.anime.SetBool("tradeComplete", true);
            if (move.target != null)
            {
                move.target.GetComponent<Sneaker.Movement.dottedCircle>().occupied = false;
                move.target = null;
            }
        }
        void takeCloth()
        {
            if (clothTookFromPlayer && !isTradeComplete)
            {
                timerToTakeItemFromPlayer -= Time.deltaTime;
                if (timerToTakeItemFromPlayer <= 0)
                {                    
                    move.anime.SetBool("tradeComplete", false);
                    move.tradeIsOver = true;
                    isTradeComplete = true;
                    isPlayerNear = false;
                    timerToTakeItemFromPlayer = waitTimer;
                }
            }
        }

        public void CutomerRandomNeedItemGenrator()
        {
            if (move.levelManager.OpenRacks.Count > 0)
            {
                int a = Random.Range(0, move.levelManager.OpenRacks.Count);
                NeedItemCode = move.levelManager.OpenRacks[a];
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("end") && isTradeComplete)
            {                
                Destroy(this.gameObject);
            }

            if (other.gameObject.CompareTag("Player") && !clothTookFromPlayer)
            {
                isPlayerNear = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Slot") && move.agent.velocity.magnitude<=0)
            {
                move.isRechedStation = true;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, move.Rotate, transform.eulerAngles.x);
            }
        }
    }

}
