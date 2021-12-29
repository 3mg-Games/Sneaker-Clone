using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackColliderActivator : MonoBehaviour
{
    public bool isColliderActivate;
    public bool isp;
    public Collider col;

    private void Start()
    {
        if(FindObjectOfType<Sneaker.Core.GameManager>().Level>0 || isColliderActivate)
        {
            col.enabled = true;
        }
    }
    void Update()
    {
        if (!isColliderActivate)
        {
            col.enabled = false;
        }

        if (isColliderActivate)
        {
            col.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isp = true;
        }
    }

}
