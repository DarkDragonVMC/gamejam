using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public int Health = 4;
    public int maxHealth = 4;

    private GameManager man;

    //Unbesiegbar
    private bool invincible = false;
    public float invincTime = 2.0f;
    private Color Full;
    private Color notFull;

    private HealthBar hb;

    //Camera
    private Camera cam;

    //Audio Manager
    public AudioManager aud;

    // Start is called before the first frame update
    void Start()
    {
        hb = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        man = GameObject.Find("GameManager").GetComponent<GameManager>();

        Health = maxHealth;
        hb.SetMaxHealth(maxHealth);

        invincible = false;
        Full = this.GetComponent<SpriteRenderer>().color;
        Full.a = 1f;
        notFull = this.GetComponent<SpriteRenderer>().color;
        notFull.a = 0.4f;
        this.GetComponent<SpriteRenderer>().color = Full;

        //Camera
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damDeal)
    {
        if(invincible == false)
        {
            //Play sound
            aud.Play("hurt");

            Health -= damDeal;
            StartCoroutine(cam.Shake(0.4f,0.7f));
            invincible = true;
            this.GetComponent<SpriteRenderer>().color = notFull;
            Invoke("vincible", invincTime);
            if (Health <= 0)
            {
                Health = 0;
                hb.SetHealth(Health);
                Die();
            }
            else hb.SetHealth(Health);
        }
    }

    void Die()
    {
        man.GameOver();
    }

    void vincible()
    {
        invincible = false;
        this.GetComponent<SpriteRenderer>().color = Full;
    }

}
