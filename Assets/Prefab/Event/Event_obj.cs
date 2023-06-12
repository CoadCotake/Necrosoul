using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_obj : MonoBehaviour
{

    public List<GameObject> Event_list=new List<GameObject>();
    public GameObject selected_event;
   GameObject selected_event_create;

    public void Event_start()
    {
        Gamemanager.GM.can_handle = false;
 
      selected_event_create = Instantiate(selected_event);
        selected_event_create.transform.SetParent(this.gameObject.transform);
    }
    private void Start()
    {
        if (selected_event == null)
        {
            int rand = Random.Range(0, Event_list.Count);
            selected_event = Event_list[rand];
        }
        GameObject a = Instantiate(Gamemanager.GM.event_minimap_pos, this.transform);
        a.transform.position = this.transform.position;
    }
    private void OnDisable()
    {
        var a = transform.parent.GetComponent<room>();
        a.event_clear = true;
    }
    void Update()
    {
        
    }
}
