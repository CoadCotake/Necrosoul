using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_contents_controller : MonoBehaviour
{
    public GameObject[] spawn_contents;
    public GameObject[] normal_contents;
    public GameObject[] event_contents;
    public GameObject[] shop_contents;
    public GameObject[] endroom_contents;
    public GameObject[] rare_event_contents;
    public GameObject content;
    public room this_room;
    public bool check_room_contents;
    public List<GameObject> enemy = new List<GameObject>();//적이 확인되면 집어 넣기

    // Start is called before the first frame update
    void Start()
    {
        this_room = gameObject.GetComponentInParent<room>();
        //this_room = this.GetComponent<room>();
    }
    private void Awake()
    {
       /* if (!check_room_contents)
        {
            active_contents();
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        if (!check_room_contents)
        {
            active_contents();
        }
    }
    void active_contents()
    {
        int random;
        switch (this_room.room_element)
        {
            case 1:
                random = Random.Range(0, spawn_contents.Length);
               // Debug.Log("난수:" + random);
               // Debug.Log("길이:" + spawn_contents.Length);
                GameObject a = Instantiate(spawn_contents[random], this.transform.position, Quaternion.identity);
                a.transform.SetParent(this.transform);
                check_room_contents = true;
                content = a;
                break;
            case 2:
                random = Random.Range(0, normal_contents.Length);
                GameObject b = Instantiate(normal_contents[random], this.transform.position, Quaternion.identity);
                b.transform.SetParent(this.transform);
        
                check_room_contents = true;
                content = b;
                
                break;
            case 3:
                random = Random.Range(0, 1);
                if (random < 0.1f)
                {

                }
                else
                {
                    random = Random.Range(0, event_contents.Length);
                    GameObject c = Instantiate(event_contents[random], this.transform.position, Quaternion.identity);
                    c.transform.SetParent(this.transform);
                    content = c;
     
                    check_room_contents = true;
                }
                break;
            case 4:
                random = Random.Range(0, endroom_contents.Length);
                GameObject g = Instantiate(endroom_contents[random], this.transform.position, Quaternion.identity);
                g.transform.SetParent(this.transform);
                content = g;

                check_room_contents = true;
                break;
            case 6:
                {
                    random = Random.Range(0, shop_contents.Length);
                    GameObject c = Instantiate(shop_contents[random], this.transform.position, Quaternion.identity);
                    c.transform.SetParent(this.transform);
                    content = c;
     
                    check_room_contents = true;
                }
                break;
        }
        
    }
}
