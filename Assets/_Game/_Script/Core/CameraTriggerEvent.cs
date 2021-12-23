using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerEvent : MonoBehaviour
{

    public void unPauseGame()
    {
        StartCoroutine(delay(0.7f));
    }

    IEnumerator delay(float t)
    {
        yield return new WaitForSeconds(t);
        if (FindObjectOfType<Clone.Core.GameManager>().GameplayPause)
            FindObjectOfType<Clone.Core.GameManager>().GameplayPause = false;
    }
    public void doNothing()
    {

    }
}
