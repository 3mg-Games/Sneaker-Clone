using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Clone.Core
{
    public class CustomerUI : MonoBehaviour
    {
        public Image Cloth;
        public Transform UIHolder;
        public Vector3 HolderPositionOffset;

        public Clone.Control._CustomerControl CC;

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
            if (CC.NeedItemCode == CC.move.levelManager.Rack_0.ClothIDNumber)
                Cloth.sprite = CC.move.levelManager.RackCloth_0;
            if (CC.NeedItemCode == CC.move.levelManager.Rack_1.ClothIDNumber)
                Cloth.sprite = CC.move.levelManager.RackCloth_1;
            if (CC.NeedItemCode == CC.move.levelManager.Rack_2.ClothIDNumber)
                Cloth.sprite = CC.move.levelManager.RackCloth_2;
            if (CC.NeedItemCode == CC.move.levelManager.Rack_3.ClothIDNumber)
                Cloth.sprite = CC.move.levelManager.RackCloth_3;
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
