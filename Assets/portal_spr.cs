using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal_spr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void animation_end_check()
    {
        if (this.transform.parent.GetComponent<portallV2>()!=null)
        this.transform.parent.GetComponent<portallV2>().anim_check = true;
    }
}
