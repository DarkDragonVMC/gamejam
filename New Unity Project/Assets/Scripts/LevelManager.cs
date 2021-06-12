using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public Level[] levels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToScene(int array)
    {
        Instantiate(levels[array].prefab, Vector3.zero, Quaternion.identity);
    }
}
