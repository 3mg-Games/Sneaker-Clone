using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{

    private Sneaker.Core.GameManager gm;
    public Sneaker.Core.Tutorial tut;
    public bool s2;

    float x = 2f;
    float y = 2f;
    private void Awake()
    {
        x = 5f;
        y = 5f;
        gm = FindObjectOfType<Sneaker.Core.GameManager>();
        if (!s2 && tut.CustomerServing || gm.Level > 0)
        {
            Destroy(this.gameObject);
        }
        if(s2 && tut.tutorial2Over || gm.Level > 5)
        {
            GetComponent<Sneaker.Movement._CustomerMovement>().target.GetComponent<Sneaker.Movement.dottedCircle>().occupied = false;
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if (tut.CustomerServing)
            x -= Time.deltaTime;
        if (!s2 && tut.CustomerServing && x<=0 || gm.Level > 0)
        {
            Destroy(this.gameObject);
        }

        if (tut.tutorial2Over)
        {
            y -= Time.deltaTime;
        }
        if(y <= 0 && s2 && tut.tutorial2Over || gm.Level > 5)
        {
            GetComponent<Sneaker.Movement._CustomerMovement>().target.GetComponent<Sneaker.Movement.dottedCircle>().occupied = false;
            Destroy(this.gameObject);
        }
    }
}
