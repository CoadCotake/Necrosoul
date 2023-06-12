using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dash_wall : MonoBehaviour
{
    PlayerCharacter p;
    Vector2 dir;
    bool on_player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        if (on_player&&!p.on_dash)
        {
            push_player();
        }
        
    }
    void push_player()
    {
        if (dir.x > 0)
        {
            p.player_hiited_force(this.transform.position, new Vector2(40, 5));
            p.player_hitted(1);
            p.untouchable_timer = Player_status.p_status.get_untouchable_time();


            // p.transform.Translate(Vector3.left * Player_status.p_status.get_speed()*2 * Time.deltaTime);
            //on_player = false;
        }
        else
        {
            p.player_hiited_force(this.transform.position, new Vector2(40, 5));
            p.player_hitted(1);
            p.untouchable_timer = Player_status.p_status.get_untouchable_time();
       

            //p.transform.Translate(Vector3.right * Player_status.p_status.get_speed()*2 * Time.deltaTime);
            //on_player = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (p == null)
            {
             
                p = collision.GetComponent<PlayerCharacter>();
                on_player = true;
          
                dir = this.transform.position - p.transform.position;

            }
            else
            {
                on_player = true;
         
                dir = this.transform.position - p.transform.position;
            }
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
          //  p.can_move = true;
         on_player = false;
            //Player_status.p_status.set_hp(Player_status.);
        }
    }
   
    //layer로 대시 보충하기
}
