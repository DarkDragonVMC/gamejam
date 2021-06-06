using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{

    public GameObject[] MapParts;
    public GameObject[] PartPosition = new GameObject[8];

    // Start is called before the first frame update
    void Start()
    {
        SpawnMap(PartPosition[0]);
        SpawnMap(PartPosition[1]);
        SpawnMap(PartPosition[2]);
        SpawnMap(PartPosition[3]);
        SpawnMap(PartPosition[4]);
        SpawnMap(PartPosition[5]);
        SpawnMap(PartPosition[6]);
        SpawnMap(PartPosition[7]);
    }

    void SpawnMap(GameObject pos)
    {
        GameObject objToSpawn = MapParts[Random.Range(0, MapParts.Length)];
        Instantiate(objToSpawn, pos.transform.position, Quaternion.identity);
    }
}
