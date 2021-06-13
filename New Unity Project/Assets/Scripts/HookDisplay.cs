using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDisplay : MonoBehaviour
{

    private PlayerMovement pm;
    public GameObject hook1;
    public GameObject sil1;
    public GameObject hook2;
    public GameObject sil2;
    public GameObject hook3;
    public GameObject sil3;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.hooks == 0)
        {
            hook1.SetActive(false);
            hook2.SetActive(false);
            hook3.SetActive(false);

            sil1.SetActive(true);
            sil2.SetActive(true);
            sil3.SetActive(true);
        }


        if (pm.hooks == 1)
        {
            hook1.SetActive(true);
            hook2.SetActive(false);
            hook3.SetActive(false);

            sil1.SetActive(false);
            sil2.SetActive(true);
            sil3.SetActive(true);
        }


        if (pm.hooks == 2)
        {
            hook1.SetActive(true);
            hook2.SetActive(true);
            hook3.SetActive(false);

            sil1.SetActive(false);
            sil2.SetActive(false);
            sil3.SetActive(true);
        }


        if (pm.hooks == 3)
        {
            hook1.SetActive(true);
            hook2.SetActive(true);
            hook3.SetActive(true);

            sil1.SetActive(false);
            sil2.SetActive(false);
            sil3.SetActive(false);
        }
    }
}
