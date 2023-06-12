using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_boost : MonoBehaviour
{
    public bool active=true;
    public float active_timer_max=2;
    public float active_timer;
    public GameObject grab_object;
    public float Timer_max;
    public float Timer;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
    }
    private void Awake()
    {
        active = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (grab_object != null&&active==true)
        {
            Timer -= Time.deltaTime;
            if (Timer > 0) {
                if (!grab_object.GetComponent<player_room_boost_mode>().on_boost)
                {
                    grab_object.GetComponent<PlayerCharacter>().set_col_boost(true);
                    grab_object.GetComponent<player_room_boost_mode>().boost_pos = this.transform.position;
                    //if (Mathf.Sqrt(Mathf.Abs(dir.x)* Mathf.Abs(dir.y)) >0.1) { 

                    grab_object.transform.position = this.transform.position;
                }
                else
                {
                    grab_object.GetComponent<PlayerCharacter>().set_col_boost(false);

                    grab_object = null;

                    active_timer = active_timer_max;
                    active = false;
                    Timer = 0;
                }
               // }
                //else
               // {
                //    grab_object.GetComponent<Rigidbody2D>().AddForce(dir * force/2);
                //}
                    }
            else
            {
                this.gameObject.layer = 6;
                grab_object.GetComponent<player_room_boost_mode>().un_boost();
                grab_object.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                grab_object = null;
                active_timer = active_timer_max;
                active = false;
            }
        }
        if (active == false) {
            active_timer -= Time.deltaTime;
            Color color = this.GetComponent<SpriteRenderer>().color;
            color.a = 0.5f;
            this.GetComponent<SpriteRenderer>().color = color;
            if (active_timer < 0)
                active = true;
        }
        else
        {
            Color color = this.GetComponent<SpriteRenderer>().color;
            color.a = 1.0f;
            this.GetComponent<SpriteRenderer>().color = color;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&active==true)
        {
            grab_object = collision.gameObject;
            Timer = Timer_max;
            collision.gameObject.layer = 10;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerCharacter>().set_col_boost(false);
            
            grab_object = null;
            
            active_timer = active_timer_max;
            active = false;
            Timer = 0;
        }
    }
}
