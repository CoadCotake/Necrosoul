using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_07_range : MonoBehaviour
{
    public bool on_player;
    Quaternion q;
    public Transform E;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        q = this.transform.rotation;
        transform.position = E.position;
        if (E.transform.GetChild(1).rotation.y % 360 != 0&&this.transform.rotation.y%360==0)
        {
            this.transform.Rotate(0, 180, 0);
        }
        else if(E.transform.GetChild(1).rotation.y % 360 == 0 && this.transform.rotation.y % 360 != 0)
        {
            this.transform.Rotate(0, 180, 0);
        }
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            on_player = true;
            //ani.SetBool("attack_delay", false);
        }
    }
    private void OnTriggeExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            on_player = false;
           // ani.SetBool("attack_delay", true);
        }
    }
}
