using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneaker.Core
{
    public class Tutorial : MonoBehaviour
    {
        public Sneaker.Core.PlayerStackingAndUnstacking psau;
        [Header("Section 1")]
        public GameObject PlayerArrow;
        
        public GameObject RackArrow1;
        public GameObject RackArrow2;
        
        public GameObject CustomerArrow1;
        public GameObject CustomerArrow2;
        
        public GameObject FittingStation;

        public Transform Rack1;
        public Transform Rack2;
        
        public Sneaker.Control._CustomerControl con1;
        public Sneaker.Control._CustomerControl con2;
        public FittingStation fs;
        public bool CustomerServing;
        public bool FittingStationUnlocked;

        [Header("Level UI")]
        public GameObject Background;
        public GameObject Hand;
        public GameObject Glow;

        [Header("Camera")]
        public FittingStation fs1;
        public GameObject Section2;
        public Animator CamAnime;
        public bool isCameraMoved;

        [Header("Section 2")]
        public GameObject SRack1;
        public GameObject CustomerArrow3;
        public GameObject CustomerArrow4;
        public GameObject PackingStationArrow;
        public bool c1;
        public bool c2;
        public bool c3;
        public bool SCustomerServing;
        public bool SFittingStationUnlocked;
        public Sneaker.Core.Racks rack;
        public Sneaker.Control._CustomerControl con3;
        public Sneaker.Control._CustomerControl con4;
        public Sneaker.Control.ProcessingStationRack pSR;

        public bool cinematic1;
        public bool cinematic2;
        public bool tutorial2Over;
        void Start()
        {
            TutorialSetOnce();
            TutorialSetOnce1();
            if (FindObjectOfType<Sneaker.Core.GameManager>().Level > 0)
            {
                RackArrow1.SetActive(false);
                PlayerArrow.SetActive(false);
                resetUI();
            }
        }

        public void TutorialSetOnce()
        {
            resetUI();
            Rack2.GetComponent<RackColliderActivator>().isColliderActivate = false;
            PlayerArrow.SetActive(false);
            RackArrow1.SetActive(false);
            RackArrow2.SetActive(false);
            CustomerArrow1.SetActive(false);
            CustomerArrow2.SetActive(false);
            FittingStation.SetActive(false);
        }
        public void TutorialSetOnce1()
        {
            SRack1.SetActive(false);
            CustomerArrow3.SetActive(false);
            CustomerArrow4.SetActive(false);
            PackingStationArrow.SetActive(false);
        }

        public void resetUI()
        {
            Background.SetActive(false);
            Hand.SetActive(false);
            Glow.SetActive(false);
        }
        private void Update()
        {
            if (!FittingStationUnlocked && FindObjectOfType<GameManager>().Level <= 0)
            {
                tutorial_1();
            }
            if(FindObjectOfType<GameManager>().Level > 0 && !FittingStationUnlocked)
            {
                FittingStation.SetActive(false);
                PlayerArrow.SetActive(false);
            }
            if (FindObjectOfType<GameManager>().currentServedCount >= FindObjectOfType<GameManager>().maxCustomerToServe && FindObjectOfType<GameManager>().Level <= 0)
            {
                Background.SetActive(true);
                Hand.SetActive(true);
                Glow.SetActive(true);
            }
            if(FindObjectOfType<GameManager>().Level >= 5 && FindObjectOfType<GameManager>().Level <= 5 && !tutorial2Over)
            {
                tutorial_3();
            }
            if(FindObjectOfType<GameManager>().Level >= 6)
            {
                TutorialSetOnce();
                TutorialSetOnce1();
            }

            tutorial_2();
        }
        public void tutorial_1()
        {
            if (psau.ClothObject.Count <= 0)
            {
                PlayerArrow.SetActive(true);
                RackArrow1.SetActive(true);
                PlayerArrow.transform.LookAt(new Vector3(RackArrow1.transform.position.x, PlayerArrow.transform.position.y, RackArrow1.transform.position.z));
            }

            if (psau.ClothObject.Count > 0)
            {
                RackArrow1.SetActive(false);
                CustomerArrow1.SetActive(true);
                PlayerArrow.transform.LookAt(new Vector3(CustomerArrow1.transform.position.x, PlayerArrow.transform.position.y, CustomerArrow1.transform.position.z));
            }

            if (con1.clothTookFromPlayer && !Rack2.GetComponent<RackColliderActivator>().isp)
            {
                CustomerArrow1.SetActive(false);
                RackArrow2.SetActive(true);
                Rack2.GetComponent<RackColliderActivator>().isColliderActivate = true;
                PlayerArrow.transform.LookAt(new Vector3(RackArrow2.transform.position.x, PlayerArrow.transform.position.y, RackArrow2.transform.position.z));
            }

            if (Rack2.GetComponent<RackColliderActivator>().isp)
            {
                RackArrow2.SetActive(false);
                CustomerArrow2.SetActive(true);
                CustomerArrow1.SetActive(false);
                PlayerArrow.transform.LookAt(new Vector3(CustomerArrow2.transform.position.x, PlayerArrow.transform.position.y, CustomerArrow2.transform.position.z));
            }

            if (con2.clothTookFromPlayer)
            {
                CustomerServing = true;
                CustomerArrow2.SetActive(false);
                CustomerArrow1.SetActive(false);
            }

            if (CustomerServing)
            {
                FittingStation.SetActive(true);
                PlayerArrow.transform.LookAt(new Vector3(FittingStation.transform.position.x, PlayerArrow.transform.position.y, FittingStation.transform.position.z));
                FittingStationUnlocked = fs.isStationOpen;
            }

            if (FittingStationUnlocked)
            {
                FittingStation.SetActive(false);
                PlayerArrow.SetActive(false);
            }
        }
        public void tutorial_2()
        {
            if (fs1.isStationOpen && fs.isStationOpen && !cinematic1)
            {
                if (!Section2.activeSelf && !isCameraMoved)
                {
                    FindObjectOfType<GameManager>().GameplayPause = true;
                    CamAnime.Play("Second");
                    StartCoroutine(cin1(0.5f));
                    isCameraMoved = true;
                }
            }
            
        }
        public void tutorial_3()
        {
            if (Section2.activeSelf && !c1)
            {
                PlayerArrow.SetActive(true);
                CustomerArrow3.SetActive(true);
                SRack1.SetActive(true);
                PlayerArrow.transform.LookAt(new Vector3(SRack1.transform.position.x, PlayerArrow.transform.position.y, SRack1.transform.position.z));
            }

            if (rack.isPlayerNear && !c1)
            {
                SRack1.SetActive(false);
                PlayerArrow.transform.LookAt(new Vector3(CustomerArrow3.transform.position.x, PlayerArrow.transform.position.y, CustomerArrow3.transform.position.z));
                c1 = true;
            }
            if(c1 && !con3.clothTookFromPlayer)
            {
                PlayerArrow.transform.LookAt(new Vector3(CustomerArrow3.transform.position.x, PlayerArrow.transform.position.y, CustomerArrow3.transform.position.z));
            }
            if (con3.clothTookFromPlayer)
            {
                CustomerArrow3.SetActive(false);
                CustomerArrow4.SetActive(true);
                PackingStationArrow.SetActive(true);
                PlayerArrow.transform.LookAt(new Vector3(PackingStationArrow.transform.position.x, PlayerArrow.transform.position.y, PackingStationArrow.transform.position.z));
            }
            if(con3.clothTookFromPlayer && !c2 && !cinematic2)
            {
                FindObjectOfType<GameManager>().GameplayPause = true;
                CamAnime.Play("4");
                StartCoroutine(cin2(0.5f));
                c2 = true;
            }
            if(pSR.isPlayerNear && !c3)
            {
                PackingStationArrow.SetActive(false);
                PlayerArrow.transform.LookAt(new Vector3(CustomerArrow4.transform.position.x, PlayerArrow.transform.position.y, CustomerArrow4.transform.position.z));
                c3 = true;
            }
            if (c3 && !con4.clothTookFromPlayer)
            {
                PlayerArrow.transform.LookAt(new Vector3(CustomerArrow4.transform.position.x, PlayerArrow.transform.position.y, CustomerArrow4.transform.position.z));
            }
            if (con4.clothTookFromPlayer)
            {
                CustomerArrow3.SetActive(false);
                CustomerArrow4.SetActive(false);
                PackingStationArrow.SetActive(false);
                PlayerArrow.SetActive(false);
                tutorial2Over = true;
            }
        }


        IEnumerator cin1(float t)
        {
            yield return new WaitForSeconds(t);
            cinematic1 = true;
        }
        IEnumerator cin2(float t)
        {
            yield return new WaitForSeconds(t);
            cinematic2 = true;
        }
    }
}
