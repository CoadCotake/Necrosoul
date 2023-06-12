using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEvnet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Titleinput()
    {
        Debug.Log("타이틀 끝남");
        this.gameObject.GetComponent<Animator>().SetBool("End", true);
    }
}
