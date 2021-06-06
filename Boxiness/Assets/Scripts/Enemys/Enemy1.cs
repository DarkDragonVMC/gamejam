using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{

    public float speed;
    public float fleeSpeed;
    public float stoppingDistance;
    public float fleeDistance;

    private Transform Player;

    private Rigidbody2D rb;

    //Shooting
    public GameObject bullet;
    public Transform FirePoint;

    private bool cooldown;
    public float delay;

    public Vector2 bloom;

    //Rotation
    public float rotateSpeed;

    //Particles
    private ParticleSystem explosion;
    private bool shouldDie = false;

    //other scripts
    private Player p;

    //Spawning
    private Spawner Spawner;
    public GameObject prefab;

    //Levels
    [Header("Levelsystem")]

    private LevelSystem levels;
    public int xpReward;

    //Audio
    private AudioManager aud;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        Spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        aud = GameObject.Find("Player").GetComponent<AudioManager>();

        cooldown = false;

        p = GameObject.Find("Player").GetComponent<Player>();

        explosion = transform.Find("Explosion").GetComponent<ParticleSystem>();

        if(explosion.isPlaying == true) explosion.Stop();

        shouldDie = false;

        //Levels
        levels = GameObject.Find("LevelBar").GetComponent<LevelSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //Start running
        Vector3 dirToMove = transform.position - Player.position;

        Vector2 rotateTo = new Vector2(Player.position.x - transform.position.x, Player.position.y - transform.position.y);
        transform.right = rotateTo * rotateSpeed * Time.deltaTime;

        if(Vector2.Distance(transform.position,Player.position) > stoppingDistance)
        {
            rb.AddForce(dirToMove * -speed);
        } else if(Vector2.Distance(transform.position, Player.position) < stoppingDistance && Vector2.Distance(transform.position, Player.position) > fleeDistance)
        {
            rb.velocity = Vector3.zero;
            Shoot();
        } else if(Vector2.Distance(transform.position, Player.position) < fleeDistance)
        {
            rb.AddForce(dirToMove * fleeSpeed);
            Shoot();
        }

        //Die
        if (shouldDie == true) if (explosion.isPlaying == false)
            {
                Destroy(gameObject);
                return;
            }
    }

    void Shoot()
    {
        Vector3 spray = new Vector3(0, 0, Random.Range(bloom.x, bloom.y));

        if (cooldown == false) if (shouldDie == false)
            {
                //play sound
                aud.Play("shoot");

                Instantiate(bullet, FirePoint.position, FirePoint.rotation * Quaternion.Euler(spray));
                cooldown = true;
                Invoke("Resetcooldown", delay);
            }
    }

    void Resetcooldown()
    {
        cooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") p.TakeDamage(1);
        if (collision.tag == "Player_Bullet") Die(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Exp") Die(true);
        if (collision.collider.tag == "Player") p.TakeDamage(1);
    }

    public void Die(bool gainExp)
    {
        if(shouldDie == false)
        {
        //Play sound
        aud.Play("enemyDie");

        transform.Find("Dropshadow").GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        if (explosion.isPlaying == false) explosion.Play();

        //Add xp Reward to Player
        if(gainExp == true) levels.addXp(xpReward);

        shouldDie = true;
        }
    }

}
