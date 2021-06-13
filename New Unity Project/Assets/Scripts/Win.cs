using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    public GameObject win;

    private void Start()
    {
        win = GameObject.Find("Win");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        win.GetComponent<Text>().enabled = true;
    }
}
