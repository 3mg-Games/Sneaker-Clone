using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSwitch : MonoBehaviour
{
    Sneaker.Control.ProcessingStationRack rack;
    public Collider col1, col2;
    void Start()
    {
        rack = GetComponent<Sneaker.Control.ProcessingStationRack>();
    }


    void Update()
    {
        if (rack.Unlock)
        {
            col1.enabled = true;
            col2.enabled = false;
        }
        if (!rack.Unlock)
        {
            col2.enabled = true;
            col1.enabled = false;
        }
    }
}
