using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class test2 : MonoBehaviour
{
    public Rig rig;
    public bool t;
    public Animator a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(t)
        {
            if(rig.weight <= 01)
            {
                rig.weight += 2*Time.deltaTime;
            }
            if(rig.weight >= 0.4f && rig.weight <= 0.5f)
            {
                GetComponent<Animator>().SetBool("tt", true);
                StartCoroutine(cam(1f));
            }
        }
    }

    public void dataTrue()
    {
        t = true;
    }
    IEnumerator cam(float t)
    {
        yield return new WaitForSeconds(t);
        a.SetBool("t", true);

    }
}
