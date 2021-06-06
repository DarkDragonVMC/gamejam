using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour
{

    private bool q = false;

    public GameObject bullet;
    public Transform FirePoint;

    private bool cooldown = false;
    private bool bursting = false;

    public Weapon weapon;

    public float delay = 0.4f;
    public int damage;
    public float speed = 15;
    public float range = 1.75f;
    public Vector2 bloom;

    public bool bouncy;
    public bool damageSelf;
    public bool path;
    public GameObject pathObj;

    public bool explosive;
    public float expRadius;

    public float rotateSpeed;

    public int bulletsPerTap;
    public float burstDelay;

    public GameObject bulletSkin;

    public int QEdir = 2;

    //Beeing stuck when shooting
    public bool stuck;
    public Move mv;

    //Audio
    public AudioManager aud;

    // Start is called before the first frame update
    void Start()
    {
        SetWeapon(weapon);
    }

    // Update is called once per frame
    void Update()
    {
        //If Input
        if (Input.GetKeyDown(KeyCode.Space)) q = true;
        if (Input.GetKeyUp(KeyCode.Space)) q = false;

        if (Input.GetKey(KeyCode.Q)) QEdir = 0;
        if (Input.GetKey(KeyCode.E)) QEdir = 1;

        if (!Input.GetKey(KeyCode.E)) if (!Input.GetKey(KeyCode.Q)) QEdir = 2;
        if (Input.GetKey(KeyCode.Q)) if (Input.GetKey(KeyCode.E)) QEdir = 2;

        if (Input.GetKey(KeyCode.Space))
        {
            if (path == true) QEdir = 2;
            Fire();
        }

        //Rotate
        if(QEdir == 0) this.transform.RotateAround(this.transform.parent.parent.position, this.transform.parent.parent.forward, rotateSpeed * Time.deltaTime);
        if (QEdir == 1) this.transform.RotateAround(this.transform.parent.parent.position, this.transform.parent.parent.forward, -rotateSpeed * Time.deltaTime);
    }

    void Fire()
    {
        if(cooldown == false)
        {
            if (stuck == true) mv.stuck = true;

            //Configure bullet
            bullet.GetComponent<Bullet1>().range = range;
            bullet.GetComponent<Bullet1>().speed = speed;
            bullet.GetComponent<Bullet1>().explosive = explosive;
            bullet.GetComponent<Bullet1>().expRadius = expRadius;
            bullet.GetComponent<Bullet1>().damage = damage;
            bullet.GetComponent<Bullet1>().bouncy = bouncy;
            bullet.GetComponent<Bullet1>().damageSelf = damageSelf;
            if (explosive == true) bullet.transform.Find("Explosion").GetComponent<ParticleSystem>().startSize = expRadius;


            for (int i = 0; i < bulletsPerTap; i++)
            {
                if(bursting == false)
                {
                    //Calculate spray
                    Vector3 spray = new Vector3(0, 0, Random.Range(bloom.x, bloom.y));

                    var bulletObj = Instantiate(bullet, FirePoint.position, FirePoint.rotation * Quaternion.Euler(spray));
                    var bSkin = Instantiate(bulletSkin, bulletObj.transform.position, bulletObj.transform.rotation);
                    bSkin.transform.parent = bulletObj.transform;

                    //Play Sound
                    aud.Play("shoot");
                }

                if (bulletsPerTap > 1)
                {
                    bursting = true;
                    if(burstDelay > 0) Invoke("ResetBurst", burstDelay);
                    if (burstDelay <= 0) ResetBurst();
                }
            }

            cooldown = true;
            Invoke("ResetCooldown", delay);
        }
    }

    void ResetCooldown()
    {
        cooldown = false;
    }

    void ResetBurst()
    {
        bursting = false;
    }

    public void SetWeapon(Weapon weapon)
    {
        delay = weapon.delay;
        damage = weapon.damage;
        speed = weapon.speed;
        range = weapon.range;
        explosive = weapon.explosive;
        expRadius = weapon.expRadius;
        rotateSpeed = weapon.roatateSpeed;
        stuck = weapon.stuck;
        bulletsPerTap = weapon.bulletsPerTap;
        burstDelay = weapon.burstDelay;
        bloom = weapon.bloom;
        bouncy = weapon.bouncy;
        damageSelf = weapon.damageSelf;
        bulletSkin = weapon.bulletSkin;
        path = weapon.path;

        if (path == true) pathObj.SetActive(true);
        if (path == false) pathObj.SetActive(false);
    }
}
