using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_AI_02_Anim : MonoBehaviour
{
    E_AI_02 E_AI;
    void Start()
    {
        E_AI = this.transform.parent.GetComponent<E_AI_02>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void create_bullet()
    {
        E_AI.create_bullet();
    }
}
