using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneaker.Core
{
    public class bonusUI : MonoBehaviour
    {
        [HideInInspector] public int amount;
        private AudioManager audio;
        void Start()
        {
            audio = FindObjectOfType<AudioManager>();
        }


        void Update()
        {

        }

        public void DesableUI()
        {
            if (this.gameObject.activeSelf)
                this.gameObject.SetActive(false);
        }
        public void addMoney()
        {
            FindObjectOfType<GameManager>().MaxMoney += amount;
        }
        public void Upgrade()
        {
            audio.source.PlayOneShot(audio.Upgrade);
        }
        public void MoneyCounting()
        {
            audio.source.PlayOneShot(audio.MoneyCounting);
        }
    }
}
