using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{

    private Sneaker.Core.GameManager gm;


    private void Awake()
    {
        gm = FindObjectOfType<Sneaker.Core.GameManager>();
        if (gm.Level > 0)
        {
            Destroy(this.gameObject);
        }
    }
}
