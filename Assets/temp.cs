using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    public int y;
    public temp1 t;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) || GetComponent<Sneaker.Movement._CustomerMovement>().isRechedStation)
        {
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z), Quaternion.Euler(transform.eulerAngles.x, y, transform.eulerAngles.z), 10 * Time.deltaTime);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("x"))
        {
            t.x = true;
        }
    }
}
