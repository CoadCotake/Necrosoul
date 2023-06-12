using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_wave : MonoBehaviour
{
   public int i;
    void Start()
    {
        i = this.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        i = this.transform.childCount;
        if (i == 0)
        {
            this.transform.parent.GetComponent<boss_stage>().room_cleared = true;
        }
    }
}
