using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAVE : MonoBehaviour
{
    public float saveGame;
    public float Save;
    
    void Start()
    {
        Save = saveGame;
    }


    void Update()
    {
        if (Save >= 0)
            Save -= Time.deltaTime;
        if (Save <= 0)
        {
            try
            {
                FindObjectOfType<SaveAndLoad>().SaveGame();
                FindObjectOfType<SaveAndLoadSection1>().SaveGame();
                FindObjectOfType<SaveAndLoadSection2>().SaveGame();
                FindObjectOfType<SaveAndLoadSection3>().SaveGame();
            }
            catch
            {

            }
            Save = saveGame;
        }
    }
}
