using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _CustomerAnimationEvents : MonoBehaviour
{
    public void ResetAnimation()
    {
        GetComponent<Animator>().SetBool("tradeComplete", false);
    }
}
