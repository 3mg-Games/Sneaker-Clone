using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneaker.Control
{
    public class PackingStation : MonoBehaviour
    {
        public int clothCodeInput;
        public bool isPlayerNear;
        public Transform ClothCollector;

        public GameObject ClothObject;
        public Vector3 oneCloth;
        public List<GameObject> Cloth = new List<GameObject>();
        public Sneaker.Control.ProcessingStationRack rack;
        public Animator processingStationAnim;
        public Sneaker.Core.Racks racks;
        void Start()
        {

        }

        
        void Update()
        {
            removeDeletedCloth();
            anim();


        }

        public void anim()
        {
            if (Cloth.Count > 0)
            {
                processingStationAnim.SetBool("process", true);
            }
            if (Cloth.Count <= 0)
            {
                processingStationAnim.SetBool("process", false);
            }
        }


        float l = 0.3f;
        float a;
        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Sneaker.Movement._PlayerMovment>().direction.magnitude <= 0 && rack.isRackOpen)
            {
                if (a >= 0)
                    a -= Time.deltaTime;
                if (a <= 0)
                {                    
                    collision.gameObject.GetComponent<Sneaker.Core.PlayerStackingAndUnstacking>().RemoveClothForPacking(ClothCollector, clothCodeInput,this.gameObject);
                    racks.limitToGiveCloth--;
                    print("Hello");
                    a = l;
                }

                isPlayerNear = true;
            }
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
                c.transform.localPosition = oneCloth;
            }

            if (Cloth.Count >= 1 && Cloth.Count%5 !=0 )
            {
                
                GameObject c = Instantiate(ClothObject);
                c.transform.parent = ClothCollector;
                c.transform.localPosition = new Vector3(Cloth[Cloth.Count - 1].transform.localPosition.x + 0.7f, Cloth[Cloth.Count - 1].transform.localPosition.y, Cloth[Cloth.Count - 1].transform.localPosition.z);
                Cloth.Add(c);
                
            }
            if(Cloth.Count % 5 == 0)
            {
                
                GameObject c = Instantiate(ClothObject);
                c.transform.parent = ClothCollector;
                c.transform.localPosition = new Vector3(oneCloth.x, Cloth[Cloth.Count - 5].transform.localPosition.y + 0.2f,oneCloth.z); 
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
        private void OnCollisionExit(Collision collision)
        {
            if (isPlayerNear)
                isPlayerNear = false;
        }

    }
}
