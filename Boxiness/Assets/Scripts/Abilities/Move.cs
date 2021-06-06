using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    private bool Space = false;

    public float Speed;

    public ParticleSystem Auspuff;

    public AudioManager aud;

    public float rotateSpeed = 290;

    public bool stuck = false;

    public float unStuckTime;

    public int QEdir = 2;

    // Start is called before the first frame update
    void Start()
    {
        Auspuff.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //Space
        if (Input.GetKeyDown(KeyCode.W))
        {
            Space = true;
            aud.Play("move");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            Space = false;
            aud.Stop("move");
        }

        if (Input.GetKey(KeyCode.Q)) QEdir = 0;
        if (Input.GetKey(KeyCode.E)) QEdir = 1;

        if (!Input.GetKey(KeyCode.E)) if (!Input.GetKey(KeyCode.Q)) QEdir = 2;
        if (Input.GetKey(KeyCode.Q)) if (Input.GetKey(KeyCode.E)) QEdir = 2;

        if (Space==false)
        {
            if (QEdir == 0) this.transform.RotateAround(this.transform.parent.parent.position, this.transform.parent.parent.forward, rotateSpeed * Time.deltaTime);
            if (QEdir == 1) this.transform.RotateAround(this.transform.parent.parent.position, this.transform.parent.parent.forward, -rotateSpeed * Time.deltaTime);

            if (Auspuff.isPlaying == true)
                Auspuff.Stop();
        }

        if (Space == true) if (stuck == false)
            {
                Vector3 dirToMove = transform.position - this.transform.parent.parent.position;
                this.transform.parent.parent.GetComponent<Rigidbody2D>().AddForce(dirToMove * -Speed);

                if (Auspuff.isPlaying == false)
                    Auspuff.Play();
            }
            else if (stuck == true)
            {
                Space = false;
                Invoke("unStuck", unStuckTime);
            }
    }

    private void unStuck()
    {
        stuck = false;
    }
}
