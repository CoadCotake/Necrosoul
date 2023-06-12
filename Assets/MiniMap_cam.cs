using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MiniMap_cam : MonoBehaviour
{
    public Vector2 target;
    public float speed;
    room r;
    boss_stage b_s;
    static public Vector2 center;
    public Vector2 size;
    bool pos_check;
    float height;
    float width;
    GameObject[] room_;
    Camera c;
    private void Start()
    {
        c = this.GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        room_ = GameObject.FindGameObjectsWithTag("room");
        for (int i = 0; i < room_.Length; i++)
        {
            if (room_[i].GetComponent<room>() != null)
            {
                if (room_[i].GetComponent<room>().on_player)
                {

                    r = room_[i].GetComponent<room>();
                }
            }
            else
            {
                b_s = room_[i].GetComponent<boss_stage>();
            }
        }

       
        if (r != null)
        {
            this.transform.position = new Vector3(r.camera_point.transform.position.x,r.camera_point.transform.position.y,this.transform.position.z);
            if (r.size.x >= r.size.y) {
                c.orthographicSize = r.size.x*0.75f;
        }
            else
            {
                c.orthographicSize = r.size.y * 0.75f;
            }
        }
        if (b_s != null)
        {
            this.transform.position = new Vector3(b_s.camera_point.transform.position.x, b_s.camera_point.transform.position.y, this.transform.position.z);
            if (b_s.size.x >= b_s.size.y)
            {
                c.orthographicSize = b_s.size.x * 0.75f;
            }
            else
            {
                c.orthographicSize = b_s.size.y * 0.75f;
            }
        }
       

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

   
}
