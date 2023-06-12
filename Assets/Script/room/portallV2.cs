using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portallV2 : MonoBehaviour
{
    public GameObject connenct_room;
    public GameObject this_room;
    public bool move_check;
    public bool anim_check;
    GameObject element_frame;
    public Sprite[] room_element_image;
    // Start is called before the first frame update
    void Start()
    {
        element_frame = this.transform.GetChild(1).GetChild(0).gameObject;
        if (connenct_room.GetComponent<room>().room_element == 2)
        {
            set_frame_image(0);
        }
        else if (connenct_room.GetComponent<room>().room_element == 3)
        {
            set_frame_image(2);

        }
        else if (connenct_room.GetComponent<room>().room_element == 4)
        {
            set_frame_image(1);
        }
        GameObject a = Instantiate(Gamemanager.GM.portal_minimap_pos, this.transform);
        a.transform.position = this.transform.position;
    }
            public void animation_end_check()
    {
        anim_check = true;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (move_check && Gamemanager.GM.fade_out_complete)
        {
            if (connenct_room == null)
            {
                Destroy(this.gameObject);
            }
            connenct_room.SetActive(true);
            connenct_room.GetComponent<room>().on_player = true;
            connenct_room.GetComponent<room>().exit_portal.SetActive(true);
           this_room.SetActive(false);
            this_room.GetComponent<room>().on_player = false;
            GameObject p = Gamemanager.GM.Player_obj;
            Debug.Log(connenct_room.GetComponent<room>().exit_portal.transform.position);
            p.transform.position = connenct_room.GetComponent<room>().exit_portal.transform.position;
           
           

            move_check = false;
            connenct_room.GetComponent<room>().move_player();
        }
    }
    void set_frame_image(int i)
    {
        element_frame.GetComponent<SpriteRenderer>().sprite = room_element_image[i];
    }
    public void move_player()
    {

        Gamemanager.GM.fade_out();
        Gamemanager.GM.can_handle = false;
        move_check = true;
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
       /* if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                connenct_room.SetActive(true);
                connenct_room.GetComponent<room>().on_player = true;
                this_room.SetActive(false);
                this_room.GetComponent<room>().on_player = false;
                GameObject p = Gamemanager.GM.Player_obj;
                Debug.Log(connenct_room.GetComponent<room>().exit_portal.transform.position);
                p.transform.position = connenct_room.GetComponent<room>().exit_portal.transform.position;
         

            }
        }*/
    }
}
