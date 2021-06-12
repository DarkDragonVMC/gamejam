using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

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

    //Schlampigkeit
    private bool ripped;
    private bool notyetready;
    private bool movement;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();

        //Shoot
        if (Input.GetKeyDown(KeyCode.Mouse0))
            ShootHook();

        //Update Rope location
        lr.SetPosition(1, transform.position);

        curDis = Vector2.Distance(transform.position, new Vector2(mPos.x, mPos.y));

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            rb.velocity = Vector2.zero;

        if (curDis == 0) return;
        if (ripped == true) return;

        if (curDis > maxDis && !notyetready)
        {
            ripped = true;
            lr.SetPosition(0, Vector3.zero);
            lr.enabled = false;
            Debug.LogError("Ripped + TODO: Display");
            curDis = 0;
            rb.velocity = Vector2.zero;
            return;
        }

        if (curDis > startDis + 0.75f && !notyetready)
        {
            ripped = true;
            lr.SetPosition(0, Vector3.zero);
            lr.enabled = false;
            Debug.LogError("Ripped + TODO: Display");
            curDis = 0;
            rb.velocity = Vector2.zero;
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
    }

    void ShootHook()
    {
        lr.enabled = true;
        ripped = false;
        movement = true;
        mPos = Input.mousePosition;
        mPos = Camera.main.ScreenToWorldPoint(mPos);

        curDis = Vector2.Distance(transform.position, new Vector2(mPos.x, mPos.y));
        startDis = Vector2.Distance(transform.position, new Vector2(mPos.x, mPos.y));

        if (curDis > maxDis)
        {
            lr.enabled = false;
            Debug.LogError("Too long + TODO: Display");
            curDis = 0;
            startDis = 0;
            notyetready = true;
            movement = false;
            return;
        }

        notyetready = false;
        lr.SetPosition(0, new Vector3(mPos.x, mPos.y, 0));
    }

    void ChangeColor()
    {
        if (curDis > startDis - 0.5f) lr.material = rope_redm;
        else lr.material = ropem;
    }
}
