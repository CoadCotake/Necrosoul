using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvnet : MonoBehaviour
{
    public Animator Died;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            Player_status.p_status.DamgeTest(1000);
        }

        if(Player_status.p_status.get_hp()<=0&&Player_status.p_status.spawn_check)
        {
            //Debug.Log("Á×À½??????????");
            Died.gameObject.SetActive(true);
            Died.SetBool("Died", true);
        }
    }
}
