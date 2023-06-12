using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bee_anim_event : MonoBehaviour
{
    Unit g;
    Animator e_ani;
    void Start()
    {
        g = this.transform.parent.GetComponent<Unit>();
    }
    void hitted_ani()
    {
        g.can_move = false;
    }
    void hitted_ani_end()
    {
        g.can_move = true ;
        g.transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
