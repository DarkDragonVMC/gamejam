using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private LineRenderer lr;
    public float speed;
    public float maxDis;
    public float curDis;
    public float startDis;

    private Vector3 mPos;

    public Material ropem;
    public Material rope_redm;

    public float animationTime;

    public RaycastHit2D anchor;

    public int hooks;

    //Script References
    private AudioManager am;

    //Schlampigkeit
    private bool ripped;
    private bool notyetready;
    private bool movement;

    public LayerMask Mask;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        hooks = 3;
        GameObject.Find("HookDisplay").GetComponent<Text>().text = "Hooks: " + hooks;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) GameObject.Find("LevelManager").GetComponent<LevelManager>().SwitchToScene(0);

        if (!movement)
        {
            rb.velocity = Vector2.zero;
        }
        
        ChangeColor();

        //Shoot
        if (Input.GetKeyDown(KeyCode.Mouse0))
            ShootHook();

        //Update Rope location
        lr.SetPosition(1, transform.position);

        curDis = Vector2.Distance(transform.position, new Vector2(anchor.point.x, anchor.point.y));

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            rb.velocity = Vector2.zero;

        if (curDis == 0) return;
        if (ripped == true) return;

        if (curDis > maxDis && !notyetready)
        {
            //Audio
            am.Play("rip");

            ripped = true;
            lr.SetPosition(0, Vector3.zero);
            lr.enabled = false;
            Debug.LogError("Ripped1 + TODO: Display");
            curDis = 0;
            rb.velocity = Vector2.zero;
            GameObject.Find("Toolong").GetComponent<Animation>().Play();
            return;
        }

        if (curDis > startDis + 1f && !notyetready)
        {
            //Audio
            am.Play("rip");

            ripped = true;
            lr.SetPosition(0, Vector3.zero);
            lr.enabled = false;
            Debug.LogError("Ripped2 + TODO: Display");
            curDis = 0;
            rb.velocity = Vector2.zero;
            GameObject.Find("Toolong").GetComponent<Animation>().Play();
            return;
        }

        //Movement
        
        if(movement)

        if (Input.GetKey(KeyCode.W))
            rb.velocity = new Vector2(0, speed);
        else if (Input.GetKey(KeyCode.A))
            rb.velocity = new Vector2(-speed, 0);
        else if (Input.GetKey(KeyCode.S))
            rb.velocity = new Vector2(0, -speed);
        else if (Input.GetKey(KeyCode.D))
            rb.velocity = new Vector2(speed, 0);
        if (!movement)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void ShootHook()
    {
        if(hooks == 0)
        {
            return;
        }

        lr.enabled = true;
        ripped = false;
        movement = true;
        mPos = Input.mousePosition;
        mPos = Camera.main.ScreenToWorldPoint(mPos);
        Vector2 dir = new Vector2(mPos.x - transform.position.x, mPos.y - transform.position.y);

        anchor = Physics2D.Raycast(transform.position, dir, maxDis, Mask);

        if(anchor.collider == null)
        {
            lr.enabled = false;
            ripped = true;
            movement = false;
            Debug.LogError("Too long + TODO: Display");
            am.Play("too_long");
            return;
        }

        if(curDis >= maxDis)
        {
            lr.enabled = false;
            ripped = true;
            movement = false;
            Debug.LogError("Too long + TODO: Display");
            am.Play("too_long");
            rb.velocity = Vector2.zero;
            return;
        }

        curDis = Vector2.Distance(transform.position, new Vector2(anchor.point.x, anchor.point.y));
        startDis = Vector2.Distance(transform.position, new Vector2(anchor.point.x, anchor.point.y));


        notyetready = false;
        lr.SetPosition(0, new Vector3(anchor.point.x, anchor.point.y, 0));
        StartCoroutine(AnimateRope());

        //Audio
        am.Play("Shoot");
        Invoke("PlayHook", animationTime * startDis - 0.11f);

        hooks -= 1;
        GameObject.Find("HookDisplay").GetComponent<Text>().text = "Hooks: " + hooks;
    }

    void ChangeColor()
    {
        if (curDis > startDis) lr.material = rope_redm;
        else lr.material = ropem;
    }

    IEnumerator AnimateRope()
    {
        float startTime = Time.time;

        Vector3 startPosition = lr.GetPosition(1);
        Vector3 endPosition = lr.GetPosition(0);

        Vector3 pos = startPosition;

        while(pos != endPosition)
        {
            float t = (Time.time - startTime) / animationTime / startDis;
            pos = Vector3.Lerp(startPosition, endPosition, t);
            lr.SetPosition(0, pos);
            yield return null;
        }
    }

    void PlayHook()
    {
        am.Play("hook");
    }
}
