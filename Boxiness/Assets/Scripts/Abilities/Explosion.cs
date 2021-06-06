using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Explosion : MonoBehaviour
{

    private bool damageSelf;
    private int damage;

    private void Start()
    {
        damageSelf = transform.GetComponentInParent<Bullet1>().damageSelf;
        damage = transform.GetComponentInParent<Bullet1>().damage;
    }

    void Update()
    {
        if(GetComponent<ParticleSystem>().isPlaying == true)
        {

            Vector2 triggerRadius = new Vector2(GetComponentInParent<Bullet1>().expRadius, GetComponentInParent<Bullet1>().expRadius);

            Collider2D[] exploders = Physics2D.OverlapBoxAll(transform.position, triggerRadius, 0);

            foreach(Collider2D objToKill in exploders)
            {
                //If object is Klett
                if(objToKill.tag == "Klett") objToKill.GetComponent<Klett>().Kill(true);

                //If object is Enemy
                if (objToKill.tag == "Enemy") objToKill.GetComponent<Enemy1>().Die(true);

                //If it's the stupid player deal damage!!!
                if (objToKill.tag == "Player") if(damageSelf == true) objToKill.GetComponent<Player>().TakeDamage(damage);
            }
        }
    }
}
