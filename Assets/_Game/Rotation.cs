using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed;
    float x;

    void Update()
    {
        x += speed;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, x);
    }
}
