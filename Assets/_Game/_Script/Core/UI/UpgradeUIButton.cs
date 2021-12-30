using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneaker.Core
{
    public class UpgradeUIButton : MonoBehaviour
    {
        private AudioManager audio;
        private void Start()
        {
            audio = FindObjectOfType<AudioManager>();
        }
        public void DesableUI()
        {
            if (this.gameObject.activeSelf)
                this.gameObject.SetActive(false);
        }
        public void UpgradeButton()
        {
            audio.source.PlayOneShot(audio.UpgradeButton);
        }

        public void SendProgressionData()
        {
            FindObjectOfType<GASetup>().LevelComplete(FindObjectOfType<Sneaker.Core.GameManager>().Level + 1);
        }
    }
}
