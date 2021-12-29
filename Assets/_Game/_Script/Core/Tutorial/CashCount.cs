using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashCount : MonoBehaviour
{
    float x;
    
    void Start()
    {
        x = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (x >= 0)
        {
            x -= Time.deltaTime;
        }
        if (x <= 0 && this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
            x = 2;
        }
    }
}
