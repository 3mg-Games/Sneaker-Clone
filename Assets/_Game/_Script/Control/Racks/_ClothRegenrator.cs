using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneaker.Control
{
    public class _ClothRegenrator : MonoBehaviour
    {
        private Sneaker.Core.Racks rack;
        public GameObject Cloth;
        public Transform parant;
        public List<Vector3> spwanPoint = new List<Vector3>();

        void Start()
        {
            rack = GetComponent<Sneaker.Core.Racks>();
            a = l;
            if(rack.isRackOpen)
                spwanOnce();
        }


        void Update()
        {            
            if(rack.isRackOpen)
                spawnner();
        }

        public void spwanOnce()
        {
            if (rack.RackCloths.Count <= rack.LimitCountForCloth)
            {
                for (int i = 0; i <= rack.LimitCountForCloth; i++)
                {
                    GameObject c = Instantiate(Cloth);
                    c.transform.parent = parant;
                    c.transform.localPosition = spwanPoint[i];
                    
                }
            }
        }

        float l = 0.5f;
        int i = 0;
        float a;
        void spawnner()
        {
            if(rack.RackCloths.Count  <= rack.LimitCountForCloth)
            {
                a -= Time.deltaTime;
                if (a <= 0 && rack.RackCloths.Count <= rack.LimitCountForCloth)
                {                    
                    if(i<= rack.LimitCountForCloth)
                    {
                        GameObject c = Instantiate(Cloth);
                        c.transform.parent = parant;
                        c.transform.localPosition = spwanPoint[i];
                        a = l;
                        i++;
                    }
                    if (i == rack.LimitCountForCloth+1)
                        i = 0;
                }
                
            }
        }
    }
}
