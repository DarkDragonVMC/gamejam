using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision)
        {
            UnityEngine.Debug.Log("Test");
        }
        
    }
}
