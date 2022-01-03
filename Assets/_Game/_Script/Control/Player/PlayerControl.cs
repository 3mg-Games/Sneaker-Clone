using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneaker.Control
{
    public class PlayerControl : MonoBehaviour
    {
        Sneaker.Core.PlayerStackingAndUnstacking PlayerStackingAndUnstacking;
        Sneaker.Core.GameManager gm;
        Sneaker.Movement._PlayerMovment move;
        public Sneaker.Core.customerServedUI cSUI;

        public Vector3 customerUISpwanOffset;
        void Start()
        {
            gm = FindObjectOfType<Sneaker.Core.GameManager>();
            PlayerStackingAndUnstacking = GetComponent<Sneaker.Core.PlayerStackingAndUnstacking>();
            move = GetComponent<Sneaker.Movement._PlayerMovment>();
        }


        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Customer") && PlayerStackingAndUnstacking.ClothObject.Count > 0 && !other.GetComponent<Sneaker.Control._CustomerControl>().clothTookFromPlayer && 
                other.GetComponent<Sneaker.Control._CustomerControl>().move.isRechedStation )
            {
                Debug.LogError("Tag");
                PlayerStackingAndUnstacking.RemoveCloth(other, other.GetComponent<Sneaker.Control._CustomerControl>().NeedItemCode, customerUISpwanOffset);
            }

   /*         if (other.gameObject.CompareTag("PackingPlace") && move.direction.magnitude >=0.1f)
            {
                PlayerStackingAndUnstacking.RemoveClothForPacking(other, other.GetComponent<Clone.Control.PackingStation>().clothCodeInput, customerUISpwanOffset);
            }*/

        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Customer") && !gm.GameplayPause &&  PlayerStackingAndUnstacking.ClothObject.Count > 0 && !other.GetComponent<Sneaker.Control._CustomerControl>().clothTookFromPlayer &&
                other.GetComponent<Sneaker.Control._CustomerControl>().move.isRechedStation)
            {
                Debug.LogError("Tag");
                PlayerStackingAndUnstacking.RemoveCloth(other, other.GetComponent<Sneaker.Control._CustomerControl>().NeedItemCode, customerUISpwanOffset);
            }

        /*    if (other.gameObject.CompareTag("PackingPlace") && move.direction.magnitude >= 0.1f)
            {
                PlayerStackingAndUnstacking.RemoveClothForPacking(other, other.GetComponent<Clone.Control.PackingStation>().clothCodeInput, customerUISpwanOffset);
            }*/
        }
    }
}
