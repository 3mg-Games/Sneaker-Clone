using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp1 : MonoBehaviour
{
    public Transform target;
    public bool x;
    private void Update()
    {
        if (Input.GetKey(KeyCode.L)|| x)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
    }
}
