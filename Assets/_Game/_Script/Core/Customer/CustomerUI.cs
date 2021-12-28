using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sneaker.Core
{
    public class CustomerUI : MonoBehaviour
    {
        public Image Cloth;
        public Transform UIHolder;
        public Vector3 HolderPositionOffset;

        public Sneaker.Control._CustomerControl CC;

        void Start()
        {
            UIHolder.gameObject.SetActive(false);
        }

        void Update()
        {
            AskForCloth();
            HolderAppearance();
        }

        void AskForCloth()
        {
            try
            {
                if (CC.NeedItemCode == CC.move.levelManager.Rack0.ClothIDNumber)
                    Cloth.sprite = CC.move.levelManager.RackCloth0;

                if (CC.move.levelManager.Rack1 != null && CC.NeedItemCode == CC.move.levelManager.Rack1.ClothIDNumber)
                    Cloth.sprite = CC.move.levelManager.RackCloth1;

                if (CC.move.levelManager.PStationRack0 != null &&  CC.NeedItemCode == CC.move.levelManager.PStationRack0.ClothIDNumber)
                    Cloth.sprite = CC.move.levelManager.RackCloth2;
                if (CC.move.levelManager.PStationRack1 != null && CC.NeedItemCode == CC.move.levelManager.PStationRack1.ClothIDNumber)
                    Cloth.sprite = CC.move.levelManager.RackCloth3;
            }
            catch
            {

            }
           
        }

        void HolderAppearance()
        {
            UIHolder.forward = -Camera.main.transform.forward;

            if (CC.clothTookFromPlayer)
                UIHolder.gameObject.SetActive(false);

            if(CC.move.isRechedStation && !CC.clothTookFromPlayer)
                UIHolder.gameObject.SetActive(true);

        }
    }
}
