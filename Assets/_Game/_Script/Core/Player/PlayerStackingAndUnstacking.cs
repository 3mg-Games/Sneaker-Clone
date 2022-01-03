using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneaker.Core
{
    public class PlayerStackingAndUnstacking : MonoBehaviour
    {
        public List<GameObject> ClothObject = new List<GameObject>();
        public GameObject StackingPlace;

        public GameObject pooflEffect;
        public GameObject poofEffect_1;

        [Range(0, 1)]
        public float PickupDileverVolume;

        private GameManager gm;
        private AudioManager audioManager;
        void Start()
        {
            gm = FindObjectOfType<GameManager>();
            audioManager = FindObjectOfType<AudioManager>();
        }


        void Update()
        {
            if (ClothObject.Count <= 0)
                poof = false;

            StackingClothListing();
            StackingPlace.transform.rotation = Quaternion.Euler(0, StackingPlace.transform.eulerAngles.y, 0);
            //StackingPlace.transform.position = new Vector3(-4.0206439f, 0.285546035f, 0.716603279f);

        }
        bool poof;
        public void resetStacking()
        {
            if (!poof && ClothObject.Count > 0)
            {
                Destroy(Instantiate(pooflEffect, StackingPlace.transform.position, Quaternion.identity), 2);
                poof = true;
            }

            if (ClothObject.Count > 0)
            {
                for (int i = 0; i <= ClothObject.Count - 1; i++)
                {
                    if (ClothObject[i] != null)
                    {
                        Destroy(ClothObject[i]);
                    }

                    if (ClothObject[i] == null)
                    {
                        ClothObject.Remove(ClothObject[i]);
                    }
                }
            }

        }
        public void addClothToStack(GameObject cloth, int num)
        {
            if (ClothObject.Count <= 0)
            {
                poofEffect_1.SetActive(true);
                GameObject o = Instantiate(cloth, StackingPlace.transform.position, Quaternion.identity);
                o.transform.parent = StackingPlace.transform;
                 o.GetComponent<Cloths>().ClothNumber = num;
                 o.GetComponent<Cloths>().Collector = this.gameObject;
            }

            if (ClothObject.Count > 0)
            {
                poofEffect_1.SetActive(true);
                GameObject o = Instantiate(cloth, ClothObject[ClothObject.Count - 1].transform.position + new Vector3(0, 0.05f, 0), Quaternion.identity);
                o.transform.parent = StackingPlace.transform;
                o.GetComponent<Cloths>().ClothNumber = num;
                o.GetComponent<Cloths>().Collector = this.gameObject;
            }
        }
        public void RemoveCloth(Collider other,int needCode, Vector3 customerUISpwanOffset)
        {
            if (ClothObject.Count > 0)
            {
                for (int i = 0; i <= ClothObject.Count - 1;)
                {
                    if (i >= ClothObject.Count - 1 && ClothObject[i].GetComponent<Cloths>().ClothNumber != needCode)
                    {
                        return;
                    }

                    if (ClothObject[i].GetComponent<Cloths>().ClothNumber != needCode)
                    {
                        i++;
                    }

                    if (ClothObject[i].GetComponent<Cloths>().ClothNumber == needCode)
                    {
                        //other.gameObject.layer = 17;
                        //GetComponent<playerMovement>().AM.source.PlayOneShot(GetComponent<playerMovement>().AM.PandD, PickupDileverVolume);
                        audioManager.source.PlayOneShot(audioManager.GivingToCustomer);
                        ClothObject[i].GetComponent<Cloths>().throwCloth(other.gameObject.transform);
                        other.gameObject.GetComponent<Sneaker.Control._CustomerControl>().isPlayerNear = true;
                        other.gameObject.GetComponent<Sneaker.Control._CustomerControl>().clothTookFromPlayer = true;
                        //Instantiate(gm.customerUI, other.transform.position + customerUISpwanOffset, Quaternion.identity);
                        break;
                    }

                }
            }
        }
       
        public void RemoveClothForPacking(Transform transform, int needCode, GameObject obj)
        {
            if (ClothObject.Count > 0)
            {
                for (int i = 0; i <= ClothObject.Count - 1;)
                {
                    if (i >= ClothObject.Count - 1 && ClothObject[i].GetComponent<Cloths>().ClothNumber != needCode)
                    {
                        return;
                    }

                    if (ClothObject[i].GetComponent<Cloths>().ClothNumber != needCode)
                    {
                        i++;
                    }

                    if (ClothObject[i].GetComponent<Cloths>().ClothNumber == needCode )
                    {
                        audioManager.source.PlayOneShot(audioManager.GivingToCustomer);
                        ClothObject[i].GetComponent<Cloths>().throwCloth(transform);
                        obj.GetComponent<Sneaker.Control.PackingStation>().placeCloth();
                        break;
                    }

                }
            }
        }
        public void StackingClothListing()
        {
            foreach (Transform T in StackingPlace.transform)
            {
                if (!ClothObject.Contains(T.gameObject))
                {
                    T.localPosition = new Vector3(T.transform.localPosition.x, T.transform.localPosition.y + 0.05f, T.transform.localPosition.z);
                    ClothObject.Add(T.gameObject);
                }
            }

            if (ClothObject.Count > 0)
            {
                for (int i = 0; i <= ClothObject.Count - 1; i++)
                {
                    if (ClothObject[i] != null)
                    {
                        ClothObject[i].transform.localPosition = new Vector3(ClothObject[i].transform.localPosition.x, ClothObject[i].transform.localPosition.y, ClothObject[i].transform.localPosition.z);
                    }

                    if (ClothObject[i] == null)
                    {
                        ClothObject.Remove(ClothObject[i]);
                    }
                }
            }
        }
    }
}
