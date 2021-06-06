using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haken : MonoBehaviour
{

    public bool full = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(full == false) if(collision.tag == "Klett")
        {
            collision.GetComponent<Klett>().anheften(gameObject);
        }
    }

}
