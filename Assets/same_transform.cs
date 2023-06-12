using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class same_transform : MonoBehaviour
{
    Transform t;
    // Start is called before the first frame update
    void Start()
    {
        t = this.transform.parent.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.position =t.transform.position;
        this.transform.rotation = t.transform.rotation;
    }
}
