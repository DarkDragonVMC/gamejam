using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLine : MonoBehaviour
{

    private RaycastHit2D anchor;
    private PlayerMovement pm;
    private LineRenderer lr;

    public Material line;
    public Material lineRed;

    // Start is called before the first frame update
    void Start()
    {
        pm = transform.parent.GetComponent<PlayerMovement>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position;
        Vector3 mPos = Input.mousePosition;
        mPos = Camera.main.ScreenToWorldPoint(mPos);
        Vector2 dir = new Vector2(mPos.x - transform.position.x, mPos.y - transform.position.y);

        anchor = Physics2D.Raycast(pm.transform.position, dir, 100, pm.Mask);

        if(anchor.collider == null)
        {
            Vector3 infinit = transform.parent.position + new Vector3(dir.x, dir.y, 0) * 100;

            lr.material = lineRed;
            lr.SetPosition(0, transform.parent.position);
            lr.SetPosition(1, infinit);
            return;
        }

        lr.SetPosition(0, transform.parent.position);
        lr.SetPosition(1, anchor.point);

        if (Vector2.Distance(transform.parent.position, anchor.point) >= pm.maxDis) lr.material = lineRed;
        else lr.material = line;
    }
}
