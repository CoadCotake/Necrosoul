using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsAni : MonoBehaviour
{
    static public bool ButAni_End = false;
    // Start is called before the first frame update
    void Start()
    {
        ButAni_End = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonAniEnd()      //애니메이션에 들어가있음
    {
        ButAni_End = true;
    }
}
