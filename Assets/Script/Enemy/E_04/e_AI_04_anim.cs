using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e_AI_04_anim : MonoBehaviour
{
    E_04_AI e_04;
    // Start is called before the first frame update
    void Start()
    {
        e_04 = this.transform.parent.GetComponent<E_04_AI>();
    }
    public void hitted()
    {
        e_04.hitted_chk = true;
    }
    public void hitedt_end()
    {
        e_04.hitted_chk = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
   
}
