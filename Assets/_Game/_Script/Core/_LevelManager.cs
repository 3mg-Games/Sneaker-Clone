using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clone.Core
{
   public class _LevelManager : MonoBehaviour
    {
        public List<GameObject> StationPosition = new List<GameObject>();
        public List<int> OpenRacks = new List<int>();

        public Transform spwanPos;
        public Transform EndPos;
        public GameObject Female1;

        [Header("Rack Data")]
        public Racks Rack_0;
        public Racks Rack_1;
        public Racks Rack_2;
        public Racks Rack_3;

        [Header("Rack Open")]
        public bool isRackOpen_0;
        public bool isRackOpen_1;
        public bool isRackOpen_2;
        public bool isRackOpen_3;

        [Header("Racks Cloth UI")]
        public Sprite RackCloth_0;
        public Sprite RackCloth_1;
        public Sprite RackCloth_2;
        public Sprite RackCloth_3;

        public float spwanTimer = 2;
        void Start()
        {
            spwnner = spwanTimer;
        }

        
        void Update()
        {
            if (StationPosition.Count > 0) 
            {
                spwnCustomers();



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
                GameObject customer = Instantiate(Female1, spwanPos.position, Quaternion.identity);
                customer.GetComponent<Clone.Movement._CustomerMovement>().levelManager = GetComponent<_LevelManager>();
                customer.GetComponent<Clone.Movement._CustomerMovement>().end = EndPos;
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
