using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clone.Core
{
    public class PlayerStackingAndUnstacking : MonoBehaviour
    {
        public List<GameObject> ClothObject = new List<GameObject>();
        public GameObject StackingPlace;

        public GameObject pooflEffect;

        [Range(0, 1)]
        public float PickupDileverVolume;
        void Start()
        {

        }


        void Update()
        {
            if (ClothObject.Count <= 0)
                poof = false;

            StackingClothListing();
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
                GameObject o = Instantiate(cloth, StackingPlace.transform.position, Quaternion.identity);
                o.transform.parent = StackingPlace.transform;
                 o.GetComponent<Cloths>().ClothNumber = num;
                 o.GetComponent<Cloths>().Collector = this.gameObject;
            }

            if (ClothObject.Count > 0)
            {
                GameObject o = Instantiate(cloth, ClothObject[ClothObject.Count - 1].transform.position + new Vector3(0, 0.05f, 0), Quaternion.identity);
                o.transform.parent = StackingPlace.transform;
                o.GetComponent<Cloths>().ClothNumber = num;
                o.GetComponent<Cloths>().Collector = this.gameObject;
            }
        }
        public void RemoveCloth(Collider other)
        {
            if (ClothObject.Count > 0)
            {
                for (int i = 0; i <= ClothObject.Count - 1;)
                {
                    if (i >= ClothObject.Count - 1 && ClothObject[i].GetComponent<Cloths>().ClothNumber != other.gameObject.GetComponent<Clone.Control._CustomerControl>().NeedItemCode)
                    {
                        return;
                    }

                    if (ClothObject[i].GetComponent<Cloths>().ClothNumber != other.gameObject.GetComponent<Clone.Control._CustomerControl>().NeedItemCode)
                    {
                        i++;
                    }

                    if (ClothObject[i].GetComponent<Cloths>().ClothNumber == other.gameObject.GetComponent<Clone.Control._CustomerControl>().NeedItemCode)
                    {
                        //other.gameObject.layer = 17;
                        //GetComponent<playerMovement>().AM.source.PlayOneShot(GetComponent<playerMovement>().AM.PandD, PickupDileverVolume);
                        ClothObject[i].GetComponent<Cloths>().throwCloth(other.gameObject.transform);
                        other.gameObject.GetComponent<Clone.Control._CustomerControl>().isPlayerNear = true;
                        other.gameObject.GetComponent<Clone.Control._CustomerControl>().clothTookFromPlayer = true;
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
                    T.position = new Vector3(T.transform.position.x, T.transform.position.y/* + 0.25f*/, T.transform.position.z);
                    ClothObject.Add(T.gameObject);
                }
            }

            if (ClothObject.Count > 0)
            {
                for (int i = 0; i <= ClothObject.Count - 1; i++)
                {
                    if (ClothObject[i] != null)
                    {
                        ClothObject[i].transform.position = new Vector3(ClothObject[i].transform.position.x, ClothObject[i].transform.position.y, ClothObject[i].transform.position.z);
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
