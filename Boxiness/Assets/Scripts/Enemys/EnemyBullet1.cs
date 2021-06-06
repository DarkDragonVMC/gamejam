using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet1 : MonoBehaviour
{

    public float speed;
    public float range;
    private Rigidbody2D rb;

    private GameObject p;

    //Particle System
    private ParticleSystem par;
    private bool shouldDie = false;

    void Start()
    {
        p = GameObject.Find("Player");

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        //Destory after time
        Invoke("Kill", range);

        //Particle System
        par = transform.GetChild(0).GetComponent<ParticleSystem>();

        par.Stop();

        if (shouldDie == true) if (par.isPlaying == false) Kill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            p.GetComponent<Player>().TakeDamage(1);

            if (par.isPlaying == false) par.Play();
            shouldDie = true;
            this.GetComponent<SpriteRenderer>().enabled = false;
            transform.Find("Dropshadow").GetComponent<SpriteRenderer>().enabled = false;
            rb.velocity = transform.right * -speed * 0.15f;
        }

        if(collision.tag == "Untagged")
        {
            if (par.isPlaying == false) par.Play();
            shouldDie = true;
            this.GetComponent<SpriteRenderer>().enabled = false;
            transform.Find("Dropshadow").GetComponent<SpriteRenderer>().enabled = false;
            rb.velocity = transform.right * -speed * 0.15f;
        }

        if (collision.tag == "Box")
        {
            if (par.isPlaying == false) par.Play();
            shouldDie = true;
            this.GetComponent<SpriteRenderer>().enabled = false;
            transform.Find("Dropshadow").GetComponent<SpriteRenderer>().enabled = false;
            rb.velocity = transform.right * -speed * 0.15f;
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

}
