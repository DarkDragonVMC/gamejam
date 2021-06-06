using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    //Things to Spawn
    public GameObject Enemy;
    public GameObject Klett;

    public Vector2 EnemyRate;
    public Vector2 KlettRate;
    public float StartTime;

    public Vector2 SpawnField;

    public float DisToPlayer;

    //References
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

        //Start Spawning Loop
        StartCoroutine(Spawn(Enemy, StartTime, EnemyRate));
        StartCoroutine(Spawn(Klett, StartTime, KlettRate));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn(GameObject ObjToSpawn, float delayTime, Vector2 rate)
    {
        yield return new WaitForSeconds(delayTime);

        Vector3 spawnPos = new Vector3(Random.Range(-SpawnField.x, SpawnField.x), Random.Range(-SpawnField.y, SpawnField.y));

        //If too close to Player
        if(Vector3.Distance(spawnPos, Player.transform.position) < DisToPlayer)
        {
            StartCoroutine(Spawn(ObjToSpawn, 0, rate));
            yield break;
        }

        Instantiate(ObjToSpawn, spawnPos, Quaternion.identity);

        StartCoroutine(Spawn(ObjToSpawn, Random.Range(rate.x, rate.y), rate));
    }

}
