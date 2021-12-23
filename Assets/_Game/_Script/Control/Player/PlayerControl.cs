using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clone.Control
{
    public class PlayerControl : MonoBehaviour
    {
        Clone.Core.PlayerStackingAndUnstacking PlayerStackingAndUnstacking;
        Clone.Core.GameManager gm;
        public Clone.Core.customerServedUI cSUI;

        public Vector3 customerUISpwanOffset;
        void Start()
        {
            gm = FindObjectOfType<Clone.Core.GameManager>();
            PlayerStackingAndUnstacking = GetComponent<Clone.Core.PlayerStackingAndUnstacking>();
        }


        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Customer") && PlayerStackingAndUnstacking.ClothObject.Count > 0 && !other.GetComponent<Clone.Control._CustomerControl>().clothTookFromPlayer && 
                other.GetComponent<Clone.Control._CustomerControl>().move.isRechedStation )
            {
                //cSUI.StartUIMoving(other.transform.position);
                Instantiate(gm.customerUI, other.transform.position + customerUISpwanOffset, Quaternion.identity);
                
                PlayerStackingAndUnstacking.RemoveCloth(other);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Customer") && !gm.GameplayPause &&  PlayerStackingAndUnstacking.ClothObject.Count > 0 && !other.GetComponent<Clone.Control._CustomerControl>().clothTookFromPlayer &&
                other.GetComponent<Clone.Control._CustomerControl>().move.isRechedStation)
            {
                //cSUI.StartUIMoving(other.transform.position);
                Instantiate(gm.customerUI, other.transform.position + customerUISpwanOffset, Quaternion.identity);

                PlayerStackingAndUnstacking.RemoveCloth(other);
            }
        }
    }
}
