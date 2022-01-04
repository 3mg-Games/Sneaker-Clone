using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneaker.Core
{
   public class _LevelManager : MonoBehaviour
    {
        public List<GameObject> StationPosition = new List<GameObject>();
        public List<int> OpenRacks = new List<int>();

        public Transform spwanPos;
        public Transform EndPos;
        public GameObject[] Female;

        public GameObject Money;
        public GameObject Money1;

        [Header("Rack Data")]
        public Racks Rack0;
        public Racks Rack1;

        public Sneaker.Control.ProcessingStationRack PStationRack0;
        public Sneaker.Control.ProcessingStationRack PStationRack1;

        //public Racks Rack_2;
        //public Racks Rack_3;

        [Header("Rack Open")]
        public bool isRackOpen0;
        public bool isRackOpen1;

        public bool isRackOpen2;
        public bool isRackOpen3;

        [Header("Racks Cloth UI")]
        public Sprite RackCloth0;
        public Sprite RackCloth1;

        public Sprite RackCloth2;
        public Sprite RackCloth3;

        public float spwanTimer = 2;

        private GameManager gm;
        void Start()
        {
            spwnner = spwanTimer;
            gm = FindObjectOfType<GameManager>();
        }

        public Tutorial t;
        void Update()
        {
            if (this.gameObject.activeSelf && !FindObjectOfType<GameManager>().SectionsList.Contains(this.gameObject))
                FindObjectOfType<GameManager>().SectionsList.Add(this.gameObject);

            if (Rack0 != null && Rack0.Unlock)
                isRackOpen0 = true;

            if ( Rack1 != null && Rack1.Unlock )
                isRackOpen1 = true;

            if (PStationRack0 != null && PStationRack0.Unlock)
                isRackOpen2 = true;

            if (PStationRack1 != null && PStationRack1.Unlock)
                isRackOpen3 = true;


            if (StationPosition.Count > 0) 
            {
                try
                {
                    if (t != null &&  t.CustomerServing|| gm.Level > 0)
                        spwnCustomers();
                    if (t == null)
                    {
                        spwnCustomers();
                    }
                }
                catch
                {
                    spwnCustomers();
                }

                //StartCoroutine(CustomerSpawnnerTimer(5));
            }            
        }

        float spwnner;
        void spwnCustomers()
        {
            if (spwnner > 0)
                spwnner -= Time.deltaTime;
            if (spwnner <= 0)
            {
                int i = Random.Range(0, 6);
                GameObject customer = Instantiate(Female[i], spwanPos.position, Quaternion.identity);
                customer.GetComponent<Sneaker.Movement._CustomerMovement>().levelManager = GetComponent<_LevelManager>();
                customer.GetComponent<Sneaker.Movement._CustomerMovement>().end = EndPos;
                customer.transform.parent = spwanPos;
                spwnner = spwanTimer;
            }
        }

        IEnumerator CustomerSpawnnerTimer(float t)
        {
            for(int i = 0; i <= StationPosition.Count;)
            {
                spwnCustomers();
                yield return new WaitForSeconds(t);
                i++;
            }
        }

    }
}
