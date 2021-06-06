using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public IEnumerator Shake(float duration, float strength)
    {
        Vector3 originalPos = transform.localPosition;

        float timeAfterStart = 0.0f;

        while(timeAfterStart < duration)
        {
            float x = Random.Range(-1f, 1) * strength;
            float y = Random.Range(-1f, 1) * strength;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            timeAfterStart += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
