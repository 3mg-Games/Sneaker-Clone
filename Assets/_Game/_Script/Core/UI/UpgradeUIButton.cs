using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clone.Core
{
    public class UpgradeUIButton : MonoBehaviour
    {
        public void DesableUI()
        {
            if (this.gameObject.activeSelf)
                this.gameObject.SetActive(false);
        }
    }
}
