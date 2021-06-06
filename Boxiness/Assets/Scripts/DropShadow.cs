using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DropShadow : MonoBehaviour
{

    public Color shadowColor;

    public Vector3 Offset = new Vector3(0.1f, -0.1f);
    GameObject dropshadow;

    // Start is called before the first frame update
    void Start()
    {
        dropshadow = new GameObject("Dropshadow");
        dropshadow.transform.parent = transform;

        dropshadow.transform.position = this.transform.position + Offset;
        dropshadow.transform.rotation = Quaternion.identity;
        dropshadow.transform.localScale = new Vector3(1, 1, 1);

        SpriteRenderer thisSpr = GetComponent<SpriteRenderer>();
        SpriteRenderer sr = dropshadow.AddComponent<SpriteRenderer>();
        sr.sprite = thisSpr.sprite;
        sr.color = shadowColor;

        //Sorting Layer
        sr.sortingLayerName = thisSpr.sortingLayerName;
        sr.sortingOrder = thisSpr.sortingOrder -1;
    }

    private void FixedUpdate()
    {
        dropshadow.transform.position = this.transform.position + Offset;
        dropshadow.transform.rotation = this.transform.rotation;
    }
}
