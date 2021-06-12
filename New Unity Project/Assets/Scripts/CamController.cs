using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{

    public float camSpeed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("MoveCam");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator MoveCam()
    {
        Debug.Log("Moving");

        float startTime = Time.time;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = GameObject.Find("Camchor").transform.position;

        Vector3 pos = startPosition;

        Invoke("DestroyAnchor", camSpeed);

        while (pos != endPosition)
        {
            float t = (Time.time - startTime) / camSpeed;
            pos = Vector3.Lerp(startPosition, endPosition, t);
            transform.position = pos;
            yield return null;
        }
    }

    private void DestroyAnchor()
    {
        Destroy(GameObject.Find("Camchor"));
    }
}
