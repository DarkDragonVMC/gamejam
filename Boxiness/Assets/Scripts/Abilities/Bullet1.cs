using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{

    public float speed = 20f;
    public float range = 0.6f;
    public bool explosive;
    public float expRadius;
    public int damage;

    public bool damageSelf;

    //Bounciness
    public bool bouncy;
    private Vector3 lastVelocity;
    private BounceOff bounceoff;

    private Rigidbody2D rb;

    //Particles
    private ParticleSystem par;
    private ParticleSystem exp;
    public bool waitForKill = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * -speed;

        bounceoff = this.GetComponent<BounceOff>();

        bounceoff.bouncy = bouncy;

        //Destory after Time
        Invoke("ShouldDie", range);

        //Particle System
        par = transform.GetChild(0).GetComponent<ParticleSystem>();
        exp = transform.Find("Explosion").GetComponent<ParticleSystem>();

        exp.Stop();
        par.Stop();
    }

    void Update()
    {
        if (waitForKill == true) if (exp.isPlaying == false) if (par.isPlaying == false) Kill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Untagged")
        {
            if (bouncy == false)
            {
                if (explosive == false) if (par.isPlaying == false) par.Play();
                if (explosive == true) if (exp.isPlaying == false) exp.Play();

                waitForKill = true;
                transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(2).Find("Dropshadow").GetComponent<SpriteRenderer>().enabled = false;
                rb.velocity = transform.right * -speed * 0.15f;
            }
        }

        if (collision.tag == "Box")
        {
            if (bouncy == false)
            {
                if (explosive == false) if (par.isPlaying == false) par.Play();
                if (explosive == true) if (exp.isPlaying == false) exp.Play();

                waitForKill = true;
                transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(2).Find("Dropshadow").GetComponent<SpriteRenderer>().enabled = false;
                rb.velocity = transform.right * -speed * 0.15f;
            }
        }

        if (collision.tag == "Klett")
        {
            if (bouncy == false)
            {
                if (explosive == false) if (par.isPlaying == false) par.Play();
                if (explosive == true) if (exp.isPlaying == false) exp.Play();

                waitForKill = true;
                transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(2).Find("Dropshadow").GetComponent<SpriteRenderer>().enabled = false;
                rb.velocity = transform.right * -speed * 0.15f;
            }
        }

        if (collision.tag == "Enemy")
        {
            if(bouncy == false)
            {
                if (explosive == false) if (par.isPlaying == false) par.Play();
                if (explosive == true) if (exp.isPlaying == false) exp.Play();

                waitForKill = true;
                transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(2).Find("Dropshadow").GetComponent<SpriteRenderer>().enabled = false;
                rb.velocity = transform.right * -speed * 0.15f;
            }
        }

        if(collision.tag == "Player")
        {
            if(damageSelf == true)
            {
                if(bouncy == false)
                {
                    if (explosive == false) if (par.isPlaying == false) par.Play();
                    if (explosive == true) if (exp.isPlaying == false) exp.Play();

                    waitForKill = true;
                    transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
                    transform.GetChild(2).Find("Dropshadow").GetComponent<SpriteRenderer>().enabled = false;
                    rb.velocity = transform.right * -speed * 0.15f;

                    GameObject.Find("Player").GetComponent<Player>().TakeDamage(damage);
                } 
                
                else
                {
                    GameObject.Find("Player").GetComponent<Player>().TakeDamage(damage);
                }
            }
        }
    }

    void ShouldDie()
    {
        if (explosive == false) Kill();
        if (explosive == true)
        {
            if (exp.isPlaying == false) exp.Play();
            waitForKill = true;
            transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(2).Find("Dropshadow").GetComponent<SpriteRenderer>().enabled = false;
            rb.velocity = transform.right * -speed * 0.15f;
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }

}
