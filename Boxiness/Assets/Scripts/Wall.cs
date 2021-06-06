using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    private Player p;

    private void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.name == "Player") p.TakeDamage(1);
    }
}
