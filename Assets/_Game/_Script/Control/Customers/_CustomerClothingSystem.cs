using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneaker.Control
{
    public class _CustomerClothingSystem : MonoBehaviour
    {
        public _CustomerControl customerControl;
        public GameObject cloth1,cloth2;
        public GameObject giftBox1, giftBox2;
        public GameObject defaultCloth;
        public bool poof;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            activeCloth();
            changedCloth();
        }
        float x = 0.5f;
        void activeCloth()
        {
            if (!customerControl.clothTookFromPlayer)
            {
                poof = false;
                x = 0.15f;
                if (customerControl.NeedItemCode == customerControl.move.levelManager.Rack0.ClothIDNumber)
                {
                    defaultCloth.SetActive(true);
                    cloth1.SetActive(false);                    
                    cloth2.SetActive(false);                    
                }
                if (customerControl.move.levelManager.Rack1 != null && customerControl.NeedItemCode == customerControl.move.levelManager.Rack1.ClothIDNumber)
                {
                    defaultCloth.SetActive(true);
                    cloth1.SetActive(false);
                    cloth2.SetActive(false);
                }

                if(customerControl.move.levelManager.PStationRack0 !=null && customerControl.NeedItemCode == customerControl.move.levelManager.PStationRack0.ClothIDNumber)
                {
                    giftBox1.SetActive(false);
                    giftBox2.SetActive(false);
                }
                if (customerControl.move.levelManager.PStationRack1 != null && customerControl.NeedItemCode == customerControl.move.levelManager.PStationRack1.ClothIDNumber)
                {
                    giftBox1.SetActive(false);
                    giftBox2.SetActive(false);
                }
            }
        }

        public ParticleSystem poofPartical;
        public Transform poofEffectPos;
        
        void changedCloth()
        {
            if (poof && x >= 0)
                x -= Time.deltaTime;


            if (customerControl.isTradingGoingOn)
            {
                if (!poof)
                {
                    GameObject poofObj = Instantiate(poofPartical.gameObject, poofEffectPos.position, Quaternion.identity);
                    poofObj.transform.parent = poofEffectPos;
                    Destroy(poofObj, 1);
                    poof = true;
                }
                if (x <= 0)
                {
                    if (customerControl.NeedItemCode == customerControl.move.levelManager.Rack0.ClothIDNumber)
                    {
                        defaultCloth.SetActive(false);
                        cloth1.SetActive(true);
                        cloth2.SetActive(false);
                    }
                    if (customerControl.move.levelManager.Rack1 != null && customerControl.NeedItemCode == customerControl.move.levelManager.Rack1.ClothIDNumber)
                    {
                        defaultCloth.SetActive(false);
                        cloth1.SetActive(false);
                        cloth2.SetActive(true);
                    }

                    if (customerControl.move.levelManager.PStationRack0 != null && customerControl.NeedItemCode == customerControl.move.levelManager.PStationRack0.ClothIDNumber)
                    {
                        giftBox1.SetActive(true);
                        giftBox2.SetActive(false);
                        GetComponent<Animator>().SetBool("hold", true);
                    }
                    if (customerControl.move.levelManager.PStationRack1 != null && customerControl.NeedItemCode == customerControl.move.levelManager.PStationRack1.ClothIDNumber)
                    {
                        giftBox1.SetActive(false);
                        giftBox2.SetActive(true);
                        GetComponent<Animator>().SetBool("hold", true);
                    }
                }
                
            }
        }
    }
}
