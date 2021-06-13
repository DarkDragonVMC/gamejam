using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public Level[] levels;
    public int curArray;
    public CamController cam;
    public PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        curArray = 0;
        cam = GameObject.Find("Main Camera").GetComponent<CamController>();
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToScene(int array)
    {
        curArray += 1;

        if (array != -1) curArray = array;

        GameObject after;
        GameObject oldTrans = GameObject.Find("transition");

        Vector3 pos = GameObject.Find("transition").transform.position;
        float rot = GameObject.Find("transition").transform.rotation.z;
        rot += levels[curArray].rotoffset.z;

        pos.x += levels[curArray].offset.x;
        pos.y += levels[curArray].offset.y;

        after = Instantiate(levels[curArray].prefab, pos, Quaternion.identity);

        after.transform.rotation = new Quaternion(0, 0, rot, 0);

        GameObject.Find("Player").transform.position = GameObject.Find("SpawnPoint").transform.position;
        Destroy(GameObject.Find("SpawnPoint"));

        Destroy(oldTrans.transform.parent.gameObject);

        cam.StartCoroutine("MoveCam");

        pm.hooks = 3;
        pm.ripRope();

        return;
    }
}
