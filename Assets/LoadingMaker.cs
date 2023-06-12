using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingMaker : MonoBehaviour
{
    public GameObject l;
    public GameObject[] lm;
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lm = GameObject.FindGameObjectsWithTag("loading");
        if (lm.Length == 0)
        {
            Instantiate(l);

        }
        else if (lm.Length > 1)
        {
            Destroy(lm[lm.Length - 1]);
        }
    }
}
