using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Klett : MonoBehaviour
{

    public bool onPlayer = false;
    public float speed = 0.2f;
    public float KlettTime = 15f;

    private GameObject Player;
    private Rigidbody2D rb;
    private Camera cam;

    private GameObject currentSlot;

    //Particle System
    private ParticleSystem par;

    private bool shouldDie = false;

    //Levels
    [Header("Levelsystem")]

    private LevelSystem levels;
    public int xpReward;

    //Audio Manager
    private AudioManager aud;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        par = transform.Find("Particle System").GetComponent<ParticleSystem>();
        aud = GameObject.Find("Player").GetComponent<AudioManager>();

        if (par.isPlaying == true) par.Stop();

        //Levels
        levels = GameObject.Find("LevelBar").GetComponent<LevelSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dirToMove = transform.position - Player.transform.position;

       if(onPlayer == false)
        {
            rb.AddForce(dirToMove * -speed);
        }

        //Kill
        if (shouldDie == true) if (par.isPlaying == false)
            {
                Destroy(gameObject);
                return;
            }
    }

    private void LateUpdate()
    {
        if (onPlayer == true) transform.position = currentSlot.transform.position;
    }

    public void anheften(GameObject slot)
    {
        if(onPlayer == false)
        {
            //Play sound
            aud.Play("anheften");

            onPlayer = true;
            rb.velocity = Vector2.zero;
            transform.position = slot.transform.position;
            currentSlot = slot;
            slot.GetComponent<Haken>().full = true;
            StartCoroutine(cam.Shake(0.25f, 0.3f));
            Invoke("DamagePlayer", KlettTime);
        }
    }

    private void DamagePlayer()
    {
        //Play sound
        aud.Play("enemyDie");

        Player.GetComponent<Player>().TakeDamage(1);
        Kill(false);
    }

    public void Kill(bool gainExp)
    {
        if(shouldDie == false)
        {
            //Play sound
            aud.Play("enemyDie");

            if (par.isPlaying == false) par.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            transform.Find("Dropshadow").GetComponent<SpriteRenderer>().enabled = false;
            transform.Find("Eye1").GetComponent<SpriteRenderer>().enabled = false;
            transform.Find("Eye2").GetComponent<SpriteRenderer>().enabled = false;
            if(currentSlot != null) currentSlot.GetComponent<Haken>().full = false;

            //Add xp Reward to Player
            if (shouldDie == false) if(gainExp == true) levels.addXp(xpReward);

            shouldDie = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "NorthWall") if(onPlayer == true)
        {
            CancelInvoke();
            Kill(true);
        }

        if (collision.name == "SouthWall") if (onPlayer == true)
            {
            CancelInvoke();
            Kill(true);
        }

        if (collision.name == "EastWall") if (onPlayer == true)
            {
            CancelInvoke();
            Kill(true);
        }

        if (collision.name == "WestWall") if (onPlayer == true)
            {
            CancelInvoke();
            Kill(true);
        }

        if (collision.tag == "Box") if (onPlayer == true)
            {
                CancelInvoke();
                Kill(true);
            }


        if (collision.tag == "Player_Bullet")
        {
            CancelInvoke();
            Kill(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player_Bullet")
        {
            CancelInvoke();
            Kill(true);
        }

        if (collision.collider.tag == "Exp") Kill(true);
    }

}
