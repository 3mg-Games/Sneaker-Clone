using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FashionM.Core
{
    public class DropCoin : MonoBehaviour
    {
        public float force;
        public int Coins = 100;
        private bool StoreToPlayer;
        private Sneaker.Core.AudioManager audioManager;
        public AudioSource audioSource;
        public void detatachFromClient()
        {
            transform.parent = null;
        }
        public void Destroy()
        {
            Destroy(this.gameObject);
        }

        void Start()
        {
            transform.rotation = Random.rotation;
            audioManager = FindObjectOfType<Sneaker.Core.AudioManager>();
            GetComponent<Rigidbody>().AddForce(transform.up * force, ForceMode.Impulse);
            GetComponent<Rigidbody>().AddForce(transform.forward * (force), ForceMode.Impulse);
        }

        private void Update()
        {
            if (StoreToPlayer)
            {
                transform.GetComponent<Collider>().enabled = false;
                transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0,01,0), 50 * Time.deltaTime);
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("fittingStation"))
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
            if (collision.gameObject.CompareTag("Player") && !GetComponent<Rigidbody>().isKinematic)
            {
                StoreToPlayer = true;
                //audioManager.source.PlayOneShot(audioManager.CollectingMoney);
                if (audioSource != null)
                    audioSource.Play();
                //GetComponent<Rigidbody>().isKinematic = true;
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<Sneaker.Core.GameManager>().MaxMoney += Coins;

                //GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().dailyAmount += Coins;
                Destroy(this.gameObject, 0.15f);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if ( other.gameObject.CompareTag("fittingStation"))
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
            if (other.gameObject.CompareTag("Player") && GetComponent<Rigidbody>().isKinematic)
            {
                StoreToPlayer = true;


                if(audioSource !=null)
                    audioSource.Play();
                //audioManager.source.PlayOneShot(audioManager.CollectingMoney);
                //GetComponent<Rigidbody>().isKinematic = true;
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<Sneaker.Core.GameManager>().MaxMoney += Coins;
                
                //GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().dailyAmount += Coins;
                Destroy(this.gameObject, 0.15f);
            }
        }

    }
}
