using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class map_ui : MonoBehaviour
{
    public Camera cam;
    public Camera mini_cam;
    public GameObject mapui;
    public room t_room;
    public room p_room;
    public Transform tp_pos;
    public GameObject p;
    public Vector3 pos;
    public Vector3 pos2;
    public GameObject PlayerMark;
    void Start()
    {
       // mini_cam = GameObject.Find("Minimap_cam").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!Gamemanager.GM.map_mode)
            {
                mapui.SetActive(true);
                mapui.transform.position = mini_cam.gameObject.transform.position;
                cam.gameObject.SetActive(true);

                cam.transform.position = mini_cam.gameObject.transform.position;
                mini_cam.gameObject.SetActive(false);
                Gamemanager.GM.map_mode = true;
            }
            else
            {
                Gamemanager.GM.map_mode = false;
                mapui.SetActive(false);
                mini_cam.gameObject.SetActive(true);
            }
        }
        if (Gamemanager.GM.map_mode == true)
        {
            if (Input.GetMouseButtonDown(0))//텔레포트 구현?
            {

                /* RaycastHit hit;
                 Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                 Physics.Raycast(ray, out hit);*/
                RaycastHit2D hit;
                pos = cam.ScreenToWorldPoint(Input.mousePosition);
                hit = Physics2D.Raycast(new Vector3(pos.x, pos.y, 100), pos, 0);
                Debug.Log(cam.ScreenToWorldPoint(Input.mousePosition));
                if (hit.collider != null)
                {
                    GameObject obj = hit.collider.gameObject;
                    if (obj.layer == 3)
                    {
                        Debug.Log("텔레포트");
                        tp_pos = obj.transform.GetChild(0);
                        t_room = obj.transform.parent.parent.parent.GetComponent<room>();
                        var room_ = this.gameObject.transform.GetChild(0).GetComponent<room_controller>().room;

                        for (int i = 0; i < room_.Count; i++)
                        {
                            if (room_[i].GetComponent<room>().on_player == true)
                            {
                                p_room = room_[i].GetComponent<room>();
                                break;
                            }
                        }
                        p = Gamemanager.GM.Player_create.transform.GetChild(0).gameObject;
                        p.transform.position = tp_pos.position;
                        t_room.on_player = true;
                        p_room.on_player = false;
                        Gamemanager.GM.map_mode = false;
                        mapui.SetActive(false);
                        mini_cam.gameObject.SetActive(true);
                    }
                    //광선이 오브젝트에 닿게 만들기(우선 처리)
                    Debug.Log(hit.collider.gameObject.layer);
                    Debug.Log(hit.collider.name);
                }
            }
            if (Input.GetMouseButton(0))
            {
                pos2 = cam.ScreenToWorldPoint(Input.mousePosition);
                // if (pos != pos2)
                // {
                cam.gameObject.transform.position += pos - pos2;
                //pos = pos2;
                // }
            }
        }
    }
}
