using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed;
   public room r;
    public Vector2 center;
    public Vector2 size;
    public Vector2 this_pos;
    float height;
    float width;
    GameObject[] room_;
    float l;
    private void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;

    }

    private void LateUpdate()
    {
        /*room_ = GameObject.FindGameObjectsWithTag("room");
        for (int i = 0; i < room_.Length; i++) {
            if (room_[i].GetComponent<room>().on_player)
            {
                target = room_[i].transform;
            }
        }*/
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            //target = GameObject.FindGameObjectWithTag("Player").transform;
            target = GameObject.FindGameObjectWithTag("Player").transform.GetChild(3);
        }
        if (target != null)
        {
            room_ = GameObject.FindGameObjectsWithTag("room");
            for (int i = 0; i < room_.Length; i++)
            {
                if (room_[i].GetComponent<room>().on_player)
                {
                    r = room_[i].GetComponent<room>();
                    center = r.camera_point.transform.position;
                    size = r.size;
                }
            }
            this_pos = this.transform.position;
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
            
            //transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            float clampX=center.x;
            if (r != null)
            {
               
                if (!r.x_pin)
                {
                    float lx = size.x * 0.5f - width;
                    clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);
                }
                float clampY = center.y;
                if (!r.y_pin)
                {
                    float ly = size.y * 0.5f - height;
                    clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);
                }
                transform.position = new Vector3(clampX, clampY, -10f);
            }
            l = transform.position.x - this_pos.x;
            Gamemanager.GM.game_ev.when_camera_move(l) ;
            
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center,size);
    }
}
/*
 * 방 가운데 
 *방 크기가 다를 경우에
 * 플레이어캐릭터 카메라기중간
 * */