using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneaker.Control
{
    public class ProcessingStation : MonoBehaviour
    {
        public int ClothCodeOutput;
        public bool isPlayerNear;


        public Transform ClothCollector;

        public GameObject ClothObject;
        public Vector3 oneCloth;
        public List<GameObject> Cloth = new List<GameObject>();

        public Sneaker.Control.PackingStation PS;
       
        void Start()
        {

        }

        
        void Update()
        {
            removeDeletedCloth();
        }


        public void DestoryOtherCloth()
        {
            Destroy(PS.Cloth[PS.Cloth.Count - 1]);
        }
        public void placeCloth()
        {
            if (Cloth.Count == 0)
            {
                GameObject c = Instantiate(ClothObject);
                c.transform.parent = ClothCollector;
                c.transform.localPosition = oneCloth;                
                Cloth.Add(c);
                return;
            }

            if (Cloth.Count >= 1 && Cloth.Count % 5 != 0)
            {
                GameObject c = Instantiate(ClothObject);
                c.transform.parent = ClothCollector;
                c.transform.localPosition = new Vector3(Cloth[Cloth.Count - 1].transform.localPosition.x + 0.7f, Cloth[Cloth.Count - 1].transform.localPosition.y, Cloth[Cloth.Count - 1].transform.localPosition.z);
                Cloth.Add(c);
            }
            if (Cloth.Count % 5 == 0)
            {
                GameObject c = Instantiate(ClothObject);
                c.transform.parent = ClothCollector;
                c.transform.localPosition = new Vector3(oneCloth.x, Cloth[Cloth.Count - 5].transform.localPosition.y + 0.18f, oneCloth.z); 
                Cloth.Add(c);
            }
        }

        public void removeDeletedCloth()
        {
            if (Cloth.Count > 0)
            {
                for (int i = 0; i <= Cloth.Count - 1; i++)
                {
                    if (Cloth[i] == null)
                    {
                        Cloth.Remove(Cloth[i]);
                    }
                }
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Sneaker.Movement._PlayerMovment>().direction.magnitude <= 0)
            {
                isPlayerNear = true;
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (isPlayerNear)
                isPlayerNear = false;
        }
    }
}
