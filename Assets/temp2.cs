using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp2 : MonoBehaviour
{
    public Sneaker.Control._CustomerControl move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move.clothTookFromPlayer)
        {
            GetComponent<Animator>().SetBool("complete", true);
        }
        if(move !=null && move.clothTookFromPlayer)
        {
            transform.parent.transform.LookAt(new Vector3(move.transform.position.x, transform.position.y, move.transform.position.z));
        }
    }
}
