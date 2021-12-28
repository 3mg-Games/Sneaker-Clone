using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneaker.Movement
{
    public class dottedCircle : MonoBehaviour
    {
        public bool occupied = false;
        public float speed = 5;
        public float Rotate;

        private float a;
        float C;
        private SpriteRenderer SR;
        private Sneaker.Core._LevelManager levelManager;
        private void Start()
        {
            //levelManager = FindObjectOfType<Clone.Core._LevelManager>();
            levelManager = GetComponentInParent<Sneaker.Core._LevelManager>();
            StartCoroutine(StationPositionAdd(0.5f));
            SR = GetComponent<SpriteRenderer>();
            C = SR.color.a;
        }

        IEnumerator StationPositionAdd(float t)
        {
            yield return new WaitForSeconds(t);
            if (!levelManager.StationPosition.Contains(this.gameObject) && !occupied && this.gameObject.activeSelf)
                levelManager.StationPosition.Add(this.gameObject);
        }
        void Update()
        {
            a += speed * Time.deltaTime;            
            transform.rotation = Quaternion.Euler(90, a, 0);
            occupieding();
            if (occupied)
            {
                if (C > 0)
                {
                    C -= Time.deltaTime;
                }
                SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, C);
            }
        }

        void occupieding()
        {
            if (!levelManager.StationPosition.Contains(this.gameObject) && !occupied)
            {
                levelManager.StationPosition.Add(this.gameObject);
                SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 1);
            }
            if (levelManager.StationPosition.Contains(this.gameObject) && occupied)
                levelManager.StationPosition.Remove(this.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {

           
        }
    }
}
