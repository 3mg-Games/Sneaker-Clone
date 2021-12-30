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
        if (FindObjectOfType<Sneaker.Core.GameManager>().Level > 0 || isColliderActivate || FindObjectOfType<Sneaker.Core.Tutorial>().CustomerServing)
        {
            col.enabled = true;
        }
    }
    void Update()
    {
        if (!isColliderActivate && FindObjectOfType<Sneaker.Core.GameManager>().Level <= 0 && !FindObjectOfType<Sneaker.Core.Tutorial>().CustomerServing)
        {
            col.enabled = false;
        }

        if (isColliderActivate || FindObjectOfType<Sneaker.Core.Tutorial>().CustomerServing)
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
